using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002E9 RID: 745
	public static class ExtendedWeb
	{
		// Token: 0x060013D9 RID: 5081 RVA: 0x00044D4F File Offset: 0x00042F4F
		public static void SetCookie(Uri siteUrl, string cookieName, string cookieData)
		{
			if (!ExtendedWeb.NativeMethods.InternetSetCookie(siteUrl.ToString(), cookieName, cookieData))
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x0200078B RID: 1931
		private static class NativeMethods
		{
			// Token: 0x060030B3 RID: 12467
			[DllImport("wininet.dll", CharSet = CharSet.Unicode, SetLastError = true)]
			public static extern bool InternetSetCookie(string lpszUrlName, string lbszCookieName, string lpszCookieData);
		}
	}
}
