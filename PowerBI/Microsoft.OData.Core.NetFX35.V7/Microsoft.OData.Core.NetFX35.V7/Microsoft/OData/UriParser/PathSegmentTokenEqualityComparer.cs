using System;
using System.Collections.Generic;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000192 RID: 402
	internal sealed class PathSegmentTokenEqualityComparer : EqualityComparer<PathSegmentToken>
	{
		// Token: 0x0600104A RID: 4170 RVA: 0x0002DA3E File Offset: 0x0002BC3E
		public override bool Equals(PathSegmentToken first, PathSegmentToken second)
		{
			return (first == null && second == null) || (first != null && second != null && this.ToHashableString(first) == this.ToHashableString(second));
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x0002DA63 File Offset: 0x0002BC63
		public override int GetHashCode(PathSegmentToken path)
		{
			if (path == null)
			{
				return 0;
			}
			return this.ToHashableString(path).GetHashCode();
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x0002DA76 File Offset: 0x0002BC76
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
