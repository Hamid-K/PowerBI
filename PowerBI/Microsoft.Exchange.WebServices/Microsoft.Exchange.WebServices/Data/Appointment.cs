using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000196 RID: 406
	[Attachable]
	[ServiceObjectDefinition("CalendarItem")]
	public class Appointment : Item, ICalendarActionProvider
	{
		// Token: 0x06001256 RID: 4694 RVA: 0x0003430D File Offset: 0x0003330D
		public Appointment(ExchangeService service)
			: base(service)
		{
			if (service.RequestedServerVersion == ExchangeVersion.Exchange2007_SP1)
			{
				this.StartTimeZone = service.TimeZone;
			}
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x0003432A File Offset: 0x0003332A
		internal Appointment(ItemAttachment parentAttachment, bool isNew)
			: base(parentAttachment)
		{
			if (parentAttachment.Service.RequestedServerVersion == ExchangeVersion.Exchange2007_SP1 && isNew)
			{
				this.StartTimeZone = parentAttachment.Service.TimeZone;
			}
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x00034354 File Offset: 0x00033354
		public new static Appointment Bind(ExchangeService service, ItemId id, PropertySet propertySet)
		{
			return service.BindToItem<Appointment>(id, propertySet);
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x0003435E File Offset: 0x0003335E
		public new static Appointment Bind(ExchangeService service, ItemId id)
		{
			return Appointment.Bind(service, id, PropertySet.FirstClassProperties);
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x0003436C File Offset: 0x0003336C
		public static Appointment BindToOccurrence(ExchangeService service, ItemId recurringMasterId, int occurenceIndex)
		{
			return Appointment.BindToOccurrence(service, recurringMasterId, occurenceIndex, PropertySet.FirstClassProperties);
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x0003437C File Offset: 0x0003337C
		public static Appointment BindToOccurrence(ExchangeService service, ItemId recurringMasterId, int occurenceIndex, PropertySet propertySet)
		{
			AppointmentOccurrenceId appointmentOccurrenceId = new AppointmentOccurrenceId(recurringMasterId.UniqueId, occurenceIndex);
			return Appointment.Bind(service, appointmentOccurrenceId, propertySet);
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x0003439E File Offset: 0x0003339E
		public static Appointment BindToRecurringMaster(ExchangeService service, ItemId occurrenceId)
		{
			return Appointment.BindToRecurringMaster(service, occurrenceId, PropertySet.FirstClassProperties);
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x000343AC File Offset: 0x000333AC
		public static Appointment BindToRecurringMaster(ExchangeService service, ItemId occurrenceId, PropertySet propertySet)
		{
			RecurringAppointmentMasterId recurringAppointmentMasterId = new RecurringAppointmentMasterId(occurrenceId.UniqueId);
			return Appointment.Bind(service, recurringAppointmentMasterId, propertySet);
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x000343CD File Offset: 0x000333CD
		internal override ServiceObjectSchema GetSchema()
		{
			return AppointmentSchema.Instance;
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x000343D4 File Offset: 0x000333D4
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x000343D8 File Offset: 0x000333D8
		internal override bool GetIsTimeZoneHeaderRequired(bool isUpdateOperation)
		{
			if (isUpdateOperation)
			{
				return false;
			}
			bool flag = base.PropertyBag.IsPropertyUpdated(AppointmentSchema.StartTimeZone);
			bool flag2 = base.PropertyBag.IsPropertyUpdated(AppointmentSchema.EndTimeZone);
			if (flag && flag2)
			{
				TimeZoneInfo timeZoneInfo;
				base.PropertyBag.TryGetProperty<TimeZoneInfo>(AppointmentSchema.StartTimeZone, out timeZoneInfo);
				TimeZoneInfo timeZoneInfo2;
				base.PropertyBag.TryGetProperty<TimeZoneInfo>(AppointmentSchema.EndTimeZone, out timeZoneInfo2);
				return timeZoneInfo == base.Service.TimeZone || timeZoneInfo2 == base.Service.TimeZone;
			}
			return true;
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x00034458 File Offset: 0x00033458
		internal override bool GetIsCustomDateTimeScopingRequired()
		{
			return true;
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x0003445C File Offset: 0x0003345C
		internal override void Validate()
		{
			base.Validate();
			if (base.Service.RequestedServerVersion == ExchangeVersion.Exchange2007_SP1 && !base.Service.Exchange2007CompatibilityMode && (base.PropertyBag.IsPropertyUpdated(AppointmentSchema.Start) || base.PropertyBag.IsPropertyUpdated(AppointmentSchema.End) || base.PropertyBag.IsPropertyUpdated(AppointmentSchema.IsAllDayEvent) || base.PropertyBag.IsPropertyUpdated(AppointmentSchema.Recurrence)))
			{
				if (!base.PropertyBag.Contains(AppointmentSchema.StartTimeZone))
				{
					throw new ServiceLocalException(Strings.StartTimeZoneRequired);
				}
				this.StartTimeZone = this.StartTimeZone;
			}
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x00034502 File Offset: 0x00033502
		public ResponseMessage CreateReply(bool replyAll)
		{
			base.ThrowIfThisIsNew();
			return new ResponseMessage(this, replyAll ? ResponseMessageType.ReplyAll : ResponseMessageType.Reply);
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x00034518 File Offset: 0x00033518
		public void Reply(MessageBody bodyPrefix, bool replyAll)
		{
			ResponseMessage responseMessage = this.CreateReply(replyAll);
			responseMessage.BodyPrefix = bodyPrefix;
			responseMessage.SendAndSaveCopy();
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x0003453A File Offset: 0x0003353A
		public ResponseMessage CreateForward()
		{
			base.ThrowIfThisIsNew();
			return new ResponseMessage(this, ResponseMessageType.Forward);
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x00034549 File Offset: 0x00033549
		public void Forward(MessageBody bodyPrefix, params EmailAddress[] toRecipients)
		{
			this.Forward(bodyPrefix, (IEnumerable<EmailAddress>)toRecipients);
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x00034558 File Offset: 0x00033558
		public void Forward(MessageBody bodyPrefix, IEnumerable<EmailAddress> toRecipients)
		{
			ResponseMessage responseMessage = this.CreateForward();
			responseMessage.BodyPrefix = bodyPrefix;
			responseMessage.ToRecipients.AddRange(toRecipients);
			responseMessage.SendAndSaveCopy();
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x00034588 File Offset: 0x00033588
		public void Save(WellKnownFolderName destinationFolderName, SendInvitationsMode sendInvitationsMode)
		{
			base.InternalCreate(new FolderId(destinationFolderName), default(MessageDisposition?), new SendInvitationsMode?(sendInvitationsMode));
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x000345B0 File Offset: 0x000335B0
		public void Save(FolderId destinationFolderId, SendInvitationsMode sendInvitationsMode)
		{
			EwsUtilities.ValidateParam(destinationFolderId, "destinationFolderId");
			base.InternalCreate(destinationFolderId, default(MessageDisposition?), new SendInvitationsMode?(sendInvitationsMode));
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x000345E0 File Offset: 0x000335E0
		public void Save(SendInvitationsMode sendInvitationsMode)
		{
			base.InternalCreate(null, default(MessageDisposition?), new SendInvitationsMode?(sendInvitationsMode));
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x00034604 File Offset: 0x00033604
		public void Update(ConflictResolutionMode conflictResolutionMode, SendInvitationsOrCancellationsMode sendInvitationsOrCancellationsMode)
		{
			base.InternalUpdate(null, conflictResolutionMode, default(MessageDisposition?), new SendInvitationsOrCancellationsMode?(sendInvitationsOrCancellationsMode));
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x0003462C File Offset: 0x0003362C
		public void Delete(DeleteMode deleteMode, SendCancellationsMode sendCancellationsMode)
		{
			this.InternalDelete(deleteMode, new SendCancellationsMode?(sendCancellationsMode), default(AffectedTaskOccurrence?));
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x0003464F File Offset: 0x0003364F
		public AcceptMeetingInvitationMessage CreateAcceptMessage(bool tentative)
		{
			return new AcceptMeetingInvitationMessage(this, tentative);
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x00034658 File Offset: 0x00033658
		public CancelMeetingMessage CreateCancelMeetingMessage()
		{
			return new CancelMeetingMessage(this);
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x00034660 File Offset: 0x00033660
		public DeclineMeetingInvitationMessage CreateDeclineMessage()
		{
			return new DeclineMeetingInvitationMessage(this);
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x00034668 File Offset: 0x00033668
		public CalendarActionResults Accept(bool sendResponse)
		{
			return this.InternalAccept(false, sendResponse);
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x00034672 File Offset: 0x00033672
		public CalendarActionResults AcceptTentatively(bool sendResponse)
		{
			return this.InternalAccept(true, sendResponse);
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x0003467C File Offset: 0x0003367C
		internal CalendarActionResults InternalAccept(bool tentative, bool sendResponse)
		{
			AcceptMeetingInvitationMessage acceptMeetingInvitationMessage = this.CreateAcceptMessage(tentative);
			if (sendResponse)
			{
				return acceptMeetingInvitationMessage.SendAndSaveCopy();
			}
			return acceptMeetingInvitationMessage.Save();
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x000346A1 File Offset: 0x000336A1
		public CalendarActionResults CancelMeeting()
		{
			return this.CreateCancelMeetingMessage().SendAndSaveCopy();
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x000346B0 File Offset: 0x000336B0
		public CalendarActionResults CancelMeeting(string cancellationMessageText)
		{
			CancelMeetingMessage cancelMeetingMessage = this.CreateCancelMeetingMessage();
			cancelMeetingMessage.Body = cancellationMessageText;
			return cancelMeetingMessage.SendAndSaveCopy();
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x000346D8 File Offset: 0x000336D8
		public CalendarActionResults Decline(bool sendResponse)
		{
			DeclineMeetingInvitationMessage declineMeetingInvitationMessage = this.CreateDeclineMessage();
			if (sendResponse)
			{
				return declineMeetingInvitationMessage.SendAndSaveCopy();
			}
			return declineMeetingInvitationMessage.Save();
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06001276 RID: 4726 RVA: 0x000346FC File Offset: 0x000336FC
		internal override SendCancellationsMode? DefaultSendCancellationsMode
		{
			get
			{
				return new SendCancellationsMode?(SendCancellationsMode.SendToAllAndSaveCopy);
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06001277 RID: 4727 RVA: 0x00034704 File Offset: 0x00033704
		internal override SendInvitationsMode? DefaultSendInvitationsMode
		{
			get
			{
				return new SendInvitationsMode?(SendInvitationsMode.SendToAllAndSaveCopy);
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06001278 RID: 4728 RVA: 0x0003470C File Offset: 0x0003370C
		internal override SendInvitationsOrCancellationsMode? DefaultSendInvitationsOrCancellationsMode
		{
			get
			{
				return new SendInvitationsOrCancellationsMode?(SendInvitationsOrCancellationsMode.SendToAllAndSaveCopy);
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06001279 RID: 4729 RVA: 0x00034714 File Offset: 0x00033714
		// (set) Token: 0x0600127A RID: 4730 RVA: 0x0003472B File Offset: 0x0003372B
		public DateTime Start
		{
			get
			{
				return (DateTime)base.PropertyBag[AppointmentSchema.Start];
			}
			set
			{
				base.PropertyBag[AppointmentSchema.Start] = value;
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x0600127B RID: 4731 RVA: 0x00034743 File Offset: 0x00033743
		// (set) Token: 0x0600127C RID: 4732 RVA: 0x0003475A File Offset: 0x0003375A
		public DateTime End
		{
			get
			{
				return (DateTime)base.PropertyBag[AppointmentSchema.End];
			}
			set
			{
				base.PropertyBag[AppointmentSchema.End] = value;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x0600127D RID: 4733 RVA: 0x00034772 File Offset: 0x00033772
		public DateTime OriginalStart
		{
			get
			{
				return (DateTime)base.PropertyBag[AppointmentSchema.OriginalStart];
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x0600127E RID: 4734 RVA: 0x00034789 File Offset: 0x00033789
		// (set) Token: 0x0600127F RID: 4735 RVA: 0x000347A0 File Offset: 0x000337A0
		public bool IsAllDayEvent
		{
			get
			{
				return (bool)base.PropertyBag[AppointmentSchema.IsAllDayEvent];
			}
			set
			{
				base.PropertyBag[AppointmentSchema.IsAllDayEvent] = value;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06001280 RID: 4736 RVA: 0x000347B8 File Offset: 0x000337B8
		// (set) Token: 0x06001281 RID: 4737 RVA: 0x000347CF File Offset: 0x000337CF
		public LegacyFreeBusyStatus LegacyFreeBusyStatus
		{
			get
			{
				return (LegacyFreeBusyStatus)base.PropertyBag[AppointmentSchema.LegacyFreeBusyStatus];
			}
			set
			{
				base.PropertyBag[AppointmentSchema.LegacyFreeBusyStatus] = value;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06001282 RID: 4738 RVA: 0x000347E7 File Offset: 0x000337E7
		// (set) Token: 0x06001283 RID: 4739 RVA: 0x000347FE File Offset: 0x000337FE
		public string Location
		{
			get
			{
				return (string)base.PropertyBag[AppointmentSchema.Location];
			}
			set
			{
				base.PropertyBag[AppointmentSchema.Location] = value;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06001284 RID: 4740 RVA: 0x00034811 File Offset: 0x00033811
		public string When
		{
			get
			{
				return (string)base.PropertyBag[AppointmentSchema.When];
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06001285 RID: 4741 RVA: 0x00034828 File Offset: 0x00033828
		public bool IsMeeting
		{
			get
			{
				return (bool)base.PropertyBag[AppointmentSchema.IsMeeting];
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06001286 RID: 4742 RVA: 0x0003483F File Offset: 0x0003383F
		public bool IsCancelled
		{
			get
			{
				return (bool)base.PropertyBag[AppointmentSchema.IsCancelled];
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06001287 RID: 4743 RVA: 0x00034856 File Offset: 0x00033856
		public bool IsRecurring
		{
			get
			{
				return (bool)base.PropertyBag[AppointmentSchema.IsRecurring];
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06001288 RID: 4744 RVA: 0x0003486D File Offset: 0x0003386D
		public bool MeetingRequestWasSent
		{
			get
			{
				return (bool)base.PropertyBag[AppointmentSchema.MeetingRequestWasSent];
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06001289 RID: 4745 RVA: 0x00034884 File Offset: 0x00033884
		// (set) Token: 0x0600128A RID: 4746 RVA: 0x0003489B File Offset: 0x0003389B
		public bool IsResponseRequested
		{
			get
			{
				return (bool)base.PropertyBag[AppointmentSchema.IsResponseRequested];
			}
			set
			{
				base.PropertyBag[AppointmentSchema.IsResponseRequested] = value;
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x0600128B RID: 4747 RVA: 0x000348B3 File Offset: 0x000338B3
		public AppointmentType AppointmentType
		{
			get
			{
				return (AppointmentType)base.PropertyBag[AppointmentSchema.AppointmentType];
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x0600128C RID: 4748 RVA: 0x000348CA File Offset: 0x000338CA
		public MeetingResponseType MyResponseType
		{
			get
			{
				return (MeetingResponseType)base.PropertyBag[AppointmentSchema.MyResponseType];
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x0600128D RID: 4749 RVA: 0x000348E1 File Offset: 0x000338E1
		public EmailAddress Organizer
		{
			get
			{
				return (EmailAddress)base.PropertyBag[AppointmentSchema.Organizer];
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x0600128E RID: 4750 RVA: 0x000348F8 File Offset: 0x000338F8
		public AttendeeCollection RequiredAttendees
		{
			get
			{
				return (AttendeeCollection)base.PropertyBag[AppointmentSchema.RequiredAttendees];
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x0600128F RID: 4751 RVA: 0x0003490F File Offset: 0x0003390F
		public AttendeeCollection OptionalAttendees
		{
			get
			{
				return (AttendeeCollection)base.PropertyBag[AppointmentSchema.OptionalAttendees];
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06001290 RID: 4752 RVA: 0x00034926 File Offset: 0x00033926
		public AttendeeCollection Resources
		{
			get
			{
				return (AttendeeCollection)base.PropertyBag[AppointmentSchema.Resources];
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06001291 RID: 4753 RVA: 0x0003493D File Offset: 0x0003393D
		public int ConflictingMeetingCount
		{
			get
			{
				return (int)base.PropertyBag[AppointmentSchema.ConflictingMeetingCount];
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06001292 RID: 4754 RVA: 0x00034954 File Offset: 0x00033954
		public int AdjacentMeetingCount
		{
			get
			{
				return (int)base.PropertyBag[AppointmentSchema.AdjacentMeetingCount];
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06001293 RID: 4755 RVA: 0x0003496B File Offset: 0x0003396B
		public ItemCollection<Appointment> ConflictingMeetings
		{
			get
			{
				return (ItemCollection<Appointment>)base.PropertyBag[AppointmentSchema.ConflictingMeetings];
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06001294 RID: 4756 RVA: 0x00034982 File Offset: 0x00033982
		public ItemCollection<Appointment> AdjacentMeetings
		{
			get
			{
				return (ItemCollection<Appointment>)base.PropertyBag[AppointmentSchema.AdjacentMeetings];
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06001295 RID: 4757 RVA: 0x00034999 File Offset: 0x00033999
		public TimeSpan Duration
		{
			get
			{
				return (TimeSpan)base.PropertyBag[AppointmentSchema.Duration];
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06001296 RID: 4758 RVA: 0x000349B0 File Offset: 0x000339B0
		public string TimeZone
		{
			get
			{
				return (string)base.PropertyBag[AppointmentSchema.TimeZone];
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06001297 RID: 4759 RVA: 0x000349C7 File Offset: 0x000339C7
		public DateTime AppointmentReplyTime
		{
			get
			{
				return (DateTime)base.PropertyBag[AppointmentSchema.AppointmentReplyTime];
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06001298 RID: 4760 RVA: 0x000349DE File Offset: 0x000339DE
		public int AppointmentSequenceNumber
		{
			get
			{
				return (int)base.PropertyBag[AppointmentSchema.AppointmentSequenceNumber];
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06001299 RID: 4761 RVA: 0x000349F5 File Offset: 0x000339F5
		public int AppointmentState
		{
			get
			{
				return (int)base.PropertyBag[AppointmentSchema.AppointmentState];
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x0600129A RID: 4762 RVA: 0x00034A0C File Offset: 0x00033A0C
		// (set) Token: 0x0600129B RID: 4763 RVA: 0x00034A23 File Offset: 0x00033A23
		public Recurrence Recurrence
		{
			get
			{
				return (Recurrence)base.PropertyBag[AppointmentSchema.Recurrence];
			}
			set
			{
				if (value != null && value.IsRegenerationPattern)
				{
					throw new ServiceLocalException(Strings.RegenerationPatternsOnlyValidForTasks);
				}
				base.PropertyBag[AppointmentSchema.Recurrence] = value;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x0600129C RID: 4764 RVA: 0x00034A51 File Offset: 0x00033A51
		public OccurrenceInfo FirstOccurrence
		{
			get
			{
				return (OccurrenceInfo)base.PropertyBag[AppointmentSchema.FirstOccurrence];
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x0600129D RID: 4765 RVA: 0x00034A68 File Offset: 0x00033A68
		public OccurrenceInfo LastOccurrence
		{
			get
			{
				return (OccurrenceInfo)base.PropertyBag[AppointmentSchema.LastOccurrence];
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x0600129E RID: 4766 RVA: 0x00034A7F File Offset: 0x00033A7F
		public OccurrenceInfoCollection ModifiedOccurrences
		{
			get
			{
				return (OccurrenceInfoCollection)base.PropertyBag[AppointmentSchema.ModifiedOccurrences];
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x0600129F RID: 4767 RVA: 0x00034A96 File Offset: 0x00033A96
		public DeletedOccurrenceInfoCollection DeletedOccurrences
		{
			get
			{
				return (DeletedOccurrenceInfoCollection)base.PropertyBag[AppointmentSchema.DeletedOccurrences];
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x060012A0 RID: 4768 RVA: 0x00034AAD File Offset: 0x00033AAD
		// (set) Token: 0x060012A1 RID: 4769 RVA: 0x00034AC4 File Offset: 0x00033AC4
		public TimeZoneInfo StartTimeZone
		{
			get
			{
				return (TimeZoneInfo)base.PropertyBag[AppointmentSchema.StartTimeZone];
			}
			set
			{
				base.PropertyBag[AppointmentSchema.StartTimeZone] = value;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x060012A2 RID: 4770 RVA: 0x00034AD7 File Offset: 0x00033AD7
		// (set) Token: 0x060012A3 RID: 4771 RVA: 0x00034AEE File Offset: 0x00033AEE
		public TimeZoneInfo EndTimeZone
		{
			get
			{
				return (TimeZoneInfo)base.PropertyBag[AppointmentSchema.EndTimeZone];
			}
			set
			{
				base.PropertyBag[AppointmentSchema.EndTimeZone] = value;
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x060012A4 RID: 4772 RVA: 0x00034B01 File Offset: 0x00033B01
		// (set) Token: 0x060012A5 RID: 4773 RVA: 0x00034B18 File Offset: 0x00033B18
		public int ConferenceType
		{
			get
			{
				return (int)base.PropertyBag[AppointmentSchema.ConferenceType];
			}
			set
			{
				base.PropertyBag[AppointmentSchema.ConferenceType] = value;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x060012A6 RID: 4774 RVA: 0x00034B30 File Offset: 0x00033B30
		// (set) Token: 0x060012A7 RID: 4775 RVA: 0x00034B47 File Offset: 0x00033B47
		public bool AllowNewTimeProposal
		{
			get
			{
				return (bool)base.PropertyBag[AppointmentSchema.AllowNewTimeProposal];
			}
			set
			{
				base.PropertyBag[AppointmentSchema.AllowNewTimeProposal] = value;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x060012A8 RID: 4776 RVA: 0x00034B5F File Offset: 0x00033B5F
		// (set) Token: 0x060012A9 RID: 4777 RVA: 0x00034B76 File Offset: 0x00033B76
		public bool IsOnlineMeeting
		{
			get
			{
				return (bool)base.PropertyBag[AppointmentSchema.IsOnlineMeeting];
			}
			set
			{
				base.PropertyBag[AppointmentSchema.IsOnlineMeeting] = value;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x060012AA RID: 4778 RVA: 0x00034B8E File Offset: 0x00033B8E
		// (set) Token: 0x060012AB RID: 4779 RVA: 0x00034BA5 File Offset: 0x00033BA5
		public string MeetingWorkspaceUrl
		{
			get
			{
				return (string)base.PropertyBag[AppointmentSchema.MeetingWorkspaceUrl];
			}
			set
			{
				base.PropertyBag[AppointmentSchema.MeetingWorkspaceUrl] = value;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x060012AC RID: 4780 RVA: 0x00034BB8 File Offset: 0x00033BB8
		// (set) Token: 0x060012AD RID: 4781 RVA: 0x00034BCF File Offset: 0x00033BCF
		public string NetShowUrl
		{
			get
			{
				return (string)base.PropertyBag[AppointmentSchema.NetShowUrl];
			}
			set
			{
				base.PropertyBag[AppointmentSchema.NetShowUrl] = value;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x060012AE RID: 4782 RVA: 0x00034BE2 File Offset: 0x00033BE2
		// (set) Token: 0x060012AF RID: 4783 RVA: 0x00034BF9 File Offset: 0x00033BF9
		public string ICalUid
		{
			get
			{
				return (string)base.PropertyBag[AppointmentSchema.ICalUid];
			}
			set
			{
				base.PropertyBag[AppointmentSchema.ICalUid] = value;
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x060012B0 RID: 4784 RVA: 0x00034C0C File Offset: 0x00033C0C
		public DateTime? ICalRecurrenceId
		{
			get
			{
				return (DateTime?)base.PropertyBag[AppointmentSchema.ICalRecurrenceId];
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x060012B1 RID: 4785 RVA: 0x00034C23 File Offset: 0x00033C23
		public DateTime? ICalDateTimeStamp
		{
			get
			{
				return (DateTime?)base.PropertyBag[AppointmentSchema.ICalDateTimeStamp];
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x060012B2 RID: 4786 RVA: 0x00034C3A File Offset: 0x00033C3A
		// (set) Token: 0x060012B3 RID: 4787 RVA: 0x00034C51 File Offset: 0x00033C51
		public EnhancedLocation EnhancedLocation
		{
			get
			{
				return (EnhancedLocation)base.PropertyBag[AppointmentSchema.EnhancedLocation];
			}
			set
			{
				base.PropertyBag[AppointmentSchema.EnhancedLocation] = value;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x060012B4 RID: 4788 RVA: 0x00034C64 File Offset: 0x00033C64
		public string JoinOnlineMeetingUrl
		{
			get
			{
				return (string)base.PropertyBag[AppointmentSchema.JoinOnlineMeetingUrl];
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x060012B5 RID: 4789 RVA: 0x00034C7B File Offset: 0x00033C7B
		public OnlineMeetingSettings OnlineMeetingSettings
		{
			get
			{
				return (OnlineMeetingSettings)base.PropertyBag[AppointmentSchema.OnlineMeetingSettings];
			}
		}
	}
}
