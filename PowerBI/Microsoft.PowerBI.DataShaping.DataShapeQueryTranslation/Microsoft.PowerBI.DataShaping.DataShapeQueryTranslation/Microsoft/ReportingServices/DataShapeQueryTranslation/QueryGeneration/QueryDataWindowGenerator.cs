using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.InfoNav;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;
using Microsoft.ReportingServices.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration
{
	// Token: 0x0200007C RID: 124
	internal sealed class QueryDataWindowGenerator
	{
		// Token: 0x060005F9 RID: 1529 RVA: 0x00015398 File Offset: 0x00013598
		private QueryDataWindowGenerator(QueryGenerationContext context, List<GroupAndSortingContext> groups, List<QueryLimitConstraintContext> limits, List<KeyValuePair<SortKeyContext, ScalarValue>> startPosition)
		{
			this.m_context = context;
			this.m_groups = groups;
			this.m_limits = limits;
			this.m_startPosition = startPosition;
			this.m_windows = new QueryDataWindowGenerator.QueryWindowConstraintManager(context, groups);
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x000153CA File Offset: 0x000135CA
		public static QueryDataWindowGenerationResult Generate(QueryGenerationContext context, List<GroupAndSortingContext> groups, List<QueryLimitConstraintContext> limits, List<KeyValuePair<SortKeyContext, ScalarValue>> startPosition)
		{
			return new QueryDataWindowGenerator(context, groups, limits, startPosition).Generate();
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x000153DC File Offset: 0x000135DC
		private QueryDataWindowGenerationResult Generate()
		{
			if (this.m_groups.Count == 0)
			{
				return QueryDataWindowGenerationResult.ForConstrained();
			}
			IConceptualSchema conceptualSchema;
			if (!this.m_context.Schema.TryGetDefaultSchema(out conceptualSchema))
			{
				Microsoft.DataShaping.Contract.RetailFail("Fail to get default schema from IFederatedConceptualSchema");
			}
			if (this.HasStartAtOnVariant() && !QueryAlgorithms.CanUseIsOnOrAfter(conceptualSchema))
			{
				return QueryDataWindowGenerationResult.ForUnconstrained(UnconstrainedQueryReasons.StartAtOnVariant);
			}
			bool flag;
			List<QueryDataWindowGenerator.QueryConstraintChain> list = this.BuildConstraintChains(out flag);
			if (flag)
			{
				return QueryDataWindowGenerationResult.ForUnconstrained(UnconstrainedQueryReasons.UnconstrainedGroup);
			}
			if (list == null)
			{
				return QueryDataWindowGenerationResult.ForConstrained();
			}
			QueryDataWindowGenerator.QueryConstraintChain queryConstraintChain = QueryDataWindowGenerator.FindMinimumConstraintChain(list);
			if (queryConstraintChain.HasWindow)
			{
				return QueryDataWindowGenerationResult.ForWindow(new Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.TopLimitOperator(queryConstraintChain.CumulativePaddedCount, null));
			}
			return QueryDataWindowGenerationResult.ForConstrained();
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0001547C File Offset: 0x0001367C
		private bool HasStartAtOnVariant()
		{
			if (this.m_startPosition == null)
			{
				return false;
			}
			for (int i = 0; i < this.m_startPosition.Count; i++)
			{
				if (QueryAlgorithms.IsVariant(this.m_startPosition[i].Key.QueryExpressionContext.QueryExpression))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x000154D4 File Offset: 0x000136D4
		private static QueryDataWindowGenerator.QueryConstraintChain FindMinimumConstraintChain(List<QueryDataWindowGenerator.QueryConstraintChain> chains)
		{
			QueryDataWindowGenerator.QueryConstraintChain queryConstraintChain = chains[0];
			for (int i = 1; i < chains.Count; i++)
			{
				QueryDataWindowGenerator.QueryConstraintChain queryConstraintChain2 = chains[i];
				if (queryConstraintChain2.CumulativeRawCount == queryConstraintChain.CumulativeRawCount && !queryConstraintChain2.HasWindow && queryConstraintChain.HasWindow)
				{
					queryConstraintChain = queryConstraintChain2;
				}
				if (queryConstraintChain2.CumulativeRawCount < queryConstraintChain.CumulativeRawCount)
				{
					queryConstraintChain = queryConstraintChain2;
				}
			}
			return queryConstraintChain;
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00015534 File Offset: 0x00013734
		private List<QueryDataWindowGenerator.QueryConstraintChain> BuildConstraintChains(out bool hasUnconstrainedGroup)
		{
			List<QueryDataWindowGenerator.QueryConstraintChain> list = null;
			bool flag = false;
			for (int i = this.m_groups.Count - 1; i >= 0; i--)
			{
				GroupAndSortingContext groupAndSortingContext = this.m_groups[i];
				if (groupAndSortingContext.IsProjected || flag)
				{
					flag |= groupAndSortingContext.IsProjected;
					HashSet<IQueryConstraint> hashSet = this.FindConstraints(groupAndSortingContext);
					if (hashSet == null)
					{
						hasUnconstrainedGroup = true;
						return null;
					}
					if (list == null)
					{
						list = QueryDataWindowGenerator.BeginConstraintChains(hashSet);
					}
					else
					{
						list = QueryDataWindowGenerator.ExtendConstraintChains(list, hashSet);
					}
				}
			}
			hasUnconstrainedGroup = false;
			return list;
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x000155AC File Offset: 0x000137AC
		private static List<QueryDataWindowGenerator.QueryConstraintChain> BeginConstraintChains(HashSet<IQueryConstraint> constraintsForGroup)
		{
			List<QueryDataWindowGenerator.QueryConstraintChain> list = new List<QueryDataWindowGenerator.QueryConstraintChain>(constraintsForGroup.Count);
			foreach (IQueryConstraint queryConstraint in constraintsForGroup)
			{
				list.Add(new QueryDataWindowGenerator.QueryConstraintChain(queryConstraint));
			}
			return list;
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0001560C File Offset: 0x0001380C
		private static List<QueryDataWindowGenerator.QueryConstraintChain> ExtendConstraintChains(List<QueryDataWindowGenerator.QueryConstraintChain> chains, HashSet<IQueryConstraint> constraintsForGroup)
		{
			List<IQueryConstraint> list = null;
			List<QueryDataWindowGenerator.QueryConstraintChain> list2 = new List<QueryDataWindowGenerator.QueryConstraintChain>(chains.Count);
			for (int i = 0; i < chains.Count; i++)
			{
				QueryDataWindowGenerator.QueryConstraintChain queryConstraintChain = chains[i];
				if (constraintsForGroup.Contains(queryConstraintChain.Constraint))
				{
					list2.Add(queryConstraintChain);
				}
				else
				{
					if (list == null)
					{
						list = constraintsForGroup.Except(chains.Select((QueryDataWindowGenerator.QueryConstraintChain c) => c.Constraint)).ToList<IQueryConstraint>();
					}
					if (list.Count > 0)
					{
						foreach (IQueryConstraint queryConstraint in list)
						{
							list2.Add(queryConstraintChain.Extend(queryConstraint));
						}
					}
				}
			}
			return list2;
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x000156E8 File Offset: 0x000138E8
		private HashSet<IQueryConstraint> FindConstraints(GroupAndSortingContext group)
		{
			HashSet<IQueryConstraint> hashSet = null;
			for (int i = 0; i < this.m_limits.Count; i++)
			{
				QueryLimitConstraintContext queryLimitConstraintContext = this.m_limits[i];
				if ((queryLimitConstraintContext.IsPostRegroup && !group.IsProjected) || group.GroupReferences.IsSubsetOf(queryLimitConstraintContext.Groups))
				{
					Microsoft.DataShaping.Util.AddToLazySet<IQueryConstraint>(ref hashSet, queryLimitConstraintContext, null);
				}
			}
			QueryWindowContext queryWindowContext;
			if (group.IsProjected && this.m_windows.TryGetWindow(group, out queryWindowContext))
			{
				Microsoft.DataShaping.Util.AddToLazySet<IQueryConstraint>(ref hashSet, queryWindowContext, null);
			}
			return hashSet;
		}

		// Token: 0x040002FC RID: 764
		private readonly QueryGenerationContext m_context;

		// Token: 0x040002FD RID: 765
		private readonly List<GroupAndSortingContext> m_groups;

		// Token: 0x040002FE RID: 766
		private readonly List<QueryLimitConstraintContext> m_limits;

		// Token: 0x040002FF RID: 767
		private readonly List<KeyValuePair<SortKeyContext, ScalarValue>> m_startPosition;

		// Token: 0x04000300 RID: 768
		private readonly QueryDataWindowGenerator.QueryWindowConstraintManager m_windows;

		// Token: 0x02000286 RID: 646
		private sealed class QueryWindowConstraintManager
		{
			// Token: 0x06001564 RID: 5476 RVA: 0x0004FDD7 File Offset: 0x0004DFD7
			internal QueryWindowConstraintManager(QueryGenerationContext context, List<GroupAndSortingContext> groups)
			{
				this.m_windows = QueryDataWindowGenerator.QueryWindowConstraintManager.BuildWindowContexts(context, groups);
			}

			// Token: 0x06001565 RID: 5477 RVA: 0x0004FDEC File Offset: 0x0004DFEC
			private static List<QueryWindowContext> BuildWindowContexts(QueryGenerationContext context, List<GroupAndSortingContext> groups)
			{
				if (groups == null)
				{
					return null;
				}
				List<QueryWindowContext> list = null;
				for (int i = 0; i < groups.Count; i++)
				{
					GroupAndSortingContext groupAndSortingContext = groups[i];
					QueryWindowContext queryWindowContext;
					if (context.Annotations.DataMemberAnnotations.IsPrimaryMember(groupAndSortingContext.Scope) && !QueryDataWindowGenerator.QueryWindowConstraintManager.TryGetWindow(groupAndSortingContext, list, out queryWindowContext))
					{
						DataShape containingDataShapeOrSelf = context.ScopeTree.GetContainingDataShapeOrSelf(groupAndSortingContext.Scope);
						if (containingDataShapeOrSelf.RequestedPrimaryLeafCount.IsSpecified<int>())
						{
							queryWindowContext = new QueryWindowContext(containingDataShapeOrSelf);
							Microsoft.DataShaping.Util.AddToLazyList<QueryWindowContext>(ref list, queryWindowContext);
						}
					}
				}
				return list;
			}

			// Token: 0x06001566 RID: 5478 RVA: 0x0004FE70 File Offset: 0x0004E070
			private static bool TryGetWindow(GroupAndSortingContext group, List<QueryWindowContext> windows, out QueryWindowContext window)
			{
				if (windows != null)
				{
					for (int i = 0; i < windows.Count; i++)
					{
						QueryWindowContext queryWindowContext = windows[i];
						if (queryWindowContext.Contains(group))
						{
							window = queryWindowContext;
							return true;
						}
					}
				}
				window = null;
				return false;
			}

			// Token: 0x06001567 RID: 5479 RVA: 0x0004FEAB File Offset: 0x0004E0AB
			internal bool TryGetWindow(GroupAndSortingContext group, out QueryWindowContext window)
			{
				return QueryDataWindowGenerator.QueryWindowConstraintManager.TryGetWindow(group, this.m_windows, out window);
			}

			// Token: 0x040009DB RID: 2523
			private readonly List<QueryWindowContext> m_windows;
		}

		// Token: 0x02000287 RID: 647
		private sealed class QueryConstraintChain
		{
			// Token: 0x06001568 RID: 5480 RVA: 0x0004FEBA File Offset: 0x0004E0BA
			internal QueryConstraintChain(IQueryConstraint constraint)
			{
				this.m_constraint = constraint;
				this.m_rest = null;
			}

			// Token: 0x06001569 RID: 5481 RVA: 0x0004FED0 File Offset: 0x0004E0D0
			private QueryConstraintChain(IQueryConstraint constraint, QueryDataWindowGenerator.QueryConstraintChain rest)
			{
				this.m_constraint = constraint;
				this.m_rest = rest;
			}

			// Token: 0x170003D5 RID: 981
			// (get) Token: 0x0600156A RID: 5482 RVA: 0x0004FEE6 File Offset: 0x0004E0E6
			public IQueryConstraint Constraint
			{
				get
				{
					return this.m_constraint;
				}
			}

			// Token: 0x170003D6 RID: 982
			// (get) Token: 0x0600156B RID: 5483 RVA: 0x0004FEEE File Offset: 0x0004E0EE
			public QueryDataWindowGenerator.QueryConstraintChain Rest
			{
				get
				{
					return this.m_rest;
				}
			}

			// Token: 0x170003D7 RID: 983
			// (get) Token: 0x0600156C RID: 5484 RVA: 0x0004FEF8 File Offset: 0x0004E0F8
			public bool HasWindow
			{
				get
				{
					if (this.m_hasWindow == null)
					{
						this.m_hasWindow = new bool?(this.m_constraint.IsWindow || (this.m_rest != null && this.m_rest.HasWindow));
					}
					return this.m_hasWindow.Value;
				}
			}

			// Token: 0x170003D8 RID: 984
			// (get) Token: 0x0600156D RID: 5485 RVA: 0x0004FF50 File Offset: 0x0004E150
			public int CumulativePaddedCount
			{
				get
				{
					if (this.m_cumulativePaddedCount == null)
					{
						this.m_cumulativePaddedCount = new int?(this.m_constraint.RawCount);
						if (this.m_rest != null)
						{
							this.m_cumulativePaddedCount *= this.m_rest.GetInnerLimitPaddedCount();
						}
						int num = this.m_constraint.PaddedCount - this.m_constraint.RawCount;
						this.m_cumulativePaddedCount += num;
					}
					return this.m_cumulativePaddedCount.Value;
				}
			}

			// Token: 0x0600156E RID: 5486 RVA: 0x0005001C File Offset: 0x0004E21C
			private int GetInnerLimitPaddedCount()
			{
				int num = this.m_constraint.PaddedCount;
				if (this.m_rest != null)
				{
					num *= this.m_rest.GetInnerLimitPaddedCount();
				}
				return num;
			}

			// Token: 0x170003D9 RID: 985
			// (get) Token: 0x0600156F RID: 5487 RVA: 0x0005004C File Offset: 0x0004E24C
			public int CumulativeRawCount
			{
				get
				{
					if (this.m_cumulativeRawCount == null)
					{
						this.m_cumulativeRawCount = new int?(this.m_constraint.RawCount);
						if (this.m_rest != null)
						{
							this.m_cumulativeRawCount *= this.m_rest.CumulativeRawCount;
						}
					}
					return this.m_cumulativeRawCount.Value;
				}
			}

			// Token: 0x06001570 RID: 5488 RVA: 0x000500CA File Offset: 0x0004E2CA
			public QueryDataWindowGenerator.QueryConstraintChain Extend(IQueryConstraint constraint)
			{
				return new QueryDataWindowGenerator.QueryConstraintChain(constraint, this);
			}

			// Token: 0x040009DC RID: 2524
			private readonly IQueryConstraint m_constraint;

			// Token: 0x040009DD RID: 2525
			private readonly QueryDataWindowGenerator.QueryConstraintChain m_rest;

			// Token: 0x040009DE RID: 2526
			private int? m_cumulativePaddedCount;

			// Token: 0x040009DF RID: 2527
			private int? m_cumulativeRawCount;

			// Token: 0x040009E0 RID: 2528
			private bool? m_hasWindow;
		}
	}
}
