using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.ReportingServices.RdlExpressions.SafeExpressions.Interfaces;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions
{
	// Token: 0x0200002E RID: 46
	internal sealed class PredefinedCastExpressionSyntaxVisitor : VisitorBase, IExpressionSyntaxVisitor<PredefinedCastExpressionSyntax>
	{
		// Token: 0x060000CA RID: 202 RVA: 0x000048C4 File Offset: 0x00002AC4
		public PredefinedCastExpressionSyntaxVisitor()
		{
			this._evaluator = EvaluatorFactory.CreateConversionEvaluator();
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004918 File Offset: 0x00002B18
		public ExpressionEvaluationResult Evaluate(IExpressionVisitorHost host, PredefinedCastExpressionSyntax node)
		{
			this.Validate(host, node);
			object value = host.Evaluate(node.Expression).Value;
			string conversionName = this.GetConversionName(node);
			object obj = this._evaluator.Evaluate(conversionName, value);
			Type type = this._conversionReturnType[conversionName];
			return new ExpressionEvaluationResult(obj, type);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000496C File Offset: 0x00002B6C
		public void Validate(IExpressionVisitorHost host, PredefinedCastExpressionSyntax node)
		{
			base.CheckDiagnostics(node);
			host.Validate(node.Expression);
			string conversionName = this.GetConversionName(node);
			if (!(conversionName == "CSTR") && !(conversionName == "CDATE"))
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000049B4 File Offset: 0x00002BB4
		public ExpressionAnalysisResult Analyze(IExpressionVisitorHost host, PredefinedCastExpressionSyntax node)
		{
			return host.Analyze(node.Expression);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000049C2 File Offset: 0x00002BC2
		public bool IsEnabled(PredefinedCastExpressionSyntax node)
		{
			return true;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000049C8 File Offset: 0x00002BC8
		private string GetConversionName(PredefinedCastExpressionSyntax node)
		{
			return node.Keyword.Text.ToUpperInvariant();
		}

		// Token: 0x04000041 RID: 65
		private readonly IConversionEvaluator _evaluator;

		// Token: 0x04000042 RID: 66
		private readonly Dictionary<string, Type> _conversionReturnType = new Dictionary<string, Type>
		{
			{
				"CSTR",
				typeof(string)
			},
			{
				"CDATE",
				typeof(DateTime)
			}
		};
	}
}
