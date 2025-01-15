using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200011E RID: 286
	internal sealed class SelectExpandTermParser
	{
		// Token: 0x06000D3F RID: 3391 RVA: 0x000269BA File Offset: 0x00024BBA
		internal SelectExpandTermParser(ExpressionLexer lexer, int maxPathLength, bool isSelect)
		{
			this.lexer = lexer;
			this.maxPathLength = maxPathLength;
			this.isSelect = isSelect;
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x000269D8 File Offset: 0x00024BD8
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

		// Token: 0x06000D41 RID: 3393 RVA: 0x00026A4F File Offset: 0x00024C4F
		private void CheckPathLength(int pathLength)
		{
			if (pathLength > this.maxPathLength)
			{
				throw new ODataException(Strings.UriQueryExpressionParser_TooDeep);
			}
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x00026A68 File Offset: 0x00024C68
		private PathSegmentToken ParseSegment(PathSegmentToken previousSegment, bool allowRef)
		{
			if (this.lexer.CurrentToken.Text.StartsWith("$", 4) && (!allowRef || this.lexer.CurrentToken.Text != "$ref"))
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

		// Token: 0x0400071B RID: 1819
		private readonly ExpressionLexer lexer;

		// Token: 0x0400071C RID: 1820
		private readonly int maxPathLength;

		// Token: 0x0400071D RID: 1821
		private readonly bool isSelect;
	}
}
