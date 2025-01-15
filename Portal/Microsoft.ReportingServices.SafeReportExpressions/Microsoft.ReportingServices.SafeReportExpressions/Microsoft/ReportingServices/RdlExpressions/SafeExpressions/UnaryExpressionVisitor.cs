using System;
using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.ReportingServices.RdlExpressions.SafeExpressions.Interfaces;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions
{
	// Token: 0x02000030 RID: 48
	internal sealed class UnaryExpressionVisitor : VisitorBase, IExpressionSyntaxVisitor<UnaryExpressionSyntax>
	{
		// Token: 0x060000DB RID: 219 RVA: 0x00004DF6 File Offset: 0x00002FF6
		public UnaryExpressionVisitor()
		{
			this._evaluator = EvaluatorFactory.CreateUnaryExpressionEvaluator();
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004E0C File Offset: 0x0000300C
		public ExpressionEvaluationResult Evaluate(IExpressionVisitorHost host, UnaryExpressionSyntax node)
		{
			this.Validate(host, node);
			object value = host.Evaluate(node.Operand).Value;
			return new ExpressionEvaluationResult(this._evaluator.Evaluate(value, node.Kind()));
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004E50 File Offset: 0x00003050
		public void Validate(IExpressionVisitorHost host, UnaryExpressionSyntax node)
		{
			base.CheckDiagnostics(node);
			host.Validate(node.Operand);
			SyntaxKind syntaxKind = node.Kind();
			if (syntaxKind - 333 > 2)
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004E87 File Offset: 0x00003087
		public ExpressionAnalysisResult Analyze(IExpressionVisitorHost host, UnaryExpressionSyntax node)
		{
			return host.Analyze(node.Operand);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004E95 File Offset: 0x00003095
		public bool IsEnabled(UnaryExpressionSyntax node)
		{
			return true;
		}

		// Token: 0x04000050 RID: 80
		private readonly IUnaryExpressionEvaluator _evaluator;
	}
}
