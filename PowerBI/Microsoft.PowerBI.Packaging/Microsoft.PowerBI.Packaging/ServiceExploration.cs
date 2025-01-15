using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.PowerBI.Packaging
{
	// Token: 0x02000029 RID: 41
	public class ServiceExploration
	{
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600011B RID: 283 RVA: 0x000054B8 File Offset: 0x000036B8
		// (set) Token: 0x0600011C RID: 284 RVA: 0x000054C0 File Offset: 0x000036C0
		[JsonProperty("sections", Required = Required.Always)]
		public IReadOnlyList<SectionReferenceIdentifier> Sections { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600011D RID: 285 RVA: 0x000054C9 File Offset: 0x000036C9
		// (set) Token: 0x0600011E RID: 286 RVA: 0x000054D1 File Offset: 0x000036D1
		[JsonProperty("pods", Required = Required.Default)]
		public IReadOnlyList<Pod> Pods { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600011F RID: 287 RVA: 0x000054DA File Offset: 0x000036DA
		// (set) Token: 0x06000120 RID: 288 RVA: 0x000054E2 File Offset: 0x000036E2
		[JsonProperty("resourcePackages", Required = Required.Always)]
		public JToken ResourcePackages { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000121 RID: 289 RVA: 0x000054EB File Offset: 0x000036EB
		// (set) Token: 0x06000122 RID: 290 RVA: 0x000054F3 File Offset: 0x000036F3
		[JsonProperty("publicCustomVisuals", Required = Required.Default)]
		public JToken PublicCustomVisuals { get; set; }
	}
}
