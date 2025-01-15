using System;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions
{
	// Token: 0x0200002C RID: 44
	internal sealed class MemberAccessExpressionVisitor : VisitorBase, IExpressionSyntaxVisitor<MemberAccessExpressionSyntax>
	{
		// Token: 0x060000C0 RID: 192 RVA: 0x0000434E File Offset: 0x0000254E
		public MemberAccessExpressionVisitor(ISafeExpressionsReportContext safeExpressionsReportContext)
		{
			this._reportCollectionAccessor = new ReportCollectionAccessor(safeExpressionsReportContext);
			this._enumAccessor = new EnumAccessor();
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x0000438C File Offset: 0x0000258C
		public ExpressionEvaluationResult Evaluate(IExpressionVisitorHost host, MemberAccessExpressionSyntax node)
		{
			this.Validate(host, node);
			SyntaxKind syntaxKind = VisualBasicExtensions.Kind(node.OperatorToken);
			if (syntaxKind != 634)
			{
				if (syntaxKind == 650)
				{
					if (node.Expression is MemberAccessExpressionSyntax)
					{
						object value = host.Evaluate(node.Expression).Value;
						string text = base.EvaluateSimpleName(node.Name);
						Type type = value.GetType();
						PropertyInfo propertyInfo = base.GetPropertyInfo(type, text);
						if (propertyInfo != null)
						{
							return new ExpressionEvaluationResult(propertyInfo.GetValue(value), propertyInfo.PropertyType);
						}
						MethodInfo methodInfo = base.GetMethodInfo(type, text);
						if (methodInfo != null)
						{
							return new ExpressionEvaluationResult(methodInfo.Invoke(value, null), methodInfo.ReturnType);
						}
						if (this.SupportedMethodsWithNoParam.Contains(text, StringComparer.OrdinalIgnoreCase))
						{
							methodInfo = base.GetMethodInfoWithOneReferenceTypeParameter(type, text);
							if (methodInfo != null)
							{
								return new ExpressionEvaluationResult(methodInfo.Invoke(value, new object[1]), methodInfo.ReturnType);
							}
						}
						if (type.IsEnum)
						{
							string[] names = Enum.GetNames(type);
							for (int i = 0; i < names.Length; i++)
							{
								if (names[i].Equals(text, StringComparison.OrdinalIgnoreCase))
								{
									return new ExpressionEvaluationResult(Enum.Parse(type, text), type);
								}
							}
						}
						throw new MissingMemberException(string.Concat(new string[] { "Property or method '", text, "' does not exist on Object '", type.Name, "'" }));
					}
					else
					{
						IdentifierNameSyntax identifierNameSyntax = node.Expression as IdentifierNameSyntax;
						if (identifierNameSyntax != null)
						{
							string text2 = base.EvaluateIdentifierName(identifierNameSyntax);
							string text3 = base.EvaluateSimpleName(node.Name);
							return new ExpressionEvaluationResult(this._enumAccessor.GetValue(text2, text3), this._enumAccessor.GetEnumItemType(text2));
						}
					}
				}
				throw new NotSupportedException(string.Format("Unsupported operator for member access expression. Operator syntax: {0}", VisualBasicExtensions.Kind(node.OperatorToken)));
			}
			string text4 = base.EvaluateIdentifierName(node.Expression as IdentifierNameSyntax);
			string text5 = base.EvaluateSimpleName(node.Name);
			return new ExpressionEvaluationResult(this._reportCollectionAccessor.GetValue(text4, text5));
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000045B8 File Offset: 0x000027B8
		public void Validate(IExpressionVisitorHost host, MemberAccessExpressionSyntax node)
		{
			base.CheckDiagnostics(node);
			SyntaxKind syntaxKind = VisualBasicExtensions.Kind(node.OperatorToken);
			if (syntaxKind != 634)
			{
				if (syntaxKind != 650)
				{
					throw new NotSupportedException(string.Format("Unsupported operator for member access expression. Operator syntax: {0}", VisualBasicExtensions.Kind(node.OperatorToken)));
				}
				if (node.Expression is MemberAccessExpressionSyntax)
				{
					host.Validate(node.Expression);
					return;
				}
				IdentifierNameSyntax identifierNameSyntax = node.Expression as IdentifierNameSyntax;
				if (identifierNameSyntax == null)
				{
					throw new NotSupportedException("Unsupported expression for member access expression and '.' operator.");
				}
				string text = base.EvaluateIdentifierName(identifierNameSyntax);
				if (!this._enumAccessor.IsSupportedEnum(text))
				{
					throw new NotSupportedException("The specified enum name is not supported: " + text);
				}
				return;
			}
			else
			{
				IdentifierNameSyntax identifierNameSyntax2 = node.Expression as IdentifierNameSyntax;
				if (identifierNameSyntax2 == null)
				{
					throw new NotSupportedException("Unsupported expression for member access expression and '!' operator.");
				}
				string text2 = base.EvaluateIdentifierName(identifierNameSyntax2);
				if (!this._reportCollectionAccessor.IsValidCollectionName(text2))
				{
					throw new NotSupportedException("The specified collection name is not supported: " + text2);
				}
				return;
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000046AC File Offset: 0x000028AC
		public ExpressionAnalysisResult Analyze(IExpressionVisitorHost host, MemberAccessExpressionSyntax node)
		{
			SyntaxKind syntaxKind = VisualBasicExtensions.Kind(node.OperatorToken);
			if (syntaxKind != 634)
			{
				if (syntaxKind == 650)
				{
					MemberAccessExpressionSyntax memberAccessExpressionSyntax = node.Expression as MemberAccessExpressionSyntax;
					if (memberAccessExpressionSyntax != null)
					{
						IdentifierNameSyntax identifierNameSyntax = memberAccessExpressionSyntax.Expression as IdentifierNameSyntax;
						Type type;
						if (identifierNameSyntax == null)
						{
							if (memberAccessExpressionSyntax.Expression is MemberAccessExpressionSyntax)
							{
								try
								{
									type = host.Evaluate(memberAccessExpressionSyntax).Value.GetType();
									goto IL_00B3;
								}
								catch (Exception)
								{
									return new ExpressionAnalysisResult(true);
								}
							}
							throw new NotSupportedException("Unsupported expression node for analyze.");
						}
						string text = base.EvaluateIdentifierName(identifierNameSyntax);
						if (!this._reportCollectionAccessor.IsCollection(text))
						{
							return new ExpressionAnalysisResult(true);
						}
						type = this._reportCollectionAccessor.GetCollectionItemType(text);
						IL_00B3:
						string text2 = base.EvaluateSimpleName(node.Name);
						PropertyInfo propertyInfo = base.GetPropertyInfo(type, text2);
						if (propertyInfo != null)
						{
							return new ExpressionAnalysisResult(base.IsObjectReference(propertyInfo.PropertyType));
						}
						MethodInfo methodInfo = base.GetMethodInfo(type, text2);
						if (methodInfo != null)
						{
							return new ExpressionAnalysisResult(base.IsObjectReference(methodInfo.ReturnType));
						}
						throw new MissingMemberException(string.Concat(new string[] { "Property or method '", text2, "' does not exist on Object '", type.Name, "'" }));
					}
					else
					{
						IdentifierNameSyntax identifierNameSyntax2 = node.Expression as IdentifierNameSyntax;
						if (identifierNameSyntax2 != null)
						{
							string text3 = base.EvaluateIdentifierName(identifierNameSyntax2);
							if (!this._enumAccessor.IsSupportedEnum(text3))
							{
								throw new NotSupportedException("The specified enum name is not supported: " + text3);
							}
							return new ExpressionAnalysisResult(false);
						}
					}
				}
				throw new NotSupportedException(string.Format("Unsupported operator for member access expression. Operator syntax: {0}", VisualBasicExtensions.Kind(node.OperatorToken)));
			}
			return new ExpressionAnalysisResult(true);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000487C File Offset: 0x00002A7C
		public bool IsEnabled(MemberAccessExpressionSyntax node)
		{
			return true;
		}

		// Token: 0x0400003E RID: 62
		private readonly ReportCollectionAccessor _reportCollectionAccessor;

		// Token: 0x0400003F RID: 63
		private readonly EnumAccessor _enumAccessor;

		// Token: 0x04000040 RID: 64
		private readonly string[] SupportedMethodsWithNoParam = new string[] { "TrimStart", "TrimEnd" };
	}
}
