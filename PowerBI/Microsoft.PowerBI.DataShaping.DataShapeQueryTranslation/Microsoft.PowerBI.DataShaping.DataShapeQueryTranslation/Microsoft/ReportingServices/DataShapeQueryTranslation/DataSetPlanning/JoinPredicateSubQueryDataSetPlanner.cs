using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x02000109 RID: 265
	internal sealed class JoinPredicateSubQueryDataSetPlanner : DataSetPlanElementVisitor
	{
		// Token: 0x06000A44 RID: 2628 RVA: 0x00027C39 File Offset: 0x00025E39
		internal JoinPredicateSubQueryDataSetPlanner(DataSetPlan dataSetPlan, ScopeTree scopeTree, FilterCondition subQueryJoinPredicate)
		{
			this.m_dataSetPlan = dataSetPlan;
			this.m_scopeTree = scopeTree;
			this.m_subQueryJoinPredicate = subQueryJoinPredicate;
			this.m_joinPredicatePlanScopes = new List<ScopePlanElement>();
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x00027C61 File Offset: 0x00025E61
		public static DataSetPlan DeterminePlan(DataSetPlan dataSetPlan, ScopeTree scopeTree, FilterCondition subQueryJoinPredicate, int suffix)
		{
			return new JoinPredicateSubQueryDataSetPlanner(dataSetPlan, scopeTree, subQueryJoinPredicate).DetermineSubQueryJoinPredicatePlan(suffix);
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x00027C74 File Offset: 0x00025E74
		private DataSetPlan DetermineSubQueryJoinPredicatePlan(int suffix)
		{
			ReadOnlyCollection<ScopePlanElement> scopes = this.m_dataSetPlan.Scopes;
			for (int i = 0; i < scopes.Count; i++)
			{
				scopes[i].Accept(this);
			}
			return new DataSetPlan(JoinPredicateSubQueryDataSetPlanner.MakePlanName(this.m_dataSetPlan.Name, suffix), 0, this.m_joinPredicatePlanScopes, false, false, null, null, null, null);
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x00027CD0 File Offset: 0x00025ED0
		internal override void Visit(DataShapePlanElement planElement)
		{
			if (!this.IncludeInPlan(planElement))
			{
				return;
			}
			DataShapePlanElement dataShapePlanElement = (DataShapePlanElement)planElement.OmitProjection();
			dataShapePlanElement = new DataShapePlanElement(dataShapePlanElement.DataShape, null, dataShapePlanElement.IsProjected, this.m_subQueryJoinPredicate, null, null, planElement.ValueFilter, planElement.AnyValueFilters, planElement.DefaultValueFilter);
			this.m_joinPredicatePlanScopes.Add(dataShapePlanElement);
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x00027D2C File Offset: 0x00025F2C
		internal override void Visit(DataMemberPlanElement planElement)
		{
			if (!this.IncludeInPlan(planElement))
			{
				return;
			}
			DataMemberPlanElement dataMemberPlanElement = new DataMemberPlanElement(planElement.DataMember, null, false, null, null, null, false, false);
			this.m_joinPredicatePlanScopes.Add(dataMemberPlanElement);
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x00027D64 File Offset: 0x00025F64
		internal override void Visit(DataIntersectionPlanElement planElement)
		{
			if (!this.IncludeInPlan(planElement))
			{
				return;
			}
			DataIntersectionPlanElement dataIntersectionPlanElement = new DataIntersectionPlanElement(planElement.DataIntersection, null, false, null, null);
			this.m_joinPredicatePlanScopes.Add(dataIntersectionPlanElement);
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x00027D97 File Offset: 0x00025F97
		protected override void DefaultVisit(NestedPlanElement planElement)
		{
			Contract.RetailFail("We should not hit nested plan elements as we are not visiting them.");
			throw new InvalidOperationException();
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x00027DA8 File Offset: 0x00025FA8
		private bool IncludeInPlan(ScopePlanElement planElement)
		{
			return this.m_scopeTree.TraverseUp(planElement.Scope, (IScope scope) => scope.ObjectType != ObjectType.DataShape || this.m_scopeTree.GetParentScope(scope) == null);
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x00027DC7 File Offset: 0x00025FC7
		private static string MakePlanName(string inputDataSetPlanName, int suffix)
		{
			return inputDataSetPlanName + "_JoinPredicatePlan" + suffix.ToString();
		}

		// Token: 0x04000500 RID: 1280
		private const string JoinPredicatePlanNameSuffix = "_JoinPredicatePlan";

		// Token: 0x04000501 RID: 1281
		private const int JoinPredicateSubQueryPlanIndex = 0;

		// Token: 0x04000502 RID: 1282
		private readonly DataSetPlan m_dataSetPlan;

		// Token: 0x04000503 RID: 1283
		private readonly ScopeTree m_scopeTree;

		// Token: 0x04000504 RID: 1284
		private readonly FilterCondition m_subQueryJoinPredicate;

		// Token: 0x04000505 RID: 1285
		private readonly List<ScopePlanElement> m_joinPredicatePlanScopes;
	}
}
