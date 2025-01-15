using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations
{
	// Token: 0x0200024E RID: 590
	internal sealed class DataShapeSharedAnnotation
	{
		// Token: 0x06001473 RID: 5235 RVA: 0x0004E77F File Offset: 0x0004C97F
		internal DataShapeSharedAnnotation(ExistsFilterCondition existsFilter, IReadOnlyList<Filter> valueFilters, IReadOnlyList<Filter> dataShapeValueFilters)
		{
			this.ExistsFilter = existsFilter;
			this.ScopedValueFilters = valueFilters;
			this.DataShapeValueFilters = dataShapeValueFilters;
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06001474 RID: 5236 RVA: 0x0004E79C File Offset: 0x0004C99C
		public ExistsFilterCondition ExistsFilter { get; }

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06001475 RID: 5237 RVA: 0x0004E7A4 File Offset: 0x0004C9A4
		public IReadOnlyList<Filter> ScopedValueFilters { get; }

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06001476 RID: 5238 RVA: 0x0004E7AC File Offset: 0x0004C9AC
		public IReadOnlyList<Filter> DataShapeValueFilters { get; }
	}
}
