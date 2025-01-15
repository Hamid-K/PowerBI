using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020003C6 RID: 966
	internal sealed class DataShapeIntersectionList : CellList
	{
		// Token: 0x06002730 RID: 10032 RVA: 0x000BA605 File Offset: 0x000B8805
		public DataShapeIntersectionList()
		{
		}

		// Token: 0x06002731 RID: 10033 RVA: 0x000BA60D File Offset: 0x000B880D
		internal DataShapeIntersectionList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17001403 RID: 5123
		internal DataShapeIntersection this[int index]
		{
			get
			{
				return (DataShapeIntersection)base[index];
			}
		}
	}
}
