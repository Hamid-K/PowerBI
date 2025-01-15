using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration
{
	// Token: 0x02000093 RID: 147
	internal sealed class QueryTrimmer
	{
		// Token: 0x060006F3 RID: 1779 RVA: 0x0001A2D8 File Offset: 0x000184D8
		internal QueryTrimmer(ScopeTree scopeTree, ExpressionTable expressionTable, List<GroupAndSortingContext> scopeToGroupMapping, DataSetPlan dataSetPlan)
		{
			this.m_scopeTree = scopeTree;
			this.m_expressionTable = expressionTable;
			this.m_scopeToGroupMapping = scopeToGroupMapping.AsReadOnly();
			this.m_limits = dataSetPlan.Scopes.Select((ScopePlanElement d) => d.Limit).WhereNonNull<Microsoft.DataShaping.InternalContracts.DataShapeQuery.Limit>().ToList<Microsoft.DataShaping.InternalContracts.DataShapeQuery.Limit>()
				.AsReadOnly();
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0001A348 File Offset: 0x00018548
		public List<Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.Group> GetNonProjectedGroupsToTrimFromQuery(QueryDefinition queryDefinition)
		{
			if (!QueryAlgorithms.CanTrimGroups(queryDefinition))
			{
				return null;
			}
			List<Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.Group> list = new List<Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.Group>(queryDefinition.Groups.Count);
			for (int i = queryDefinition.Groups.Count - 1; i >= 0; i--)
			{
				Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.Group group = queryDefinition.Groups[i];
				if (!group.IsProjected)
				{
					if (this.ShouldBlockTrimming(group))
					{
						break;
					}
					if ((queryDefinition.PostRegroupLimit == null || !queryDefinition.PostRegroupLimit.RefersTo(group)) && !queryDefinition.Limits.Any((Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.Limit l) => l.RefersTo(group)) && (queryDefinition.GroupFilter == null || !queryDefinition.GroupFilter.RefersTo(group)) && (queryDefinition.Rollup == null || !queryDefinition.Rollup.RollupGroups.Any((RollupGroup r) => r.RefersTo(group))))
					{
						list.Add(group);
					}
				}
			}
			return list;
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0001A44C File Offset: 0x0001864C
		private bool ShouldBlockTrimming(Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.Group group)
		{
			Func<GroupReference, bool> <>9__2;
			DataMember dataMember = (from a in this.m_scopeToGroupMapping.Where(delegate(GroupAndSortingContext a)
				{
					IEnumerable<GroupReference> groupReferences = a.GroupReferences;
					Func<GroupReference, bool> func;
					if ((func = <>9__2) == null)
					{
						func = (<>9__2 = (GroupReference r) => r.Group.EqualsByName(@group) && !r.WasReused);
					}
					return groupReferences.Any(func);
				})
				select a.Scope).Single<DataMember>();
			foreach (Microsoft.DataShaping.InternalContracts.DataShapeQuery.Limit limit in this.m_limits)
			{
				IScope resolvedScope = limit.GetInnermostTarget().GetResolvedScope(this.m_expressionTable);
				if (this.m_scopeTree.IsParentScope(dataMember, resolvedScope))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400035E RID: 862
		private readonly ScopeTree m_scopeTree;

		// Token: 0x0400035F RID: 863
		private readonly ExpressionTable m_expressionTable;

		// Token: 0x04000360 RID: 864
		private readonly ReadOnlyCollection<GroupAndSortingContext> m_scopeToGroupMapping;

		// Token: 0x04000361 RID: 865
		private readonly ReadOnlyCollection<Microsoft.DataShaping.InternalContracts.DataShapeQuery.Limit> m_limits;
	}
}
