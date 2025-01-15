using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExpressionAnalysis;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.Normalization
{
	// Token: 0x02000097 RID: 151
	internal sealed class DataShapeNormalizer : DataShapeVisitor
	{
		// Token: 0x0600070E RID: 1806 RVA: 0x0001AA24 File Offset: 0x00018C24
		private DataShapeNormalizer(ExpressionTable expressionTable, ScopeTree scopeTree, TranslationErrorContext errorContext, BatchSubtotalAnnotations subtotalAnnotations, RestartTokenToStartPositionTranslator startPositionTranslator)
		{
			this.m_expressionTable = expressionTable;
			this.m_scopeTree = scopeTree;
			this.m_errorContext = errorContext;
			this.m_outputExpressionTable = expressionTable.CopyTable();
			this.m_parentDataShapes = new Stack<DataShapeNormalizer.DataShapeInfo>();
			this.m_filtersToElevate = new List<DataShapeNormalizer.FilterElevationInfo>();
			this.m_outputSubtotalAnnotations = new WritableBatchSubtotalAnnotations(subtotalAnnotations);
			this.m_startPositionTranslator = startPositionTranslator;
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x0001AA83 File Offset: 0x00018C83
		private DataShapeNormalizer.DataShapeInfo ParentDataShapeInfo
		{
			get
			{
				return this.m_parentDataShapes.Peek();
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x0001AA90 File Offset: 0x00018C90
		private DataShape ParentDataShape
		{
			get
			{
				return this.ParentDataShapeInfo.DataShape;
			}
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0001AAA0 File Offset: 0x00018CA0
		public static DataShapeNormalizationResult Normalize(DataShape dataShape, ExpressionTable expressionTable, ScopeTree scopeTree, TranslationErrorContext errorContext, BatchSubtotalAnnotations subtotalAnnotations)
		{
			RestartTokenToStartPositionTranslator restartTokenToStartPositionTranslator = DataShapeNormalizer.CreateStartPositionTranslator(dataShape, expressionTable, subtotalAnnotations, errorContext);
			DataShapeNormalizer dataShapeNormalizer = new DataShapeNormalizer(expressionTable, scopeTree, errorContext, subtotalAnnotations, restartTokenToStartPositionTranslator);
			dataShapeNormalizer.Visit(dataShape);
			return new DataShapeNormalizationResult(dataShapeNormalizer.m_outputExpressionTable.AsReadOnly(), dataShapeNormalizer.m_outputSubtotalAnnotations);
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0001AAE4 File Offset: 0x00018CE4
		private static RestartTokenToStartPositionTranslator CreateStartPositionTranslator(DataShape dataShape, ExpressionTable expressionTable, BatchSubtotalAnnotations subtotalAnnotations, TranslationErrorContext errorContext)
		{
			if (dataShape.RestartTokens.IsNullOrEmpty<RestartToken>() && dataShape.Limits.IsNullOrEmpty<Limit>())
			{
				return null;
			}
			if (dataShape.RestartTokens != null)
			{
				List<RestartTokenGroupingBinding> list = NormalizationUtils.BuildRestartGroupingBindings((from m in dataShape.GetRestartMembers(subtotalAnnotations)
					where m.IsDynamic
					select m).ToList<DataMember>(), subtotalAnnotations);
				return RestartTokenToStartPositionTranslator.Create(errorContext, dataShape.RestartTokens, list);
			}
			List<List<RestartToken>> list2 = null;
			List<List<RestartTokenGroupingBinding>> list3 = null;
			foreach (Limit limit in dataShape.Limits)
			{
				WindowLimitOperator windowLimitOperator = limit.Operator as WindowLimitOperator;
				if (windowLimitOperator != null)
				{
					DataShapeNormalizer.AddRestartBindings(limit, windowLimitOperator, expressionTable, subtotalAnnotations, ref list3, ref list2);
				}
			}
			if (list2 == null)
			{
				return null;
			}
			return RestartTokenToStartPositionTranslator.Create(errorContext, list2, list3);
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0001ABCC File Offset: 0x00018DCC
		private static void AddRestartBindings(Limit dataWindow, WindowLimitOperator windowOperator, ExpressionTable expressionTable, BatchSubtotalAnnotations subtotalAnnotations, ref List<List<RestartTokenGroupingBinding>> allGroupingBindings, ref List<List<RestartToken>> allRestartTokens)
		{
			if (windowOperator.RestartTokens.IsNullOrEmpty<RestartToken>())
			{
				return;
			}
			Util.AddToLazyList<List<RestartToken>>(ref allRestartTokens, windowOperator.RestartTokens);
			List<RestartTokenGroupingBinding> list = NormalizationUtils.BuildRestartGroupingBindings(dataWindow.GetGroupScopesFromTargets(expressionTable).GetSegmentationMembers(subtotalAnnotations).ToList<DataMember>(), subtotalAnnotations);
			Util.AddToLazyList<List<RestartTokenGroupingBinding>>(ref allGroupingBindings, list);
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x0001AC15 File Offset: 0x00018E15
		protected override void Enter(DataShape dataShape)
		{
			this.m_parentDataShapes.Push(new DataShapeNormalizer.DataShapeInfo(dataShape));
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0001AC28 File Offset: 0x00018E28
		protected override void Exit(DataShape dataShape)
		{
			this.ElevateFilters();
			this.RemoveGrandTotalRowIfUnnecessary(dataShape);
			this.m_parentDataShapes.Pop();
			if (this.m_parentDataShapes.Count != 0 && dataShape.RestartTokens != null)
			{
				this.m_errorContext.Register(TranslationMessages.RestartTokensOnNestedDataShape(EngineMessageSeverity.Error, ObjectType.DataShape, dataShape.Id, "RestartTokens"));
			}
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x0001AC84 File Offset: 0x00018E84
		protected override void Visit(Filter filter, Identifier dataShapeId)
		{
			FilterNormalizer.Normalize(filter, this.m_scopeTree, this.m_outputExpressionTable);
			ResolvedStructureReferenceExpressionNode resolvedStructureReferenceExpressionNode = (ResolvedStructureReferenceExpressionNode)this.m_expressionTable.GetNode(filter.Target);
			if (FilterAnalyzer.IsScopeFilter(filter, this.m_expressionTable))
			{
				IIdentifiable target = resolvedStructureReferenceExpressionNode.Target;
				if (target.ObjectType == ObjectType.DataShape)
				{
					DataShape dataShape;
					IScope scope;
					if (this.TryGetFirstDynamicParentScopeWithDataShape(target.Id, out dataShape, out scope) && !this.HasScopeFilter(dataShape))
					{
						this.RegisterFilterForElevation(filter, dataShape, scope);
						return;
					}
				}
				else
				{
					this.ParentDataShapeInfo.RegisterScopeFilter(filter);
				}
			}
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x0001AD0C File Offset: 0x00018F0C
		protected override void Enter(DataMember dataMember)
		{
			if (!this.ParentDataShapeInfo.PrimaryMembers.Contains(dataMember))
			{
				return;
			}
			BatchSubtotalAnnotation batchSubtotalAnnotation;
			bool flag = this.m_outputSubtotalAnnotations.TryGetSubtotalSourceAnnotation(dataMember, out batchSubtotalAnnotation);
			this.MapRestartTokenToStartPosition(dataMember, flag);
			if (flag && this.ParentDataShape.HasGroupStartPosition() && !dataMember.SubtotalStartPosition.IsSpecified<bool>())
			{
				dataMember.SubtotalStartPosition = Candidate<bool>.Valid(false);
			}
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0001AD70 File Offset: 0x00018F70
		private void MapRestartTokenToStartPosition(DataMember dataMember, bool isSubtotal)
		{
			if (this.m_startPositionTranslator != null && this.m_parentDataShapes.Count == 1)
			{
				Group group = dataMember.Group;
				if ((dataMember.IsDynamic && group.StartPosition != null) || dataMember.SubtotalStartPosition.IsSpecified<bool>())
				{
					this.m_errorContext.Register(TranslationMessages.StartPositionWithRestartTokens(EngineMessageSeverity.Error, ObjectType.Group, dataMember.Id, "StartPosition"));
					return;
				}
				if (dataMember.IsDynamic)
				{
					group.StartPosition = this.m_startPositionTranslator.GetNextStartPosition(dataMember.Id);
					return;
				}
				if (isSubtotal)
				{
					dataMember.SubtotalStartPosition = this.m_startPositionTranslator.GetNextSubtotalStartPosition(dataMember.Id);
				}
			}
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0001AE14 File Offset: 0x00019014
		private bool TryGetFirstDynamicParentScopeWithDataShape(Identifier id, out DataShape dataShape, out IScope dynamicScope)
		{
			IScope resultScope = null;
			DataShape resultDataShape = null;
			IScope scope2 = this.m_scopeTree.GetScope(id);
			this.m_scopeTree.TraverseUp(scope2, delegate(IScope scope)
			{
				ObjectType objectType = scope.ObjectType;
				if (objectType - ObjectType.DataIntersection <= 1)
				{
					if (resultScope == null)
					{
						resultScope = scope;
					}
					return true;
				}
				if (objectType != ObjectType.DataShape)
				{
					return true;
				}
				if (resultScope != null)
				{
					resultDataShape = (DataShape)scope;
					return false;
				}
				return true;
			});
			dynamicScope = resultScope;
			dataShape = resultDataShape;
			return dynamicScope != null;
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0001AE70 File Offset: 0x00019070
		private void RegisterFilterForElevation(Filter filter, DataShape targetDataShape, IScope targetScope)
		{
			DataShapeNormalizer.FilterElevationInfo filterElevationInfo = new DataShapeNormalizer.FilterElevationInfo(filter, this.ParentDataShape, targetDataShape, targetScope);
			this.m_filtersToElevate.Add(filterElevationInfo);
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x0001AE98 File Offset: 0x00019098
		private void ElevateFilters()
		{
			DataShape dataShape = this.ParentDataShape;
			foreach (Filter filter in from f in this.m_filtersToElevate
				where f.SourceDataShape == dataShape
				select f.Filter)
			{
				dataShape.Filters.Remove(filter);
			}
			foreach (DataShapeNormalizer.FilterElevationInfo filterElevationInfo in this.m_filtersToElevate.Where((DataShapeNormalizer.FilterElevationInfo f) => f.TargetDataShape == dataShape))
			{
				if (dataShape.Filters == null)
				{
					dataShape.Filters = new List<Filter>();
				}
				dataShape.Filters.Add(filterElevationInfo.Filter);
				this.m_outputExpressionTable.SetNode(filterElevationInfo.Filter.Target, new ResolvedScopeReferenceExpressionNode(filterElevationInfo.NewTarget));
			}
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0001AFD8 File Offset: 0x000191D8
		private bool HasScopeFilter(DataShape dataShape)
		{
			return dataShape.Filters != null && dataShape.Filters.Where((Filter f) => FilterAnalyzer.IsScopeFilter(f, this.m_expressionTable)).Any<Filter>();
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0001B000 File Offset: 0x00019200
		private void RemoveGrandTotalRowIfUnnecessary(DataShape dataShape)
		{
			if (!dataShape.HasStartPosition() || dataShape.SecondaryHierarchy.HasDynamic())
			{
				return;
			}
			List<DataMember> list = this.RemoveUnnecessaryMembers(dataShape);
			this.RemoveUnneccessarySubtotalAnnotations(list);
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0001B034 File Offset: 0x00019234
		private List<DataMember> RemoveUnnecessaryMembers(DataShape dataShape)
		{
			List<DataMember> list = dataShape.PrimaryHierarchy.DataMembers.TakeWhile((DataMember m) => !m.IsDynamic && !m.SubtotalStartPosition.GetValueOrDefault<bool>() && !m.HasExplicitSubtotal).ToList<DataMember>();
			if (list.Count == 0)
			{
				return list;
			}
			int num = list.ComputeLeafCount();
			List<DataRow> dataRows = dataShape.DataRows;
			for (int i = 0; i < list.Count; i++)
			{
				this.RemoveUnnecessaryCalculations(list[i]);
			}
			dataShape.PrimaryHierarchy.DataMembers.RemoveRange(0, list.Count);
			if (dataRows != null)
			{
				for (int j = 0; j < num; j++)
				{
					List<DataIntersection> intersections = dataRows[j].Intersections;
					for (int k = 0; k < intersections.Count; k++)
					{
						DataIntersection dataIntersection = intersections[k];
						this.RemoveUnnecessaryCalculations(dataIntersection.Calculations);
					}
				}
				dataRows.RemoveRange(0, num);
			}
			return list;
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x0001B11C File Offset: 0x0001931C
		private void RemoveUnnecessaryCalculations(DataMember member)
		{
			this.RemoveUnnecessaryCalculations(member.Calculations);
			List<DataMember> dataMembers = member.DataMembers;
			if (dataMembers == null)
			{
				return;
			}
			for (int i = 0; i < dataMembers.Count; i++)
			{
				this.RemoveUnnecessaryCalculations(dataMembers[i]);
			}
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x0001B160 File Offset: 0x00019360
		private void RemoveUnnecessaryCalculations(List<Calculation> calculations)
		{
			if (calculations == null || calculations.Count == 0)
			{
				return;
			}
			for (int i = 0; i < calculations.Count; i++)
			{
				this.m_scopeTree.Remove(calculations[i]);
			}
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x0001B19C File Offset: 0x0001939C
		private void RemoveUnneccessarySubtotalAnnotations(List<DataMember> membersRemoved)
		{
			foreach (DataMember dataMember in membersRemoved)
			{
				BatchSubtotalAnnotation batchSubtotalAnnotation;
				if (this.m_outputSubtotalAnnotations.TryGetSubtotalSourceAnnotation(dataMember, out batchSubtotalAnnotation))
				{
					this.m_outputSubtotalAnnotations.RemoveSubtotalSourceAnnotation(dataMember);
					this.m_outputSubtotalAnnotations.RemoveSubtotalAnnotation(batchSubtotalAnnotation);
				}
			}
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0001B20C File Offset: 0x0001940C
		protected override void Exit(DataMember dataMember)
		{
			this.RewriteSortOnSubtotalExpressions(dataMember);
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0001B218 File Offset: 0x00019418
		private void RewriteSortOnSubtotalExpressions(DataMember member)
		{
			if (member.Group == null)
			{
				return;
			}
			if (member.Group.SortKeys != null)
			{
				foreach (SortKey sortKey in member.Group.SortKeys)
				{
					NormalizationUtils.RewriteSubtotalToEvalRollup(member, sortKey.Value, this.m_outputExpressionTable, this.m_scopeTree);
				}
			}
			ScopeIdDefinition scopeIdDefinition = member.Group.ScopeIdDefinition;
			if (scopeIdDefinition != null && scopeIdDefinition.Values != null)
			{
				foreach (ScopeValueDefinition scopeValueDefinition in scopeIdDefinition.Values)
				{
					NormalizationUtils.RewriteSubtotalToEvalRollup(member, scopeValueDefinition.Value, this.m_outputExpressionTable, this.m_scopeTree);
				}
			}
		}

		// Token: 0x0400036C RID: 876
		private readonly ExpressionTable m_expressionTable;

		// Token: 0x0400036D RID: 877
		private readonly WritableExpressionTable m_outputExpressionTable;

		// Token: 0x0400036E RID: 878
		private readonly ScopeTree m_scopeTree;

		// Token: 0x0400036F RID: 879
		private readonly TranslationErrorContext m_errorContext;

		// Token: 0x04000370 RID: 880
		private readonly WritableBatchSubtotalAnnotations m_outputSubtotalAnnotations;

		// Token: 0x04000371 RID: 881
		private readonly RestartTokenToStartPositionTranslator m_startPositionTranslator;

		// Token: 0x04000372 RID: 882
		private readonly Stack<DataShapeNormalizer.DataShapeInfo> m_parentDataShapes;

		// Token: 0x04000373 RID: 883
		private readonly List<DataShapeNormalizer.FilterElevationInfo> m_filtersToElevate;

		// Token: 0x0200029D RID: 669
		private sealed class DataShapeInfo
		{
			// Token: 0x060015AA RID: 5546 RVA: 0x000504B8 File Offset: 0x0004E6B8
			internal DataShapeInfo(DataShape dataShape)
			{
				this.m_dataShape = dataShape;
			}

			// Token: 0x170003DA RID: 986
			// (get) Token: 0x060015AB RID: 5547 RVA: 0x000504C7 File Offset: 0x0004E6C7
			public DataShape DataShape
			{
				get
				{
					return this.m_dataShape;
				}
			}

			// Token: 0x060015AC RID: 5548 RVA: 0x000504CF File Offset: 0x0004E6CF
			public void RegisterScopeFilter(Filter filter)
			{
				if (this.m_scopeFilter == null)
				{
					this.m_scopeFilter = filter;
				}
			}

			// Token: 0x170003DB RID: 987
			// (get) Token: 0x060015AD RID: 5549 RVA: 0x000504E0 File Offset: 0x0004E6E0
			public Filter ScopeFilter
			{
				get
				{
					return this.m_scopeFilter;
				}
			}

			// Token: 0x170003DC RID: 988
			// (get) Token: 0x060015AE RID: 5550 RVA: 0x000504E8 File Offset: 0x0004E6E8
			public IList<DataMember> PrimaryMembers
			{
				get
				{
					if (this.m_primaryMembers == null)
					{
						this.m_primaryMembers = this.m_dataShape.PrimaryHierarchy.GetAllMembersDepthFirst().Evaluate<DataMember>();
					}
					return this.m_primaryMembers;
				}
			}

			// Token: 0x04000A18 RID: 2584
			private readonly DataShape m_dataShape;

			// Token: 0x04000A19 RID: 2585
			private Filter m_scopeFilter;

			// Token: 0x04000A1A RID: 2586
			private IList<DataMember> m_primaryMembers;
		}

		// Token: 0x0200029E RID: 670
		private sealed class FilterElevationInfo
		{
			// Token: 0x060015AF RID: 5551 RVA: 0x00050513 File Offset: 0x0004E713
			internal FilterElevationInfo(Filter filter, DataShape sourceDataShape, DataShape targetDataShape, IScope newTarget)
			{
				this.m_filter = filter;
				this.m_sourceDataShape = sourceDataShape;
				this.m_targetDataShape = targetDataShape;
				this.m_newTarget = newTarget;
			}

			// Token: 0x170003DD RID: 989
			// (get) Token: 0x060015B0 RID: 5552 RVA: 0x00050538 File Offset: 0x0004E738
			public Filter Filter
			{
				get
				{
					return this.m_filter;
				}
			}

			// Token: 0x170003DE RID: 990
			// (get) Token: 0x060015B1 RID: 5553 RVA: 0x00050540 File Offset: 0x0004E740
			public DataShape SourceDataShape
			{
				get
				{
					return this.m_sourceDataShape;
				}
			}

			// Token: 0x170003DF RID: 991
			// (get) Token: 0x060015B2 RID: 5554 RVA: 0x00050548 File Offset: 0x0004E748
			public DataShape TargetDataShape
			{
				get
				{
					return this.m_targetDataShape;
				}
			}

			// Token: 0x170003E0 RID: 992
			// (get) Token: 0x060015B3 RID: 5555 RVA: 0x00050550 File Offset: 0x0004E750
			public IScope NewTarget
			{
				get
				{
					return this.m_newTarget;
				}
			}

			// Token: 0x04000A1B RID: 2587
			private readonly Filter m_filter;

			// Token: 0x04000A1C RID: 2588
			private readonly DataShape m_sourceDataShape;

			// Token: 0x04000A1D RID: 2589
			private readonly DataShape m_targetDataShape;

			// Token: 0x04000A1E RID: 2590
			private readonly IScope m_newTarget;
		}
	}
}
