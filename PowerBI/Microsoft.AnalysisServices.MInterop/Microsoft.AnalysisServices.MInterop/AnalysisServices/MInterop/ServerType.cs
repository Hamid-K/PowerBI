using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.MInterop
{
	// Token: 0x0200002F RID: 47
	[ComVisible(true)]
	[Guid("3B8B1444-45AF-4B83-AB92-257CE75067EF")]
	public enum ServerType
	{
		// Token: 0x04000142 RID: 322
		OnPrem,
		// Token: 0x04000143 RID: 323
		ASAzure,
		// Token: 0x04000144 RID: 324
		PBIDedicated,
		// Token: 0x04000145 RID: 325
		PowerBI
	}
}
