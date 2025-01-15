using System;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations
{
	// Token: 0x02000241 RID: 577
	internal sealed class BatchSortByMeasureSourceAnnotation
	{
		// Token: 0x060013B6 RID: 5046 RVA: 0x0004CC0F File Offset: 0x0004AE0F
		internal BatchSortByMeasureSourceAnnotation(BatchSubtotalAnnotation sameHierarchyAnnotation, BatchSubtotalAnnotation otherHierarchyAnnotation)
		{
			this.m_sameHierarchyAnnotation = sameHierarchyAnnotation;
			this.m_otherHierarchyAnnotation = otherHierarchyAnnotation;
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x060013B7 RID: 5047 RVA: 0x0004CC25 File Offset: 0x0004AE25
		internal BatchSubtotalAnnotation SameHierarchyAnnotation
		{
			get
			{
				return this.m_sameHierarchyAnnotation;
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x060013B8 RID: 5048 RVA: 0x0004CC2D File Offset: 0x0004AE2D
		internal BatchSubtotalAnnotation OtherHierarchyAnnotation
		{
			get
			{
				return this.m_otherHierarchyAnnotation;
			}
		}

		// Token: 0x040008B2 RID: 2226
		private readonly BatchSubtotalAnnotation m_sameHierarchyAnnotation;

		// Token: 0x040008B3 RID: 2227
		private readonly BatchSubtotalAnnotation m_otherHierarchyAnnotation;
	}
}
