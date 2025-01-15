using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000186 RID: 390
	public sealed class SyncFolderItemsResponse : SyncResponse<Item, ItemChange>
	{
		// Token: 0x0600112B RID: 4395 RVA: 0x0003243E File Offset: 0x0003143E
		internal SyncFolderItemsResponse(PropertySet propertySet)
			: base(propertySet)
		{
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x00032447 File Offset: 0x00031447
		internal override string GetIncludesLastInRangeXmlElementName()
		{
			return "IncludesLastItemInRange";
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x0003244E File Offset: 0x0003144E
		internal override ItemChange CreateChangeInstance()
		{
			return new ItemChange();
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x00032455 File Offset: 0x00031455
		internal override string GetChangeElementName()
		{
			return "Item";
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x0003245C File Offset: 0x0003145C
		internal override string GetChangeIdElementName()
		{
			return "ItemId";
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06001130 RID: 4400 RVA: 0x00032463 File Offset: 0x00031463
		internal override bool SummaryPropertiesOnly
		{
			get
			{
				return true;
			}
		}
	}
}
