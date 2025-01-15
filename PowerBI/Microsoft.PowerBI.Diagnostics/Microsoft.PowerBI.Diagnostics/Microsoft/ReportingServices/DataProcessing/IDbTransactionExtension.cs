using System;

namespace Microsoft.ReportingServices.DataProcessing
{
	// Token: 0x0200002C RID: 44
	public interface IDbTransactionExtension : IDbTransaction, IDisposable
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000B3 RID: 179
		bool AllowMultiConnection { get; }
	}
}
