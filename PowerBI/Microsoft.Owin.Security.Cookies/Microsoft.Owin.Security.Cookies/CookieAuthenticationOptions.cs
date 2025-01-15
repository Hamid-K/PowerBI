using System;
using Microsoft.Owin.Infrastructure;

namespace Microsoft.Owin.Security.Cookies
{
	// Token: 0x02000006 RID: 6
	public class CookieAuthenticationOptions : AuthenticationOptions
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002464 File Offset: 0x00000664
		public CookieAuthenticationOptions()
			: base("Cookies")
		{
			this.ReturnUrlParameter = "ReturnUrl";
			this.CookiePath = "/";
			this.ExpireTimeSpan = TimeSpan.FromDays(14.0);
			this.SlidingExpiration = true;
			this.CookieHttpOnly = true;
			this.CookieSecure = CookieSecureOption.SameAsRequest;
			this.SystemClock = new SystemClock();
			this.Provider = new CookieAuthenticationProvider();
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000024D1 File Offset: 0x000006D1
		// (set) Token: 0x0600000D RID: 13 RVA: 0x000024D9 File Offset: 0x000006D9
		public string CookieName
		{
			get
			{
				return this._cookieName;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._cookieName = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000024F0 File Offset: 0x000006F0
		// (set) Token: 0x0600000F RID: 15 RVA: 0x000024F8 File Offset: 0x000006F8
		public string CookieDomain { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002501 File Offset: 0x00000701
		// (set) Token: 0x06000011 RID: 17 RVA: 0x00002509 File Offset: 0x00000709
		public string CookiePath { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002512 File Offset: 0x00000712
		// (set) Token: 0x06000013 RID: 19 RVA: 0x0000251A File Offset: 0x0000071A
		public bool CookieHttpOnly { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002523 File Offset: 0x00000723
		// (set) Token: 0x06000015 RID: 21 RVA: 0x0000252B File Offset: 0x0000072B
		public SameSiteMode? CookieSameSite { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002534 File Offset: 0x00000734
		// (set) Token: 0x06000017 RID: 23 RVA: 0x0000253C File Offset: 0x0000073C
		public CookieSecureOption CookieSecure { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002545 File Offset: 0x00000745
		// (set) Token: 0x06000019 RID: 25 RVA: 0x0000254D File Offset: 0x0000074D
		public TimeSpan ExpireTimeSpan { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002556 File Offset: 0x00000756
		// (set) Token: 0x0600001B RID: 27 RVA: 0x0000255E File Offset: 0x0000075E
		public bool SlidingExpiration { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002567 File Offset: 0x00000767
		// (set) Token: 0x0600001D RID: 29 RVA: 0x0000256F File Offset: 0x0000076F
		public PathString LoginPath { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002578 File Offset: 0x00000778
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002580 File Offset: 0x00000780
		public PathString LogoutPath { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002589 File Offset: 0x00000789
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002591 File Offset: 0x00000791
		public string ReturnUrlParameter { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000022 RID: 34 RVA: 0x0000259A File Offset: 0x0000079A
		// (set) Token: 0x06000023 RID: 35 RVA: 0x000025A2 File Offset: 0x000007A2
		public ICookieAuthenticationProvider Provider { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000025AB File Offset: 0x000007AB
		// (set) Token: 0x06000025 RID: 37 RVA: 0x000025B3 File Offset: 0x000007B3
		public ISecureDataFormat<AuthenticationTicket> TicketDataFormat { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000025BC File Offset: 0x000007BC
		// (set) Token: 0x06000027 RID: 39 RVA: 0x000025C4 File Offset: 0x000007C4
		public ISystemClock SystemClock { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000025CD File Offset: 0x000007CD
		// (set) Token: 0x06000029 RID: 41 RVA: 0x000025D5 File Offset: 0x000007D5
		public ICookieManager CookieManager { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000025DE File Offset: 0x000007DE
		// (set) Token: 0x0600002B RID: 43 RVA: 0x000025E6 File Offset: 0x000007E6
		public IAuthenticationSessionStore SessionStore { get; set; }

		// Token: 0x04000012 RID: 18
		private string _cookieName;
	}
}
