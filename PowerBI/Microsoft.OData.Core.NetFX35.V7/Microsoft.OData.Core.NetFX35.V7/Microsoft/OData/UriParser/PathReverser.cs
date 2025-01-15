using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000118 RID: 280
	internal sealed class PathReverser : PathSegmentTokenVisitor<PathSegmentToken>
	{
		// Token: 0x06000D07 RID: 3335 RVA: 0x00025AD7 File Offset: 0x00023CD7
		public PathReverser()
		{
			this.childToken = null;
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x00025AE6 File Offset: 0x00023CE6
		private PathReverser(PathSegmentToken childToken)
		{
			this.childToken = childToken;
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x00025AF8 File Offset: 0x00023CF8
		public override PathSegmentToken Visit(NonSystemToken tokenIn)
		{
			ExceptionUtils.CheckArgumentNotNull<NonSystemToken>(tokenIn, "tokenIn");
			if (tokenIn.NextToken != null)
			{
				NonSystemToken nonSystemToken = new NonSystemToken(tokenIn.Identifier, tokenIn.NamedValues, this.childToken);
				return PathReverser.BuildNextStep(tokenIn.NextToken, nonSystemToken);
			}
			return new NonSystemToken(tokenIn.Identifier, tokenIn.NamedValues, this.childToken);
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x00025B58 File Offset: 0x00023D58
		public override PathSegmentToken Visit(SystemToken tokenIn)
		{
			ExceptionUtils.CheckArgumentNotNull<SystemToken>(tokenIn, "tokenIn");
			if (tokenIn.NextToken != null)
			{
				SystemToken systemToken = new SystemToken(tokenIn.Identifier, this.childToken);
				return PathReverser.BuildNextStep(tokenIn.NextToken, systemToken);
			}
			return new SystemToken(tokenIn.Identifier, this.childToken);
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x00025BAC File Offset: 0x00023DAC
		private static PathSegmentToken BuildNextStep(PathSegmentToken nextLevelToken, PathSegmentToken nextChildToken)
		{
			PathReverser pathReverser = new PathReverser(nextChildToken);
			return nextLevelToken.Accept<PathSegmentToken>(pathReverser);
		}

		// Token: 0x04000706 RID: 1798
		private readonly PathSegmentToken childToken;
	}
}
