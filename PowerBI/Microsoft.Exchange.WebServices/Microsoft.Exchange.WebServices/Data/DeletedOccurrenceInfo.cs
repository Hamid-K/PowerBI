using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200004F RID: 79
	public class DeletedOccurrenceInfo : ComplexProperty
	{
		// Token: 0x0600038C RID: 908 RVA: 0x0000D29D File Offset: 0x0000C29D
		internal DeletedOccurrenceInfo()
		{
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000D2A8 File Offset: 0x0000C2A8
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null && localName == "Start")
			{
				this.originalStart = reader.ReadElementValueAsDateTime().Value;
				return true;
			}
			return false;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000D2E4 File Offset: 0x0000C2E4
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			if (jsonProperty.ContainsKey("Start"))
			{
				this.originalStart = service.ConvertUniversalDateTimeStringToLocalDateTime(jsonProperty.ReadAsString("Start")).Value;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0000D31D File Offset: 0x0000C31D
		public DateTime OriginalStart
		{
			get
			{
				return this.originalStart;
			}
		}

		// Token: 0x04000179 RID: 377
		private DateTime originalStart;
	}
}
