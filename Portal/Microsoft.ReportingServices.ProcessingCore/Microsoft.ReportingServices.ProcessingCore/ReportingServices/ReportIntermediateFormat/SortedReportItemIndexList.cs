using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020003BB RID: 955
	internal sealed class SortedReportItemIndexList : ArrayList
	{
		// Token: 0x060026BA RID: 9914 RVA: 0x000B97FE File Offset: 0x000B79FE
		internal SortedReportItemIndexList()
		{
		}

		// Token: 0x060026BB RID: 9915 RVA: 0x000B9806 File Offset: 0x000B7A06
		internal SortedReportItemIndexList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x060026BC RID: 9916 RVA: 0x000B9810 File Offset: 0x000B7A10
		public void Add(List<ReportItem> collection, int collectionIndex, bool sortVertically)
		{
			Global.Tracer.Assert(collection != null, "(null != collection)");
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

		// Token: 0x170013E3 RID: 5091
		internal int this[int index]
		{
			get
			{
				return (int)base[index];
			}
		}
	}
}
