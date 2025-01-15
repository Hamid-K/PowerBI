using System;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.WebServices.Autodiscover
{
	// Token: 0x02000020 RID: 32
	public sealed class WebClientUrl
	{
		// Token: 0x0600015A RID: 346 RVA: 0x00007079 File Offset: 0x00006079
		private WebClientUrl()
		{
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00007081 File Offset: 0x00006081
		internal WebClientUrl(string authenticationMethods, string url)
		{
			this.authenticationMethods = authenticationMethods;
			this.url = url;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00007098 File Offset: 0x00006098
		internal static WebClientUrl LoadFromXml(EwsXmlReader reader)
		{
			WebClientUrl webClientUrl = new WebClientUrl();
			do
			{
				reader.Read();
				string localName;
				if (reader.NodeType == 1 && (localName = reader.LocalName) != null)
				{
					if (!(localName == "AuthenticationMethods"))
					{
						if (localName == "Url")
						{
							webClientUrl.Url = reader.ReadElementValue<string>();
						}
					}
					else
					{
						webClientUrl.AuthenticationMethods = reader.ReadElementValue<string>();
					}
				}
			}
			while (!reader.IsEndElement(XmlNamespace.Autodiscover, "WebClientUrl"));
			return webClientUrl;
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600015D RID: 349 RVA: 0x0000710A File Offset: 0x0000610A
		// (set) Token: 0x0600015E RID: 350 RVA: 0x00007112 File Offset: 0x00006112
		public string AuthenticationMethods
		{
			get
			{
				return this.authenticationMethods;
			}
			internal set
			{
				this.authenticationMethods = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600015F RID: 351 RVA: 0x0000711B File Offset: 0x0000611B
		// (set) Token: 0x06000160 RID: 352 RVA: 0x00007123 File Offset: 0x00006123
		public string Url
		{
			get
			{
				return this.url;
			}
			internal set
			{
				this.url = value;
			}
		}

		// Token: 0x0400007C RID: 124
		private string authenticationMethods;

		// Token: 0x0400007D RID: 125
		private string url;
	}
}
