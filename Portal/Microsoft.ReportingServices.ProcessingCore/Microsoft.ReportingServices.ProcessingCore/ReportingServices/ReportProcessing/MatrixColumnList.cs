using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006A7 RID: 1703
	[Serializable]
	internal sealed class MatrixColumnList : ArrayList
	{
		// Token: 0x06005C8A RID: 23690 RVA: 0x00179A3E File Offset: 0x00177C3E
		internal MatrixColumnList()
		{
		}

		// Token: 0x06005C8B RID: 23691 RVA: 0x00179A46 File Offset: 0x00177C46
		internal MatrixColumnList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17002081 RID: 8321
		internal MatrixColumn this[int index]
		{
			get
			{
				return (MatrixColumn)base[index];
			}
		}
	}
}
