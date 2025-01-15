using System;

namespace Microsoft.Owin.Security.Cookies
{
	// Token: 0x02000003 RID: 3
	public static class CookieAuthenticationDefaults
	{
		// Token: 0x04000001 RID: 1
		public const string AuthenticationType = "Cookies";

		// Token: 0x04000002 RID: 2
		public const string CookiePrefix = ".AspNet.";

		// Token: 0x04000003 RID: 3
		public static readonly PathString LoginPath = new PathString("/Account/Login");

		// Token: 0x04000004 RID: 4
		public static readonly PathString LogoutPath = new PathString("/Account/Logout");

		// Token: 0x04000005 RID: 5
		public const string ReturnUrlParameter = "ReturnUrl";
	}
}
