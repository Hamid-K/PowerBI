using System;

namespace Microsoft.AnalysisServices.Tabular.Metadata
{
	// Token: 0x020001E3 RID: 483
	[Flags]
	internal enum CopyFlags
	{
		// Token: 0x0400066D RID: 1645
		None = 0,
		// Token: 0x0400066E RID: 1646
		IncludeObjectIds = 1,
		// Token: 0x0400066F RID: 1647
		CloningBody = 2,
		// Token: 0x04000670 RID: 1648
		DontTrackObjectChanges = 4,
		// Token: 0x04000671 RID: 1649
		Incremental = 8,
		// Token: 0x04000672 RID: 1650
		ShallowCopy = 16,
		// Token: 0x04000673 RID: 1651
		IgnoreIdsForChildLinks = 32,
		// Token: 0x04000674 RID: 1652
		DontResolveCrossLinks = 64,
		// Token: 0x04000675 RID: 1653
		IgnoreInferredProperties = 128,
		// Token: 0x04000676 RID: 1654
		IgnoreInferredObjects = 256,
		// Token: 0x04000677 RID: 1655
		IncludeCompatRestictions = 512,
		// Token: 0x04000678 RID: 1656
		IncludeOperationalFlags = 1024,
		// Token: 0x04000679 RID: 1657
		MetadataSync = 2048,
		// Token: 0x0400067A RID: 1658
		UserClone = 416,
		// Token: 0x0400067B RID: 1659
		UserCopy = 928
	}
}
