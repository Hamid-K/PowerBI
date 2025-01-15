using System;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.DataProcessing;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000610 RID: 1552
	public sealed class DefaultOpenConnectionExtension : IOpenConnectionExtension
	{
		// Token: 0x0600555A RID: 21850 RVA: 0x00168489 File Offset: 0x00166689
		public void OpenConnection(IDbConnection conn, string connectionString, DataSourceInfo dsInfo)
		{
			conn.ConnectionString = connectionString;
			conn.Open();
		}
	}
}
