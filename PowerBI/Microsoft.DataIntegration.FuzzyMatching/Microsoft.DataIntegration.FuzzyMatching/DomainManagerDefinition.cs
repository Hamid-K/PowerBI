using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200002B RID: 43
	[Serializable]
	public class DomainManagerDefinition : IXmlSerializable, IName
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00005B4D File Offset: 0x00003D4D
		// (set) Token: 0x0600013E RID: 318 RVA: 0x00005B55 File Offset: 0x00003D55
		public string Name { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00005B5E File Offset: 0x00003D5E
		// (set) Token: 0x06000140 RID: 320 RVA: 0x00005B66 File Offset: 0x00003D66
		public string Description { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00005B6F File Offset: 0x00003D6F
		// (set) Token: 0x06000142 RID: 322 RVA: 0x00005B77 File Offset: 0x00003D77
		public List<Property> Properties { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00005B80 File Offset: 0x00003D80
		// (set) Token: 0x06000144 RID: 324 RVA: 0x00005B88 File Offset: 0x00003D88
		public string CreationConnectionName { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00005B91 File Offset: 0x00003D91
		// (set) Token: 0x06000146 RID: 326 RVA: 0x00005B99 File Offset: 0x00003D99
		public TokenIdProvider TokenIdProvider { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00005BA2 File Offset: 0x00003DA2
		// (set) Token: 0x06000148 RID: 328 RVA: 0x00005BAA File Offset: 0x00003DAA
		public List<Domain> Domains { get; set; }

		// Token: 0x06000149 RID: 329 RVA: 0x00005BB3 File Offset: 0x00003DB3
		public DomainManagerDefinition()
		{
			this.Properties = new List<Property>();
			this.Domains = new List<Domain>();
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00005BD1 File Offset: 0x00003DD1
		public DomainManagerDefinition(XmlReader reader)
			: this()
		{
			this.ReadXml(reader);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00005BE0 File Offset: 0x00003DE0
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00005BE4 File Offset: 0x00003DE4
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			reader = FuzzyLookupXmlBuilder.CreateValidatingXmlReader(reader);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(reader);
			XmlNamespaceManager xmlNamespaceManager = xmlDocument.CreateFL3XmlNamespaceManager();
			XmlNode xmlNode = xmlDocument.SelectSingleNode("/ns:DomainManager", xmlNamespaceManager);
			if (xmlNode != null)
			{
				this.Name = xmlNode.Attributes.GetNamedItemOrDefault("name", null);
				this.Description = xmlNode.Attributes.GetNamedItemOrDefault("description", null);
				XmlNode xmlNode2;
				if ((xmlNode2 = xmlNode.Attributes.GetNamedItem("creationConnectionName")) != null)
				{
					this.CreationConnectionName = xmlNode2.Value;
				}
				if ((xmlNode2 = xmlNode.SelectSingleNode("/*/ns:TokenIdProvider", xmlNamespaceManager)) != null)
				{
					this.TokenIdProvider = new TokenIdProvider();
					this.TokenIdProvider.ReadXml(new XmlNodeReader(xmlNode2));
				}
				if ((xmlNode2 = xmlNode.SelectSingleNode("/*/ns:Properties", xmlNamespaceManager)) != null)
				{
					CollectionSerialization.ReadXml<Property>(new XmlNodeReader(xmlNode2), "Properties", this.Properties);
				}
				if ((xmlNode2 = xmlNode.SelectSingleNode("/*/ns:Domains", xmlNamespaceManager)) != null)
				{
					CollectionSerialization.ReadXml<Domain>(new XmlNodeReader(xmlNode2), "Domains", this.Domains);
				}
			}
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00005CE4 File Offset: 0x00003EE4
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("DomainManager");
			if (!string.IsNullOrEmpty(this.Name))
			{
				writer.WriteAttributeString("name", this.Name);
			}
			if (!string.IsNullOrEmpty(this.Description))
			{
				writer.WriteAttributeString("description", this.Description);
			}
			if (!string.IsNullOrEmpty(this.CreationConnectionName))
			{
				writer.WriteAttributeString("creationConnectionName", this.CreationConnectionName);
			}
			if (this.TokenIdProvider != null)
			{
				this.TokenIdProvider.WriteXml(writer);
			}
			CollectionSerialization.WriteXml<Property>(writer, "Properties", this.Properties);
			CollectionSerialization.WriteXml<Domain>(writer, "Domains", this.Domains);
			writer.WriteEndElement();
		}
	}
}
