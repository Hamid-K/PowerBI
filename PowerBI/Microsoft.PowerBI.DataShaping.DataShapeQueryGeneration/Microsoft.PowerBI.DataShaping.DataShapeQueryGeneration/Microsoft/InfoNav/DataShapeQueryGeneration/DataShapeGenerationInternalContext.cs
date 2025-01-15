using System;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000041 RID: 65
	internal sealed class DataShapeGenerationInternalContext
	{
		// Token: 0x0600023E RID: 574 RVA: 0x0000A3F0 File Offset: 0x000085F0
		internal DataShapeGenerationInternalContext(ITracer tracer, ITelemetryService telemetryService, IFeatureSwitchProvider featureSwitchProvider, IFederatedConceptualSchema federatedConceptualSchema, IDateTimeProvider dateTimeProvider, DataShapeGenerationErrorContext errorContext, DataShapeGenerationTelemetry telemetryInfo, string topLevelDataShapeId, bool useDynamicLimits)
		{
			this.Tracer = tracer;
			this.TelemetryService = telemetryService;
			this.FeatureSwitchProvider = featureSwitchProvider;
			this.FederatedConceptualSchema = federatedConceptualSchema;
			this.DateTimeProvider = dateTimeProvider;
			this.ErrorContext = errorContext;
			this.Telemetry = telemetryInfo;
			this.TopLevelDataShapeId = topLevelDataShapeId;
			this.UseDynamicLimits = useDynamicLimits;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000A448 File Offset: 0x00008648
		internal DataShapeGenerationInternalContext(DataShapeGenerationContext externalContext, IFederatedConceptualSchema federatedConceptualSchema, DataShapeGenerationErrorContext errorContext, IDateTimeProvider dateTimeProvider)
		{
			this.Tracer = externalContext.Tracer;
			this.TelemetryService = externalContext.TelemetryService;
			this.FeatureSwitchProvider = externalContext.FeatureSwitchProvider;
			this.FederatedConceptualSchema = federatedConceptualSchema;
			this.DateTimeProvider = dateTimeProvider;
			this.ErrorContext = errorContext;
			this.Telemetry = externalContext.TelemetryInfo;
			this.TopLevelDataShapeId = externalContext.TopLevelDataShapeId;
			this.UseDynamicLimits = externalContext.UseDynamicLimits;
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000240 RID: 576 RVA: 0x0000A4B9 File Offset: 0x000086B9
		internal IDateTimeProvider DateTimeProvider { get; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000241 RID: 577 RVA: 0x0000A4C1 File Offset: 0x000086C1
		internal IFederatedConceptualSchema FederatedConceptualSchema { get; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000242 RID: 578 RVA: 0x0000A4C9 File Offset: 0x000086C9
		internal ITracer Tracer { get; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000A4D1 File Offset: 0x000086D1
		internal IFeatureSwitchProvider FeatureSwitchProvider { get; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000A4D9 File Offset: 0x000086D9
		internal DataShapeGenerationErrorContext ErrorContext { get; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0000A4E1 File Offset: 0x000086E1
		internal DataShapeGenerationTelemetry Telemetry { get; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000246 RID: 582 RVA: 0x0000A4E9 File Offset: 0x000086E9
		public string TopLevelDataShapeId { get; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000A4F1 File Offset: 0x000086F1
		public bool UseDynamicLimits { get; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000A4F9 File Offset: 0x000086F9
		internal ITelemetryService TelemetryService { get; }

		// Token: 0x06000249 RID: 585 RVA: 0x0000A504 File Offset: 0x00008704
		internal DataShapeGenerationInternalContext Clone(IFederatedConceptualSchema newFederatedConceptualSchema)
		{
			return new DataShapeGenerationInternalContext(this.Tracer, this.TelemetryService, this.FeatureSwitchProvider, newFederatedConceptualSchema, this.DateTimeProvider, this.ErrorContext, this.Telemetry, this.TopLevelDataShapeId, this.UseDynamicLimits);
		}
	}
}
