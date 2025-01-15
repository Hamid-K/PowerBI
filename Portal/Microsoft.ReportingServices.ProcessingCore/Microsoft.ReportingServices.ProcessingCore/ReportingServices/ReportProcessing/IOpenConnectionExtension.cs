using System;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.DataProcessing;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200060F RID: 1551
	public interface IOpenConnectionExtension
	{
		// Token: 0x06005559 RID: 21849
		void OpenConnection(IDbConnection conn, string connectionString, DataSourceInfo dsInfo);
	}
}
