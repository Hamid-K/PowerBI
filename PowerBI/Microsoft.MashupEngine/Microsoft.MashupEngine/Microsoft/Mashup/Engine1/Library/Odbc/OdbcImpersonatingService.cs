using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x020005FB RID: 1531
	internal sealed class OdbcImpersonatingService : IOdbcService
	{
		// Token: 0x0600304D RID: 12365 RVA: 0x0009228C File Offset: 0x0009048C
		public OdbcImpersonatingService(IOdbcService service, Func<IDisposable> impersonationWrapper)
		{
			this.service = service;
			this.impersonationWrapper = impersonationWrapper;
		}

		// Token: 0x0600304E RID: 12366 RVA: 0x000922A2 File Offset: 0x000904A2
		public IOdbcConnection CreateConnection(OdbcConnectionProperties args)
		{
			return new OdbcImpersonatingService.ImpersonatingConnection(this.service.CreateConnection(args), this.impersonationWrapper);
		}

		// Token: 0x0600304F RID: 12367 RVA: 0x000922BC File Offset: 0x000904BC
		public IList<string> GetInstalledDrivers()
		{
			IList<string> installedDrivers;
			using (this.impersonationWrapper())
			{
				installedDrivers = this.service.GetInstalledDrivers();
			}
			return installedDrivers;
		}

		// Token: 0x170011DC RID: 4572
		// (get) Token: 0x06003050 RID: 12368 RVA: 0x00092300 File Offset: 0x00090500
		public int PageSize
		{
			get
			{
				return this.service.PageSize;
			}
		}

		// Token: 0x0400153A RID: 5434
		private readonly IOdbcService service;

		// Token: 0x0400153B RID: 5435
		private readonly Func<IDisposable> impersonationWrapper;

		// Token: 0x020005FC RID: 1532
		private sealed class ImpersonatingConnection : IOdbcConnection, IDisposable
		{
			// Token: 0x06003051 RID: 12369 RVA: 0x0009230D File Offset: 0x0009050D
			public ImpersonatingConnection(IOdbcConnection odbcConnection, Func<IDisposable> impersonationWrapper)
			{
				this.odbcConnection = odbcConnection;
				this.impersonationWrapper = impersonationWrapper;
			}

			// Token: 0x06003052 RID: 12370 RVA: 0x00092324 File Offset: 0x00090524
			public void Open()
			{
				using (this.impersonationWrapper())
				{
					this.odbcConnection.Open();
				}
			}

			// Token: 0x06003053 RID: 12371 RVA: 0x00092364 File Offset: 0x00090564
			public int GetInfoInt32(Odbc32.SQL_INFO infoType)
			{
				int infoInt;
				using (this.impersonationWrapper())
				{
					infoInt = this.odbcConnection.GetInfoInt32(infoType);
				}
				return infoInt;
			}

			// Token: 0x06003054 RID: 12372 RVA: 0x000923A8 File Offset: 0x000905A8
			public string GetInfoString(Odbc32.SQL_INFO infoType)
			{
				string infoString;
				using (this.impersonationWrapper())
				{
					infoString = this.odbcConnection.GetInfoString(infoType);
				}
				return infoString;
			}

			// Token: 0x06003055 RID: 12373 RVA: 0x000923EC File Offset: 0x000905EC
			public bool GetFunctions(Odbc32.SQL_API functionId)
			{
				bool functions;
				using (this.impersonationWrapper())
				{
					functions = this.odbcConnection.GetFunctions(functionId);
				}
				return functions;
			}

			// Token: 0x06003056 RID: 12374 RVA: 0x00092430 File Offset: 0x00090630
			public string GetConnectAttrString(Odbc32.SQL_ATTR attribute)
			{
				string connectAttrString;
				using (this.impersonationWrapper())
				{
					connectAttrString = this.odbcConnection.GetConnectAttrString(attribute);
				}
				return connectAttrString;
			}

			// Token: 0x06003057 RID: 12375 RVA: 0x00092474 File Offset: 0x00090674
			public IDisposable UseCatalog(string catalog)
			{
				IDisposable disposable2;
				using (this.impersonationWrapper())
				{
					disposable2 = this.odbcConnection.UseCatalog(catalog);
				}
				return disposable2;
			}

			// Token: 0x06003058 RID: 12376 RVA: 0x000924B8 File Offset: 0x000906B8
			public IDataReaderWithTableSchema GetTables(string catalogName, string schemaName, string tableName, string tableType)
			{
				IDataReaderWithTableSchema dataReaderWithTableSchema;
				using (this.impersonationWrapper())
				{
					dataReaderWithTableSchema = new OdbcImpersonatingService.ImpersonatingDataReader(this.impersonationWrapper, this.odbcConnection.GetTables(catalogName, schemaName, tableName, tableType));
				}
				return dataReaderWithTableSchema;
			}

			// Token: 0x06003059 RID: 12377 RVA: 0x0009250C File Offset: 0x0009070C
			public IDataReaderWithTableSchema GetColumns(string catalogName, string schemaName, string tableName)
			{
				IDataReaderWithTableSchema dataReaderWithTableSchema;
				using (this.impersonationWrapper())
				{
					dataReaderWithTableSchema = new OdbcImpersonatingService.ImpersonatingDataReader(this.impersonationWrapper, this.odbcConnection.GetColumns(catalogName, schemaName, tableName));
				}
				return dataReaderWithTableSchema;
			}

			// Token: 0x0600305A RID: 12378 RVA: 0x0009255C File Offset: 0x0009075C
			public IDataReaderWithTableSchema GetPrimaryKeys(string catalogName, string schemaName, string tableName)
			{
				IDataReaderWithTableSchema dataReaderWithTableSchema;
				using (this.impersonationWrapper())
				{
					dataReaderWithTableSchema = new OdbcImpersonatingService.ImpersonatingDataReader(this.impersonationWrapper, this.odbcConnection.GetPrimaryKeys(catalogName, schemaName, tableName));
				}
				return dataReaderWithTableSchema;
			}

			// Token: 0x0600305B RID: 12379 RVA: 0x000925AC File Offset: 0x000907AC
			public IDataReaderWithTableSchema GetForeignKeys(string pkCatalogName, string pkSchemaName, string pkTableName, string fkCatalogName, string fkSchemaName, string fkTableName)
			{
				IDataReaderWithTableSchema dataReaderWithTableSchema;
				using (this.impersonationWrapper())
				{
					dataReaderWithTableSchema = new OdbcImpersonatingService.ImpersonatingDataReader(this.impersonationWrapper, this.odbcConnection.GetForeignKeys(pkCatalogName, pkSchemaName, pkTableName, fkCatalogName, fkSchemaName, fkTableName));
				}
				return dataReaderWithTableSchema;
			}

			// Token: 0x0600305C RID: 12380 RVA: 0x00092604 File Offset: 0x00090804
			public IDataReaderWithTableSchema GetTypeInfo(short dataType)
			{
				IDataReaderWithTableSchema dataReaderWithTableSchema;
				using (this.impersonationWrapper())
				{
					dataReaderWithTableSchema = new OdbcImpersonatingService.ImpersonatingDataReader(this.impersonationWrapper, this.odbcConnection.GetTypeInfo(dataType));
				}
				return dataReaderWithTableSchema;
			}

			// Token: 0x0600305D RID: 12381 RVA: 0x00092654 File Offset: 0x00090854
			public IDataReaderWithTableSchema GetBestRowId(string catalogName, string schemaName, string tableName)
			{
				IDataReaderWithTableSchema dataReaderWithTableSchema;
				using (this.impersonationWrapper())
				{
					dataReaderWithTableSchema = new OdbcImpersonatingService.ImpersonatingDataReader(this.impersonationWrapper, this.odbcConnection.GetBestRowId(catalogName, schemaName, tableName));
				}
				return dataReaderWithTableSchema;
			}

			// Token: 0x0600305E RID: 12382 RVA: 0x000926A4 File Offset: 0x000908A4
			public IPageReader Execute(OdbcStatementHandle statement, IList<OdbcParameter> parameters, RowRange rowRange)
			{
				IPageReader pageReader;
				using (this.impersonationWrapper())
				{
					pageReader = new OdbcImpersonatingService.ImpersonatingPageReader(this.impersonationWrapper, this.odbcConnection.Execute(statement, parameters, rowRange));
				}
				return pageReader;
			}

			// Token: 0x0600305F RID: 12383 RVA: 0x000926F4 File Offset: 0x000908F4
			public IPageReader ExecuteDirect(string commandText, IList<OdbcParameter> parameters, RowRange rowRange, IOdbcStatementRegistrar statementRegistrar)
			{
				IPageReader pageReader;
				using (this.impersonationWrapper())
				{
					pageReader = new OdbcImpersonatingService.ImpersonatingPageReader(this.impersonationWrapper, this.odbcConnection.ExecuteDirect(commandText, parameters, rowRange, statementRegistrar));
				}
				return pageReader;
			}

			// Token: 0x06003060 RID: 12384 RVA: 0x00092748 File Offset: 0x00090948
			public long ExecuteNonQueryDirect(string commandText, IList<OdbcParameter> parameters, IOdbcStatementRegistrar statementRegistrar)
			{
				long num;
				using (this.impersonationWrapper())
				{
					num = this.odbcConnection.ExecuteNonQueryDirect(commandText, parameters, statementRegistrar);
				}
				return num;
			}

			// Token: 0x06003061 RID: 12385 RVA: 0x00092790 File Offset: 0x00090990
			public OdbcStatementRegistration Prepare(string commandText, IOdbcStatementRegistrar statementRegistrar)
			{
				OdbcStatementRegistration odbcStatementRegistration;
				using (this.impersonationWrapper())
				{
					odbcStatementRegistration = this.odbcConnection.Prepare(commandText, statementRegistrar);
				}
				return odbcStatementRegistration;
			}

			// Token: 0x06003062 RID: 12386 RVA: 0x000927D4 File Offset: 0x000909D4
			public void Dispose()
			{
				using (this.impersonationWrapper())
				{
					this.odbcConnection.Dispose();
				}
			}

			// Token: 0x0400153C RID: 5436
			private readonly IOdbcConnection odbcConnection;

			// Token: 0x0400153D RID: 5437
			private readonly Func<IDisposable> impersonationWrapper;
		}

		// Token: 0x020005FD RID: 1533
		private sealed class ImpersonatingDataReader : IDataReaderWithTableSchema, IDataReader, IDisposable, IDataRecord
		{
			// Token: 0x06003063 RID: 12387 RVA: 0x00092814 File Offset: 0x00090A14
			public ImpersonatingDataReader(Func<IDisposable> impersonationWrapper, IDataReaderWithTableSchema dataReader)
			{
				this.impersonationWrapper = impersonationWrapper;
				this.dataReader = dataReader;
			}

			// Token: 0x06003064 RID: 12388 RVA: 0x0009282C File Offset: 0x00090A2C
			public void Close()
			{
				using (this.impersonationWrapper())
				{
					this.dataReader.Close();
				}
			}

			// Token: 0x170011DD RID: 4573
			// (get) Token: 0x06003065 RID: 12389 RVA: 0x0009286C File Offset: 0x00090A6C
			public int Depth
			{
				get
				{
					int depth;
					using (this.impersonationWrapper())
					{
						depth = this.dataReader.Depth;
					}
					return depth;
				}
			}

			// Token: 0x170011DE RID: 4574
			// (get) Token: 0x06003066 RID: 12390 RVA: 0x000928B0 File Offset: 0x00090AB0
			public bool IsClosed
			{
				get
				{
					bool isClosed;
					using (this.impersonationWrapper())
					{
						isClosed = this.dataReader.IsClosed;
					}
					return isClosed;
				}
			}

			// Token: 0x06003067 RID: 12391 RVA: 0x000928F4 File Offset: 0x00090AF4
			public bool NextResult()
			{
				bool flag2;
				using (this.impersonationWrapper())
				{
					bool flag = this.dataReader.NextResult();
					if (flag)
					{
						this.schema = null;
					}
					flag2 = flag;
				}
				return flag2;
			}

			// Token: 0x06003068 RID: 12392 RVA: 0x00092940 File Offset: 0x00090B40
			public bool Read()
			{
				bool flag;
				using (this.impersonationWrapper())
				{
					flag = this.dataReader.Read();
				}
				return flag;
			}

			// Token: 0x170011DF RID: 4575
			// (get) Token: 0x06003069 RID: 12393 RVA: 0x00092984 File Offset: 0x00090B84
			public int RecordsAffected
			{
				get
				{
					int recordsAffected;
					using (this.impersonationWrapper())
					{
						recordsAffected = this.dataReader.RecordsAffected;
					}
					return recordsAffected;
				}
			}

			// Token: 0x0600306A RID: 12394 RVA: 0x000929C8 File Offset: 0x00090BC8
			public void Dispose()
			{
				using (this.impersonationWrapper())
				{
					this.dataReader.Dispose();
				}
			}

			// Token: 0x170011E0 RID: 4576
			// (get) Token: 0x0600306B RID: 12395 RVA: 0x00092A08 File Offset: 0x00090C08
			public int FieldCount
			{
				get
				{
					int fieldCount;
					using (this.impersonationWrapper())
					{
						fieldCount = this.dataReader.FieldCount;
					}
					return fieldCount;
				}
			}

			// Token: 0x170011E1 RID: 4577
			// (get) Token: 0x0600306C RID: 12396 RVA: 0x00092A4C File Offset: 0x00090C4C
			public TableSchema Schema
			{
				get
				{
					if (this.schema == null)
					{
						using (this.impersonationWrapper())
						{
							this.schema = this.dataReader.Schema;
						}
					}
					return this.schema;
				}
			}

			// Token: 0x0600306D RID: 12397 RVA: 0x00092AA0 File Offset: 0x00090CA0
			public bool GetBoolean(int i)
			{
				return this.dataReader.GetBoolean(i);
			}

			// Token: 0x0600306E RID: 12398 RVA: 0x00092AAE File Offset: 0x00090CAE
			public byte GetByte(int i)
			{
				return this.dataReader.GetByte(i);
			}

			// Token: 0x0600306F RID: 12399 RVA: 0x00092ABC File Offset: 0x00090CBC
			public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
			{
				return this.dataReader.GetBytes(i, fieldOffset, buffer, bufferoffset, length);
			}

			// Token: 0x06003070 RID: 12400 RVA: 0x00092AD0 File Offset: 0x00090CD0
			public char GetChar(int i)
			{
				return this.dataReader.GetChar(i);
			}

			// Token: 0x06003071 RID: 12401 RVA: 0x00092ADE File Offset: 0x00090CDE
			public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
			{
				return this.dataReader.GetChars(i, fieldoffset, buffer, bufferoffset, length);
			}

			// Token: 0x06003072 RID: 12402 RVA: 0x00092AF2 File Offset: 0x00090CF2
			public IDataReader GetData(int i)
			{
				return this.dataReader.GetData(i);
			}

			// Token: 0x06003073 RID: 12403 RVA: 0x00092B00 File Offset: 0x00090D00
			public string GetDataTypeName(int i)
			{
				return this.dataReader.GetDataTypeName(i);
			}

			// Token: 0x06003074 RID: 12404 RVA: 0x00092B0E File Offset: 0x00090D0E
			public DateTime GetDateTime(int i)
			{
				return this.dataReader.GetDateTime(i);
			}

			// Token: 0x06003075 RID: 12405 RVA: 0x00092B1C File Offset: 0x00090D1C
			public decimal GetDecimal(int i)
			{
				return this.dataReader.GetDecimal(i);
			}

			// Token: 0x06003076 RID: 12406 RVA: 0x00092B2A File Offset: 0x00090D2A
			public double GetDouble(int i)
			{
				return this.dataReader.GetDouble(i);
			}

			// Token: 0x06003077 RID: 12407 RVA: 0x00092B38 File Offset: 0x00090D38
			public Type GetFieldType(int i)
			{
				return this.dataReader.GetFieldType(i);
			}

			// Token: 0x06003078 RID: 12408 RVA: 0x00092B46 File Offset: 0x00090D46
			public float GetFloat(int i)
			{
				return this.dataReader.GetFloat(i);
			}

			// Token: 0x06003079 RID: 12409 RVA: 0x00092B54 File Offset: 0x00090D54
			public Guid GetGuid(int i)
			{
				return this.dataReader.GetGuid(i);
			}

			// Token: 0x0600307A RID: 12410 RVA: 0x00092B62 File Offset: 0x00090D62
			public short GetInt16(int i)
			{
				return this.dataReader.GetInt16(i);
			}

			// Token: 0x0600307B RID: 12411 RVA: 0x00092B70 File Offset: 0x00090D70
			public int GetInt32(int i)
			{
				return this.dataReader.GetInt32(i);
			}

			// Token: 0x0600307C RID: 12412 RVA: 0x00092B7E File Offset: 0x00090D7E
			public long GetInt64(int i)
			{
				return this.dataReader.GetInt64(i);
			}

			// Token: 0x0600307D RID: 12413 RVA: 0x00092B8C File Offset: 0x00090D8C
			public string GetName(int i)
			{
				return this.dataReader.GetName(i);
			}

			// Token: 0x0600307E RID: 12414 RVA: 0x00092B9A File Offset: 0x00090D9A
			public int GetOrdinal(string name)
			{
				return this.dataReader.GetOrdinal(name);
			}

			// Token: 0x0600307F RID: 12415 RVA: 0x00092BA8 File Offset: 0x00090DA8
			public string GetString(int i)
			{
				return this.dataReader.GetString(i);
			}

			// Token: 0x06003080 RID: 12416 RVA: 0x00092BB6 File Offset: 0x00090DB6
			public object GetValue(int i)
			{
				return this.dataReader.GetValue(i);
			}

			// Token: 0x06003081 RID: 12417 RVA: 0x00092BC4 File Offset: 0x00090DC4
			public int GetValues(object[] values)
			{
				return this.dataReader.GetValues(values);
			}

			// Token: 0x06003082 RID: 12418 RVA: 0x00092BD2 File Offset: 0x00090DD2
			public bool IsDBNull(int i)
			{
				return this.dataReader.IsDBNull(i);
			}

			// Token: 0x170011E2 RID: 4578
			public object this[string name]
			{
				get
				{
					return this.dataReader[name];
				}
			}

			// Token: 0x170011E3 RID: 4579
			public object this[int i]
			{
				get
				{
					return this.dataReader[i];
				}
			}

			// Token: 0x06003085 RID: 12421 RVA: 0x00092BFC File Offset: 0x00090DFC
			[Obsolete]
			public DataTable GetSchemaTable()
			{
				return this.Schema.ToDataTable();
			}

			// Token: 0x0400153E RID: 5438
			private readonly Func<IDisposable> impersonationWrapper;

			// Token: 0x0400153F RID: 5439
			private readonly IDataReaderWithTableSchema dataReader;

			// Token: 0x04001540 RID: 5440
			private TableSchema schema;
		}

		// Token: 0x020005FE RID: 1534
		private sealed class ImpersonatingPageReader : IPageReader, IDisposable
		{
			// Token: 0x06003086 RID: 12422 RVA: 0x00092C09 File Offset: 0x00090E09
			public ImpersonatingPageReader(Func<IDisposable> impersonationWrapper, IPageReader pageReader)
			{
				this.impersonationWrapper = impersonationWrapper;
				this.pageReader = pageReader;
			}

			// Token: 0x170011E4 RID: 4580
			// (get) Token: 0x06003087 RID: 12423 RVA: 0x00092C20 File Offset: 0x00090E20
			public TableSchema Schema
			{
				get
				{
					if (this.schema == null)
					{
						using (this.impersonationWrapper())
						{
							this.schema = this.pageReader.Schema;
						}
					}
					return this.schema;
				}
			}

			// Token: 0x170011E5 RID: 4581
			// (get) Token: 0x06003088 RID: 12424 RVA: 0x00092C74 File Offset: 0x00090E74
			public IProgress Progress
			{
				get
				{
					return this.pageReader.Progress;
				}
			}

			// Token: 0x170011E6 RID: 4582
			// (get) Token: 0x06003089 RID: 12425 RVA: 0x00092C81 File Offset: 0x00090E81
			public int MaxPageRowCount
			{
				get
				{
					return this.pageReader.MaxPageRowCount;
				}
			}

			// Token: 0x0600308A RID: 12426 RVA: 0x00092C90 File Offset: 0x00090E90
			public IPage CreatePage()
			{
				IPage page;
				using (this.impersonationWrapper())
				{
					page = this.pageReader.CreatePage();
				}
				return page;
			}

			// Token: 0x0600308B RID: 12427 RVA: 0x00092CD4 File Offset: 0x00090ED4
			public void Read(IPage page)
			{
				using (this.impersonationWrapper())
				{
					this.pageReader.Read(page);
				}
			}

			// Token: 0x0600308C RID: 12428 RVA: 0x00092D18 File Offset: 0x00090F18
			public IPageReader NextResult()
			{
				IPageReader pageReader2;
				using (this.impersonationWrapper())
				{
					IPageReader pageReader = this.pageReader.NextResult();
					if (pageReader != null)
					{
						pageReader = new OdbcImpersonatingService.ImpersonatingPageReader(this.impersonationWrapper, pageReader);
					}
					pageReader2 = pageReader;
				}
				return pageReader2;
			}

			// Token: 0x0600308D RID: 12429 RVA: 0x00092D6C File Offset: 0x00090F6C
			public void Dispose()
			{
				using (this.impersonationWrapper())
				{
					this.pageReader.Dispose();
				}
			}

			// Token: 0x04001541 RID: 5441
			private readonly Func<IDisposable> impersonationWrapper;

			// Token: 0x04001542 RID: 5442
			private readonly IPageReader pageReader;

			// Token: 0x04001543 RID: 5443
			private TableSchema schema;
		}
	}
}
