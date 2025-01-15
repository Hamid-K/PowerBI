using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000166 RID: 358
	internal sealed class GetEventsResponse : ServiceResponse
	{
		// Token: 0x060010A5 RID: 4261 RVA: 0x00031074 File Offset: 0x00030074
		internal GetEventsResponse()
		{
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x00031087 File Offset: 0x00030087
		internal override void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
			base.ReadElementsFromXml(reader);
			this.results.LoadFromXml(reader);
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x0003109C File Offset: 0x0003009C
		internal override void ReadElementsFromJson(JsonObject responseObject, ExchangeService service)
		{
			base.ReadElementsFromJson(responseObject, service);
			this.results.LoadFromJson(responseObject.ReadAsJsonObject("Notification"), service);
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x060010A8 RID: 4264 RVA: 0x000310BD File Offset: 0x000300BD
		internal GetEventsResults Results
		{
			get
			{
				return this.results;
			}
		}

		// Token: 0x040009B9 RID: 2489
		private GetEventsResults results = new GetEventsResults();
	}
}
