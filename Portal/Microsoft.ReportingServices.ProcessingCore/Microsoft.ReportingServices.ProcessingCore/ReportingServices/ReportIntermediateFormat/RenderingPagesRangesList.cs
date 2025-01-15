using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004EC RID: 1260
	[Serializable]
	internal sealed class RenderingPagesRangesList : ArrayList
	{
		// Token: 0x06003FF7 RID: 16375 RVA: 0x0010E8A5 File Offset: 0x0010CAA5
		public RenderingPagesRangesList()
		{
		}

		// Token: 0x06003FF8 RID: 16376 RVA: 0x0010E8AD File Offset: 0x0010CAAD
		internal RenderingPagesRangesList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17001B05 RID: 6917
		internal RenderingPagesRanges this[int index]
		{
			get
			{
				return (RenderingPagesRanges)base[index];
			}
		}

		// Token: 0x06003FFA RID: 16378 RVA: 0x0010E8C4 File Offset: 0x0010CAC4
		internal void MoveAllToFirstPage(int totalCount)
		{
			int count = this.Count;
			if (count == 0)
			{
				return;
			}
			if (count > 1)
			{
				base.RemoveRange(1, count - 1);
			}
			RenderingPagesRanges renderingPagesRanges = (RenderingPagesRanges)base[0];
			renderingPagesRanges.NumberOfDetails = totalCount;
			renderingPagesRanges.EndPage = renderingPagesRanges.StartPage;
		}
	}
}
