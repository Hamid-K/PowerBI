using System;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.Packaging
{
	// Token: 0x0200002B RID: 43
	public class ArtifactIdentifier
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000127 RID: 295 RVA: 0x0000551D File Offset: 0x0000371D
		// (set) Token: 0x06000128 RID: 296 RVA: 0x00005525 File Offset: 0x00003725
		[JsonProperty("objectName", Required = Required.Always)]
		public string ObjectName { get; set; }
	}
}
