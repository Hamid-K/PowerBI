using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000162 RID: 354
	internal sealed class SelectExpandTermParser
	{
		// Token: 0x06001208 RID: 4616 RVA: 0x0003595A File Offset: 0x00033B5A
		internal SelectExpandTermParser(ExpressionLexer lexer, int maxPathLength, bool isSelect)
		{
			this.lexer = lexer;
			this.maxPathLength = maxPathLength;
			this.isSelect = isSelect;
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x00035978 File Offset: 0x00033B78
		internal PathSegmentToken ParseTerm(bool allowRef = false)
		{
			PathSegmentToken pathSegmentToken = this.ParseSegment(null, allowRef);
			if (pathSegmentToken != null)
			{
				int num = 1;
				this.CheckPathLength(num);
				while (this.lexer.CurrentToken.Kind == ExpressionTokenKind.Slash)
				{
					this.lexer.NextToken();
					if (num > 1 && this.lexer.CurrentToken.Kind == ExpressionTokenKind.End)
					{
						break;
					}
					pathSegmentToken = this.ParseSegment(pathSegmentToken, allowRef);
					if (pathSegmentToken != null)
					{
						this.CheckPathLength(++num);
					}
				}
				return pathSegmentToken;
			}
			return null;
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x000359EF File Offset: 0x00033BEF
		private void CheckPathLength(int pathLength)
		{
			if (pathLength > this.maxPathLength)
			{
				throw new ODataException(Strings.UriQueryExpressionParser_TooDeep);
			}
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x00035A08 File Offset: 0x00033C08
		private PathSegmentToken ParseSegment(PathSegmentToken previousSegment, bool allowRef)
		{
			if (this.lexer.CurrentToken.Text.StartsWith("$", StringComparison.Ordinal) && (!allowRef || this.lexer.CurrentToken.Text != "$ref"))
			{
				throw new ODataException(Strings.UriSelectParser_SystemTokenInSelectExpand(this.lexer.CurrentToken.Text, this.lexer.ExpressionText));
			}
			if (!this.isSelect)
			{
				if (previousSegment != null && previousSegment.Identifier == "*" && this.lexer.CurrentToken.GetIdentifier() != "$ref")
				{
					throw new ODataException(Strings.ExpressionToken_OnlyRefAllowWithStarInExpand);
				}
				if (previousSegment != null && previousSegment.Identifier == "$ref")
				{
					throw new ODataException(Strings.ExpressionToken_NoPropAllowedAfterRef);
				}
			}
			string text;
			if (this.lexer.PeekNextToken().Kind == ExpressionTokenKind.Dot)
			{
				text = this.lexer.ReadDottedIdentifier(this.isSelect);
			}
			else if (this.lexer.CurrentToken.Kind == ExpressionTokenKind.Star)
			{
				if (this.lexer.PeekNextToken().Kind == ExpressionTokenKind.Slash && this.isSelect)
				{
					throw new ODataException(Strings.ExpressionToken_IdentifierExpected(this.lexer.Position));
				}
				if (previousSegment != null && !this.isSelect)
				{
					throw new ODataException(Strings.ExpressionToken_NoSegmentAllowedBeforeStarInExpand);
				}
				text = this.lexer.CurrentToken.Text;
				this.lexer.NextToken();
			}
			else
			{
				text = this.lexer.CurrentToken.GetIdentifier();
				this.lexer.NextToken();
			}
			return new NonSystemToken(text, null, previousSegment);
		}

		// Token: 0x0400083A RID: 2106
		private readonly ExpressionLexer lexer;

		// Token: 0x0400083B RID: 2107
		private readonly int maxPathLength;

		// Token: 0x0400083C RID: 2108
		private readonly bool isSelect;
	}
}
