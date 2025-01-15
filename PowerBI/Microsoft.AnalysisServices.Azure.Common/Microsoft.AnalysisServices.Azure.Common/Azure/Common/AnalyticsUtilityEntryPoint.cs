using System;
using System.IO;
using Microsoft.Cloud.Platform.Eventing.EventsKit;
using Microsoft.Cloud.Platform.Modularization;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000037 RID: 55
	public static class AnalyticsUtilityEntryPoint
	{
		// Token: 0x06000353 RID: 851 RVA: 0x0000E980 File Offset: 0x0000CB80
		public static void Run<U, T>(string[] args) where U : UtilityBlock, new() where T : AnalyticsUtilityApplicationRoot<U>, new()
		{
			int ret = 0;
			TopLevelHandler.Run(null, TopLevelHandlerOption.SwallowNothing, delegate
			{
				string text = Path.Combine(CurrentProcess.MainModuleDirectory, ANPerformanceCountersConstants.BiAzurePerformanceCountersManifestFileName);
				TraceSourceBase<ANCommonTrace>.Tracer.TraceInformation("Installing performance counters");
				using (PerformanceCountersInstaller performanceCountersInstaller = new PerformanceCountersInstaller(ANPerformanceCountersConstants.BiAzurePerformanceCountersProvider, text))
				{
					performanceCountersInstaller.InstallPerformanceCountersProvider();
					TraceSourceBase<ANCommonTrace>.Tracer.TraceInformation("Performance counters installed");
				}
				ret = WinStarter.Run<T>(args);
			});
			Environment.Exit(ret);
		}
	}
}
