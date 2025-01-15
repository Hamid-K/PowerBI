using System;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.DataProcessing
{
	// Token: 0x02000025 RID: 37
	public interface IDbPoolableConnection : IDbConnection, IDisposable, IExtension
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600004C RID: 76
		bool IsAlive { get; }

		// Token: 0x0600004D RID: 77
		string GetConnectionStringForPooling();

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600004E RID: 78
		// (set) Token: 0x0600004F RID: 79
		bool IsFromPool { get; set; }
	}
}
