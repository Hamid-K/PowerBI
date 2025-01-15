using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200029B RID: 667
	internal class ItemIdWrapperList : IEnumerable<AbstractItemIdWrapper>, IEnumerable
	{
		// Token: 0x06001778 RID: 6008 RVA: 0x0003FC6D File Offset: 0x0003EC6D
		internal ItemIdWrapperList()
		{
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x0003FC80 File Offset: 0x0003EC80
		internal void Add(Item item)
		{
			this.itemIds.Add(new ItemWrapper(item));
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x0003FC94 File Offset: 0x0003EC94
		internal void AddRange(IEnumerable<Item> items)
		{
			foreach (Item item in items)
			{
				this.Add(item);
			}
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x0003FCDC File Offset: 0x0003ECDC
		internal void Add(ItemId itemId)
		{
			this.itemIds.Add(new ItemIdWrapper(itemId));
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x0003FCF0 File Offset: 0x0003ECF0
		internal void AddRange(IEnumerable<ItemId> itemIds)
		{
			foreach (ItemId itemId in itemIds)
			{
				this.Add(itemId);
			}
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x0003FD38 File Offset: 0x0003ED38
		internal void WriteToXml(EwsServiceXmlWriter writer, XmlNamespace ewsNamesapce, string xmlElementName)
		{
			if (this.Count > 0)
			{
				writer.WriteStartElement(ewsNamesapce, xmlElementName);
				foreach (AbstractItemIdWrapper abstractItemIdWrapper in this.itemIds)
				{
					abstractItemIdWrapper.WriteToXml(writer);
				}
				writer.WriteEndElement();
			}
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x0003FDA4 File Offset: 0x0003EDA4
		internal object InternalToJson(ExchangeService service)
		{
			List<object> list = new List<object>();
			foreach (AbstractItemIdWrapper abstractItemIdWrapper in this.itemIds)
			{
				list.Add(((IJsonSerializable)abstractItemIdWrapper).ToJson(service));
			}
			return list.ToArray();
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x0600177F RID: 6015 RVA: 0x0003FE0C File Offset: 0x0003EE0C
		internal int Count
		{
			get
			{
				return this.itemIds.Count;
			}
		}

		// Token: 0x170005B9 RID: 1465
		internal Item this[int index]
		{
			get
			{
				return this.itemIds[index].GetItem();
			}
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x0003FE2C File Offset: 0x0003EE2C
		public IEnumerator<AbstractItemIdWrapper> GetEnumerator()
		{
			return this.itemIds.GetEnumerator();
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x0003FE3E File Offset: 0x0003EE3E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.itemIds.GetEnumerator();
		}

		// Token: 0x04001359 RID: 4953
		private List<AbstractItemIdWrapper> itemIds = new List<AbstractItemIdWrapper>();
	}
}
