using System;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Edm.Services
{
	// Token: 0x02000179 RID: 377
	internal abstract class StructuralTypeMappingGenerator
	{
		// Token: 0x060016DB RID: 5851 RVA: 0x0003CC70 File Offset: 0x0003AE70
		protected StructuralTypeMappingGenerator(DbProviderManifest providerManifest)
		{
			this._providerManifest = providerManifest;
		}

		// Token: 0x060016DC RID: 5852 RVA: 0x0003CC80 File Offset: 0x0003AE80
		protected EdmProperty MapTableColumn(EdmProperty property, string columnName, bool isInstancePropertyOnDerivedType)
		{
			TypeUsage typeUsage = TypeUsage.Create(property.UnderlyingPrimitiveType, property.TypeUsage.Facets);
			TypeUsage storeType = this._providerManifest.GetStoreType(typeUsage);
			EdmProperty edmProperty = new EdmProperty(columnName, storeType)
			{
				Nullable = (isInstancePropertyOnDerivedType || property.Nullable)
			};
			if (edmProperty.IsPrimaryKeyColumn)
			{
				edmProperty.Nullable = false;
			}
			StoreGeneratedPattern? storeGeneratedPattern = property.GetStoreGeneratedPattern();
			if (storeGeneratedPattern != null)
			{
				edmProperty.StoreGeneratedPattern = storeGeneratedPattern.Value;
			}
			StructuralTypeMappingGenerator.MapPrimitivePropertyFacets(property, edmProperty, storeType);
			return edmProperty;
		}

		// Token: 0x060016DD RID: 5853 RVA: 0x0003CD00 File Offset: 0x0003AF00
		internal static void MapPrimitivePropertyFacets(EdmProperty property, EdmProperty column, TypeUsage typeUsage)
		{
			if (StructuralTypeMappingGenerator.IsValidFacet(typeUsage, "FixedLength") && property.IsFixedLength != null)
			{
				column.IsFixedLength = property.IsFixedLength;
			}
			if (StructuralTypeMappingGenerator.IsValidFacet(typeUsage, "MaxLength"))
			{
				column.IsMaxLength = property.IsMaxLength;
				if (!column.IsMaxLength || property.MaxLength != null)
				{
					column.MaxLength = property.MaxLength;
				}
			}
			if (StructuralTypeMappingGenerator.IsValidFacet(typeUsage, "Unicode") && property.IsUnicode != null)
			{
				column.IsUnicode = property.IsUnicode;
			}
			if (StructuralTypeMappingGenerator.IsValidFacet(typeUsage, "Precision") && property.Precision != null)
			{
				column.Precision = property.Precision;
			}
			if (StructuralTypeMappingGenerator.IsValidFacet(typeUsage, "Scale") && property.Scale != null)
			{
				column.Scale = property.Scale;
			}
		}

		// Token: 0x060016DE RID: 5854 RVA: 0x0003CDF0 File Offset: 0x0003AFF0
		private static bool IsValidFacet(TypeUsage typeUsage, string name)
		{
			Facet facet;
			return typeUsage.Facets.TryGetValue(name, false, out facet) && !facet.Description.IsConstant;
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x0003CE20 File Offset: 0x0003B020
		protected static EntityTypeMapping GetEntityTypeMappingInHierarchy(DbDatabaseMapping databaseMapping, EntityType entityType)
		{
			EntityTypeMapping entityTypeMapping = databaseMapping.GetEntityTypeMapping(entityType);
			if (entityTypeMapping == null)
			{
				EntitySetMapping entitySetMapping = databaseMapping.GetEntitySetMapping(databaseMapping.Model.GetEntitySet(entityType));
				if (entitySetMapping != null)
				{
					entityTypeMapping = entitySetMapping.EntityTypeMappings.First((EntityTypeMapping etm) => entityType.DeclaredProperties.All((EdmProperty dp) => etm.MappingFragments.First<MappingFragment>().ColumnMappings.Select((ColumnMappingBuilder pm) => pm.PropertyPath.First<EdmProperty>()).Contains(dp)));
				}
			}
			return entityTypeMapping;
		}

		// Token: 0x04000A18 RID: 2584
		protected readonly DbProviderManifest _providerManifest;
	}
}
