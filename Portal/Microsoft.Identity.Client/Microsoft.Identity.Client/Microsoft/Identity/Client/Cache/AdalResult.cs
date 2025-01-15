using System;
using Microsoft.Identity.Json;

namespace Microsoft.Identity.Client.Cache
{
	// Token: 0x0200029D RID: 669
	[JsonObject]
	[Preserve(AllMembers = true)]
	internal sealed class AdalResult
	{
		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06001947 RID: 6471 RVA: 0x00053064 File Offset: 0x00051264
		// (set) Token: 0x06001948 RID: 6472 RVA: 0x0005306C File Offset: 0x0005126C
		[JsonProperty]
		public AdalUserInfo UserInfo { get; internal set; }
	}
}
