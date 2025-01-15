using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200017E RID: 382
	public sealed class SearchMailboxesResponse : ServiceResponse
	{
		// Token: 0x06001104 RID: 4356 RVA: 0x00031CE1 File Offset: 0x00030CE1
		internal SearchMailboxesResponse()
		{
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x00031CE9 File Offset: 0x00030CE9
		internal override void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
			this.searchResult = new SearchMailboxesResult();
			base.ReadElementsFromXml(reader);
			this.searchResult = SearchMailboxesResult.LoadFromXml(reader);
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x00031D0C File Offset: 0x00030D0C
		internal override void ReadElementsFromJson(JsonObject responseObject, ExchangeService service)
		{
			base.ReadElementsFromJson(responseObject, service);
			if (responseObject.ContainsKey("SearchMailboxesResult"))
			{
				JsonObject jsonObject = responseObject.ReadAsJsonObject("SearchMailboxesResult");
				this.searchResult = SearchMailboxesResult.LoadFromJson(jsonObject);
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06001107 RID: 4359 RVA: 0x00031D46 File Offset: 0x00030D46
		// (set) Token: 0x06001108 RID: 4360 RVA: 0x00031D4E File Offset: 0x00030D4E
		public SearchMailboxesResult SearchResult
		{
			get
			{
				return this.searchResult;
			}
			internal set
			{
				this.searchResult = value;
			}
		}

		// Token: 0x040009D7 RID: 2519
		private SearchMailboxesResult searchResult;
	}
}
