using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Core.UriParser.Syntactic;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x020001DD RID: 477
	internal sealed class SelectTreeNormalizer
	{
		// Token: 0x0600117B RID: 4475 RVA: 0x0003E404 File Offset: 0x0003C604
		public SelectToken NormalizeSelectTree(SelectToken treeToNormalize)
		{
			PathReverser pathReverser = new PathReverser();
			List<PathSegmentToken> list = Enumerable.ToList<PathSegmentToken>(Enumerable.Select<PathSegmentToken, PathSegmentToken>(treeToNormalize.Properties, (PathSegmentToken property) => property.Accept<PathSegmentToken>(pathReverser)));
			return new SelectToken(list);
		}
	}
}
