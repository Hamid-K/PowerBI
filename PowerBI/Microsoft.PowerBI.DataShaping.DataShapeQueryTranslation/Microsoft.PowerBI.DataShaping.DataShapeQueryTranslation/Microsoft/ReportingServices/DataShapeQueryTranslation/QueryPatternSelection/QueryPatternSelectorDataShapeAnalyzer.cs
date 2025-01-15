using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.ReportingServices.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.QueryPatternSelection
{
	// Token: 0x02000070 RID: 112
	internal sealed class QueryPatternSelectorDataShapeAnalyzer : DataShapeVisitor
	{
		// Token: 0x060005AE RID: 1454 RVA: 0x00014204 File Offset: 0x00012404
		private QueryPatternSelectorDataShapeAnalyzer(DataShapeContext dsContext, QueryPatternSelectionContext patternSelectionContext, QueryPatternReasonCollection reasons)
		{
			this.m_topLevelDsContext = dsContext;
			this.m_patternSelectionContext = patternSelectionContext;
			this.m_reasons = reasons;
			this.m_groupKeys = new List<GroupKey>();
			this.m_filterVisitor = new QueryPatternSelectorFilterVisitor(this.m_patternSelectionContext, this.m_reasons);
			this.m_dsContexts = new Stack<DataShapeContext>();
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x00014259 File Offset: 0x00012459
		public DataShapeContext CurrentDataShapeContext
		{
			get
			{
				return this.m_dsContexts.Peek();
			}
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00014266 File Offset: 0x00012466
		public static void Analyze(DataShapeContext dsContext, QueryPatternSelectionContext patternSelectionContext, QueryPatternReasonCollection reasons)
		{
			new QueryPatternSelectorDataShapeAnalyzer(dsContext, patternSelectionContext, reasons).Analyze();
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00014275 File Offset: 0x00012475
		private void Analyze()
		{
			this.Visit(this.m_topLevelDsContext.DataShape);
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00014288 File Offset: 0x00012488
		protected override void TraverseDataShapeStructure(DataShape dataShape)
		{
			base.Visit(dataShape.PrimaryHierarchy);
			base.Visit(dataShape.SecondaryHierarchy);
			base.Visit<DataRow>(dataShape.DataRows, new Action<DataRow>(base.Visit));
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x000142BC File Offset: 0x000124BC
		protected override void Enter(DataShape dataShape)
		{
			DataShapeContext dataShapeContext = this.GetDataShapeContext(dataShape);
			this.m_dsContexts.Push(dataShapeContext);
			this.m_reasons.CheckBatchPrerequisite(this.IsDataShapeAllowed(dataShape), QueryPatternReason.NestedDataShape);
			if (dataShape.PrimaryHierarchy != null)
			{
				this.CheckPeerPrimaryMembers(dataShape.PrimaryHierarchy.DataMembers);
			}
			if (dataShape.SecondaryHierarchy != null)
			{
				this.CheckPeerSecondaryMembers(dataShape.SecondaryHierarchy.DataMembers);
			}
			if (this.m_patternSelectionContext.ApplyTransformsInQuery && this.m_patternSelectionContext.Annotations.HasOrContainsTransforms)
			{
				this.m_reasons.RegisterBatchPatternOnlyReason(QueryPatternReason.InlineDataTransform);
			}
			if (dataShapeContext.HasInstanceFilters)
			{
				this.m_reasons.RegisterBatchPatternOnlyReason(QueryPatternReason.InstanceFilter);
			}
			if (dataShape.Usage == DataShapeUsage.Synchronization)
			{
				this.m_reasons.RegisterBatchPatternOnlyReason(QueryPatternReason.GroupSynchronization);
			}
			RestartMatchingBehavior? restartMatchingBehavior = dataShape.RestartMatchingBehavior;
			RestartMatchingBehavior restartMatchingBehavior2 = RestartMatchingBehavior.IsAfter;
			if ((restartMatchingBehavior.GetValueOrDefault() == restartMatchingBehavior2) & (restartMatchingBehavior != null))
			{
				this.m_reasons.RegisterBatchPatternOnlyReason(QueryPatternReason.IsAfter);
			}
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x000143A2 File Offset: 0x000125A2
		private DataShapeContext GetDataShapeContext(DataShape ds)
		{
			if (this.m_topLevelDsContext.DataShape != ds)
			{
				return this.m_topLevelDsContext.GetNestedDataShapeContext(ds);
			}
			return this.m_topLevelDsContext;
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x000143C5 File Offset: 0x000125C5
		protected override void Exit(DataShape dataShape)
		{
			DataShape dataShape2 = this.m_dsContexts.Pop().DataShape;
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x000143D8 File Offset: 0x000125D8
		private bool IsDataShapeAllowed(DataShape dataShape)
		{
			if (dataShape.IsIndependent)
			{
				return true;
			}
			if (this.m_patternSelectionContext.ScopeTree.AreSameScope(this.m_topLevelDsContext.DataShape, dataShape))
			{
				return true;
			}
			IScope parentScope = this.m_patternSelectionContext.ScopeTree.GetParentScope(dataShape);
			return this.m_patternSelectionContext.ScopeTree.AreSameScope(this.m_topLevelDsContext.DataShape, parentScope) && (dataShape.ContextOnly.GetValueOrDefault<bool>() || dataShape.Usage == DataShapeUsage.Synchronization);
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00014458 File Offset: 0x00012658
		protected override void Visit(Limit limit, DataShape dataShape)
		{
			IScope resolvedScope = limit.Within.GetResolvedScope(this.m_patternSelectionContext.ExpressionTable);
			this.m_reasons.CheckBatchPrerequisite(this.m_patternSelectionContext.ScopeTree.AreSameScope(resolvedScope, dataShape), QueryPatternReason.LimitWithin);
			IScope resolvedScope2 = limit.GetInnermostTarget().GetResolvedScope(this.m_patternSelectionContext.ExpressionTable);
			DataMember dataMember = resolvedScope2 as DataMember;
			if (dataMember != null)
			{
				if (!this.IsInnermostDynamicInHierarchy(dataMember))
				{
					this.m_reasons.RegisterBatchPatternOnlyReason(QueryPatternReason.ScopedLimit);
				}
			}
			else
			{
				DataIntersection dataIntersection = resolvedScope2 as DataIntersection;
				if (dataIntersection != null && this.CurrentDataShapeContext.InnermostScopeExcludingContextOnly != dataIntersection)
				{
					this.m_reasons.RegisterBatchPatternOnlyReason(QueryPatternReason.ScopedLimit);
				}
			}
			if (limit.Operator is BinnedLineSampleLimitOperator)
			{
				this.m_reasons.RegisterBatchPatternOnlyReason(QueryPatternReason.BinnedLineSampleLimit);
			}
			if (limit.Operator is OverlappingPointsSampleLimitOperator)
			{
				this.m_reasons.RegisterBatchPatternOnlyReason(QueryPatternReason.OverlappingPointsSampleLimit);
			}
			if (limit.Operator is TopNPerLevelLimitOperator)
			{
				this.m_reasons.RegisterBatchPatternOnlyReason(QueryPatternReason.TopNPerLevelLimit);
			}
			WindowLimitOperator windowLimitOperator = limit.Operator as WindowLimitOperator;
			if (windowLimitOperator != null)
			{
				RestartMatchingBehavior? restartMatchingBehavior = windowLimitOperator.RestartMatchingBehavior;
				RestartMatchingBehavior restartMatchingBehavior2 = RestartMatchingBehavior.IsAfter;
				if ((restartMatchingBehavior.GetValueOrDefault() == restartMatchingBehavior2) & (restartMatchingBehavior != null))
				{
					this.m_reasons.RegisterBatchPatternOnlyReason(QueryPatternReason.IsAfter);
				}
			}
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00014585 File Offset: 0x00012785
		private bool IsInnermostDynamicInHierarchy(DataMember member)
		{
			return !member.GetAllDynamicMembers().Skip(1).Any<DataMember>();
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x0001459C File Offset: 0x0001279C
		protected override void Visit(DataMember dataMember)
		{
			DataMember currentMember = this.m_currentMember;
			this.m_currentMember = dataMember;
			if (dataMember.ContextOnly)
			{
				this.m_reasons.RegisterBatchPatternOnlyReason(QueryPatternReason.ContextOnlyDataMember);
			}
			if (dataMember.Group != null)
			{
				this.CheckSortKeys(dataMember);
				if (!this.m_patternSelectionContext.Annotations.IsPrimaryMember(dataMember))
				{
					if (this.CurrentDataShapeContext.HasAnyPrimaryDynamic)
					{
						this.CheckMeasuresAboveInnermostScope(dataMember, false);
					}
				}
				else if (this.CurrentDataShapeContext.HasAnySecondaryDynamic)
				{
					this.CheckMeasuresAboveInnermostScope(dataMember, true);
				}
			}
			if (this.m_patternSelectionContext.Annotations.IsPrimaryMember(dataMember))
			{
				this.CheckPeerPrimaryMembers(dataMember.DataMembers);
			}
			else
			{
				this.CheckPeerSecondaryMembers(dataMember.DataMembers);
			}
			base.Visit(dataMember);
			this.m_currentMember = currentMember;
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00014658 File Offset: 0x00012858
		private void CheckMeasuresAboveInnermostScope(DataMember dataMember, bool allowedOnDataMember)
		{
			if (!this.m_patternSelectionContext.ScopeTree.AreSameScope(dataMember, this.CurrentDataShapeContext.InnermostScope))
			{
				foreach (Calculation calculation in this.m_patternSelectionContext.ScopeTree.GetItems<Calculation>(dataMember.Id))
				{
					if (this.m_patternSelectionContext.Annotations.IsMeasure(calculation))
					{
						if (allowedOnDataMember)
						{
							this.m_reasons.CheckBatchPrerequisite(dataMember.Calculations != null && dataMember.Calculations.Contains(calculation), QueryPatternReason.UnsupportedMeasureOutsideInnermostScope);
						}
						else
						{
							this.m_reasons.RegisterSingleResultTablePatternOnlyReason(QueryPatternReason.UnsupportedMeasureOutsideInnermostScope);
						}
					}
				}
			}
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0001471C File Offset: 0x0001291C
		private void CheckPeerPrimaryMembers(List<DataMember> members)
		{
			int num;
			int num2;
			QueryPatternSelectorDataShapeAnalyzer.CountMembers(members, out num, out num2);
			bool flag = num <= 1 && num2 <= 1;
			this.m_reasons.CheckBatchPrerequisite(flag, QueryPatternReason.PeerPrimaryMembers);
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00014754 File Offset: 0x00012954
		private void CheckPeerSecondaryMembers(List<DataMember> members)
		{
			int num;
			int num2;
			QueryPatternSelectorDataShapeAnalyzer.CountMembers(members, out num, out num2);
			bool flag = num <= 1;
			this.m_reasons.CheckBatchPrerequisite(flag, QueryPatternReason.PeerSecondaryMembers);
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00014784 File Offset: 0x00012984
		private static void CountMembers(List<DataMember> members, out int dynamicCount, out int staticCount)
		{
			dynamicCount = 0;
			staticCount = 0;
			if (members == null || members.Count == 0)
			{
				return;
			}
			for (int i = 0; i < members.Count; i++)
			{
				if (members[i].IsDynamic)
				{
					dynamicCount++;
				}
				else
				{
					staticCount++;
				}
			}
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x000147D0 File Offset: 0x000129D0
		private void CheckSortKeys(DataMember member)
		{
			Group group = member.Group;
			if (group.SortKeys == null)
			{
				return;
			}
			bool flag = this.m_patternSelectionContext.ScopeTree.AreSameScope(member, this.CurrentDataShapeContext.InnermostScope) || this.m_patternSelectionContext.Annotations.IsPrimaryMember(member);
			foreach (SortKey sortKey in group.SortKeys)
			{
				if (flag)
				{
					this.Visit(sortKey.Value, member, true, true, true);
				}
				else
				{
					ExpressionNode node = this.m_patternSelectionContext.ExpressionTable.GetNode(sortKey.Value);
					this.m_reasons.CheckBatchPrerequisite(node.Kind == ExpressionNodeKind.ResolvedProperty, QueryPatternReason.ComplexSortExpression);
				}
			}
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x000148A4 File Offset: 0x00012AA4
		protected override void Visit(Calculation calculation)
		{
			if (this.m_patternSelectionContext.Annotations.IsVisualCalculation(calculation))
			{
				this.m_reasons.RegisterBatchPatternOnlyReason(QueryPatternReason.VisualCalculation);
			}
			if (calculation.IsContextOnly)
			{
				this.m_reasons.RegisterBatchPatternOnlyReason(QueryPatternReason.ContextOnlyCalculation);
			}
			bool flag = this.m_patternSelectionContext.Annotations.IsSubtotal(calculation);
			IScope containingScope = this.m_patternSelectionContext.ScopeTree.GetContainingScope(calculation);
			bool flag2 = containingScope == this.m_topLevelDsContext.DataShape;
			bool flag3 = flag && this.IsSubtotalSupported(calculation);
			bool flag4 = containingScope == this.m_topLevelDsContext.DataShape;
			this.Visit(calculation.Value, containingScope, flag2, flag3, flag4);
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00014944 File Offset: 0x00012B44
		private void Visit(Expression expression, IScope containingScope, bool allowEvaluate, bool allowSubtotal, bool allowRollup)
		{
			ExpressionNode node = this.m_patternSelectionContext.ExpressionTable.GetNode(expression);
			bool flag2;
			bool flag = QueryPatternSelectorExpressionVisitor.IsValidForBatchQueryPattern(this.m_patternSelectionContext, node, allowEvaluate, allowRollup, allowSubtotal, containingScope, out flag2);
			this.m_reasons.CheckBatchPrerequisite(flag, QueryPatternReason.ExpressionFeature);
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00014988 File Offset: 0x00012B88
		private bool IsSubtotalSupported(Calculation calculation)
		{
			DataShape containingDataShape = this.m_patternSelectionContext.Annotations.GetContainingDataShape(calculation);
			bool flag = this.m_reasons.CheckBatchPrerequisite(containingDataShape == this.m_topLevelDsContext.DataShape, QueryPatternReason.NestedTotal);
			DataShapeContext topLevelDsContext = this.m_topLevelDsContext;
			IIdentifiable parent = this.m_patternSelectionContext.Annotations.GetParent(calculation);
			bool flag2 = parent.ObjectType == ObjectType.DataIntersection;
			bool flag3 = parent.ObjectType == ObjectType.DataMember && !((DataMember)parent).IsDynamic;
			IScope containingScope = this.m_patternSelectionContext.ScopeTree.GetContainingScope(calculation);
			DataShapeQueryTranslationUtils.SubtotalKind subtotalKind = DataShapeQueryTranslationUtils.DetermineSubtotalKind(topLevelDsContext, this.m_patternSelectionContext.Annotations, this.m_patternSelectionContext.ScopeTree, containingScope);
			if (topLevelDsContext.PrimaryDynamics.Count == 1 && !topLevelDsContext.HasAnySecondaryDynamic)
			{
				flag &= this.m_reasons.CheckBatchPrerequisite(subtotalKind == DataShapeQueryTranslationUtils.SubtotalKind.GrandTotal, QueryPatternReason.SubtotalPosition);
				flag &= this.m_reasons.CheckBatchPrerequisite(flag2 || flag3, QueryPatternReason.SubtotalPosition);
			}
			else if (topLevelDsContext.HasAnyPrimaryDynamic && !topLevelDsContext.HasAnySecondaryDynamic)
			{
				flag &= this.m_reasons.CheckBatchPrerequisite(flag2 || flag3, QueryPatternReason.SubtotalPosition);
			}
			else if (!topLevelDsContext.HasAnyPrimaryDynamic && topLevelDsContext.HasAnySecondaryDynamic)
			{
				flag &= this.m_reasons.CheckBatchPrerequisite(flag2, QueryPatternReason.SubtotalPosition);
			}
			else
			{
				flag &= this.m_reasons.CheckBatchPrerequisite(flag2, QueryPatternReason.SubtotalPosition);
			}
			return flag;
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00014AD8 File Offset: 0x00012CD8
		protected override void Visit(Filter filter, Identifier dataShapeId)
		{
			if (this.m_patternSelectionContext.Annotations.IsValueFilter(filter))
			{
				IScope resolvedScope = filter.Target.GetResolvedScope(this.m_patternSelectionContext.ExpressionTable);
				this.m_reasons.CheckBatchPrerequisite(this.IsValidValueFilterTarget(resolvedScope), QueryPatternReason.ValueFilterTarget);
			}
			this.m_filterVisitor.Analyze(filter);
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00014B30 File Offset: 0x00012D30
		private bool IsValidValueFilterTarget(IScope target)
		{
			if (target is DataIntersection)
			{
				return true;
			}
			DataMember dataMember = target as DataMember;
			return (dataMember != null && this.IsInnermostDynamicInHierarchy(dataMember)) || target is DataShape;
		}

		// Token: 0x040002D7 RID: 727
		private readonly DataShapeContext m_topLevelDsContext;

		// Token: 0x040002D8 RID: 728
		private readonly QueryPatternSelectionContext m_patternSelectionContext;

		// Token: 0x040002D9 RID: 729
		private readonly QueryPatternReasonCollection m_reasons;

		// Token: 0x040002DA RID: 730
		private readonly QueryPatternSelectorFilterVisitor m_filterVisitor;

		// Token: 0x040002DB RID: 731
		private readonly List<GroupKey> m_groupKeys;

		// Token: 0x040002DC RID: 732
		private readonly Stack<DataShapeContext> m_dsContexts;

		// Token: 0x040002DD RID: 733
		private DataMember m_currentMember;
	}
}
