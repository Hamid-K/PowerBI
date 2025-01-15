using System;
using System.Collections.Generic;
using Microsoft.OData.Core.UriParser.Syntactic;

namespace Microsoft.OData.Core.UriParser.Visitors
{
	// Token: 0x02000296 RID: 662
	internal sealed class PathSegmentTokenEqualityComparer : EqualityComparer<PathSegmentToken>
	{
		// Token: 0x060016BF RID: 5823 RVA: 0x0004E7E3 File Offset: 0x0004C9E3
		public override bool Equals(PathSegmentToken first, PathSegmentToken second)
		{
			return (first == null && second == null) || (first != null && second != null && this.ToHashableString(first) == this.ToHashableString(second));
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x0004E808 File Offset: 0x0004CA08
		public override int GetHashCode(PathSegmentToken path)
		{
			if (path == null)
			{
				return 0;
			}
			return this.ToHashableString(path).GetHashCode();
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x0004E81B File Offset: 0x0004CA1B
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
