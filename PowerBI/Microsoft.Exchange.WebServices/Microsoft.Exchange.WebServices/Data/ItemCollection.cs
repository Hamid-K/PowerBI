using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200006D RID: 109
	[EditorBrowsable(1)]
	public sealed class ItemCollection<TItem> : ComplexProperty, IEnumerable<TItem>, IEnumerable, IJsonCollectionDeserializer where TItem : Item
	{
		// Token: 0x06000508 RID: 1288 RVA: 0x00011FC0 File Offset: 0x00010FC0
		internal ItemCollection()
		{
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00011FD4 File Offset: 0x00010FD4
		internal override void LoadFromXml(EwsServiceXmlReader reader, string localElementName)
		{
			reader.EnsureCurrentNodeIsStartElement(XmlNamespace.Types, localElementName);
			if (!reader.IsEmptyElement)
			{
				do
				{
					reader.Read();
					if (reader.NodeType == 1)
					{
						TItem titem = EwsUtilities.CreateEwsObjectFromXmlElementName<Item>(reader.Service, reader.LocalName) as TItem;
						if (titem == null)
						{
							reader.SkipCurrentElement();
						}
						else
						{
							titem.LoadFromXml(reader, true);
							this.items.Add(titem);
						}
					}
				}
				while (!reader.IsEndElement(XmlNamespace.Types, localElementName));
			}
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00012054 File Offset: 0x00011054
		void IJsonCollectionDeserializer.CreateFromJsonCollection(object[] jsonCollection, ExchangeService service)
		{
			foreach (object obj in jsonCollection)
			{
				JsonObject jsonObject = obj as JsonObject;
				TItem titem = EwsUtilities.CreateEwsObjectFromXmlElementName<Item>(service, jsonObject.ReadTypeString()) as TItem;
				titem.LoadFromJson(jsonObject, service, true);
				this.items.Add(titem);
			}
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x000120B4 File Offset: 0x000110B4
		void IJsonCollectionDeserializer.UpdateFromJsonCollection(object[] jsonCollection, ExchangeService service)
		{
			throw new NotImplementedException();
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x000120BB File Offset: 0x000110BB
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x1700010E RID: 270
		public TItem this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index", Strings.IndexIsOutOfRange);
				}
				return this.items[index];
			}
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x000120F8 File Offset: 0x000110F8
		public IEnumerator<TItem> GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0001210A File Offset: 0x0001110A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x040001B5 RID: 437
		private List<TItem> items = new List<TItem>();
	}
}
