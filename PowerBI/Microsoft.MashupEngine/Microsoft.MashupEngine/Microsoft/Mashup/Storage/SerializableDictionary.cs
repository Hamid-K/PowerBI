using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x02002090 RID: 8336
	[XmlRoot("dict")]
	public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
	{
		// Token: 0x0600CC09 RID: 52233 RVA: 0x002898EA File Offset: 0x00287AEA
		public SerializableDictionary()
		{
		}

		// Token: 0x0600CC0A RID: 52234 RVA: 0x002898F2 File Offset: 0x00287AF2
		public SerializableDictionary(IDictionary<TKey, TValue> properties)
			: base(properties)
		{
		}

		// Token: 0x0600CC0B RID: 52235 RVA: 0x000020FA File Offset: 0x000002FA
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x0600CC0C RID: 52236 RVA: 0x002898FC File Offset: 0x00287AFC
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			bool isEmptyElement = reader.IsEmptyElement;
			reader.Read();
			if (isEmptyElement)
			{
				return;
			}
			while (reader.NodeType != XmlNodeType.EndElement)
			{
				reader.ReadStartElement("item");
				reader.ReadStartElement("key");
				TKey tkey = (TKey)((object)SerializableDictionary<TKey, TValue>.keySerializer.Deserialize(reader));
				reader.ReadEndElement();
				reader.ReadStartElement("value");
				TValue tvalue = (TValue)((object)SerializableDictionary<TKey, TValue>.valueSerializer.Deserialize(reader));
				reader.ReadEndElement();
				base.Add(tkey, tvalue);
				reader.ReadEndElement();
				reader.MoveToContent();
			}
			reader.ReadEndElement();
		}

		// Token: 0x0600CC0D RID: 52237 RVA: 0x00289990 File Offset: 0x00287B90
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
			{
				writer.WriteStartElement("item");
				writer.WriteStartElement("key");
				SerializableDictionary<TKey, TValue>.keySerializer.Serialize(writer, keyValuePair.Key);
				writer.WriteEndElement();
				writer.WriteStartElement("value");
				SerializableDictionary<TKey, TValue>.valueSerializer.Serialize(writer, keyValuePair.Value);
				writer.WriteEndElement();
				writer.WriteEndElement();
			}
		}

		// Token: 0x04006770 RID: 26480
		private static readonly XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));

		// Token: 0x04006771 RID: 26481
		private static readonly XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

		// Token: 0x04006772 RID: 26482
		private const string ItemTag = "item";

		// Token: 0x04006773 RID: 26483
		private const string KeyTag = "key";

		// Token: 0x04006774 RID: 26484
		private const string ValueTag = "value";
	}
}
