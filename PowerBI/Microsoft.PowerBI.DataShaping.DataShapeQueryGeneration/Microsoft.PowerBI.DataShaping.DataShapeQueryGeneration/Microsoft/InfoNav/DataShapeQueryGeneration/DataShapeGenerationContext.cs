using System;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000040 RID: 64
	internal sealed class DataShapeGenerationContext
	{
		// Token: 0x0600022E RID: 558 RVA: 0x0000A2CC File Offset: 0x000084CC
		internal DataShapeGenerationContext(ITracer tracer, ITelemetryService telemetryService, IFeatureSwitchProvider featureSwitchProvider, IDumper dumper, IConceptualSchema model, DataShapeGenerationTelemetry telemetryInfo, IExpressionToExtensionSchemaItemQueryRewriter expressionToExtensionSchemaItemQueryRewriter, int dataShapeId = 0, bool useDynamicLimits = true, bool returnInternalSchema = false, bool allowQueryParameters = false, bool testOnlyReturnDsqReferenceInternalSchema = false)
			: this(tracer, telemetryService, featureSwitchProvider, dumper, model, DateTimeProviderFactory.Instance, dataShapeId, useDynamicLimits, telemetryInfo, expressionToExtensionSchemaItemQueryRewriter, returnInternalSchema, allowQueryParameters, testOnlyReturnDsqReferenceInternalSchema)
		{
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000A2FC File Offset: 0x000084FC
		internal DataShapeGenerationContext(ITracer tracer, ITelemetryService telemetryService, IFeatureSwitchProvider featureSwitchProvider, IDumper dumper, IConceptualSchema model, IDateTimeProviderFactory dateTimeProviderFactory, int dataShapeId, bool useDynamicLimits, DataShapeGenerationTelemetry telemetryInfo, IExpressionToExtensionSchemaItemQueryRewriter expressionToExtensionSchemaItemQueryRewriter, bool returnInternalSchema = false, bool allowQueryParameters = false, bool testOnlyReturnDsqReferenceInternalSchema = false)
		{
			this.Tracer = tracer;
			this.TelemetryService = telemetryService;
			this.FeatureSwitchProvider = featureSwitchProvider;
			this.Dumper = dumper;
			this.Model = model;
			this.DateTimeProviderFactoryInstance = dateTimeProviderFactory;
			this.TopLevelDataShapeId = DataShapeGenerationContext.CreateDataShapeId(dataShapeId);
			this.UseDynamicLimits = useDynamicLimits;
			this.TelemetryInfo = telemetryInfo;
			this.ReturnInternalSchema = returnInternalSchema;
			this.AllowQueryParameters = allowQueryParameters;
			this.TestOnlyReturnInternalDsqReferenceSchema = testOnlyReturnDsqReferenceInternalSchema;
			this.ExpressionToExtensionSchemaItemQueryRewriter = expressionToExtensionSchemaItemQueryRewriter;
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000230 RID: 560 RVA: 0x0000A379 File Offset: 0x00008579
		internal IDateTimeProviderFactory DateTimeProviderFactoryInstance { get; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000231 RID: 561 RVA: 0x0000A381 File Offset: 0x00008581
		internal IConceptualSchema Model { get; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000A389 File Offset: 0x00008589
		internal ITracer Tracer { get; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000233 RID: 563 RVA: 0x0000A391 File Offset: 0x00008591
		internal ITelemetryService TelemetryService { get; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0000A399 File Offset: 0x00008599
		internal IFeatureSwitchProvider FeatureSwitchProvider { get; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000235 RID: 565 RVA: 0x0000A3A1 File Offset: 0x000085A1
		internal IDumper Dumper { get; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0000A3A9 File Offset: 0x000085A9
		public string TopLevelDataShapeId { get; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000237 RID: 567 RVA: 0x0000A3B1 File Offset: 0x000085B1
		public bool UseDynamicLimits { get; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0000A3B9 File Offset: 0x000085B9
		public DataShapeGenerationTelemetry TelemetryInfo { get; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0000A3C1 File Offset: 0x000085C1
		internal IExpressionToExtensionSchemaItemQueryRewriter ExpressionToExtensionSchemaItemQueryRewriter { get; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000A3C9 File Offset: 0x000085C9
		internal bool ReturnInternalSchema { get; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000A3D1 File Offset: 0x000085D1
		internal bool AllowQueryParameters { get; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000A3D9 File Offset: 0x000085D9
		internal bool TestOnlyReturnInternalDsqReferenceSchema { get; }

		// Token: 0x0600023D RID: 573 RVA: 0x0000A3E1 File Offset: 0x000085E1
		private static string CreateDataShapeId(int num)
		{
			return DataShapeIdGenerator.CreateId("DS", num);
		}

		// Token: 0x04000107 RID: 263
		private const string DataShapeIdPrefix = "DS";

		// Token: 0x04000108 RID: 264
		internal const bool DefaultUseDynamicLimits = true;
	}
}
