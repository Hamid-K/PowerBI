using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x02000672 RID: 1650
	internal class OdbcTracingService : OdbcDelegatingService
	{
		// Token: 0x060033E9 RID: 13289 RVA: 0x000A6530 File Offset: 0x000A4730
		public static IOdbcService New(IEngineHost host, Tracer tracer, IOdbcService service, Action<IHostTrace> additionalTraces = null)
		{
			if (TracingService.GetService(host).Enabled())
			{
				return new OdbcTracingService(tracer, service, additionalTraces);
			}
			return new OdbcTracingService.OdbcPerformanceTracingService(tracer, service);
		}

		// Token: 0x060033EA RID: 13290 RVA: 0x000A654F File Offset: 0x000A474F
		private OdbcTracingService(Tracer tracer, IOdbcService service, Action<IHostTrace> additionalTraces)
			: base(service)
		{
			this.tracer = tracer;
			this.additionalTraces = additionalTraces;
		}

		// Token: 0x060033EB RID: 13291 RVA: 0x000A6566 File Offset: 0x000A4766
		public override IOdbcConnection CreateConnection(OdbcConnectionProperties args)
		{
			return new OdbcTracingService.OdbcTracingConnection(this.tracer, base.CreateConnection(args), args.Catalog, this.additionalTraces);
		}

		// Token: 0x060033EC RID: 13292 RVA: 0x000A6586 File Offset: 0x000A4786
		public override IList<string> GetInstalledDrivers()
		{
			return this.tracer.Trace<IList<string>>("Service/GetInstalledDrivers", delegate(IHostTrace trace)
			{
				IList<string> installedDrivers = base.GetInstalledDrivers();
				trace.AddArray("Drivers", installedDrivers, false);
				return installedDrivers;
			});
		}

		// Token: 0x04001730 RID: 5936
		private static HashSet<Odbc32.SQL_INFO> SqlInfoNonPiiStrings = new HashSet<Odbc32.SQL_INFO>
		{
			Odbc32.SQL_INFO.SQL_DRIVER_NAME,
			Odbc32.SQL_INFO.SQL_DRIVER_VER,
			Odbc32.SQL_INFO.SQL_DBMS_NAME,
			Odbc32.SQL_INFO.SQL_DBMS_VER,
			Odbc32.SQL_INFO.SQL_IDENTIFIER_QUOTE_CHAR,
			Odbc32.SQL_INFO.SQL_DRIVER_ODBC_VER,
			Odbc32.SQL_INFO.SQL_SEARCH_PATTERN_ESCAPE,
			Odbc32.SQL_INFO.SQL_ORDER_BY_COLUMNS_IN_SELECT,
			Odbc32.SQL_INFO.SQL_COLUMN_ALIAS,
			Odbc32.SQL_INFO.SQL_CATALOG_NAME,
			Odbc32.SQL_INFO.SQL_CATALOG_TERM,
			Odbc32.SQL_INFO.SQL_SCHEMA_TERM,
			Odbc32.SQL_INFO.SQL_CATALOG_NAME_SEPARATOR,
			Odbc32.SQL_INFO.SQL_SPECIAL_CHARACTERS
		};

		// Token: 0x04001731 RID: 5937
		private readonly Tracer tracer;

		// Token: 0x04001732 RID: 5938
		private readonly Action<IHostTrace> additionalTraces;

		// Token: 0x02000673 RID: 1651
		private class OdbcTracingConnection : OdbcDelegatingConnection
		{
			// Token: 0x060033EF RID: 13295 RVA: 0x000A665E File Offset: 0x000A485E
			public OdbcTracingConnection(Tracer tracing, IOdbcConnection connection, string catalog, Action<IHostTrace> additionalTraces)
				: base(connection)
			{
				this.tracing = tracing;
				this.additionalTraces = additionalTraces;
				this.currentCatalog = catalog;
			}

			// Token: 0x17001293 RID: 4755
			// (get) Token: 0x060033F0 RID: 13296 RVA: 0x000A667D File Offset: 0x000A487D
			private IHostTrace CurrentTrace
			{
				get
				{
					if (this.currentTrace == null)
					{
						this.currentTrace = this.tracing.CreateTrace("Connection/DriverInfo", TraceEventType.Information);
					}
					return this.currentTrace;
				}
			}

			// Token: 0x060033F1 RID: 13297 RVA: 0x000A66A4 File Offset: 0x000A48A4
			public override void Open()
			{
				this.tracing.Trace("Connection/Open", delegate(IHostTrace trace)
				{
					trace.Add("Catalog", this.currentCatalog, true);
					if (this.additionalTraces != null)
					{
						this.additionalTraces(trace);
					}
					base.Open();
					this.TraceDriverDetails(trace);
				});
			}

			// Token: 0x060033F2 RID: 13298 RVA: 0x000A66C4 File Offset: 0x000A48C4
			private void TraceDriverDetails(IHostTrace trace)
			{
				try
				{
					string infoString = base.GetInfoString(Odbc32.SQL_INFO.SQL_DRIVER_NAME);
					string infoString2 = base.GetInfoString(Odbc32.SQL_INFO.SQL_DRIVER_VER);
					string infoString3 = base.GetInfoString(Odbc32.SQL_INFO.SQL_DBMS_NAME);
					string infoString4 = base.GetInfoString(Odbc32.SQL_INFO.SQL_DBMS_VER);
					trace.Add("DriverName", infoString, false);
					trace.Add("DriverVersion", infoString2, false);
					trace.Add("DBMSName", infoString3, false);
					trace.Add("DBMSVersion", infoString4, false);
					this.tracing.LogFeature(string.Concat(new string[] { "Odbc.DataSource/", infoString, "/", infoString2, "/", infoString3, "/", infoString4 }));
				}
				catch (OdbcException ex)
				{
					if (!ex.IsNonTransient)
					{
						throw;
					}
				}
			}

			// Token: 0x060033F3 RID: 13299 RVA: 0x000A678C File Offset: 0x000A498C
			public override int GetInfoInt32(Odbc32.SQL_INFO infoType)
			{
				string name = OdbcTracingService.OdbcTracingConnection.GetName(infoType);
				int num;
				try
				{
					int infoInt = base.GetInfoInt32(infoType);
					this.CurrentTrace.Add(name, infoInt, false);
					num = infoInt;
				}
				catch (Exception ex) when (Microsoft.Mashup.Common.SafeExceptions.IsSafeException(ex))
				{
					if (ex is OdbcException && ((OdbcException)ex).IsSafe)
					{
						this.CurrentTrace.Add(name, (ex.Message.IndexOf("HY096", StringComparison.OrdinalIgnoreCase) >= 0) ? "(not supported)" : ex.Message, false);
					}
					else
					{
						this.FlushTrace();
						using (IHostTrace hostTrace = this.tracing.CreateTrace("Connection/GetInfoInt32", TraceEventType.Information))
						{
							hostTrace.Add("InfoType", name, false);
							hostTrace.Add(ex, true);
						}
					}
					throw;
				}
				return num;
			}

			// Token: 0x060033F4 RID: 13300 RVA: 0x000A6880 File Offset: 0x000A4A80
			public override string GetInfoString(Odbc32.SQL_INFO infoType)
			{
				string name = OdbcTracingService.OdbcTracingConnection.GetName(infoType);
				string text;
				try
				{
					string infoString = base.GetInfoString(infoType);
					this.CurrentTrace.Add(name, infoString, !OdbcTracingService.SqlInfoNonPiiStrings.Contains(infoType));
					text = infoString;
				}
				catch (Exception ex) when (Microsoft.Mashup.Common.SafeExceptions.IsSafeException(ex))
				{
					if (ex is OdbcException && ((OdbcException)ex).IsSafe)
					{
						this.CurrentTrace.Add(name, ex.Message, false);
					}
					else
					{
						this.FlushTrace();
						using (IHostTrace hostTrace = this.tracing.CreateTrace("Connection/GetInfoString", TraceEventType.Information))
						{
							hostTrace.Add("InfoType", name, false);
							hostTrace.Add(ex, true);
						}
					}
					throw;
				}
				return text;
			}

			// Token: 0x060033F5 RID: 13301 RVA: 0x000A6960 File Offset: 0x000A4B60
			public override bool GetFunctions(Odbc32.SQL_API functionId)
			{
				string name = OdbcTracingService.OdbcTracingConnection.GetName(functionId);
				bool flag;
				try
				{
					bool functions = base.GetFunctions(functionId);
					this.CurrentTrace.Add(name, functions ? "True" : "False", false);
					flag = functions;
				}
				catch (Exception ex) when (Microsoft.Mashup.Common.SafeExceptions.IsSafeException(ex))
				{
					if (ex is OdbcException && ((OdbcException)ex).IsSafe)
					{
						this.CurrentTrace.Add(name, ex.Message, false);
					}
					else
					{
						this.FlushTrace();
						using (IHostTrace hostTrace = this.tracing.CreateTrace("Connection/GetFunctions", TraceEventType.Information))
						{
							hostTrace.Add("FunctionId", name, false);
							hostTrace.Add(ex, true);
						}
					}
					throw;
				}
				return flag;
			}

			// Token: 0x060033F6 RID: 13302 RVA: 0x000A6A40 File Offset: 0x000A4C40
			public override IDisposable UseCatalog(string catalog)
			{
				string previousCatalog = this.currentCatalog;
				IDisposable disposable2;
				using (IHostTrace trace = this.tracing.CreateTrace("Connection/UseCatalog", TraceEventType.Information))
				{
					trace.Add("Previous", previousCatalog, true);
					trace.Add("New", catalog, true);
					IDisposable disposable = base.UseCatalog(catalog);
					this.currentCatalog = catalog;
					disposable2 = disposable.AfterDispose(delegate
					{
						using (this.tracing.CreateTrace("Connection/UseCatalog/Dispose", TraceEventType.Information))
						{
							trace.Add("Previous", catalog, true);
							trace.Add("New", previousCatalog, true);
						}
					});
				}
				return disposable2;
			}

			// Token: 0x060033F7 RID: 13303 RVA: 0x000A6B04 File Offset: 0x000A4D04
			public override OdbcStatementRegistration Prepare(string commandText, IOdbcStatementRegistrar statementRegistrar)
			{
				return this.tracing.TracePerformance<OdbcStatementRegistration>("Command/Prepare", delegate(IHostTrace trace)
				{
					trace.Add("CommandText", commandText, true);
					OdbcStatementRegistration odbcStatementRegistration = this.<>n__0(commandText, statementRegistrar);
					trace.Add("Statement", odbcStatementRegistration.Statement, false);
					return odbcStatementRegistration;
				});
			}

			// Token: 0x060033F8 RID: 13304 RVA: 0x000A6B48 File Offset: 0x000A4D48
			public override IPageReader Execute(OdbcStatementHandle statement, IList<OdbcParameter> parameters, RowRange rowRange)
			{
				IPageReader pageReader;
				using (SplitsTrace splitsTrace = this.tracing.CreatePerformanceTrace("Command/Execute", TraceEventType.Information).CreateSplits())
				{
					using (IHostTrace hostTrace = splitsTrace.CreateSplit("Execution"))
					{
						hostTrace.Add("Statement", statement, false);
						hostTrace.Add("ParameterCount", parameters.Count, false);
						hostTrace.Add("Skip", rowRange.SkipCount, false);
						hostTrace.Add("Take", rowRange.TakeCount, false);
						pageReader = base.Execute(statement, parameters, rowRange).TraceToAndDispose(splitsTrace.CreateSplit("Result"));
					}
				}
				return pageReader;
			}

			// Token: 0x060033F9 RID: 13305 RVA: 0x000A6C1C File Offset: 0x000A4E1C
			public override IPageReader ExecuteDirect(string commandText, IList<OdbcParameter> parameters, RowRange rowRange, IOdbcStatementRegistrar statementRegistrar)
			{
				IPageReader pageReader;
				using (SplitsTrace splitsTrace = this.tracing.CreatePerformanceTrace("Command/ExecuteDirect", TraceEventType.Information).CreateSplits())
				{
					using (IHostTrace hostTrace = splitsTrace.CreateSplit("Execution"))
					{
						hostTrace.Add("CommandText", commandText, true);
						hostTrace.Add("ParameterCount", parameters.Count, false);
						hostTrace.Add("Skip", rowRange.SkipCount, false);
						hostTrace.Add("Take", rowRange.TakeCount, false);
						pageReader = base.ExecuteDirect(commandText, parameters, rowRange, statementRegistrar).TraceToAndDispose(splitsTrace.CreateSplit("Result"));
					}
				}
				return pageReader;
			}

			// Token: 0x060033FA RID: 13306 RVA: 0x000A6CF0 File Offset: 0x000A4EF0
			public override IDataReaderWithTableSchema GetTables(string catalogName, string schemaName, string tableName, string tableType)
			{
				IDataReaderWithTableSchema dataReaderWithTableSchema;
				using (SplitsTrace splitsTrace = this.tracing.CreatePerformanceTrace("Command/GetTables", TraceEventType.Information).CreateSplits())
				{
					using (IHostTrace hostTrace = splitsTrace.CreateSplit("Execution"))
					{
						hostTrace.Add("CatalogName", catalogName, true);
						hostTrace.Add("SchemaName", schemaName, true);
						hostTrace.Add("TableName", tableName, true);
						hostTrace.Add("TableType", tableType, false);
						IDataReaderWithTableSchema tables = base.GetTables(catalogName, schemaName, tableName, tableType);
						OdbcTracingService.OdbcTracingConnection.TraceColumnNames(hostTrace, tables, false);
						dataReaderWithTableSchema = tables.AfterDispose(new Action(splitsTrace.CreateSplit("Result").Dispose));
					}
				}
				return dataReaderWithTableSchema;
			}

			// Token: 0x060033FB RID: 13307 RVA: 0x000A6DBC File Offset: 0x000A4FBC
			public override IDataReaderWithTableSchema GetColumns(string catalogName, string schemaName, string tableName)
			{
				IDataReaderWithTableSchema dataReaderWithTableSchema;
				using (SplitsTrace splitsTrace = this.tracing.CreatePerformanceTrace("Command/GetColumns", TraceEventType.Information).CreateSplits())
				{
					using (IHostTrace hostTrace = splitsTrace.CreateSplit("Execution"))
					{
						hostTrace.Add("CatalogName", catalogName, true);
						hostTrace.Add("SchemaName", schemaName, true);
						hostTrace.Add("TableName", tableName, true);
						IDataReaderWithTableSchema columns = base.GetColumns(catalogName, schemaName, tableName);
						OdbcTracingService.OdbcTracingConnection.TraceColumnNames(hostTrace, columns, false);
						dataReaderWithTableSchema = columns.AfterDispose(new Action(splitsTrace.CreateSplit("Result").Dispose));
					}
				}
				return dataReaderWithTableSchema;
			}

			// Token: 0x060033FC RID: 13308 RVA: 0x000A6E78 File Offset: 0x000A5078
			public override IDataReaderWithTableSchema GetTypeInfo(short dataType)
			{
				IDataReaderWithTableSchema dataReaderWithTableSchema;
				using (SplitsTrace splitsTrace = this.tracing.CreatePerformanceTrace("Command/GetTypeInfo", TraceEventType.Information).CreateSplits())
				{
					using (IHostTrace hostTrace = splitsTrace.CreateSplit("Execution"))
					{
						hostTrace.Add("DataType", dataType, false);
						IDataReaderWithTableSchema typeInfo = base.GetTypeInfo(dataType);
						OdbcTracingService.OdbcTracingConnection.TraceColumnNames(hostTrace, typeInfo, false);
						dataReaderWithTableSchema = typeInfo.AfterDispose(new Action(splitsTrace.CreateSplit("Result").Dispose));
					}
				}
				return dataReaderWithTableSchema;
			}

			// Token: 0x060033FD RID: 13309 RVA: 0x000A6F1C File Offset: 0x000A511C
			public override IDataReaderWithTableSchema GetPrimaryKeys(string catalogName, string schemaName, string tableName)
			{
				IDataReaderWithTableSchema dataReaderWithTableSchema;
				using (SplitsTrace splitsTrace = this.tracing.CreatePerformanceTrace("Command/GetPrimaryKeys", TraceEventType.Information).CreateSplits())
				{
					using (IHostTrace hostTrace = splitsTrace.CreateSplit("Execution"))
					{
						hostTrace.Add("CatalogName", catalogName, true);
						hostTrace.Add("SchemaName", schemaName, true);
						hostTrace.Add("TableName", tableName, true);
						IDataReaderWithTableSchema primaryKeys = base.GetPrimaryKeys(catalogName, schemaName, tableName);
						OdbcTracingService.OdbcTracingConnection.TraceColumnNames(hostTrace, primaryKeys, false);
						dataReaderWithTableSchema = primaryKeys.AfterDispose(new Action(splitsTrace.CreateSplit("Result").Dispose));
					}
				}
				return dataReaderWithTableSchema;
			}

			// Token: 0x060033FE RID: 13310 RVA: 0x000A6FD8 File Offset: 0x000A51D8
			public override IDataReaderWithTableSchema GetForeignKeys(string pkCatalogName, string pkSchemaName, string pkTableName, string fkCatalogName, string fkSchemaName, string fkTableName)
			{
				IDataReaderWithTableSchema dataReaderWithTableSchema;
				using (SplitsTrace splitsTrace = this.tracing.CreatePerformanceTrace("Command/GetForeignKeys", TraceEventType.Information).CreateSplits())
				{
					using (IHostTrace hostTrace = splitsTrace.CreateSplit("Execution"))
					{
						hostTrace.Add("PKCatalogName", pkCatalogName, true);
						hostTrace.Add("PKSchemaName", pkSchemaName, true);
						hostTrace.Add("PKTableName", pkTableName, true);
						hostTrace.Add("FKCatalogName", fkCatalogName, true);
						hostTrace.Add("FKSchemaName", fkSchemaName, true);
						hostTrace.Add("FKTableName", fkTableName, true);
						IDataReaderWithTableSchema foreignKeys = base.GetForeignKeys(pkCatalogName, pkSchemaName, pkTableName, fkCatalogName, fkSchemaName, fkTableName);
						OdbcTracingService.OdbcTracingConnection.TraceColumnNames(hostTrace, foreignKeys, false);
						dataReaderWithTableSchema = foreignKeys.AfterDispose(new Action(splitsTrace.CreateSplit("Result").Dispose));
					}
				}
				return dataReaderWithTableSchema;
			}

			// Token: 0x060033FF RID: 13311 RVA: 0x000A70C4 File Offset: 0x000A52C4
			public override IDataReaderWithTableSchema GetBestRowId(string catalogName, string schemaName, string tableName)
			{
				IDataReaderWithTableSchema dataReaderWithTableSchema;
				using (SplitsTrace splitsTrace = this.tracing.CreatePerformanceTrace("Command/GetBestRowId", TraceEventType.Information).CreateSplits())
				{
					using (IHostTrace hostTrace = splitsTrace.CreateSplit("Execution"))
					{
						hostTrace.Add("CatalogName", catalogName, true);
						hostTrace.Add("SchemaName", schemaName, true);
						hostTrace.Add("TableName", tableName, true);
						IDataReaderWithTableSchema bestRowId = base.GetBestRowId(catalogName, schemaName, tableName);
						OdbcTracingService.OdbcTracingConnection.TraceColumnNames(hostTrace, bestRowId, true);
						dataReaderWithTableSchema = bestRowId.AfterDispose(new Action(splitsTrace.CreateSplit("Result").Dispose));
					}
				}
				return dataReaderWithTableSchema;
			}

			// Token: 0x06003400 RID: 13312 RVA: 0x000A7180 File Offset: 0x000A5380
			public override void Dispose()
			{
				this.FlushTrace();
				this.tracing.Trace("Connection/Dispose", delegate(IHostTrace trace)
				{
					base.Dispose();
				});
			}

			// Token: 0x06003401 RID: 13313 RVA: 0x000A71A4 File Offset: 0x000A53A4
			private void FlushTrace()
			{
				if (this.currentTrace != null)
				{
					this.currentTrace.Dispose();
					this.currentTrace = null;
				}
			}

			// Token: 0x06003402 RID: 13314 RVA: 0x000A71C0 File Offset: 0x000A53C0
			private static string GetName(Odbc32.SQL_INFO infoType)
			{
				string text;
				if (!OdbcTracingService.OdbcTracingConnection.infoNames.TryGetValue(infoType, out text))
				{
					text = infoType.ToString();
					OdbcTracingService.OdbcTracingConnection.infoNames.Add(infoType, text);
				}
				return text;
			}

			// Token: 0x06003403 RID: 13315 RVA: 0x000A71F8 File Offset: 0x000A53F8
			private static string GetName(Odbc32.SQL_API functionId)
			{
				string text;
				if (!OdbcTracingService.OdbcTracingConnection.functionNames.TryGetValue(functionId, out text))
				{
					text = functionId.ToString();
					OdbcTracingService.OdbcTracingConnection.functionNames.Add(functionId, text);
				}
				return text;
			}

			// Token: 0x06003404 RID: 13316 RVA: 0x000A7230 File Offset: 0x000A5430
			private static void TraceColumnNames(IHostTrace trace, IDataReader reader, bool isPii)
			{
				if (trace.VerboseEnabled())
				{
					int fieldCount = reader.FieldCount;
					string[] array = new string[fieldCount];
					for (int i = 0; i < fieldCount; i++)
					{
						array[i] = reader.GetName(i);
					}
					trace.AddArray("ColumnNames", array, isPii);
				}
			}

			// Token: 0x04001733 RID: 5939
			private static readonly Dictionary<Odbc32.SQL_INFO, string> infoNames = new Dictionary<Odbc32.SQL_INFO, string>();

			// Token: 0x04001734 RID: 5940
			private static readonly Dictionary<Odbc32.SQL_API, string> functionNames = new Dictionary<Odbc32.SQL_API, string>();

			// Token: 0x04001735 RID: 5941
			private readonly Tracer tracing;

			// Token: 0x04001736 RID: 5942
			private readonly Action<IHostTrace> additionalTraces;

			// Token: 0x04001737 RID: 5943
			private string currentCatalog;

			// Token: 0x04001738 RID: 5944
			private IHostTrace currentTrace;
		}

		// Token: 0x02000676 RID: 1654
		private class OdbcPerformanceTracingService : OdbcDelegatingService
		{
			// Token: 0x0600340D RID: 13325 RVA: 0x000A738E File Offset: 0x000A558E
			public OdbcPerformanceTracingService(Tracer tracer, IOdbcService service)
				: base(service)
			{
				this.tracer = tracer;
			}

			// Token: 0x0600340E RID: 13326 RVA: 0x000A739E File Offset: 0x000A559E
			public override IOdbcConnection CreateConnection(OdbcConnectionProperties args)
			{
				return new OdbcTracingService.OdbcPerformanceTracingService.OdbcPerformanceTracingConnection(this.tracer, base.CreateConnection(args));
			}

			// Token: 0x04001740 RID: 5952
			private readonly Tracer tracer;

			// Token: 0x02000677 RID: 1655
			private class OdbcPerformanceTracingConnection : OdbcDelegatingConnection
			{
				// Token: 0x0600340F RID: 13327 RVA: 0x000A73B2 File Offset: 0x000A55B2
				public OdbcPerformanceTracingConnection(Tracer tracing, IOdbcConnection connection)
					: base(connection)
				{
					this.tracing = tracing;
				}

				// Token: 0x06003410 RID: 13328 RVA: 0x000A73C4 File Offset: 0x000A55C4
				public override OdbcStatementRegistration Prepare(string commandText, IOdbcStatementRegistrar statementRegistrar)
				{
					return this.tracing.TracePerformance<OdbcStatementRegistration>("Command/Prepare", (IHostTrace trace) => this.<>n__0(commandText, statementRegistrar));
				}

				// Token: 0x06003411 RID: 13329 RVA: 0x000A7408 File Offset: 0x000A5608
				public override IPageReader Execute(OdbcStatementHandle statement, IList<OdbcParameter> parameters, RowRange rowRange)
				{
					IPageReader pageReader;
					using (SplitsTrace splitsTrace = this.tracing.CreatePerformanceTrace("Command/Execute", TraceEventType.Information).CreateSplits())
					{
						using (splitsTrace.CreateSplit("Execution"))
						{
							pageReader = base.Execute(statement, parameters, rowRange).TraceToAndDispose(splitsTrace.CreateSplit("Result"));
						}
					}
					return pageReader;
				}

				// Token: 0x06003412 RID: 13330 RVA: 0x000A7488 File Offset: 0x000A5688
				public override IPageReader ExecuteDirect(string commandText, IList<OdbcParameter> parameters, RowRange rowRange, IOdbcStatementRegistrar statementRegistrar)
				{
					IPageReader pageReader;
					using (SplitsTrace splitsTrace = this.tracing.CreatePerformanceTrace("Command/ExecuteDirect", TraceEventType.Information).CreateSplits())
					{
						using (splitsTrace.CreateSplit("Execution"))
						{
							pageReader = base.ExecuteDirect(commandText, parameters, rowRange, statementRegistrar).TraceToAndDispose(splitsTrace.CreateSplit("Result"));
						}
					}
					return pageReader;
				}

				// Token: 0x04001741 RID: 5953
				private readonly Tracer tracing;
			}
		}
	}
}
