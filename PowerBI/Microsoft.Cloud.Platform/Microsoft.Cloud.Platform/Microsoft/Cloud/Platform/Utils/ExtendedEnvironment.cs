using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.EventsKit;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001F7 RID: 503
	public static class ExtendedEnvironment
	{
		// Token: 0x06000D49 RID: 3401 RVA: 0x0002E6BC File Offset: 0x0002C8BC
		internal static void Initialize()
		{
			AppDomain.CurrentDomain.UnhandledException += ExtendedEnvironment.UnhandledExceptionHandler;
			TaskScheduler.UnobservedTaskException += delegate(object sender, UnobservedTaskExceptionEventArgs eventArgs)
			{
				eventArgs.SetObserved();
				ExtendedEnvironment.CreateMemoryDump(new NonObservedExceptionWrapperException(null, eventArgs.Exception), false);
			};
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x0002E6F8 File Offset: 0x0002C8F8
		public static void CreateMemoryDump(Exception e, bool fatal)
		{
			ExtendedEnvironment.CreateMemoryDump(e, fatal, ExtendedEnvironment.GetTraceDump(e, fatal).ToString());
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x0002E70D File Offset: 0x0002C90D
		public static void CreateMemoryDump(Exception e, bool fatal, string errorText)
		{
			if (fatal || ExtendedEnvironment.DumpProcessMemoryEnabled)
			{
				Dumper.Current.CreateMemoryDump(e, fatal, errorText);
				return;
			}
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Error, "CreateMemoryDump: DumpProcessMemoryEnabled = false, fatal = {0}, error: \n{1}", new object[] { fatal, errorText });
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000D4C RID: 3404 RVA: 0x0002E74C File Offset: 0x0002C94C
		// (remove) Token: 0x06000D4D RID: 3405 RVA: 0x0002E780 File Offset: 0x0002C980
		public static event EventHandler<CrashEventArgs> OnCrash;

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000D4E RID: 3406 RVA: 0x0002E7B3 File Offset: 0x0002C9B3
		public static bool DumpProcessMemoryEnabled
		{
			get
			{
				return ExtendedEnvironment.s_dumpProcessMemoryEnabledTweak.Value;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000D4F RID: 3407 RVA: 0x0002E7BF File Offset: 0x0002C9BF
		public static bool CrashOnUnhandledCommunicationException
		{
			get
			{
				return ExtendedEnvironment.s_crashOnUnhandledCommunicationException.Value;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000D50 RID: 3408 RVA: 0x0002E7CB File Offset: 0x0002C9CB
		public static bool CrashOnUnhandledTimerException
		{
			get
			{
				return ExtendedEnvironment.s_crashOnUnhandledTimerException.Value;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000D51 RID: 3409 RVA: 0x0002E7D7 File Offset: 0x0002C9D7
		public static bool UseCloudCacheForDumpHashes
		{
			get
			{
				return ExtendedEnvironment.s_useCloudCacheForDumpHashes.Value;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000D52 RID: 3410 RVA: 0x0002E7E3 File Offset: 0x0002C9E3
		public static bool TreatNullReferenceExceptionAsFatal
		{
			get
			{
				return ExtendedEnvironment.s_treatNullReferenceExceptionAsFatal.Value;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000D53 RID: 3411 RVA: 0x0002E7EF File Offset: 0x0002C9EF
		public static Exception CrashException
		{
			get
			{
				return ExtendedEnvironment.s_crashTrail.Exception;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000D54 RID: 3412 RVA: 0x0002E7FB File Offset: 0x0002C9FB
		public static string CrashStackTrace
		{
			get
			{
				return ExtendedEnvironment.s_crashTrail.ExceptionStackTrace;
			}
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x0002E808 File Offset: 0x0002CA08
		private static void Crash(bool terminateOnEnd, object sender, CrashEventArgs args)
		{
			try
			{
			}
			finally
			{
				if (sender == null)
				{
					sender = "(No sender was provided)";
				}
				if (args == null || args.ExceptionObject == null)
				{
					args = ExtendedEnvironment.c_crashEmptyEventArgs;
				}
				CrashTrail crashTrail = new CrashTrail
				{
					CrashScenario = args.CrashScenario
				};
				if (CurrentProcess.WellKnownHost != ProcessWellKnownHost.MSTest && CurrentProcess.WellKnownHost != ProcessWellKnownHost.UIAF)
				{
					ExtendedDiagnostics.AlertDebuggerIfAttached();
				}
				bool flag = ExtendedEnvironment.PreOnCrash(crashTrail, terminateOnEnd, sender, args);
				EventHandler<CrashEventArgs> onCrash = ExtendedEnvironment.OnCrash;
				if (onCrash != null)
				{
					foreach (Delegate @delegate in onCrash.GetInvocationList())
					{
						if (@delegate != null)
						{
							try
							{
								@delegate.DynamicInvoke(new object[] { sender, args });
							}
							catch (Exception ex)
							{
								if (ex.IsFatal())
								{
									ExtendedEnvironment.FailFast(ex.Message ?? "Crash handler threw a fatal exception");
								}
							}
						}
					}
				}
				ExtendedEnvironment.PostOnCrash(crashTrail, sender, args);
				if (terminateOnEnd)
				{
					if (!flag)
					{
						throw new CrashException(null, crashTrail.Exception);
					}
					ExtendedEnvironment.FailFast(crashTrail.Exception.Message);
				}
			}
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x0002E918 File Offset: 0x0002CB18
		public static TraceDump GetTraceDump(Exception ex, bool fatal)
		{
			TraceDump traceDump = new TraceDump();
			traceDump.Add("********************************************************************************");
			traceDump.Add(string.Format(CultureInfo.InvariantCulture, "Process (path:pid:tid '{0}':{1}:{2}) {3} ('{4}':'{5}')", new object[]
			{
				CurrentProcess.MainModuleFileName,
				CurrentProcess.Id,
				ExtendedThread.GetCurrentThreadId(),
				fatal ? "crashed" : "had exception",
				ex.GetType(),
				ex.Message
			}));
			traceDump.Add("********************************************************************************");
			ContextManager.Dump(traceDump, new int[0]);
			traceDump.Add("********************************************************************************");
			traceDump.Add("Exception stack trace:");
			traceDump.Add(ex.ToString());
			return traceDump;
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x0002E9D4 File Offset: 0x0002CBD4
		private static bool PreOnCrash(CrashTrail crashTrail, bool terminateOnEnd, object sender, CrashEventArgs args)
		{
			using (CallStackRef callStackRef = CallStackRef.Capture(true))
			{
				crashTrail.CrashStackTrace = callStackRef.StackTrace.ToString();
			}
			crashTrail.Sender = sender;
			crashTrail.EventArgs = args;
			Exception exceptionObject = args.ExceptionObject;
			crashTrail.Exception = exceptionObject;
			crashTrail.ExceptionStackTrace = exceptionObject.StackTrace;
			if (string.IsNullOrEmpty(crashTrail.ExceptionStackTrace) || exceptionObject == ExtendedEnvironment.c_crashEmptyException || exceptionObject == ExtendedEnvironment.c_crashNonExceptionException)
			{
				using (CallStackRef callStackRef2 = CallStackRef.Capture(true))
				{
					crashTrail.ExceptionStackTrace = callStackRef2.ToString();
				}
			}
			crashTrail.Id = string.Format(CultureInfo.InvariantCulture, "{0}_{1}", new object[]
			{
				CurrentProcess.Name,
				Guid.NewGuid()
			});
			bool flag = terminateOnEnd && !ExtendedEnvironment.IsTest;
			Exception exceptionObject2 = args.ExceptionObject;
			Thread currentThread = Thread.CurrentThread;
			TraceDump traceDump = ExtendedEnvironment.GetTraceDump(exceptionObject2, true);
			if (exceptionObject2.StackTrace != crashTrail.ExceptionStackTrace)
			{
				traceDump.Add("********************************************************************************");
				traceDump.Add("Crash trail exception stack trace:");
				traceDump.Add(crashTrail.ExceptionStackTrace);
			}
			traceDump.Add("********************************************************************************");
			traceDump.Add("Crash trail crash stack trace:");
			traceDump.Add(crashTrail.CrashStackTrace.ToString());
			crashTrail.Dump = traceDump;
			traceDump.WriteFatal();
			if (ExtendedEnvironment.IsTest)
			{
				try
				{
					traceDump.WriteConsole();
				}
				catch (ObjectDisposedException)
				{
				}
			}
			ExtendedEnvironment.s_crashTrail = crashTrail;
			return flag;
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x0002EB6C File Offset: 0x0002CD6C
		private static void PostOnCrash(CrashTrail crashTrail, object sender, CrashEventArgs args)
		{
			ExtendedEnvironment.CreateMemoryDump(crashTrail.Exception, true, crashTrail.Dump.ToString());
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x0002EB88 File Offset: 0x0002CD88
		public static void FailSlow(object sender, string message)
		{
			CrashEventArgs crashEventArgs = new CrashEventArgs(CrashScenario.FailSlow, new CrashException(message));
			ExtendedEnvironment.Crash(true, sender, crashEventArgs);
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x0002EBAC File Offset: 0x0002CDAC
		public static void FailSlow(object sender, Exception ex)
		{
			CrashEventArgs crashEventArgs = new CrashEventArgs(CrashScenario.FailSlow, ex);
			ExtendedEnvironment.Crash(true, sender, crashEventArgs);
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x0002EBCC File Offset: 0x0002CDCC
		public static void ApplyFailSlowOnFatalPolicy(object sender, Exception ex)
		{
			if (ExtendedEnvironment.s_failSlowPolicyEnabledTweak.Value && ex != null && ex.IsFatal())
			{
				CrashEventArgs crashEventArgs = new CrashEventArgs(CrashScenario.FailSlow, ex);
				ExtendedEnvironment.Crash(true, sender, crashEventArgs);
			}
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x0002EC00 File Offset: 0x0002CE00
		public static void FailFast(string message)
		{
			object obj = ExtendedEnvironment.s_failFastLock;
			lock (obj)
			{
				new WindowsEventLogWriter(WindowsEventLogConstants.DefaultEventLogSourceName).WriteEntry(message, EventLogEntryType.Error, 500);
				if (ExtendedEnvironment.IsTest)
				{
					throw new CrashException(message);
				}
				for (int i = 0; i < ExtendedEnvironment.c_maxRetriesBeforeKillProcess; i++)
				{
					if (Process.GetProcessesByName("sqldumper").Length != 0)
					{
						Thread.Sleep(ExtendedEnvironment.c_waitIntervalForSqlDumper);
					}
				}
				Process.GetCurrentProcess().Kill();
			}
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x0002EC90 File Offset: 0x0002CE90
		public static void FailFast(string message, Exception ex)
		{
			ExtendedEnvironment.FailFast(message + ", " + ex.ToString());
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x0002ECA8 File Offset: 0x0002CEA8
		private static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs args)
		{
			object exceptionObject = args.ExceptionObject;
			Exception ex;
			if (exceptionObject == null)
			{
				ex = ExtendedEnvironment.c_crashEmptyException;
			}
			else
			{
				ex = exceptionObject as Exception;
				if (ex == null)
				{
					ex = ExtendedEnvironment.c_crashNonExceptionException;
				}
			}
			if (ex is OutOfMemoryException || ex is StackOverflowException)
			{
				return;
			}
			CrashEventArgs crashEventArgs = new CrashEventArgs(CrashScenario.UnhandledException, ex);
			ExtendedEnvironment.Crash(false, sender, crashEventArgs);
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x0002ECFC File Offset: 0x0002CEFC
		public static MemoryStatus GetMemoryStatus()
		{
			ExtendedEnvironment.MEMORYSTATUSEX memorystatusex = new ExtendedEnvironment.MEMORYSTATUSEX
			{
				dwLength = (uint)Marshal.SizeOf(typeof(ExtendedEnvironment.MEMORYSTATUSEX))
			};
			if (!ExtendedEnvironment.NativeMethods.GlobalMemoryStatusEx(ref memorystatusex))
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
			return new MemoryStatus((long)memorystatusex.ullTotalPhys, (long)memorystatusex.ullAvailPhys, (long)memorystatusex.ullTotalVirtual, (long)memorystatusex.ullAvailVirtual);
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x0002ED5C File Offset: 0x0002CF5C
		public static bool IsElevated()
		{
			bool flag = false;
			if (Environment.OSVersion.Version.CompareTo(new Version(5, 2, 2147483647, 2147483647)) > 0)
			{
				WindowsIdentity current = WindowsIdentity.GetCurrent();
				if (current != null)
				{
					flag = new WindowsPrincipal(current).IsInRole(WindowsBuiltInRole.Administrator);
				}
			}
			return flag;
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000D61 RID: 3425 RVA: 0x0002EDA9 File Offset: 0x0002CFA9
		public static bool IsMsTest
		{
			get
			{
				return CurrentProcess.WellKnownHost == ProcessWellKnownHost.MSTest;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000D62 RID: 3426 RVA: 0x0002EDB3 File Offset: 0x0002CFB3
		public static bool IsUiaf
		{
			get
			{
				return CurrentProcess.WellKnownHost == ProcessWellKnownHost.UIAF;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000D63 RID: 3427 RVA: 0x0002EDBD File Offset: 0x0002CFBD
		public static bool IsTest
		{
			get
			{
				return ExtendedEnvironment.IsMsTest || ExtendedEnvironment.IsUiaf;
			}
		}

		// Token: 0x04000523 RID: 1315
		private static readonly Exception c_crashEmptyException = new StokeCrashException("(No exception is associated with the crash)");

		// Token: 0x04000524 RID: 1316
		private const string c_crashEmptySender = "(No sender was provided)";

		// Token: 0x04000525 RID: 1317
		private static readonly Exception c_crashNonExceptionException = new StokeCrashException("(The exception provided does not derive System.Exception)");

		// Token: 0x04000526 RID: 1318
		private static readonly CrashEventArgs c_crashEmptyEventArgs = new CrashEventArgs(CrashScenario.Unknown, new StokeCrashException("(No reason was provided)"));

		// Token: 0x04000527 RID: 1319
		private static readonly int c_maxRetriesBeforeKillProcess = 200;

		// Token: 0x04000528 RID: 1320
		private static readonly int c_waitIntervalForSqlDumper = 5000;

		// Token: 0x04000529 RID: 1321
		private static readonly object s_failFastLock = new object();

		// Token: 0x0400052A RID: 1322
		private static CrashTrail s_crashTrail;

		// Token: 0x0400052B RID: 1323
		public const string DumpProcessMemoryEnabledTweakName = "Microsoft.Cloud.Platform.Utils.DumpProcessMemoryEnabled";

		// Token: 0x0400052C RID: 1324
		public const string FailSlowPolicyTweakName = "Microsoft.Cloud.Platform.Utils.FailSlowPolicyEnabled";

		// Token: 0x0400052D RID: 1325
		public const string CrashOnUnhandledCommunicationExceptionTweakName = "Microsoft.Cloud.Platform.Utils.CrashOnUnhandledCommunicationException";

		// Token: 0x0400052E RID: 1326
		public const string CrashOnUnhandledTimerExceptionTweakName = "Microsoft.Cloud.Platform.Utils.CrashOnUnhandledTimerException";

		// Token: 0x0400052F RID: 1327
		public const string UseCloudCacheForDumpHashesTweakName = "Microsoft.Cloud.Platform.Utils.UseCloudCacheForDumpHashes";

		// Token: 0x04000530 RID: 1328
		public const string TreatNullReferenceExceptionAsFatalTweakName = "Microsoft.Cloud.Platform.Utils.TreatNullReferenceExceptionAsFatal";

		// Token: 0x04000531 RID: 1329
		private static Tweak<bool> s_dumpProcessMemoryEnabledTweak = Anchor.Tweaks.RegisterTweak<bool>("Microsoft.Cloud.Platform.Utils.DumpProcessMemoryEnabled", "When set, process memory dump will not be taken on process crash and FailSlow calls", true);

		// Token: 0x04000532 RID: 1330
		private static Tweak<bool> s_failSlowPolicyEnabledTweak = Anchor.Tweaks.RegisterTweak<bool>("Microsoft.Cloud.Platform.Utils.FailSlowPolicyEnabled", "When true, detection of fatal exceptions in Utils results in the immediate call to ExtendedEnvironment.FailSlow", false);

		// Token: 0x04000533 RID: 1331
		private static Tweak<bool> s_crashOnUnhandledCommunicationException = Anchor.Tweaks.RegisterTweak<bool>("Microsoft.Cloud.Platform.Utils.CrashOnUnhandledCommunicationException", "Crash process on unhandled unmonitored exception in Communication services", true);

		// Token: 0x04000534 RID: 1332
		private static Tweak<bool> s_crashOnUnhandledTimerException = Anchor.Tweaks.RegisterTweak<bool>("Microsoft.Cloud.Platform.Utils.CrashOnUnhandledTimerException", "Crash process on unhandled unmonitored exception in timers", true);

		// Token: 0x04000535 RID: 1333
		private static Tweak<bool> s_useCloudCacheForDumpHashes = Anchor.Tweaks.RegisterTweak<bool>("Microsoft.Cloud.Platform.Utils.UseCloudCacheForDumpHashes", "Use Cloud Cache to store dump hashes", false);

		// Token: 0x04000536 RID: 1334
		private static Tweak<bool> s_treatNullReferenceExceptionAsFatal = Anchor.Tweaks.RegisterTweak<bool>("Microsoft.Cloud.Platform.Utils.TreatNullReferenceExceptionAsFatal", "Treat NullReferenceException as fatal", false);

		// Token: 0x0200069C RID: 1692
		private struct MEMORYSTATUSEX
		{
			// Token: 0x040012B8 RID: 4792
			public uint dwLength;

			// Token: 0x040012B9 RID: 4793
			public uint dwMemoryLoad;

			// Token: 0x040012BA RID: 4794
			public ulong ullTotalPhys;

			// Token: 0x040012BB RID: 4795
			public ulong ullAvailPhys;

			// Token: 0x040012BC RID: 4796
			public ulong ullTotalPageFile;

			// Token: 0x040012BD RID: 4797
			public ulong ullAvailPageFile;

			// Token: 0x040012BE RID: 4798
			public ulong ullTotalVirtual;

			// Token: 0x040012BF RID: 4799
			public ulong ullAvailVirtual;

			// Token: 0x040012C0 RID: 4800
			public ulong ullAvailExtendedVirtual;
		}

		// Token: 0x0200069D RID: 1693
		private static class NativeMethods
		{
			// Token: 0x06002E02 RID: 11778
			[DllImport("kernel32.dll", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool GlobalMemoryStatusEx([In] [Out] ref ExtendedEnvironment.MEMORYSTATUSEX lpBuffer);
		}
	}
}
