using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020001BD RID: 445
	[Schema]
	public class MeetingMessageSchema : EmailMessageSchema
	{
		// Token: 0x060014F4 RID: 5364 RVA: 0x0003A944 File Offset: 0x00039944
		internal override void RegisterProperties()
		{
			base.RegisterProperties();
			base.RegisterProperty(MeetingMessageSchema.AssociatedAppointmentId);
			base.RegisterProperty(MeetingMessageSchema.IsDelegated);
			base.RegisterProperty(MeetingMessageSchema.IsOutOfDate);
			base.RegisterProperty(MeetingMessageSchema.HasBeenProcessed);
			base.RegisterProperty(MeetingMessageSchema.ResponseType);
			base.RegisterProperty(MeetingMessageSchema.ICalUid);
			base.RegisterProperty(MeetingMessageSchema.ICalRecurrenceId);
			base.RegisterProperty(MeetingMessageSchema.ICalDateTimeStamp);
			base.RegisterProperty(MeetingMessageSchema.IsOrganizer);
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x0003A9BA File Offset: 0x000399BA
		internal MeetingMessageSchema()
		{
		}

		// Token: 0x04000C38 RID: 3128
		public static readonly PropertyDefinition AssociatedAppointmentId = new ComplexPropertyDefinition<ItemId>("AssociatedCalendarItemId", "meeting:AssociatedCalendarItemId", ExchangeVersion.Exchange2007_SP1, () => new ItemId());

		// Token: 0x04000C39 RID: 3129
		public static readonly PropertyDefinition IsDelegated = new BoolPropertyDefinition("IsDelegated", "meeting:IsDelegated", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000C3A RID: 3130
		public static readonly PropertyDefinition IsOutOfDate = new BoolPropertyDefinition("IsOutOfDate", "meeting:IsOutOfDate", ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000C3B RID: 3131
		public static readonly PropertyDefinition HasBeenProcessed = new BoolPropertyDefinition("HasBeenProcessed", "meeting:HasBeenProcessed", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000C3C RID: 3132
		public static readonly PropertyDefinition ResponseType = new GenericPropertyDefinition<MeetingResponseType>("ResponseType", "meeting:ResponseType", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000C3D RID: 3133
		public static readonly PropertyDefinition ICalUid = AppointmentSchema.ICalUid;

		// Token: 0x04000C3E RID: 3134
		public static readonly PropertyDefinition ICalRecurrenceId = AppointmentSchema.ICalRecurrenceId;

		// Token: 0x04000C3F RID: 3135
		public static readonly PropertyDefinition ICalDateTimeStamp = AppointmentSchema.ICalDateTimeStamp;

		// Token: 0x04000C40 RID: 3136
		public static readonly PropertyDefinition IsOrganizer = new GenericPropertyDefinition<bool>("IsOrganizer", "cal:IsOrganizer", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2013);

		// Token: 0x04000C41 RID: 3137
		internal new static readonly MeetingMessageSchema Instance = new MeetingMessageSchema();

		// Token: 0x020001BE RID: 446
		private static class FieldUris
		{
			// Token: 0x04000C43 RID: 3139
			public const string AssociatedCalendarItemId = "meeting:AssociatedCalendarItemId";

			// Token: 0x04000C44 RID: 3140
			public const string IsDelegated = "meeting:IsDelegated";

			// Token: 0x04000C45 RID: 3141
			public const string IsOutOfDate = "meeting:IsOutOfDate";

			// Token: 0x04000C46 RID: 3142
			public const string HasBeenProcessed = "meeting:HasBeenProcessed";

			// Token: 0x04000C47 RID: 3143
			public const string ResponseType = "meeting:ResponseType";

			// Token: 0x04000C48 RID: 3144
			public const string IsOrganizer = "cal:IsOrganizer";
		}
	}
}
