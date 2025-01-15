using System;
using System.Data;
using System.Data.Common;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x020010D3 RID: 4307
	public interface IDbService
	{
		// Token: 0x060070C9 RID: 28873
		DbConnection CreateDbConnection();

		// Token: 0x060070CA RID: 28874
		bool TryGetBulkCopy(string schema, string table, out IBulkCopy bulkCopy);

		// Token: 0x060070CB RID: 28875
		DataTable LoadDatabaseInformation(DbConnection connection);

		// Token: 0x060070CC RID: 28876
		DataTable LoadSchemas(DbConnection connection);

		// Token: 0x060070CD RID: 28877
		DataTable LoadTables(DbConnection connection, string schemaFilter, string tableFilter);

		// Token: 0x060070CE RID: 28878
		DataTable LoadProcedures(DbConnection connection, string schemaFilter, string procedureFilter);

		// Token: 0x060070CF RID: 28879
		DataTable LoadColumns(DbConnection connection, string schema, string table);

		// Token: 0x060070D0 RID: 28880
		DataTable LoadProcedureColumns(DbConnection connection, string schema, string procedure);

		// Token: 0x060070D1 RID: 28881
		DataTable LoadIndexes(DbConnection connection, string schema, string table);

		// Token: 0x060070D2 RID: 28882
		DataTable LoadForeignKeysParent(DbConnection connection, string schema, string table);

		// Token: 0x060070D3 RID: 28883
		DataTable LoadForeignKeysChild(DbConnection connection, string schema, string table);

		// Token: 0x060070D4 RID: 28884
		DataTable LoadProcedureParameters(DbConnection connection, string schema, string procedure);

		// Token: 0x060070D5 RID: 28885
		DataTable LoadIdentityColumns(DbConnection connection, string schema, string table);

		// Token: 0x060070D6 RID: 28886
		DataTable LoadResourceInformation(DbConnection connection, string schema, string table);
	}
}
