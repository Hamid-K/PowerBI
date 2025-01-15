using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Core.UriParser.Syntactic;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x020001F9 RID: 505
	internal sealed class FunctionCallParser : IFunctionCallParser
	{
		// Token: 0x06001276 RID: 4726 RVA: 0x00042CD6 File Offset: 0x00040ED6
		public FunctionCallParser(ExpressionLexer lexer, UriQueryExpressionParser parser)
		{
			ExceptionUtils.CheckArgumentNotNull<ExpressionLexer>(lexer, "lexer");
			ExceptionUtils.CheckArgumentNotNull<UriQueryExpressionParser>(parser, "parser");
			this.lexer = lexer;
			this.parser = parser;
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06001277 RID: 4727 RVA: 0x00042D02 File Offset: 0x00040F02
		public UriQueryExpressionParser UriQueryExpressionParser
		{
			get
			{
				return this.parser;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06001278 RID: 4728 RVA: 0x00042D0A File Offset: 0x00040F0A
		public ExpressionLexer Lexer
		{
			get
			{
				return this.lexer;
			}
		}

		// Token: 0x06001279 RID: 4729 RVA: 0x00042D14 File Offset: 0x00040F14
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

		// Token: 0x0600127A RID: 4730 RVA: 0x00042D70 File Offset: 0x00040F70
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

		// Token: 0x0600127B RID: 4731 RVA: 0x00042E38 File Offset: 0x00041038
		public FunctionParameterToken[] ParseArguments()
		{
			ICollection<FunctionParameterToken> collection;
			if (this.TryReadArgumentsAsNamedValues(out collection))
			{
				return Enumerable.ToArray<FunctionParameterToken>(collection);
			}
			return this.ReadArgumentsAsPositionalValues().ToArray();
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x00042E64 File Offset: 0x00041064
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

		// Token: 0x0600127D RID: 4733 RVA: 0x00042EBC File Offset: 0x000410BC
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

		// Token: 0x040007F9 RID: 2041
		private readonly ExpressionLexer lexer;

		// Token: 0x040007FA RID: 2042
		private readonly UriQueryExpressionParser parser;
	}
}
