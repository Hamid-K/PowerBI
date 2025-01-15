using System;
using Microsoft.PowerBI.Packaging.Project;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.Packaging
{
	// Token: 0x0200000B RID: 11
	public class Pages
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000027CE File Offset: 0x000009CE
		// (set) Token: 0x0600003F RID: 63 RVA: 0x000027D6 File Offset: 0x000009D6
		[JsonProperty("pagesMetadata", Required = Required.Default)]
		public ExplorationArtifact PagesMetadata { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000027DF File Offset: 0x000009DF
		// (set) Token: 0x06000041 RID: 65 RVA: 0x000027E7 File Offset: 0x000009E7
		[JsonProperty("pages", Required = Required.Always)]
		public NonNulls<Page> PagesList { get; set; }
	}
}
