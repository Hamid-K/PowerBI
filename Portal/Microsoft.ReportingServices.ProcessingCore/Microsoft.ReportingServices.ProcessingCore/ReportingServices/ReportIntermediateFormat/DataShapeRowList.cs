using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020003CF RID: 975
	internal sealed class DataShapeRowList : RowList
	{
		// Token: 0x06002777 RID: 10103 RVA: 0x000BAAC5 File Offset: 0x000B8CC5
		public DataShapeRowList()
		{
		}

		// Token: 0x06002778 RID: 10104 RVA: 0x000BAACD File Offset: 0x000B8CCD
		internal DataShapeRowList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17001418 RID: 5144
		internal DataShapeRow this[int index]
		{
			get
			{
				return (DataShapeRow)base[index];
			}
		}
	}
}
