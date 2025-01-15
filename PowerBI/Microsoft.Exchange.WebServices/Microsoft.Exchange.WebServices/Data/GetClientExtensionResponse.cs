using System;
using System.Collections.ObjectModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000160 RID: 352
	public sealed class GetClientExtensionResponse : ServiceResponse
	{
		// Token: 0x06001089 RID: 4233 RVA: 0x00030BA6 File Offset: 0x0002FBA6
		internal GetClientExtensionResponse()
		{
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x0600108A RID: 4234 RVA: 0x00030BB9 File Offset: 0x0002FBB9
		public Collection<ClientExtension> ClientExtensions
		{
			get
			{
				return this.clientExtension;
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x0600108B RID: 4235 RVA: 0x00030BC1 File Offset: 0x0002FBC1
		public string RawMasterTableXml
		{
			get
			{
				return this.rawMasterTableXml;
			}
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x00030BCC File Offset: 0x0002FBCC
		internal override void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
			this.ClientExtensions.Clear();
			base.ReadElementsFromXml(reader);
			reader.ReadStartElement(XmlNamespace.Messages, "ClientExtensions");
			if (!reader.IsEmptyElement)
			{
				reader.Read();
				while (reader.IsStartElement(XmlNamespace.Types, "ClientExtension"))
				{
					ClientExtension clientExtension = new ClientExtension();
					clientExtension.LoadFromXml(reader, XmlNamespace.Types, "ClientExtension");
					this.ClientExtensions.Add(clientExtension);
					reader.EnsureCurrentNodeIsEndElement(XmlNamespace.Types, "ClientExtension");
					reader.Read();
				}
				reader.EnsureCurrentNodeIsEndElement(XmlNamespace.Messages, "ClientExtensions");
			}
			reader.Read();
			if (reader.IsStartElement(XmlNamespace.Messages, "RawMasterTableXml"))
			{
				this.rawMasterTableXml = reader.ReadElementValue();
			}
		}

		// Token: 0x040009AD RID: 2477
		private Collection<ClientExtension> clientExtension = new Collection<ClientExtension>();

		// Token: 0x040009AE RID: 2478
		private string rawMasterTableXml;
	}
}
