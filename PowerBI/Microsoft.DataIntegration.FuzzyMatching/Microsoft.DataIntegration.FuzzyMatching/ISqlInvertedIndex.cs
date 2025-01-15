using System;
using System.Data;
using System.Data.SqlClient;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000086 RID: 134
	internal interface ISqlInvertedIndex : ISqlInvertedIndexBuilder, IInvertedIndexUpdate, ISqlInvertedIndexReader
	{
		// Token: 0x0600054D RID: 1357
		void CreateTables(DataTable referenceSchema, SqlConnection connection, bool overwriteExistingTables, string sqlSchemaName, string tableNamePrefix);

		// Token: 0x0600054E RID: 1358
		void DropIndex(SqlConnection connection);

		// Token: 0x0600054F RID: 1359
		IDataReader CreateSignaturesReader(SqlConnection connection);
	}
}
