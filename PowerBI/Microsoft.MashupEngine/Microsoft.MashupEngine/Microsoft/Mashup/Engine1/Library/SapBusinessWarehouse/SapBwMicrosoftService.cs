using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Mdx;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.SapBusinessWarehouse
{
	// Token: 0x020004C4 RID: 1220
	internal sealed class SapBwMicrosoftService : SapBwService
	{
		// Token: 0x0600280C RID: 10252 RVA: 0x00076214 File Offset: 0x00074414
		public SapBwMicrosoftService(IEngineHost host, IResource resource, SapBwOptions options, SapBwOlapDataSourceLocation location, SapBwRouterString routerString, IDbProviderFactoryService factoryService)
			: base(host, resource, options, location, routerString, "2.0")
		{
			this.helper = new SapBwModuleHelper(host, resource);
			this.sapBwProviderFactoryService = factoryService as ISapBwDbProviderFactoryService;
			if (this.sapBwProviderFactoryService != null)
			{
				this.destinationTracker = this.sapBwProviderFactoryService.GetEvaluationCleanup();
				host.QueryService<ILifetimeService>().Register(this.destinationTracker);
				this.helper.Trace("Versions", delegate(IHostTrace trace)
				{
					trace.Add("SapNetConnector", this.sapBwProviderFactoryService.SapConnectorVersion, false);
					trace.Add("SapBwProvider", this.sapBwProviderFactoryService.SapBwProviderVersion, false);
				});
			}
			factoryService = TracingDbProviderFactoryService.New("Microsoft.Mashup.SapBwProvider", "SAP Business Warehouse", host, factoryService, resource);
			DbProviderFactory dbProviderFactory = factoryService.GetProviderFactory();
			Func<IDisposable> func = base.ProcessCredentialsAndGetImpersonation();
			if (func != new Func<IDisposable>(CredentialExtensions.NullImpersonationWrapper))
			{
				this.impersonationWrapper = func;
			}
			this.providerFactory = ConnectionPoolingDbProviderFactory.New(host, "Microsoft.Mashup.SapBwProvider", dbProviderFactory);
		}

		// Token: 0x17000F9A RID: 3994
		// (get) Token: 0x0600280D RID: 10253 RVA: 0x000762E4 File Offset: 0x000744E4
		protected override ConnectionPoolingDbProviderFactory ProviderFactory
		{
			get
			{
				return this.providerFactory;
			}
		}

		// Token: 0x17000F9B RID: 3995
		// (get) Token: 0x0600280E RID: 10254 RVA: 0x00002139 File Offset: 0x00000339
		public override bool MeasuresAsDbNull
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000F9C RID: 3996
		// (get) Token: 0x0600280F RID: 10255 RVA: 0x00002105 File Offset: 0x00000305
		public override bool PreferTablesForMultipleHierarchyNodes
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000F9D RID: 3997
		// (get) Token: 0x06002810 RID: 10256 RVA: 0x000762EC File Offset: 0x000744EC
		public override bool ScaleMeasures
		{
			get
			{
				return this.options.ExecutionMode != SapBusinessWarehouseExecutionModeOption.Flattening || this.options.ScaleMeasures;
			}
		}

		// Token: 0x17000F9E RID: 3998
		// (get) Token: 0x06002811 RID: 10257 RVA: 0x00002139 File Offset: 0x00000339
		public override bool SupportsEnhancedMetadata
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000F9F RID: 3999
		// (get) Token: 0x06002812 RID: 10258 RVA: 0x0007630A File Offset: 0x0007450A
		public override bool SupportsColumnFolding
		{
			get
			{
				return this.options.ExecutionMode == SapBusinessWarehouseExecutionModeOption.BasXml || this.options.ExecutionMode == SapBusinessWarehouseExecutionModeOption.BasXmlGzip;
			}
		}

		// Token: 0x06002813 RID: 10259 RVA: 0x0007632C File Offset: 0x0007452C
		protected override DbConnection CreateConnection(bool newConnection, string connectionString)
		{
			DbConnection dbConnection = this.ProviderFactory.CreateConnection();
			dbConnection.ConnectionString = connectionString;
			if (this.impersonationWrapper != null)
			{
				ISapBwMicrosoftConnection sapBwMicrosoftConnection = DbEnvironment.GetUnwrappedConnection(ConnectionPoolingDbProviderFactory.GetUnwrappedConnection(dbConnection)) as ISapBwMicrosoftConnection;
				if (sapBwMicrosoftConnection == null)
				{
					throw new InvalidOperationException("Failed to unwrap connection for impersonation.");
				}
				sapBwMicrosoftConnection.ImpersonationWrapper = (string method) => new SapBwMicrosoftService.MultiStackDisposable(new IDisposable[]
				{
					base.Tracer.CreateTrace(method, TraceEventType.Information),
					this.impersonationWrapper()
				});
			}
			return dbConnection;
		}

		// Token: 0x06002814 RID: 10260 RVA: 0x00076384 File Offset: 0x00074584
		protected override IDbConnection CreateAndOpenConnection(bool newConnection, string connectionString)
		{
			IDbConnection dbConnection2;
			try
			{
				DbConnection dbConnection = this.CreateConnection(newConnection, connectionString);
				dbConnection.Open();
				if (this.destinationTracker != null)
				{
					this.destinationTracker.AddOrEditDestination(dbConnection.DataSource);
				}
				dbConnection2 = dbConnection;
			}
			catch (Exception ex)
			{
				Exception ex2;
				if (this.helper.TryWrapException(ex, out ex2))
				{
					throw ex2;
				}
				throw;
			}
			return dbConnection2;
		}

		// Token: 0x06002815 RID: 10261 RVA: 0x000763E4 File Offset: 0x000745E4
		public override IDataReaderWithTableSchema ExtractMetadata(string bapiName, string bapiReturnTable, SapBwRestrictions restrictions)
		{
			string traceKey = SapBwService.GetBapiCommandText(bapiName, bapiReturnTable, restrictions);
			return base.ExecuteCommand(delegate(IDbConnection connection)
			{
				IDbCommand dbCommand = connection.CreateCommand();
				dbCommand.CommandText = bapiName;
				dbCommand.CommandType = (CommandType)128;
				dbCommand.CreateParameter(ParameterDirection.Output, "TRACEKEY", traceKey);
				dbCommand.CreateParameter(ParameterDirection.Output, "HELPER", this.helper);
				dbCommand.CreateParameter(ParameterDirection.Output, "ENHANCEDMETADATA", this.SupportsEnhancedMetadata);
				dbCommand.CreateParameter(ParameterDirection.ReturnValue, "table", bapiReturnTable);
				if (restrictions != null)
				{
					foreach (KeyValuePair<string, object> keyValuePair in restrictions)
					{
						dbCommand.CreateParameter(ParameterDirection.Input, keyValuePair.Key, keyValuePair.Value);
					}
				}
				return dbCommand;
			}, traceKey, true, null);
		}

		// Token: 0x06002816 RID: 10262 RVA: 0x0007644C File Offset: 0x0007464C
		public override bool TryExtractTable(string traceInfo, SapBwMetadataAstCreator astCreator, out IDataReaderWithTableSchema reader)
		{
			string traceKey = astCreator.GenerateStatement();
			return base.TryExecuteCommand(traceInfo, delegate(IDbConnection connection)
			{
				IDbCommand dbCommand = connection.CreateCommand();
				dbCommand.CreateParameter(ParameterDirection.Output, "TRACEKEY", traceKey);
				dbCommand.CreateParameter(ParameterDirection.Output, "HELPER", this.helper);
				dbCommand.CommandType = (CommandType)256;
				astCreator.AddCommandParameters(dbCommand);
				return dbCommand;
			}, traceKey, out reader);
		}

		// Token: 0x06002817 RID: 10263 RVA: 0x00076498 File Offset: 0x00074698
		public override IDataReaderWithTableSchema ExecuteMdx(string mdx, RowRange range, bool cacheResults, bool newConnection = false, object[][] columnInfos = null, string cubeName = null)
		{
			CommandType executionMode = (CommandType)this.options.ExecutionMode;
			return this.ExecuteMdxInternal(mdx, range, cacheResults, newConnection, columnInfos, cubeName, executionMode);
		}

		// Token: 0x06002818 RID: 10264 RVA: 0x000764C4 File Offset: 0x000746C4
		internal IDataReaderWithTableSchema ExecuteMdxInternal(string mdx, RowRange range, bool cacheResults, bool newConnection, object[][] columnInfos, string cubeName, CommandType commandType)
		{
			Func<IDbConnection, IDbCommand> func = delegate(IDbConnection connection)
			{
				IDbCommand dbCommand = connection.CreateCommand();
				dbCommand.CommandText = mdx;
				dbCommand.CreateParameter(ParameterDirection.Output, "TRACEKEY", dbCommand.CommandText);
				dbCommand.CreateParameter(ParameterDirection.Output, "HELPER", this.helper);
				dbCommand.CreateParameter(ParameterDirection.Output, "SCALEMEASURES", this.ScaleMeasures);
				if (columnInfos != null)
				{
					dbCommand.CreateParameter(ParameterDirection.Output, "COLUMNINFOS", columnInfos);
				}
				if (cubeName != null)
				{
					dbCommand.CreateParameter(ParameterDirection.Output, "CUBENAME", cubeName);
				}
				dbCommand.CommandType = commandType;
				return dbCommand;
			};
			string text = "~";
			string[] array = new string[3];
			int num = 0;
			uint commandType2 = (uint)commandType;
			array[num] = commandType2.ToString(CultureInfo.InvariantCulture);
			array[1] = this.options.BatchSize.ToString(CultureInfo.InvariantCulture);
			array[2] = mdx;
			return base.ExecuteMdxCommand(func, string.Join(text, array), range, cacheResults, newConnection);
		}

		// Token: 0x06002819 RID: 10265 RVA: 0x00076560 File Offset: 0x00074760
		protected override void SetConnectionString(DbConnectionStringBuilder builder)
		{
			base.SetConnectionString(builder);
			builder["TraceEnabled"] = base.Tracer.Enabled;
			builder["VerboseEnabled"] = base.Tracer.VerboseEnabled;
			builder["BatchSize"] = this.options.BatchSize;
		}

		// Token: 0x0600281A RID: 10266 RVA: 0x000765C8 File Offset: 0x000747C8
		public override ILookup<string, SapBwVariable> GroupVariablesForAdditionalMetadata(SapBwMdxCube cube, Dictionary<string, SapBwVariable> variables)
		{
			HashSet<string> timeDimensions = new HashSet<string>(from d in cube.Dimensions.Values
				where d.DimensionType == MdxDimensionType.Time
				select d.MdxIdentifier);
			HashSet<string> variableKeysForPreLoad = new HashSet<string>(from v in variables
				where v.Value.Type == SapBwVariableType.CharacteristicValue && timeDimensions.Contains(v.Value.Dimension)
				select v.Key);
			return variables.Values.Where((SapBwVariable v) => variableKeysForPreLoad.Contains(v.MdxIdentifier)).ToLookup((SapBwVariable v) => SapBwExtensions.UnquoteIdentifier(v.Dimension));
		}

		// Token: 0x0600281B RID: 10267 RVA: 0x000766B5 File Offset: 0x000748B5
		public override bool TryGetInfoObjectsDetail(string[] infoObjects, out IDataReaderWithTableSchema reader)
		{
			return base.TryGetSchema("Metadata/INFOOBJECTS", "INFOOBJECTS", infoObjects, out reader);
		}

		// Token: 0x0600281C RID: 10268 RVA: 0x000766C9 File Offset: 0x000748C9
		public override IEnumerable<MdxCellPropertyMetadata> GetCellProperties()
		{
			if (this.options.ExecutionMode != SapBusinessWarehouseExecutionModeOption.Flattening)
			{
				yield return SapBwMicrosoftService.NewCellPropertyMetadata("FORMAT_STRING", "Format string", OleDbType.Char);
				yield return SapBwMicrosoftService.NewCellPropertyMetadata("UNIT_OF_MEASURE", "Unit of measure", OleDbType.Char);
				if (this.options.ExecutionMode != SapBusinessWarehouseExecutionModeOption.DataStream)
				{
					yield return SapBwMicrosoftService.NewCellPropertyMetadata("FORMATTED_VALUE", "Formatted Value", OleDbType.Char);
				}
				if (this.options.ExecutionMode == SapBusinessWarehouseExecutionModeOption.Multidimensional)
				{
					yield return SapBwMicrosoftService.NewCellPropertyMetadata("UNIT", "Unit", OleDbType.Char);
					yield return SapBwMicrosoftService.NewCellPropertyMetadata("CURRENCY", "Currency", OleDbType.Char);
					yield return SapBwMicrosoftService.NewCellPropertyMetadata("CELL_ORDINAL", "Cell ordinal", OleDbType.Integer);
					yield return SapBwMicrosoftService.NewCellPropertyMetadata("BACK_COLOR", "Back color", OleDbType.Char);
					yield return SapBwMicrosoftService.NewCellPropertyMetadata("FORMAT_PREFIX", "Format prefix", OleDbType.Char);
					yield return SapBwMicrosoftService.NewCellPropertyMetadata("FORMAT_SUFFIX", "Format suffix", OleDbType.Char);
					yield return SapBwMicrosoftService.NewCellPropertyMetadata("VALUE_DATA_TYPE", "Value data type", OleDbType.Char);
					yield return SapBwMicrosoftService.NewCellPropertyMetadata("FORE_COLOR", "Fore color", OleDbType.Char);
					yield return SapBwMicrosoftService.NewCellPropertyMetadata("FONT_NAME", "Font name", OleDbType.Char);
					yield return SapBwMicrosoftService.NewCellPropertyMetadata("FONT_SIZE", "Font size", OleDbType.Char);
					yield return SapBwMicrosoftService.NewCellPropertyMetadata("FONT_FLAGS", "Font flags", OleDbType.Char);
					yield return SapBwMicrosoftService.NewCellPropertyMetadata("CELL_STATUS", "Cell status", OleDbType.Char);
					yield return SapBwMicrosoftService.NewCellPropertyMetadata("UPDATEABLE", "Updateable", OleDbType.Char);
					yield return SapBwMicrosoftService.NewCellPropertyMetadata("VISIBLE", "Visible", OleDbType.Char);
					yield return SapBwMicrosoftService.NewCellPropertyMetadata("EXPRESSION", "Expression", OleDbType.Char);
				}
			}
			yield break;
		}

		// Token: 0x0600281D RID: 10269 RVA: 0x000766D9 File Offset: 0x000748D9
		private static MdxCellPropertyMetadata NewCellPropertyMetadata(string name, string caption, OleDbType type)
		{
			return new MdxCellPropertyMetadata
			{
				Name = name,
				Caption = caption,
				DataType = type
			};
		}

		// Token: 0x040010FE RID: 4350
		public const string ProviderName = "Microsoft.Mashup.SapBwProvider";

		// Token: 0x040010FF RID: 4351
		private const string HelperParameter = "HELPER";

		// Token: 0x04001100 RID: 4352
		private const string TraceKeyParameter = "TRACEKEY";

		// Token: 0x04001101 RID: 4353
		private const string EnhancedMetadataParameter = "ENHANCEDMETADATA";

		// Token: 0x04001102 RID: 4354
		private const string ColumnInfosParameter = "COLUMNINFOS";

		// Token: 0x04001103 RID: 4355
		private const string ScaleMeasuresParameter = "SCALEMEASURES";

		// Token: 0x04001104 RID: 4356
		private const string CubeNameParameter = "CUBENAME";

		// Token: 0x04001105 RID: 4357
		private readonly SapBwModuleHelper helper;

		// Token: 0x04001106 RID: 4358
		private readonly SapBwDestinationTracker destinationTracker;

		// Token: 0x04001107 RID: 4359
		private readonly ConnectionPoolingDbProviderFactory providerFactory;

		// Token: 0x04001108 RID: 4360
		private readonly Func<IDisposable> impersonationWrapper;

		// Token: 0x04001109 RID: 4361
		private readonly ISapBwDbProviderFactoryService sapBwProviderFactoryService;

		// Token: 0x020004C5 RID: 1221
		private enum SapBwCommandType
		{
			// Token: 0x0400110B RID: 4363
			Bapi = 128,
			// Token: 0x0400110C RID: 4364
			Table = 256
		}

		// Token: 0x020004C6 RID: 1222
		private class MultiStackDisposable : IDisposable
		{
			// Token: 0x06002820 RID: 10272 RVA: 0x00076750 File Offset: 0x00074950
			public MultiStackDisposable(params IDisposable[] disposablesStack)
			{
				this.disposablesStack = disposablesStack;
			}

			// Token: 0x06002821 RID: 10273 RVA: 0x00076760 File Offset: 0x00074960
			public void Dispose()
			{
				for (int i = this.disposablesStack.Length - 1; i >= 0; i--)
				{
					this.disposablesStack[i].Dispose();
				}
			}

			// Token: 0x0400110D RID: 4365
			private readonly IDisposable[] disposablesStack;
		}
	}
}
