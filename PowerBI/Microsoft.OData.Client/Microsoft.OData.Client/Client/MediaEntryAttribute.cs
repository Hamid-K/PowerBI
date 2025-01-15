using System;

namespace Microsoft.OData.Client
{
	// Token: 0x020000D7 RID: 215
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class MediaEntryAttribute : Attribute
	{
		// Token: 0x06000707 RID: 1799 RVA: 0x0001D228 File Offset: 0x0001B428
		public MediaEntryAttribute(string mediaMemberName)
		{
			Util.CheckArgumentNull<string>(mediaMemberName, "mediaMemberName");
			this.mediaMemberName = mediaMemberName;
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x0001D243 File Offset: 0x0001B443
		public string MediaMemberName
		{
			get
			{
				return this.mediaMemberName;
			}
		}

		// Token: 0x0400032B RID: 811
		private readonly string mediaMemberName;
	}
}
