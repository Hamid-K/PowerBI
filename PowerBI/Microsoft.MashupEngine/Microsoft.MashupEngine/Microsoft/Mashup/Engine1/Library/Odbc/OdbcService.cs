using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OleDb.Serialization;
using Microsoft.Win32;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x02000647 RID: 1607
	internal abstract class OdbcService : IOdbcService
	{
		// Token: 0x17001263 RID: 4707
		// (get) Token: 0x0600330B RID: 13067 RVA: 0x000A391E File Offset: 0x000A1B1E
		public static IOdbcService WindowsInstance
		{
			get
			{
				return OdbcService.windows.Value;
			}
		}

		// Token: 0x17001264 RID: 4708
		// (get) Token: 0x0600330C RID: 13068 RVA: 0x000A392A File Offset: 0x000A1B2A
		public static IOdbcService EmbeddedInstance
		{
			get
			{
				return OdbcService.embedded.Value;
			}
		}

		// Token: 0x17001265 RID: 4709
		// (get) Token: 0x0600330D RID: 13069 RVA: 0x000A3936 File Offset: 0x000A1B36
		public static IOdbcService ManagerPoolingEmbeddedInstance
		{
			get
			{
				return OdbcService.managerPoolingEmbedded.Value;
			}
		}

		// Token: 0x0600330E RID: 13070 RVA: 0x000A3942 File Offset: 0x000A1B42
		public OdbcService(IOdbcInterop odbcInterop, OdbcEnvironmentHandle environment)
		{
			this.odbcInterop = odbcInterop;
			this.environment = environment;
		}

		// Token: 0x17001266 RID: 4710
		// (get) Token: 0x0600330F RID: 13071 RVA: 0x000A3958 File Offset: 0x000A1B58
		public int PageSize
		{
			get
			{
				return 4096;
			}
		}

		// Token: 0x06003310 RID: 13072 RVA: 0x000A395F File Offset: 0x000A1B5F
		public IOdbcConnection CreateConnection(OdbcConnectionProperties args)
		{
			return new OdbcService.OdbcEngineConnection(this.odbcInterop, this.environment, args);
		}

		// Token: 0x06003311 RID: 13073
		public abstract IList<string> GetInstalledDrivers();

		// Token: 0x040016B8 RID: 5816
		private static readonly Lazy<IOdbcService> windows = new Lazy<IOdbcService>(new Func<IOdbcService>(OdbcService.WindowsOdbcService.New));

		// Token: 0x040016B9 RID: 5817
		private static readonly Lazy<IOdbcService> embedded = new Lazy<IOdbcService>(new Func<IOdbcService>(OdbcService.EmbeddedOdbcService.New));

		// Token: 0x040016BA RID: 5818
		private static readonly Lazy<IOdbcService> managerPoolingEmbedded = new Lazy<IOdbcService>(new Func<IOdbcService>(OdbcService.EmbeddedOdbcService.NewManagerPooling));

		// Token: 0x040016BB RID: 5819
		private const int pageSize = 4096;

		// Token: 0x040016BC RID: 5820
		private readonly IOdbcInterop odbcInterop;

		// Token: 0x040016BD RID: 5821
		private readonly OdbcEnvironmentHandle environment;

		// Token: 0x02000648 RID: 1608
		private class WindowsOdbcService : OdbcService
		{
			// Token: 0x06003313 RID: 13075 RVA: 0x000A39C3 File Offset: 0x000A1BC3
			public WindowsOdbcService(IOdbcInterop odbcInterop, OdbcEnvironmentHandle environment)
				: base(odbcInterop, environment)
			{
			}

			// Token: 0x06003314 RID: 13076 RVA: 0x000A39D0 File Offset: 0x000A1BD0
			public override IList<string> GetInstalledDrivers()
			{
				IList<string> list;
				try
				{
					using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\ODBC\\ODBCINST.INI\\ODBC Drivers", false))
					{
						if (registryKey == null)
						{
							list = EmptyArray<string>.Instance;
						}
						else
						{
							list = registryKey.GetValueNames();
						}
					}
				}
				catch (Exception ex)
				{
					if (!(ex is SecurityException) && !(ex is UnauthorizedAccessException) && !(ex is IOException))
					{
						throw;
					}
					throw ValueException.NewDataSourceError<Message2>(OdbcModule.CreateDataSourceExceptionMessage(ex.Message), Value.Null, ex);
				}
				return list;
			}

			// Token: 0x06003315 RID: 13077 RVA: 0x000A3A5C File Offset: 0x000A1C5C
			public static OdbcService New()
			{
				OdbcInterop odbcInterop = new OdbcInterop("odbc32.dll");
				OdbcEnvironmentHandle odbcEnvironmentHandle = new OdbcEnvironmentHandle(odbcInterop);
				return new OdbcService.WindowsOdbcService(odbcInterop, odbcEnvironmentHandle);
			}
		}

		// Token: 0x02000649 RID: 1609
		private class EmbeddedOdbcService : OdbcService
		{
			// Token: 0x06003316 RID: 13078 RVA: 0x000A39C3 File Offset: 0x000A1BC3
			public EmbeddedOdbcService(IOdbcInterop odbcInterop, OdbcEnvironmentHandle environment)
				: base(odbcInterop, environment)
			{
			}

			// Token: 0x06003317 RID: 13079 RVA: 0x000A3A80 File Offset: 0x000A1C80
			public override IList<string> GetInstalledDrivers()
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(OdbcService.EmbeddedOdbcService.binariesPath, "ODBC Drivers"));
				if (directoryInfo.Exists)
				{
					FileInfo[] files = directoryInfo.GetFiles("*.ini");
					string[] array = new string[files.Length];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = Path.GetFileNameWithoutExtension(files[i].Name);
					}
					return array;
				}
				return EmptyArray<string>.Instance;
			}

			// Token: 0x06003318 RID: 13080 RVA: 0x000A3AE4 File Offset: 0x000A1CE4
			public static IOdbcService New()
			{
				return OdbcService.EmbeddedOdbcService.NewService(false);
			}

			// Token: 0x06003319 RID: 13081 RVA: 0x000A3AEC File Offset: 0x000A1CEC
			public static IOdbcService NewManagerPooling()
			{
				return OdbcService.EmbeddedOdbcService.NewService(true);
			}

			// Token: 0x0600331A RID: 13082 RVA: 0x000A3AF4 File Offset: 0x000A1CF4
			private static IOdbcService NewService(bool enableDriverManagerPooling)
			{
				OdbcInterop odbcInterop = new OdbcInterop(Path.Combine(OdbcService.EmbeddedOdbcService.binariesPath, "PRIVATE_ODBC32.dll"));
				OdbcEnvironmentHandle odbcEnvironmentHandle = new OdbcEnvironmentHandle(odbcInterop, enableDriverManagerPooling);
				return new OdbcService.EmbeddedOdbcService(odbcInterop, odbcEnvironmentHandle);
			}

			// Token: 0x040016BE RID: 5822
			private static string binariesPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
		}

		// Token: 0x0200064A RID: 1610
		private class OdbcEngineConnection : IOdbcConnection, IDisposable
		{
			// Token: 0x0600331C RID: 13084 RVA: 0x000A3B39 File Offset: 0x000A1D39
			public OdbcEngineConnection(IOdbcInterop odbcInterop, OdbcEnvironmentHandle environment, OdbcConnectionProperties args)
			{
				this.odbcInterop = odbcInterop;
				this.environment = environment;
				this.args = args;
			}

			// Token: 0x0600331D RID: 13085 RVA: 0x000A3B58 File Offset: 0x000A1D58
			public void Open()
			{
				this.connection = new OdbcConnectionHandle(this.odbcInterop, this.environment, this.args.ConnectionString, this.args.ConnectionTimeout, this.args.ConnectionAttributes);
				if (this.args.Catalog != null)
				{
					this.SetCurrentCatalog(this.args.Catalog);
				}
			}

			// Token: 0x0600331E RID: 13086 RVA: 0x000A3BBC File Offset: 0x000A1DBC
			public int GetInfoInt32(Odbc32.SQL_INFO infoType)
			{
				int num;
				OdbcUtils.HandleError(this.connection, this.connection.GetInfoInt32Unhandled(infoType, out num));
				return num;
			}

			// Token: 0x0600331F RID: 13087 RVA: 0x000A3BE4 File Offset: 0x000A1DE4
			public string GetInfoString(Odbc32.SQL_INFO infoType)
			{
				return this.connection.GetInfoString(infoType);
			}

			// Token: 0x06003320 RID: 13088 RVA: 0x000A3BF4 File Offset: 0x000A1DF4
			public bool GetFunctions(Odbc32.SQL_API functionId)
			{
				short num;
				OdbcUtils.HandleError(this.connection, this.connection.GetFunctions(functionId, out num));
				return num == 1;
			}

			// Token: 0x06003321 RID: 13089 RVA: 0x000A3C20 File Offset: 0x000A1E20
			public string GetConnectAttrString(Odbc32.SQL_ATTR attribute)
			{
				string text;
				OdbcUtils.HandleError(this.connection, this.connection.GetConnectionAttribute(attribute, out text));
				return text;
			}

			// Token: 0x06003322 RID: 13090 RVA: 0x000A3C48 File Offset: 0x000A1E48
			public IDisposable UseCatalog(string catalog)
			{
				string previousCatalog = this.GetCurrentCatalog();
				if (previousCatalog == catalog)
				{
					return DisposableExtensions.Empty;
				}
				this.SetCurrentCatalog(catalog);
				if (string.IsNullOrEmpty(previousCatalog))
				{
					return DisposableExtensions.Empty;
				}
				return new ActionOnDispose(delegate
				{
					this.SetCurrentCatalog(previousCatalog);
				});
			}

			// Token: 0x06003323 RID: 13091 RVA: 0x000A3CB0 File Offset: 0x000A1EB0
			public IPageReader Execute(OdbcStatementHandle statement, IList<OdbcParameter> parameters, RowRange rowRange)
			{
				OdbcBuffer odbcBuffer;
				if (OdbcService.OdbcEngineConnection.ExecuteStatement(null, parameters, statement, out odbcBuffer))
				{
					return OdbcPageReader.New(this.connection, statement, odbcBuffer, rowRange, this.args.FetchPlanFactory);
				}
				return new EmptyPageReader(OdbcPageReader.New(this.connection, statement, odbcBuffer, rowRange, this.args.FetchPlanFactory));
			}

			// Token: 0x06003324 RID: 13092 RVA: 0x000A3D04 File Offset: 0x000A1F04
			public IPageReader ExecuteDirect(string commandText, IList<OdbcParameter> parameters, RowRange rowRange, IOdbcStatementRegistrar statementRegistrar)
			{
				OdbcStatementRegistration odbcStatementRegistration = this.NewStatement(statementRegistrar);
				OdbcStatementHandle statement = odbcStatementRegistration.Statement;
				IPageReader pageReader;
				try
				{
					OdbcBuffer odbcBuffer;
					if (OdbcService.OdbcEngineConnection.ExecuteStatement(commandText, parameters, statement, out odbcBuffer))
					{
						pageReader = OdbcPageReader.New(this.connection, statement, odbcBuffer, rowRange, this.args.FetchPlanFactory).AfterDispose(new Action(odbcStatementRegistration.Unregister));
					}
					else
					{
						pageReader = new EmptyPageReader(OdbcPageReader.New(this.connection, statement, odbcBuffer, rowRange, this.args.FetchPlanFactory)).AfterDispose(new Action(odbcStatementRegistration.Unregister));
					}
				}
				catch
				{
					odbcStatementRegistration.Unregister();
					throw;
				}
				return pageReader;
			}

			// Token: 0x06003325 RID: 13093 RVA: 0x000A3DA8 File Offset: 0x000A1FA8
			public long ExecuteNonQueryDirect(string commandText, IList<OdbcParameter> parameters, IOdbcStatementRegistrar statementRegistrar)
			{
				OdbcStatementRegistration odbcStatementRegistration = this.NewStatement(statementRegistrar);
				OdbcStatementHandle statement = odbcStatementRegistration.Statement;
				OdbcBuffer odbcBuffer = null;
				long num;
				try
				{
					if (OdbcService.OdbcEngineConnection.ExecuteStatement(commandText, parameters, statement, out odbcBuffer))
					{
						num = statement.GetRowCount();
					}
					else
					{
						num = 0L;
					}
				}
				finally
				{
					odbcStatementRegistration.Unregister();
					if (odbcBuffer != null)
					{
						odbcBuffer.Dispose();
					}
					if (statement != null)
					{
						statement.Dispose();
					}
				}
				return num;
			}

			// Token: 0x06003326 RID: 13094 RVA: 0x000A3E0C File Offset: 0x000A200C
			private static bool ExecuteStatement(string commandText, IList<OdbcParameter> parameters, OdbcStatementHandle statement, out OdbcBuffer parameterBuffer)
			{
				parameterBuffer = null;
				bool flag;
				try
				{
					if (parameters.Count > 0)
					{
						int num = 0;
						foreach (OdbcParameter odbcParameter in parameters)
						{
							odbcParameter.PrepareForBind(ref num);
						}
						parameterBuffer = new OdbcBuffer(num);
						for (int i = 0; i < parameters.Count; i++)
						{
							parameters[i].Bind(statement, (short)(i + 1), parameterBuffer);
						}
					}
					flag = ((commandText != null) ? OdbcUtils.HandleErrorCheckNoData(statement, statement.ExecuteDirect(commandText)) : OdbcUtils.HandleErrorCheckNoData(statement, statement.Execute()));
				}
				catch (Exception)
				{
					if (parameterBuffer != null)
					{
						parameterBuffer.Dispose();
					}
					if (statement != null)
					{
						statement.Dispose();
					}
					throw;
				}
				return flag;
			}

			// Token: 0x06003327 RID: 13095 RVA: 0x000A3ED4 File Offset: 0x000A20D4
			private OdbcStatementRegistration NewStatement(IOdbcStatementRegistrar registrar)
			{
				OdbcStatementHandle odbcStatementHandle = null;
				OdbcStatementRegistration odbcStatementRegistration = null;
				OdbcStatementRegistration odbcStatementRegistration2;
				try
				{
					odbcStatementHandle = new OdbcStatementHandle(this.odbcInterop, this.connection);
					odbcStatementRegistration = registrar.Register(odbcStatementHandle);
					if (this.args.CommandTimeout != null)
					{
						OdbcUtils.HandleError(odbcStatementHandle, odbcStatementHandle.SetStatementAttribute(Odbc32.SQL_ATTR.QUERY_TIMEOUT, (IntPtr)this.args.CommandTimeout.Value, (Odbc32.SQL_IS)0));
					}
					odbcStatementRegistration2 = odbcStatementRegistration;
				}
				catch (Exception)
				{
					if (odbcStatementRegistration != null)
					{
						odbcStatementRegistration.Unregister();
					}
					if (odbcStatementHandle != null)
					{
						odbcStatementHandle.Dispose();
					}
					throw;
				}
				return odbcStatementRegistration2;
			}

			// Token: 0x06003328 RID: 13096 RVA: 0x000A3F68 File Offset: 0x000A2168
			public IDataReaderWithTableSchema GetTables(string catalogName, string schemaName, string tableName, string tableType)
			{
				return this.GetMetadataDataReader((OdbcStatementHandle s) => s.Tables(catalogName, schemaName, tableName, tableType));
			}

			// Token: 0x06003329 RID: 13097 RVA: 0x000A3FAC File Offset: 0x000A21AC
			public IDataReaderWithTableSchema GetColumns(string catalogName, string schemaName, string tableName)
			{
				return this.GetMetadataDataReader((OdbcStatementHandle s) => s.Columns(catalogName, schemaName, tableName, null));
			}

			// Token: 0x0600332A RID: 13098 RVA: 0x000A3FE8 File Offset: 0x000A21E8
			public IDataReaderWithTableSchema GetPrimaryKeys(string catalogName, string schemaName, string tableName)
			{
				return this.GetMetadataDataReader((OdbcStatementHandle s) => s.PrimaryKeys(catalogName, schemaName, tableName));
			}

			// Token: 0x0600332B RID: 13099 RVA: 0x000A4024 File Offset: 0x000A2224
			public IDataReaderWithTableSchema GetBestRowId(string catalogName, string schemaName, string tableName)
			{
				return this.GetMetadataDataReader((OdbcStatementHandle s) => s.SpecialColumns(catalogName, schemaName, tableName, Odbc32.SQL_SPECIALCOLS.BEST_ROWID, Odbc32.SQL_SCOPE.SESSION, Odbc32.SQL_NULLABILITY.NO_NULLS));
			}

			// Token: 0x0600332C RID: 13100 RVA: 0x000A4060 File Offset: 0x000A2260
			public IDataReaderWithTableSchema GetForeignKeys(string pkCatalogName, string pkSchemaName, string pkTableName, string fkCatalogName, string fkSchemaName, string fkTableName)
			{
				return this.GetMetadataDataReader((OdbcStatementHandle s) => s.ForeignKeys(pkCatalogName, pkSchemaName, pkTableName, fkCatalogName, fkSchemaName, fkTableName));
			}

			// Token: 0x0600332D RID: 13101 RVA: 0x000A40B4 File Offset: 0x000A22B4
			public IDataReaderWithTableSchema GetTypeInfo(short dataType)
			{
				return this.GetMetadataDataReader((OdbcStatementHandle s) => s.GetTypeInfo(dataType));
			}

			// Token: 0x0600332E RID: 13102 RVA: 0x000A40E0 File Offset: 0x000A22E0
			private IDataReaderWithTableSchema GetMetadataDataReader(Func<OdbcStatementHandle, Odbc32.RetCode> execute)
			{
				OdbcStatementHandle odbcStatementHandle = null;
				IDataReaderWithTableSchema dataReaderWithTableSchema;
				try
				{
					odbcStatementHandle = new OdbcStatementHandle(this.odbcInterop, this.connection);
					Odbc32.RetCode retCode = execute(odbcStatementHandle);
					OdbcUtils.HandleError(odbcStatementHandle, retCode);
					dataReaderWithTableSchema = new PageReaderDataReader(OdbcPageReader.New(this.connection, odbcStatementHandle, null, RowRange.All, this.args.FetchPlanFactory), new Func<ISerializedException, Exception>(PageExceptionSerializer.GetExceptionFromProperties), new Func<ISerializedException, Exception>(PageExceptionSerializer.GetExceptionFromProperties));
				}
				catch
				{
					if (odbcStatementHandle != null)
					{
						odbcStatementHandle.Dispose();
					}
					throw;
				}
				return dataReaderWithTableSchema;
			}

			// Token: 0x0600332F RID: 13103 RVA: 0x000A416C File Offset: 0x000A236C
			public OdbcStatementRegistration Prepare(string commandText, IOdbcStatementRegistrar statementRegistrar)
			{
				OdbcStatementHandle odbcStatementHandle = null;
				OdbcStatementRegistration odbcStatementRegistration2;
				try
				{
					OdbcStatementRegistration odbcStatementRegistration = this.NewStatement(statementRegistrar);
					odbcStatementHandle = odbcStatementRegistration.Statement;
					OdbcUtils.HandleErrorCheckNoData(odbcStatementHandle, odbcStatementHandle.Prepare(commandText));
					odbcStatementRegistration2 = odbcStatementRegistration;
				}
				catch (Exception)
				{
					if (odbcStatementHandle != null)
					{
						odbcStatementHandle.Dispose();
					}
					throw;
				}
				return odbcStatementRegistration2;
			}

			// Token: 0x06003330 RID: 13104 RVA: 0x000A41B8 File Offset: 0x000A23B8
			public void Dispose()
			{
				if (this.connection != null)
				{
					SafeHandle safeHandle = this.connection;
					this.connection = null;
					safeHandle.Dispose();
				}
			}

			// Token: 0x06003331 RID: 13105 RVA: 0x000A41D4 File Offset: 0x000A23D4
			private string GetCurrentCatalog()
			{
				string text;
				OdbcUtils.HandleError(this.connection, this.connection.GetConnectionAttribute(Odbc32.SQL_ATTR.CURRENT_CATALOG, out text));
				return text;
			}

			// Token: 0x06003332 RID: 13106 RVA: 0x000A4200 File Offset: 0x000A2400
			private void SetCurrentCatalog(string catalog)
			{
				try
				{
					OdbcUtils.HandleError(this.connection, this.connection.SetConnectionAttribute3(Odbc32.SQL_ATTR.CURRENT_CATALOG, catalog, -3));
				}
				catch (OdbcException obj) when (string.IsNullOrEmpty(catalog))
				{
				}
			}

			// Token: 0x040016BF RID: 5823
			private readonly IOdbcInterop odbcInterop;

			// Token: 0x040016C0 RID: 5824
			private readonly OdbcEnvironmentHandle environment;

			// Token: 0x040016C1 RID: 5825
			private readonly OdbcConnectionProperties args;

			// Token: 0x040016C2 RID: 5826
			private OdbcConnectionHandle connection;
		}
	}
}
