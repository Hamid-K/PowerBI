using System;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Mashup.Engine1.Library.Exchange
{
	// Token: 0x02000BC9 RID: 3017
	internal class ExchangeAppointmentCatalog : ExchangeCatalog
	{
		// Token: 0x06005252 RID: 21074 RVA: 0x00116133 File Offset: 0x00114333
		public ExchangeAppointmentCatalog(ExchangeVersion exchangeVersion)
			: base(exchangeVersion, ExchangeAppointmentCatalog.ItemSchemaType, "IPM.Appointment", "IPF.Appointment")
		{
		}

		// Token: 0x17001969 RID: 6505
		// (get) Token: 0x06005253 RID: 21075 RVA: 0x0011614C File Offset: 0x0011434C
		protected override PropertyDefinitionBase[] TopLevelPropertiesAllowedList
		{
			get
			{
				if (this.topLevelPropertiesAllowedList == null)
				{
					this.topLevelPropertiesAllowedList = new PropertyDefinitionBase[]
					{
						ItemSchema.Subject,
						AppointmentSchema.Location,
						AppointmentSchema.Start,
						AppointmentSchema.End,
						ItemSchema.DisplayTo,
						ItemSchema.DisplayCc,
						AppointmentSchema.RequiredAttendees,
						AppointmentSchema.OptionalAttendees,
						AppointmentSchema.IsAllDayEvent,
						AppointmentSchema.LegacyFreeBusyStatus,
						ItemSchema.IsReminderSet,
						ItemSchema.ReminderMinutesBeforeStart,
						ItemSchema.Importance,
						ItemSchema.Categories,
						ItemSchema.HasAttachments,
						ItemSchema.Attachments,
						ItemSchema.Preview
					};
				}
				return this.topLevelPropertiesAllowedList;
			}
		}

		// Token: 0x04002D50 RID: 11600
		public const string ItemClass = "IPM.Appointment";

		// Token: 0x04002D51 RID: 11601
		public const string FolderClass = "IPF.Appointment";

		// Token: 0x04002D52 RID: 11602
		public static readonly Type ItemSchemaType = typeof(AppointmentSchema);
	}
}
