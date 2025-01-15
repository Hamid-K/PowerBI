using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNet.OData.Query;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x0200012D RID: 301
	internal class EdmTypeBuilder
	{
		// Token: 0x06000A58 RID: 2648 RVA: 0x00029D20 File Offset: 0x00027F20
		internal EdmTypeBuilder(IEnumerable<IEdmTypeConfiguration> configurations)
		{
			this._configurations = configurations.ToList<IEdmTypeConfiguration>();
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x00029D8C File Offset: 0x00027F8C
		private Dictionary<Type, IEdmType> GetEdmTypes()
		{
			this._types.Clear();
			this._properties.Clear();
			this._members.Clear();
			this._openTypes.Clear();
			foreach (IEdmTypeConfiguration edmTypeConfiguration in this._configurations)
			{
				this.CreateEdmTypeHeader(edmTypeConfiguration);
			}
			foreach (IEdmTypeConfiguration edmTypeConfiguration2 in this._configurations)
			{
				this.CreateEdmTypeBody(edmTypeConfiguration2);
			}
			foreach (StructuralTypeConfiguration structuralTypeConfiguration in this._configurations.OfType<StructuralTypeConfiguration>())
			{
				this.CreateNavigationProperty(structuralTypeConfiguration);
			}
			return this._types;
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00029E98 File Offset: 0x00028098
		private void CreateEdmTypeHeader(IEdmTypeConfiguration config)
		{
			IEdmType edmType = this.GetEdmType(config.ClrType);
			if (edmType == null)
			{
				if (config.Kind == EdmTypeKind.Complex)
				{
					ComplexTypeConfiguration complexTypeConfiguration = (ComplexTypeConfiguration)config;
					IEdmComplexType edmComplexType = null;
					if (complexTypeConfiguration.BaseType != null)
					{
						this.CreateEdmTypeHeader(complexTypeConfiguration.BaseType);
						edmComplexType = this.GetEdmType(complexTypeConfiguration.BaseType.ClrType) as IEdmComplexType;
					}
					EdmComplexType edmComplexType2 = new EdmComplexType(config.Namespace, config.Name, edmComplexType, complexTypeConfiguration.IsAbstract.GetValueOrDefault(), complexTypeConfiguration.IsOpen);
					this._types.Add(config.ClrType, edmComplexType2);
					if (complexTypeConfiguration.IsOpen)
					{
						this._openTypes.Add(edmComplexType2, complexTypeConfiguration.DynamicPropertyDictionary);
					}
					edmType = edmComplexType2;
				}
				else if (config.Kind == EdmTypeKind.Entity)
				{
					EntityTypeConfiguration entityTypeConfiguration = config as EntityTypeConfiguration;
					IEdmEntityType edmEntityType = null;
					if (entityTypeConfiguration.BaseType != null)
					{
						this.CreateEdmTypeHeader(entityTypeConfiguration.BaseType);
						edmEntityType = this.GetEdmType(entityTypeConfiguration.BaseType.ClrType) as IEdmEntityType;
					}
					EdmEntityType edmEntityType2 = new EdmEntityType(config.Namespace, config.Name, edmEntityType, entityTypeConfiguration.IsAbstract.GetValueOrDefault(), entityTypeConfiguration.IsOpen, entityTypeConfiguration.HasStream);
					this._types.Add(config.ClrType, edmEntityType2);
					if (entityTypeConfiguration.IsOpen)
					{
						this._openTypes.Add(edmEntityType2, entityTypeConfiguration.DynamicPropertyDictionary);
					}
					edmType = edmEntityType2;
				}
				else
				{
					EnumTypeConfiguration enumTypeConfiguration = config as EnumTypeConfiguration;
					this._types.Add(enumTypeConfiguration.ClrType, new EdmEnumType(enumTypeConfiguration.Namespace, enumTypeConfiguration.Name, EdmTypeBuilder.GetTypeKind(enumTypeConfiguration.UnderlyingType), enumTypeConfiguration.IsFlags));
				}
			}
			IEdmStructuredType edmStructuredType = edmType as IEdmStructuredType;
			StructuralTypeConfiguration structuralTypeConfiguration = config as StructuralTypeConfiguration;
			if (edmStructuredType != null && structuralTypeConfiguration != null && !this._structuredTypeQuerySettings.ContainsKey(edmStructuredType) && structuralTypeConfiguration.QueryConfiguration.ModelBoundQuerySettings != null)
			{
				this._structuredTypeQuerySettings.Add(edmStructuredType, structuralTypeConfiguration.QueryConfiguration.ModelBoundQuerySettings);
			}
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x0002A090 File Offset: 0x00028290
		private void CreateEdmTypeBody(IEdmTypeConfiguration config)
		{
			IEdmType edmType = this.GetEdmType(config.ClrType);
			if (edmType.TypeKind == EdmTypeKind.Complex)
			{
				this.CreateComplexTypeBody((EdmComplexType)edmType, (ComplexTypeConfiguration)config);
				return;
			}
			if (edmType.TypeKind == EdmTypeKind.Entity)
			{
				this.CreateEntityTypeBody((EdmEntityType)edmType, (EntityTypeConfiguration)config);
				return;
			}
			this.CreateEnumTypeBody((EdmEnumType)edmType, (EnumTypeConfiguration)config);
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x0002A0F4 File Offset: 0x000282F4
		private static IEdmTypeReference AddPrecisionConfigInPrimitiveTypeReference(PrecisionPropertyConfiguration precisionProperty, IEdmTypeReference primitiveTypeReference)
		{
			if (primitiveTypeReference is EdmTemporalTypeReference && precisionProperty.Precision != null)
			{
				return new EdmTemporalTypeReference((IEdmPrimitiveType)primitiveTypeReference.Definition, primitiveTypeReference.IsNullable, precisionProperty.Precision);
			}
			return primitiveTypeReference;
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x0002A138 File Offset: 0x00028338
		private static IEdmTypeReference AddLengthConfigInPrimitiveTypeReference(LengthPropertyConfiguration lengthProperty, IEdmTypeReference primitiveTypeReference)
		{
			if (lengthProperty.MaxLength != null)
			{
				if (primitiveTypeReference is EdmStringTypeReference)
				{
					return new EdmStringTypeReference((IEdmPrimitiveType)primitiveTypeReference.Definition, primitiveTypeReference.IsNullable, false, lengthProperty.MaxLength, new bool?(true));
				}
				if (primitiveTypeReference is EdmBinaryTypeReference)
				{
					return new EdmBinaryTypeReference((IEdmPrimitiveType)primitiveTypeReference.Definition, primitiveTypeReference.IsNullable, false, lengthProperty.MaxLength);
				}
			}
			return primitiveTypeReference;
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x0002A1A8 File Offset: 0x000283A8
		private void CreateStructuralTypeBody(EdmStructuredType type, StructuralTypeConfiguration config)
		{
			foreach (PropertyConfiguration propertyConfiguration in config.Properties)
			{
				IEdmProperty edmProperty = null;
				switch (propertyConfiguration.Kind)
				{
				case PropertyKind.Primitive:
				{
					PrimitivePropertyConfiguration primitivePropertyConfiguration = (PrimitivePropertyConfiguration)propertyConfiguration;
					EdmPrimitiveTypeKind edmPrimitiveTypeKind = primitivePropertyConfiguration.TargetEdmTypeKind ?? EdmTypeBuilder.GetTypeKind(primitivePropertyConfiguration.PropertyInfo.PropertyType);
					IEdmTypeReference edmTypeReference = EdmCoreModel.Instance.GetPrimitive(edmPrimitiveTypeKind, primitivePropertyConfiguration.OptionalProperty);
					if (edmPrimitiveTypeKind == EdmPrimitiveTypeKind.Decimal)
					{
						DecimalPropertyConfiguration decimalPropertyConfiguration = primitivePropertyConfiguration as DecimalPropertyConfiguration;
						if (decimalPropertyConfiguration.Precision != null || decimalPropertyConfiguration.Scale != null)
						{
							edmTypeReference = new EdmDecimalTypeReference((IEdmPrimitiveType)edmTypeReference.Definition, edmTypeReference.IsNullable, decimalPropertyConfiguration.Precision, (decimalPropertyConfiguration.Scale != null) ? decimalPropertyConfiguration.Scale : new int?(0));
						}
					}
					else if (EdmLibHelpers.HasPrecision(edmPrimitiveTypeKind))
					{
						edmTypeReference = EdmTypeBuilder.AddPrecisionConfigInPrimitiveTypeReference(primitivePropertyConfiguration as PrecisionPropertyConfiguration, edmTypeReference);
					}
					else if (EdmLibHelpers.HasLength(edmPrimitiveTypeKind))
					{
						edmTypeReference = EdmTypeBuilder.AddLengthConfigInPrimitiveTypeReference(primitivePropertyConfiguration as LengthPropertyConfiguration, edmTypeReference);
					}
					edmProperty = type.AddStructuralProperty(primitivePropertyConfiguration.Name, edmTypeReference, primitivePropertyConfiguration.DefaultValueString);
					break;
				}
				case PropertyKind.Complex:
				{
					ComplexPropertyConfiguration complexPropertyConfiguration = propertyConfiguration as ComplexPropertyConfiguration;
					IEdmComplexType edmComplexType = this.GetEdmType(complexPropertyConfiguration.RelatedClrType) as IEdmComplexType;
					edmProperty = type.AddStructuralProperty(complexPropertyConfiguration.Name, new EdmComplexTypeReference(edmComplexType, complexPropertyConfiguration.OptionalProperty));
					break;
				}
				case PropertyKind.Collection:
					edmProperty = this.CreateStructuralTypeCollectionPropertyBody(type, (CollectionPropertyConfiguration)propertyConfiguration);
					break;
				case PropertyKind.Enum:
					edmProperty = this.CreateStructuralTypeEnumPropertyBody(type, (EnumPropertyConfiguration)propertyConfiguration);
					break;
				}
				if (edmProperty != null)
				{
					if (propertyConfiguration.PropertyInfo != null)
					{
						this._properties[propertyConfiguration.PropertyInfo] = edmProperty;
					}
					if (propertyConfiguration.IsRestricted)
					{
						this._propertiesRestrictions[edmProperty] = new QueryableRestrictions(propertyConfiguration);
					}
					if (propertyConfiguration.QueryConfiguration.ModelBoundQuerySettings != null)
					{
						this._propertiesQuerySettings.Add(edmProperty, propertyConfiguration.QueryConfiguration.ModelBoundQuerySettings);
					}
				}
			}
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x0002A3E8 File Offset: 0x000285E8
		private IEdmProperty CreateStructuralTypeCollectionPropertyBody(EdmStructuredType type, CollectionPropertyConfiguration collectionProperty)
		{
			Type underlyingTypeOrSelf = TypeHelper.GetUnderlyingTypeOrSelf(collectionProperty.ElementType);
			IEdmTypeReference edmTypeReference;
			if (TypeHelper.IsEnum(underlyingTypeOrSelf))
			{
				IEdmType edmType = this.GetEdmType(underlyingTypeOrSelf);
				if (edmType == null)
				{
					throw Error.InvalidOperation(SRResources.EnumTypeDoesNotExist, new object[] { underlyingTypeOrSelf.Name });
				}
				IEdmEnumType edmEnumType = (IEdmEnumType)edmType;
				bool flag = collectionProperty.ElementType != underlyingTypeOrSelf;
				edmTypeReference = new EdmEnumTypeReference(edmEnumType, flag);
			}
			else
			{
				IEdmType edmType2 = this.GetEdmType(collectionProperty.ElementType);
				if (edmType2 != null)
				{
					edmTypeReference = new EdmComplexTypeReference(edmType2 as IEdmComplexType, collectionProperty.OptionalProperty);
				}
				else
				{
					edmTypeReference = EdmLibHelpers.GetEdmPrimitiveTypeReferenceOrNull(collectionProperty.ElementType);
				}
			}
			return type.AddStructuralProperty(collectionProperty.Name, new EdmCollectionTypeReference(new EdmCollectionType(edmTypeReference)));
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0002A498 File Offset: 0x00028698
		private IEdmProperty CreateStructuralTypeEnumPropertyBody(EdmStructuredType type, EnumPropertyConfiguration enumProperty)
		{
			Type underlyingTypeOrSelf = TypeHelper.GetUnderlyingTypeOrSelf(enumProperty.RelatedClrType);
			IEdmType edmType = this.GetEdmType(underlyingTypeOrSelf);
			if (edmType == null)
			{
				throw Error.InvalidOperation(SRResources.EnumTypeDoesNotExist, new object[] { underlyingTypeOrSelf.Name });
			}
			IEdmTypeReference edmTypeReference = new EdmEnumTypeReference((IEdmEnumType)edmType, enumProperty.OptionalProperty);
			return type.AddStructuralProperty(enumProperty.Name, edmTypeReference, enumProperty.DefaultValueString);
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0002A4FB File Offset: 0x000286FB
		private void CreateComplexTypeBody(EdmComplexType type, ComplexTypeConfiguration config)
		{
			this.CreateStructuralTypeBody(type, config);
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0002A508 File Offset: 0x00028708
		private void CreateEntityTypeBody(EdmEntityType type, EntityTypeConfiguration config)
		{
			this.CreateStructuralTypeBody(type, config);
			IEnumerable<IEdmStructuralProperty> enumerable = from p in config.Keys.Concat(config.EnumKeys)
				orderby p.Order, p.Name
				select type.DeclaredProperties.OfType<IEdmStructuralProperty>().First((IEdmStructuralProperty dp) => dp.Name == p.Name);
			type.AddKeys(enumerable);
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0002A5A8 File Offset: 0x000287A8
		private void CreateNavigationProperty(StructuralTypeConfiguration config)
		{
			EdmStructuredType edmStructuredType = (EdmStructuredType)this.GetEdmType(config.ClrType);
			using (IEnumerator<NavigationPropertyConfiguration> enumerator = config.NavigationProperties.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					NavigationPropertyConfiguration navProp = enumerator.Current;
					Func<NavigationPropertyConfiguration, EdmNavigationPropertyInfo> func = delegate(NavigationPropertyConfiguration nav)
					{
						EdmNavigationPropertyInfo edmNavigationPropertyInfo = new EdmNavigationPropertyInfo
						{
							Name = nav.Name,
							TargetMultiplicity = nav.Multiplicity,
							Target = (this.GetEdmType(nav.RelatedClrType) as IEdmEntityType),
							ContainsTarget = nav.ContainsTarget,
							OnDelete = nav.OnDeleteAction
						};
						if (nav.PrincipalProperties.Any<PropertyInfo>())
						{
							edmNavigationPropertyInfo.PrincipalProperties = this.GetDeclaringPropertyInfo(nav.PrincipalProperties);
						}
						if (nav.DependentProperties.Any<PropertyInfo>())
						{
							edmNavigationPropertyInfo.DependentProperties = this.GetDeclaringPropertyInfo(nav.DependentProperties);
						}
						return edmNavigationPropertyInfo;
					};
					EdmNavigationPropertyInfo navInfo = func(navProp);
					Dictionary<IEdmProperty, NavigationPropertyConfiguration> dictionary = new Dictionary<IEdmProperty, NavigationPropertyConfiguration>();
					EdmEntityType edmEntityType = edmStructuredType as EdmEntityType;
					if (edmEntityType != null && navProp.Partner != null)
					{
						EdmNavigationProperty edmNavigationProperty = edmEntityType.AddBidirectionalNavigation(navInfo, func(navProp.Partner));
						IEdmProperty edmProperty = (navInfo.Target as EdmEntityType).Properties().Single((IEdmProperty p) => p.Name == navProp.Partner.Name);
						dictionary.Add(edmNavigationProperty, navProp);
						dictionary.Add(edmProperty, navProp.Partner);
					}
					else if (!(config.ModelBuilder.GetTypeConfigurationOrNull(navProp.RelatedClrType) as StructuralTypeConfiguration).NavigationProperties.Any((NavigationPropertyConfiguration p) => p.Partner != null && p.Partner.Name == navInfo.Name))
					{
						EdmNavigationProperty edmNavigationProperty2 = edmStructuredType.AddUnidirectionalNavigation(navInfo);
						dictionary.Add(edmNavigationProperty2, navProp);
					}
					foreach (KeyValuePair<IEdmProperty, NavigationPropertyConfiguration> keyValuePair in dictionary)
					{
						IEdmProperty key = keyValuePair.Key;
						NavigationPropertyConfiguration value = keyValuePair.Value;
						if (value.PropertyInfo != null)
						{
							this._properties[value.PropertyInfo] = key;
						}
						if (value.IsRestricted)
						{
							this._propertiesRestrictions[key] = new QueryableRestrictions(value);
						}
						if (value.QueryConfiguration.ModelBoundQuerySettings != null)
						{
							this._propertiesQuerySettings.Add(key, value.QueryConfiguration.ModelBoundQuerySettings);
						}
					}
				}
			}
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0002A7E8 File Offset: 0x000289E8
		private IList<IEdmStructuralProperty> GetDeclaringPropertyInfo(IEnumerable<PropertyInfo> propertyInfos)
		{
			IList<IEdmProperty> list = new List<IEdmProperty>();
			foreach (PropertyInfo propertyInfo in propertyInfos)
			{
				IEdmProperty edmProperty;
				if (this._properties.TryGetValue(propertyInfo, out edmProperty))
				{
					list.Add(edmProperty);
				}
				else
				{
					Type type = TypeHelper.GetBaseType(TypeHelper.GetReflectedType(propertyInfo));
					while (type != null)
					{
						PropertyInfo property = type.GetProperty(propertyInfo.Name);
						if (this._properties.TryGetValue(property, out edmProperty))
						{
							list.Add(edmProperty);
							break;
						}
						type = TypeHelper.GetBaseType(type);
					}
				}
			}
			return list.OfType<IEdmStructuralProperty>().ToList<IEdmStructuralProperty>();
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0002A8A0 File Offset: 0x00028AA0
		private void CreateEnumTypeBody(EdmEnumType type, EnumTypeConfiguration config)
		{
			foreach (EnumMemberConfiguration enumMemberConfiguration in config.Members)
			{
				long num;
				try
				{
					num = Convert.ToInt64(enumMemberConfiguration.MemberInfo, CultureInfo.InvariantCulture);
				}
				catch
				{
					throw Error.Argument("value", SRResources.EnumValueCannotBeLong, new object[] { Enum.GetName(enumMemberConfiguration.MemberInfo.GetType(), enumMemberConfiguration.MemberInfo) });
				}
				EdmEnumMember edmEnumMember = new EdmEnumMember(type, enumMemberConfiguration.Name, new EdmEnumMemberValue(num));
				type.AddMember(edmEnumMember);
				this._members[enumMemberConfiguration.MemberInfo] = edmEnumMember;
			}
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0002A964 File Offset: 0x00028B64
		private IEdmType GetEdmType(Type clrType)
		{
			IEdmType edmType;
			this._types.TryGetValue(clrType, out edmType);
			return edmType;
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0002A984 File Offset: 0x00028B84
		public static EdmTypeMap GetTypesAndProperties(IEnumerable<IEdmTypeConfiguration> configurations)
		{
			if (configurations == null)
			{
				throw Error.ArgumentNull("configurations");
			}
			EdmTypeBuilder edmTypeBuilder = new EdmTypeBuilder(configurations);
			return new EdmTypeMap(edmTypeBuilder.GetEdmTypes(), edmTypeBuilder._properties, edmTypeBuilder._propertiesRestrictions, edmTypeBuilder._propertiesQuerySettings, edmTypeBuilder._structuredTypeQuerySettings, edmTypeBuilder._members, edmTypeBuilder._openTypes);
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x0002A9D8 File Offset: 0x00028BD8
		public static EdmPrimitiveTypeKind GetTypeKind(Type clrType)
		{
			IEdmPrimitiveType edmPrimitiveTypeOrNull = EdmLibHelpers.GetEdmPrimitiveTypeOrNull(clrType);
			if (edmPrimitiveTypeOrNull == null)
			{
				throw Error.Argument("clrType", SRResources.MustBePrimitiveType, new object[] { clrType.FullName });
			}
			return edmPrimitiveTypeOrNull.PrimitiveKind;
		}

		// Token: 0x0400033E RID: 830
		private readonly List<IEdmTypeConfiguration> _configurations;

		// Token: 0x0400033F RID: 831
		private readonly Dictionary<Type, IEdmType> _types = new Dictionary<Type, IEdmType>();

		// Token: 0x04000340 RID: 832
		private readonly Dictionary<PropertyInfo, IEdmProperty> _properties = new Dictionary<PropertyInfo, IEdmProperty>();

		// Token: 0x04000341 RID: 833
		private readonly Dictionary<IEdmProperty, QueryableRestrictions> _propertiesRestrictions = new Dictionary<IEdmProperty, QueryableRestrictions>();

		// Token: 0x04000342 RID: 834
		private readonly Dictionary<IEdmProperty, ModelBoundQuerySettings> _propertiesQuerySettings = new Dictionary<IEdmProperty, ModelBoundQuerySettings>();

		// Token: 0x04000343 RID: 835
		private readonly Dictionary<IEdmStructuredType, ModelBoundQuerySettings> _structuredTypeQuerySettings = new Dictionary<IEdmStructuredType, ModelBoundQuerySettings>();

		// Token: 0x04000344 RID: 836
		private readonly Dictionary<Enum, IEdmEnumMember> _members = new Dictionary<Enum, IEdmEnumMember>();

		// Token: 0x04000345 RID: 837
		private readonly Dictionary<IEdmStructuredType, PropertyInfo> _openTypes = new Dictionary<IEdmStructuredType, PropertyInfo>();
	}
}
