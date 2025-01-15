using System;
using System.Collections.Generic;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x020005ED RID: 1517
	internal abstract class OdbcDelegatingConnection : IOdbcConnection, IDisposable
	{
		// Token: 0x06002FC9 RID: 12233 RVA: 0x0009082E File Offset: 0x0008EA2E
		public OdbcDelegatingConnection(IOdbcConnection innerConnection)
		{
			this.innerConnection = innerConnection;
		}

		// Token: 0x170011CA RID: 4554
		// (get) Token: 0x06002FCA RID: 12234 RVA: 0x0009083D File Offset: 0x0008EA3D
		public IOdbcConnection InnerConnection
		{
			get
			{
				return this.innerConnection;
			}
		}

		// Token: 0x06002FCB RID: 12235 RVA: 0x00090845 File Offset: 0x0008EA45
		public virtual void Open()
		{
			this.innerConnection.Open();
		}

		// Token: 0x06002FCC RID: 12236 RVA: 0x00090852 File Offset: 0x0008EA52
		public virtual int GetInfoInt32(Odbc32.SQL_INFO infoType)
		{
			return this.innerConnection.GetInfoInt32(infoType);
		}

		// Token: 0x06002FCD RID: 12237 RVA: 0x00090860 File Offset: 0x0008EA60
		public virtual string GetInfoString(Odbc32.SQL_INFO infoType)
		{
			return this.innerConnection.GetInfoString(infoType);
		}

		// Token: 0x06002FCE RID: 12238 RVA: 0x0009086E File Offset: 0x0008EA6E
		public virtual bool GetFunctions(Odbc32.SQL_API functionId)
		{
			return this.innerConnection.GetFunctions(functionId);
		}

		// Token: 0x06002FCF RID: 12239 RVA: 0x0009087C File Offset: 0x0008EA7C
		public virtual string GetConnectAttrString(Odbc32.SQL_ATTR attribute)
		{
			return this.innerConnection.GetConnectAttrString(attribute);
		}

		// Token: 0x06002FD0 RID: 12240 RVA: 0x0009088A File Offset: 0x0008EA8A
		public virtual IDisposable UseCatalog(string catalog)
		{
			return this.innerConnection.UseCatalog(catalog);
		}

		// Token: 0x06002FD1 RID: 12241 RVA: 0x00090898 File Offset: 0x0008EA98
		public virtual IDataReaderWithTableSchema GetTables(string catalogName, string schemaName, string tableName, string tableType)
		{
			return this.innerConnection.GetTables(catalogName, schemaName, tableName, tableType);
		}

		// Token: 0x06002FD2 RID: 12242 RVA: 0x000908AA File Offset: 0x0008EAAA
		public virtual IDataReaderWithTableSchema GetColumns(string catalogName, string schemaName, string tableName)
		{
			return this.innerConnection.GetColumns(catalogName, schemaName, tableName);
		}

		// Token: 0x06002FD3 RID: 12243 RVA: 0x000908BA File Offset: 0x0008EABA
		public virtual IDataReaderWithTableSchema GetPrimaryKeys(string catalogName, string schemaName, string tableName)
		{
			return this.innerConnection.GetPrimaryKeys(catalogName, schemaName, tableName);
		}

		// Token: 0x06002FD4 RID: 12244 RVA: 0x000908CA File Offset: 0x0008EACA
		public virtual IDataReaderWithTableSchema GetTypeInfo(short dataType)
		{
			return this.innerConnection.GetTypeInfo(dataType);
		}

		// Token: 0x06002FD5 RID: 12245 RVA: 0x000908D8 File Offset: 0x0008EAD8
		public virtual IDataReaderWithTableSchema GetForeignKeys(string pkCatalogName, string pkSchemaName, string pkTableName, string fkCatalogName, string fkSchemaName, string fkTableName)
		{
			return this.innerConnection.GetForeignKeys(pkCatalogName, pkSchemaName, pkTableName, fkCatalogName, fkSchemaName, fkTableName);
		}

		// Token: 0x06002FD6 RID: 12246 RVA: 0x000908EE File Offset: 0x0008EAEE
		public virtual IDataReaderWithTableSchema GetBestRowId(string catalogName, string schemaName, string tableName)
		{
			return this.innerConnection.GetBestRowId(catalogName, schemaName, tableName);
		}

		// Token: 0x06002FD7 RID: 12247 RVA: 0x000908FE File Offset: 0x0008EAFE
		public virtual IPageReader Execute(OdbcStatementHandle statement, IList<OdbcParameter> parameters, RowRange rowRange)
		{
			return this.innerConnection.Execute(statement, parameters, rowRange);
		}

		// Token: 0x06002FD8 RID: 12248 RVA: 0x0009090E File Offset: 0x0008EB0E
		public virtual IPageReader ExecuteDirect(string commandText, IList<OdbcParameter> parameters, RowRange rowRange, IOdbcStatementRegistrar statementRegistrar)
		{
			return this.innerConnection.ExecuteDirect(commandText, parameters, rowRange, statementRegistrar);
		}

		// Token: 0x06002FD9 RID: 12249 RVA: 0x00090920 File Offset: 0x0008EB20
		public virtual long ExecuteNonQueryDirect(string commandText, IList<OdbcParameter> parameters, IOdbcStatementRegistrar statementRegistrar)
		{
			return this.innerConnection.ExecuteNonQueryDirect(commandText, parameters, statementRegistrar);
		}

		// Token: 0x06002FDA RID: 12250 RVA: 0x00090930 File Offset: 0x0008EB30
		public virtual OdbcStatementRegistration Prepare(string commandText, IOdbcStatementRegistrar statementRegistrar)
		{
			return this.innerConnection.Prepare(commandText, statementRegistrar);
		}

		// Token: 0x06002FDB RID: 12251 RVA: 0x0009093F File Offset: 0x0008EB3F
		public virtual void Dispose()
		{
			if (this.innerConnection != null)
			{
				this.innerConnection.Dispose();
				this.innerConnection = null;
			}
		}

		// Token: 0x04001513 RID: 5395
		private IOdbcConnection innerConnection;
	}
}
