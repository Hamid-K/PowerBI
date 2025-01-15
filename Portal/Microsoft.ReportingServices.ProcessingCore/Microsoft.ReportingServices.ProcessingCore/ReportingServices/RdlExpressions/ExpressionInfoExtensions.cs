using System;
using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.RdlExpressions
{
	// Token: 0x02000577 RID: 1399
	internal static class ExpressionInfoExtensions
	{
		// Token: 0x060050BA RID: 20666 RVA: 0x0015326C File Offset: 0x0015146C
		public static ExpressionSyntax GetAndCacheExpressionSyntax(this Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo)
		{
			if (expressionInfo.ExpressionStructureInfo == null)
			{
				ExpressionSyntax expressionSyntax = SyntaxFactory.ParseExpression(expressionInfo.GetExpressionString(), 0, true);
				expressionInfo.ExpressionStructureInfo = new ExpressionStructureInfo(expressionSyntax);
			}
			return expressionInfo.ExpressionStructureInfo.ExpressionSyntax;
		}

		// Token: 0x060050BB RID: 20667 RVA: 0x001532A6 File Offset: 0x001514A6
		public static ExpressionSyntax GetExpressionSyntaxForOriginalExpression(this Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo)
		{
			return SyntaxFactory.ParseExpression(ExpressionInfoExtensions.GetOriginalExpressionString(expressionInfo), 0, true);
		}

		// Token: 0x060050BC RID: 20668 RVA: 0x001532B5 File Offset: 0x001514B5
		public static string GetExpressionString(this Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo)
		{
			Global.Tracer.Assert(expressionInfo.OriginalText != null, "(expressionInfo.OriginalText != null)");
			if (expressionInfo.TransformedExpression != null)
			{
				return expressionInfo.TransformedExpression.Trim();
			}
			return ExpressionInfoExtensions.GetOriginalExpressionString(expressionInfo);
		}

		// Token: 0x060050BD RID: 20669 RVA: 0x001532EC File Offset: 0x001514EC
		private static string GetOriginalExpressionString(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo)
		{
			string text = expressionInfo.OriginalText.Trim();
			if (text.StartsWith("="))
			{
				text = text.Substring(1);
			}
			return text;
		}
	}
}
