using System;
using System.Web.Security;

namespace Microsoft.BIServer.Owin.Common.Services
{
	// Token: 0x02000011 RID: 17
	public interface IAuthenticationService
	{
		// Token: 0x06000043 RID: 67
		IWebHostUserContext GetUserInfo(RsRequestContext requestContext);

		// Token: 0x06000044 RID: 68
		bool TryLogonUser(string user, string password, string domain, out FormsAuthenticationTicket ticket);

		// Token: 0x06000045 RID: 69
		bool IsAuthExtensionInBackCompatMode();
	}
}
