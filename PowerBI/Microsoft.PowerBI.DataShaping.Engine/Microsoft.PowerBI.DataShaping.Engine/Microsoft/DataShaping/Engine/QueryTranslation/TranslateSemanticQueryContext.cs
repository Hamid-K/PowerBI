using System;
using System.Threading;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.DataShaping.ServiceContracts.QueryTranslation;
using Microsoft.InfoNav.Data.Contracts.QueryTranslation;
using Microsoft.PowerBI.Analytics.Contracts.DaxDataTransform;
using Microsoft.ReportingServices.DataShapeQueryTranslation;

namespace Microsoft.DataShaping.Engine.QueryTranslation
{
	// Token: 0x02000023 RID: 35
	public sealed class TranslateSemanticQueryContext : TranslateQueryContextBase
	{
		// Token: 0x060000D6 RID: 214 RVA: 0x00003728 File Offset: 0x00001928
		public TranslateSemanticQueryContext(ITracer tracer, IDumper dumper, ITelemetryService telemetryService, IFeatureSwitchProvider featureSwitchProvider, TranslateQueryCommand command, EngineDataModel dataModel, IDaxDataTransformMetadataFactory transformMetadataFactory, TranslateSemanticQueryConfigKind configKind, bool enableRemoteErrors, int queryId, CancellationToken? cancelToken)
			: this(tracer, dumper, telemetryService, featureSwitchProvider, command, dataModel, transformMetadataFactory, configKind, enableRemoteErrors, EngineComponentsFactory.DefaultDsqTranslator, null, DataShapeQueryTranslationOptions.Default, queryId, cancelToken)
		{
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00003764 File Offset: 0x00001964
		internal TranslateSemanticQueryContext(ITracer tracer, IDumper dumper, ITelemetryService telemetryService, IFeatureSwitchProvider featureSwitchProvider, TranslateQueryCommand command, EngineDataModel dataModel, IDaxDataTransformMetadataFactory transformMetadataFactory, TranslateSemanticQueryConfigKind configKind, bool enableRemoteErrors, IDataShapeQueryTranslator dataShapeQueryTranslator, QueryPatternKind? testOnlyQueryPatternOverride, DataShapeQueryTranslationOptions dsqtOptions, int queryId, CancellationToken? cancelToken)
			: base(tracer, dumper, telemetryService, featureSwitchProvider, dataModel, transformMetadataFactory, enableRemoteErrors, dataShapeQueryTranslator, testOnlyQueryPatternOverride, dsqtOptions, queryId, cancelToken)
		{
			this.Command = command;
			this.ConfigKind = configKind;
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x0000379C File Offset: 0x0000199C
		public TranslateQueryCommand Command { get; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x000037A4 File Offset: 0x000019A4
		// (set) Token: 0x060000DA RID: 218 RVA: 0x000037AC File Offset: 0x000019AC
		public TranslateSemanticQueryConfigKind ConfigKind { get; set; }
	}
}
