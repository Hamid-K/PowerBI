using System;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.DataProcessing
{
	// Token: 0x02000003 RID: 3
	public interface IDbConnection : IDisposable, IExtension
	{
		// Token: 0x06000001 RID: 1
		void Open();

		// Token: 0x06000002 RID: 2
		void Close();

		// Token: 0x06000003 RID: 3
		IDbCommand CreateCommand();

		// Token: 0x06000004 RID: 4
		IDbTransaction BeginTransaction();

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5
		// (set) Token: 0x06000006 RID: 6
		string ConnectionString { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000007 RID: 7
		int ConnectionTimeout { get; }
	}
}
