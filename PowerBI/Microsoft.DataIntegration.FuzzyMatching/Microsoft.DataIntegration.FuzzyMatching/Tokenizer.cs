using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200001F RID: 31
	[Serializable]
	public class Tokenizer : ObjectDefinition, IXmlSerializable
	{
		// Token: 0x060000DF RID: 223 RVA: 0x00004BEF File Offset: 0x00002DEF
		public Tokenizer()
		{
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004BF7 File Offset: 0x00002DF7
		public Tokenizer(XmlReader reader)
		{
			this.ReadXml(reader);
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00004C06 File Offset: 0x00002E06
		public static Tokenizer Default
		{
			get
			{
				return new Tokenizer
				{
					TypeName = "StringRecordTokenizer"
				};
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004C18 File Offset: 0x00002E18
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004C1C File Offset: 0x00002E1C
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			reader = FuzzyLookupXmlBuilder.CreateValidatingXmlReader(reader);
			if (reader.ReadToFollowing("Tokenizer"))
			{
				if (reader.MoveToAttribute("name"))
				{
					base.Name = reader.Value;
				}
				if (reader.MoveToAttribute("assemblyName"))
				{
					base.AssemblyName = reader.Value;
				}
				if (reader.MoveToAttribute("typeName"))
				{
					base.TypeName = reader.Value;
				}
				if (reader.ReadToFollowing("Properties"))
				{
					CollectionSerialization.ReadXml<Property>(reader.ReadSubtree(), "Properties", base.Properties);
				}
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004CAC File Offset: 0x00002EAC
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("Tokenizer");
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
