using System;

namespace Microsoft.Owin
{
	// Token: 0x02000006 RID: 6
	public class CookieOptions
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000022DB File Offset: 0x000004DB
		public CookieOptions()
		{
			this.Path = "/";
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000022EE File Offset: 0x000004EE
		// (set) Token: 0x0600000A RID: 10 RVA: 0x000022F6 File Offset: 0x000004F6
		public string Domain { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000022FF File Offset: 0x000004FF
		// (set) Token: 0x0600000C RID: 12 RVA: 0x00002307 File Offset: 0x00000507
		public string Path { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002310 File Offset: 0x00000510
		// (set) Token: 0x0600000E RID: 14 RVA: 0x00002318 File Offset: 0x00000518
		public DateTime? Expires { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002321 File Offset: 0x00000521
		// (set) Token: 0x06000010 RID: 16 RVA: 0x00002329 File Offset: 0x00000529
		public bool Secure { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002332 File Offset: 0x00000532
		// (set) Token: 0x06000012 RID: 18 RVA: 0x0000233A File Offset: 0x0000053A
		public bool HttpOnly { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002343 File Offset: 0x00000543
		// (set) Token: 0x06000014 RID: 20 RVA: 0x0000234B File Offset: 0x0000054B
		public SameSiteMode? SameSite { get; set; }
	}
}
