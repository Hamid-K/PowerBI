using System;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000F9 RID: 249
	internal class ConfigManager
	{
		// Token: 0x060006D6 RID: 1750 RVA: 0x0001ACF4 File Offset: 0x00018EF4
		static ConfigManager()
		{
			ConfigManager.InitCodeVersion();
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x0001B702 File Offset: 0x00019902
		internal static string ValidatePath(string path)
		{
			if (path == null || path.Trim().Length == 0)
			{
				Provider.IsEnabled(TraceLevel.Info);
				path = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
				Provider.IsEnabled(TraceLevel.Info);
			}
			return path;
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x0001B734 File Offset: 0x00019934
		internal static void InitializeTimeouts(int masterTimeout)
		{
			int num = 2000;
			ConfigManager.INITIAL_PING_DELAY = masterTimeout / 60;
			if (ConfigManager.INITIAL_PING_DELAY < num)
			{
				ConfigManager.INITIAL_PING_DELAY = num;
			}
			int num2 = 600000;
			ConfigManager.MAX_PING_DELAY = masterTimeout * 3;
			if (ConfigManager.MAX_PING_DELAY < num2)
			{
				ConfigManager.MAX_PING_DELAY = num2;
			}
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x0001B77A File Offset: 0x0001997A
		private static string GetString(string str)
		{
			return GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, str);
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x0001B787 File Offset: 0x00019987
		private static void InitCodeVersion()
		{
			ConfigManager.CodeVersion = ConfigManager.GetCodeVersion();
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x0001B793 File Offset: 0x00019993
		private static long GetCodeVersion()
		{
			return 1002L;
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x0001B79B File Offset: 0x0001999B
		internal static bool IsStoreVersionHigherThan2000(Version storeVersion)
		{
			return storeVersion == null || storeVersion.Major > ConfigManager.StoreVersion2000.Major;
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x0001B7BA File Offset: 0x000199BA
		internal static bool IsStoreVersionHigherThan3000(Version storeVersion)
		{
			return storeVersion == null || storeVersion.Major > ConfigManager.StoreVersion3000.Major;
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0001B7DC File Offset: 0x000199DC
		public static string GetLogFileName(string moduleName)
		{
			string text = "DCache" + moduleName + "{0}[{1}]";
			string text2 = "yyyy-MM-dd-HHmmss";
			return string.Format(CultureInfo.InvariantCulture, text, new object[]
			{
				DateTime.Now.ToString(text2, CultureInfo.InvariantCulture),
				Process.GetCurrentProcess().Id
			});
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x0001B83C File Offset: 0x00019A3C
		public static string GetProviderName(string provider)
		{
			if (provider.Equals("xml", StringComparison.OrdinalIgnoreCase))
			{
				return "xml";
			}
			if (provider.Equals("System.Data.SqlClient", StringComparison.OrdinalIgnoreCase))
			{
				return "System.Data.SqlClient";
			}
			if (provider.Equals("SqlAzure", StringComparison.OrdinalIgnoreCase))
			{
				return "SqlAzure";
			}
			if (provider.Equals("WindowsAzureBlobProvider", StringComparison.OrdinalIgnoreCase))
			{
				return "WindowsAzureBlobProvider";
			}
			return provider;
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x0001B89C File Offset: 0x00019A9C
		public static int GetPartitionCount(string clusterSize)
		{
			int num = 0;
			if (clusterSize.Equals("small", StringComparison.OrdinalIgnoreCase))
			{
				num = 32;
			}
			else if (clusterSize.Equals("medium", StringComparison.OrdinalIgnoreCase))
			{
				num = 256;
			}
			else if (clusterSize.Equals("large", StringComparison.OrdinalIgnoreCase))
			{
				num = 1024;
			}
			else
			{
				ConfigFile.ThrowException(9005, "size", clusterSize, string.Format(CultureInfo.CurrentCulture, "{0},{1},{2}", new object[] { "small", "medium", "large" }));
			}
			return num;
		}

		// Token: 0x04000452 RID: 1106
		public const string CONFIGURATION_MANAGER = "CONFIGURATION_MANAGER";

		// Token: 0x04000453 RID: 1107
		public const string LOG_FILE_NAME = "DCacheTrace[$]/dd-HH";

		// Token: 0x04000454 RID: 1108
		public const string PERFORMANCE_COUNTER_MANIFEST_FILE = "Microsoft.ApplicationServer.Caching.PerformanceCounter.man";

		// Token: 0x04000455 RID: 1109
		public const string PROVIDER_FILE = "Microsoft.ApplicationServer.Caching.EventDefinitions.dll";

		// Token: 0x04000456 RID: 1110
		public const string PERFORMANCE_COUNTER_PROVIDER_GUID = "{6B09CE88-F32A-40ed-9AF8-26BB292DFB3F}";

		// Token: 0x04000457 RID: 1111
		public const string PERFORMANCE_COUNTER_COUNTER_SET_HOST_GUID = "{64c8448e-6a84-429a-95e5-6abeccb67f53}";

		// Token: 0x04000458 RID: 1112
		public const string PERFORMANCE_COUNTER_COUNTER_SET_CACHE_GUID = "{86bc60eb-5e2f-4002-97a1-dc0f0209ee03}";

		// Token: 0x04000459 RID: 1113
		public const string PERFORMANCE_COUNTER_COUNTER_SET_SECONDARY_HOST_GUID = "{d2c8bc17-4226-4886-a39c-6eb9459b52b8}";

		// Token: 0x0400045A RID: 1114
		public const int NUMBER_OF_RETRY_FOR_SLOT = 1000;

		// Token: 0x0400045B RID: 1115
		public const int MAXIMUM_CHANNELS_ALLOWED = 100;

		// Token: 0x0400045C RID: 1116
		public const string DEFAULT_CLIENT_NAME = "default";

		// Token: 0x0400045D RID: 1117
		public const string SYSTEM_CACHE = "__system";

		// Token: 0x0400045E RID: 1118
		public const int DelayedRequestThreshold = 1000;

		// Token: 0x0400045F RID: 1119
		public const string VASFabricConfigurationFile = "Microsoft.Cloud.AzureRoleHost.dll.config";

		// Token: 0x04000460 RID: 1120
		public const string SERVICE_STOP_TIMEOUT_REGISTRY = "HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Control";

		// Token: 0x04000461 RID: 1121
		public const string SERVICE_STOP_TIMEOUT_KEY = "WaitToKillServiceTimeout";

		// Token: 0x04000462 RID: 1122
		public const long SERVER_MAX_BUFFER_POOL_SIZE = 268435456L;

		// Token: 0x04000463 RID: 1123
		public const int SERVER_MAX_BUFFER_SIZE = 8388608;

		// Token: 0x04000464 RID: 1124
		public const int SERVER_MAX_PENDING_ACCEPTS = 5;

		// Token: 0x04000465 RID: 1125
		public const int SERVER_LISTEN_BACKLOG = 100;

		// Token: 0x04000466 RID: 1126
		public const int SERVER_MAX_PENDING_CONNECTIONS = 10;

		// Token: 0x04000467 RID: 1127
		public const string Authorization = "Authorization";

		// Token: 0x04000468 RID: 1128
		public const string SmartRouting = "SmartRouting";

		// Token: 0x04000469 RID: 1129
		public const string DesiredDestinationHint = "DHint";

		// Token: 0x0400046A RID: 1130
		public const string HeaderNampeSpace = "urn:AppFabricCaching";

		// Token: 0x0400046B RID: 1131
		internal const string To = "To";

		// Token: 0x0400046C RID: 1132
		internal const string VipForDip = "VipForDip";

		// Token: 0x0400046D RID: 1133
		internal const string Tracker = "Tracker";

		// Token: 0x0400046E RID: 1134
		internal const string GatewayId = "GatewayId";

		// Token: 0x0400046F RID: 1135
		internal const string PrimaryId = "PrimaryId";

		// Token: 0x04000470 RID: 1136
		internal const string GatewayDisplayFriendlyId = "GatewayDisplayFriendlyId";

		// Token: 0x04000471 RID: 1137
		internal const string PrimaryDisplayFriendlyId = "PrimaryDisplayFriendlyId";

		// Token: 0x04000472 RID: 1138
		internal const string PrimaryTracker = "PrimaryTracker";

		// Token: 0x04000473 RID: 1139
		internal const string TrackingId = "TrackingId";

		// Token: 0x04000474 RID: 1140
		internal const string CacheConnectionProperty = "CacheConnectionProperty";

		// Token: 0x04000475 RID: 1141
		internal const string RemoteEndpoint = "RemoteEndpoint";

		// Token: 0x04000476 RID: 1142
		public const int REGN_ROOTDIRBITMASKSIZE = 4;

		// Token: 0x04000477 RID: 1143
		public const int REGN_SUBDIRBITMASKSIZE = 4;

		// Token: 0x04000478 RID: 1144
		public const int CND_ROOTDIRBITMASKSIZE = 4;

		// Token: 0x04000479 RID: 1145
		public const int CND_SUBDIRBITMASKSIZE = 4;

		// Token: 0x0400047A RID: 1146
		public const int CND_MAXNAMEDCACHECOUNT = 128;

		// Token: 0x0400047B RID: 1147
		public const int MAX_NAMED_CACHE_NAME_LENGTH = 255;

		// Token: 0x0400047C RID: 1148
		public const int EVICTION_INTERVAL = 180;

		// Token: 0x0400047D RID: 1149
		public const int NUM_EXPIRY_RUNS_TO_TRIGGER_COMPACT = 5;

		// Token: 0x0400047E RID: 1150
		public const int CACHELINE_BUFFER = 3;

		// Token: 0x0400047F RID: 1151
		public const double SEED_NODE_JOIN_TIMEOUT = 16.0;

		// Token: 0x04000480 RID: 1152
		public const double SEED_NODE_JOIN_TIMEOUT_FOR_VAS = 8.0;

		// Token: 0x04000481 RID: 1153
		public const int EPOCH_BYTE_COUNT = 3;

		// Token: 0x04000482 RID: 1154
		public const int DATA_LOSS_EPOCH_FLAG = 16777215;

		// Token: 0x04000483 RID: 1155
		public const int STATIC_ENUMERATOR_REGION_NAME = 100;

		// Token: 0x04000484 RID: 1156
		public const int MAX_SLOT_LATCH_RETRIES = 3;

		// Token: 0x04000485 RID: 1157
		public const int MIN_REFRESH_TIMEOUT = 3000;

		// Token: 0x04000486 RID: 1158
		public const int MIN_CREATE_DELETE_APP_TIMEOUT = 45000;

		// Token: 0x04000487 RID: 1159
		public const int NUM_RETRIES_TO_RELOAD_CONFIG = 1;

		// Token: 0x04000488 RID: 1160
		public const int REGION_BATCH_SIZE_FOR_COPY = 32;

		// Token: 0x04000489 RID: 1161
		public const int MAX_WORKITEM_TIME = 30;

		// Token: 0x0400048A RID: 1162
		public const int SHUTDOWN_STATUS_QUERY_INTERVAL = 1000;

		// Token: 0x0400048B RID: 1163
		public const int SHUTDOWN_RETRY_INTERVAL = 3000;

		// Token: 0x0400048C RID: 1164
		public const int SERVICE_DEFAULT_STOP_TIME = 10000;

		// Token: 0x0400048D RID: 1165
		public const int DEFAULT_SYSTEM_REGION_COUNT = 1024;

		// Token: 0x0400048E RID: 1166
		public const string DEFAULT_REGION_START = "Default_Region_";

		// Token: 0x0400048F RID: 1167
		public const string FORMAT_DEFAULT_REGION_NAME = "d4";

		// Token: 0x04000490 RID: 1168
		public const int BATCHSIZE = 65536;

		// Token: 0x04000491 RID: 1169
		public const int FIXED_MESSAGE_OVERHEAD = 2048;

		// Token: 0x04000492 RID: 1170
		public const string DEFAULT_NAMED_CACHE = "default";

		// Token: 0x04000493 RID: 1171
		public const int EXPIRY_INTERVAL = 300;

		// Token: 0x04000494 RID: 1172
		public const int RETRIES_FOR_COMPLAINT = 20;

		// Token: 0x04000495 RID: 1173
		public const int MIN_CLIENT_INITIALIZE_TIMEOUT = 20000;

		// Token: 0x04000496 RID: 1174
		public const string LOGS_BACKUP_FOLDER_NAME = "LogsBackup";

		// Token: 0x04000497 RID: 1175
		public const int OLD_LOG_FOLDERS_COUNT = 10;

		// Token: 0x04000498 RID: 1176
		public const int STORE_RETRY_INTERVAL = 1;

		// Token: 0x04000499 RID: 1177
		public const int STORE_OPEN_RETRIES = 1;

		// Token: 0x0400049A RID: 1178
		public const int ServiceStopTime = 25;

		// Token: 0x0400049B RID: 1179
		public const int LocalCacheDefaultTTL = 300;

		// Token: 0x0400049C RID: 1180
		public const long HwmCountLocalCacheItem = 10000L;

		// Token: 0x0400049D RID: 1181
		public const int PercentageCleanup = 20;

		// Token: 0x0400049E RID: 1182
		public const int MemoryPressurePollInterval = 30;

		// Token: 0x0400049F RID: 1183
		public const int MemoryPressurePollIntervalHigh = 1;

		// Token: 0x040004A0 RID: 1184
		public const ulong VirtualMemoryLimit64BitProcess = 1099511627776UL;

		// Token: 0x040004A1 RID: 1185
		public const ulong VirtualMemoryLimit2GBProcess = 1395864371UL;

		// Token: 0x040004A2 RID: 1186
		public const ulong VirtualMemoryLimit3GBProcess = 2469606195UL;

		// Token: 0x040004A3 RID: 1187
		public const long ItemToRemoveBeforeCompactionLocalCache = 100000L;

		// Token: 0x040004A4 RID: 1188
		public const int HwmMemoryLocalCache = 85;

		// Token: 0x040004A5 RID: 1189
		public const int RetryCount = 60;

		// Token: 0x040004A6 RID: 1190
		public const int MaximumRetryInterval = 1;

		// Token: 0x040004A7 RID: 1191
		public const int MaxPendingEvents = 100000;

		// Token: 0x040004A8 RID: 1192
		public const int NmPollInterval = 300;

		// Token: 0x040004A9 RID: 1193
		public const int DefaultEventPerPartition = 1024;

		// Token: 0x040004AA RID: 1194
		public const int NmCopyBatchSize = 512;

		// Token: 0x040004AB RID: 1195
		public const int GCInterval = 0;

		// Token: 0x040004AC RID: 1196
		public const int MaxAllocSize = 65536;

		// Token: 0x040004AD RID: 1197
		public const int InvalidPort = 0;

		// Token: 0x040004AE RID: 1198
		public const int CacheSocketPortDefaultValue = 0;

		// Token: 0x040004AF RID: 1199
		public const int CacheDiscoveryPortDefaultValue = 0;

		// Token: 0x040004B0 RID: 1200
		public const string CacheEventSourceName = "Microsoft-Windows Server AppFabric Caching";

		// Token: 0x040004B1 RID: 1201
		public const long SampleInterval = 950L;

		// Token: 0x040004B2 RID: 1202
		public const bool IsPerfmonEnabled = true;

		// Token: 0x040004B3 RID: 1203
		public const long UsageEventGenerationPeriodicity = 5000L;

		// Token: 0x040004B4 RID: 1204
		public const bool DefaultIsMemoryManagerEnabled = true;

		// Token: 0x040004B5 RID: 1205
		public const bool DefaultIsExternalMemoryCheckEnabled = true;

		// Token: 0x040004B6 RID: 1206
		public const int DefaultInternalThrottleLowPercent = 0;

		// Token: 0x040004B7 RID: 1207
		public const int DefaultInternalThrottleHighPercent = 0;

		// Token: 0x040004B8 RID: 1208
		public const int DefaultExternalThrottleLowPercent = 0;

		// Token: 0x040004B9 RID: 1209
		public const int DefaultExternalThrottleHighPercent = 0;

		// Token: 0x040004BA RID: 1210
		public const long DefaultBlockSize = 524288L;

		// Token: 0x040004BB RID: 1211
		public const long DefaultBufferSize = 1024L;

		// Token: 0x040004BC RID: 1212
		public const long MaxBufferSize = 4194304L;

		// Token: 0x040004BD RID: 1213
		public const long MaxPayloadForSingleSegment = 4096L;

		// Token: 0x040004BE RID: 1214
		public const long AdditionalDirectory = 200L;

		// Token: 0x040004BF RID: 1215
		public const double MaxNumberOfItemsPerDirectory = 2.5;

		// Token: 0x040004C0 RID: 1216
		public const double MinNumberOfItemsPerDirectory = 2.0;

		// Token: 0x040004C1 RID: 1217
		public const long MaxToMinRatio = 3L;

		// Token: 0x040004C2 RID: 1218
		public const int PercentPoolGrowth = 10;

		// Token: 0x040004C3 RID: 1219
		public const int PercentItemInFinalizerQueue = 5;

		// Token: 0x040004C4 RID: 1220
		public const int PeriodicMemoryManagementRunTimeSec = 15;

		// Token: 0x040004C5 RID: 1221
		public const int SleepDurationOnMemoryExhaustion = 10;

		// Token: 0x040004C6 RID: 1222
		internal const int DefaultMemoryManagerPauseThreshold = 15;

		// Token: 0x040004C7 RID: 1223
		internal const int MinMemoryManagerPauseThreshold = 1;

		// Token: 0x040004C8 RID: 1224
		internal const int CacheSizeInMemoryPercent = 50;

		// Token: 0x040004C9 RID: 1225
		internal const int CacheSizeInMemoryPercentOnebox = 1;

		// Token: 0x040004CA RID: 1226
		internal const int CacheSizeInMemoryPercentForDedicatedOnDevFabric = 16;

		// Token: 0x040004CB RID: 1227
		internal const uint MaxMemorySizeOn32BitInstallation = 1228U;

		// Token: 0x040004CC RID: 1228
		public const int MonitorInterval = 1000;

		// Token: 0x040004CD RID: 1229
		public const int ThrottleGCInterval = 300;

		// Token: 0x040004CE RID: 1230
		public const bool IsProcessMinWorkingSetBoundsEnabled = true;

		// Token: 0x040004CF RID: 1231
		public const int LowMemoryPercent = 15;

		// Token: 0x040004D0 RID: 1232
		public const int LowMemoryReleasePercent = 7;

		// Token: 0x040004D1 RID: 1233
		public const long MaxProcessMemorySizeOn32BitSystem = 1395864371L;

		// Token: 0x040004D2 RID: 1234
		public const long MinMemoryTobeReleased = 52428800L;

		// Token: 0x040004D3 RID: 1235
		public const long DefaultHWM = 99L;

		// Token: 0x040004D4 RID: 1236
		public const long DefaultLWM = 90L;

		// Token: 0x040004D5 RID: 1237
		public const int MAX_EVICTION_GENERATIONS = 32;

		// Token: 0x040004D6 RID: 1238
		public const int REPEAT_GENERATION_INTERVALS = 10;

		// Token: 0x040004D7 RID: 1239
		public const int CacheEvictionHighWaterMarkPercent = 110;

		// Token: 0x040004D8 RID: 1240
		public const string RegKeyHKLMRoot = "SOFTWARE\\Microsoft\\AppFabric";

		// Token: 0x040004D9 RID: 1241
		public const string RegKeyHKLMProduct = "SOFTWARE\\Microsoft\\AppFabric\\V1.0";

		// Token: 0x040004DA RID: 1242
		public const string RegKeyHKLMConfig = "SOFTWARE\\Microsoft\\AppFabric\\V1.0\\Configuration";

		// Token: 0x040004DB RID: 1243
		public const string RegKeyCustomProviders = "SOFTWARE\\Microsoft\\AppFabric\\V1.0\\Providers\\AppFabricCaching";

		// Token: 0x040004DC RID: 1244
		public const string CustomProviderFQTNKey = "Type";

		// Token: 0x040004DD RID: 1245
		public const string CustomProviderDisplayName = "DisplayName";

		// Token: 0x040004DE RID: 1246
		public const string EventLogRegistryKey = "SYSTEM\\CurrentControlSet\\Services\\Eventlog\\Application\\AppFabricCachingService";

		// Token: 0x040004DF RID: 1247
		public const string RegKeyHKLMVersion = "SOFTWARE\\Microsoft\\AppFabric\\V1.0\\Version";

		// Token: 0x040004E0 RID: 1248
		public const string CodeVersionKey = "CodeVersion";

		// Token: 0x040004E1 RID: 1249
		public const string BeginAllowedCodeVersionRangeKey = "BeginAllowedCodeVersionRange";

		// Token: 0x040004E2 RID: 1250
		public const string EndAllowedCodeVersionRangeKey = "EndAllowedCodeVersionRange";

		// Token: 0x040004E3 RID: 1251
		public const string BeginAllowedClientCodeVersionRangeKey = "BeginAllowedClientCodeVersionRange";

		// Token: 0x040004E4 RID: 1252
		public const string EndAllowedClientCodeVersionRangeKey = "EndAllowedClientCodeVersionRange";

		// Token: 0x040004E5 RID: 1253
		public const string InstallPathKey = "InstallPath";

		// Token: 0x040004E6 RID: 1254
		public const string AdminConfiguredKey = "AdminConfigured";

		// Token: 0x040004E7 RID: 1255
		public const string ServiceConfiguredKey = "ServiceConfigured";

		// Token: 0x040004E8 RID: 1256
		public const string ProviderKey = "Provider";

		// Token: 0x040004E9 RID: 1257
		public const string ConnectionStringKey = "ConnectionString";

		// Token: 0x040004EA RID: 1258
		public const string TempStoreRegistryKey = "SOFTWARE\\Microsoft\\AppFabric\\V1.0\\Temp";

		// Token: 0x040004EB RID: 1259
		public const string CurrentStoreVersion = "3.0.0.0";

		// Token: 0x040004EC RID: 1260
		public const long MinimumVersion = 1L;

		// Token: 0x040004ED RID: 1261
		public const long InternalCodeVersion = 1002L;

		// Token: 0x040004EE RID: 1262
		public const long FirstAzureCodeVersion = 1000L;

		// Token: 0x040004EF RID: 1263
		public const int MinimumConfigPollWait = 300000;

		// Token: 0x040004F0 RID: 1264
		public const int MaximumConfigPollWait = 360000;

		// Token: 0x040004F1 RID: 1265
		public const int MAX_NUMBER_OF_SEED_NODES_VAS = 9;

		// Token: 0x040004F2 RID: 1266
		public const int MAX_NUMBER_OF_SEED_NODES_ONPREM = 7;

		// Token: 0x040004F3 RID: 1267
		public const int SMALL_CLUSTER_NO_OF_PARTITIONS = 32;

		// Token: 0x040004F4 RID: 1268
		public const int MED_CLUSTER_NO_OF_PARTITIONS = 256;

		// Token: 0x040004F5 RID: 1269
		public const int LARGE_CLUSTER_NO_OF_PARTITIONS = 1024;

		// Token: 0x040004F6 RID: 1270
		public const string SMALL_CLUSTER = "small";

		// Token: 0x040004F7 RID: 1271
		public const string MED_CLUSTER = "medium";

		// Token: 0x040004F8 RID: 1272
		public const string LARGE_CLUSTER = "large";

		// Token: 0x040004F9 RID: 1273
		public const string LogFileExtension = ".log";

		// Token: 0x040004FA RID: 1274
		public const string CacheServiceName = "AppFabricCachingService";

		// Token: 0x040004FB RID: 1275
		public const string CacheServiceExeName = "DistributedCacheService.exe";

		// Token: 0x040004FC RID: 1276
		public const string LocalConfigFileName = "DistributedCacheService.exe.config";

		// Token: 0x040004FD RID: 1277
		public const int CONFIG_FILE_SAVE_RETRIES = 3;

		// Token: 0x040004FE RID: 1278
		public const int CONFIG_FILE_SAVE_RETRY_INTERVAL = 5000;

		// Token: 0x040004FF RID: 1279
		internal const string XmlProvider = "xml";

		// Token: 0x04000500 RID: 1280
		internal const string LocalHost = "localhost";

		// Token: 0x04000501 RID: 1281
		public const int PartitionRangeHigh = 2147483647;

		// Token: 0x04000502 RID: 1282
		public const int PartitionRangeLow = -2147483648;

		// Token: 0x04000503 RID: 1283
		internal const string SQLServerProvider = "System.Data.SqlClient";

		// Token: 0x04000504 RID: 1284
		internal const string SQLAzureProvider = "SqlAzure";

		// Token: 0x04000505 RID: 1285
		internal const string WindowsAzureBlobProvider = "WindowsAzureBlobProvider";

		// Token: 0x04000506 RID: 1286
		internal const string EXTERNAL_STORE_LOG_SOURCE_PREFIX = "ExternalStore.";

		// Token: 0x04000507 RID: 1287
		internal const string SQLServerProviderLogSource = "ExternalStore.SqlServer";

		// Token: 0x04000508 RID: 1288
		internal const string SQLAzureProviderLogSource = "ExternalStore.SqlAzure";

		// Token: 0x04000509 RID: 1289
		internal const string WindowsAzureBlobProviderLogSource = "ExternalStore.WindowsAzureBlob";

		// Token: 0x0400050A RID: 1290
		internal const string SQLServerProviderType = "Microsoft.ApplicationServer.Caching.SqlServerCustomProvider, Microsoft.ApplicationServer.Caching.SqlProvider, Culture=neutral";

		// Token: 0x0400050B RID: 1291
		internal const string SQLAzureProviderType = "Microsoft.ApplicationServer.Caching.SqlAzureCustomProvider, Microsoft.ApplicationServer.Caching.SqlAzureProvider, Culture=neutral";

		// Token: 0x0400050C RID: 1292
		internal const string WindowsAzureBlobProviderType = "Microsoft.ApplicationServer.Caching.WindowsAzureBlobProvider, Microsoft.ApplicationServer.Caching.WindowsAzureBlobProvider, Culture=neutral";

		// Token: 0x0400050D RID: 1293
		internal const string AzureProviderType = "Microsoft.ApplicationServer.Caching.AzureProvider, Microsoft.ApplicationServer.Caching.CacheAzureProvider, Culture=neutral";

		// Token: 0x0400050E RID: 1294
		internal const string SQLServerProviderDisplayName = "SQL Server Distributed Cache Configuration Store Provider";

		// Token: 0x0400050F RID: 1295
		internal const string DEL_TKT_ARG = "deleteTKT";

		// Token: 0x04000510 RID: 1296
		internal const string MONITORED_START = "monitoredStart";

		// Token: 0x04000511 RID: 1297
		internal const int WB_CHECKPOINT_BATCHSIZE = 180;

		// Token: 0x04000512 RID: 1298
		internal const int DefaultETWMonitorInterval = 5000;

		// Token: 0x04000513 RID: 1299
		public const string CacheServicePort = "cacheServicePort";

		// Token: 0x04000514 RID: 1300
		public const string CacheSocketPort = "cacheSocketPort";

		// Token: 0x04000515 RID: 1301
		public const string CacheDiscoveryPort = "cacheDiscoveryPort";

		// Token: 0x04000516 RID: 1302
		public const string CacheSslSocketPort = "cacheSslSocketPort";

		// Token: 0x04000517 RID: 1303
		public const string CacheSslDiscoveryPort = "cacheSslDiscoveryPort";

		// Token: 0x04000518 RID: 1304
		public const string CacheServicePortInternal = "cacheServicePortInternal";

		// Token: 0x04000519 RID: 1305
		public const string CacheClusterPort = "cacheClusterPort";

		// Token: 0x0400051A RID: 1306
		public const string CacheArbitrationPort = "cacheArbitrationPort";

		// Token: 0x0400051B RID: 1307
		public const string CacheReplicationPort = "cacheReplicationPort";

		// Token: 0x0400051C RID: 1308
		public const string DedicatedPluginNamespace = "Microsoft.WindowsAzure.Plugins.Caching";

		// Token: 0x0400051D RID: 1309
		public const string AzureClientHelperAssemblyName = "Microsoft.ApplicationServer.Caching.AzureClientHelper";

		// Token: 0x0400051E RID: 1310
		public const string AzureClientHelperTypeName = "Microsoft.ApplicationServer.Caching.AzureClientHelper.RoleUtility";

		// Token: 0x0400051F RID: 1311
		public const int SEED_NODE_COUNT_DEDICATED = 9;

		// Token: 0x04000520 RID: 1312
		public const int MAX_UD_COUNT_DEDICATED = 20;

		// Token: 0x04000521 RID: 1313
		public const int MAX_FD_COUNT_DEDICATED = 5;

		// Token: 0x04000522 RID: 1314
		internal const string DedicatedMemcacheEndpointPrefix = "memcache_";

		// Token: 0x04000523 RID: 1315
		internal const string ClientShimDefaultConfigurationName = "DefaultShimConfig";

		// Token: 0x04000524 RID: 1316
		public static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		// Token: 0x04000525 RID: 1317
		public static readonly int SignalTimeout = 1000;

		// Token: 0x04000526 RID: 1318
		public static readonly int NumberOfSignalHandlerContainers = 32;

		// Token: 0x04000527 RID: 1319
		public static readonly int NumEmptyWrappers = 30000;

		// Token: 0x04000528 RID: 1320
		public static readonly int REGN_INDEXLEVEL = 2;

		// Token: 0x04000529 RID: 1321
		public static readonly bool RGN_EVICTABLE = true;

		// Token: 0x0400052A RID: 1322
		public static readonly bool RGN_EXPIRABLE = true;

		// Token: 0x0400052B RID: 1323
		public static readonly ExpirationType RGB_EXPTYPE = ExpirationType.AbsoluteExpiration;

		// Token: 0x0400052C RID: 1324
		public static readonly int NC_REGULARBATCHSIZE = 30000;

		// Token: 0x0400052D RID: 1325
		public static readonly int NC_ONDEMANDBATCHSIZE = 30000;

		// Token: 0x0400052E RID: 1326
		public static readonly int NC_QUEUESCOUNT = 1;

		// Token: 0x0400052F RID: 1327
		public static readonly bool NC_EVICTABLE = true;

		// Token: 0x04000530 RID: 1328
		public static readonly bool NC_EXPIRABLE = true;

		// Token: 0x04000531 RID: 1329
		public static readonly bool CND_EVICTABLE = true;

		// Token: 0x04000532 RID: 1330
		public static readonly bool CND_EXPIRABLE = true;

		// Token: 0x04000533 RID: 1331
		public static readonly int CND_QUEUESCOUNT = 1;

		// Token: 0x04000534 RID: 1332
		public static int INITIAL_DELAY_BETWEEN_REPLICATION_RETRIES;

		// Token: 0x04000535 RID: 1333
		public static int EVICTION_REPLICATION_BATCH_SIZE = 256;

		// Token: 0x04000536 RID: 1334
		public static int NUM_SECONDARIES_DEFAULT;

		// Token: 0x04000537 RID: 1335
		public static int TIMEOUT = 15000;

		// Token: 0x04000538 RID: 1336
		public static int MAX_INTERVAL;

		// Token: 0x04000539 RID: 1337
		public static int MAX_REQUEST_RETRIES = 20;

		// Token: 0x0400053A RID: 1338
		public static int INTERVAL_BETWEEN_RETRIES;

		// Token: 0x0400053B RID: 1339
		public static int INTERVAL_BETWEEN_COMPLAINTS;

		// Token: 0x0400053C RID: 1340
		public static int WAIT_ON_REQUEST_RECEIVED;

		// Token: 0x0400053D RID: 1341
		public static TimeSpan LocalCacheExpiryInterval = new TimeSpan(0, 0, 300);

		// Token: 0x0400053E RID: 1342
		public static TimeSpan LocalCacheEvictionInterval = new TimeSpan(0, 0, 10);

		// Token: 0x0400053F RID: 1343
		public static int INITIAL_PING_DELAY;

		// Token: 0x04000540 RID: 1344
		public static int MAX_PING_DELAY;

		// Token: 0x04000541 RID: 1345
		public static readonly TimeSpan CLIENT_CHANNEL_OPEN_WAIT = TimeSpan.FromSeconds(3.0);

		// Token: 0x04000542 RID: 1346
		public static TimeSpan CLIENT_CHANNEL_OPEN_TIMEOUT = TimeSpan.FromMinutes(2.0);

		// Token: 0x04000543 RID: 1347
		public static TimeSpan SERVER_CHANNEL_OPEN_TIMEOUT = TimeSpan.FromSeconds(20.0);

		// Token: 0x04000544 RID: 1348
		public static TimeSpan RECEIVE_TIMEOUT = TimeSpan.FromMinutes(10.0);

		// Token: 0x04000545 RID: 1349
		public static TimeSpan MinSendTimeout = new TimeSpan(0, 0, 10);

		// Token: 0x04000546 RID: 1350
		public static TimeSpan CLIENT_CHANNEL_RECIEVE_TIMEOUT = TimeSpan.FromSeconds(40.0);

		// Token: 0x04000547 RID: 1351
		public static TimeSpanValidator TimeSpanNonNegativeValidator = new TimeSpanValidator(TimeSpan.Zero, TimeSpan.MaxValue);

		// Token: 0x04000548 RID: 1352
		public static readonly string HostCounterCategory = "Windows Azure Caching:Host";

		// Token: 0x04000549 RID: 1353
		public static readonly string HostCounterCategoryHelp = ConfigManager.GetString("PerfmonHostCategoryHelp");

		// Token: 0x0400054A RID: 1354
		public static readonly string HostTotalDataSize = "Total Data Size MBytes";

		// Token: 0x0400054B RID: 1355
		public static readonly string HostTotalDataSizeHelp = ConfigManager.GetString("PerfmonHostTotalDataSizeHelp");

		// Token: 0x0400054C RID: 1356
		public static readonly string HostTotalMissCount = "Total Cache Misses";

		// Token: 0x0400054D RID: 1357
		public static readonly string HostTotalMissCountHelp = ConfigManager.GetString("PerfmonHostTotalMissCountHelp");

		// Token: 0x0400054E RID: 1358
		public static readonly string HostTotalMissCountPerSecond = "Total Cache Misses /sec";

		// Token: 0x0400054F RID: 1359
		public static readonly string HostTotalMissCountPerSecondHelp = ConfigManager.GetString("PerfmonHostTotalMissCountPerSecondHelp");

		// Token: 0x04000550 RID: 1360
		public static readonly string HostTotalGetRequest = "Total Get Requests";

		// Token: 0x04000551 RID: 1361
		public static readonly string HostTotalGetRequestHelp = ConfigManager.GetString("PerfmonHostTotalGetRequestHelp");

		// Token: 0x04000552 RID: 1362
		public static readonly string HostTotalGetRequestPerSecond = "Total Get Requests /sec";

		// Token: 0x04000553 RID: 1363
		public static readonly string HostTotalGetRequestPerSecondHelp = ConfigManager.GetString("PerfmonHostTotalGetRequestPerSecondHelp");

		// Token: 0x04000554 RID: 1364
		public static readonly string HostTotalGetRequestMissCount = "Total Get Misses";

		// Token: 0x04000555 RID: 1365
		public static readonly string HostTotalGetRequestMissCountHelp = ConfigManager.GetString("PerfmonHostTotalGetRequestMissCountHelp");

		// Token: 0x04000556 RID: 1366
		public static readonly string HostTotalGetRequestMissCountPerSecond = "Total Get Misses /sec";

		// Token: 0x04000557 RID: 1367
		public static readonly string HostTotalGetRequestMissCountPerSecondHelp = ConfigManager.GetString("PerfmonHostTotalGetRequestMissCountPerSecondHelp");

		// Token: 0x04000558 RID: 1368
		public static readonly string HostTotalPollRequest = "Total Notification Poll Requests";

		// Token: 0x04000559 RID: 1369
		public static readonly string HostTotalPollRequestHelp = ConfigManager.GetString("PerfmonHostTotalPollRequestHelp");

		// Token: 0x0400055A RID: 1370
		public static readonly string HostTotalPollRequestPerSecond = "Total Notification Poll Requests /sec";

		// Token: 0x0400055B RID: 1371
		public static readonly string HostTotalPollRequestPerSecondHelp = ConfigManager.GetString("PerfmonHostTotalPollRequestPerSecondHelp");

		// Token: 0x0400055C RID: 1372
		public static readonly string HostTotalNotificationDelivered = "Total Notification Delivered";

		// Token: 0x0400055D RID: 1373
		public static readonly string HostTotalNotificationDeliveredHelp = ConfigManager.GetString("PerfmonHostTotalNotificationDeliveredHelp");

		// Token: 0x0400055E RID: 1374
		public static readonly string HostTotalNotificationDeliveredPerSecond = "Total Notification Delivered /sec";

		// Token: 0x0400055F RID: 1375
		public static readonly string HostTotalNotificationDeliveredPerSecondHelp = ConfigManager.GetString("PerfmonHostTotalNotificationDeliveredPerSecondHelp");

		// Token: 0x04000560 RID: 1376
		public static readonly string HostTotalRequest = "Total Client Requests";

		// Token: 0x04000561 RID: 1377
		public static readonly string HostTotalRequestHelp = ConfigManager.GetString("PerfmonHostTotalRequestHelp");

		// Token: 0x04000562 RID: 1378
		public static readonly string HostTotalRequestPerSecond = "Total Client Requests /sec";

		// Token: 0x04000563 RID: 1379
		public static readonly string HostTotalRequestPerSecondHelp = ConfigManager.GetString("PerfmonHostTotalRequestPerSecondHelp");

		// Token: 0x04000564 RID: 1380
		public static readonly string HostTotalResponse = "Total Requests Served";

		// Token: 0x04000565 RID: 1381
		public static readonly string HostTotalResponseHelp = ConfigManager.GetString("PerfmonHostTotalResponseHelp");

		// Token: 0x04000566 RID: 1382
		public static readonly string HostTotalResponsePerSecond = "Total Requests Served /sec";

		// Token: 0x04000567 RID: 1383
		public static readonly string HostTotalResponsePerSecondHelp = ConfigManager.GetString("PerfmonHostTotalResponsePerSecondHelp");

		// Token: 0x04000568 RID: 1384
		public static readonly string HostTotalReadRequest = "Total Read Requests";

		// Token: 0x04000569 RID: 1385
		public static readonly string HostTotalReadRequestHelp = ConfigManager.GetString("PerfmonHostTotalReadRequestHelp");

		// Token: 0x0400056A RID: 1386
		public static readonly string HostTotalReadRequestPerSecond = "Total Read Requests /sec";

		// Token: 0x0400056B RID: 1387
		public static readonly string HostTotalReadRequestPerSecondHelp = ConfigManager.GetString("PerfmonHostTotalReadRequestPerSecondHelp");

		// Token: 0x0400056C RID: 1388
		public static readonly string HostTotalWriteRequest = "Total Write Operations";

		// Token: 0x0400056D RID: 1389
		public static readonly string HostTotalWriteRequestHelp = ConfigManager.GetString("PerfmonHostTotalWriteRequestHelp");

		// Token: 0x0400056E RID: 1390
		public static readonly string HostTotalWriteRequestPerSecond = "Total Write Operations /sec";

		// Token: 0x0400056F RID: 1391
		public static readonly string HostTotalWriteRequestPerSecondHelp = ConfigManager.GetString("PerfmonHostTotalWriteRequestPerSecondHelp");

		// Token: 0x04000570 RID: 1392
		public static readonly string HostTotalException = "Total Failure Exceptions";

		// Token: 0x04000571 RID: 1393
		public static readonly string HostTotalExceptionHelp = ConfigManager.GetString("PerfmonHostTotalExceptionHelp");

		// Token: 0x04000572 RID: 1394
		public static readonly string HostTotalExceptionPerSecond = "Total Failure Exceptions /sec";

		// Token: 0x04000573 RID: 1395
		public static readonly string HostTotalExceptionPerSecondHelp = ConfigManager.GetString("PerfmonHostTotalExceptionPerSecondHelp");

		// Token: 0x04000574 RID: 1396
		public static readonly string HostTotalRetryException = "Total Retry Exception";

		// Token: 0x04000575 RID: 1397
		public static readonly string HostTotalRetryExceptionHelp = ConfigManager.GetString("PerfmonHostTotalRetryExceptionHelp");

		// Token: 0x04000576 RID: 1398
		public static readonly string HostTotalRetryExceptionPerSecond = "Total Retry Exception /sec";

		// Token: 0x04000577 RID: 1399
		public static readonly string HostTotalRetryExceptionPerSecondHelp = ConfigManager.GetString("PerfmonHostTotalRetryExceptionPerSecondHelp");

		// Token: 0x04000578 RID: 1400
		public static readonly string HostTotalGetAndLockRequest = "Total GetAndLock Requests";

		// Token: 0x04000579 RID: 1401
		public static readonly string HostTotalGetAndLockRequestHelp = ConfigManager.GetString("PerfmonHostTotalGetAndLockRequestHelp");

		// Token: 0x0400057A RID: 1402
		public static readonly string HostTotalGetAndLockRequestPerSecond = "Total GetAndLock Requests /sec";

		// Token: 0x0400057B RID: 1403
		public static readonly string HostTotalGetAndLockRequestPerSecondHelp = ConfigManager.GetString("PerfmonHostTotalGetAndLockRequestPerSecondHelp");

		// Token: 0x0400057C RID: 1404
		public static readonly string HostTotalSuccessfulGetAndLockRequest = "Total Successful GetAndLock Requests";

		// Token: 0x0400057D RID: 1405
		public static readonly string HostTotalSuccessfulGetAndLockRequestHelp = ConfigManager.GetString("PerfmonHostTotalSuccessfulGetAndLockRequestHelp");

		// Token: 0x0400057E RID: 1406
		public static readonly string HostTotalSuccessfulGetAndLockRequestPerSecond = "Total Successful GetAndLock Requests /sec";

		// Token: 0x0400057F RID: 1407
		public static readonly string HostTotalSuccessfulGetAndLockRequestPerSecondHelp = ConfigManager.GetString("PerfmonHostTotalSuccessfulGetAndLockRequestPerSecondHelp");

		// Token: 0x04000580 RID: 1408
		public static readonly string HostPercentageMiss = "Cache Miss Percentage";

		// Token: 0x04000581 RID: 1409
		public static readonly string HostPercentageMissHelp = ConfigManager.GetString("PerfmonHostPercentageMissHelp");

		// Token: 0x04000582 RID: 1410
		public static readonly string HostPercentageMissBase = "Base Cache Miss Percentage";

		// Token: 0x04000583 RID: 1411
		public static readonly string HostPercentageMissBaseHelp = ConfigManager.GetString("PerfmonHostBasePercentageMissHelp");

		// Token: 0x04000584 RID: 1412
		public static readonly string HostTotalExpiredObjects = "Total Expired Objects";

		// Token: 0x04000585 RID: 1413
		public static readonly string HostTotalExpiredObjectsHelp = ConfigManager.GetString("PerfmonHostTotalExpiredObjectsHelp");

		// Token: 0x04000586 RID: 1414
		public static readonly string HostTotalEvictedObjects = "Total Evicted Objects";

		// Token: 0x04000587 RID: 1415
		public static readonly string HostTotalEvictedObjectsHelp = ConfigManager.GetString("PerfmonHostTotalEvictedObjectsHelp");

		// Token: 0x04000588 RID: 1416
		public static readonly string HostTotalEvictionRun = "Total Eviction Runs";

		// Token: 0x04000589 RID: 1417
		public static readonly string HostTotalEvictionRunHelp = ConfigManager.GetString("PerfmonHostTotalEvictionRunHelp");

		// Token: 0x0400058A RID: 1418
		public static readonly string HostTotalMemoryEvicted = "Total Memory Evicted Bytes";

		// Token: 0x0400058B RID: 1419
		public static readonly string HostTotalMemoryEvictedHelp = ConfigManager.GetString("PerfmonHostTotalMemoryEvictedHelp");

		// Token: 0x0400058C RID: 1420
		public static readonly string HostTotalObjectReturned = "Total Objects Returned";

		// Token: 0x0400058D RID: 1421
		public static readonly string HostTotalObjectReturnedHelp = ConfigManager.GetString("PerfmonHostHostTotalObjectReturnedHelp");

		// Token: 0x0400058E RID: 1422
		public static readonly string HostTotalObjectReturnedPerSec = "Total Objects Returned /sec";

		// Token: 0x0400058F RID: 1423
		public static readonly string HostTotalObjectReturnedPerSecHelp = ConfigManager.GetString("PerfmonHostTotalObjectReturnedPerSecondHelp");

		// Token: 0x04000590 RID: 1424
		public static readonly string HostAverageQuorumResponseTime = "Average Quorum Response Time / operation Microsecond";

		// Token: 0x04000591 RID: 1425
		public static readonly string HostAverageQuorumResponseTimeHelp = ConfigManager.GetString("PerfmonHostAverageQuorumResponseTimeHelp");

		// Token: 0x04000592 RID: 1426
		public static readonly string HostAverageQuorumResponseTimeBase = "Base Average Quorum Response Time / operation";

		// Token: 0x04000593 RID: 1427
		public static readonly string HostAverageQuorumResponseTimeBaseHelp = ConfigManager.GetString("PerfmonHostAverageQuorumResponseTimeBaseHelp");

		// Token: 0x04000594 RID: 1428
		public static readonly string HostAverageAllSecondaryResponseTime = "Average Secondary Response Time / operation Microsecond";

		// Token: 0x04000595 RID: 1429
		public static readonly string HostAverageAllSecondaryResponseTimeHelp = ConfigManager.GetString("PerfmonHostAverageAllSecondaryResponseTimeHelp");

		// Token: 0x04000596 RID: 1430
		public static readonly string HostAverageAllSecondaryResponseTimeBase = "Base Average Secondary Response Time / operation";

		// Token: 0x04000597 RID: 1431
		public static readonly string HostAverageAllSecondaryResponseTimeBaseHelp = ConfigManager.GetString("PerfmonHostAverageAllSecondaryResponseTimeBaseHelp");

		// Token: 0x04000598 RID: 1432
		public static readonly string HostTotalObjectCount = "Total Object Count";

		// Token: 0x04000599 RID: 1433
		public static readonly string HostTotalObjectCountHelp = ConfigManager.GetString("PerfmonHostTotalObjectCountHelp");

		// Token: 0x0400059A RID: 1434
		public static readonly string HostTotalPrimaryDataSize = "Total Primary Data Size";

		// Token: 0x0400059B RID: 1435
		public static readonly string HostTotalPrimaryDataSizeHelp = ConfigManager.GetString("PerfmonHostTotalPrimaryDataSizeHelp");

		// Token: 0x0400059C RID: 1436
		public static readonly string HostTotalSecondaryDataSize = "Total Secondary Data Size";

		// Token: 0x0400059D RID: 1437
		public static readonly string HostTotalSecondaryDataSizeHelp = ConfigManager.GetString("PerfmonHostTotalSecondaryDataSizeHelp");

		// Token: 0x0400059E RID: 1438
		public static readonly string TotalAvailableMemory = "Total Available Memory Bytes";

		// Token: 0x0400059F RID: 1439
		public static readonly string AvailableMemoryPercentageBase = "Base Available Memory Percentage";

		// Token: 0x040005A0 RID: 1440
		public static readonly string AvailableMemoryPercentage = "Available Memory Percentage";

		// Token: 0x040005A1 RID: 1441
		public static readonly string TotalAvailableCacheItemCount = "Total Available CacheItem Count";

		// Token: 0x040005A2 RID: 1442
		public static readonly string TotalAllocatedCacheItemCount = "Total Allocated CacheItem Count";

		// Token: 0x040005A3 RID: 1443
		public static readonly string AvailableCacheItemPercentageBase = "Base Available CacheItem Percentage";

		// Token: 0x040005A4 RID: 1444
		public static readonly string AvailableCacheItemPercentage = "Available CacheItem Percentage";

		// Token: 0x040005A5 RID: 1445
		public static readonly string TotalAvailableDirectoryCount = "Total Available Directory Count";

		// Token: 0x040005A6 RID: 1446
		public static readonly string TotalAllocatedDirectoryCount = "Total Allocated Directory Count";

		// Token: 0x040005A7 RID: 1447
		public static readonly string AvailableDirectoryPercentageBase = "Base Available Directory Percentage";

		// Token: 0x040005A8 RID: 1448
		public static readonly string AvailableDirectoryPercentage = "Available Directory Percentage";

		// Token: 0x040005A9 RID: 1449
		public static readonly string TotalAvailableWBItemCount = "Total Available WBItem Count";

		// Token: 0x040005AA RID: 1450
		public static readonly string TotalAllocatedWBItemCount = "Total Allocated WBItem Count";

		// Token: 0x040005AB RID: 1451
		public static readonly string AvailableWBItemPercentageBase = "Base Available WBItem Percentage";

		// Token: 0x040005AC RID: 1452
		public static readonly string AvailableWBItemPercentage = "Available WBItem Percentage";

		// Token: 0x040005AD RID: 1453
		public static readonly string HostWBFlushedItemCount = "Total Items Written Behind";

		// Token: 0x040005AE RID: 1454
		public static readonly string HostWBDroppedItemCount = "Total Items Dropped";

		// Token: 0x040005AF RID: 1455
		public static readonly string HostWBQueueCount = "Write Behind Queue Count";

		// Token: 0x040005B0 RID: 1456
		public static readonly string HostRTPendingCount = "Read Through Queue Count";

		// Token: 0x040005B1 RID: 1457
		public static readonly string HostRTMissingCount = "Total Read Through Misses";

		// Token: 0x040005B2 RID: 1458
		public static readonly string HostRTErrorCount = "Total Read Through Errors";

		// Token: 0x040005B3 RID: 1459
		public static readonly string HostRTSuccessCount = "Total Read Through Items";

		// Token: 0x040005B4 RID: 1460
		public static readonly string RouteFailingPercentage = "Gateway Failure Percentage";

		// Token: 0x040005B5 RID: 1461
		public static readonly string RouteFailingPercentageBase = "Base Gateway Failure Percentage";

		// Token: 0x040005B6 RID: 1462
		public static readonly string GatewayProcessTime = "Gateway Process Time";

		// Token: 0x040005B7 RID: 1463
		public static readonly string GatewayProcessTimeBase = "Base Gateway Process Time";

		// Token: 0x040005B8 RID: 1464
		public static readonly string RequestProcessingErrorsPercentage = "Request Processing Error Percentage";

		// Token: 0x040005B9 RID: 1465
		public static readonly string RequestProcessingErrorsPercentageBase = "Base Request Processing Error Percentage";

		// Token: 0x040005BA RID: 1466
		public static readonly string TimeSinceGracefulShutdownStart = "Time since Graceful Shutdown Started";

		// Token: 0x040005BB RID: 1467
		public static readonly string TotalConnectionsCount = "Total Connections Count";

		// Token: 0x040005BC RID: 1468
		public static readonly string ThrottledConnectionsCount = "Throttled Connections Count";

		// Token: 0x040005BD RID: 1469
		public static readonly string CacheCounterCategory = "Windows Azure Caching:Cache";

		// Token: 0x040005BE RID: 1470
		public static readonly string CacheCounterCategoryHelp = ConfigManager.GetString("PerfmonCacheCategoryHelp");

		// Token: 0x040005BF RID: 1471
		public static readonly string CacheTotalDataSize = "Total Data Size MBytes";

		// Token: 0x040005C0 RID: 1472
		public static readonly string CacheTotalDataSizeHelp = ConfigManager.GetString("PerfmonCacheTotalDataSizeHelp");

		// Token: 0x040005C1 RID: 1473
		public static readonly string CacheTotalMissCount = "Total Cache Misses";

		// Token: 0x040005C2 RID: 1474
		public static readonly string CacheTotalMissCountHelp = ConfigManager.GetString("PerfmonCacheTotalMissCountHelp");

		// Token: 0x040005C3 RID: 1475
		public static readonly string CacheTotalMissCountPerSecond = "Total Cache Misses /sec";

		// Token: 0x040005C4 RID: 1476
		public static readonly string CacheTotalMissCountPerSecondHelp = ConfigManager.GetString("PerfmonCacheTotalMissCountPerSecondHelp");

		// Token: 0x040005C5 RID: 1477
		public static readonly string CachePercentageMiss = "Cache Miss Percentage";

		// Token: 0x040005C6 RID: 1478
		public static readonly string CachePercentageMissHelp = ConfigManager.GetString("PerfmonCachePercentageMissHelp");

		// Token: 0x040005C7 RID: 1479
		public static readonly string CachePercentageMissBase = "Base Cache Miss Percentage";

		// Token: 0x040005C8 RID: 1480
		public static readonly string CachePercentageMissBaseHelp = ConfigManager.GetString("PerfmonCacheBasePercentageMissHelp");

		// Token: 0x040005C9 RID: 1481
		public static readonly string CacheTotalReadRequest = "Total Read Requests";

		// Token: 0x040005CA RID: 1482
		public static readonly string CacheTotalReadRequestHelp = ConfigManager.GetString("PerfmonCacheTotalReadRequestHelp");

		// Token: 0x040005CB RID: 1483
		public static readonly string CacheTotalReadRequestPerSecond = "Total Read Requests /sec";

		// Token: 0x040005CC RID: 1484
		public static readonly string CacheTotalReadRequestPerSecondHelp = ConfigManager.GetString("PerfmonCacheTotalReadRequestPerSecondHelp");

		// Token: 0x040005CD RID: 1485
		public static readonly string CacheTotalWriteRequest = "Total Write Operations";

		// Token: 0x040005CE RID: 1486
		public static readonly string CacheTotalWriteRequestHelp = ConfigManager.GetString("PerfmonCacheTotalWriteRequestHelp");

		// Token: 0x040005CF RID: 1487
		public static readonly string CacheTotalWriteRequestPerSecond = "Total Write Operations /sec";

		// Token: 0x040005D0 RID: 1488
		public static readonly string CacheTotalWriteRequestPerSecondHelp = ConfigManager.GetString("PerfmonCacheTotalWriteRequestPerSecondHelp");

		// Token: 0x040005D1 RID: 1489
		public static readonly string CacheTotalObjectReturned = "Total Objects Returned";

		// Token: 0x040005D2 RID: 1490
		public static readonly string CacheTotalObjectReturnedHelp = ConfigManager.GetString("PerfmonCacheTotalObjectReturnedHelp");

		// Token: 0x040005D3 RID: 1491
		public static readonly string CacheTotalObjectReturnedPerSec = "Total Objects Returned /sec";

		// Token: 0x040005D4 RID: 1492
		public static readonly string CacheTotalObjectReturnedPerSecHelp = ConfigManager.GetString("PerfmonCacheTotalObjectReturnedPerSecondHelp");

		// Token: 0x040005D5 RID: 1493
		public static readonly string CacheTotalGetAndLockRequest = "Total GetAndLock Requests";

		// Token: 0x040005D6 RID: 1494
		public static readonly string CacheTotalGetAndLockRequestHelp = ConfigManager.GetString("PerfmonCacheTotalGetAndLockRequestHelp");

		// Token: 0x040005D7 RID: 1495
		public static readonly string CacheTotalGetAndLockRequestPerSecond = "Total GetAndLock Requests /sec";

		// Token: 0x040005D8 RID: 1496
		public static readonly string CacheTotalGetAndLockRequestPerSecondHelp = ConfigManager.GetString("PerfmonCacheTotalGetAndLockRequestPerSecondHelp");

		// Token: 0x040005D9 RID: 1497
		public static readonly string CacheTotalSuccessfulGetAndLockRequest = "Total Successful GetAndLock Requests";

		// Token: 0x040005DA RID: 1498
		public static readonly string CacheTotalSuccessfulGetAndLockRequestHelp = ConfigManager.GetString("PerfmonCacheTotalSuccessfulGetAndLockRequestHelp");

		// Token: 0x040005DB RID: 1499
		public static readonly string CacheTotalSuccessfulGetAndLockRequestPerSecond = "Total Successful GetAndLock Requests /sec";

		// Token: 0x040005DC RID: 1500
		public static readonly string CacheTotalSuccessfulGetAndLockRequestPerSecondHelp = ConfigManager.GetString("PerfmonCacheTotalSuccessfulGetAndLockRequestPerSecondHelp");

		// Token: 0x040005DD RID: 1501
		public static readonly string CacheTotalRequest = "Total Client Requests";

		// Token: 0x040005DE RID: 1502
		public static readonly string CacheTotalRequestHelp = ConfigManager.GetString("PerfmonCacheTotalRequestHelp");

		// Token: 0x040005DF RID: 1503
		public static readonly string CacheTotalRequestPerSecond = "Total Client Requests /sec";

		// Token: 0x040005E0 RID: 1504
		public static readonly string CacheTotalRequestPerSecondHelp = ConfigManager.GetString("PerfmonCacheTotalRequestPerSecondHelp");

		// Token: 0x040005E1 RID: 1505
		public static readonly string CacheTotalObjectCount = "Total Object Count";

		// Token: 0x040005E2 RID: 1506
		public static readonly string CacheTotalObjectCountHelp = ConfigManager.GetString("PerfmonCacheTotalObjectCountHelp");

		// Token: 0x040005E3 RID: 1507
		public static readonly string CacheTotalPrimaryDataSize = "Total Primary Data Size";

		// Token: 0x040005E4 RID: 1508
		public static readonly string CacheTotalPrimaryDataSizeHelp = ConfigManager.GetString("PerfmonCacheTotalPrimaryDataSizeHelp");

		// Token: 0x040005E5 RID: 1509
		public static readonly string CacheTotalSecodaryDataSize = "Total Secodary Data Size";

		// Token: 0x040005E6 RID: 1510
		public static readonly string CacheTotalSecodaryDataSizeHelp = ConfigManager.GetString("PerfmonCacheTotalSecondaryDataSizeHelp");

		// Token: 0x040005E7 RID: 1511
		public static readonly string CacheTotalEvictionRuns = "Total Eviction Runs";

		// Token: 0x040005E8 RID: 1512
		public static readonly string CacheTotalEvictionRunsHelp = ConfigManager.GetString("PerfmonCacheTotalEvictionRunsHelp");

		// Token: 0x040005E9 RID: 1513
		public static readonly string CacheWBFlushedItemCount = "Total Items Written Behind";

		// Token: 0x040005EA RID: 1514
		public static readonly string CacheWBDroppedItemCount = "Total Items Dropped";

		// Token: 0x040005EB RID: 1515
		public static readonly string CacheWBQueueCount = "Write Behind Queue Count";

		// Token: 0x040005EC RID: 1516
		public static readonly string CacheRTPendingCount = "Read Through Queue Count";

		// Token: 0x040005ED RID: 1517
		public static readonly string CacheRTMissingCount = "Total Read Through Misses";

		// Token: 0x040005EE RID: 1518
		public static readonly string CacheRTErrorCount = "Total Read Through Errors";

		// Token: 0x040005EF RID: 1519
		public static readonly string CacheRTSuccessCount = "Total Read Through Items";

		// Token: 0x040005F0 RID: 1520
		public static readonly string PerSecondaryCounterCategory = "Windows Azure Caching:Secondary Host";

		// Token: 0x040005F1 RID: 1521
		public static readonly string PerSecondaryCounterCategoryHelp = ConfigManager.GetString("PerfmonPerSecondaryMachineCategoryHelp");

		// Token: 0x040005F2 RID: 1522
		public static readonly string PerSecondaryTotalReplicationRetries = "Total Replication Retries";

		// Token: 0x040005F3 RID: 1523
		public static readonly string PerSecondaryTotalReplicationRetriesHelp = ConfigManager.GetString("PerfmonPerSecondaryTotalReplicationRetriesHelp");

		// Token: 0x040005F4 RID: 1524
		internal static bool Win64BitInstallation = IntPtr.Size == 8;

		// Token: 0x040005F5 RID: 1525
		public static readonly TimeSpan MAX_GENERATION_INTERVAL = TimeSpan.FromDays(1.0);

		// Token: 0x040005F6 RID: 1526
		public static readonly TimeSpan MIN_GENERATION_INTERVAL = TimeSpan.FromSeconds(5.0);

		// Token: 0x040005F7 RID: 1527
		public static long CodeVersion;

		// Token: 0x040005F8 RID: 1528
		public static ServerInformation ServerInfo;

		// Token: 0x040005F9 RID: 1529
		internal static long CodeVersion3_GettingClientVersion = 3L;

		// Token: 0x040005FA RID: 1530
		public static Version StoreVersion1000 = new Version(1, 0, 0, 0);

		// Token: 0x040005FB RID: 1531
		public static readonly Version StoreVersion2000 = new Version(2, 0, 0, 0);

		// Token: 0x040005FC RID: 1532
		public static readonly Version StoreVersion3000 = new Version(3, 0, 0, 0);

		// Token: 0x040005FD RID: 1533
		public static bool IsTestingMode;

		// Token: 0x040005FE RID: 1534
		internal static ClientLocationType DefaultClientLocationType = ClientLocationType.None;

		// Token: 0x040005FF RID: 1535
		internal static TimeSpan LookupTableRequestTimeout = TimeSpan.FromSeconds(30.0);

		// Token: 0x04000600 RID: 1536
		internal static int DefaultOptimalConnectionCount = 8;

		// Token: 0x04000601 RID: 1537
		internal static DataCacheDeploymentMode CurrentClusterDeploymentMode = DataCacheDeploymentMode.Unknown;

		// Token: 0x04000602 RID: 1538
		internal static readonly string[] DedicatedEndpointPrefixes = new string[] { "Microsoft.WindowsAzure.Plugins.Caching" + '.' };

		// Token: 0x04000603 RID: 1539
		public static bool IgnoreHighAvailabilityCheck = false;

		// Token: 0x04000604 RID: 1540
		public static int LargeObjectHeapCheckSize = 84987;

		// Token: 0x020000FA RID: 250
		internal static class Index
		{
			// Token: 0x020000FB RID: 251
			internal static class RootLevelHashTable
			{
				// Token: 0x04000605 RID: 1541
				public static readonly int RootDirectorySizeInBits = 2;

				// Token: 0x04000606 RID: 1542
				public static readonly int SubDirectorySizeInBits = 4;
			}

			// Token: 0x020000FC RID: 252
			internal static class SubLevelHashTable
			{
				// Token: 0x04000607 RID: 1543
				public static readonly int RootDirectorySizeInBits = 4;

				// Token: 0x04000608 RID: 1544
				public static readonly int SubDirectorySizeInBits = 4;
			}
		}
	}
}
