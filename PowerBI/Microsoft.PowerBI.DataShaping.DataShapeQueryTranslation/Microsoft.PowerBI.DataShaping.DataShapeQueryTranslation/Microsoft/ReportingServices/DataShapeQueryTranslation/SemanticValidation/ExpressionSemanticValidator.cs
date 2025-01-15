using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.SemanticValidation
{
	// Token: 0x02000065 RID: 101
	internal sealed class ExpressionSemanticValidator
	{
		// Token: 0x0600054A RID: 1354 RVA: 0x00011DDE File Offset: 0x0000FFDE
		internal ExpressionSemanticValidator(TranslationErrorContext errorContext, ExpressionTable expressionTable, ScopeTree scopeTree, DataShapeAnnotations annotations, DaxCapabilitiesAnnotation daxCapabilitiesAnnotation)
		{
			this.m_errorContext = errorContext;
			this.m_inputExpressionTable = expressionTable;
			this.m_scopeTree = scopeTree;
			this.m_annotations = annotations;
			this.m_daxCapabilitiesAnnotation = daxCapabilitiesAnnotation;
			this.m_outputExpressionTable = this.m_inputExpressionTable.CopyTable();
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x00011E1C File Offset: 0x0001001C
		public ReadOnlyExpressionTable OutputExpressionTable
		{
			get
			{
				return this.m_outputExpressionTable.AsReadOnly();
			}
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00011E2C File Offset: 0x0001002C
		public ExpressionValidationResult Validate(Expression expression, ExpressionFeatureFlags allowedFeatures, ObjectType objectType, Identifier objectId, string propertyName, IScope containingScope)
		{
			ExpressionNode node = this.m_inputExpressionTable.GetNode(expression);
			ExpressionContext expressionContext = new ExpressionContext(this.m_errorContext, objectType, objectId, propertyName);
			ExpressionValidationResult expressionValidationResult = ExpressionNodeSemanticValidator.Validate(node, allowedFeatures, expressionContext, this.m_scopeTree, containingScope, this.m_inputExpressionTable, this.m_annotations, this.m_daxCapabilitiesAnnotation);
			this.CheckReferencedCalculationScopes(containingScope, expressionContext, expressionValidationResult);
			if (expressionValidationResult.OutputNode != null)
			{
				this.m_outputExpressionTable.SetNode(expression, expressionValidationResult.OutputNode);
			}
			return expressionValidationResult;
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00011EA0 File Offset: 0x000100A0
		private void CheckReferencedCalculationScopes(IScope containingScope, ExpressionContext exprContext, ExpressionValidationResult validationResult)
		{
			if (validationResult.ReferencedCalculations != null)
			{
				foreach (Calculation calculation in validationResult.ReferencedCalculations)
				{
					IScope containingScope2 = this.m_scopeTree.GetContainingScope(calculation);
					if (this.m_scopeTree.IsParentScope(containingScope2, containingScope))
					{
						exprContext.ErrorContext.Register(TranslationMessages.InvalidExpression(EngineMessageSeverity.Error, exprContext.ObjectType, exprContext.ObjectId, exprContext.PropertyName, TranslationMessagePhrases.CalculationRefersToAncestorScope()));
					}
				}
			}
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00011F34 File Offset: 0x00010134
		private static bool FindCondition(ExpressionContext exprContext, FilterCondition condition, Identifier id, ref int depth)
		{
			if (condition == null)
			{
				return false;
			}
			if (condition.Id == id && condition.ObjectType != ObjectType.FilterEmptyGroupsCondition)
			{
				return true;
			}
			if (condition.ObjectType == ObjectType.CompoundFilterCondition)
			{
				depth++;
				CompoundFilterCondition compoundFilterCondition = (CompoundFilterCondition)condition;
				for (int i = 0; i < compoundFilterCondition.Conditions.Count; i++)
				{
					if (ExpressionSemanticValidator.FindCondition(exprContext, compoundFilterCondition.Conditions[i], id, ref depth))
					{
						if (compoundFilterCondition.Operator.Value != CompoundFilterOperator.Any)
						{
							exprContext.ErrorContext.Register(TranslationMessages.CompoundFilterOperatorNotSupportedWithRemove(EngineMessageSeverity.Error, exprContext.ObjectType, exprContext.ObjectId, exprContext.PropertyName, compoundFilterCondition.Id, compoundFilterCondition.Operator.Value));
						}
						return true;
					}
				}
				depth--;
			}
			return false;
		}

		// Token: 0x04000287 RID: 647
		private readonly TranslationErrorContext m_errorContext;

		// Token: 0x04000288 RID: 648
		private readonly ExpressionTable m_inputExpressionTable;

		// Token: 0x04000289 RID: 649
		private readonly WritableExpressionTable m_outputExpressionTable;

		// Token: 0x0400028A RID: 650
		private readonly ScopeTree m_scopeTree;

		// Token: 0x0400028B RID: 651
		private readonly DataShapeAnnotations m_annotations;

		// Token: 0x0400028C RID: 652
		private readonly DaxCapabilitiesAnnotation m_daxCapabilitiesAnnotation;
	}
}
