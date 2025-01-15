using System;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x02000055 RID: 85
	internal sealed class PathReverser : PathSegmentTokenVisitor<PathSegmentToken>
	{
		// Token: 0x0600022F RID: 559 RVA: 0x000084C8 File Offset: 0x000066C8
		public PathReverser()
		{
			this.childToken = null;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x000084D7 File Offset: 0x000066D7
		private PathReverser(PathSegmentToken childToken)
		{
			this.childToken = childToken;
		}

		// Token: 0x06000231 RID: 561 RVA: 0x000084E8 File Offset: 0x000066E8
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

		// Token: 0x06000232 RID: 562 RVA: 0x00008544 File Offset: 0x00006744
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

		// Token: 0x06000233 RID: 563 RVA: 0x00008594 File Offset: 0x00006794
		private static PathSegmentToken BuildNextStep(PathSegmentToken nextLevelToken, PathSegmentToken nextChildToken)
		{
			PathReverser pathReverser = new PathReverser(nextChildToken);
			return nextLevelToken.Accept<PathSegmentToken>(pathReverser);
		}

		// Token: 0x04000088 RID: 136
		private readonly PathSegmentToken childToken;
	}
}
