using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Utilities;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Mappers
{
	// Token: 0x0200015C RID: 348
	internal sealed class PropertyMapper
	{
		// Token: 0x06001605 RID: 5637 RVA: 0x00039169 File Offset: 0x00037369
		public PropertyMapper(TypeMapper typeMapper)
		{
			this._typeMapper = typeMapper;
		}

		// Token: 0x06001606 RID: 5638 RVA: 0x00039178 File Offset: 0x00037378
		public void Map(PropertyInfo propertyInfo, ComplexType complexType, Func<ComplexTypeConfiguration> complexTypeConfiguration)
		{
			EdmProperty edmProperty = this.MapPrimitiveOrComplexOrEnumProperty(propertyInfo, complexTypeConfiguration, true);
			if (edmProperty != null)
			{
				complexType.AddMember(edmProperty);
			}
		}

		// Token: 0x06001607 RID: 5639 RVA: 0x0003919C File Offset: 0x0003739C
		public void Map(PropertyInfo propertyInfo, EntityType entityType, Func<EntityTypeConfiguration> entityTypeConfiguration)
		{
			EdmProperty edmProperty = this.MapPrimitiveOrComplexOrEnumProperty(propertyInfo, entityTypeConfiguration, false);
			if (edmProperty != null)
			{
				entityType.AddMember(edmProperty);
				return;
			}
			new NavigationPropertyMapper(this._typeMapper).Map(propertyInfo, entityType, entityTypeConfiguration);
		}

		// Token: 0x06001608 RID: 5640 RVA: 0x000391D4 File Offset: 0x000373D4
		internal bool MapIfNotNavigationProperty(PropertyInfo propertyInfo, EntityType entityType, Func<EntityTypeConfiguration> entityTypeConfiguration)
		{
			EdmProperty edmProperty = this.MapPrimitiveOrComplexOrEnumProperty(propertyInfo, entityTypeConfiguration, false);
			if (edmProperty != null)
			{
				entityType.AddMember(edmProperty);
				return true;
			}
			return false;
		}

		// Token: 0x06001609 RID: 5641 RVA: 0x000391F8 File Offset: 0x000373F8
		private EdmProperty MapPrimitiveOrComplexOrEnumProperty(PropertyInfo propertyInfo, Func<StructuralTypeConfiguration> structuralTypeConfiguration, bool discoverComplexTypes = false)
		{
			EdmProperty edmProperty = propertyInfo.AsEdmPrimitiveProperty();
			if (edmProperty == null)
			{
				Type propertyType = propertyInfo.PropertyType;
				ComplexType complexType = this._typeMapper.MapComplexType(propertyType, discoverComplexTypes);
				if (complexType != null)
				{
					edmProperty = EdmProperty.CreateComplex(propertyInfo.Name, complexType);
				}
				else
				{
					bool flag = propertyType.TryUnwrapNullableType(out propertyType);
					if (propertyType.IsEnum())
					{
						EnumType enumType = this._typeMapper.MapEnumType(propertyType);
						if (enumType != null)
						{
							edmProperty = EdmProperty.CreateEnum(propertyInfo.Name, enumType);
							edmProperty.Nullable = flag;
						}
					}
				}
			}
			if (edmProperty != null)
			{
				edmProperty.SetClrPropertyInfo(propertyInfo);
				new AttributeMapper(this._typeMapper.MappingContext.AttributeProvider).Map(propertyInfo, edmProperty.GetMetadataProperties());
				if (!edmProperty.IsComplexType)
				{
					this._typeMapper.MappingContext.ConventionsConfiguration.ApplyPropertyConfiguration(propertyInfo, () => structuralTypeConfiguration().Property(new PropertyPath(propertyInfo), null), this._typeMapper.MappingContext.ModelConfiguration);
				}
			}
			return edmProperty;
		}

		// Token: 0x040009FD RID: 2557
		private readonly TypeMapper _typeMapper;
	}
}
