using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006B6 RID: 1718
	[Serializable]
	internal sealed class VariantList : ArrayList
	{
		// Token: 0x06005CC3 RID: 23747 RVA: 0x0017A2BD File Offset: 0x001784BD
		internal VariantList()
		{
		}

		// Token: 0x06005CC4 RID: 23748 RVA: 0x0017A2C5 File Offset: 0x001784C5
		internal VariantList(int capacity)
			: base(capacity)
		{
		}
	}
}
