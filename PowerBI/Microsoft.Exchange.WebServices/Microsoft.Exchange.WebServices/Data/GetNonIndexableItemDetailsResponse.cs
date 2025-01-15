using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200016D RID: 365
	public sealed class GetNonIndexableItemDetailsResponse : ServiceResponse
	{
		// Token: 0x060010C0 RID: 4288 RVA: 0x00031478 File Offset: 0x00030478
		internal GetNonIndexableItemDetailsResponse()
		{
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x00031480 File Offset: 0x00030480
		internal override void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
			base.ReadElementsFromXml(reader);
			this.NonIndexableItemsResult = NonIndexableItemDetailsResult.LoadFromXml(reader);
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x00031495 File Offset: 0x00030495
		internal override void ReadElementsFromJson(JsonObject responseObject, ExchangeService service)
		{
			throw new NotImplementedException("GetNonIndexableItemdDetails doesn't support JSON.");
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x060010C3 RID: 4291 RVA: 0x000314A1 File Offset: 0x000304A1
		// (set) Token: 0x060010C4 RID: 4292 RVA: 0x000314A9 File Offset: 0x000304A9
		public NonIndexableItemDetailsResult NonIndexableItemsResult { get; internal set; }
	}
}
