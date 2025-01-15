using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200017B RID: 379
	public sealed class MoveCopyItemResponse : ServiceResponse
	{
		// Token: 0x060010F6 RID: 4342 RVA: 0x00031B3D File Offset: 0x00030B3D
		internal MoveCopyItemResponse()
		{
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x00031B45 File Offset: 0x00030B45
		private Item GetObjectInstance(ExchangeService service, string xmlElementName)
		{
			return EwsUtilities.CreateEwsObjectFromXmlElementName<Item>(service, xmlElementName);
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x00031B50 File Offset: 0x00030B50
		internal override void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
			base.ReadElementsFromXml(reader);
			List<Item> list = reader.ReadServiceObjectsCollectionFromXml<Item>("Items", new GetObjectInstanceDelegate<Item>(this.GetObjectInstance), false, null, false);
			if (list.Count > 0)
			{
				this.item = list[0];
			}
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x00031B98 File Offset: 0x00030B98
		internal override void ReadElementsFromJson(JsonObject responseObject, ExchangeService service)
		{
			EwsServiceJsonReader ewsServiceJsonReader = new EwsServiceJsonReader(service);
			List<Item> list = ewsServiceJsonReader.ReadServiceObjectsCollectionFromJson<Item>(responseObject, "Folders", new GetObjectInstanceDelegate<Item>(this.GetObjectInstance), false, null, false);
			if (list.Count > 0)
			{
				this.item = list[0];
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x060010FA RID: 4346 RVA: 0x00031BDE File Offset: 0x00030BDE
		public Item Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x040009D4 RID: 2516
		private Item item;
	}
}
