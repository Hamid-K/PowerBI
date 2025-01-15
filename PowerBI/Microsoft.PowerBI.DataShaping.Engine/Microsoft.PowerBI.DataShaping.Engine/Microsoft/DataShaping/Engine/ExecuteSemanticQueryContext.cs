using System;
using System.Threading;
using Microsoft.DataShaping.InternalContracts.DataShapeResultWriter;
using Microsoft.DataShaping.Processing;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.PowerBI.Analytics.Contracts.DaxDataTransform;
using Microsoft.ReportingServices.DataShapeQueryTranslation;

namespace Microsoft.DataShaping.Engine
{
	// Token: 0x02000010 RID: 16
	public sealed class ExecuteSemanticQueryContext
	{
		// Token: 0x06000044 RID: 68 RVA: 0x00002C80 File Offset: 0x00000E80
		public ExecuteSemanticQueryContext(HostServices hostServices, SemanticQueryDataShapeCommand command, IExecuteSemanticQueryResultWriter resultWriter, EngineDataModel dataModel, IDataShapingDataSourceInfo dataSourceInfo, IDaxDataTransformMetadataFactory transformMetadataFactory, DataReductionConfiguration dataReductionConfig, int queryId, DbCommandOptions dbCommandOptions, bool enableRemoteErrors, QueryExecutionOptions queryExecutionOptions = null, CancellationToken? cancelToken = null, ExecutionMetricsOptions executionMetricsOptions = null, bool writeDsrV1 = false)
			: this(hostServices, command, resultWriter, dataModel, dataSourceInfo, transformMetadataFactory, dataReductionConfig, queryId, "EntityDataSource", dbCommandOptions, enableRemoteErrors, true, EngineComponentsFactory.DefaultDsqTranslator, EngineComponentsFactory.DefaultProcessor, null, DataShapeQueryTranslationOptions.Default, queryExecutionOptions ?? QueryExecutionOptions.Default, cancelToken, executionMetricsOptions ?? ExecutionMetricsOptions.Default, writeDsrV1 ? DsrWriterOptions.V1 : DsrWriterOptions.V2)
		{
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002CEC File Offset: 0x00000EEC
		internal ExecuteSemanticQueryContext(HostServices hostServices, SemanticQueryDataShapeCommand command, IExecuteSemanticQueryResultWriter resultWriter, EngineDataModel dataModel, IDataShapingDataSourceInfo dataSourceInfo, IDaxDataTransformMetadataFactory transformMetadataFactory, DataReductionConfiguration dataReductionConfig, int queryId, string dataSourceId, DbCommandOptions dbCommandOptions, bool enableRemoteErrors, bool useDynamicLimits, IDataShapeQueryTranslator dataShapeQueryTranslator, IDataShapeProcessor processor, QueryPatternKind? testOnlyQueryPatternOverride, DataShapeQueryTranslationOptions dsqtOptions, QueryExecutionOptions queryExecutionOptions, CancellationToken? cancelToken, ExecutionMetricsOptions executionMetricsOptions, DsrWriterOptions dsrWriterOptions)
		{
			this.HostServices = hostServices;
			this.Command = command;
			this.ResultWriter = resultWriter;
			this.DataModel = dataModel;
			this.DataSourceInfo = dataSourceInfo;
			this.TransformMetadataFactory = transformMetadataFactory;
			this.DataReductionConfig = dataReductionConfig;
			this.QueryId = queryId;
			this.DataSourceId = dataSourceId;
			this.DbCommandOptions = dbCommandOptions;
			this.EnableRemoteErrors = enableRemoteErrors;
			this.UseDynamicLimits = useDynamicLimits;
			this.DataShapeQueryTranslator = dataShapeQueryTranslator;
			this.Processor = processor;
			this.TestOnlyQueryPatternOverride = testOnlyQueryPatternOverride;
			this.DsqtOptions = dsqtOptions;
			this.QueryExecutionOptions = queryExecutionOptions;
			this.CancelToken = cancelToken ?? CancellationToken.None;
			this.ExecutionMetricsOptions = executionMetricsOptions;
			this.DsrWriterOptions = dsrWriterOptions;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002DB4 File Offset: 0x00000FB4
		public HostServices HostServices { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002DBC File Offset: 0x00000FBC
		public SemanticQueryDataShapeCommand Command { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002DC4 File Offset: 0x00000FC4
		public IExecuteSemanticQueryResultWriter ResultWriter { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002DCC File Offset: 0x00000FCC
		public EngineDataModel DataModel { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002DD4 File Offset: 0x00000FD4
		public IDataShapingDataSourceInfo DataSourceInfo { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002DDC File Offset: 0x00000FDC
		public IDaxDataTransformMetadataFactory TransformMetadataFactory { get; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002DE4 File Offset: 0x00000FE4
		public DataReductionConfiguration DataReductionConfig { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002DEC File Offset: 0x00000FEC
		public int QueryId { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002DF4 File Offset: 0x00000FF4
		public string DataSourceId { get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002DFC File Offset: 0x00000FFC
		public DbCommandOptions DbCommandOptions { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002E04 File Offset: 0x00001004
		public bool EnableRemoteErrors { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002E0C File Offset: 0x0000100C
		public bool UseDynamicLimits { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002E14 File Offset: 0x00001014
		internal IDataShapeQueryTranslator DataShapeQueryTranslator { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002E1C File Offset: 0x0000101C
		internal IDataShapeProcessor Processor { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002E24 File Offset: 0x00001024
		internal QueryPatternKind? TestOnlyQueryPatternOverride { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002E2C File Offset: 0x0000102C
		internal DataShapeQueryTranslationOptions DsqtOptions { get; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002E34 File Offset: 0x00001034
		public QueryExecutionOptions QueryExecutionOptions { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002E3C File Offset: 0x0000103C
		public CancellationToken CancelToken { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002E44 File Offset: 0x00001044
		public ExecutionMetricsOptions ExecutionMetricsOptions { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002E4C File Offset: 0x0000104C
		internal DsrWriterOptions DsrWriterOptions { get; }
	}
}
