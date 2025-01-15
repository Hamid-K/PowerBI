using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200016A RID: 362
	public sealed class GetHoldOnMailboxesResponse : ServiceResponse
	{
		// Token: 0x060010B4 RID: 4276 RVA: 0x000312C8 File Offset: 0x000302C8
		internal GetHoldOnMailboxesResponse()
		{
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x000312D0 File Offset: 0x000302D0
		internal override void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
			base.ReadElementsFromXml(reader);
			this.holdResult = MailboxHoldResult.LoadFromXml(reader);
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x000312E8 File Offset: 0x000302E8
		internal override void ReadElementsFromJson(JsonObject responseObject, ExchangeService service)
		{
			base.ReadElementsFromJson(responseObject, service);
			if (responseObject.ContainsKey("MailboxHoldResult"))
			{
				JsonObject jsonObject = responseObject.ReadAsJsonObject("MailboxHoldResult");
				this.holdResult = MailboxHoldResult.LoadFromJson(jsonObject);
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x060010B7 RID: 4279 RVA: 0x00031322 File Offset: 0x00030322
		public MailboxHoldResult HoldResult
		{
			get
			{
				return this.holdResult;
			}
		}

		// Token: 0x040009C2 RID: 2498
		private MailboxHoldResult holdResult;
	}
}
