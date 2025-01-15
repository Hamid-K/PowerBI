using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000153 RID: 339
	internal static class FunctionParameterParser
	{
		// Token: 0x0600117D RID: 4477 RVA: 0x00031AE7 File Offset: 0x0002FCE7
		internal static bool TrySplitFunctionParameters(this UriQueryExpressionParser parser, out ICollection<FunctionParameterToken> splitParameters)
		{
			return parser.TrySplitOperationParameters(ExpressionTokenKind.CloseParen, out splitParameters);
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x00031AF4 File Offset: 0x0002FCF4
		internal static bool TrySplitOperationParameters(string parenthesisExpression, ODataUriParserConfiguration configuration, out ICollection<FunctionParameterToken> splitParameters)
		{
			ExpressionLexer expressionLexer = new ExpressionLexer(parenthesisExpression, true, false, true);
			UriQueryExpressionParser uriQueryExpressionParser = new UriQueryExpressionParser(configuration.Settings.FilterLimit, expressionLexer);
			bool flag = uriQueryExpressionParser.TrySplitOperationParameters(ExpressionTokenKind.End, out splitParameters);
			if (splitParameters.Select((FunctionParameterToken t) => t.ParameterName).Distinct<string>().Count<string>() != splitParameters.Count)
			{
				throw new ODataException(Strings.FunctionCallParser_DuplicateParameterOrEntityKeyName);
			}
			return flag;
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x00031B6C File Offset: 0x0002FD6C
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
