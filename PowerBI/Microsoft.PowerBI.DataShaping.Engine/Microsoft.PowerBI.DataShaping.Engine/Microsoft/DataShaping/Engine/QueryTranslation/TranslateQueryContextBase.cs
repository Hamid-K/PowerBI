using System;
using System.Threading;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.PowerBI.Analytics.Contracts.DaxDataTransform;
using Microsoft.ReportingServices.DataShapeQueryTranslation;

namespace Microsoft.DataShaping.Engine.QueryTranslation
{
	// Token: 0x02000022 RID: 34
	public abstract class TranslateQueryContextBase
	{
		// Token: 0x060000C9 RID: 201 RVA: 0x00003640 File Offset: 0x00001840
		internal TranslateQueryContextBase(ITracer tracer, IDumper dumper, ITelemetryService telemetryService, IFeatureSwitchProvider featureSwitchProvider, EngineDataModel dataModel, IDaxDataTransformMetadataFactory transformMetadataFactory, bool enableRemoteErrors, IDataShapeQueryTranslator dataShapeQueryTranslator, QueryPatternKind? testOnlyQueryPatternOverride, DataShapeQueryTranslationOptions dsqtOptions, int queryId, CancellationToken? cancelToken)
		{
			this.Tracer = tracer;
			this.Dumper = dumper;
			this.TelemetryService = telemetryService;
			this.FeatureSwitchProvider = featureSwitchProvider;
			this.DataModel = dataModel;
			this.TransformMetadataFactory = transformMetadataFactory;
			this.EnableRemoteErrors = enableRemoteErrors;
			this.DataShapeQueryTranslator = dataShapeQueryTranslator;
			this.TestOnlyQueryPatternOverride = testOnlyQueryPatternOverride;
			this.DsqtOptions = dsqtOptions;
			this.QueryId = queryId;
			this.CancelToken = cancelToken ?? CancellationToken.None;
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000CA RID: 202 RVA: 0x000036C8 File Offset: 0x000018C8
		public ITracer Tracer { get; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000CB RID: 203 RVA: 0x000036D0 File Offset: 0x000018D0
		public IDumper Dumper { get; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000CC RID: 204 RVA: 0x000036D8 File Offset: 0x000018D8
		public ITelemetryService TelemetryService { get; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000CD RID: 205 RVA: 0x000036E0 File Offset: 0x000018E0
		public IFeatureSwitchProvider FeatureSwitchProvider { get; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000CE RID: 206 RVA: 0x000036E8 File Offset: 0x000018E8
		public EngineDataModel DataModel { get; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000CF RID: 207 RVA: 0x000036F0 File Offset: 0x000018F0
		public IDaxDataTransformMetadataFactory TransformMetadataFactory { get; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x000036F8 File Offset: 0x000018F8
		public bool EnableRemoteErrors { get; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00003700 File Offset: 0x00001900
		internal IDataShapeQueryTranslator DataShapeQueryTranslator { get; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00003708 File Offset: 0x00001908
		internal QueryPatternKind? TestOnlyQueryPatternOverride { get; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00003710 File Offset: 0x00001910
		internal DataShapeQueryTranslationOptions DsqtOptions { get; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00003718 File Offset: 0x00001918
		public int QueryId { get; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00003720 File Offset: 0x00001920
		public CancellationToken CancelToken { get; }
	}
}
