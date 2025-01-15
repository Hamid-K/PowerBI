using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200001B RID: 27
	[Serializable]
	public class RecordBindingCollection : ListWithDefault<RecordBinding>, IXmlSerializable
	{
		// Token: 0x060000B5 RID: 181 RVA: 0x000044FC File Offset: 0x000026FC
		public RecordBindingCollection()
			: base((RecordBinding b) => b.Name)
		{
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004523 File Offset: 0x00002723
		public RecordBindingCollection(XmlReader reader)
			: this()
		{
			this.ReadXml(reader);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004534 File Offset: 0x00002734
		public bool TryGetRecordBindingForRowset(string rowsetName, out RecordBinding recordBinding)
		{
			recordBinding = null;
			foreach (RecordBinding recordBinding2 in this)
			{
				if (rowsetName.Equals(recordBinding2.RowsetName))
				{
					recordBinding = recordBinding2;
					return true;
				}
			}
			foreach (RecordBinding recordBinding3 in this)
			{
				if ((string.IsNullOrEmpty(rowsetName) || string.Equals(rowsetName, "default", 1)) && (string.IsNullOrEmpty(recordBinding3.RowsetName) || string.Equals(recordBinding3.RowsetName, "default", 1)))
				{
					recordBinding = recordBinding3;
					return true;
				}
			}
			if ((string.IsNullOrEmpty(rowsetName) || string.Equals(rowsetName, "default", 1)) && base.Count > 0)
			{
				recordBinding = base[0];
				return true;
			}
			return false;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004638 File Offset: 0x00002838
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x0000463C File Offset: 0x0000283C
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			reader = FuzzyLookupXmlBuilder.CreateValidatingXmlReader(reader);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(reader);
			XmlNamespaceManager xmlNamespaceManager = xmlDocument.CreateFL3XmlNamespaceManager();
			XmlNode xmlNode = xmlDocument.SelectSingleNode("/*/ns:RecordBinding", xmlNamespaceManager);
			if (xmlNode != null)
			{
				foreach (object obj in xmlNode.SelectNodes("//ns:RecordBinding", xmlNamespaceManager))
				{
					XmlNode xmlNode2 = (XmlNode)obj;
					base.Add(new RecordBinding(new XmlNodeReader(xmlNode2)));
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

		// Token: 0x060000BA RID: 186 RVA: 0x0000470C File Offset: 0x0000290C
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("RecordBinding");
			if (!string.IsNullOrEmpty(base.Default))
			{
				writer.WriteAttributeString("default", base.Default);
			}
			foreach (RecordBinding recordBinding in this)
			{
				recordBinding.WriteXml(writer);
			}
			writer.WriteEndElement();
		}
	}
}
