using System;
using System.Collections.Generic;
using Microsoft.DataShaping;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000C4 RID: 196
	internal static class QueryUtils
	{
		// Token: 0x06000723 RID: 1827 RVA: 0x0001AF98 File Offset: 0x00019198
		internal static bool HasAllGroupKeysFromSameEntity(this QueryGroup group)
		{
			IReadOnlyList<QueryGroupKey> keys = group.Keys;
			if (keys.Count == 0)
			{
				return true;
			}
			IConceptualEntity conceptualEntity = null;
			for (int i = 0; i < keys.Count; i++)
			{
				IConceptualColumn field = keys[i].Field;
				if (conceptualEntity == null)
				{
					conceptualEntity = field.Entity;
				}
				else if (!conceptualEntity.Equals(field.Entity))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x0001AFF4 File Offset: 0x000191F4
		internal static bool HasBinnableOrderByColumn(this QueryGroup group, out int binnableKeyCount)
		{
			IReadOnlyList<QueryGroupKey> keys = group.Keys;
			binnableKeyCount = 0;
			HashSet<IConceptualColumn> hashSet = new HashSet<IConceptualColumn>();
			for (int i = 0; i < keys.Count; i++)
			{
				IConceptualColumn field = keys[i].Field;
				if (field != null)
				{
					if (field.OrderByColumns.Count > 0)
					{
						hashSet.UnionWith(field.OrderByColumns);
					}
					if (field.ConceptualDataType.IsScalar())
					{
						binnableKeyCount++;
					}
				}
			}
			bool flag = false;
			using (HashSet<IConceptualColumn>.Enumerator enumerator = hashSet.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.ConceptualDataType.IsScalar())
					{
						flag = true;
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0001B0B4 File Offset: 0x000192B4
		internal static bool HasSortByMeasure(this QueryGroup group)
		{
			IReadOnlyList<DsqSortKey> sortKeys = group.SortKeys;
			if (sortKeys.Count == 0)
			{
				return false;
			}
			for (int i = 0; i < sortKeys.Count; i++)
			{
				if (sortKeys[i].IsMeasure)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0001B0F4 File Offset: 0x000192F4
		internal static bool HasSortByMeasure(this QueryProjections projections)
		{
			if (!projections.HasGroups)
			{
				return false;
			}
			for (int i = 0; i < projections.PrimaryMembers.Count; i++)
			{
				if (projections.PrimaryMembers[i].Group.HasSortByMeasure())
				{
					return true;
				}
			}
			if (projections.SecondaryMembers.Count == 0)
			{
				return false;
			}
			for (int j = 0; j < projections.SecondaryMembers.Count; j++)
			{
				if (projections.SecondaryMembers[j].Group.HasSortByMeasure())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0001B17C File Offset: 0x0001937C
		internal static IReadOnlyList<QueryGroupValue> FindGroupValuesBySelectIndex(this QueryProjections projections, int selectIndex)
		{
			if (projections == null)
			{
				return null;
			}
			IReadOnlyList<QueryGroupValue> readOnlyList = QueryUtils.FindGroupValuesBySelectIndex(projections.PrimaryMembers, selectIndex);
			if (readOnlyList == null)
			{
				readOnlyList = QueryUtils.FindGroupValuesBySelectIndex(projections.SecondaryMembers, selectIndex);
			}
			return readOnlyList;
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0001B1AC File Offset: 0x000193AC
		internal static IReadOnlyList<ProjectedDsqExpression> FindMeasuresBySelectIndex(this QueryProjections projections, int selectIndex)
		{
			if (projections == null)
			{
				return null;
			}
			List<ProjectedDsqExpression> list = QueryUtils.FindMeasuresBySelectIndex(projections.Measures, selectIndex);
			List<ProjectedDsqExpression> list2 = QueryUtils.FindMeasuresBySelectIndex(projections.DataShapeProjections, selectIndex);
			if (!list2.IsNullOrEmptyCollection<ProjectedDsqExpression>())
			{
				Util.AddToLazyList<ProjectedDsqExpression>(ref list, list2);
			}
			List<ProjectedDsqExpression> list3 = QueryUtils.FindMeasureCalculationsBySelectIndex(projections.PrimaryMembers, selectIndex);
			if (!list3.IsNullOrEmptyCollection<ProjectedDsqExpression>())
			{
				Util.AddToLazyList<ProjectedDsqExpression>(ref list, list3);
			}
			List<ProjectedDsqExpression> list4 = QueryUtils.FindMeasureCalculationsBySelectIndex(projections.SecondaryMembers, selectIndex);
			if (!list4.IsNullOrEmptyCollection<ProjectedDsqExpression>())
			{
				Util.AddToLazyList<ProjectedDsqExpression>(ref list, list4);
			}
			return list;
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0001B224 File Offset: 0x00019424
		private static IReadOnlyList<QueryGroupValue> FindGroupValuesBySelectIndex(IReadOnlyList<QueryMember> members, int selectIndex)
		{
			if (members.IsNullOrEmpty<QueryMember>())
			{
				return null;
			}
			List<QueryGroupValue> list = null;
			foreach (QueryMember queryMember in members)
			{
				if (!queryMember.Values.IsNullOrEmpty<QueryGroupValue>())
				{
					bool selectIndexReferencesProjectedExpression = false;
					QueryGroupValueProjectionVisitor queryGroupValueProjectionVisitor = new QueryGroupValueProjectionVisitor(delegate(ProjectedDsqExpression projection)
					{
						selectIndexReferencesProjectedExpression = QueryUtils.SelectIndexReferencesProjectedExpression(selectIndex, projection);
					});
					foreach (QueryGroupValue queryGroupValue in queryMember.Values)
					{
						selectIndexReferencesProjectedExpression = false;
						queryGroupValue.Accept<QueryGroupValue>(queryGroupValueProjectionVisitor);
						if (selectIndexReferencesProjectedExpression)
						{
							Util.AddToLazyList<QueryGroupValue>(ref list, queryGroupValue);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0001B31C File Offset: 0x0001951C
		internal static List<ProjectedDsqExpression> FindMeasuresBySelectIndex(IReadOnlyList<ProjectedDsqExpression> measures, int selectIndex)
		{
			if (measures.IsNullOrEmpty<ProjectedDsqExpression>())
			{
				return null;
			}
			List<ProjectedDsqExpression> list = null;
			foreach (ProjectedDsqExpression projectedDsqExpression in measures)
			{
				if (QueryUtils.SelectIndexReferencesProjectedExpression(selectIndex, projectedDsqExpression))
				{
					Util.AddToLazyList<ProjectedDsqExpression>(ref list, projectedDsqExpression);
				}
			}
			return list;
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0001B37C File Offset: 0x0001957C
		internal static HashSet<int> GetAllProjectedExpressionIndices(IReadOnlyList<ProjectedDsqExpression> measures)
		{
			HashSet<int> hashSet = new HashSet<int>();
			if (measures.IsNullOrEmpty<ProjectedDsqExpression>())
			{
				return hashSet;
			}
			foreach (ProjectedDsqExpression projectedDsqExpression in measures)
			{
				if (projectedDsqExpression.SemanticQuerySelectIndex != null)
				{
					hashSet.Add(projectedDsqExpression.SemanticQuerySelectIndex.Value);
				}
				if (!projectedDsqExpression.AdditionalSemanticQuerySelectIndices.IsNullOrEmpty<int>())
				{
					foreach (int num in projectedDsqExpression.AdditionalSemanticQuerySelectIndices)
					{
						hashSet.Add(num);
					}
				}
			}
			return hashSet;
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0001B448 File Offset: 0x00019648
		private static List<ProjectedDsqExpression> FindMeasureCalculationsBySelectIndex(IReadOnlyList<QueryMember> members, int selectIndex)
		{
			if (members.IsNullOrEmpty<QueryMember>())
			{
				return null;
			}
			List<ProjectedDsqExpression> list = null;
			foreach (QueryMember queryMember in members)
			{
				if (!queryMember.MeasureCalculations.IsNullOrEmpty<ProjectedDsqExpression>())
				{
					foreach (ProjectedDsqExpression projectedDsqExpression in queryMember.MeasureCalculations)
					{
						if (QueryUtils.SelectIndexReferencesProjectedExpression(selectIndex, projectedDsqExpression))
						{
							Util.AddToLazyList<ProjectedDsqExpression>(ref list, projectedDsqExpression);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0001B4EC File Offset: 0x000196EC
		internal static bool SelectIndexReferencesProjectedExpression(int selectIndex, ProjectedDsqExpression expression)
		{
			return (expression.SemanticQuerySelectIndex != null && selectIndex == expression.SemanticQuerySelectIndex.Value) || (!expression.AdditionalSemanticQuerySelectIndices.IsNullOrEmpty<int>() && expression.AdditionalSemanticQuerySelectIndices.Contains(selectIndex));
		}
	}
}
