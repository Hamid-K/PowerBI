using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200041A RID: 1050
	internal sealed class MapRowList : RowList
	{
		// Token: 0x06002DE5 RID: 11749 RVA: 0x000D1B00 File Offset: 0x000CFD00
		public MapRowList()
		{
		}

		// Token: 0x06002DE6 RID: 11750 RVA: 0x000D1B08 File Offset: 0x000CFD08
		internal MapRowList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x170015E6 RID: 5606
		internal MapRow this[int index]
		{
			get
			{
				return (MapRow)base[index];
			}
		}
	}
}
