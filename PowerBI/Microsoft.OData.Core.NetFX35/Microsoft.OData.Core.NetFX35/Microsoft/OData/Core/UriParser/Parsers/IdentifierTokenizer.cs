using System;
using System.Collections.Generic;
using Microsoft.OData.Core.UriParser.Syntactic;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x020001FC RID: 508
	internal sealed class IdentifierTokenizer
	{
		// Token: 0x0600128C RID: 4748 RVA: 0x000434A3 File Offset: 0x000416A3
		public IdentifierTokenizer(HashSet<string> parameters, IFunctionCallParser functionCallParser)
		{
			ExceptionUtils.CheckArgumentNotNull<HashSet<string>>(parameters, "parameters");
			ExceptionUtils.CheckArgumentNotNull<IFunctionCallParser>(functionCallParser, "functionCallParser");
			this.lexer = functionCallParser.Lexer;
			this.parameters = parameters;
			this.functionCallParser = functionCallParser;
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x000434DC File Offset: 0x000416DC
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

		// Token: 0x0600128E RID: 4750 RVA: 0x00043544 File Offset: 0x00041744
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

		// Token: 0x0600128F RID: 4751 RVA: 0x000435C0 File Offset: 0x000417C0
		public QueryToken ParseStarMemberAccess(QueryToken instance)
		{
			if (this.lexer.CurrentToken.Text != "*")
			{
				throw IdentifierTokenizer.ParseError(Strings.UriQueryExpressionParser_CannotCreateStarTokenFromNonStar(this.lexer.CurrentToken.Text));
			}
			this.lexer.NextToken();
			return new StarToken(instance);
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x00043616 File Offset: 0x00041816
		private static Exception ParseError(string message)
		{
			return new ODataException(message);
		}

		// Token: 0x04000804 RID: 2052
		private readonly ExpressionLexer lexer;

		// Token: 0x04000805 RID: 2053
		private readonly HashSet<string> parameters;

		// Token: 0x04000806 RID: 2054
		private readonly IFunctionCallParser functionCallParser;
	}
}
