using System;
using System.Collections.Generic;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.WebServices
{
	// Token: 0x0200030C RID: 780
	internal static class Strings
	{
		// Token: 0x06001BB2 RID: 7090 RVA: 0x00049D88 File Offset: 0x00048D88
		static Strings()
		{
			Strings.stringIDs.Add(1631423810U, "CannotRemoveSubscriptionFromLiveConnection");
			Strings.stringIDs.Add(3355844483U, "ReadAccessInvalidForNonCalendarFolder");
			Strings.stringIDs.Add(1413133863U, "PropertyDefinitionPropertyMustBeSet");
			Strings.stringIDs.Add(2808916828U, "ArgumentIsBlankString");
			Strings.stringIDs.Add(2110564001U, "InvalidAutodiscoverDomainsCount");
			Strings.stringIDs.Add(3846446647U, "MinutesMustBeBetween0And1439");
			Strings.stringIDs.Add(3725066606U, "DeleteInvalidForUnsavedUserConfiguration");
			Strings.stringIDs.Add(732877372U, "PeriodNotFound");
			Strings.stringIDs.Add(742945049U, "InvalidAutodiscoverSmtpAddress");
			Strings.stringIDs.Add(2371807741U, "InvalidOAuthToken");
			Strings.stringIDs.Add(540781291U, "MaxScpHopsExceeded");
			Strings.stringIDs.Add(412932664U, "ContactGroupMemberCannotBeUpdatedWithoutBeingLoadedFirst");
			Strings.stringIDs.Add(3932722495U, "CurrentPositionNotElementStart");
			Strings.stringIDs.Add(1762296216U, "CannotConvertBetweenTimeZones");
			Strings.stringIDs.Add(2947629837U, "FrequencyMustBeBetween1And1440");
			Strings.stringIDs.Add(4255072555U, "CannotSetDelegateFolderPermissionLevelToCustom");
			Strings.stringIDs.Add(231532733U, "PartnerTokenIncompatibleWithRequestVersion");
			Strings.stringIDs.Add(216906786U, "InvalidAutodiscoverRequest");
			Strings.stringIDs.Add(2795977038U, "InvalidAsyncResult");
			Strings.stringIDs.Add(1875536889U, "InvalidMailboxType");
			Strings.stringIDs.Add(368467777U, "AttachmentCollectionNotLoaded");
			Strings.stringIDs.Add(1937216341U, "ParameterIncompatibleWithRequestVersion");
			Strings.stringIDs.Add(814325722U, "DayOfWeekIndexMustBeSpecifiedForRecurrencePattern");
			Strings.stringIDs.Add(365885070U, "WLIDCredentialsCannotBeUsedWithLegacyAutodiscover");
			Strings.stringIDs.Add(2534953608U, "PropertyCannotBeUpdated");
			Strings.stringIDs.Add(3820761979U, "IncompatibleTypeForArray");
			Strings.stringIDs.Add(3936886128U, "PercentCompleteMustBeBetween0And100");
			Strings.stringIDs.Add(460414997U, "AutodiscoverServiceIncompatibleWithRequestVersion");
			Strings.stringIDs.Add(1774410042U, "InvalidAutodiscoverSmtpAddressesCount");
			Strings.stringIDs.Add(1922401890U, "ServiceUrlMustBeSet");
			Strings.stringIDs.Add(4181025268U, "ItemTypeNotCompatible");
			Strings.stringIDs.Add(1516841384U, "AttachmentItemTypeMismatch");
			Strings.stringIDs.Add(3620572079U, "UnsupportedWebProtocol");
			Strings.stringIDs.Add(777131942U, "EnumValueIncompatibleWithRequestVersion");
			Strings.stringIDs.Add(2080190431U, "UnexpectedElement");
			Strings.stringIDs.Add(162617974U, "InvalidOrderBy");
			Strings.stringIDs.Add(3730786468U, "NoAppropriateConstructorForItemClass");
			Strings.stringIDs.Add(1334400254U, "SearchFilterAtIndexIsInvalid");
			Strings.stringIDs.Add(3957228737U, "DeletingThisObjectTypeNotAuthorized");
			Strings.stringIDs.Add(2011990502U, "PropertyCannotBeDeleted");
			Strings.stringIDs.Add(49538054U, "ValuePropertyMustBeSet");
			Strings.stringIDs.Add(4177209255U, "TagValueIsOutOfRange");
			Strings.stringIDs.Add(893972063U, "ItemToUpdateCannotBeNullOrNew");
			Strings.stringIDs.Add(2492696699U, "SearchParametersRootFolderIdsEmpty");
			Strings.stringIDs.Add(2182404464U, "MailboxQueriesParameterIsNotSpecified");
			Strings.stringIDs.Add(3079787672U, "FolderPermissionHasInvalidUserId");
			Strings.stringIDs.Add(95137117U, "InvalidAutodiscoverDomain");
			Strings.stringIDs.Add(1274908260U, "MailboxesParameterIsNotSpecified");
			Strings.stringIDs.Add(3207115397U, "ParentFolderDoesNotHaveId");
			Strings.stringIDs.Add(190140884U, "DayOfMonthMustBeSpecifiedForRecurrencePattern");
			Strings.stringIDs.Add(886236812U, "ClassIncompatibleWithRequestVersion");
			Strings.stringIDs.Add(956539910U, "CertificateHasNoPrivateKey");
			Strings.stringIDs.Add(2660350763U, "InvalidOrUnsupportedTimeZoneDefinition");
			Strings.stringIDs.Add(980862610U, "HourMustBeBetween0And23");
			Strings.stringIDs.Add(1864811546U, "TimeoutMustBeBetween1And1440");
			Strings.stringIDs.Add(107509017U, "CredentialsRequired");
			Strings.stringIDs.Add(1301287431U, "MustLoadOrAssignPropertyBeforeAccess");
			Strings.stringIDs.Add(2421645987U, "InvalidAutodiscoverServiceResponse");
			Strings.stringIDs.Add(3637880390U, "CannotCallConnectDuringLiveConnection");
			Strings.stringIDs.Add(1990317298U, "ObjectDoesNotHaveId");
			Strings.stringIDs.Add(2374077290U, "CannotAddSubscriptionToLiveConnection");
			Strings.stringIDs.Add(3989266019U, "MaxChangesMustBeBetween1And512");
			Strings.stringIDs.Add(3745271395U, "AttributeValueCannotBeSerialized");
			Strings.stringIDs.Add(2182989540U, "SearchFilterMustBeSet");
			Strings.stringIDs.Add(2696927076U, "EndDateMustBeGreaterThanStartDate");
			Strings.stringIDs.Add(3410810540U, "InvalidDateTime");
			Strings.stringIDs.Add(2505974152U, "UpdateItemsDoesNotAllowAttachments");
			Strings.stringIDs.Add(3210574608U, "TimeoutMustBeGreaterThanZero");
			Strings.stringIDs.Add(81925120U, "AutodiscoverInvalidSettingForOutlookProvider");
			Strings.stringIDs.Add(706868687U, "InvalidRedirectionResponseReturned");
			Strings.stringIDs.Add(3728006586U, "ExpectedStartElement");
			Strings.stringIDs.Add(2925351706U, "DaysOfTheWeekNotSpecified");
			Strings.stringIDs.Add(2887145506U, "FolderToUpdateCannotBeNullOrNew");
			Strings.stringIDs.Add(1709653493U, "PartnerTokenRequestRequiresUrl");
			Strings.stringIDs.Add(4020293177U, "NumberOfOccurrencesMustBeGreaterThanZero");
			Strings.stringIDs.Add(537341821U, "JsonSerializationNotImplemented");
			Strings.stringIDs.Add(263088904U, "StartTimeZoneRequired");
			Strings.stringIDs.Add(2291792901U, "PropertyAlreadyExistsInOrderByCollection");
			Strings.stringIDs.Add(2209938519U, "ItemAttachmentMustBeNamed");
			Strings.stringIDs.Add(2688936715U, "InvalidAutodiscoverSettingsCount");
			Strings.stringIDs.Add(1313605428U, "LoadingThisObjectTypeNotSupported");
			Strings.stringIDs.Add(3460610998U, "UserIdForDelegateUserNotSpecified");
			Strings.stringIDs.Add(2720737469U, "PhoneCallAlreadyDisconnected");
			Strings.stringIDs.Add(1464025756U, "OperationDoesNotSupportAttachments");
			Strings.stringIDs.Add(98512741U, "UnsupportedTimeZonePeriodTransitionTarget");
			Strings.stringIDs.Add(474853648U, "IEnumerableDoesNotContainThatManyObject");
			Strings.stringIDs.Add(953072612U, "UpdateItemsDoesNotSupportNewOrUnchangedItems");
			Strings.stringIDs.Add(1341796948U, "ValidationFailed");
			Strings.stringIDs.Add(3636462697U, "InvalidRecurrencePattern");
			Strings.stringIDs.Add(4170253059U, "TimeWindowStartTimeMustBeGreaterThanEndTime");
			Strings.stringIDs.Add(369811396U, "InvalidAttributeValue");
			Strings.stringIDs.Add(2449142619U, "FileAttachmentContentIsNotSet");
			Strings.stringIDs.Add(628121484U, "AutodiscoverDidNotReturnEwsUrl");
			Strings.stringIDs.Add(423461609U, "RecurrencePatternMustHaveStartDate");
			Strings.stringIDs.Add(1396112272U, "OccurrenceIndexMustBeGreaterThanZero");
			Strings.stringIDs.Add(1981959699U, "ServiceResponseDoesNotContainXml");
			Strings.stringIDs.Add(2560682386U, "ItemIsOutOfDate");
			Strings.stringIDs.Add(3637133283U, "MinuteMustBeBetween0And59");
			Strings.stringIDs.Add(3954825173U, "NoSoapOrWsSecurityEndpointAvailable");
			Strings.stringIDs.Add(3189440097U, "ElementNotFound");
			Strings.stringIDs.Add(4223767916U, "IndexIsOutOfRange");
			Strings.stringIDs.Add(869119007U, "PropertyIsReadOnly");
			Strings.stringIDs.Add(2817837707U, "AttachmentCreationFailed");
			Strings.stringIDs.Add(3133762315U, "DayOfMonthMustBeBetween1And31");
			Strings.stringIDs.Add(2091738407U, "ServiceRequestFailed");
			Strings.stringIDs.Add(129422921U, "DelegateUserHasInvalidUserId");
			Strings.stringIDs.Add(1069581653U, "SearchFilterComparisonValueTypeIsNotSupported");
			Strings.stringIDs.Add(4258284629U, "ElementValueCannotBeSerialized");
			Strings.stringIDs.Add(2924950297U, "PropertyValueMustBeSpecifiedForRecurrencePattern");
			Strings.stringIDs.Add(2436895661U, "NonSummaryPropertyCannotBeUsed");
			Strings.stringIDs.Add(134310332U, "HoldIdParameterIsNotSpecified");
			Strings.stringIDs.Add(21384399U, "TransitionGroupNotFound");
			Strings.stringIDs.Add(4144876524U, "ObjectTypeNotSupported");
			Strings.stringIDs.Add(3454211069U, "InvalidTimeoutValue");
			Strings.stringIDs.Add(3339063014U, "AutodiscoverRedirectBlocked");
			Strings.stringIDs.Add(3717641032U, "PropertySetCannotBeModified");
			Strings.stringIDs.Add(3877446129U, "DayOfTheWeekMustBeSpecifiedForRecurrencePattern");
			Strings.stringIDs.Add(3918321785U, "ServiceObjectAlreadyHasId");
			Strings.stringIDs.Add(4274338115U, "MethodIncompatibleWithRequestVersion");
			Strings.stringIDs.Add(2727810523U, "OperationNotSupportedForPropertyDefinitionType");
			Strings.stringIDs.Add(3934659801U, "InvalidElementStringValue");
			Strings.stringIDs.Add(500353177U, "CollectionIsEmpty");
			Strings.stringIDs.Add(885177846U, "InvalidFrequencyValue");
			Strings.stringIDs.Add(546825189U, "UnexpectedEndOfXmlDocument");
			Strings.stringIDs.Add(3578997681U, "FolderTypeNotCompatible");
			Strings.stringIDs.Add(3451337077U, "RequestIncompatibleWithRequestVersion");
			Strings.stringIDs.Add(4148083232U, "PropertyTypeIncompatibleWhenUpdatingCollection");
			Strings.stringIDs.Add(603284986U, "ServerVersionNotSupported");
			Strings.stringIDs.Add(666454105U, "DurationMustBeSpecifiedWhenScheduled");
			Strings.stringIDs.Add(1005127777U, "NoError");
			Strings.stringIDs.Add(3972010693U, "CannotUpdateNewUserConfiguration");
			Strings.stringIDs.Add(2438108153U, "ObjectTypeIncompatibleWithRequestVersion");
			Strings.stringIDs.Add(3604837092U, "NullStringArrayElementInvalid");
			Strings.stringIDs.Add(333950754U, "HttpsIsRequired");
			Strings.stringIDs.Add(2480357954U, "MergedFreeBusyIntervalMustBeSmallerThanTimeWindow");
			Strings.stringIDs.Add(328519365U, "SecondMustBeBetween0And59");
			Strings.stringIDs.Add(3596957401U, "AtLeastOneAttachmentCouldNotBeDeleted");
			Strings.stringIDs.Add(1233804470U, "IdAlreadyInList");
			Strings.stringIDs.Add(3295662635U, "BothSearchFilterAndQueryStringCannotBeSpecified");
			Strings.stringIDs.Add(3383788511U, "AdditionalPropertyIsNull");
			Strings.stringIDs.Add(2316486059U, "InvalidEmailAddress");
			Strings.stringIDs.Add(1588497945U, "MaximumRedirectionHopsExceeded");
			Strings.stringIDs.Add(3097538091U, "AutodiscoverCouldNotBeLocated");
			Strings.stringIDs.Add(2715578908U, "NoSubscriptionsOnConnection");
			Strings.stringIDs.Add(1063351272U, "PermissionLevelInvalidForNonCalendarFolder");
			Strings.stringIDs.Add(2845550636U, "InvalidAuthScheme");
			Strings.stringIDs.Add(311071154U, "JsonDeserializationNotImplemented");
			Strings.stringIDs.Add(2743202310U, "ValuePropertyNotLoaded");
			Strings.stringIDs.Add(843308875U, "PropertyIncompatibleWithRequestVersion");
			Strings.stringIDs.Add(2437116628U, "OffsetMustBeGreaterThanZero");
			Strings.stringIDs.Add(690508625U, "CreateItemsDoesNotAllowAttachments");
			Strings.stringIDs.Add(2445370550U, "PropertyDefinitionTypeMismatch");
			Strings.stringIDs.Add(2129318611U, "IntervalMustBeGreaterOrEqualToOne");
			Strings.stringIDs.Add(961741172U, "CannotSetPermissionLevelToCustom");
			Strings.stringIDs.Add(2921139860U, "CannotAddRequestHeader");
			Strings.stringIDs.Add(4264880578U, "ArrayMustHaveAtLeastOneElement");
			Strings.stringIDs.Add(567828041U, "MonthMustBeSpecifiedForRecurrencePattern");
			Strings.stringIDs.Add(3869807514U, "ValueOfTypeCannotBeConverted");
			Strings.stringIDs.Add(788051255U, "ValueCannotBeConverted");
			Strings.stringIDs.Add(1481761255U, "ServerErrorAndStackTraceDetails");
			Strings.stringIDs.Add(2586079185U, "FolderPermissionLevelMustBeSet");
			Strings.stringIDs.Add(4003396996U, "AutodiscoverError");
			Strings.stringIDs.Add(3066801652U, "ArrayMustHaveSingleDimension");
			Strings.stringIDs.Add(2233059550U, "InvalidPropertyValueNotInRange");
			Strings.stringIDs.Add(1453973661U, "RegenerationPatternsOnlyValidForTasks");
			Strings.stringIDs.Add(914733855U, "ItemAttachmentCannotBeUpdated");
			Strings.stringIDs.Add(1467147488U, "EqualityComparisonFilterIsInvalid");
			Strings.stringIDs.Add(2950491364U, "AutodiscoverServiceRequestRequiresDomainOrUrl");
			Strings.stringIDs.Add(3929050450U, "InvalidUser");
			Strings.stringIDs.Add(2901788841U, "AccountIsLocked");
			Strings.stringIDs.Add(2762661174U, "InvalidDomainName");
			Strings.stringIDs.Add(710118117U, "TooFewServiceReponsesReturned");
			Strings.stringIDs.Add(463464377U, "CannotSubscribeToStatusEvents");
			Strings.stringIDs.Add(1066736932U, "InvalidSortByPropertyForMailboxSearch");
			Strings.stringIDs.Add(2100812591U, "UnexpectedElementType");
			Strings.stringIDs.Add(691200302U, "ValueMustBeGreaterThanZero");
			Strings.stringIDs.Add(2292458752U, "AttachmentCannotBeUpdated");
			Strings.stringIDs.Add(3912371609U, "CreateItemsDoesNotHandleExistingItems");
			Strings.stringIDs.Add(2967125165U, "MultipleContactPhotosInAttachment");
			Strings.stringIDs.Add(3387532664U, "InvalidRecurrenceRange");
			Strings.stringIDs.Add(4210899574U, "CannotSetBothImpersonatedAndPrivilegedUser");
			Strings.stringIDs.Add(1565629292U, "NewMessagesWithAttachmentsCannotBeSentDirectly");
			Strings.stringIDs.Add(3633425214U, "CannotCallDisconnectWithNoLiveConnection");
			Strings.stringIDs.Add(3940556486U, "IdPropertyMustBeSet");
			Strings.stringIDs.Add(1562822901U, "ValuePropertyNotAssigned");
			Strings.stringIDs.Add(4061174908U, "ZeroLengthArrayInvalid");
			Strings.stringIDs.Add(3345946933U, "HoldMailboxesParameterIsNotSpecified");
			Strings.stringIDs.Add(2270311116U, "CannotSaveNotNewUserConfiguration");
			Strings.stringIDs.Add(2111195463U, "ServiceObjectDoesNotHaveId");
			Strings.stringIDs.Add(491519754U, "PropertyCollectionSizeMismatch");
			Strings.stringIDs.Add(2861470707U, "XsDurationCouldNotBeParsed");
			Strings.stringIDs.Add(2279096081U, "UnknownTimeZonePeriodTransitionType");
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06001BB3 RID: 7091 RVA: 0x0004ACC2 File Offset: 0x00049CC2
		public static LocalizedString CannotRemoveSubscriptionFromLiveConnection
		{
			get
			{
				return new LocalizedString("CannotRemoveSubscriptionFromLiveConnection", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06001BB4 RID: 7092 RVA: 0x0004ACD9 File Offset: 0x00049CD9
		public static LocalizedString ReadAccessInvalidForNonCalendarFolder
		{
			get
			{
				return new LocalizedString("ReadAccessInvalidForNonCalendarFolder", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001BB5 RID: 7093 RVA: 0x0004ACF0 File Offset: 0x00049CF0
		public static LocalizedString PropertyDefinitionPropertyMustBeSet
		{
			get
			{
				return new LocalizedString("PropertyDefinitionPropertyMustBeSet", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06001BB6 RID: 7094 RVA: 0x0004AD07 File Offset: 0x00049D07
		public static LocalizedString ArgumentIsBlankString
		{
			get
			{
				return new LocalizedString("ArgumentIsBlankString", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06001BB7 RID: 7095 RVA: 0x0004AD1E File Offset: 0x00049D1E
		public static LocalizedString InvalidAutodiscoverDomainsCount
		{
			get
			{
				return new LocalizedString("InvalidAutodiscoverDomainsCount", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06001BB8 RID: 7096 RVA: 0x0004AD35 File Offset: 0x00049D35
		public static LocalizedString MinutesMustBeBetween0And1439
		{
			get
			{
				return new LocalizedString("MinutesMustBeBetween0And1439", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06001BB9 RID: 7097 RVA: 0x0004AD4C File Offset: 0x00049D4C
		public static LocalizedString DeleteInvalidForUnsavedUserConfiguration
		{
			get
			{
				return new LocalizedString("DeleteInvalidForUnsavedUserConfiguration", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06001BBA RID: 7098 RVA: 0x0004AD63 File Offset: 0x00049D63
		public static LocalizedString PeriodNotFound
		{
			get
			{
				return new LocalizedString("PeriodNotFound", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06001BBB RID: 7099 RVA: 0x0004AD7A File Offset: 0x00049D7A
		public static LocalizedString InvalidAutodiscoverSmtpAddress
		{
			get
			{
				return new LocalizedString("InvalidAutodiscoverSmtpAddress", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06001BBC RID: 7100 RVA: 0x0004AD91 File Offset: 0x00049D91
		public static LocalizedString InvalidOAuthToken
		{
			get
			{
				return new LocalizedString("InvalidOAuthToken", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06001BBD RID: 7101 RVA: 0x0004ADA8 File Offset: 0x00049DA8
		public static LocalizedString MaxScpHopsExceeded
		{
			get
			{
				return new LocalizedString("MaxScpHopsExceeded", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06001BBE RID: 7102 RVA: 0x0004ADBF File Offset: 0x00049DBF
		public static LocalizedString ContactGroupMemberCannotBeUpdatedWithoutBeingLoadedFirst
		{
			get
			{
				return new LocalizedString("ContactGroupMemberCannotBeUpdatedWithoutBeingLoadedFirst", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06001BBF RID: 7103 RVA: 0x0004ADD6 File Offset: 0x00049DD6
		public static LocalizedString CurrentPositionNotElementStart
		{
			get
			{
				return new LocalizedString("CurrentPositionNotElementStart", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06001BC0 RID: 7104 RVA: 0x0004ADED File Offset: 0x00049DED
		public static LocalizedString CannotConvertBetweenTimeZones
		{
			get
			{
				return new LocalizedString("CannotConvertBetweenTimeZones", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06001BC1 RID: 7105 RVA: 0x0004AE04 File Offset: 0x00049E04
		public static LocalizedString FrequencyMustBeBetween1And1440
		{
			get
			{
				return new LocalizedString("FrequencyMustBeBetween1And1440", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06001BC2 RID: 7106 RVA: 0x0004AE1B File Offset: 0x00049E1B
		public static LocalizedString CannotSetDelegateFolderPermissionLevelToCustom
		{
			get
			{
				return new LocalizedString("CannotSetDelegateFolderPermissionLevelToCustom", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06001BC3 RID: 7107 RVA: 0x0004AE32 File Offset: 0x00049E32
		public static LocalizedString PartnerTokenIncompatibleWithRequestVersion
		{
			get
			{
				return new LocalizedString("PartnerTokenIncompatibleWithRequestVersion", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06001BC4 RID: 7108 RVA: 0x0004AE49 File Offset: 0x00049E49
		public static LocalizedString InvalidAutodiscoverRequest
		{
			get
			{
				return new LocalizedString("InvalidAutodiscoverRequest", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06001BC5 RID: 7109 RVA: 0x0004AE60 File Offset: 0x00049E60
		public static LocalizedString InvalidAsyncResult
		{
			get
			{
				return new LocalizedString("InvalidAsyncResult", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06001BC6 RID: 7110 RVA: 0x0004AE77 File Offset: 0x00049E77
		public static LocalizedString InvalidMailboxType
		{
			get
			{
				return new LocalizedString("InvalidMailboxType", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06001BC7 RID: 7111 RVA: 0x0004AE8E File Offset: 0x00049E8E
		public static LocalizedString AttachmentCollectionNotLoaded
		{
			get
			{
				return new LocalizedString("AttachmentCollectionNotLoaded", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06001BC8 RID: 7112 RVA: 0x0004AEA5 File Offset: 0x00049EA5
		public static LocalizedString ParameterIncompatibleWithRequestVersion
		{
			get
			{
				return new LocalizedString("ParameterIncompatibleWithRequestVersion", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06001BC9 RID: 7113 RVA: 0x0004AEBC File Offset: 0x00049EBC
		public static LocalizedString DayOfWeekIndexMustBeSpecifiedForRecurrencePattern
		{
			get
			{
				return new LocalizedString("DayOfWeekIndexMustBeSpecifiedForRecurrencePattern", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06001BCA RID: 7114 RVA: 0x0004AED3 File Offset: 0x00049ED3
		public static LocalizedString WLIDCredentialsCannotBeUsedWithLegacyAutodiscover
		{
			get
			{
				return new LocalizedString("WLIDCredentialsCannotBeUsedWithLegacyAutodiscover", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06001BCB RID: 7115 RVA: 0x0004AEEA File Offset: 0x00049EEA
		public static LocalizedString PropertyCannotBeUpdated
		{
			get
			{
				return new LocalizedString("PropertyCannotBeUpdated", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06001BCC RID: 7116 RVA: 0x0004AF01 File Offset: 0x00049F01
		public static LocalizedString IncompatibleTypeForArray
		{
			get
			{
				return new LocalizedString("IncompatibleTypeForArray", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06001BCD RID: 7117 RVA: 0x0004AF18 File Offset: 0x00049F18
		public static LocalizedString PercentCompleteMustBeBetween0And100
		{
			get
			{
				return new LocalizedString("PercentCompleteMustBeBetween0And100", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06001BCE RID: 7118 RVA: 0x0004AF2F File Offset: 0x00049F2F
		public static LocalizedString AutodiscoverServiceIncompatibleWithRequestVersion
		{
			get
			{
				return new LocalizedString("AutodiscoverServiceIncompatibleWithRequestVersion", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06001BCF RID: 7119 RVA: 0x0004AF46 File Offset: 0x00049F46
		public static LocalizedString InvalidAutodiscoverSmtpAddressesCount
		{
			get
			{
				return new LocalizedString("InvalidAutodiscoverSmtpAddressesCount", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06001BD0 RID: 7120 RVA: 0x0004AF5D File Offset: 0x00049F5D
		public static LocalizedString ServiceUrlMustBeSet
		{
			get
			{
				return new LocalizedString("ServiceUrlMustBeSet", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06001BD1 RID: 7121 RVA: 0x0004AF74 File Offset: 0x00049F74
		public static LocalizedString ItemTypeNotCompatible
		{
			get
			{
				return new LocalizedString("ItemTypeNotCompatible", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06001BD2 RID: 7122 RVA: 0x0004AF8B File Offset: 0x00049F8B
		public static LocalizedString AttachmentItemTypeMismatch
		{
			get
			{
				return new LocalizedString("AttachmentItemTypeMismatch", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06001BD3 RID: 7123 RVA: 0x0004AFA2 File Offset: 0x00049FA2
		public static LocalizedString UnsupportedWebProtocol
		{
			get
			{
				return new LocalizedString("UnsupportedWebProtocol", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06001BD4 RID: 7124 RVA: 0x0004AFB9 File Offset: 0x00049FB9
		public static LocalizedString EnumValueIncompatibleWithRequestVersion
		{
			get
			{
				return new LocalizedString("EnumValueIncompatibleWithRequestVersion", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06001BD5 RID: 7125 RVA: 0x0004AFD0 File Offset: 0x00049FD0
		public static LocalizedString UnexpectedElement
		{
			get
			{
				return new LocalizedString("UnexpectedElement", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06001BD6 RID: 7126 RVA: 0x0004AFE7 File Offset: 0x00049FE7
		public static LocalizedString InvalidOrderBy
		{
			get
			{
				return new LocalizedString("InvalidOrderBy", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06001BD7 RID: 7127 RVA: 0x0004AFFE File Offset: 0x00049FFE
		public static LocalizedString NoAppropriateConstructorForItemClass
		{
			get
			{
				return new LocalizedString("NoAppropriateConstructorForItemClass", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06001BD8 RID: 7128 RVA: 0x0004B015 File Offset: 0x0004A015
		public static LocalizedString SearchFilterAtIndexIsInvalid
		{
			get
			{
				return new LocalizedString("SearchFilterAtIndexIsInvalid", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06001BD9 RID: 7129 RVA: 0x0004B02C File Offset: 0x0004A02C
		public static LocalizedString DeletingThisObjectTypeNotAuthorized
		{
			get
			{
				return new LocalizedString("DeletingThisObjectTypeNotAuthorized", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06001BDA RID: 7130 RVA: 0x0004B043 File Offset: 0x0004A043
		public static LocalizedString PropertyCannotBeDeleted
		{
			get
			{
				return new LocalizedString("PropertyCannotBeDeleted", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06001BDB RID: 7131 RVA: 0x0004B05A File Offset: 0x0004A05A
		public static LocalizedString ValuePropertyMustBeSet
		{
			get
			{
				return new LocalizedString("ValuePropertyMustBeSet", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06001BDC RID: 7132 RVA: 0x0004B071 File Offset: 0x0004A071
		public static LocalizedString TagValueIsOutOfRange
		{
			get
			{
				return new LocalizedString("TagValueIsOutOfRange", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06001BDD RID: 7133 RVA: 0x0004B088 File Offset: 0x0004A088
		public static LocalizedString ItemToUpdateCannotBeNullOrNew
		{
			get
			{
				return new LocalizedString("ItemToUpdateCannotBeNullOrNew", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06001BDE RID: 7134 RVA: 0x0004B09F File Offset: 0x0004A09F
		public static LocalizedString SearchParametersRootFolderIdsEmpty
		{
			get
			{
				return new LocalizedString("SearchParametersRootFolderIdsEmpty", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06001BDF RID: 7135 RVA: 0x0004B0B6 File Offset: 0x0004A0B6
		public static LocalizedString MailboxQueriesParameterIsNotSpecified
		{
			get
			{
				return new LocalizedString("MailboxQueriesParameterIsNotSpecified", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06001BE0 RID: 7136 RVA: 0x0004B0CD File Offset: 0x0004A0CD
		public static LocalizedString FolderPermissionHasInvalidUserId
		{
			get
			{
				return new LocalizedString("FolderPermissionHasInvalidUserId", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06001BE1 RID: 7137 RVA: 0x0004B0E4 File Offset: 0x0004A0E4
		public static LocalizedString InvalidAutodiscoverDomain
		{
			get
			{
				return new LocalizedString("InvalidAutodiscoverDomain", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06001BE2 RID: 7138 RVA: 0x0004B0FB File Offset: 0x0004A0FB
		public static LocalizedString MailboxesParameterIsNotSpecified
		{
			get
			{
				return new LocalizedString("MailboxesParameterIsNotSpecified", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06001BE3 RID: 7139 RVA: 0x0004B112 File Offset: 0x0004A112
		public static LocalizedString ParentFolderDoesNotHaveId
		{
			get
			{
				return new LocalizedString("ParentFolderDoesNotHaveId", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06001BE4 RID: 7140 RVA: 0x0004B129 File Offset: 0x0004A129
		public static LocalizedString DayOfMonthMustBeSpecifiedForRecurrencePattern
		{
			get
			{
				return new LocalizedString("DayOfMonthMustBeSpecifiedForRecurrencePattern", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06001BE5 RID: 7141 RVA: 0x0004B140 File Offset: 0x0004A140
		public static LocalizedString ClassIncompatibleWithRequestVersion
		{
			get
			{
				return new LocalizedString("ClassIncompatibleWithRequestVersion", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06001BE6 RID: 7142 RVA: 0x0004B157 File Offset: 0x0004A157
		public static LocalizedString CertificateHasNoPrivateKey
		{
			get
			{
				return new LocalizedString("CertificateHasNoPrivateKey", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06001BE7 RID: 7143 RVA: 0x0004B16E File Offset: 0x0004A16E
		public static LocalizedString InvalidOrUnsupportedTimeZoneDefinition
		{
			get
			{
				return new LocalizedString("InvalidOrUnsupportedTimeZoneDefinition", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06001BE8 RID: 7144 RVA: 0x0004B185 File Offset: 0x0004A185
		public static LocalizedString HourMustBeBetween0And23
		{
			get
			{
				return new LocalizedString("HourMustBeBetween0And23", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06001BE9 RID: 7145 RVA: 0x0004B19C File Offset: 0x0004A19C
		public static LocalizedString TimeoutMustBeBetween1And1440
		{
			get
			{
				return new LocalizedString("TimeoutMustBeBetween1And1440", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06001BEA RID: 7146 RVA: 0x0004B1B3 File Offset: 0x0004A1B3
		public static LocalizedString CredentialsRequired
		{
			get
			{
				return new LocalizedString("CredentialsRequired", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06001BEB RID: 7147 RVA: 0x0004B1CA File Offset: 0x0004A1CA
		public static LocalizedString MustLoadOrAssignPropertyBeforeAccess
		{
			get
			{
				return new LocalizedString("MustLoadOrAssignPropertyBeforeAccess", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06001BEC RID: 7148 RVA: 0x0004B1E1 File Offset: 0x0004A1E1
		public static LocalizedString InvalidAutodiscoverServiceResponse
		{
			get
			{
				return new LocalizedString("InvalidAutodiscoverServiceResponse", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06001BED RID: 7149 RVA: 0x0004B1F8 File Offset: 0x0004A1F8
		public static LocalizedString CannotCallConnectDuringLiveConnection
		{
			get
			{
				return new LocalizedString("CannotCallConnectDuringLiveConnection", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06001BEE RID: 7150 RVA: 0x0004B20F File Offset: 0x0004A20F
		public static LocalizedString ObjectDoesNotHaveId
		{
			get
			{
				return new LocalizedString("ObjectDoesNotHaveId", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06001BEF RID: 7151 RVA: 0x0004B226 File Offset: 0x0004A226
		public static LocalizedString CannotAddSubscriptionToLiveConnection
		{
			get
			{
				return new LocalizedString("CannotAddSubscriptionToLiveConnection", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06001BF0 RID: 7152 RVA: 0x0004B23D File Offset: 0x0004A23D
		public static LocalizedString MaxChangesMustBeBetween1And512
		{
			get
			{
				return new LocalizedString("MaxChangesMustBeBetween1And512", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06001BF1 RID: 7153 RVA: 0x0004B254 File Offset: 0x0004A254
		public static LocalizedString AttributeValueCannotBeSerialized
		{
			get
			{
				return new LocalizedString("AttributeValueCannotBeSerialized", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06001BF2 RID: 7154 RVA: 0x0004B26B File Offset: 0x0004A26B
		public static LocalizedString SearchFilterMustBeSet
		{
			get
			{
				return new LocalizedString("SearchFilterMustBeSet", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06001BF3 RID: 7155 RVA: 0x0004B282 File Offset: 0x0004A282
		public static LocalizedString EndDateMustBeGreaterThanStartDate
		{
			get
			{
				return new LocalizedString("EndDateMustBeGreaterThanStartDate", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06001BF4 RID: 7156 RVA: 0x0004B299 File Offset: 0x0004A299
		public static LocalizedString InvalidDateTime
		{
			get
			{
				return new LocalizedString("InvalidDateTime", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06001BF5 RID: 7157 RVA: 0x0004B2B0 File Offset: 0x0004A2B0
		public static LocalizedString UpdateItemsDoesNotAllowAttachments
		{
			get
			{
				return new LocalizedString("UpdateItemsDoesNotAllowAttachments", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06001BF6 RID: 7158 RVA: 0x0004B2C7 File Offset: 0x0004A2C7
		public static LocalizedString TimeoutMustBeGreaterThanZero
		{
			get
			{
				return new LocalizedString("TimeoutMustBeGreaterThanZero", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06001BF7 RID: 7159 RVA: 0x0004B2DE File Offset: 0x0004A2DE
		public static LocalizedString AutodiscoverInvalidSettingForOutlookProvider
		{
			get
			{
				return new LocalizedString("AutodiscoverInvalidSettingForOutlookProvider", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06001BF8 RID: 7160 RVA: 0x0004B2F5 File Offset: 0x0004A2F5
		public static LocalizedString InvalidRedirectionResponseReturned
		{
			get
			{
				return new LocalizedString("InvalidRedirectionResponseReturned", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06001BF9 RID: 7161 RVA: 0x0004B30C File Offset: 0x0004A30C
		public static LocalizedString ExpectedStartElement
		{
			get
			{
				return new LocalizedString("ExpectedStartElement", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06001BFA RID: 7162 RVA: 0x0004B323 File Offset: 0x0004A323
		public static LocalizedString DaysOfTheWeekNotSpecified
		{
			get
			{
				return new LocalizedString("DaysOfTheWeekNotSpecified", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06001BFB RID: 7163 RVA: 0x0004B33A File Offset: 0x0004A33A
		public static LocalizedString FolderToUpdateCannotBeNullOrNew
		{
			get
			{
				return new LocalizedString("FolderToUpdateCannotBeNullOrNew", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06001BFC RID: 7164 RVA: 0x0004B351 File Offset: 0x0004A351
		public static LocalizedString PartnerTokenRequestRequiresUrl
		{
			get
			{
				return new LocalizedString("PartnerTokenRequestRequiresUrl", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06001BFD RID: 7165 RVA: 0x0004B368 File Offset: 0x0004A368
		public static LocalizedString NumberOfOccurrencesMustBeGreaterThanZero
		{
			get
			{
				return new LocalizedString("NumberOfOccurrencesMustBeGreaterThanZero", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06001BFE RID: 7166 RVA: 0x0004B37F File Offset: 0x0004A37F
		public static LocalizedString JsonSerializationNotImplemented
		{
			get
			{
				return new LocalizedString("JsonSerializationNotImplemented", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06001BFF RID: 7167 RVA: 0x0004B396 File Offset: 0x0004A396
		public static LocalizedString StartTimeZoneRequired
		{
			get
			{
				return new LocalizedString("StartTimeZoneRequired", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06001C00 RID: 7168 RVA: 0x0004B3AD File Offset: 0x0004A3AD
		public static LocalizedString PropertyAlreadyExistsInOrderByCollection
		{
			get
			{
				return new LocalizedString("PropertyAlreadyExistsInOrderByCollection", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06001C01 RID: 7169 RVA: 0x0004B3C4 File Offset: 0x0004A3C4
		public static LocalizedString ItemAttachmentMustBeNamed
		{
			get
			{
				return new LocalizedString("ItemAttachmentMustBeNamed", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06001C02 RID: 7170 RVA: 0x0004B3DB File Offset: 0x0004A3DB
		public static LocalizedString InvalidAutodiscoverSettingsCount
		{
			get
			{
				return new LocalizedString("InvalidAutodiscoverSettingsCount", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06001C03 RID: 7171 RVA: 0x0004B3F2 File Offset: 0x0004A3F2
		public static LocalizedString LoadingThisObjectTypeNotSupported
		{
			get
			{
				return new LocalizedString("LoadingThisObjectTypeNotSupported", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06001C04 RID: 7172 RVA: 0x0004B409 File Offset: 0x0004A409
		public static LocalizedString UserIdForDelegateUserNotSpecified
		{
			get
			{
				return new LocalizedString("UserIdForDelegateUserNotSpecified", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06001C05 RID: 7173 RVA: 0x0004B420 File Offset: 0x0004A420
		public static LocalizedString PhoneCallAlreadyDisconnected
		{
			get
			{
				return new LocalizedString("PhoneCallAlreadyDisconnected", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06001C06 RID: 7174 RVA: 0x0004B437 File Offset: 0x0004A437
		public static LocalizedString OperationDoesNotSupportAttachments
		{
			get
			{
				return new LocalizedString("OperationDoesNotSupportAttachments", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06001C07 RID: 7175 RVA: 0x0004B44E File Offset: 0x0004A44E
		public static LocalizedString UnsupportedTimeZonePeriodTransitionTarget
		{
			get
			{
				return new LocalizedString("UnsupportedTimeZonePeriodTransitionTarget", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06001C08 RID: 7176 RVA: 0x0004B465 File Offset: 0x0004A465
		public static LocalizedString IEnumerableDoesNotContainThatManyObject
		{
			get
			{
				return new LocalizedString("IEnumerableDoesNotContainThatManyObject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06001C09 RID: 7177 RVA: 0x0004B47C File Offset: 0x0004A47C
		public static LocalizedString UpdateItemsDoesNotSupportNewOrUnchangedItems
		{
			get
			{
				return new LocalizedString("UpdateItemsDoesNotSupportNewOrUnchangedItems", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06001C0A RID: 7178 RVA: 0x0004B493 File Offset: 0x0004A493
		public static LocalizedString ValidationFailed
		{
			get
			{
				return new LocalizedString("ValidationFailed", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06001C0B RID: 7179 RVA: 0x0004B4AA File Offset: 0x0004A4AA
		public static LocalizedString InvalidRecurrencePattern
		{
			get
			{
				return new LocalizedString("InvalidRecurrencePattern", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06001C0C RID: 7180 RVA: 0x0004B4C1 File Offset: 0x0004A4C1
		public static LocalizedString TimeWindowStartTimeMustBeGreaterThanEndTime
		{
			get
			{
				return new LocalizedString("TimeWindowStartTimeMustBeGreaterThanEndTime", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06001C0D RID: 7181 RVA: 0x0004B4D8 File Offset: 0x0004A4D8
		public static LocalizedString InvalidAttributeValue
		{
			get
			{
				return new LocalizedString("InvalidAttributeValue", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06001C0E RID: 7182 RVA: 0x0004B4EF File Offset: 0x0004A4EF
		public static LocalizedString FileAttachmentContentIsNotSet
		{
			get
			{
				return new LocalizedString("FileAttachmentContentIsNotSet", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06001C0F RID: 7183 RVA: 0x0004B506 File Offset: 0x0004A506
		public static LocalizedString AutodiscoverDidNotReturnEwsUrl
		{
			get
			{
				return new LocalizedString("AutodiscoverDidNotReturnEwsUrl", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06001C10 RID: 7184 RVA: 0x0004B51D File Offset: 0x0004A51D
		public static LocalizedString RecurrencePatternMustHaveStartDate
		{
			get
			{
				return new LocalizedString("RecurrencePatternMustHaveStartDate", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06001C11 RID: 7185 RVA: 0x0004B534 File Offset: 0x0004A534
		public static LocalizedString OccurrenceIndexMustBeGreaterThanZero
		{
			get
			{
				return new LocalizedString("OccurrenceIndexMustBeGreaterThanZero", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06001C12 RID: 7186 RVA: 0x0004B54B File Offset: 0x0004A54B
		public static LocalizedString ServiceResponseDoesNotContainXml
		{
			get
			{
				return new LocalizedString("ServiceResponseDoesNotContainXml", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06001C13 RID: 7187 RVA: 0x0004B562 File Offset: 0x0004A562
		public static LocalizedString ItemIsOutOfDate
		{
			get
			{
				return new LocalizedString("ItemIsOutOfDate", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06001C14 RID: 7188 RVA: 0x0004B579 File Offset: 0x0004A579
		public static LocalizedString MinuteMustBeBetween0And59
		{
			get
			{
				return new LocalizedString("MinuteMustBeBetween0And59", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06001C15 RID: 7189 RVA: 0x0004B590 File Offset: 0x0004A590
		public static LocalizedString NoSoapOrWsSecurityEndpointAvailable
		{
			get
			{
				return new LocalizedString("NoSoapOrWsSecurityEndpointAvailable", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06001C16 RID: 7190 RVA: 0x0004B5A7 File Offset: 0x0004A5A7
		public static LocalizedString ElementNotFound
		{
			get
			{
				return new LocalizedString("ElementNotFound", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06001C17 RID: 7191 RVA: 0x0004B5BE File Offset: 0x0004A5BE
		public static LocalizedString IndexIsOutOfRange
		{
			get
			{
				return new LocalizedString("IndexIsOutOfRange", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06001C18 RID: 7192 RVA: 0x0004B5D5 File Offset: 0x0004A5D5
		public static LocalizedString PropertyIsReadOnly
		{
			get
			{
				return new LocalizedString("PropertyIsReadOnly", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06001C19 RID: 7193 RVA: 0x0004B5EC File Offset: 0x0004A5EC
		public static LocalizedString AttachmentCreationFailed
		{
			get
			{
				return new LocalizedString("AttachmentCreationFailed", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06001C1A RID: 7194 RVA: 0x0004B603 File Offset: 0x0004A603
		public static LocalizedString DayOfMonthMustBeBetween1And31
		{
			get
			{
				return new LocalizedString("DayOfMonthMustBeBetween1And31", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06001C1B RID: 7195 RVA: 0x0004B61A File Offset: 0x0004A61A
		public static LocalizedString ServiceRequestFailed
		{
			get
			{
				return new LocalizedString("ServiceRequestFailed", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06001C1C RID: 7196 RVA: 0x0004B631 File Offset: 0x0004A631
		public static LocalizedString DelegateUserHasInvalidUserId
		{
			get
			{
				return new LocalizedString("DelegateUserHasInvalidUserId", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06001C1D RID: 7197 RVA: 0x0004B648 File Offset: 0x0004A648
		public static LocalizedString SearchFilterComparisonValueTypeIsNotSupported
		{
			get
			{
				return new LocalizedString("SearchFilterComparisonValueTypeIsNotSupported", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06001C1E RID: 7198 RVA: 0x0004B65F File Offset: 0x0004A65F
		public static LocalizedString ElementValueCannotBeSerialized
		{
			get
			{
				return new LocalizedString("ElementValueCannotBeSerialized", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06001C1F RID: 7199 RVA: 0x0004B676 File Offset: 0x0004A676
		public static LocalizedString PropertyValueMustBeSpecifiedForRecurrencePattern
		{
			get
			{
				return new LocalizedString("PropertyValueMustBeSpecifiedForRecurrencePattern", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06001C20 RID: 7200 RVA: 0x0004B68D File Offset: 0x0004A68D
		public static LocalizedString NonSummaryPropertyCannotBeUsed
		{
			get
			{
				return new LocalizedString("NonSummaryPropertyCannotBeUsed", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06001C21 RID: 7201 RVA: 0x0004B6A4 File Offset: 0x0004A6A4
		public static LocalizedString HoldIdParameterIsNotSpecified
		{
			get
			{
				return new LocalizedString("HoldIdParameterIsNotSpecified", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06001C22 RID: 7202 RVA: 0x0004B6BB File Offset: 0x0004A6BB
		public static LocalizedString TransitionGroupNotFound
		{
			get
			{
				return new LocalizedString("TransitionGroupNotFound", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06001C23 RID: 7203 RVA: 0x0004B6D2 File Offset: 0x0004A6D2
		public static LocalizedString ObjectTypeNotSupported
		{
			get
			{
				return new LocalizedString("ObjectTypeNotSupported", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06001C24 RID: 7204 RVA: 0x0004B6E9 File Offset: 0x0004A6E9
		public static LocalizedString InvalidTimeoutValue
		{
			get
			{
				return new LocalizedString("InvalidTimeoutValue", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06001C25 RID: 7205 RVA: 0x0004B700 File Offset: 0x0004A700
		public static LocalizedString AutodiscoverRedirectBlocked
		{
			get
			{
				return new LocalizedString("AutodiscoverRedirectBlocked", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06001C26 RID: 7206 RVA: 0x0004B717 File Offset: 0x0004A717
		public static LocalizedString PropertySetCannotBeModified
		{
			get
			{
				return new LocalizedString("PropertySetCannotBeModified", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06001C27 RID: 7207 RVA: 0x0004B72E File Offset: 0x0004A72E
		public static LocalizedString DayOfTheWeekMustBeSpecifiedForRecurrencePattern
		{
			get
			{
				return new LocalizedString("DayOfTheWeekMustBeSpecifiedForRecurrencePattern", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06001C28 RID: 7208 RVA: 0x0004B745 File Offset: 0x0004A745
		public static LocalizedString ServiceObjectAlreadyHasId
		{
			get
			{
				return new LocalizedString("ServiceObjectAlreadyHasId", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06001C29 RID: 7209 RVA: 0x0004B75C File Offset: 0x0004A75C
		public static LocalizedString MethodIncompatibleWithRequestVersion
		{
			get
			{
				return new LocalizedString("MethodIncompatibleWithRequestVersion", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06001C2A RID: 7210 RVA: 0x0004B773 File Offset: 0x0004A773
		public static LocalizedString OperationNotSupportedForPropertyDefinitionType
		{
			get
			{
				return new LocalizedString("OperationNotSupportedForPropertyDefinitionType", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06001C2B RID: 7211 RVA: 0x0004B78A File Offset: 0x0004A78A
		public static LocalizedString InvalidElementStringValue
		{
			get
			{
				return new LocalizedString("InvalidElementStringValue", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06001C2C RID: 7212 RVA: 0x0004B7A1 File Offset: 0x0004A7A1
		public static LocalizedString CollectionIsEmpty
		{
			get
			{
				return new LocalizedString("CollectionIsEmpty", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06001C2D RID: 7213 RVA: 0x0004B7B8 File Offset: 0x0004A7B8
		public static LocalizedString InvalidFrequencyValue
		{
			get
			{
				return new LocalizedString("InvalidFrequencyValue", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06001C2E RID: 7214 RVA: 0x0004B7CF File Offset: 0x0004A7CF
		public static LocalizedString UnexpectedEndOfXmlDocument
		{
			get
			{
				return new LocalizedString("UnexpectedEndOfXmlDocument", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06001C2F RID: 7215 RVA: 0x0004B7E6 File Offset: 0x0004A7E6
		public static LocalizedString FolderTypeNotCompatible
		{
			get
			{
				return new LocalizedString("FolderTypeNotCompatible", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06001C30 RID: 7216 RVA: 0x0004B7FD File Offset: 0x0004A7FD
		public static LocalizedString RequestIncompatibleWithRequestVersion
		{
			get
			{
				return new LocalizedString("RequestIncompatibleWithRequestVersion", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06001C31 RID: 7217 RVA: 0x0004B814 File Offset: 0x0004A814
		public static LocalizedString PropertyTypeIncompatibleWhenUpdatingCollection
		{
			get
			{
				return new LocalizedString("PropertyTypeIncompatibleWhenUpdatingCollection", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06001C32 RID: 7218 RVA: 0x0004B82B File Offset: 0x0004A82B
		public static LocalizedString ServerVersionNotSupported
		{
			get
			{
				return new LocalizedString("ServerVersionNotSupported", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06001C33 RID: 7219 RVA: 0x0004B842 File Offset: 0x0004A842
		public static LocalizedString DurationMustBeSpecifiedWhenScheduled
		{
			get
			{
				return new LocalizedString("DurationMustBeSpecifiedWhenScheduled", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06001C34 RID: 7220 RVA: 0x0004B859 File Offset: 0x0004A859
		public static LocalizedString NoError
		{
			get
			{
				return new LocalizedString("NoError", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06001C35 RID: 7221 RVA: 0x0004B870 File Offset: 0x0004A870
		public static LocalizedString CannotUpdateNewUserConfiguration
		{
			get
			{
				return new LocalizedString("CannotUpdateNewUserConfiguration", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06001C36 RID: 7222 RVA: 0x0004B887 File Offset: 0x0004A887
		public static LocalizedString ObjectTypeIncompatibleWithRequestVersion
		{
			get
			{
				return new LocalizedString("ObjectTypeIncompatibleWithRequestVersion", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06001C37 RID: 7223 RVA: 0x0004B89E File Offset: 0x0004A89E
		public static LocalizedString NullStringArrayElementInvalid
		{
			get
			{
				return new LocalizedString("NullStringArrayElementInvalid", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06001C38 RID: 7224 RVA: 0x0004B8B5 File Offset: 0x0004A8B5
		public static LocalizedString HttpsIsRequired
		{
			get
			{
				return new LocalizedString("HttpsIsRequired", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06001C39 RID: 7225 RVA: 0x0004B8CC File Offset: 0x0004A8CC
		public static LocalizedString MergedFreeBusyIntervalMustBeSmallerThanTimeWindow
		{
			get
			{
				return new LocalizedString("MergedFreeBusyIntervalMustBeSmallerThanTimeWindow", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06001C3A RID: 7226 RVA: 0x0004B8E3 File Offset: 0x0004A8E3
		public static LocalizedString SecondMustBeBetween0And59
		{
			get
			{
				return new LocalizedString("SecondMustBeBetween0And59", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06001C3B RID: 7227 RVA: 0x0004B8FA File Offset: 0x0004A8FA
		public static LocalizedString AtLeastOneAttachmentCouldNotBeDeleted
		{
			get
			{
				return new LocalizedString("AtLeastOneAttachmentCouldNotBeDeleted", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06001C3C RID: 7228 RVA: 0x0004B911 File Offset: 0x0004A911
		public static LocalizedString IdAlreadyInList
		{
			get
			{
				return new LocalizedString("IdAlreadyInList", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06001C3D RID: 7229 RVA: 0x0004B928 File Offset: 0x0004A928
		public static LocalizedString BothSearchFilterAndQueryStringCannotBeSpecified
		{
			get
			{
				return new LocalizedString("BothSearchFilterAndQueryStringCannotBeSpecified", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06001C3E RID: 7230 RVA: 0x0004B93F File Offset: 0x0004A93F
		public static LocalizedString AdditionalPropertyIsNull
		{
			get
			{
				return new LocalizedString("AdditionalPropertyIsNull", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06001C3F RID: 7231 RVA: 0x0004B956 File Offset: 0x0004A956
		public static LocalizedString InvalidEmailAddress
		{
			get
			{
				return new LocalizedString("InvalidEmailAddress", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06001C40 RID: 7232 RVA: 0x0004B96D File Offset: 0x0004A96D
		public static LocalizedString MaximumRedirectionHopsExceeded
		{
			get
			{
				return new LocalizedString("MaximumRedirectionHopsExceeded", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06001C41 RID: 7233 RVA: 0x0004B984 File Offset: 0x0004A984
		public static LocalizedString AutodiscoverCouldNotBeLocated
		{
			get
			{
				return new LocalizedString("AutodiscoverCouldNotBeLocated", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06001C42 RID: 7234 RVA: 0x0004B99B File Offset: 0x0004A99B
		public static LocalizedString NoSubscriptionsOnConnection
		{
			get
			{
				return new LocalizedString("NoSubscriptionsOnConnection", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06001C43 RID: 7235 RVA: 0x0004B9B2 File Offset: 0x0004A9B2
		public static LocalizedString PermissionLevelInvalidForNonCalendarFolder
		{
			get
			{
				return new LocalizedString("PermissionLevelInvalidForNonCalendarFolder", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06001C44 RID: 7236 RVA: 0x0004B9C9 File Offset: 0x0004A9C9
		public static LocalizedString InvalidAuthScheme
		{
			get
			{
				return new LocalizedString("InvalidAuthScheme", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06001C45 RID: 7237 RVA: 0x0004B9E0 File Offset: 0x0004A9E0
		public static LocalizedString JsonDeserializationNotImplemented
		{
			get
			{
				return new LocalizedString("JsonDeserializationNotImplemented", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06001C46 RID: 7238 RVA: 0x0004B9F7 File Offset: 0x0004A9F7
		public static LocalizedString ValuePropertyNotLoaded
		{
			get
			{
				return new LocalizedString("ValuePropertyNotLoaded", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06001C47 RID: 7239 RVA: 0x0004BA0E File Offset: 0x0004AA0E
		public static LocalizedString PropertyIncompatibleWithRequestVersion
		{
			get
			{
				return new LocalizedString("PropertyIncompatibleWithRequestVersion", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06001C48 RID: 7240 RVA: 0x0004BA25 File Offset: 0x0004AA25
		public static LocalizedString OffsetMustBeGreaterThanZero
		{
			get
			{
				return new LocalizedString("OffsetMustBeGreaterThanZero", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06001C49 RID: 7241 RVA: 0x0004BA3C File Offset: 0x0004AA3C
		public static LocalizedString CreateItemsDoesNotAllowAttachments
		{
			get
			{
				return new LocalizedString("CreateItemsDoesNotAllowAttachments", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06001C4A RID: 7242 RVA: 0x0004BA53 File Offset: 0x0004AA53
		public static LocalizedString PropertyDefinitionTypeMismatch
		{
			get
			{
				return new LocalizedString("PropertyDefinitionTypeMismatch", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06001C4B RID: 7243 RVA: 0x0004BA6A File Offset: 0x0004AA6A
		public static LocalizedString IntervalMustBeGreaterOrEqualToOne
		{
			get
			{
				return new LocalizedString("IntervalMustBeGreaterOrEqualToOne", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06001C4C RID: 7244 RVA: 0x0004BA81 File Offset: 0x0004AA81
		public static LocalizedString CannotSetPermissionLevelToCustom
		{
			get
			{
				return new LocalizedString("CannotSetPermissionLevelToCustom", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06001C4D RID: 7245 RVA: 0x0004BA98 File Offset: 0x0004AA98
		public static LocalizedString CannotAddRequestHeader
		{
			get
			{
				return new LocalizedString("CannotAddRequestHeader", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06001C4E RID: 7246 RVA: 0x0004BAAF File Offset: 0x0004AAAF
		public static LocalizedString ArrayMustHaveAtLeastOneElement
		{
			get
			{
				return new LocalizedString("ArrayMustHaveAtLeastOneElement", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06001C4F RID: 7247 RVA: 0x0004BAC6 File Offset: 0x0004AAC6
		public static LocalizedString MonthMustBeSpecifiedForRecurrencePattern
		{
			get
			{
				return new LocalizedString("MonthMustBeSpecifiedForRecurrencePattern", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06001C50 RID: 7248 RVA: 0x0004BADD File Offset: 0x0004AADD
		public static LocalizedString ValueOfTypeCannotBeConverted
		{
			get
			{
				return new LocalizedString("ValueOfTypeCannotBeConverted", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06001C51 RID: 7249 RVA: 0x0004BAF4 File Offset: 0x0004AAF4
		public static LocalizedString ValueCannotBeConverted
		{
			get
			{
				return new LocalizedString("ValueCannotBeConverted", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06001C52 RID: 7250 RVA: 0x0004BB0B File Offset: 0x0004AB0B
		public static LocalizedString ServerErrorAndStackTraceDetails
		{
			get
			{
				return new LocalizedString("ServerErrorAndStackTraceDetails", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06001C53 RID: 7251 RVA: 0x0004BB22 File Offset: 0x0004AB22
		public static LocalizedString FolderPermissionLevelMustBeSet
		{
			get
			{
				return new LocalizedString("FolderPermissionLevelMustBeSet", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06001C54 RID: 7252 RVA: 0x0004BB39 File Offset: 0x0004AB39
		public static LocalizedString AutodiscoverError
		{
			get
			{
				return new LocalizedString("AutodiscoverError", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06001C55 RID: 7253 RVA: 0x0004BB50 File Offset: 0x0004AB50
		public static LocalizedString ArrayMustHaveSingleDimension
		{
			get
			{
				return new LocalizedString("ArrayMustHaveSingleDimension", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06001C56 RID: 7254 RVA: 0x0004BB67 File Offset: 0x0004AB67
		public static LocalizedString InvalidPropertyValueNotInRange
		{
			get
			{
				return new LocalizedString("InvalidPropertyValueNotInRange", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06001C57 RID: 7255 RVA: 0x0004BB7E File Offset: 0x0004AB7E
		public static LocalizedString RegenerationPatternsOnlyValidForTasks
		{
			get
			{
				return new LocalizedString("RegenerationPatternsOnlyValidForTasks", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06001C58 RID: 7256 RVA: 0x0004BB95 File Offset: 0x0004AB95
		public static LocalizedString ItemAttachmentCannotBeUpdated
		{
			get
			{
				return new LocalizedString("ItemAttachmentCannotBeUpdated", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06001C59 RID: 7257 RVA: 0x0004BBAC File Offset: 0x0004ABAC
		public static LocalizedString EqualityComparisonFilterIsInvalid
		{
			get
			{
				return new LocalizedString("EqualityComparisonFilterIsInvalid", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06001C5A RID: 7258 RVA: 0x0004BBC3 File Offset: 0x0004ABC3
		public static LocalizedString AutodiscoverServiceRequestRequiresDomainOrUrl
		{
			get
			{
				return new LocalizedString("AutodiscoverServiceRequestRequiresDomainOrUrl", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06001C5B RID: 7259 RVA: 0x0004BBDA File Offset: 0x0004ABDA
		public static LocalizedString InvalidUser
		{
			get
			{
				return new LocalizedString("InvalidUser", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06001C5C RID: 7260 RVA: 0x0004BBF1 File Offset: 0x0004ABF1
		public static LocalizedString AccountIsLocked
		{
			get
			{
				return new LocalizedString("AccountIsLocked", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06001C5D RID: 7261 RVA: 0x0004BC08 File Offset: 0x0004AC08
		public static LocalizedString InvalidDomainName
		{
			get
			{
				return new LocalizedString("InvalidDomainName", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06001C5E RID: 7262 RVA: 0x0004BC1F File Offset: 0x0004AC1F
		public static LocalizedString TooFewServiceReponsesReturned
		{
			get
			{
				return new LocalizedString("TooFewServiceReponsesReturned", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06001C5F RID: 7263 RVA: 0x0004BC36 File Offset: 0x0004AC36
		public static LocalizedString CannotSubscribeToStatusEvents
		{
			get
			{
				return new LocalizedString("CannotSubscribeToStatusEvents", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06001C60 RID: 7264 RVA: 0x0004BC4D File Offset: 0x0004AC4D
		public static LocalizedString InvalidSortByPropertyForMailboxSearch
		{
			get
			{
				return new LocalizedString("InvalidSortByPropertyForMailboxSearch", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06001C61 RID: 7265 RVA: 0x0004BC64 File Offset: 0x0004AC64
		public static LocalizedString UnexpectedElementType
		{
			get
			{
				return new LocalizedString("UnexpectedElementType", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06001C62 RID: 7266 RVA: 0x0004BC7B File Offset: 0x0004AC7B
		public static LocalizedString ValueMustBeGreaterThanZero
		{
			get
			{
				return new LocalizedString("ValueMustBeGreaterThanZero", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06001C63 RID: 7267 RVA: 0x0004BC92 File Offset: 0x0004AC92
		public static LocalizedString AttachmentCannotBeUpdated
		{
			get
			{
				return new LocalizedString("AttachmentCannotBeUpdated", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06001C64 RID: 7268 RVA: 0x0004BCA9 File Offset: 0x0004ACA9
		public static LocalizedString CreateItemsDoesNotHandleExistingItems
		{
			get
			{
				return new LocalizedString("CreateItemsDoesNotHandleExistingItems", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06001C65 RID: 7269 RVA: 0x0004BCC0 File Offset: 0x0004ACC0
		public static LocalizedString MultipleContactPhotosInAttachment
		{
			get
			{
				return new LocalizedString("MultipleContactPhotosInAttachment", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06001C66 RID: 7270 RVA: 0x0004BCD7 File Offset: 0x0004ACD7
		public static LocalizedString InvalidRecurrenceRange
		{
			get
			{
				return new LocalizedString("InvalidRecurrenceRange", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06001C67 RID: 7271 RVA: 0x0004BCEE File Offset: 0x0004ACEE
		public static LocalizedString CannotSetBothImpersonatedAndPrivilegedUser
		{
			get
			{
				return new LocalizedString("CannotSetBothImpersonatedAndPrivilegedUser", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06001C68 RID: 7272 RVA: 0x0004BD05 File Offset: 0x0004AD05
		public static LocalizedString NewMessagesWithAttachmentsCannotBeSentDirectly
		{
			get
			{
				return new LocalizedString("NewMessagesWithAttachmentsCannotBeSentDirectly", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06001C69 RID: 7273 RVA: 0x0004BD1C File Offset: 0x0004AD1C
		public static LocalizedString CannotCallDisconnectWithNoLiveConnection
		{
			get
			{
				return new LocalizedString("CannotCallDisconnectWithNoLiveConnection", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06001C6A RID: 7274 RVA: 0x0004BD33 File Offset: 0x0004AD33
		public static LocalizedString IdPropertyMustBeSet
		{
			get
			{
				return new LocalizedString("IdPropertyMustBeSet", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06001C6B RID: 7275 RVA: 0x0004BD4A File Offset: 0x0004AD4A
		public static LocalizedString ValuePropertyNotAssigned
		{
			get
			{
				return new LocalizedString("ValuePropertyNotAssigned", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06001C6C RID: 7276 RVA: 0x0004BD61 File Offset: 0x0004AD61
		public static LocalizedString ZeroLengthArrayInvalid
		{
			get
			{
				return new LocalizedString("ZeroLengthArrayInvalid", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06001C6D RID: 7277 RVA: 0x0004BD78 File Offset: 0x0004AD78
		public static LocalizedString HoldMailboxesParameterIsNotSpecified
		{
			get
			{
				return new LocalizedString("HoldMailboxesParameterIsNotSpecified", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06001C6E RID: 7278 RVA: 0x0004BD8F File Offset: 0x0004AD8F
		public static LocalizedString CannotSaveNotNewUserConfiguration
		{
			get
			{
				return new LocalizedString("CannotSaveNotNewUserConfiguration", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06001C6F RID: 7279 RVA: 0x0004BDA6 File Offset: 0x0004ADA6
		public static LocalizedString ServiceObjectDoesNotHaveId
		{
			get
			{
				return new LocalizedString("ServiceObjectDoesNotHaveId", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06001C70 RID: 7280 RVA: 0x0004BDBD File Offset: 0x0004ADBD
		public static LocalizedString PropertyCollectionSizeMismatch
		{
			get
			{
				return new LocalizedString("PropertyCollectionSizeMismatch", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06001C71 RID: 7281 RVA: 0x0004BDD4 File Offset: 0x0004ADD4
		public static LocalizedString XsDurationCouldNotBeParsed
		{
			get
			{
				return new LocalizedString("XsDurationCouldNotBeParsed", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06001C72 RID: 7282 RVA: 0x0004BDEB File Offset: 0x0004ADEB
		public static LocalizedString UnknownTimeZonePeriodTransitionType
		{
			get
			{
				return new LocalizedString("UnknownTimeZonePeriodTransitionType", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001C73 RID: 7283 RVA: 0x0004BE02 File Offset: 0x0004AE02
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x04001468 RID: 5224
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(192);

		// Token: 0x04001469 RID: 5225
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.WebServices.Strings", typeof(Strings).Assembly);

		// Token: 0x0200030D RID: 781
		public enum IDs : uint
		{
			// Token: 0x0400146B RID: 5227
			CannotRemoveSubscriptionFromLiveConnection = 1631423810U,
			// Token: 0x0400146C RID: 5228
			ReadAccessInvalidForNonCalendarFolder = 3355844483U,
			// Token: 0x0400146D RID: 5229
			PropertyDefinitionPropertyMustBeSet = 1413133863U,
			// Token: 0x0400146E RID: 5230
			ArgumentIsBlankString = 2808916828U,
			// Token: 0x0400146F RID: 5231
			InvalidAutodiscoverDomainsCount = 2110564001U,
			// Token: 0x04001470 RID: 5232
			MinutesMustBeBetween0And1439 = 3846446647U,
			// Token: 0x04001471 RID: 5233
			DeleteInvalidForUnsavedUserConfiguration = 3725066606U,
			// Token: 0x04001472 RID: 5234
			PeriodNotFound = 732877372U,
			// Token: 0x04001473 RID: 5235
			InvalidAutodiscoverSmtpAddress = 742945049U,
			// Token: 0x04001474 RID: 5236
			InvalidOAuthToken = 2371807741U,
			// Token: 0x04001475 RID: 5237
			MaxScpHopsExceeded = 540781291U,
			// Token: 0x04001476 RID: 5238
			ContactGroupMemberCannotBeUpdatedWithoutBeingLoadedFirst = 412932664U,
			// Token: 0x04001477 RID: 5239
			CurrentPositionNotElementStart = 3932722495U,
			// Token: 0x04001478 RID: 5240
			CannotConvertBetweenTimeZones = 1762296216U,
			// Token: 0x04001479 RID: 5241
			FrequencyMustBeBetween1And1440 = 2947629837U,
			// Token: 0x0400147A RID: 5242
			CannotSetDelegateFolderPermissionLevelToCustom = 4255072555U,
			// Token: 0x0400147B RID: 5243
			PartnerTokenIncompatibleWithRequestVersion = 231532733U,
			// Token: 0x0400147C RID: 5244
			InvalidAutodiscoverRequest = 216906786U,
			// Token: 0x0400147D RID: 5245
			InvalidAsyncResult = 2795977038U,
			// Token: 0x0400147E RID: 5246
			InvalidMailboxType = 1875536889U,
			// Token: 0x0400147F RID: 5247
			AttachmentCollectionNotLoaded = 368467777U,
			// Token: 0x04001480 RID: 5248
			ParameterIncompatibleWithRequestVersion = 1937216341U,
			// Token: 0x04001481 RID: 5249
			DayOfWeekIndexMustBeSpecifiedForRecurrencePattern = 814325722U,
			// Token: 0x04001482 RID: 5250
			WLIDCredentialsCannotBeUsedWithLegacyAutodiscover = 365885070U,
			// Token: 0x04001483 RID: 5251
			PropertyCannotBeUpdated = 2534953608U,
			// Token: 0x04001484 RID: 5252
			IncompatibleTypeForArray = 3820761979U,
			// Token: 0x04001485 RID: 5253
			PercentCompleteMustBeBetween0And100 = 3936886128U,
			// Token: 0x04001486 RID: 5254
			AutodiscoverServiceIncompatibleWithRequestVersion = 460414997U,
			// Token: 0x04001487 RID: 5255
			InvalidAutodiscoverSmtpAddressesCount = 1774410042U,
			// Token: 0x04001488 RID: 5256
			ServiceUrlMustBeSet = 1922401890U,
			// Token: 0x04001489 RID: 5257
			ItemTypeNotCompatible = 4181025268U,
			// Token: 0x0400148A RID: 5258
			AttachmentItemTypeMismatch = 1516841384U,
			// Token: 0x0400148B RID: 5259
			UnsupportedWebProtocol = 3620572079U,
			// Token: 0x0400148C RID: 5260
			EnumValueIncompatibleWithRequestVersion = 777131942U,
			// Token: 0x0400148D RID: 5261
			UnexpectedElement = 2080190431U,
			// Token: 0x0400148E RID: 5262
			InvalidOrderBy = 162617974U,
			// Token: 0x0400148F RID: 5263
			NoAppropriateConstructorForItemClass = 3730786468U,
			// Token: 0x04001490 RID: 5264
			SearchFilterAtIndexIsInvalid = 1334400254U,
			// Token: 0x04001491 RID: 5265
			DeletingThisObjectTypeNotAuthorized = 3957228737U,
			// Token: 0x04001492 RID: 5266
			PropertyCannotBeDeleted = 2011990502U,
			// Token: 0x04001493 RID: 5267
			ValuePropertyMustBeSet = 49538054U,
			// Token: 0x04001494 RID: 5268
			TagValueIsOutOfRange = 4177209255U,
			// Token: 0x04001495 RID: 5269
			ItemToUpdateCannotBeNullOrNew = 893972063U,
			// Token: 0x04001496 RID: 5270
			SearchParametersRootFolderIdsEmpty = 2492696699U,
			// Token: 0x04001497 RID: 5271
			MailboxQueriesParameterIsNotSpecified = 2182404464U,
			// Token: 0x04001498 RID: 5272
			FolderPermissionHasInvalidUserId = 3079787672U,
			// Token: 0x04001499 RID: 5273
			InvalidAutodiscoverDomain = 95137117U,
			// Token: 0x0400149A RID: 5274
			MailboxesParameterIsNotSpecified = 1274908260U,
			// Token: 0x0400149B RID: 5275
			ParentFolderDoesNotHaveId = 3207115397U,
			// Token: 0x0400149C RID: 5276
			DayOfMonthMustBeSpecifiedForRecurrencePattern = 190140884U,
			// Token: 0x0400149D RID: 5277
			ClassIncompatibleWithRequestVersion = 886236812U,
			// Token: 0x0400149E RID: 5278
			CertificateHasNoPrivateKey = 956539910U,
			// Token: 0x0400149F RID: 5279
			InvalidOrUnsupportedTimeZoneDefinition = 2660350763U,
			// Token: 0x040014A0 RID: 5280
			HourMustBeBetween0And23 = 980862610U,
			// Token: 0x040014A1 RID: 5281
			TimeoutMustBeBetween1And1440 = 1864811546U,
			// Token: 0x040014A2 RID: 5282
			CredentialsRequired = 107509017U,
			// Token: 0x040014A3 RID: 5283
			MustLoadOrAssignPropertyBeforeAccess = 1301287431U,
			// Token: 0x040014A4 RID: 5284
			InvalidAutodiscoverServiceResponse = 2421645987U,
			// Token: 0x040014A5 RID: 5285
			CannotCallConnectDuringLiveConnection = 3637880390U,
			// Token: 0x040014A6 RID: 5286
			ObjectDoesNotHaveId = 1990317298U,
			// Token: 0x040014A7 RID: 5287
			CannotAddSubscriptionToLiveConnection = 2374077290U,
			// Token: 0x040014A8 RID: 5288
			MaxChangesMustBeBetween1And512 = 3989266019U,
			// Token: 0x040014A9 RID: 5289
			AttributeValueCannotBeSerialized = 3745271395U,
			// Token: 0x040014AA RID: 5290
			SearchFilterMustBeSet = 2182989540U,
			// Token: 0x040014AB RID: 5291
			EndDateMustBeGreaterThanStartDate = 2696927076U,
			// Token: 0x040014AC RID: 5292
			InvalidDateTime = 3410810540U,
			// Token: 0x040014AD RID: 5293
			UpdateItemsDoesNotAllowAttachments = 2505974152U,
			// Token: 0x040014AE RID: 5294
			TimeoutMustBeGreaterThanZero = 3210574608U,
			// Token: 0x040014AF RID: 5295
			AutodiscoverInvalidSettingForOutlookProvider = 81925120U,
			// Token: 0x040014B0 RID: 5296
			InvalidRedirectionResponseReturned = 706868687U,
			// Token: 0x040014B1 RID: 5297
			ExpectedStartElement = 3728006586U,
			// Token: 0x040014B2 RID: 5298
			DaysOfTheWeekNotSpecified = 2925351706U,
			// Token: 0x040014B3 RID: 5299
			FolderToUpdateCannotBeNullOrNew = 2887145506U,
			// Token: 0x040014B4 RID: 5300
			PartnerTokenRequestRequiresUrl = 1709653493U,
			// Token: 0x040014B5 RID: 5301
			NumberOfOccurrencesMustBeGreaterThanZero = 4020293177U,
			// Token: 0x040014B6 RID: 5302
			JsonSerializationNotImplemented = 537341821U,
			// Token: 0x040014B7 RID: 5303
			StartTimeZoneRequired = 263088904U,
			// Token: 0x040014B8 RID: 5304
			PropertyAlreadyExistsInOrderByCollection = 2291792901U,
			// Token: 0x040014B9 RID: 5305
			ItemAttachmentMustBeNamed = 2209938519U,
			// Token: 0x040014BA RID: 5306
			InvalidAutodiscoverSettingsCount = 2688936715U,
			// Token: 0x040014BB RID: 5307
			LoadingThisObjectTypeNotSupported = 1313605428U,
			// Token: 0x040014BC RID: 5308
			UserIdForDelegateUserNotSpecified = 3460610998U,
			// Token: 0x040014BD RID: 5309
			PhoneCallAlreadyDisconnected = 2720737469U,
			// Token: 0x040014BE RID: 5310
			OperationDoesNotSupportAttachments = 1464025756U,
			// Token: 0x040014BF RID: 5311
			UnsupportedTimeZonePeriodTransitionTarget = 98512741U,
			// Token: 0x040014C0 RID: 5312
			IEnumerableDoesNotContainThatManyObject = 474853648U,
			// Token: 0x040014C1 RID: 5313
			UpdateItemsDoesNotSupportNewOrUnchangedItems = 953072612U,
			// Token: 0x040014C2 RID: 5314
			ValidationFailed = 1341796948U,
			// Token: 0x040014C3 RID: 5315
			InvalidRecurrencePattern = 3636462697U,
			// Token: 0x040014C4 RID: 5316
			TimeWindowStartTimeMustBeGreaterThanEndTime = 4170253059U,
			// Token: 0x040014C5 RID: 5317
			InvalidAttributeValue = 369811396U,
			// Token: 0x040014C6 RID: 5318
			FileAttachmentContentIsNotSet = 2449142619U,
			// Token: 0x040014C7 RID: 5319
			AutodiscoverDidNotReturnEwsUrl = 628121484U,
			// Token: 0x040014C8 RID: 5320
			RecurrencePatternMustHaveStartDate = 423461609U,
			// Token: 0x040014C9 RID: 5321
			OccurrenceIndexMustBeGreaterThanZero = 1396112272U,
			// Token: 0x040014CA RID: 5322
			ServiceResponseDoesNotContainXml = 1981959699U,
			// Token: 0x040014CB RID: 5323
			ItemIsOutOfDate = 2560682386U,
			// Token: 0x040014CC RID: 5324
			MinuteMustBeBetween0And59 = 3637133283U,
			// Token: 0x040014CD RID: 5325
			NoSoapOrWsSecurityEndpointAvailable = 3954825173U,
			// Token: 0x040014CE RID: 5326
			ElementNotFound = 3189440097U,
			// Token: 0x040014CF RID: 5327
			IndexIsOutOfRange = 4223767916U,
			// Token: 0x040014D0 RID: 5328
			PropertyIsReadOnly = 869119007U,
			// Token: 0x040014D1 RID: 5329
			AttachmentCreationFailed = 2817837707U,
			// Token: 0x040014D2 RID: 5330
			DayOfMonthMustBeBetween1And31 = 3133762315U,
			// Token: 0x040014D3 RID: 5331
			ServiceRequestFailed = 2091738407U,
			// Token: 0x040014D4 RID: 5332
			DelegateUserHasInvalidUserId = 129422921U,
			// Token: 0x040014D5 RID: 5333
			SearchFilterComparisonValueTypeIsNotSupported = 1069581653U,
			// Token: 0x040014D6 RID: 5334
			ElementValueCannotBeSerialized = 4258284629U,
			// Token: 0x040014D7 RID: 5335
			PropertyValueMustBeSpecifiedForRecurrencePattern = 2924950297U,
			// Token: 0x040014D8 RID: 5336
			NonSummaryPropertyCannotBeUsed = 2436895661U,
			// Token: 0x040014D9 RID: 5337
			HoldIdParameterIsNotSpecified = 134310332U,
			// Token: 0x040014DA RID: 5338
			TransitionGroupNotFound = 21384399U,
			// Token: 0x040014DB RID: 5339
			ObjectTypeNotSupported = 4144876524U,
			// Token: 0x040014DC RID: 5340
			InvalidTimeoutValue = 3454211069U,
			// Token: 0x040014DD RID: 5341
			AutodiscoverRedirectBlocked = 3339063014U,
			// Token: 0x040014DE RID: 5342
			PropertySetCannotBeModified = 3717641032U,
			// Token: 0x040014DF RID: 5343
			DayOfTheWeekMustBeSpecifiedForRecurrencePattern = 3877446129U,
			// Token: 0x040014E0 RID: 5344
			ServiceObjectAlreadyHasId = 3918321785U,
			// Token: 0x040014E1 RID: 5345
			MethodIncompatibleWithRequestVersion = 4274338115U,
			// Token: 0x040014E2 RID: 5346
			OperationNotSupportedForPropertyDefinitionType = 2727810523U,
			// Token: 0x040014E3 RID: 5347
			InvalidElementStringValue = 3934659801U,
			// Token: 0x040014E4 RID: 5348
			CollectionIsEmpty = 500353177U,
			// Token: 0x040014E5 RID: 5349
			InvalidFrequencyValue = 885177846U,
			// Token: 0x040014E6 RID: 5350
			UnexpectedEndOfXmlDocument = 546825189U,
			// Token: 0x040014E7 RID: 5351
			FolderTypeNotCompatible = 3578997681U,
			// Token: 0x040014E8 RID: 5352
			RequestIncompatibleWithRequestVersion = 3451337077U,
			// Token: 0x040014E9 RID: 5353
			PropertyTypeIncompatibleWhenUpdatingCollection = 4148083232U,
			// Token: 0x040014EA RID: 5354
			ServerVersionNotSupported = 603284986U,
			// Token: 0x040014EB RID: 5355
			DurationMustBeSpecifiedWhenScheduled = 666454105U,
			// Token: 0x040014EC RID: 5356
			NoError = 1005127777U,
			// Token: 0x040014ED RID: 5357
			CannotUpdateNewUserConfiguration = 3972010693U,
			// Token: 0x040014EE RID: 5358
			ObjectTypeIncompatibleWithRequestVersion = 2438108153U,
			// Token: 0x040014EF RID: 5359
			NullStringArrayElementInvalid = 3604837092U,
			// Token: 0x040014F0 RID: 5360
			HttpsIsRequired = 333950754U,
			// Token: 0x040014F1 RID: 5361
			MergedFreeBusyIntervalMustBeSmallerThanTimeWindow = 2480357954U,
			// Token: 0x040014F2 RID: 5362
			SecondMustBeBetween0And59 = 328519365U,
			// Token: 0x040014F3 RID: 5363
			AtLeastOneAttachmentCouldNotBeDeleted = 3596957401U,
			// Token: 0x040014F4 RID: 5364
			IdAlreadyInList = 1233804470U,
			// Token: 0x040014F5 RID: 5365
			BothSearchFilterAndQueryStringCannotBeSpecified = 3295662635U,
			// Token: 0x040014F6 RID: 5366
			AdditionalPropertyIsNull = 3383788511U,
			// Token: 0x040014F7 RID: 5367
			InvalidEmailAddress = 2316486059U,
			// Token: 0x040014F8 RID: 5368
			MaximumRedirectionHopsExceeded = 1588497945U,
			// Token: 0x040014F9 RID: 5369
			AutodiscoverCouldNotBeLocated = 3097538091U,
			// Token: 0x040014FA RID: 5370
			NoSubscriptionsOnConnection = 2715578908U,
			// Token: 0x040014FB RID: 5371
			PermissionLevelInvalidForNonCalendarFolder = 1063351272U,
			// Token: 0x040014FC RID: 5372
			InvalidAuthScheme = 2845550636U,
			// Token: 0x040014FD RID: 5373
			JsonDeserializationNotImplemented = 311071154U,
			// Token: 0x040014FE RID: 5374
			ValuePropertyNotLoaded = 2743202310U,
			// Token: 0x040014FF RID: 5375
			PropertyIncompatibleWithRequestVersion = 843308875U,
			// Token: 0x04001500 RID: 5376
			OffsetMustBeGreaterThanZero = 2437116628U,
			// Token: 0x04001501 RID: 5377
			CreateItemsDoesNotAllowAttachments = 690508625U,
			// Token: 0x04001502 RID: 5378
			PropertyDefinitionTypeMismatch = 2445370550U,
			// Token: 0x04001503 RID: 5379
			IntervalMustBeGreaterOrEqualToOne = 2129318611U,
			// Token: 0x04001504 RID: 5380
			CannotSetPermissionLevelToCustom = 961741172U,
			// Token: 0x04001505 RID: 5381
			CannotAddRequestHeader = 2921139860U,
			// Token: 0x04001506 RID: 5382
			ArrayMustHaveAtLeastOneElement = 4264880578U,
			// Token: 0x04001507 RID: 5383
			MonthMustBeSpecifiedForRecurrencePattern = 567828041U,
			// Token: 0x04001508 RID: 5384
			ValueOfTypeCannotBeConverted = 3869807514U,
			// Token: 0x04001509 RID: 5385
			ValueCannotBeConverted = 788051255U,
			// Token: 0x0400150A RID: 5386
			ServerErrorAndStackTraceDetails = 1481761255U,
			// Token: 0x0400150B RID: 5387
			FolderPermissionLevelMustBeSet = 2586079185U,
			// Token: 0x0400150C RID: 5388
			AutodiscoverError = 4003396996U,
			// Token: 0x0400150D RID: 5389
			ArrayMustHaveSingleDimension = 3066801652U,
			// Token: 0x0400150E RID: 5390
			InvalidPropertyValueNotInRange = 2233059550U,
			// Token: 0x0400150F RID: 5391
			RegenerationPatternsOnlyValidForTasks = 1453973661U,
			// Token: 0x04001510 RID: 5392
			ItemAttachmentCannotBeUpdated = 914733855U,
			// Token: 0x04001511 RID: 5393
			EqualityComparisonFilterIsInvalid = 1467147488U,
			// Token: 0x04001512 RID: 5394
			AutodiscoverServiceRequestRequiresDomainOrUrl = 2950491364U,
			// Token: 0x04001513 RID: 5395
			InvalidUser = 3929050450U,
			// Token: 0x04001514 RID: 5396
			AccountIsLocked = 2901788841U,
			// Token: 0x04001515 RID: 5397
			InvalidDomainName = 2762661174U,
			// Token: 0x04001516 RID: 5398
			TooFewServiceReponsesReturned = 710118117U,
			// Token: 0x04001517 RID: 5399
			CannotSubscribeToStatusEvents = 463464377U,
			// Token: 0x04001518 RID: 5400
			InvalidSortByPropertyForMailboxSearch = 1066736932U,
			// Token: 0x04001519 RID: 5401
			UnexpectedElementType = 2100812591U,
			// Token: 0x0400151A RID: 5402
			ValueMustBeGreaterThanZero = 691200302U,
			// Token: 0x0400151B RID: 5403
			AttachmentCannotBeUpdated = 2292458752U,
			// Token: 0x0400151C RID: 5404
			CreateItemsDoesNotHandleExistingItems = 3912371609U,
			// Token: 0x0400151D RID: 5405
			MultipleContactPhotosInAttachment = 2967125165U,
			// Token: 0x0400151E RID: 5406
			InvalidRecurrenceRange = 3387532664U,
			// Token: 0x0400151F RID: 5407
			CannotSetBothImpersonatedAndPrivilegedUser = 4210899574U,
			// Token: 0x04001520 RID: 5408
			NewMessagesWithAttachmentsCannotBeSentDirectly = 1565629292U,
			// Token: 0x04001521 RID: 5409
			CannotCallDisconnectWithNoLiveConnection = 3633425214U,
			// Token: 0x04001522 RID: 5410
			IdPropertyMustBeSet = 3940556486U,
			// Token: 0x04001523 RID: 5411
			ValuePropertyNotAssigned = 1562822901U,
			// Token: 0x04001524 RID: 5412
			ZeroLengthArrayInvalid = 4061174908U,
			// Token: 0x04001525 RID: 5413
			HoldMailboxesParameterIsNotSpecified = 3345946933U,
			// Token: 0x04001526 RID: 5414
			CannotSaveNotNewUserConfiguration = 2270311116U,
			// Token: 0x04001527 RID: 5415
			ServiceObjectDoesNotHaveId = 2111195463U,
			// Token: 0x04001528 RID: 5416
			PropertyCollectionSizeMismatch = 491519754U,
			// Token: 0x04001529 RID: 5417
			XsDurationCouldNotBeParsed = 2861470707U,
			// Token: 0x0400152A RID: 5418
			UnknownTimeZonePeriodTransitionType = 2279096081U
		}
	}
}
