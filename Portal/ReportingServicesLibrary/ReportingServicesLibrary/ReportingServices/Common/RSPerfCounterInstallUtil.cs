using System;
using System.Diagnostics;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x02000372 RID: 882
	internal static class RSPerfCounterInstallUtil
	{
		// Token: 0x06001CDE RID: 7390 RVA: 0x00074BA0 File Offset: 0x00072DA0
		internal static void InstallWebServicePerfCounters(bool defaultOnly, bool isSharePointMode)
		{
			string text = (isSharePointMode ? "MSRS 2019 Web Service SharePoint Mode" : "MSRS 2019 Web Service");
			if (!PerformanceCounterCategory.Exists(text))
			{
				CounterCreationDataCollection webServiceCounterInfo = RSPerfCounterInfo.GetWebServiceCounterInfo(defaultOnly);
				PerformanceCounterCategory.Create(text, RSPerfCounterRes.WebService, PerformanceCounterCategoryType.MultiInstance, webServiceCounterInfo);
			}
		}

		// Token: 0x06001CDF RID: 7391 RVA: 0x00074BDC File Offset: 0x00072DDC
		internal static void InstallWindowsServicePerfCounters(bool defaultOnly, bool isSharePointMode)
		{
			string text = (isSharePointMode ? "MSRS 2019 Windows Service SharePoint Mode" : "MSRS 2019 Windows Service");
			if (!PerformanceCounterCategory.Exists(text))
			{
				CounterCreationDataCollection windowsServiceCounterInfo = RSPerfCounterInfo.GetWindowsServiceCounterInfo(defaultOnly, isSharePointMode);
				PerformanceCounterCategory.Create(text, RSPerfCounterRes.WinService, PerformanceCounterCategoryType.MultiInstance, windowsServiceCounterInfo);
			}
		}

		// Token: 0x06001CE0 RID: 7392 RVA: 0x00074C18 File Offset: 0x00072E18
		internal static void InstallSharePointServicePerfCounters()
		{
			string text = "ReportServerSharePoint:Service";
			if (!PerformanceCounterCategory.Exists(text))
			{
				CounterCreationDataCollection sharePointServicePerfCounters = RSPerfCounterInfo.GetSharePointServicePerfCounters();
				PerformanceCounterCategory.Create(text, RSPerfCounterRes.SharePointService, PerformanceCounterCategoryType.MultiInstance, sharePointServicePerfCounters);
			}
		}

		// Token: 0x06001CE1 RID: 7393 RVA: 0x00074C48 File Offset: 0x00072E48
		internal static void InstallProgressiveReportPerfCounters()
		{
			string text = "ReportServer.Power View";
			if (!PerformanceCounterCategory.Exists(text))
			{
				CounterCreationDataCollection progressiveReportPerfCounters = ProgressiveReportCounterInfo.GetProgressiveReportPerfCounters();
				PerformanceCounterCategory.Create(text, RSPerfCounterRes.ProgressiveReportsCategory, PerformanceCounterCategoryType.MultiInstance, progressiveReportPerfCounters);
			}
		}

		// Token: 0x06001CE2 RID: 7394 RVA: 0x00074C78 File Offset: 0x00072E78
		internal static void Uninstall(bool isSharePointMode)
		{
			if (isSharePointMode)
			{
				if (PerformanceCounterCategory.Exists("MSRS 2019 Web Service SharePoint Mode"))
				{
					PerformanceCounterCategory.Delete("MSRS 2019 Web Service SharePoint Mode");
				}
				if (PerformanceCounterCategory.Exists("MSRS 2019 Windows Service SharePoint Mode"))
				{
					PerformanceCounterCategory.Delete("MSRS 2019 Windows Service SharePoint Mode");
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
				if (PerformanceCounterCategory.Exists("MSRS 2019 Web Service"))
				{
					PerformanceCounterCategory.Delete("MSRS 2019 Web Service");
				}
				if (PerformanceCounterCategory.Exists("MSRS 2019 Windows Service"))
				{
					PerformanceCounterCategory.Delete("MSRS 2019 Windows Service");
				}
			}
		}

		// Token: 0x06001CE3 RID: 7395 RVA: 0x00074D0D File Offset: 0x00072F0D
		internal static void DeleteKatmai()
		{
			PerformanceCounterCategory.Delete("MSRS 2008 Web Service");
			PerformanceCounterCategory.Delete("MSRS 2008 Windows Service");
		}
	}
}
