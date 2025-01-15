using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.BIServer.HostingEnvironment.HostingInfo;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.OnPrem
{
	// Token: 0x02000007 RID: 7
	internal class OnPremProcessingRenderingConfiguration : IProcessRenderingConfiguration
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002050 File Offset: 0x00000250
		public OnPremProcessingRenderingConfiguration(RSConfiguration configToWrap, RunningApplication runningApplication)
		{
			this.CurrentApplication = runningApplication;
			this.WrappedConfig = configToWrap;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002066 File Offset: 0x00000266
		// (set) Token: 0x06000009 RID: 9 RVA: 0x0000206E File Offset: 0x0000026E
		public RunningApplication CurrentApplication { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002077 File Offset: 0x00000277
		public bool CurrentApplicationHasCatalogAccess
		{
			get
			{
				return this.CurrentApplication == RunningApplication.WindowsService || this.CurrentApplication == RunningApplication.WebService || this.CurrentApplication == RunningApplication.ReportServerWebApp;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002095 File Offset: 0x00000295
		public bool IsExtensibilityEnabled
		{
			get
			{
				return Sku.IsFeatureEnabled(this.InstanceID, RestrictedFeatures.Extensibility);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000020A3 File Offset: 0x000002A3
		public bool IsCustomAuthEnabled
		{
			get
			{
				return Sku.IsFeatureEnabled(this.InstanceID, RestrictedFeatures.CustomAuth);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000020B2 File Offset: 0x000002B2
		public long MaxMemoryThresholdMB
		{
			get
			{
				return SkuUtil.GetMaxMemoryThresholdMB(Sku.GetInstalledSku(ProcessingContext.Configuration.InstanceID));
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000020C8 File Offset: 0x000002C8
		public bool NonSqlDataSourcesEnabled
		{
			get
			{
				return Sku.IsFeatureEnabled(this.InstanceID, RestrictedFeatures.NonSqlDataSources);
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000020D6 File Offset: 0x000002D6
		public void EnsureCorrectEdition(IDbConnection sqlConn, string connectionString, bool checkRestrictedSkus)
		{
			Sku.EnsureCorrectEdition(sqlConn, connectionString, checkRestrictedSkus);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000020E1 File Offset: 0x000002E1
		public void CheckSslAndHttpUrlReservations(RunningApplication application, out bool IsSslUrlReserved, out bool IsHttpUrlReserved)
		{
			this.WrappedConfig.CheckSslAndHttpUrlReservations(application, out IsSslUrlReserved, out IsHttpUrlReserved);
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000020F1 File Offset: 0x000002F1
		public string InstanceID
		{
			get
			{
				return this.WrappedConfig.InstanceID;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000020FE File Offset: 0x000002FE
		public string ConfigFilePath
		{
			get
			{
				return this.WrappedConfig.ConfigFilePath;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000013 RID: 19 RVA: 0x0000210B File Offset: 0x0000030B
		public Dictionary<string, EventExtension> EventTypes
		{
			get
			{
				return this.WrappedConfig.EventTypes;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002118 File Offset: 0x00000318
		public ExtensionsConfiguration Extensions
		{
			get
			{
				return this.WrappedConfig.Extensions;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002125 File Offset: 0x00000325
		public string InstanceName
		{
			get
			{
				return this.WrappedConfig.InstanceName;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002132 File Offset: 0x00000332
		public bool IsReportBuilderAnonymousAccessEnabled
		{
			get
			{
				return this.WrappedConfig.IsReportBuilderAnonymousAccessEnabled;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000213F File Offset: 0x0000033F
		public bool IsSurrogatePresent
		{
			get
			{
				return this.WrappedConfig.IsSurrogatePresent;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000018 RID: 24 RVA: 0x0000214C File Offset: 0x0000034C
		public bool IsWebServiceEnabled
		{
			get
			{
				return this.WrappedConfig.IsWebServiceEnabled;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002159 File Offset: 0x00000359
		public LogonMethod LogonMethod
		{
			get
			{
				return this.WrappedConfig.LogonMethod;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002166 File Offset: 0x00000366
		public int MaxActiveReqForOneUser
		{
			get
			{
				return this.WrappedConfig.MaxActiveReqForOneUser;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002173 File Offset: 0x00000373
		public int MaxQueueThreads
		{
			get
			{
				return this.WrappedConfig.MaxQueueThreads;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002180 File Offset: 0x00000380
		public int MaxScheduleWait
		{
			get
			{
				return this.WrappedConfig.MaxScheduleWait;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600001D RID: 29 RVA: 0x0000218D File Offset: 0x0000038D
		public TimeSpan MaxTimedAppDomainUnload
		{
			get
			{
				return this.WrappedConfig.MaxTimedAppDomainUnload;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600001E RID: 30 RVA: 0x0000219A File Offset: 0x0000039A
		public IOAuthConfiguration OAuthConfiguration
		{
			get
			{
				return this.WrappedConfig.OAuthConfiguration;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000021A7 File Offset: 0x000003A7
		public string ReportServerVirtualDirectory
		{
			get
			{
				return this.WrappedConfig.ReportServerExternalUrlCalculated;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000021B4 File Offset: 0x000003B4
		public int RequestCacheSlots
		{
			get
			{
				return this.WrappedConfig.RequestCacheSlots;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000021C1 File Offset: 0x000003C1
		public int RunningRequestsScavengerCycle
		{
			get
			{
				return this.WrappedConfig.RunningRequestsScavengerCycle;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000021CE File Offset: 0x000003CE
		public string ServerProductNameAndVersion
		{
			get
			{
				return this.WrappedConfig.ServerProductNameAndVersion;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000021DB File Offset: 0x000003DB
		public string ServerProductVersion
		{
			get
			{
				return this.WrappedConfig.ServerProductVersion;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000021E8 File Offset: 0x000003E8
		public string SurrogateDomain
		{
			get
			{
				return this.WrappedConfig.SurrogateDomain;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000021F5 File Offset: 0x000003F5
		public string SurrogatePassword
		{
			get
			{
				return this.WrappedConfig.SurrogatePassword;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002202 File Offset: 0x00000402
		public string SurrogateUserName
		{
			get
			{
				return this.WrappedConfig.SurrogateUserName;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000220F File Offset: 0x0000040F
		public string UrlRootCalculated
		{
			get
			{
				return this.WrappedConfig.UrlRootCalculated;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000028 RID: 40 RVA: 0x0000221C File Offset: 0x0000041C
		public IEnumerable<string> SupportedHyperlinkSchemes
		{
			get
			{
				if (this.m_supportedHyperlinkSchemes == null)
				{
					this.m_supportedHyperlinkSchemes = (from x in StaticConfig.Current.GetOrDefault(ConfigSettings.SupportedHyperlinkSchemes.ToString(), string.Empty).Split(new char[] { ',' })
						select x.Trim()).ToList<string>();
				}
				return this.m_supportedHyperlinkSchemes;
			}
		}

		// Token: 0x04000036 RID: 54
		private RSConfiguration WrappedConfig;

		// Token: 0x04000037 RID: 55
		private List<string> m_supportedHyperlinkSchemes;
	}
}
