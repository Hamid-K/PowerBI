using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000181 RID: 385
	public sealed class SetHoldOnMailboxesResponse : ServiceResponse
	{
		// Token: 0x06001111 RID: 4369 RVA: 0x00031E34 File Offset: 0x00030E34
		internal SetHoldOnMailboxesResponse()
		{
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x00031E3C File Offset: 0x00030E3C
		internal override void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
			base.ReadElementsFromXml(reader);
			this.holdResult = MailboxHoldResult.LoadFromXml(reader);
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x00031E54 File Offset: 0x00030E54
		internal override void ReadElementsFromJson(JsonObject responseObject, ExchangeService service)
		{
			base.ReadElementsFromJson(responseObject, service);
			if (responseObject.ContainsKey("MailboxHoldResult"))
			{
				JsonObject jsonObject = responseObject.ReadAsJsonObject("MailboxHoldResult");
				this.holdResult = MailboxHoldResult.LoadFromJson(jsonObject);
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06001114 RID: 4372 RVA: 0x00031E8E File Offset: 0x00030E8E
		public MailboxHoldResult HoldResult
		{
			get
			{
				return this.holdResult;
			}
		}

		// Token: 0x040009DA RID: 2522
		private MailboxHoldResult holdResult;
	}
}
