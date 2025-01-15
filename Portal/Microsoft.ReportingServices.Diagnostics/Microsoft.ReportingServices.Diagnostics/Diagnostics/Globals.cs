using System;
using System.Diagnostics;
using System.Globalization;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Web;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.OnPrem;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200004C RID: 76
	internal static class Globals
	{
		// Token: 0x06000266 RID: 614 RVA: 0x0000B994 File Offset: 0x00009B94
		public static AppDomain GetDefaultDomain()
		{
			object obj = null;
			((Globals.ICorRuntimeHost)new Globals.CorRuntimeHost()).GetDefaultDomain(ref obj);
			return (AppDomain)obj;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000B9BC File Offset: 0x00009BBC
		public static void InitServer(bool resetAllPerfCounters)
		{
			object staticLock = Globals.StaticLock;
			lock (staticLock)
			{
				Globals.InitServerWithoutDumperInner(resetAllPerfCounters);
				Dumper.Current.Init();
			}
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000BA08 File Offset: 0x00009C08
		public static void InitConfiguration(RSConfigurationManager configManager, RunningApplication app)
		{
			object staticLock = Globals.StaticLock;
			lock (staticLock)
			{
				Globals.CurrentApplication = app;
				Globals.ConfigurationManager = configManager;
				Globals.Configuration = configManager.GetConfiguration();
				ProcessingContext.Configuration = new OnPremProcessingRenderingConfiguration(Globals.Configuration, app);
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000BA68 File Offset: 0x00009C68
		public static void InitWithoutDumper(RSConfigurationFileManager configManager, RunningApplication app)
		{
			Globals.InitConfiguration(configManager, app);
			Globals.InitServerWithoutDumper(true);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000BA78 File Offset: 0x00009C78
		private static void InitServerWithoutDumper(bool resetAllPerfCounters)
		{
			object staticLock = Globals.StaticLock;
			lock (staticLock)
			{
				Globals.InitServerWithoutDumperInner(resetAllPerfCounters);
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000BAB8 File Offset: 0x00009CB8
		private static void InitServerWithoutDumperInner(bool resetAllPerfCounters)
		{
			RSTrace.ConfigManagerTracer.Assert(Globals.Configuration != null, "Configuration is not initialized.");
			RunningApplication currentApplication = Globals.CurrentApplication;
			if (currentApplication <= RunningApplication.WebService || currentApplication - RunningApplication.ReportServerWebApp <= 2)
			{
				RSEventLog.Current.Init(new NativeRSEventLogProvider());
				RSTraceInternal.Current.EnsureTraceInitializedCorrectly();
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0000BB05 File Offset: 0x00009D05
		// (set) Token: 0x0600026D RID: 621 RVA: 0x0000BB0C File Offset: 0x00009D0C
		public static RSConfiguration Configuration { get; private set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000BB14 File Offset: 0x00009D14
		public static bool IsConfigurationInitialized
		{
			get
			{
				return Globals.Configuration != null;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600026F RID: 623 RVA: 0x0000BB1E File Offset: 0x00009D1E
		// (set) Token: 0x06000270 RID: 624 RVA: 0x0000BB25 File Offset: 0x00009D25
		internal static RunningApplication CurrentApplication { get; private set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0000BB2D File Offset: 0x00009D2D
		// (set) Token: 0x06000272 RID: 626 RVA: 0x0000BB34 File Offset: 0x00009D34
		internal static RSConfigurationManager ConfigurationManager { get; private set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000BB3C File Offset: 0x00009D3C
		internal static bool IsServiceProcess
		{
			get
			{
				return Globals.CurrentApplication == RunningApplication.WindowsService || Globals.CurrentApplication == RunningApplication.ReportServerWebApp;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000BB3C File Offset: 0x00009D3C
		internal static bool IsDatabaseUpgradeAllowed
		{
			get
			{
				return Globals.CurrentApplication == RunningApplication.WindowsService || Globals.CurrentApplication == RunningApplication.ReportServerWebApp;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000BB4F File Offset: 0x00009D4F
		public static bool NoMemoryThrottling
		{
			get
			{
				return Sku.IsFeatureEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.NoMemoryThrottling);
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000BB62 File Offset: 0x00009D62
		internal static bool IsRecycleAppDomainsAllowed
		{
			get
			{
				return Globals.CurrentApplication != RunningApplication.ReportServerWebApp;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000BB6F File Offset: 0x00009D6F
		public static string ServiceConfigurationFileName
		{
			get
			{
				return "RSReportServer.config";
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000278 RID: 632 RVA: 0x0000BB76 File Offset: 0x00009D76
		public static string SqlSharedCodeDirectory
		{
			get
			{
				if (Globals.m_sqlSharedCodeDirectory == null)
				{
					RevertImpersonationContext.Run(delegate
					{
						try
						{
							Globals.m_sqlSharedCodeDirectory = SqlInstallation.SqlSharedCodeDirectory;
						}
						catch (SqlInstallation.ConfigurationErrorException ex)
						{
							throw new ServerConfigurationErrorException(ex.Message);
						}
					});
				}
				return Globals.m_sqlSharedCodeDirectory;
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000BBA8 File Offset: 0x00009DA8
		internal static bool IsHostedInSOS()
		{
			return AppDomain.CurrentDomain.DomainManager != null && AppDomain.CurrentDomain.DomainManager.GetType().AssemblyQualifiedName == ProcessingContext.RsAppDomainManagerTypeName;
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600027A RID: 634 RVA: 0x0000BBD6 File Offset: 0x00009DD6
		// (set) Token: 0x0600027B RID: 635 RVA: 0x0000BBDF File Offset: 0x00009DDF
		public static bool ApplicationEndCalled
		{
			get
			{
				return Globals.m_applicationEndCalled;
			}
			set
			{
				Globals.m_applicationEndCalled = value;
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000BBE9 File Offset: 0x00009DE9
		internal static bool IsRetriableSmtpError(SmtpStatusCode statusCode)
		{
			if (statusCode <= SmtpStatusCode.MailboxBusy)
			{
				if (statusCode != SmtpStatusCode.ServiceNotAvailable && statusCode != SmtpStatusCode.MailboxBusy)
				{
					return false;
				}
			}
			else if (statusCode != SmtpStatusCode.MustIssueStartTlsFirst && statusCode != SmtpStatusCode.MailboxUnavailable)
			{
				return false;
			}
			return true;
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0000BC18 File Offset: 0x00009E18
		public static bool CurrentApplicationHasCatalogAccess
		{
			get
			{
				return (Globals.CurrentApplication == RunningApplication.WindowsService || Globals.CurrentApplication == RunningApplication.WebService || Globals.CurrentApplication == RunningApplication.ReportServerWebApp) && Globals.Configuration.ConnectionStringSet;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600027E RID: 638 RVA: 0x0000BC40 File Offset: 0x00009E40
		public static bool CanTranslateIncomingHttpRequest
		{
			get
			{
				bool flag = false;
				if (Globals.CurrentApplication == RunningApplication.WebService && Globals.IsConfigurationInitialized)
				{
					flag = Globals.Configuration.CanTranslateReportServerRequest;
				}
				return flag && HttpContext.Current != null;
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000BC76 File Offset: 0x00009E76
		public static object CreateInstance(string assemblyName, string typeName)
		{
			object instance = null;
			RevertImpersonationContext.Run(delegate
			{
				Assembly assembly = Assembly.Load(assemblyName);
				instance = assembly.CreateInstance(typeName);
			});
			return instance;
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000BCA8 File Offset: 0x00009EA8
		public static string CurrentProcessId
		{
			get
			{
				if (Globals.m_currentProcessId == null)
				{
					Globals.m_currentProcessId = Process.GetCurrentProcess().Id.ToString(CultureInfo.InvariantCulture);
				}
				return Globals.m_currentProcessId;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0000BCDD File Offset: 0x00009EDD
		// (set) Token: 0x06000282 RID: 642 RVA: 0x0000BCE4 File Offset: 0x00009EE4
		public static bool DisableEPAuthTypes
		{
			get
			{
				return Globals.m_DisableEPAuthTypes;
			}
			set
			{
				Globals.m_DisableEPAuthTypes = value;
			}
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000BCEC File Offset: 0x00009EEC
		internal static bool IsRetriableSqlError(int sqlErrorNumber)
		{
			if (sqlErrorNumber <= 233)
			{
				if (sqlErrorNumber <= 2)
				{
					if (sqlErrorNumber - -2 > 1 && sqlErrorNumber != 2)
					{
						return false;
					}
				}
				else if (sqlErrorNumber != 53 && sqlErrorNumber != 64 && sqlErrorNumber != 233)
				{
					return false;
				}
			}
			else if (sqlErrorNumber <= 1205)
			{
				switch (sqlErrorNumber)
				{
				case 921:
				case 922:
				case 924:
				case 927:
					break;
				case 923:
				case 925:
				case 926:
					return false;
				default:
					if (sqlErrorNumber - 1203 > 2)
					{
						return false;
					}
					break;
				}
			}
			else if (sqlErrorNumber != 1214 && sqlErrorNumber - 1221 > 1 && sqlErrorNumber - 10060 > 1)
			{
				return false;
			}
			return true;
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0000BD81 File Offset: 0x00009F81
		public static string PublicDateTimeFormat
		{
			get
			{
				return "yyyy-MM-ddTHH:mm:ss.fffzzz";
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000BD88 File Offset: 0x00009F88
		public static string ToPublicDateTimeFormat(DateTime date)
		{
			string text = null;
			if (date != DateTime.MinValue)
			{
				text = date.ToString(Globals.PublicDateTimeFormat, CultureInfo.InvariantCulture);
			}
			return text;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000BDB7 File Offset: 0x00009FB7
		public static DateTime ParsePublicDateTimeFormat(string dateString)
		{
			return DateTime.ParseExact(dateString, Globals.PublicDateTimeFormat, DateTimeFormatInfo.InvariantInfo);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000BDCC File Offset: 0x00009FCC
		public static DateTime ParseSnapshotDateParameter(string snapshotDate, bool nullIsValid)
		{
			if (snapshotDate != null)
			{
				DateTime dateTime;
				try
				{
					dateTime = DateTime.ParseExact(snapshotDate, "s", DateTimeFormatInfo.InvariantInfo);
				}
				catch (FormatException ex)
				{
					throw new ParameterTypeMismatchException("snapshotID", ex);
				}
				return dateTime;
			}
			if (!nullIsValid)
			{
				throw new MissingParameterException("snapshotID");
			}
			return DateTime.MinValue;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000BE24 File Offset: 0x0000A024
		public static string ToSnapshotDateFormat(DateTime dateTime)
		{
			if (dateTime == DateTime.MinValue)
			{
				return null;
			}
			return dateTime.ToString("s", CultureInfo.InvariantCulture);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000BE46 File Offset: 0x0000A046
		public static int ParsePaginationModeParameter(string parameterValue, bool nullIsValid)
		{
			if (parameterValue == null)
			{
				if (!nullIsValid)
				{
					throw new MissingParameterException("PageCountMode");
				}
				return 0;
			}
			else
			{
				if (parameterValue == "Actual")
				{
					return 0;
				}
				if (!(parameterValue == "Estimate"))
				{
					return 0;
				}
				return 1;
			}
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000BE7C File Offset: 0x0000A07C
		public static Guid ParseGuidParameter(string guidString, string parameterName)
		{
			if (guidString == null)
			{
				throw new MissingParameterException(parameterName);
			}
			Guid guid;
			try
			{
				guid = new Guid(guidString);
			}
			catch (FormatException ex)
			{
				throw new ParameterTypeMismatchException(parameterName, ex);
			}
			return guid;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000BEB8 File Offset: 0x0000A0B8
		public static Guid ParseGuidElement(string guidString, string elementName)
		{
			if (guidString == null)
			{
				throw new MissingElementException(elementName);
			}
			Guid guid;
			try
			{
				guid = new Guid(guidString);
			}
			catch (FormatException ex)
			{
				throw new ElementTypeMismatchException(elementName, ex);
			}
			return guid;
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0000BEF4 File Offset: 0x0000A0F4
		public static bool IsAnonymous
		{
			get
			{
				return ProcessingContext.ReqContext == null || ProcessingContext.ReqContext.User == null || !ProcessingContext.ReqContext.User.Identity.IsAuthenticated;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600028D RID: 653 RVA: 0x00004F49 File Offset: 0x00003149
		public static bool IsCloud
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600028E RID: 654 RVA: 0x0000BF22 File Offset: 0x0000A122
		// (set) Token: 0x0600028F RID: 655 RVA: 0x0000BF29 File Offset: 0x0000A129
		public static bool TryRemoteLogon
		{
			get
			{
				return Globals.m_remoteLogon;
			}
			set
			{
				Globals.m_remoteLogon = value;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000290 RID: 656 RVA: 0x0000BF31 File Offset: 0x0000A131
		// (set) Token: 0x06000291 RID: 657 RVA: 0x0000BF38 File Offset: 0x0000A138
		public static JobTypeEnum RunningJobType
		{
			get
			{
				return Globals.m_runningJobType;
			}
			set
			{
				Globals.m_runningJobType = value;
			}
		}

		// Token: 0x0400021F RID: 543
		private static object StaticLock = new object();

		// Token: 0x04000223 RID: 547
		private const string m_ServiceConfigurationFileName = "RSReportServer.config";

		// Token: 0x04000224 RID: 548
		private static string m_sqlSharedCodeDirectory;

		// Token: 0x04000225 RID: 549
		private static volatile bool m_applicationEndCalled = false;

		// Token: 0x04000226 RID: 550
		private static string m_currentProcessId = null;

		// Token: 0x04000227 RID: 551
		private static bool m_DisableEPAuthTypes = false;

		// Token: 0x04000228 RID: 552
		private const string m_snapshotDateFormat = "s";

		// Token: 0x04000229 RID: 553
		private static bool m_remoteLogon = false;

		// Token: 0x0400022A RID: 554
		private static JobTypeEnum m_runningJobType;

		// Token: 0x0200009E RID: 158
		public enum ServerMode
		{
			// Token: 0x0400039D RID: 925
			Native,
			// Token: 0x0400039E RID: 926
			SharePoint
		}

		// Token: 0x0200009F RID: 159
		public enum ServiceStopMode
		{
			// Token: 0x040003A0 RID: 928
			FullStop,
			// Token: 0x040003A1 RID: 929
			StopPollingOnly
		}

		// Token: 0x020000A0 RID: 160
		[Guid("CB2F6723-AB3A-11d2-9C40-00C04FA30A3E")]
		[ComImport]
		private class CorRuntimeHost
		{
			// Token: 0x0600043D RID: 1085
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			public extern CorRuntimeHost();
		}

		// Token: 0x020000A1 RID: 161
		[Guid("CB2F6722-AB3A-11d2-9C40-00C04FA30A3E")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		private interface ICorRuntimeHost
		{
			// Token: 0x0600043E RID: 1086
			void CreateLogicalThreadState();

			// Token: 0x0600043F RID: 1087
			void DeleteLogicalThreadState();

			// Token: 0x06000440 RID: 1088
			void SwitchInLogicalThreadState();

			// Token: 0x06000441 RID: 1089
			void SwitchOutLogicalThreadState();

			// Token: 0x06000442 RID: 1090
			void LocksHeldByLogicalThread();

			// Token: 0x06000443 RID: 1091
			void MapFile();

			// Token: 0x06000444 RID: 1092
			void GetConfiguration();

			// Token: 0x06000445 RID: 1093
			void Start();

			// Token: 0x06000446 RID: 1094
			void Stop();

			// Token: 0x06000447 RID: 1095
			void CreateDomain();

			// Token: 0x06000448 RID: 1096
			void GetDefaultDomain([MarshalAs(UnmanagedType.IUnknown)] ref object o);

			// Token: 0x06000449 RID: 1097
			void EnumDomains();

			// Token: 0x0600044A RID: 1098
			void NextDomain();

			// Token: 0x0600044B RID: 1099
			void CloseEnum();

			// Token: 0x0600044C RID: 1100
			void CreateDomainEx();

			// Token: 0x0600044D RID: 1101
			void CreateDomainSetup();

			// Token: 0x0600044E RID: 1102
			void CreateEvidence();

			// Token: 0x0600044F RID: 1103
			void UnloadDomain();

			// Token: 0x06000450 RID: 1104
			void CurrentDomain();
		}
	}
}
