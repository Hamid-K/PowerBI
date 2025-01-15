using System;
using System.Threading.Tasks;

namespace Microsoft.Owin.Security.Cookies
{
	// Token: 0x02000008 RID: 8
	public interface IAuthenticationSessionStore
	{
		// Token: 0x0600002C RID: 44
		Task<string> StoreAsync(AuthenticationTicket ticket);

		// Token: 0x0600002D RID: 45
		Task RenewAsync(string key, AuthenticationTicket ticket);

		// Token: 0x0600002E RID: 46
		Task<AuthenticationTicket> RetrieveAsync(string key);

		// Token: 0x0600002F RID: 47
		Task RemoveAsync(string key);
	}
}
