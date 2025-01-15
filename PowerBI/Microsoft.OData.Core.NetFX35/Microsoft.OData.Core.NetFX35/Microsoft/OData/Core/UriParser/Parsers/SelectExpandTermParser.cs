using System;
using Microsoft.OData.Core.UriParser.Syntactic;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x02000214 RID: 532
	internal sealed class SelectExpandTermParser
	{
		// Token: 0x06001362 RID: 4962 RVA: 0x00046C9B File Offset: 0x00044E9B
		internal SelectExpandTermParser(ExpressionLexer lexer, int maxPathLength, bool isSelect)
		{
			this.lexer = lexer;
			this.maxPathLength = maxPathLength;
			this.isSelect = isSelect;
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x00046CB8 File Offset: 0x00044EB8
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

		// Token: 0x06001364 RID: 4964 RVA: 0x00046D2F File Offset: 0x00044F2F
		private void CheckPathLength(int pathLength)
		{
			if (pathLength > this.maxPathLength)
			{
				throw new ODataException(Strings.UriQueryExpressionParser_TooDeep);
			}
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x00046D48 File Offset: 0x00044F48
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

		// Token: 0x04000844 RID: 2116
		private readonly ExpressionLexer lexer;

		// Token: 0x04000845 RID: 2117
		private readonly int maxPathLength;

		// Token: 0x04000846 RID: 2118
		private readonly bool isSelect;
	}
}
