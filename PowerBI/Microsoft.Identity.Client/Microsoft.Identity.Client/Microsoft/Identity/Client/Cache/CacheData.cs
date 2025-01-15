using System;
using System.ComponentModel;

namespace Microsoft.Identity.Client.Cache
{
	// Token: 0x020002A4 RID: 676
	[Obsolete("This is expected to be removed in MSAL.NET v3 and ADAL.NET v5. We recommend using SerializeMsalV3/DeserializeMsalV3. Read more: https://aka.ms/msal-net-3x-cache-breaking-change", false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class CacheData
	{
		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001980 RID: 6528 RVA: 0x0005361D File Offset: 0x0005181D
		// (set) Token: 0x06001981 RID: 6529 RVA: 0x00053625 File Offset: 0x00051825
		public byte[] AdalV3State { get; set; }

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001982 RID: 6530 RVA: 0x0005362E File Offset: 0x0005182E
		// (set) Token: 0x06001983 RID: 6531 RVA: 0x00053636 File Offset: 0x00051836
		public byte[] UnifiedState { get; set; }
	}
}
