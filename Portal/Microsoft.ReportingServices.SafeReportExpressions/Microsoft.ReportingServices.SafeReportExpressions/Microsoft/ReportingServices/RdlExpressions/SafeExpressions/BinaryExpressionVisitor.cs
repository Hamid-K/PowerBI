using System;
using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.ReportingServices.RdlExpressions.SafeExpressions.Interfaces;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions
{
	// Token: 0x02000027 RID: 39
	internal sealed class BinaryExpressionVisitor : VisitorBase, IExpressionSyntaxVisitor<BinaryExpressionSyntax>
	{
		// Token: 0x0600009A RID: 154 RVA: 0x000034AF File Offset: 0x000016AF
		public BinaryExpressionVisitor()
		{
			this._evaluator = EvaluatorFactory.CreateBinaryExpressionEvaluator();
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000034C4 File Offset: 0x000016C4
		public ExpressionEvaluationResult Evaluate(IExpressionVisitorHost host, BinaryExpressionSyntax node)
		{
			this.Validate(host, node);
			object obj;
			if (this.TryEvaluateInline(host, node, out obj))
			{
				return new ExpressionEvaluationResult(obj);
			}
			return this.EvaluateUsingEvaluator(host, node);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000034F4 File Offset: 0x000016F4
		public void Validate(IExpressionVisitorHost host, BinaryExpressionSyntax node)
		{
			base.CheckDiagnostics(node);
			host.Validate(node.Left);
			host.Validate(node.Right);
			switch (node.Kind())
			{
			case 307:
			case 308:
			case 309:
			case 310:
			case 311:
			case 314:
			case 315:
			case 316:
			case 317:
			case 318:
			case 319:
			case 320:
			case 321:
			case 322:
			case 323:
			case 324:
			case 327:
			case 328:
			case 329:
			case 330:
			case 331:
			case 332:
				return;
			default:
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000035A4 File Offset: 0x000017A4
		public ExpressionAnalysisResult Analyze(IExpressionVisitorHost host, BinaryExpressionSyntax node)
		{
			ExpressionAnalysisResult expressionAnalysisResult = host.Analyze(node.Left);
			ExpressionAnalysisResult expressionAnalysisResult2 = host.Analyze(node.Right);
			return new ExpressionAnalysisResult(expressionAnalysisResult.ContainsObjectReferences || expressionAnalysisResult2.ContainsObjectReferences);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000035E3 File Offset: 0x000017E3
		public bool IsEnabled(BinaryExpressionSyntax node)
		{
			return true;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000035E8 File Offset: 0x000017E8
		private bool TryEvaluateInline(IExpressionVisitorHost host, BinaryExpressionSyntax node, out object value)
		{
			value = null;
			SyntaxKind syntaxKind = node.Kind();
			if (syntaxKind == 307)
			{
				ExpressionEvaluationResult expressionEvaluationResult = host.Evaluate(node.Left);
				ExpressionEvaluationResult expressionEvaluationResult2 = host.Evaluate(node.Right);
				if (this.IsNullString(expressionEvaluationResult) && this.IsNullString(expressionEvaluationResult2))
				{
					value = string.Empty;
				}
				else if (expressionEvaluationResult.Value == null && this.IsStringOrChar(expressionEvaluationResult2))
				{
					value = this._evaluator.Evaluate(string.Empty, expressionEvaluationResult2.Value, node.Kind());
				}
				else if (expressionEvaluationResult2.Value == null && this.IsStringOrChar(expressionEvaluationResult))
				{
					value = this._evaluator.Evaluate(expressionEvaluationResult.Value, string.Empty, node.Kind());
				}
				else
				{
					value = this._evaluator.Evaluate(expressionEvaluationResult.Value, expressionEvaluationResult2.Value, node.Kind());
				}
				return true;
			}
			if (syntaxKind == 331)
			{
				if (VBConvert.ConvertToBoolean(host.Evaluate(node.Left).Value))
				{
					value = true;
				}
				else
				{
					object value2 = host.Evaluate(node.Right).Value;
					value = VBConvert.ConvertToBoolean(value2);
				}
				return true;
			}
			if (syntaxKind != 332)
			{
				return false;
			}
			if (VBConvert.ConvertToBoolean(host.Evaluate(node.Left).Value))
			{
				object value3 = host.Evaluate(node.Right).Value;
				value = VBConvert.ConvertToBoolean(value3);
			}
			else
			{
				value = false;
			}
			return true;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000377C File Offset: 0x0000197C
		private ExpressionEvaluationResult EvaluateUsingEvaluator(IExpressionVisitorHost host, BinaryExpressionSyntax node)
		{
			ExpressionEvaluationResult expressionEvaluationResult = host.Evaluate(node.Left);
			ExpressionEvaluationResult expressionEvaluationResult2 = host.Evaluate(node.Right);
			ExpressionEvaluationResult expressionEvaluationResult3;
			try
			{
				expressionEvaluationResult3 = new ExpressionEvaluationResult(this._evaluator.Evaluate(expressionEvaluationResult.Value, expressionEvaluationResult2.Value, node.Kind()));
			}
			catch (DivideByZeroException)
			{
				if (!host.Analyze(node.Right).ContainsObjectReferences)
				{
					SyntaxKind syntaxKind = node.Kind();
					if (syntaxKind == 310)
					{
						return new ExpressionEvaluationResult(((double)VBConvert.ChangeType(expressionEvaluationResult.Value, typeof(double)) >= 0.0) ? double.PositiveInfinity : double.NegativeInfinity);
					}
					if (syntaxKind == 318)
					{
						if (!host.Analyze(node.Left).ContainsObjectReferences)
						{
							return new ExpressionEvaluationResult(double.NaN);
						}
					}
				}
				throw;
			}
			return expressionEvaluationResult3;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000388C File Offset: 0x00001A8C
		private bool IsNullString(ExpressionEvaluationResult value)
		{
			return value.Value == null && value.Type == typeof(string);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000038AF File Offset: 0x00001AAF
		private bool IsStringOrChar(ExpressionEvaluationResult value)
		{
			return value.Type == typeof(char) || value.Type == typeof(string);
		}

		// Token: 0x04000037 RID: 55
		private readonly IBinaryExpressionEvaluator _evaluator;
	}
}
