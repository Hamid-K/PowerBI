using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000115 RID: 277
	public class TypeFacetsPromotionRules
	{
		// Token: 0x06000F7C RID: 3964 RVA: 0x00026613 File Offset: 0x00024813
		public virtual int? GetPromotedPrecision(int? left, int? right)
		{
			if (left == null)
			{
				return right;
			}
			if (right != null)
			{
				return new int?(Math.Max(left.Value, right.Value));
			}
			return left;
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x00026613 File Offset: 0x00024813
		public virtual int? GetPromotedScale(int? left, int? right)
		{
			if (left == null)
			{
				return right;
			}
			if (right != null)
			{
				return new int?(Math.Max(left.Value, right.Value));
			}
			return left;
		}
	}
}
