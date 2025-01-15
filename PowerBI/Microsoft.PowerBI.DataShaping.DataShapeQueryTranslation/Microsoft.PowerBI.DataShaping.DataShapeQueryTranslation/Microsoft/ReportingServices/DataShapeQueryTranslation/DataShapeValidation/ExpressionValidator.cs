using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataShapeValidation
{
	// Token: 0x020000CB RID: 203
	internal sealed class ExpressionValidator
	{
		// Token: 0x060008A1 RID: 2209 RVA: 0x00021288 File Offset: 0x0001F488
		internal ExpressionValidator(TranslationErrorContext errorContext)
			: this(errorContext, new WritableExpressionTable())
		{
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x00021296 File Offset: 0x0001F496
		internal ExpressionValidator(TranslationErrorContext errorContext, WritableExpressionTable expressionTable)
		{
			this.m_errorContext = errorContext;
			this.m_expressionTable = expressionTable;
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060008A3 RID: 2211 RVA: 0x000212AC File Offset: 0x0001F4AC
		public WritableExpressionTable ExpressionTable
		{
			get
			{
				return this.m_expressionTable;
			}
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x000212B4 File Offset: 0x0001F4B4
		public void ValidateExpressions(IReadOnlyList<Expression> expressions, ObjectType objectType, Identifier objectId, string propertyName)
		{
			if (expressions.IsNullOrEmpty<Expression>())
			{
				return;
			}
			ExpressionContext expressionContext = new ExpressionContext(this.m_errorContext, objectType, objectId, propertyName);
			foreach (Expression expression in expressions)
			{
				this.ValidateExpression(expression, expressionContext);
			}
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x00021318 File Offset: 0x0001F518
		public void ValidateExpression(Expression expression, ObjectType objectType, Identifier objectId, string propertyName)
		{
			if (expression == null)
			{
				return;
			}
			ExpressionContext expressionContext = new ExpressionContext(this.m_errorContext, objectType, objectId, propertyName);
			this.ValidateExpression(expression, expressionContext);
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x00021341 File Offset: 0x0001F541
		public void ValidateRequiredExpression(Expression expression, ObjectType objectType, Identifier objectId, string propertyName)
		{
			if (expression == null)
			{
				this.m_errorContext.Register(TranslationMessages.MissingOrInvalidPropertyValue(EngineMessageSeverity.Error, objectType, objectId, propertyName));
			}
			this.ValidateExpression(expression, objectType, objectId, propertyName);
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x00021366 File Offset: 0x0001F566
		public void ValidateCandidateValue<T>(Candidate<T> candidate, ObjectType objectType, Identifier objectId, string propertyName)
		{
			candidate.ValidateCandidateValue(this.m_errorContext, objectType, objectId, propertyName);
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x00021378 File Offset: 0x0001F578
		public void ValidateRequiredCandidateValue<T>(Candidate<T> candidate, ObjectType objectType, Identifier objectId, string propertyName)
		{
			candidate.ValidateRequiredCandidateValue(this.m_errorContext, objectType, objectId, propertyName);
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0002138A File Offset: 0x0001F58A
		private void ValidateExpression(Expression expression, ExpressionContext parserContext)
		{
			Contract.RetailAssert(expression.OriginalNode != null, "Expression must have an OriginalNode");
			this.m_expressionTable.SetNode(expression, expression.OriginalNode);
		}

		// Token: 0x04000429 RID: 1065
		private readonly TranslationErrorContext m_errorContext;

		// Token: 0x0400042A RID: 1066
		private readonly WritableExpressionTable m_expressionTable;
	}
}
