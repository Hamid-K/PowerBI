using System;
using System.Collections;
using System.Data;
using System.Security.Permissions;
using Microsoft.Data.SqlClient;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000005 RID: 5
	internal sealed class CachedSystemProperties : IParameterSource
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		internal static string Get(string name)
		{
			return CachedSystemProperties.Get(name, false);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000205C File Offset: 0x0000025C
		internal static string Get(string name, bool noThrow)
		{
			Hashtable cache = CachedSystemProperties.Cache;
			Hashtable hashtable = cache;
			bool flag2;
			string text;
			lock (hashtable)
			{
				flag2 = cache.ContainsKey(name);
				text = (string)cache[name];
			}
			if (!flag2)
			{
				text = null;
				if (noThrow)
				{
					try
					{
						text = CachedSystemProperties.GetSystemProperty(name);
						goto IL_0052;
					}
					catch (RSException)
					{
						text = null;
						goto IL_0052;
					}
				}
				text = CachedSystemProperties.GetSystemProperty(name);
				IL_0052:
				hashtable = cache;
				lock (hashtable)
				{
					cache[name] = text;
				}
			}
			return text;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002108 File Offset: 0x00000308
		internal static void InvalidateCache()
		{
			CachedSystemProperties.Cache = null;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002110 File Offset: 0x00000310
		public string GetParameter(string name)
		{
			return this.GetParameter(name, false);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000211A File Offset: 0x0000031A
		public string GetParameter(string name, bool noThrow)
		{
			return CachedSystemProperties.Get(name);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002122 File Offset: 0x00000322
		public string GetSourceNameForTrace()
		{
			return "Server system properties";
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002129 File Offset: 0x00000329
		public bool UseExternalStore
		{
			get
			{
				return Globals.CurrentApplication == RunningApplication.WebService;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002134 File Offset: 0x00000334
		public static int SessionTimeoutSeconds
		{
			get
			{
				if (CachedSystemProperties.m_SessionTimeoutSecondsParam == null)
				{
					CachedSystemProperties.m_SessionTimeoutSecondsParam = new IntParameter(CachedSystemProperties.Instance, RSTrace.CatalogTrace, "SessionTimeout", CachedSystemProperties.Instance.GetParameter("SessionTimeout"), CachedSystemProperties.m_DefaultSessionTimeoutSeconds, "second(s)");
					CachedSystemProperties.m_SessionTimeoutSecondsParam.MinValue = 10;
					CachedSystemProperties.m_SessionTimeoutSecondsParam.TraceSuccess = true;
				}
				return CachedSystemProperties.m_SessionTimeoutSecondsParam.Value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000219C File Offset: 0x0000039C
		public static int RdlxReportTimeoutSeconds
		{
			get
			{
				if (CachedSystemProperties.m_rdlxReportTimeoutSecondsParam == null)
				{
					CachedSystemProperties.m_rdlxReportTimeoutSecondsParam = new IntParameter(CachedSystemProperties.Instance, RSTrace.CatalogTrace, "RDLXReportTimeout", CachedSystemProperties.Instance.GetParameter("RDLXReportTimeout"), CachedSystemProperties.m_defaultRdlxReportTimeoutSeconds, "second(s)");
					CachedSystemProperties.m_rdlxReportTimeoutSecondsParam.MinValue = 10;
					CachedSystemProperties.m_rdlxReportTimeoutSecondsParam.TraceSuccess = true;
				}
				return CachedSystemProperties.m_rdlxReportTimeoutSecondsParam.Value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002204 File Offset: 0x00000404
		public static bool EnableClientPrinting
		{
			get
			{
				if (CachedSystemProperties.m_EnableClientPrintingParam == null)
				{
					CachedSystemProperties.m_EnableClientPrintingParam = new BooleanParameter(CachedSystemProperties.Instance, RSTrace.CatalogTrace, "EnableClientPrinting", CachedSystemProperties.Instance.GetParameter("EnableClientPrinting"), CachedSystemProperties.m_DefaultEnableClientPrinting, "");
					CachedSystemProperties.m_EnableClientPrintingParam.TraceSuccess = true;
				}
				return CachedSystemProperties.m_EnableClientPrintingParam.Value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002260 File Offset: 0x00000460
		internal static int SessionAccessTimeout
		{
			get
			{
				if (CachedSystemProperties.m_SessionAccessTimeoutParam == null)
				{
					CachedSystemProperties.m_SessionAccessTimeoutParam = new IntParameter(CachedSystemProperties.Instance, RSTrace.CatalogTrace, "SessionAccessTimeout", CachedSystemProperties.Instance.GetParameter("SessionAccessTimeout"), 600, "seconds");
					CachedSystemProperties.m_SessionAccessTimeoutParam.TraceSuccess = true;
					CachedSystemProperties.m_SessionAccessTimeoutParam.MinValue = 10;
				}
				return CachedSystemProperties.m_SessionAccessTimeoutParam.Value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000022C8 File Offset: 0x000004C8
		internal static int EditSessionTimeoutMinutes
		{
			get
			{
				if (CachedSystemProperties.m_editsessionTimeoutParam == null)
				{
					CachedSystemProperties.m_editsessionTimeoutParam = new IntParameter(CachedSystemProperties.Instance, RSTrace.CatalogTrace, "EditSessionTimeout", CachedSystemProperties.Instance.GetParameter("EditSessionTimeout"), 7200, "seconds");
					CachedSystemProperties.m_editsessionTimeoutParam.TraceSuccess = true;
					CachedSystemProperties.m_editsessionTimeoutParam.MinValue = 60;
				}
				return (CachedSystemProperties.m_editsessionTimeoutParam.Value + 59) / 60;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002338 File Offset: 0x00000538
		internal static int EditSessionCacheLimit
		{
			get
			{
				if (CachedSystemProperties.m_editSessionCacheLimitParam == null)
				{
					CachedSystemProperties.m_editSessionCacheLimitParam = new IntParameter(CachedSystemProperties.Instance, RSTrace.CatalogTrace, "EditSessionCacheLimit", CachedSystemProperties.Instance.GetParameter("EditSessionCacheLimit"), 5, "");
					CachedSystemProperties.m_editSessionCacheLimitParam.TraceSuccess = true;
					CachedSystemProperties.m_editSessionCacheLimitParam.MinValue = 0;
				}
				return CachedSystemProperties.m_editSessionCacheLimitParam.Value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000E RID: 14 RVA: 0x0000239C File Offset: 0x0000059C
		public static bool EnableRemoteErrors
		{
			get
			{
				bool flag;
				try
				{
					if (CachedSystemProperties.m_EnableRemoteErrorsParam == null)
					{
						CachedSystemProperties.m_EnableRemoteErrorsParam = new BooleanParameter(CachedSystemProperties.Instance, RSTrace.CatalogTrace, "EnableRemoteErrors", CachedSystemProperties.Instance.GetParameter("EnableRemoteErrors", true), false, "");
						CachedSystemProperties.m_EnableRemoteErrorsParam.TraceSuccess = true;
					}
					flag = CachedSystemProperties.m_EnableRemoteErrorsParam.Value;
				}
				catch (Exception)
				{
					flag = false;
				}
				return flag;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002410 File Offset: 0x00000610
		internal static SnapshotCompressionFlags SnapshotCompressionSetting
		{
			get
			{
				if (CachedSystemProperties.m_SnapshotCompressionSettingParam == null)
				{
					CachedSystemProperties.m_SnapshotCompressionSettingParam = new EnumParameter<SnapshotCompressionFlags>(CachedSystemProperties.Instance, RSTrace.CatalogTrace, "SnapshotCompression", CachedSystemProperties.Instance.GetParameter("SnapshotCompression"), SnapshotCompressionFlags.SQL, "");
					CachedSystemProperties.m_SnapshotCompressionSettingParam.TraceSuccess = true;
				}
				return CachedSystemProperties.m_SnapshotCompressionSettingParam.Value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002468 File Offset: 0x00000668
		internal static ExecutionLogLevel ExecutionLogLevel
		{
			get
			{
				if (CachedSystemProperties.m_executionLogLevelParam == null)
				{
					CachedSystemProperties.m_executionLogLevelParam = new EnumParameter<ExecutionLogLevel>(CachedSystemProperties.Instance, RSTrace.CatalogTrace, "ExecutionLogLevel", CachedSystemProperties.Instance.GetParameter("ExecutionLogLevel"), ExecutionLogLevel.Normal, string.Empty);
					CachedSystemProperties.m_executionLogLevelParam.TraceSuccess = true;
				}
				return CachedSystemProperties.m_executionLogLevelParam.Value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000024C0 File Offset: 0x000006C0
		public static bool EnableIntegratedSecurity
		{
			get
			{
				if (CachedSystemProperties.m_EnableIntegratedSecurityParam == null)
				{
					CachedSystemProperties.m_EnableIntegratedSecurityParam = new BooleanParameter(CachedSystemProperties.Instance, RSTrace.CatalogTrace, "EnableIntegratedSecurity", CachedSystemProperties.Instance.GetParameter("EnableIntegratedSecurity"), true, "");
					CachedSystemProperties.m_EnableIntegratedSecurityParam.TraceSuccess = true;
				}
				return CachedSystemProperties.m_EnableIntegratedSecurityParam.Value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002518 File Offset: 0x00000718
		public static int ExternalImagesTimeout
		{
			get
			{
				if (CachedSystemProperties.m_ExternalImagesTimeoutParam == null)
				{
					CachedSystemProperties.m_ExternalImagesTimeoutParam = new IntParameter(CachedSystemProperties.Instance, RSTrace.CatalogTrace, "ExternalImagesTimeout", CachedSystemProperties.Instance.GetParameter("ExternalImagesTimeout"), CachedSystemProperties.m_externalImagesTimeoutDefault, "second(s)");
					CachedSystemProperties.m_ExternalImagesTimeoutParam.TraceSuccess = true;
				}
				return CachedSystemProperties.m_ExternalImagesTimeoutParam.Value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002574 File Offset: 0x00000774
		public static int StoredParametersThreshold
		{
			get
			{
				if (CachedSystemProperties.m_storedParametersThreshold == null)
				{
					CachedSystemProperties.m_storedParametersThreshold = new IntParameter(CachedSystemProperties.Instance, RSTrace.CatalogTrace, "StoredParametersThreshold", CachedSystemProperties.Instance.GetParameter("StoredParametersThreshold"), CachedSystemProperties.m_defaultStoredParametersThreshold, "characters");
					CachedSystemProperties.m_storedParametersThreshold.TraceSuccess = true;
					CachedSystemProperties.m_storedParametersThreshold.MinValue = 1;
				}
				return CachedSystemProperties.m_storedParametersThreshold.Value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000025DC File Offset: 0x000007DC
		public static int StoredParametersLifetime
		{
			get
			{
				if (CachedSystemProperties.m_storedParametersLifetime == null)
				{
					CachedSystemProperties.m_storedParametersLifetime = new IntParameter(CachedSystemProperties.Instance, RSTrace.CatalogTrace, "StoredParametersLifetime", CachedSystemProperties.Instance.GetParameter("StoredParametersLifetime"), CachedSystemProperties.m_defaultStoredParametersLifetime, "days");
					CachedSystemProperties.m_storedParametersLifetime.TraceSuccess = true;
					CachedSystemProperties.m_storedParametersLifetime.MinValue = 1;
				}
				return CachedSystemProperties.m_storedParametersLifetime.Value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002644 File Offset: 0x00000844
		public static bool SessionCookies
		{
			get
			{
				if (CachedSystemProperties.m_SessionCookiesParam == null)
				{
					CachedSystemProperties.m_SessionCookiesParam = new BooleanParameter(CachedSystemProperties.Instance, RSTrace.CatalogTrace, "UseSessionCookies", CachedSystemProperties.Instance.GetParameter("UseSessionCookies"), CachedSystemProperties.m_DefaultSessionCookies, "");
					CachedSystemProperties.m_SessionCookiesParam.TraceSuccess = true;
				}
				return CachedSystemProperties.m_SessionCookiesParam.Value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000026A0 File Offset: 0x000008A0
		public static int ResponseBufferSizeKb
		{
			get
			{
				if (CachedSystemProperties.m_ResponseBufferSizeKbParam == null)
				{
					CachedSystemProperties.m_ResponseBufferSizeKbParam = new IntParameter(CachedSystemProperties.Instance, RSTrace.CatalogTrace, "ResponseBufferSizeKb", CachedSystemProperties.Instance.GetParameter("ResponseBufferSizeKb"), CachedSystemProperties.m_DefaultResponseBufferSizeKb, "KB");
					CachedSystemProperties.m_ResponseBufferSizeKbParam.MinValue = 1;
					CachedSystemProperties.m_ResponseBufferSizeKbParam.MaxValue = 20480;
					CachedSystemProperties.m_ResponseBufferSizeKbParam.TraceSuccess = true;
				}
				return CachedSystemProperties.m_ResponseBufferSizeKbParam.Value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002718 File Offset: 0x00000918
		public static int MaxFileSizeMb
		{
			get
			{
				if (CachedSystemProperties.m_MaxFileSizeMb == null)
				{
					CachedSystemProperties.m_MaxFileSizeMb = new IntParameter(CachedSystemProperties.Instance, RSTrace.CatalogTrace, "MaxFileSizeMb", CachedSystemProperties.Instance.GetParameter("MaxFileSizeMb"), -1, "MB");
					CachedSystemProperties.m_MaxFileSizeMb.MinValue = -1;
					CachedSystemProperties.m_MaxFileSizeMb.MaxValue = int.MaxValue;
					CachedSystemProperties.m_MaxFileSizeMb.TraceSuccess = true;
				}
				return CachedSystemProperties.m_MaxFileSizeMb.Value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000018 RID: 24 RVA: 0x0000278C File Offset: 0x0000098C
		public static int SqlStreamingBufferSize
		{
			get
			{
				if (CachedSystemProperties.m_SqlStreamingBufferSizeParam == null)
				{
					CachedSystemProperties.m_SqlStreamingBufferSizeParam = new IntParameter(CachedSystemProperties.Instance, RSTrace.CatalogTrace, "SqlStreamingBufferSize", CachedSystemProperties.Instance.GetParameter("SqlStreamingBufferSize"), CachedSystemProperties.m_DefaultSqlStreamingBufferSize, "Bytes");
					CachedSystemProperties.m_SqlStreamingBufferSizeParam.MinValue = 1024;
					CachedSystemProperties.m_SqlStreamingBufferSizeParam.MaxValue = 104857600;
					CachedSystemProperties.m_SqlStreamingBufferSizeParam.TraceSuccess = true;
				}
				return CachedSystemProperties.m_SqlStreamingBufferSizeParam.Value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002808 File Offset: 0x00000A08
		public static int ChunkSegmentSize
		{
			get
			{
				if (CachedSystemProperties.m_ChunkSegmentSizeParam == null)
				{
					CachedSystemProperties.m_ChunkSegmentSizeParam = new IntParameter(CachedSystemProperties.Instance, RSTrace.CatalogTrace, "ChunkSegmentSize", CachedSystemProperties.Instance.GetParameter("ChunkSegmentSize"), CachedSystemProperties.SqlStreamingBufferSize / 2, "Bytes");
					CachedSystemProperties.m_ChunkSegmentSizeParam.MinValue = 1024;
					CachedSystemProperties.m_ChunkSegmentSizeParam.MaxValue = 104857600;
					CachedSystemProperties.m_ChunkSegmentSizeParam.TraceSuccess = true;
				}
				return CachedSystemProperties.m_ChunkSegmentSizeParam.Value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002884 File Offset: 0x00000A84
		public static bool EnableTestConnectionDetailedErrors
		{
			get
			{
				if (CachedSystemProperties.m_enableTestConnectionDetailedErrorsParam == null)
				{
					CachedSystemProperties.m_enableTestConnectionDetailedErrorsParam = new BooleanParameter(CachedSystemProperties.Instance, RSTrace.CatalogTrace, "EnableTestConnectionDetailedErrors", CachedSystemProperties.Instance.GetParameter("EnableTestConnectionDetailedErrors"), true, "");
					CachedSystemProperties.m_enableTestConnectionDetailedErrorsParam.TraceSuccess = true;
				}
				return CachedSystemProperties.m_enableTestConnectionDetailedErrorsParam.Value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000028DC File Offset: 0x00000ADC
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002930 File Offset: 0x00000B30
		private static Hashtable Cache
		{
			get
			{
				object lockObj = CachedSystemProperties.m_lockObj;
				Hashtable cache;
				lock (lockObj)
				{
					if (CachedSystemProperties.m_Cache == null)
					{
						CachedSystemProperties.m_Cache = new Hashtable();
					}
					cache = CachedSystemProperties.m_Cache;
				}
				return cache;
			}
			set
			{
				object lockObj = CachedSystemProperties.m_lockObj;
				lock (lockObj)
				{
					CachedSystemProperties.m_Cache = value;
				}
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002970 File Offset: 0x00000B70
		[SqlClientPermission(SecurityAction.Assert, Unrestricted = true, AllowBlankPassword = true)]
		[SecurityPermission(SecurityAction.Assert, Unrestricted = true, ControlPrincipal = true)]
		private static string GetSystemProperty(string name)
		{
			if (Globals.CurrentApplicationHasCatalogAccess)
			{
				Storage storage = new Storage();
				try
				{
					storage.ConnectionManager = new ConnectionManager(ConnectionTransactionType.AutoCommit, IsolationLevel.ReadCommitted);
					storage.ConnectionManager.WillDisconnectStorage();
					using (InstrumentedSqlCommand instrumentedSqlCommand = storage.NewStandardSqlCommand("GetOneConfigurationInfo", null))
					{
						instrumentedSqlCommand.AddParameter("@Name", SqlDbType.NVarChar, name);
						return (string)instrumentedSqlCommand.ExecuteScalar();
					}
				}
				finally
				{
					storage.DisconnectStorage();
				}
			}
			return Globals.Configuration.Settings[name];
		}

		// Token: 0x04000035 RID: 53
		internal static readonly CachedSystemProperties Instance = new CachedSystemProperties();

		// Token: 0x04000036 RID: 54
		private static readonly int m_DefaultSessionTimeoutSeconds = 600;

		// Token: 0x04000037 RID: 55
		private static IntParameter m_SessionTimeoutSecondsParam = null;

		// Token: 0x04000038 RID: 56
		internal const int SessionTimeoutMinimum = 10;

		// Token: 0x04000039 RID: 57
		private static readonly int m_defaultRdlxReportTimeoutSeconds = 1800;

		// Token: 0x0400003A RID: 58
		private static IntParameter m_rdlxReportTimeoutSecondsParam = null;

		// Token: 0x0400003B RID: 59
		private static readonly bool m_DefaultEnableClientPrinting = false;

		// Token: 0x0400003C RID: 60
		private static BooleanParameter m_EnableClientPrintingParam = null;

		// Token: 0x0400003D RID: 61
		private const int SessionAccessDefaultTimeout = 600;

		// Token: 0x0400003E RID: 62
		private static IntParameter m_SessionAccessTimeoutParam = null;

		// Token: 0x0400003F RID: 63
		private const int EditSessionTimeoutDefault = 7200;

		// Token: 0x04000040 RID: 64
		private static IntParameter m_editsessionTimeoutParam = null;

		// Token: 0x04000041 RID: 65
		private const int EditSessionCacheLimitDefault = 5;

		// Token: 0x04000042 RID: 66
		private static IntParameter m_editSessionCacheLimitParam = null;

		// Token: 0x04000043 RID: 67
		private const bool DefaultEnableRemoteErrors = false;

		// Token: 0x04000044 RID: 68
		private static BooleanParameter m_EnableRemoteErrorsParam = null;

		// Token: 0x04000045 RID: 69
		private static EnumParameter<SnapshotCompressionFlags> m_SnapshotCompressionSettingParam = null;

		// Token: 0x04000046 RID: 70
		private static EnumParameter<ExecutionLogLevel> m_executionLogLevelParam;

		// Token: 0x04000047 RID: 71
		private static BooleanParameter m_EnableIntegratedSecurityParam = null;

		// Token: 0x04000048 RID: 72
		private static IntParameter m_ExternalImagesTimeoutParam = null;

		// Token: 0x04000049 RID: 73
		private static int m_externalImagesTimeoutDefault = 600;

		// Token: 0x0400004A RID: 74
		private static readonly int m_defaultStoredParametersThreshold = 1500;

		// Token: 0x0400004B RID: 75
		private static IntParameter m_storedParametersThreshold = null;

		// Token: 0x0400004C RID: 76
		private static readonly int m_defaultStoredParametersLifetime = 180;

		// Token: 0x0400004D RID: 77
		private static IntParameter m_storedParametersLifetime = null;

		// Token: 0x0400004E RID: 78
		private static readonly bool m_DefaultSessionCookies = true;

		// Token: 0x0400004F RID: 79
		private static BooleanParameter m_SessionCookiesParam = null;

		// Token: 0x04000050 RID: 80
		private static readonly int m_DefaultResponseBufferSizeKb = 64;

		// Token: 0x04000051 RID: 81
		private static IntParameter m_ResponseBufferSizeKbParam = null;

		// Token: 0x04000052 RID: 82
		private static IntParameter m_MaxFileSizeMb = null;

		// Token: 0x04000053 RID: 83
		private static readonly int m_DefaultSqlStreamingBufferSize = 64640;

		// Token: 0x04000054 RID: 84
		private static IntParameter m_SqlStreamingBufferSizeParam = null;

		// Token: 0x04000055 RID: 85
		private static IntParameter m_ChunkSegmentSizeParam = null;

		// Token: 0x04000056 RID: 86
		private static BooleanParameter m_enableTestConnectionDetailedErrorsParam = null;

		// Token: 0x04000057 RID: 87
		private static Hashtable m_Cache;

		// Token: 0x04000058 RID: 88
		private static readonly object m_lockObj = new object();
	}
}
