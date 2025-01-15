using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Newtonsoft.Json;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions.Utils
{
	// Token: 0x02000033 RID: 51
	internal class SyntaxTreeSerializer
	{
		// Token: 0x060000FF RID: 255 RVA: 0x00005FD0 File Offset: 0x000041D0
		public SyntaxTreeSerializer(int maxAllowedNodeCount)
		{
			this._maxAllowedNodeCount = maxAllowedNodeCount;
			this._vbConstantIdentifier = new VisualBasicConstantIdentifier();
			this._reportCollectionAccessor = new ReportCollectionAccessor(null);
			this._functionFactory = new FunctionFactory(null);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00006004 File Offset: 0x00004204
		public string SerializeSyntaxTree(SyntaxTree syntaxTree)
		{
			StringWriter stringWriter = new StringWriter();
			int num = 0;
			string text;
			using (JsonWriter jsonWriter = new JsonTextWriter(stringWriter))
			{
				jsonWriter.Formatting = Formatting.None;
				this.TraverseSyntaxGraph(jsonWriter, syntaxTree.GetRoot(default(CancellationToken)), ref num);
				text = stringWriter.ToString();
			}
			return text;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00006068 File Offset: 0x00004268
		private void TraverseSyntaxGraph(JsonWriter jsonWriter, SyntaxNode syntaxNode, ref int nodeCount)
		{
			nodeCount++;
			if (nodeCount > this._maxAllowedNodeCount)
			{
				throw new InvalidOperationException(string.Format("Syntax tree node count has crossed the max limit of {0}.", this._maxAllowedNodeCount));
			}
			jsonWriter.WriteStartObject();
			string name = syntaxNode.GetType().Name;
			string text = VisualBasicExtensions.Kind(syntaxNode).ToString();
			jsonWriter.WritePropertyName("Syntax");
			jsonWriter.WriteValue(name);
			if (!name.StartsWith(text))
			{
				jsonWriter.WritePropertyName("Kind");
				jsonWriter.WriteValue(text);
			}
			if (syntaxNode.ContainsDiagnostics)
			{
				jsonWriter.WritePropertyName("ContainsDiagnostics");
				jsonWriter.WriteValue(true);
			}
			this.WriteNodeSpecificProperties(jsonWriter, syntaxNode);
			IEnumerable<SyntaxNode> enumerable = syntaxNode.ChildNodes();
			if (enumerable.Any<SyntaxNode>())
			{
				jsonWriter.WritePropertyName("Children");
				jsonWriter.WriteStartArray();
				foreach (SyntaxNode syntaxNode2 in enumerable)
				{
					this.TraverseSyntaxGraph(jsonWriter, syntaxNode2, ref nodeCount);
				}
				jsonWriter.WriteEndArray();
			}
			jsonWriter.WriteEndObject();
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00006188 File Offset: 0x00004388
		private void WriteNodeSpecificProperties(JsonWriter jsonWriter, SyntaxNode syntaxNode)
		{
			IdentifierNameSyntax identifierNameSyntax = syntaxNode as IdentifierNameSyntax;
			if (identifierNameSyntax != null)
			{
				this.WriteIdentiferNameProperties(jsonWriter, identifierNameSyntax);
			}
			LiteralExpressionSyntax literalExpressionSyntax = syntaxNode as LiteralExpressionSyntax;
			if (literalExpressionSyntax != null)
			{
				this.WriteLiteralExpressionSyntaxProperties(jsonWriter, literalExpressionSyntax);
			}
			BinaryExpressionSyntax binaryExpressionSyntax = syntaxNode as BinaryExpressionSyntax;
			if (binaryExpressionSyntax != null)
			{
				this.WriteBinaryExpressionSyntaxProperties(jsonWriter, binaryExpressionSyntax);
			}
			MemberAccessExpressionSyntax memberAccessExpressionSyntax = syntaxNode as MemberAccessExpressionSyntax;
			if (memberAccessExpressionSyntax != null)
			{
				this.WriteMemberAccessExpressionSyntaxProperties(jsonWriter, memberAccessExpressionSyntax);
			}
			ArgumentListSyntax argumentListSyntax = syntaxNode as ArgumentListSyntax;
			if (argumentListSyntax != null)
			{
				this.WriteArgumentListSyntaxProperties(jsonWriter, argumentListSyntax);
			}
			TernaryConditionalExpressionSyntax ternaryConditionalExpressionSyntax = syntaxNode as TernaryConditionalExpressionSyntax;
			if (ternaryConditionalExpressionSyntax != null)
			{
				this.WriteTernaryConditionalExpressionSyntaxProperties(jsonWriter, ternaryConditionalExpressionSyntax);
			}
			UnaryExpressionSyntax unaryExpressionSyntax = syntaxNode as UnaryExpressionSyntax;
			if (unaryExpressionSyntax != null)
			{
				this.WriteUnaryExpressionSyntaxProperties(jsonWriter, unaryExpressionSyntax);
			}
			ParenthesizedExpressionSyntax parenthesizedExpressionSyntax = syntaxNode as ParenthesizedExpressionSyntax;
			if (parenthesizedExpressionSyntax != null)
			{
				this.WriteParenthesizedExpressionSyntaxProperties(jsonWriter, parenthesizedExpressionSyntax);
			}
			CollectionInitializerSyntax collectionInitializerSyntax = syntaxNode as CollectionInitializerSyntax;
			if (collectionInitializerSyntax != null)
			{
				this.WriteCollectionInitializerSyntaxProperties(jsonWriter, collectionInitializerSyntax);
			}
			PredefinedCastExpressionSyntax predefinedCastExpressionSyntax = syntaxNode as PredefinedCastExpressionSyntax;
			if (predefinedCastExpressionSyntax != null)
			{
				this.WritePredefinedCastExpressionSyntaxProperties(jsonWriter, predefinedCastExpressionSyntax);
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000625C File Offset: 0x0000445C
		private void WriteIdentiferNameProperties(JsonWriter jsonWriter, IdentifierNameSyntax syntax)
		{
			string valueText = syntax.Identifier.ValueText;
			string text = syntax.Identifier.Text;
			if (this.IsKnownIdentifierName(valueText) || this.IsSimpleMemberAccessExpression(syntax.Parent))
			{
				jsonWriter.WritePropertyName("IdentifierName");
				jsonWriter.WriteValue(text);
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000062B0 File Offset: 0x000044B0
		private bool IsSimpleMemberAccessExpression(SyntaxNode syntax)
		{
			return syntax != null && syntax is MemberAccessExpressionSyntax && VisualBasicExtensions.IsKind(syntax, 291);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000062CC File Offset: 0x000044CC
		private void WriteLiteralExpressionSyntaxProperties(JsonWriter jsonWriter, LiteralExpressionSyntax syntax)
		{
			SyntaxToken token = syntax.Token;
			if (token.Value == null)
			{
				return;
			}
			Type type = token.Value.GetType();
			int length = token.ValueText.Length;
			jsonWriter.WritePropertyName("ValueType");
			jsonWriter.WriteValue(type.Name);
			jsonWriter.WritePropertyName("ValueLength");
			jsonWriter.WriteValue(length);
			if (this.IsFloatingPointNumber(type))
			{
				int num = token.ValueText.IndexOf('.');
				jsonWriter.WritePropertyName("DecimalPointIndex");
				jsonWriter.WriteValue(num);
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00006357 File Offset: 0x00004557
		private void WriteBinaryExpressionSyntaxProperties(JsonWriter jsonWriter, BinaryExpressionSyntax syntax)
		{
			this.WriteOperatorTokenProperty(jsonWriter, syntax.OperatorToken);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00006366 File Offset: 0x00004566
		private void WriteMemberAccessExpressionSyntaxProperties(JsonWriter jsonWriter, MemberAccessExpressionSyntax syntax)
		{
			this.WriteOperatorTokenProperty(jsonWriter, syntax.OperatorToken);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00006378 File Offset: 0x00004578
		private void WriteArgumentListSyntaxProperties(JsonWriter jsonWriter, ArgumentListSyntax syntax)
		{
			this.WriteOpenCloseParanTokenProperties(jsonWriter, syntax.OpenParenToken, syntax.CloseParenToken);
			jsonWriter.WritePropertyName("ArgumentCount");
			jsonWriter.WriteValue(syntax.Arguments.Count);
			jsonWriter.WritePropertyName("SeparatorCount");
			jsonWriter.WriteValue(syntax.Arguments.SeparatorCount);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000063D8 File Offset: 0x000045D8
		private void WriteTernaryConditionalExpressionSyntaxProperties(JsonWriter jsonWriter, TernaryConditionalExpressionSyntax syntax)
		{
			this.WriteOpenCloseParanTokenProperties(jsonWriter, syntax.OpenParenToken, syntax.CloseParenToken);
			jsonWriter.WritePropertyName("FirstCommaToken");
			jsonWriter.WriteValue(syntax.FirstCommaToken.Text);
			jsonWriter.WritePropertyName("SecondCommaToken");
			jsonWriter.WriteValue(syntax.SecondCommaToken.Text);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00006436 File Offset: 0x00004636
		private void WriteUnaryExpressionSyntaxProperties(JsonWriter jsonWriter, UnaryExpressionSyntax syntax)
		{
			this.WriteOperatorTokenProperty(jsonWriter, syntax.OperatorToken);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00006445 File Offset: 0x00004645
		private void WriteParenthesizedExpressionSyntaxProperties(JsonWriter jsonWriter, ParenthesizedExpressionSyntax syntax)
		{
			this.WriteOpenCloseParanTokenProperties(jsonWriter, syntax.OpenParenToken, syntax.CloseParenToken);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000645C File Offset: 0x0000465C
		private void WriteCollectionInitializerSyntaxProperties(JsonWriter jsonWriter, CollectionInitializerSyntax syntax)
		{
			jsonWriter.WritePropertyName("ItemCount");
			jsonWriter.WriteValue(syntax.Initializers.Count);
			jsonWriter.WritePropertyName("SeparatorCount");
			jsonWriter.WriteValue(syntax.Initializers.SeparatorCount);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000064A8 File Offset: 0x000046A8
		private void WritePredefinedCastExpressionSyntaxProperties(JsonWriter jsonWriter, PredefinedCastExpressionSyntax syntax)
		{
			jsonWriter.WritePropertyName("Kind");
			jsonWriter.WriteValue(syntax.Keyword.Text);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000064D4 File Offset: 0x000046D4
		private void WriteOperatorTokenProperty(JsonWriter jsonWriter, SyntaxToken operatorToken)
		{
			jsonWriter.WritePropertyName("OperatorToken");
			jsonWriter.WriteValue(operatorToken.Text);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000064EE File Offset: 0x000046EE
		private void WriteOpenCloseParanTokenProperties(JsonWriter jsonWriter, SyntaxToken openParanToken, SyntaxToken closeParanToken)
		{
			jsonWriter.WritePropertyName("OpenParenToken");
			jsonWriter.WriteValue(openParanToken.Text);
			jsonWriter.WritePropertyName("CloseParenToken");
			jsonWriter.WriteValue(closeParanToken.Text);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00006520 File Offset: 0x00004720
		private bool IsFloatingPointNumber(Type valueType)
		{
			return valueType == typeof(float) || valueType == typeof(double) || valueType == typeof(decimal);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00006558 File Offset: 0x00004758
		public bool IsKnownIdentifierName(string identifierName)
		{
			return this._vbConstantIdentifier.IsIdentifierVisualBasicConstant(identifierName) || this._functionFactory.Supports(identifierName) || this._reportCollectionAccessor.IsValidCollectionName(identifierName);
		}

		// Token: 0x04000065 RID: 101
		private const string SyntaxProperty = "Syntax";

		// Token: 0x04000066 RID: 102
		private const string KindProperty = "Kind";

		// Token: 0x04000067 RID: 103
		private const string ChildrenProperty = "Children";

		// Token: 0x04000068 RID: 104
		private const string ContainsDiagnosticsProperty = "ContainsDiagnostics";

		// Token: 0x04000069 RID: 105
		private const string IdentifierNameProperty = "IdentifierName";

		// Token: 0x0400006A RID: 106
		private const string ValueTypeProperty = "ValueType";

		// Token: 0x0400006B RID: 107
		private const string ValueLengthProperty = "ValueLength";

		// Token: 0x0400006C RID: 108
		private const string DecimalPointIndexProperty = "DecimalPointIndex";

		// Token: 0x0400006D RID: 109
		private const string OperatorTokenProperty = "OperatorToken";

		// Token: 0x0400006E RID: 110
		private const string OpenParenTokenProperty = "OpenParenToken";

		// Token: 0x0400006F RID: 111
		private const string CloseParenTokenProperty = "CloseParenToken";

		// Token: 0x04000070 RID: 112
		private const string ArgumentCountProperty = "ArgumentCount";

		// Token: 0x04000071 RID: 113
		private const string ItemCountProperty = "ItemCount";

		// Token: 0x04000072 RID: 114
		private const string SeperatorCountProperty = "SeparatorCount";

		// Token: 0x04000073 RID: 115
		private const string FirstCommaTokenProperty = "FirstCommaToken";

		// Token: 0x04000074 RID: 116
		private const string SecondCommaTokenProperty = "SecondCommaToken";

		// Token: 0x04000075 RID: 117
		private readonly int _maxAllowedNodeCount;

		// Token: 0x04000076 RID: 118
		private readonly VisualBasicConstantIdentifier _vbConstantIdentifier;

		// Token: 0x04000077 RID: 119
		private readonly ReportCollectionAccessor _reportCollectionAccessor;

		// Token: 0x04000078 RID: 120
		private readonly FunctionFactory _functionFactory;
	}
}
