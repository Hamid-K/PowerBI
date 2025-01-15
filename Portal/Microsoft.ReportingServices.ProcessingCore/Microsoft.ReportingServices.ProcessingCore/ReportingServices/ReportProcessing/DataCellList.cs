using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200075A RID: 1882
	[Serializable]
	internal sealed class DataCellList : ArrayList
	{
		// Token: 0x0600683F RID: 26687 RVA: 0x00195C40 File Offset: 0x00193E40
		internal DataCellList()
		{
		}

		// Token: 0x06006840 RID: 26688 RVA: 0x00195C48 File Offset: 0x00193E48
		internal DataCellList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x170024D7 RID: 9431
		internal DataValueCRIList this[int index]
		{
			get
			{
				return (DataValueCRIList)base[index];
			}
		}
	}
}
