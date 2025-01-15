using System;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.DataProcessing
{
	// Token: 0x02000019 RID: 25
	public interface IDbConnection : IDisposable, IExtension
	{
		// Token: 0x0600007E RID: 126
		void Open();

		// Token: 0x0600007F RID: 127
		void Close();

		// Token: 0x06000080 RID: 128
		IDbCommand CreateCommand();

		// Token: 0x06000081 RID: 129
		IDbTransaction BeginTransaction();

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000082 RID: 130
		// (set) Token: 0x06000083 RID: 131
		string ConnectionString { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000084 RID: 132
		int ConnectionTimeout { get; }
	}
}
