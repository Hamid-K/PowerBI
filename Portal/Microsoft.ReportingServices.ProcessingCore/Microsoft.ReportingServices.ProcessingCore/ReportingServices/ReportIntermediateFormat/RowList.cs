using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200047D RID: 1149
	[Serializable]
	internal class RowList : ArrayList
	{
		// Token: 0x06003514 RID: 13588 RVA: 0x000E8BFF File Offset: 0x000E6DFF
		internal RowList()
		{
		}

		// Token: 0x06003515 RID: 13589 RVA: 0x000E8C07 File Offset: 0x000E6E07
		internal RowList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x1700179B RID: 6043
		internal Row this[int index]
		{
			get
			{
				return (Row)base[index];
			}
		}
	}
}
