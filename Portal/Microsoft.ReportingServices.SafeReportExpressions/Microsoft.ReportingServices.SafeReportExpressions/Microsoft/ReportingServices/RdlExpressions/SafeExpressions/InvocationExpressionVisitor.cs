using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions
{
	// Token: 0x0200002A RID: 42
	internal sealed class InvocationExpressionVisitor : VisitorBase, IExpressionSyntaxVisitor<InvocationExpressionSyntax>
	{
		// Token: 0x060000AD RID: 173 RVA: 0x00003A94 File Offset: 0x00001C94
		public InvocationExpressionVisitor(FunctionFactory functionFactory)
		{
			this._functionFactory = functionFactory;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003B44 File Offset: 0x00001D44
		public ExpressionEvaluationResult Evaluate(IExpressionVisitorHost host, InvocationExpressionSyntax node)
		{
			this.Validate(host, node);
			IFunction function = this.GetFunction(node.Expression);
			List<ExpressionSyntax> argumentExpressions = this.GetArgumentExpressions(node.ArgumentList);
			List<ExpressionEvaluationResult> list = this.EvaluateExpressions(host, argumentExpressions);
			object obj;
			if (!this.TryEvaluateInline(function.Name, list, out obj))
			{
				obj = function.Evaluate(list.ConvertAll<object>((ExpressionEvaluationResult a) => a.Value));
			}
			return new ExpressionEvaluationResult(obj, function.ReturnType);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003BC8 File Offset: 0x00001DC8
		public void Validate(IExpressionVisitorHost host, InvocationExpressionSyntax node)
		{
			base.CheckDiagnostics(node);
			IFunction function = this.GetFunction(node.Expression);
			List<ExpressionSyntax> argumentExpressions = this.GetArgumentExpressions(node.ArgumentList);
			function.ValidateArguments(argumentExpressions);
			this.ValidateExpressions(host, argumentExpressions);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003C04 File Offset: 0x00001E04
		public ExpressionAnalysisResult Analyze(IExpressionVisitorHost host, InvocationExpressionSyntax node)
		{
			IFunction function = this.GetFunction(node.Expression);
			if (base.IsObjectReference(function.ReturnType))
			{
				return new ExpressionAnalysisResult(true);
			}
			List<ExpressionSyntax> argumentExpressions = this.GetArgumentExpressions(node.ArgumentList);
			return this.AnalyzeExpressions(host, argumentExpressions);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003C48 File Offset: 0x00001E48
		public bool IsEnabled(InvocationExpressionSyntax node)
		{
			MemberAccessExpressionSyntax memberAccessExpressionSyntax = node.Expression as MemberAccessExpressionSyntax;
			if (memberAccessExpressionSyntax != null)
			{
				string text = string.Join(".", this.EvaluateFullyQualifiedName(memberAccessExpressionSyntax));
				if (text.ToUpperInvariant() == "SYSTEM.MATH.ROUND" || text.ToUpperInvariant() == "MATH.ROUND")
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003CA0 File Offset: 0x00001EA0
		private IFunction GetFunction(ExpressionSyntax expressionSyntax)
		{
			string text = null;
			IdentifierNameSyntax identifierNameSyntax = expressionSyntax as IdentifierNameSyntax;
			if (identifierNameSyntax != null)
			{
				text = base.EvaluateIdentifierName(identifierNameSyntax);
			}
			else
			{
				MemberAccessExpressionSyntax memberAccessExpressionSyntax = expressionSyntax as MemberAccessExpressionSyntax;
				if (memberAccessExpressionSyntax != null)
				{
					string[] array = this.EvaluateFullyQualifiedName(memberAccessExpressionSyntax);
					this.ValidateFullyQualifiedName(string.Join(".", array));
					text = array.Last<string>();
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				throw new NotSupportedException("Expression of type " + ((expressionSyntax != null) ? expressionSyntax.GetType().Name : null) + " is not supported");
			}
			IFunction function;
			if (this._functionFactory.TryGet(text, out function))
			{
				return function;
			}
			throw new NotSupportedException("Function '" + text + "' is not supported");
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003D48 File Offset: 0x00001F48
		private string[] EvaluateFullyQualifiedName(MemberAccessExpressionSyntax memberAccessExpressionSyntax)
		{
			List<string> list = new List<string>();
			this.TraverseFullyQualfiedName(memberAccessExpressionSyntax, list);
			return list.ToArray();
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003D6C File Offset: 0x00001F6C
		private void TraverseFullyQualfiedName(MemberAccessExpressionSyntax memberAccessExpressionSyntax, IList<string> fullyQualifiedName)
		{
			IdentifierNameSyntax identifierNameSyntax = memberAccessExpressionSyntax.Expression as IdentifierNameSyntax;
			if (identifierNameSyntax != null)
			{
				fullyQualifiedName.Add(base.EvaluateIdentifierName(identifierNameSyntax));
			}
			else
			{
				MemberAccessExpressionSyntax memberAccessExpressionSyntax2 = memberAccessExpressionSyntax.Expression as MemberAccessExpressionSyntax;
				if (memberAccessExpressionSyntax2 == null)
				{
					string text = "TraverseFullyQualfiedName: Expression of type ";
					ExpressionSyntax expression = memberAccessExpressionSyntax.Expression;
					throw new NotSupportedException(text + ((expression != null) ? expression.GetType().Name : null) + " is not supported");
				}
				this.TraverseFullyQualfiedName(memberAccessExpressionSyntax2, fullyQualifiedName);
			}
			IdentifierNameSyntax identifierNameSyntax2 = memberAccessExpressionSyntax.Name as IdentifierNameSyntax;
			if (identifierNameSyntax2 != null)
			{
				fullyQualifiedName.Add(base.EvaluateIdentifierName(identifierNameSyntax2));
				return;
			}
			string text2 = "TraverseFullyQualfiedName: Expression of type ";
			SimpleNameSyntax name = memberAccessExpressionSyntax.Name;
			throw new NotSupportedException(text2 + ((name != null) ? name.GetType().Name : null) + " is not supported");
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003E24 File Offset: 0x00002024
		private void ValidateFullyQualifiedName(string fullyQualifiedName)
		{
			if (this.SupportedFullyQualifiedNames.Contains(fullyQualifiedName, StringComparer.OrdinalIgnoreCase))
			{
				return;
			}
			throw new NotSupportedException("ValidateFullyQualifiedName: " + fullyQualifiedName + " is not supported");
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003E50 File Offset: 0x00002050
		private List<ExpressionSyntax> GetArgumentExpressions(ArgumentListSyntax argumentListSyntax)
		{
			List<ExpressionSyntax> list = new List<ExpressionSyntax>();
			foreach (ArgumentSyntax argumentSyntax in argumentListSyntax.Arguments)
			{
				Type type = argumentSyntax.GetType();
				if (type == typeof(SimpleArgumentSyntax))
				{
					list.Add(argumentSyntax.GetExpression());
				}
				else
				{
					if (type == typeof(OmittedArgumentSyntax))
					{
						throw new NotSupportedException("InvocationExpressionVisitor: Omitted arguments are not supported.");
					}
					throw new NotSupportedException(string.Format("InvocationExpressionVisitor: Argument of type {0} is not supported.", type));
				}
			}
			return list;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003EE0 File Offset: 0x000020E0
		private List<ExpressionEvaluationResult> EvaluateExpressions(IExpressionVisitorHost host, List<ExpressionSyntax> expressions)
		{
			List<ExpressionEvaluationResult> list = new List<ExpressionEvaluationResult>();
			foreach (ExpressionSyntax expressionSyntax in expressions)
			{
				list.Add(host.Evaluate(expressionSyntax));
			}
			return list;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003F3C File Offset: 0x0000213C
		private void ValidateExpressions(IExpressionVisitorHost host, List<ExpressionSyntax> expressions)
		{
			foreach (ExpressionSyntax expressionSyntax in expressions)
			{
				IdentifierNameSyntax identifierNameSyntax = expressionSyntax as IdentifierNameSyntax;
				if (identifierNameSyntax != null)
				{
					string text = base.EvaluateIdentifierName(identifierNameSyntax).ToUpperInvariant();
					if (!this.BuiltInFunctionArgIdentifiers.Contains(text))
					{
						host.Validate(identifierNameSyntax);
					}
				}
				else
				{
					host.Validate(expressionSyntax);
				}
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003FB8 File Offset: 0x000021B8
		private bool TryEvaluateInline(string functionName, List<ExpressionEvaluationResult> arguments, out object value)
		{
			value = null;
			if (!(functionName == "LCASE"))
			{
				if (!(functionName == "UCASE"))
				{
					if (functionName == "LEN")
					{
						ExpressionEvaluationResult expressionEvaluationResult = arguments[0];
						if (expressionEvaluationResult.Value != null && expressionEvaluationResult.Type == typeof(decimal))
						{
							value = 8;
							return true;
						}
					}
				}
				else
				{
					ExpressionEvaluationResult expressionEvaluationResult2 = arguments[0];
					if (expressionEvaluationResult2.Value == null && expressionEvaluationResult2.Type == typeof(string))
					{
						value = string.Empty;
						return true;
					}
				}
			}
			else
			{
				ExpressionEvaluationResult expressionEvaluationResult3 = arguments[0];
				if (expressionEvaluationResult3.Value == null && expressionEvaluationResult3.Type == typeof(string))
				{
					value = null;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000408C File Offset: 0x0000228C
		private ExpressionAnalysisResult AnalyzeExpressions(IExpressionVisitorHost host, List<ExpressionSyntax> expressions)
		{
			foreach (ExpressionSyntax expressionSyntax in expressions)
			{
				if (host.Analyze(expressionSyntax).ContainsObjectReferences)
				{
					return new ExpressionAnalysisResult(true);
				}
			}
			return new ExpressionAnalysisResult(false);
		}

		// Token: 0x0400003B RID: 59
		private readonly string[] BuiltInFunctionArgIdentifiers = new string[]
		{
			"AVG", "COUNT", "COUNTDISTINCT", "COUNTROWS", "FIRST", "LAST", "MAX", "MIN", "STDEV", "STDEVP",
			"SUM", "VAR", "VARP"
		};

		// Token: 0x0400003C RID: 60
		private readonly string[] SupportedFullyQualifiedNames = new string[] { "System.Math.Round", "Math.Round" };

		// Token: 0x0400003D RID: 61
		private readonly FunctionFactory _functionFactory;
	}
}
