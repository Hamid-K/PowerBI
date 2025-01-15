using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200004F RID: 79
	internal interface IDataShapeAbortHelper : IAbortHelper, IDisposable
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000246 RID: 582
		// (remove) Token: 0x06000247 RID: 583
		event EventHandler ProcessingAbortEvent;

		// Token: 0x06000248 RID: 584
		void ThrowIfAborted(CancelationTrigger cancelationTrigger);
	}
}
