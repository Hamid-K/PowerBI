using System;
using System.Net;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020001D3 RID: 467
	public sealed class WebCredentials : ExchangeCredentials
	{
		// Token: 0x06001544 RID: 5444 RVA: 0x0003BBCE File Offset: 0x0003ABCE
		public WebCredentials()
			: this(CredentialCache.DefaultNetworkCredentials)
		{
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x0003BBDB File Offset: 0x0003ABDB
		public WebCredentials(ICredentials credentials)
		{
			EwsUtilities.ValidateParam(credentials, "credentials");
			this.credentials = credentials;
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x0003BBF5 File Offset: 0x0003ABF5
		public WebCredentials(string username, string password)
			: this(new NetworkCredential(username, password))
		{
		}

		// Token: 0x06001547 RID: 5447 RVA: 0x0003BC04 File Offset: 0x0003AC04
		public WebCredentials(string username, string password, string domain)
			: this(new NetworkCredential(username, password, domain))
		{
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x0003BC14 File Offset: 0x0003AC14
		internal override void PrepareWebRequest(IEwsHttpWebRequest request)
		{
			request.Credentials = this.credentials;
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06001549 RID: 5449 RVA: 0x0003BC22 File Offset: 0x0003AC22
		public ICredentials Credentials
		{
			get
			{
				return this.credentials;
			}
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x0003BC2A File Offset: 0x0003AC2A
		internal override Uri AdjustUrl(Uri url)
		{
			return url;
		}

		// Token: 0x04000CE0 RID: 3296
		private ICredentials credentials;
	}
}
