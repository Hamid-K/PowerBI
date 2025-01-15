using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Cloud.Platform.Eventing.Base;
using Microsoft.Cloud.Platform.EventsKit;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Eventing.EventsKit
{
	// Token: 0x020003AD RID: 941
	public sealed class PerformanceCountersInstaller : IDisposable
	{
		// Token: 0x06001D12 RID: 7442 RVA: 0x0006E75F File Offset: 0x0006C95F
		public PerformanceCountersInstaller(Guid providerGuid, string manifestFilePath)
		{
			this.m_mutex = MutexUtils.GetNamedSystemMutexScope("PERFCTRINIT_49B68702-A75F-4B23-8C0A-E44D0A0A8070", MutexUtilsOptions.ContinueOnMutexAborted);
			this.m_providerGuid = providerGuid;
			this.m_manifestFilePath = manifestFilePath;
		}

		// Token: 0x06001D13 RID: 7443 RVA: 0x0006E786 File Offset: 0x0006C986
		public void InstallPerformanceCountersProvider()
		{
			this.UnloadPerformanceCountersProvider(InstallPerformanceCountersFailureMode.Ignore);
			this.LoadPerformanceCountersProvider(InstallPerformanceCountersFailureMode.ThrowException);
		}

		// Token: 0x06001D14 RID: 7444 RVA: 0x0006E798 File Offset: 0x0006C998
		private void UnloadPerformanceCountersProvider(InstallPerformanceCountersFailureMode mode)
		{
			TraceSourceBase<EventingTrace>.Tracer.TraceInformation("Uninstalling performance counters V2 of provider {0}", new object[] { this.m_providerGuid });
			int exitCode = 0;
			string output = string.Empty;
			string error = string.Empty;
			Exception ex = TopLevelHandler.Run(null, TopLevelHandlerOption.SwallowNothing, delegate
			{
				exitCode = ExtendedProcess.Run("unlodctr.exe", "/g:{0}".FormatWithInvariantCulture(new object[] { this.m_providerGuid.ToString("B") }), PerformanceCountersInstaller.c_unloadPerformanceCountersProviderTimeout, ExtendedProcessOptions.KillProcessOnTimeout, out output, out error);
				if (exitCode != 0)
				{
					TraceSourceBase<EventingTrace>.Tracer.TraceError("Unloading performance counters V2 failed with ExitCode:{0}, Error:{1}, Output:{2}.", new object[] { exitCode, error, output });
					return;
				}
				TraceSourceBase<EventingTrace>.Tracer.TraceInformation("Unloading performance counters V2 succeeded: {0}", new object[] { output });
			});
			if (mode == InstallPerformanceCountersFailureMode.ThrowException)
			{
				if (ex != null)
				{
					throw new UnloadingPerformanceCountersException("Exception was thrown during the execution of unlodctr.exe", ex);
				}
				if (exitCode != 0)
				{
					throw new UnloadingPerformanceCountersException(exitCode, error);
				}
			}
		}

		// Token: 0x06001D15 RID: 7445 RVA: 0x0006E834 File Offset: 0x0006CA34
		private void LoadPerformanceCountersProvider(InstallPerformanceCountersFailureMode mode)
		{
			TraceSourceBase<EventingTrace>.Tracer.TraceInformation("Installing performance counters V2 from manifest {0}, CurrentDirectory: {1}, MainModuleDirectory: {2}.", new object[]
			{
				this.m_manifestFilePath,
				Directory.GetCurrentDirectory(),
				CurrentProcess.MainModuleDirectory
			});
			string output = string.Empty;
			string error = string.Empty;
			int exitCode = 0;
			Exception ex = TopLevelHandler.Run(null, TopLevelHandlerOption.SwallowNothing, delegate
			{
				ProcessStartInfo processStartInfo = ExtendedProcess.CreateDefaultProcessStartInfo("lodctr.exe", "/m:\"{0}\"".FormatWithInvariantCulture(new object[] { this.m_manifestFilePath }));
				processStartInfo.WorkingDirectory = CurrentProcess.MainModuleDirectory;
				exitCode = ExtendedProcess.Run(processStartInfo, PerformanceCountersInstaller.c_loadPerformanceCountersProviderTimeout, ExtendedProcessOptions.KillProcessOnTimeout, out output, out error);
				if (exitCode != 0)
				{
					TraceSourceBase<EventingTrace>.Tracer.TraceError("Installing performance counters V2 from manifest failed with ExitCode: {0}, Error: {1}, Output:{2}.", new object[] { exitCode, error, output });
					return;
				}
				TraceSourceBase<EventingTrace>.Tracer.TraceInformation("Installing performance counters V2 from manifest succeeded: {0}", new object[] { output });
			});
			if (mode == InstallPerformanceCountersFailureMode.ThrowException)
			{
				if (ex != null)
				{
					throw new LoadingPerformanceCountersException("Exception was thrown during the execution of lodctr.exe", ex);
				}
				if (exitCode != 0)
				{
					throw new LoadingPerformanceCountersException(exitCode, error);
				}
			}
		}

		// Token: 0x06001D16 RID: 7446 RVA: 0x0006E8DA File Offset: 0x0006CADA
		public void Dispose()
		{
			if (this.m_mutex != null)
			{
				this.m_mutex.Dispose();
			}
			this.m_mutex = null;
		}

		// Token: 0x040009BC RID: 2492
		private IDisposable m_mutex;

		// Token: 0x040009BD RID: 2493
		private const string c_mutexName = "PERFCTRINIT_49B68702-A75F-4B23-8C0A-E44D0A0A8070";

		// Token: 0x040009BE RID: 2494
		private Guid m_providerGuid;

		// Token: 0x040009BF RID: 2495
		private readonly string m_manifestFilePath;

		// Token: 0x040009C0 RID: 2496
		private const string c_lodctrTool = "lodctr.exe";

		// Token: 0x040009C1 RID: 2497
		private const string c_unlodctrTool = "unlodctr.exe";

		// Token: 0x040009C2 RID: 2498
		private static readonly int c_loadPerformanceCountersProviderTimeout = (int)TimeSpan.FromSeconds(10.0).TotalMilliseconds;

		// Token: 0x040009C3 RID: 2499
		private static readonly int c_unloadPerformanceCountersProviderTimeout = (int)TimeSpan.FromSeconds(10.0).TotalMilliseconds;
	}
}
