using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000119 RID: 281
	internal sealed class SearchParser
	{
		// Token: 0x06000D0C RID: 3340 RVA: 0x00025BC7 File Offset: 0x00023DC7
		internal SearchParser(int maxDepth)
		{
			this.maxDepth = maxDepth;
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x00025BD8 File Offset: 0x00023DD8
		internal QueryToken ParseSearch(string expressionText)
		{
			this.recursionDepth = 0;
			this.lexer = new SearchLexer(expressionText);
			QueryToken queryToken = this.ParseExpression();
			this.lexer.ValidateToken(ExpressionTokenKind.End);
			return queryToken;
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x0001F734 File Offset: 0x0001D934
		private static Exception ParseError(string message)
		{
			return new ODataException(message);
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x00025C0C File Offset: 0x00023E0C
		private QueryToken ParseExpression()
		{
			this.RecurseEnter();
			QueryToken queryToken = this.ParseLogicalOr();
			this.RecurseLeave();
			return queryToken;
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x00025C30 File Offset: 0x00023E30
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

		// Token: 0x06000D11 RID: 3345 RVA: 0x00025C7C File Offset: 0x00023E7C
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

		// Token: 0x06000D12 RID: 3346 RVA: 0x00025D0C File Offset: 0x00023F0C
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

		// Token: 0x06000D13 RID: 3347 RVA: 0x00025D5C File Offset: 0x00023F5C
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

		// Token: 0x06000D14 RID: 3348 RVA: 0x00025DEC File Offset: 0x00023FEC
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

		// Token: 0x06000D15 RID: 3349 RVA: 0x00025E98 File Offset: 0x00024098
		private bool TokenIdentifierIs(string id)
		{
			return this.lexer.CurrentToken.IdentifierIs(id, false);
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x00025EBA File Offset: 0x000240BA
		private void RecurseEnter()
		{
			this.recursionDepth++;
			if (this.recursionDepth > this.maxDepth)
			{
				throw new ODataException(Strings.UriQueryExpressionParser_TooDeep);
			}
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x00025EE3 File Offset: 0x000240E3
		private void RecurseLeave()
		{
			this.recursionDepth--;
		}

		// Token: 0x04000707 RID: 1799
		private readonly int maxDepth;

		// Token: 0x04000708 RID: 1800
		private int recursionDepth;

		// Token: 0x04000709 RID: 1801
		private ExpressionLexer lexer;
	}
}
