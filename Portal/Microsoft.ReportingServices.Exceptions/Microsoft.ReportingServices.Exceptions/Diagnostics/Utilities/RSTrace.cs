using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Threading;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000CD RID: 205
	public sealed class RSTrace
	{
		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x000071D4 File Offset: 0x000053D4
		// (set) Token: 0x06000426 RID: 1062 RVA: 0x00007238 File Offset: 0x00005438
		public TraceSwitch RSTraceSwitch
		{
			get
			{
				if (RSTrace.m_rsTraceSwitch == null)
				{
					lock (this)
					{
						string defaultTraceLevel = RSTrace.m_traceInternal.GetDefaultTraceLevel();
						RSTrace.m_rsTraceSwitch = new TraceSwitch("DefaultTraceSwitch", "Default Trace Switch", defaultTraceLevel);
					}
				}
				return RSTrace.m_rsTraceSwitch;
			}
			set
			{
				lock (this)
				{
					RSTrace.m_rsTraceSwitch = value;
				}
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x00007274 File Offset: 0x00005474
		public static RSTrace CryptoTrace
		{
			get
			{
				if (RSTrace.m_CryptoTrace == null)
				{
					RSTrace rstrace = new RSTrace(RSTrace.TraceComponents.Crypto.ToString());
					Interlocked.CompareExchange<RSTrace>(ref RSTrace.m_CryptoTrace, rstrace, null);
				}
				return RSTrace.m_CryptoTrace;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x000072B4 File Offset: 0x000054B4
		public static RSTrace ResourceUtilTrace
		{
			get
			{
				if (RSTrace.m_ResourceUtilTrace == null)
				{
					RSTrace rstrace = new RSTrace(RSTrace.TraceComponents.ResourceUtilities.ToString());
					Interlocked.CompareExchange<RSTrace>(ref RSTrace.m_ResourceUtilTrace, rstrace, null);
				}
				return RSTrace.m_ResourceUtilTrace;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x000072F4 File Offset: 0x000054F4
		public static RSTrace CatalogTrace
		{
			get
			{
				if (RSTrace.m_CatalogTrace == null)
				{
					RSTrace rstrace = new RSTrace(RSTrace.TraceComponents.Library.ToString());
					Interlocked.CompareExchange<RSTrace>(ref RSTrace.m_CatalogTrace, rstrace, null);
				}
				return RSTrace.m_CatalogTrace;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x00007334 File Offset: 0x00005534
		public static RSTrace ConfigManagerTracer
		{
			get
			{
				if (RSTrace.m_ConfigManagerTrace == null)
				{
					RSTrace rstrace = new RSTrace(RSTrace.TraceComponents.ConfigManager.ToString());
					Interlocked.CompareExchange<RSTrace>(ref RSTrace.m_ConfigManagerTrace, rstrace, null);
				}
				return RSTrace.m_ConfigManagerTrace;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x00007374 File Offset: 0x00005574
		public static RSTrace WebServerTracer
		{
			get
			{
				if (RSTrace.m_WebServerTrace == null)
				{
					RSTrace rstrace = new RSTrace(RSTrace.TraceComponents.WebServer.ToString());
					Interlocked.CompareExchange<RSTrace>(ref RSTrace.m_WebServerTrace, rstrace, null);
				}
				return RSTrace.m_WebServerTrace;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x000073B4 File Offset: 0x000055B4
		public static RSTrace WcfRuntimeTracer
		{
			get
			{
				if (RSTrace.m_WcfRuntimeTrace == null)
				{
					RSTrace rstrace = new RSTrace(RSTrace.TraceComponents.WcfRuntime.ToString());
					Interlocked.CompareExchange<RSTrace>(ref RSTrace.m_WcfRuntimeTrace, rstrace, null);
				}
				return RSTrace.m_WcfRuntimeTrace;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x000073F4 File Offset: 0x000055F4
		public static RSTrace AlertingRuntimeTracer
		{
			get
			{
				if (RSTrace.m_AlertingRuntimeTrace == null)
				{
					RSTrace rstrace = new RSTrace(RSTrace.TraceComponents.AlertingRuntime.ToString());
					Interlocked.CompareExchange<RSTrace>(ref RSTrace.m_AlertingRuntimeTrace, rstrace, null);
				}
				return RSTrace.m_AlertingRuntimeTrace;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x00007434 File Offset: 0x00005634
		public static RSTrace NtServiceTracer
		{
			get
			{
				if (RSTrace.m_NtServiceTrace == null)
				{
					RSTrace rstrace = new RSTrace(RSTrace.TraceComponents.NtService.ToString());
					Interlocked.CompareExchange<RSTrace>(ref RSTrace.m_NtServiceTrace, rstrace, null);
				}
				return RSTrace.m_NtServiceTrace;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x00007474 File Offset: 0x00005674
		public static RSTrace SessionTrace
		{
			get
			{
				if (RSTrace.m_SessionTrace == null)
				{
					RSTrace rstrace = new RSTrace(RSTrace.TraceComponents.Session.ToString());
					Interlocked.CompareExchange<RSTrace>(ref RSTrace.m_SessionTrace, rstrace, null);
				}
				return RSTrace.m_SessionTrace;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x000074B4 File Offset: 0x000056B4
		public static RSTrace BufferedResponseTracer
		{
			get
			{
				if (RSTrace.m_BufRespTrace == null)
				{
					RSTrace rstrace = new RSTrace(RSTrace.TraceComponents.BufferedResponse.ToString());
					Interlocked.CompareExchange<RSTrace>(ref RSTrace.m_BufRespTrace, rstrace, null);
				}
				return RSTrace.m_BufRespTrace;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x000074F4 File Offset: 0x000056F4
		public static RSTrace RunningRequestsTracer
		{
			get
			{
				if (RSTrace.m_RunningRequestsTrace == null)
				{
					RSTrace rstrace = new RSTrace(RSTrace.TraceComponents.RunningRequests.ToString());
					Interlocked.CompareExchange<RSTrace>(ref RSTrace.m_RunningRequestsTrace, rstrace, null);
				}
				return RSTrace.m_RunningRequestsTrace;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x00007534 File Offset: 0x00005734
		public static RSTrace DbPollingTracer
		{
			get
			{
				if (RSTrace.m_DbPollingTrace == null)
				{
					RSTrace rstrace = new RSTrace(RSTrace.TraceComponents.DbPolling.ToString());
					Interlocked.CompareExchange<RSTrace>(ref RSTrace.m_DbPollingTrace, rstrace, null);
				}
				return RSTrace.m_DbPollingTrace;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x00007574 File Offset: 0x00005774
		public static RSTrace NotificationTracer
		{
			get
			{
				if (RSTrace.m_NotificationTrace == null)
				{
					RSTrace rstrace = new RSTrace(RSTrace.TraceComponents.Notification.ToString());
					Interlocked.CompareExchange<RSTrace>(ref RSTrace.m_NotificationTrace, rstrace, null);
				}
				return RSTrace.m_NotificationTrace;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x000075B4 File Offset: 0x000057B4
		public static RSTrace ProviderTracer
		{
			get
			{
				if (RSTrace.m_ProviderTrace == null)
				{
					RSTrace rstrace = new RSTrace(RSTrace.TraceComponents.Provider.ToString());
					Interlocked.CompareExchange<RSTrace>(ref RSTrace.m_ProviderTrace, rstrace, null);
				}
				return RSTrace.m_ProviderTrace;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x000075F4 File Offset: 0x000057F4
		public static RSTrace ScheduleTracer
		{
			get
			{
				if (RSTrace.m_ScheduleTrace == null)
				{
					RSTrace rstrace = new RSTrace(RSTrace.TraceComponents.Schedule.ToString());
					Interlocked.CompareExchange<RSTrace>(ref RSTrace.m_ScheduleTrace, rstrace, null);
				}
				return RSTrace.m_ScheduleTrace;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x00007634 File Offset: 0x00005834
		public static RSTrace SubscriptionTracer
		{
			get
			{
				if (RSTrace.m_SubscriptionTrace == null)
				{
					RSTrace rstrace = new RSTrace(RSTrace.TraceComponents.Subscription.ToString());
					Interlocked.CompareExchange<RSTrace>(ref RSTrace.m_SubscriptionTrace, rstrace, null);
				}
				return RSTrace.m_SubscriptionTrace;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x00007674 File Offset: 0x00005874
		public static RSTrace SecurityTracer
		{
			get
			{
				if (RSTrace.m_SecurityTrace == null)
				{
					RSTrace rstrace = new RSTrace(RSTrace.TraceComponents.Security.ToString());
					Interlocked.CompareExchange<RSTrace>(ref RSTrace.m_SecurityTrace, rstrace, null);
				}
				return RSTrace.m_SecurityTrace;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x000076B4 File Offset: 0x000058B4
		public static RSTrace ServiceControllerTracer
		{
			get
			{
				if (RSTrace.m_ServiceControllerTrace == null)
				{
					RSTrace rstrace = new RSTrace(RSTrace.TraceComponents.ServiceController.ToString());
					Interlocked.CompareExchange<RSTrace>(ref RSTrace.m_ServiceControllerTrace, rstrace, null);
				}
				return RSTrace.m_ServiceControllerTrace;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x000076F4 File Offset: 0x000058F4
		public static RSTrace CleanupTracer
		{
			get
			{
				if (RSTrace.m_CleanupTrace == null)
				{
					RSTrace rstrace = new RSTrace(RSTrace.TraceComponents.DbCleanup.ToString());
					Interlocked.CompareExchange<RSTrace>(ref RSTrace.m_CleanupTrace, rstrace, null);
				}
				return RSTrace.m_CleanupTrace;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x00007734 File Offset: 0x00005934
		public static RSTrace CacheTracer
		{
			get
			{
				if (RSTrace.m_CacheTrace == null)
				{
					RSTrace rstrace = new RSTrace(RSTrace.TraceComponents.Cache.ToString());
					Interlocked.CompareExchange<RSTrace>(ref RSTrace.m_CacheTrace, rstrace, null);
				}
				return RSTrace.m_CacheTrace;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x00007774 File Offset: 0x00005974
		public static RSTrace ChunkTracer
		{
			get
			{
				if (RSTrace.m_ChunkTrace == null)
				{
					RSTrace rstrace = new RSTrace(RSTrace.TraceComponents.Chunks.ToString());
					Interlocked.CompareExchange<RSTrace>(ref RSTrace.m_ChunkTrace, rstrace, null);
				}
				return RSTrace.m_ChunkTrace;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x000077B4 File Offset: 0x000059B4
		public static RSTrace ExtensionFactoryTracer
		{
			get
			{
				if (RSTrace.m_ExtTrace == null)
				{
					RSTrace rstrace = new RSTrace(RSTrace.TraceComponents.ExtensionFactory.ToString());
					Interlocked.CompareExchange<RSTrace>(ref RSTrace.m_ExtTrace, rstrace, null);
				}
				return RSTrace.m_ExtTrace;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x000077F4 File Offset: 0x000059F4
		public static RSTrace RunningJobsTrace
		{
			get
			{
				if (RSTrace.m_RunningJobsTrace == null)
				{
					RSTrace rstrace = new RSTrace(RSTrace.TraceComponents.RunningJobs.ToString());
					Interlocked.CompareExchange<RSTrace>(ref RSTrace.m_RunningJobsTrace, rstrace, null);
				}
				return RSTrace.m_RunningJobsTrace;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x00007834 File Offset: 0x00005A34
		public static RSTrace ProcessingTracer
		{
			get
			{
				if (RSTrace.m_ProcessingTrace == null)
				{
					RSTrace rstrace = new RSTrace(RSTrace.TraceComponents.Processing.ToString());
					Interlocked.CompareExchange<RSTrace>(ref RSTrace.m_ProcessingTrace, rstrace, null);
				}
				return RSTrace.m_ProcessingTrace;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x00007874 File Offset: 0x00005A74
		public static RSTrace RenderingTracer
		{
			get
			{
				if (RSTrace.m_RenderingTrace == null)
				{
					RSTrace rstrace = new RSTrace(RSTrace.TraceComponents.ReportRendering.ToString());
					Interlocked.CompareExchange<RSTrace>(ref RSTrace.m_RenderingTrace, rstrace, null);
				}
				return RSTrace.m_RenderingTrace;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x000078B4 File Offset: 0x00005AB4
		public static RSTrace ViewerTracer
		{
			get
			{
				if (RSTrace.m_ViewerTrace == null)
				{
					RSTrace rstrace = new RSTrace(RSTrace.TraceComponents.ConfigManager.ToString());
					Interlocked.CompareExchange<RSTrace>(ref RSTrace.m_ViewerTrace, rstrace, null);
				}
				return RSTrace.m_ViewerTrace;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x000078F4 File Offset: 0x00005AF4
		public static RSTrace DataExtensionTracer
		{
			get
			{
				if (RSTrace.m_DataExtTrace == null)
				{
					RSTrace rstrace = new RSTrace(RSTrace.TraceComponents.DataExtension.ToString());
					Interlocked.CompareExchange<RSTrace>(ref RSTrace.m_DataExtTrace, rstrace, null);
				}
				return RSTrace.m_DataExtTrace;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x00007934 File Offset: 0x00005B34
		public static RSTrace RSWebAppTracer
		{
			get
			{
				if (RSTrace.m_RSWebAppTracer == null)
				{
					RSTrace rstrace = new RSTrace(RSTrace.TraceComponents.ReportServerWebApp.ToString());
					Interlocked.CompareExchange<RSTrace>(ref RSTrace.m_RSWebAppTracer, rstrace, null);
				}
				return RSTrace.m_RSWebAppTracer;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x00007974 File Offset: 0x00005B74
		public static RSTrace EmailExtensionTracer
		{
			get
			{
				if (RSTrace.m_EmailExtensionTrace == null)
				{
					RSTrace rstrace = new RSTrace(RSTrace.TraceComponents.EmailExtension.ToString());
					Interlocked.CompareExchange<RSTrace>(ref RSTrace.m_EmailExtensionTrace, rstrace, null);
				}
				return RSTrace.m_EmailExtensionTrace;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x000079B4 File Offset: 0x00005BB4
		public static RSTrace ImageRendererTracer
		{
			get
			{
				if (RSTrace.m_ImageRendererTrace == null)
				{
					RSTrace rstrace = new RSTrace(RSTrace.TraceComponents.ImageRenderer.ToString());
					Interlocked.CompareExchange<RSTrace>(ref RSTrace.m_ImageRendererTrace, rstrace, null);
				}
				return RSTrace.m_ImageRendererTrace;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x000079F4 File Offset: 0x00005BF4
		public static RSTrace ExcelRendererTracer
		{
			get
			{
				if (RSTrace.m_ExcelRendererTrace == null)
				{
					RSTrace rstrace = new RSTrace(RSTrace.TraceComponents.ExcelRenderer.ToString());
					Interlocked.CompareExchange<RSTrace>(ref RSTrace.m_ExcelRendererTrace, rstrace, null);
				}
				return RSTrace.m_ExcelRendererTrace;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x00007A34 File Offset: 0x00005C34
		public static RSTrace PreviewServerTracer
		{
			get
			{
				if (RSTrace.m_PreviewServerTrace == null)
				{
					RSTrace rstrace = new RSTrace(RSTrace.TraceComponents.PreviewServer.ToString());
					Interlocked.CompareExchange<RSTrace>(ref RSTrace.m_PreviewServerTrace, rstrace, null);
				}
				return RSTrace.m_PreviewServerTrace;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x00007A74 File Offset: 0x00005C74
		public static RSTrace ReportPreviewTracer
		{
			get
			{
				if (RSTrace.m_ReportPreviewTrace == null)
				{
					RSTrace rstrace = new RSTrace(RSTrace.TraceComponents.ReportPreview.ToString());
					Interlocked.CompareExchange<RSTrace>(ref RSTrace.m_ReportPreviewTrace, rstrace, null);
				}
				return RSTrace.m_ReportPreviewTrace;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x00007AB4 File Offset: 0x00005CB4
		public static RSTrace UITracer
		{
			get
			{
				if (RSTrace.m_UITrace == null)
				{
					RSTrace rstrace = new RSTrace(RSTrace.TraceComponents.UI.ToString());
					Interlocked.CompareExchange<RSTrace>(ref RSTrace.m_UITrace, rstrace, null);
				}
				return RSTrace.m_UITrace;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x00007AF4 File Offset: 0x00005CF4
		public static RSTrace SMGTracer
		{
			get
			{
				if (RSTrace.m_SMGTrace == null)
				{
					RSTrace rstrace = new RSTrace(RSTrace.TraceComponents.SemanticModelGenerator.ToString());
					Interlocked.CompareExchange<RSTrace>(ref RSTrace.m_SMGTrace, rstrace, null);
				}
				return RSTrace.m_SMGTrace;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x00007B34 File Offset: 0x00005D34
		public static RSTrace SQETracer
		{
			get
			{
				if (RSTrace.m_SQETrace == null)
				{
					RSTrace rstrace = new RSTrace(RSTrace.TraceComponents.SemanticQueryEngine.ToString());
					Interlocked.CompareExchange<RSTrace>(ref RSTrace.m_SQETrace, rstrace, null);
				}
				return RSTrace.m_SQETrace;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x00007B74 File Offset: 0x00005D74
		public static RSTrace AppDomainManagerTracer
		{
			get
			{
				if (RSTrace.m_AppDomainManagerTrace == null)
				{
					RSTrace rstrace = new RSTrace(RSTrace.TraceComponents.AppDomainManager.ToString());
					Interlocked.CompareExchange<RSTrace>(ref RSTrace.m_AppDomainManagerTrace, rstrace, null);
				}
				return RSTrace.m_AppDomainManagerTrace;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x00007BB2 File Offset: 0x00005DB2
		public static RSTrace SanitizedRdlEngineHostTracer
		{
			get
			{
				return RSTrace.m_RdlEngineHostTrace.Value;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x00007BC0 File Offset: 0x00005DC0
		public static RSTrace HttpRuntimeTracer
		{
			get
			{
				if (RSTrace.m_HttpRuntimeTrace == null)
				{
					RSTrace rstrace = new RSTrace(RSTrace.TraceComponents.HttpRuntime.ToString());
					Interlocked.CompareExchange<RSTrace>(ref RSTrace.m_HttpRuntimeTrace, rstrace, null);
				}
				return RSTrace.m_HttpRuntimeTrace;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x00007C00 File Offset: 0x00005E00
		public static RSTrace UndoManager
		{
			get
			{
				if (RSTrace.m_UndoManagerTrace == null)
				{
					RSTrace rstrace = new RSTrace(RSTrace.TraceComponents.UndoManager.ToString());
					Interlocked.CompareExchange<RSTrace>(ref RSTrace.m_UndoManagerTrace, rstrace, null);
				}
				return RSTrace.m_UndoManagerTrace;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x00007C40 File Offset: 0x00005E40
		public static RSTrace DataManager
		{
			get
			{
				if (RSTrace.m_DataManagerTrace == null)
				{
					RSTrace rstrace = new RSTrace(RSTrace.TraceComponents.DataManager.ToString());
					Interlocked.CompareExchange<RSTrace>(ref RSTrace.m_DataManagerTrace, rstrace, null);
				}
				return RSTrace.m_DataManagerTrace;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x00007C80 File Offset: 0x00005E80
		public static RSTrace DataStructureManager
		{
			get
			{
				if (RSTrace.m_DataStructureManagerTrace == null)
				{
					RSTrace rstrace = new RSTrace(RSTrace.TraceComponents.DataStructureManager.ToString());
					Interlocked.CompareExchange<RSTrace>(ref RSTrace.m_DataStructureManagerTrace, rstrace, null);
				}
				return RSTrace.m_DataStructureManagerTrace;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x00007CC0 File Offset: 0x00005EC0
		public static RSTrace QueryDesign
		{
			get
			{
				if (RSTrace.m_QueryDesignTrace == null)
				{
					RSTrace rstrace = new RSTrace(RSTrace.TraceComponents.QueryDesign.ToString());
					Interlocked.CompareExchange<RSTrace>(ref RSTrace.m_QueryDesignTrace, rstrace, null);
				}
				return RSTrace.m_QueryDesignTrace;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x00007D00 File Offset: 0x00005F00
		public static RSTrace Controls
		{
			get
			{
				if (RSTrace.m_ControlsTrace == null)
				{
					RSTrace rstrace = new RSTrace(RSTrace.TraceComponents.Controls.ToString());
					Interlocked.CompareExchange<RSTrace>(ref RSTrace.m_ControlsTrace, rstrace, null);
				}
				return RSTrace.m_ControlsTrace;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x00007D40 File Offset: 0x00005F40
		public static RSTrace ClientEventTracer
		{
			get
			{
				if (RSTrace.m_ClientEventTracer == null)
				{
					RSTrace rstrace = new RSTrace(RSTrace.TraceComponents.PowerView.ToString());
					Interlocked.CompareExchange<RSTrace>(ref RSTrace.m_ClientEventTracer, rstrace, null);
				}
				return RSTrace.m_ClientEventTracer;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x00007D80 File Offset: 0x00005F80
		public static RSTrace ThreadTracer
		{
			get
			{
				if (RSTrace.m_ThreadTracer == null)
				{
					RSTrace rstrace = new RSTrace(RSTrace.TraceComponents.Thread.ToString());
					Interlocked.CompareExchange<RSTrace>(ref RSTrace.m_ThreadTracer, rstrace, null);
				}
				return RSTrace.m_ThreadTracer;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x00007DC0 File Offset: 0x00005FC0
		public static RSTrace MonitoredScope
		{
			get
			{
				if (RSTrace.m_MonitoredScope == null)
				{
					RSTrace rstrace = new RSTrace(RSTrace.TraceComponents.MonitoredScope.ToString());
					Interlocked.CompareExchange<RSTrace>(ref RSTrace.m_MonitoredScope, rstrace, null);
				}
				return RSTrace.m_MonitoredScope;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x00007E00 File Offset: 0x00006000
		public static RSTrace DsqtTracer
		{
			get
			{
				if (RSTrace.m_dsqtTracer == null)
				{
					RSTrace rstrace = new RSTrace(RSTrace.TraceComponents.DataShapeQueryTranslation.ToString());
					Interlocked.CompareExchange<RSTrace>(ref RSTrace.m_dsqtTracer, rstrace, null);
				}
				return RSTrace.m_dsqtTracer;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x00007E40 File Offset: 0x00006040
		public static RSTrace InfoNavTracer
		{
			get
			{
				if (RSTrace.m_infoNavTracer == null)
				{
					RSTrace rstrace = new RSTrace(RSTrace.TraceComponents.InfoNav.ToString());
					Interlocked.CompareExchange<RSTrace>(ref RSTrace.m_infoNavTracer, rstrace, null);
				}
				return RSTrace.m_infoNavTracer;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x00007E7E File Offset: 0x0000607E
		public bool TraceInfo
		{
			get
			{
				return this.IsTraceLevelEnabled(TraceLevel.Info);
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x00007E87 File Offset: 0x00006087
		public bool TraceError
		{
			get
			{
				return this.IsTraceLevelEnabled(TraceLevel.Error);
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x00007E90 File Offset: 0x00006090
		public bool TraceWarning
		{
			get
			{
				return this.IsTraceLevelEnabled(TraceLevel.Warning);
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x00007E99 File Offset: 0x00006099
		public bool TraceVerbose
		{
			get
			{
				return this.IsTraceLevelEnabled(TraceLevel.Verbose);
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x00007EA2 File Offset: 0x000060A2
		internal static bool IsTraceInitialized
		{
			get
			{
				return RSTrace.m_traceInternal.IsTraceInitialized;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x00007EAE File Offset: 0x000060AE
		internal string TraceFileName
		{
			get
			{
				return RSTrace.m_traceInternal.CurrentTraceFilePath;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x00007EBA File Offset: 0x000060BA
		internal IPrivateInformationFormatter PrivateInformationFormatter
		{
			get
			{
				return RSTrace.m_privateInformationFormatter;
			}
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00007EC1 File Offset: 0x000060C1
		public RSTrace(string componentName)
		{
			this.m_ComponentName = componentName.ToLowerInvariant();
			this.m_componentTraceLevel = RSTrace.GetTraceLevel(this.m_ComponentName);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00007EE6 File Offset: 0x000060E6
		public void Trace(string message)
		{
			RSTrace.m_traceInternal.Trace(this.m_ComponentName, message);
			if (RSTrace.m_alternateTraceInternal != null)
			{
				RSTrace.m_alternateTraceInternal.Trace(this.m_ComponentName, message);
			}
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00007F11 File Offset: 0x00006111
		public void Trace(TraceLevel traceLevel, string message)
		{
			RSTrace.m_traceInternal.Trace(traceLevel, this.m_ComponentName, message);
			if (RSTrace.m_alternateTraceInternal != null)
			{
				RSTrace.m_alternateTraceInternal.Trace(traceLevel, this.m_ComponentName, message);
			}
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00007F3E File Offset: 0x0000613E
		public void TraceWithDetails(TraceLevel traceLevel, string message, string details)
		{
			RSTrace.m_traceInternal.TraceWithDetails(traceLevel, this.m_ComponentName, message, details);
			if (RSTrace.m_alternateTraceInternal != null)
			{
				RSTrace.m_alternateTraceInternal.TraceWithDetails(traceLevel, this.m_ComponentName, message, details);
			}
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00007F6D File Offset: 0x0000616D
		public void TraceException(TraceLevel traceLevel, string message)
		{
			RSTrace.m_traceInternal.TraceException(traceLevel, this.m_ComponentName, message);
			if (RSTrace.m_alternateTraceInternal != null)
			{
				RSTrace.m_alternateTraceInternal.TraceException(traceLevel, this.m_ComponentName, message);
			}
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00007F9A File Offset: 0x0000619A
		public void Trace(string format, params object[] arg)
		{
			RSTrace.m_traceInternal.Trace(this.m_ComponentName, format, arg);
			if (RSTrace.m_alternateTraceInternal != null)
			{
				RSTrace.m_alternateTraceInternal.Trace(this.m_ComponentName, format, arg);
			}
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00007FC7 File Offset: 0x000061C7
		public void Trace(TraceLevel traceLevel, string format, params object[] arg)
		{
			RSTrace.m_traceInternal.Trace(traceLevel, this.m_ComponentName, format, arg);
			if (RSTrace.m_alternateTraceInternal != null)
			{
				RSTrace.m_alternateTraceInternal.Trace(traceLevel, this.m_ComponentName, format, arg);
			}
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00007FF8 File Offset: 0x000061F8
		public void TraceException(TraceLevel traceLevel, string format, params object[] arg)
		{
			if (this.IsTraceLevelEnabled(traceLevel))
			{
				string text = string.Format(CultureInfo.InvariantCulture, format, arg);
				RSTrace.m_traceInternal.TraceException(traceLevel, this.m_ComponentName, text);
				if (RSTrace.m_alternateTraceInternal != null)
				{
					RSTrace.m_alternateTraceInternal.TraceException(traceLevel, this.m_ComponentName, text);
				}
			}
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00008046 File Offset: 0x00006246
		public void TraceWithNoEventLog(TraceLevel traceLevel, string format, params object[] arg)
		{
			RSTrace.m_traceInternal.TraceWithNoEventLog(traceLevel, this.m_ComponentName, format, arg);
			if (RSTrace.m_alternateTraceInternal != null)
			{
				RSTrace.m_alternateTraceInternal.TraceWithNoEventLog(traceLevel, this.m_ComponentName, format, arg);
			}
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00008078 File Offset: 0x00006278
		public void Assert(bool condition)
		{
			if (!condition)
			{
				if (RSTrace.m_alternateTraceInternal != null)
				{
					try
					{
						RSTrace.m_alternateTraceInternal.Fail(this.m_ComponentName);
					}
					catch
					{
						RSTrace.m_traceInternal.Fail(this.m_ComponentName);
					}
				}
				RSTrace.m_traceInternal.Fail(this.m_ComponentName);
			}
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x000080D4 File Offset: 0x000062D4
		public void Assert(bool condition, string message)
		{
			if (!condition)
			{
				if (RSTrace.m_alternateTraceInternal != null)
				{
					try
					{
						RSTrace.m_alternateTraceInternal.Fail(this.m_ComponentName, message);
					}
					catch
					{
						RSTrace.m_traceInternal.Fail(this.m_ComponentName, message);
					}
				}
				RSTrace.m_traceInternal.Fail(this.m_ComponentName, message);
			}
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00008134 File Offset: 0x00006334
		public void Assert(bool condition, string message, params object[] args)
		{
			if (!condition)
			{
				this.Assert(condition, string.Format(CultureInfo.InvariantCulture, message, args));
			}
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0000814C File Offset: 0x0000634C
		[Conditional("DEBUG")]
		public void DebugAssert(bool condition)
		{
			this.Assert(condition);
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00008155 File Offset: 0x00006355
		[Conditional("DEBUG")]
		public void DebugAssert(string message)
		{
			this.Assert(false, message);
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0000815F File Offset: 0x0000635F
		[Conditional("DEBUG")]
		public void DebugAssert(bool condition, string message)
		{
			this.Assert(condition, message);
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00008169 File Offset: 0x00006369
		[Conditional("DEBUG")]
		public void DebugAssert(bool condition, string message, object arg1)
		{
			this.Assert(condition, message, new object[] { arg1 });
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00008180 File Offset: 0x00006380
		private static TraceLevel GetTraceLevel(string componentName)
		{
			TraceLevel traceLevel = TraceLevel.Error;
			int num;
			if (RSTrace.m_traceInternal != null && !RSTrace.m_traceInternal.GetTraceLevel(componentName, out traceLevel) && !RSTrace.m_traceInternal.GetTraceLevel("all", out traceLevel) && int.TryParse(RSTrace.m_traceInternal.GetDefaultTraceLevel(), NumberStyles.None, CultureInfo.InvariantCulture, out num) && num >= 0 && num <= 3)
			{
				traceLevel = (TraceLevel)num;
			}
			return traceLevel;
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x000081DC File Offset: 0x000063DC
		public bool IsTraceLevelEnabled(TraceLevel level)
		{
			TraceLevel traceLevel;
			if (RSTrace.m_traceInternal is IRSTraceInternalWithDynamicLevel && RSTrace.m_traceInternal.GetTraceLevel(this.m_ComponentName, out traceLevel))
			{
				this.m_componentTraceLevel = traceLevel;
			}
			TraceLevel traceLevel2;
			return level <= this.m_componentTraceLevel || (RSTrace.m_alternateTraceInternal != null && RSTrace.m_alternateTraceInternal.GetTraceLevel(this.m_ComponentName, out traceLevel2) && level <= traceLevel2);
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x0000823F File Offset: 0x0000643F
		public string TraceDirectory
		{
			get
			{
				return RSTrace.m_traceInternal.TraceDirectory;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x0000824B File Offset: 0x0000644B
		// (set) Token: 0x06000473 RID: 1139 RVA: 0x00008257 File Offset: 0x00006457
		public bool BufferOutput
		{
			get
			{
				return RSTrace.m_traceInternal.BufferOutput;
			}
			set
			{
				RSTrace.m_traceInternal.BufferOutput = value;
			}
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00008264 File Offset: 0x00006464
		public void ClearBuffer()
		{
			RSTrace.m_traceInternal.ClearBuffer();
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00008270 File Offset: 0x00006470
		public void WriteBuffer()
		{
			RSTrace.m_traceInternal.WriteBuffer();
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0000827C File Offset: 0x0000647C
		public static void SetTrace(IRSTraceInternal trace)
		{
			if (trace == null)
			{
				throw new ArgumentNullException("trace");
			}
			RSTrace.m_traceInternal = trace;
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00008292 File Offset: 0x00006492
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal static IRSTraceInternal GetTrace()
		{
			return RSTrace.m_traceInternal;
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00008299 File Offset: 0x00006499
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static IRSTraceInternal GetAlternateTrace()
		{
			return RSTrace.m_alternateTraceInternal;
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x000082A0 File Offset: 0x000064A0
		public static void SetAlternateTrace(IRSTraceInternalWithDynamicLevel alternateTrace, IPrivateInformationFormatter privateInformationFormatter)
		{
			RSTrace.m_alternateTraceInternal = alternateTrace;
			RSTrace.m_privateInformationFormatter = privateInformationFormatter;
		}

		// Token: 0x04000029 RID: 41
		private static TraceSwitch m_rsTraceSwitch;

		// Token: 0x0400002A RID: 42
		private static RSTrace m_CryptoTrace;

		// Token: 0x0400002B RID: 43
		private static RSTrace m_ResourceUtilTrace;

		// Token: 0x0400002C RID: 44
		private static RSTrace m_CatalogTrace;

		// Token: 0x0400002D RID: 45
		private static RSTrace m_ConfigManagerTrace;

		// Token: 0x0400002E RID: 46
		private static RSTrace m_WebServerTrace;

		// Token: 0x0400002F RID: 47
		private static RSTrace m_WcfRuntimeTrace;

		// Token: 0x04000030 RID: 48
		private static RSTrace m_AlertingRuntimeTrace;

		// Token: 0x04000031 RID: 49
		private static RSTrace m_NtServiceTrace;

		// Token: 0x04000032 RID: 50
		private static RSTrace m_SessionTrace;

		// Token: 0x04000033 RID: 51
		private static RSTrace m_BufRespTrace;

		// Token: 0x04000034 RID: 52
		private static RSTrace m_RunningRequestsTrace;

		// Token: 0x04000035 RID: 53
		private static RSTrace m_DbPollingTrace;

		// Token: 0x04000036 RID: 54
		private static RSTrace m_NotificationTrace;

		// Token: 0x04000037 RID: 55
		private static RSTrace m_ProviderTrace;

		// Token: 0x04000038 RID: 56
		private static RSTrace m_ScheduleTrace;

		// Token: 0x04000039 RID: 57
		private static RSTrace m_SubscriptionTrace;

		// Token: 0x0400003A RID: 58
		private static RSTrace m_SecurityTrace;

		// Token: 0x0400003B RID: 59
		private static RSTrace m_ServiceControllerTrace;

		// Token: 0x0400003C RID: 60
		private static RSTrace m_CleanupTrace;

		// Token: 0x0400003D RID: 61
		private static RSTrace m_CacheTrace;

		// Token: 0x0400003E RID: 62
		private static RSTrace m_ChunkTrace;

		// Token: 0x0400003F RID: 63
		private static RSTrace m_ExtTrace;

		// Token: 0x04000040 RID: 64
		private static RSTrace m_RunningJobsTrace;

		// Token: 0x04000041 RID: 65
		private static RSTrace m_ProcessingTrace;

		// Token: 0x04000042 RID: 66
		private static RSTrace m_RenderingTrace;

		// Token: 0x04000043 RID: 67
		private static RSTrace m_ViewerTrace;

		// Token: 0x04000044 RID: 68
		private static RSTrace m_DataExtTrace;

		// Token: 0x04000045 RID: 69
		private static RSTrace m_RSWebAppTracer;

		// Token: 0x04000046 RID: 70
		private static RSTrace m_EmailExtensionTrace;

		// Token: 0x04000047 RID: 71
		private static RSTrace m_ImageRendererTrace;

		// Token: 0x04000048 RID: 72
		private static RSTrace m_ExcelRendererTrace;

		// Token: 0x04000049 RID: 73
		private static RSTrace m_PreviewServerTrace;

		// Token: 0x0400004A RID: 74
		private static RSTrace m_ReportPreviewTrace;

		// Token: 0x0400004B RID: 75
		private static RSTrace m_UITrace;

		// Token: 0x0400004C RID: 76
		private static RSTrace m_SMGTrace;

		// Token: 0x0400004D RID: 77
		private static RSTrace m_SQETrace;

		// Token: 0x0400004E RID: 78
		private static RSTrace m_AppDomainManagerTrace;

		// Token: 0x0400004F RID: 79
		private static Lazy<RSTrace> m_RdlEngineHostTrace = new Lazy<RSTrace>(() => new RSTrace(RSTrace.TraceComponents.RdlEngineHost.ToString()));

		// Token: 0x04000050 RID: 80
		private static RSTrace m_HttpRuntimeTrace;

		// Token: 0x04000051 RID: 81
		private static RSTrace m_UndoManagerTrace;

		// Token: 0x04000052 RID: 82
		private static RSTrace m_DataManagerTrace;

		// Token: 0x04000053 RID: 83
		private static RSTrace m_DataStructureManagerTrace;

		// Token: 0x04000054 RID: 84
		private static RSTrace m_QueryDesignTrace;

		// Token: 0x04000055 RID: 85
		private static RSTrace m_ControlsTrace;

		// Token: 0x04000056 RID: 86
		private static RSTrace m_ClientEventTracer;

		// Token: 0x04000057 RID: 87
		private static RSTrace m_ThreadTracer;

		// Token: 0x04000058 RID: 88
		private static RSTrace m_MonitoredScope;

		// Token: 0x04000059 RID: 89
		private static RSTrace m_dsqtTracer;

		// Token: 0x0400005A RID: 90
		private static RSTrace m_infoNavTracer;

		// Token: 0x0400005B RID: 91
		private readonly string m_ComponentName;

		// Token: 0x0400005C RID: 92
		private TraceLevel m_componentTraceLevel;

		// Token: 0x0400005D RID: 93
		private const string m_allComponents = "all";

		// Token: 0x0400005E RID: 94
		private static IRSTraceInternal m_traceInternal = new RSTrace.DefaultRSTraceInternal();

		// Token: 0x0400005F RID: 95
		private static IRSTraceInternalWithDynamicLevel m_alternateTraceInternal;

		// Token: 0x04000060 RID: 96
		private static IPrivateInformationFormatter m_privateInformationFormatter = new PassThroughPrivateInformationFormatter();

		// Token: 0x020000D9 RID: 217
		public sealed class WriteOnce
		{
			// Token: 0x060004C4 RID: 1220 RVA: 0x00008764 File Offset: 0x00006964
			public bool TraceWritten(string text)
			{
				if (text != null && !this.m_traceWriteOnce.Contains(text))
				{
					this.m_traceWriteOnce.Add(text, null);
					object syncRoot = RSTrace.WriteOnce.s_traceWriteOnce.SyncRoot;
					lock (syncRoot)
					{
						if (!RSTrace.WriteOnce.s_traceWriteOnce.Contains(text))
						{
							RSTrace.WriteOnce.s_traceWriteOnce.Add(text, null);
							return false;
						}
					}
					return true;
				}
				return true;
			}

			// Token: 0x040001AC RID: 428
			private Hashtable m_traceWriteOnce = new Hashtable();

			// Token: 0x040001AD RID: 429
			private static Hashtable s_traceWriteOnce = new Hashtable();
		}

		// Token: 0x020000DA RID: 218
		public enum TraceComponents
		{
			// Token: 0x040001AF RID: 431
			Library,
			// Token: 0x040001B0 RID: 432
			ConfigManager,
			// Token: 0x040001B1 RID: 433
			WebServer,
			// Token: 0x040001B2 RID: 434
			NtService,
			// Token: 0x040001B3 RID: 435
			Session,
			// Token: 0x040001B4 RID: 436
			BufferedResponse,
			// Token: 0x040001B5 RID: 437
			RunningRequests,
			// Token: 0x040001B6 RID: 438
			DbPolling,
			// Token: 0x040001B7 RID: 439
			Notification,
			// Token: 0x040001B8 RID: 440
			Provider,
			// Token: 0x040001B9 RID: 441
			Schedule,
			// Token: 0x040001BA RID: 442
			Subscription,
			// Token: 0x040001BB RID: 443
			Security,
			// Token: 0x040001BC RID: 444
			ServiceController,
			// Token: 0x040001BD RID: 445
			DbCleanup,
			// Token: 0x040001BE RID: 446
			Cache,
			// Token: 0x040001BF RID: 447
			Chunks,
			// Token: 0x040001C0 RID: 448
			ExtensionFactory,
			// Token: 0x040001C1 RID: 449
			RunningJobs,
			// Token: 0x040001C2 RID: 450
			Processing,
			// Token: 0x040001C3 RID: 451
			ReportRendering,
			// Token: 0x040001C4 RID: 452
			HtmlViewer,
			// Token: 0x040001C5 RID: 453
			DataExtension,
			// Token: 0x040001C6 RID: 454
			EmailExtension,
			// Token: 0x040001C7 RID: 455
			ImageRenderer,
			// Token: 0x040001C8 RID: 456
			ExcelRenderer,
			// Token: 0x040001C9 RID: 457
			PreviewServer,
			// Token: 0x040001CA RID: 458
			ResourceUtilities,
			// Token: 0x040001CB RID: 459
			ReportPreview,
			// Token: 0x040001CC RID: 460
			UI,
			// Token: 0x040001CD RID: 461
			Crypto,
			// Token: 0x040001CE RID: 462
			SemanticModelGenerator,
			// Token: 0x040001CF RID: 463
			SemanticQueryEngine,
			// Token: 0x040001D0 RID: 464
			AppDomainManager,
			// Token: 0x040001D1 RID: 465
			HttpRuntime,
			// Token: 0x040001D2 RID: 466
			WcfRuntime,
			// Token: 0x040001D3 RID: 467
			AlertingRuntime,
			// Token: 0x040001D4 RID: 468
			UndoManager,
			// Token: 0x040001D5 RID: 469
			DataManager,
			// Token: 0x040001D6 RID: 470
			DataStructureManager,
			// Token: 0x040001D7 RID: 471
			Controls,
			// Token: 0x040001D8 RID: 472
			PowerView,
			// Token: 0x040001D9 RID: 473
			QueryDesign,
			// Token: 0x040001DA RID: 474
			MonitoredScope,
			// Token: 0x040001DB RID: 475
			CloudReportServer,
			// Token: 0x040001DC RID: 476
			ExecutionLog,
			// Token: 0x040001DD RID: 477
			DataShapeQueryTranslation,
			// Token: 0x040001DE RID: 478
			InfoNav,
			// Token: 0x040001DF RID: 479
			ReportServerWebApp,
			// Token: 0x040001E0 RID: 480
			Thread,
			// Token: 0x040001E1 RID: 481
			RdlEngineHost
		}

		// Token: 0x020000DB RID: 219
		private class DefaultRSTraceInternal : IRSTraceInternal
		{
			// Token: 0x17000211 RID: 529
			// (get) Token: 0x060004C7 RID: 1223 RVA: 0x000087FF File Offset: 0x000069FF
			public string TraceDirectory
			{
				get
				{
					return string.Empty;
				}
			}

			// Token: 0x17000212 RID: 530
			// (get) Token: 0x060004C8 RID: 1224 RVA: 0x00008806 File Offset: 0x00006A06
			public string CurrentTraceFilePath
			{
				get
				{
					return string.Empty;
				}
			}

			// Token: 0x17000213 RID: 531
			// (get) Token: 0x060004C9 RID: 1225 RVA: 0x0000880D File Offset: 0x00006A0D
			// (set) Token: 0x060004CA RID: 1226 RVA: 0x00008810 File Offset: 0x00006A10
			public bool BufferOutput
			{
				get
				{
					return false;
				}
				set
				{
				}
			}

			// Token: 0x17000214 RID: 532
			// (get) Token: 0x060004CB RID: 1227 RVA: 0x00008812 File Offset: 0x00006A12
			public bool IsTraceInitialized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060004CC RID: 1228 RVA: 0x00008815 File Offset: 0x00006A15
			public void ClearBuffer()
			{
			}

			// Token: 0x060004CD RID: 1229 RVA: 0x00008817 File Offset: 0x00006A17
			public void WriteBuffer()
			{
			}

			// Token: 0x060004CE RID: 1230 RVA: 0x00008819 File Offset: 0x00006A19
			public string GetDefaultTraceLevel()
			{
				return "0";
			}

			// Token: 0x060004CF RID: 1231 RVA: 0x00008820 File Offset: 0x00006A20
			public void Trace(string componentName, string message)
			{
			}

			// Token: 0x060004D0 RID: 1232 RVA: 0x00008822 File Offset: 0x00006A22
			public void Trace(string componentName, string format, params object[] arg)
			{
			}

			// Token: 0x060004D1 RID: 1233 RVA: 0x00008824 File Offset: 0x00006A24
			public void Trace(TraceLevel traceLevel, string componentName, string message)
			{
			}

			// Token: 0x060004D2 RID: 1234 RVA: 0x00008826 File Offset: 0x00006A26
			public void TraceWithDetails(TraceLevel traceLevel, string componentName, string message, string details)
			{
			}

			// Token: 0x060004D3 RID: 1235 RVA: 0x00008828 File Offset: 0x00006A28
			public void Trace(TraceLevel traceLevel, string componentName, string format, params object[] arg)
			{
			}

			// Token: 0x060004D4 RID: 1236 RVA: 0x0000882A File Offset: 0x00006A2A
			public void TraceException(TraceLevel traceLevel, string componentName, string message)
			{
			}

			// Token: 0x060004D5 RID: 1237 RVA: 0x0000882C File Offset: 0x00006A2C
			public void TraceWithNoEventLog(TraceLevel traceLevel, string componentName, string format, params object[] arg)
			{
			}

			// Token: 0x060004D6 RID: 1238 RVA: 0x0000882E File Offset: 0x00006A2E
			public void Fail(string componentName)
			{
				throw new InvalidOperationException(componentName);
			}

			// Token: 0x060004D7 RID: 1239 RVA: 0x00008836 File Offset: 0x00006A36
			public void Fail(string componentName, string message)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "({0}): {1}", componentName, message));
			}

			// Token: 0x060004D8 RID: 1240 RVA: 0x0000884E File Offset: 0x00006A4E
			public bool GetTraceLevel(string componentName, out TraceLevel componentTraceLevel)
			{
				componentTraceLevel = TraceLevel.Off;
				return false;
			}
		}
	}
}
