using System;
using Microsoft.PowerBI.Packaging.Project;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.Packaging
{
	// Token: 0x02000011 RID: 17
	public class MobileStatePage
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002886 File Offset: 0x00000A86
		// (set) Token: 0x06000055 RID: 85 RVA: 0x0000288E File Offset: 0x00000A8E
		[JsonProperty("filePath", Required = Required.Always)]
		public string FilePath { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002897 File Offset: 0x00000A97
		// (set) Token: 0x06000057 RID: 87 RVA: 0x0000289F File Offset: 0x00000A9F
		[JsonProperty("visualContainers", Required = Required.Default)]
		public NonNulls<ExplorationArtifact> VisualContainers { get; set; }
	}
}
