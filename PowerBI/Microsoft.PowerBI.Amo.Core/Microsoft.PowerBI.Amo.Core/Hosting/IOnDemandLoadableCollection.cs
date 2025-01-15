using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.Hosting
{
	// Token: 0x0200015E RID: 350
	[Guid("581524E3-A62D-4ce7-89D1-A8E2BA8D485C")]
	public interface IOnDemandLoadableCollection
	{
		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x060011BB RID: 4539
		// (set) Token: 0x060011BC RID: 4540
		bool Loaded { get; set; }

		// Token: 0x060011BD RID: 4541
		int BlockOnDemandLoad(bool block);
	}
}
