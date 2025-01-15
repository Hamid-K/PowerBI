using System;
using System.Collections;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200068F RID: 1679
	[ArrayOfReferences]
	[Serializable]
	internal sealed class DataRegionList : ArrayList
	{
		// Token: 0x06005C33 RID: 23603 RVA: 0x001794ED File Offset: 0x001776ED
		internal DataRegionList()
		{
		}

		// Token: 0x06005C34 RID: 23604 RVA: 0x001794F5 File Offset: 0x001776F5
		internal DataRegionList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17002069 RID: 8297
		internal DataRegion this[int index]
		{
			get
			{
				return (DataRegion)base[index];
			}
		}
	}
}
