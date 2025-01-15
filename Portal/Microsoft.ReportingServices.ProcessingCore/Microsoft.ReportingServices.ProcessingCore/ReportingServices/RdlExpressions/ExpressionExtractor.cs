using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.VisualBasic;

namespace Microsoft.ReportingServices.RdlExpressions
{
	// Token: 0x0200057A RID: 1402
	internal sealed class ExpressionExtractor
	{
		// Token: 0x060050CB RID: 20683 RVA: 0x001539DC File Offset: 0x00151BDC
		internal ExpressionExtractor(ExpressionUsage expressionUsage)
		{
			this.m_expressionUsage = expressionUsage;
			foreach (global::System.Reflection.FieldInfo fieldInfo in (typeof(Constants) as TypeInfo).DeclaredFields)
			{
				this.m_vbConstantsName.Add(fieldInfo.Name);
			}
		}

		// Token: 0x060050CC RID: 20684 RVA: 0x00153A60 File Offset: 0x00151C60
		public void ExtractExpressionTelemetry(ExpressionInfo expressionInfo)
		{
			if (expressionInfo == null)
			{
				return;
			}
			switch (expressionInfo.Type)
			{
			case ExpressionInfo.Types.Expression:
			case ExpressionInfo.Types.Aggregate:
			case ExpressionInfo.Types.Lookup_OneValue:
			case ExpressionInfo.Types.Lookup_MultiValue:
			{
				ExpressionSyntax andCacheExpressionSyntax = expressionInfo.GetAndCacheExpressionSyntax();
				this.TraverseSyntaxTree(andCacheExpressionSyntax);
				if (expressionInfo.ExpressionWasTransformed())
				{
					ExpressionSyntax expressionSyntaxForOriginalExpression = expressionInfo.GetExpressionSyntaxForOriginalExpression();
					this.TraverseSyntaxTree(expressionSyntaxForOriginalExpression);
				}
				break;
			}
			case ExpressionInfo.Types.Field:
			case ExpressionInfo.Types.Constant:
			case ExpressionInfo.Types.Token:
				break;
			default:
				return;
			}
		}

		// Token: 0x060050CD RID: 20685 RVA: 0x00153AC0 File Offset: 0x00151CC0
		private void TraverseSyntaxTree(SyntaxNode node)
		{
			if (node is LiteralExpressionSyntax)
			{
				this.m_expressionUsage.HasLiterals = true;
			}
			else
			{
				MemberAccessExpressionSyntax memberAccessExpressionSyntax = node as MemberAccessExpressionSyntax;
				if (memberAccessExpressionSyntax != null)
				{
					this.ExtractReportCollectionType(memberAccessExpressionSyntax);
				}
				else
				{
					InvocationExpressionSyntax invocationExpressionSyntax = node as InvocationExpressionSyntax;
					if (invocationExpressionSyntax != null)
					{
						this.ExtractFunction(invocationExpressionSyntax);
					}
					else
					{
						PredefinedCastExpressionSyntax predefinedCastExpressionSyntax = node as PredefinedCastExpressionSyntax;
						if (predefinedCastExpressionSyntax != null)
						{
							this.ExtractPredefinedCastFunction(predefinedCastExpressionSyntax);
						}
					}
				}
			}
			this.AddToSyntaxNodeCollection(node);
			foreach (SyntaxNode syntaxNode in node.ChildNodes())
			{
				this.TraverseSyntaxTree(syntaxNode);
			}
		}

		// Token: 0x060050CE RID: 20686 RVA: 0x00153B64 File Offset: 0x00151D64
		private void ExtractReportCollectionType(MemberAccessExpressionSyntax node)
		{
			if (VisualBasicExtensions.IsKind(node.OperatorToken, 634))
			{
				IdentifierNameSyntax identifierNameSyntax = node.Expression as IdentifierNameSyntax;
				if (identifierNameSyntax != null)
				{
					string text = this.GetSimpleNameIdentifier(identifierNameSyntax).ToUpperInvariant();
					if (text == "PARAMETERS")
					{
						this.m_expressionUsage.HasParameters = true;
						return;
					}
					if (text == "FIELDS")
					{
						this.m_expressionUsage.HasFields = true;
						return;
					}
					if (text == "VARIABLES")
					{
						this.m_expressionUsage.HasVariables = true;
						return;
					}
					if (text == "GLOBALS")
					{
						this.m_expressionUsage.HasGlobals = true;
						return;
					}
					if (!(text == "USER"))
					{
						return;
					}
					this.m_expressionUsage.HasUser = true;
				}
			}
		}

		// Token: 0x060050CF RID: 20687 RVA: 0x00153C28 File Offset: 0x00151E28
		private void ExtractFunction(InvocationExpressionSyntax node)
		{
			MemberAccessExpressionSyntax memberAccessExpressionSyntax = node.Expression as MemberAccessExpressionSyntax;
			string text;
			if (memberAccessExpressionSyntax != null)
			{
				InvocationExpressionSyntax invocationExpressionSyntax = memberAccessExpressionSyntax.Expression as InvocationExpressionSyntax;
				if (invocationExpressionSyntax != null)
				{
					text = memberAccessExpressionSyntax.Name.ToString();
					this.ExtractFunction(invocationExpressionSyntax);
					goto IL_005D;
				}
			}
			MemberAccessExpressionSyntax memberAccessExpressionSyntax2 = node.Expression as MemberAccessExpressionSyntax;
			if (memberAccessExpressionSyntax2 != null)
			{
				text = memberAccessExpressionSyntax2.ToString();
			}
			else
			{
				text = this.GetSimpleNameIdentifier((SimpleNameSyntax)node.Expression);
			}
			IL_005D:
			text = (text.StartsWith("Code.", StringComparison.InvariantCultureIgnoreCase) ? "Code" : text);
			this.AddToFunctionCollection(text);
		}

		// Token: 0x060050D0 RID: 20688 RVA: 0x00153CB0 File Offset: 0x00151EB0
		private void ExtractPredefinedCastFunction(PredefinedCastExpressionSyntax node)
		{
			this.AddToFunctionCollection(node.Keyword.ValueText);
		}

		// Token: 0x060050D1 RID: 20689 RVA: 0x00153CD4 File Offset: 0x00151ED4
		private string GetSimpleNameIdentifier(SimpleNameSyntax simpleNameSyntax)
		{
			return simpleNameSyntax.Identifier.ValueText;
		}

		// Token: 0x060050D2 RID: 20690 RVA: 0x00153CF0 File Offset: 0x00151EF0
		private void AddToFunctionCollection(string functionName)
		{
			if (functionName.Contains("!"))
			{
				return;
			}
			functionName = functionName.ToLower();
			int num;
			this.m_expressionUsage.FunctionCollection.TryGetValue(functionName, out num);
			this.m_expressionUsage.FunctionCollection[functionName] = num + 1;
		}

		// Token: 0x060050D3 RID: 20691 RVA: 0x00153D3C File Offset: 0x00151F3C
		private void AddToSyntaxNodeCollection(SyntaxNode node)
		{
			string name = node.GetType().Name;
			HashSet<string> hashSet;
			if (!this.m_expressionUsage.SyntaxNodeCollection.TryGetValue(name, out hashSet))
			{
				hashSet = new HashSet<string>();
				this.m_expressionUsage.SyntaxNodeCollection.Add(name, hashSet);
			}
			BinaryExpressionSyntax binaryExpressionSyntax = node as BinaryExpressionSyntax;
			if (binaryExpressionSyntax != null)
			{
				hashSet.Add(binaryExpressionSyntax.OperatorToken.ValueText);
				return;
			}
			UnaryExpressionSyntax unaryExpressionSyntax = node as UnaryExpressionSyntax;
			if (unaryExpressionSyntax != null)
			{
				hashSet.Add(unaryExpressionSyntax.OperatorToken.ValueText);
				return;
			}
			LiteralExpressionSyntax literalExpressionSyntax = node as LiteralExpressionSyntax;
			if (literalExpressionSyntax != null)
			{
				hashSet.Add(VisualBasicExtensions.Kind(literalExpressionSyntax.Token).ToString());
				return;
			}
			PredefinedCastExpressionSyntax predefinedCastExpressionSyntax = node as PredefinedCastExpressionSyntax;
			if (predefinedCastExpressionSyntax != null)
			{
				hashSet.Add(predefinedCastExpressionSyntax.Keyword.ValueText);
				return;
			}
			IdentifierNameSyntax identifierNameSyntax = node as IdentifierNameSyntax;
			if (identifierNameSyntax != null)
			{
				string valueText = identifierNameSyntax.Identifier.ValueText;
				if (this.m_vbConstantsName.Contains(valueText))
				{
					hashSet.Add(valueText.ToLowerInvariant());
				}
			}
		}

		// Token: 0x040028C1 RID: 10433
		private readonly ExpressionUsage m_expressionUsage;

		// Token: 0x040028C2 RID: 10434
		private readonly HashSet<string> m_vbConstantsName = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x040028C3 RID: 10435
		private const string Parameters = "PARAMETERS";

		// Token: 0x040028C4 RID: 10436
		private const string Fields = "FIELDS";

		// Token: 0x040028C5 RID: 10437
		private const string Variables = "VARIABLES";

		// Token: 0x040028C6 RID: 10438
		private const string User = "USER";

		// Token: 0x040028C7 RID: 10439
		private const string Globals = "GLOBALS";

		// Token: 0x040028C8 RID: 10440
		private const string CustomCodeCall = "Code";
	}
}
