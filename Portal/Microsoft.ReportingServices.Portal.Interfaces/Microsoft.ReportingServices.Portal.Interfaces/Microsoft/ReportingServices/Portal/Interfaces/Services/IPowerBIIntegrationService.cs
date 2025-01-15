using System;
using System.Security.Principal;
using Model;

namespace Microsoft.ReportingServices.Portal.Interfaces.Services
{
	// Token: 0x02000092 RID: 146
	public interface IPowerBIIntegrationService
	{
		// Token: 0x06000468 RID: 1128
		bool IsPowerBIEnabled();

		// Token: 0x06000469 RID: 1129
		PowerBIUserInfo GetUserInfo(IPrincipal userPrincipal);

		// Token: 0x0600046A RID: 1130
		bool LogOutUser(IPrincipal userPrincipal);
	}
}
