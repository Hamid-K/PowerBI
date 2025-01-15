using System;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition;
using Microsoft.DataShaping.InternalContracts.DataShapeResultWriter;
using Microsoft.DataShaping.InternalContracts.ExecutionMetadata;
using Microsoft.DataShaping.InternalContracts.QueryExecution;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.PowerBI.Analytics.Contracts;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;

namespace Microsoft.DataShaping.Processing
{
	// Token: 0x02000007 RID: 7
	internal sealed class DataShapeProcessorContext
	{
		// Token: 0x0600000A RID: 10 RVA: 0x00002260 File Offset: 0x00000460
		internal DataShapeProcessorContext(Microsoft.DataShaping.ServiceContracts.ITelemetryService telemetryService, Microsoft.DataShaping.ServiceContracts.ITracer tracer, DataShapeDefinition dsd, IStreamingStructureEncodedWriter writer, IConnectionFactory connectionFactory, IDataShapingDataSourceInfo dataSourceInfo, QueryCommandOptions commandOptions, IConnectionPool connectionPool, IConnectionStringResolver connectionStringResolver, QueryPatternKind patternKind, IDataTransformPluginFactory transformFactory, IConnectionUserImpersonator connectionUserImpersonator, IFeatureSwitchProvider featureSwitchProvider, ProcessingTelemetry telemetryInfo, QueryExecutionOptions queryExecutionOptions, IDataShapingExecutionMetricsService executionMetricsService, DsrWriterOptions dsrWriterOptions)
		{
			this.TelemetryService = telemetryService;
			this.Tracer = tracer;
			this.Dsd = dsd;
			this.Writer = writer;
			this.ConnectionFactory = connectionFactory;
			this.DataSourceInfo = dataSourceInfo;
			this.CommandOptions = commandOptions;
			this.ConnectionPool = connectionPool;
			this.ConnectionStringResolver = connectionStringResolver;
			this.PatternKind = patternKind;
			this.TransformFactory = transformFactory;
			this.ConnectionUserImpersonator = connectionUserImpersonator;
			this.FeatureSwitchProvider = featureSwitchProvider;
			this.TelemetryInfo = telemetryInfo;
			this.QueryExecutionOptions = queryExecutionOptions;
			this.ExecutionMetricsService = executionMetricsService;
			this.DsrWriterOptions = dsrWriterOptions;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000022F8 File Offset: 0x000004F8
		public IFeatureSwitchProvider FeatureSwitchProvider { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002300 File Offset: 0x00000500
		internal Microsoft.DataShaping.ServiceContracts.ITelemetryService TelemetryService { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002308 File Offset: 0x00000508
		internal Microsoft.DataShaping.ServiceContracts.ITracer Tracer { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002310 File Offset: 0x00000510
		internal DataShapeDefinition Dsd { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002318 File Offset: 0x00000518
		internal IStreamingStructureEncodedWriter Writer { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002320 File Offset: 0x00000520
		internal IConnectionFactory ConnectionFactory { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002328 File Offset: 0x00000528
		internal IDataShapingDataSourceInfo DataSourceInfo { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002330 File Offset: 0x00000530
		internal QueryCommandOptions CommandOptions { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002338 File Offset: 0x00000538
		internal IConnectionPool ConnectionPool { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002340 File Offset: 0x00000540
		internal IConnectionStringResolver ConnectionStringResolver { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002348 File Offset: 0x00000548
		internal QueryPatternKind PatternKind { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002350 File Offset: 0x00000550
		internal IDataTransformPluginFactory TransformFactory { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002358 File Offset: 0x00000558
		internal IConnectionUserImpersonator ConnectionUserImpersonator { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002360 File Offset: 0x00000560
		internal ProcessingTelemetry TelemetryInfo { get; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002368 File Offset: 0x00000568
		internal QueryExecutionOptions QueryExecutionOptions { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002370 File Offset: 0x00000570
		internal IDataShapingExecutionMetricsService ExecutionMetricsService { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002378 File Offset: 0x00000578
		public DsrWriterOptions DsrWriterOptions { get; }
	}
}
