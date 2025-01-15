using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020000FA RID: 250
	internal sealed class SelectTreeNormalizer
	{
		// Token: 0x06000BE9 RID: 3049 RVA: 0x0001F5EC File Offset: 0x0001D7EC
		public static SelectToken NormalizeSelectTree(SelectToken treeToNormalize)
		{
			PathReverser pathReverser = new PathReverser();
			List<PathSegmentToken> list = Enumerable.ToList<PathSegmentToken>(Enumerable.Select<PathSegmentToken, PathSegmentToken>(treeToNormalize.Properties, (PathSegmentToken property) => property.Accept<PathSegmentToken>(pathReverser)));
			return new SelectToken(list);
		}
	}
}
