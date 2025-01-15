using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200029C RID: 668
	internal class ItemWrapper : AbstractItemIdWrapper
	{
		// Token: 0x06001783 RID: 6019 RVA: 0x0003FE50 File Offset: 0x0003EE50
		internal ItemWrapper(Item item)
		{
			EwsUtilities.Assert(item != null, "ItemWrapper.ctor", "item is null");
			EwsUtilities.Assert(!item.IsNew, "ItemWrapper.ctor", "item does not have an Id");
			this.item = item;
		}

		// Token: 0x06001784 RID: 6020 RVA: 0x0003FE8D File Offset: 0x0003EE8D
		public override Item GetItem()
		{
			return this.item;
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x0003FE95 File Offset: 0x0003EE95
		internal override void WriteToXml(EwsServiceXmlWriter writer)
		{
			this.item.Id.WriteToXml(writer);
		}

		// Token: 0x06001786 RID: 6022 RVA: 0x0003FEA8 File Offset: 0x0003EEA8
		internal override object IternalToJson(ExchangeService service)
		{
			return ((IJsonSerializable)this.item.Id).ToJson(service);
		}

		// Token: 0x0400135A RID: 4954
		private Item item;
	}
}
