using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Configuration.Mapping
{
	// Token: 0x02000212 RID: 530
	internal static class EntityMappingOperations
	{
		// Token: 0x06001C1A RID: 7194 RVA: 0x0004F6EC File Offset: 0x0004D8EC
		public static MappingFragment CreateTypeMappingFragment(EntityTypeMapping entityTypeMapping, MappingFragment templateFragment, EntitySet tableSet)
		{
			MappingFragment mappingFragment = new MappingFragment(tableSet, entityTypeMapping, false);
			entityTypeMapping.AddFragment(mappingFragment);
			foreach (ColumnMappingBuilder columnMappingBuilder in templateFragment.ColumnMappings.Where((ColumnMappingBuilder pm) => pm.ColumnProperty.IsPrimaryKeyColumn))
			{
				EntityMappingOperations.CopyPropertyMappingToFragment(columnMappingBuilder, mappingFragment, TablePrimitiveOperations.GetNameMatcher(columnMappingBuilder.ColumnProperty.Name), true);
			}
			return mappingFragment;
		}

		// Token: 0x06001C1B RID: 7195 RVA: 0x0004F780 File Offset: 0x0004D980
		private static void UpdatePropertyMapping(DbDatabaseMapping databaseMapping, IEnumerable<EntitySet> entitySets, Dictionary<EdmProperty, IList<ColumnMappingBuilder>> columnMappingIndex, ColumnMappingBuilder propertyMappingBuilder, EntityType fromTable, EntityType toTable, bool useExisting)
		{
			propertyMappingBuilder.ColumnProperty = TableOperations.CopyColumnAndAnyConstraints(databaseMapping.Database, fromTable, toTable, propertyMappingBuilder.ColumnProperty, EntityMappingOperations.GetPropertyPathMatcher(columnMappingIndex, propertyMappingBuilder), useExisting);
			propertyMappingBuilder.SyncNullabilityCSSpace(databaseMapping, entitySets, toTable);
		}

		// Token: 0x06001C1C RID: 7196 RVA: 0x0004F7B0 File Offset: 0x0004D9B0
		private static Func<EdmProperty, bool> GetPropertyPathMatcher(Dictionary<EdmProperty, IList<ColumnMappingBuilder>> columnMappingIndex, ColumnMappingBuilder propertyMappingBuilder)
		{
			return delegate(EdmProperty c)
			{
				if (!columnMappingIndex.ContainsKey(c))
				{
					return false;
				}
				IList<ColumnMappingBuilder> list = columnMappingIndex[c];
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i].PropertyPath.PathEqual(propertyMappingBuilder.PropertyPath))
					{
						return true;
					}
				}
				return false;
			};
		}

		// Token: 0x06001C1D RID: 7197 RVA: 0x0004F7D0 File Offset: 0x0004D9D0
		private static bool PathEqual(this IList<EdmProperty> listA, IList<EdmProperty> listB)
		{
			if (listA == null || listB == null)
			{
				return false;
			}
			if (listA.Count != listB.Count)
			{
				return false;
			}
			for (int i = 0; i < listA.Count; i++)
			{
				if (listA[i] != listB[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001C1E RID: 7198 RVA: 0x0004F81C File Offset: 0x0004DA1C
		private static Dictionary<EdmProperty, IList<ColumnMappingBuilder>> GetColumnMappingIndex(DbDatabaseMapping databaseMapping)
		{
			Dictionary<EdmProperty, IList<ColumnMappingBuilder>> dictionary = new Dictionary<EdmProperty, IList<ColumnMappingBuilder>>();
			IEnumerable<EntitySetMapping> entitySetMappings = databaseMapping.EntityContainerMappings.Single<EntityContainerMapping>().EntitySetMappings;
			if (entitySetMappings == null)
			{
				return dictionary;
			}
			List<EntitySetMapping> list = entitySetMappings.ToList<EntitySetMapping>();
			for (int i = 0; i < list.Count; i++)
			{
				IList<EntityTypeMapping> entityTypeMappings = list[i].EntityTypeMappings;
				if (entityTypeMappings != null)
				{
					for (int j = 0; j < entityTypeMappings.Count; j++)
					{
						IList<MappingFragment> mappingFragments = entityTypeMappings[j].MappingFragments;
						if (mappingFragments != null)
						{
							for (int k = 0; k < mappingFragments.Count; k++)
							{
								IList<ColumnMappingBuilder> list2 = mappingFragments[k].ColumnMappings as IList<ColumnMappingBuilder>;
								if (list2 != null)
								{
									for (int l = 0; l < list2.Count; l++)
									{
										ColumnMappingBuilder columnMappingBuilder = list2[l];
										IList<ColumnMappingBuilder> list3;
										if (dictionary.ContainsKey(columnMappingBuilder.ColumnProperty))
										{
											list3 = dictionary[columnMappingBuilder.ColumnProperty];
										}
										else
										{
											dictionary.Add(columnMappingBuilder.ColumnProperty, list3 = new List<ColumnMappingBuilder>());
										}
										list3.Add(columnMappingBuilder);
									}
								}
							}
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06001C1F RID: 7199 RVA: 0x0004F944 File Offset: 0x0004DB44
		public static void UpdatePropertyMappings(DbDatabaseMapping databaseMapping, IEnumerable<EntitySet> entitySets, EntityType fromTable, MappingFragment fragment, bool useExisting)
		{
			if (fromTable != fragment.Table)
			{
				Dictionary<EdmProperty, IList<ColumnMappingBuilder>> columnMappingIndex = EntityMappingOperations.GetColumnMappingIndex(databaseMapping);
				List<ColumnMappingBuilder> list = fragment.ColumnMappings.ToList<ColumnMappingBuilder>();
				for (int i = 0; i < list.Count; i++)
				{
					EntityMappingOperations.UpdatePropertyMapping(databaseMapping, entitySets, columnMappingIndex, list[i], fromTable, fragment.Table, useExisting);
				}
			}
		}

		// Token: 0x06001C20 RID: 7200 RVA: 0x0004F998 File Offset: 0x0004DB98
		public static void MovePropertyMapping(DbDatabaseMapping databaseMapping, IEnumerable<EntitySet> entitySets, MappingFragment fromFragment, MappingFragment toFragment, ColumnMappingBuilder propertyMappingBuilder, bool requiresUpdate, bool useExisting)
		{
			if (requiresUpdate && fromFragment.Table != toFragment.Table)
			{
				EntityMappingOperations.UpdatePropertyMapping(databaseMapping, entitySets, EntityMappingOperations.GetColumnMappingIndex(databaseMapping), propertyMappingBuilder, fromFragment.Table, toFragment.Table, useExisting);
			}
			fromFragment.RemoveColumnMapping(propertyMappingBuilder);
			toFragment.AddColumnMapping(propertyMappingBuilder);
		}

		// Token: 0x06001C21 RID: 7201 RVA: 0x0004F9E4 File Offset: 0x0004DBE4
		public static void CopyPropertyMappingToFragment(ColumnMappingBuilder propertyMappingBuilder, MappingFragment fragment, Func<EdmProperty, bool> isCompatible, bool useExisting)
		{
			EdmProperty edmProperty = TablePrimitiveOperations.IncludeColumn(fragment.Table, propertyMappingBuilder.ColumnProperty, isCompatible, useExisting);
			fragment.AddColumnMapping(new ColumnMappingBuilder(edmProperty, propertyMappingBuilder.PropertyPath));
		}

		// Token: 0x06001C22 RID: 7202 RVA: 0x0004FA18 File Offset: 0x0004DC18
		public static void UpdateConditions(EdmModel database, EntityType fromTable, MappingFragment fragment)
		{
			if (fromTable != fragment.Table)
			{
				fragment.ColumnConditions.Each(delegate(ConditionPropertyMapping cc)
				{
					cc.Column = TableOperations.CopyColumnAndAnyConstraints(database, fromTable, fragment.Table, cc.Column, TablePrimitiveOperations.GetNameMatcher(cc.Column.Name), true);
				});
			}
		}
	}
}
