using System;
using System.Threading;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.PowerBI.Analytics.Contracts.DaxDataTransform;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation
{
	// Token: 0x02000051 RID: 81
	internal sealed class DataShapeQueryTranslationContext
	{
		// Token: 0x060003C9 RID: 969 RVA: 0x0000C9C8 File Offset: 0x0000ABC8
		internal DataShapeQueryTranslationContext(DataShape dataShape, ITracer tracer, ITelemetryService telemetryService, IFeatureSwitchProvider featureSwitchProvider, IDumper dumper, DataSourceContext dataSourceContext, QueryPatternKind? testOnlyOverrideQueryPattern, DataShapeQueryTranslationTelemetry telemetryInfo, CancellationToken cancellationToken, DataShapeQueryTranslationOptions options = null, IDaxDataTransformMetadataFactory transformMetadataFactory = null)
		{
			this.DataShape = dataShape;
			this.Tracer = tracer;
			this.TelemetryService = telemetryService;
			this.FeatureSwitchProvider = featureSwitchProvider;
			this.Dumper = dumper;
			this.DataSourceContext = dataSourceContext;
			this.TestOnlyOverrideQueryPattern = testOnlyOverrideQueryPattern;
			this.Options = options ?? DataShapeQueryTranslationOptions.DefaultWithApplyTransformsInQueryFalse;
			this.TransformMetadataFactory = transformMetadataFactory;
			this.TelemetryInfo = telemetryInfo;
			this.CancellationToken = cancellationToken;
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060003CA RID: 970 RVA: 0x0000CA39 File Offset: 0x0000AC39
		public DataSourceContext DataSourceContext { get; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060003CB RID: 971 RVA: 0x0000CA41 File Offset: 0x0000AC41
		public DataShape DataShape { get; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060003CC RID: 972 RVA: 0x0000CA49 File Offset: 0x0000AC49
		public ITracer Tracer { get; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060003CD RID: 973 RVA: 0x0000CA51 File Offset: 0x0000AC51
		public ITelemetryService TelemetryService { get; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060003CE RID: 974 RVA: 0x0000CA59 File Offset: 0x0000AC59
		internal IFeatureSwitchProvider FeatureSwitchProvider { get; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060003CF RID: 975 RVA: 0x0000CA61 File Offset: 0x0000AC61
		internal IDumper Dumper { get; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x0000CA69 File Offset: 0x0000AC69
		public QueryPatternKind? TestOnlyOverrideQueryPattern { get; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x0000CA71 File Offset: 0x0000AC71
		public DataShapeQueryTranslationOptions Options { get; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x0000CA79 File Offset: 0x0000AC79
		public IDaxDataTransformMetadataFactory TransformMetadataFactory { get; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x0000CA81 File Offset: 0x0000AC81
		public DataShapeQueryTranslationTelemetry TelemetryInfo { get; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x0000CA89 File Offset: 0x0000AC89
		public CancellationToken CancellationToken { get; }

		// Token: 0x060003D5 RID: 981 RVA: 0x0000CA94 File Offset: 0x0000AC94
		public DataShapeQueryTranslationContext CloneWithOverrides(CancellationToken? newCancelToken)
		{
			return new DataShapeQueryTranslationContext(this.DataShape, this.Tracer, this.TelemetryService, this.FeatureSwitchProvider, this.Dumper, this.DataSourceContext, this.TestOnlyOverrideQueryPattern, this.TelemetryInfo, newCancelToken ?? this.CancellationToken, this.Options, this.TransformMetadataFactory);
		}
	}
}
