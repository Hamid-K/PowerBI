using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x0200009F RID: 159
	internal readonly ref struct ValueSqlStatisticsScope
	{
		// Token: 0x06000CAB RID: 3243 RVA: 0x000267D2 File Offset: 0x000249D2
		public ValueSqlStatisticsScope(SqlStatistics statistics)
		{
			this._statistics = SqlStatistics.StartTimer(statistics);
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x000267E0 File Offset: 0x000249E0
		public void Dispose()
		{
			SqlStatistics.StopTimer(this._statistics);
		}

		// Token: 0x04000358 RID: 856
		private readonly SqlStatistics _statistics;
	}
}
