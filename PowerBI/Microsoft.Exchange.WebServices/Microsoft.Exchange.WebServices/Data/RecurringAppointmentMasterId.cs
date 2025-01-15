using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000088 RID: 136
	public sealed class RecurringAppointmentMasterId : ItemId
	{
		// Token: 0x06000610 RID: 1552 RVA: 0x00014C4B File Offset: 0x00013C4B
		public RecurringAppointmentMasterId(string occurrenceId)
			: base(occurrenceId)
		{
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00014C54 File Offset: 0x00013C54
		internal override string GetXmlElementName()
		{
			return "RecurringMasterItemId";
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00014C5B File Offset: 0x00013C5B
		internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteAttributeValue("OccurrenceId", base.UniqueId);
			writer.WriteAttributeValue("ChangeKey", base.ChangeKey);
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00014C80 File Offset: 0x00013C80
		internal override object InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.AddTypeParameter(this.GetXmlElementName());
			jsonObject.Add("OccurrenceId", base.UniqueId);
			jsonObject.Add("ChangeKey", base.ChangeKey);
			return jsonObject;
		}
	}
}
