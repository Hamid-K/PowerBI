using System;
using System.Diagnostics;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x02000008 RID: 8
	internal static class RSPerfCounterInstallUtil
	{
		// Token: 0x0600006F RID: 111 RVA: 0x000042F0 File Offset: 0x000024F0
		internal static void InstallWebServicePerfCounters(bool defaultOnly, bool isSharePointMode)
		{
			string text = (isSharePointMode ? "MSRS 2017 Web Service SharePoint Mode" : "MSRS 2017 Web Service");
			if (!PerformanceCounterCategory.Exists(text))
			{
				CounterCreationDataCollection webServiceCounterInfo = RSPerfCounterInfo.GetWebServiceCounterInfo(defaultOnly);
				PerformanceCounterCategory.Create(text, RSPerfCounterRes.WebService, PerformanceCounterCategoryType.MultiInstance, webServiceCounterInfo);
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000432C File Offset: 0x0000252C
		internal static void InstallWindowsServicePerfCounters(bool defaultOnly, bool isSharePointMode)
		{
			string text = (isSharePointMode ? "MSRS 2017 Windows Service SharePoint Mode" : "MSRS 2017 Windows Service");
			if (!PerformanceCounterCategory.Exists(text))
			{
				CounterCreationDataCollection windowsServiceCounterInfo = RSPerfCounterInfo.GetWindowsServiceCounterInfo(defaultOnly, isSharePointMode);
				PerformanceCounterCategory.Create(text, RSPerfCounterRes.WinService, PerformanceCounterCategoryType.MultiInstance, windowsServiceCounterInfo);
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00004368 File Offset: 0x00002568
		internal static void InstallSharePointServicePerfCounters()
		{
			string text = "ReportServerSharePoint:Service";
			if (!PerformanceCounterCategory.Exists(text))
			{
				CounterCreationDataCollection sharePointServicePerfCounters = RSPerfCounterInfo.GetSharePointServicePerfCounters();
				PerformanceCounterCategory.Create(text, RSPerfCounterRes.SharePointService, PerformanceCounterCategoryType.MultiInstance, sharePointServicePerfCounters);
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00004398 File Offset: 0x00002598
		internal static void InstallProgressiveReportPerfCounters()
		{
			string text = "ReportServer.Power View";
			if (!PerformanceCounterCategory.Exists(text))
			{
				CounterCreationDataCollection progressiveReportPerfCounters = ProgressiveReportCounterInfo.GetProgressiveReportPerfCounters();
				PerformanceCounterCategory.Create(text, RSPerfCounterRes.ProgressiveReportsCategory, PerformanceCounterCategoryType.MultiInstance, progressiveReportPerfCounters);
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000043C8 File Offset: 0x000025C8
		internal static void Uninstall(bool isSharePointMode)
		{
			if (isSharePointMode)
			{
				if (PerformanceCounterCategory.Exists("MSRS 2017 Web Service SharePoint Mode"))
				{
					PerformanceCounterCategory.Delete("MSRS 2017 Web Service SharePoint Mode");
				}
				if (PerformanceCounterCategory.Exists("MSRS 2017 Windows Service SharePoint Mode"))
				{
					PerformanceCounterCategory.Delete("MSRS 2017 Windows Service SharePoint Mode");
				}
				if (PerformanceCounterCategory.Exists("ReportServerSharePoint:Service"))
				{
					PerformanceCounterCategory.Delete("ReportServerSharePoint:Service");
				}
				if (PerformanceCounterCategory.Exists("ReportServer.Power View"))
				{
					PerformanceCounterCategory.Delete("ReportServer.Power View");
					return;
				}
			}
			else
			{
				if (PerformanceCounterCategory.Exists("MSRS 2017 Web Service"))
				{
					PerformanceCounterCategory.Delete("MSRS 2017 Web Service");
				}
				if (PerformanceCounterCategory.Exists("MSRS 2017 Windows Service"))
				{
					PerformanceCounterCategory.Delete("MSRS 2017 Windows Service");
				}
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000445D File Offset: 0x0000265D
		internal static void DeleteKatmai()
		{
			PerformanceCounterCategory.Delete("MSRS 2008 Web Service");
			PerformanceCounterCategory.Delete("MSRS 2008 Windows Service");
		}
	}
}
