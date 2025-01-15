using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security.OAuth;

namespace Microsoft.Owin.Security.ActiveDirectory
{
	// Token: 0x02000004 RID: 4
	public class WindowsAzureActiveDirectoryBearerAuthenticationOptions : AuthenticationOptions
	{
		// Token: 0x06000019 RID: 25 RVA: 0x00002134 File Offset: 0x00000334
		public WindowsAzureActiveDirectoryBearerAuthenticationOptions()
			: base("Bearer")
		{
			this.BackchannelTimeout = TimeSpan.FromMinutes(1.0);
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002155 File Offset: 0x00000355
		// (set) Token: 0x0600001B RID: 27 RVA: 0x0000215D File Offset: 0x0000035D
		[Obsolete("Use TokenValidationParameters.ValidAudience", false)]
		public string Audience { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002166 File Offset: 0x00000366
		// (set) Token: 0x0600001D RID: 29 RVA: 0x0000216E File Offset: 0x0000036E
		public string Realm { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002177 File Offset: 0x00000377
		// (set) Token: 0x0600001F RID: 31 RVA: 0x0000217F File Offset: 0x0000037F
		public string Tenant { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002188 File Offset: 0x00000388
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002190 File Offset: 0x00000390
		public string MetadataAddress { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002199 File Offset: 0x00000399
		// (set) Token: 0x06000023 RID: 35 RVA: 0x000021A1 File Offset: 0x000003A1
		public IOAuthBearerAuthenticationProvider Provider { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000021AA File Offset: 0x000003AA
		// (set) Token: 0x06000025 RID: 37 RVA: 0x000021B2 File Offset: 0x000003B2
		public ICertificateValidator BackchannelCertificateValidator { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000021BB File Offset: 0x000003BB
		// (set) Token: 0x06000027 RID: 39 RVA: 0x000021C3 File Offset: 0x000003C3
		public TimeSpan BackchannelTimeout { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000021CC File Offset: 0x000003CC
		// (set) Token: 0x06000029 RID: 41 RVA: 0x000021D4 File Offset: 0x000003D4
		public HttpMessageHandler BackchannelHttpHandler { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000021DD File Offset: 0x000003DD
		// (set) Token: 0x0600002B RID: 43 RVA: 0x000021E5 File Offset: 0x000003E5
		public TokenValidationParameters TokenValidationParameters { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000021EE File Offset: 0x000003EE
		// (set) Token: 0x0600002D RID: 45 RVA: 0x000021F6 File Offset: 0x000003F6
		public JwtSecurityTokenHandler TokenHandler { get; set; }
	}
}
