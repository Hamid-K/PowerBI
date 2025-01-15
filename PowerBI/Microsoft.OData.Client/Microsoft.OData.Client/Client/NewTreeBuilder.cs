using System;
using Microsoft.OData.Client.ALinq.UriParser;

namespace Microsoft.OData.Client
{
	// Token: 0x0200001C RID: 28
	internal class NewTreeBuilder : IPathSegmentTokenVisitor<PathSegmentToken>
	{
		// Token: 0x060000C8 RID: 200 RVA: 0x00004B12 File Offset: 0x00002D12
		public PathSegmentToken Visit(SystemToken tokenIn)
		{
			throw new NotSupportedException(Strings.ALinq_IllegalSystemQueryOption(tokenIn.Identifier));
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00004D3C File Offset: 0x00002F3C
		public PathSegmentToken Visit(NonSystemToken tokenIn)
		{
			if (tokenIn == null)
			{
				return null;
			}
			PathSegmentToken pathSegmentToken = ((tokenIn.NextToken != null) ? tokenIn.NextToken.Accept<PathSegmentToken>(this) : null);
			return new NonSystemToken(tokenIn.Identifier, tokenIn.NamedValues, pathSegmentToken);
		}
	}
}
