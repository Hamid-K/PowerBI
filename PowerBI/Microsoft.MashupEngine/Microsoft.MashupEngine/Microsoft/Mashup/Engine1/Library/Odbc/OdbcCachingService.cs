using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x020005BA RID: 1466
	internal class OdbcCachingService : OdbcDelegatingService
	{
		// Token: 0x06002E10 RID: 11792 RVA: 0x0008C468 File Offset: 0x0008A668
		public OdbcCachingService(IEngineHost host, IOdbcService odbc)
			: base(odbc)
		{
			this.objectCache = host.QueryService<ICacheSets>().Metadata.PersistentObjectCache;
			this.odbc = odbc;
		}

		// Token: 0x06002E11 RID: 11793 RVA: 0x0008C48E File Offset: 0x0008A68E
		public override IOdbcConnection CreateConnection(OdbcConnectionProperties args)
		{
			return new OdbcCachingService.OdbcCachingConnection(this, args);
		}

		// Token: 0x0400142C RID: 5164
		private readonly IPersistentObjectCache objectCache;

		// Token: 0x0400142D RID: 5165
		private readonly IOdbcService odbc;

		// Token: 0x020005BB RID: 1467
		private class OdbcCachingConnection : IOdbcConnection, IDisposable
		{
			// Token: 0x06002E12 RID: 11794 RVA: 0x0008C498 File Offset: 0x0008A698
			public OdbcCachingConnection(OdbcCachingService service, OdbcConnectionProperties args)
			{
				this.service = service;
				this.args = args;
				this.cacheContext = args.CacheContext;
				this.getInfo = new OdbcCachingService.OdbcCachingConnection.OdbcCachingSqlGetInfo(this);
				this.getFunctions = new OdbcCachingService.OdbcCachingConnection.OdbcCachingSqlGetFunctions(this);
				this.getConnectAttr = new OdbcCachingService.OdbcCachingConnection.OdbcCachingSqlGetConnectAttr(this);
				this.currentCatalog = this.args.Catalog;
				this.delayedUseCatalog = OdbcCachingService.OdbcCachingConnection.OdbcCachingUseCatalog.Empty;
			}

			// Token: 0x06002E13 RID: 11795 RVA: 0x0008C508 File Offset: 0x0008A708
			private IOdbcConnection GetOpenedInnerConnection()
			{
				if (this.innerConnection == null)
				{
					IOdbcConnection odbcConnection = null;
					try
					{
						odbcConnection = this.service.odbc.CreateConnection(this.args);
						odbcConnection.Open();
						this.innerConnection = odbcConnection;
						this.delayedUseCatalog.UseCatalog(this.innerConnection);
					}
					catch
					{
						if (odbcConnection != null)
						{
							odbcConnection.Dispose();
						}
						throw;
					}
				}
				return this.innerConnection;
			}

			// Token: 0x06002E14 RID: 11796 RVA: 0x0000336E File Offset: 0x0000156E
			public void Open()
			{
			}

			// Token: 0x06002E15 RID: 11797 RVA: 0x0008C578 File Offset: 0x0008A778
			public int GetInfoInt32(Odbc32.SQL_INFO infoType)
			{
				return this.getInfo.GetInfoInt32(infoType);
			}

			// Token: 0x06002E16 RID: 11798 RVA: 0x0008C586 File Offset: 0x0008A786
			public string GetInfoString(Odbc32.SQL_INFO infoType)
			{
				return this.getInfo.GetInfoString(infoType);
			}

			// Token: 0x06002E17 RID: 11799 RVA: 0x0008C594 File Offset: 0x0008A794
			public bool GetFunctions(Odbc32.SQL_API functionId)
			{
				return this.getFunctions.GetFunctions(functionId);
			}

			// Token: 0x06002E18 RID: 11800 RVA: 0x0008C5A2 File Offset: 0x0008A7A2
			public string GetConnectAttrString(Odbc32.SQL_ATTR attribute)
			{
				return this.getConnectAttr.GetConnectAttrString(attribute);
			}

			// Token: 0x06002E19 RID: 11801 RVA: 0x0008C5B0 File Offset: 0x0008A7B0
			public IDisposable UseCatalog(string catalog)
			{
				if (this.currentCatalog == catalog)
				{
					return DisposableExtensions.Empty;
				}
				this.delayedUseCatalog = new OdbcCachingService.OdbcCachingConnection.OdbcCachingUseCatalog(this, catalog);
				if (this.innerConnection != null)
				{
					this.delayedUseCatalog.UseCatalog(this.innerConnection);
				}
				this.cacheContext = this.cacheContext.WithCatalog(catalog);
				return this.delayedUseCatalog;
			}

			// Token: 0x06002E1A RID: 11802 RVA: 0x0008C610 File Offset: 0x0008A810
			public IDataReaderWithTableSchema GetTables(string catalogName, string schemaName, string tableName, string tableType)
			{
				return this.Cache(new string[] { "SQLTables/1", catalogName, schemaName, tableName, tableType }, (IOdbcConnection connection) => connection.GetTables(catalogName, schemaName, tableName, tableType));
			}

			// Token: 0x06002E1B RID: 11803 RVA: 0x0008C684 File Offset: 0x0008A884
			public IDataReaderWithTableSchema GetColumns(string catalogName, string schemaName, string tableName)
			{
				return this.Cache(new string[] { "SQLColumns/1", catalogName, schemaName, tableName }, (IOdbcConnection connection) => connection.GetColumns(catalogName, schemaName, tableName));
			}

			// Token: 0x06002E1C RID: 11804 RVA: 0x0008C6E8 File Offset: 0x0008A8E8
			public IDataReaderWithTableSchema GetPrimaryKeys(string catalogName, string schemaName, string tableName)
			{
				return this.Cache(new string[] { "SQLPrimaryKeys/1", catalogName, schemaName, tableName }, (IOdbcConnection connection) => connection.GetPrimaryKeys(catalogName, schemaName, tableName));
			}

			// Token: 0x06002E1D RID: 11805 RVA: 0x0008C74C File Offset: 0x0008A94C
			public IDataReaderWithTableSchema GetForeignKeys(string pkCatalogName, string pkSchemaName, string pkTableName, string fkCatalogName, string fkSchemaName, string fkTableName)
			{
				return this.Cache(new string[] { "SQLForeignKeys/1", pkCatalogName, pkSchemaName, pkTableName, fkCatalogName, fkSchemaName, fkTableName }, (IOdbcConnection connection) => connection.GetForeignKeys(pkCatalogName, pkSchemaName, pkTableName, fkCatalogName, fkSchemaName, fkTableName));
			}

			// Token: 0x06002E1E RID: 11806 RVA: 0x0008C7E4 File Offset: 0x0008A9E4
			public IDataReaderWithTableSchema GetTypeInfo(short dataType)
			{
				return this.Cache(new string[]
				{
					"SQLGetTypeInfo/1",
					dataType.ToString(CultureInfo.InvariantCulture)
				}, (IOdbcConnection connection) => connection.GetTypeInfo(dataType));
			}

			// Token: 0x06002E1F RID: 11807 RVA: 0x0008C834 File Offset: 0x0008AA34
			public IDataReaderWithTableSchema GetBestRowId(string catalogName, string schemaName, string tableName)
			{
				return this.Cache(new string[] { "GetBestRowId/1", catalogName, schemaName, tableName }, (IOdbcConnection connection) => connection.GetBestRowId(catalogName, schemaName, tableName));
			}

			// Token: 0x06002E20 RID: 11808 RVA: 0x0008C897 File Offset: 0x0008AA97
			public IPageReader Execute(OdbcStatementHandle statement, IList<OdbcParameter> parameters, RowRange rowRange)
			{
				return this.GetOpenedInnerConnection().Execute(statement, parameters, rowRange);
			}

			// Token: 0x06002E21 RID: 11809 RVA: 0x0008C8A7 File Offset: 0x0008AAA7
			public IPageReader ExecuteDirect(string commandText, IList<OdbcParameter> parameters, RowRange rowRange, IOdbcStatementRegistrar statementRegistrar)
			{
				return this.GetOpenedInnerConnection().ExecuteDirect(commandText, parameters, rowRange, statementRegistrar);
			}

			// Token: 0x06002E22 RID: 11810 RVA: 0x0008C8B9 File Offset: 0x0008AAB9
			public long ExecuteNonQueryDirect(string commandText, IList<OdbcParameter> parameters, IOdbcStatementRegistrar statementRegistrar)
			{
				return this.GetOpenedInnerConnection().ExecuteNonQueryDirect(commandText, parameters, statementRegistrar);
			}

			// Token: 0x06002E23 RID: 11811 RVA: 0x0008C8C9 File Offset: 0x0008AAC9
			public OdbcStatementRegistration Prepare(string commandText, IOdbcStatementRegistrar statementRegistrar)
			{
				return this.GetOpenedInnerConnection().Prepare(commandText, statementRegistrar);
			}

			// Token: 0x06002E24 RID: 11812 RVA: 0x0008C8D8 File Offset: 0x0008AAD8
			private IDataReaderWithTableSchema Cache(string[] keyParts, Func<IOdbcConnection, IDataReaderWithTableSchema> func)
			{
				OneOf<DataTable, OdbcException> orCommitValue = this.service.objectCache.GetOrCommitValue(this.cacheContext.GetStructuredCacheKey(keyParts), delegate
				{
					IOdbcConnection openedInnerConnection = this.GetOpenedInnerConnection();
					OneOf<DataTable, OdbcException> oneOf;
					try
					{
						using (IDataReader dataReader = func(openedInnerConnection))
						{
							oneOf = OdbcCachingService.OdbcCachingConnection.DataTableFrom(dataReader);
						}
					}
					catch (OdbcException ex2)
					{
						if (!ex2.IsNonTransient)
						{
							throw;
						}
						oneOf = ex2;
					}
					return oneOf;
				}, new Action<Stream, OneOf<DataTable, OdbcException>>(OdbcCachingService.OdbcCachingConnection.SerializeTableOrException), new Func<Stream, OneOf<DataTable, OdbcException>>(OdbcCachingService.OdbcCachingConnection.DeserializeTableOrException));
				OdbcException ex = orCommitValue.As<OdbcException>();
				if (ex != null)
				{
					throw ex;
				}
				return orCommitValue.As<DataTable>().CreateDataReader().WithTableSchema();
			}

			// Token: 0x06002E25 RID: 11813 RVA: 0x0008C958 File Offset: 0x0008AB58
			public void Dispose()
			{
				if (this.getFunctions != null)
				{
					this.getFunctions.Flush();
					this.getFunctions = null;
				}
				if (this.getInfo != null)
				{
					this.getInfo.Flush();
					this.getInfo = null;
				}
				if (this.innerConnection != null)
				{
					this.innerConnection.Dispose();
					this.innerConnection = null;
				}
			}

			// Token: 0x06002E26 RID: 11814 RVA: 0x0008C9B4 File Offset: 0x0008ABB4
			private static void WriteException(Stream stream, OdbcException e)
			{
				BinaryWriter binaryWriter = new BinaryWriter(stream);
				binaryWriter.Write(false);
				binaryWriter.Write((short)e.ReturnCode);
				binaryWriter.Write(e.Errors.Count);
				foreach (OdbcError odbcError in e.Errors)
				{
					binaryWriter.Write(odbcError.Message);
					binaryWriter.Write(odbcError.NativeError);
					binaryWriter.Write(odbcError.SQLState);
				}
				binaryWriter.Flush();
			}

			// Token: 0x06002E27 RID: 11815 RVA: 0x0008CA50 File Offset: 0x0008AC50
			private static OdbcException ReadException(BinaryReader reader)
			{
				reader.ReadBoolean();
				Odbc32.RetCode retCode = (Odbc32.RetCode)reader.ReadInt16();
				int num = reader.ReadInt32();
				OdbcError[] array = new OdbcError[num];
				for (int i = 0; i < num; i++)
				{
					string text = reader.ReadString();
					int num2 = reader.ReadInt32();
					string text2 = reader.ReadString();
					array[i] = new OdbcError(text, text2, num2);
				}
				return new OdbcException(retCode, array);
			}

			// Token: 0x06002E28 RID: 11816 RVA: 0x0008CAB4 File Offset: 0x0008ACB4
			private static OneOf<DataTable, OdbcException> DeserializeTableOrException(Stream stream)
			{
				BinaryReader binaryReader = new BinaryReader(stream);
				if (binaryReader.ReadBoolean())
				{
					using (IDataReader dataReader = DbData.Deserialize(stream))
					{
						return OdbcCachingService.OdbcCachingConnection.DataTableFrom(dataReader);
					}
				}
				return OdbcCachingService.OdbcCachingConnection.ReadException(binaryReader);
			}

			// Token: 0x06002E29 RID: 11817 RVA: 0x0008CB0C File Offset: 0x0008AD0C
			private static void SerializeTableOrException(Stream stream, OneOf<DataTable, OdbcException> value)
			{
				BinaryWriter binaryWriter = new BinaryWriter(stream);
				OdbcException ex = value.As<OdbcException>();
				if (ex != null)
				{
					binaryWriter.Write(false);
					binaryWriter.Flush();
					OdbcCachingService.OdbcCachingConnection.WriteException(stream, ex);
					return;
				}
				binaryWriter.Write(true);
				binaryWriter.Flush();
				using (IDataReaderWithTableSchema dataReaderWithTableSchema = value.CreateDataReader().WithTableSchema())
				{
					DbData.Serialize(dataReaderWithTableSchema, stream);
				}
			}

			// Token: 0x06002E2A RID: 11818 RVA: 0x0008CB84 File Offset: 0x0008AD84
			private static DataTable DataTableFrom(IDataReader reader)
			{
				DataTable dataTable = new DataTable();
				dataTable.Locale = CultureInfo.InvariantCulture;
				foreach (SchemaColumn schemaColumn in TableSchema.FromDataReader(reader))
				{
					dataTable.Columns.Add(OdbcCachingService.OdbcCachingConnection.GetUniqueName(dataTable.Columns, schemaColumn.Name), schemaColumn.DataType);
				}
				object[] array = new object[dataTable.Columns.Count];
				while (reader.Read())
				{
					reader.GetValues(array);
					dataTable.Rows.Add(array);
				}
				return dataTable;
			}

			// Token: 0x06002E2B RID: 11819 RVA: 0x0008CC30 File Offset: 0x0008AE30
			private static string GetUniqueName(DataColumnCollection existingColumns, string columnName)
			{
				string text = columnName;
				int num = 2;
				while (existingColumns.Contains(text))
				{
					text = columnName + num.ToString(CultureInfo.InvariantCulture);
					num++;
				}
				return text;
			}

			// Token: 0x0400142E RID: 5166
			private readonly OdbcCachingService service;

			// Token: 0x0400142F RID: 5167
			private readonly OdbcConnectionProperties args;

			// Token: 0x04001430 RID: 5168
			private OdbcCachingService.OdbcCachingConnection.OdbcCachingSqlGetInfo getInfo;

			// Token: 0x04001431 RID: 5169
			private OdbcCachingService.OdbcCachingConnection.OdbcCachingSqlGetFunctions getFunctions;

			// Token: 0x04001432 RID: 5170
			private OdbcCachingService.OdbcCachingConnection.OdbcCachingSqlGetConnectAttr getConnectAttr;

			// Token: 0x04001433 RID: 5171
			private IOdbcConnection innerConnection;

			// Token: 0x04001434 RID: 5172
			private string currentCatalog;

			// Token: 0x04001435 RID: 5173
			private OdbcCachingService.OdbcCachingConnection.OdbcCachingUseCatalog delayedUseCatalog;

			// Token: 0x04001436 RID: 5174
			private OdbcCacheContext cacheContext;

			// Token: 0x020005BC RID: 1468
			private class OdbcCachingUseCatalog : IDisposable
			{
				// Token: 0x06002E2C RID: 11820 RVA: 0x0008CC64 File Offset: 0x0008AE64
				public OdbcCachingUseCatalog(OdbcCachingService.OdbcCachingConnection connection, string catalog)
				{
					this.connection = connection;
					this.catalog = catalog;
					if (this.connection != null)
					{
						this.previousCatalog = this.connection.currentCatalog;
						this.connection.currentCatalog = this.catalog;
					}
				}

				// Token: 0x06002E2D RID: 11821 RVA: 0x0008CCA4 File Offset: 0x0008AEA4
				public void UseCatalog(IOdbcConnection openedInnerConnection)
				{
					if (this.connection != null)
					{
						this.previousDelayedUseCatalog = this.connection.delayedUseCatalog;
						this.disposable = openedInnerConnection.UseCatalog(this.catalog);
					}
				}

				// Token: 0x06002E2E RID: 11822 RVA: 0x0008CCD4 File Offset: 0x0008AED4
				public void Dispose()
				{
					try
					{
						if (this.disposable != null)
						{
							this.disposable.Dispose();
							this.disposable = null;
						}
					}
					finally
					{
						if (this.connection != null)
						{
							this.connection.currentCatalog = this.previousCatalog;
							this.connection.delayedUseCatalog = this.previousDelayedUseCatalog;
							this.connection = null;
						}
					}
				}

				// Token: 0x04001437 RID: 5175
				public static readonly OdbcCachingService.OdbcCachingConnection.OdbcCachingUseCatalog Empty = new OdbcCachingService.OdbcCachingConnection.OdbcCachingUseCatalog(null, null);

				// Token: 0x04001438 RID: 5176
				private readonly string catalog;

				// Token: 0x04001439 RID: 5177
				private readonly string previousCatalog;

				// Token: 0x0400143A RID: 5178
				private OdbcCachingService.OdbcCachingConnection connection;

				// Token: 0x0400143B RID: 5179
				private OdbcCachingService.OdbcCachingConnection.OdbcCachingUseCatalog previousDelayedUseCatalog;

				// Token: 0x0400143C RID: 5180
				private IDisposable disposable;
			}

			// Token: 0x020005BD RID: 1469
			private class OdbcCachingSqlGetInfo : OdbcCachingService.OdbcCachingConnection.OdbcFunctionCaching
			{
				// Token: 0x06002E30 RID: 11824 RVA: 0x0008CD4E File Offset: 0x0008AF4E
				public OdbcCachingSqlGetInfo(OdbcCachingService.OdbcCachingConnection connection)
					: base(connection, "SQLGetInfo")
				{
				}

				// Token: 0x06002E31 RID: 11825 RVA: 0x0008CD5C File Offset: 0x0008AF5C
				public int GetInfoInt32(Odbc32.SQL_INFO infoType)
				{
					return base.GetResult<int>((ushort)infoType, (IOdbcConnection connnection, ushort t) => connnection.GetInfoInt32((Odbc32.SQL_INFO)t));
				}

				// Token: 0x06002E32 RID: 11826 RVA: 0x0008CD84 File Offset: 0x0008AF84
				public string GetInfoString(Odbc32.SQL_INFO infoType)
				{
					return base.GetResult<string>((ushort)infoType, (IOdbcConnection connection, ushort t) => connection.GetInfoString((Odbc32.SQL_INFO)t));
				}
			}

			// Token: 0x020005BF RID: 1471
			private class OdbcCachingSqlGetFunctions : OdbcCachingService.OdbcCachingConnection.OdbcFunctionCaching
			{
				// Token: 0x06002E37 RID: 11831 RVA: 0x0008CDCA File Offset: 0x0008AFCA
				public OdbcCachingSqlGetFunctions(OdbcCachingService.OdbcCachingConnection connection)
					: base(connection, "SQLGetFunctions")
				{
				}

				// Token: 0x06002E38 RID: 11832 RVA: 0x0008CDD8 File Offset: 0x0008AFD8
				public bool GetFunctions(Odbc32.SQL_API functionId)
				{
					return base.GetResult<bool>((ushort)functionId, (IOdbcConnection connection, ushort t) => connection.GetFunctions((Odbc32.SQL_API)t));
				}
			}

			// Token: 0x020005C1 RID: 1473
			private class OdbcCachingSqlGetConnectAttr : OdbcCachingService.OdbcCachingConnection.OdbcFunctionCaching
			{
				// Token: 0x06002E3C RID: 11836 RVA: 0x0008CE15 File Offset: 0x0008B015
				public OdbcCachingSqlGetConnectAttr(OdbcCachingService.OdbcCachingConnection connection)
					: base(connection, "SqlGetConnectAttr")
				{
				}

				// Token: 0x06002E3D RID: 11837 RVA: 0x0008CE23 File Offset: 0x0008B023
				public string GetConnectAttrString(Odbc32.SQL_ATTR attribute)
				{
					return base.GetResult<string>((ushort)attribute, (IOdbcConnection connection, ushort t) => connection.GetConnectAttrString((Odbc32.SQL_ATTR)t));
				}
			}

			// Token: 0x020005C3 RID: 1475
			private abstract class OdbcFunctionCaching
			{
				// Token: 0x06002E41 RID: 11841 RVA: 0x0008CE61 File Offset: 0x0008B061
				public OdbcFunctionCaching(OdbcCachingService.OdbcCachingConnection connection, string functionKey)
				{
					this.connection = connection;
					this.key = this.connection.cacheContext.GetStructuredCacheKey(new string[] { "FunctionCache/1", functionKey });
				}

				// Token: 0x06002E42 RID: 11842 RVA: 0x0008CE98 File Offset: 0x0008B098
				public void Flush()
				{
					if (this.connection != null && this.cacheMissed)
					{
						this.Serialize();
					}
				}

				// Token: 0x06002E43 RID: 11843 RVA: 0x0008CEB0 File Offset: 0x0008B0B0
				private void EnsureInitialized()
				{
					if (this.results == null)
					{
						this.Deserialize();
					}
				}

				// Token: 0x06002E44 RID: 11844 RVA: 0x0008CEC0 File Offset: 0x0008B0C0
				protected Output GetResult<Output>(ushort input, Func<IOdbcConnection, ushort, Output> function)
				{
					this.EnsureInitialized();
					object obj;
					if (!this.results.TryGetValue(input, out obj))
					{
						if (!this.cacheMissed)
						{
							this.cacheMissed = true;
							Dictionary<ushort, object> dictionary = new Dictionary<ushort, object>(this.results);
							this.results = dictionary;
						}
						IOdbcConnection openedInnerConnection = this.connection.GetOpenedInnerConnection();
						Output output2;
						try
						{
							Output output = function(openedInnerConnection, input);
							this.results[input] = output;
							output2 = output;
						}
						catch (OdbcException ex)
						{
							if (ex.IsSafe)
							{
								this.results[input] = ex.Errors;
							}
							throw;
						}
						return output2;
					}
					IList<OdbcError> list = obj as IList<OdbcError>;
					if (list == null)
					{
						return (Output)((object)obj);
					}
					throw new OdbcException(Odbc32.RetCode.ERROR, list);
				}

				// Token: 0x06002E45 RID: 11845 RVA: 0x0008CF80 File Offset: 0x0008B180
				private void Deserialize()
				{
					object obj;
					if (this.connection.service.objectCache.TryGetValue(this.key, new Func<Stream, object>(OdbcCachingService.OdbcCachingConnection.OdbcFunctionCaching.DeserializeDictionary), out obj))
					{
						this.results = (Dictionary<ushort, object>)obj;
						return;
					}
					this.results = new Dictionary<ushort, object>();
				}

				// Token: 0x06002E46 RID: 11846 RVA: 0x0008CFD8 File Offset: 0x0008B1D8
				private static object DeserializeDictionary(Stream stream)
				{
					Dictionary<ushort, object> dictionary = new Dictionary<ushort, object>();
					using (BinaryReader binaryReader = new BinaryReader(stream))
					{
						int num = binaryReader.ReadInt32();
						for (int i = 0; i < num; i++)
						{
							ushort num2 = binaryReader.ReadUInt16();
							if (binaryReader.ReadBoolean())
							{
								ObjectTag objectTag = binaryReader.ReadObjectTag();
								object obj = binaryReader.ReadObject(objectTag);
								dictionary.Add(num2, obj);
							}
							else
							{
								int num3 = binaryReader.ReadInt32();
								OdbcError[] array = new OdbcError[num3];
								for (int j = 0; j < num3; j++)
								{
									string text = binaryReader.ReadString();
									int num4 = binaryReader.ReadInt32();
									string text2 = binaryReader.ReadString();
									array[j] = new OdbcError(text2, text, num4);
								}
								dictionary.Add(num2, array);
							}
						}
					}
					return dictionary;
				}

				// Token: 0x06002E47 RID: 11847 RVA: 0x0008D0AC File Offset: 0x0008B2AC
				private void Serialize()
				{
					this.connection.service.objectCache.CommitValue(this.key, new Action<Stream, object>(OdbcCachingService.OdbcCachingConnection.OdbcFunctionCaching.SerializeDictionary), this.results);
				}

				// Token: 0x06002E48 RID: 11848 RVA: 0x0008D0E0 File Offset: 0x0008B2E0
				private static void SerializeDictionary(Stream stream, object value)
				{
					Dictionary<ushort, object> dictionary = (Dictionary<ushort, object>)value;
					BinaryWriter binaryWriter = new BinaryWriter(stream);
					binaryWriter.Write(dictionary.Count);
					foreach (KeyValuePair<ushort, object> keyValuePair in dictionary)
					{
						binaryWriter.Write(keyValuePair.Key);
						IList<OdbcError> list = keyValuePair.Value as IList<OdbcError>;
						if (list == null)
						{
							binaryWriter.Write(true);
							binaryWriter.WriteObject(keyValuePair.Value);
						}
						else
						{
							binaryWriter.Write(false);
							binaryWriter.Write(list.Count);
							foreach (OdbcError odbcError in list)
							{
								binaryWriter.Write(odbcError.SQLState);
								binaryWriter.Write(odbcError.NativeError);
								binaryWriter.Write(odbcError.Message);
							}
						}
					}
					binaryWriter.Flush();
				}

				// Token: 0x04001444 RID: 5188
				private readonly StructuredCacheKey key;

				// Token: 0x04001445 RID: 5189
				protected readonly OdbcCachingService.OdbcCachingConnection connection;

				// Token: 0x04001446 RID: 5190
				private Dictionary<ushort, object> results;

				// Token: 0x04001447 RID: 5191
				private bool cacheMissed;
			}
		}
	}
}
