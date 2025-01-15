using System;
using System.IO;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000041 RID: 65
	public sealed class ClientExtension : ComplexProperty
	{
		// Token: 0x060002E6 RID: 742 RVA: 0x0000B99F File Offset: 0x0000A99F
		internal ClientExtension()
		{
			base.Namespace = XmlNamespace.Types;
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000B9B0 File Offset: 0x0000A9B0
		public ClientExtension(ExtensionType type, ExtensionInstallScope scope, Stream manifestStream, string marketplaceAssetID, string marketplaceContentMarket, bool isAvailable, bool isMandatory, bool isEnabledByDefault, ClientExtensionProvidedTo providedTo, StringList specificUsers, string appStatus, string etoken)
			: this()
		{
			this.Type = type;
			this.Scope = scope;
			this.ManifestStream = manifestStream;
			this.MarketplaceAssetID = marketplaceAssetID;
			this.MarketplaceContentMarket = marketplaceContentMarket;
			this.IsAvailable = isAvailable;
			this.IsMandatory = isMandatory;
			this.IsEnabledByDefault = isEnabledByDefault;
			this.ProvidedTo = providedTo;
			this.SpecificUsers = specificUsers;
			this.AppStatus = appStatus;
			this.Etoken = etoken;
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000BA20 File Offset: 0x0000AA20
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x0000BA28 File Offset: 0x0000AA28
		public ExtensionType Type { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000BA31 File Offset: 0x0000AA31
		// (set) Token: 0x060002EB RID: 747 RVA: 0x0000BA39 File Offset: 0x0000AA39
		public ExtensionInstallScope Scope { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0000BA42 File Offset: 0x0000AA42
		// (set) Token: 0x060002ED RID: 749 RVA: 0x0000BA4A File Offset: 0x0000AA4A
		public Stream ManifestStream { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000BA53 File Offset: 0x0000AA53
		// (set) Token: 0x060002EF RID: 751 RVA: 0x0000BA5B File Offset: 0x0000AA5B
		public string MarketplaceAssetID { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000BA64 File Offset: 0x0000AA64
		// (set) Token: 0x060002F1 RID: 753 RVA: 0x0000BA6C File Offset: 0x0000AA6C
		public string MarketplaceContentMarket { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x0000BA75 File Offset: 0x0000AA75
		// (set) Token: 0x060002F3 RID: 755 RVA: 0x0000BA7D File Offset: 0x0000AA7D
		public string AppStatus { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000BA86 File Offset: 0x0000AA86
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x0000BA8E File Offset: 0x0000AA8E
		public string Etoken { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000BA97 File Offset: 0x0000AA97
		// (set) Token: 0x060002F7 RID: 759 RVA: 0x0000BA9F File Offset: 0x0000AA9F
		public bool IsAvailable { get; set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x0000BAA8 File Offset: 0x0000AAA8
		// (set) Token: 0x060002F9 RID: 761 RVA: 0x0000BAB0 File Offset: 0x0000AAB0
		public bool IsMandatory { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002FA RID: 762 RVA: 0x0000BAB9 File Offset: 0x0000AAB9
		// (set) Token: 0x060002FB RID: 763 RVA: 0x0000BAC1 File Offset: 0x0000AAC1
		public bool IsEnabledByDefault { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0000BACA File Offset: 0x0000AACA
		// (set) Token: 0x060002FD RID: 765 RVA: 0x0000BAD2 File Offset: 0x0000AAD2
		public ClientExtensionProvidedTo ProvidedTo { get; set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0000BADB File Offset: 0x0000AADB
		// (set) Token: 0x060002FF RID: 767 RVA: 0x0000BAE3 File Offset: 0x0000AAE3
		public StringList SpecificUsers { get; set; }

		// Token: 0x06000300 RID: 768 RVA: 0x0000BAEC File Offset: 0x0000AAEC
		internal override void ReadAttributesFromXml(EwsServiceXmlReader reader)
		{
			string text = reader.ReadAttributeValue("Type");
			if (!string.IsNullOrEmpty(text))
			{
				this.Type = reader.ReadAttributeValue<ExtensionType>("Type");
			}
			text = reader.ReadAttributeValue("Scope");
			if (!string.IsNullOrEmpty(text))
			{
				this.Scope = reader.ReadAttributeValue<ExtensionInstallScope>("Scope");
			}
			text = reader.ReadAttributeValue("MarketplaceAssetId");
			if (!string.IsNullOrEmpty(text))
			{
				this.MarketplaceAssetID = reader.ReadAttributeValue<string>("MarketplaceAssetId");
			}
			text = reader.ReadAttributeValue("MarketplaceContentMarket");
			if (!string.IsNullOrEmpty(text))
			{
				this.MarketplaceContentMarket = reader.ReadAttributeValue<string>("MarketplaceContentMarket");
			}
			text = reader.ReadAttributeValue("AppStatus");
			if (!string.IsNullOrEmpty(text))
			{
				this.AppStatus = reader.ReadAttributeValue<string>("AppStatus");
			}
			text = reader.ReadAttributeValue("Etoken");
			if (!string.IsNullOrEmpty(text))
			{
				this.Etoken = reader.ReadAttributeValue<string>("Etoken");
			}
			text = reader.ReadAttributeValue("IsAvailable");
			if (!string.IsNullOrEmpty(text))
			{
				this.IsAvailable = reader.ReadAttributeValue<bool>("IsAvailable");
			}
			text = reader.ReadAttributeValue("IsMandatory");
			if (!string.IsNullOrEmpty(text))
			{
				this.IsMandatory = reader.ReadAttributeValue<bool>("IsMandatory");
			}
			text = reader.ReadAttributeValue("IsEnabledByDefault");
			if (!string.IsNullOrEmpty(text))
			{
				this.IsEnabledByDefault = reader.ReadAttributeValue<bool>("IsEnabledByDefault");
			}
			text = reader.ReadAttributeValue("ProvidedTo");
			if (!string.IsNullOrEmpty(text))
			{
				this.ProvidedTo = reader.ReadAttributeValue<ClientExtensionProvidedTo>("ProvidedTo");
			}
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000BC6C File Offset: 0x0000AC6C
		internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteAttributeValue("Type", this.Type);
			writer.WriteAttributeValue("Scope", this.Scope);
			writer.WriteAttributeValue("MarketplaceAssetId", this.MarketplaceAssetID);
			writer.WriteAttributeValue("MarketplaceContentMarket", this.MarketplaceContentMarket);
			writer.WriteAttributeValue("AppStatus", this.AppStatus);
			writer.WriteAttributeValue("Etoken", this.Etoken);
			writer.WriteAttributeValue("IsAvailable", this.IsAvailable);
			writer.WriteAttributeValue("IsMandatory", this.IsMandatory);
			writer.WriteAttributeValue("IsEnabledByDefault", this.IsEnabledByDefault);
			writer.WriteAttributeValue("ProvidedTo", this.ProvidedTo);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000BD44 File Offset: 0x0000AD44
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (localName == "Manifest")
				{
					this.ManifestStream = new MemoryStream();
					reader.ReadBase64ElementValue(this.ManifestStream);
					this.ManifestStream.Position = 0L;
					return true;
				}
				if (localName == "SpecificUsers")
				{
					this.SpecificUsers = new StringList();
					this.SpecificUsers.LoadFromXml(reader, XmlNamespace.Types, "SpecificUsers");
					return true;
				}
			}
			return base.TryReadElementFromXml(reader);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000BDC4 File Offset: 0x0000ADC4
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			if (this.SpecificUsers != null)
			{
				writer.WriteStartElement(XmlNamespace.Types, "SpecificUsers");
				this.SpecificUsers.WriteElementsToXml(writer);
				writer.WriteEndElement();
			}
			if (this.ManifestStream != null)
			{
				if (this.ManifestStream.CanSeek)
				{
					this.ManifestStream.Position = 0L;
				}
				writer.WriteStartElement(XmlNamespace.Types, "Manifest");
				writer.WriteBase64ElementValue(this.ManifestStream);
				writer.WriteEndElement();
			}
		}
	}
}
