using System;

namespace Microsoft.AnalysisServices.MInterop
{
	// Token: 0x0200002B RID: 43
	public static class Providers
	{
		// Token: 0x060000CC RID: 204 RVA: 0x00004A6C File Offset: 0x00002C6C
		public static bool IsManagedProvider(string providerName)
		{
			return providerName.Equals("System.Data.SqlClient", StringComparison.OrdinalIgnoreCase) || providerName.Equals("Microsoft.Data.SqlClient", StringComparison.OrdinalIgnoreCase) || providerName.Equals("Microsoft.PowerBI.Dataflows", StringComparison.OrdinalIgnoreCase) || providerName.Equals("Teradata.Client.Provider", StringComparison.OrdinalIgnoreCase) || providerName.Equals("Oracle.DataAccess.Client", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x04000106 RID: 262
		public const string DefaultSqlClient = "System.Data.SqlClient";

		// Token: 0x04000107 RID: 263
		public const string CurrentSqlClient = "Microsoft.Data.SqlClient";

		// Token: 0x04000108 RID: 264
		public const string OracleOLEDB = "OraOLEDB.Oracle";

		// Token: 0x04000109 RID: 265
		public const string TeradataClient = "Teradata.Client.Provider";

		// Token: 0x0400010A RID: 266
		public const string SqlOLEDB = "SQLOLEDB";

		// Token: 0x0400010B RID: 267
		public const string OracleClient = "Oracle.DataAccess.Client";

		// Token: 0x0400010C RID: 268
		public const string OLEDBForODBC = "MSDASQL.1";

		// Token: 0x0400010D RID: 269
		public const string DataflowsClient = "Microsoft.PowerBI.Dataflows";

		// Token: 0x0400010E RID: 270
		public const string MSOLAP = "MSOLAP";
	}
}
