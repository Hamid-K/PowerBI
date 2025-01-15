using System;
using System.Runtime.InteropServices;

namespace Microsoft.EnterpriseSingleSignOn.Interop
{
	// Token: 0x020004B1 RID: 1201
	[InterfaceType(ComInterfaceType.InterfaceIsDual)]
	[Guid("77437865-7472-46A8-9269-431F143E6A27")]
	[CoClass(typeof(SSOTicket))]
	[ComImport]
	public interface ISSOTicket
	{
		// Token: 0x0600293F RID: 10559
		string IssueTicket(int flags);

		// Token: 0x06002940 RID: 10560
		string[] RedeemTicket(string applicationName, string sender, string ticket, int flags, out string externalUserName);
	}
}
