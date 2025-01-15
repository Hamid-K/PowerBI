using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.Packaging
{
	// Token: 0x0200002A RID: 42
	public class SectionReferenceIdentifier : ArtifactIdentifier
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00005504 File Offset: 0x00003704
		// (set) Token: 0x06000125 RID: 293 RVA: 0x0000550C File Offset: 0x0000370C
		[JsonProperty("visualContainers", Required = Required.Default)]
		public IReadOnlyList<ArtifactIdentifier> VisualContainers { get; set; }
	}
}
