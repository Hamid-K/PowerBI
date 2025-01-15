using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations
{
	// Token: 0x02000240 RID: 576
	internal sealed class BatchSubtotalSortAnnotations
	{
		// Token: 0x060013B3 RID: 5043 RVA: 0x0004CBDE File Offset: 0x0004ADDE
		internal BatchSubtotalSortAnnotations()
		{
			this.m_sortByMeasureSourceAnnotations = new Dictionary<DataMember, BatchSortByMeasureSourceAnnotation>();
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x0004CBF1 File Offset: 0x0004ADF1
		internal void AddSortByMeasureSourceAnnotation(DataMember member, BatchSortByMeasureSourceAnnotation annotation)
		{
			this.m_sortByMeasureSourceAnnotations.Add(member, annotation);
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x0004CC00 File Offset: 0x0004AE00
		internal bool TryGetSortyByMeasureSourceAnnotation(DataMember member, out BatchSortByMeasureSourceAnnotation annotation)
		{
			return this.m_sortByMeasureSourceAnnotations.TryGetValue(member, out annotation);
		}

		// Token: 0x040008B1 RID: 2225
		private readonly Dictionary<DataMember, BatchSortByMeasureSourceAnnotation> m_sortByMeasureSourceAnnotations;
	}
}
