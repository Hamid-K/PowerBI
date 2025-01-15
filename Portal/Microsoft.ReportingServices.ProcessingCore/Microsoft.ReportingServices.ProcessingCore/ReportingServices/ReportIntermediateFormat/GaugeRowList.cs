using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020003E7 RID: 999
	internal sealed class GaugeRowList : RowList
	{
		// Token: 0x06002932 RID: 10546 RVA: 0x000C0E1C File Offset: 0x000BF01C
		public GaugeRowList()
		{
		}

		// Token: 0x06002933 RID: 10547 RVA: 0x000C0E24 File Offset: 0x000BF024
		internal GaugeRowList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17001481 RID: 5249
		internal GaugeRow this[int index]
		{
			get
			{
				return (GaugeRow)base[index];
			}
		}
	}
}
