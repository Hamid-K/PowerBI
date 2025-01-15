using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020001A5 RID: 421
	public class MappingInheritedPropertiesSupportConvention : IDbMappingConvention, IConvention
	{
		// Token: 0x06001767 RID: 5991 RVA: 0x0003ECF4 File Offset: 0x0003CEF4
		void IDbMappingConvention.Apply(DbDatabaseMapping databaseMapping)
		{
			Check.NotNull<DbDatabaseMapping>(databaseMapping, "databaseMapping");
			databaseMapping.EntityContainerMappings.SelectMany((EntityContainerMapping ecm) => ecm.EntitySetMappings).Each(delegate(EntitySetMapping esm)
			{
				foreach (EntityTypeMapping entityTypeMapping in esm.EntityTypeMappings)
				{
					if (MappingInheritedPropertiesSupportConvention.RemapsInheritedProperties(databaseMapping, entityTypeMapping) && MappingInheritedPropertiesSupportConvention.HasBaseWithIsTypeOf(esm, entityTypeMapping.EntityType))
					{
						throw Error.UnsupportedHybridInheritanceMapping(entityTypeMapping.EntityType.Name);
					}
				}
			});
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x0003ED60 File Offset: 0x0003CF60
		private static bool RemapsInheritedProperties(DbDatabaseMapping databaseMapping, EntityTypeMapping entityTypeMapping)
		{
			using (IEnumerator<EdmProperty> enumerator = entityTypeMapping.EntityType.Properties.Except(entityTypeMapping.EntityType.DeclaredProperties).Except(entityTypeMapping.EntityType.GetKeyProperties()).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EdmProperty property = enumerator.Current;
					MappingFragment fragment = MappingInheritedPropertiesSupportConvention.GetFragmentForPropertyMapping(entityTypeMapping, property);
					if (fragment != null)
					{
						Func<MappingFragment, bool> <>9__1;
						Func<EntityTypeMapping, MappingFragment> <>9__0;
						for (EntityType entityType = (EntityType)entityTypeMapping.EntityType.BaseType; entityType != null; entityType = (EntityType)entityType.BaseType)
						{
							IEnumerable<EntityTypeMapping> entityTypeMappings = databaseMapping.GetEntityTypeMappings(entityType);
							Func<EntityTypeMapping, MappingFragment> func;
							if ((func = <>9__0) == null)
							{
								func = (<>9__0 = (EntityTypeMapping baseTypeMapping) => MappingInheritedPropertiesSupportConvention.GetFragmentForPropertyMapping(baseTypeMapping, property));
							}
							IEnumerable<MappingFragment> enumerable = entityTypeMappings.Select(func);
							Func<MappingFragment, bool> func2;
							if ((func2 = <>9__1) == null)
							{
								func2 = (<>9__1 = (MappingFragment baseFragment) => baseFragment != null && baseFragment.Table != fragment.Table);
							}
							if (enumerable.Any(func2))
							{
								return true;
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x0003EE7C File Offset: 0x0003D07C
		private static MappingFragment GetFragmentForPropertyMapping(EntityTypeMapping entityTypeMapping, EdmProperty property)
		{
			Func<ColumnMappingBuilder, bool> <>9__1;
			return entityTypeMapping.MappingFragments.SingleOrDefault(delegate(MappingFragment tmf)
			{
				IEnumerable<ColumnMappingBuilder> columnMappings = tmf.ColumnMappings;
				Func<ColumnMappingBuilder, bool> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = (ColumnMappingBuilder pm) => pm.PropertyPath.Last<EdmProperty>() == property);
				}
				return columnMappings.Any(func);
			});
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x0003EEB0 File Offset: 0x0003D0B0
		private static bool HasBaseWithIsTypeOf(EntitySetMapping entitySetMapping, EntityType entityType)
		{
			EdmType baseType;
			for (baseType = entityType.BaseType; baseType != null; baseType = baseType.BaseType)
			{
				if (entitySetMapping.EntityTypeMappings.Where((EntityTypeMapping etm) => etm.EntityType == baseType).Any((EntityTypeMapping etm) => etm.IsHierarchyMapping))
				{
					return true;
				}
			}
			return false;
		}
	}
}
