using System;
using System.Collections.Generic;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.WebServices.Autodiscover
{
	// Token: 0x02000021 RID: 33
	public sealed class WebClientUrlCollection
	{
		// Token: 0x06000161 RID: 353 RVA: 0x0000712C File Offset: 0x0000612C
		internal WebClientUrlCollection()
		{
			this.urls = new List<WebClientUrl>();
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00007140 File Offset: 0x00006140
		internal static WebClientUrlCollection LoadFromXml(EwsXmlReader reader)
		{
			WebClientUrlCollection webClientUrlCollection = new WebClientUrlCollection();
			do
			{
				reader.Read();
				if (reader.NodeType == 1 && reader.LocalName == "WebClientUrl")
				{
					webClientUrlCollection.Urls.Add(WebClientUrl.LoadFromXml(reader));
				}
			}
			while (!reader.IsEndElement(XmlNamespace.Autodiscover, "WebClientUrls"));
			return webClientUrlCollection;
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00007195 File Offset: 0x00006195
		public List<WebClientUrl> Urls
		{
			get
			{
				return this.urls;
			}
		}

		// Token: 0x0400007E RID: 126
		private List<WebClientUrl> urls;
	}
}
