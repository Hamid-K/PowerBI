using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000021 RID: 33
	internal enum ConfigurationProperty
	{
		// Token: 0x04000072 RID: 114
		None,
		// Token: 0x04000073 RID: 115
		CurrentSequence,
		// Token: 0x04000074 RID: 116
		MiscellaneousProperty,
		// Token: 0x04000075 RID: 117
		AuthenticationTypes,
		// Token: 0x04000076 RID: 118
		LogonMethod,
		// Token: 0x04000077 RID: 119
		AuthRealm,
		// Token: 0x04000078 RID: 120
		AuthDomain,
		// Token: 0x04000079 RID: 121
		AuthPersistence,
		// Token: 0x0400007A RID: 122
		MaxUnauthenticatedRequests,
		// Token: 0x0400007B RID: 123
		UnauthenticatedRequestWindow,
		// Token: 0x0400007C RID: 124
		UnauthenticatedRequestLockoutTime,
		// Token: 0x0400007D RID: 125
		LoginPageUrl,
		// Token: 0x0400007E RID: 126
		LoginPageUseSSL,
		// Token: 0x0400007F RID: 127
		PassthroughCookies,
		// Token: 0x04000080 RID: 128
		AuthTokenCacheMaxSize,
		// Token: 0x04000081 RID: 129
		AuthTokenCacheMaintenanceInterval,
		// Token: 0x04000082 RID: 130
		AuthTokenCacheLogonTimeout,
		// Token: 0x04000083 RID: 131
		AuthTokenCacheEntryTimeout,
		// Token: 0x04000084 RID: 132
		EnableConnectionKeepAlives,
		// Token: 0x04000085 RID: 133
		ConnectionKeepAliveTimeOut,
		// Token: 0x04000086 RID: 134
		Hostname,
		// Token: 0x04000087 RID: 135
		ReportManagerReportServerUrl,
		// Token: 0x04000088 RID: 136
		ReportManagerReportServerExternalUrl,
		// Token: 0x04000089 RID: 137
		MaxActiveReqForOneUser,
		// Token: 0x0400008A RID: 138
		MaxConcurrencyReportManager,
		// Token: 0x0400008B RID: 139
		MaxConcurrencyWebService,
		// Token: 0x0400008C RID: 140
		MaxConcurrencyUnattendedExecution,
		// Token: 0x0400008D RID: 141
		PhysicalPath,
		// Token: 0x0400008E RID: 142
		InstanceId,
		// Token: 0x0400008F RID: 143
		InstallationID,
		// Token: 0x04000090 RID: 144
		InstallationIDWebApp,
		// Token: 0x04000091 RID: 145
		Version,
		// Token: 0x04000092 RID: 146
		EditionId,
		// Token: 0x04000093 RID: 147
		EditionName,
		// Token: 0x04000094 RID: 148
		InstanceName,
		// Token: 0x04000095 RID: 149
		IsInitialized,
		// Token: 0x04000096 RID: 150
		AutoFlush,
		// Token: 0x04000097 RID: 151
		LogFileBufferSize,
		// Token: 0x04000098 RID: 152
		HttpLogFileFields,
		// Token: 0x04000099 RID: 153
		DaysToKeepLogs,
		// Token: 0x0400009A RID: 154
		AppDomainRecycleTime,
		// Token: 0x0400009B RID: 155
		MaxAppDomainUnloadTime,
		// Token: 0x0400009C RID: 156
		WorkingSetMax,
		// Token: 0x0400009D RID: 157
		WorkingSetMin,
		// Token: 0x0400009E RID: 158
		MemorySafetyMargin,
		// Token: 0x0400009F RID: 159
		MemoryThreshold,
		// Token: 0x040000A0 RID: 160
		CleanupCycleMinutes,
		// Token: 0x040000A1 RID: 161
		CleanupCycleMinuteOfDay,
		// Token: 0x040000A2 RID: 162
		AlertingCleanupCycleMinutes,
		// Token: 0x040000A3 RID: 163
		AlertingDataCleanupMinutes,
		// Token: 0x040000A4 RID: 164
		AlertingExecutionLogCleanupMinutes,
		// Token: 0x040000A5 RID: 165
		AlertingMaxDataRetentionDays,
		// Token: 0x040000A6 RID: 166
		RunningRequestsScavengerCycle,
		// Token: 0x040000A7 RID: 167
		RunningRequestsDbCycle,
		// Token: 0x040000A8 RID: 168
		RunningRequestsAge,
		// Token: 0x040000A9 RID: 169
		MaxScheduleWait,
		// Token: 0x040000AA RID: 170
		PollingInterval,
		// Token: 0x040000AB RID: 171
		DatabaseVersion,
		// Token: 0x040000AC RID: 172
		ExpectedDatabaseVersion,
		// Token: 0x040000AD RID: 173
		Dsn,
		// Token: 0x040000AE RID: 174
		ConnectionType,
		// Token: 0x040000AF RID: 175
		LogonUser,
		// Token: 0x040000B0 RID: 176
		LogonDomain,
		// Token: 0x040000B1 RID: 177
		LogonCred,
		// Token: 0x040000B2 RID: 178
		DatabaseLogonTimeout,
		// Token: 0x040000B3 RID: 179
		DatabaseQueryTimeout,
		// Token: 0x040000B4 RID: 180
		ProcessTimeout,
		// Token: 0x040000B5 RID: 181
		ProcessTimeoutGcExtension,
		// Token: 0x040000B6 RID: 182
		DatabaseCleanupTimeout,
		// Token: 0x040000B7 RID: 183
		DatabaseCleanupBatchFactor,
		// Token: 0x040000B8 RID: 184
		ConnectionTimeout,
		// Token: 0x040000B9 RID: 185
		MaxCatalogConnectionPoolSizePerProcess,
		// Token: 0x040000BA RID: 186
		SecureConnectionRequired,
		// Token: 0x040000BB RID: 187
		DisplayErrorLink,
		// Token: 0x040000BC RID: 188
		UrlRoot,
		// Token: 0x040000BD RID: 189
		EnableReportDesignClientButton,
		// Token: 0x040000BE RID: 190
		DefaultViewerStyleSheet,
		// Token: 0x040000BF RID: 191
		PageCountMode,
		// Token: 0x040000C0 RID: 192
		PostbackTimeout,
		// Token: 0x040000C1 RID: 193
		WebServiceUseFileShareStorage,
		// Token: 0x040000C2 RID: 194
		WindowsServiceUseFileShareStorage,
		// Token: 0x040000C3 RID: 195
		FileShareStorageLocation,
		// Token: 0x040000C4 RID: 196
		PolicyLevelServer,
		// Token: 0x040000C5 RID: 197
		PolicyLevelReportManager,
		// Token: 0x040000C6 RID: 198
		IsWebService,
		// Token: 0x040000C7 RID: 199
		IsSchedulingService,
		// Token: 0x040000C8 RID: 200
		IsNotificationService,
		// Token: 0x040000C9 RID: 201
		IsEventService,
		// Token: 0x040000CA RID: 202
		IsConfigurationService,
		// Token: 0x040000CB RID: 203
		IsRdceEnabled,
		// Token: 0x040000CC RID: 204
		UnattendedExecutionAccountDomain,
		// Token: 0x040000CD RID: 205
		UnattendedExecutionAccountUser,
		// Token: 0x040000CE RID: 206
		UnattendedExecutionAccountPassword,
		// Token: 0x040000CF RID: 207
		WindowsServiceIdentityActual,
		// Token: 0x040000D0 RID: 208
		WindowsServiceIdentityConfigured,
		// Token: 0x040000D1 RID: 209
		ServicePrincipalName,
		// Token: 0x040000D2 RID: 210
		WatsonFlags,
		// Token: 0x040000D3 RID: 211
		WatsonDumpOnExceptions,
		// Token: 0x040000D4 RID: 212
		WatsonDumpExcludeIfContainsExceptions,
		// Token: 0x040000D5 RID: 213
		Extensions,
		// Token: 0x040000D6 RID: 214
		UrlConfiguration,
		// Token: 0x040000D7 RID: 215
		WebServiceAccount,
		// Token: 0x040000D8 RID: 216
		IsReportBuilderAnonymousAccessEnabled,
		// Token: 0x040000D9 RID: 217
		RequestCacheSlots,
		// Token: 0x040000DA RID: 218
		RDLSandboxing,
		// Token: 0x040000DB RID: 219
		MapTileServerConfiguration,
		// Token: 0x040000DC RID: 220
		ExtendedProtectionLevel,
		// Token: 0x040000DD RID: 221
		ExtendedProtectionScenario,
		// Token: 0x040000DE RID: 222
		IsAlertingService,
		// Token: 0x040000DF RID: 223
		DisableSecureFormsAuthenticationCookie,
		// Token: 0x040000E0 RID: 224
		FileShareAccountDomain,
		// Token: 0x040000E1 RID: 225
		FileShareAccountUser,
		// Token: 0x040000E2 RID: 226
		FileShareAccountPassword,
		// Token: 0x040000E3 RID: 227
		EnablePowerBIFeatures,
		// Token: 0x040000E4 RID: 228
		PowerBIConnectionConfiguration,
		// Token: 0x040000E5 RID: 229
		UsernameSIDRefreshMinutes,
		// Token: 0x040000E6 RID: 230
		UpdatePoliciesSeconds,
		// Token: 0x040000E7 RID: 231
		UpdatePoliciesChunkSize,
		// Token: 0x040000E8 RID: 232
		OAuthConnectionConfiguration
	}
}
