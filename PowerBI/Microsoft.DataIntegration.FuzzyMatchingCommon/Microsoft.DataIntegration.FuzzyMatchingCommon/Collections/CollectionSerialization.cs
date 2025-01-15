using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x0200009E RID: 158
	public sealed class CollectionSerialization
	{
		// Token: 0x060006B5 RID: 1717 RVA: 0x00023E73 File Offset: 0x00022073
		public static XmlSchema GetSchema()
		{
			return null;
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x00023E76 File Offset: 0x00022076
		public static void ReadXml<T>(XmlReader reader, string collectionTagName, ICollection<T> items) where T : IXmlSerializable, new()
		{
			CollectionSerialization.ReadXml<T>(reader, collectionTagName, typeof(T).Name, items);
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x00023E90 File Offset: 0x00022090
		public static void ReadXml<T>(XmlReader reader, string collectionTagName, string elementTagName, ICollection<T> items) where T : IXmlSerializable, new()
		{
			items.Clear();
			if (reader.ReadToFollowing(collectionTagName))
			{
				XmlReader xmlReader = reader.ReadSubtree();
				while (xmlReader.ReadToFollowing(elementTagName))
				{
					T t = new T();
					t.ReadXml(xmlReader.ReadSubtree());
					items.Add(t);
				}
			}
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x00023EDE File Offset: 0x000220DE
		public static List<T> ReadXmlObjects<T>(XmlNode n, string collectionTagName) where T : IXmlSerializable, new()
		{
			return CollectionSerialization.ReadXmlObjects<T>(new XmlNodeReader(n), collectionTagName, typeof(T).Name);
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x00023EFB File Offset: 0x000220FB
		public static List<T> ReadXmlObjects<T>(XmlReader reader, string collectionTagName) where T : IXmlSerializable, new()
		{
			return CollectionSerialization.ReadXmlObjects<T>(reader, collectionTagName, typeof(T).Name);
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x00023F13 File Offset: 0x00022113
		public static List<T> ReadXmlObjects<T>(XmlNode n, string collectionTagName, string elementTagName) where T : IXmlSerializable, new()
		{
			return CollectionSerialization.ReadXmlObjects<T>(new XmlNodeReader(n), collectionTagName, elementTagName);
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x00023F24 File Offset: 0x00022124
		public static List<T> ReadXmlObjects<T>(XmlReader reader, string collectionTagName, string elementTagName) where T : IXmlSerializable, new()
		{
			List<T> list = new List<T>();
			if (reader.ReadToFollowing(collectionTagName))
			{
				XmlReader xmlReader = reader.ReadSubtree();
				while (xmlReader.ReadToFollowing(elementTagName))
				{
					T t = new T();
					t.ReadXml(xmlReader.ReadSubtree());
					list.Add(t);
				}
			}
			return list;
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x00023F74 File Offset: 0x00022174
		public static void WriteXml<T>(XmlWriter writer, string collectionTagName, ICollection<T> items) where T : IXmlSerializable
		{
			writer.WriteStartElement(collectionTagName);
			foreach (T t in items)
			{
				t.WriteXml(writer);
			}
			writer.WriteEndElement();
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x00023FCC File Offset: 0x000221CC
		public static void ReadXml(XmlReader reader, string collectionTagName, ICollection<XmlDocument> items)
		{
			items.Clear();
			if (reader.ReadToFollowing(collectionTagName))
			{
				reader = reader.ReadSubtree();
				while (reader.ReadToFollowing("XmlDocument"))
				{
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.LoadXml(reader.ReadString());
					items.Add(xmlDocument);
				}
			}
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x00024018 File Offset: 0x00022218
		public static void WriteXml(XmlWriter writer, string collectionTagName, ICollection<XmlDocument> items)
		{
			writer.WriteStartElement(collectionTagName);
			foreach (XmlDocument xmlDocument in items)
			{
				writer.WriteStartElement("XmlDocument");
				writer.WriteCData(xmlDocument.OuterXml);
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
		}
	}
}
