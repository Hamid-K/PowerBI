using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions
{
	// Token: 0x02000028 RID: 40
	internal sealed class CollectionInitializerVisitor : VisitorBase, IExpressionSyntaxVisitor<CollectionInitializerSyntax>
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x000038E4 File Offset: 0x00001AE4
		public ExpressionEvaluationResult Evaluate(IExpressionVisitorHost host, CollectionInitializerSyntax node)
		{
			this.Validate(host, node);
			List<object> list = new List<object>();
			foreach (ExpressionSyntax expressionSyntax in node.Initializers)
			{
				object value = host.Evaluate(expressionSyntax).Value;
				list.Add(value);
			}
			return new ExpressionEvaluationResult(list.ToArray());
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003948 File Offset: 0x00001B48
		public void Validate(IExpressionVisitorHost host, CollectionInitializerSyntax node)
		{
			base.CheckDiagnostics(node);
			foreach (ExpressionSyntax expressionSyntax in node.Initializers)
			{
				if (expressionSyntax is CollectionInitializerSyntax)
				{
					throw new NotSupportedException("Multi-dimensional array initialization is not supported.");
				}
				host.Validate(expressionSyntax);
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003998 File Offset: 0x00001B98
		public ExpressionAnalysisResult Analyze(IExpressionVisitorHost host, CollectionInitializerSyntax node)
		{
			return new ExpressionAnalysisResult(false);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000039A0 File Offset: 0x00001BA0
		public bool IsEnabled(CollectionInitializerSyntax node)
		{
			return true;
		}
	}
}
