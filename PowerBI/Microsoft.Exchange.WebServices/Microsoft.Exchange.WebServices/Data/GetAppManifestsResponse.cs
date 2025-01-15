using System;
using System.Collections.ObjectModel;
using System.Xml;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000162 RID: 354
	internal sealed class GetAppManifestsResponse : ServiceResponse
	{
		// Token: 0x06001093 RID: 4243 RVA: 0x00030CF8 File Offset: 0x0002FCF8
		internal GetAppManifestsResponse()
		{
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06001094 RID: 4244 RVA: 0x00030D16 File Offset: 0x0002FD16
		public Collection<XmlDocument> Manifests
		{
			get
			{
				return this.manifests;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06001095 RID: 4245 RVA: 0x00030D1E File Offset: 0x0002FD1E
		public Collection<ClientApp> Apps
		{
			get
			{
				return this.apps;
			}
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x00030D28 File Offset: 0x0002FD28
		internal override void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
			this.Manifests.Clear();
			base.ReadElementsFromXml(reader);
			reader.Read(1);
			bool flag;
			if ("Manifests".Equals(reader.LocalName))
			{
				flag = true;
			}
			else
			{
				if (!"Apps".Equals(reader.LocalName))
				{
					throw new ServiceXmlDeserializationException(string.Format(Strings.UnexpectedElement, new object[]
					{
						EwsUtilities.GetNamespacePrefix(XmlNamespace.Messages),
						"Manifests",
						1,
						reader.LocalName,
						reader.NodeType
					}));
				}
				flag = false;
			}
			if (!reader.IsEmptyElement)
			{
				reader.Read();
				if (flag)
				{
					this.ReadFromExchange2013(reader);
				}
				else
				{
					this.ReadFromExchange2013Sp1(reader);
				}
			}
			reader.EnsureCurrentNodeIsEndElement(XmlNamespace.Messages, flag ? "Manifests" : "Apps");
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x00030E00 File Offset: 0x0002FE00
		private void ReadFromExchange2013(EwsServiceXmlReader reader)
		{
			while (reader.IsStartElement(XmlNamespace.Messages, "Manifest"))
			{
				XmlDocument xmlDocument = ClientApp.ReadToXmlDocument(reader);
				this.Manifests.Add(xmlDocument);
				this.Apps.Add(new ClientApp
				{
					Manifest = xmlDocument
				});
			}
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x00030E4C File Offset: 0x0002FE4C
		private void ReadFromExchange2013Sp1(EwsServiceXmlReader reader)
		{
			while (reader.IsStartElement(XmlNamespace.Types, "App"))
			{
				ClientApp clientApp = new ClientApp();
				clientApp.LoadFromXml(reader, "App");
				this.Apps.Add(clientApp);
				this.Manifests.Add(clientApp.Manifest);
				reader.EnsureCurrentNodeIsEndElement(XmlNamespace.Types, "App");
				reader.Read();
			}
		}

		// Token: 0x040009B3 RID: 2483
		private Collection<XmlDocument> manifests = new Collection<XmlDocument>();

		// Token: 0x040009B4 RID: 2484
		private Collection<ClientApp> apps = new Collection<ClientApp>();
	}
}
