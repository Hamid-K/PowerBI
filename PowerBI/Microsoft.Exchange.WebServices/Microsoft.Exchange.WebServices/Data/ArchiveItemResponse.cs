using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200014C RID: 332
	public sealed class ArchiveItemResponse : ServiceResponse
	{
		// Token: 0x0600102F RID: 4143 RVA: 0x0002F5F7 File Offset: 0x0002E5F7
		internal ArchiveItemResponse()
		{
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x0002F5FF File Offset: 0x0002E5FF
		private Item GetObjectInstance(ExchangeService service, string xmlElementName)
		{
			return EwsUtilities.CreateEwsObjectFromXmlElementName<Item>(service, xmlElementName);
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x0002F608 File Offset: 0x0002E608
		internal override void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
			base.ReadElementsFromXml(reader);
			List<Item> list = reader.ReadServiceObjectsCollectionFromXml<Item>("Items", new GetObjectInstanceDelegate<Item>(this.GetObjectInstance), false, null, false);
			if (list.Count > 0)
			{
				this.item = list[0];
			}
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x0002F650 File Offset: 0x0002E650
		internal override void ReadElementsFromJson(JsonObject responseObject, ExchangeService service)
		{
			EwsServiceJsonReader ewsServiceJsonReader = new EwsServiceJsonReader(service);
			List<Item> list = ewsServiceJsonReader.ReadServiceObjectsCollectionFromJson<Item>(responseObject, "Folders", new GetObjectInstanceDelegate<Item>(this.GetObjectInstance), false, null, false);
			if (list.Count > 0)
			{
				this.item = list[0];
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06001033 RID: 4147 RVA: 0x0002F696 File Offset: 0x0002E696
		public Item Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x0400098E RID: 2446
		private Item item;
	}
}
