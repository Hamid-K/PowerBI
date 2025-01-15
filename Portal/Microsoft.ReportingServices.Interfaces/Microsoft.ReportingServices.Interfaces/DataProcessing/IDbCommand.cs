using System;

namespace Microsoft.ReportingServices.DataProcessing
{
	// Token: 0x0200000F RID: 15
	public interface IDbCommand : IDisposable
	{
		// Token: 0x0600001B RID: 27
		IDataReader ExecuteReader(CommandBehavior behavior);

		// Token: 0x0600001C RID: 28
		IDataParameter CreateParameter();

		// Token: 0x0600001D RID: 29
		void Cancel();

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001E RID: 30
		// (set) Token: 0x0600001F RID: 31
		string CommandText { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000020 RID: 32
		// (set) Token: 0x06000021 RID: 33
		int CommandTimeout { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000022 RID: 34
		// (set) Token: 0x06000023 RID: 35
		CommandType CommandType { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000024 RID: 36
		IDataParameterCollection Parameters { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000025 RID: 37
		// (set) Token: 0x06000026 RID: 38
		IDbTransaction Transaction { get; set; }
	}
}
