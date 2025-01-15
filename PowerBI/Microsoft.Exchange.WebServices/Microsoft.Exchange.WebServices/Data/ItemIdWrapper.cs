using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200029A RID: 666
	internal class ItemIdWrapper : AbstractItemIdWrapper
	{
		// Token: 0x06001775 RID: 6005 RVA: 0x0003FC2C File Offset: 0x0003EC2C
		internal ItemIdWrapper(ItemId itemId)
		{
			EwsUtilities.Assert(itemId != null, "ItemIdWrapper.ctor", "itemId is null");
			this.itemId = itemId;
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x0003FC51 File Offset: 0x0003EC51
		internal override void WriteToXml(EwsServiceXmlWriter writer)
		{
			this.itemId.WriteToXml(writer);
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x0003FC5F File Offset: 0x0003EC5F
		internal override object IternalToJson(ExchangeService service)
		{
			return this.itemId.InternalToJson(service);
		}

		// Token: 0x04001358 RID: 4952
		private ItemId itemId;
	}
}
