using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.InfoNav
{
	// Token: 0x02000035 RID: 53
	public interface IConceptualColumn : IConceptualProperty, IConceptualDisplayItem, IEquatable<IConceptualProperty>
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000B9 RID: 185
		ConceptualColumnGrouping Grouping { get; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000BA RID: 186
		IReadOnlyList<IConceptualColumn> OrderByColumns { get; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000BB RID: 187
		ConceptualDefaultAggregate ConceptualDefaultAggregate { get; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000BC RID: 188
		ConceptualTypeColumn ConceptualTypeColumn { get; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000BD RID: 189
		PrimitiveValue DefaultValue { get; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000BE RID: 190
		AggregateBehavior AggregateBehavior { get; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000BF RID: 191
		bool IsRowNumber { get; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000C0 RID: 192
		IReadOnlyList<IConceptualVariationSource> VariationSources { get; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000C1 RID: 193
		ConceptualGroupingMetadata GroupingMetadata { get; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000C2 RID: 194
		ConceptualParameterMetadata ParameterMetadata { get; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000C3 RID: 195
		bool Nullable { get; }

		// Token: 0x060000C4 RID: 196
		bool TryGetVariationSource(string referenceName, out IConceptualVariationSource variationSource);
	}
}
