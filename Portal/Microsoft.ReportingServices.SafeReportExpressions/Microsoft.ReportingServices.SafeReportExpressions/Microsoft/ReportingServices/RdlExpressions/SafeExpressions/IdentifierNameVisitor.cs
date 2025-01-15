using System;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.ReportingServices.RdlExpressions.SafeExpressions.Interfaces;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions
{
	// Token: 0x02000029 RID: 41
	internal sealed class IdentifierNameVisitor : VisitorBase, IExpressionSyntaxVisitor<IdentifierNameSyntax>
	{
		// Token: 0x060000A8 RID: 168 RVA: 0x000039AB File Offset: 0x00001BAB
		public IdentifierNameVisitor(FunctionFactory functionFactory)
		{
			this._functionFactory = functionFactory;
			this._vbConstantIdentifierEvaluator = EvaluatorFactory.CreateVbConstantIdentifierEvaluator();
			this._vbConstantIdentifier = new VisualBasicConstantIdentifier();
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000039D0 File Offset: 0x00001BD0
		public ExpressionEvaluationResult Evaluate(IExpressionVisitorHost host, IdentifierNameSyntax node)
		{
			this.Validate(host, node);
			string text = base.EvaluateIdentifierName(node);
			object obj;
			if (this._vbConstantIdentifier.IsIdentifierVisualBasicConstant(text))
			{
				obj = this._vbConstantIdentifierEvaluator.EvaluateConstant(text);
			}
			else
			{
				IFunction function;
				this._functionFactory.TryGet(text, out function);
				obj = function.Evaluate();
			}
			return new ExpressionEvaluationResult(obj);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003A28 File Offset: 0x00001C28
		public void Validate(IExpressionVisitorHost host, IdentifierNameSyntax node)
		{
			base.CheckDiagnostics(node);
			string text = base.EvaluateIdentifierName(node);
			if (string.IsNullOrEmpty(text))
			{
				throw new NotSupportedException("Empty or null identifier name is not supported");
			}
			if (this._vbConstantIdentifier.IsIdentifierVisualBasicConstant(text))
			{
				return;
			}
			IFunction function;
			if (this._functionFactory.TryGet(text, out function) && function.HasNoArguments)
			{
				return;
			}
			throw new NotSupportedException("The specified identifier name is not supported.");
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003A89 File Offset: 0x00001C89
		public ExpressionAnalysisResult Analyze(IExpressionVisitorHost host, IdentifierNameSyntax node)
		{
			return new ExpressionAnalysisResult(false);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003A91 File Offset: 0x00001C91
		public bool IsEnabled(IdentifierNameSyntax node)
		{
			return true;
		}

		// Token: 0x04000038 RID: 56
		private readonly IVisualBasicConstantEvaluator _vbConstantIdentifierEvaluator;

		// Token: 0x04000039 RID: 57
		private readonly VisualBasicConstantIdentifier _vbConstantIdentifier;

		// Token: 0x0400003A RID: 58
		private readonly FunctionFactory _functionFactory;
	}
}
