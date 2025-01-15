using System;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions
{
	// Token: 0x0200002D RID: 45
	internal sealed class ParenthesizedExpressionVisitor : VisitorBase, IExpressionSyntaxVisitor<ParenthesizedExpressionSyntax>
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x0000487F File Offset: 0x00002A7F
		public ExpressionEvaluationResult Evaluate(IExpressionVisitorHost host, ParenthesizedExpressionSyntax node)
		{
			this.Validate(host, node);
			return host.Evaluate(node.Expression);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004895 File Offset: 0x00002A95
		public void Validate(IExpressionVisitorHost host, ParenthesizedExpressionSyntax node)
		{
			base.CheckDiagnostics(node);
			host.Validate(node.Expression);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000048AA File Offset: 0x00002AAA
		public ExpressionAnalysisResult Analyze(IExpressionVisitorHost host, ParenthesizedExpressionSyntax node)
		{
			return host.Analyze(node.Expression);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000048B8 File Offset: 0x00002AB8
		public bool IsEnabled(ParenthesizedExpressionSyntax node)
		{
			return true;
		}
	}
}
