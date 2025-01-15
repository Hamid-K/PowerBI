using System;
using Microsoft.OData.Core.UriParser.Syntactic;
using Microsoft.OData.Core.UriParser.Visitors;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x0200020E RID: 526
	internal sealed class PathReverser : PathSegmentTokenVisitor<PathSegmentToken>
	{
		// Token: 0x06001328 RID: 4904 RVA: 0x00045D1A File Offset: 0x00043F1A
		public PathReverser()
		{
			this.childToken = null;
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x00045D29 File Offset: 0x00043F29
		private PathReverser(PathSegmentToken childToken)
		{
			this.childToken = childToken;
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x00045D38 File Offset: 0x00043F38
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

		// Token: 0x0600132B RID: 4907 RVA: 0x00045D94 File Offset: 0x00043F94
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

		// Token: 0x0600132C RID: 4908 RVA: 0x00045DE4 File Offset: 0x00043FE4
		private static PathSegmentToken BuildNextStep(PathSegmentToken nextLevelToken, PathSegmentToken nextChildToken)
		{
			PathReverser pathReverser = new PathReverser(nextChildToken);
			return nextLevelToken.Accept<PathSegmentToken>(pathReverser);
		}

		// Token: 0x0400082E RID: 2094
		private readonly PathSegmentToken childToken;
	}
}
