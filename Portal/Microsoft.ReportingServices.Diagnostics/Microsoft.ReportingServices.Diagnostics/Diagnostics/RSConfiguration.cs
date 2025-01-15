using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Editions;
using Microsoft.ReportingServices.Exceptions;
using RSRemoteRpcClient;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000039 RID: 57
	[Serializable]
	internal class RSConfiguration : RSBaseConfiguration, IParameterSource, IRSPortalConfiguration
	{
		// Token: 0x0600012D RID: 301 RVA: 0x00004A7C File Offset: 0x00002C7C
		public static bool ParseUrlPrefix(string url, out string scheme, out string host, out string port, out string prefix, out string path)
		{
			scheme = null;
			prefix = null;
			path = null;
			host = null;
			port = null;
			Match match = Regex.Match(url, "^(?<prefix>(?<scheme>.+)://(?<host>[^/]+):(?<port>[0-9]+))/?(?<path>.*)");
			if (!match.Success)
			{
				return false;
			}
			scheme = match.Groups["scheme"].Value;
			prefix = match.Groups["prefix"].Value;
			path = match.Groups["path"].Value;
			host = match.Groups["host"].Value;
			port = match.Groups["port"].Value;
			return true;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00004B28 File Offset: 0x00002D28
		internal RSConfiguration(string configFileName, string configLocation)
		{
			this.m_configFileName = configFileName;
			this.m_configLocation = configLocation;
			this.Init();
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00004BA8 File Offset: 0x00002DA8
		private void Init()
		{
			this.m_sequence = 0L;
			this.m_extensions = null;
			this.m_pairs = null;
			this.m_dsn = null;
			this.m_events = new Dictionary<string, EventExtension>();
			this.m_connectionType = RSBaseConfiguration.CatalogConnectionType.Default;
			this.m_connectionAuth = RSBaseConfiguration.CatalogConnectionAuth.Sql;
			this.m_encryptedCatalogUser = null;
			this.m_encryptedCatalogDomain = null;
			this.m_encryptedCatalogCred = null;
			this.m_defaultViewerStyle = null;
			this.m_displayErrorLink = true;
			this.m_WebServiceUseFileShareStorage = SnapshotTemporaryStorage.False;
			this.m_isSchedulingService = false;
			this.m_isNotificationService = false;
			this.m_isEventService = false;
			this.m_pollingInterval = 10;
			this.m_maxCatalogConnectionPoolSizePerProcess = 0;
			this.m_memoryLimit = 0.8f;
			this.m_maxMemoryLimit = 0.9f;
			this.m_recycleTime = new TimeSpan(0, 720, 0);
			this.m_maxAppDomainUnloadTime = new TimeSpan(0, 30, 0);
			this.m_maxTimedAppDomainUnload = new TimeSpan(0, 90, 0);
			this.m_maxQueueThreads = 0;
			this.m_urlRoot = "";
			this.m_encryptedSurrogateUserName = null;
			this.m_encryptedSurrogateDomain = null;
			this.m_encryptedSurrogatePassword = null;
			this.m_policyLevel = null;
			this.m_fileShareStoragePath = null;
			this.m_windowsServiceUserFileShareStorage = SnapshotTemporaryStorage.True;
			this.m_webServiceAccount = null;
			this.m_isWebServiceEnabled = true;
			this.m_isReportBuilderAnonymousAccessEnabled = false;
			this.m_isRdceEnabled = false;
			this.m_serviceApplicationId = Guid.Empty;
			this.m_workingSetMin = 0L;
			this.m_workingSetMax = 0L;
			this.m_serverDirectory = null;
			this.m_serverUrl = null;
			this.m_serverExternalUrl = null;
			this.m_loginUrl = null;
			this.m_useSSL = false;
			this.m_enableReportDesignClientButton = true;
			this.m_pageCountMode = null;
			this.m_postbackTimeout = 60;
			this.m_passthroughCookies = null;
			this.m_urlConfiguration = null;
			this.m_authenticationTypes = AuthenticationTypes.None;
			this.m_logonMethod = LogonMethod.Cleartext;
			this.m_authRealm = null;
			this.m_authDomain = null;
			this.m_authPersistence = true;
			this.m_maxUnauthenticatedRequests = 50;
			this.m_unauthenticatedRequestWindow = 10;
			this.m_unauthenticatedRequestLockoutTime = 60;
			this.m_authTokenCacheMaxSize = 600;
			this.m_authTokenCacheMaintenanceInterval = 600;
			this.m_authTokenCacheLogonTimeout = 0;
			this.m_authTokenCacheEntryTimeout = 540;
			this.m_requiredHttpsLevel = 1;
			this.m_disableSecureFormsAuthenticationCookie = false;
			this.m_maxActiveReqForOneUser = 20;
			this.m_maxScheduleWait = 1;
			this.m_DBQueryTimeout = 30;
			this.m_processTimeout = 150;
			this.m_processTimeoutGcExtension = 30;
			this.m_DBCleanupTimeout = 420;
			this.m_DBCleanupBatchFactor = 1000;
			this.m_connectionTimeout = 120;
			this.m_installationID = null;
			this.m_instanceID = null;
			this.m_rpcEndpoint = null;
			this.m_windowsServiceName = null;
			this.m_RecycleProcessOnSevereErrors = RSConfiguration.RecycleOptions.Recycle;
			this.m_RunningRequestsScavengerCycleParam = 30;
			this.m_RunningRequestsDBCycleParam = 30;
			this.m_RunningRequestsAgeParam = 30;
			this.m_CleanupCycleMinutesParam = 10;
			this.m_dailyCleanupMinuteOfDayParam = 120;
			this.m_AlertingCleanupCycleMinutesParam = 20;
			this.m_AlertingDataCleanupMinutesParam = 360;
			this.m_AlertingExecutionLogCleanupMinutesParam = 10080;
			this.m_AlertingMaxDataRetentionDaysParam = 180;
			this.m_enableRemoteErrors = false;
			this.m_execLogLevel = ExecutionLogLevel.Normal;
			this.m_rdlxSessionTimeout = 600;
			this.m_rdlxExecutionTimeout = 900;
			this.m_globalConnectionPoolEvictionTimeout = 1.0;
			this.m_asOnPremConnectionPoolEvictionTimeout = 0.083;
			this.m_watsonFlags = 1064;
			this.m_watsonDumpOnExceptions = RSConfiguration._DefaultWatsonDumpOnExceptions;
			this.m_watsonDumpExcludeIfContainsExceptions = RSConfiguration._DefaultWatsonDumpExcludeIfContainsExceptions;
			this.m_requestCacheSlots = 16;
			this.m_rdlSandboxing = null;
			this.m_mapTileServerConfiguration = null;
			this.m_extendedProtectionLevel = ExtendedProtectionLevel.Invalid;
			this.m_extendedProtectionScenario = ExtendedProtectionScenario.Invalid;
			this.m_enablePowerBIFeatures = false;
			this.m_powerBIConnectionConfiguration = null;
			this.m_UsernameSIDRefreshMinutesParam = 360;
			this.m_UserNameSIDRefreshSQLTimeout = 1800;
			this.m_UpdatePolicySecondsParam = 30;
			this.m_UpdatePoliciesChunkSizeParam = 500;
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00004F24 File Offset: 0x00003124
		public NameValueCollection Settings
		{
			get
			{
				if (this.m_pairs == null)
				{
					this.m_pairs = new NameValueCollection();
				}
				return this.m_pairs;
			}
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00004F3F File Offset: 0x0000313F
		public string GetSourceNameForTrace()
		{
			return "Configuration file";
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00004F46 File Offset: 0x00003146
		public string GetParameter(string name)
		{
			return null;
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00004F49 File Offset: 0x00003149
		public bool UseExternalStore
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00004F4C File Offset: 0x0000314C
		// (set) Token: 0x06000135 RID: 309 RVA: 0x00004F54 File Offset: 0x00003154
		public override long CurrentSequence
		{
			get
			{
				return this.m_sequence;
			}
			set
			{
				this.m_sequence = value;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00004F5D File Offset: 0x0000315D
		public ExtensionsConfiguration Extensions
		{
			get
			{
				return this.m_extensions;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00004F65 File Offset: 0x00003165
		public override Dictionary<RunningApplication, UrlConfiguration> UrlConfiguration
		{
			get
			{
				return this.m_urlConfiguration;
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00004F70 File Offset: 0x00003170
		internal static string DecryptConfigData(string encryptedData, string elementParent, string elementName)
		{
			string text = elementParent + "\\" + elementName;
			return RSConfiguration.DecryptConfigData(encryptedData, text);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00004F94 File Offset: 0x00003194
		internal static string DecryptConfigData(string encryptedData, string element)
		{
			string text = null;
			try
			{
				text = MachineKeyEncryption.Instance.DecryptToString(encryptedData);
			}
			catch (Exception ex)
			{
				throw new FailedToDecryptConfigInformationException(ex, element);
			}
			return text;
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00004FCC File Offset: 0x000031CC
		public bool ConnectionStringSet
		{
			get
			{
				return !string.IsNullOrEmpty(this.m_dsn);
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00004FDC File Offset: 0x000031DC
		public override string BaseConnectionString
		{
			get
			{
				if (string.IsNullOrEmpty(this.m_dsn))
				{
					throw new ServerConfigurationErrorException(null, "No DSN present in configuration file");
				}
				return RSConfiguration.DecryptConfigData(this.m_dsn, "Dsn");
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00005007 File Offset: 0x00003207
		public override RSBaseConfiguration.CatalogConnectionType ConnectionType
		{
			get
			{
				return this.m_connectionType;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600013D RID: 317 RVA: 0x0000500F File Offset: 0x0000320F
		public override RSBaseConfiguration.CatalogConnectionAuth ConnectionAuth
		{
			get
			{
				return this.m_connectionAuth;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00005017 File Offset: 0x00003217
		public string EncryptedCatalogUser
		{
			get
			{
				return this.m_encryptedCatalogUser;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600013F RID: 319 RVA: 0x0000501F File Offset: 0x0000321F
		public override string CatalogUser
		{
			get
			{
				return RSConfiguration.DecryptConfigData(this.EncryptedCatalogUser, "LogonUser");
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00005031 File Offset: 0x00003231
		public string EncryptedCatalogDomain
		{
			get
			{
				return this.m_encryptedCatalogDomain;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00005039 File Offset: 0x00003239
		public override string CatalogDomain
		{
			get
			{
				return RSConfiguration.DecryptConfigData(this.EncryptedCatalogDomain, "LogonDomain");
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000142 RID: 322 RVA: 0x0000504B File Offset: 0x0000324B
		public string EncryptedCatalogCred
		{
			get
			{
				return this.m_encryptedCatalogCred;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00005053 File Offset: 0x00003253
		public override string CatalogCred
		{
			get
			{
				return RSConfiguration.DecryptConfigData(this.EncryptedCatalogCred, "LogonCred");
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00005065 File Offset: 0x00003265
		public string DefaultViewerStyleSheet
		{
			get
			{
				return this.m_defaultViewerStyle;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000145 RID: 325 RVA: 0x0000506D File Offset: 0x0000326D
		public bool DisplayErrorLink
		{
			get
			{
				return this.m_displayErrorLink;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00005075 File Offset: 0x00003275
		public SnapshotTemporaryStorage WebServiceUseFileShareStore
		{
			get
			{
				return this.m_WebServiceUseFileShareStorage;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000147 RID: 327 RVA: 0x0000507D File Offset: 0x0000327D
		// (set) Token: 0x06000148 RID: 328 RVA: 0x00005085 File Offset: 0x00003285
		public Guid SharePointApplicationId
		{
			get
			{
				return this.m_serviceApplicationId;
			}
			set
			{
				this.m_serviceApplicationId = value;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000149 RID: 329 RVA: 0x0000508E File Offset: 0x0000328E
		public Dictionary<string, EventExtension> EventTypes
		{
			get
			{
				return this.m_events;
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00005096 File Offset: 0x00003296
		public bool IsSupportedEvent(string eventName)
		{
			return this.EventTypes.ContainsKey(eventName) || eventName.Equals("DataModelRefresh");
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600014B RID: 331 RVA: 0x000050B3 File Offset: 0x000032B3
		public bool IsSchedulingService
		{
			get
			{
				return this.m_isSchedulingService;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600014C RID: 332 RVA: 0x000050BB File Offset: 0x000032BB
		public bool IsAlertingService
		{
			get
			{
				return this.m_isAlertingService;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600014D RID: 333 RVA: 0x000050C3 File Offset: 0x000032C3
		public SnapshotTemporaryStorage WindowsServiceUseFileShareStorage
		{
			get
			{
				return this.m_windowsServiceUserFileShareStorage;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600014E RID: 334 RVA: 0x000050CB File Offset: 0x000032CB
		public bool IsNotificationService
		{
			get
			{
				return this.m_isNotificationService;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600014F RID: 335 RVA: 0x000050D3 File Offset: 0x000032D3
		public bool IsEventService
		{
			get
			{
				return this.m_isEventService;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000150 RID: 336 RVA: 0x000050DB File Offset: 0x000032DB
		public int PollingInterval
		{
			get
			{
				return this.m_pollingInterval;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000151 RID: 337 RVA: 0x000050E3 File Offset: 0x000032E3
		public int MaxCatalogConnectionPoolSizePerProcess
		{
			get
			{
				return this.m_maxCatalogConnectionPoolSizePerProcess;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000152 RID: 338 RVA: 0x000050EB File Offset: 0x000032EB
		public float MemoryLimit
		{
			get
			{
				return this.m_memoryLimit;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000153 RID: 339 RVA: 0x000050F3 File Offset: 0x000032F3
		public float MaxMemoryLimit
		{
			get
			{
				return this.m_maxMemoryLimit;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000154 RID: 340 RVA: 0x000050FB File Offset: 0x000032FB
		public long WorkingSetMax
		{
			get
			{
				return this.m_workingSetMax;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00005103 File Offset: 0x00003303
		public long WorkingSetMin
		{
			get
			{
				return this.m_workingSetMin;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000156 RID: 342 RVA: 0x0000510B File Offset: 0x0000330B
		public TimeSpan RecycleTime
		{
			get
			{
				return this.m_recycleTime;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00005113 File Offset: 0x00003313
		public string WebServiceAccount
		{
			get
			{
				return this.m_webServiceAccount;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000158 RID: 344 RVA: 0x0000511B File Offset: 0x0000331B
		public override bool IsWebServiceEnabled
		{
			get
			{
				return this.m_isWebServiceEnabled;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00005123 File Offset: 0x00003323
		public override bool IsReportBuilderAnonymousAccessEnabled
		{
			get
			{
				return this.m_isReportBuilderAnonymousAccessEnabled;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600015A RID: 346 RVA: 0x0000512B File Offset: 0x0000332B
		public bool IsRdceEnabled
		{
			get
			{
				return this.m_isRdceEnabled;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00005133 File Offset: 0x00003333
		public TimeSpan MaxAppDomainUnloadTime
		{
			get
			{
				return this.m_maxAppDomainUnloadTime;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600015C RID: 348 RVA: 0x0000513B File Offset: 0x0000333B
		public TimeSpan MaxTimedAppDomainUnload
		{
			get
			{
				return this.m_maxTimedAppDomainUnload;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00005143 File Offset: 0x00003343
		public int MaxQueueThreads
		{
			get
			{
				return this.m_maxQueueThreads;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600015E RID: 350 RVA: 0x0000514B File Offset: 0x0000334B
		private string UrlRoot
		{
			get
			{
				return this.m_urlRoot;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00005153 File Offset: 0x00003353
		public string PolicyLevel
		{
			get
			{
				return this.m_policyLevel;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000160 RID: 352 RVA: 0x0000515B File Offset: 0x0000335B
		public string FileShareStoragePath
		{
			get
			{
				return this.m_fileShareStoragePath;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00005163 File Offset: 0x00003363
		public bool IsSurrogatePresent
		{
			get
			{
				return this.m_encryptedSurrogateUserName != null && this.m_encryptedSurrogateUserName != string.Empty;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000162 RID: 354 RVA: 0x0000517F File Offset: 0x0000337F
		public string SurrogateUserName
		{
			get
			{
				return RSConfiguration.DecryptConfigData(this.m_encryptedSurrogateUserName, "UnattendedExecutionAccount\\UserName");
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00005191 File Offset: 0x00003391
		public string SurrogatePassword
		{
			get
			{
				return RSConfiguration.DecryptConfigData(this.m_encryptedSurrogatePassword, "UnattendedExecutionAccount\\Password");
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000164 RID: 356 RVA: 0x000051A3 File Offset: 0x000033A3
		public string SurrogateDomain
		{
			get
			{
				return RSConfiguration.DecryptConfigData(this.m_encryptedSurrogateDomain, "UnattendedExecutionAccount\\Domain");
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000165 RID: 357 RVA: 0x000051B5 File Offset: 0x000033B5
		public bool IsFileShareAccountPresent
		{
			get
			{
				return !string.IsNullOrEmpty(this.m_encryptedFileShareUserName);
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000166 RID: 358 RVA: 0x000051C5 File Offset: 0x000033C5
		public string FileShareUserName
		{
			get
			{
				return RSConfiguration.DecryptConfigData(this.m_encryptedFileShareUserName, "DefaultFileShareAccount", "UserName");
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000167 RID: 359 RVA: 0x000051DC File Offset: 0x000033DC
		public string FileSharePassword
		{
			get
			{
				return RSConfiguration.DecryptConfigData(this.m_encryptedFileSharePassword, "DefaultFileShareAccount", "Password");
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000168 RID: 360 RVA: 0x000051F3 File Offset: 0x000033F3
		public string FileShareDomain
		{
			get
			{
				return RSConfiguration.DecryptConfigData(this.m_encryptedFileShareDomain, "DefaultFileShareAccount", "Domain");
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000169 RID: 361 RVA: 0x0000520A File Offset: 0x0000340A
		public bool IsPowerBIAccountPresent
		{
			get
			{
				return !string.IsNullOrEmpty(this.m_encryptedPowerBIUserName);
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600016A RID: 362 RVA: 0x0000521A File Offset: 0x0000341A
		public string PowerBIUserName
		{
			get
			{
				return RSConfiguration.DecryptConfigData(this.m_encryptedPowerBIUserName, "DefaultFileShareAccount", "UserName");
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00005231 File Offset: 0x00003431
		public string PowerBIPassword
		{
			get
			{
				return RSConfiguration.DecryptConfigData(this.m_encryptedPowerBIPassword, "DefaultFileShareAccount", "Password");
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00005248 File Offset: 0x00003448
		public string PowerBIDomain
		{
			get
			{
				return RSConfiguration.DecryptConfigData(this.m_encryptedPowerBIDomain, "DefaultFileShareAccount", "Domain");
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600016D RID: 365 RVA: 0x0000525F File Offset: 0x0000345F
		public string ReportServerVirtualDirectory
		{
			get
			{
				return this.m_serverDirectory;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00005267 File Offset: 0x00003467
		private string ReportServerUrl
		{
			get
			{
				return this.m_serverUrl;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600016F RID: 367 RVA: 0x0000526F File Offset: 0x0000346F
		private string ReportServerExternalUrl
		{
			get
			{
				return this.m_serverExternalUrl;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00005277 File Offset: 0x00003477
		public string LoginPageUrl
		{
			get
			{
				return this.m_loginUrl;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000171 RID: 369 RVA: 0x0000527F File Offset: 0x0000347F
		public bool LoginPageUseSSL
		{
			get
			{
				return this.m_useSSL;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00005287 File Offset: 0x00003487
		public StringCollection PassthroughCookies
		{
			get
			{
				return this.m_passthroughCookies;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000173 RID: 371 RVA: 0x0000528F File Offset: 0x0000348F
		public bool EnableReportDesignClientButton
		{
			get
			{
				return this.m_enableReportDesignClientButton;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00005297 File Offset: 0x00003497
		public string PageCountMode
		{
			get
			{
				return this.m_pageCountMode;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000175 RID: 373 RVA: 0x0000529F File Offset: 0x0000349F
		public int PostbackTimeout
		{
			get
			{
				return this.m_postbackTimeout;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000176 RID: 374 RVA: 0x000052A7 File Offset: 0x000034A7
		public override AuthenticationTypes AuthenticationTypes
		{
			get
			{
				return this.m_authenticationTypes;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000177 RID: 375 RVA: 0x000052AF File Offset: 0x000034AF
		public override LogonMethod LogonMethod
		{
			get
			{
				return this.m_logonMethod;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000178 RID: 376 RVA: 0x000052B7 File Offset: 0x000034B7
		public override string AuthRealm
		{
			get
			{
				if ((this.m_authenticationTypes & AuthenticationTypes.RSWindowsBasic) != AuthenticationTypes.None && this.m_authRealm == null)
				{
					return string.Empty;
				}
				return this.m_authRealm;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000179 RID: 377 RVA: 0x000052D7 File Offset: 0x000034D7
		public override string AuthDomain
		{
			get
			{
				return this.m_authDomain;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600017A RID: 378 RVA: 0x000052DF File Offset: 0x000034DF
		public override bool AuthPersistence
		{
			get
			{
				return this.m_authPersistence;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600017B RID: 379 RVA: 0x000052E7 File Offset: 0x000034E7
		public int MaxUnauthenticatedRequests
		{
			get
			{
				return this.m_maxUnauthenticatedRequests;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600017C RID: 380 RVA: 0x000052EF File Offset: 0x000034EF
		public int UnauthenticatedRequestWindow
		{
			get
			{
				return this.m_unauthenticatedRequestWindow;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600017D RID: 381 RVA: 0x000052F7 File Offset: 0x000034F7
		public int UnauthenticatedRequestLockoutTime
		{
			get
			{
				return this.m_unauthenticatedRequestLockoutTime;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600017E RID: 382 RVA: 0x000052FF File Offset: 0x000034FF
		public int AuthTokenCacheMaxSize
		{
			get
			{
				return this.m_authTokenCacheMaxSize;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00005307 File Offset: 0x00003507
		public int AuthTokenCacheMaintenanceInterval
		{
			get
			{
				return this.m_authTokenCacheMaintenanceInterval;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000180 RID: 384 RVA: 0x0000530F File Offset: 0x0000350F
		public int AuthTokenCacheLogonTimeout
		{
			get
			{
				return this.m_authTokenCacheLogonTimeout;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00005317 File Offset: 0x00003517
		public int AuthTokenCacheEntryTimeout
		{
			get
			{
				return this.m_authTokenCacheEntryTimeout;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000182 RID: 386 RVA: 0x0000531F File Offset: 0x0000351F
		public string Hostname
		{
			get
			{
				return this.m_hostname;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00005327 File Offset: 0x00003527
		public override int RequestCacheSlots
		{
			get
			{
				return this.m_requestCacheSlots;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000184 RID: 388 RVA: 0x0000532F File Offset: 0x0000352F
		public override ExtendedProtectionLevel ExtendedProtectionLevel
		{
			get
			{
				return this.m_extendedProtectionLevel;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00005337 File Offset: 0x00003537
		public override ExtendedProtectionScenario ExtendedProtectionScenario
		{
			get
			{
				return this.m_extendedProtectionScenario;
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00005340 File Offset: 0x00003540
		public void Load(ConfigurationPropertyBag properties, bool resetProperties)
		{
			if (resetProperties)
			{
				this.Init();
			}
			if (properties == null)
			{
				return;
			}
			foreach (KeyValuePair<ConfigurationProperty, ConfigurationPropertyInfo> keyValuePair in properties)
			{
				object value = keyValuePair.Value.Value;
				if (value != null)
				{
					switch (keyValuePair.Key)
					{
					case ConfigurationProperty.CurrentSequence:
						this.m_sequence = (long)value;
						continue;
					case ConfigurationProperty.MiscellaneousProperty:
						this.m_pairs = (NameValueCollection)value;
						continue;
					case ConfigurationProperty.AuthenticationTypes:
						this.m_authenticationTypes = (AuthenticationTypes)value;
						continue;
					case ConfigurationProperty.LogonMethod:
						this.m_logonMethod = (LogonMethod)value;
						continue;
					case ConfigurationProperty.AuthRealm:
						this.m_authRealm = (string)value;
						continue;
					case ConfigurationProperty.AuthDomain:
						this.m_authDomain = (string)value;
						continue;
					case ConfigurationProperty.AuthPersistence:
						this.m_authPersistence = (bool)value;
						continue;
					case ConfigurationProperty.MaxUnauthenticatedRequests:
						this.m_maxUnauthenticatedRequests = (int)value;
						continue;
					case ConfigurationProperty.UnauthenticatedRequestWindow:
						this.m_unauthenticatedRequestWindow = (int)value;
						continue;
					case ConfigurationProperty.UnauthenticatedRequestLockoutTime:
						this.m_unauthenticatedRequestLockoutTime = (int)value;
						continue;
					case ConfigurationProperty.LoginPageUrl:
						this.m_loginUrl = (string)value;
						continue;
					case ConfigurationProperty.LoginPageUseSSL:
						this.m_useSSL = (bool)value;
						continue;
					case ConfigurationProperty.PassthroughCookies:
						this.m_passthroughCookies = (StringCollection)value;
						continue;
					case ConfigurationProperty.AuthTokenCacheMaxSize:
						this.m_authTokenCacheMaxSize = (int)value;
						continue;
					case ConfigurationProperty.AuthTokenCacheMaintenanceInterval:
						this.m_authTokenCacheMaintenanceInterval = (int)value;
						continue;
					case ConfigurationProperty.AuthTokenCacheLogonTimeout:
						this.m_authTokenCacheLogonTimeout = (int)value;
						continue;
					case ConfigurationProperty.AuthTokenCacheEntryTimeout:
						this.m_authTokenCacheEntryTimeout = (int)value;
						continue;
					case ConfigurationProperty.EnableConnectionKeepAlives:
					case ConfigurationProperty.ConnectionKeepAliveTimeOut:
					case ConfigurationProperty.MaxConcurrencyReportManager:
					case ConfigurationProperty.MaxConcurrencyWebService:
					case ConfigurationProperty.PhysicalPath:
					case ConfigurationProperty.Version:
					case ConfigurationProperty.EditionId:
					case ConfigurationProperty.EditionName:
					case ConfigurationProperty.InstanceName:
					case ConfigurationProperty.IsInitialized:
					case ConfigurationProperty.AutoFlush:
					case ConfigurationProperty.LogFileBufferSize:
					case ConfigurationProperty.HttpLogFileFields:
					case ConfigurationProperty.DaysToKeepLogs:
					case ConfigurationProperty.DatabaseVersion:
					case ConfigurationProperty.ExpectedDatabaseVersion:
					case ConfigurationProperty.DatabaseLogonTimeout:
					case ConfigurationProperty.PolicyLevelReportManager:
					case ConfigurationProperty.IsConfigurationService:
					case ConfigurationProperty.WindowsServiceIdentityActual:
					case ConfigurationProperty.WindowsServiceIdentityConfigured:
					case ConfigurationProperty.ServicePrincipalName:
					case ConfigurationProperty.WebServiceAccount:
					case ConfigurationProperty.RequestCacheSlots:
						continue;
					case ConfigurationProperty.Hostname:
						this.m_hostname = (string)value;
						continue;
					case ConfigurationProperty.ReportManagerReportServerUrl:
						this.m_serverUrl = (string)value;
						continue;
					case ConfigurationProperty.ReportManagerReportServerExternalUrl:
						this.m_serverExternalUrl = (string)value;
						continue;
					case ConfigurationProperty.MaxActiveReqForOneUser:
						this.m_maxActiveReqForOneUser = (int)value;
						continue;
					case ConfigurationProperty.MaxConcurrencyUnattendedExecution:
						this.m_maxQueueThreads = (int)value;
						continue;
					case ConfigurationProperty.InstanceId:
						this.m_instanceID = (string)value;
						continue;
					case ConfigurationProperty.InstallationID:
						this.m_installationID = (string)value;
						continue;
					case ConfigurationProperty.InstallationIDWebApp:
						this.m_installationIDWebApp = (string)value;
						continue;
					case ConfigurationProperty.AppDomainRecycleTime:
					{
						int num = (int)value;
						if (num == 0)
						{
							this.m_recycleTime = TimeSpan.MinValue;
							continue;
						}
						this.m_recycleTime = new TimeSpan(0, num, 0);
						continue;
					}
					case ConfigurationProperty.MaxAppDomainUnloadTime:
					{
						int num2 = (int)value;
						if (num2 == 0)
						{
							this.m_maxAppDomainUnloadTime = TimeSpan.MinValue;
							this.m_maxTimedAppDomainUnload = TimeSpan.MinValue;
							continue;
						}
						this.m_maxAppDomainUnloadTime = new TimeSpan(0, num2, 0);
						this.m_maxTimedAppDomainUnload = new TimeSpan(0, num2 * 3, 0);
						continue;
					}
					case ConfigurationProperty.WorkingSetMax:
					{
						long num3 = (long)value;
						this.m_workingSetMax = num3;
						continue;
					}
					case ConfigurationProperty.WorkingSetMin:
					{
						long num4 = (long)value;
						this.m_workingSetMin = num4;
						continue;
					}
					case ConfigurationProperty.MemorySafetyMargin:
					{
						int num5 = (int)value;
						if (num5 > 0)
						{
							this.m_memoryLimit = (float)num5 / 100f;
							continue;
						}
						continue;
					}
					case ConfigurationProperty.MemoryThreshold:
					{
						int num6 = (int)value;
						if (num6 > 0)
						{
							this.m_maxMemoryLimit = (float)num6 / 100f;
							continue;
						}
						continue;
					}
					case ConfigurationProperty.CleanupCycleMinutes:
						this.m_CleanupCycleMinutesParam = (int)value;
						continue;
					case ConfigurationProperty.CleanupCycleMinuteOfDay:
						this.m_dailyCleanupMinuteOfDayParam = (int)value;
						continue;
					case ConfigurationProperty.AlertingCleanupCycleMinutes:
						this.m_AlertingCleanupCycleMinutesParam = (int)value;
						continue;
					case ConfigurationProperty.AlertingDataCleanupMinutes:
						this.m_AlertingDataCleanupMinutesParam = (int)value;
						continue;
					case ConfigurationProperty.AlertingExecutionLogCleanupMinutes:
						this.m_AlertingExecutionLogCleanupMinutesParam = (int)value;
						continue;
					case ConfigurationProperty.AlertingMaxDataRetentionDays:
						this.m_AlertingMaxDataRetentionDaysParam = (int)value;
						continue;
					case ConfigurationProperty.RunningRequestsScavengerCycle:
						this.m_RunningRequestsScavengerCycleParam = (int)value;
						continue;
					case ConfigurationProperty.RunningRequestsDbCycle:
						this.m_RunningRequestsDBCycleParam = (int)value;
						continue;
					case ConfigurationProperty.RunningRequestsAge:
						this.m_RunningRequestsAgeParam = (int)value;
						continue;
					case ConfigurationProperty.MaxScheduleWait:
						this.m_maxScheduleWait = (int)value;
						continue;
					case ConfigurationProperty.PollingInterval:
						this.m_pollingInterval = (int)value;
						continue;
					case ConfigurationProperty.Dsn:
						break;
					case ConfigurationProperty.ConnectionType:
						this.m_connectionType = (RSBaseConfiguration.CatalogConnectionType)value;
						continue;
					case ConfigurationProperty.LogonUser:
						this.m_encryptedCatalogUser = (string)value;
						continue;
					case ConfigurationProperty.LogonDomain:
						this.m_encryptedCatalogDomain = (string)value;
						if (this.m_encryptedCatalogDomain != null)
						{
							this.m_connectionAuth = RSBaseConfiguration.CatalogConnectionAuth.Windows;
							continue;
						}
						continue;
					case ConfigurationProperty.LogonCred:
						this.m_encryptedCatalogCred = (string)value;
						continue;
					case ConfigurationProperty.DatabaseQueryTimeout:
						this.m_DBQueryTimeout = (int)value;
						continue;
					case ConfigurationProperty.ProcessTimeout:
						this.m_processTimeout = (int)value;
						continue;
					case ConfigurationProperty.ProcessTimeoutGcExtension:
						this.m_processTimeoutGcExtension = (int)value;
						continue;
					case ConfigurationProperty.DatabaseCleanupTimeout:
						this.m_DBCleanupTimeout = (int)value;
						continue;
					case ConfigurationProperty.DatabaseCleanupBatchFactor:
						this.m_DBCleanupBatchFactor = (int)value;
						continue;
					case ConfigurationProperty.ConnectionTimeout:
						this.m_connectionTimeout = (int)value;
						continue;
					case ConfigurationProperty.MaxCatalogConnectionPoolSizePerProcess:
						this.m_maxCatalogConnectionPoolSizePerProcess = (int)value;
						continue;
					case ConfigurationProperty.SecureConnectionRequired:
						this.m_requiredHttpsLevel = (int)value;
						continue;
					case ConfigurationProperty.DisplayErrorLink:
						this.m_displayErrorLink = (bool)value;
						continue;
					case ConfigurationProperty.UrlRoot:
						this.m_urlRoot = (string)value;
						continue;
					case ConfigurationProperty.EnableReportDesignClientButton:
						this.m_enableReportDesignClientButton = (bool)value;
						continue;
					case ConfigurationProperty.DefaultViewerStyleSheet:
						this.m_defaultViewerStyle = (string)value;
						continue;
					case ConfigurationProperty.PageCountMode:
						this.m_pageCountMode = (string)value;
						continue;
					case ConfigurationProperty.PostbackTimeout:
						this.m_postbackTimeout = (int)value;
						continue;
					case ConfigurationProperty.WebServiceUseFileShareStorage:
						this.m_WebServiceUseFileShareStorage = (SnapshotTemporaryStorage)value;
						continue;
					case ConfigurationProperty.WindowsServiceUseFileShareStorage:
						this.m_windowsServiceUserFileShareStorage = (SnapshotTemporaryStorage)value;
						continue;
					case ConfigurationProperty.FileShareStorageLocation:
						this.m_fileShareStoragePath = (string)value;
						continue;
					case ConfigurationProperty.PolicyLevelServer:
						this.m_policyLevel = (string)value;
						if (this.m_policyLevel != null && !Path.IsPathRooted(this.m_policyLevel))
						{
							this.m_policyLevel = Path.Combine(this.m_configLocation, this.m_policyLevel);
							continue;
						}
						continue;
					case ConfigurationProperty.IsWebService:
						this.m_isWebServiceEnabled = (bool)value;
						continue;
					case ConfigurationProperty.IsSchedulingService:
						this.m_isSchedulingService = (bool)value;
						continue;
					case ConfigurationProperty.IsNotificationService:
						this.m_isNotificationService = (bool)value;
						continue;
					case ConfigurationProperty.IsEventService:
						this.m_isEventService = (bool)value;
						continue;
					case ConfigurationProperty.IsRdceEnabled:
						this.m_isRdceEnabled = (bool)value;
						continue;
					case ConfigurationProperty.UnattendedExecutionAccountDomain:
						this.m_encryptedSurrogateDomain = (string)value;
						continue;
					case ConfigurationProperty.UnattendedExecutionAccountUser:
						this.m_encryptedSurrogateUserName = (string)value;
						continue;
					case ConfigurationProperty.UnattendedExecutionAccountPassword:
						this.m_encryptedSurrogatePassword = (string)value;
						continue;
					case ConfigurationProperty.WatsonFlags:
						this.m_watsonFlags = (int)value;
						continue;
					case ConfigurationProperty.WatsonDumpOnExceptions:
					{
						string[] array = ((string)value).Split(new char[] { ',' });
						this.m_watsonDumpOnExceptions = new StringCollection();
						this.m_watsonDumpOnExceptions.AddRange(array);
						continue;
					}
					case ConfigurationProperty.WatsonDumpExcludeIfContainsExceptions:
					{
						string[] array2 = ((string)value).Split(new char[] { ',' });
						this.m_watsonDumpExcludeIfContainsExceptions = new StringCollection();
						this.m_watsonDumpExcludeIfContainsExceptions.AddRange(array2);
						continue;
					}
					case ConfigurationProperty.Extensions:
					{
						this.m_extensions = (ExtensionsConfiguration)value;
						this.m_events.Clear();
						using (IEnumerator enumerator2 = this.m_extensions.Event.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								object obj = enumerator2.Current;
								EventExtension eventExtension = (EventExtension)obj;
								this.m_events.Add(eventExtension.EventType, eventExtension);
							}
							continue;
						}
						break;
					}
					case ConfigurationProperty.UrlConfiguration:
						this.m_urlConfiguration = (Dictionary<RunningApplication, UrlConfiguration>)value;
						if (this.m_urlConfiguration.ContainsKey(RunningApplication.WebService))
						{
							this.m_serverDirectory = this.m_urlConfiguration[RunningApplication.WebService].VirtualRoot;
							continue;
						}
						continue;
					case ConfigurationProperty.IsReportBuilderAnonymousAccessEnabled:
						this.m_isReportBuilderAnonymousAccessEnabled = (bool)value;
						continue;
					case ConfigurationProperty.RDLSandboxing:
						this.m_rdlSandboxing = new RDLSandboxingConfiguration();
						this.m_rdlSandboxing.Load((RDLSandboxingPropertyBag)value);
						continue;
					case ConfigurationProperty.MapTileServerConfiguration:
						this.m_mapTileServerConfiguration = new MapTileServerConfiguration();
						this.m_mapTileServerConfiguration.Load((MapTileServerConfigurationPropertyBag)value);
						continue;
					case ConfigurationProperty.ExtendedProtectionLevel:
						this.m_extendedProtectionLevel = (ExtendedProtectionLevel)value;
						continue;
					case ConfigurationProperty.ExtendedProtectionScenario:
						this.m_extendedProtectionScenario = (ExtendedProtectionScenario)value;
						continue;
					case ConfigurationProperty.IsAlertingService:
						this.m_isAlertingService = (bool)value;
						continue;
					case ConfigurationProperty.DisableSecureFormsAuthenticationCookie:
						this.m_disableSecureFormsAuthenticationCookie = (bool)value;
						continue;
					case ConfigurationProperty.FileShareAccountDomain:
						this.m_encryptedFileShareDomain = (string)value;
						continue;
					case ConfigurationProperty.FileShareAccountUser:
						this.m_encryptedFileShareUserName = (string)value;
						continue;
					case ConfigurationProperty.FileShareAccountPassword:
						this.m_encryptedFileSharePassword = (string)value;
						continue;
					case ConfigurationProperty.EnablePowerBIFeatures:
						this.m_enablePowerBIFeatures = (bool)value;
						continue;
					case ConfigurationProperty.PowerBIConnectionConfiguration:
						this.m_powerBIConnectionConfiguration = new PowerBIConfiguration();
						this.m_powerBIConnectionConfiguration.Load((OAuthConnectionConfigurationPropertyBag)value);
						continue;
					case ConfigurationProperty.UsernameSIDRefreshMinutes:
						this.m_UsernameSIDRefreshMinutesParam = (int)value;
						continue;
					case ConfigurationProperty.UpdatePoliciesSeconds:
						this.m_UpdatePolicySecondsParam = (int)value;
						continue;
					case ConfigurationProperty.UpdatePoliciesChunkSize:
						this.m_UpdatePoliciesChunkSizeParam = (int)value;
						continue;
					case ConfigurationProperty.OAuthConnectionConfiguration:
						this.m_oAuthConnectionConfiguration = new OAuthConnectionConfiguration();
						this.m_oAuthConnectionConfiguration.Load((OAuthConnectionConfigurationPropertyBag)value);
						continue;
					default:
						continue;
					}
					this.m_dsn = (string)value;
				}
			}
			this.CalculateProperties();
			this.ValidatePostLoad();
			this.CheckIfFeaturesEnabledBySku();
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00005DD4 File Offset: 0x00003FD4
		public void Validate(ConfigurationPropertyBag properties)
		{
			foreach (KeyValuePair<ConfigurationProperty, ConfigurationPropertyInfo> keyValuePair in properties)
			{
				ConfigurationPropertyInfo value = keyValuePair.Value;
				string specifiedValue = value.SpecifiedValue;
				switch (keyValuePair.Key)
				{
				case ConfigurationProperty.AuthenticationTypes:
				{
					AuthenticationTypes value2 = (AuthenticationTypes)new IntParameter(this, RSTrace.CatalogTrace, "AuthenticationTypes", specifiedValue, 0, "")
					{
						MinValue = 1,
						MaxValue = 127
					}.Value;
					if ((((value2 & (AuthenticationTypes.RSWindowsNegotiate | AuthenticationTypes.RSWindowsKerberos | AuthenticationTypes.RSWindowsNTLM | AuthenticationTypes.RSWindowsBasic)) != AuthenticationTypes.None) ? 1 : 0) + (((value2 & AuthenticationTypes.Custom) != AuthenticationTypes.None) ? 1 : 0) + (((value2 & AuthenticationTypes.RSForms) != AuthenticationTypes.None) ? 1 : 0) + (((value2 & AuthenticationTypes.OAuth) != AuthenticationTypes.None) ? 1 : 0) == 1)
					{
						value.Value = value2;
					}
					else if (RSTrace.ConfigManagerTracer.TraceWarning)
					{
						RSTrace.ConfigManagerTracer.Trace(TraceLevel.Warning, "Invalid authentication types 0x{0:x}", new object[] { value2 });
					}
					break;
				}
				case ConfigurationProperty.LogonMethod:
					value.Value = new IntParameter(this, RSTrace.CatalogTrace, "AuthenticationTypes", specifiedValue, 3, "")
					{
						MinValue = 2,
						MaxValue = 3
					}.Value;
					break;
				case ConfigurationProperty.AuthRealm:
				case ConfigurationProperty.AuthDomain:
					if (!string.IsNullOrEmpty(specifiedValue))
					{
						value.Value = specifiedValue;
					}
					break;
				case ConfigurationProperty.AuthPersistence:
				{
					BooleanParameter booleanParameter = new BooleanParameter(this, RSTrace.CatalogTrace, "EnableAuthPersistence", specifiedValue, true, "");
					value.Value = booleanParameter.Value;
					break;
				}
				case ConfigurationProperty.MaxUnauthenticatedRequests:
					value.Value = new IntParameter(this, RSTrace.CatalogTrace, "MaxUnauthenticatedRequests", specifiedValue, this.m_maxUnauthenticatedRequests, "")
					{
						MinValue = 1,
						MaxValue = int.MaxValue
					}.Value;
					break;
				case ConfigurationProperty.UnauthenticatedRequestWindow:
					value.Value = new IntParameter(this, RSTrace.CatalogTrace, "UnauthenticatedRequestWindow", specifiedValue, this.m_unauthenticatedRequestWindow, "second(s)")
					{
						MinValue = 1,
						MaxValue = 3600
					}.Value;
					break;
				case ConfigurationProperty.UnauthenticatedRequestLockoutTime:
					value.Value = new IntParameter(this, RSTrace.CatalogTrace, "UnauthenticatedRequestLockoutTime", specifiedValue, this.m_unauthenticatedRequestLockoutTime, "second(s)")
					{
						MinValue = 1,
						MaxValue = 60
					}.Value;
					break;
				case ConfigurationProperty.LoginPageUrl:
				{
					UrlParameter urlParameter = new UrlParameter(this, RSTrace.CatalogTrace, "loginUrl", specifiedValue, null, UriKind.RelativeOrAbsolute);
					value.Value = urlParameter.Value;
					break;
				}
				case ConfigurationProperty.LoginPageUseSSL:
				{
					BooleanParameter booleanParameter = new BooleanParameter(this, RSTrace.CatalogTrace, "UseSSL", specifiedValue, false, "");
					value.Value = booleanParameter.Value;
					break;
				}
				case ConfigurationProperty.AuthTokenCacheMaxSize:
					value.Value = new IntParameter(this, RSTrace.CatalogTrace, "AuthTokenCacheMaxSize", specifiedValue, this.m_authTokenCacheMaxSize, "")
					{
						MinValue = 0,
						MaxValue = 600
					}.Value;
					break;
				case ConfigurationProperty.AuthTokenCacheMaintenanceInterval:
					value.Value = new IntParameter(this, RSTrace.CatalogTrace, "AuthTokenCacheMaintenanceInterval", specifiedValue, this.m_authTokenCacheMaintenanceInterval, "second(s)")
					{
						MinValue = 1,
						MaxValue = 3600
					}.Value;
					break;
				case ConfigurationProperty.AuthTokenCacheLogonTimeout:
					value.Value = new IntParameter(this, RSTrace.CatalogTrace, "AuthTokenCacheLogonTimeout", specifiedValue, this.m_authTokenCacheLogonTimeout, "second(s)")
					{
						MinValue = 0,
						MaxValue = int.MaxValue
					}.Value;
					break;
				case ConfigurationProperty.AuthTokenCacheEntryTimeout:
					value.Value = new IntParameter(this, RSTrace.CatalogTrace, "AuthTokenCacheEntryTimeout", specifiedValue, this.m_authTokenCacheEntryTimeout, "second(s)")
					{
						MinValue = 1,
						MaxValue = int.MaxValue
					}.Value;
					break;
				case ConfigurationProperty.Hostname:
					value.Value = specifiedValue.Trim();
					break;
				case ConfigurationProperty.ReportManagerReportServerUrl:
				case ConfigurationProperty.ReportManagerReportServerExternalUrl:
					if (!string.IsNullOrEmpty(specifiedValue))
					{
						UrlParameter urlParameter2 = new UrlParameter(this, RSTrace.CatalogTrace, (keyValuePair.Key == ConfigurationProperty.ReportManagerReportServerUrl) ? "ReportServerUrl" : "ReportServerExternalUrl", specifiedValue, null, UriKind.Absolute);
						value.Value = urlParameter2.Value;
					}
					break;
				case ConfigurationProperty.MaxActiveReqForOneUser:
					value.Value = new IntParameter(this, RSTrace.CatalogTrace, "MaxActiveReqForOneUser", specifiedValue, this.m_maxActiveReqForOneUser, "requests(s)")
					{
						MinValue = 1
					}.Value;
					break;
				case ConfigurationProperty.MaxConcurrencyUnattendedExecution:
					value.Value = new IntParameter(this, RSTrace.CatalogTrace, "MaxQueueThreads", specifiedValue, 0, "thread(s)")
					{
						MinValue = 0,
						MaxValue = int.MaxValue
					}.Value;
					break;
				case ConfigurationProperty.InstanceId:
				case ConfigurationProperty.InstallationID:
				case ConfigurationProperty.InstallationIDWebApp:
				case ConfigurationProperty.Dsn:
				case ConfigurationProperty.LogonUser:
				case ConfigurationProperty.LogonDomain:
				case ConfigurationProperty.LogonCred:
				case ConfigurationProperty.DefaultViewerStyleSheet:
					if (!string.IsNullOrEmpty(specifiedValue))
					{
						value.Value = specifiedValue;
					}
					break;
				case ConfigurationProperty.AppDomainRecycleTime:
					value.Value = new IntParameter(this, RSTrace.CatalogTrace, "RecycleTime", specifiedValue, 0, "minute(s)")
					{
						MinValue = 0,
						MaxValue = int.MaxValue
					}.Value;
					break;
				case ConfigurationProperty.MaxAppDomainUnloadTime:
					value.Value = new IntParameter(this, RSTrace.CatalogTrace, "MaxAppDomainUnloadTime", specifiedValue, 0, "minute(s)")
					{
						MinValue = 0,
						MaxValue = int.MaxValue
					}.Value;
					break;
				case ConfigurationProperty.WorkingSetMax:
				case ConfigurationProperty.WorkingSetMin:
					value.Value = new LongParameter(this, RSTrace.CatalogTrace, (keyValuePair.Key == ConfigurationProperty.WorkingSetMax) ? "WorkingSetMaximum" : "WorkingSetMinimum", specifiedValue, 0L, "kilobytes")
					{
						MinValue = 0L,
						MaxValue = long.MaxValue
					}.Value;
					break;
				case ConfigurationProperty.MemorySafetyMargin:
					value.Value = new IntParameter(this, RSTrace.CatalogTrace, "MemorySafetyMargin", specifiedValue, 80, "percent")
					{
						MinValue = 0,
						MaxValue = 100
					}.Value;
					break;
				case ConfigurationProperty.MemoryThreshold:
					value.Value = new IntParameter(this, RSTrace.CatalogTrace, "MemoryThreshold", specifiedValue, 90, "percent")
					{
						MinValue = 0,
						MaxValue = 100
					}.Value;
					break;
				case ConfigurationProperty.CleanupCycleMinutes:
					value.Value = new IntParameter(this, RSTrace.CatalogTrace, "CleanupCycleMinutes", specifiedValue, this.m_CleanupCycleMinutesParam, "minute(s)")
					{
						MinValue = 1,
						MaxValue = int.MaxValue
					}.Value;
					break;
				case ConfigurationProperty.CleanupCycleMinuteOfDay:
					value.Value = new IntParameter(this, RSTrace.CatalogTrace, "DailyCleanupMinuteOfDay", specifiedValue, this.m_dailyCleanupMinuteOfDayParam, "minutes since midnight")
					{
						MinValue = 30,
						MaxValue = 1380
					}.Value;
					break;
				case ConfigurationProperty.AlertingCleanupCycleMinutes:
					value.Value = new IntParameter(this, RSTrace.AlertingRuntimeTracer, "AlertingCleanupCycleMinutes", specifiedValue, this.m_AlertingCleanupCycleMinutesParam, "minute(s)")
					{
						MinValue = 1,
						MaxValue = int.MaxValue
					}.Value;
					break;
				case ConfigurationProperty.AlertingDataCleanupMinutes:
					value.Value = new IntParameter(this, RSTrace.AlertingRuntimeTracer, "AlertingDataCleanupMinutes", specifiedValue, this.m_AlertingDataCleanupMinutesParam, "minute(s)")
					{
						MinValue = 1,
						MaxValue = int.MaxValue
					}.Value;
					break;
				case ConfigurationProperty.AlertingExecutionLogCleanupMinutes:
					value.Value = new IntParameter(this, RSTrace.AlertingRuntimeTracer, "AlertingExecutionLogCleanupMinutes", specifiedValue, this.m_AlertingExecutionLogCleanupMinutesParam, "minute(s)")
					{
						MinValue = 1,
						MaxValue = int.MaxValue
					}.Value;
					break;
				case ConfigurationProperty.AlertingMaxDataRetentionDays:
					value.Value = new IntParameter(this, RSTrace.AlertingRuntimeTracer, "AlertingMaxDataRetentionDays", specifiedValue, this.m_AlertingMaxDataRetentionDaysParam, "day(s)")
					{
						MinValue = 1,
						MaxValue = int.MaxValue
					}.Value;
					break;
				case ConfigurationProperty.RunningRequestsScavengerCycle:
					value.Value = new IntParameter(this, RSTrace.CatalogTrace, "RunningRequestsScavengerCycle", specifiedValue, this.m_RunningRequestsScavengerCycleParam, "second(s)")
					{
						MinValue = 1,
						MaxValue = int.MaxValue
					}.Value;
					break;
				case ConfigurationProperty.RunningRequestsDbCycle:
					value.Value = new IntParameter(this, RSTrace.CatalogTrace, "RunningRequestsDbCycle", specifiedValue, this.m_RunningRequestsDBCycleParam, "second(s)")
					{
						MinValue = 1,
						MaxValue = int.MaxValue
					}.Value;
					break;
				case ConfigurationProperty.RunningRequestsAge:
					value.Value = new IntParameter(this, RSTrace.CatalogTrace, "RunningRequestsAge", specifiedValue, this.m_RunningRequestsAgeParam, "second(s)")
					{
						MinValue = 1,
						MaxValue = int.MaxValue
					}.Value;
					break;
				case ConfigurationProperty.MaxScheduleWait:
					value.Value = new IntParameter(this, RSTrace.CatalogTrace, "MaxScheduleWait", specifiedValue, this.m_maxScheduleWait, "second(s)")
					{
						MinValue = 1,
						MaxValue = 60
					}.Value;
					break;
				case ConfigurationProperty.PollingInterval:
					value.Value = new IntParameter(this, RSTrace.CatalogTrace, "PollingInterval", specifiedValue, 10, "second(s)")
					{
						MinValue = 1,
						MaxValue = int.MaxValue
					}.Value;
					break;
				case ConfigurationProperty.ConnectionType:
				{
					string text = specifiedValue;
					try
					{
						text = ((int)((RSBaseConfiguration.CatalogConnectionType)Enum.Parse(typeof(RSBaseConfiguration.CatalogConnectionType), text, true))).ToString(CultureInfo.InvariantCulture);
					}
					catch (ArgumentException)
					{
					}
					value.Value = (RSBaseConfiguration.CatalogConnectionType)new IntParameter(this, RSTrace.CatalogTrace, "ConnectionType", text, 0, "")
					{
						MinValue = 0,
						MaxValue = 1
					}.Value;
					break;
				}
				case ConfigurationProperty.DatabaseQueryTimeout:
					value.Value = new IntParameter(this, RSTrace.CatalogTrace, "DatabaseQueryTimeout", specifiedValue, this.m_DBQueryTimeout, "second(s)")
					{
						MinValue = 0,
						MaxValue = int.MaxValue
					}.Value;
					break;
				case ConfigurationProperty.ProcessTimeout:
				{
					IntParameter intParameter = new IntParameter(this, RSTrace.CatalogTrace, "ProcessTimeout", specifiedValue, this.m_processTimeout, "second(s)");
					intParameter.MinValue = 0;
					intParameter.MaxValue = int.MaxValue;
					if (intParameter.Value != 0 && intParameter.Value < 150)
					{
						if (RSTrace.CatalogTrace.TraceWarning)
						{
							RSTrace.CatalogTrace.Trace(TraceLevel.Warning, "ProcessTimeout is {0} and is less than recommended minimum of {1}. Increasing ProcessTimeout to {1}.", new object[] { intParameter.Value, 150 });
						}
						value.Value = 150;
					}
					else
					{
						value.Value = intParameter.Value;
					}
					break;
				}
				case ConfigurationProperty.ProcessTimeoutGcExtension:
					value.Value = new IntParameter(this, RSTrace.CatalogTrace, "ProcessTimeoutGcExtension", specifiedValue, this.m_processTimeoutGcExtension, "second(s)")
					{
						MinValue = 0,
						MaxValue = int.MaxValue
					}.Value;
					break;
				case ConfigurationProperty.ConnectionTimeout:
					value.Value = new IntParameter(this, RSTrace.CatalogTrace, "ConnectionTimeout", specifiedValue, this.m_connectionTimeout, "second(s)")
					{
						MinValue = 1,
						MaxValue = int.MaxValue
					}.Value;
					break;
				case ConfigurationProperty.MaxCatalogConnectionPoolSizePerProcess:
					value.Value = new IntParameter(this, RSTrace.CatalogTrace, "MaxCatalogConnectionPoolSizePerProcess", specifiedValue, 0, " connections")
					{
						MinValue = 0,
						MaxValue = int.MaxValue
					}.Value;
					break;
				case ConfigurationProperty.SecureConnectionRequired:
					value.Value = new IntParameter(this, RSTrace.CatalogTrace, "SecureConnectionLevel", specifiedValue, this.m_requiredHttpsLevel, "")
					{
						MinValue = 0,
						MaxValue = 3
					}.Value;
					break;
				case ConfigurationProperty.DisplayErrorLink:
				{
					BooleanParameter booleanParameter = new BooleanParameter(this, RSTrace.CatalogTrace, "DisplayErrorLink", specifiedValue, this.m_displayErrorLink, "");
					value.Value = booleanParameter.Value;
					break;
				}
				case ConfigurationProperty.UrlRoot:
				{
					UrlParameter urlParameter3 = new UrlParameter(this, RSTrace.CatalogTrace, "UrlRoot", specifiedValue, "", UriKind.Absolute);
					value.Value = urlParameter3.Value;
					break;
				}
				case ConfigurationProperty.EnableReportDesignClientButton:
				{
					BooleanParameter booleanParameter = new BooleanParameter(this, RSTrace.CatalogTrace, "EnableReportDesignClientButton", specifiedValue, true, "");
					value.Value = booleanParameter.Value;
					break;
				}
				case ConfigurationProperty.PageCountMode:
					if (!string.IsNullOrEmpty(specifiedValue))
					{
						value.Value = specifiedValue;
					}
					break;
				case ConfigurationProperty.PostbackTimeout:
					value.Value = new IntParameter(this, RSTrace.CatalogTrace, "PostbackTimeout", specifiedValue, 60, "minutes")
					{
						MinValue = 1,
						MaxValue = int.MaxValue
					}.Value;
					break;
				case ConfigurationProperty.WebServiceUseFileShareStorage:
				{
					EnumParameter<SnapshotTemporaryStorage> enumParameter = new EnumParameter<SnapshotTemporaryStorage>(this, RSTrace.CatalogTrace, "WebServiceUseFileShareStorage", specifiedValue, SnapshotTemporaryStorage.False, "");
					value.Value = enumParameter.Value;
					break;
				}
				case ConfigurationProperty.WindowsServiceUseFileShareStorage:
				{
					EnumParameter<SnapshotTemporaryStorage> enumParameter2 = new EnumParameter<SnapshotTemporaryStorage>(this, RSTrace.CatalogTrace, "WindowsServiceUseFileShareStorage", specifiedValue, SnapshotTemporaryStorage.True, "");
					value.Value = enumParameter2.Value;
					break;
				}
				case ConfigurationProperty.FileShareStorageLocation:
				case ConfigurationProperty.PolicyLevelServer:
				case ConfigurationProperty.UnattendedExecutionAccountDomain:
				case ConfigurationProperty.UnattendedExecutionAccountUser:
				case ConfigurationProperty.UnattendedExecutionAccountPassword:
				case ConfigurationProperty.FileShareAccountDomain:
				case ConfigurationProperty.FileShareAccountUser:
				case ConfigurationProperty.FileShareAccountPassword:
					if (!string.IsNullOrEmpty(specifiedValue))
					{
						value.Value = specifiedValue;
					}
					break;
				case ConfigurationProperty.IsWebService:
				{
					BooleanParameter booleanParameter = new BooleanParameter(this, RSTrace.CatalogTrace, "IsWebServiceEnabled", specifiedValue, true, "");
					value.Value = booleanParameter.Value;
					break;
				}
				case ConfigurationProperty.IsSchedulingService:
				{
					BooleanParameter booleanParameter = new BooleanParameter(this, RSTrace.CatalogTrace, "IsSchedulingService", specifiedValue, true, "");
					value.Value = booleanParameter.Value;
					break;
				}
				case ConfigurationProperty.IsNotificationService:
				{
					BooleanParameter booleanParameter = new BooleanParameter(this, RSTrace.CatalogTrace, "IsNotificationService", specifiedValue, true, "");
					value.Value = booleanParameter.Value;
					break;
				}
				case ConfigurationProperty.IsEventService:
				{
					BooleanParameter booleanParameter = new BooleanParameter(this, RSTrace.CatalogTrace, "IsEventService", specifiedValue, true, "");
					value.Value = booleanParameter.Value;
					break;
				}
				case ConfigurationProperty.IsRdceEnabled:
				{
					BooleanParameter booleanParameter = new BooleanParameter(this, RSTrace.CatalogTrace, "IsRdceEnabled", specifiedValue, false, "");
					value.Value = booleanParameter.Value;
					break;
				}
				case ConfigurationProperty.WatsonFlags:
				{
					IntParameter intParameter = new IntParameter(this, RSTrace.CatalogTrace, "WatsonFlags", specifiedValue, this.m_watsonFlags, "");
					value.Value = intParameter.Value;
					break;
				}
				case ConfigurationProperty.WatsonDumpOnExceptions:
				{
					CollectionParameter collectionParameter = new CollectionParameter(this, RSTrace.CatalogTrace, "WatsonDumpOnExceptions", specifiedValue, this.m_watsonDumpOnExceptions, "");
					value.Value = collectionParameter.Value;
					break;
				}
				case ConfigurationProperty.WatsonDumpExcludeIfContainsExceptions:
				{
					CollectionParameter collectionParameter = new CollectionParameter(this, RSTrace.CatalogTrace, "WatsonDumpExcludeIfContainsExceptions", specifiedValue, this.m_watsonDumpExcludeIfContainsExceptions, "");
					value.Value = collectionParameter.Value;
					break;
				}
				case ConfigurationProperty.IsReportBuilderAnonymousAccessEnabled:
				{
					BooleanParameter booleanParameter = new BooleanParameter(this, RSTrace.CatalogTrace, "IsReportBuilderAnonymousAccessEnabled", specifiedValue, false, "");
					value.Value = booleanParameter.Value;
					break;
				}
				case ConfigurationProperty.RDLSandboxing:
					RDLSandboxingConfiguration.Validate(this, RSTrace.CatalogTrace, (RDLSandboxingPropertyBag)value.Value);
					break;
				case ConfigurationProperty.MapTileServerConfiguration:
					Microsoft.ReportingServices.Diagnostics.MapTileServerConfiguration.Validate(this, RSTrace.CatalogTrace, (MapTileServerConfigurationPropertyBag)value.Value);
					break;
				case ConfigurationProperty.ExtendedProtectionLevel:
				{
					EnumParameter<ExtendedProtectionLevel> enumParameter3 = new EnumParameter<ExtendedProtectionLevel>(this, RSTrace.CatalogTrace, "RSWindowsExtendedProtectionLevel", specifiedValue, ExtendedProtectionLevel.Invalid, "");
					value.Value = enumParameter3.Value;
					break;
				}
				case ConfigurationProperty.ExtendedProtectionScenario:
				{
					EnumParameter<ExtendedProtectionScenario> enumParameter4 = new EnumParameter<ExtendedProtectionScenario>(this, RSTrace.CatalogTrace, "RSWindowsExtendedProtectionScenario", specifiedValue, ExtendedProtectionScenario.Invalid, "");
					value.Value = enumParameter4.Value;
					break;
				}
				case ConfigurationProperty.IsAlertingService:
				{
					BooleanParameter booleanParameter = new BooleanParameter(this, RSTrace.CatalogTrace, "IsAlertingService", specifiedValue, true, "");
					value.Value = booleanParameter.Value;
					break;
				}
				case ConfigurationProperty.DisableSecureFormsAuthenticationCookie:
				{
					BooleanParameter booleanParameter = new BooleanParameter(this, RSTrace.CatalogTrace, "DisableSecureFormsAuthenticationCookie", specifiedValue, this.m_disableSecureFormsAuthenticationCookie, "");
					value.Value = booleanParameter.Value;
					break;
				}
				case ConfigurationProperty.UsernameSIDRefreshMinutes:
					value.Value = new IntParameter(this, RSTrace.CatalogTrace, "UsernameSIDRefreshMinutes", specifiedValue, this.m_UsernameSIDRefreshMinutesParam, "minute(s)")
					{
						MinValue = 1
					}.Value;
					break;
				case ConfigurationProperty.UpdatePoliciesSeconds:
					value.Value = new IntParameter(this, RSTrace.CatalogTrace, "UpdatePoliciesSeconds", specifiedValue, this.m_UpdatePolicySecondsParam, "second(s)")
					{
						MinValue = 1
					}.Value;
					break;
				case ConfigurationProperty.UpdatePoliciesChunkSize:
					value.Value = new IntParameter(this, RSTrace.CatalogTrace, "UpdatePoliciesChunkSize", specifiedValue, this.m_UpdatePoliciesChunkSizeParam, "count")
					{
						MinValue = 1
					}.Value;
					break;
				}
			}
			this.AdjustProperties(properties);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00007058 File Offset: 0x00005258
		internal void AdjustProperties(ConfigurationPropertyBag properties)
		{
			string text = (string)properties[ConfigurationProperty.InstanceId].Value;
			if (!string.IsNullOrEmpty(text))
			{
				SkuType installedSku = Sku.GetInstalledSku(text);
				if (properties.ContainsKey(ConfigurationProperty.Extensions))
				{
					ConfigurationPropertyInfo configurationPropertyInfo = properties[ConfigurationProperty.Extensions];
					if (configurationPropertyInfo != null)
					{
						ExtensionsConfiguration extensionsConfiguration = (ExtensionsConfiguration)configurationPropertyInfo.Value;
						ExtensionClassFactory.AdjustRenderingExtensionsForSku(extensionsConfiguration, installedSku);
						ExtensionClassFactory.AdjustDataExtensionsForSku(extensionsConfiguration, installedSku);
					}
				}
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000070B8 File Offset: 0x000052B8
		private void ValidatePostLoad()
		{
			if (this.AuthPersistence && (this.AuthenticationTypes & AuthenticationTypes.RSWindowsBasic) != AuthenticationTypes.None)
			{
				RSTrace.ConfigManagerTracer.Trace(TraceLevel.Info, "AuthPersistence does not apply to basic authentication.");
			}
			if ((this.AuthenticationTypes & (AuthenticationTypes.RSWindowsNegotiate | AuthenticationTypes.RSWindowsKerberos | AuthenticationTypes.RSWindowsNTLM)) != AuthenticationTypes.None)
			{
				if (this.ExtendedProtectionLevel == ExtendedProtectionLevel.Invalid)
				{
					Globals.DisableEPAuthTypes = true;
					RSTrace.ConfigManagerTracer.Trace(TraceLevel.Error, "Missing or Invalid ExtendedProtectionLevel setting");
					return;
				}
				if (this.ExtendedProtectionLevel != ExtendedProtectionLevel.Off)
				{
					bool flag;
					bool flag2;
					this.CheckSslAndHttpUrlReservations(RunningApplication.WebService, out flag, out flag2);
					bool flag3;
					bool flag4;
					this.CheckSslAndHttpUrlReservations(RunningApplication.ReportManager, out flag3, out flag4);
					if (this.ExtendedProtectionScenario == ExtendedProtectionScenario.Invalid)
					{
						Globals.DisableEPAuthTypes = true;
						RSTrace.ConfigManagerTracer.Trace(TraceLevel.Error, "Missing or Invalid ExtendedProtectionScenario setting");
						return;
					}
					if (this.ExtendedProtectionScenario == ExtendedProtectionScenario.Direct)
					{
						if (!flag)
						{
							Globals.DisableEPAuthTypes = true;
							RSTrace.ConfigManagerTracer.Trace(TraceLevel.Error, "SSL is required on Report Server connections when ExtendedProtectionScenario is set to Direct");
						}
						else if (flag2)
						{
							RSTrace.ConfigManagerTracer.Trace(TraceLevel.Warning, "HTTP traffic to Report Server will fail NTLM, Kerberos, Negotiate Authentication");
						}
						if (!flag3)
						{
							RSTrace.ConfigManagerTracer.Trace(TraceLevel.Error, "SSL is required on Report Manager connections when ExtendedProtectionScenario is set to Direct");
							return;
						}
						if (flag4)
						{
							RSTrace.ConfigManagerTracer.Trace(TraceLevel.Warning, "HTTP traffic to Report Manager will fail NTLM, Kerberos, Negotiate Authentication");
							return;
						}
					}
					else if (this.ExtendedProtectionScenario == ExtendedProtectionScenario.Proxy)
					{
						if (!flag)
						{
							RSTrace.ConfigManagerTracer.Trace(TraceLevel.Warning, "Direct connections to Report Server will fail NTLM, Kerberos, Negotiate Authentication");
						}
						else if (flag2)
						{
							RSTrace.ConfigManagerTracer.Trace(TraceLevel.Warning, "HTTP traffic to Report Server may fail NTLM, Kerberos, Negotiate Authentication");
						}
						if (!flag3)
						{
							RSTrace.ConfigManagerTracer.Trace(TraceLevel.Warning, "Direct connections to Report Manager will fail NTLM, Kerberos, Negotiate Authentication");
							return;
						}
						if (flag4)
						{
							RSTrace.ConfigManagerTracer.Trace(TraceLevel.Warning, "HTTP traffic to Report Manager may fail NTLM, Kerberos, Negotiate Authentication");
						}
					}
				}
			}
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000720C File Offset: 0x0000540C
		private void CheckIfFeaturesEnabledBySku()
		{
			if (this.m_isSchedulingService && !Sku.IsFeatureEnabled(this.InstanceID, RestrictedFeatures.Scheduling))
			{
				if (RSTrace.ConfigManagerTracer.TraceWarning)
				{
					RSTrace.ConfigManagerTracer.Trace(TraceLevel.Warning, "Restricted feature: " + RestrictedFeatures.Scheduling.ToString());
				}
				this.m_isSchedulingService = false;
			}
			if (this.m_isAlertingService && !Sku.IsFeatureEnabled(this.InstanceID, RestrictedFeatures.DataAlerting))
			{
				if (RSTrace.ConfigManagerTracer.TraceWarning)
				{
					RSTrace.ConfigManagerTracer.Trace(TraceLevel.Warning, "Restricted feature: " + RestrictedFeatures.DataAlerting.ToString());
				}
				this.m_isAlertingService = false;
			}
			if (this.m_isNotificationService && !Sku.IsFeatureEnabled(this.InstanceID, RestrictedFeatures.Delivery))
			{
				if (RSTrace.ConfigManagerTracer.TraceWarning)
				{
					RSTrace.ConfigManagerTracer.Trace(TraceLevel.Warning, "Restricted feature: " + RestrictedFeatures.Delivery.ToString());
				}
				this.m_isNotificationService = false;
			}
			if (this.m_isEventService && !Sku.IsFeatureEnabled(this.InstanceID, RestrictedFeatures.Scheduling))
			{
				if (RSTrace.ConfigManagerTracer.TraceWarning)
				{
					RSTrace.ConfigManagerTracer.Trace(TraceLevel.Warning, "Restricted feature: " + RestrictedFeatures.Scheduling.ToString());
				}
				this.m_isEventService = false;
			}
			if (this.m_isRdceEnabled && !Sku.IsFeatureEnabled(this.InstanceID, RestrictedFeatures.Extensibility))
			{
				RSTrace.ConfigManagerTracer.Trace(TraceLevel.Warning, "RDCE is not supported on this edition of Reporting Services and has been disabled.");
				this.m_isRdceEnabled = false;
			}
			if (this.m_enablePowerBIFeatures && !Sku.IsFeatureEnabled(this.InstanceID, RestrictedFeatures.PowerBIPinning))
			{
				RSTrace.ConfigManagerTracer.Trace(TraceLevel.Warning, "Pinning report items to PowerBI is not supported on this edition of Reporting Services and has been disabled.");
				this.m_enablePowerBIFeatures = false;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600018B RID: 395 RVA: 0x000073AA File Offset: 0x000055AA
		public int RequireHttpsLevel
		{
			get
			{
				return this.m_requiredHttpsLevel;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600018C RID: 396 RVA: 0x000073B2 File Offset: 0x000055B2
		public bool DisableSecureFormsAuthenticationCookie
		{
			get
			{
				return this.m_disableSecureFormsAuthenticationCookie;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600018D RID: 397 RVA: 0x000073BA File Offset: 0x000055BA
		public bool EnablePowerBIFeatures
		{
			get
			{
				return this.m_enablePowerBIFeatures;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600018E RID: 398 RVA: 0x000073C2 File Offset: 0x000055C2
		public int MaxActiveReqForOneUser
		{
			get
			{
				return this.m_maxActiveReqForOneUser;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600018F RID: 399 RVA: 0x000073CA File Offset: 0x000055CA
		public int MaxScheduleWait
		{
			get
			{
				return this.m_maxScheduleWait;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000190 RID: 400 RVA: 0x000073D2 File Offset: 0x000055D2
		public override int DBQueryTimeout
		{
			get
			{
				return this.m_DBQueryTimeout;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000191 RID: 401 RVA: 0x000073DA File Offset: 0x000055DA
		public override int ProcessTimeout
		{
			get
			{
				return this.m_processTimeout;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000192 RID: 402 RVA: 0x000073E2 File Offset: 0x000055E2
		public override int ProcessTimeoutGcExtension
		{
			get
			{
				return this.m_processTimeoutGcExtension;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000193 RID: 403 RVA: 0x000073EA File Offset: 0x000055EA
		public override int DBCleanupTimeout
		{
			get
			{
				return this.m_DBCleanupTimeout;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000194 RID: 404 RVA: 0x000073F2 File Offset: 0x000055F2
		public override int DBCleanupBatchFactor
		{
			get
			{
				return this.m_DBCleanupBatchFactor;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000195 RID: 405 RVA: 0x000073FC File Offset: 0x000055FC
		public override Guid InstallationID
		{
			get
			{
				Guid guid;
				try
				{
					guid = new Guid(this.m_installationID);
				}
				catch (ArgumentNullException)
				{
					throw new ServerConfigurationErrorException("InstallationID is not specified in the config file.");
				}
				catch (FormatException)
				{
					throw new ServerConfigurationErrorException("InstallationID specified in the config file is not a GUID.");
				}
				return guid;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000196 RID: 406 RVA: 0x0000744C File Offset: 0x0000564C
		public override Guid InstallationIDWebApp
		{
			get
			{
				Guid guid;
				try
				{
					guid = new Guid(this.m_installationIDWebApp);
				}
				catch (ArgumentNullException)
				{
					throw new ServerConfigurationErrorException("InstallationID is not specified in the config file.");
				}
				catch (FormatException)
				{
					throw new ServerConfigurationErrorException("InstallationID specified in the config file is not a GUID.");
				}
				return guid;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000197 RID: 407 RVA: 0x0000749C File Offset: 0x0000569C
		public override string InstanceID
		{
			get
			{
				return this.m_instanceID;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000198 RID: 408 RVA: 0x000074A4 File Offset: 0x000056A4
		public override string InstanceName
		{
			get
			{
				if (this.m_instanceName == null)
				{
					RevertImpersonationContext.Run(delegate
					{
						this.m_instanceName = Utilities.InstanceNameFromInstanceID(this.InstanceID);
					});
				}
				return this.m_instanceName;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000199 RID: 409 RVA: 0x000074C5 File Offset: 0x000056C5
		public override string RpcEndpoint
		{
			get
			{
				if (this.m_rpcEndpoint == null)
				{
					this.m_rpcEndpoint = Utilities.RPCEndpointFromInstanceID(this.InstanceID);
				}
				return this.m_rpcEndpoint;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600019A RID: 410 RVA: 0x000074E6 File Offset: 0x000056E6
		public override string WindowsServiceName
		{
			get
			{
				if (this.m_windowsServiceName == null)
				{
					this.m_windowsServiceName = Utilities.ServiceNameFromInstanceName(this.InstanceName);
				}
				return this.m_windowsServiceName;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00007507 File Offset: 0x00005707
		public RSConfiguration.RecycleOptions RecycleProcessOnSevereErrors
		{
			get
			{
				return this.m_RecycleProcessOnSevereErrors;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600019C RID: 412 RVA: 0x0000750F File Offset: 0x0000570F
		public int RunningRequestsScavengerCycle
		{
			get
			{
				return this.m_RunningRequestsScavengerCycleParam;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00007517 File Offset: 0x00005717
		public int RunningRequestsDBCycle
		{
			get
			{
				return this.m_RunningRequestsDBCycleParam;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600019E RID: 414 RVA: 0x0000751F File Offset: 0x0000571F
		public int RunningRequestsAge
		{
			get
			{
				return this.m_RunningRequestsAgeParam;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00007527 File Offset: 0x00005727
		public int CleanupCycleMinutes
		{
			get
			{
				return this.m_CleanupCycleMinutesParam;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x0000752F File Offset: 0x0000572F
		public int DailyCleanupMinuteOfDay
		{
			get
			{
				return this.m_dailyCleanupMinuteOfDayParam;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00007537 File Offset: 0x00005737
		public int AlertingCleanupCycleMinutes
		{
			get
			{
				return this.m_AlertingCleanupCycleMinutesParam;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x0000753F File Offset: 0x0000573F
		public int UsernameSIDRefreshMinutesParam
		{
			get
			{
				return this.m_UsernameSIDRefreshMinutesParam;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00007547 File Offset: 0x00005747
		public int UserNameSIDRefreshSQLTimeout
		{
			get
			{
				return this.m_UserNameSIDRefreshSQLTimeout;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x0000754F File Offset: 0x0000574F
		public int UpdatePolicySecondsParam
		{
			get
			{
				return this.m_UpdatePolicySecondsParam;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00007557 File Offset: 0x00005757
		public int UpdatePoliciesChunkSizeParam
		{
			get
			{
				return this.m_UpdatePoliciesChunkSizeParam;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x0000755F File Offset: 0x0000575F
		public int AlertingDataCleanupMinutes
		{
			get
			{
				return this.m_AlertingDataCleanupMinutesParam;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00007567 File Offset: 0x00005767
		public int AlertingExecutionLogCleanupMinutes
		{
			get
			{
				return this.m_AlertingExecutionLogCleanupMinutesParam;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x0000756F File Offset: 0x0000576F
		public int AlertingMaxDataRetentionDays
		{
			get
			{
				return this.m_AlertingMaxDataRetentionDaysParam;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00007577 File Offset: 0x00005777
		public override int WatsonFlags
		{
			get
			{
				return this.m_watsonFlags;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060001AA RID: 426 RVA: 0x0000757F File Offset: 0x0000577F
		public override StringCollection WatsonDumpOnExceptions
		{
			get
			{
				return this.m_watsonDumpOnExceptions;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00007587 File Offset: 0x00005787
		public override StringCollection WatsonDumpExcludeIfContainsExceptions
		{
			get
			{
				return this.m_watsonDumpExcludeIfContainsExceptions;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060001AC RID: 428 RVA: 0x0000758F File Offset: 0x0000578F
		public int RdlxExecutionTimeout
		{
			get
			{
				return this.m_rdlxExecutionTimeout;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00007597 File Offset: 0x00005797
		public int RdlxSessionTimeout
		{
			get
			{
				return this.m_rdlxSessionTimeout;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060001AE RID: 430 RVA: 0x0000759F File Offset: 0x0000579F
		public ExecutionLogLevel ExecutionLogLevel
		{
			get
			{
				return this.m_execLogLevel;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060001AF RID: 431 RVA: 0x000075A7 File Offset: 0x000057A7
		public bool EnableRemoteErrors
		{
			get
			{
				return this.m_enableRemoteErrors;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x000075AF File Offset: 0x000057AF
		public double GlobalConnectionPoolEvictionTimeout
		{
			get
			{
				return this.m_globalConnectionPoolEvictionTimeout;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x000075B7 File Offset: 0x000057B7
		public double ASOnPremConnectionPoolEvictionTimeout
		{
			get
			{
				return this.m_asOnPremConnectionPoolEvictionTimeout;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x000075BF File Offset: 0x000057BF
		public int ConnectionTimeout
		{
			get
			{
				return this.m_connectionTimeout;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x000075C7 File Offset: 0x000057C7
		public override string ConfigFileName
		{
			get
			{
				return this.m_configFileName;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x000075CF File Offset: 0x000057CF
		public override string ConfigFilePath
		{
			get
			{
				return this.m_configLocation;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x000075D7 File Offset: 0x000057D7
		public string ServerProductVersion
		{
			get
			{
				if (RSConfiguration.m_ProductVersion == null)
				{
					RSConfiguration.m_ProductVersion = RSConfiguration.GetProductVersion();
				}
				return RSConfiguration.m_ProductVersion;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x000075EF File Offset: 0x000057EF
		public static string NativeServerProductVersion
		{
			get
			{
				if (RSConfiguration.m_NativeProductVersion == null)
				{
					RSConfiguration.m_NativeProductVersion = RSConfiguration.GetNativeProductVersion();
				}
				return RSConfiguration.m_NativeProductVersion;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00007607 File Offset: 0x00005807
		public static string ServerProductName
		{
			get
			{
				if (!Sku.IsBiServer())
				{
					return RSConfiguration.ProductNameSSRS;
				}
				return RSConfiguration.ProductNamePBIRS;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x0000761B File Offset: 0x0000581B
		public static string ProductNameSSRS
		{
			get
			{
				return ErrorStrings.ProductNameSSRS;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00007622 File Offset: 0x00005822
		public static string ProductNamePBIRS
		{
			get
			{
				return ErrorStrings.ProductNamePBIRS;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00007629 File Offset: 0x00005829
		public string ServerProductNameAndVersion
		{
			get
			{
				if (this.ServerProductVersion == null)
				{
					return RSConfiguration.ServerProductName;
				}
				if (!Sku.IsBiServer())
				{
					return ErrorStringsWrapper.ProductNameSSRSAndVersion(this.ServerProductVersion);
				}
				return ErrorStringsWrapper.ProductNamePBIRSAndVersion(this.ServerProductVersion);
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00007658 File Offset: 0x00005858
		public string EnglishServerProductNameAndVersion
		{
			get
			{
				string serverProductNameAndVersion;
				try
				{
					ErrorStrings.Culture = new CultureInfo("en");
					serverProductNameAndVersion = this.ServerProductNameAndVersion;
				}
				finally
				{
					ErrorStrings.Culture = null;
				}
				return serverProductNameAndVersion;
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00007698 File Offset: 0x00005898
		private static string GetProductVersion()
		{
			string version = null;
			RevertImpersonationContext.Run(delegate
			{
				string location = Assembly.GetExecutingAssembly().Location;
				if (!string.IsNullOrEmpty(location))
				{
					FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(location);
					version = versionInfo.ProductVersion;
				}
			});
			return version;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x000076BC File Offset: 0x000058BC
		private static string GetNativeProductVersion()
		{
			Uri uri = new Uri(Assembly.GetExecutingAssembly().CodeBase);
			string location = Path.GetDirectoryName(uri.GetComponents(UriComponents.Path, UriFormat.Unescaped));
			string version = null;
			RevertImpersonationContext.Run(delegate
			{
				FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(Path.Combine(location, "ReportingServicesNativeClient.dll"));
				version = string.Format(CultureInfo.InvariantCulture, "{0:0000}.{1:000}.{2:0000}.{3:00}", new object[] { versionInfo.FileMajorPart, versionInfo.FileMinorPart, versionInfo.FileBuildPart, versionInfo.FilePrivatePart });
			});
			return version;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00007710 File Offset: 0x00005910
		private void CalculateProperties()
		{
			this.m_reportServerUrlCalculated = this.GetReportServerUrl(true, out this.m_reportServerUrlHostnameCalculated);
			this.m_reportServerExternalUrlCalculated = this.GetReportServerUrl(false, out this.m_reportServerExternalUrlHostnameCalculated);
			bool flag = this.IsApplicationUrlSubsetOfReportServer(RunningApplication.ReportManager);
			this.m_canTranslateReportManagerRequest = flag && string.IsNullOrEmpty(this.Hostname) && string.IsNullOrEmpty(this.ReportServerExternalUrl);
			this.m_canTranslateReportServerRequest = string.IsNullOrEmpty(this.Hostname) && string.IsNullOrEmpty(this.ReportServerExternalUrl);
			this.m_urlRootCalculated = this.UrlRoot;
			if (string.IsNullOrEmpty(this.m_urlRootCalculated))
			{
				this.m_urlRootCalculated = this.m_reportServerExternalUrlCalculated;
			}
			if (string.IsNullOrEmpty(this.m_urlRootCalculated))
			{
				this.m_urlRootCalculated = this.m_reportServerUrlCalculated;
			}
			if (RSTrace.ConfigManagerTracer.TraceInfo)
			{
				RSTrace.ConfigManagerTracer.Trace(TraceLevel.Info, "Using report server internal url {0}.", new object[] { this.m_reportServerUrlCalculated });
				RSTrace.ConfigManagerTracer.Trace(TraceLevel.Info, "Using report server external url {0}.", new object[] { this.m_reportServerExternalUrlCalculated });
				RSTrace.ConfigManagerTracer.Trace(TraceLevel.Info, "Using url root {0}.", new object[] { this.m_urlRootCalculated });
			}
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00007838 File Offset: 0x00005A38
		private string GetReportServerUrl(bool local, out string hostname)
		{
			hostname = null;
			string text = (local ? this.ReportServerUrl : this.ReportServerExternalUrl);
			if (text != null)
			{
				Uri uri = null;
				if (Uri.TryCreate(text, UriKind.RelativeOrAbsolute, out uri))
				{
					hostname = uri.Host;
				}
				return text;
			}
			if (this.UrlConfiguration == null)
			{
				if (RSTrace.ConfigManagerTracer.TraceError)
				{
					RSTrace.ConfigManagerTracer.Trace(TraceLevel.Error, "Failed to get url configuration section.");
				}
				return null;
			}
			UrlConfiguration urlConfiguration;
			if (!this.UrlConfiguration.TryGetValue(RunningApplication.WebService, out urlConfiguration))
			{
				if (RSTrace.ConfigManagerTracer.TraceError)
				{
					RSTrace.ConfigManagerTracer.Trace(TraceLevel.Error, "Failed to get report server url reservations.");
				}
				return null;
			}
			string text2 = null;
			string text3 = null;
			string text4 = null;
			string text5 = null;
			string text6 = null;
			string text7 = null;
			string text8 = null;
			string text9 = null;
			foreach (UrlReservation urlReservation in urlConfiguration.UrlReservations)
			{
				string text10;
				string text11;
				string text12;
				string text13;
				string text14;
				if (!RSConfiguration.ParseUrlPrefix(urlReservation.UrlPrefix, out text10, out text11, out text12, out text13, out text14))
				{
					if (RSTrace.ConfigManagerTracer.TraceWarning)
					{
						RSTrace.ConfigManagerTracer.Trace(TraceLevel.Warning, "Invalid Report Server URL in configuration: {0}", new object[] { urlReservation.UrlPrefix });
					}
				}
				else
				{
					bool flag = string.CompareOrdinal(text10, Uri.UriSchemeHttps) == 0;
					IPAddress ipaddress;
					if (string.CompareOrdinal(text11, "*") == 0 || string.CompareOrdinal(text11, "+") == 0)
					{
						string text15;
						if (!string.IsNullOrEmpty(this.Hostname))
						{
							text15 = this.Hostname + ":" + text12;
						}
						else
						{
							text15 = (local ? "localhost" : Environment.MachineName) + ":" + text12;
						}
						if (flag)
						{
							if (text7 == null)
							{
								text7 = text15;
							}
						}
						else if (text4 == null)
						{
							text4 = text15;
						}
					}
					else if (IPAddress.TryParse(text11, out ipaddress))
					{
						string text16 = text11 + ":" + text12;
						if (flag)
						{
							if (text8 == null)
							{
								text8 = text16;
							}
						}
						else if (text5 == null)
						{
							text5 = text16;
						}
					}
					else if (text6 == null)
					{
						string text17 = text11 + ":" + text12;
						if (flag)
						{
							if (text9 == null)
							{
								text9 = text17;
							}
						}
						else if (text6 == null)
						{
							text6 = text17;
						}
					}
				}
			}
			if (text7 != null)
			{
				text3 = text7;
			}
			else if (text8 != null)
			{
				text3 = text8;
			}
			else if (text9 != null)
			{
				text3 = text9;
			}
			if (text4 != null)
			{
				text2 = text4;
			}
			else if (text5 != null)
			{
				text2 = text5;
			}
			else if (text6 != null)
			{
				text2 = text6;
			}
			string text18;
			string text19;
			if (this.RequireHttpsLevel > 0)
			{
				if (text3 != null)
				{
					text18 = text3;
					text19 = Uri.UriSchemeHttps;
				}
				else
				{
					if (RSTrace.ConfigManagerTracer.TraceError)
					{
						RSTrace.ConfigManagerTracer.Trace(TraceLevel.Error, "Cannot find secure url from reservations.");
					}
					text18 = text2;
					text19 = Uri.UriSchemeHttp;
				}
			}
			else if (text2 != null)
			{
				text18 = text2;
				text19 = Uri.UriSchemeHttp;
			}
			else
			{
				text18 = text3;
				text19 = Uri.UriSchemeHttps;
			}
			if (text18 == null)
			{
				if (RSTrace.ConfigManagerTracer.TraceError)
				{
					RSTrace.ConfigManagerTracer.Trace(TraceLevel.Error, "Failed to find full host.");
				}
				return null;
			}
			if (text19 == null)
			{
				if (RSTrace.ConfigManagerTracer.TraceError)
				{
					RSTrace.ConfigManagerTracer.Trace(TraceLevel.Error, "Failed to find url scheme.");
				}
				return null;
			}
			text = text19 + Uri.SchemeDelimiter + text18 + this.ReportServerVirtualDirectory;
			int num = text18.IndexOf(':');
			if (num >= 0)
			{
				hostname = text18.Substring(0, num);
			}
			return text;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00007B44 File Offset: 0x00005D44
		private bool IsApplicationUrlSubsetOfReportServer(RunningApplication application)
		{
			if (application == RunningApplication.WebService)
			{
				return true;
			}
			if (this.UrlConfiguration == null)
			{
				if (RSTrace.ConfigManagerTracer.TraceWarning)
				{
					RSTrace.ConfigManagerTracer.Trace(TraceLevel.Warning, "IsApplicationUrlSubsetOfReportServer() -- Failed to get url configuration section.");
				}
				return false;
			}
			UrlConfiguration urlConfiguration;
			if (!this.UrlConfiguration.TryGetValue(RunningApplication.WebService, out urlConfiguration))
			{
				if (RSTrace.ConfigManagerTracer.TraceWarning)
				{
					RSTrace.ConfigManagerTracer.Trace(TraceLevel.Warning, "IsApplicationUrlSubsetOfReportServer() -- Failed to get report server url reservations.");
				}
				return false;
			}
			UrlConfiguration urlConfiguration2;
			if (!this.UrlConfiguration.TryGetValue(application, out urlConfiguration2))
			{
				if (RSTrace.ConfigManagerTracer.TraceVerbose)
				{
					RSTrace.ConfigManagerTracer.Trace(TraceLevel.Verbose, "Failed to get URL reservations for application {0}.", new object[] { application });
				}
				return false;
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			foreach (UrlReservation urlReservation in urlConfiguration.UrlReservations)
			{
				string text;
				string text2;
				string text3;
				string text4;
				string text5;
				if (!RSConfiguration.ParseUrlPrefix(urlReservation.UrlPrefix, out text, out text2, out text3, out text4, out text5))
				{
					if (RSTrace.ConfigManagerTracer.TraceWarning)
					{
						RSTrace.ConfigManagerTracer.Trace(TraceLevel.Warning, "IsCurrentApplicationUrlSubsetOfReportServer() -- Invalid Report Server URL in configuration: {0}", new object[] { urlReservation.UrlPrefix });
					}
				}
				else
				{
					string text6 = string.Concat(new string[]
					{
						text,
						Uri.SchemeDelimiter,
						text2,
						":",
						text3
					});
					dictionary[text6] = text6;
				}
			}
			foreach (UrlReservation urlReservation2 in urlConfiguration2.UrlReservations)
			{
				string text7;
				string text8;
				string text9;
				string text10;
				string text11;
				if (!RSConfiguration.ParseUrlPrefix(urlReservation2.UrlPrefix, out text7, out text8, out text9, out text10, out text11))
				{
					if (RSTrace.ConfigManagerTracer.TraceWarning)
					{
						RSTrace.ConfigManagerTracer.Trace(TraceLevel.Warning, "IsCurrentApplicationUrlSubsetOfReportServer() -- Invalid URL in configuration: {0}", new object[] { urlReservation2.UrlPrefix });
					}
				}
				else
				{
					string text12 = string.Concat(new string[]
					{
						text7,
						Uri.SchemeDelimiter,
						text8,
						":",
						text9
					});
					if (!dictionary.ContainsKey(text12))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00007D40 File Offset: 0x00005F40
		public IOAuthConfiguration OAuthConfiguration
		{
			get
			{
				return this.m_oAuthConnectionConfiguration;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00007D48 File Offset: 0x00005F48
		public bool CanTranslateReportManagerRequestToReportServer
		{
			get
			{
				return this.m_canTranslateReportManagerRequest;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00007D50 File Offset: 0x00005F50
		public bool CanTranslateReportServerRequest
		{
			get
			{
				return this.m_canTranslateReportServerRequest;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00007D58 File Offset: 0x00005F58
		public string ReportServerUrlCalculated
		{
			get
			{
				return this.m_reportServerUrlCalculated;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00007D60 File Offset: 0x00005F60
		public string ReportServerExternalUrlCalculated
		{
			get
			{
				string text = this.m_reportServerExternalUrlCalculated;
				if (Globals.CanTranslateIncomingHttpRequest)
				{
					text = HttpContext.Current.Request.Url.GetComponents(UriComponents.Scheme | UriComponents.Host | UriComponents.StrongPort, UriFormat.UriEscaped) + this.ReportServerVirtualDirectory;
				}
				return text;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00007DA4 File Offset: 0x00005FA4
		public string UrlRootCalculated
		{
			get
			{
				if (string.IsNullOrEmpty(this.m_urlRootCalculated))
				{
					throw new ServerConfigurationErrorException("The report server has found no valid URLs for the operation you are performing. Verify the <UrlRoot> entry in the \ufffdRSReportServer.config\ufffd file is configured correctly.");
				}
				return this.m_urlRootCalculated;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00007DD3 File Offset: 0x00005FD3
		public string ReportServerUrlHostnameCalculated
		{
			get
			{
				return this.m_reportServerUrlHostnameCalculated;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00007DDB File Offset: 0x00005FDB
		public string ReportServerExternalUrlHostnameCalculated
		{
			get
			{
				return this.m_reportServerExternalUrlHostnameCalculated;
			}
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00007DE4 File Offset: 0x00005FE4
		public void CheckSslAndHttpUrlReservations(RunningApplication application, out bool IsSslUrlReserved, out bool IsHttpUrlReserved)
		{
			IsSslUrlReserved = false;
			IsHttpUrlReserved = false;
			UrlConfiguration urlConfiguration;
			if (!this.UrlConfiguration.TryGetValue(application, out urlConfiguration))
			{
				if (RSTrace.ConfigManagerTracer.TraceWarning)
				{
					RSTrace.ConfigManagerTracer.Trace(TraceLevel.Verbose, "Failed to get URL reservations for application {0}.", new object[] { application });
				}
				return;
			}
			foreach (UrlReservation urlReservation in urlConfiguration.UrlReservations)
			{
				if (IsSslUrlReserved & IsHttpUrlReserved)
				{
					break;
				}
				string text;
				string text2;
				string text3;
				string text4;
				string text5;
				RSConfiguration.ParseUrlPrefix(urlReservation.UrlPrefix, out text, out text2, out text3, out text4, out text5);
				if (string.CompareOrdinal(text, Uri.UriSchemeHttps) == 0)
				{
					IsSslUrlReserved = true;
				}
				else if (string.CompareOrdinal(text, Uri.UriSchemeHttp) == 0)
				{
					IsHttpUrlReserved = true;
				}
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00007E91 File Offset: 0x00006091
		public IPowerBIOAuthConfiguration PowerBIConfiguration
		{
			get
			{
				return this.m_powerBIConnectionConfiguration;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00007E99 File Offset: 0x00006099
		public IRdlSandboxConfig RdlSandboxing
		{
			get
			{
				return this.m_rdlSandboxing;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00007EA1 File Offset: 0x000060A1
		public IMapTileServerConfiguration MapTileServerConfiguration
		{
			get
			{
				return this.m_mapTileServerConfiguration;
			}
		}

		// Token: 0x04000147 RID: 327
		private long m_sequence;

		// Token: 0x04000148 RID: 328
		private ExtensionsConfiguration m_extensions;

		// Token: 0x04000149 RID: 329
		private NameValueCollection m_pairs;

		// Token: 0x0400014A RID: 330
		private string m_dsn;

		// Token: 0x0400014B RID: 331
		private Dictionary<string, EventExtension> m_events;

		// Token: 0x0400014C RID: 332
		internal readonly string m_configFileName;

		// Token: 0x0400014D RID: 333
		internal readonly string m_configLocation;

		// Token: 0x0400014E RID: 334
		private RSBaseConfiguration.CatalogConnectionType m_connectionType;

		// Token: 0x0400014F RID: 335
		private RSBaseConfiguration.CatalogConnectionAuth m_connectionAuth;

		// Token: 0x04000150 RID: 336
		private string m_encryptedCatalogUser;

		// Token: 0x04000151 RID: 337
		private string m_encryptedCatalogDomain;

		// Token: 0x04000152 RID: 338
		private string m_encryptedCatalogCred;

		// Token: 0x04000153 RID: 339
		private string m_defaultViewerStyle;

		// Token: 0x04000154 RID: 340
		private bool m_displayErrorLink;

		// Token: 0x04000155 RID: 341
		private SnapshotTemporaryStorage m_WebServiceUseFileShareStorage;

		// Token: 0x04000156 RID: 342
		private Guid m_serviceApplicationId;

		// Token: 0x04000157 RID: 343
		private bool m_isSchedulingService;

		// Token: 0x04000158 RID: 344
		private bool m_isAlertingService;

		// Token: 0x04000159 RID: 345
		private bool m_isNotificationService;

		// Token: 0x0400015A RID: 346
		private bool m_isEventService;

		// Token: 0x0400015B RID: 347
		private int m_pollingInterval;

		// Token: 0x0400015C RID: 348
		private int m_maxCatalogConnectionPoolSizePerProcess;

		// Token: 0x0400015D RID: 349
		private float m_memoryLimit;

		// Token: 0x0400015E RID: 350
		private float m_maxMemoryLimit;

		// Token: 0x0400015F RID: 351
		private TimeSpan m_recycleTime;

		// Token: 0x04000160 RID: 352
		private TimeSpan m_maxAppDomainUnloadTime;

		// Token: 0x04000161 RID: 353
		private TimeSpan m_maxTimedAppDomainUnload;

		// Token: 0x04000162 RID: 354
		private int m_maxQueueThreads;

		// Token: 0x04000163 RID: 355
		private string m_urlRoot;

		// Token: 0x04000164 RID: 356
		private string m_encryptedFileShareUserName;

		// Token: 0x04000165 RID: 357
		private string m_encryptedFileShareDomain;

		// Token: 0x04000166 RID: 358
		private string m_encryptedFileSharePassword;

		// Token: 0x04000167 RID: 359
		private string m_encryptedPowerBIUserName;

		// Token: 0x04000168 RID: 360
		private string m_encryptedPowerBIDomain;

		// Token: 0x04000169 RID: 361
		private string m_encryptedPowerBIPassword;

		// Token: 0x0400016A RID: 362
		private string m_encryptedSurrogateUserName;

		// Token: 0x0400016B RID: 363
		private string m_encryptedSurrogateDomain;

		// Token: 0x0400016C RID: 364
		private string m_encryptedSurrogatePassword;

		// Token: 0x0400016D RID: 365
		private string m_policyLevel;

		// Token: 0x0400016E RID: 366
		private string m_fileShareStoragePath;

		// Token: 0x0400016F RID: 367
		private SnapshotTemporaryStorage m_windowsServiceUserFileShareStorage;

		// Token: 0x04000170 RID: 368
		private string m_webServiceAccount;

		// Token: 0x04000171 RID: 369
		private bool m_isWebServiceEnabled;

		// Token: 0x04000172 RID: 370
		private bool m_isReportBuilderAnonymousAccessEnabled;

		// Token: 0x04000173 RID: 371
		private bool m_isRdceEnabled;

		// Token: 0x04000174 RID: 372
		private long m_workingSetMin;

		// Token: 0x04000175 RID: 373
		private long m_workingSetMax;

		// Token: 0x04000176 RID: 374
		private string m_serverDirectory;

		// Token: 0x04000177 RID: 375
		private string m_serverUrl;

		// Token: 0x04000178 RID: 376
		private string m_serverExternalUrl;

		// Token: 0x04000179 RID: 377
		private string m_loginUrl;

		// Token: 0x0400017A RID: 378
		private bool m_useSSL;

		// Token: 0x0400017B RID: 379
		private bool m_enableReportDesignClientButton = true;

		// Token: 0x0400017C RID: 380
		private string m_pageCountMode;

		// Token: 0x0400017D RID: 381
		private int m_postbackTimeout = 60;

		// Token: 0x0400017E RID: 382
		private StringCollection m_passthroughCookies;

		// Token: 0x0400017F RID: 383
		private Dictionary<RunningApplication, UrlConfiguration> m_urlConfiguration;

		// Token: 0x04000180 RID: 384
		private AuthenticationTypes m_authenticationTypes;

		// Token: 0x04000181 RID: 385
		private LogonMethod m_logonMethod = LogonMethod.Cleartext;

		// Token: 0x04000182 RID: 386
		private string m_authRealm;

		// Token: 0x04000183 RID: 387
		private string m_authDomain;

		// Token: 0x04000184 RID: 388
		private bool m_authPersistence = true;

		// Token: 0x04000185 RID: 389
		private int m_maxUnauthenticatedRequests = 50;

		// Token: 0x04000186 RID: 390
		private int m_unauthenticatedRequestWindow = 10;

		// Token: 0x04000187 RID: 391
		private int m_unauthenticatedRequestLockoutTime = 60;

		// Token: 0x04000188 RID: 392
		private int m_authTokenCacheMaxSize = 600;

		// Token: 0x04000189 RID: 393
		private int m_authTokenCacheMaintenanceInterval = 600;

		// Token: 0x0400018A RID: 394
		private int m_authTokenCacheLogonTimeout;

		// Token: 0x0400018B RID: 395
		private int m_authTokenCacheEntryTimeout = 540;

		// Token: 0x0400018C RID: 396
		private string m_hostname;

		// Token: 0x0400018D RID: 397
		private int m_requestCacheSlots;

		// Token: 0x0400018E RID: 398
		private ExtendedProtectionLevel m_extendedProtectionLevel;

		// Token: 0x0400018F RID: 399
		private ExtendedProtectionScenario m_extendedProtectionScenario;

		// Token: 0x04000190 RID: 400
		private const int _DefaultMaxActiveReqForOneUser = 20;

		// Token: 0x04000191 RID: 401
		private const int _DefaultScheduleWait = 1;

		// Token: 0x04000192 RID: 402
		private const int _DefaultRunningRequestsScavengerCycle = 30;

		// Token: 0x04000193 RID: 403
		private const int _DefaultRunningRequestsDBCycle = 30;

		// Token: 0x04000194 RID: 404
		private const int _DefaultRunningRequestsAge = 30;

		// Token: 0x04000195 RID: 405
		private const int _DefaultCleanupCycleMinutes = 10;

		// Token: 0x04000196 RID: 406
		private const int _DefaultDailyCleanupMinuteOfDay = 120;

		// Token: 0x04000197 RID: 407
		private const int _DefaultAlertingCleanupCycleMinutes = 20;

		// Token: 0x04000198 RID: 408
		private const int _DefaultAlertingDataCleanupMinutes = 360;

		// Token: 0x04000199 RID: 409
		private const int _DefaultAlertingExecutionLogCleanupMinutes = 10080;

		// Token: 0x0400019A RID: 410
		private const int _DefaultAlertingMaxDataRetentionDays = 180;

		// Token: 0x0400019B RID: 411
		private const int _DefaultUsernameSIDRefreshMinutes = 360;

		// Token: 0x0400019C RID: 412
		private const int _DefaultWatsonFlags = 1064;

		// Token: 0x0400019D RID: 413
		private const int _DefaultRdlxExecutionTimeout = 900;

		// Token: 0x0400019E RID: 414
		private const int _DefaultRdlxSessionTimeout = 600;

		// Token: 0x0400019F RID: 415
		private const double _DefaultGlobalConnectionPoolEvictionTimeout = 1.0;

		// Token: 0x040001A0 RID: 416
		private const double _DefaultASOnPremConnectionPoolEvictionTimeout = 0.083;

		// Token: 0x040001A1 RID: 417
		private const int _DefaultRunningRequestsScavengerCycleParam = 60;

		// Token: 0x040001A2 RID: 418
		private const int _DefaultPvApplicationDefaultQueryMemoryLimit = 1048576;

		// Token: 0x040001A3 RID: 419
		private static readonly Dictionary<string, int> _DefaultPvApplicationQueryMemoryLimits = new Dictionary<string, int> { { "InfoNav", 512000 } };

		// Token: 0x040001A4 RID: 420
		private const int _DefaultPVApplicationDefaultQueryTimeoutInternal = 0;

		// Token: 0x040001A5 RID: 421
		private static readonly Dictionary<string, int> _DefaultPVApplicationQueryTimeoutInternal = new Dictionary<string, int>();

		// Token: 0x040001A6 RID: 422
		private const int _DefaultPVApplicationDefaultQueryTimeoutExternal = 0;

		// Token: 0x040001A7 RID: 423
		private static readonly Dictionary<string, int> _DefaultPVApplicationQueryTimeoutExternal = new Dictionary<string, int>();

		// Token: 0x040001A8 RID: 424
		private const int _DefaultPVOpenConnectionTimeoutInternal = 0;

		// Token: 0x040001A9 RID: 425
		private const int _DefaultPVOpenConnectionTimeoutExternal = 0;

		// Token: 0x040001AA RID: 426
		private const int _DefaultMinTimeBetweenConnectionRetriesInSeconds = 5;

		// Token: 0x040001AB RID: 427
		private static readonly List<Uri> _DefaultAllowedExternalImageDomains = new List<Uri>
		{
			new Uri("http://sampleimages.blob.core.windows.net/")
		};

		// Token: 0x040001AC RID: 428
		private static readonly Dictionary<string, Collection<string>> _DefaultAllowedTenantsPerApplication = new Dictionary<string, Collection<string>> { 
		{
			"SSPA",
			new Collection<string> { "ffffffff-ffff-ffff-ffff-ffffffffffff" }
		} };

		// Token: 0x040001AD RID: 429
		private static readonly StringCollection _DefaultWatsonDumpOnExceptions = new StringCollection { "Microsoft.ReportingServices.Diagnostics.Utilities.InternalCatalogException", "Microsoft.ReportingServices.Modeling.InternalModelingException", "Microsoft.ReportingServices.Diagnostics.Utilities.InternalRepublishingException" };

		// Token: 0x040001AE RID: 430
		private static readonly StringCollection _DefaultWatsonDumpExcludeIfContainsExceptions = new StringCollection { "System.Threading.ThreadAbortException", "System.Web.UI.ViewStateException", "System.OutOfMemoryException", "System.Web.HttpException" };

		// Token: 0x040001AF RID: 431
		private const int _DefaultExternalModelCacheItemEvictionTimeout = 86400;

		// Token: 0x040001B0 RID: 432
		private int m_requiredHttpsLevel;

		// Token: 0x040001B1 RID: 433
		private bool m_disableSecureFormsAuthenticationCookie;

		// Token: 0x040001B2 RID: 434
		private bool m_enablePowerBIFeatures;

		// Token: 0x040001B3 RID: 435
		private int m_maxActiveReqForOneUser;

		// Token: 0x040001B4 RID: 436
		private int m_maxScheduleWait;

		// Token: 0x040001B5 RID: 437
		private int m_DBQueryTimeout;

		// Token: 0x040001B6 RID: 438
		private int m_processTimeout;

		// Token: 0x040001B7 RID: 439
		private int m_processTimeoutGcExtension;

		// Token: 0x040001B8 RID: 440
		private int m_DBCleanupTimeout;

		// Token: 0x040001B9 RID: 441
		private int m_DBCleanupBatchFactor;

		// Token: 0x040001BA RID: 442
		private string m_installationID;

		// Token: 0x040001BB RID: 443
		private string m_installationIDWebApp;

		// Token: 0x040001BC RID: 444
		private string m_instanceID;

		// Token: 0x040001BD RID: 445
		private string m_instanceName;

		// Token: 0x040001BE RID: 446
		private string m_rpcEndpoint;

		// Token: 0x040001BF RID: 447
		private string m_windowsServiceName;

		// Token: 0x040001C0 RID: 448
		private RSConfiguration.RecycleOptions m_RecycleProcessOnSevereErrors;

		// Token: 0x040001C1 RID: 449
		private int m_RunningRequestsScavengerCycleParam;

		// Token: 0x040001C2 RID: 450
		private int m_RunningRequestsDBCycleParam;

		// Token: 0x040001C3 RID: 451
		private int m_RunningRequestsAgeParam;

		// Token: 0x040001C4 RID: 452
		private int m_CleanupCycleMinutesParam;

		// Token: 0x040001C5 RID: 453
		private int m_dailyCleanupMinuteOfDayParam;

		// Token: 0x040001C6 RID: 454
		private int m_AlertingCleanupCycleMinutesParam;

		// Token: 0x040001C7 RID: 455
		private int m_UsernameSIDRefreshMinutesParam;

		// Token: 0x040001C8 RID: 456
		private int m_UserNameSIDRefreshSQLTimeout;

		// Token: 0x040001C9 RID: 457
		private int m_UpdatePolicySecondsParam;

		// Token: 0x040001CA RID: 458
		private int m_UpdatePoliciesChunkSizeParam;

		// Token: 0x040001CB RID: 459
		private int m_AlertingDataCleanupMinutesParam;

		// Token: 0x040001CC RID: 460
		private int m_AlertingExecutionLogCleanupMinutesParam;

		// Token: 0x040001CD RID: 461
		private int m_AlertingMaxDataRetentionDaysParam;

		// Token: 0x040001CE RID: 462
		private int m_watsonFlags;

		// Token: 0x040001CF RID: 463
		private StringCollection m_watsonDumpOnExceptions;

		// Token: 0x040001D0 RID: 464
		private StringCollection m_watsonDumpExcludeIfContainsExceptions;

		// Token: 0x040001D1 RID: 465
		private int m_rdlxExecutionTimeout;

		// Token: 0x040001D2 RID: 466
		private int m_rdlxSessionTimeout;

		// Token: 0x040001D3 RID: 467
		private ExecutionLogLevel m_execLogLevel;

		// Token: 0x040001D4 RID: 468
		private bool m_enableRemoteErrors;

		// Token: 0x040001D5 RID: 469
		private double m_globalConnectionPoolEvictionTimeout;

		// Token: 0x040001D6 RID: 470
		private double m_asOnPremConnectionPoolEvictionTimeout;

		// Token: 0x040001D7 RID: 471
		private int m_connectionTimeout;

		// Token: 0x040001D8 RID: 472
		private static string m_ProductVersion = null;

		// Token: 0x040001D9 RID: 473
		private static string m_NativeProductVersion = null;

		// Token: 0x040001DA RID: 474
		private OAuthConnectionConfiguration m_oAuthConnectionConfiguration;

		// Token: 0x040001DB RID: 475
		private bool m_canTranslateReportManagerRequest;

		// Token: 0x040001DC RID: 476
		private bool m_canTranslateReportServerRequest;

		// Token: 0x040001DD RID: 477
		private string m_reportServerUrlCalculated;

		// Token: 0x040001DE RID: 478
		private string m_reportServerExternalUrlCalculated;

		// Token: 0x040001DF RID: 479
		private string m_urlRootCalculated;

		// Token: 0x040001E0 RID: 480
		private string m_reportServerUrlHostnameCalculated;

		// Token: 0x040001E1 RID: 481
		private string m_reportServerExternalUrlHostnameCalculated;

		// Token: 0x040001E2 RID: 482
		private PowerBIConfiguration m_powerBIConnectionConfiguration;

		// Token: 0x040001E3 RID: 483
		private RDLSandboxingConfiguration m_rdlSandboxing;

		// Token: 0x040001E4 RID: 484
		private MapTileServerConfiguration m_mapTileServerConfiguration;

		// Token: 0x02000097 RID: 151
		public enum RecycleOptions
		{
			// Token: 0x0400038C RID: 908
			Recycle,
			// Token: 0x0400038D RID: 909
			NoRecycle
		}
	}
}
