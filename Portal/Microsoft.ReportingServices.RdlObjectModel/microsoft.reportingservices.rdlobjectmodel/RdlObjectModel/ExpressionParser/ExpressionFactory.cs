using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000217 RID: 535
	internal static class ExpressionFactory
	{
		// Token: 0x0600121E RID: 4638 RVA: 0x00028C88 File Offset: 0x00026E88
		public static Expression CreateExpression(string expressionText, bool validate)
		{
			Expression expression = new Expression();
			if (validate)
			{
				expression.Source = expressionText;
			}
			else
			{
				expression.SourceNoValidate = expressionText;
			}
			return expression;
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x00028CAF File Offset: 0x00026EAF
		public static Expression CreateExpression(string expressionText)
		{
			return ExpressionFactory.CreateExpression(expressionText, false);
		}
	}
}
