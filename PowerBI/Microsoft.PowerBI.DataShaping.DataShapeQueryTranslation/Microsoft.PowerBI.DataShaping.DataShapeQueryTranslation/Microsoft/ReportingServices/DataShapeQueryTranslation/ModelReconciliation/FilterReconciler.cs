using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.ModelReconciliation
{
	// Token: 0x0200009F RID: 159
	internal sealed class FilterReconciler : FilterVisitor<FilterCondition>
	{
		// Token: 0x06000756 RID: 1878 RVA: 0x0001C41F File Offset: 0x0001A61F
		private FilterReconciler(ExpressionReconciler expressionReconciler, ExpressionTable expressionTable, Identifier objectId, TranslationErrorContext errorContext, VisitDataShapeDelegate visitDataShape)
			: base(visitDataShape)
		{
			this.m_expressionReconciler = expressionReconciler;
			this.m_expressionTable = expressionTable;
			this.m_objectId = objectId;
			this.m_errorContext = errorContext;
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0001C446 File Offset: 0x0001A646
		public static void Reconcile(Filter filter, ExpressionReconciler expressionReconciler, ExpressionTable expressionTable, Identifier objectId, TranslationErrorContext errorContext, VisitDataShapeDelegate visitDataShape)
		{
			new FilterReconciler(expressionReconciler, expressionTable, objectId, errorContext, visitDataShape).Visit(filter);
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x0001C45B File Offset: 0x0001A65B
		public static void Reconcile(FilterCondition filter, ExpressionReconciler expressionReconciler, ExpressionTable expressionTable, Identifier objectId, TranslationErrorContext errorContext, VisitDataShapeDelegate visitDataShape)
		{
			new FilterReconciler(expressionReconciler, expressionTable, objectId, errorContext, visitDataShape).Visit(filter);
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0001C470 File Offset: 0x0001A670
		protected override FilterCondition Visit(Filter filter)
		{
			ResolvedStructureReferenceExpressionNode resolvedStructureReferenceExpressionNode = this.m_expressionReconciler.Reconcile(filter.Target, ObjectType.Filter, this.m_objectId, "Target") as ResolvedStructureReferenceExpressionNode;
			if (resolvedStructureReferenceExpressionNode != null)
			{
				this.m_filterTarget = resolvedStructureReferenceExpressionNode.Target;
				return base.Visit(filter);
			}
			Contract.RetailAssert(this.m_errorContext.HasError, "An error should have already been registered if the expression does not resolve to a scope or calculation");
			return null;
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x0001C4D0 File Offset: 0x0001A6D0
		internal override FilterCondition Visit(UnaryFilterCondition condition)
		{
			this.m_expressionReconciler.Reconcile(condition.Expression, condition.ObjectType, this.m_objectId, "Expression");
			return condition;
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0001C4F8 File Offset: 0x0001A6F8
		internal override FilterCondition Visit(BinaryFilterCondition condition)
		{
			this.m_expressionReconciler.Reconcile(condition.LeftExpression, condition.ObjectType, this.m_objectId, "LeftExpression");
			this.m_expressionReconciler.Reconcile(condition.RightExpression, condition.ObjectType, this.m_objectId, "RightExpression");
			return condition;
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x0001C54C File Offset: 0x0001A74C
		internal override FilterCondition Visit(InFilterCondition condition)
		{
			List<Expression> expressions = condition.Expressions;
			for (int i = 0; i < expressions.Count; i++)
			{
				this.m_expressionReconciler.Reconcile(expressions[i], condition.ObjectType, this.m_objectId, "Expressions");
			}
			List<List<Expression>> values = condition.Values;
			if (values != null)
			{
				for (int j = 0; j < values.Count; j++)
				{
					List<Expression> list = values[j];
					for (int k = 0; k < list.Count; k++)
					{
						this.m_expressionReconciler.Reconcile(list[k], condition.ObjectType, this.m_objectId, "Values");
					}
				}
			}
			Expression table = condition.Table;
			if (table != null)
			{
				this.m_expressionReconciler.Reconcile(table, condition.ObjectType, this.m_objectId, "Table");
			}
			return condition;
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0001C623 File Offset: 0x0001A823
		internal override FilterCondition Visit(CompoundFilterCondition condition)
		{
			this.Visit(condition.Conditions, condition.ObjectType, "Conditions");
			return condition;
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0001C640 File Offset: 0x0001A840
		internal override FilterCondition Visit(ContextFilterCondition condition)
		{
			base.Visit(condition);
			if (this.m_filterTarget.ObjectType != ObjectType.DataShape)
			{
				this.m_errorContext.Register(TranslationMessages.InvalidContextFilterConditionTarget(EngineMessageSeverity.Error, ObjectType.DataShape, this.m_objectId, null, this.m_filterTarget.ObjectType, this.m_filterTarget.Id));
			}
			return condition;
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0001C698 File Offset: 0x0001A898
		internal override FilterCondition Visit(ApplyFilterCondition condition)
		{
			base.Visit(condition);
			if (this.m_filterTarget.ObjectType != ObjectType.DataShape)
			{
				this.m_errorContext.Register(TranslationMessages.InvalidApplyFilterConditionTarget(EngineMessageSeverity.Error, ObjectType.DataShape, this.m_objectId, null, this.m_filterTarget.ObjectType, this.m_filterTarget.Id));
			}
			if (condition.DataShapeReference != null)
			{
				this.m_expressionReconciler.Reconcile(condition.DataShapeReference, condition.ObjectType, this.m_objectId, "DataShapeReference");
			}
			return condition;
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0001C718 File Offset: 0x0001A918
		internal override FilterCondition Visit(FilterEmptyGroupsCondition condition)
		{
			if (this.m_filterTarget.ObjectType != ObjectType.DataShape)
			{
				this.m_errorContext.Register(TranslationMessages.InvalidFilterEmptyGroupsTarget(EngineMessageSeverity.Error, ObjectType.DataShape, this.m_objectId, null, this.m_filterTarget.ObjectType, this.m_filterTarget.Id));
			}
			return condition;
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0001C768 File Offset: 0x0001A968
		internal override FilterCondition Visit(ExistsFilterCondition condition)
		{
			foreach (ExistsFilterItem existsFilterItem in condition.Items)
			{
				this.ValidateExistsFilterItem(existsFilterItem);
			}
			return condition;
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0001C7BC File Offset: 0x0001A9BC
		private void ValidateExistsFilterItem(ExistsFilterItem item)
		{
			foreach (Expression expression in item.Targets)
			{
				this.ValidateExistsFilterItemExpression(expression, "Targets");
			}
			this.ValidateExistsFilterItemExpression(item.Exists, "Exists");
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0001C828 File Offset: 0x0001AA28
		private void ValidateExistsFilterItemExpression(Expression expression, string propertyName)
		{
			ExpressionNode expressionNode = this.m_expressionReconciler.Reconcile(expression, ObjectType.ExistsFilterItem, this.m_objectId, propertyName);
			if (expressionNode != null && !(expressionNode is ResolvedEntitySetExpressionNode))
			{
				this.m_errorContext.Register(TranslationMessages.InvalidExistsFilterExpression(EngineMessageSeverity.Error, ObjectType.ExistsFilterItem, this.m_objectId, propertyName, expressionNode.Kind));
			}
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x0001C878 File Offset: 0x0001AA78
		internal override FilterCondition Visit(AnyValueFilterCondition condition)
		{
			foreach (Expression expression in condition.Targets)
			{
				this.ValidateDefaultFilterContextTargetExpression(expression, "Targets", ObjectType.AnyValueFilterCondition);
			}
			return condition;
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x0001C8D4 File Offset: 0x0001AAD4
		internal override FilterCondition Visit(DefaultValueFilterCondition condition)
		{
			foreach (Expression expression in condition.Targets)
			{
				this.ValidateDefaultFilterContextTargetExpression(expression, "Targets", ObjectType.DefaultValueFilterCondition);
			}
			return condition;
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0001C930 File Offset: 0x0001AB30
		private void ValidateDefaultFilterContextTargetExpression(Expression expression, string propertyName, ObjectType objectType)
		{
			ExpressionNode expressionNode = this.m_expressionReconciler.Reconcile(expression, objectType, this.m_objectId, propertyName);
			if (expressionNode != null && !(expressionNode is ResolvedPropertyExpressionNode))
			{
				this.m_errorContext.Register(TranslationMessages.InvalidDefaultFilterContextConditionTargetExpression(EngineMessageSeverity.Error, objectType, this.m_objectId, propertyName, expressionNode.Kind));
			}
		}

		// Token: 0x0400038C RID: 908
		private readonly ExpressionReconciler m_expressionReconciler;

		// Token: 0x0400038D RID: 909
		private readonly ExpressionTable m_expressionTable;

		// Token: 0x0400038E RID: 910
		private readonly Identifier m_objectId;

		// Token: 0x0400038F RID: 911
		private readonly TranslationErrorContext m_errorContext;

		// Token: 0x04000390 RID: 912
		private IIdentifiable m_filterTarget;
	}
}
