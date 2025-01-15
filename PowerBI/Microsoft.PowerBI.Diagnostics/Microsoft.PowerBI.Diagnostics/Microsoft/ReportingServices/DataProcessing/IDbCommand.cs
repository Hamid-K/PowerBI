using System;

namespace Microsoft.ReportingServices.DataProcessing
{
	// Token: 0x02000023 RID: 35
	public interface IDbCommand : IDisposable
	{
		// Token: 0x06000094 RID: 148
		IDataReader ExecuteReader(CommandBehavior behavior);

		// Token: 0x06000095 RID: 149
		IDataParameter CreateParameter();

		// Token: 0x06000096 RID: 150
		void Cancel();

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000097 RID: 151
		// (set) Token: 0x06000098 RID: 152
		string CommandText { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000099 RID: 153
		// (set) Token: 0x0600009A RID: 154
		int CommandTimeout { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600009B RID: 155
		// (set) Token: 0x0600009C RID: 156
		CommandType CommandType { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600009D RID: 157
		IDataParameterCollection Parameters { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600009E RID: 158
		// (set) Token: 0x0600009F RID: 159
		IDbTransaction Transaction { get; set; }
	}
}
