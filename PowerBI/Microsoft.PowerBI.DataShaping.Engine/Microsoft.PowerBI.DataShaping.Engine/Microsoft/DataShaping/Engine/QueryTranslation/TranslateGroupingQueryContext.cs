using System;
using System.Threading;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.PowerBI.Analytics.Contracts.DaxDataTransform;
using Microsoft.ReportingServices.DataShapeQueryTranslation;

namespace Microsoft.DataShaping.Engine.QueryTranslation
{
	// Token: 0x02000021 RID: 33
	public sealed class TranslateGroupingQueryContext : TranslateQueryContextBase
	{
		// Token: 0x060000C6 RID: 198 RVA: 0x000035D0 File Offset: 0x000017D0
		public TranslateGroupingQueryContext(ITracer tracer, IDumper dumper, ITelemetryService telemetryService, IFeatureSwitchProvider featureSwitchProvider, TranslateGroupingQueryCommand command, EngineDataModel dataModel, IDaxDataTransformMetadataFactory transformMetadataFactory, bool enableRemoteErrors, CancellationToken? cancelToken)
			: this(tracer, dumper, telemetryService, featureSwitchProvider, command, dataModel, transformMetadataFactory, enableRemoteErrors, EngineComponentsFactory.DefaultDsqTranslator, null, DataShapeQueryTranslationOptions.Default, 0, cancelToken)
		{
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003608 File Offset: 0x00001808
		internal TranslateGroupingQueryContext(ITracer tracer, IDumper dumper, ITelemetryService telemetryService, IFeatureSwitchProvider featureSwitchProvider, TranslateGroupingQueryCommand command, EngineDataModel dataModel, IDaxDataTransformMetadataFactory transformMetadataFactory, bool enableRemoteErrors, IDataShapeQueryTranslator dataShapeQueryTranslator, QueryPatternKind? testOnlyQueryPatternOverride, DataShapeQueryTranslationOptions dsqtOptions, int queryId, CancellationToken? cancelToken)
			: base(tracer, dumper, telemetryService, featureSwitchProvider, dataModel, transformMetadataFactory, enableRemoteErrors, dataShapeQueryTranslator, testOnlyQueryPatternOverride, dsqtOptions, queryId, cancelToken)
		{
			this.Command = command;
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00003638 File Offset: 0x00001838
		public TranslateGroupingQueryCommand Command { get; }
	}
}
