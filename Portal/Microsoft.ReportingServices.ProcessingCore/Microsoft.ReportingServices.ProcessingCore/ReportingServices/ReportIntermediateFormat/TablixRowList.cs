using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000520 RID: 1312
	[Serializable]
	internal sealed class TablixRowList : RowList
	{
		// Token: 0x060046C2 RID: 18114 RVA: 0x00129693 File Offset: 0x00127893
		public TablixRowList()
		{
		}

		// Token: 0x060046C3 RID: 18115 RVA: 0x0012969B File Offset: 0x0012789B
		internal TablixRowList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17001D7F RID: 7551
		internal TablixRow this[int index]
		{
			get
			{
				return (TablixRow)base[index];
			}
		}
	}
}
