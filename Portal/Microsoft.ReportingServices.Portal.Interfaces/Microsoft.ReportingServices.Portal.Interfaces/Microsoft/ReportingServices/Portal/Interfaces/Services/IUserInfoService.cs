using System;
using System.Security.Principal;

namespace Microsoft.ReportingServices.Portal.Interfaces.Services
{
	// Token: 0x02000093 RID: 147
	public interface IUserInfoService
	{
		// Token: 0x0600046B RID: 1131
		string GetUserDisplayName(IIdentity identity);
	}
}
