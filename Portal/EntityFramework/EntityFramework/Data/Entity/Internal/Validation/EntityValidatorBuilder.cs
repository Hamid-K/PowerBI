using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.Internal.Validation
{
	// Token: 0x02000133 RID: 307
	internal class EntityValidatorBuilder
	{
		// Token: 0x060014B9 RID: 5305 RVA: 0x00036204 File Offset: 0x00034404
		public EntityValidatorBuilder(AttributeProvider attributeProvider)
		{
			this._attributeProvider = attributeProvider;
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x00036214 File Offset: 0x00034414
		public virtual EntityValidator BuildEntityValidator(InternalEntityEntry entityEntry)
		{
			return this.BuildTypeValidator<EntityValidator>(entityEntry.EntityType, entityEntry.EdmEntityType.Properties, entityEntry.EdmEntityType.NavigationProperties, (IEnumerable<PropertyValidator> propertyValidators, IEnumerable<IValidator> typeLevelValidators) => new EntityValidator(propertyValidators, typeLevelValidators));
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x00036262 File Offset: 0x00034462
		protected virtual ComplexTypeValidator BuildComplexTypeValidator(Type clrType, ComplexType complexType)
		{
			return this.BuildTypeValidator<ComplexTypeValidator>(clrType, complexType.Properties, Enumerable.Empty<NavigationProperty>(), (IEnumerable<PropertyValidator> propertyValidators, IEnumerable<IValidator> typeLevelValidators) => new ComplexTypeValidator(propertyValidators, typeLevelValidators));
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x00036298 File Offset: 0x00034498
		private T BuildTypeValidator<T>(Type clrType, IEnumerable<EdmProperty> edmProperties, IEnumerable<NavigationProperty> navigationProperties, Func<IEnumerable<PropertyValidator>, IEnumerable<IValidator>, T> validatorFactoryFunc) where T : TypeValidator
		{
			IList<PropertyValidator> list = this.BuildValidatorsForProperties(this.GetPublicInstanceProperties(clrType), edmProperties, navigationProperties);
			IEnumerable<Attribute> attributes = this._attributeProvider.GetAttributes(clrType);
			IList<IValidator> list2 = this.BuildValidationAttributeValidators(attributes);
			if (typeof(IValidatableObject).IsAssignableFrom(clrType))
			{
				list2.Add(new ValidatableObjectValidator(attributes.OfType<DisplayAttribute>().SingleOrDefault<DisplayAttribute>()));
			}
			if (!list.Any<PropertyValidator>() && !list2.Any<IValidator>())
			{
				return default(T);
			}
			return validatorFactoryFunc(list, list2);
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x00036318 File Offset: 0x00034518
		protected virtual IList<PropertyValidator> BuildValidatorsForProperties(IEnumerable<PropertyInfo> clrProperties, IEnumerable<EdmProperty> edmProperties, IEnumerable<NavigationProperty> navigationProperties)
		{
			List<PropertyValidator> list = new List<PropertyValidator>();
			using (IEnumerator<PropertyInfo> enumerator = clrProperties.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					PropertyInfo property = enumerator.Current;
					EdmProperty edmProperty = edmProperties.Where((EdmProperty p) => p.Name == property.Name).SingleOrDefault<EdmProperty>();
					PropertyValidator propertyValidator;
					if (edmProperty != null)
					{
						IEnumerable<ReferentialConstraint> enumerable = from navigationProperty in navigationProperties
							let associationType = navigationProperty.RelationshipType as AssociationType
							where associationType != null
							from constraint in associationType.ReferentialConstraints
							where constraint.ToProperties.Contains(edmProperty)
							select constraint;
						propertyValidator = this.BuildPropertyValidator(property, edmProperty, !enumerable.Any<ReferentialConstraint>());
					}
					else
					{
						propertyValidator = this.BuildPropertyValidator(property);
					}
					if (propertyValidator != null)
					{
						list.Add(propertyValidator);
					}
				}
			}
			return list;
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x000364AC File Offset: 0x000346AC
		protected virtual PropertyValidator BuildPropertyValidator(PropertyInfo clrProperty, EdmProperty edmProperty, bool buildFacetValidators)
		{
			List<IValidator> list = new List<IValidator>();
			IEnumerable<Attribute> attributes = this._attributeProvider.GetAttributes(clrProperty);
			list.AddRange(this.BuildValidationAttributeValidators(attributes));
			if (edmProperty.TypeUsage.EdmType.BuiltInTypeKind == BuiltInTypeKind.ComplexType)
			{
				ComplexType complexType = (ComplexType)edmProperty.TypeUsage.EdmType;
				ComplexTypeValidator complexTypeValidator = this.BuildComplexTypeValidator(clrProperty.PropertyType, complexType);
				if (!list.Any<IValidator>() && complexTypeValidator == null)
				{
					return null;
				}
				return new ComplexPropertyValidator(clrProperty.Name, list, complexTypeValidator);
			}
			else
			{
				if (buildFacetValidators)
				{
					list.AddRange(this.BuildFacetValidators(clrProperty, edmProperty, attributes));
				}
				if (!list.Any<IValidator>())
				{
					return null;
				}
				return new PropertyValidator(clrProperty.Name, list);
			}
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x00036550 File Offset: 0x00034750
		protected virtual PropertyValidator BuildPropertyValidator(PropertyInfo clrProperty)
		{
			IList<IValidator> list = this.BuildValidationAttributeValidators(this._attributeProvider.GetAttributes(clrProperty));
			if (list.Count <= 0)
			{
				return null;
			}
			return new PropertyValidator(clrProperty.Name, list);
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x00036588 File Offset: 0x00034788
		protected virtual IList<IValidator> BuildValidationAttributeValidators(IEnumerable<Attribute> attributes)
		{
			return (from validationAttribute in attributes
				where validationAttribute is ValidationAttribute
				select new ValidationAttributeValidator((ValidationAttribute)validationAttribute, attributes.OfType<DisplayAttribute>().SingleOrDefault<DisplayAttribute>())).ToList<IValidator>();
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x000365E2 File Offset: 0x000347E2
		protected virtual IEnumerable<PropertyInfo> GetPublicInstanceProperties(Type type)
		{
			return from p in type.GetInstanceProperties()
				where p.IsPublic() && p.GetIndexParameters().Length == 0 && p.Getter() != null
				select p;
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x00036610 File Offset: 0x00034810
		protected virtual IEnumerable<IValidator> BuildFacetValidators(PropertyInfo clrProperty, EdmMember edmProperty, IEnumerable<Attribute> existingAttributes)
		{
			List<ValidationAttribute> list = new List<ValidationAttribute>();
			MetadataProperty metadataProperty;
			edmProperty.MetadataProperties.TryGetValue("http://schemas.microsoft.com/ado/2009/02/edm/annotation:StoreGeneratedPattern", false, out metadataProperty);
			bool flag = metadataProperty != null && metadataProperty.Value != null;
			Facet facet;
			edmProperty.TypeUsage.Facets.TryGetValue("Nullable", false, out facet);
			if (facet != null && facet.Value != null && !(bool)facet.Value && !flag && clrProperty.PropertyType.IsNullable())
			{
				if (!existingAttributes.Any((Attribute a) => a is RequiredAttribute))
				{
					list.Add(new RequiredAttribute
					{
						AllowEmptyStrings = true
					});
				}
			}
			Facet facet2;
			edmProperty.TypeUsage.Facets.TryGetValue("MaxLength", false, out facet2);
			if (facet2 != null && facet2.Value != null && facet2.Value is int)
			{
				if (!existingAttributes.Any((Attribute a) => a is MaxLengthAttribute))
				{
					if (!existingAttributes.Any((Attribute a) => a is StringLengthAttribute))
					{
						list.Add(new MaxLengthAttribute((int)facet2.Value));
					}
				}
			}
			return list.Select((ValidationAttribute attribute) => new ValidationAttributeValidator(attribute, existingAttributes.OfType<DisplayAttribute>().SingleOrDefault<DisplayAttribute>()));
		}

		// Token: 0x040009BB RID: 2491
		private readonly AttributeProvider _attributeProvider;
	}
}
