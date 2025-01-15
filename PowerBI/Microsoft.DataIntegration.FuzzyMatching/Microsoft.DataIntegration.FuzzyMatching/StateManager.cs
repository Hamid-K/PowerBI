using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200002E RID: 46
	[Serializable]
	public class StateManager : ObjectDefinition, IXmlSerializable
	{
		// Token: 0x06000168 RID: 360 RVA: 0x0000630C File Offset: 0x0000450C
		public StateManager()
		{
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00006314 File Offset: 0x00004514
		public StateManager(XmlReader reader)
		{
			this.ReadXml(reader);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00006323 File Offset: 0x00004523
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00006328 File Offset: 0x00004528
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(FuzzyLookupXmlBuilder.CreateValidatingXmlReader(reader));
			XmlNamespaceManager xmlNamespaceManager = xmlDocument.CreateFL3XmlNamespaceManager();
			XmlNode xmlNode;
			if ((xmlNode = xmlDocument.SelectSingleNode("/ns:StateManager", xmlNamespaceManager)) != null)
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
				if ((xmlNode2 = xmlNode.SelectSingleNode("/*/ns:Properties", xmlNamespaceManager)) != null)
				{
					CollectionSerialization.ReadXml<Property>(new XmlNodeReader(xmlNode2), "Properties", base.Properties);
				}
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000063DC File Offset: 0x000045DC
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("StateManager");
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
			CollectionSerialization.WriteXml<Property>(writer, "Properties", base.Properties);
			writer.WriteEndElement();
		}
	}
}
