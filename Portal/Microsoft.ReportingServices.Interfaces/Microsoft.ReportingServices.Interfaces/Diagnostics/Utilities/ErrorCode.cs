using System;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000002 RID: 2
	public enum ErrorCode
	{
		// Token: 0x04000002 RID: 2
		rsSuccess,
		// Token: 0x04000003 RID: 3
		rsMissingParameter,
		// Token: 0x04000004 RID: 4
		rsInvalidParameter,
		// Token: 0x04000005 RID: 5
		rsParameterTypeMismatch,
		// Token: 0x04000006 RID: 6
		rsInvalidParameterCombination,
		// Token: 0x04000007 RID: 7
		rsMalformedXml,
		// Token: 0x04000008 RID: 8
		rsInvalidXml,
		// Token: 0x04000009 RID: 9
		rsUnrecognizedXmlElement,
		// Token: 0x0400000A RID: 10
		rsMissingElement,
		// Token: 0x0400000B RID: 11
		rsInvalidElement,
		// Token: 0x0400000C RID: 12
		rsElementTypeMismatch,
		// Token: 0x0400000D RID: 13
		rsInvalidElementCombination,
		// Token: 0x0400000E RID: 14
		rsInvalidMultipleElementCombination,
		// Token: 0x0400000F RID: 15
		rsDataSourceNotFound,
		// Token: 0x04000010 RID: 16
		rsInternalDataSourceNotFound,
		// Token: 0x04000011 RID: 17
		rsCannotBuildExternalConnectionString,
		// Token: 0x04000012 RID: 18
		rsDataSourceNoPrompt,
		// Token: 0x04000013 RID: 19
		rsInvalidItemPath,
		// Token: 0x04000014 RID: 20
		rsItemPathLengthExceeded,
		// Token: 0x04000015 RID: 21
		rsInvalidItemName,
		// Token: 0x04000016 RID: 22
		rsItemNotFound,
		// Token: 0x04000017 RID: 23
		rsItemContentInvalid,
		// Token: 0x04000018 RID: 24
		rsWrongItemType,
		// Token: 0x04000019 RID: 25
		rsItemAlreadyExists,
		// Token: 0x0400001A RID: 26
		rsInvalidMove,
		// Token: 0x0400001B RID: 27
		rsInvalidDestination,
		// Token: 0x0400001C RID: 28
		rsReservedItem,
		// Token: 0x0400001D RID: 29
		rsProcessingError,
		// Token: 0x0400001E RID: 30
		rsReadOnlyProperty,
		// Token: 0x0400001F RID: 31
		rsStreamNotFound,
		// Token: 0x04000020 RID: 32
		rsMissingSessionId,
		// Token: 0x04000021 RID: 33
		rsExecutionNotFound,
		// Token: 0x04000022 RID: 34
		rsQueryExecutionNotAllowed,
		// Token: 0x04000023 RID: 35
		rsReportNotReady,
		// Token: 0x04000024 RID: 36
		rsReportSnapshotEnabled,
		// Token: 0x04000025 RID: 37
		rsReportSnapshotNotEnabled,
		// Token: 0x04000026 RID: 38
		rsSubreportFromSnapshot,
		// Token: 0x04000027 RID: 39
		rsOperationPreventsUnattendedExecution,
		// Token: 0x04000028 RID: 40
		rsInvalidReportLink,
		// Token: 0x04000029 RID: 41
		rsSnapshotVersionMismatch,
		// Token: 0x0400002A RID: 42
		rsSPSiteNotFound,
		// Token: 0x0400002B RID: 43
		rsFileSize,
		// Token: 0x0400002C RID: 44
		rsMaxCountComments,
		// Token: 0x0400002D RID: 45
		rsInvalidSearchOperator,
		// Token: 0x0400002E RID: 46
		rsDataSourceConnectStringProcessingError,
		// Token: 0x0400002F RID: 47
		rsQueryCommandTextProcessingError,
		// Token: 0x04000030 RID: 48
		rsReportParameterProcessingError,
		// Token: 0x04000031 RID: 49
		rsReportParameterQueryProcessingError,
		// Token: 0x04000032 RID: 50
		rsInvalidReportParameter,
		// Token: 0x04000033 RID: 51
		rsReadOnlyReportParameter,
		// Token: 0x04000034 RID: 52
		rsUnknownReportParameter,
		// Token: 0x04000035 RID: 53
		rsReportParameterValueNotSet,
		// Token: 0x04000036 RID: 54
		rsReportParameterTypeMismatch,
		// Token: 0x04000037 RID: 55
		rsQueryTimeout,
		// Token: 0x04000038 RID: 56
		rsReportHistoryNotFound,
		// Token: 0x04000039 RID: 57
		rsReportHistoryEnabled,
		// Token: 0x0400003A RID: 58
		rsSchedulerNotResponding,
		// Token: 0x0400003B RID: 59
		rsHasUserProfileDependencies,
		// Token: 0x0400003C RID: 60
		rsScheduleNotFound,
		// Token: 0x0400003D RID: 61
		rsScheduleAlreadyExists,
		// Token: 0x0400003E RID: 62
		rsScheduleDateTimeRangeException,
		// Token: 0x0400003F RID: 63
		rsDataSourceInUse,
		// Token: 0x04000040 RID: 64
		rsDataSourceDisabled,
		// Token: 0x04000041 RID: 65
		rsInvalidDataSourceReference,
		// Token: 0x04000042 RID: 66
		rsInvalidDataSourceCredentialSetting,
		// Token: 0x04000043 RID: 67
		rsDatasourceCredentialsNoLongerValid,
		// Token: 0x04000044 RID: 68
		rsWindowsIntegratedSecurityDisabled,
		// Token: 0x04000045 RID: 69
		rsInvalidIntegratedSecuritySettings,
		// Token: 0x04000046 RID: 70
		rsCannotActivateSubscription,
		// Token: 0x04000047 RID: 71
		rsSubscriptionNotFound,
		// Token: 0x04000048 RID: 72
		rsDeliveryExtensionNotFound,
		// Token: 0x04000049 RID: 73
		rsDeliveryError,
		// Token: 0x0400004A RID: 74
		rsCannotPrepareQuery,
		// Token: 0x0400004B RID: 75
		rsInvalidExtensionParameter,
		// Token: 0x0400004C RID: 76
		rsPBIServiceUnavailable,
		// Token: 0x0400004D RID: 77
		rsUnknownEventType,
		// Token: 0x0400004E RID: 78
		rsCannotSubscribeToEvent,
		// Token: 0x0400004F RID: 79
		rsEventExtensionNotFoundException,
		// Token: 0x04000050 RID: 80
		rsEventMaxRetryExceededException,
		// Token: 0x04000051 RID: 81
		rsReservedRole,
		// Token: 0x04000052 RID: 82
		rsTaskNotFound,
		// Token: 0x04000053 RID: 83
		rsMixedTasks,
		// Token: 0x04000054 RID: 84
		rsEmptyRole,
		// Token: 0x04000055 RID: 85
		rsInheritedPolicy,
		// Token: 0x04000056 RID: 86
		rsInvalidPolicyDefinition,
		// Token: 0x04000057 RID: 87
		rsRoleAlreadyExists,
		// Token: 0x04000058 RID: 88
		rsRoleNotFound,
		// Token: 0x04000059 RID: 89
		rsUnknownUserName,
		// Token: 0x0400005A RID: 90
		rsCannotDeleteRootPolicy,
		// Token: 0x0400005B RID: 91
		rsAccessDenied,
		// Token: 0x0400005C RID: 92
		rsAccessDeniedDueToRLSGroup,
		// Token: 0x0400005D RID: 93
		rsSecureConnectionRequired,
		// Token: 0x0400005E RID: 94
		rsAssemblyNotPermissioned,
		// Token: 0x0400005F RID: 95
		rsBatchNotFound,
		// Token: 0x04000060 RID: 96
		rsModelItemNotFound,
		// Token: 0x04000061 RID: 97
		rsModelRootPolicyRequired,
		// Token: 0x04000062 RID: 98
		rsModelingError,
		// Token: 0x04000063 RID: 99
		rsInvalidModelDrillthroughReport,
		// Token: 0x04000064 RID: 100
		rsModelIDMismatch,
		// Token: 0x04000065 RID: 101
		rsModelNotGenerated,
		// Token: 0x04000066 RID: 102
		rsModelGenerationNotSupported,
		// Token: 0x04000067 RID: 103
		rsModelGenerationError,
		// Token: 0x04000068 RID: 104
		rsSemanticQueryEngineError,
		// Token: 0x04000069 RID: 105
		rsSemanticQueryExtensionNotFound,
		// Token: 0x0400006A RID: 106
		rsXmlDataProviderError,
		// Token: 0x0400006B RID: 107
		rsReportServerDataProviderError,
		// Token: 0x0400006C RID: 108
		rsSPDataProviderError,
		// Token: 0x0400006D RID: 109
		rsNotSupported,
		// Token: 0x0400006E RID: 110
		rsNotEnabled,
		// Token: 0x0400006F RID: 111
		rsInternalError,
		// Token: 0x04000070 RID: 112
		rsRPCError,
		// Token: 0x04000071 RID: 113
		rsInvalidReportServerDatabase,
		// Token: 0x04000072 RID: 114
		rsReportServerDatabaseUnavailable,
		// Token: 0x04000073 RID: 115
		rsAccessDeniedToSecureData,
		// Token: 0x04000074 RID: 116
		rsReportTimeoutExpired,
		// Token: 0x04000075 RID: 117
		rsJobWasCanceled,
		// Token: 0x04000076 RID: 118
		rsReportServerDatabaseLogonFailed,
		// Token: 0x04000077 RID: 119
		rsEncryptedDataUnavailable,
		// Token: 0x04000078 RID: 120
		rsCannotValidateEncryptedData,
		// Token: 0x04000079 RID: 121
		rsReportServerKeyContainerError,
		// Token: 0x0400007A RID: 122
		rsRemotePublicKeyUnavailable,
		// Token: 0x0400007B RID: 123
		rsFailedToExportSymmetricKey,
		// Token: 0x0400007C RID: 124
		rsBackupKeyPasswordInvalid,
		// Token: 0x0400007D RID: 125
		rsReportServerDisabled,
		// Token: 0x0400007E RID: 126
		rsKeyStateNotValid,
		// Token: 0x0400007F RID: 127
		rsReportServerNotActivated,
		// Token: 0x04000080 RID: 128
		rsInvalidRSEditionConfiguration,
		// Token: 0x04000081 RID: 129
		rsReportServerServiceUnavailable,
		// Token: 0x04000082 RID: 130
		rsOperationNotSupported,
		// Token: 0x04000083 RID: 131
		rsEvaluationCopyExpired,
		// Token: 0x04000084 RID: 132
		rsServerBusy,
		// Token: 0x04000085 RID: 133
		rsServerConfigurationError,
		// Token: 0x04000086 RID: 134
		rsFailedToDecryptConfigInformation,
		// Token: 0x04000087 RID: 135
		rsEventLogSourceNotFound,
		// Token: 0x04000088 RID: 136
		rsWinAuthzError,
		// Token: 0x04000089 RID: 137
		rsWinAuthzError5,
		// Token: 0x0400008A RID: 138
		rsWinAuthzError1355,
		// Token: 0x0400008B RID: 139
		rsLogonFailed,
		// Token: 0x0400008C RID: 140
		rsRenderingExtensionNotFound,
		// Token: 0x0400008D RID: 141
		rsInvalidChart,
		// Token: 0x0400008E RID: 142
		rsErrorDuringChartRendering,
		// Token: 0x0400008F RID: 143
		rsOWCNotInstalled,
		// Token: 0x04000090 RID: 144
		rsUnsupportedURLProtocol,
		// Token: 0x04000091 RID: 145
		rsMalformattedURL,
		// Token: 0x04000092 RID: 146
		rsFilterEvaluationError,
		// Token: 0x04000093 RID: 147
		rsFilterFieldError,
		// Token: 0x04000094 RID: 148
		rsParameterError,
		// Token: 0x04000095 RID: 149
		rsParametersNotSpecified,
		// Token: 0x04000096 RID: 150
		rsStoredParameterNotFound,
		// Token: 0x04000097 RID: 151
		rsCredentialsNotSpecified,
		// Token: 0x04000098 RID: 152
		rsDataSourceTypeNull,
		// Token: 0x04000099 RID: 153
		rsDataExtensionNotFound,
		// Token: 0x0400009A RID: 154
		rsNoFieldDataAtIndex,
		// Token: 0x0400009B RID: 155
		rsErrorOpeningConnection,
		// Token: 0x0400009C RID: 156
		rsErrorImpersonatingUser,
		// Token: 0x0400009D RID: 157
		rsErrorImpersonatingServiceAccount,
		// Token: 0x0400009E RID: 158
		rsErrorImpersonateServiceAccountNotAllowed,
		// Token: 0x0400009F RID: 159
		rsDataExtensionWithoutConnectionExtension,
		// Token: 0x040000A0 RID: 160
		rsManagedDataProviderWithoutConnectionExtension,
		// Token: 0x040000A1 RID: 161
		rsErrorClosingConnection,
		// Token: 0x040000A2 RID: 162
		rsErrorRollbackTransaction,
		// Token: 0x040000A3 RID: 163
		rsErrorCommitTransaction,
		// Token: 0x040000A4 RID: 164
		rsErrorCreatingCommand,
		// Token: 0x040000A5 RID: 165
		rsErrorCreatingQueryParameter,
		// Token: 0x040000A6 RID: 166
		rsErrorAddingMultiValueQueryParameter,
		// Token: 0x040000A7 RID: 167
		rsErrorAddingQueryParameter,
		// Token: 0x040000A8 RID: 168
		rsErrorSettingCommandText,
		// Token: 0x040000A9 RID: 169
		rsErrorSettingCommandType,
		// Token: 0x040000AA RID: 170
		rsErrorSettingTransaction,
		// Token: 0x040000AB RID: 171
		rsErrorSettingQueryTimeout,
		// Token: 0x040000AC RID: 172
		rsErrorExecutingCommand,
		// Token: 0x040000AD RID: 173
		rsErrorCreatingDataReader,
		// Token: 0x040000AE RID: 174
		rsErrorReadingNextDataRow,
		// Token: 0x040000AF RID: 175
		rsErrorReadingDataField,
		// Token: 0x040000B0 RID: 176
		rsErrorReadingDataAggregationField,
		// Token: 0x040000B1 RID: 177
		rsProcessingAborted,
		// Token: 0x040000B2 RID: 178
		rsUnexpectedError,
		// Token: 0x040000B3 RID: 179
		rsInvalidSortItemID,
		// Token: 0x040000B4 RID: 180
		rrRenderingError,
		// Token: 0x040000B5 RID: 181
		rrInvalidPageNumber,
		// Token: 0x040000B6 RID: 182
		rrRenderStyleError,
		// Token: 0x040000B7 RID: 183
		rrRenderSectionInstanceError,
		// Token: 0x040000B8 RID: 184
		rrRenderBeforeNewPageError,
		// Token: 0x040000B9 RID: 185
		rrRenderAfterNewPageError,
		// Token: 0x040000BA RID: 186
		rrRenderResultNull,
		// Token: 0x040000BB RID: 187
		rrRenderStreamNull,
		// Token: 0x040000BC RID: 188
		rrRenderDeviceNull,
		// Token: 0x040000BD RID: 189
		rrRenderReportNull,
		// Token: 0x040000BE RID: 190
		rrRenderReportNameNull,
		// Token: 0x040000BF RID: 191
		rrRenderUnknownReportItem,
		// Token: 0x040000C0 RID: 192
		rrRenderStyleName,
		// Token: 0x040000C1 RID: 193
		rrRenderTextBox,
		// Token: 0x040000C2 RID: 194
		rrUnexpectedError,
		// Token: 0x040000C3 RID: 195
		rrControlInvalidTag,
		// Token: 0x040000C4 RID: 196
		rrPageNamespaceInvalid,
		// Token: 0x040000C5 RID: 197
		rrInvalidAttribute,
		// Token: 0x040000C6 RID: 198
		rrInvalidProperty,
		// Token: 0x040000C7 RID: 199
		rrInvalidStyleName,
		// Token: 0x040000C8 RID: 200
		rrInvalidControl,
		// Token: 0x040000C9 RID: 201
		rrInvalidDeviceInfo,
		// Token: 0x040000CA RID: 202
		rrReportNameNull,
		// Token: 0x040000CB RID: 203
		rrReportParamsNull,
		// Token: 0x040000CC RID: 204
		rrRendererParamsNull,
		// Token: 0x040000CD RID: 205
		rrMeasurementUnitError,
		// Token: 0x040000CE RID: 206
		rrInvalidOWCRequest,
		// Token: 0x040000CF RID: 207
		rrInvalidSize,
		// Token: 0x040000D0 RID: 208
		rrInvalidMeasurementUnit,
		// Token: 0x040000D1 RID: 209
		rrNegativeSize,
		// Token: 0x040000D2 RID: 210
		rrOutOfRange,
		// Token: 0x040000D3 RID: 211
		rrInvalidColor,
		// Token: 0x040000D4 RID: 212
		rrFailedToAllocateImageBitmap,
		// Token: 0x040000D5 RID: 213
		pvInternalError,
		// Token: 0x040000D6 RID: 214
		pvInvalidDefinition,
		// Token: 0x040000D7 RID: 215
		pvInvalidUrl,
		// Token: 0x040000D8 RID: 216
		rrInvalidBorderStyle,
		// Token: 0x040000D9 RID: 217
		rrInvalidStyleArgumentType,
		// Token: 0x040000DA RID: 218
		rrInvalidUniqueName,
		// Token: 0x040000DB RID: 219
		rrInvalidActionLabel,
		// Token: 0x040000DC RID: 220
		rrInvalidMimeType,
		// Token: 0x040000DD RID: 221
		rsCRIProcessingError,
		// Token: 0x040000DE RID: 222
		rsSharePointError,
		// Token: 0x040000DF RID: 223
		rsStoredCredentialsOutOfSync,
		// Token: 0x040000E0 RID: 224
		rsFileExtensionRequired,
		// Token: 0x040000E1 RID: 225
		rsFileExtensionViolation,
		// Token: 0x040000E2 RID: 226
		rsODCVersionNotSupported,
		// Token: 0x040000E3 RID: 227
		rsOperationNotSupportedSharePointMode,
		// Token: 0x040000E4 RID: 228
		rsOperationNotSupportedNativeMode,
		// Token: 0x040000E5 RID: 229
		rsContainerNotSupported,
		// Token: 0x040000E6 RID: 230
		rsSharePointObjectModelNotInstalled,
		// Token: 0x040000E7 RID: 231
		rsInvalidRSDSSchema,
		// Token: 0x040000E8 RID: 232
		rsPropertyDisabled,
		// Token: 0x040000E9 RID: 233
		rsPropertyDisabledNativeMode,
		// Token: 0x040000EA RID: 234
		rsSecurityZoneNotSupported,
		// Token: 0x040000EB RID: 235
		rsAppDomainManagerError,
		// Token: 0x040000EC RID: 236
		rsHttpRuntimeError,
		// Token: 0x040000ED RID: 237
		rsHttpRuntimeInternalError,
		// Token: 0x040000EE RID: 238
		rsInternalRepublishingFailed,
		// Token: 0x040000EF RID: 239
		rsHttpRuntimeClientDisconnectionError,
		// Token: 0x040000F0 RID: 240
		rsReportServerDatabaseError,
		// Token: 0x040000F1 RID: 241
		rsReportBuilderFileTransmissionError,
		// Token: 0x040000F2 RID: 242
		rsInternalResourceNotSpecifiedError,
		// Token: 0x040000F3 RID: 243
		rsInternalResourceNotFoundError,
		// Token: 0x040000F4 RID: 244
		rsAuthenticationExtensionError,
		// Token: 0x040000F5 RID: 245
		rsRdceExtraElementError,
		// Token: 0x040000F6 RID: 246
		rsRdceMismatchError,
		// Token: 0x040000F7 RID: 247
		rsRdceInvalidRdlError,
		// Token: 0x040000F8 RID: 248
		rsRdceInvalidConfigurationError,
		// Token: 0x040000F9 RID: 249
		rsRdceInvalidItemTypeError,
		// Token: 0x040000FA RID: 250
		rsRdceInvalidExecutionOptionError,
		// Token: 0x040000FB RID: 251
		rsRdceInvalidCacheOptionError,
		// Token: 0x040000FC RID: 252
		rsRdceWrappedException,
		// Token: 0x040000FD RID: 253
		rsRdceMismatchRdlVersion,
		// Token: 0x040000FE RID: 254
		rsInvalidOperation,
		// Token: 0x040000FF RID: 255
		rsInvalidSqlAgentJob,
		// Token: 0x04000100 RID: 256
		rsInvalidSubscription,
		// Token: 0x04000101 RID: 257
		rsAuthorizationExtensionError,
		// Token: 0x04000102 RID: 258
		rsDataCacheMismatch,
		// Token: 0x04000103 RID: 259
		rsSoapExtensionInvalidPreambleLengthError,
		// Token: 0x04000104 RID: 260
		rsSoapExtensionInvalidPreambleError,
		// Token: 0x04000105 RID: 261
		rsUrlRemapError,
		// Token: 0x04000106 RID: 262
		rsRequestThroughHttpRedirectorNotSupportedError,
		// Token: 0x04000107 RID: 263
		rsDataSetNotFound,
		// Token: 0x04000108 RID: 264
		rsComponentPublishingError,
		// Token: 0x04000109 RID: 265
		rsUnsupportedParameterForMode,
		// Token: 0x0400010A RID: 266
		rsMissingRequiredPropertyForItemType,
		// Token: 0x0400010B RID: 267
		rsInvalidSharedDataSetReference,
		// Token: 0x0400010C RID: 268
		rsDatasetParametersNotSpecified,
		// Token: 0x0400010D RID: 269
		rsCachingNotEnabled,
		// Token: 0x0400010E RID: 270
		rsInvalidDataSetReference,
		// Token: 0x0400010F RID: 271
		rsDataSetExecutionError,
		// Token: 0x04000110 RID: 272
		rsReadOnlyDataSetParameter,
		// Token: 0x04000111 RID: 273
		rsUnknownDataSetParameter,
		// Token: 0x04000112 RID: 274
		rsDataSetParameterValueNotSet,
		// Token: 0x04000113 RID: 275
		rsCacheRefreshPlanNotFound,
		// Token: 0x04000114 RID: 276
		rsUnhandledHttpApplicationError,
		// Token: 0x04000115 RID: 277
		rsUserCannotOwnSubscription,
		// Token: 0x04000116 RID: 278
		rsInvalidDataSourceCredentialSettingForITokenDataExtension,
		// Token: 0x04000117 RID: 279
		rsInvalidCatalogRecord,
		// Token: 0x04000118 RID: 280
		rsUnknownFeedColumnType,
		// Token: 0x04000119 RID: 281
		rsFeedValueOutOfRange,
		// Token: 0x0400011A RID: 282
		rsMissingFeedColumnException,
		// Token: 0x0400011B RID: 283
		rsFeedColumnTypeMismatchException,
		// Token: 0x0400011C RID: 284
		rsSessionNotAvailable,
		// Token: 0x0400011D RID: 285
		rsFeedNotAvailable,
		// Token: 0x0400011E RID: 286
		rsAlertDefinitionRuleNotValid,
		// Token: 0x0400011F RID: 287
		rsAlertDefinitionNotAvailable,
		// Token: 0x04000120 RID: 288
		rsDataFeedNotValid,
		// Token: 0x04000121 RID: 289
		rsInvalidProgressiveFormatError,
		// Token: 0x04000122 RID: 290
		rsProgressiveFormatElementMissingError,
		// Token: 0x04000123 RID: 291
		rsProgressiveMessageWriteError,
		// Token: 0x04000124 RID: 292
		rsClaimsToWindowsTokenError,
		// Token: 0x04000125 RID: 293
		rsClaimsToWindowsTokenLoginTypeError,
		// Token: 0x04000126 RID: 294
		rsInvalidDataSourceType,
		// Token: 0x04000127 RID: 295
		rsErrorSettingStartAt,
		// Token: 0x04000128 RID: 296
		rsCannotRetrieveModel,
		// Token: 0x04000129 RID: 297
		rsModelRetrievalCanceled,
		// Token: 0x0400012A RID: 298
		rsInvalidReportArchiveFormat,
		// Token: 0x0400012B RID: 299
		rsErrorCancelingCommand,
		// Token: 0x0400012C RID: 300
		rsErrorDisposingDataReader,
		// Token: 0x0400012D RID: 301
		rsAdomdDataProviderError,
		// Token: 0x0400012E RID: 302
		rsProcessingAbortedByTransientError,
		// Token: 0x0400012F RID: 303
		rsIncompatibleRIFVersion,
		// Token: 0x04000130 RID: 304
		rsInvalidDataExtension,
		// Token: 0x04000131 RID: 305
		rsMissingFieldInStartAt,
		// Token: 0x04000132 RID: 306
		rsInvalidDataSourceCount,
		// Token: 0x04000133 RID: 307
		rsSecureStoreContextUrlNotSpecified,
		// Token: 0x04000134 RID: 308
		rsSecureStoreInvalidLookupContext,
		// Token: 0x04000135 RID: 309
		rsSecureStoreCannotRetrieveCredentials,
		// Token: 0x04000136 RID: 310
		rsSecureStoreMissingCredentialFields,
		// Token: 0x04000137 RID: 311
		rsSecureStoreAmbiguousCredentialFields,
		// Token: 0x04000138 RID: 312
		rsSecureStoreUnsupportedCredentialField,
		// Token: 0x04000139 RID: 313
		rsSecureStoreUnsupportedSharePointVersion,
		// Token: 0x0400013A RID: 314
		rsInvalidPerspectiveAndVersion,
		// Token: 0x0400013B RID: 315
		rsUnsupportedMetadataVersionRequested,
		// Token: 0x0400013C RID: 316
		rsModelRetrievalAborted,
		// Token: 0x0400013D RID: 317
		rsVersionMismatch,
		// Token: 0x0400013E RID: 318
		rsClosingRegisteredStreamException,
		// Token: 0x0400013F RID: 319
		rsDataShapeQueryTranslationError,
		// Token: 0x04000140 RID: 320
		rsInvalidSessionId,
		// Token: 0x04000141 RID: 321
		rsInvalidConcurrentRenderEditSessionRequest,
		// Token: 0x04000142 RID: 322
		rsSessionNotFound,
		// Token: 0x04000143 RID: 323
		rsInvalidSessionCatalogItems,
		// Token: 0x04000144 RID: 324
		rsApiVersionNotRecognized,
		// Token: 0x04000145 RID: 325
		rsApiVersionDiscontinued,
		// Token: 0x04000146 RID: 326
		rsReportSerializationError,
		// Token: 0x04000147 RID: 327
		rsSessionOutOfSync,
		// Token: 0x04000148 RID: 328
		rsSessionFailedOver,
		// Token: 0x04000149 RID: 329
		rsRequestEncodingFormatException,
		// Token: 0x0400014A RID: 330
		rsCertificateMissingOrInvalid,
		// Token: 0x0400014B RID: 331
		rsResolutionFailureException,
		// Token: 0x0400014C RID: 332
		rsQueryMemoryLimitExceeded,
		// Token: 0x0400014D RID: 333
		rsQueryTimeoutExceeded,
		// Token: 0x0400014E RID: 334
		rsDataShapeQueryGenerationError,
		// Token: 0x0400014F RID: 335
		rsStreamOperationFailed,
		// Token: 0x04000150 RID: 336
		rsReportServerStorageSingleRefreshConnectionExpected,
		// Token: 0x04000151 RID: 337
		rsReportServerStorageRefreshConnectionNotValidated,
		// Token: 0x04000152 RID: 338
		rsOnPremConnectionBuilderUnknownError,
		// Token: 0x04000153 RID: 339
		rsOnPremConnectionBuilderConnectionStringMissing,
		// Token: 0x04000154 RID: 340
		rsOnPremConnectionBuilderMissingEffectiveUsername,
		// Token: 0x04000155 RID: 341
		rsIdentityClaimsMissingOrInvalid,
		// Token: 0x04000156 RID: 342
		rsSystemResourcePackageMetadataNotFound,
		// Token: 0x04000157 RID: 343
		rsSystemResourcePackageMetadataValidationFailure,
		// Token: 0x04000158 RID: 344
		rsSystemResourcePackageReferencedItemMissing,
		// Token: 0x04000159 RID: 345
		rsSystemResourcePackageRequiredItemMissing,
		// Token: 0x0400015A RID: 346
		rsSystemResourcePackageCannotValidateItemContentType,
		// Token: 0x0400015B RID: 347
		rsSystemResourcePackageCannotValidateItemExtension,
		// Token: 0x0400015C RID: 348
		rsSystemResourcePackageValidationFailed,
		// Token: 0x0400015D RID: 349
		rsSystemResourcePackageWrongType,
		// Token: 0x0400015E RID: 350
		rsAuthorizationTokenInvalidOrExpired,
		// Token: 0x0400015F RID: 351
		rsAuthorizationHeaderNotFound
	}
}
