using System;
using System.Collections.Generic;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000112 RID: 274
	internal sealed class IdentifierTokenizer
	{
		// Token: 0x06000CD7 RID: 3287 RVA: 0x00024116 File Offset: 0x00022316
		public IdentifierTokenizer(HashSet<string> parameters, IFunctionCallParser functionCallParser)
		{
			ExceptionUtils.CheckArgumentNotNull<HashSet<string>>(parameters, "parameters");
			ExceptionUtils.CheckArgumentNotNull<IFunctionCallParser>(functionCallParser, "functionCallParser");
			this.lexer = functionCallParser.Lexer;
			this.parameters = parameters;
			this.functionCallParser = functionCallParser;
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x00024150 File Offset: 0x00022350
		public QueryToken ParseIdentifier(QueryToken parent)
		{
			this.lexer.ValidateToken(ExpressionTokenKind.Identifier);
			bool flag = this.lexer.ExpandIdentifierAsFunction();
			if (flag)
			{
				return this.functionCallParser.ParseIdentifierAsFunction(parent);
			}
			if (this.lexer.PeekNextToken().Kind == ExpressionTokenKind.Dot)
			{
				string text = this.lexer.ReadDottedIdentifier(false);
				return new DottedIdentifierToken(text, parent);
			}
			return this.ParseMemberAccess(parent);
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x000241B8 File Offset: 0x000223B8
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

		// Token: 0x06000CDA RID: 3290 RVA: 0x00024234 File Offset: 0x00022434
		public QueryToken ParseStarMemberAccess(QueryToken instance)
		{
			if (this.lexer.CurrentToken.Text != "*")
			{
				throw IdentifierTokenizer.ParseError(Strings.UriQueryExpressionParser_CannotCreateStarTokenFromNonStar(this.lexer.CurrentToken.Text));
			}
			this.lexer.NextToken();
			return new StarToken(instance);
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x0001F734 File Offset: 0x0001D934
		private static Exception ParseError(string message)
		{
			return new ODataException(message);
		}

		// Token: 0x040006FA RID: 1786
		private readonly ExpressionLexer lexer;

		// Token: 0x040006FB RID: 1787
		private readonly HashSet<string> parameters;

		// Token: 0x040006FC RID: 1788
		private readonly IFunctionCallParser functionCallParser;
	}
}
