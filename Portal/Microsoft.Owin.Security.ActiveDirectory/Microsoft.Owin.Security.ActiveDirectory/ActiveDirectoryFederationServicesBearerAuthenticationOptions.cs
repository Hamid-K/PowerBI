using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security.OAuth;

namespace Microsoft.Owin.Security.ActiveDirectory
{
	// Token: 0x02000002 RID: 2
	public class ActiveDirectoryFederationServicesBearerAuthenticationOptions : AuthenticationOptions
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public ActiveDirectoryFederationServicesBearerAuthenticationOptions()
			: base("Bearer")
		{
			this.BackchannelTimeout = TimeSpan.FromMinutes(1.0);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002071 File Offset: 0x00000271
		// (set) Token: 0x06000003 RID: 3 RVA: 0x00002079 File Offset: 0x00000279
		public string MetadataEndpoint { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002082 File Offset: 0x00000282
		// (set) Token: 0x06000005 RID: 5 RVA: 0x0000208A File Offset: 0x0000028A
		[Obsolete("Use TokenValidationParameters.ValidAudience", false)]
		public string Audience { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002093 File Offset: 0x00000293
		// (set) Token: 0x06000007 RID: 7 RVA: 0x0000209B File Offset: 0x0000029B
		public string Realm { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020A4 File Offset: 0x000002A4
		// (set) Token: 0x06000009 RID: 9 RVA: 0x000020AC File Offset: 0x000002AC
		public IOAuthBearerAuthenticationProvider Provider { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000020B5 File Offset: 0x000002B5
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000020BD File Offset: 0x000002BD
		public ICertificateValidator BackchannelCertificateValidator { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000020C6 File Offset: 0x000002C6
		// (set) Token: 0x0600000D RID: 13 RVA: 0x000020CE File Offset: 0x000002CE
		public TimeSpan BackchannelTimeout { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000020D7 File Offset: 0x000002D7
		// (set) Token: 0x0600000F RID: 15 RVA: 0x000020DF File Offset: 0x000002DF
		public HttpMessageHandler BackchannelHttpHandler { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000020E8 File Offset: 0x000002E8
		// (set) Token: 0x06000011 RID: 17 RVA: 0x000020F0 File Offset: 0x000002F0
		public TokenValidationParameters TokenValidationParameters { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000020F9 File Offset: 0x000002F9
		// (set) Token: 0x06000013 RID: 19 RVA: 0x00002101 File Offset: 0x00000301
		public JwtSecurityTokenHandler TokenHandler { get; set; }
	}
}
