using System;
using System.Collections.Generic;
using Microsoft.Identity.Client.OAuth2;
using Microsoft.Identity.Json;

namespace Microsoft.Identity.Client.Instance.Validation
{
	// Token: 0x02000276 RID: 630
	[JsonObject]
	[Preserve(AllMembers = true)]
	internal class AdfsWebFingerResponse : OAuth2ResponseBase
	{
		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x060018A5 RID: 6309 RVA: 0x00051859 File Offset: 0x0004FA59
		// (set) Token: 0x060018A6 RID: 6310 RVA: 0x00051861 File Offset: 0x0004FA61
		[JsonProperty("subject")]
		public string Subject { get; set; }

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x060018A7 RID: 6311 RVA: 0x0005186A File Offset: 0x0004FA6A
		// (set) Token: 0x060018A8 RID: 6312 RVA: 0x00051872 File Offset: 0x0004FA72
		[JsonProperty("links")]
		public List<LinksList> Links { get; set; }
	}
}
