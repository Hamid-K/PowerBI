using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004A8 RID: 1192
	[Serializable]
	internal sealed class DataCellList : CellList
	{
		// Token: 0x06003AA1 RID: 15009 RVA: 0x000FE6E5 File Offset: 0x000FC8E5
		public DataCellList()
		{
		}

		// Token: 0x06003AA2 RID: 15010 RVA: 0x000FE6ED File Offset: 0x000FC8ED
		internal DataCellList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x1700194B RID: 6475
		internal DataCell this[int index]
		{
			get
			{
				return (DataCell)base[index];
			}
		}
	}
}
