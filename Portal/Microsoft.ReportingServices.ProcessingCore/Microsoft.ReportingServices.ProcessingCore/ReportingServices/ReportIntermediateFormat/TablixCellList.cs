using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000522 RID: 1314
	[Serializable]
	internal sealed class TablixCellList : CellList
	{
		// Token: 0x060046D8 RID: 18136 RVA: 0x0012997A File Offset: 0x00127B7A
		public TablixCellList()
		{
		}

		// Token: 0x060046D9 RID: 18137 RVA: 0x00129982 File Offset: 0x00127B82
		internal TablixCellList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17001D85 RID: 7557
		internal TablixCell this[int index]
		{
			get
			{
				return (TablixCell)base[index];
			}
		}
	}
}
