using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000757 RID: 1879
	[Serializable]
	internal abstract class TablixHeadingList : ArrayList
	{
		// Token: 0x06006832 RID: 26674 RVA: 0x00195B26 File Offset: 0x00193D26
		internal TablixHeadingList()
		{
		}

		// Token: 0x06006833 RID: 26675 RVA: 0x00195B2E File Offset: 0x00193D2E
		internal TablixHeadingList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x170024D4 RID: 9428
		internal TablixHeading this[int index]
		{
			get
			{
				return (TablixHeading)base[index];
			}
		}

		// Token: 0x06006835 RID: 26677
		internal abstract TablixHeadingList InnerHeadings();
	}
}
