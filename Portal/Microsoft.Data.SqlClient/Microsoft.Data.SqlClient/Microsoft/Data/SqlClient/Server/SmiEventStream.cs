using System;
using Microsoft.Data.Common;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x0200014B RID: 331
	internal abstract class SmiEventStream : IDisposable
	{
		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x060019B2 RID: 6578
		internal abstract bool HasEvents { get; }

		// Token: 0x060019B3 RID: 6579
		internal abstract void Close(SmiEventSink sink);

		// Token: 0x060019B4 RID: 6580 RVA: 0x000605F2 File Offset: 0x0005E7F2
		public virtual void Dispose()
		{
			ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x060019B5 RID: 6581
		internal abstract void ProcessEvent(SmiEventSink sink);
	}
}
