using System;
using System.Net.Http;
using Microsoft.Identity.Client.AuthScheme.PoP;

namespace Microsoft.Identity.Client.AppConfig
{
	// Token: 0x020002CE RID: 718
	public class PoPAuthenticationConfiguration
	{
		// Token: 0x06001AC0 RID: 6848 RVA: 0x00056D76 File Offset: 0x00054F76
		public PoPAuthenticationConfiguration()
		{
			ApplicationBase.GuardMobileFrameworks();
		}

		// Token: 0x06001AC1 RID: 6849 RVA: 0x00056D8C File Offset: 0x00054F8C
		public PoPAuthenticationConfiguration(HttpRequestMessage httpRequestMessage)
		{
			if (httpRequestMessage == null)
			{
				throw new ArgumentNullException("httpRequestMessage");
			}
			this.HttpMethod = httpRequestMessage.Method;
			this.HttpHost = httpRequestMessage.RequestUri.Authority;
			this.HttpPath = httpRequestMessage.RequestUri.AbsolutePath;
		}

		// Token: 0x06001AC2 RID: 6850 RVA: 0x00056DE2 File Offset: 0x00054FE2
		public PoPAuthenticationConfiguration(Uri requestUri)
		{
			if (requestUri == null)
			{
				throw new ArgumentNullException("requestUri");
			}
			this.HttpHost = requestUri.Authority;
			this.HttpPath = requestUri.AbsolutePath;
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06001AC3 RID: 6851 RVA: 0x00056E1D File Offset: 0x0005501D
		// (set) Token: 0x06001AC4 RID: 6852 RVA: 0x00056E25 File Offset: 0x00055025
		public HttpMethod HttpMethod { get; set; }

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06001AC5 RID: 6853 RVA: 0x00056E2E File Offset: 0x0005502E
		// (set) Token: 0x06001AC6 RID: 6854 RVA: 0x00056E36 File Offset: 0x00055036
		public string HttpHost { get; set; }

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06001AC7 RID: 6855 RVA: 0x00056E3F File Offset: 0x0005503F
		// (set) Token: 0x06001AC8 RID: 6856 RVA: 0x00056E47 File Offset: 0x00055047
		public string HttpPath { get; set; }

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06001AC9 RID: 6857 RVA: 0x00056E50 File Offset: 0x00055050
		// (set) Token: 0x06001ACA RID: 6858 RVA: 0x00056E58 File Offset: 0x00055058
		public IPoPCryptoProvider PopCryptoProvider { get; set; }

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06001ACB RID: 6859 RVA: 0x00056E61 File Offset: 0x00055061
		// (set) Token: 0x06001ACC RID: 6860 RVA: 0x00056E69 File Offset: 0x00055069
		public string Nonce { get; set; }

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001ACD RID: 6861 RVA: 0x00056E72 File Offset: 0x00055072
		// (set) Token: 0x06001ACE RID: 6862 RVA: 0x00056E7A File Offset: 0x0005507A
		public bool SignHttpRequest { get; set; } = true;
	}
}
