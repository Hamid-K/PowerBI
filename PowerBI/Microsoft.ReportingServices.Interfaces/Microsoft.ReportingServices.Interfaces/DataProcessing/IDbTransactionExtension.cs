using System;

namespace Microsoft.ReportingServices.DataProcessing
{
	// Token: 0x0200001A RID: 26
	public interface IDbTransactionExtension : IDbTransaction, IDisposable
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600003D RID: 61
		bool AllowMultiConnection { get; }
	}
}
