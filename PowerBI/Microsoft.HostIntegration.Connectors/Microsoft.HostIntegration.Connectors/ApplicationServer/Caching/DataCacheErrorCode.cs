using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020003A1 RID: 929
	public static class DataCacheErrorCode
	{
		// Token: 0x04001397 RID: 5015
		internal const int DefaultIfNotSpecified = -1;

		// Token: 0x04001398 RID: 5016
		public const int CacheItemVersionMismatch = 1;

		// Token: 0x04001399 RID: 5017
		public const int RegistryKeyOpenFailure = 2;

		// Token: 0x0400139A RID: 5018
		public const int InvalidArgument = 3;

		// Token: 0x0400139B RID: 5019
		public const int UndefinedError = 4;

		// Token: 0x0400139C RID: 5020
		public const int RegionDoesNotExist = 5;

		// Token: 0x0400139D RID: 5021
		public const int KeyDoesNotExist = 6;

		// Token: 0x0400139E RID: 5022
		public const int RegionAlreadyExists = 7;

		// Token: 0x0400139F RID: 5023
		public const int KeyAlreadyExists = 8;

		// Token: 0x040013A0 RID: 5024
		public const int NamedCacheDoesNotExist = 9;

		// Token: 0x040013A1 RID: 5025
		public const int MaxNamedCacheCountExceeded = 10;

		// Token: 0x040013A2 RID: 5026
		public const int ObjectLocked = 11;

		// Token: 0x040013A3 RID: 5027
		public const int ObjectNotLocked = 12;

		// Token: 0x040013A4 RID: 5028
		public const int InvalidCacheLockHandle = 13;

		// Token: 0x040013A5 RID: 5029
		public const int InvalidEnumerator = 14;

		// Token: 0x040013A6 RID: 5030
		public const int NotificationInvalidationNotSupported = 15;

		// Token: 0x040013A7 RID: 5031
		public const int ConnectionTerminated = 16;

		// Token: 0x040013A8 RID: 5032
		public const int RetryLater = 17;

		// Token: 0x040013A9 RID: 5033
		public const int Timeout = 18;

		// Token: 0x040013AA RID: 5034
		public const int ClientServerVersionMismatch = 19;

		// Token: 0x040013AB RID: 5035
		public const int SerializationException = 20;

		// Token: 0x040013AC RID: 5036
		public const int ServerNull = 21;

		// Token: 0x040013AD RID: 5037
		public const int OperationNotSupported = 22;

		// Token: 0x040013AE RID: 5038
		internal const int CAOutofMemory = 23;

		// Token: 0x040013AF RID: 5039
		internal const int CAPreCallbackFailed = 24;

		// Token: 0x040013B0 RID: 5040
		internal const int VersionModified = 25;

		// Token: 0x040013B1 RID: 5041
		public const int ReadThroughProviderFailure = 26;

		// Token: 0x040013B2 RID: 5042
		public const int ReadThroughProviderDidNotReturnResult = 27;

		// Token: 0x040013B3 RID: 5043
		public const int ReadThroughProviderNotFound = 28;

		// Token: 0x040013B4 RID: 5044
		public const int AuthorizationTokenNotValid = 29;

		// Token: 0x040013B5 RID: 5045
		public const int AcsTokenRequestFailedAuthError = 30;

		// Token: 0x040013B6 RID: 5046
		public const int CacheDisabled = 31;

		// Token: 0x040013B7 RID: 5047
		public const int UsageResourceNotFound = 32;

		// Token: 0x040013B8 RID: 5048
		public const int CcrUnhandledException = 33;

		// Token: 0x040013B9 RID: 5049
		public const int ReadThroughRegionDoesNotExist = 34;

		// Token: 0x040013BA RID: 5050
		public const int CacheRedirected = 35;

		// Token: 0x040013BB RID: 5051
		public const int ConvertSimpleClient = 36;

		// Token: 0x040013BC RID: 5052
		public const int DuplicateServersSpecified = 37;

		// Token: 0x040013BD RID: 5053
		public const int StringTooLarge = 38;

		// Token: 0x040013BE RID: 5054
		public const int MessageLargerThanConfiguredSize = 39;

		// Token: 0x040013BF RID: 5055
		public const int UnsupportedOperationAttemptedOnPort = 40;

		// Token: 0x040013C0 RID: 5056
		public const int ChannelAuthenticationFailed = 41;

		// Token: 0x040013C1 RID: 5057
		public const int InvalidAutoDiscoverIdentifier = 42;

		// Token: 0x040013C2 RID: 5058
		internal const int DMDuplicateKey = 2001;

		// Token: 0x040013C3 RID: 5059
		internal const int DMKeyNotFound = 2002;

		// Token: 0x040013C4 RID: 5060
		internal const int DMVersionMismatchError = 2003;

		// Token: 0x040013C5 RID: 5061
		internal const int DMObjectLocked = 2004;

		// Token: 0x040013C6 RID: 5062
		internal const int DMIndexExists = 2005;

		// Token: 0x040013C7 RID: 5063
		internal const int DMInvalidLock = 2006;

		// Token: 0x040013C8 RID: 5064
		internal const int DMObjectNotLocked = 2007;

		// Token: 0x040013C9 RID: 5065
		internal const int DMInvalidEnumerator = 2008;

		// Token: 0x040013CA RID: 5066
		internal const int DMKeyLatched = 2009;

		// Token: 0x040013CB RID: 5067
		internal const int DMReadThroughObjectLocked = 2010;

		// Token: 0x040013CC RID: 5068
		internal const int DMReadThroughDuplicateKey = 2011;

		// Token: 0x040013CD RID: 5069
		internal const int DMReadThroughObjectNotLocked = 2012;

		// Token: 0x040013CE RID: 5070
		internal const int DMReadThroughInvalidLock = 2013;

		// Token: 0x040013CF RID: 5071
		internal const int OMFatalError = 3001;

		// Token: 0x040013D0 RID: 5072
		internal const int OMRegionNotFound = 3002;

		// Token: 0x040013D1 RID: 5073
		internal const int OMDuplicateRegion = 3003;

		// Token: 0x040013D2 RID: 5074
		internal const int OMExceedMaxNamedCacheCount = 3004;

		// Token: 0x040013D3 RID: 5075
		internal const int OMDuplicateNamedCache = 3005;

		// Token: 0x040013D4 RID: 5076
		internal const int OMNamedCacheNotFound = 3006;

		// Token: 0x040013D5 RID: 5077
		internal const int OMReadThroughRegionNotFound = 3007;

		// Token: 0x040013D6 RID: 5078
		internal const int DOMInitializationFailed = 4001;

		// Token: 0x040013D7 RID: 5079
		internal const int ServiceCrashed = 4002;

		// Token: 0x040013D8 RID: 5080
		internal const int ReplicationFailed = 4003;

		// Token: 0x040013D9 RID: 5081
		internal const int SSConfigParameterNull = 6001;

		// Token: 0x040013DA RID: 5082
		internal const int SSInvalidSessionState = 6002;

		// Token: 0x040013DB RID: 5083
		internal const int SSUnknownConfigurationParamater = 6003;

		// Token: 0x040013DC RID: 5084
		internal const int SSUnknownConfigurationError = 6004;

		// Token: 0x040013DD RID: 5085
		internal const int SSEmptyCacheName = 6005;

		// Token: 0x040013DE RID: 5086
		internal const int SSEmptyApplicationName = 6006;

		// Token: 0x040013DF RID: 5087
		internal const int RSLInitializationError = 7001;

		// Token: 0x040013E0 RID: 5088
		internal const int RSLFormatStringError = 7002;

		// Token: 0x040013E1 RID: 5089
		internal const int ClientConfigFileInvalidParameterValue = 8002;

		// Token: 0x040013E2 RID: 5090
		internal const int ClientConfigFileErrors = 8003;

		// Token: 0x040013E3 RID: 5091
		internal const int ConfigErrorDuringAccessingStore = 9001;

		// Token: 0x040013E4 RID: 5092
		internal const int ConfigFileInsufficientPermissions = 9002;

		// Token: 0x040013E5 RID: 5093
		internal const int ConfigFileDCacheSectionNotFound = 9003;

		// Token: 0x040013E6 RID: 5094
		internal const int ConfigFileNotFound = 9004;

		// Token: 0x040013E7 RID: 5095
		internal const int ClusterConfigInvalidParameterValue = 9005;

		// Token: 0x040013E8 RID: 5096
		internal const int CustomProviderLoadError = 9006;

		// Token: 0x040013E9 RID: 5097
		internal const int CustomProviderInstantiationFailed = 9007;

		// Token: 0x040013EA RID: 5098
		internal const int CustomProviderRegistrationFailed = 9008;

		// Token: 0x040013EB RID: 5099
		internal const int HighAvailabilityNotSupported = 9009;

		// Token: 0x040013EC RID: 5100
		internal const int SslCertificateNotSpecified = 9010;

		// Token: 0x040013ED RID: 5101
		internal const int InvalidCacheHostDataSize = 9011;

		// Token: 0x040013EE RID: 5102
		internal const int MissingOrIncorrectConfiguration = 9012;

		// Token: 0x040013EF RID: 5103
		internal const int MissingOrMultipleSslCertificates = 9013;

		// Token: 0x040013F0 RID: 5104
		public const int CacheAdminHostsRunning = 12001;

		// Token: 0x040013F1 RID: 5105
		public const int CacheAdminRequestTimeoutResultUnknown = 12002;

		// Token: 0x040013F2 RID: 5106
		public const int CacheAdminTimeout = 12003;

		// Token: 0x040013F3 RID: 5107
		public const int CacheAdminNullArgs = 12004;

		// Token: 0x040013F4 RID: 5108
		public const int CacheAdminUnknownError = 12005;

		// Token: 0x040013F5 RID: 5109
		public const int CacheAdminQuorumNotUp = 12006;

		// Token: 0x040013F6 RID: 5110
		public const int CacheAdminStoreAccessFailure = 12007;

		// Token: 0x040013F7 RID: 5111
		public const int CacheAdminHostsNotRunning = 12008;

		// Token: 0x040013F8 RID: 5112
		public const int CacheAdminCacheNotPresent = 12009;

		// Token: 0x040013F9 RID: 5113
		public const int CacheAdminHostNotPresent = 12010;

		// Token: 0x040013FA RID: 5114
		public const int CacheAdminCacheAlreadyPresent = 12011;

		// Token: 0x040013FB RID: 5115
		public const int CacheAdminRegionNotPresent = 12012;

		// Token: 0x040013FC RID: 5116
		public const int CacheAdminNoQuorumIfHostStopped = 12013;

		// Token: 0x040013FD RID: 5117
		public const int CacheAdminNoSeedNodes = 12014;

		// Token: 0x040013FE RID: 5118
		public const int CacheAdminHostRunning = 12015;

		// Token: 0x040013FF RID: 5119
		public const int CacheAdminHostNotRunning = 12016;

		// Token: 0x04001400 RID: 5120
		public const int CacheAdminConfigDeleteHostError = 12017;

		// Token: 0x04001401 RID: 5121
		public const int CacheAdminConfigAddHostError = 12018;

		// Token: 0x04001402 RID: 5122
		public const int CacheAdminDefaultCacheCreateFailure = 12019;

		// Token: 0x04001403 RID: 5123
		public const int CacheAdminDeleteInProgress = 12020;

		// Token: 0x04001404 RID: 5124
		public const int CacheAdminCacheCreationInconsistencyFailure = 12021;

		// Token: 0x04001405 RID: 5125
		public const int CacheAdminHostNameResolveFailure = 12022;

		// Token: 0x04001406 RID: 5126
		public const int CacheAdminNoHosts = 12023;

		// Token: 0x04001407 RID: 5127
		public const int CacheAdminClusterRefreshFailed = 12024;

		// Token: 0x04001408 RID: 5128
		public const int CacheAdminClusterTimeout = 12025;

		// Token: 0x04001409 RID: 5129
		public const int CacheAdminRemoteRegistryAccessFailed = 12026;

		// Token: 0x0400140A RID: 5130
		public const int CacheAdminWindowsAccountInvalid = 12027;

		// Token: 0x0400140B RID: 5131
		public const int CacheAdminWindowsAccountAlreadyPresent = 12028;

		// Token: 0x0400140C RID: 5132
		public const int CacheAdminWindowsAccountNotPresent = 12029;

		// Token: 0x0400140D RID: 5133
		public const int CacheAdminGrantClientAccountErrorFormat = 12030;

		// Token: 0x0400140E RID: 5134
		public const int CacheAdminRevokeClientAccountErrorFormat = 12031;

		// Token: 0x0400140F RID: 5135
		public const int CacheAdminInvalidOperation = 12032;

		// Token: 0x04001410 RID: 5136
		public const int CacheAdminClusterNotReady = 12033;

		// Token: 0x04001411 RID: 5137
		public const int CacheAdminHostRefreshFailed = 12034;

		// Token: 0x04001412 RID: 5138
		public const int CacheAdminShutdownInProgress = 12035;

		// Token: 0x04001413 RID: 5139
		public const int CacheAdminShutdownNotInProgress = 12036;

		// Token: 0x04001414 RID: 5140
		public const int CacheAdminCancelShutdownError = 12037;

		// Token: 0x04001415 RID: 5141
		public const int CacheAdminOperationNotSupported = 12038;

		// Token: 0x04001416 RID: 5142
		public const int CacheAdminHostNotReachable = 12039;

		// Token: 0x04001417 RID: 5143
		public const int CacheAdminHostsNotReachable = 12040;

		// Token: 0x04001418 RID: 5144
		public const int CacheAdminShutdownNotPossible = 12041;

		// Token: 0x04001419 RID: 5145
		public const int CacheAdminClusterSettingsReadError = 13001;

		// Token: 0x0400141A RID: 5146
		public const int CacheAdminInvalidClusterSettings = 13002;

		// Token: 0x0400141B RID: 5147
		public const int CacheAdminMaxCachesCreated = 13003;

		// Token: 0x0400141C RID: 5148
		public const int CacheAdminPortsDuplicated = 13004;

		// Token: 0x0400141D RID: 5149
		public const int CacheAdminClusterDown = 13005;

		// Token: 0x0400141E RID: 5150
		public const int CacheAdminStatsIncorrect = 13006;

		// Token: 0x0400141F RID: 5151
		public const int CacheAdminInvalidSecuritySettings = 13007;

		// Token: 0x04001420 RID: 5152
		public const int CacheAdminInstallPathReadError = 13008;

		// Token: 0x04001421 RID: 5153
		public const int CacheAdminOperationError = 13011;

		// Token: 0x04001422 RID: 5154
		public const int CacheAdminHostOperationError = 13012;

		// Token: 0x04001423 RID: 5155
		public const int CacheAdminClusterSettingsNotProvided = 13013;

		// Token: 0x04001424 RID: 5156
		public const int CacheAdminMinSecondariesOutOfBounds = 13014;

		// Token: 0x04001425 RID: 5157
		public const int CacheAdminNoValuesProvided = 13015;

		// Token: 0x04001426 RID: 5158
		public const int CacheAdminInvalidParameters = 13016;

		// Token: 0x04001427 RID: 5159
		public const int CacheAdminNullProviderSettingsKey = 13017;

		// Token: 0x04001428 RID: 5160
		public const int CacheAdminProviderTypeUnspecified = 13018;

		// Token: 0x04001429 RID: 5161
		public const int CacheAdminIncompleteParameters = 13019;

		// Token: 0x0400142A RID: 5162
		public const int CacheAdminInvalidCacheName = 13020;

		// Token: 0x0400142B RID: 5163
		public const int CacheAdminCacheConfigWriteError = 13021;

		// Token: 0x0400142C RID: 5164
		public const int CacheAdminHostConfigWriteError = 13022;

		// Token: 0x0400142D RID: 5165
		public const int CacheAdminInvalidParametersForHost = 13023;

		// Token: 0x0400142E RID: 5166
		public const int CacheAdminCommandNotAllowed = 13024;

		// Token: 0x0400142F RID: 5167
		public const int CacheAdminIncompatibleExpirationParameters = 13025;

		// Token: 0x04001430 RID: 5168
		public const int NetworkShareAsLocalPathError = 17001;

		// Token: 0x04001431 RID: 5169
		public const int NetworkShareFolderConnectionError = 17002;

		// Token: 0x04001432 RID: 5170
		public const int ClusterConfigReadError = 17003;

		// Token: 0x04001433 RID: 5171
		public const int ClusterConfigConnectionError = 17004;

		// Token: 0x04001434 RID: 5172
		public const int NewNetworkShareSetupError = 17005;

		// Token: 0x04001435 RID: 5173
		public const int ConnectionSettingsRegistrySaveError = 17006;

		// Token: 0x04001436 RID: 5174
		public const int InstallPathMissingError = 17007;

		// Token: 0x04001437 RID: 5175
		public const int HostAdditionFailureError = 17008;

		// Token: 0x04001438 RID: 5176
		public const int IncompleteConnectionParameters = 17009;

		// Token: 0x04001439 RID: 5177
		public const int PortAlreadyInUseError = 17010;

		// Token: 0x0400143A RID: 5178
		public const int ClusterAlreadyInitialized = 17011;

		// Token: 0x0400143B RID: 5179
		public const int ClusterNotInitialized = 17012;

		// Token: 0x0400143C RID: 5180
		public const int PermissionsError = 17013;

		// Token: 0x0400143D RID: 5181
		public const int HostDeletionFailureError = 17014;

		// Token: 0x0400143E RID: 5182
		public const int NonDomainBlockedAccount = 17015;

		// Token: 0x0400143F RID: 5183
		public const int ServiceAccessError = 17016;

		// Token: 0x04001440 RID: 5184
		public const int ServiceNotStopped = 17017;

		// Token: 0x04001441 RID: 5185
		public const int HostEntryNotFound = 17018;

		// Token: 0x04001442 RID: 5186
		public const int AdminAlreadyConfigured = 17020;

		// Token: 0x04001443 RID: 5187
		public const int ServiceAlreadyConfigured = 17021;

		// Token: 0x04001444 RID: 5188
		public const int AdminNotConfigured = 17022;

		// Token: 0x04001445 RID: 5189
		public const int ServiceNotConfigured = 17023;

		// Token: 0x04001446 RID: 5190
		public const int GetComputerDomainError = 17024;

		// Token: 0x04001447 RID: 5191
		public const int TestConnectionFailed = 17025;

		// Token: 0x04001448 RID: 5192
		public const int RegistryAccessFailed = 17026;

		// Token: 0x04001449 RID: 5193
		public const int ConfigurationStateSaveError = 17027;

		// Token: 0x0400144A RID: 5194
		public const int PortDuplicationError = 17028;

		// Token: 0x0400144B RID: 5195
		public const int NonDomainNWService = 17029;

		// Token: 0x0400144C RID: 5196
		public const int NetworkShareFilePermissionsError = 17031;

		// Token: 0x0400144D RID: 5197
		public const int SqlAuthenticationNotSupported = 17032;

		// Token: 0x0400144E RID: 5198
		public const int HostAlreadyPresent = 17033;

		// Token: 0x0400144F RID: 5199
		public const int DomainBlockedAccount = 17034;

		// Token: 0x04001450 RID: 5200
		public const int OffloadingWithXml = 17035;

		// Token: 0x04001451 RID: 5201
		public const int IncompatibleExpirationParameters = 17036;

		// Token: 0x04001452 RID: 5202
		internal const int MemoryPoolExhausted = 18001;

		// Token: 0x04001453 RID: 5203
		internal const int CouldNotAcquireAcsToken = 19001;

		// Token: 0x04001454 RID: 5204
		internal const int InvalidToken = 19002;

		// Token: 0x04001455 RID: 5205
		internal const int MalformedToken = 19003;

		// Token: 0x04001456 RID: 5206
		internal const int AccessDenied = 19004;

		// Token: 0x04001457 RID: 5207
		internal const int OverflowException = 20001;
	}
}
