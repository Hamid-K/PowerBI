using System;
using System.Collections.Generic;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x020005B1 RID: 1457
	internal interface IOdbcConnection : IDisposable
	{
		// Token: 0x06002DEA RID: 11754
		void Open();

		// Token: 0x06002DEB RID: 11755
		int GetInfoInt32(Odbc32.SQL_INFO infoType);

		// Token: 0x06002DEC RID: 11756
		string GetInfoString(Odbc32.SQL_INFO infoType);

		// Token: 0x06002DED RID: 11757
		bool GetFunctions(Odbc32.SQL_API functionId);

		// Token: 0x06002DEE RID: 11758
		string GetConnectAttrString(Odbc32.SQL_ATTR attribute);

		// Token: 0x06002DEF RID: 11759
		IDisposable UseCatalog(string catalog);

		// Token: 0x06002DF0 RID: 11760
		IDataReaderWithTableSchema GetTables(string catalogName, string schemaName, string tableName, string tableType);

		// Token: 0x06002DF1 RID: 11761
		IDataReaderWithTableSchema GetColumns(string catalogName, string schemaName, string tableName);

		// Token: 0x06002DF2 RID: 11762
		IDataReaderWithTableSchema GetPrimaryKeys(string catalogName, string schemaName, string tableName);

		// Token: 0x06002DF3 RID: 11763
		IDataReaderWithTableSchema GetForeignKeys(string pkCatalogName, string pkSchemaName, string pkTableName, string fkCatalogName, string fkSchemaName, string fkTableName);

		// Token: 0x06002DF4 RID: 11764
		IDataReaderWithTableSchema GetTypeInfo(short dataType);

		// Token: 0x06002DF5 RID: 11765
		IDataReaderWithTableSchema GetBestRowId(string catalogName, string schemaName, string tableName);

		// Token: 0x06002DF6 RID: 11766
		IPageReader ExecuteDirect(string commandText, IList<OdbcParameter> parameters, RowRange rowRange, IOdbcStatementRegistrar statementRegistrar);

		// Token: 0x06002DF7 RID: 11767
		IPageReader Execute(OdbcStatementHandle statement, IList<OdbcParameter> parameters, RowRange rowRange);

		// Token: 0x06002DF8 RID: 11768
		long ExecuteNonQueryDirect(string commandText, IList<OdbcParameter> parameters, IOdbcStatementRegistrar statementRegistrar);

		// Token: 0x06002DF9 RID: 11769
		OdbcStatementRegistration Prepare(string commandText, IOdbcStatementRegistrar statementRegistrar);
	}
}
