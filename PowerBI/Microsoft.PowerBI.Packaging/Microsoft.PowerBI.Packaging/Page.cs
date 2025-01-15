using System;
using Microsoft.PowerBI.Packaging.Project;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.Packaging
{
	// Token: 0x0200000D RID: 13
	public class Page : ExplorationArtifact
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002811 File Offset: 0x00000A11
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002819 File Offset: 0x00000A19
		[JsonProperty("visualContainers", Required = Required.Default)]
		public NonNulls<ExplorationArtifact> VisualContainers { get; set; }
	}
}
