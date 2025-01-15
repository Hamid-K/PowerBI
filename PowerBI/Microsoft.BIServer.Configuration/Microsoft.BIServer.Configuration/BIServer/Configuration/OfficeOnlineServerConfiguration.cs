using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;

namespace Microsoft.BIServer.Configuration
{
	// Token: 0x02000009 RID: 9
	public class OfficeOnlineServerConfiguration
	{
		// Token: 0x06000014 RID: 20 RVA: 0x000024C8 File Offset: 0x000006C8
		public OfficeOnlineServerConfiguration(Uri officeOnlineDiscoveryUrl)
		{
			this._officeOnlineDiscoveryUrl = officeOnlineDiscoveryUrl;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000024D8 File Offset: 0x000006D8
		private async Task<XmlDocument> GetDocument()
		{
			XmlDocument xmlDocument2;
			using (HttpClient client = new HttpClient())
			{
				XmlDocument xmldoc = new XmlDocument();
				XmlDocument xmlDocument = xmldoc;
				string text = await client.GetStringAsync(this._officeOnlineDiscoveryUrl);
				xmlDocument.LoadXml(text);
				xmlDocument = null;
				xmlDocument2 = xmldoc;
			}
			return xmlDocument2;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002520 File Offset: 0x00000720
		public async Task<Uri> GetExcelWopiUrl()
		{
			XmlNode highestPriorityNetZoneNode = OfficeOnlineServerConfiguration.GetHighestPriorityNetZoneNode(await this.GetDocument());
			Uri uri;
			if (highestPriorityNetZoneNode != null)
			{
				uri = OfficeOnlineServerConfiguration.GetExcelWopiUrl(highestPriorityNetZoneNode);
			}
			else
			{
				uri = null;
			}
			return uri;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002568 File Offset: 0x00000768
		public static Uri GetExcelWopiUrl(XmlNode netZoneNode)
		{
			XmlNode xmlNode = netZoneNode.SelectSingleNode("app[@name='Excel']/action[@name='view'][@ext='xlsx']/@urlsrc");
			if (xmlNode != null)
			{
				return new Uri(new Uri(xmlNode.Value).GetLeftPart(UriPartial.Path));
			}
			return null;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000259C File Offset: 0x0000079C
		public static XmlNode GetHighestPriorityNetZoneNode(XmlDocument xmlDoc)
		{
			return (from XmlNode node in xmlDoc.SelectNodes("//net-zone")
				orderby OfficeOnlineServerConfiguration.NetZonePriorityMap[node.Attributes["name"].Value]
				select node).FirstOrDefault<XmlNode>();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000025D8 File Offset: 0x000007D8
		public async Task<OfficeProofKeyValues> GetProofKeyValues()
		{
			XmlNode proofKeyNode = OfficeOnlineServerConfiguration.GetProofKeyNode(await this.GetDocument());
			OfficeProofKeyValues officeProofKeyValues;
			if (proofKeyNode != null)
			{
				officeProofKeyValues = OfficeOnlineServerConfiguration.GetProofKeyValues(proofKeyNode);
			}
			else
			{
				officeProofKeyValues = null;
			}
			return officeProofKeyValues;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000261D File Offset: 0x0000081D
		public static OfficeProofKeyValues GetProofKeyValues(XmlNode proofKeyNode)
		{
			return new OfficeProofKeyValues
			{
				Value = proofKeyNode.Attributes["value"].Value,
				OldValue = proofKeyNode.Attributes["oldvalue"].Value
			};
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000265A File Offset: 0x0000085A
		public static XmlNode GetProofKeyNode(XmlDocument xmlDoc)
		{
			return xmlDoc.SelectSingleNode("//wopi-discovery/proof-key");
		}

		// Token: 0x04000034 RID: 52
		private static readonly Dictionary<string, int> NetZonePriorityMap = new Dictionary<string, int>
		{
			{ "external-https", 0 },
			{ "external-http", 1 },
			{ "internal-https", 2 },
			{ "internal-http", 3 }
		};

		// Token: 0x04000035 RID: 53
		private readonly Uri _officeOnlineDiscoveryUrl;
	}
}
