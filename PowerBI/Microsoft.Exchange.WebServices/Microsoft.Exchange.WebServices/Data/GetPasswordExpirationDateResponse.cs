using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000170 RID: 368
	internal sealed class GetPasswordExpirationDateResponse : ServiceResponse
	{
		// Token: 0x060010CD RID: 4301 RVA: 0x0003154A File Offset: 0x0003054A
		internal GetPasswordExpirationDateResponse()
		{
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x00031552 File Offset: 0x00030552
		internal override void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
			base.ReadElementsFromXml(reader);
			this.passwordExpirationDate = reader.ReadElementValueAsDateTime(XmlNamespace.NotSpecified, "PasswordExpirationDate");
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x00031570 File Offset: 0x00030570
		internal override void ReadElementsFromJson(JsonObject responseObject, ExchangeService service)
		{
			base.ReadElementsFromJson(responseObject, service);
			this.passwordExpirationDate = new DateTime?(service.ConvertUniversalDateTimeStringToLocalDateTime(responseObject.ReadAsString("PasswordExpirationDate")).Value);
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x060010D0 RID: 4304 RVA: 0x000315A9 File Offset: 0x000305A9
		public DateTime? PasswordExpirationDate
		{
			get
			{
				return this.passwordExpirationDate;
			}
		}

		// Token: 0x040009C9 RID: 2505
		private DateTime? passwordExpirationDate;
	}
}
