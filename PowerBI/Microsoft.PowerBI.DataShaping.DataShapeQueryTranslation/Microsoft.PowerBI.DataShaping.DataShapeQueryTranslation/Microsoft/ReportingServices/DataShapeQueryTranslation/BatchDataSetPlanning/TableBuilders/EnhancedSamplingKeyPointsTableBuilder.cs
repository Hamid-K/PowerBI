using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Common;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders
{
	// Token: 0x020001CA RID: 458
	internal sealed class EnhancedSamplingKeyPointsTableBuilder
	{
		// Token: 0x06001019 RID: 4121 RVA: 0x0004236F File Offset: 0x0004056F
		private EnhancedSamplingKeyPointsTableBuilder(ILimitPlanningContext context, PlanDeclarationCollection declarations, PlanOperationContext coreTable, DataShapeContext dsContext, BatchDataSetPlannerJoinPredicates joinPredicates, DataShapeQueryTranslationTelemetry telemetryInfo)
		{
			this.m_context = context;
			this.m_declarations = declarations;
			this.m_coreTable = coreTable;
			this.m_dsContext = dsContext;
			this.m_joinPredicates = joinPredicates;
			this.m_telemetryInfo = telemetryInfo;
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x000423A4 File Offset: 0x000405A4
		[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "KeyPoints", "LimitMetadataContext", "LimitInfo" })]
		internal static global::System.ValueTuple<KeyPointsTable, PlanOperationContext, PlanLimitInfo> CreateEnhancedSamplingPrimaryOnly(ILimitPlanningContext context, PlanDeclarationCollection declarations, PlanOperationContext bodyTable, DataShapeContext dsContext, BatchDataSetPlannerJoinPredicates joinPredicates, DataShapeQueryTranslationTelemetry telemetryInfo)
		{
			PlanOperationContext planOperationContext;
			PlanLimitInfo planLimitInfo;
			return new global::System.ValueTuple<KeyPointsTable, PlanOperationContext, PlanLimitInfo>(new KeyPointsTable(EnhancedSamplingKeyPointsTableBuilder.LimitKeyPoints(EnhancedSamplingKeyPointsTableBuilder.BuildTable(context, declarations, bodyTable, dsContext, joinPredicates, telemetryInfo), declarations, dsContext, context.OutputExpressionTable, context.ScopeTree, context.Annotations, context.ErrorContext, context.EnhancedSamplingAdditionalKeyPointsRatio, out planOperationContext, out planLimitInfo), declarations), planOperationContext, planLimitInfo);
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x000423F4 File Offset: 0x000405F4
		internal static KeyPointsTable CreatePrimaryEnhancedSamplingFromPrimaryAndSecondary(ILimitPlanningContext context, DataShapeContext dsContext, DataShapeQueryTranslationTelemetry telemetryInfo, PlanDeclarationCollection declarations, PlanOperationContext coreTableOnlyOutputTotals, PlanOperationContext secondaryTable, PlanOperationContext primaryTableWithSortByMeasure, BatchDataSetPlannerJoinPredicates joinPredicates, in DynamicLimitCounts dynamicLimitCounts, out PlanOperationContext bodyTable, out List<IntermediateTelemetryItem> additionalTelemetryItems)
		{
			additionalTelemetryItems = null;
			bodyTable = coreTableOnlyOutputTotals.InnerJoin(secondaryTable);
			bodyTable = bodyTable.DeclareIfNotDeclared(PlanNames.BodyWithLimitedSecondary(dsContext.Id), declarations, false, null, false);
			PlanOperationContext planOperationContext = EnhancedSamplingKeyPointsTableBuilder.BuildTable(context, declarations, bodyTable, dsContext, joinPredicates, telemetryInfo);
			PlanOperationContext planOperationContext2 = null;
			if (planOperationContext != null)
			{
				planOperationContext = planOperationContext.DeclareIfNotDeclared(PlanNames.KeyPoints(dsContext.Id), declarations, false, null, false);
				PlanOperationContext planOperationContext3 = BatchDataSetPlanningUtils.ApplyGroupByHierarchy(context.Annotations, planOperationContext, dsContext.PrimaryDynamicsExcludingContextOnly, dsContext.PrimaryMembersExcludingContextOnly, dsContext.ScopeTree, SubtotalUsage.Output, false, true);
				if (dsContext.HasSortByMeasure)
				{
					planOperationContext3 = primaryTableWithSortByMeasure.InnerJoin(planOperationContext3);
				}
				ExpressionNode expressionNode = ExprNodes.Ceiling(ExprNodes.Literal((double)dsContext.DataShape.DynamicLimits.TargetIntersectionCount.Value * context.EnhancedSamplingAdditionalKeyPointsRatio).Divide(dynamicLimitCounts.TargetSecondaryCount), null);
				expressionNode = expressionNode.DeclareIfNotDeclared(PlanNames.KeyPrimaryLimit(dsContext.Id), declarations, context.ErrorContext, ObjectType.Limit, "KeyPrimaryValuesLimitedCount");
				planOperationContext2 = EnhancedSamplingKeyPointsTableBuilder.LimitKeyPoints(context.Annotations, context.ErrorContext, dsContext, planOperationContext3, expressionNode);
				planOperationContext2 = planOperationContext2.DeclareIfNotDeclared(PlanNames.KeyPrimaryValuesLimited(dsContext.Id), declarations, false, null, false);
				additionalTelemetryItems = new List<IntermediateTelemetryItem>(2);
				ExpressionNode expressionNode2 = planOperationContext2.CountRows();
				expressionNode2 = expressionNode2.DeclareIfNotDeclared(PlanNames.KeyPrimaryLimitedCount(dsContext.Id), declarations, context.ErrorContext, ObjectType.Limit, "KeyPrimaryValuesLimitedCount");
				additionalTelemetryItems.Add(new IntermediateTelemetryItem
				{
					ColumnName = PlanNames.KeyPrimaryLimitedCount(dsContext.Id),
					TelemetryItemName = "KeyPriLtd",
					Value = expressionNode2
				});
				additionalTelemetryItems.Add(new IntermediateTelemetryItem
				{
					ColumnName = PlanNames.KeyPrimaryLimit(dsContext.Id),
					TelemetryItemName = "KeyPriLim",
					Value = expressionNode
				});
			}
			return new KeyPointsTable(planOperationContext2, declarations);
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x000425B8 File Offset: 0x000407B8
		private static PlanOperationContext LimitKeyPoints(PlanOperationContext enhancedSamplingKeyPoints, PlanDeclarationCollection declarations, DataShapeContext dsContext, WritableExpressionTable outputExpressionTable, ScopeTree scopeTree, DataShapeAnnotations annotations, TranslationErrorContext errorContext, double enhancedSamplingAdditionalKeyPointsRatio, out PlanOperationContext limitMetadataContext, out PlanLimitInfo limitInfo)
		{
			limitMetadataContext = null;
			limitInfo = null;
			if (enhancedSamplingKeyPoints == null)
			{
				return null;
			}
			enhancedSamplingKeyPoints = enhancedSamplingKeyPoints.DeclareIfNotDeclared(PlanNames.KeyPoints(dsContext.Id), declarations, false, null, false);
			LiteralExpressionNode literalExpressionNode = ExprNodes.Literal((long)Math.Ceiling((double)dsContext.PrimaryHierarchyLimit.Operator.Count.Value * enhancedSamplingAdditionalKeyPointsRatio));
			enhancedSamplingKeyPoints = EnhancedSamplingKeyPointsTableBuilder.LimitKeyPoints(annotations, errorContext, dsContext, enhancedSamplingKeyPoints, literalExpressionNode);
			enhancedSamplingKeyPoints = enhancedSamplingKeyPoints.DeclareIfNotDeclared(PlanNames.KeyPointsLimited(dsContext.Id), declarations, false, null, false);
			ExpressionNode expressionNode = enhancedSamplingKeyPoints.CountRows();
			expressionNode = expressionNode.DeclareIfNotDeclared(PlanNames.KeyPointsLimitedCount(dsContext.Id), declarations, errorContext, ObjectType.Limit, "KeyPointsLimitedCount");
			LimitMetadataTableBuilder limitMetadataTableBuilder = new LimitMetadataTableBuilder(outputExpressionTable, errorContext);
			ExpressionId expressionId = limitMetadataTableBuilder.AddColumn(PlanNames.KeyPointsLimitedCount(dsContext.Id), expressionNode, ObjectType.DynamicLimits, null);
			limitMetadataContext = limitMetadataTableBuilder.ToTableContext(dsContext.DataShape);
			limitInfo = new PlanLimitInfo(null, new List<LimitTelemetryItem>(1)
			{
				new LimitTelemetryItem("KeyPtsLtd", expressionId)
			});
			return enhancedSamplingKeyPoints;
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x000426B4 File Offset: 0x000408B4
		private static PlanOperationContext BuildTable(ILimitPlanningContext context, PlanDeclarationCollection declarations, PlanOperationContext coreTable, DataShapeContext dsContext, BatchDataSetPlannerJoinPredicates joinPredicates, DataShapeQueryTranslationTelemetry telemetryInfo)
		{
			return new EnhancedSamplingKeyPointsTableBuilder(context, declarations, coreTable, dsContext, joinPredicates, telemetryInfo).BuildTable();
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x000426C8 File Offset: 0x000408C8
		private PlanOperationContext BuildTable()
		{
			this.m_telemetryInfo.UsedKeyPointSampling = true;
			List<EnhancedSamplingKeyPointsTableBuilder.KeyMeasureMapping> list = this.CollectKeyMeasures();
			if (list.IsNullOrEmpty<EnhancedSamplingKeyPointsTableBuilder.KeyMeasureMapping>())
			{
				this.m_telemetryInfo.KeyPointMeasureCount = new int?(0);
				return null;
			}
			this.m_telemetryInfo.KeyPointMeasureCount = new int?(list.Count);
			IList<PlanGroupByItem> list2;
			PlanOperation planOperation = this.CreateKeyValuesTable(list, out list2);
			planOperation = planOperation.DeclareIfNotDeclared(PlanNames.KeyValues(this.m_dsContext.Id), this.m_declarations, false, false, null, false);
			PlanOperation planOperation2 = this.CreateKeyPointsTable(planOperation, list, list2);
			return this.m_coreTable.ReplaceTable(planOperation2, null, null, null);
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x00042760 File Offset: 0x00040960
		private PlanOperation CreateKeyPointsTable(PlanOperation keyValues, List<EnhancedSamplingKeyPointsTableBuilder.KeyMeasureMapping> keyMeasures, IList<PlanGroupByItem> preservedGroupItems)
		{
			PlanOperation planOperation;
			if (preservedGroupItems.Count == 0)
			{
				foreach (EnhancedSamplingKeyPointsTableBuilder.KeyMeasureMapping keyMeasureMapping in keyMeasures)
				{
					keyMeasureMapping.MinReferenceExpr = keyValues.ExtractScalarFromSingleRowTable(keyMeasureMapping.MinColumnName, keyMeasureMapping.MinColumnName, this.m_declarations);
					keyMeasureMapping.MaxReferenceExpr = keyValues.ExtractScalarFromSingleRowTable(keyMeasureMapping.MaxColumnName, keyMeasureMapping.MaxColumnName, this.m_declarations);
				}
				planOperation = this.FilterToKeyPoints(this.m_coreTable.Table, keyMeasures);
			}
			else
			{
				PlanOperation planOperation2 = this.m_coreTable.Table.InnerJoin(keyValues);
				foreach (EnhancedSamplingKeyPointsTableBuilder.KeyMeasureMapping keyMeasureMapping2 in keyMeasures)
				{
					keyMeasureMapping2.MinReferenceExpr = ExprNodes.BatchColumnReference(keyMeasureMapping2.MinColumnName);
					keyMeasureMapping2.MaxReferenceExpr = ExprNodes.BatchColumnReference(keyMeasureMapping2.MaxColumnName);
				}
				planOperation = this.FilterToKeyPoints(planOperation2, keyMeasures);
				planOperation = EnhancedSamplingKeyPointsTableBuilder.RemoveMinMaxColumns(planOperation, keyMeasures);
			}
			return planOperation;
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x0004287C File Offset: 0x00040A7C
		private PlanOperation CreateKeyValuesTable(IReadOnlyList<EnhancedSamplingKeyPointsTableBuilder.KeyMeasureMapping> keyMeasures, out IList<PlanGroupByItem> preservedGroupItems)
		{
			List<PlanAggregateItem> list = this.CreateAggregates(keyMeasures);
			IEnumerable<DataMember> enumerable = this.m_coreTable.GetAllGroups().Except(this.m_dsContext.PrimaryDynamicsExcludingContextOnly);
			preservedGroupItems = enumerable.ToGroupByItems(this.m_context.Annotations, SubtotalUsage.Output, false, true, null).Cast<PlanGroupByItem>().Evaluate<PlanGroupByItem>();
			return this.m_coreTable.Table.GroupBy(preservedGroupItems, list);
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x000428E4 File Offset: 0x00040AE4
		private List<PlanAggregateItem> CreateAggregates(IReadOnlyList<EnhancedSamplingKeyPointsTableBuilder.KeyMeasureMapping> keyMeasures)
		{
			List<PlanAggregateItem> list = new List<PlanAggregateItem>(keyMeasures.Count * 2);
			foreach (EnhancedSamplingKeyPointsTableBuilder.KeyMeasureMapping keyMeasureMapping in keyMeasures)
			{
				ExpressionNode expressionNode = ExprNodes.CurrentGroup(ExprNodes.CalculationReference(keyMeasureMapping.Calculation));
				list.Add(this.CreateAggregateItem(ExprNodes.Min(new ExpressionNode[] { expressionNode }), keyMeasureMapping.MinColumnName));
				list.Add(this.CreateAggregateItem(ExprNodes.Max(new ExpressionNode[] { expressionNode }), keyMeasureMapping.MaxColumnName));
			}
			return list;
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x00042988 File Offset: 0x00040B88
		private PlanAggregateExpressionItem CreateAggregateItem(ExpressionNode expr, string columnName)
		{
			ExpressionId expressionId = this.m_context.OutputExpressionTable.Add(expr);
			ExpressionContext expressionContext = new ExpressionContext(this.m_context.ErrorContext, ObjectType.Limit, columnName, columnName);
			return new PlanAggregateExpressionItem(columnName, expressionId, expressionContext, false);
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x000429CC File Offset: 0x00040BCC
		private PlanOperation FilterToKeyPoints(PlanOperation taggedPoints, List<EnhancedSamplingKeyPointsTableBuilder.KeyMeasureMapping> keyMeasures)
		{
			ExpressionNode expressionNode = null;
			bool flag = keyMeasures.Count > 1 || !keyMeasures[0].IsJoinPredicate || this.m_dsContext.HasShowItemsWithNoData;
			foreach (EnhancedSamplingKeyPointsTableBuilder.KeyMeasureMapping keyMeasureMapping in keyMeasures)
			{
				ResolvedCalculationReferenceExpressionNode resolvedCalculationReferenceExpressionNode = ExprNodes.CalculationReference(keyMeasureMapping.Calculation);
				ExpressionNode minReferenceExpr = keyMeasureMapping.MinReferenceExpr;
				ExpressionNode maxReferenceExpr = keyMeasureMapping.MaxReferenceExpr;
				ExpressionNode expressionNode2 = resolvedCalculationReferenceExpressionNode.Equal(minReferenceExpr);
				ExpressionNode expressionNode3 = resolvedCalculationReferenceExpressionNode.Equal(maxReferenceExpr);
				ExpressionNode expressionNode4 = expressionNode2.Or(expressionNode3);
				UnaryOperatorExpressionNode unaryOperatorExpressionNode = (flag ? ExprNodes.IsNull(resolvedCalculationReferenceExpressionNode).Not() : null);
				UnaryOperatorExpressionNode unaryOperatorExpressionNode2 = ExprNodes.IsZero(resolvedCalculationReferenceExpressionNode, !flag).Not();
				UnaryOperatorExpressionNode unaryOperatorExpressionNode3 = minReferenceExpr.Equal(maxReferenceExpr).Not();
				ExpressionNode expressionNode5 = expressionNode4.And(unaryOperatorExpressionNode3).And(unaryOperatorExpressionNode).And(unaryOperatorExpressionNode2);
				expressionNode = expressionNode.Or(expressionNode5);
			}
			ExpressionContext expressionContext = new ExpressionContext(this.m_context.ErrorContext, ObjectType.Limit, "Sample", "IsKeyPoint");
			PlanExpression planExpression = new PlanExpression(expressionNode, expressionContext);
			return taggedPoints.FilterBy(planExpression);
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x00042B04 File Offset: 0x00040D04
		private static PlanOperation RemoveMinMaxColumns(PlanOperation keyPoints, List<EnhancedSamplingKeyPointsTableBuilder.KeyMeasureMapping> keyMeasures)
		{
			List<string> list = new List<string>(keyMeasures.Count * 2);
			foreach (EnhancedSamplingKeyPointsTableBuilder.KeyMeasureMapping keyMeasureMapping in keyMeasures)
			{
				list.Add(keyMeasureMapping.MinColumnName);
				list.Add(keyMeasureMapping.MaxColumnName);
			}
			PlanPreserveAllColumnsExceptProjectItem planPreserveAllColumnsExceptProjectItem = new PlanPreserveAllColumnsExceptProjectItem(list);
			keyPoints = keyPoints.Project(new PlanProjectItem[] { planPreserveAllColumnsExceptProjectItem }, false);
			return keyPoints;
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x00042B8C File Offset: 0x00040D8C
		private List<EnhancedSamplingKeyPointsTableBuilder.KeyMeasureMapping> CollectKeyMeasures()
		{
			IReadOnlyList<Calculation> calculations = this.m_joinPredicates.Calculations;
			IReadOnlyList<Calculation> readOnlyList;
			if (calculations == null)
			{
				readOnlyList = null;
			}
			else
			{
				readOnlyList = calculations.Where((Calculation calc) => !calc.IsContextOnly).ToList<Calculation>();
			}
			IReadOnlyList<Calculation> readOnlyList2 = readOnlyList;
			IReadOnlyList<Calculation> readOnlyList3 = readOnlyList2 ?? Array.Empty<Calculation>();
			IReadOnlyList<Calculation> readOnlyList4 = this.CollectCandidateVisualCalcs();
			int num = readOnlyList3.Count + readOnlyList4.Count;
			if (num == 0)
			{
				return null;
			}
			HashSet<ExpressionNode> hashSet = new HashSet<ExpressionNode>();
			NamingContext namingContext = new NamingContext(null);
			List<EnhancedSamplingKeyPointsTableBuilder.KeyMeasureMapping> list = new List<EnhancedSamplingKeyPointsTableBuilder.KeyMeasureMapping>(num);
			foreach (Calculation calculation in readOnlyList3)
			{
				EnhancedSamplingKeyPointsTableBuilder.KeyMeasureMapping keyMeasureMapping;
				if (this.TryCreateKeyMeasureMapping(calculation, true, hashSet, namingContext, out keyMeasureMapping))
				{
					list.Add(keyMeasureMapping);
				}
			}
			foreach (Calculation calculation2 in readOnlyList4)
			{
				EnhancedSamplingKeyPointsTableBuilder.KeyMeasureMapping keyMeasureMapping2;
				if (this.TryCreateKeyMeasureMapping(calculation2, false, hashSet, namingContext, out keyMeasureMapping2))
				{
					list.Add(keyMeasureMapping2);
				}
			}
			return list;
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x00042CB8 File Offset: 0x00040EB8
		private bool TryCreateKeyMeasureMapping(Calculation calculation, bool isJoinPredicate, HashSet<ExpressionNode> addedExpressions, NamingContext namingContext, out EnhancedSamplingKeyPointsTableBuilder.KeyMeasureMapping mapping)
		{
			mapping = null;
			if (!EnhancedSamplingKeyPointsTableBuilder.IsEligibleKeyMeasure(calculation, this.m_context.OutputExpressionTable))
			{
				return false;
			}
			ExpressionNode node = this.m_context.OutputExpressionTable.GetNode(calculation.Value);
			if (!addedExpressions.Add(node))
			{
				return false;
			}
			mapping = new EnhancedSamplingKeyPointsTableBuilder.KeyMeasureMapping
			{
				Calculation = calculation
			};
			mapping.IsJoinPredicate = isJoinPredicate;
			mapping.MinColumnName = namingContext.GenerateUniqueName(calculation.Id.Value + "Min");
			mapping.MaxColumnName = namingContext.GenerateUniqueName(calculation.Id.Value + "Max");
			return true;
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x00042D64 File Offset: 0x00040F64
		internal static bool IsEligibleKeyMeasure(Calculation calc, ExpressionTable inputExprTable)
		{
			ResolvedPropertyExpressionNode resolvedPropertyExpressionNode = inputExprTable.GetNode(calc.Value) as ResolvedPropertyExpressionNode;
			if (resolvedPropertyExpressionNode != null)
			{
				IConceptualMeasure conceptualMeasure = resolvedPropertyExpressionNode.Property as IConceptualMeasure;
				if (conceptualMeasure != null)
				{
					ConceptualPrimitiveType conceptualDataType = conceptualMeasure.ConceptualDataType;
					if (!conceptualDataType.IsNumeric() && !conceptualDataType.IsDateTime())
					{
						return false;
					}
					if (conceptualMeasure.IsVariant)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x00042DBC File Offset: 0x00040FBC
		private IReadOnlyList<Calculation> CollectCandidateVisualCalcs()
		{
			if (!this.m_context.FeatureSwitches.IsEnabled(FeatureSwitchKind.VisualCalculations))
			{
				return Microsoft.DataShaping.Util.EmptyReadOnlyList<Calculation>();
			}
			DataShapeAnnotations annotations = this.m_context.Annotations;
			return this.m_coreTable.Calculations.Where((Calculation c) => annotations.IsVisualCalculation(c)).ToList<Calculation>();
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x00042E1C File Offset: 0x0004101C
		internal static PlanOperationContext LimitKeyPoints(DataShapeAnnotations annotations, TranslationErrorContext errorContext, DataShapeContext dsContext, PlanOperationContext keyPoints, ExpressionNode count)
		{
			ExpressionContext expressionContext = new ExpressionContext(errorContext, ObjectType.Limit, dsContext.Id, "KeyPointsLimitedCount");
			PlanExpression planExpression = new PlanExpression(count, expressionContext);
			IEnumerable<PlanSortItem> enumerable = keyPoints.GetAllGroups().ToSortItems(annotations, true);
			return keyPoints.TopN(planExpression, enumerable, false);
		}

		// Token: 0x04000784 RID: 1924
		private readonly ILimitPlanningContext m_context;

		// Token: 0x04000785 RID: 1925
		private readonly PlanDeclarationCollection m_declarations;

		// Token: 0x04000786 RID: 1926
		private readonly PlanOperationContext m_coreTable;

		// Token: 0x04000787 RID: 1927
		private readonly DataShapeContext m_dsContext;

		// Token: 0x04000788 RID: 1928
		private readonly BatchDataSetPlannerJoinPredicates m_joinPredicates;

		// Token: 0x04000789 RID: 1929
		private readonly DataShapeQueryTranslationTelemetry m_telemetryInfo;

		// Token: 0x02000310 RID: 784
		private class KeyMeasureMapping
		{
			// Token: 0x17000418 RID: 1048
			// (get) Token: 0x06001735 RID: 5941 RVA: 0x0005286C File Offset: 0x00050A6C
			// (set) Token: 0x06001736 RID: 5942 RVA: 0x00052874 File Offset: 0x00050A74
			internal Calculation Calculation { get; set; }

			// Token: 0x17000419 RID: 1049
			// (get) Token: 0x06001737 RID: 5943 RVA: 0x0005287D File Offset: 0x00050A7D
			// (set) Token: 0x06001738 RID: 5944 RVA: 0x00052885 File Offset: 0x00050A85
			internal string MinColumnName { get; set; }

			// Token: 0x1700041A RID: 1050
			// (get) Token: 0x06001739 RID: 5945 RVA: 0x0005288E File Offset: 0x00050A8E
			// (set) Token: 0x0600173A RID: 5946 RVA: 0x00052896 File Offset: 0x00050A96
			internal string MaxColumnName { get; set; }

			// Token: 0x1700041B RID: 1051
			// (get) Token: 0x0600173B RID: 5947 RVA: 0x0005289F File Offset: 0x00050A9F
			// (set) Token: 0x0600173C RID: 5948 RVA: 0x000528A7 File Offset: 0x00050AA7
			internal ExpressionNode MinReferenceExpr { get; set; }

			// Token: 0x1700041C RID: 1052
			// (get) Token: 0x0600173D RID: 5949 RVA: 0x000528B0 File Offset: 0x00050AB0
			// (set) Token: 0x0600173E RID: 5950 RVA: 0x000528B8 File Offset: 0x00050AB8
			internal ExpressionNode MaxReferenceExpr { get; set; }

			// Token: 0x1700041D RID: 1053
			// (get) Token: 0x0600173F RID: 5951 RVA: 0x000528C1 File Offset: 0x00050AC1
			// (set) Token: 0x06001740 RID: 5952 RVA: 0x000528C9 File Offset: 0x00050AC9
			internal bool IsJoinPredicate { get; set; }
		}
	}
}
