using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.DataShapeValidation;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExpressionAnalysis;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.ModelReconciliation
{
	// Token: 0x020000A1 RID: 161
	internal sealed class ModelReconciler : DataShapeVisitor
	{
		// Token: 0x0600076B RID: 1899 RVA: 0x0001CB10 File Offset: 0x0001AD10
		private ModelReconciler(IFederatedConceptualSchema schema, ScopeTree scopeTree, IdentifierValidator idValidator, ExpressionTable inputExpressionTable, TranslationErrorContext errorContext, IFeatureSwitchProvider featureSwitchProvider)
		{
			this.m_expressionReconciler = new ExpressionReconciler(schema, scopeTree, idValidator, inputExpressionTable, errorContext, featureSwitchProvider);
			this.m_limitTargetValidator = new LimitExpressionValidator(this.ExpressionTable, scopeTree, errorContext);
			this.m_scopeTree = scopeTree;
			this.m_errorContext = errorContext;
			this.m_batchSubtotalAnnotationAnalyzer = new BatchSubtotalAnnotationAnalyzer(scopeTree);
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600076C RID: 1900 RVA: 0x0001CB66 File Offset: 0x0001AD66
		public WritableExpressionTable ExpressionTable
		{
			get
			{
				return this.m_expressionReconciler.ExpressionTable;
			}
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x0001CB74 File Offset: 0x0001AD74
		public static ModelReconciliationResult Reconcile(DataShape dataShape, ExpressionTable inputExpressionTable, IFederatedConceptualSchema schema, ScopeTree scopeTree, IdentifierValidator idValidator, TranslationErrorContext errorContext, IFeatureSwitchProvider featureSwitchProvider)
		{
			ModelReconciler modelReconciler = new ModelReconciler(schema, scopeTree, idValidator, inputExpressionTable, errorContext, featureSwitchProvider);
			modelReconciler.Visit(dataShape);
			return new ModelReconciliationResult(modelReconciler.ExpressionTable.AsReadOnly(), modelReconciler.m_batchSubtotalAnnotationAnalyzer.SubtotalAnnotations);
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0001CBB2 File Offset: 0x0001ADB2
		protected override void Enter(DataShape dataShape)
		{
			this.m_batchSubtotalAnnotationAnalyzer.EnterDataShape(dataShape);
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0001CBC0 File Offset: 0x0001ADC0
		protected override void Exit(DataShape dataShape)
		{
			this.m_batchSubtotalAnnotationAnalyzer.ExitDataShape(dataShape);
			this.Visit(dataShape.DynamicLimits, dataShape.Id);
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0001CBE0 File Offset: 0x0001ADE0
		private void Visit(DynamicLimits dynamicLimits, Identifier dataShapeId)
		{
			if (dynamicLimits == null)
			{
				return;
			}
			if (dynamicLimits.IntersectionLimit != null)
			{
				this.m_expressionReconciler.Reconcile(dynamicLimits.IntersectionLimit, ObjectType.DynamicLimits, dataShapeId, "IntersectionLimit");
			}
			if (dynamicLimits.Blocks != null)
			{
				foreach (DynamicLimitBlock dynamicLimitBlock in dynamicLimits.Blocks)
				{
					this.Visit(dynamicLimitBlock, dataShapeId);
				}
			}
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0001CC64 File Offset: 0x0001AE64
		private void Visit(DynamicLimitBlock block, Identifier objectId)
		{
			DynamicLimitEvenDistributionBlock dynamicLimitEvenDistributionBlock = block as DynamicLimitEvenDistributionBlock;
			if (dynamicLimitEvenDistributionBlock != null)
			{
				this.Visit(dynamicLimitEvenDistributionBlock, objectId);
				return;
			}
			DynamicLimitPrimarySecondaryBlock dynamicLimitPrimarySecondaryBlock = block as DynamicLimitPrimarySecondaryBlock;
			if (dynamicLimitPrimarySecondaryBlock == null)
			{
				throw new InvalidOperationException("Unexpected DynamicLimitBlock type");
			}
			this.Visit(dynamicLimitPrimarySecondaryBlock, objectId);
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0001CCA4 File Offset: 0x0001AEA4
		private void Visit(DynamicLimitEvenDistributionBlock block, Identifier objectId)
		{
			foreach (DynamicLimit dynamicLimit in block.Limits)
			{
				this.Visit(dynamicLimit, objectId);
			}
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x0001CCF8 File Offset: 0x0001AEF8
		private void Visit(DynamicLimitPrimarySecondaryBlock block, Identifier objectId)
		{
			this.Visit(block.Primary, objectId);
			this.Visit(block.Secondary, objectId);
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0001CD14 File Offset: 0x0001AF14
		private void Visit(DynamicLimit limit, Identifier objectId)
		{
			this.m_expressionReconciler.Reconcile(limit.LimitRef, ObjectType.DynamicLimit, objectId, "LimitRef");
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0001CD30 File Offset: 0x0001AF30
		protected override void Visit(Filter filter, Identifier dataShapeId)
		{
			FilterReconciler.Reconcile(filter, this.m_expressionReconciler, this.ExpressionTable, dataShapeId, this.m_errorContext, delegate(DataShape dataShape, ObjectType filterConditionType)
			{
				this.Visit(dataShape);
			});
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0001CD57 File Offset: 0x0001AF57
		protected override void Visit(VisualAxis visualAxis, Identifier dataShapeId)
		{
			VisualAxisReconciler.Reconcile(visualAxis, this.m_expressionReconciler, this.ExpressionTable, dataShapeId, this.m_errorContext);
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0001CD74 File Offset: 0x0001AF74
		protected override void Visit(Calculation calculation)
		{
			ExpressionNode expressionNode = this.m_expressionReconciler.Reconcile(calculation.Value, calculation.ObjectType, calculation.Id, "Value");
			if (expressionNode == null)
			{
				return;
			}
			Calculation calculation2;
			if (SubtotalAnalyzer.IsSubtotal(expressionNode, out calculation2))
			{
				this.m_batchSubtotalAnnotationAnalyzer.AddBatchSubtotalAnnotation(calculation, calculation2);
			}
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0001CDC0 File Offset: 0x0001AFC0
		protected override void Visit(ModelParameter modelParameter)
		{
			foreach (Expression expression in modelParameter.Values)
			{
				this.m_expressionReconciler.Reconcile(expression, ObjectType.ModelParameter, modelParameter.Name, "Values");
			}
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0001CE2C File Offset: 0x0001B02C
		protected override void Enter(DataMember dataMember)
		{
			this.Visit(dataMember.Group, dataMember);
			this.Visit(dataMember.InstanceFilters, dataMember);
			if (dataMember.HasExplicitSubtotal)
			{
				Microsoft.DataShaping.Contract.RetailAssert(!dataMember.IsDynamic, "Only static members can have context subtotals");
				this.m_batchSubtotalAnnotationAnalyzer.AddBatchExplicitSubtotalAnnotation(dataMember);
			}
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0001CE7C File Offset: 0x0001B07C
		private void Visit(List<FilterCondition> instanceFilters, DataMember dataMember)
		{
			if (instanceFilters == null)
			{
				return;
			}
			foreach (FilterCondition filterCondition in instanceFilters)
			{
				FilterReconciler.Reconcile(filterCondition, this.m_expressionReconciler, this.ExpressionTable, dataMember.Id, this.m_errorContext, delegate(DataShape dataShape, ObjectType filterConditionType)
				{
					this.Visit(dataShape);
				});
			}
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0001CEF0 File Offset: 0x0001B0F0
		private void Visit(Group group, DataMember dataMember)
		{
			if (group == null)
			{
				return;
			}
			this.VisitGroupKeys(group, dataMember);
			this.VisitSortKeys(group, dataMember);
			this.VisitScopeIdDef(group, dataMember);
			this.VisitDetailGroupIdentity(group, dataMember);
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0001CF18 File Offset: 0x0001B118
		private void VisitGroupKeys(Group group, DataMember dataMember)
		{
			foreach (GroupKey groupKey in group.GroupKeys)
			{
				this.m_expressionReconciler.Reconcile(groupKey.Value, ObjectType.GroupKey, dataMember.Id, "Value");
			}
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0001CF84 File Offset: 0x0001B184
		private void VisitSortKeys(Group group, DataMember dataMember)
		{
			List<SortKey> sortKeys = group.SortKeys;
			if (sortKeys != null)
			{
				foreach (SortKey sortKey in sortKeys)
				{
					this.m_expressionReconciler.Reconcile(sortKey.Value, ObjectType.SortKey, dataMember.Id, "Value");
				}
			}
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x0001CFF4 File Offset: 0x0001B1F4
		private void VisitScopeIdDef(Group group, DataMember dataMember)
		{
			if (group.ScopeIdDefinition == null)
			{
				return;
			}
			foreach (ScopeValueDefinition scopeValueDefinition in group.ScopeIdDefinition.Values)
			{
				this.m_expressionReconciler.Reconcile(scopeValueDefinition.Value, ObjectType.ScopeValueDefinition, dataMember.Id, "Value");
			}
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x0001D070 File Offset: 0x0001B270
		private void VisitDetailGroupIdentity(Group group, DataMember dataMember)
		{
			if (group.DetailGroupIdentity == null)
			{
				return;
			}
			this.m_expressionReconciler.Reconcile(group.DetailGroupIdentity.Value, ObjectType.DetailGroupIdentity, dataMember.Id, "Value");
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0001D0A0 File Offset: 0x0001B2A0
		protected override void Visit(Limit limit, DataShape dataShape)
		{
			this.m_expressionReconciler.Reconcile(limit.Within, ObjectType.Limit, dataShape.Id, "Within");
			this.m_limitTargetValidator.ValidateWithin(limit);
			foreach (Expression expression in limit.Targets)
			{
				this.m_expressionReconciler.Reconcile(expression, ObjectType.Limit, dataShape.Id, "Targets");
				this.m_limitTargetValidator.ValidateTarget(expression, limit.Id.Value);
			}
			BinnedLineSampleLimitOperator binnedLineSampleLimitOperator = limit.Operator as BinnedLineSampleLimitOperator;
			if (binnedLineSampleLimitOperator != null)
			{
				foreach (Expression expression2 in binnedLineSampleLimitOperator.Measures)
				{
					this.m_expressionReconciler.Reconcile(expression2, ObjectType.BinnedLineSampleLimitOperator, dataShape.Id, "Measures");
				}
				if (binnedLineSampleLimitOperator.PrimaryScalarKey != null)
				{
					this.m_expressionReconciler.Reconcile(binnedLineSampleLimitOperator.PrimaryScalarKey, ObjectType.BinnedLineSampleLimitOperator, dataShape.Id, "PrimaryScalarKey");
				}
			}
			OverlappingPointsSampleLimitOperator overlappingPointsSampleLimitOperator = limit.Operator as OverlappingPointsSampleLimitOperator;
			if (overlappingPointsSampleLimitOperator != null)
			{
				if (overlappingPointsSampleLimitOperator.X != null)
				{
					this.m_expressionReconciler.Reconcile(overlappingPointsSampleLimitOperator.X.Key, ObjectType.OverlappingPointsSampleLimitOperator, dataShape.Id, "X");
				}
				if (overlappingPointsSampleLimitOperator.Y != null)
				{
					this.m_expressionReconciler.Reconcile(overlappingPointsSampleLimitOperator.Y.Key, ObjectType.OverlappingPointsSampleLimitOperator, dataShape.Id, "Y");
				}
			}
			TopNPerLevelLimitOperator topNPerLevelLimitOperator = limit.Operator as TopNPerLevelLimitOperator;
			if (topNPerLevelLimitOperator != null)
			{
				if (!topNPerLevelLimitOperator.Levels.IsNullOrEmpty<List<Expression>>())
				{
					foreach (List<Expression> list in topNPerLevelLimitOperator.Levels)
					{
						foreach (Expression expression3 in list)
						{
							this.m_expressionReconciler.Reconcile(expression3, topNPerLevelLimitOperator.ObjectType, dataShape.Id, "Levels");
						}
					}
				}
				this.Visit(topNPerLevelLimitOperator.WindowExpansionInstance, dataShape.Id);
			}
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0001D300 File Offset: 0x0001B500
		protected override void Visit(DataTransformTableColumn column)
		{
			this.m_expressionReconciler.Reconcile(column.Value, ObjectType.DataTransformTableColumn, column.Id, "Value");
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0001D321 File Offset: 0x0001B521
		protected override void Visit(DataTransformParameter param)
		{
			this.m_expressionReconciler.Reconcile(param.Value, ObjectType.DataTransformParameter, param.Id, "Value");
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0001D344 File Offset: 0x0001B544
		private void Visit(LimitWindowExpansionInstance windowExpansionInstance, Identifier dataShapeId)
		{
			if (windowExpansionInstance != null)
			{
				if (!windowExpansionInstance.Values.IsNullOrEmpty<Expression>())
				{
					foreach (Expression expression in windowExpansionInstance.Values)
					{
						this.m_expressionReconciler.Reconcile(expression, ObjectType.TopNPerLevelLimitOperator, dataShapeId, "WindowExpansionInstance");
					}
				}
				if (!windowExpansionInstance.WindowValues.IsNullOrEmpty<LimitWindowExpansionValue>())
				{
					foreach (Expression expression2 in windowExpansionInstance.WindowValues.SelectMany((LimitWindowExpansionValue w) => w.Values))
					{
						this.m_expressionReconciler.Reconcile(expression2, ObjectType.TopNPerLevelLimitOperator, dataShapeId, "WindowExpansionInstance");
					}
				}
				if (!windowExpansionInstance.Children.IsNullOrEmpty<LimitWindowExpansionInstance>())
				{
					foreach (LimitWindowExpansionInstance limitWindowExpansionInstance in windowExpansionInstance.Children)
					{
						this.Visit(limitWindowExpansionInstance, dataShapeId);
					}
				}
			}
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0001D488 File Offset: 0x0001B688
		protected override void VisitExtensionEntity(ExtensionEntity extensionEntity)
		{
			base.TraverseExtensionEntityContents(extensionEntity);
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0001D491 File Offset: 0x0001B691
		protected override void VisitExtensionColumn(ExtensionColumn extensionColumn)
		{
			this.m_expressionReconciler.Reconcile(extensionColumn.Expression, ObjectType.ExtensionColumn, extensionColumn.Name, "Expression");
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0001D4B7 File Offset: 0x0001B6B7
		protected override void VisitExtensionMeasure(ExtensionMeasure extensionMeasure)
		{
			this.m_expressionReconciler.Reconcile(extensionMeasure.Expression, ObjectType.ExtensionMeasure, extensionMeasure.Name, "Expression");
		}

		// Token: 0x04000394 RID: 916
		private readonly ExpressionReconciler m_expressionReconciler;

		// Token: 0x04000395 RID: 917
		private readonly LimitExpressionValidator m_limitTargetValidator;

		// Token: 0x04000396 RID: 918
		private readonly ScopeTree m_scopeTree;

		// Token: 0x04000397 RID: 919
		private readonly TranslationErrorContext m_errorContext;

		// Token: 0x04000398 RID: 920
		private readonly BatchSubtotalAnnotationAnalyzer m_batchSubtotalAnnotationAnalyzer;
	}
}
