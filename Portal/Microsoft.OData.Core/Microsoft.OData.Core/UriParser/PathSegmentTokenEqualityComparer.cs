using System;
using System.Collections.Generic;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001E2 RID: 482
	internal sealed class PathSegmentTokenEqualityComparer : EqualityComparer<PathSegmentToken>
	{
		// Token: 0x060015C2 RID: 5570 RVA: 0x0003E24F File Offset: 0x0003C44F
		public override bool Equals(PathSegmentToken first, PathSegmentToken second)
		{
			return (first == null && second == null) || (first != null && second != null && this.ToHashableString(first) == this.ToHashableString(second));
		}

		// Token: 0x060015C3 RID: 5571 RVA: 0x0003E274 File Offset: 0x0003C474
		public override int GetHashCode(PathSegmentToken path)
		{
			if (path == null)
			{
				return 0;
			}
			return this.ToHashableString(path).GetHashCode();
		}

		// Token: 0x060015C4 RID: 5572 RVA: 0x0003E287 File Offset: 0x0003C487
		private string ToHashableString(PathSegmentToken token)
		{
			if (token.NextToken == null)
			{
				return token.Identifier;
			}
			return token.Identifier + "/" + this.ToHashableString(token.NextToken);
		}
	}
}
