using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200015B RID: 347
	internal static class PathReverser
	{
		// Token: 0x060011BC RID: 4540 RVA: 0x00033DC0 File Offset: 0x00031FC0
		public static PathSegmentToken Reverse(this PathSegmentToken head)
		{
			PathSegmentToken pathSegmentToken = null;
			PathSegmentToken nextToken;
			for (PathSegmentToken pathSegmentToken2 = head; pathSegmentToken2 != null; pathSegmentToken2 = nextToken)
			{
				nextToken = pathSegmentToken2.NextToken;
				pathSegmentToken2.NextToken = pathSegmentToken;
				pathSegmentToken = pathSegmentToken2;
			}
			return pathSegmentToken;
		}
	}
}
