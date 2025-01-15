using System;
using Microsoft.PowerBI.Packaging.Project.Artifacts;

namespace Microsoft.PowerBI.Packaging.Project
{
	// Token: 0x02000056 RID: 86
	public class ArtifactSourceControl
	{
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000295 RID: 661 RVA: 0x00007E01 File Offset: 0x00006001
		// (set) Token: 0x06000296 RID: 662 RVA: 0x00007E09 File Offset: 0x00006009
		public ArtifactConfig ArtifactConfig { get; set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000297 RID: 663 RVA: 0x00007E12 File Offset: 0x00006012
		// (set) Token: 0x06000298 RID: 664 RVA: 0x00007E1A File Offset: 0x0000601A
		public ArtifactMetadata ArtifactMetadata { get; set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000299 RID: 665 RVA: 0x00007E23 File Offset: 0x00006023
		// (set) Token: 0x0600029A RID: 666 RVA: 0x00007E2B File Offset: 0x0000602B
		public ArtifactDetails ArtifactDetails { get; set; }
	}
}
