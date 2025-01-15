using System;
using System.Runtime.InteropServices;

namespace Microsoft.ReportingServices.HostingInterfaces
{
	// Token: 0x02000003 RID: 3
	[ComVisible(true)]
	public enum RsAppDomainType
	{
		// Token: 0x04000029 RID: 41
		Unknown = -1,
		// Token: 0x0400002A RID: 42
		Default,
		// Token: 0x0400002B RID: 43
		WindowsService,
		// Token: 0x0400002C RID: 44
		ReportServer,
		// Token: 0x0400002D RID: 45
		AppDomainCount
	}
}
