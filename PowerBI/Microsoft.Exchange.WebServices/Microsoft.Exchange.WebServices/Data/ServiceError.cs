using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200023F RID: 575
	public enum ServiceError
	{
		// Token: 0x04000FF8 RID: 4088
		NoError,
		// Token: 0x04000FF9 RID: 4089
		ErrorAccessDenied,
		// Token: 0x04000FFA RID: 4090
		ErrorAccessModeSpecified,
		// Token: 0x04000FFB RID: 4091
		ErrorAccountDisabled,
		// Token: 0x04000FFC RID: 4092
		ErrorAddDelegatesFailed,
		// Token: 0x04000FFD RID: 4093
		ErrorAddressSpaceNotFound,
		// Token: 0x04000FFE RID: 4094
		ErrorADOperation,
		// Token: 0x04000FFF RID: 4095
		ErrorADSessionFilter,
		// Token: 0x04001000 RID: 4096
		ErrorADUnavailable,
		// Token: 0x04001001 RID: 4097
		ErrorAffectedTaskOccurrencesRequired,
		// Token: 0x04001002 RID: 4098
		ErrorApplyConversationActionFailed,
		// Token: 0x04001003 RID: 4099
		ErrorArchiveMailboxNotEnabled,
		// Token: 0x04001004 RID: 4100
		ErrorArchiveFolderPathCreation,
		// Token: 0x04001005 RID: 4101
		ErrorArchiveMailboxServiceDiscoveryFailed,
		// Token: 0x04001006 RID: 4102
		ErrorAttachmentNestLevelLimitExceeded,
		// Token: 0x04001007 RID: 4103
		ErrorAttachmentSizeLimitExceeded,
		// Token: 0x04001008 RID: 4104
		ErrorAutoDiscoverFailed,
		// Token: 0x04001009 RID: 4105
		ErrorAvailabilityConfigNotFound,
		// Token: 0x0400100A RID: 4106
		ErrorBatchProcessingStopped,
		// Token: 0x0400100B RID: 4107
		ErrorCalendarCannotMoveOrCopyOccurrence,
		// Token: 0x0400100C RID: 4108
		ErrorCalendarCannotUpdateDeletedItem,
		// Token: 0x0400100D RID: 4109
		ErrorCalendarCannotUseIdForOccurrenceId,
		// Token: 0x0400100E RID: 4110
		ErrorCalendarCannotUseIdForRecurringMasterId,
		// Token: 0x0400100F RID: 4111
		ErrorCalendarDurationIsTooLong,
		// Token: 0x04001010 RID: 4112
		ErrorCalendarEndDateIsEarlierThanStartDate,
		// Token: 0x04001011 RID: 4113
		ErrorCalendarFolderIsInvalidForCalendarView,
		// Token: 0x04001012 RID: 4114
		ErrorCalendarInvalidAttributeValue,
		// Token: 0x04001013 RID: 4115
		ErrorCalendarInvalidDayForTimeChangePattern,
		// Token: 0x04001014 RID: 4116
		ErrorCalendarInvalidDayForWeeklyRecurrence,
		// Token: 0x04001015 RID: 4117
		ErrorCalendarInvalidPropertyState,
		// Token: 0x04001016 RID: 4118
		ErrorCalendarInvalidPropertyValue,
		// Token: 0x04001017 RID: 4119
		ErrorCalendarInvalidRecurrence,
		// Token: 0x04001018 RID: 4120
		ErrorCalendarInvalidTimeZone,
		// Token: 0x04001019 RID: 4121
		ErrorCalendarIsCancelledForAccept,
		// Token: 0x0400101A RID: 4122
		ErrorCalendarIsCancelledForDecline,
		// Token: 0x0400101B RID: 4123
		ErrorCalendarIsCancelledForRemove,
		// Token: 0x0400101C RID: 4124
		ErrorCalendarIsCancelledForTentative,
		// Token: 0x0400101D RID: 4125
		ErrorCalendarIsDelegatedForAccept,
		// Token: 0x0400101E RID: 4126
		ErrorCalendarIsDelegatedForDecline,
		// Token: 0x0400101F RID: 4127
		ErrorCalendarIsDelegatedForRemove,
		// Token: 0x04001020 RID: 4128
		ErrorCalendarIsDelegatedForTentative,
		// Token: 0x04001021 RID: 4129
		ErrorCalendarIsNotOrganizer,
		// Token: 0x04001022 RID: 4130
		ErrorCalendarIsOrganizerForAccept,
		// Token: 0x04001023 RID: 4131
		ErrorCalendarIsOrganizerForDecline,
		// Token: 0x04001024 RID: 4132
		ErrorCalendarIsOrganizerForRemove,
		// Token: 0x04001025 RID: 4133
		ErrorCalendarIsOrganizerForTentative,
		// Token: 0x04001026 RID: 4134
		ErrorCalendarMeetingRequestIsOutOfDate,
		// Token: 0x04001027 RID: 4135
		ErrorCalendarOccurrenceIndexIsOutOfRecurrenceRange,
		// Token: 0x04001028 RID: 4136
		ErrorCalendarOccurrenceIsDeletedFromRecurrence,
		// Token: 0x04001029 RID: 4137
		ErrorCalendarOutOfRange,
		// Token: 0x0400102A RID: 4138
		ErrorCalendarViewRangeTooBig,
		// Token: 0x0400102B RID: 4139
		ErrorCallerIsInvalidADAccount,
		// Token: 0x0400102C RID: 4140
		ErrorCannotArchiveCalendarContactTaskFolderException,
		// Token: 0x0400102D RID: 4141
		ErrorCannotArchiveItemsInArchiveMailbox,
		// Token: 0x0400102E RID: 4142
		ErrorCannotArchiveItemsInPublicFolders,
		// Token: 0x0400102F RID: 4143
		ErrorCannotCreateCalendarItemInNonCalendarFolder,
		// Token: 0x04001030 RID: 4144
		ErrorCannotCreateContactInNonContactFolder,
		// Token: 0x04001031 RID: 4145
		ErrorCannotCreatePostItemInNonMailFolder,
		// Token: 0x04001032 RID: 4146
		ErrorCannotCreateTaskInNonTaskFolder,
		// Token: 0x04001033 RID: 4147
		ErrorCannotDeleteObject,
		// Token: 0x04001034 RID: 4148
		ErrorCannotDeleteTaskOccurrence,
		// Token: 0x04001035 RID: 4149
		ErrorCannotDisableMandatoryExtension,
		// Token: 0x04001036 RID: 4150
		ErrorCannotEmptyFolder,
		// Token: 0x04001037 RID: 4151
		ErrorCannotGetExternalEcpUrl,
		// Token: 0x04001038 RID: 4152
		ErrorCannotGetSourceFolderPath,
		// Token: 0x04001039 RID: 4153
		ErrorCannotOpenFileAttachment,
		// Token: 0x0400103A RID: 4154
		ErrorCannotSetCalendarPermissionOnNonCalendarFolder,
		// Token: 0x0400103B RID: 4155
		ErrorCannotSetNonCalendarPermissionOnCalendarFolder,
		// Token: 0x0400103C RID: 4156
		ErrorCannotSetPermissionUnknownEntries,
		// Token: 0x0400103D RID: 4157
		ErrorCannotSpecifySearchFolderAsSourceFolder,
		// Token: 0x0400103E RID: 4158
		ErrorCannotUseFolderIdForItemId,
		// Token: 0x0400103F RID: 4159
		ErrorCannotUseItemIdForFolderId,
		// Token: 0x04001040 RID: 4160
		ErrorChangeKeyRequired,
		// Token: 0x04001041 RID: 4161
		ErrorChangeKeyRequiredForWriteOperations,
		// Token: 0x04001042 RID: 4162
		ErrorClientDisconnected,
		// Token: 0x04001043 RID: 4163
		ErrorConnectionFailed,
		// Token: 0x04001044 RID: 4164
		ErrorContainsFilterWrongType,
		// Token: 0x04001045 RID: 4165
		ErrorContentConversionFailed,
		// Token: 0x04001046 RID: 4166
		ErrorCorruptData,
		// Token: 0x04001047 RID: 4167
		ErrorCreateItemAccessDenied,
		// Token: 0x04001048 RID: 4168
		ErrorCreateManagedFolderPartialCompletion,
		// Token: 0x04001049 RID: 4169
		ErrorCreateSubfolderAccessDenied,
		// Token: 0x0400104A RID: 4170
		ErrorCrossMailboxMoveCopy,
		// Token: 0x0400104B RID: 4171
		ErrorCrossSiteRequest,
		// Token: 0x0400104C RID: 4172
		ErrorDataSizeLimitExceeded,
		// Token: 0x0400104D RID: 4173
		ErrorDataSourceOperation,
		// Token: 0x0400104E RID: 4174
		ErrorDelegateAlreadyExists,
		// Token: 0x0400104F RID: 4175
		ErrorDelegateCannotAddOwner,
		// Token: 0x04001050 RID: 4176
		ErrorDelegateMissingConfiguration,
		// Token: 0x04001051 RID: 4177
		ErrorDelegateNoUser,
		// Token: 0x04001052 RID: 4178
		ErrorDelegateValidationFailed,
		// Token: 0x04001053 RID: 4179
		ErrorDeleteDistinguishedFolder,
		// Token: 0x04001054 RID: 4180
		ErrorDeleteItemsFailed,
		// Token: 0x04001055 RID: 4181
		ErrorDistinguishedUserNotSupported,
		// Token: 0x04001056 RID: 4182
		ErrorDistributionListMemberNotExist,
		// Token: 0x04001057 RID: 4183
		ErrorDuplicateInputFolderNames,
		// Token: 0x04001058 RID: 4184
		ErrorDuplicateLegacyDistinguishedName,
		// Token: 0x04001059 RID: 4185
		ErrorDuplicateSOAPHeader,
		// Token: 0x0400105A RID: 4186
		ErrorDuplicateUserIdsSpecified,
		// Token: 0x0400105B RID: 4187
		ErrorEmailAddressMismatch,
		// Token: 0x0400105C RID: 4188
		ErrorEventNotFound,
		// Token: 0x0400105D RID: 4189
		ErrorExceededConnectionCount,
		// Token: 0x0400105E RID: 4190
		ErrorExceededFindCountLimit,
		// Token: 0x0400105F RID: 4191
		ErrorExceededSubscriptionCount,
		// Token: 0x04001060 RID: 4192
		ErrorExpiredSubscription,
		// Token: 0x04001061 RID: 4193
		ErrorExtensionNotFound,
		// Token: 0x04001062 RID: 4194
		ErrorFolderCorrupt,
		// Token: 0x04001063 RID: 4195
		ErrorFolderExists,
		// Token: 0x04001064 RID: 4196
		ErrorFolderNotFound,
		// Token: 0x04001065 RID: 4197
		ErrorFolderPropertRequestFailed,
		// Token: 0x04001066 RID: 4198
		ErrorFolderSave,
		// Token: 0x04001067 RID: 4199
		ErrorFolderSaveFailed,
		// Token: 0x04001068 RID: 4200
		ErrorFolderSavePropertyError,
		// Token: 0x04001069 RID: 4201
		ErrorFreeBusyDLLimitReached,
		// Token: 0x0400106A RID: 4202
		ErrorFreeBusyGenerationFailed,
		// Token: 0x0400106B RID: 4203
		ErrorGetServerSecurityDescriptorFailed,
		// Token: 0x0400106C RID: 4204
		ErrorImContactLimitReached,
		// Token: 0x0400106D RID: 4205
		ErrorImGroupDisplayNameAlreadyExists,
		// Token: 0x0400106E RID: 4206
		ErrorImGroupLimitReached,
		// Token: 0x0400106F RID: 4207
		ErrorImpersonateUserDenied,
		// Token: 0x04001070 RID: 4208
		ErrorImpersonationDenied,
		// Token: 0x04001071 RID: 4209
		ErrorImpersonationFailed,
		// Token: 0x04001072 RID: 4210
		ErrorInboxRulesValidationError,
		// Token: 0x04001073 RID: 4211
		ErrorIncorrectSchemaVersion,
		// Token: 0x04001074 RID: 4212
		ErrorIncorrectUpdatePropertyCount,
		// Token: 0x04001075 RID: 4213
		ErrorIndividualMailboxLimitReached,
		// Token: 0x04001076 RID: 4214
		ErrorInsufficientResources,
		// Token: 0x04001077 RID: 4215
		ErrorInternalServerError,
		// Token: 0x04001078 RID: 4216
		ErrorInternalServerTransientError,
		// Token: 0x04001079 RID: 4217
		ErrorInvalidAccessLevel,
		// Token: 0x0400107A RID: 4218
		ErrorInvalidArgument,
		// Token: 0x0400107B RID: 4219
		ErrorInvalidAttachmentId,
		// Token: 0x0400107C RID: 4220
		ErrorInvalidAttachmentSubfilter,
		// Token: 0x0400107D RID: 4221
		ErrorInvalidAttachmentSubfilterTextFilter,
		// Token: 0x0400107E RID: 4222
		ErrorInvalidAuthorizationContext,
		// Token: 0x0400107F RID: 4223
		ErrorInvalidChangeKey,
		// Token: 0x04001080 RID: 4224
		ErrorInvalidClientSecurityContext,
		// Token: 0x04001081 RID: 4225
		ErrorInvalidCompleteDate,
		// Token: 0x04001082 RID: 4226
		ErrorInvalidContactEmailAddress,
		// Token: 0x04001083 RID: 4227
		ErrorInvalidContactEmailIndex,
		// Token: 0x04001084 RID: 4228
		ErrorInvalidCrossForestCredentials,
		// Token: 0x04001085 RID: 4229
		ErrorInvalidDelegatePermission,
		// Token: 0x04001086 RID: 4230
		ErrorInvalidDelegateUserId,
		// Token: 0x04001087 RID: 4231
		ErrorInvalidExchangeImpersonationHeaderData,
		// Token: 0x04001088 RID: 4232
		ErrorInvalidExcludesRestriction,
		// Token: 0x04001089 RID: 4233
		ErrorInvalidExpressionTypeForSubFilter,
		// Token: 0x0400108A RID: 4234
		ErrorInvalidExtendedProperty,
		// Token: 0x0400108B RID: 4235
		ErrorInvalidExtendedPropertyValue,
		// Token: 0x0400108C RID: 4236
		ErrorInvalidExternalSharingInitiator,
		// Token: 0x0400108D RID: 4237
		ErrorInvalidExternalSharingSubscriber,
		// Token: 0x0400108E RID: 4238
		ErrorInvalidFederatedOrganizationId,
		// Token: 0x0400108F RID: 4239
		ErrorInvalidFolderId,
		// Token: 0x04001090 RID: 4240
		ErrorInvalidFolderTypeForOperation,
		// Token: 0x04001091 RID: 4241
		ErrorInvalidFractionalPagingParameters,
		// Token: 0x04001092 RID: 4242
		ErrorInvalidFreeBusyViewType,
		// Token: 0x04001093 RID: 4243
		ErrorInvalidGetSharingFolderRequest,
		// Token: 0x04001094 RID: 4244
		ErrorInvalidId,
		// Token: 0x04001095 RID: 4245
		ErrorInvalidImContactId,
		// Token: 0x04001096 RID: 4246
		ErrorInvalidImDistributionGroupSmtpAddress,
		// Token: 0x04001097 RID: 4247
		ErrorInvalidImGroupId,
		// Token: 0x04001098 RID: 4248
		ErrorInvalidIdEmpty,
		// Token: 0x04001099 RID: 4249
		ErrorInvalidIdMalformed,
		// Token: 0x0400109A RID: 4250
		ErrorInvalidIdMalformedEwsLegacyIdFormat,
		// Token: 0x0400109B RID: 4251
		ErrorInvalidIdMonikerTooLong,
		// Token: 0x0400109C RID: 4252
		ErrorInvalidIdNotAnItemAttachmentId,
		// Token: 0x0400109D RID: 4253
		ErrorInvalidIdReturnedByResolveNames,
		// Token: 0x0400109E RID: 4254
		ErrorInvalidIdStoreObjectIdTooLong,
		// Token: 0x0400109F RID: 4255
		ErrorInvalidIdTooManyAttachmentLevels,
		// Token: 0x040010A0 RID: 4256
		ErrorInvalidIdXml,
		// Token: 0x040010A1 RID: 4257
		ErrorInvalidIndexedPagingParameters,
		// Token: 0x040010A2 RID: 4258
		ErrorInvalidInternetHeaderChildNodes,
		// Token: 0x040010A3 RID: 4259
		ErrorInvalidItemForOperationAcceptItem,
		// Token: 0x040010A4 RID: 4260
		ErrorInvalidItemForOperationArchiveItem,
		// Token: 0x040010A5 RID: 4261
		ErrorInvalidItemForOperationCancelItem,
		// Token: 0x040010A6 RID: 4262
		ErrorInvalidItemForOperationCreateItem,
		// Token: 0x040010A7 RID: 4263
		ErrorInvalidItemForOperationCreateItemAttachment,
		// Token: 0x040010A8 RID: 4264
		ErrorInvalidItemForOperationDeclineItem,
		// Token: 0x040010A9 RID: 4265
		ErrorInvalidItemForOperationExpandDL,
		// Token: 0x040010AA RID: 4266
		ErrorInvalidItemForOperationRemoveItem,
		// Token: 0x040010AB RID: 4267
		ErrorInvalidItemForOperationSendItem,
		// Token: 0x040010AC RID: 4268
		ErrorInvalidItemForOperationTentative,
		// Token: 0x040010AD RID: 4269
		ErrorInvalidLogonType,
		// Token: 0x040010AE RID: 4270
		ErrorInvalidMailbox,
		// Token: 0x040010AF RID: 4271
		ErrorInvalidManagedFolderProperty,
		// Token: 0x040010B0 RID: 4272
		ErrorInvalidManagedFolderQuota,
		// Token: 0x040010B1 RID: 4273
		ErrorInvalidManagedFolderSize,
		// Token: 0x040010B2 RID: 4274
		ErrorInvalidMergedFreeBusyInterval,
		// Token: 0x040010B3 RID: 4275
		ErrorInvalidNameForNameResolution,
		// Token: 0x040010B4 RID: 4276
		ErrorInvalidNetworkServiceContext,
		// Token: 0x040010B5 RID: 4277
		ErrorInvalidOofParameter,
		// Token: 0x040010B6 RID: 4278
		ErrorInvalidOperation,
		// Token: 0x040010B7 RID: 4279
		ErrorInvalidOrganizationRelationshipForFreeBusy,
		// Token: 0x040010B8 RID: 4280
		ErrorInvalidPagingMaxRows,
		// Token: 0x040010B9 RID: 4281
		ErrorInvalidParentFolder,
		// Token: 0x040010BA RID: 4282
		ErrorInvalidPercentCompleteValue,
		// Token: 0x040010BB RID: 4283
		ErrorInvalidPermissionSettings,
		// Token: 0x040010BC RID: 4284
		ErrorInvalidPhoneCallId,
		// Token: 0x040010BD RID: 4285
		ErrorInvalidPhoneNumber,
		// Token: 0x040010BE RID: 4286
		ErrorInvalidPropertyAppend,
		// Token: 0x040010BF RID: 4287
		ErrorInvalidPropertyDelete,
		// Token: 0x040010C0 RID: 4288
		ErrorInvalidPropertyForExists,
		// Token: 0x040010C1 RID: 4289
		ErrorInvalidPropertyForOperation,
		// Token: 0x040010C2 RID: 4290
		ErrorInvalidPropertyRequest,
		// Token: 0x040010C3 RID: 4291
		ErrorInvalidPropertySet,
		// Token: 0x040010C4 RID: 4292
		ErrorInvalidPropertyUpdateSentMessage,
		// Token: 0x040010C5 RID: 4293
		ErrorInvalidProxySecurityContext,
		// Token: 0x040010C6 RID: 4294
		ErrorInvalidPullSubscriptionId,
		// Token: 0x040010C7 RID: 4295
		ErrorInvalidPushSubscriptionUrl,
		// Token: 0x040010C8 RID: 4296
		ErrorInvalidRecipients,
		// Token: 0x040010C9 RID: 4297
		ErrorInvalidRecipientSubfilter,
		// Token: 0x040010CA RID: 4298
		ErrorInvalidRecipientSubfilterComparison,
		// Token: 0x040010CB RID: 4299
		ErrorInvalidRecipientSubfilterOrder,
		// Token: 0x040010CC RID: 4300
		ErrorInvalidRecipientSubfilterTextFilter,
		// Token: 0x040010CD RID: 4301
		ErrorInvalidReferenceItem,
		// Token: 0x040010CE RID: 4302
		ErrorInvalidRequest,
		// Token: 0x040010CF RID: 4303
		ErrorInvalidRestriction,
		// Token: 0x040010D0 RID: 4304
		ErrorInvalidRetentionTagTypeMismatch,
		// Token: 0x040010D1 RID: 4305
		ErrorInvalidRetentionTagInvisible,
		// Token: 0x040010D2 RID: 4306
		ErrorInvalidRetentionTagInheritance,
		// Token: 0x040010D3 RID: 4307
		ErrorInvalidRetentionTagIdGuid,
		// Token: 0x040010D4 RID: 4308
		ErrorInvalidRoutingType,
		// Token: 0x040010D5 RID: 4309
		ErrorInvalidScheduledOofDuration,
		// Token: 0x040010D6 RID: 4310
		ErrorInvalidSchemaVersionForMailboxVersion,
		// Token: 0x040010D7 RID: 4311
		ErrorInvalidSecurityDescriptor,
		// Token: 0x040010D8 RID: 4312
		ErrorInvalidSendItemSaveSettings,
		// Token: 0x040010D9 RID: 4313
		ErrorInvalidSerializedAccessToken,
		// Token: 0x040010DA RID: 4314
		ErrorInvalidServerVersion,
		// Token: 0x040010DB RID: 4315
		ErrorInvalidSharingData,
		// Token: 0x040010DC RID: 4316
		ErrorInvalidSharingMessage,
		// Token: 0x040010DD RID: 4317
		ErrorInvalidSid,
		// Token: 0x040010DE RID: 4318
		ErrorInvalidSIPUri,
		// Token: 0x040010DF RID: 4319
		ErrorInvalidSmtpAddress,
		// Token: 0x040010E0 RID: 4320
		ErrorInvalidSubfilterType,
		// Token: 0x040010E1 RID: 4321
		ErrorInvalidSubfilterTypeNotAttendeeType,
		// Token: 0x040010E2 RID: 4322
		ErrorInvalidSubfilterTypeNotRecipientType,
		// Token: 0x040010E3 RID: 4323
		ErrorInvalidSubscription,
		// Token: 0x040010E4 RID: 4324
		ErrorInvalidSubscriptionRequest,
		// Token: 0x040010E5 RID: 4325
		ErrorInvalidSyncStateData,
		// Token: 0x040010E6 RID: 4326
		ErrorInvalidTimeInterval,
		// Token: 0x040010E7 RID: 4327
		ErrorInvalidUserInfo,
		// Token: 0x040010E8 RID: 4328
		ErrorInvalidUserOofSettings,
		// Token: 0x040010E9 RID: 4329
		ErrorInvalidUserPrincipalName,
		// Token: 0x040010EA RID: 4330
		ErrorInvalidUserSid,
		// Token: 0x040010EB RID: 4331
		ErrorInvalidUserSidMissingUPN,
		// Token: 0x040010EC RID: 4332
		ErrorInvalidValueForProperty,
		// Token: 0x040010ED RID: 4333
		ErrorInvalidWatermark,
		// Token: 0x040010EE RID: 4334
		ErrorIPGatewayNotFound,
		// Token: 0x040010EF RID: 4335
		ErrorIrresolvableConflict,
		// Token: 0x040010F0 RID: 4336
		ErrorItemCorrupt,
		// Token: 0x040010F1 RID: 4337
		ErrorItemNotFound,
		// Token: 0x040010F2 RID: 4338
		ErrorItemPropertyRequestFailed,
		// Token: 0x040010F3 RID: 4339
		ErrorItemSave,
		// Token: 0x040010F4 RID: 4340
		ErrorItemSavePropertyError,
		// Token: 0x040010F5 RID: 4341
		ErrorLegacyMailboxFreeBusyViewTypeNotMerged,
		// Token: 0x040010F6 RID: 4342
		ErrorLocalServerObjectNotFound,
		// Token: 0x040010F7 RID: 4343
		ErrorLogonAsNetworkServiceFailed,
		// Token: 0x040010F8 RID: 4344
		ErrorMailboxConfiguration,
		// Token: 0x040010F9 RID: 4345
		ErrorMailboxDataArrayEmpty,
		// Token: 0x040010FA RID: 4346
		ErrorMailboxDataArrayTooBig,
		// Token: 0x040010FB RID: 4347
		ErrorMailboxFailover,
		// Token: 0x040010FC RID: 4348
		ErrorMailboxHoldNotFound,
		// Token: 0x040010FD RID: 4349
		ErrorMailboxLogonFailed,
		// Token: 0x040010FE RID: 4350
		ErrorMailboxMoveInProgress,
		// Token: 0x040010FF RID: 4351
		ErrorMailboxStoreUnavailable,
		// Token: 0x04001100 RID: 4352
		ErrorMailRecipientNotFound,
		// Token: 0x04001101 RID: 4353
		ErrorMailTipsDisabled,
		// Token: 0x04001102 RID: 4354
		ErrorManagedFolderAlreadyExists,
		// Token: 0x04001103 RID: 4355
		ErrorManagedFolderNotFound,
		// Token: 0x04001104 RID: 4356
		ErrorManagedFoldersRootFailure,
		// Token: 0x04001105 RID: 4357
		ErrorMeetingSuggestionGenerationFailed,
		// Token: 0x04001106 RID: 4358
		ErrorMessageDispositionRequired,
		// Token: 0x04001107 RID: 4359
		ErrorMessageSizeExceeded,
		// Token: 0x04001108 RID: 4360
		ErrorMessageTrackingNoSuchDomain,
		// Token: 0x04001109 RID: 4361
		ErrorMessageTrackingPermanentError,
		// Token: 0x0400110A RID: 4362
		ErrorMessageTrackingTransientError,
		// Token: 0x0400110B RID: 4363
		ErrorMimeContentConversionFailed,
		// Token: 0x0400110C RID: 4364
		ErrorMimeContentInvalid,
		// Token: 0x0400110D RID: 4365
		ErrorMimeContentInvalidBase64String,
		// Token: 0x0400110E RID: 4366
		ErrorMissedNotificationEvents,
		// Token: 0x0400110F RID: 4367
		ErrorMissingArgument,
		// Token: 0x04001110 RID: 4368
		ErrorMissingEmailAddress,
		// Token: 0x04001111 RID: 4369
		ErrorMissingEmailAddressForManagedFolder,
		// Token: 0x04001112 RID: 4370
		ErrorMissingInformationEmailAddress,
		// Token: 0x04001113 RID: 4371
		ErrorMissingInformationReferenceItemId,
		// Token: 0x04001114 RID: 4372
		ErrorMissingInformationSharingFolderId,
		// Token: 0x04001115 RID: 4373
		ErrorMissingItemForCreateItemAttachment,
		// Token: 0x04001116 RID: 4374
		ErrorMissingManagedFolderId,
		// Token: 0x04001117 RID: 4375
		ErrorMissingRecipients,
		// Token: 0x04001118 RID: 4376
		ErrorMissingUserIdInformation,
		// Token: 0x04001119 RID: 4377
		ErrorMoreThanOneAccessModeSpecified,
		// Token: 0x0400111A RID: 4378
		ErrorMoveCopyFailed,
		// Token: 0x0400111B RID: 4379
		ErrorMoveDistinguishedFolder,
		// Token: 0x0400111C RID: 4380
		ErrorMultiLegacyMailboxAccess,
		// Token: 0x0400111D RID: 4381
		ErrorNameResolutionMultipleResults,
		// Token: 0x0400111E RID: 4382
		ErrorNameResolutionNoMailbox,
		// Token: 0x0400111F RID: 4383
		ErrorNameResolutionNoResults,
		// Token: 0x04001120 RID: 4384
		ErrorNewEventStreamConnectionOpened,
		// Token: 0x04001121 RID: 4385
		ErrorNoApplicableProxyCASServersAvailable,
		// Token: 0x04001122 RID: 4386
		ErrorNoCalendar,
		// Token: 0x04001123 RID: 4387
		ErrorNoDestinationCASDueToKerberosRequirements,
		// Token: 0x04001124 RID: 4388
		ErrorNoDestinationCASDueToSSLRequirements,
		// Token: 0x04001125 RID: 4389
		ErrorNoDestinationCASDueToVersionMismatch,
		// Token: 0x04001126 RID: 4390
		ErrorNoFolderClassOverride,
		// Token: 0x04001127 RID: 4391
		ErrorNoFreeBusyAccess,
		// Token: 0x04001128 RID: 4392
		ErrorNonExistentMailbox,
		// Token: 0x04001129 RID: 4393
		ErrorNonPrimarySmtpAddress,
		// Token: 0x0400112A RID: 4394
		ErrorNoPropertyTagForCustomProperties,
		// Token: 0x0400112B RID: 4395
		ErrorNoPublicFolderReplicaAvailable,
		// Token: 0x0400112C RID: 4396
		ErrorNoPublicFolderServerAvailable,
		// Token: 0x0400112D RID: 4397
		ErrorNoRespondingCASInDestinationSite,
		// Token: 0x0400112E RID: 4398
		ErrorNotAllowedExternalSharingByPolicy,
		// Token: 0x0400112F RID: 4399
		ErrorNotDelegate,
		// Token: 0x04001130 RID: 4400
		ErrorNotEnoughMemory,
		// Token: 0x04001131 RID: 4401
		ErrorNotSupportedSharingMessage,
		// Token: 0x04001132 RID: 4402
		ErrorObjectTypeChanged,
		// Token: 0x04001133 RID: 4403
		ErrorOccurrenceCrossingBoundary,
		// Token: 0x04001134 RID: 4404
		ErrorOccurrenceTimeSpanTooBig,
		// Token: 0x04001135 RID: 4405
		ErrorOperationNotAllowedWithPublicFolderRoot,
		// Token: 0x04001136 RID: 4406
		ErrorOrganizationNotFederated,
		// Token: 0x04001137 RID: 4407
		ErrorOutlookRuleBlobExists,
		// Token: 0x04001138 RID: 4408
		ErrorParentFolderIdRequired,
		// Token: 0x04001139 RID: 4409
		ErrorParentFolderNotFound,
		// Token: 0x0400113A RID: 4410
		ErrorPasswordChangeRequired,
		// Token: 0x0400113B RID: 4411
		ErrorPasswordExpired,
		// Token: 0x0400113C RID: 4412
		ErrorPermissionNotAllowedByPolicy,
		// Token: 0x0400113D RID: 4413
		ErrorPhoneNumberNotDialable,
		// Token: 0x0400113E RID: 4414
		ErrorPropertyUpdate,
		// Token: 0x0400113F RID: 4415
		ErrorPropertyValidationFailure,
		// Token: 0x04001140 RID: 4416
		ErrorProxiedSubscriptionCallFailure,
		// Token: 0x04001141 RID: 4417
		ErrorProxyCallFailed,
		// Token: 0x04001142 RID: 4418
		ErrorProxyGroupSidLimitExceeded,
		// Token: 0x04001143 RID: 4419
		ErrorProxyRequestNotAllowed,
		// Token: 0x04001144 RID: 4420
		ErrorProxyRequestProcessingFailed,
		// Token: 0x04001145 RID: 4421
		ErrorProxyServiceDiscoveryFailed,
		// Token: 0x04001146 RID: 4422
		ErrorProxyTokenExpired,
		// Token: 0x04001147 RID: 4423
		ErrorPublicFolderRequestProcessingFailed,
		// Token: 0x04001148 RID: 4424
		ErrorPublicFolderServerNotFound,
		// Token: 0x04001149 RID: 4425
		ErrorQueryFilterTooLong,
		// Token: 0x0400114A RID: 4426
		ErrorQuotaExceeded,
		// Token: 0x0400114B RID: 4427
		ErrorReadEventsFailed,
		// Token: 0x0400114C RID: 4428
		ErrorReadReceiptNotPending,
		// Token: 0x0400114D RID: 4429
		ErrorRecurrenceEndDateTooBig,
		// Token: 0x0400114E RID: 4430
		ErrorRecurrenceHasNoOccurrence,
		// Token: 0x0400114F RID: 4431
		ErrorRemoveDelegatesFailed,
		// Token: 0x04001150 RID: 4432
		ErrorRequestAborted,
		// Token: 0x04001151 RID: 4433
		ErrorRequestStreamTooBig,
		// Token: 0x04001152 RID: 4434
		ErrorRequiredPropertyMissing,
		// Token: 0x04001153 RID: 4435
		ErrorResolveNamesInvalidFolderType,
		// Token: 0x04001154 RID: 4436
		ErrorResolveNamesOnlyOneContactsFolderAllowed,
		// Token: 0x04001155 RID: 4437
		ErrorResponseSchemaValidation,
		// Token: 0x04001156 RID: 4438
		ErrorRestrictionTooComplex,
		// Token: 0x04001157 RID: 4439
		ErrorRestrictionTooLong,
		// Token: 0x04001158 RID: 4440
		ErrorResultSetTooBig,
		// Token: 0x04001159 RID: 4441
		ErrorRulesOverQuota,
		// Token: 0x0400115A RID: 4442
		ErrorSavedItemFolderNotFound,
		// Token: 0x0400115B RID: 4443
		ErrorSchemaValidation,
		// Token: 0x0400115C RID: 4444
		ErrorSearchFolderNotInitialized,
		// Token: 0x0400115D RID: 4445
		ErrorSendAsDenied,
		// Token: 0x0400115E RID: 4446
		ErrorSendMeetingCancellationsRequired,
		// Token: 0x0400115F RID: 4447
		ErrorSendMeetingInvitationsOrCancellationsRequired,
		// Token: 0x04001160 RID: 4448
		ErrorSendMeetingInvitationsRequired,
		// Token: 0x04001161 RID: 4449
		ErrorSentMeetingRequestUpdate,
		// Token: 0x04001162 RID: 4450
		ErrorSentTaskRequestUpdate,
		// Token: 0x04001163 RID: 4451
		ErrorServerBusy,
		// Token: 0x04001164 RID: 4452
		ErrorServiceDiscoveryFailed,
		// Token: 0x04001165 RID: 4453
		ErrorSharingNoExternalEwsAvailable,
		// Token: 0x04001166 RID: 4454
		ErrorSharingSynchronizationFailed,
		// Token: 0x04001167 RID: 4455
		ErrorStaleObject,
		// Token: 0x04001168 RID: 4456
		ErrorSubmissionQuotaExceeded,
		// Token: 0x04001169 RID: 4457
		ErrorSubscriptionAccessDenied,
		// Token: 0x0400116A RID: 4458
		ErrorSubscriptionDelegateAccessNotSupported,
		// Token: 0x0400116B RID: 4459
		ErrorSubscriptionNotFound,
		// Token: 0x0400116C RID: 4460
		ErrorSubscriptionUnsubscribed,
		// Token: 0x0400116D RID: 4461
		ErrorSyncFolderNotFound,
		// Token: 0x0400116E RID: 4462
		ErrorTeamMailboxNotFound,
		// Token: 0x0400116F RID: 4463
		ErrorTeamMailboxNotLinkedToSharePoint,
		// Token: 0x04001170 RID: 4464
		ErrorTeamMailboxUrlValidationFailed,
		// Token: 0x04001171 RID: 4465
		ErrorTeamMailboxNotAuthorizedOwner,
		// Token: 0x04001172 RID: 4466
		ErrorTeamMailboxActiveToPendingDelete,
		// Token: 0x04001173 RID: 4467
		ErrorTeamMailboxFailedSendingNotifications,
		// Token: 0x04001174 RID: 4468
		ErrorTeamMailboxErrorUnknown,
		// Token: 0x04001175 RID: 4469
		ErrorTimeIntervalTooBig,
		// Token: 0x04001176 RID: 4470
		ErrorTimeoutExpired,
		// Token: 0x04001177 RID: 4471
		ErrorTimeZone,
		// Token: 0x04001178 RID: 4472
		ErrorToFolderNotFound,
		// Token: 0x04001179 RID: 4473
		ErrorTokenSerializationDenied,
		// Token: 0x0400117A RID: 4474
		ErrorUnableToGetUserOofSettings,
		// Token: 0x0400117B RID: 4475
		ErrorUnableToRemoveImContactFromGroup,
		// Token: 0x0400117C RID: 4476
		ErrorUnifiedMessagingDialPlanNotFound,
		// Token: 0x0400117D RID: 4477
		ErrorUnifiedMessagingRequestFailed,
		// Token: 0x0400117E RID: 4478
		ErrorUnifiedMessagingServerNotFound,
		// Token: 0x0400117F RID: 4479
		ErrorUnsupportedCulture,
		// Token: 0x04001180 RID: 4480
		ErrorUnsupportedMapiPropertyType,
		// Token: 0x04001181 RID: 4481
		ErrorUnsupportedMimeConversion,
		// Token: 0x04001182 RID: 4482
		ErrorUnsupportedPathForQuery,
		// Token: 0x04001183 RID: 4483
		ErrorUnsupportedPathForSortGroup,
		// Token: 0x04001184 RID: 4484
		ErrorUnsupportedPropertyDefinition,
		// Token: 0x04001185 RID: 4485
		ErrorUnsupportedQueryFilter,
		// Token: 0x04001186 RID: 4486
		ErrorUnsupportedRecurrence,
		// Token: 0x04001187 RID: 4487
		ErrorUnsupportedSubFilter,
		// Token: 0x04001188 RID: 4488
		ErrorUnsupportedTypeForConversion,
		// Token: 0x04001189 RID: 4489
		ErrorUpdateDelegatesFailed,
		// Token: 0x0400118A RID: 4490
		ErrorUpdatePropertyMismatch,
		// Token: 0x0400118B RID: 4491
		ErrorUserNotAllowedByPolicy,
		// Token: 0x0400118C RID: 4492
		ErrorUserNotUnifiedMessagingEnabled,
		// Token: 0x0400118D RID: 4493
		ErrorUserWithoutFederatedProxyAddress,
		// Token: 0x0400118E RID: 4494
		ErrorValueOutOfRange,
		// Token: 0x0400118F RID: 4495
		ErrorVirusDetected,
		// Token: 0x04001190 RID: 4496
		ErrorVirusMessageDeleted,
		// Token: 0x04001191 RID: 4497
		ErrorVoiceMailNotImplemented,
		// Token: 0x04001192 RID: 4498
		ErrorWebRequestInInvalidState,
		// Token: 0x04001193 RID: 4499
		ErrorWin32InteropError,
		// Token: 0x04001194 RID: 4500
		ErrorWorkingHoursSaveFailed,
		// Token: 0x04001195 RID: 4501
		ErrorWorkingHoursXmlMalformed,
		// Token: 0x04001196 RID: 4502
		ErrorWrongServerVersion,
		// Token: 0x04001197 RID: 4503
		ErrorWrongServerVersionDelegate,
		// Token: 0x04001198 RID: 4504
		ErrorInvalidClientAccessTokenRequest,
		// Token: 0x04001199 RID: 4505
		ErrorInvalidManagementRoleHeader,
		// Token: 0x0400119A RID: 4506
		ErrorSearchQueryHasTooManyKeywords,
		// Token: 0x0400119B RID: 4507
		ErrorSearchTooManyMailboxes,
		// Token: 0x0400119C RID: 4508
		ErrorInvalidRetentionTagNone,
		// Token: 0x0400119D RID: 4509
		ErrorDiscoverySearchesDisabled,
		// Token: 0x0400119E RID: 4510
		ErrorCalendarSeekToConditionNotSupported,
		// Token: 0x0400119F RID: 4511
		ErrorArchiveMailboxSearchFailed,
		// Token: 0x040011A0 RID: 4512
		ErrorGetRemoteArchiveFolderFailed,
		// Token: 0x040011A1 RID: 4513
		ErrorFindRemoteArchiveFolderFailed,
		// Token: 0x040011A2 RID: 4514
		ErrorGetRemoteArchiveItemFailed,
		// Token: 0x040011A3 RID: 4515
		ErrorExportRemoteArchiveItemsFailed,
		// Token: 0x040011A4 RID: 4516
		ErrorClientIntentInvalidStateDefinition,
		// Token: 0x040011A5 RID: 4517
		ErrorClientIntentNotFound,
		// Token: 0x040011A6 RID: 4518
		ErrorContentIndexingNotEnabled,
		// Token: 0x040011A7 RID: 4519
		ErrorDeleteUnifiedMessagingPromptFailed,
		// Token: 0x040011A8 RID: 4520
		ErrorLocationServicesDisabled,
		// Token: 0x040011A9 RID: 4521
		ErrorLocationServicesInvalidRequest,
		// Token: 0x040011AA RID: 4522
		ErrorLocationServicesRequestFailed,
		// Token: 0x040011AB RID: 4523
		ErrorLocationServicesRequestTimedOut,
		// Token: 0x040011AC RID: 4524
		ErrorWeatherServiceDisabled,
		// Token: 0x040011AD RID: 4525
		ErrorMailboxScopeNotAllowedWithoutQueryString,
		// Token: 0x040011AE RID: 4526
		ErrorNoSpeechDetected,
		// Token: 0x040011AF RID: 4527
		ErrorPromptPublishingOperationFailed,
		// Token: 0x040011B0 RID: 4528
		ErrorPublicFolderMailboxDiscoveryFailed,
		// Token: 0x040011B1 RID: 4529
		ErrorPublicFolderOperationFailed,
		// Token: 0x040011B2 RID: 4530
		ErrorPublicFolderSyncException,
		// Token: 0x040011B3 RID: 4531
		ErrorRecipientNotFound,
		// Token: 0x040011B4 RID: 4532
		ErrorRecognizerNotInstalled,
		// Token: 0x040011B5 RID: 4533
		ErrorSpeechGrammarError,
		// Token: 0x040011B6 RID: 4534
		ErrorTooManyObjectsOpened,
		// Token: 0x040011B7 RID: 4535
		ErrorUMServerUnavailable,
		// Token: 0x040011B8 RID: 4536
		ErrorUnifiedMessagingPromptNotFound,
		// Token: 0x040011B9 RID: 4537
		ErrorUnifiedMessagingReportDataNotFound,
		// Token: 0x040011BA RID: 4538
		ErrorInvalidPhotoSize,
		// Token: 0x040011BB RID: 4539
		ErrorCalendarIsGroupMailboxForAccept,
		// Token: 0x040011BC RID: 4540
		ErrorCalendarIsGroupMailboxForDecline,
		// Token: 0x040011BD RID: 4541
		ErrorCalendarIsGroupMailboxForTentative,
		// Token: 0x040011BE RID: 4542
		ErrorCalendarIsGroupMailboxForSuppressReadReceipt
	}
}
