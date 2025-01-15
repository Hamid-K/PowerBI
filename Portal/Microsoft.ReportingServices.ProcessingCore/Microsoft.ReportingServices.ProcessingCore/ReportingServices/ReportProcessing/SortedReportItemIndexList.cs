using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006AC RID: 1708
	internal sealed class SortedReportItemIndexList : ArrayList
	{
		// Token: 0x06005C9C RID: 23708 RVA: 0x00179B61 File Offset: 0x00177D61
		internal SortedReportItemIndexList()
		{
		}

		// Token: 0x06005C9D RID: 23709 RVA: 0x00179B69 File Offset: 0x00177D69
		internal SortedReportItemIndexList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x06005C9E RID: 23710 RVA: 0x00179B74 File Offset: 0x00177D74
		public void Add(ReportItemList collection, int collectionIndex, bool sortVertically)
		{
			Global.Tracer.Assert(collection != null);
			ReportItem reportItem = collection[collectionIndex];
			int i = 0;
			while (i < base.Count)
			{
				if (sortVertically && reportItem.AbsoluteTopValue > collection[this[i]].AbsoluteTopValue)
				{
					i++;
				}
				else
				{
					if (sortVertically || reportItem.AbsoluteLeftValue <= collection[this[i]].AbsoluteLeftValue)
					{
						break;
					}
					i++;
				}
			}
			base.Insert(i, collectionIndex);
		}

		// Token: 0x17002087 RID: 8327
		internal int this[int index]
		{
			get
			{
				return (int)base[index];
			}
		}
	}
}
