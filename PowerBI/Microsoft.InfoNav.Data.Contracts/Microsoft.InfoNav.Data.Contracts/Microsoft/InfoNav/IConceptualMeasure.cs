using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav
{
	// Token: 0x0200003C RID: 60
	public interface IConceptualMeasure : IConceptualProperty, IConceptualDisplayItem, IEquatable<IConceptualProperty>
	{
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060000EF RID: 239
		IConceptualKpi Kpi { get; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060000F0 RID: 240
		IConceptualMeasure DynamicFormatString { get; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060000F1 RID: 241
		IConceptualMeasure DynamicFormatCulture { get; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060000F2 RID: 242
		ConceptualMeasureTemplate Template { get; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060000F3 RID: 243
		ConceptualDataChangeDetectionMetadata ChangeDetectionMetadata { get; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060000F4 RID: 244
		ConceptualDistributiveAggregateKind? DistributiveAggegate { get; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060000F5 RID: 245
		IReadOnlyList<IConceptualEntity> DistributiveBy { get; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060000F6 RID: 246
		bool IsVariant { get; }
	}
}
