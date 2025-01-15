using System;
using System.Collections.Generic;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Mashup.Engine1.Library.Exchange
{
	// Token: 0x02000BE2 RID: 3042
	internal class ExchangeFoldingAllowedLists
	{
		// Token: 0x04002DC1 RID: 11713
		public static readonly HashSet<PropertyDefinitionBase> FoldableProperties = new HashSet<PropertyDefinitionBase>
		{
			ItemSchema.Subject,
			EmailMessageSchema.Sender,
			ItemSchema.DisplayTo,
			ItemSchema.DisplayCc,
			ItemSchema.DateTimeReceived,
			ItemSchema.DateTimeSent,
			ItemSchema.Importance,
			ItemSchema.HasAttachments,
			ItemSchema.Body,
			EmailMessageSchema.ConversationTopic,
			ItemSchema.DateTimeCreated,
			EmailMessageSchema.From,
			ItemSchema.IsDraft,
			EmailMessageSchema.IsRead,
			ItemSchema.IsReminderSet,
			ItemSchema.IsSubmitted,
			ItemSchema.IsUnmodified,
			ItemSchema.ItemClass,
			ItemSchema.LastModifiedName,
			ItemSchema.LastModifiedTime,
			ItemSchema.ReminderMinutesBeforeStart,
			ItemSchema.Sensitivity,
			ItemSchema.Size,
			MeetingRequestSchema.Location,
			MeetingRequestSchema.Start,
			MeetingRequestSchema.End,
			MeetingRequestSchema.IsAllDayEvent,
			MeetingRequestSchema.LegacyFreeBusyStatus,
			MeetingRequestSchema.AllowNewTimeProposal,
			MeetingRequestSchema.AppointmentReplyTime,
			MeetingRequestSchema.AppointmentSequenceNumber,
			AppointmentSchema.IsOnlineMeeting,
			MeetingRequestSchema.ConferenceType,
			MeetingRequestSchema.IntendedFreeBusyStatus,
			MeetingRequestSchema.IsRecurring,
			MeetingRequestSchema.TimeZone,
			ContactSchema.DisplayName,
			ContactSchema.Surname,
			ContactSchema.GivenName,
			ContactSchema.MiddleName,
			ContactSchema.CompanyName,
			ContactSchema.JobTitle,
			ContactSchema.FileAs,
			ContactSchema.AssistantName,
			ContactSchema.Birthday,
			ContactSchema.Department,
			ContactSchema.FileAsMapping,
			ContactSchema.Generation,
			ContactSchema.HasPicture,
			ContactSchema.Initials,
			ContactSchema.NickName,
			ContactSchema.Manager,
			ContactSchema.OfficeLocation,
			TaskSchema.StartDate,
			TaskSchema.DueDate,
			TaskSchema.PercentComplete,
			TaskSchema.Owner,
			TaskSchema.IsComplete
		};

		// Token: 0x04002DC2 RID: 11714
		public static readonly HashSet<PropertyDefinitionBase> DefaultDelayedPropertiesAllowedList = new HashSet<PropertyDefinitionBase>
		{
			ItemSchema.Attachments,
			ItemSchema.Categories,
			EmailMessageSchema.Sender,
			EmailMessageSchema.From,
			EmailMessageSchema.CcRecipients,
			ItemSchema.InternetMessageHeaders,
			ItemSchema.Body,
			EmailMessageSchema.ReplyTo,
			EmailMessageSchema.ToRecipients,
			EmailMessageSchema.CcRecipients,
			EmailMessageSchema.BccRecipients,
			ItemSchema.AllowedResponseActions,
			MeetingRequestSchema.MeetingRequestType,
			MeetingRequestSchema.OriginalStart,
			MeetingRequestSchema.ConflictingMeetingCount,
			MeetingRequestSchema.AdjacentMeetingCount,
			MeetingRequestSchema.AppointmentSequenceNumber,
			MeetingRequestSchema.Recurrence,
			ContactSchema.Photo,
			ContactSchema.EmailAddresses,
			ContactSchema.ImAddresses,
			ContactSchema.PhysicalAddresses,
			ContactSchema.PhoneNumbers
		};

		// Token: 0x04002DC3 RID: 11715
		public static readonly HashSet<Type> NonFoldableTypes = new HashSet<Type>
		{
			typeof(byte[]),
			typeof(TimeSpan)
		};
	}
}
