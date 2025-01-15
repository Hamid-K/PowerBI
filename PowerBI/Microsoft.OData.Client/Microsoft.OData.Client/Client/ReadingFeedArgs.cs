using System;

namespace Microsoft.OData.Client
{
	// Token: 0x02000043 RID: 67
	public sealed class ReadingFeedArgs
	{
		// Token: 0x06000206 RID: 518 RVA: 0x00008C64 File Offset: 0x00006E64
		public ReadingFeedArgs(ODataResourceSet feed)
		{
			Util.CheckArgumentNull<ODataResourceSet>(feed, "feed");
			this.Feed = feed;
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00008C7F File Offset: 0x00006E7F
		// (set) Token: 0x06000208 RID: 520 RVA: 0x00008C87 File Offset: 0x00006E87
		public ODataResourceSet Feed { get; private set; }
	}
}
