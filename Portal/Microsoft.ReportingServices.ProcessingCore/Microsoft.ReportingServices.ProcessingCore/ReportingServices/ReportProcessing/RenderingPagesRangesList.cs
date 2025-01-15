using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000697 RID: 1687
	[Serializable]
	internal sealed class RenderingPagesRangesList : ArrayList
	{
		// Token: 0x06005C4E RID: 23630 RVA: 0x001796BD File Offset: 0x001778BD
		internal RenderingPagesRangesList()
		{
		}

		// Token: 0x06005C4F RID: 23631 RVA: 0x001796C5 File Offset: 0x001778C5
		internal RenderingPagesRangesList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17002071 RID: 8305
		internal RenderingPagesRanges this[int index]
		{
			get
			{
				return (RenderingPagesRanges)base[index];
			}
		}

		// Token: 0x06005C51 RID: 23633 RVA: 0x001796DC File Offset: 0x001778DC
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
