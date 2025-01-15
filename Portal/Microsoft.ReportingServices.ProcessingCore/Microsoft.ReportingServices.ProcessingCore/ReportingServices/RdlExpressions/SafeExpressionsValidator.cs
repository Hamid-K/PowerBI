using System;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.ReportingServices.RdlExpressions.SafeExpressions;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.RdlExpressions
{
	// Token: 0x02000576 RID: 1398
	internal sealed class SafeExpressionsValidator
	{
		// Token: 0x060050B8 RID: 20664 RVA: 0x001531A4 File Offset: 0x001513A4
		public SafeExpressionsValidator()
		{
			this.m_safeExpressionEvaluator = new ExpressionEvaluator(null);
		}

		// Token: 0x060050B9 RID: 20665 RVA: 0x001531B8 File Offset: 0x001513B8
		public ExpressionValidationResult ValidateExpression(ExpressionInfo expressionInfo)
		{
			if (expressionInfo != null)
			{
				switch (expressionInfo.Type)
				{
				case ExpressionInfo.Types.Expression:
				case ExpressionInfo.Types.Aggregate:
				case ExpressionInfo.Types.Lookup_OneValue:
				case ExpressionInfo.Types.Lookup_MultiValue:
				{
					ExpressionSyntax andCacheExpressionSyntax = expressionInfo.GetAndCacheExpressionSyntax();
					ExpressionValidationResult expressionValidationResult = this.m_safeExpressionEvaluator.Validate(andCacheExpressionSyntax);
					if (!expressionValidationResult.Supported)
					{
						return ExpressionValidationResult.NotSupported;
					}
					bool flag = expressionValidationResult.Enabled;
					if (expressionInfo.ExpressionWasTransformed())
					{
						ExpressionSyntax expressionSyntaxForOriginalExpression = expressionInfo.GetExpressionSyntaxForOriginalExpression();
						ExpressionValidationResult expressionValidationResult2 = this.m_safeExpressionEvaluator.Validate(expressionSyntaxForOriginalExpression);
						if (!expressionValidationResult2.Supported)
						{
							return ExpressionValidationResult.NotSupported;
						}
						flag = flag && expressionValidationResult2.Enabled;
					}
					return new ExpressionValidationResult(true, flag);
				}
				}
			}
			return new ExpressionValidationResult(true, true);
		}

		// Token: 0x040028B4 RID: 10420
		private readonly IExpressionEvaluator m_safeExpressionEvaluator;
	}
}
