using System;
using System.IO;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x0200088D RID: 2189
	internal interface ISpaceManager
	{
		// Token: 0x0600781A RID: 30746
		void Free(long offset, long size);

		// Token: 0x0600781B RID: 30747
		long AllocateSpace(long size);

		// Token: 0x0600781C RID: 30748
		long Resize(long offset, long oldSize, long newSize);

		// Token: 0x0600781D RID: 30749
		void Seek(long offset, SeekOrigin origin);

		// Token: 0x170027F3 RID: 10227
		// (get) Token: 0x0600781E RID: 30750
		// (set) Token: 0x0600781F RID: 30751
		long StreamEnd { get; set; }

		// Token: 0x06007820 RID: 30752
		void TraceStats();
	}
}
