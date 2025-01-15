using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000151 RID: 337
	internal sealed class FunctionCallParser : IFunctionCallParser
	{
		// Token: 0x06001171 RID: 4465 RVA: 0x00031468 File Offset: 0x0002F668
		public FunctionCallParser(ExpressionLexer lexer, UriQueryExpressionParser parser)
			: this(lexer, parser, false)
		{
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x00031473 File Offset: 0x0002F673
		public FunctionCallParser(ExpressionLexer lexer, UriQueryExpressionParser parser, bool restoreStateIfFail)
		{
			ExceptionUtils.CheckArgumentNotNull<ExpressionLexer>(lexer, "lexer");
			ExceptionUtils.CheckArgumentNotNull<UriQueryExpressionParser>(parser, "parser");
			this.lexer = lexer;
			this.parser = parser;
			this.restoreStateIfFail = restoreStateIfFail;
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06001173 RID: 4467 RVA: 0x000314A8 File Offset: 0x0002F6A8
		public UriQueryExpressionParser UriQueryExpressionParser
		{
			get
			{
				return this.parser;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06001174 RID: 4468 RVA: 0x000314B0 File Offset: 0x0002F6B0
		public ExpressionLexer Lexer
		{
			get
			{
				return this.lexer;
			}
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x000314B8 File Offset: 0x0002F6B8
		public bool TryParseIdentifierAsFunction(QueryToken parent, out QueryToken result)
		{
			result = null;
			ExpressionLexer.ExpressionLexerPosition position = this.lexer.SnapshotPosition();
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
			FunctionParameterToken[] array = this.ParseArgumentListOrEntityKeyList(delegate
			{
				this.lexer.RestorePosition(position);
			});
			if (array != null)
			{
				result = new FunctionCallToken(text, array, parent);
			}
			return result != null;
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x0003154C File Offset: 0x0002F74C
		public FunctionParameterToken[] ParseArgumentListOrEntityKeyList(Action restoreAction = null)
		{
			if (this.Lexer.CurrentToken.Kind != ExpressionTokenKind.OpenParen)
			{
				if (this.restoreStateIfFail && restoreAction != null)
				{
					restoreAction();
					return null;
				}
				throw new ODataException(Strings.UriQueryExpressionParser_OpenParenExpected(this.Lexer.CurrentToken.Position, this.Lexer.ExpressionText));
			}
			else
			{
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
				if (this.Lexer.CurrentToken.Kind == ExpressionTokenKind.CloseParen)
				{
					this.Lexer.NextToken();
					return array;
				}
				if (this.restoreStateIfFail && restoreAction != null)
				{
					restoreAction();
					return null;
				}
				throw new ODataException(Strings.UriQueryExpressionParser_CloseParenOrCommaExpected(this.Lexer.CurrentToken.Position, this.Lexer.ExpressionText));
			}
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x0003163C File Offset: 0x0002F83C
		public FunctionParameterToken[] ParseArguments()
		{
			ICollection<FunctionParameterToken> collection;
			if (this.TryReadArgumentsAsNamedValues(out collection))
			{
				return collection.ToArray<FunctionParameterToken>();
			}
			return this.ReadArgumentsAsPositionalValues().ToArray();
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x00031668 File Offset: 0x0002F868
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

		// Token: 0x06001179 RID: 4473 RVA: 0x000316B8 File Offset: 0x0002F8B8
		private bool TryReadArgumentsAsNamedValues(out ICollection<FunctionParameterToken> argList)
		{
			if (!this.parser.TrySplitFunctionParameters(out argList))
			{
				return false;
			}
			if (argList.Select((FunctionParameterToken t) => t.ParameterName).Distinct<string>().Count<string>() != argList.Count)
			{
				throw new ODataException(Strings.FunctionCallParser_DuplicateParameterOrEntityKeyName);
			}
			return true;
		}

		// Token: 0x0400080D RID: 2061
		private readonly ExpressionLexer lexer;

		// Token: 0x0400080E RID: 2062
		private readonly UriQueryExpressionParser parser;

		// Token: 0x0400080F RID: 2063
		private readonly bool restoreStateIfFail;
	}
}
