using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.InfoNav;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration
{
	// Token: 0x02000075 RID: 117
	internal sealed class GroupClusterBuilder
	{
		// Token: 0x060005D5 RID: 1493 RVA: 0x00014E64 File Offset: 0x00013064
		private GroupClusterBuilder(Group group, ExpressionTable expressionTable)
		{
			this.m_group = group;
			this.m_expressionTable = expressionTable;
			this.m_initialClusters = new SortedDictionary<int, GroupKey>();
			this.m_columnToClusterIndexMap = new Dictionary<IConceptualColumn, int>();
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x00014E90 File Offset: 0x00013090
		public static List<GroupCluster> Build(Group group, ExpressionTable expressionTable)
		{
			return new GroupClusterBuilder(group, expressionTable).BuildGroupClusters();
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00014EA0 File Offset: 0x000130A0
		private List<GroupCluster> BuildGroupClusters()
		{
			this.BuildInitialClusters();
			Dictionary<IConceptualColumn, HashSet<int>> dictionary = this.BuildMappingBetweenInitialClustersForConceptualSchema();
			Dictionary<int, List<GroupKey>> dictionary2 = this.BuildMergedClusters(dictionary);
			return this.BuildGroupClusters(dictionary2);
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00014ECC File Offset: 0x000130CC
		private void BuildInitialClusters()
		{
			for (int i = 0; i < this.m_group.GroupKeys.Count; i++)
			{
				GroupKey groupKey = this.m_group.GroupKeys[i];
				IConceptualColumn column = groupKey.GetColumn(this.m_expressionTable);
				this.m_initialClusters.Add(i, groupKey);
				this.m_columnToClusterIndexMap.Add(column, i);
			}
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00014F30 File Offset: 0x00013130
		private Dictionary<IConceptualColumn, HashSet<int>> BuildMappingBetweenInitialClustersForConceptualSchema()
		{
			Dictionary<IConceptualColumn, HashSet<int>> dictionary = new Dictionary<IConceptualColumn, HashSet<int>>();
			for (int i = 0; i < this.m_group.GroupKeys.Count; i++)
			{
				IConceptualColumn column = this.m_group.GroupKeys[i].GetColumn(this.m_expressionTable);
				IEnumerable<IConceptualColumn> enumerable = column.Grouping.QueryGroupColumns.Union(column.OrderByColumns).Union(column.AsReadOnlyList<IConceptualColumn>());
				IConceptualColumn colorByColumn = GroupClusterBuilder.GetColorByColumn(column);
				if (colorByColumn != null)
				{
					enumerable = enumerable.Union(colorByColumn.AsReadOnlyList<IConceptualColumn>());
				}
				foreach (IConceptualColumn conceptualColumn in enumerable)
				{
					this.AddToMapping(dictionary, conceptualColumn, i);
					int num;
					if (this.m_columnToClusterIndexMap.TryGetValue(conceptualColumn, out num))
					{
						this.AddToMapping(dictionary, column, num);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00015020 File Offset: 0x00013220
		private static IConceptualColumn GetColorByColumn(IConceptualColumn parentColumn)
		{
			IList<IConceptualColumn> list = (from p in parentColumn.Entity.Properties.OfType<IConceptualColumn>()
				where p.ConceptualDataCategory == ConceptualDataCategory.Color
				select p).Evaluate<IConceptualColumn>();
			if (list.Count == 1)
			{
				return list[0].AsColumn();
			}
			string targetName = parentColumn.EdmName + "Color";
			return list.FirstOrDefault((IConceptualColumn c) => string.Equals(c.EdmName, targetName, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x000150AB File Offset: 0x000132AB
		private void AddToMapping(Dictionary<IConceptualColumn, HashSet<int>> mapping, IConceptualColumn item, int clusterIndex)
		{
			if (!mapping.ContainsKey(item))
			{
				mapping.Add(item, new HashSet<int>());
			}
			mapping[item].Add(clusterIndex);
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x000150D0 File Offset: 0x000132D0
		private Dictionary<int, List<GroupKey>> BuildMergedClusters(Dictionary<IConceptualColumn, HashSet<int>> queryGroupingColumnToInitialClustersMap)
		{
			Dictionary<int, List<GroupKey>> dictionary = new Dictionary<int, List<GroupKey>>(this.m_initialClusters.Count);
			Queue<int> queue = new Queue<int>(this.m_initialClusters.Count);
			int num = -1;
			while (this.m_initialClusters.Count > 0)
			{
				num++;
				List<GroupKey> list = new List<GroupKey>();
				dictionary.Add(num, list);
				List<int> list2 = new List<int>();
				queue.Enqueue(this.m_initialClusters.Keys.First<int>());
				while (queue.Count > 0)
				{
					int num2 = queue.Dequeue();
					list2.Add(num2);
					GroupKey groupKey = this.m_initialClusters[num2];
					IConceptualColumn column = groupKey.GetColumn(this.m_expressionTable);
					list.Add(groupKey);
					HashSet<int> hashSet;
					if (queryGroupingColumnToInitialClustersMap.TryGetValue(column, out hashSet))
					{
						foreach (int num3 in hashSet)
						{
							if (!list2.Contains(num3) && !queue.Contains(num3))
							{
								queue.Enqueue(num3);
							}
						}
					}
					this.m_initialClusters.Remove(num2);
				}
			}
			return dictionary;
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00015204 File Offset: 0x00013404
		private List<GroupCluster> BuildGroupClusters(Dictionary<int, List<GroupKey>> mergedClusters)
		{
			Dictionary<int, List<GroupKey>>.KeyCollection keys = mergedClusters.Keys;
			List<GroupCluster> list = new List<GroupCluster>();
			foreach (int num in keys)
			{
				bool flag = false;
				foreach (GroupKey groupKey in mergedClusters[num])
				{
					flag |= groupKey.ShowItemsWithNoData.GetValueOrDefault<bool>();
				}
				List<GroupKey> list2 = mergedClusters[num].ToList<GroupKey>();
				list.Add(new GroupCluster(list2, flag));
			}
			return list;
		}

		// Token: 0x040002ED RID: 749
		private readonly Group m_group;

		// Token: 0x040002EE RID: 750
		private readonly ExpressionTable m_expressionTable;

		// Token: 0x040002EF RID: 751
		private readonly SortedDictionary<int, GroupKey> m_initialClusters;

		// Token: 0x040002F0 RID: 752
		private readonly Dictionary<IConceptualColumn, int> m_columnToClusterIndexMap;
	}
}
