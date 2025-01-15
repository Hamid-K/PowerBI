using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000039 RID: 57
	[Serializable]
	public class QueryDefinition : IXmlSerializable
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600020A RID: 522 RVA: 0x00009AB4 File Offset: 0x00007CB4
		// (set) Token: 0x0600020B RID: 523 RVA: 0x00009ABC File Offset: 0x00007CBC
		public string Name { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600020C RID: 524 RVA: 0x00009AC5 File Offset: 0x00007CC5
		// (set) Token: 0x0600020D RID: 525 RVA: 0x00009ACD File Offset: 0x00007CCD
		public string Description { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600020E RID: 526 RVA: 0x00009AD6 File Offset: 0x00007CD6
		// (set) Token: 0x0600020F RID: 527 RVA: 0x00009ADE File Offset: 0x00007CDE
		public string IndexName { get; set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000210 RID: 528 RVA: 0x00009AE7 File Offset: 0x00007CE7
		// (set) Token: 0x06000211 RID: 529 RVA: 0x00009AEF File Offset: 0x00007CEF
		public string LookupName { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00009AF8 File Offset: 0x00007CF8
		// (set) Token: 0x06000213 RID: 531 RVA: 0x00009B00 File Offset: 0x00007D00
		public string RowsetName { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000214 RID: 532 RVA: 0x00009B09 File Offset: 0x00007D09
		// (set) Token: 0x06000215 RID: 533 RVA: 0x00009B11 File Offset: 0x00007D11
		public string CreationConnectionName { get; set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00009B1A File Offset: 0x00007D1A
		// (set) Token: 0x06000217 RID: 535 RVA: 0x00009B22 File Offset: 0x00007D22
		public List<Property> Properties { get; set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00009B2B File Offset: 0x00007D2B
		// (set) Token: 0x06000219 RID: 537 RVA: 0x00009B33 File Offset: 0x00007D33
		public Comparer Comparer { get; set; }

		// Token: 0x0600021A RID: 538 RVA: 0x00009B3C File Offset: 0x00007D3C
		public QueryDefinition()
		{
			this.RowsetName = string.Empty;
			this.Properties = new List<Property>();
			this.Comparer = new Comparer();
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00009B65 File Offset: 0x00007D65
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00009B68 File Offset: 0x00007D68
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			reader = FuzzyLookupXmlBuilder.CreateValidatingXmlReader(reader);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(reader);
			XmlNamespaceManager xmlNamespaceManager = xmlDocument.CreateFL3XmlNamespaceManager();
			XmlNode xmlNode = xmlDocument.SelectSingleNode("/ns:Query", xmlNamespaceManager);
			if (xmlNode != null)
			{
				this.Name = xmlNode.Attributes.GetNamedItemOrDefault("name", null);
				this.Description = xmlNode.Attributes.GetNamedItemOrDefault("description", null);
				XmlNode xmlNode2;
				if ((xmlNode2 = xmlNode.Attributes.GetNamedItem("indexName")) != null)
				{
					this.IndexName = xmlNode2.Value;
				}
				if ((xmlNode2 = xmlNode.Attributes.GetNamedItem("lookupName")) != null)
				{
					this.LookupName = xmlNode2.Value;
				}
				if ((xmlNode2 = xmlNode.Attributes.GetNamedItem("rowsetName")) != null)
				{
					this.RowsetName = xmlNode2.Value;
				}
				if ((xmlNode2 = xmlNode.Attributes.GetNamedItem("creationConnectionName")) != null)
				{
					this.CreationConnectionName = xmlNode2.Value;
				}
				if ((xmlNode2 = xmlNode.SelectSingleNode("/*/ns:Properties", xmlNamespaceManager)) != null)
				{
					CollectionSerialization.ReadXml<Property>(new XmlNodeReader(xmlNode2), "Properties", this.Properties);
				}
				if ((xmlNode2 = xmlNode.SelectSingleNode("/*/ns:Comparer", xmlNamespaceManager)) != null)
				{
					this.Comparer.ReadXml(new XmlNodeReader(xmlNode2));
				}
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00009C98 File Offset: 0x00007E98
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("Query");
			if (!string.IsNullOrEmpty(this.Name))
			{
				writer.WriteAttributeString("name", this.Name);
			}
			if (!string.IsNullOrEmpty(this.Description))
			{
				writer.WriteAttributeString("description", this.Description);
			}
			if (!string.IsNullOrEmpty(this.IndexName))
			{
				writer.WriteAttributeString("indexName", this.IndexName);
			}
			if (!string.IsNullOrEmpty(this.RowsetName))
			{
				writer.WriteAttributeString("rowsetName", this.RowsetName);
			}
			if (!string.IsNullOrEmpty(this.LookupName))
			{
				writer.WriteAttributeString("lookupName", this.LookupName);
			}
			if (!string.IsNullOrEmpty(this.CreationConnectionName))
			{
				writer.WriteAttributeString("creationConnectionName", this.CreationConnectionName);
			}
			CollectionSerialization.WriteXml<Property>(writer, "Properties", this.Properties);
			if (this.Comparer != null)
			{
				this.Comparer.WriteXml(writer);
			}
			writer.WriteEndElement();
		}
	}
}
