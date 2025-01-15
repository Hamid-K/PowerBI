using System;
using System.Runtime.InteropServices;

namespace Microsoft.ReportingServices.HostingInterfaces
{
	// Token: 0x02000004 RID: 4
	[ComVisible(true)]
	public enum HttpAuthType
	{
		// Token: 0x0400002F RID: 47
		Anonymous,
		// Token: 0x04000030 RID: 48
		Negotiate,
		// Token: 0x04000031 RID: 49
		Kerberos,
		// Token: 0x04000032 RID: 50
		Ntlm,
		// Token: 0x04000033 RID: 51
		Digest,
		// Token: 0x04000034 RID: 52
		Basic
	}
}
