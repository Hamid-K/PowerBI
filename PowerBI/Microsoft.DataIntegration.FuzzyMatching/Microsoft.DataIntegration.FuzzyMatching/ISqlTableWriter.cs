using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Data.Sql;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200007D RID: 125
	internal interface ISqlTableWriter : IDisposable
	{
		// Token: 0x06000516 RID: 1302
		void BeginUpdate(SqlConnection connection, SqlName tableName, DataTable schema);

		// Token: 0x06000517 RID: 1303
		void AddRecord(IDataRecord record);

		// Token: 0x06000518 RID: 1304
		void EndUpdate();
	}
}
