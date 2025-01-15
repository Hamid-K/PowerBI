using System;
using System.Collections.Generic;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000155 RID: 341
	internal sealed class IdentifierTokenizer
	{
		// Token: 0x06001182 RID: 4482 RVA: 0x00031C4A File Offset: 0x0002FE4A
		public IdentifierTokenizer(HashSet<string> parameters, IFunctionCallParser functionCallParser)
		{
			ExceptionUtils.CheckArgumentNotNull<HashSet<string>>(parameters, "parameters");
			ExceptionUtils.CheckArgumentNotNull<IFunctionCallParser>(functionCallParser, "functionCallParser");
			this.lexer = functionCallParser.Lexer;
			this.parameters = parameters;
			this.functionCallParser = functionCallParser;
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x00031C84 File Offset: 0x0002FE84
		public QueryToken ParseIdentifier(QueryToken parent)
		{
			this.lexer.ValidateToken(ExpressionTokenKind.Identifier);
			bool flag = this.lexer.ExpandIdentifierAsFunction();
			QueryToken queryToken;
			if (flag && this.functionCallParser.TryParseIdentifierAsFunction(parent, out queryToken))
			{
				return queryToken;
			}
			if (this.lexer.PeekNextToken().Kind == ExpressionTokenKind.Dot)
			{
				string text = this.lexer.ReadDottedIdentifier(false);
				return new DottedIdentifierToken(text, parent);
			}
			return this.ParseMemberAccess(parent);
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x00031CF0 File Offset: 0x0002FEF0
		public QueryToken ParseMemberAccess(QueryToken instance)
		{
			if (this.lexer.CurrentToken.Text == "*")
			{
				return this.ParseStarMemberAccess(instance);
			}
			string identifier = this.lexer.CurrentToken.GetIdentifier();
			if (instance == null && this.parameters.Contains(identifier))
			{
				this.lexer.NextToken();
				return new RangeVariableToken(identifier);
			}
			this.lexer.NextToken();
			return new EndPathToken(identifier, instance);
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x00031D6C File Offset: 0x0002FF6C
		public QueryToken ParseStarMemberAccess(QueryToken instance)
		{
			if (this.lexer.CurrentToken.Text != "*")
			{
				throw IdentifierTokenizer.ParseError(Strings.UriQueryExpressionParser_CannotCreateStarTokenFromNonStar(this.lexer.CurrentToken.Text));
			}
			this.lexer.NextToken();
			return new StarToken(instance);
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x0002CC1C File Offset: 0x0002AE1C
		private static Exception ParseError(string message)
		{
			return new ODataException(message);
		}

		// Token: 0x04000810 RID: 2064
		private readonly ExpressionLexer lexer;

		// Token: 0x04000811 RID: 2065
		private readonly HashSet<string> parameters;

		// Token: 0x04000812 RID: 2066
		private readonly IFunctionCallParser functionCallParser;
	}
}
