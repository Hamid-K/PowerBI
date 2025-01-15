using System;
using Microsoft.Identity.Json;

namespace Microsoft.Identity.Client.Instance.Validation
{
	// Token: 0x02000275 RID: 629
	[JsonObject(Title = "links")]
	[Preserve(AllMembers = true)]
	internal class LinksList
	{
		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x060018A0 RID: 6304 RVA: 0x0005182F File Offset: 0x0004FA2F
		// (set) Token: 0x060018A1 RID: 6305 RVA: 0x00051837 File Offset: 0x0004FA37
		[JsonProperty("rel")]
		public string Rel { get; set; }

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x060018A2 RID: 6306 RVA: 0x00051840 File Offset: 0x0004FA40
		// (set) Token: 0x060018A3 RID: 6307 RVA: 0x00051848 File Offset: 0x0004FA48
		[JsonProperty("href")]
		public string Href { get; set; }
	}
}
