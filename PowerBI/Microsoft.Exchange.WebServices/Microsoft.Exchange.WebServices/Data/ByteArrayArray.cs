using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200003D RID: 61
	public sealed class ByteArrayArray : ComplexProperty, IJsonCollectionDeserializer
	{
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000B3EC File Offset: 0x0000A3EC
		public byte[][] Content
		{
			get
			{
				return this.content.ToArray();
			}
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000B3F9 File Offset: 0x0000A3F9
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			if (reader.LocalName == "Base64Binary")
			{
				this.content.Add(reader.ReadBase64ElementValue());
				return true;
			}
			return false;
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000B424 File Offset: 0x0000A424
		void IJsonCollectionDeserializer.CreateFromJsonCollection(object[] jsonCollection, ExchangeService service)
		{
			foreach (object obj in jsonCollection)
			{
				this.content.Add(Convert.FromBase64String(obj as string));
			}
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000B45B File Offset: 0x0000A45B
		void IJsonCollectionDeserializer.UpdateFromJsonCollection(object[] jsonCollection, ExchangeService service)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000B464 File Offset: 0x0000A464
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			foreach (byte[] array in this.content)
			{
				writer.WriteStartElement(XmlNamespace.Types, "Base64Binary");
				writer.WriteBase64ElementValue(array);
				writer.WriteEndElement();
			}
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000B4CC File Offset: 0x0000A4CC
		internal override object InternalToJson(ExchangeService service)
		{
			List<string> list = new List<string>(this.content.Count);
			foreach (byte[] array in this.content)
			{
				list.Add(Convert.ToBase64String(array));
			}
			return list.ToArray();
		}

		// Token: 0x04000135 RID: 309
		private const string ItemXmlElementName = "Base64Binary";

		// Token: 0x04000136 RID: 310
		private List<byte[]> content = new List<byte[]>();
	}
}
