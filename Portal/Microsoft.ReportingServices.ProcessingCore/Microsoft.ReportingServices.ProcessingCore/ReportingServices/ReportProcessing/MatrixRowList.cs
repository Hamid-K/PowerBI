using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006A8 RID: 1704
	[Serializable]
	internal sealed class MatrixRowList : ArrayList
	{
		// Token: 0x06005C8D RID: 23693 RVA: 0x00179A5D File Offset: 0x00177C5D
		internal MatrixRowList()
		{
		}

		// Token: 0x06005C8E RID: 23694 RVA: 0x00179A65 File Offset: 0x00177C65
		internal MatrixRowList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17002082 RID: 8322
		internal MatrixRow this[int index]
		{
			get
			{
				return (MatrixRow)base[index];
			}
		}
	}
}
