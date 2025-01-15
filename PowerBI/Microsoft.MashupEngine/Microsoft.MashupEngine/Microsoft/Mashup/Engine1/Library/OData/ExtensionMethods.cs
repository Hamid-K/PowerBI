using System;

namespace Microsoft.Mashup.Engine1.Library.OData
{
	// Token: 0x02000748 RID: 1864
	public static class ExtensionMethods
	{
		// Token: 0x0600372F RID: 14127 RVA: 0x000B00EC File Offset: 0x000AE2EC
		public static bool IsCollection(this IODataNavigationLinkWrapper link)
		{
			return link.IsCollection == null || link.IsCollection.Value;
		}
	}
}
