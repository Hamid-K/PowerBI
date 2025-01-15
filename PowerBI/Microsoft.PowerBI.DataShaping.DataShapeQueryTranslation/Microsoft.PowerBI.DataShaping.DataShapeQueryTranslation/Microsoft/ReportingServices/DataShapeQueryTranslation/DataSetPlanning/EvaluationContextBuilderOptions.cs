using System;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x02000106 RID: 262
	internal sealed class EvaluationContextBuilderOptions
	{
		// Token: 0x06000A3F RID: 2623 RVA: 0x00027C0A File Offset: 0x00025E0A
		private EvaluationContextBuilderOptions(ContextOnlyElementMarking contextOnlyMarkingKind)
		{
			this.ContextOnlyMarkingKind = contextOnlyMarkingKind;
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000A40 RID: 2624 RVA: 0x00027C19 File Offset: 0x00025E19
		internal ContextOnlyElementMarking ContextOnlyMarkingKind { get; }

		// Token: 0x040004FA RID: 1274
		internal static EvaluationContextBuilderOptions Default = new EvaluationContextBuilderOptions(ContextOnlyElementMarking.All);

		// Token: 0x040004FB RID: 1275
		internal static EvaluationContextBuilderOptions ContextOnlyChildrenMarked = new EvaluationContextBuilderOptions(ContextOnlyElementMarking.TargetItemChildrenOnly);
	}
}
