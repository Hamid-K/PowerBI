using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts;
using Microsoft.Reporting.QueryDesign.Edm.Internal;

namespace Microsoft.Reporting.QueryDesign.BatchQueries
{
	// Token: 0x0200026E RID: 622
	internal sealed class QueryExistsFilterConsolidator
	{
		// Token: 0x06001AD5 RID: 6869 RVA: 0x0004AF88 File Offset: 0x00049188
		internal static IEnumerable<QueryExistsFilter> Consolidate(IEnumerable<QueryExistsFilter> inputFilters)
		{
			List<QueryExistsFilter> list = inputFilters.ToList<QueryExistsFilter>();
			for (int i = 0; i < list.Count; i++)
			{
				QueryExistsFilter queryExistsFilter = list[i];
				List<EntitySet> list2 = null;
				List<IConceptualEntity> list3 = null;
				for (int j = list.Count - 1; j > i; j--)
				{
					QueryExistsFilter queryExistsFilter2 = list[j];
					if (queryExistsFilter.ExistsEntity != null && queryExistsFilter.ExistsEntity.Equals(queryExistsFilter2.ExistsEntity))
					{
						if (list3 == null)
						{
							list3 = new List<IConceptualEntity>();
							list3.AddRange(queryExistsFilter.TargetEntities);
						}
						if (queryExistsFilter.TargetEntities != null)
						{
							QueryExistsFilterConsolidator.AddDistinct(list3, queryExistsFilter2.TargetEntities);
						}
						list.RemoveAt(j);
					}
					else if (queryExistsFilter.ExistsEntitySet != null && queryExistsFilter.ExistsEntitySet.Equals(queryExistsFilter2.ExistsEntitySet))
					{
						if (list2 == null)
						{
							list2 = new List<EntitySet>();
							list2.AddRange(queryExistsFilter.TargetEntitySets);
						}
						if (queryExistsFilter.TargetEntitySets != null)
						{
							QueryExistsFilterConsolidator.AddDistinct(list2, queryExistsFilter2.TargetEntitySets);
						}
						list.RemoveAt(j);
					}
				}
				if (list3 != null || list2 != null)
				{
					list[i] = new QueryExistsFilter(list2, queryExistsFilter.ExistsEntitySet, list3, queryExistsFilter.ExistsEntity);
				}
			}
			return list;
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x0004B0AC File Offset: 0x000492AC
		private static void AddDistinct(List<EntitySet> targets, IList<EntitySet> newTargets)
		{
			for (int i = 0; i < newTargets.Count; i++)
			{
				EntitySet entitySet = newTargets[i];
				if (!targets.Contains(entitySet))
				{
					targets.Add(entitySet);
				}
			}
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x0004B0E4 File Offset: 0x000492E4
		private static void AddDistinct(List<IConceptualEntity> targetEntities, IReadOnlyList<IConceptualEntity> newTargetEntities)
		{
			for (int i = 0; i < newTargetEntities.Count; i++)
			{
				IConceptualEntity conceptualEntity = newTargetEntities[i];
				if (!targetEntities.Contains(conceptualEntity, ConceptualEntityExtensionAwareEqualityComparer.Instance))
				{
					targetEntities.Add(conceptualEntity);
				}
			}
		}
	}
}
