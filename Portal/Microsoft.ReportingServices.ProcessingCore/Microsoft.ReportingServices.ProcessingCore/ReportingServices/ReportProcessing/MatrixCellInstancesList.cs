using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006B1 RID: 1713
	[Serializable]
	internal sealed class MatrixCellInstancesList : ArrayList
	{
		// Token: 0x06005CB1 RID: 23729 RVA: 0x00179F7E File Offset: 0x0017817E
		internal MatrixCellInstancesList()
		{
		}

		// Token: 0x06005CB2 RID: 23730 RVA: 0x00179F86 File Offset: 0x00178186
		internal MatrixCellInstancesList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x1700208C RID: 8332
		internal MatrixCellInstanceList this[int index]
		{
			get
			{
				return (MatrixCellInstanceList)base[index];
			}
		}
	}
}
