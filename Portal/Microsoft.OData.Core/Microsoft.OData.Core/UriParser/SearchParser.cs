using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200015C RID: 348
	internal sealed class SearchParser
	{
		// Token: 0x060011BD RID: 4541 RVA: 0x00033DE9 File Offset: 0x00031FE9
		internal SearchParser(int maxDepth)
		{
			this.maxDepth = maxDepth;
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x00033DF8 File Offset: 0x00031FF8
		internal QueryToken ParseSearch(string expressionText)
		{
			this.recursionDepth = 0;
			this.lexer = new SearchLexer(expressionText);
			QueryToken queryToken = this.ParseExpression();
			this.lexer.ValidateToken(ExpressionTokenKind.End);
			return queryToken;
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x0002CC1C File Offset: 0x0002AE1C
		private static Exception ParseError(string message)
		{
			return new ODataException(message);
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x00033E2C File Offset: 0x0003202C
		private QueryToken ParseExpression()
		{
			this.RecurseEnter();
			QueryToken queryToken = this.ParseLogicalOr();
			this.RecurseLeave();
			return queryToken;
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x00033E50 File Offset: 0x00032050
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

		// Token: 0x060011C2 RID: 4546 RVA: 0x00033E9C File Offset: 0x0003209C
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

		// Token: 0x060011C3 RID: 4547 RVA: 0x00033F2C File Offset: 0x0003212C
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

		// Token: 0x060011C4 RID: 4548 RVA: 0x00033F7C File Offset: 0x0003217C
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

		// Token: 0x060011C5 RID: 4549 RVA: 0x0003400C File Offset: 0x0003220C
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

		// Token: 0x060011C6 RID: 4550 RVA: 0x000340B8 File Offset: 0x000322B8
		private bool TokenIdentifierIs(string id)
		{
			return this.lexer.CurrentToken.IdentifierIs(id, false);
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x000340DA File Offset: 0x000322DA
		private void RecurseEnter()
		{
			this.recursionDepth++;
			if (this.recursionDepth > this.maxDepth)
			{
				throw new ODataException(Strings.UriQueryExpressionParser_TooDeep);
			}
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x00034103 File Offset: 0x00032303
		private void RecurseLeave()
		{
			this.recursionDepth--;
		}

		// Token: 0x0400081D RID: 2077
		private readonly int maxDepth;

		// Token: 0x0400081E RID: 2078
		private int recursionDepth;

		// Token: 0x0400081F RID: 2079
		private ExpressionLexer lexer;
	}
}
