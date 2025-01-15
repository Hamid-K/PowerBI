using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Mappers
{
	// Token: 0x0200015D RID: 349
	internal sealed class TypeMapper
	{
		// Token: 0x0600160A RID: 5642 RVA: 0x00039310 File Offset: 0x00037510
		public TypeMapper(MappingContext mappingContext)
		{
			this._mappingContext = mappingContext;
			this._knownTypes.AddRange(mappingContext.ModelConfiguration.ConfiguredTypes.Select((Type t) => t.Assembly()).Distinct<Assembly>().SelectMany((Assembly a) => from type in a.GetAccessibleTypes()
				where type.IsValidStructuralType()
				select type));
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x0600160B RID: 5643 RVA: 0x00039398 File Offset: 0x00037598
		public MappingContext MappingContext
		{
			get
			{
				return this._mappingContext;
			}
		}

		// Token: 0x0600160C RID: 5644 RVA: 0x000393A0 File Offset: 0x000375A0
		public EnumType MapEnumType(Type type)
		{
			EnumType enumType = TypeMapper.GetExistingEdmType<EnumType>(this._mappingContext.Model, type);
			if (enumType == null)
			{
				PrimitiveType primitiveType;
				if (!Enum.GetUnderlyingType(type).IsPrimitiveType(out primitiveType))
				{
					return null;
				}
				enumType = this._mappingContext.Model.AddEnumType(type.Name, this._mappingContext.ModelConfiguration.ModelNamespace);
				enumType.IsFlags = type.GetCustomAttributes(false).Any<FlagsAttribute>();
				enumType.SetClrType(type);
				enumType.UnderlyingType = primitiveType;
				foreach (string text in Enum.GetNames(type))
				{
					enumType.AddMember(new EnumMember(text, Convert.ChangeType(Enum.Parse(type, text), type.GetEnumUnderlyingType(), CultureInfo.InvariantCulture)));
				}
			}
			return enumType;
		}

		// Token: 0x0600160D RID: 5645 RVA: 0x00039460 File Offset: 0x00037660
		public ComplexType MapComplexType(Type type, bool discoverNested = false)
		{
			if (!type.IsValidStructuralType())
			{
				return null;
			}
			this._mappingContext.ConventionsConfiguration.ApplyModelConfiguration(type, this._mappingContext.ModelConfiguration);
			if (this._mappingContext.ModelConfiguration.IsIgnoredType(type) || (!discoverNested && !this._mappingContext.ModelConfiguration.IsComplexType(type)))
			{
				return null;
			}
			ComplexType complexType = TypeMapper.GetExistingEdmType<ComplexType>(this._mappingContext.Model, type);
			if (complexType == null)
			{
				complexType = this._mappingContext.Model.AddComplexType(type.Name, this._mappingContext.ModelConfiguration.ModelNamespace);
				Func<ComplexTypeConfiguration> complexTypeConfiguration = () => this._mappingContext.ModelConfiguration.ComplexType(type);
				this._mappingContext.ConventionsConfiguration.ApplyTypeConfiguration<ComplexTypeConfiguration>(type, complexTypeConfiguration, this._mappingContext.ModelConfiguration);
				this.MapStructuralElements<ComplexTypeConfiguration>(type, complexType.GetMetadataProperties(), delegate(PropertyMapper m, PropertyInfo p)
				{
					m.Map(p, complexType, complexTypeConfiguration);
				}, complexTypeConfiguration);
			}
			return complexType;
		}

		// Token: 0x0600160E RID: 5646 RVA: 0x000395D4 File Offset: 0x000377D4
		public EntityType MapEntityType(Type type)
		{
			if (!type.IsValidStructuralType() || this._mappingContext.ModelConfiguration.IsIgnoredType(type) || this._mappingContext.ModelConfiguration.IsComplexType(type))
			{
				return null;
			}
			EntityType entityType = TypeMapper.GetExistingEdmType<EntityType>(this._mappingContext.Model, type);
			if (entityType == null)
			{
				this._mappingContext.ConventionsConfiguration.ApplyModelConfiguration(type, this._mappingContext.ModelConfiguration);
				if (this._mappingContext.ModelConfiguration.IsIgnoredType(type) || this._mappingContext.ModelConfiguration.IsComplexType(type))
				{
					return null;
				}
				entityType = this._mappingContext.Model.AddEntityType(type.Name, this._mappingContext.ModelConfiguration.ModelNamespace);
				entityType.Abstract = type.IsAbstract();
				EntityType entityType2 = this._mappingContext.Model.GetEntityType(type.BaseType().Name);
				if (entityType2 == null)
				{
					this._mappingContext.Model.AddEntitySet(entityType.Name, entityType, null);
				}
				else if (entityType2 == entityType)
				{
					throw new NotSupportedException(Strings.SimpleNameCollision(type.FullName, type.BaseType().FullName, type.Name));
				}
				entityType.BaseType = entityType2;
				Func<EntityTypeConfiguration> entityTypeConfiguration = () => this._mappingContext.ModelConfiguration.Entity(type);
				this._mappingContext.ConventionsConfiguration.ApplyTypeConfiguration<EntityTypeConfiguration>(type, entityTypeConfiguration, this._mappingContext.ModelConfiguration);
				List<PropertyInfo> navigationProperties = new List<PropertyInfo>();
				this.MapStructuralElements<EntityTypeConfiguration>(type, entityType.GetMetadataProperties(), delegate(PropertyMapper m, PropertyInfo p)
				{
					if (!m.MapIfNotNavigationProperty(p, entityType, entityTypeConfiguration))
					{
						navigationProperties.Add(p);
					}
				}, entityTypeConfiguration);
				IEnumerable<PropertyInfo> enumerable = navigationProperties;
				if (this._mappingContext.ModelBuilderVersion.IsEF6OrHigher())
				{
					enumerable = enumerable.OrderBy((PropertyInfo p) => p.Name);
				}
				foreach (PropertyInfo propertyInfo in enumerable)
				{
					new NavigationPropertyMapper(this).Map(propertyInfo, entityType, entityTypeConfiguration);
				}
				if (entityType.BaseType != null)
				{
					this.LiftInheritedProperties(type, entityType);
				}
				this.MapDerivedTypes(type, entityType);
			}
			return entityType;
		}

		// Token: 0x0600160F RID: 5647 RVA: 0x00039944 File Offset: 0x00037B44
		private static T GetExistingEdmType<T>(EdmModel model, Type type) where T : EdmType
		{
			EdmType structuralOrEnumType = model.GetStructuralOrEnumType(type.Name);
			if (structuralOrEnumType != null && type != structuralOrEnumType.GetClrType())
			{
				throw new NotSupportedException(Strings.SimpleNameCollision(type.FullName, structuralOrEnumType.GetClrType().FullName, type.Name));
			}
			return structuralOrEnumType as T;
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x0003999C File Offset: 0x00037B9C
		private void MapStructuralElements<TStructuralTypeConfiguration>(Type type, ICollection<MetadataProperty> annotations, Action<PropertyMapper, PropertyInfo> propertyMappingAction, Func<TStructuralTypeConfiguration> structuralTypeConfiguration) where TStructuralTypeConfiguration : StructuralTypeConfiguration
		{
			annotations.SetClrType(type);
			new AttributeMapper(this._mappingContext.AttributeProvider).Map(type, annotations);
			PropertyMapper propertyMapper = new PropertyMapper(this);
			List<PropertyInfo> list = new PropertyFilter(this._mappingContext.ModelBuilderVersion).GetProperties(type, false, this._mappingContext.ModelConfiguration.GetConfiguredProperties(type), this._mappingContext.ModelConfiguration.StructuralTypes, false).ToList<PropertyInfo>();
			for (int i = 0; i < list.Count; i++)
			{
				PropertyInfo propertyInfo = list[i];
				this._mappingContext.ConventionsConfiguration.ApplyPropertyConfiguration(propertyInfo, this._mappingContext.ModelConfiguration);
				this._mappingContext.ConventionsConfiguration.ApplyPropertyTypeConfiguration<TStructuralTypeConfiguration>(propertyInfo, structuralTypeConfiguration, this._mappingContext.ModelConfiguration);
				if (!this._mappingContext.ModelConfiguration.IsIgnoredProperty(type, propertyInfo))
				{
					propertyMappingAction(propertyMapper, propertyInfo);
				}
			}
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x00039A7C File Offset: 0x00037C7C
		private void MapDerivedTypes(Type type, EntityType entityType)
		{
			if (type.IsSealed())
			{
				return;
			}
			if (!this._knownTypes.Contains(type))
			{
				this._knownTypes.AddRange(from t in type.Assembly().GetAccessibleTypes()
					where t.IsValidStructuralType()
					select t);
			}
			IEnumerable<Type> enumerable = this._knownTypes.Where((Type t) => t.BaseType() == type);
			if (this._mappingContext.ModelBuilderVersion.IsEF6OrHigher())
			{
				enumerable = enumerable.OrderBy((Type t) => t.FullName);
			}
			List<Type> list = enumerable.ToList<Type>();
			for (int i = 0; i < list.Count; i++)
			{
				Type type2 = list[i];
				EntityType entityType2 = this.MapEntityType(type2);
				if (entityType2 != null)
				{
					entityType2.BaseType = entityType;
					this.LiftDerivedType(type2, entityType2, entityType);
				}
			}
		}

		// Token: 0x06001612 RID: 5650 RVA: 0x00039B88 File Offset: 0x00037D88
		private void LiftDerivedType(Type derivedType, EntityType derivedEntityType, EntityType entityType)
		{
			this._mappingContext.Model.ReplaceEntitySet(derivedEntityType, this._mappingContext.Model.GetEntitySet(entityType));
			this.LiftInheritedProperties(derivedType, derivedEntityType);
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x00039BB4 File Offset: 0x00037DB4
		private void LiftInheritedProperties(Type type, EntityType entityType)
		{
			EntityTypeConfiguration entityTypeConfiguration = this._mappingContext.ModelConfiguration.GetStructuralTypeConfiguration(type) as EntityTypeConfiguration;
			if (entityTypeConfiguration != null)
			{
				entityTypeConfiguration.ClearKey();
				using (IEnumerator<PropertyInfo> enumerator = type.BaseType().GetInstanceProperties().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						PropertyInfo property = enumerator.Current;
						if (!this._mappingContext.AttributeProvider.GetAttributes(property).OfType<NotMappedAttribute>().Any<NotMappedAttribute>() && entityTypeConfiguration.IgnoredProperties.Any((PropertyInfo p) => p.IsSameAs(property)))
						{
							throw Error.CannotIgnoreMappedBaseProperty(property.Name, type, property.DeclaringType);
						}
					}
				}
			}
			List<EdmMember> list = entityType.DeclaredMembers.ToList<EdmMember>();
			HashSet<PropertyInfo> hashSet = new HashSet<PropertyInfo>(new PropertyFilter(this._mappingContext.ModelBuilderVersion).GetProperties(type, true, this._mappingContext.ModelConfiguration.GetConfiguredProperties(type), this._mappingContext.ModelConfiguration.StructuralTypes, false));
			foreach (EdmMember edmMember in list)
			{
				PropertyInfo clrPropertyInfo = edmMember.GetClrPropertyInfo();
				if (!hashSet.Contains(clrPropertyInfo))
				{
					NavigationProperty navigationProperty = edmMember as NavigationProperty;
					if (navigationProperty != null)
					{
						this._mappingContext.Model.RemoveAssociationType(navigationProperty.Association);
					}
					entityType.RemoveMember(edmMember);
				}
			}
		}

		// Token: 0x040009FE RID: 2558
		private readonly MappingContext _mappingContext;

		// Token: 0x040009FF RID: 2559
		private readonly List<Type> _knownTypes = new List<Type>();
	}
}
