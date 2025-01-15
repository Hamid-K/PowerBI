using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200018A RID: 394
	public sealed class UpdateItemResponse : ServiceResponse
	{
		// Token: 0x06001139 RID: 4409 RVA: 0x00032525 File Offset: 0x00031525
		internal UpdateItemResponse(Item item)
		{
			EwsUtilities.Assert(item != null, "UpdateItemResponse.ctor", "item is null");
			this.item = item;
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x0003254C File Offset: 0x0003154C
		internal override void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
			base.ReadElementsFromXml(reader);
			reader.ReadServiceObjectsCollectionFromXml<Item>("Items", new GetObjectInstanceDelegate<Item>(this.GetObjectInstance), false, null, false);
			if (!reader.Service.Exchange2007CompatibilityMode)
			{
				reader.ReadStartElement(XmlNamespace.Messages, "ConflictResults");
				this.conflictCount = reader.ReadElementValue<int>(XmlNamespace.Types, "Count");
				reader.ReadEndElement(XmlNamespace.Messages, "ConflictResults");
			}
			if (this.returnedItem != null && this.item.Id.UniqueId == this.returnedItem.Id.UniqueId)
			{
				this.item.Id.ChangeKey = this.returnedItem.Id.ChangeKey;
				this.returnedItem = null;
			}
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x00032608 File Offset: 0x00031608
		internal override void ReadElementsFromJson(JsonObject responseObject, ExchangeService service)
		{
			base.ReadElementsFromJson(responseObject, service);
			new EwsServiceJsonReader(service).ReadServiceObjectsCollectionFromJson<Item>(responseObject, "Items", new GetObjectInstanceDelegate<Item>(this.GetObjectInstance), false, null, false);
			if (!service.Exchange2007CompatibilityMode)
			{
				this.conflictCount = responseObject.ReadAsJsonObject("ConflictResults").ReadAsInt("Count");
			}
			if (this.returnedItem != null && this.item.Id.UniqueId == this.returnedItem.Id.UniqueId)
			{
				this.item.Id.ChangeKey = this.returnedItem.Id.ChangeKey;
				this.returnedItem = null;
			}
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x000326B7 File Offset: 0x000316B7
		internal override void Loaded()
		{
			if (base.Result == ServiceResult.Success)
			{
				this.item.ClearChangeLog();
			}
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x000326CC File Offset: 0x000316CC
		private Item GetObjectInstance(ExchangeService service, string xmlElementName)
		{
			this.returnedItem = EwsUtilities.CreateEwsObjectFromXmlElementName<Item>(service, xmlElementName);
			return this.returnedItem;
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x0600113E RID: 4414 RVA: 0x000326E1 File Offset: 0x000316E1
		public Item ReturnedItem
		{
			get
			{
				return this.returnedItem;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x0600113F RID: 4415 RVA: 0x000326E9 File Offset: 0x000316E9
		public int ConflictCount
		{
			get
			{
				return this.conflictCount;
			}
		}

		// Token: 0x040009E1 RID: 2529
		private Item item;

		// Token: 0x040009E2 RID: 2530
		private Item returnedItem;

		// Token: 0x040009E3 RID: 2531
		private int conflictCount;
	}
}
