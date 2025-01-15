using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020001C0 RID: 448
	[Schema]
	public class MeetingRequestSchema : MeetingMessageSchema
	{
		// Token: 0x060014FB RID: 5371 RVA: 0x0003AB58 File Offset: 0x00039B58
		internal override void RegisterProperties()
		{
			base.RegisterProperties();
			base.RegisterProperty(MeetingRequestSchema.MeetingRequestType);
			base.RegisterProperty(MeetingRequestSchema.IntendedFreeBusyStatus);
			base.RegisterProperty(MeetingRequestSchema.ChangeHighlights);
			base.RegisterProperty(MeetingRequestSchema.Start);
			base.RegisterProperty(MeetingRequestSchema.End);
			base.RegisterProperty(MeetingRequestSchema.OriginalStart);
			base.RegisterProperty(MeetingRequestSchema.IsAllDayEvent);
			base.RegisterProperty(MeetingRequestSchema.LegacyFreeBusyStatus);
			base.RegisterProperty(MeetingRequestSchema.Location);
			base.RegisterProperty(MeetingRequestSchema.When);
			base.RegisterProperty(MeetingRequestSchema.IsMeeting);
			base.RegisterProperty(MeetingRequestSchema.IsCancelled);
			base.RegisterProperty(MeetingRequestSchema.IsRecurring);
			base.RegisterProperty(MeetingRequestSchema.MeetingRequestWasSent);
			base.RegisterProperty(MeetingRequestSchema.AppointmentType);
			base.RegisterProperty(MeetingRequestSchema.MyResponseType);
			base.RegisterProperty(MeetingRequestSchema.Organizer);
			base.RegisterProperty(MeetingRequestSchema.RequiredAttendees);
			base.RegisterProperty(MeetingRequestSchema.OptionalAttendees);
			base.RegisterProperty(MeetingRequestSchema.Resources);
			base.RegisterProperty(MeetingRequestSchema.ConflictingMeetingCount);
			base.RegisterProperty(MeetingRequestSchema.AdjacentMeetingCount);
			base.RegisterProperty(MeetingRequestSchema.ConflictingMeetings);
			base.RegisterProperty(MeetingRequestSchema.AdjacentMeetings);
			base.RegisterProperty(MeetingRequestSchema.Duration);
			base.RegisterProperty(MeetingRequestSchema.TimeZone);
			base.RegisterProperty(MeetingRequestSchema.AppointmentReplyTime);
			base.RegisterProperty(MeetingRequestSchema.AppointmentSequenceNumber);
			base.RegisterProperty(MeetingRequestSchema.AppointmentState);
			base.RegisterProperty(MeetingRequestSchema.Recurrence);
			base.RegisterProperty(MeetingRequestSchema.FirstOccurrence);
			base.RegisterProperty(MeetingRequestSchema.LastOccurrence);
			base.RegisterProperty(MeetingRequestSchema.ModifiedOccurrences);
			base.RegisterProperty(MeetingRequestSchema.DeletedOccurrences);
			base.RegisterInternalProperty(MeetingRequestSchema.MeetingTimeZone);
			base.RegisterProperty(MeetingRequestSchema.StartTimeZone);
			base.RegisterProperty(MeetingRequestSchema.EndTimeZone);
			base.RegisterProperty(MeetingRequestSchema.ConferenceType);
			base.RegisterProperty(MeetingRequestSchema.AllowNewTimeProposal);
			base.RegisterProperty(MeetingRequestSchema.IsOnlineMeeting);
			base.RegisterProperty(MeetingRequestSchema.MeetingWorkspaceUrl);
			base.RegisterProperty(MeetingRequestSchema.NetShowUrl);
			base.RegisterProperty(MeetingRequestSchema.EnhancedLocation);
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x0003AD44 File Offset: 0x00039D44
		internal MeetingRequestSchema()
		{
		}

		// Token: 0x04000C50 RID: 3152
		public static readonly PropertyDefinition MeetingRequestType = new GenericPropertyDefinition<MeetingRequestType>("MeetingRequestType", "meetingRequest:MeetingRequestType", ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000C51 RID: 3153
		public static readonly PropertyDefinition IntendedFreeBusyStatus = new GenericPropertyDefinition<LegacyFreeBusyStatus>("IntendedFreeBusyStatus", "meetingRequest:IntendedFreeBusyStatus", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000C52 RID: 3154
		public static readonly PropertyDefinition ChangeHighlights = new ComplexPropertyDefinition<ChangeHighlights>("ChangeHighlights", "meetingRequest:ChangeHighlights", ExchangeVersion.Exchange2013, () => new ChangeHighlights());

		// Token: 0x04000C53 RID: 3155
		public static readonly PropertyDefinition EnhancedLocation = AppointmentSchema.EnhancedLocation;

		// Token: 0x04000C54 RID: 3156
		public static readonly PropertyDefinition Start = AppointmentSchema.Start;

		// Token: 0x04000C55 RID: 3157
		public static readonly PropertyDefinition End = AppointmentSchema.End;

		// Token: 0x04000C56 RID: 3158
		public static readonly PropertyDefinition OriginalStart = AppointmentSchema.OriginalStart;

		// Token: 0x04000C57 RID: 3159
		public static readonly PropertyDefinition IsAllDayEvent = AppointmentSchema.IsAllDayEvent;

		// Token: 0x04000C58 RID: 3160
		public static readonly PropertyDefinition LegacyFreeBusyStatus = AppointmentSchema.LegacyFreeBusyStatus;

		// Token: 0x04000C59 RID: 3161
		public static readonly PropertyDefinition Location = AppointmentSchema.Location;

		// Token: 0x04000C5A RID: 3162
		public static readonly PropertyDefinition When = AppointmentSchema.When;

		// Token: 0x04000C5B RID: 3163
		public static readonly PropertyDefinition IsMeeting = AppointmentSchema.IsMeeting;

		// Token: 0x04000C5C RID: 3164
		public static readonly PropertyDefinition IsCancelled = AppointmentSchema.IsCancelled;

		// Token: 0x04000C5D RID: 3165
		public static readonly PropertyDefinition IsRecurring = AppointmentSchema.IsRecurring;

		// Token: 0x04000C5E RID: 3166
		public static readonly PropertyDefinition MeetingRequestWasSent = AppointmentSchema.MeetingRequestWasSent;

		// Token: 0x04000C5F RID: 3167
		public static readonly PropertyDefinition AppointmentType = AppointmentSchema.AppointmentType;

		// Token: 0x04000C60 RID: 3168
		public static readonly PropertyDefinition MyResponseType = AppointmentSchema.MyResponseType;

		// Token: 0x04000C61 RID: 3169
		public static readonly PropertyDefinition Organizer = AppointmentSchema.Organizer;

		// Token: 0x04000C62 RID: 3170
		public static readonly PropertyDefinition RequiredAttendees = AppointmentSchema.RequiredAttendees;

		// Token: 0x04000C63 RID: 3171
		public static readonly PropertyDefinition OptionalAttendees = AppointmentSchema.OptionalAttendees;

		// Token: 0x04000C64 RID: 3172
		public static readonly PropertyDefinition Resources = AppointmentSchema.Resources;

		// Token: 0x04000C65 RID: 3173
		public static readonly PropertyDefinition ConflictingMeetingCount = AppointmentSchema.ConflictingMeetingCount;

		// Token: 0x04000C66 RID: 3174
		public static readonly PropertyDefinition AdjacentMeetingCount = AppointmentSchema.AdjacentMeetingCount;

		// Token: 0x04000C67 RID: 3175
		public static readonly PropertyDefinition ConflictingMeetings = AppointmentSchema.ConflictingMeetings;

		// Token: 0x04000C68 RID: 3176
		public static readonly PropertyDefinition AdjacentMeetings = AppointmentSchema.AdjacentMeetings;

		// Token: 0x04000C69 RID: 3177
		public static readonly PropertyDefinition Duration = AppointmentSchema.Duration;

		// Token: 0x04000C6A RID: 3178
		public static readonly PropertyDefinition TimeZone = AppointmentSchema.TimeZone;

		// Token: 0x04000C6B RID: 3179
		public static readonly PropertyDefinition AppointmentReplyTime = AppointmentSchema.AppointmentReplyTime;

		// Token: 0x04000C6C RID: 3180
		public static readonly PropertyDefinition AppointmentSequenceNumber = AppointmentSchema.AppointmentSequenceNumber;

		// Token: 0x04000C6D RID: 3181
		public static readonly PropertyDefinition AppointmentState = AppointmentSchema.AppointmentState;

		// Token: 0x04000C6E RID: 3182
		public static readonly PropertyDefinition Recurrence = AppointmentSchema.Recurrence;

		// Token: 0x04000C6F RID: 3183
		public static readonly PropertyDefinition FirstOccurrence = AppointmentSchema.FirstOccurrence;

		// Token: 0x04000C70 RID: 3184
		public static readonly PropertyDefinition LastOccurrence = AppointmentSchema.LastOccurrence;

		// Token: 0x04000C71 RID: 3185
		public static readonly PropertyDefinition ModifiedOccurrences = AppointmentSchema.ModifiedOccurrences;

		// Token: 0x04000C72 RID: 3186
		public static readonly PropertyDefinition DeletedOccurrences = AppointmentSchema.DeletedOccurrences;

		// Token: 0x04000C73 RID: 3187
		internal static readonly PropertyDefinition MeetingTimeZone = AppointmentSchema.MeetingTimeZone;

		// Token: 0x04000C74 RID: 3188
		public static readonly PropertyDefinition StartTimeZone = AppointmentSchema.StartTimeZone;

		// Token: 0x04000C75 RID: 3189
		public static readonly PropertyDefinition EndTimeZone = AppointmentSchema.EndTimeZone;

		// Token: 0x04000C76 RID: 3190
		public static readonly PropertyDefinition ConferenceType = AppointmentSchema.ConferenceType;

		// Token: 0x04000C77 RID: 3191
		public static readonly PropertyDefinition AllowNewTimeProposal = AppointmentSchema.AllowNewTimeProposal;

		// Token: 0x04000C78 RID: 3192
		public static readonly PropertyDefinition IsOnlineMeeting = AppointmentSchema.IsOnlineMeeting;

		// Token: 0x04000C79 RID: 3193
		public static readonly PropertyDefinition MeetingWorkspaceUrl = AppointmentSchema.MeetingWorkspaceUrl;

		// Token: 0x04000C7A RID: 3194
		public static readonly PropertyDefinition NetShowUrl = AppointmentSchema.NetShowUrl;

		// Token: 0x04000C7B RID: 3195
		internal new static readonly MeetingRequestSchema Instance = new MeetingRequestSchema();

		// Token: 0x020001C1 RID: 449
		private static class FieldUris
		{
			// Token: 0x04000C7D RID: 3197
			public const string MeetingRequestType = "meetingRequest:MeetingRequestType";

			// Token: 0x04000C7E RID: 3198
			public const string IntendedFreeBusyStatus = "meetingRequest:IntendedFreeBusyStatus";

			// Token: 0x04000C7F RID: 3199
			public const string ChangeHighlights = "meetingRequest:ChangeHighlights";
		}
	}
}
