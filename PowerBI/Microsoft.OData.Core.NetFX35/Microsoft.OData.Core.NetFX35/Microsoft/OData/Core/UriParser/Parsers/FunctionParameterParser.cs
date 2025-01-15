using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Core.UriParser.Syntactic;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x020001FB RID: 507
	internal static class FunctionParameterParser
	{
		// Token: 0x06001288 RID: 4744 RVA: 0x00043334 File Offset: 0x00041534
		internal static bool TrySplitFunctionParameters(this UriQueryExpressionParser parser, out ICollection<FunctionParameterToken> splitParameters)
		{
			return parser.TrySplitOperationParameters(ExpressionTokenKind.CloseParen, out splitParameters);
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x00043348 File Offset: 0x00041548
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

		// Token: 0x0600128A RID: 4746 RVA: 0x000433C0 File Offset: 0x000415C0
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
