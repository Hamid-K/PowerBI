using System;
using Microsoft.OData.Core.UriParser.Syntactic;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x0200020F RID: 527
	internal sealed class SearchParser
	{
		// Token: 0x0600132D RID: 4909 RVA: 0x00045DFF File Offset: 0x00043FFF
		internal SearchParser(int maxDepth)
		{
			this.maxDepth = maxDepth;
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x00045E10 File Offset: 0x00044010
		internal QueryToken ParseSearch(string expressionText)
		{
			this.recursionDepth = 0;
			this.lexer = new SearchLexer(expressionText);
			QueryToken queryToken = this.ParseExpression();
			this.lexer.ValidateToken(ExpressionTokenKind.End);
			return queryToken;
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x00045E44 File Offset: 0x00044044
		private static Exception ParseError(string message)
		{
			return new ODataException(message);
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x00045E4C File Offset: 0x0004404C
		private QueryToken ParseExpression()
		{
			this.RecurseEnter();
			QueryToken queryToken = this.ParseLogicalOr();
			this.RecurseLeave();
			return queryToken;
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x00045E70 File Offset: 0x00044070
		private QueryToken ParseLogicalOr()
		{
			this.RecurseEnter();
			QueryToken queryToken = this.ParseLogicalAnd();
			while (this.TokenIdentifierIs("OR"))
			{
				this.lexer.NextToken();
				QueryToken queryToken2 = this.ParseLogicalAnd();
				queryToken = new BinaryOperatorToken(BinaryOperatorKind.Or, queryToken, queryToken2);
			}
			this.RecurseLeave();
			return queryToken;
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x00045EBC File Offset: 0x000440BC
		private QueryToken ParseLogicalAnd()
		{
			this.RecurseEnter();
			QueryToken queryToken = this.ParseUnary();
			while (this.TokenIdentifierIs("AND") || this.TokenIdentifierIs("NOT") || this.lexer.CurrentToken.Kind == ExpressionTokenKind.StringLiteral || this.lexer.CurrentToken.Kind == ExpressionTokenKind.OpenParen)
			{
				if (this.TokenIdentifierIs("AND"))
				{
					this.lexer.NextToken();
				}
				QueryToken queryToken2 = this.ParseUnary();
				queryToken = new BinaryOperatorToken(BinaryOperatorKind.And, queryToken, queryToken2);
			}
			this.RecurseLeave();
			return queryToken;
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x00045F4C File Offset: 0x0004414C
		private QueryToken ParseUnary()
		{
			this.RecurseEnter();
			if (this.TokenIdentifierIs("NOT"))
			{
				this.lexer.NextToken();
				QueryToken queryToken = this.ParseUnary();
				this.RecurseLeave();
				return new UnaryOperatorToken(UnaryOperatorKind.Not, queryToken);
			}
			this.RecurseLeave();
			return this.ParsePrimary();
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x00045F9C File Offset: 0x0004419C
		private QueryToken ParsePrimary()
		{
			this.RecurseEnter();
			ExpressionTokenKind kind = this.lexer.CurrentToken.Kind;
			QueryToken queryToken;
			if (kind != ExpressionTokenKind.StringLiteral)
			{
				if (kind != ExpressionTokenKind.OpenParen)
				{
					throw new ODataException(Strings.UriQueryExpressionParser_ExpressionExpected(this.lexer.CurrentToken.Position, this.lexer.ExpressionText));
				}
				queryToken = this.ParseParenExpression();
			}
			else
			{
				queryToken = new StringLiteralToken(this.lexer.CurrentToken.Text);
				this.lexer.NextToken();
			}
			this.RecurseLeave();
			return queryToken;
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x0004602C File Offset: 0x0004422C
		private QueryToken ParseParenExpression()
		{
			if (this.lexer.CurrentToken.Kind != ExpressionTokenKind.OpenParen)
			{
				throw SearchParser.ParseError(Strings.UriQueryExpressionParser_OpenParenExpected(this.lexer.CurrentToken.Position, this.lexer.ExpressionText));
			}
			this.lexer.NextToken();
			QueryToken queryToken = this.ParseExpression();
			if (this.lexer.CurrentToken.Kind != ExpressionTokenKind.CloseParen)
			{
				throw SearchParser.ParseError(Strings.UriQueryExpressionParser_CloseParenOrOperatorExpected(this.lexer.CurrentToken.Position, this.lexer.ExpressionText));
			}
			this.lexer.NextToken();
			return queryToken;
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x000460D8 File Offset: 0x000442D8
		private bool TokenIdentifierIs(string id)
		{
			return this.lexer.CurrentToken.IdentifierIs(id, false);
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x000460FA File Offset: 0x000442FA
		private void RecurseEnter()
		{
			this.recursionDepth++;
			if (this.recursionDepth > this.maxDepth)
			{
				throw new ODataException(Strings.UriQueryExpressionParser_TooDeep);
			}
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x00046123 File Offset: 0x00044323
		private void RecurseLeave()
		{
			this.recursionDepth--;
		}

		// Token: 0x0400082F RID: 2095
		private readonly int maxDepth;

		// Token: 0x04000830 RID: 2096
		private int recursionDepth;

		// Token: 0x04000831 RID: 2097
		private ExpressionLexer lexer;
	}
}
