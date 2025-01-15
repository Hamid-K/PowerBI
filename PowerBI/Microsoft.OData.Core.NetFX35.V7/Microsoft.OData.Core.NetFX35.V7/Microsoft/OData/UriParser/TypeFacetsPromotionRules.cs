using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001BB RID: 443
	public class TypeFacetsPromotionRules
	{
		// Token: 0x06001189 RID: 4489 RVA: 0x00030B86 File Offset: 0x0002ED86
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

		// Token: 0x0600118A RID: 4490 RVA: 0x00030B86 File Offset: 0x0002ED86
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
