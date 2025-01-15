using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000158 RID: 344
	internal sealed class GetAppMarketplaceUrlResponse : ServiceResponse
	{
		// Token: 0x0600105C RID: 4188 RVA: 0x0002FDAE File Offset: 0x0002EDAE
		internal GetAppMarketplaceUrlResponse()
		{
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x0002FDB6 File Offset: 0x0002EDB6
		internal override void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
			base.ReadElementsFromXml(reader);
			this.appMarketplaceUrl = reader.ReadElementValue<string>(XmlNamespace.NotSpecified, "AppMarketplaceUrl");
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x0600105E RID: 4190 RVA: 0x0002FDD1 File Offset: 0x0002EDD1
		public string AppMarketplaceUrl
		{
			get
			{
				return this.appMarketplaceUrl;
			}
		}

		// Token: 0x0400099E RID: 2462
		private string appMarketplaceUrl;
	}
}
