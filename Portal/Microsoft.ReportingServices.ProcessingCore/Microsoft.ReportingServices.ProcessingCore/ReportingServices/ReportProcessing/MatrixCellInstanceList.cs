using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006B2 RID: 1714
	[Serializable]
	internal sealed class MatrixCellInstanceList : ArrayList
	{
		// Token: 0x06005CB4 RID: 23732 RVA: 0x00179F9D File Offset: 0x0017819D
		internal MatrixCellInstanceList()
		{
		}

		// Token: 0x06005CB5 RID: 23733 RVA: 0x00179FA5 File Offset: 0x001781A5
		internal MatrixCellInstanceList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x1700208D RID: 8333
		internal MatrixCellInstance this[int index]
		{
			get
			{
				return (MatrixCellInstance)base[index];
			}
		}
	}
}
