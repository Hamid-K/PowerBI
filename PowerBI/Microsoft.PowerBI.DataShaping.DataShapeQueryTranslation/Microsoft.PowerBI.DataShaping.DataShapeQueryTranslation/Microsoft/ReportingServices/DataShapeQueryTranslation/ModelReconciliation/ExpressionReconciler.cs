using System;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.DataShapeValidation;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.ModelReconciliation
{
	// Token: 0x0200009D RID: 157
	internal sealed class ExpressionReconciler
	{
		// Token: 0x06000746 RID: 1862 RVA: 0x0001BD48 File Offset: 0x00019F48
		internal ExpressionReconciler(IFederatedConceptualSchema schema, ScopeTree scopeTree, IdentifierValidator idValidator, ExpressionTable inputExpressionTable, TranslationErrorContext errorContext, IFeatureSwitchProvider featureSwitchProvider)
		{
			this.m_errorContext = errorContext;
			this.m_schema = schema;
			this.m_scopeTree = scopeTree;
			this.m_idValidator = idValidator;
			this.m_inputExpressionTable = inputExpressionTable;
			this.m_featureSwitchProvider = featureSwitchProvider;
			this.m_expressionTable = inputExpressionTable.CreateEmptyWritableTable();
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x0001BD95 File Offset: 0x00019F95
		public WritableExpressionTable ExpressionTable
		{
			get
			{
				return this.m_expressionTable;
			}
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0001BDA0 File Offset: 0x00019FA0
		public ExpressionNode Reconcile(Expression expression, ObjectType parentObjectType, Identifier parentId, string propertyName)
		{
			ExpressionNode expressionNode2;
			try
			{
				ExpressionNode node = this.m_inputExpressionTable.GetNode(expression);
				ExpressionNode expressionNode = ExpressionNodeTreeReconciler.Reconcile(new ExpressionContext(this.m_errorContext, parentObjectType, parentId, propertyName), this.m_schema, this.m_scopeTree, this.m_idValidator, node, this.m_featureSwitchProvider);
				this.m_expressionTable.SetNode(expression, expressionNode);
				expressionNode2 = expressionNode;
			}
			catch (Exception ex)
			{
				if (ErrorUtils.IsStoppingException(ex))
				{
					throw;
				}
				this.m_errorContext.Register(TranslationMessages.InvalidExpression(EngineMessageSeverity.Error, parentObjectType, parentId, propertyName, TranslationMessagePhrases.GeneralQueryError(ex.Message)));
				expressionNode2 = null;
			}
			return expressionNode2;
		}

		// Token: 0x04000380 RID: 896
		private readonly TranslationErrorContext m_errorContext;

		// Token: 0x04000381 RID: 897
		private readonly IFederatedConceptualSchema m_schema;

		// Token: 0x04000382 RID: 898
		private readonly ScopeTree m_scopeTree;

		// Token: 0x04000383 RID: 899
		private readonly IdentifierValidator m_idValidator;

		// Token: 0x04000384 RID: 900
		private readonly ExpressionTable m_inputExpressionTable;

		// Token: 0x04000385 RID: 901
		private readonly IFeatureSwitchProvider m_featureSwitchProvider;

		// Token: 0x04000386 RID: 902
		private readonly WritableExpressionTable m_expressionTable;
	}
}
