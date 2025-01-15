using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200010E RID: 270
	internal sealed class FunctionCallParser : IFunctionCallParser
	{
		// Token: 0x06000CC8 RID: 3272 RVA: 0x0002399A File Offset: 0x00021B9A
		public FunctionCallParser(ExpressionLexer lexer, UriQueryExpressionParser parser)
		{
			ExceptionUtils.CheckArgumentNotNull<ExpressionLexer>(lexer, "lexer");
			ExceptionUtils.CheckArgumentNotNull<UriQueryExpressionParser>(parser, "parser");
			this.lexer = lexer;
			this.parser = parser;
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000CC9 RID: 3273 RVA: 0x000239C8 File Offset: 0x00021BC8
		public UriQueryExpressionParser UriQueryExpressionParser
		{
			get
			{
				return this.parser;
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000CCA RID: 3274 RVA: 0x000239D0 File Offset: 0x00021BD0
		public ExpressionLexer Lexer
		{
			get
			{
				return this.lexer;
			}
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x000239D8 File Offset: 0x00021BD8
		public QueryToken ParseIdentifierAsFunction(QueryToken parent)
		{
			string text;
			if (this.Lexer.PeekNextToken().Kind == ExpressionTokenKind.Dot)
			{
				text = this.Lexer.ReadDottedIdentifier(false);
			}
			else
			{
				text = this.Lexer.CurrentToken.Text;
				this.Lexer.NextToken();
			}
			FunctionParameterToken[] array = this.ParseArgumentListOrEntityKeyList();
			return new FunctionCallToken(text, array, parent);
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x00023A34 File Offset: 0x00021C34
		public FunctionParameterToken[] ParseArgumentListOrEntityKeyList()
		{
			if (this.Lexer.CurrentToken.Kind != ExpressionTokenKind.OpenParen)
			{
				throw new ODataException(Strings.UriQueryExpressionParser_OpenParenExpected(this.Lexer.CurrentToken.Position, this.Lexer.ExpressionText));
			}
			this.Lexer.NextToken();
			FunctionParameterToken[] array;
			if (this.Lexer.CurrentToken.Kind == ExpressionTokenKind.CloseParen)
			{
				array = FunctionParameterToken.EmptyParameterList;
			}
			else
			{
				array = this.ParseArguments();
			}
			if (this.Lexer.CurrentToken.Kind != ExpressionTokenKind.CloseParen)
			{
				throw new ODataException(Strings.UriQueryExpressionParser_CloseParenOrCommaExpected(this.Lexer.CurrentToken.Position, this.Lexer.ExpressionText));
			}
			this.Lexer.NextToken();
			return array;
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x00023AFC File Offset: 0x00021CFC
		public FunctionParameterToken[] ParseArguments()
		{
			ICollection<FunctionParameterToken> collection;
			if (this.TryReadArgumentsAsNamedValues(out collection))
			{
				return Enumerable.ToArray<FunctionParameterToken>(collection);
			}
			return this.ReadArgumentsAsPositionalValues().ToArray();
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x00023B28 File Offset: 0x00021D28
		private List<FunctionParameterToken> ReadArgumentsAsPositionalValues()
		{
			List<FunctionParameterToken> list = new List<FunctionParameterToken>();
			for (;;)
			{
				list.Add(new FunctionParameterToken(null, this.parser.ParseExpression()));
				if (this.Lexer.CurrentToken.Kind != ExpressionTokenKind.Comma)
				{
					break;
				}
				this.Lexer.NextToken();
			}
			return list;
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x00023B78 File Offset: 0x00021D78
		private bool TryReadArgumentsAsNamedValues(out ICollection<FunctionParameterToken> argList)
		{
			if (!this.parser.TrySplitFunctionParameters(out argList))
			{
				return false;
			}
			if (Enumerable.Count<string>(Enumerable.Distinct<string>(Enumerable.Select<FunctionParameterToken, string>(argList, (FunctionParameterToken t) => t.ParameterName))) != argList.Count)
			{
				throw new ODataException(Strings.FunctionCallParser_DuplicateParameterOrEntityKeyName);
			}
			return true;
		}

		// Token: 0x040006F8 RID: 1784
		private readonly ExpressionLexer lexer;

		// Token: 0x040006F9 RID: 1785
		private readonly UriQueryExpressionParser parser;
	}
}
