using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions
{
	// Token: 0x02000005 RID: 5
	internal sealed class ExpressionEvaluator : IExpressionEvaluator
	{
		// Token: 0x06000004 RID: 4 RVA: 0x00002069 File Offset: 0x00000269
		internal ExpressionEvaluator(ISafeExpressionsReportContext reportProcessingContext)
		{
			this._expressionSyntaxWalkerSharedState = new ExpressionSyntaxWalkerSharedState(reportProcessingContext);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002080 File Offset: 0x00000280
		public object Evaluate(ExpressionSyntax expressionSyntax)
		{
			return this.CreateSyntaxWalker().Evaluate(expressionSyntax).Value;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020A4 File Offset: 0x000002A4
		public void EvaluateAndCollectNodeEvaluations(ExpressionSyntax expressionSyntax, out List<SyntaxNodeEvaluation> nodeEvaluations)
		{
			ExpressionSyntaxWalker expressionSyntaxWalker = this.CreateSyntaxWalker();
			expressionSyntaxWalker.InitCollectionOfNodeEvaluations();
			nodeEvaluations = expressionSyntaxWalker.GetNodeEvaluations();
			expressionSyntaxWalker.Evaluate(expressionSyntax);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020D0 File Offset: 0x000002D0
		public ExpressionValidationResult Validate(ExpressionSyntax expressionSyntax)
		{
			ExpressionSyntaxWalker expressionSyntaxWalker = this.CreateSyntaxWalker();
			ExpressionValidationResult expressionValidationResult;
			try
			{
				bool flag = expressionSyntaxWalker.ValidateAndCheckIsEnabled(expressionSyntax);
				expressionValidationResult = new ExpressionValidationResult(true, flag);
			}
			catch (Exception ex) when (ex is NotSupportedException || ex is SyntaxErrorException)
			{
				expressionValidationResult = ExpressionValidationResult.NotSupported;
			}
			return expressionValidationResult;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002138 File Offset: 0x00000338
		public ExpressionAnalysisResult Analyze(ExpressionSyntax expressionSyntax)
		{
			return this.CreateSyntaxWalker().Analyze(expressionSyntax);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002146 File Offset: 0x00000346
		public ExpressionSyntax ParseExpression(string expressionString)
		{
			expressionString = this.ConvertFirstDateLiteralToTernary(expressionString.Trim());
			return SyntaxFactory.ParseExpression(expressionString, 0, true);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002160 File Offset: 0x00000360
		private string ConvertFirstDateLiteralToTernary(string expression)
		{
			if (!expression.StartsWith("#"))
			{
				return expression;
			}
			int num = expression.IndexOf("#", 1);
			if (num == -1)
			{
				return expression;
			}
			int num2 = num + 1;
			string text = expression.Substring(0, num2);
			return "If(True, " + text + ", Nothing)" + expression.Substring(num2);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021B3 File Offset: 0x000003B3
		private ExpressionSyntaxWalker CreateSyntaxWalker()
		{
			return new ExpressionSyntaxWalker(this._expressionSyntaxWalkerSharedState);
		}

		// Token: 0x04000001 RID: 1
		private readonly ExpressionSyntaxWalkerSharedState _expressionSyntaxWalkerSharedState;
	}
}
