using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200019D RID: 413
	[ServiceObjectDefinition("MeetingRequest")]
	public class MeetingRequest : MeetingMessage, ICalendarActionProvider
	{
		// Token: 0x06001395 RID: 5013 RVA: 0x00036491 File Offset: 0x00035491
		internal MeetingRequest(ItemAttachment parentAttachment)
			: base(parentAttachment)
		{
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x0003649A File Offset: 0x0003549A
		internal MeetingRequest(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x000364A3 File Offset: 0x000354A3
		public new static MeetingRequest Bind(ExchangeService service, ItemId id, PropertySet propertySet)
		{
			return service.BindToItem<MeetingRequest>(id, propertySet);
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x000364AD File Offset: 0x000354AD
		public new static MeetingRequest Bind(ExchangeService service, ItemId id)
		{
			return MeetingRequest.Bind(service, id, PropertySet.FirstClassProperties);
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x000364BB File Offset: 0x000354BB
		internal override ServiceObjectSchema GetSchema()
		{
			return MeetingRequestSchema.Instance;
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x000364C2 File Offset: 0x000354C2
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x000364C5 File Offset: 0x000354C5
		public AcceptMeetingInvitationMessage CreateAcceptMessage(bool tentative)
		{
			return new AcceptMeetingInvitationMessage(this, tentative);
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x000364CE File Offset: 0x000354CE
		public DeclineMeetingInvitationMessage CreateDeclineMessage()
		{
			return new DeclineMeetingInvitationMessage(this);
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x000364D6 File Offset: 0x000354D6
		public CalendarActionResults Accept(bool sendResponse)
		{
			return this.InternalAccept(false, sendResponse);
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x000364E0 File Offset: 0x000354E0
		public CalendarActionResults AcceptTentatively(bool sendResponse)
		{
			return this.InternalAccept(true, sendResponse);
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x000364EC File Offset: 0x000354EC
		internal CalendarActionResults InternalAccept(bool tentative, bool sendResponse)
		{
			AcceptMeetingInvitationMessage acceptMeetingInvitationMessage = this.CreateAcceptMessage(tentative);
			if (sendResponse)
			{
				return acceptMeetingInvitationMessage.SendAndSaveCopy();
			}
			return acceptMeetingInvitationMessage.Save();
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x00036514 File Offset: 0x00035514
		public CalendarActionResults Decline(bool sendResponse)
		{
			DeclineMeetingInvitationMessage declineMeetingInvitationMessage = this.CreateDeclineMessage();
			if (sendResponse)
			{
				return declineMeetingInvitationMessage.SendAndSaveCopy();
			}
			return declineMeetingInvitationMessage.Save();
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x060013A1 RID: 5025 RVA: 0x00036538 File Offset: 0x00035538
		public MeetingRequestType MeetingRequestType
		{
			get
			{
				return (MeetingRequestType)base.PropertyBag[MeetingRequestSchema.MeetingRequestType];
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x060013A2 RID: 5026 RVA: 0x0003654F File Offset: 0x0003554F
		public LegacyFreeBusyStatus IntendedFreeBusyStatus
		{
			get
			{
				return (LegacyFreeBusyStatus)base.PropertyBag[MeetingRequestSchema.IntendedFreeBusyStatus];
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x060013A3 RID: 5027 RVA: 0x00036566 File Offset: 0x00035566
		public ChangeHighlights ChangeHighlights
		{
			get
			{
				return (ChangeHighlights)base.PropertyBag[MeetingRequestSchema.ChangeHighlights];
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x060013A4 RID: 5028 RVA: 0x0003657D File Offset: 0x0003557D
		public EnhancedLocation EnhancedLocation
		{
			get
			{
				return (EnhancedLocation)base.PropertyBag[MeetingRequestSchema.EnhancedLocation];
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x060013A5 RID: 5029 RVA: 0x00036594 File Offset: 0x00035594
		public DateTime Start
		{
			get
			{
				return (DateTime)base.PropertyBag[AppointmentSchema.Start];
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x060013A6 RID: 5030 RVA: 0x000365AB File Offset: 0x000355AB
		public DateTime End
		{
			get
			{
				return (DateTime)base.PropertyBag[AppointmentSchema.End];
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x060013A7 RID: 5031 RVA: 0x000365C2 File Offset: 0x000355C2
		public DateTime OriginalStart
		{
			get
			{
				return (DateTime)base.PropertyBag[AppointmentSchema.OriginalStart];
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x060013A8 RID: 5032 RVA: 0x000365D9 File Offset: 0x000355D9
		public bool IsAllDayEvent
		{
			get
			{
				return (bool)base.PropertyBag[AppointmentSchema.IsAllDayEvent];
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x060013A9 RID: 5033 RVA: 0x000365F0 File Offset: 0x000355F0
		public LegacyFreeBusyStatus LegacyFreeBusyStatus
		{
			get
			{
				return (LegacyFreeBusyStatus)base.PropertyBag[AppointmentSchema.LegacyFreeBusyStatus];
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x060013AA RID: 5034 RVA: 0x00036607 File Offset: 0x00035607
		public string Location
		{
			get
			{
				return (string)base.PropertyBag[AppointmentSchema.Location];
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x060013AB RID: 5035 RVA: 0x0003661E File Offset: 0x0003561E
		public string When
		{
			get
			{
				return (string)base.PropertyBag[AppointmentSchema.When];
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x060013AC RID: 5036 RVA: 0x00036635 File Offset: 0x00035635
		public bool IsMeeting
		{
			get
			{
				return (bool)base.PropertyBag[AppointmentSchema.IsMeeting];
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x060013AD RID: 5037 RVA: 0x0003664C File Offset: 0x0003564C
		public bool IsCancelled
		{
			get
			{
				return (bool)base.PropertyBag[AppointmentSchema.IsCancelled];
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x060013AE RID: 5038 RVA: 0x00036663 File Offset: 0x00035663
		public bool IsRecurring
		{
			get
			{
				return (bool)base.PropertyBag[AppointmentSchema.IsRecurring];
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x060013AF RID: 5039 RVA: 0x0003667A File Offset: 0x0003567A
		public bool MeetingRequestWasSent
		{
			get
			{
				return (bool)base.PropertyBag[AppointmentSchema.MeetingRequestWasSent];
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x060013B0 RID: 5040 RVA: 0x00036691 File Offset: 0x00035691
		public AppointmentType AppointmentType
		{
			get
			{
				return (AppointmentType)base.PropertyBag[AppointmentSchema.AppointmentType];
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x060013B1 RID: 5041 RVA: 0x000366A8 File Offset: 0x000356A8
		public MeetingResponseType MyResponseType
		{
			get
			{
				return (MeetingResponseType)base.PropertyBag[AppointmentSchema.MyResponseType];
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x060013B2 RID: 5042 RVA: 0x000366BF File Offset: 0x000356BF
		public EmailAddress Organizer
		{
			get
			{
				return (EmailAddress)base.PropertyBag[AppointmentSchema.Organizer];
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x060013B3 RID: 5043 RVA: 0x000366D6 File Offset: 0x000356D6
		public AttendeeCollection RequiredAttendees
		{
			get
			{
				return (AttendeeCollection)base.PropertyBag[AppointmentSchema.RequiredAttendees];
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x060013B4 RID: 5044 RVA: 0x000366ED File Offset: 0x000356ED
		public AttendeeCollection OptionalAttendees
		{
			get
			{
				return (AttendeeCollection)base.PropertyBag[AppointmentSchema.OptionalAttendees];
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x060013B5 RID: 5045 RVA: 0x00036704 File Offset: 0x00035704
		public AttendeeCollection Resources
		{
			get
			{
				return (AttendeeCollection)base.PropertyBag[AppointmentSchema.Resources];
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x060013B6 RID: 5046 RVA: 0x0003671B File Offset: 0x0003571B
		public int ConflictingMeetingCount
		{
			get
			{
				return (int)base.PropertyBag[AppointmentSchema.ConflictingMeetingCount];
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x060013B7 RID: 5047 RVA: 0x00036732 File Offset: 0x00035732
		public int AdjacentMeetingCount
		{
			get
			{
				return (int)base.PropertyBag[AppointmentSchema.AdjacentMeetingCount];
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x060013B8 RID: 5048 RVA: 0x00036749 File Offset: 0x00035749
		public ItemCollection<Appointment> ConflictingMeetings
		{
			get
			{
				return (ItemCollection<Appointment>)base.PropertyBag[AppointmentSchema.ConflictingMeetings];
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x060013B9 RID: 5049 RVA: 0x00036760 File Offset: 0x00035760
		public ItemCollection<Appointment> AdjacentMeetings
		{
			get
			{
				return (ItemCollection<Appointment>)base.PropertyBag[AppointmentSchema.AdjacentMeetings];
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x060013BA RID: 5050 RVA: 0x00036777 File Offset: 0x00035777
		public TimeSpan Duration
		{
			get
			{
				return (TimeSpan)base.PropertyBag[AppointmentSchema.Duration];
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x060013BB RID: 5051 RVA: 0x0003678E File Offset: 0x0003578E
		public string TimeZone
		{
			get
			{
				return (string)base.PropertyBag[AppointmentSchema.TimeZone];
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x060013BC RID: 5052 RVA: 0x000367A5 File Offset: 0x000357A5
		public DateTime AppointmentReplyTime
		{
			get
			{
				return (DateTime)base.PropertyBag[AppointmentSchema.AppointmentReplyTime];
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x060013BD RID: 5053 RVA: 0x000367BC File Offset: 0x000357BC
		public int AppointmentSequenceNumber
		{
			get
			{
				return (int)base.PropertyBag[AppointmentSchema.AppointmentSequenceNumber];
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x060013BE RID: 5054 RVA: 0x000367D3 File Offset: 0x000357D3
		public int AppointmentState
		{
			get
			{
				return (int)base.PropertyBag[AppointmentSchema.AppointmentState];
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x060013BF RID: 5055 RVA: 0x000367EA File Offset: 0x000357EA
		public Recurrence Recurrence
		{
			get
			{
				return (Recurrence)base.PropertyBag[AppointmentSchema.Recurrence];
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x060013C0 RID: 5056 RVA: 0x00036801 File Offset: 0x00035801
		public OccurrenceInfo FirstOccurrence
		{
			get
			{
				return (OccurrenceInfo)base.PropertyBag[AppointmentSchema.FirstOccurrence];
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x060013C1 RID: 5057 RVA: 0x00036818 File Offset: 0x00035818
		public OccurrenceInfo LastOccurrence
		{
			get
			{
				return (OccurrenceInfo)base.PropertyBag[AppointmentSchema.LastOccurrence];
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x060013C2 RID: 5058 RVA: 0x0003682F File Offset: 0x0003582F
		public OccurrenceInfoCollection ModifiedOccurrences
		{
			get
			{
				return (OccurrenceInfoCollection)base.PropertyBag[AppointmentSchema.ModifiedOccurrences];
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x060013C3 RID: 5059 RVA: 0x00036846 File Offset: 0x00035846
		public DeletedOccurrenceInfoCollection DeletedOccurrences
		{
			get
			{
				return (DeletedOccurrenceInfoCollection)base.PropertyBag[AppointmentSchema.DeletedOccurrences];
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x060013C4 RID: 5060 RVA: 0x0003685D File Offset: 0x0003585D
		public TimeZoneInfo StartTimeZone
		{
			get
			{
				return (TimeZoneInfo)base.PropertyBag[AppointmentSchema.StartTimeZone];
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x060013C5 RID: 5061 RVA: 0x00036874 File Offset: 0x00035874
		public TimeZoneInfo EndTimeZone
		{
			get
			{
				return (TimeZoneInfo)base.PropertyBag[AppointmentSchema.EndTimeZone];
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x060013C6 RID: 5062 RVA: 0x0003688B File Offset: 0x0003588B
		public int ConferenceType
		{
			get
			{
				return (int)base.PropertyBag[AppointmentSchema.ConferenceType];
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x060013C7 RID: 5063 RVA: 0x000368A2 File Offset: 0x000358A2
		public bool AllowNewTimeProposal
		{
			get
			{
				return (bool)base.PropertyBag[AppointmentSchema.AllowNewTimeProposal];
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x060013C8 RID: 5064 RVA: 0x000368B9 File Offset: 0x000358B9
		public bool IsOnlineMeeting
		{
			get
			{
				return (bool)base.PropertyBag[AppointmentSchema.IsOnlineMeeting];
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x060013C9 RID: 5065 RVA: 0x000368D0 File Offset: 0x000358D0
		public string MeetingWorkspaceUrl
		{
			get
			{
				return (string)base.PropertyBag[AppointmentSchema.MeetingWorkspaceUrl];
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x060013CA RID: 5066 RVA: 0x000368E7 File Offset: 0x000358E7
		public string NetShowUrl
		{
			get
			{
				return (string)base.PropertyBag[AppointmentSchema.NetShowUrl];
			}
		}
	}
}
