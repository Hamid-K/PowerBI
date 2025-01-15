using System;
using System.Security.Principal;

namespace Microsoft.PowerBI.ReportServer.WebApi.Logon
{
	// Token: 0x02000030 RID: 48
	public interface IUserService
	{
		// Token: 0x060000DD RID: 221
		WindowsIdentity Logon(UserCredentials usercredentials);

		// Token: 0x060000DE RID: 222
		string GetUserPrincipalName(IIdentity identity);

		// Token: 0x060000DF RID: 223
		bool IsWindowsIdentity(IIdentity identity);

		// Token: 0x060000E0 RID: 224
		IIdentity GetCurrentWindowsIdentity();
	}
}
