using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200047B RID: 1147
	[Serializable]
	internal class CellList : ArrayList
	{
		// Token: 0x06003507 RID: 13575 RVA: 0x000E8B1B File Offset: 0x000E6D1B
		internal CellList()
		{
		}

		// Token: 0x06003508 RID: 13576 RVA: 0x000E8B23 File Offset: 0x000E6D23
		internal CellList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17001799 RID: 6041
		internal Cell this[int index]
		{
			get
			{
				return (Cell)base[index];
			}
		}
	}
}
