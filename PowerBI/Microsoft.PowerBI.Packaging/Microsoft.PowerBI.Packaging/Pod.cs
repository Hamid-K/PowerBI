using System;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.Packaging
{
	// Token: 0x0200002C RID: 44
	public class Pod
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00005536 File Offset: 0x00003736
		// (set) Token: 0x0600012B RID: 299 RVA: 0x0000553E File Offset: 0x0000373E
		[JsonProperty("name", Required = Required.Always)]
		public string Name { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00005547 File Offset: 0x00003747
		// (set) Token: 0x0600012D RID: 301 RVA: 0x0000554F File Offset: 0x0000374F
		[JsonProperty("boundSection", Required = Required.Always)]
		public string BoundSection { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00005558 File Offset: 0x00003758
		// (set) Token: 0x0600012F RID: 303 RVA: 0x00005560 File Offset: 0x00003760
		[JsonProperty("parameters", Required = Required.Default)]
		public string Parameters { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00005569 File Offset: 0x00003769
		// (set) Token: 0x06000131 RID: 305 RVA: 0x00005571 File Offset: 0x00003771
		[JsonProperty("referenceScope", Required = Required.Default)]
		public int ReferenceScope { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000132 RID: 306 RVA: 0x0000557A File Offset: 0x0000377A
		// (set) Token: 0x06000133 RID: 307 RVA: 0x00005582 File Offset: 0x00003782
		[JsonProperty("type", Required = Required.Default)]
		public int Type { get; set; }
	}
}
