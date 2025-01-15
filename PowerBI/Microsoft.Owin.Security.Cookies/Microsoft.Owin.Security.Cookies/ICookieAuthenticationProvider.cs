using System;
using System.Threading.Tasks;

namespace Microsoft.Owin.Security.Cookies
{
	// Token: 0x02000011 RID: 17
	public interface ICookieAuthenticationProvider
	{
		// Token: 0x0600006B RID: 107
		Task ValidateIdentity(CookieValidateIdentityContext context);

		// Token: 0x0600006C RID: 108
		void ResponseSignIn(CookieResponseSignInContext context);

		// Token: 0x0600006D RID: 109
		void ResponseSignedIn(CookieResponseSignedInContext context);

		// Token: 0x0600006E RID: 110
		void ApplyRedirect(CookieApplyRedirectContext context);

		// Token: 0x0600006F RID: 111
		void ResponseSignOut(CookieResponseSignOutContext context);

		// Token: 0x06000070 RID: 112
		void Exception(CookieExceptionContext context);
	}
}
