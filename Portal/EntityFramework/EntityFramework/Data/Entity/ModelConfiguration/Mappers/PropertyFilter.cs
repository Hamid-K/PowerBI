using System;
using System.Collections.Generic;
using System.Data.Entity.Hierarchy;
using System.Data.Entity.Resources;
using System.Data.Entity.Spatial;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Mappers
{
	// Token: 0x0200015B RID: 347
	internal sealed class PropertyFilter
	{
		// Token: 0x060015FD RID: 5629 RVA: 0x00038F55 File Offset: 0x00037155
		public PropertyFilter(DbModelBuilderVersion modelBuilderVersion = DbModelBuilderVersion.Latest)
		{
			this._modelBuilderVersion = modelBuilderVersion;
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x00038F64 File Offset: 0x00037164
		public IEnumerable<PropertyInfo> GetProperties(Type type, bool declaredOnly, IEnumerable<PropertyInfo> explicitlyMappedProperties = null, IEnumerable<Type> knownTypes = null, bool includePrivate = false)
		{
			explicitlyMappedProperties = explicitlyMappedProperties ?? Enumerable.Empty<PropertyInfo>();
			knownTypes = knownTypes ?? Enumerable.Empty<Type>();
			this.ValidatePropertiesForModelVersion(type, explicitlyMappedProperties);
			return from p in declaredOnly ? type.GetDeclaredProperties() : type.GetNonHiddenProperties()
				where !p.IsStatic() && p.IsValidStructuralProperty()
				let m = p.Getter()
				where (includePrivate || m.IsPublic || explicitlyMappedProperties.Contains(p) || knownTypes.Contains(p.PropertyType)) && (!declaredOnly || type.BaseType().GetInstanceProperties().All((PropertyInfo bp) => bp.Name != p.Name)) && (this.EdmV3FeaturesSupported || (!PropertyFilter.IsEnumType(p.PropertyType) && !PropertyFilter.IsSpatialType(p.PropertyType) && !PropertyFilter.IsHierarchyIdType(p.PropertyType))) && (this.Ef6FeaturesSupported || !p.PropertyType.IsNested)
				select p;
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x0003907C File Offset: 0x0003727C
		public void ValidatePropertiesForModelVersion(Type type, IEnumerable<PropertyInfo> explicitlyMappedProperties)
		{
			if (this._modelBuilderVersion == DbModelBuilderVersion.Latest)
			{
				return;
			}
			if (!this.EdmV3FeaturesSupported)
			{
				PropertyInfo propertyInfo = explicitlyMappedProperties.FirstOrDefault((PropertyInfo p) => PropertyFilter.IsEnumType(p.PropertyType) || PropertyFilter.IsSpatialType(p.PropertyType) || PropertyFilter.IsHierarchyIdType(p.PropertyType));
				if (propertyInfo != null)
				{
					throw Error.UnsupportedUseOfV3Type(type.Name, propertyInfo.Name);
				}
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06001600 RID: 5632 RVA: 0x000390DB File Offset: 0x000372DB
		public bool EdmV3FeaturesSupported
		{
			get
			{
				return this._modelBuilderVersion.GetEdmVersion() >= 3.0;
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06001601 RID: 5633 RVA: 0x000390F6 File Offset: 0x000372F6
		public bool Ef6FeaturesSupported
		{
			get
			{
				return this._modelBuilderVersion == DbModelBuilderVersion.Latest || this._modelBuilderVersion >= DbModelBuilderVersion.V6_0;
			}
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x0003910E File Offset: 0x0003730E
		private static bool IsEnumType(Type type)
		{
			type.TryUnwrapNullableType(out type);
			return type.IsEnum();
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x0003911F File Offset: 0x0003731F
		private static bool IsHierarchyIdType(Type type)
		{
			type.TryUnwrapNullableType(out type);
			return type == typeof(HierarchyId);
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x0003913A File Offset: 0x0003733A
		private static bool IsSpatialType(Type type)
		{
			type.TryUnwrapNullableType(out type);
			return type == typeof(DbGeometry) || type == typeof(DbGeography);
		}

		// Token: 0x040009FC RID: 2556
		private readonly DbModelBuilderVersion _modelBuilderVersion;
	}
}
