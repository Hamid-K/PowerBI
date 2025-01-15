using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000110 RID: 272
	internal static class FunctionParameterParser
	{
		// Token: 0x06000CD2 RID: 3282 RVA: 0x00023FB4 File Offset: 0x000221B4
		internal static bool TrySplitFunctionParameters(this UriQueryExpressionParser parser, out ICollection<FunctionParameterToken> splitParameters)
		{
			return parser.TrySplitOperationParameters(ExpressionTokenKind.CloseParen, out splitParameters);
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x00023FC0 File Offset: 0x000221C0
		internal static bool TrySplitOperationParameters(string parenthesisExpression, ODataUriParserConfiguration configuration, out ICollection<FunctionParameterToken> splitParameters)
		{
			ExpressionLexer expressionLexer = new ExpressionLexer(parenthesisExpression, true, false, true);
			UriQueryExpressionParser uriQueryExpressionParser = new UriQueryExpressionParser(configuration.Settings.FilterLimit, expressionLexer);
			bool flag = uriQueryExpressionParser.TrySplitOperationParameters(ExpressionTokenKind.End, out splitParameters);
			if (Enumerable.Count<string>(Enumerable.Distinct<string>(Enumerable.Select<FunctionParameterToken, string>(splitParameters, (FunctionParameterToken t) => t.ParameterName))) != splitParameters.Count)
			{
				throw new ODataException(Strings.FunctionCallParser_DuplicateParameterOrEntityKeyName);
			}
			return flag;
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x00024038 File Offset: 0x00022238
		private static bool TrySplitOperationParameters(this UriQueryExpressionParser parser, ExpressionTokenKind endTokenKind, out ICollection<FunctionParameterToken> splitParameters)
		{
			ExpressionLexer lexer = parser.Lexer;
			List<FunctionParameterToken> list = new List<FunctionParameterToken>();
			splitParameters = list;
			ExpressionToken expressionToken = lexer.CurrentToken;
			if (expressionToken.Kind == endTokenKind)
			{
				return true;
			}
			if (expressionToken.Kind != ExpressionTokenKind.Identifier || lexer.PeekNextToken().Kind != ExpressionTokenKind.Equal)
			{
				return false;
			}
			while (expressionToken.Kind != endTokenKind)
			{
				lexer.ValidateToken(ExpressionTokenKind.Identifier);
				string identifier = lexer.CurrentToken.GetIdentifier();
				lexer.NextToken();
				lexer.ValidateToken(ExpressionTokenKind.Equal);
				lexer.NextToken();
				QueryToken queryToken = parser.ParseExpression();
				list.Add(new FunctionParameterToken(identifier, queryToken));
				expressionToken = lexer.CurrentToken;
				if (expressionToken.Kind == ExpressionTokenKind.Comma)
				{
					lexer.NextToken();
					expressionToken = lexer.CurrentToken;
					if (expressionToken.Kind == endTokenKind)
					{
						throw new ODataException(Strings.ExpressionLexer_SyntaxError(lexer.Position, lexer.ExpressionText));
					}
				}
			}
			return true;
		}
	}
}
