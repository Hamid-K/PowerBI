using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020001C2 RID: 450
	[Schema]
	public class MeetingResponseSchema : MeetingMessageSchema
	{
		// Token: 0x060014FF RID: 5375 RVA: 0x0003AF5C File Offset: 0x00039F5C
		internal override void RegisterProperties()
		{
			base.RegisterProperties();
			base.RegisterProperty(MeetingResponseSchema.Start);
			base.RegisterProperty(MeetingResponseSchema.End);
			base.RegisterProperty(MeetingResponseSchema.Location);
			base.RegisterProperty(MeetingResponseSchema.Recurrence);
			base.RegisterProperty(MeetingResponseSchema.AppointmentType);
			base.RegisterProperty(MeetingResponseSchema.ProposedStart);
			base.RegisterProperty(MeetingResponseSchema.ProposedEnd);
			base.RegisterProperty(MeetingResponseSchema.EnhancedLocation);
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x0003AFC7 File Offset: 0x00039FC7
		internal MeetingResponseSchema()
		{
		}

		// Token: 0x04000C80 RID: 3200
		public static readonly PropertyDefinition Start = AppointmentSchema.Start;

		// Token: 0x04000C81 RID: 3201
		public static readonly PropertyDefinition End = AppointmentSchema.End;

		// Token: 0x04000C82 RID: 3202
		public static readonly PropertyDefinition Location = AppointmentSchema.Location;

		// Token: 0x04000C83 RID: 3203
		public static readonly PropertyDefinition AppointmentType = AppointmentSchema.AppointmentType;

		// Token: 0x04000C84 RID: 3204
		public static readonly PropertyDefinition Recurrence = AppointmentSchema.Recurrence;

		// Token: 0x04000C85 RID: 3205
		public static readonly PropertyDefinition ProposedStart = new ScopedDateTimePropertyDefinition("ProposedStart", "meeting:ProposedStart", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2013, (ExchangeVersion version) => AppointmentSchema.StartTimeZone);

		// Token: 0x04000C86 RID: 3206
		public static readonly PropertyDefinition ProposedEnd = new ScopedDateTimePropertyDefinition("ProposedEnd", "meeting:ProposedEnd", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2013, (ExchangeVersion version) => AppointmentSchema.EndTimeZone);

		// Token: 0x04000C87 RID: 3207
		public static readonly PropertyDefinition EnhancedLocation = AppointmentSchema.EnhancedLocation;

		// Token: 0x04000C88 RID: 3208
		internal new static readonly MeetingResponseSchema Instance = new MeetingResponseSchema();

		// Token: 0x020001C3 RID: 451
		private static class FieldUris
		{
			// Token: 0x04000C8B RID: 3211
			public const string ProposedStart = "meeting:ProposedStart";

			// Token: 0x04000C8C RID: 3212
			public const string ProposedEnd = "meeting:ProposedEnd";
		}
	}
}
