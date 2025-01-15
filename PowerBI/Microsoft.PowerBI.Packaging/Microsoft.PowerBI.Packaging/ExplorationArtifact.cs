using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.PowerBI.Packaging
{
	// Token: 0x0200000E RID: 14
	public class ExplorationArtifact
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000049 RID: 73 RVA: 0x0000282A File Offset: 0x00000A2A
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00002832 File Offset: 0x00000A32
		[JsonProperty("filePath", Required = Required.Always)]
		public string FilePath { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600004B RID: 75 RVA: 0x0000283B File Offset: 0x00000A3B
		// (set) Token: 0x0600004C RID: 76 RVA: 0x00002843 File Offset: 0x00000A43
		[JsonProperty("content", Required = Required.Always)]
		public JToken Content { get; set; }
	}
}
