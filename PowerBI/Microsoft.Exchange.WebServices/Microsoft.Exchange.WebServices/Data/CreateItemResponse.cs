using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000152 RID: 338
	internal sealed class CreateItemResponse : CreateItemResponseBase
	{
		// Token: 0x0600104C RID: 4172 RVA: 0x0002FB47 File Offset: 0x0002EB47
		internal override Item GetObjectInstance(ExchangeService service, string xmlElementName)
		{
			return this.item;
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x0002FB4F File Offset: 0x0002EB4F
		internal CreateItemResponse(Item item)
		{
			this.item = item;
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x0002FB5E File Offset: 0x0002EB5E
		internal override void Loaded()
		{
			if (base.Result == ServiceResult.Success)
			{
				this.item.ClearChangeLog();
			}
		}

		// Token: 0x04000997 RID: 2455
		private Item item;
	}
}
