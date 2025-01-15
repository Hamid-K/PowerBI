using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020001AF RID: 431
	[Schema]
	public class AppointmentSchema : ItemSchema
	{
		// Token: 0x060014A4 RID: 5284 RVA: 0x00038550 File Offset: 0x00037550
		internal override void RegisterProperties()
		{
			base.RegisterProperties();
			base.RegisterProperty(AppointmentSchema.ICalUid);
			base.RegisterProperty(AppointmentSchema.ICalRecurrenceId);
			base.RegisterProperty(AppointmentSchema.ICalDateTimeStamp);
			base.RegisterProperty(AppointmentSchema.Start);
			base.RegisterProperty(AppointmentSchema.End);
			base.RegisterProperty(AppointmentSchema.OriginalStart);
			base.RegisterProperty(AppointmentSchema.IsAllDayEvent);
			base.RegisterProperty(AppointmentSchema.LegacyFreeBusyStatus);
			base.RegisterProperty(AppointmentSchema.Location);
			base.RegisterProperty(AppointmentSchema.When);
			base.RegisterProperty(AppointmentSchema.IsMeeting);
			base.RegisterProperty(AppointmentSchema.IsCancelled);
			base.RegisterProperty(AppointmentSchema.IsRecurring);
			base.RegisterProperty(AppointmentSchema.MeetingRequestWasSent);
			base.RegisterProperty(AppointmentSchema.IsResponseRequested);
			base.RegisterProperty(AppointmentSchema.AppointmentType);
			base.RegisterProperty(AppointmentSchema.MyResponseType);
			base.RegisterProperty(AppointmentSchema.Organizer);
			base.RegisterProperty(AppointmentSchema.RequiredAttendees);
			base.RegisterProperty(AppointmentSchema.OptionalAttendees);
			base.RegisterProperty(AppointmentSchema.Resources);
			base.RegisterProperty(AppointmentSchema.ConflictingMeetingCount);
			base.RegisterProperty(AppointmentSchema.AdjacentMeetingCount);
			base.RegisterProperty(AppointmentSchema.ConflictingMeetings);
			base.RegisterProperty(AppointmentSchema.AdjacentMeetings);
			base.RegisterProperty(AppointmentSchema.Duration);
			base.RegisterProperty(AppointmentSchema.TimeZone);
			base.RegisterProperty(AppointmentSchema.AppointmentReplyTime);
			base.RegisterProperty(AppointmentSchema.AppointmentSequenceNumber);
			base.RegisterProperty(AppointmentSchema.AppointmentState);
			base.RegisterProperty(AppointmentSchema.Recurrence);
			base.RegisterProperty(AppointmentSchema.FirstOccurrence);
			base.RegisterProperty(AppointmentSchema.LastOccurrence);
			base.RegisterProperty(AppointmentSchema.ModifiedOccurrences);
			base.RegisterProperty(AppointmentSchema.DeletedOccurrences);
			base.RegisterInternalProperty(AppointmentSchema.MeetingTimeZone);
			base.RegisterProperty(AppointmentSchema.StartTimeZone);
			base.RegisterProperty(AppointmentSchema.EndTimeZone);
			base.RegisterProperty(AppointmentSchema.ConferenceType);
			base.RegisterProperty(AppointmentSchema.AllowNewTimeProposal);
			base.RegisterProperty(AppointmentSchema.IsOnlineMeeting);
			base.RegisterProperty(AppointmentSchema.MeetingWorkspaceUrl);
			base.RegisterProperty(AppointmentSchema.NetShowUrl);
			base.RegisterProperty(AppointmentSchema.EnhancedLocation);
			base.RegisterProperty(AppointmentSchema.JoinOnlineMeetingUrl);
			base.RegisterProperty(AppointmentSchema.OnlineMeetingSettings);
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x0003875D File Offset: 0x0003775D
		internal AppointmentSchema()
		{
		}

		// Token: 0x04000A8D RID: 2701
		public static readonly PropertyDefinition StartTimeZone = new StartTimeZonePropertyDefinition("StartTimeZone", "calendar:StartTimeZone", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000A8E RID: 2702
		public static readonly PropertyDefinition EndTimeZone = new TimeZonePropertyDefinition("EndTimeZone", "calendar:EndTimeZone", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2010);

		// Token: 0x04000A8F RID: 2703
		public static readonly PropertyDefinition Start = new ScopedDateTimePropertyDefinition("Start", "calendar:Start", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1, (ExchangeVersion version) => AppointmentSchema.StartTimeZone);

		// Token: 0x04000A90 RID: 2704
		public static readonly PropertyDefinition End = new ScopedDateTimePropertyDefinition("End", "calendar:End", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1, delegate(ExchangeVersion version)
		{
			if (version != ExchangeVersion.Exchange2007_SP1)
			{
				return AppointmentSchema.EndTimeZone;
			}
			return AppointmentSchema.StartTimeZone;
		});

		// Token: 0x04000A91 RID: 2705
		public static readonly PropertyDefinition OriginalStart = new DateTimePropertyDefinition("OriginalStart", "calendar:OriginalStart", ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000A92 RID: 2706
		public static readonly PropertyDefinition IsAllDayEvent = new BoolPropertyDefinition("IsAllDayEvent", "calendar:IsAllDayEvent", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000A93 RID: 2707
		public static readonly PropertyDefinition LegacyFreeBusyStatus = new GenericPropertyDefinition<LegacyFreeBusyStatus>("LegacyFreeBusyStatus", "calendar:LegacyFreeBusyStatus", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000A94 RID: 2708
		public static readonly PropertyDefinition Location = new StringPropertyDefinition("Location", "calendar:Location", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000A95 RID: 2709
		public static readonly PropertyDefinition When = new StringPropertyDefinition("When", "calendar:When", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000A96 RID: 2710
		public static readonly PropertyDefinition IsMeeting = new BoolPropertyDefinition("IsMeeting", "calendar:IsMeeting", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000A97 RID: 2711
		public static readonly PropertyDefinition IsCancelled = new BoolPropertyDefinition("IsCancelled", "calendar:IsCancelled", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000A98 RID: 2712
		public static readonly PropertyDefinition IsRecurring = new BoolPropertyDefinition("IsRecurring", "calendar:IsRecurring", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000A99 RID: 2713
		public static readonly PropertyDefinition MeetingRequestWasSent = new BoolPropertyDefinition("MeetingRequestWasSent", "calendar:MeetingRequestWasSent", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000A9A RID: 2714
		public static readonly PropertyDefinition IsResponseRequested = new BoolPropertyDefinition("IsResponseRequested", "calendar:IsResponseRequested", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000A9B RID: 2715
		public static readonly PropertyDefinition AppointmentType = new GenericPropertyDefinition<AppointmentType>("CalendarItemType", "calendar:CalendarItemType", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000A9C RID: 2716
		public static readonly PropertyDefinition MyResponseType = new GenericPropertyDefinition<MeetingResponseType>("MyResponseType", "calendar:MyResponseType", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000A9D RID: 2717
		public static readonly PropertyDefinition Organizer = new ContainedPropertyDefinition<EmailAddress>("Organizer", "calendar:Organizer", "Mailbox", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1, () => new EmailAddress());

		// Token: 0x04000A9E RID: 2718
		public static readonly PropertyDefinition RequiredAttendees = new ComplexPropertyDefinition<AttendeeCollection>("RequiredAttendees", "calendar:RequiredAttendees", PropertyDefinitionFlags.AutoInstantiateOnRead | PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete, ExchangeVersion.Exchange2007_SP1, () => new AttendeeCollection());

		// Token: 0x04000A9F RID: 2719
		public static readonly PropertyDefinition OptionalAttendees = new ComplexPropertyDefinition<AttendeeCollection>("OptionalAttendees", "calendar:OptionalAttendees", PropertyDefinitionFlags.AutoInstantiateOnRead | PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete, ExchangeVersion.Exchange2007_SP1, () => new AttendeeCollection());

		// Token: 0x04000AA0 RID: 2720
		public static readonly PropertyDefinition Resources = new ComplexPropertyDefinition<AttendeeCollection>("Resources", "calendar:Resources", PropertyDefinitionFlags.AutoInstantiateOnRead | PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete, ExchangeVersion.Exchange2007_SP1, () => new AttendeeCollection());

		// Token: 0x04000AA1 RID: 2721
		public static readonly PropertyDefinition ConflictingMeetingCount = new IntPropertyDefinition("ConflictingMeetingCount", "calendar:ConflictingMeetingCount", ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000AA2 RID: 2722
		public static readonly PropertyDefinition AdjacentMeetingCount = new IntPropertyDefinition("AdjacentMeetingCount", "calendar:AdjacentMeetingCount", ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000AA3 RID: 2723
		public static readonly PropertyDefinition ConflictingMeetings = new ComplexPropertyDefinition<ItemCollection<Appointment>>("ConflictingMeetings", "calendar:ConflictingMeetings", ExchangeVersion.Exchange2007_SP1, () => new ItemCollection<Appointment>());

		// Token: 0x04000AA4 RID: 2724
		public static readonly PropertyDefinition AdjacentMeetings = new ComplexPropertyDefinition<ItemCollection<Appointment>>("AdjacentMeetings", "calendar:AdjacentMeetings", ExchangeVersion.Exchange2007_SP1, () => new ItemCollection<Appointment>());

		// Token: 0x04000AA5 RID: 2725
		public static readonly PropertyDefinition Duration = new TimeSpanPropertyDefinition("Duration", "calendar:Duration", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000AA6 RID: 2726
		public static readonly PropertyDefinition TimeZone = new StringPropertyDefinition("TimeZone", "calendar:TimeZone", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000AA7 RID: 2727
		public static readonly PropertyDefinition AppointmentReplyTime = new DateTimePropertyDefinition("AppointmentReplyTime", "calendar:AppointmentReplyTime", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000AA8 RID: 2728
		public static readonly PropertyDefinition AppointmentSequenceNumber = new IntPropertyDefinition("AppointmentSequenceNumber", "calendar:AppointmentSequenceNumber", ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000AA9 RID: 2729
		public static readonly PropertyDefinition AppointmentState = new IntPropertyDefinition("AppointmentState", "calendar:AppointmentState", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000AAA RID: 2730
		public static readonly PropertyDefinition Recurrence = new RecurrencePropertyDefinition("Recurrence", "calendar:Recurrence", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000AAB RID: 2731
		public static readonly PropertyDefinition FirstOccurrence = new ComplexPropertyDefinition<OccurrenceInfo>("FirstOccurrence", "calendar:FirstOccurrence", ExchangeVersion.Exchange2007_SP1, () => new OccurrenceInfo());

		// Token: 0x04000AAC RID: 2732
		public static readonly PropertyDefinition LastOccurrence = new ComplexPropertyDefinition<OccurrenceInfo>("LastOccurrence", "calendar:LastOccurrence", ExchangeVersion.Exchange2007_SP1, () => new OccurrenceInfo());

		// Token: 0x04000AAD RID: 2733
		public static readonly PropertyDefinition ModifiedOccurrences = new ComplexPropertyDefinition<OccurrenceInfoCollection>("ModifiedOccurrences", "calendar:ModifiedOccurrences", ExchangeVersion.Exchange2007_SP1, () => new OccurrenceInfoCollection());

		// Token: 0x04000AAE RID: 2734
		public static readonly PropertyDefinition DeletedOccurrences = new ComplexPropertyDefinition<DeletedOccurrenceInfoCollection>("DeletedOccurrences", "calendar:DeletedOccurrences", ExchangeVersion.Exchange2007_SP1, () => new DeletedOccurrenceInfoCollection());

		// Token: 0x04000AAF RID: 2735
		internal static readonly PropertyDefinition MeetingTimeZone = new MeetingTimeZonePropertyDefinition("MeetingTimeZone", "calendar:MeetingTimeZone", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000AB0 RID: 2736
		public static readonly PropertyDefinition ConferenceType = new IntPropertyDefinition("ConferenceType", "calendar:ConferenceType", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000AB1 RID: 2737
		public static readonly PropertyDefinition AllowNewTimeProposal = new BoolPropertyDefinition("AllowNewTimeProposal", "calendar:AllowNewTimeProposal", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000AB2 RID: 2738
		public static readonly PropertyDefinition IsOnlineMeeting = new BoolPropertyDefinition("IsOnlineMeeting", "calendar:IsOnlineMeeting", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000AB3 RID: 2739
		public static readonly PropertyDefinition MeetingWorkspaceUrl = new StringPropertyDefinition("MeetingWorkspaceUrl", "calendar:MeetingWorkspaceUrl", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000AB4 RID: 2740
		public static readonly PropertyDefinition NetShowUrl = new StringPropertyDefinition("NetShowUrl", "calendar:NetShowUrl", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000AB5 RID: 2741
		public static readonly PropertyDefinition ICalUid = new StringPropertyDefinition("UID", "calendar:UID", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000AB6 RID: 2742
		public static readonly PropertyDefinition ICalRecurrenceId = new DateTimePropertyDefinition("RecurrenceId", "calendar:RecurrenceId", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1, true);

		// Token: 0x04000AB7 RID: 2743
		public static readonly PropertyDefinition ICalDateTimeStamp = new DateTimePropertyDefinition("DateTimeStamp", "calendar:DateTimeStamp", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1, true);

		// Token: 0x04000AB8 RID: 2744
		public static readonly PropertyDefinition EnhancedLocation = new ComplexPropertyDefinition<EnhancedLocation>("EnhancedLocation", "calendar:EnhancedLocation", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2013, () => new EnhancedLocation());

		// Token: 0x04000AB9 RID: 2745
		public static readonly PropertyDefinition JoinOnlineMeetingUrl = new StringPropertyDefinition("JoinOnlineMeetingUrl", "calendar:JoinOnlineMeetingUrl", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2013);

		// Token: 0x04000ABA RID: 2746
		public static readonly PropertyDefinition OnlineMeetingSettings = new ComplexPropertyDefinition<OnlineMeetingSettings>("OnlineMeetingSettings", "calendar:OnlineMeetingSettings", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2013, () => new OnlineMeetingSettings());

		// Token: 0x04000ABB RID: 2747
		internal new static readonly AppointmentSchema Instance = new AppointmentSchema();

		// Token: 0x020001B0 RID: 432
		private static class FieldUris
		{
			// Token: 0x04000ACA RID: 2762
			public const string Start = "calendar:Start";

			// Token: 0x04000ACB RID: 2763
			public const string End = "calendar:End";

			// Token: 0x04000ACC RID: 2764
			public const string OriginalStart = "calendar:OriginalStart";

			// Token: 0x04000ACD RID: 2765
			public const string IsAllDayEvent = "calendar:IsAllDayEvent";

			// Token: 0x04000ACE RID: 2766
			public const string LegacyFreeBusyStatus = "calendar:LegacyFreeBusyStatus";

			// Token: 0x04000ACF RID: 2767
			public const string Location = "calendar:Location";

			// Token: 0x04000AD0 RID: 2768
			public const string When = "calendar:When";

			// Token: 0x04000AD1 RID: 2769
			public const string IsMeeting = "calendar:IsMeeting";

			// Token: 0x04000AD2 RID: 2770
			public const string IsCancelled = "calendar:IsCancelled";

			// Token: 0x04000AD3 RID: 2771
			public const string IsRecurring = "calendar:IsRecurring";

			// Token: 0x04000AD4 RID: 2772
			public const string MeetingRequestWasSent = "calendar:MeetingRequestWasSent";

			// Token: 0x04000AD5 RID: 2773
			public const string IsResponseRequested = "calendar:IsResponseRequested";

			// Token: 0x04000AD6 RID: 2774
			public const string CalendarItemType = "calendar:CalendarItemType";

			// Token: 0x04000AD7 RID: 2775
			public const string MyResponseType = "calendar:MyResponseType";

			// Token: 0x04000AD8 RID: 2776
			public const string Organizer = "calendar:Organizer";

			// Token: 0x04000AD9 RID: 2777
			public const string RequiredAttendees = "calendar:RequiredAttendees";

			// Token: 0x04000ADA RID: 2778
			public const string OptionalAttendees = "calendar:OptionalAttendees";

			// Token: 0x04000ADB RID: 2779
			public const string Resources = "calendar:Resources";

			// Token: 0x04000ADC RID: 2780
			public const string ConflictingMeetingCount = "calendar:ConflictingMeetingCount";

			// Token: 0x04000ADD RID: 2781
			public const string AdjacentMeetingCount = "calendar:AdjacentMeetingCount";

			// Token: 0x04000ADE RID: 2782
			public const string ConflictingMeetings = "calendar:ConflictingMeetings";

			// Token: 0x04000ADF RID: 2783
			public const string AdjacentMeetings = "calendar:AdjacentMeetings";

			// Token: 0x04000AE0 RID: 2784
			public const string Duration = "calendar:Duration";

			// Token: 0x04000AE1 RID: 2785
			public const string TimeZone = "calendar:TimeZone";

			// Token: 0x04000AE2 RID: 2786
			public const string AppointmentReplyTime = "calendar:AppointmentReplyTime";

			// Token: 0x04000AE3 RID: 2787
			public const string AppointmentSequenceNumber = "calendar:AppointmentSequenceNumber";

			// Token: 0x04000AE4 RID: 2788
			public const string AppointmentState = "calendar:AppointmentState";

			// Token: 0x04000AE5 RID: 2789
			public const string Recurrence = "calendar:Recurrence";

			// Token: 0x04000AE6 RID: 2790
			public const string FirstOccurrence = "calendar:FirstOccurrence";

			// Token: 0x04000AE7 RID: 2791
			public const string LastOccurrence = "calendar:LastOccurrence";

			// Token: 0x04000AE8 RID: 2792
			public const string ModifiedOccurrences = "calendar:ModifiedOccurrences";

			// Token: 0x04000AE9 RID: 2793
			public const string DeletedOccurrences = "calendar:DeletedOccurrences";

			// Token: 0x04000AEA RID: 2794
			public const string MeetingTimeZone = "calendar:MeetingTimeZone";

			// Token: 0x04000AEB RID: 2795
			public const string StartTimeZone = "calendar:StartTimeZone";

			// Token: 0x04000AEC RID: 2796
			public const string EndTimeZone = "calendar:EndTimeZone";

			// Token: 0x04000AED RID: 2797
			public const string ConferenceType = "calendar:ConferenceType";

			// Token: 0x04000AEE RID: 2798
			public const string AllowNewTimeProposal = "calendar:AllowNewTimeProposal";

			// Token: 0x04000AEF RID: 2799
			public const string IsOnlineMeeting = "calendar:IsOnlineMeeting";

			// Token: 0x04000AF0 RID: 2800
			public const string MeetingWorkspaceUrl = "calendar:MeetingWorkspaceUrl";

			// Token: 0x04000AF1 RID: 2801
			public const string NetShowUrl = "calendar:NetShowUrl";

			// Token: 0x04000AF2 RID: 2802
			public const string Uid = "calendar:UID";

			// Token: 0x04000AF3 RID: 2803
			public const string RecurrenceId = "calendar:RecurrenceId";

			// Token: 0x04000AF4 RID: 2804
			public const string DateTimeStamp = "calendar:DateTimeStamp";

			// Token: 0x04000AF5 RID: 2805
			public const string EnhancedLocation = "calendar:EnhancedLocation";

			// Token: 0x04000AF6 RID: 2806
			public const string JoinOnlineMeetingUrl = "calendar:JoinOnlineMeetingUrl";

			// Token: 0x04000AF7 RID: 2807
			public const string OnlineMeetingSettings = "calendar:OnlineMeetingSettings";
		}
	}
}
