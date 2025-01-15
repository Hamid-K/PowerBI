using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Web;
using System.Xml;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.HostingInterfaces;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000054 RID: 84
	internal static class Global
	{
		// Token: 0x060003AD RID: 941 RVA: 0x000103BA File Offset: 0x0000E5BA
		public static ReportProcessing GetProcessingEngine()
		{
			return new ReportProcessing
			{
				Configuration = Global.ProcessingConfiguration
			};
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060003AE RID: 942 RVA: 0x000103CC File Offset: 0x0000E5CC
		public static bool Hosted
		{
			get
			{
				IRsManagedCallback rsManagedCallback = AppDomain.CurrentDomain.DomainManager as IRsManagedCallback;
				return rsManagedCallback != null;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060003AF RID: 943 RVA: 0x000103ED File Offset: 0x0000E5ED
		public static bool ApplicationMode
		{
			get
			{
				return Global.ParamsContainSwitch(Environment.GetCommandLineArgs(), "app");
			}
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00010400 File Offset: 0x0000E600
		public static bool ParamsContainSwitch(string[] args, string checkSwitch)
		{
			foreach (string text in args)
			{
				if (string.Compare(text, "/" + checkSwitch, StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(text, "-" + checkSwitch, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0001044C File Offset: 0x0000E64C
		public static AppDomain GetAppDomain(RsAppDomainType appDomainType)
		{
			IRsManagedCallback rsManagedCallback = AppDomain.CurrentDomain.DomainManager as IRsManagedCallback;
			if (rsManagedCallback != null)
			{
				return rsManagedCallback.GetAppDomain(appDomainType);
			}
			return null;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00010478 File Offset: 0x0000E678
		public static void SetInitialized()
		{
			IRsManagedCallback rsManagedCallback = Globals.GetDefaultDomain().DomainManager as IRsManagedCallback;
			if (rsManagedCallback != null)
			{
				rsManagedCallback.SetInitialized();
			}
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x000104A0 File Offset: 0x0000E6A0
		public static bool IsInitialized()
		{
			IRsManagedCallback rsManagedCallback = Globals.GetDefaultDomain().DomainManager as IRsManagedCallback;
			return rsManagedCallback != null && rsManagedCallback.IsInitialized();
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x000104C8 File Offset: 0x0000E6C8
		public static void UnloadAppDomain(RsAppDomainType appDomainType, bool memoryRecycle)
		{
			IRsManagedCallback rsManagedCallback = AppDomain.CurrentDomain.DomainManager as IRsManagedCallback;
			if (rsManagedCallback != null)
			{
				rsManagedCallback.UnloadAppDomain(appDomainType, memoryRecycle);
			}
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x000104F0 File Offset: 0x0000E6F0
		public static void UnloadAppDomain(AppDomain appDomain, bool memoryRecycle)
		{
			IRsManagedCallback rsManagedCallback = AppDomain.CurrentDomain.DomainManager as IRsManagedCallback;
			if (rsManagedCallback != null)
			{
				rsManagedCallback.UnloadAppDomain(appDomain, memoryRecycle);
			}
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00010518 File Offset: 0x0000E718
		public static RsMemoryPressureLevel GetCurrentMemoryLevel()
		{
			RsMemoryPressureLevel rsMemoryPressureLevel = RsMemoryPressureLevel.Unknown;
			IRsUnmanagedCallback irsUnmanagedCallback = Global.IRsUnmanagedCallback;
			if (irsUnmanagedCallback != null)
			{
				rsMemoryPressureLevel = irsUnmanagedCallback.GetCurrentMemoryLevel();
			}
			return rsMemoryPressureLevel;
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x00010538 File Offset: 0x0000E738
		public static IRsUnmanagedCallback IRsUnmanagedCallback
		{
			get
			{
				IServiceProvider serviceProvider = AppDomain.CurrentDomain.DomainManager as IServiceProvider;
				if (serviceProvider != null)
				{
					return (IRsUnmanagedCallback)serviceProvider.GetService(typeof(IRsUnmanagedCallback));
				}
				return null;
			}
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00010570 File Offset: 0x0000E770
		public static bool UseLocalFileStore(bool isPermanentSnapshot)
		{
			if (isPermanentSnapshot)
			{
				return false;
			}
			switch (Global.TemporaryStorageSettings)
			{
			case SnapshotTemporaryStorage.True:
			case SnapshotTemporaryStorage.DataOnly:
				return true;
			case SnapshotTemporaryStorage.False:
				return false;
			default:
				throw new InternalCatalogException("Unknown SnapshotTemporaryStorage setting");
			}
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x000105AC File Offset: 0x0000E7AC
		public static bool UseLocalFileStoreForChunkType(bool isPermanentSnapshot, int chunkType)
		{
			if (isPermanentSnapshot)
			{
				return false;
			}
			switch (Global.TemporaryStorageSettings)
			{
			case SnapshotTemporaryStorage.True:
				return true;
			case SnapshotTemporaryStorage.False:
				return false;
			case SnapshotTemporaryStorage.DataOnly:
				return 5 == chunkType;
			default:
				throw new InternalCatalogException("Unknown SnapshotTemporaryStorage setting");
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060003BA RID: 954 RVA: 0x000105EC File Offset: 0x0000E7EC
		private static SnapshotTemporaryStorage TemporaryStorageSettings
		{
			[DebuggerStepThrough]
			get
			{
				RunningApplication currentApplication = Globals.CurrentApplication;
				if (currentApplication == RunningApplication.WindowsService)
				{
					return Globals.Configuration.WindowsServiceUseFileShareStorage;
				}
				if (currentApplication != RunningApplication.WebService)
				{
					return SnapshotTemporaryStorage.False;
				}
				return Globals.Configuration.WebServiceUseFileShareStore;
			}
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00010620 File Offset: 0x0000E820
		public static bool CompressSnapshots(bool isPermanentSnapshot)
		{
			SnapshotCompressionFlags snapshotCompressionSetting = Global.SnapshotCompressionSetting;
			return snapshotCompressionSetting == SnapshotCompressionFlags.All || (snapshotCompressionSetting == SnapshotCompressionFlags.SQL && !Global.UseLocalFileStore(isPermanentSnapshot));
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060003BC RID: 956 RVA: 0x00010648 File Offset: 0x0000E848
		public static PartitionManager PartitionManager
		{
			get
			{
				if (Global.m_partitionManager == null)
				{
					string text = Globals.Configuration.FileShareStoragePath;
					string[] array;
					if (text == null)
					{
						List<string> list = new List<string>(2);
						IServiceInstanceContext serviceInstanceContext = Microsoft.ReportingServices.Diagnostics.ProcessingContext.ServiceInstanceContext;
						if (serviceInstanceContext == null)
						{
							text = RSTrace.CatalogTrace.TraceDirectory;
							if (!string.IsNullOrEmpty(text))
							{
								text = new DirectoryInfo(text).Parent.FullName;
								text = Path.Combine(text, "ReportServer\\RSTempFiles");
								list.Add(text);
							}
						}
						else
						{
							list.Add(serviceInstanceContext.TempFilesDirectoryPath);
						}
						string tempPath = Path.GetTempPath();
						list.Add(tempPath);
						array = list.ToArray();
					}
					else
					{
						array = new string[] { text };
					}
					Global.m_partitionManager = new PartitionManager(array);
				}
				return Global.m_partitionManager;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060003BD RID: 957 RVA: 0x000053DC File Offset: 0x000035DC
		public static bool SaveSessionAsync
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060003BE RID: 958 RVA: 0x000106F7 File Offset: 0x0000E8F7
		public static bool RecycleOnSevereErrors
		{
			get
			{
				return Globals.Configuration.RecycleProcessOnSevereErrors == RSConfiguration.RecycleOptions.Recycle;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060003BF RID: 959 RVA: 0x00010706 File Offset: 0x0000E906
		public static int SessionTimeoutSeconds
		{
			get
			{
				return CachedSystemProperties.SessionTimeoutSeconds;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x0001070D File Offset: 0x0000E90D
		public static int RdlxReportTimeoutSeconds
		{
			get
			{
				return CachedSystemProperties.RdlxReportTimeoutSeconds;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x00010714 File Offset: 0x0000E914
		public static bool EnableClientPrinting
		{
			get
			{
				return CachedSystemProperties.EnableClientPrinting;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x0001071B File Offset: 0x0000E91B
		internal static int SessionAccessTimeout
		{
			get
			{
				return CachedSystemProperties.SessionAccessTimeout;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x00010722 File Offset: 0x0000E922
		internal static int EditSessionTimeoutMinutes
		{
			get
			{
				return CachedSystemProperties.EditSessionTimeoutMinutes;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x00010729 File Offset: 0x0000E929
		internal static int EditSessionCacheLimit
		{
			get
			{
				return CachedSystemProperties.EditSessionCacheLimit;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x00010730 File Offset: 0x0000E930
		public static bool EnableRemoteErrors
		{
			get
			{
				return CachedSystemProperties.EnableRemoteErrors;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x00010737 File Offset: 0x0000E937
		internal static SnapshotCompressionFlags SnapshotCompressionSetting
		{
			get
			{
				return CachedSystemProperties.SnapshotCompressionSetting;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x0001073E File Offset: 0x0000E93E
		internal static ExecutionLogLevel ExecutionLogLevel
		{
			get
			{
				return CachedSystemProperties.ExecutionLogLevel;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x00010745 File Offset: 0x0000E945
		public static bool EnableIntegratedSecurity
		{
			get
			{
				return CachedSystemProperties.EnableIntegratedSecurity;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x0001074C File Offset: 0x0000E94C
		public static int ExternalImagesTimeout
		{
			get
			{
				return CachedSystemProperties.ExternalImagesTimeout;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060003CA RID: 970 RVA: 0x00010753 File Offset: 0x0000E953
		public static int StoredParametersThreshold
		{
			get
			{
				return CachedSystemProperties.StoredParametersThreshold;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060003CB RID: 971 RVA: 0x0001075A File Offset: 0x0000E95A
		public static int StoredParametersLifetime
		{
			get
			{
				return CachedSystemProperties.StoredParametersLifetime;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060003CC RID: 972 RVA: 0x00010761 File Offset: 0x0000E961
		public static bool SessionCookies
		{
			get
			{
				return CachedSystemProperties.SessionCookies;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060003CD RID: 973 RVA: 0x00010768 File Offset: 0x0000E968
		public static int ResponseBufferSizeKb
		{
			get
			{
				return CachedSystemProperties.ResponseBufferSizeKb;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060003CE RID: 974 RVA: 0x0001076F File Offset: 0x0000E96F
		public static int ResponseBufferSizeBytes
		{
			get
			{
				return Global.ResponseBufferSizeKb * 1024;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060003CF RID: 975 RVA: 0x0001077C File Offset: 0x0000E97C
		public static int SqlStreamingBufferSize
		{
			get
			{
				return CachedSystemProperties.SqlStreamingBufferSize;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x0000F918 File Offset: 0x0000DB18
		public static int MaxFileSizeMb
		{
			get
			{
				return CachedSystemProperties.MaxFileSizeMb;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x00010783 File Offset: 0x0000E983
		public static int ChunkSegmentSize
		{
			get
			{
				return CachedSystemProperties.ChunkSegmentSize;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x0001078A File Offset: 0x0000E98A
		public static bool EnableTestConnectionDetailedErrors
		{
			get
			{
				return CachedSystemProperties.EnableTestConnectionDetailedErrors;
			}
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00010794 File Offset: 0x0000E994
		public static string PrintCacheContents()
		{
			string text = "Cache content:";
			foreach (object obj in HttpRuntime.Cache)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				text += "Key: ";
				text += dictionaryEntry.Key;
				text += "\n";
			}
			return text;
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x00010814 File Offset: 0x0000EA14
		public static int BufferInitialSize
		{
			get
			{
				return 4096;
			}
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0001081C File Offset: 0x0000EA1C
		public static string OperationsToXml(StringCollection operations)
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
			xmlTextWriter.WriteStartElement("Permissions");
			foreach (string text in operations)
			{
				xmlTextWriter.WriteElementString("Operation", text);
			}
			xmlTextWriter.WriteEndElement();
			return stringWriter.ToString();
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x00005BEF File Offset: 0x00003DEF
		public static bool SharePointIntegratedFlagFromCatalog
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x0001089C File Offset: 0x0000EA9C
		public static SystemProperties ConfigurationFromCatalog
		{
			get
			{
				SystemProperties systemProperties = null;
				if (Globals.CurrentApplicationHasCatalogAccess)
				{
					DBInterface dbinterface = new DBInterface();
					try
					{
						ConnectionManager connectionManager = new ConnectionManager(ConnectionTransactionType.AutoCommit, IsolationLevel.ReadCommitted);
						dbinterface.ConnectionManager = connectionManager;
						connectionManager.GetUnverifiedConnection();
						dbinterface.ConnectionManager.WillDisconnectStorage();
						systemProperties = dbinterface.GetAllConfigurationInfo();
					}
					finally
					{
						dbinterface.DisconnectStorage();
					}
				}
				return systemProperties;
			}
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00010900 File Offset: 0x0000EB00
		internal static byte[] NameToSid(string name, AuthenticationType authType)
		{
			if (authType != AuthenticationType.Windows)
			{
				return null;
			}
			return Native.NameToSid(name);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0001090E File Offset: 0x0000EB0E
		internal static byte[] NameToSid(UserContext user)
		{
			if (user.AuthenticationType != AuthenticationType.Windows)
			{
				return null;
			}
			return Native.NameToSid(user.UserName);
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00010926 File Offset: 0x0000EB26
		internal static byte[] GetSystemSid(AuthenticationType authType)
		{
			if (authType != AuthenticationType.Windows)
			{
				return null;
			}
			return Native.GetSystemSid();
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00010933 File Offset: 0x0000EB33
		internal static string GetSystemUserName(AuthenticationType authType)
		{
			if (authType != AuthenticationType.Windows)
			{
				return UserUtil.CustomAuthSystemUserName;
			}
			return Native.GetSystemUserName();
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060003DC RID: 988 RVA: 0x00010944 File Offset: 0x0000EB44
		public static IConfiguration ProcessingConfiguration
		{
			get
			{
				if (Global.m_processingConfiguration == null)
				{
					Global.m_processingConfiguration = new ProcessingConfiguration();
				}
				return Global.m_processingConfiguration;
			}
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0001095C File Offset: 0x0000EB5C
		internal static void CheckItemPath(string itemPath, ItemType type, string parameterName)
		{
			if (itemPath == null)
			{
				throw new MissingParameterException(parameterName);
			}
			if (string.Empty == itemPath.Trim())
			{
				throw new InvalidItemPathException(itemPath, parameterName);
			}
			try
			{
				Path.GetExtension(itemPath);
			}
			catch (ArgumentException ex)
			{
				throw new InvalidItemPathException(itemPath, parameterName, ex);
			}
		}

		// Token: 0x060003DE RID: 990 RVA: 0x000109B4 File Offset: 0x0000EBB4
		internal static void CheckItemName(string itemName, ItemType type, string parameterName)
		{
			Global.CheckNativeItemName(itemName, type, parameterName);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x000109C0 File Offset: 0x0000EBC0
		internal static string CheckNativeItemName(string itemName, ItemType type, string parameterName)
		{
			if (itemName == null)
			{
				throw new MissingParameterException(parameterName);
			}
			if (string.Empty == itemName.Trim())
			{
				throw new InvalidItemNameException(itemName, CatalogItemNameUtility.MaxItemNameLength);
			}
			string extension;
			try
			{
				extension = Path.GetExtension(itemName);
			}
			catch (ArgumentException)
			{
				throw new InvalidItemNameException(itemName, CatalogItemNameUtility.MaxItemNameLength);
			}
			return extension;
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00010A20 File Offset: 0x0000EC20
		private static void CheckItemExtension(string pathExtension, ItemType type)
		{
			if (string.IsNullOrEmpty(pathExtension))
			{
				throw new FileExtensionMissingException();
			}
			switch (type)
			{
			case ItemType.Report:
				if (!string.Equals(pathExtension, ".RDL", StringComparison.OrdinalIgnoreCase))
				{
					throw new FileExtensionMissingException();
				}
				break;
			case ItemType.Resource:
			case ItemType.LinkedReport:
			case ItemType.Site:
				break;
			case ItemType.DataSource:
				if (!string.Equals(pathExtension, ".RSDS", StringComparison.OrdinalIgnoreCase) && !string.Equals(pathExtension, ".ODC", StringComparison.OrdinalIgnoreCase))
				{
					throw new FileExtensionMissingException();
				}
				break;
			case ItemType.Model:
				if (!string.Equals(pathExtension, ".SMDL", StringComparison.OrdinalIgnoreCase))
				{
					throw new FileExtensionMissingException();
				}
				break;
			case ItemType.DataSet:
				if (!string.Equals(pathExtension, ".RSD", StringComparison.OrdinalIgnoreCase))
				{
					throw new FileExtensionMissingException();
				}
				break;
			case ItemType.Component:
				if (!string.Equals(pathExtension, ".RSC", StringComparison.OrdinalIgnoreCase))
				{
					throw new FileExtensionMissingException();
				}
				break;
			case ItemType.RdlxReport:
				if (!string.Equals(pathExtension, ".RDLX", StringComparison.OrdinalIgnoreCase))
				{
					throw new FileExtensionMissingException();
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00010AF0 File Offset: 0x0000ECF0
		internal static ItemType ItemTypeFromItemPath(string itemPath, string parameterName)
		{
			if (itemPath == null)
			{
				throw new MissingParameterException(parameterName);
			}
			if (!new CatalogItemContext(new RSService(false)).SetPath(itemPath))
			{
				throw new InvalidItemPathException(itemPath, parameterName);
			}
			string extension;
			try
			{
				extension = Path.GetExtension(itemPath);
			}
			catch (ArgumentException ex)
			{
				throw new InvalidItemPathException(itemPath, parameterName, ex);
			}
			if (string.IsNullOrEmpty(extension))
			{
				return ItemType.Unknown;
			}
			if (string.Equals(extension, ".RDL", StringComparison.OrdinalIgnoreCase))
			{
				return ItemType.Report;
			}
			if (string.Equals(extension, ".RSDS", StringComparison.OrdinalIgnoreCase) || string.Equals(extension, ".ODC", StringComparison.OrdinalIgnoreCase))
			{
				return ItemType.DataSource;
			}
			if (string.Equals(extension, ".SMDL", StringComparison.OrdinalIgnoreCase))
			{
				return ItemType.Model;
			}
			if (string.Equals(extension, ".RSD", StringComparison.OrdinalIgnoreCase))
			{
				return ItemType.DataSet;
			}
			if (string.Equals(extension, ".RSC", StringComparison.OrdinalIgnoreCase))
			{
				return ItemType.Component;
			}
			if (string.Equals(extension, ".RDLX", StringComparison.OrdinalIgnoreCase))
			{
				return ItemType.RdlxReport;
			}
			return ItemType.Unknown;
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00010BC4 File Offset: 0x0000EDC4
		internal static void EnsureParameterNotUsedForMode(Globals.ServerMode mode, string param, string paramName)
		{
			if (string.IsNullOrEmpty(param))
			{
				return;
			}
			RSException ex = null;
			if (mode == Globals.ServerMode.Native)
			{
				ex = new UnsupportedParameterForModeException(Globals.ServerMode.Native.ToString(), paramName);
			}
			if (ex != null)
			{
				throw ex.WebServerToSoapException(Global.RecycleOnSevereErrors, Global.EnableRemoteErrors);
			}
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00010C09 File Offset: 0x0000EE09
		internal static Stream GetResourceStream(string name)
		{
			return typeof(Global).Assembly.GetManifestResourceStream(name);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00010C20 File Offset: 0x0000EE20
		internal static byte[] GetResourceBinary(string name)
		{
			Stream resourceStream = Global.GetResourceStream(name);
			byte[] array = StreamSupport.ReadToEndUsingLength(resourceStream);
			resourceStream.Close();
			return array;
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00010C40 File Offset: 0x0000EE40
		static Global()
		{
			ResourceManager resourceManager = new ResourceManager("Microsoft.ReportingServices.Library.RepLibRes", typeof(RepLibRes).Module.Assembly);
			Global.VirtualMyReportsName = resourceManager.GetString("MyReportsFolderName", CultureInfo.InvariantCulture);
			Global.VirtualMyReportsPath = "/" + Global.VirtualMyReportsName;
			Global.VirtualMyReportsPathSlash = Global.VirtualMyReportsPath + "/";
			Global.AllUsersFolderName = resourceManager.GetString("UsersFolderName", CultureInfo.InvariantCulture);
			Global.AllUsersFolderPath = "/" + Global.AllUsersFolderName;
			Global.AllUsersFolderPathSlash = Global.AllUsersFolderPath + "/";
		}

		// Token: 0x0400018B RID: 395
		private const string _TempFolderName = "ReportServer\\RSTempFiles";

		// Token: 0x0400018C RID: 396
		private static PartitionManager m_partitionManager = null;

		// Token: 0x0400018D RID: 397
		private const int m_bufferInitialSize = 4096;

		// Token: 0x0400018E RID: 398
		private static ProcessingConfiguration m_processingConfiguration = null;

		// Token: 0x0400018F RID: 399
		internal const int MaxDescriptionLength = 512;

		// Token: 0x04000190 RID: 400
		internal const int MaxSearchLength = 128;

		// Token: 0x04000191 RID: 401
		internal const int MaxSiteNameLength = 100;

		// Token: 0x04000192 RID: 402
		internal const int SnapshotLimitUnlimited = -1;

		// Token: 0x04000193 RID: 403
		internal const int ReportTimeoutDefaultTimeout = 1800;

		// Token: 0x04000194 RID: 404
		internal const int SnapshotLimitNotSet = -2;

		// Token: 0x04000195 RID: 405
		internal const int MaxMimeTypeLength = 260;

		// Token: 0x04000196 RID: 406
		internal const int RoleNameLength = 260;

		// Token: 0x04000197 RID: 407
		internal const int RoleDescriptionLength = 512;

		// Token: 0x04000198 RID: 408
		internal const int SnapshotMaxExpirationMinutes = 1440;

		// Token: 0x04000199 RID: 409
		internal static readonly string VirtualMyReportsName;

		// Token: 0x0400019A RID: 410
		internal static readonly string VirtualMyReportsPath = "/" + Global.VirtualMyReportsName;

		// Token: 0x0400019B RID: 411
		internal static readonly string VirtualMyReportsPathSlash = Global.VirtualMyReportsPath + "/";

		// Token: 0x0400019C RID: 412
		internal static readonly string AllUsersFolderName = RepLibRes.UsersFolderName;

		// Token: 0x0400019D RID: 413
		internal static readonly string AllUsersFolderPath = "/" + Global.AllUsersFolderName;

		// Token: 0x0400019E RID: 414
		internal static readonly string AllUsersFolderPathSlash = Global.AllUsersFolderPath + "/";

		// Token: 0x0400019F RID: 415
		internal static readonly string PhysicalMyReportsName = "My Reports";

		// Token: 0x040001A0 RID: 416
		internal static readonly string DaxDataExtensionName = "DAX";

		// Token: 0x040001A1 RID: 417
		internal static RSTrace m_Tracer = RSTrace.CatalogTrace;
	}
}
