using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x02000061 RID: 97
	internal static class SelectTreeNormalizer
	{
		// Token: 0x0600026D RID: 621 RVA: 0x00009F44 File Offset: 0x00008144
		public static SelectToken NormalizeSelectTree(SelectToken treeToNormalize)
		{
			PathReverser pathReverser = new PathReverser();
			List<PathSegmentToken> list = Enumerable.ToList<PathSegmentToken>(Enumerable.Select<PathSegmentToken, PathSegmentToken>(treeToNormalize.Properties, (PathSegmentToken property) => property.Accept<PathSegmentToken>(pathReverser)));
			return new SelectToken(list);
		}
	}
}
