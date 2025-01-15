using System;
using System.Diagnostics;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002FD RID: 765
	internal static class DumpToEventLog
	{
		// Token: 0x06001C6C RID: 7276 RVA: 0x00055D0D File Offset: 0x00053F0D
		private static void WriteEventViewerEntry(object sender, UnhandledExceptionEventArgs args)
		{
			EventLogProvider.EventWriteServiceCrashUnkownError("AppFabricCachingService.Crash", args.ExceptionObject.ToString());
		}

		// Token: 0x06001C6D RID: 7277 RVA: 0x00055D28 File Offset: 0x00053F28
		private static void LogException(object sender, UnhandledExceptionEventArgs args)
		{
			if (Provider.IsEnabled(TraceLevel.Error))
			{
				EventLogWriter.WriteError("DistributedCache.CrashDump", "Service crashed. Exception information : Sender - {0}, Exception - {1}, Is process terminating - {2}", new object[] { sender, args.ExceptionObject, args.IsTerminating });
			}
		}

		// Token: 0x04000F65 RID: 3941
		internal static UnhandledExceptionEventHandler MinimalUHExceptionHandler = (UnhandledExceptionEventHandler)Delegate.Combine((UnhandledExceptionEventHandler)Delegate.Combine(new UnhandledExceptionEventHandler(DumpToEventLog.LogException), new UnhandledExceptionEventHandler(DumpToEventLog.WriteEventViewerEntry)), new UnhandledExceptionEventHandler(VelocityDiagnostics.DrainLogsOnUnhandledException));
	}
}
