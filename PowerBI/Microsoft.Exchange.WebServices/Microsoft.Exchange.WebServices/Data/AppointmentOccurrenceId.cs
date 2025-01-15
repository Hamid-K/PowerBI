using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000032 RID: 50
	public sealed class AppointmentOccurrenceId : ItemId
	{
		// Token: 0x0600024A RID: 586 RVA: 0x00009D8D File Offset: 0x00008D8D
		public AppointmentOccurrenceId(string recurringMasterUniqueId, int occurrenceIndex)
			: base(recurringMasterUniqueId)
		{
			this.OccurrenceIndex = occurrenceIndex;
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600024B RID: 587 RVA: 0x00009D9D File Offset: 0x00008D9D
		// (set) Token: 0x0600024C RID: 588 RVA: 0x00009DA5 File Offset: 0x00008DA5
		public int OccurrenceIndex
		{
			get
			{
				return this.occurrenceIndex;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentException(Strings.OccurrenceIndexMustBeGreaterThanZero);
				}
				this.occurrenceIndex = value;
			}
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00009DC2 File Offset: 0x00008DC2
		internal override string GetXmlElementName()
		{
			return "OccurrenceItemId";
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00009DC9 File Offset: 0x00008DC9
		internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteAttributeValue("RecurringMasterId", base.UniqueId);
			writer.WriteAttributeValue("InstanceIndex", this.OccurrenceIndex);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00009DF4 File Offset: 0x00008DF4
		internal override object InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.AddTypeParameter(this.GetXmlElementName());
			jsonObject.Add("RecurringMasterId", base.UniqueId);
			jsonObject.Add("InstanceIndex", this.OccurrenceIndex);
			return jsonObject;
		}

		// Token: 0x0400011A RID: 282
		private int occurrenceIndex;
	}
}
