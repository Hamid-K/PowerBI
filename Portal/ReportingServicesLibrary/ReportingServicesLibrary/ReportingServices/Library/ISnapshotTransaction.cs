using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000289 RID: 649
	internal interface ISnapshotTransaction : IDisposable
	{
		// Token: 0x060017A9 RID: 6057
		void Commit();

		// Token: 0x060017AA RID: 6058
		void Rollback();

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x060017AB RID: 6059
		// (set) Token: 0x060017AC RID: 6060
		bool IsRootTransaction { get; set; }
	}
}
