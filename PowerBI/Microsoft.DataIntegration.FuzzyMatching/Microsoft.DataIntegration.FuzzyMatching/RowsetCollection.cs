using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200001A RID: 26
	[Serializable]
	public class RowsetCollection : ListWithDefault<IRowsetDefinition>, IXmlSerializable
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00004336 File Offset: 0x00002536
		// (set) Token: 0x060000AF RID: 175 RVA: 0x0000433E File Offset: 0x0000253E
		public IRowsetFactory RowsetFactory { get; set; }

		// Token: 0x060000B0 RID: 176 RVA: 0x00004347 File Offset: 0x00002547
		public RowsetCollection()
			: base((IRowsetDefinition d) => d.Name)
		{
			this.RowsetFactory = new DefaultRowsetFactory();
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004379 File Offset: 0x00002579
		public RowsetCollection(XmlReader reader)
			: this()
		{
			this.ReadXml(reader);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004388 File Offset: 0x00002588
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000438C File Offset: 0x0000258C
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			reader = FuzzyLookupXmlBuilder.CreateValidatingXmlReader(reader);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(reader);
			XmlNamespaceManager xmlNamespaceManager = xmlDocument.CreateFL3XmlNamespaceManager();
			XmlNode xmlNode = xmlDocument.SelectSingleNode("/ns:Rowsets", xmlNamespaceManager);
			if (xmlNode != null)
			{
				foreach (object obj in xmlNode.SelectNodes("./*", xmlNamespaceManager))
				{
					XmlNode xmlNode2 = (XmlNode)obj;
					IRowsetDefinition rowsetDefinition;
					if (!this.RowsetFactory.TryCreate(xmlNode2, out rowsetDefinition))
					{
						throw new Exception(string.Format("Unable to instantiate rowset type: {0}", xmlNode2.Name));
					}
					base.Add(rowsetDefinition);
				}
				XmlNode namedItem;
				if ((namedItem = xmlNode.Attributes.GetNamedItem("default")) != null)
				{
					base.Default = namedItem.Value;
					return;
				}
				if (base.Count > 0)
				{
					base.Default = "0";
				}
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000447C File Offset: 0x0000267C
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("Rowsets");
			if (!string.IsNullOrEmpty(base.Default))
			{
				writer.WriteAttributeString("default", base.Default);
			}
			foreach (IRowsetDefinition rowsetDefinition in this)
			{
				((RowsetDefinition)rowsetDefinition).WriteXml(writer);
			}
			writer.WriteEndElement();
		}
	}
}
