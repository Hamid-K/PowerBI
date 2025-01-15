using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000028 RID: 40
	[Serializable]
	public class TransformationProvider : ObjectDefinition, IXmlSerializable
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00005516 File Offset: 0x00003716
		// (set) Token: 0x0600011F RID: 287 RVA: 0x0000551E File Offset: 0x0000371E
		public List<TransformationFilter> TransformationFilters { get; set; }

		// Token: 0x06000120 RID: 288 RVA: 0x00005527 File Offset: 0x00003727
		public TransformationProvider()
		{
			this.TransformationFilters = new List<TransformationFilter>();
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000553A File Offset: 0x0000373A
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00005540 File Offset: 0x00003740
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			reader = FuzzyLookupXmlBuilder.CreateValidatingXmlReader(reader);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(reader);
			XmlNamespaceManager xmlNamespaceManager = xmlDocument.CreateFL3XmlNamespaceManager();
			XmlNode xmlNode = xmlDocument.SelectSingleNode("/ns:TransformationProvider", xmlNamespaceManager);
			if (xmlNode != null)
			{
				base.Name = xmlNode.Attributes.GetNamedItemOrDefault("name", null);
				XmlNode xmlNode2;
				if ((xmlNode2 = xmlNode.Attributes.GetNamedItem("assemblyName")) != null)
				{
					base.AssemblyName = xmlNode2.Value;
				}
				if ((xmlNode2 = xmlNode.Attributes.GetNamedItem("typeName")) != null)
				{
					base.TypeName = xmlNode2.Value;
				}
				if ((xmlNode2 = xmlNode.Attributes.GetNamedItem("isReference")) != null)
				{
					base.IsReference = bool.Parse(xmlNode2.Value);
				}
				if (base.IsReference && (string.IsNullOrEmpty(base.Name) || !string.IsNullOrEmpty(base.AssemblyName) || !string.IsNullOrEmpty(base.TypeName)))
				{
					throw new XmlException("If isReference attribute is true, the name attribute must be defined and the assemblyName and typeName attributes must be empty.");
				}
				if ((xmlNode2 = xmlNode.SelectSingleNode("/*/ns:Properties", xmlNamespaceManager)) != null)
				{
					CollectionSerialization.ReadXml<Property>(new XmlNodeReader(xmlNode2), "Properties", base.Properties);
				}
				if ((xmlNode2 = xmlNode.SelectSingleNode("/*/ns:TransformationFilters", xmlNamespaceManager)) != null)
				{
					CollectionSerialization.ReadXml<TransformationFilter>(new XmlNodeReader(xmlNode2), "TransformationFilters", this.TransformationFilters);
				}
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000567C File Offset: 0x0000387C
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("TransformationProvider");
			if (!string.IsNullOrEmpty(base.Name))
			{
				writer.WriteAttributeString("name", base.Name);
			}
			if (!string.IsNullOrEmpty(base.AssemblyName))
			{
				writer.WriteAttributeString("assemblyName", base.AssemblyName);
			}
			if (!string.IsNullOrEmpty(base.TypeName))
			{
				writer.WriteAttributeString("typeName", base.TypeName);
			}
			writer.WriteAttributeString("isReference", base.IsReference.ToString());
			CollectionSerialization.WriteXml<Property>(writer, "Properties", base.Properties);
			CollectionSerialization.WriteXml<TransformationFilter>(writer, "TransformationFilters", this.TransformationFilters);
			writer.WriteEndElement();
		}
	}
}
