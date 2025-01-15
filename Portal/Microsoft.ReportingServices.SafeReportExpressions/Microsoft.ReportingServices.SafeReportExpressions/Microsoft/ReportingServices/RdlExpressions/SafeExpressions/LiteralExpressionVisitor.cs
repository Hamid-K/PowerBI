using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions
{
	// Token: 0x0200002B RID: 43
	internal sealed class LiteralExpressionVisitor : VisitorBase, IExpressionSyntaxVisitor<LiteralExpressionSyntax>
	{
		// Token: 0x060000BB RID: 187 RVA: 0x000040F8 File Offset: 0x000022F8
		public ExpressionEvaluationResult Evaluate(IExpressionVisitorHost host, LiteralExpressionSyntax node)
		{
			this.Validate(host, node);
			SyntaxToken token = node.Token;
			SyntaxKind syntaxKind = VisualBasicExtensions.Kind(token);
			if (syntaxKind <= 513)
			{
				if (syntaxKind == 474)
				{
					return new ExpressionEvaluationResult(false);
				}
				if (syntaxKind == 513)
				{
					return ExpressionEvaluationResult.CreateNull();
				}
			}
			else
			{
				if (syntaxKind == 563)
				{
					return new ExpressionEvaluationResult(true);
				}
				switch (syntaxKind)
				{
				case 701:
				{
					TypeCode typeCode = Type.GetTypeCode(token.Value.GetType());
					switch (typeCode)
					{
					case TypeCode.Int16:
						return new ExpressionEvaluationResult((short)token.Value);
					case TypeCode.UInt16:
						return new ExpressionEvaluationResult((ushort)token.Value);
					case TypeCode.Int32:
						return new ExpressionEvaluationResult((int)token.Value);
					case TypeCode.UInt32:
						return new ExpressionEvaluationResult((uint)token.Value);
					case TypeCode.Int64:
						return new ExpressionEvaluationResult((long)token.Value);
					case TypeCode.UInt64:
						return new ExpressionEvaluationResult((ulong)token.Value);
					default:
						throw new InvalidOperationException(string.Format("Unexpected TypeCode '{0}' for IntegerLiteralToken", typeCode));
					}
					break;
				}
				case 702:
				{
					TypeCode typeCode2 = Type.GetTypeCode(token.Value.GetType());
					if (typeCode2 == TypeCode.Single)
					{
						return new ExpressionEvaluationResult((float)token.Value);
					}
					if (typeCode2 == TypeCode.Double)
					{
						return new ExpressionEvaluationResult((double)token.Value);
					}
					throw new InvalidOperationException(string.Format("Unexpected TypeCode '{0}' for FloatingLiteralToken", typeCode2));
				}
				case 703:
					return new ExpressionEvaluationResult((decimal)token.Value);
				case 704:
					return new ExpressionEvaluationResult((DateTime)token.Value);
				case 705:
					return new ExpressionEvaluationResult((string)token.Value);
				case 706:
					return new ExpressionEvaluationResult((char)token.Value);
				}
			}
			throw new InvalidOperationException(string.Format("Unexpected Token.Kind value '{0}'", VisualBasicExtensions.Kind(token)));
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004332 File Offset: 0x00002532
		public void Validate(IExpressionVisitorHost host, LiteralExpressionSyntax node)
		{
			base.CheckDiagnostics(node);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000433B File Offset: 0x0000253B
		public ExpressionAnalysisResult Analyze(IExpressionVisitorHost host, LiteralExpressionSyntax node)
		{
			return new ExpressionAnalysisResult(false);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004343 File Offset: 0x00002543
		public bool IsEnabled(LiteralExpressionSyntax node)
		{
			return true;
		}
	}
}
