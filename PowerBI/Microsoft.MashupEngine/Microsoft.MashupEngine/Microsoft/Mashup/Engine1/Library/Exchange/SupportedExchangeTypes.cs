using System;
using System.Collections.Generic;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Exchange
{
	// Token: 0x02000C0B RID: 3083
	internal static class SupportedExchangeTypes
	{
		// Token: 0x170019B4 RID: 6580
		// (get) Token: 0x060053D9 RID: 21465 RVA: 0x0011CC14 File Offset: 0x0011AE14
		public static ExtendedPropertyDefinition[] ExtendedPropertiesWithStringKeys
		{
			get
			{
				if (SupportedExchangeTypes.extendedPropertiesWithStringKeys == null)
				{
					Guid guid = new Guid("96357f7f-59e1-47d0-99a7-46515c183b54");
					Guid guid2 = new Guid("4442858E-A9E3-4E80-B900-317A210CC15B");
					SupportedExchangeTypes.extendedPropertiesWithStringKeys = new ExtendedPropertyDefinition[]
					{
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.PublicStrings, "AppName", MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.InternetHeaders, "Accept-Language", MapiPropertyType.String),
						new ExtendedPropertyDefinition(guid, "AttachmentMacContentType", MapiPropertyType.String),
						new ExtendedPropertyDefinition(guid, "AttachmentMacInfo", MapiPropertyType.Binary),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.PublicStrings, "Author", MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.PublicStrings, "ByteCount", MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.PublicStrings, "Category", MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.PublicStrings, "CharCount", MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.PublicStrings, "Comments", MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.PublicStrings, "Company", MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.InternetHeaders, "Content-Base", MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.InternetHeaders, "Content-Class", MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.InternetHeaders, "Content-Transfer-Encoding", MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.InternetHeaders, "Content-Type", MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.PublicStrings, "CreateDtmRo", MapiPropertyType.SystemTime),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.PublicStrings, "DRMLicense", MapiPropertyType.BinaryArray),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.PublicStrings, "EditTime", MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.PublicStrings, "HiddenCount", MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.PublicStrings, "http://schemas.microsoft.com/exchange/junkemailmovestamp", MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.PublicStrings, "http://schemas.microsoft.com/outlook/phishingstamp", MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.PublicStrings, "Keywords", MapiPropertyType.StringArray),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.PublicStrings, "LastAuthor", MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.PublicStrings, "LastPrinted", MapiPropertyType.SystemTime),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.PublicStrings, "LastSaveDtm", MapiPropertyType.SystemTime),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.PublicStrings, "LineCount", MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.PublicStrings, "Manager (Extended)", MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.PublicStrings, "MMClipCount", MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.PublicStrings, "NoteCount", MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.PublicStrings, "PageCount", MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.PublicStrings, "ParCount", MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.PublicStrings, "PresFormat", MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.PublicStrings, "RevNumber", MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.PublicStrings, "Security", MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.PublicStrings, "SlideCount", MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.PublicStrings, "Subject (Extended)", MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.PublicStrings, "Template", MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.PublicStrings, "Title", MapiPropertyType.String),
						new ExtendedPropertyDefinition(guid2, "UMAudioNotes", MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.PublicStrings, "WordCount", MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.InternetHeaders, "X-Sharing-Capabilities", MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.InternetHeaders, "X-Sharing-Config-Url", MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.InternetHeaders, "X-Sharing-Flavor", MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.InternetHeaders, "X-Sharing-Local-Type", MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.InternetHeaders, "X-Sharing-Provider-Guid", MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.InternetHeaders, "X-Sharing-Provider-Name", MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.InternetHeaders, "X-Sharing-Provider-Url", MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.InternetHeaders, "X-Sharing-Remote-Name", MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.InternetHeaders, "X-Sharing-Remote-Store-Uid", MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.InternetHeaders, "X-Sharing-Remote-Type", MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.InternetHeaders, "X-Sharing-Remote-Uid", MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.InternetHeaders, "Xref", MapiPropertyType.String)
					};
				}
				return SupportedExchangeTypes.extendedPropertiesWithStringKeys;
			}
		}

		// Token: 0x170019B5 RID: 6581
		// (get) Token: 0x060053DA RID: 21466 RVA: 0x0011CFAC File Offset: 0x0011B1AC
		public static Dictionary<int, string> PidLidToPropertyName
		{
			get
			{
				if (SupportedExchangeTypes.pidLidToPropertyName == null)
				{
					SupportedExchangeTypes.pidLidToPropertyName = new Dictionary<int, string>
					{
						{ 32809, "AddressBookProviderArrayType" },
						{ 32808, "AddressBookProviderEmailList" },
						{ 32989, "AddressCountryCode" },
						{ 34062, "AgingDontAgeMe" },
						{ 33336, "AllAttendeesString" },
						{ 32846, "AnniversaryEventEntryId" },
						{ 33287, "AppointmentAuxiliaryFlags" },
						{ 33300, "AppointmentColor" },
						{ 33367, "AppointmentCounterProposal" },
						{ 33299, "AppointmentDuration" },
						{ 33294, "AppointmentEndWhole" },
						{ 33283, "AppointmentLastSequence" },
						{ 36, "AppointmentMessageClass" },
						{ 33370, "AppointmentNotAllowPropose" },
						{ 33369, "AppointmentProposalNumber" },
						{ 33366, "AppointmentProposedDuration" },
						{ 33361, "AppointmentProposedEndWhole" },
						{ 33360, "AppointmentProposedStartWhole" },
						{ 33302, "AppointmentRecur" },
						{ 33328, "AppointmentReplyName" },
						{ 33312, "AppointmentReplyTime (Extended)" },
						{ 33281, "AppointmentSequence" },
						{ 33282, "AppointmentSequenceTime" },
						{ 33293, "AppointmentStartWhole" },
						{ 33303, "AppointmentStateFlags" },
						{ 33301, "AppointmentSubType" },
						{ 33375, "AppointmentTimeZoneDefinitionEndDisplay" },
						{ 33376, "AppointmentTimeZoneDefinitionRecur" },
						{ 33374, "AppointmentTimeZoneDefinitionStartDisplay" },
						{ 33341, "AppointmentUnsendableRecipients" },
						{ 1, "AttendeeCriticalChange" },
						{ 33338, "AutoFillLocation" },
						{ 32805, "AutoLog" },
						{ 34074, "AutoProcessState" },
						{ 34101, "Billing" },
						{ 32845, "BirthdayEventEntryId" },
						{ 32833, "BusinessCardCardPicture" },
						{ 32832, "BusinessCardDisplayDefinition" },
						{ 33285, "BusyStatus" },
						{ 28, "CalendarType" },
						{ 9000, "Categories (Extended)" },
						{ 33340, "CcAttendeesString" },
						{ 33284, "ChangeHighlight" },
						{ 34230, "Classification" },
						{ 34231, "ClassificationDescription" },
						{ 34232, "ClassificationGuid" },
						{ 34234, "ClassificationKeep" },
						{ 34229, "Classified" },
						{ 35, "CleanGlobalObjectId" },
						{ 33334, "ClipEnd" },
						{ 33333, "ClipStart" },
						{ 34071, "CommonEnd" },
						{ 34070, "CommonStart" },
						{ 34105, "Companies" },
						{ 32803, "ContactCharacterSet" },
						{ 32775, "ContactItemData" },
						{ 34181, "ContactLinkEntry" },
						{ 34182, "ContactLinkName" },
						{ 34180, "ContactLinkSearchKey" },
						{ 34106, "Contacts" },
						{ 32847, "ContactUserField1" },
						{ 32848, "ContactUserField2" },
						{ 32849, "ContactUserField3" },
						{ 32850, "ContactUserField4" },
						{ 34130, "CurrentVersion" },
						{ 34132, "CurrentVersionName" },
						{ 32844, "DistributionListChecksum" },
						{ 32853, "DistributionListMembers" },
						{ 32851, "DistributionListName" },
						{ 32852, "DistributionListOneOffMembers" },
						{ 32898, "Email1AddressType" },
						{ 32896, "Email1DisplayName" },
						{ 32899, "Email1EmailAddress" },
						{ 32900, "Email1OriginalDisplayName" },
						{ 32901, "Email1OriginalEntryId" },
						{ 32914, "Email2AddressType" },
						{ 32912, "Email2DisplayName" },
						{ 32915, "Email2EmailAddress" },
						{ 32916, "Email2OriginalDisplayName" },
						{ 32917, "Email2OriginalEntryId" },
						{ 32930, "Email3AddressType" },
						{ 32928, "Email3DisplayName" },
						{ 32931, "Email3EmailAddress" },
						{ 32932, "Email3OriginalDisplayName" },
						{ 32933, "Email3OriginalEntryId" },
						{ 33320, "ExceptionReplaceTime" },
						{ 32946, "Fax1AddressType" },
						{ 32944, "Fax1DisplayName" },
						{ 32947, "Fax1EmailAddress" },
						{ 32951, "Fax1EmailType" },
						{ 32945, "Fax1EntryId" },
						{ 32948, "Fax1OriginalDisplayName" },
						{ 32949, "Fax1OriginalEntryId" },
						{ 32950, "Fax1RichTextFormat" },
						{ 32962, "Fax2AddressType" },
						{ 32960, "Fax2DisplayName" },
						{ 32963, "Fax2EmailAddress" },
						{ 32967, "Fax2EmailType" },
						{ 32961, "Fax2EntryId" },
						{ 32964, "Fax2OriginalDisplayName" },
						{ 32965, "Fax2OriginalEntryId" },
						{ 32966, "Fax2RichTextFormat" },
						{ 32978, "Fax3AddressType" },
						{ 32976, "Fax3DisplayName" },
						{ 32979, "Fax3EmailAddress" },
						{ 32983, "Fax3EmailType" },
						{ 32977, "Fax3EntryId" },
						{ 32980, "Fax3OriginalDisplayName" },
						{ 32981, "Fax3OriginalEntryId" },
						{ 32982, "Fax3RichTextFormat" },
						{ 33323, "FExceptionalAttendees" },
						{ 33286, "FExceptionalBody" },
						{ 32773, "FileUnder" },
						{ 32774, "FileUnderId" },
						{ 32806, "FileUnderList" },
						{ 33321, "FInvited" },
						{ 34096, "FlagRequest" },
						{ 34240, "FlagString" },
						{ 33290, "ForwardInstance" },
						{ 34213, "FShouldTNEF" },
						{ 32984, "FreeBusyLocation" },
						{ 3, "GlobalObjectId" },
						{ 32789, "HasPicture (Extended)" },
						{ 34168, "HeaderItem" },
						{ 32794, "HomeAddress" },
						{ 32986, "HomeAddressCountryCode" },
						{ 32811, "Html" },
						{ 34195, "ImageAttachmentsCompressionLevel" },
						{ 34160, "ImapDeleted" },
						{ 32866, "InstantMessagingAddress" },
						{ 33316, "IntendedBusyStatus" },
						{ 34176, "InternetAccountName" },
						{ 34177, "InternetAccountStamp" },
						{ 10, "IsException" },
						{ 34203, "IsInterpersonalFax" },
						{ 5, "IsRecurring (Extended)" },
						{ 4, "IsSilent" },
						{ 33292, "LinkedTaskItems" },
						{ 33288, "Location (Extended)" },
						{ 34577, "LogDocumentPosted" },
						{ 34574, "LogDocumentPrinted" },
						{ 34576, "LogDocumentRouted" },
						{ 34575, "LogDocumentSaved" },
						{ 34567, "LogDuration" },
						{ 34568, "LogEnd" },
						{ 34572, "LogFlags" },
						{ 34566, "LogStart" },
						{ 34560, "LogType" },
						{ 34578, "LogTypeDesc" },
						{ 38, "MeetingType" },
						{ 33289, "MeetingWorkspaceUrl (Extended)" },
						{ 34100, "Mileage (Extended)" },
						{ 34104, "NonSendableBcc" },
						{ 34103, "NonSendableCc" },
						{ 34102, "NonSendableTo" },
						{ 34117, "NonSendBccTrackStatus" },
						{ 34116, "NonSendCcTrackStatus" },
						{ 34115, "NonSendToTrackStatus" },
						{ 35584, "NoteColor" },
						{ 35587, "NoteHeight" },
						{ 35586, "NoteWidth" },
						{ 35588, "NoteX" },
						{ 35589, "NoteY" },
						{ 34233, "OfflineStatus" },
						{ 40, "OldLocation" },
						{ 42, "OldWhenEndWhole" },
						{ 41, "OldWhenStartWhole" },
						{ 33335, "OriginalStoreEntryId" },
						{ 32796, "OtherAddress" },
						{ 32988, "OtherAddressCountryCode" },
						{ 26, "OwnerCriticalChange" },
						{ 33026, "PercentComplete (Extended)" },
						{ 32802, "PostalAddressId" },
						{ 35076, "PostRssChannel" },
						{ 35072, "PostRssChannelLink" },
						{ 34054, "Private" },
						{ 34112, "PropertyDefinitionStream" },
						{ 34121, "RecallTime" },
						{ 33330, "RecurrencePattern" },
						{ 33329, "RecurrenceType" },
						{ 33315, "Recurring" },
						{ 34237, "ReferenceEntryId" },
						{ 34049, "ReminderDelta" },
						{ 34079, "ReminderFileParameter" },
						{ 34076, "ReminderOverride" },
						{ 34078, "ReminderPlaySound" },
						{ 34051, "ReminderSet" },
						{ 34144, "ReminderSignalTime" },
						{ 34050, "ReminderTime" },
						{ 36615, "RemoteAttachment" },
						{ 36609, "RemoteEntryId" },
						{ 36610, "RemoteMessageClass" },
						{ 36614, "RemoteSearchKey" },
						{ 36613, "RemoteTransferSize" },
						{ 36612, "RemoteTransferTime" },
						{ 36611, "RemoteTransport" },
						{ 36096, "ResendTime" },
						{ 33304, "ResponseStatus" },
						{ 33280, "SendMeetingAsIcal" },
						{ 35351, "SharingCapabilities" },
						{ 35364, "SharingConfigurationUrl" },
						{ 35352, "SharingFlavor" },
						{ 35337, "SharingInitiatorEntryId" },
						{ 35335, "SharingInitiatorName" },
						{ 35336, "SharingInitiatorSmtp" },
						{ 35348, "SharingLocalType" },
						{ 35329, "SharingProviderGuid" },
						{ 35330, "SharingProviderName" },
						{ 35331, "SharingProviderUrl" },
						{ 35333, "SharingRemoteName" },
						{ 35400, "SharingRemoteStoreUid" },
						{ 35357, "SharingRemoteType" },
						{ 35334, "SharingRemoteUid" },
						{ 35368, "SharingResponseTime" },
						{ 35367, "SharingResponseType" },
						{ 34064, "SideEffects" },
						{ 34068, "SmartNoAttach" },
						{ 34204, "SpamOriginalFolder" },
						{ 33066, "TaskAcceptanceState" },
						{ 33032, "TaskAccepted" },
						{ 33040, "TaskActualEffort" },
						{ 33057, "TaskAssigner" },
						{ 33047, "TaskAssigners" },
						{ 33052, "TaskComplete" },
						{ 33039, "TaskDateCompleted" },
						{ 33033, "TaskDeadOccurrence" },
						{ 33029, "TaskDueDate" },
						{ 33041, "TaskEstimatedEffort" },
						{ 33054, "TaskFCreator" },
						{ 33068, "TaskFFixOffline" },
						{ 33062, "TaskFRecurring" },
						{ 34073, "TaskGlobalId" },
						{ 33050, "TaskHistory" },
						{ 33061, "TaskLastDelegate" },
						{ 33045, "TaskLastUpdate" },
						{ 33058, "TaskLastUser" },
						{ 34072, "TaskMode" },
						{ 33056, "TaskMultipleRecipients" },
						{ 33059, "TaskOrdinal" },
						{ 33055, "TaskOwner" },
						{ 33065, "TaskOwnership" },
						{ 33046, "TaskRecurrence" },
						{ 33031, "TaskResetReminder" },
						{ 33028, "TaskStartDate" },
						{ 33043, "TaskState" },
						{ 33025, "TaskStatus" },
						{ 33049, "TaskStatusOnComplete" },
						{ 33051, "TaskUpdates" },
						{ 33042, "TaskVersion" },
						{ 12, "TimeZone (Extended)" },
						{ 33332, "TimeZoneDescription" },
						{ 33331, "TimeZoneStruct" },
						{ 33339, "ToAttendeesString" },
						{ 34208, "ToDoOrdinalDate" },
						{ 34209, "ToDoSubOrdinal" },
						{ 34212, "ToDoTitle" },
						{ 32985, "UserX509Certificate" },
						{ 34178, "UseTnef" },
						{ 34239, "ValidFlagStringProof" },
						{ 34084, "VerbResponse" },
						{ 34080, "VerbStream" },
						{ 2, "Where" },
						{ 32795, "WorkAddress" },
						{ 32838, "WorkAddressCity" },
						{ 32841, "WorkAddressCountry" },
						{ 32987, "WorkAddressCountryCode" },
						{ 32840, "WorkAddressPostalCode" },
						{ 32842, "WorkAddressPostOfficeBox" },
						{ 32839, "WorkAddressState" },
						{ 32837, "WorkAddressStreet" },
						{ 32814, "YomiCompanyName" },
						{ 32812, "YomiFirstName" },
						{ 32813, "YomiLastName" }
					};
				}
				return SupportedExchangeTypes.pidLidToPropertyName;
			}
		}

		// Token: 0x170019B6 RID: 6582
		// (get) Token: 0x060053DB RID: 21467 RVA: 0x0011E0B0 File Offset: 0x0011C2B0
		public static ExtendedPropertyDefinition[] ExtendedPropertiesWithNumericKeys
		{
			get
			{
				if (SupportedExchangeTypes.extendedPropertiesWithNumericKeys == null)
				{
					Guid guid = new Guid("0006200A-0000-0000-C000-000000000046");
					Guid guid2 = new Guid("0006200E-0000-0000-C000-000000000046");
					Guid guid3 = new Guid("00062014-0000-0000-C000-000000000046");
					Guid guid4 = new Guid("00062040-0000-0000-C000-000000000046");
					Guid guid5 = new Guid("00062041-0000-0000-C000-000000000046");
					SupportedExchangeTypes.extendedPropertiesWithNumericKeys = new ExtendedPropertyDefinition[]
					{
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32809, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32808, MapiPropertyType.IntegerArray),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32989, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34062, MapiPropertyType.Boolean),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Appointment, 33336, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32846, MapiPropertyType.Binary),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Appointment, 33287, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Appointment, 33300, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Appointment, 33367, MapiPropertyType.Boolean),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Appointment, 33299, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Appointment, 33294, MapiPropertyType.SystemTime),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Appointment, 33283, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Meeting, 36, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Appointment, 33370, MapiPropertyType.Boolean),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Appointment, 33369, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Appointment, 33366, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Appointment, 33361, MapiPropertyType.SystemTime),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Appointment, 33360, MapiPropertyType.SystemTime),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Appointment, 33302, MapiPropertyType.Binary),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Appointment, 33328, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Appointment, 33312, MapiPropertyType.SystemTime),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Appointment, 33281, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Appointment, 33282, MapiPropertyType.SystemTime),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Appointment, 33293, MapiPropertyType.SystemTime),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Appointment, 33303, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Appointment, 33301, MapiPropertyType.Boolean),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Appointment, 33375, MapiPropertyType.Binary),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Appointment, 33376, MapiPropertyType.Binary),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Appointment, 33374, MapiPropertyType.Binary),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Appointment, 33341, MapiPropertyType.Binary),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Meeting, 1, MapiPropertyType.SystemTime),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Appointment, 33338, MapiPropertyType.Boolean),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32805, MapiPropertyType.Boolean),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34074, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34101, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32845, MapiPropertyType.Binary),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32833, MapiPropertyType.Binary),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32832, MapiPropertyType.Binary),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Appointment, 33285, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Meeting, 28, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.PublicStrings, 9000, MapiPropertyType.StringArray),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Appointment, 33340, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Appointment, 33284, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34230, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34231, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34232, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34234, MapiPropertyType.Boolean),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34229, MapiPropertyType.Boolean),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Meeting, 35, MapiPropertyType.Binary),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Appointment, 33334, MapiPropertyType.SystemTime),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Appointment, 33333, MapiPropertyType.SystemTime),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34071, MapiPropertyType.SystemTime),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34070, MapiPropertyType.SystemTime),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34105, MapiPropertyType.StringArray),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32803, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32775, MapiPropertyType.IntegerArray),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34181, MapiPropertyType.Binary),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34182, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34180, MapiPropertyType.Binary),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34106, MapiPropertyType.StringArray),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32847, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32848, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32849, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32850, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34130, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34132, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32844, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32853, MapiPropertyType.BinaryArray),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32851, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32852, MapiPropertyType.BinaryArray),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32898, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32896, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32899, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32900, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32901, MapiPropertyType.Binary),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32914, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32912, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32915, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32916, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32917, MapiPropertyType.Binary),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32930, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32928, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32931, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32932, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32933, MapiPropertyType.Binary),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Appointment, 33320, MapiPropertyType.SystemTime),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32946, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32944, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32947, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32951, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32945, MapiPropertyType.Binary),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32948, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32949, MapiPropertyType.Binary),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32950, MapiPropertyType.Boolean),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32962, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32960, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32963, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32967, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32961, MapiPropertyType.Binary),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32964, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32965, MapiPropertyType.Binary),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32966, MapiPropertyType.Boolean),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32978, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32976, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32979, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32983, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32977, MapiPropertyType.Binary),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32980, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32981, MapiPropertyType.Binary),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32982, MapiPropertyType.Boolean),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Appointment, 33323, MapiPropertyType.Boolean),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Appointment, 33286, MapiPropertyType.Boolean),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32773, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32774, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32806, MapiPropertyType.IntegerArray),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Appointment, 33321, MapiPropertyType.Boolean),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34096, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34240, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Appointment, 33290, MapiPropertyType.Boolean),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34213, MapiPropertyType.Boolean),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32984, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Meeting, 3, MapiPropertyType.Binary),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32789, MapiPropertyType.Boolean),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34168, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32794, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32986, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32811, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34195, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34160, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32866, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Appointment, 33316, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34176, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34177, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Meeting, 10, MapiPropertyType.Boolean),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34203, MapiPropertyType.Boolean),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Meeting, 5, MapiPropertyType.Boolean),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Meeting, 4, MapiPropertyType.Boolean),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Appointment, 33292, MapiPropertyType.BinaryArray),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Appointment, 33288, MapiPropertyType.String),
						new ExtendedPropertyDefinition(guid, 34577, MapiPropertyType.Boolean),
						new ExtendedPropertyDefinition(guid, 34574, MapiPropertyType.Boolean),
						new ExtendedPropertyDefinition(guid, 34576, MapiPropertyType.Boolean),
						new ExtendedPropertyDefinition(guid, 34575, MapiPropertyType.Boolean),
						new ExtendedPropertyDefinition(guid, 34567, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(guid, 34568, MapiPropertyType.SystemTime),
						new ExtendedPropertyDefinition(guid, 34572, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(guid, 34566, MapiPropertyType.SystemTime),
						new ExtendedPropertyDefinition(guid, 34560, MapiPropertyType.String),
						new ExtendedPropertyDefinition(guid, 34578, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Meeting, 38, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Appointment, 33289, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34100, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34104, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34103, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34102, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34117, MapiPropertyType.IntegerArray),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34116, MapiPropertyType.IntegerArray),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34115, MapiPropertyType.IntegerArray),
						new ExtendedPropertyDefinition(guid2, 35584, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(guid2, 35587, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(guid2, 35586, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(guid2, 35588, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(guid2, 35589, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34233, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Meeting, 40, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Meeting, 42, MapiPropertyType.SystemTime),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Meeting, 41, MapiPropertyType.SystemTime),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Appointment, 33335, MapiPropertyType.Binary),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32796, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32988, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Meeting, 26, MapiPropertyType.SystemTime),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Task, 33026, MapiPropertyType.Double),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32802, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(guid5, 35076, MapiPropertyType.String),
						new ExtendedPropertyDefinition(guid5, 35072, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34054, MapiPropertyType.Boolean),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34112, MapiPropertyType.Binary),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34121, MapiPropertyType.SystemTime),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Appointment, 33330, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Appointment, 33329, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Appointment, 33315, MapiPropertyType.Boolean),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34237, MapiPropertyType.Binary),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34049, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34079, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34076, MapiPropertyType.Boolean),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34078, MapiPropertyType.Boolean),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34051, MapiPropertyType.Boolean),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34144, MapiPropertyType.SystemTime),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34050, MapiPropertyType.SystemTime),
						new ExtendedPropertyDefinition(guid3, 36615, MapiPropertyType.Boolean),
						new ExtendedPropertyDefinition(guid3, 36609, MapiPropertyType.Binary),
						new ExtendedPropertyDefinition(guid3, 36610, MapiPropertyType.String),
						new ExtendedPropertyDefinition(guid3, 36614, MapiPropertyType.Binary),
						new ExtendedPropertyDefinition(guid3, 36613, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(guid3, 36612, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(guid3, 36611, MapiPropertyType.String),
						new ExtendedPropertyDefinition(guid3, 36096, MapiPropertyType.SystemTime),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Appointment, 33304, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Appointment, 33280, MapiPropertyType.Boolean),
						new ExtendedPropertyDefinition(guid4, 35351, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(guid4, 35364, MapiPropertyType.String),
						new ExtendedPropertyDefinition(guid4, 35352, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(guid4, 35337, MapiPropertyType.Binary),
						new ExtendedPropertyDefinition(guid4, 35335, MapiPropertyType.String),
						new ExtendedPropertyDefinition(guid4, 35336, MapiPropertyType.String),
						new ExtendedPropertyDefinition(guid4, 35348, MapiPropertyType.String),
						new ExtendedPropertyDefinition(guid4, 35329, MapiPropertyType.Binary),
						new ExtendedPropertyDefinition(guid4, 35330, MapiPropertyType.String),
						new ExtendedPropertyDefinition(guid4, 35331, MapiPropertyType.String),
						new ExtendedPropertyDefinition(guid4, 35333, MapiPropertyType.String),
						new ExtendedPropertyDefinition(guid4, 35400, MapiPropertyType.String),
						new ExtendedPropertyDefinition(guid4, 35357, MapiPropertyType.String),
						new ExtendedPropertyDefinition(guid4, 35334, MapiPropertyType.String),
						new ExtendedPropertyDefinition(guid4, 35368, MapiPropertyType.SystemTime),
						new ExtendedPropertyDefinition(guid4, 35367, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34064, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34068, MapiPropertyType.Boolean),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34204, MapiPropertyType.Binary),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Task, 33066, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Task, 33032, MapiPropertyType.Boolean),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Task, 33040, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Task, 33057, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Task, 33047, MapiPropertyType.Binary),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Task, 33052, MapiPropertyType.Boolean),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Task, 33039, MapiPropertyType.SystemTime),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Task, 33033, MapiPropertyType.Boolean),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Task, 33029, MapiPropertyType.SystemTime),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Task, 33041, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Task, 33054, MapiPropertyType.Boolean),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Task, 33068, MapiPropertyType.Boolean),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Task, 33062, MapiPropertyType.Boolean),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34073, MapiPropertyType.Binary),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Task, 33050, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Task, 33061, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Task, 33045, MapiPropertyType.SystemTime),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Task, 33058, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34072, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Task, 33056, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Task, 33059, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Task, 33055, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Task, 33065, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Task, 33046, MapiPropertyType.Binary),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Task, 33031, MapiPropertyType.Boolean),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Task, 33028, MapiPropertyType.SystemTime),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Task, 33043, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Task, 33025, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Task, 33049, MapiPropertyType.Boolean),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Task, 33051, MapiPropertyType.Boolean),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Task, 33042, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Meeting, 12, MapiPropertyType.Integer),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Appointment, 33332, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Appointment, 33331, MapiPropertyType.Binary),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Appointment, 33339, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34208, MapiPropertyType.SystemTime),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34209, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34212, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32985, MapiPropertyType.BinaryArray),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34178, MapiPropertyType.Boolean),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34239, MapiPropertyType.SystemTime),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34084, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Common, 34080, MapiPropertyType.Binary),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Meeting, 2, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32795, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32838, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32841, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32987, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32840, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32842, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32839, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32837, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32814, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32812, MapiPropertyType.String),
						new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Address, 32813, MapiPropertyType.String)
					};
				}
				return SupportedExchangeTypes.extendedPropertiesWithNumericKeys;
			}
		}

		// Token: 0x060053DC RID: 21468 RVA: 0x0011F460 File Offset: 0x0011D660
		public static string GetPropertyNameFromPid(int pid)
		{
			string text;
			if (SupportedExchangeTypes.PidLidToPropertyName.TryGetValue(pid, out text))
			{
				return text;
			}
			throw new InvalidOperationException("No property defined with Property ID = " + pid.ToString() + ".");
		}

		// Token: 0x060053DD RID: 21469 RVA: 0x0011F499 File Offset: 0x0011D699
		public static bool TryGetComplexType(Type type, out FieldSelectorInfo[] fieldSelectors)
		{
			return SupportedExchangeTypes.ComplexTypeToFieldSelectors.TryGetValue(type, out fieldSelectors) || SupportedExchangeTypes.ComplexTypeToFieldSelectors.TryGetValue(type.BaseType, out fieldSelectors);
		}

		// Token: 0x060053DE RID: 21470 RVA: 0x0011F4BC File Offset: 0x0011D6BC
		public static bool TryGetPrimitiveType(Type type, out MarshalInfo marshalInfo)
		{
			return SupportedExchangeTypes.PrimitiveTypeToTypeValue.TryGetValue(type, out marshalInfo) || SupportedExchangeTypes.PrimitiveTypeToTypeValue.TryGetValue(type.BaseType, out marshalInfo);
		}

		// Token: 0x060053DF RID: 21471 RVA: 0x0011F4DF File Offset: 0x0011D6DF
		public static bool TryGetAddionalProperties(PropertyDefinitionBase property, out IndexedPropertyDefinition[] additionalProperties)
		{
			return SupportedExchangeTypes.AdditionalIndexedProperties.TryGetValue(property, out additionalProperties);
		}

		// Token: 0x04002E6F RID: 11887
		private static ExtendedPropertyDefinition[] extendedPropertiesWithNumericKeys;

		// Token: 0x04002E70 RID: 11888
		private static ExtendedPropertyDefinition[] extendedPropertiesWithStringKeys;

		// Token: 0x04002E71 RID: 11889
		public static readonly Dictionary<MapiPropertyType, TypeValue> MapiToTypeValue = new Dictionary<MapiPropertyType, TypeValue>
		{
			{
				MapiPropertyType.ApplicationTime,
				TypeValue.Time
			},
			{
				MapiPropertyType.ApplicationTimeArray,
				TypeValue.List
			},
			{
				MapiPropertyType.Binary,
				TypeValue.Binary
			},
			{
				MapiPropertyType.BinaryArray,
				TypeValue.List
			},
			{
				MapiPropertyType.Boolean,
				TypeValue.Logical
			},
			{
				MapiPropertyType.CLSID,
				TypeValue.Text
			},
			{
				MapiPropertyType.CLSIDArray,
				TypeValue.List
			},
			{
				MapiPropertyType.Currency,
				TypeValue.Currency
			},
			{
				MapiPropertyType.CurrencyArray,
				TypeValue.List
			},
			{
				MapiPropertyType.Double,
				TypeValue.Double
			},
			{
				MapiPropertyType.DoubleArray,
				TypeValue.List
			},
			{
				MapiPropertyType.Error,
				TypeValue.Any
			},
			{
				MapiPropertyType.Float,
				TypeValue.Double
			},
			{
				MapiPropertyType.FloatArray,
				TypeValue.List
			},
			{
				MapiPropertyType.Integer,
				TypeValue.Int32
			},
			{
				MapiPropertyType.IntegerArray,
				TypeValue.List
			},
			{
				MapiPropertyType.Long,
				TypeValue.Int64
			},
			{
				MapiPropertyType.LongArray,
				TypeValue.List
			},
			{
				MapiPropertyType.Null,
				TypeValue.Null
			},
			{
				MapiPropertyType.Object,
				TypeValue.Any
			},
			{
				MapiPropertyType.ObjectArray,
				TypeValue.List
			},
			{
				MapiPropertyType.Short,
				TypeValue.Int16
			},
			{
				MapiPropertyType.ShortArray,
				TypeValue.List
			},
			{
				MapiPropertyType.String,
				TypeValue.Text
			},
			{
				MapiPropertyType.StringArray,
				TypeValue.List
			},
			{
				MapiPropertyType.SystemTime,
				TypeValue.DateTime
			},
			{
				MapiPropertyType.SystemTimeArray,
				TypeValue.List
			}
		};

		// Token: 0x04002E72 RID: 11890
		public static Dictionary<int, string> pidLidToPropertyName;

		// Token: 0x04002E73 RID: 11891
		private static readonly FieldSelectorInfo[] CompleteNameFieldSelectors = new FieldSelectorInfo[]
		{
			new FieldSelectorInfo("FullName", typeof(string), (object x) => ((CompleteName)x).FullName),
			new FieldSelectorInfo("GivenName", typeof(string), (object x) => ((CompleteName)x).GivenName),
			new FieldSelectorInfo("Initials", typeof(string), (object x) => ((CompleteName)x).Initials),
			new FieldSelectorInfo("MiddleName", typeof(string), (object x) => ((CompleteName)x).MiddleName),
			new FieldSelectorInfo("NickName", typeof(string), (object x) => ((CompleteName)x).NickName),
			new FieldSelectorInfo("Suffix", typeof(string), (object x) => ((CompleteName)x).Suffix),
			new FieldSelectorInfo("Surname", typeof(string), (object x) => ((CompleteName)x).Surname),
			new FieldSelectorInfo("YomiGivenName", typeof(string), (object x) => ((CompleteName)x).YomiGivenName),
			new FieldSelectorInfo("YomiSurname", typeof(string), (object x) => ((CompleteName)x).YomiSurname)
		};

		// Token: 0x04002E74 RID: 11892
		private static readonly FieldSelectorInfo[] EmailAddressFieldSelectors = new FieldSelectorInfo[]
		{
			new FieldSelectorInfo("Name", typeof(string), (object x) => ((EmailAddress)x).Name),
			new FieldSelectorInfo("Address", typeof(string), (object x) => ((EmailAddress)x).Address, true)
		};

		// Token: 0x04002E75 RID: 11893
		private static readonly FieldSelectorInfo[] AttendeeSelectors = new FieldSelectorInfo[]
		{
			new FieldSelectorInfo("Name", typeof(string), (object x) => ((Attendee)x).Name),
			new FieldSelectorInfo("Address", typeof(string), (object x) => ((Attendee)x).Address, true),
			new FieldSelectorInfo("Response", typeof(string), (object x) => ((Attendee)x).ResponseType.ToString()),
			new FieldSelectorInfo("LastResponseTime", typeof(DateTime?), (object x) => ((Attendee)x).LastResponseTime)
		};

		// Token: 0x04002E76 RID: 11894
		private static readonly FieldSelectorInfo[] RecurrenceFieldSelectors = new FieldSelectorInfo[]
		{
			new FieldSelectorInfo("StartDate", typeof(DateTime), (object x) => ((Recurrence)x).StartDate),
			new FieldSelectorInfo("EndDate", typeof(DateTime), (object x) => ((Recurrence)x).EndDate),
			new FieldSelectorInfo("HasEnd", typeof(bool), (object x) => ((Recurrence)x).HasEnd),
			new FieldSelectorInfo("NumberOfOccurrences", typeof(int?), (object x) => ((Recurrence)x).NumberOfOccurrences),
			new FieldSelectorInfo("Pattern", typeof(string), (object x) => x.GetType().Name),
			new FieldSelectorInfo("Interval", typeof(int), delegate(object x)
			{
				Recurrence.IntervalPattern intervalPattern = x as Recurrence.IntervalPattern;
				if (intervalPattern != null)
				{
					return intervalPattern.Interval;
				}
				return null;
			}),
			new FieldSelectorInfo("DayOfMonth", typeof(int), delegate(object x)
			{
				Recurrence.MonthlyPattern monthlyPattern = x as Recurrence.MonthlyPattern;
				if (monthlyPattern != null)
				{
					return monthlyPattern.DayOfMonth;
				}
				Recurrence.YearlyPattern yearlyPattern = x as Recurrence.YearlyPattern;
				if (yearlyPattern != null)
				{
					return yearlyPattern.DayOfMonth;
				}
				return null;
			}),
			new FieldSelectorInfo("DayOfTheWeek", typeof(string), delegate(object x)
			{
				Recurrence.RelativeMonthlyPattern relativeMonthlyPattern = x as Recurrence.RelativeMonthlyPattern;
				if (relativeMonthlyPattern != null)
				{
					return relativeMonthlyPattern.DayOfTheWeek.ToString();
				}
				Recurrence.RelativeYearlyPattern relativeYearlyPattern = x as Recurrence.RelativeYearlyPattern;
				if (relativeYearlyPattern != null)
				{
					return relativeYearlyPattern.DayOfTheWeek.ToString();
				}
				return null;
			}),
			new FieldSelectorInfo("DayOfTheWeekIndex", typeof(string), delegate(object x)
			{
				Recurrence.RelativeMonthlyPattern relativeMonthlyPattern2 = x as Recurrence.RelativeMonthlyPattern;
				if (relativeMonthlyPattern2 != null)
				{
					return relativeMonthlyPattern2.DayOfTheWeekIndex.ToString();
				}
				Recurrence.RelativeYearlyPattern relativeYearlyPattern2 = x as Recurrence.RelativeYearlyPattern;
				if (relativeYearlyPattern2 != null)
				{
					return relativeYearlyPattern2.DayOfTheWeekIndex.ToString();
				}
				return null;
			}),
			new FieldSelectorInfo("DaysOfTheWeek", typeof(string), delegate(object x)
			{
				Recurrence.WeeklyPattern weeklyPattern = x as Recurrence.WeeklyPattern;
				if (weeklyPattern != null)
				{
					return weeklyPattern.DaysOfTheWeek.ToString();
				}
				return null;
			}),
			new FieldSelectorInfo("FirstDayOfWeek", typeof(string), delegate(object x)
			{
				Recurrence.WeeklyPattern weeklyPattern2 = x as Recurrence.WeeklyPattern;
				if (weeklyPattern2 != null)
				{
					return weeklyPattern2.FirstDayOfWeek.ToString();
				}
				return null;
			}),
			new FieldSelectorInfo("Month", typeof(int), delegate(object x)
			{
				Recurrence.RelativeYearlyPattern relativeYearlyPattern3 = x as Recurrence.RelativeYearlyPattern;
				if (relativeYearlyPattern3 != null)
				{
					return (int)relativeYearlyPattern3.Month;
				}
				Recurrence.YearlyPattern yearlyPattern2 = x as Recurrence.YearlyPattern;
				if (yearlyPattern2 != null)
				{
					return (int)yearlyPattern2.Month;
				}
				return null;
			})
		};

		// Token: 0x04002E77 RID: 11895
		private static readonly FieldSelectorInfo[] GroupMemberFieldSelectors = new FieldSelectorInfo[]
		{
			new FieldSelectorInfo("Key", typeof(string), (object x) => ((GroupMember)x).Key),
			new FieldSelectorInfo("Status", typeof(string), (object x) => ((GroupMember)x).Status.ToString()),
			new FieldSelectorInfo("Name", typeof(string), (object x) => ((GroupMember)x).AddressInformation.Name),
			new FieldSelectorInfo("Address", typeof(string), (object x) => ((GroupMember)x).AddressInformation.Address)
		};

		// Token: 0x04002E78 RID: 11896
		private static readonly FieldSelectorInfo[] OccurrenceInfoFieldSelectors = new FieldSelectorInfo[]
		{
			new FieldSelectorInfo("Start", typeof(DateTime), (object x) => ((OccurrenceInfo)x).Start),
			new FieldSelectorInfo("End", typeof(DateTime), (object x) => ((OccurrenceInfo)x).End),
			new FieldSelectorInfo("OriginalStart", typeof(DateTime), (object x) => ((OccurrenceInfo)x).OriginalStart)
		};

		// Token: 0x04002E79 RID: 11897
		private static readonly FieldSelectorInfo[] DeletedOccurrenceInfoInfoFieldSelectors = new FieldSelectorInfo[]
		{
			new FieldSelectorInfo("OriginalStart", typeof(DateTime), (object x) => ((DeletedOccurrenceInfo)x).OriginalStart)
		};

		// Token: 0x04002E7A RID: 11898
		private static readonly FieldSelectorInfo[] EmailAddressDictionaryFieldSelectors = new FieldSelectorInfo[]
		{
			new FieldSelectorInfo("EmailAddress1", typeof(string), delegate(object x)
			{
				EmailAddress emailAddress;
				if (!((EmailAddressDictionary)x).TryGetValue(EmailAddressKey.EmailAddress1, out emailAddress))
				{
					return null;
				}
				return emailAddress.Address;
			}),
			new FieldSelectorInfo("EmailAddress2", typeof(string), delegate(object x)
			{
				EmailAddress emailAddress2;
				if (!((EmailAddressDictionary)x).TryGetValue(EmailAddressKey.EmailAddress2, out emailAddress2))
				{
					return null;
				}
				return emailAddress2.Address;
			}),
			new FieldSelectorInfo("EmailAddress3", typeof(string), delegate(object x)
			{
				EmailAddress emailAddress3;
				if (!((EmailAddressDictionary)x).TryGetValue(EmailAddressKey.EmailAddress3, out emailAddress3))
				{
					return null;
				}
				return emailAddress3.Address;
			})
		};

		// Token: 0x04002E7B RID: 11899
		private static readonly FieldSelectorInfo[] ImAddressDictionaryFieldSelectors = new FieldSelectorInfo[]
		{
			new FieldSelectorInfo("ImAddress1", typeof(string), delegate(object x)
			{
				string text;
				if (!((ImAddressDictionary)x).TryGetValue(ImAddressKey.ImAddress1, out text))
				{
					return null;
				}
				return text;
			}),
			new FieldSelectorInfo("ImAddress2", typeof(string), delegate(object x)
			{
				string text2;
				if (!((ImAddressDictionary)x).TryGetValue(ImAddressKey.ImAddress2, out text2))
				{
					return null;
				}
				return text2;
			}),
			new FieldSelectorInfo("ImAddress3", typeof(string), delegate(object x)
			{
				string text3;
				if (!((ImAddressDictionary)x).TryGetValue(ImAddressKey.ImAddress3, out text3))
				{
					return null;
				}
				return text3;
			})
		};

		// Token: 0x04002E7C RID: 11900
		private static readonly FieldSelectorInfo[] PhoneNumberDictionaryFieldSelectors = new FieldSelectorInfo[]
		{
			new FieldSelectorInfo("AssistantPhone", typeof(string), delegate(object x)
			{
				string text4;
				if (!((PhoneNumberDictionary)x).TryGetValue(PhoneNumberKey.AssistantPhone, out text4))
				{
					return null;
				}
				return text4;
			}),
			new FieldSelectorInfo("BusinessFax", typeof(string), delegate(object x)
			{
				string text5;
				if (!((PhoneNumberDictionary)x).TryGetValue(PhoneNumberKey.BusinessFax, out text5))
				{
					return null;
				}
				return text5;
			}),
			new FieldSelectorInfo("BusinessPhone", typeof(string), delegate(object x)
			{
				string text6;
				if (!((PhoneNumberDictionary)x).TryGetValue(PhoneNumberKey.BusinessPhone, out text6))
				{
					return null;
				}
				return text6;
			}),
			new FieldSelectorInfo("BusinessPhone2", typeof(string), delegate(object x)
			{
				string text7;
				if (!((PhoneNumberDictionary)x).TryGetValue(PhoneNumberKey.BusinessPhone2, out text7))
				{
					return null;
				}
				return text7;
			}),
			new FieldSelectorInfo("Callback", typeof(string), delegate(object x)
			{
				string text8;
				if (!((PhoneNumberDictionary)x).TryGetValue(PhoneNumberKey.Callback, out text8))
				{
					return null;
				}
				return text8;
			}),
			new FieldSelectorInfo("CarPhone", typeof(string), delegate(object x)
			{
				string text9;
				if (!((PhoneNumberDictionary)x).TryGetValue(PhoneNumberKey.CarPhone, out text9))
				{
					return null;
				}
				return text9;
			}),
			new FieldSelectorInfo("CompanyMainPhone", typeof(string), delegate(object x)
			{
				string text10;
				if (!((PhoneNumberDictionary)x).TryGetValue(PhoneNumberKey.CompanyMainPhone, out text10))
				{
					return null;
				}
				return text10;
			}),
			new FieldSelectorInfo("HomeFax", typeof(string), delegate(object x)
			{
				string text11;
				if (!((PhoneNumberDictionary)x).TryGetValue(PhoneNumberKey.HomeFax, out text11))
				{
					return null;
				}
				return text11;
			}),
			new FieldSelectorInfo("HomePhone", typeof(string), delegate(object x)
			{
				string text12;
				if (!((PhoneNumberDictionary)x).TryGetValue(PhoneNumberKey.HomePhone, out text12))
				{
					return null;
				}
				return text12;
			}),
			new FieldSelectorInfo("HomePhone2", typeof(string), delegate(object x)
			{
				string text13;
				if (!((PhoneNumberDictionary)x).TryGetValue(PhoneNumberKey.HomePhone2, out text13))
				{
					return null;
				}
				return text13;
			}),
			new FieldSelectorInfo("Isdn", typeof(string), delegate(object x)
			{
				string text14;
				if (!((PhoneNumberDictionary)x).TryGetValue(PhoneNumberKey.Isdn, out text14))
				{
					return null;
				}
				return text14;
			}),
			new FieldSelectorInfo("MobilePhone", typeof(string), delegate(object x)
			{
				string text15;
				if (!((PhoneNumberDictionary)x).TryGetValue(PhoneNumberKey.MobilePhone, out text15))
				{
					return null;
				}
				return text15;
			}),
			new FieldSelectorInfo("OtherFax", typeof(string), delegate(object x)
			{
				string text16;
				if (!((PhoneNumberDictionary)x).TryGetValue(PhoneNumberKey.OtherFax, out text16))
				{
					return null;
				}
				return text16;
			}),
			new FieldSelectorInfo("OtherTelephone", typeof(string), delegate(object x)
			{
				string text17;
				if (!((PhoneNumberDictionary)x).TryGetValue(PhoneNumberKey.OtherTelephone, out text17))
				{
					return null;
				}
				return text17;
			}),
			new FieldSelectorInfo("Pager", typeof(string), delegate(object x)
			{
				string text18;
				if (!((PhoneNumberDictionary)x).TryGetValue(PhoneNumberKey.Pager, out text18))
				{
					return null;
				}
				return text18;
			}),
			new FieldSelectorInfo("PrimaryPhone", typeof(string), delegate(object x)
			{
				string text19;
				if (!((PhoneNumberDictionary)x).TryGetValue(PhoneNumberKey.PrimaryPhone, out text19))
				{
					return null;
				}
				return text19;
			}),
			new FieldSelectorInfo("RadioPhone", typeof(string), delegate(object x)
			{
				string text20;
				if (!((PhoneNumberDictionary)x).TryGetValue(PhoneNumberKey.RadioPhone, out text20))
				{
					return null;
				}
				return text20;
			}),
			new FieldSelectorInfo("Telex", typeof(string), delegate(object x)
			{
				string text21;
				if (!((PhoneNumberDictionary)x).TryGetValue(PhoneNumberKey.Telex, out text21))
				{
					return null;
				}
				return text21;
			}),
			new FieldSelectorInfo("TtyTddPhone", typeof(string), delegate(object x)
			{
				string text22;
				if (!((PhoneNumberDictionary)x).TryGetValue(PhoneNumberKey.TtyTddPhone, out text22))
				{
					return null;
				}
				return text22;
			})
		};

		// Token: 0x04002E7D RID: 11901
		private static readonly Dictionary<PropertyDefinitionBase, IndexedPropertyDefinition[]> AdditionalIndexedProperties = new Dictionary<PropertyDefinitionBase, IndexedPropertyDefinition[]>
		{
			{
				ContactSchema.EmailAddresses,
				new IndexedPropertyDefinition[]
				{
					ContactSchema.EmailAddress1,
					ContactSchema.EmailAddress2,
					ContactSchema.EmailAddress3
				}
			},
			{
				ContactSchema.ImAddresses,
				new IndexedPropertyDefinition[]
				{
					ContactSchema.ImAddress1,
					ContactSchema.ImAddress2,
					ContactSchema.ImAddress3
				}
			},
			{
				ContactSchema.PhoneNumbers,
				new IndexedPropertyDefinition[]
				{
					ContactSchema.AssistantPhone,
					ContactSchema.BusinessFax,
					ContactSchema.BusinessPhone,
					ContactSchema.BusinessPhone2,
					ContactSchema.Callback,
					ContactSchema.CarPhone,
					ContactSchema.CompanyMainPhone,
					ContactSchema.HomeFax,
					ContactSchema.HomePhone,
					ContactSchema.HomePhone2,
					ContactSchema.Isdn,
					ContactSchema.MobilePhone,
					ContactSchema.OtherFax,
					ContactSchema.OtherTelephone,
					ContactSchema.Pager,
					ContactSchema.PrimaryPhone,
					ContactSchema.RadioPhone,
					ContactSchema.Telex,
					ContactSchema.TtyTddPhone
				}
			}
		};

		// Token: 0x04002E7E RID: 11902
		private static readonly Dictionary<Type, FieldSelectorInfo[]> ComplexTypeToFieldSelectors = new Dictionary<Type, FieldSelectorInfo[]>
		{
			{
				typeof(EmailAddress),
				SupportedExchangeTypes.EmailAddressFieldSelectors
			},
			{
				typeof(CompleteName),
				SupportedExchangeTypes.CompleteNameFieldSelectors
			},
			{
				typeof(Recurrence),
				SupportedExchangeTypes.RecurrenceFieldSelectors
			},
			{
				typeof(GroupMember),
				SupportedExchangeTypes.GroupMemberFieldSelectors
			},
			{
				typeof(OccurrenceInfo),
				SupportedExchangeTypes.OccurrenceInfoFieldSelectors
			},
			{
				typeof(DeletedOccurrenceInfo),
				SupportedExchangeTypes.DeletedOccurrenceInfoInfoFieldSelectors
			},
			{
				typeof(EmailAddressDictionary),
				SupportedExchangeTypes.EmailAddressDictionaryFieldSelectors
			},
			{
				typeof(ImAddressDictionary),
				SupportedExchangeTypes.ImAddressDictionaryFieldSelectors
			},
			{
				typeof(PhoneNumberDictionary),
				SupportedExchangeTypes.PhoneNumberDictionaryFieldSelectors
			},
			{
				typeof(Attendee),
				SupportedExchangeTypes.AttendeeSelectors
			}
		};

		// Token: 0x04002E7F RID: 11903
		private static readonly Dictionary<Type, MarshalInfo> PrimitiveTypeToTypeValue = new Dictionary<Type, MarshalInfo>
		{
			{
				typeof(string),
				new MarshalInfo(TypeValue.Text.Nullable, (object o) => TextValue.NewOrNull((string)o))
			},
			{
				typeof(int),
				new MarshalInfo(TypeValue.Number, (object o) => NumberValue.New((int)o))
			},
			{
				typeof(int?),
				new MarshalInfo(TypeValue.Number.Nullable, (object o) => NumberValue.New((int)o))
			},
			{
				typeof(DateTime),
				new MarshalInfo(TypeValue.DateTime, (object o) => DateTimeValue.New((DateTime)o))
			},
			{
				typeof(DateTime?),
				new MarshalInfo(TypeValue.DateTime.Nullable, delegate(object o)
				{
					if (o == null)
					{
						return Value.Null;
					}
					return DateTimeValue.New((DateTime)o);
				})
			},
			{
				typeof(TimeSpan),
				new MarshalInfo(TypeValue.Duration, (object o) => DurationValue.New((TimeSpan)o))
			},
			{
				typeof(bool),
				new MarshalInfo(TypeValue.Logical, (object o) => LogicalValue.New((bool)o))
			},
			{
				typeof(bool?),
				new MarshalInfo(TypeValue.Logical.Nullable, (object o) => LogicalValue.New((bool)o))
			},
			{
				typeof(Enum),
				new MarshalInfo(TypeValue.Text, (object o) => TextValue.New(o.ToString()))
			},
			{
				typeof(byte[]),
				new MarshalInfo(TypeValue.Binary, (object o) => BinaryValue.New((byte[])o))
			},
			{
				typeof(double),
				new MarshalInfo(TypeValue.Number, (object o) => NumberValue.New((double)o))
			}
		};
	}
}
