using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.BIServer.HostingEnvironment.Request;
using NLog;
using NLog.Config;
using NLog.LayoutRenderers;
using NLog.Targets;
using NLog.Targets.Wrappers;

namespace Microsoft.BIServer.HostingEnvironment
{
	// Token: 0x0200000F RID: 15
	public static class Logger
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002AB1 File Offset: 0x00000CB1
		public static bool MetricsEnabled
		{
			get
			{
				return Logger._metricsEnabled;
			}
		}

		// Token: 0x0600004A RID: 74
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern IntPtr GetStdHandle(int nStdHandle);

		// Token: 0x0600004B RID: 75 RVA: 0x00002AB8 File Offset: 0x00000CB8
		public static bool ConsoleAvailable()
		{
			return !(Logger.GetStdHandle(-11) == IntPtr.Zero) && Environment.UserInteractive;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002AD4 File Offset: 0x00000CD4
		static Logger()
		{
			int.TryParse(Logger.ConfigValueOrDefault("Logger.Metrics.MinLogMs", "10"), out Logger.MetricsMinLogMs);
			LogManager.Configuration = new LoggingConfiguration();
			if (Logger.ConsoleAvailable())
			{
				ColoredConsoleTarget coloredConsoleTarget = new ColoredConsoleTarget();
				LogManager.Configuration.AddTarget("console", coloredConsoleTarget);
				coloredConsoleTarget.Layout = "${threadid}|${message}${exception:format=ToString}";
				LogLevel logLevel = LogLevel.FromString(Logger.ConfigValueOrDefault("Logger.consoleLevel", "Info"));
				LoggingRule loggingRule = new LoggingRule("eventLog", logLevel, coloredConsoleTarget);
				LogManager.Configuration.LoggingRules.Add(loggingRule);
				Console.WriteLine("Configured Console Logger with level = {0}", logLevel);
			}
			LogManager.ReconfigExistingLoggers();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002BC4 File Offset: 0x00000DC4
		private static string ConfigValueOrDefault(string key, string defaultValue)
		{
			string text;
			if (!Args.CommandLine.TryGetSwitch(key, out text))
			{
				text = Environment.GetEnvironmentVariable(key);
				if (text == null)
				{
					text = defaultValue;
					Console.WriteLine("CONFIG: {0} = {1} (default)", key, text);
				}
				else
				{
					Console.WriteLine("CONFIG: {0} = {1} (env)", key, text);
				}
			}
			else
			{
				Console.WriteLine("CONFIG: {0} = {1} (switch)", key, text);
			}
			return text;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002C15 File Offset: 0x00000E15
		public static bool IsFatalEnabled()
		{
			return Logger.Log.IsEnabled(LogLevel.Fatal);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002C26 File Offset: 0x00000E26
		public static bool IsErrorEnabled()
		{
			return Logger.Log.IsEnabled(LogLevel.Error);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002C37 File Offset: 0x00000E37
		public static bool IsWarnEnabled()
		{
			return Logger.Log.IsEnabled(LogLevel.Warn);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002C48 File Offset: 0x00000E48
		public static bool IsInfoEnabled()
		{
			return Logger.Log.IsEnabled(LogLevel.Info);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002C59 File Offset: 0x00000E59
		public static bool IsDebugEnabled()
		{
			return Logger.Log.IsEnabled(LogLevel.Debug);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002C6A File Offset: 0x00000E6A
		public static bool IsTraceEnabled()
		{
			return Logger.Log.IsEnabled(LogLevel.Trace);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002C7C File Offset: 0x00000E7C
		public static Logger.Level ParseLevel(string levelString)
		{
			Logger.Level level;
			if (!Enum.TryParse<Logger.Level>(levelString, out level))
			{
				throw new ConfigurationException(string.Format("{0} is not a valid Logger.Level", levelString));
			}
			return level;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002CA5 File Offset: 0x00000EA5
		private static LogLevel Convert(Logger.Level level)
		{
			return LogLevel.FromString(level.ToString());
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002CBC File Offset: 0x00000EBC
		public static MemoryTarget CreateTest(Logger.Level level = Logger.Level.Info)
		{
			LogLevel logLevel = Logger.Convert(level);
			MemoryTarget memoryTarget = new MemoryTarget();
			LogManager.Configuration.AddTarget("memory", memoryTarget);
			memoryTarget.Layout = "${level:uppercase=true}|${message}${exception:format=ToString}";
			LoggingRule loggingRule = new LoggingRule("eventLog", logLevel, memoryTarget);
			LogManager.Configuration.LoggingRules.Add(loggingRule);
			LogManager.ReconfigExistingLoggers();
			Logger.Info("Memory Logger created: level {0}", new object[] { level });
			return memoryTarget;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002D34 File Offset: 0x00000F34
		public static IDisposable Create(string filePrefix, Logger.Level defaultLevel = Logger.Level.Info)
		{
			return Logger.Create(filePrefix, Logger.ParseLevel(StaticConfig.Current.GetOrDefault("Logger.level", defaultLevel.ToString())), StaticConfig.Current.Get("Logger.path"), StaticConfig.Current.GetIntOrDefault("Logger.rollMb", 32), StaticConfig.Current.GetIntOrDefault("Logger.keepFilesForDays", 14), StaticConfig.Current.GetIntOrDefault("Logger.asyncQueueDepth", 5000));
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002DB0 File Offset: 0x00000FB0
		public static IDisposable Create(string filePrefix, StaticConfig config)
		{
			return Logger.Create(filePrefix, Logger.ParseLevel(config.GetOrDefault("Logger.level", "Info")), config.Get("Logger.path"), config.GetIntOrDefault("Logger.rollMb", 32), config.GetIntOrDefault("Logger.keepFilesForDays", 14), config.GetIntOrDefault("Logger.asyncQueueDepth", 5000));
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002E10 File Offset: 0x00001010
		private static void LogHeaderWithSystemInfo(string logFileName)
		{
			try
			{
				Func<int, string> func = (int deltaHours) => string.Format("UTC{0}{1}", (deltaHours > 0) ? "+" : "-", deltaHours);
				if (Logger.LogHeader == null)
				{
					object logHeaderLock = Logger.LogHeaderLock;
					lock (logHeaderLock)
					{
						string fileVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;
						string text = "Microsoft Power BI Report Server";
						OsInfoProvider osInfoProvider = new OsInfoProvider();
						Logger.LogHeader = string.Format("<Header>\r\n  <Product>{0}</Product>\r\n  <Locale>{1}</Locale>\r\n  <TimeZone>{2}</TimeZone>\r\n  <Path>{3}</Path>\r\n  <SystemName>{4}</SystemName>\r\n  <OSName>{5}</OSName>\r\n  <OSVersion>{6}</OSVersion>\r\n  <ProcessID>{7}</ProcessID>\r\n  <Virtualization>{8}</Virtualization>\r\n</Header>\r\n<ProcessorArchitecture>{9}</ProcessorArchitecture>\r\n<ApplicationArchitecture>{10}</ApplicationArchitecture>", new object[]
						{
							string.Format("{0} {1} ({2})", text, osInfoProvider.GetAppProductVersion(text), fileVersion),
							CultureInfo.CurrentCulture.NativeName ?? "",
							func(DateTimeOffset.Now.Offset.Hours),
							logFileName,
							Environment.MachineName,
							osInfoProvider.GetOsName(),
							osInfoProvider.GetOsVersion(),
							Process.GetCurrentProcess().Id.ToString(),
							osInfoProvider.GetVirtualizationEnabled(),
							osInfoProvider.GetCpuArchitecture(),
							"AMD64"
						});
					}
				}
				Logger.Info(Logger.LogHeader, Array.Empty<object>());
			}
			catch (Exception ex)
			{
				Logger.Warning("Error logging header with system/hardware parameters: {0}", new object[] { ex.ToString() });
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002FA8 File Offset: 0x000011A8
		public static IDisposable Create(string filePrefix, Logger.Level level, string traceFolder, int rollAtMb = 32, int keepFilesForDays = 14, int asyncQueueLimit = 5000)
		{
			if (Interlocked.Increment(ref Logger._configCount) > 1)
			{
				throw new Exception(string.Format("Attempt to reconfigure static logger. #{0} Do once per process.", Logger._configCount));
			}
			LogLevel logLevel = Logger.Convert(level);
			if (string.IsNullOrEmpty(traceFolder))
			{
				traceFolder = AppDomain.CurrentDomain.BaseDirectory;
			}
			traceFolder = Path.GetFullPath(traceFolder);
			string text = Path.Combine(traceFolder, filePrefix);
			ConfigurationItemFactory.Default.LayoutRenderers.RegisterDefinition("DailyDate", typeof(Logger.DailyDateLayoutRenderer));
			ConfigurationItemFactory.Default.LayoutRenderers.RegisterDefinition("ArchiveDate", typeof(Logger.ArchiveDateLayoutRenderer));
			Logger._eventLogFileTarget = Logger.CreateFileTarget(Logger.Log, text, ".log", "${longdate}|${level:uppercase=true}|${threadid}|${message}${exception:format=ToString}");
			Logger._eventLogFileTarget.ArchiveFileName = text + "_{#}.log";
			Logger._eventLogFileTarget.ArchiveDateFormat = "yyyy_MM_dd_HH_mm_ss";
			Logger._eventLogFileTarget.ArchiveNumbering = ArchiveNumberingMode.DateAndSequence;
			Logger._eventLogFileTarget.ArchiveAboveSize = (long)(rollAtMb * 1048576);
			AsyncTargetWrapper asyncTargetWrapper = Logger.SetAsyncLogging(Logger.Log, Logger._eventLogFileTarget, asyncQueueLimit, logLevel);
			AsyncTargetWrapper asyncTargetWrapper2 = null;
			string logFileName = Logger.FilePathFromTarget(Logger._eventLogFileTarget);
			string text2 = null;
			if (Logger._metricsEnabled)
			{
				FileTarget fileTarget = Logger.CreateFileTarget(Logger.Metrics, text, ".mtrx", "${longdate}, ${message}");
				text2 = Logger.FilePathFromTarget(fileTarget);
				asyncTargetWrapper2 = Logger.SetAsyncLogging(Logger.Metrics, fileTarget, asyncQueueLimit, LogLevel.Info);
			}
			LogManager.ReconfigExistingLoggers();
			Task.Run(delegate
			{
				Logger.LogHeaderWithSystemInfo(logFileName);
			});
			Logger.Info("File Logger created: {0} - level {1}, will roll at {2} Mb, process id {3}", new object[]
			{
				logFileName,
				level,
				rollAtMb,
				Process.GetCurrentProcess().Id.ToString()
			});
			if (text2 != null)
			{
				Logger.Info("Metrics Logger created: {0} - level {1}", new object[]
				{
					text2,
					LogLevel.Info
				});
			}
			return new Logger.LogShutdown(asyncTargetWrapper, asyncTargetWrapper2);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x0000318F File Offset: 0x0000138F
		private static string FilePathFromTarget(FileTarget target)
		{
			return target.FileName.Render(new LogEventInfo
			{
				TimeStamp = DateTime.Now
			});
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000031AC File Offset: 0x000013AC
		private static AsyncTargetWrapper SetAsyncLogging(Logger log, FileTarget fileTarget, int asyncQueueLimit, LogLevel level)
		{
			AsyncTargetWrapper asyncTargetWrapper = new AsyncTargetWrapper(fileTarget, asyncQueueLimit, AsyncTargetWrapperOverflowAction.Block);
			LoggingRule loggingRule = new LoggingRule(log.Name, level, asyncTargetWrapper);
			LogManager.Configuration.LoggingRules.Add(loggingRule);
			return asyncTargetWrapper;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000031E4 File Offset: 0x000013E4
		private static FileTarget CreateFileTarget(Logger logger, string filePathBase, string suffix, string format)
		{
			string text = logger.Name + ".fileTarget";
			FileTarget fileTarget = new FileTarget
			{
				Name = text,
				ConcurrentWrites = false,
				EnableFileDelete = false,
				FileName = filePathBase + "_${DailyDate}" + suffix,
				Layout = format
			};
			LogManager.Configuration.AddTarget(text, fileTarget);
			return fileTarget;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003250 File Offset: 0x00001450
		private static string AppendRequestContext(string formatString, LogLevel level)
		{
			if (!Logger.Log.IsEnabled(level))
			{
				return formatString;
			}
			RequestContext fromCallContext = RequestContext.GetFromCallContext();
			if (fromCallContext == null)
			{
				return formatString;
			}
			return formatString + "| " + fromCallContext.ToString();
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003288 File Offset: 0x00001488
		public static IDisposable UsingRegion(string label, params string[] labelArgs)
		{
			return new Logger.LogRegion(string.Format(label, labelArgs));
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000032A3 File Offset: 0x000014A3
		public static void Verbose(string formatString, params object[] formatParams)
		{
			formatString = Logger.AppendRequestContext(formatString, LogLevel.Trace);
			Logger.Log.Trace(formatString, formatParams);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000032A3 File Offset: 0x000014A3
		public static void Trace(string formatString, params object[] formatParams)
		{
			formatString = Logger.AppendRequestContext(formatString, LogLevel.Trace);
			Logger.Log.Trace(formatString, formatParams);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000032BE File Offset: 0x000014BE
		public static void Trace(Exception ex, string formatString, params object[] formatParams)
		{
			formatString = Logger.AppendRequestContext(formatString, LogLevel.Trace);
			Logger.Log.Trace(ex, formatString, formatParams);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000032DA File Offset: 0x000014DA
		public static void Debug(string formatString, params object[] formatParams)
		{
			formatString = Logger.AppendRequestContext(formatString, LogLevel.Debug);
			Logger.Log.Debug(formatString, formatParams);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000032F5 File Offset: 0x000014F5
		public static void Debug(Exception ex, string formatString, params object[] formatParams)
		{
			formatString = Logger.AppendRequestContext(formatString, LogLevel.Debug);
			Logger.Log.Debug(ex, formatString, formatParams);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003311 File Offset: 0x00001511
		public static void Config(string key, string value)
		{
			Logger.Log.Info<string, string>("CONFIG : {0} = [{1}]", key, value);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003324 File Offset: 0x00001524
		public static void Config(string key, object value, string src)
		{
			Logger.Log.Info<string, object, string>("CONFIG : {0} = [{1}] ({2})", key, value, src);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003338 File Offset: 0x00001538
		public static void Meter(string key, long timeMs)
		{
			if (Logger._metricsEnabled && timeMs >= (long)Logger.MetricsMinLogMs)
			{
				RequestContext fromCallContext = RequestContext.GetFromCallContext();
				if (fromCallContext == null)
				{
					string text = string.Format(", {0}, {1}", key, timeMs);
					Logger.Metrics.Info(text);
					return;
				}
				string text2 = string.Format("{0}, {1}, {2}", fromCallContext.RequestID, key, timeMs);
				Logger.Metrics.Info(text2);
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x0000339F File Offset: 0x0000159F
		public static void Info(string formatString, params object[] formatParams)
		{
			formatString = Logger.AppendRequestContext(formatString, LogLevel.Info);
			Logger.Log.Info(formatString, formatParams);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000033BA File Offset: 0x000015BA
		public static void Info(Exception ex, string formatString, params object[] formatParams)
		{
			formatString = Logger.AppendRequestContext(formatString, LogLevel.Info);
			Logger.Log.Info(ex, formatString, formatParams);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000033D6 File Offset: 0x000015D6
		public static void Warning(string formatString, params object[] formatParams)
		{
			formatString = Logger.AppendRequestContext(formatString, LogLevel.Warn);
			Logger.Log.Warn(formatString, formatParams);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000033F1 File Offset: 0x000015F1
		public static void Warning(Exception ex, string formatString, params object[] formatParams)
		{
			formatString = Logger.AppendRequestContext(formatString, LogLevel.Warn);
			Logger.Log.Warn(ex, formatString, formatParams);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000340D File Offset: 0x0000160D
		public static void Error(string formatString, params object[] formatParams)
		{
			formatString = Logger.AppendRequestContext(formatString, LogLevel.Error);
			Logger.Log.Error(formatString, formatParams);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003428 File Offset: 0x00001628
		public static void Error(Exception ex, string formatString, params object[] formatParams)
		{
			formatString = Logger.AppendRequestContext(formatString, LogLevel.Error);
			Logger.Log.Error(ex, formatString, formatParams);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003444 File Offset: 0x00001644
		public static void Fatal(string formatString, params object[] formatParams)
		{
			formatString = Logger.AppendRequestContext(formatString, LogLevel.Fatal);
			Logger.Log.Fatal(formatString, formatParams);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000345F File Offset: 0x0000165F
		public static void Fatal(Exception ex, string formatString, params object[] formatParams)
		{
			formatString = Logger.AppendRequestContext(formatString, LogLevel.Fatal);
			Logger.Log.Fatal(ex, formatString, formatParams);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000347B File Offset: 0x0000167B
		public static bool TryGetFileTargetName(out string result)
		{
			result = null;
			if (Logger._eventLogFileTarget == null || Logger._eventLogFileTarget.FileName == null)
			{
				return false;
			}
			result = Logger.FilePathFromTarget(Logger._eventLogFileTarget);
			return true;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000034A4 File Offset: 0x000016A4
		public static IEnumerable<LogFileInfo> GetExpiredLogFiles(IEnumerable<LogFileInfo> logFiles, int keepUntilDays)
		{
			return logFiles.Where((LogFileInfo fileInfo) => fileInfo.LastWriteTime + TimeSpan.FromDays((double)keepUntilDays) < DateTime.Now);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000034D0 File Offset: 0x000016D0
		public static void DeleteExpiredFiles(string traceFolder, int keepUntilDays)
		{
			Logger.DeleteExpiredFilesInternal(keepUntilDays, new DirectoryInfo(traceFolder).GetFiles("*.log"));
			Logger.DeleteExpiredFilesInternal(keepUntilDays, new DirectoryInfo(traceFolder).GetFiles("*.mtrx"));
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003500 File Offset: 0x00001700
		private static void DeleteExpiredFilesInternal(int keepUntilDays, FileInfo[] logFiles)
		{
			foreach (LogFileInfo logFileInfo in Logger.GetExpiredLogFiles(logFiles.Select((FileInfo fileInfo) => new LogFileInfo(fileInfo)), keepUntilDays))
			{
				Logger.Info("Deleting expired log file: {0} Last written: {1}", new object[] { logFileInfo.FullName, logFileInfo.LastWriteTime });
				try
				{
					File.Delete(logFileInfo.FullName);
				}
				catch (Exception ex)
				{
					Logger.Warning(ex, "Exception deleting expired log file", Array.Empty<object>());
				}
			}
		}

		// Token: 0x0400004E RID: 78
		private const string LevelKey = "Logger.level";

		// Token: 0x0400004F RID: 79
		private const string ConsoleLevelKey = "Logger.consoleLevel";

		// Token: 0x04000050 RID: 80
		private const string FolderKey = "Logger.path";

		// Token: 0x04000051 RID: 81
		private const string RollMbKey = "Logger.rollMb";

		// Token: 0x04000052 RID: 82
		private const string KeepFilesForDaysKey = "Logger.keepFilesForDays";

		// Token: 0x04000053 RID: 83
		private const string FolderAsyncQueueLimitKey = "Logger.asyncQueueDepth";

		// Token: 0x04000054 RID: 84
		private const string LogFormatConsole = "${threadid}|${message}${exception:format=ToString}";

		// Token: 0x04000055 RID: 85
		private const string LogFormatMemory = "${level:uppercase=true}|${message}${exception:format=ToString}";

		// Token: 0x04000056 RID: 86
		private const string LogFormatFile = "${longdate}|${level:uppercase=true}|${threadid}|${message}${exception:format=ToString}";

		// Token: 0x04000057 RID: 87
		private const string LogFormatMetrics = "${longdate}, ${message}";

		// Token: 0x04000058 RID: 88
		private const string LogHeaderTemplate = "<Header>\r\n  <Product>{0}</Product>\r\n  <Locale>{1}</Locale>\r\n  <TimeZone>{2}</TimeZone>\r\n  <Path>{3}</Path>\r\n  <SystemName>{4}</SystemName>\r\n  <OSName>{5}</OSName>\r\n  <OSVersion>{6}</OSVersion>\r\n  <ProcessID>{7}</ProcessID>\r\n  <Virtualization>{8}</Virtualization>\r\n</Header>\r\n<ProcessorArchitecture>{9}</ProcessorArchitecture>\r\n<ApplicationArchitecture>{10}</ApplicationArchitecture>";

		// Token: 0x04000059 RID: 89
		private static volatile string LogHeader;

		// Token: 0x0400005A RID: 90
		private static readonly object LogHeaderLock = new object();

		// Token: 0x0400005B RID: 91
		private const int DefaultLogRollMb = 32;

		// Token: 0x0400005C RID: 92
		private const int DefaultAsyncQueueDepth = 5000;

		// Token: 0x0400005D RID: 93
		private const int DefaultKeepFilesForDays = 14;

		// Token: 0x0400005E RID: 94
		private const int MegaBytes = 1048576;

		// Token: 0x0400005F RID: 95
		private const string LogSuffix = ".log";

		// Token: 0x04000060 RID: 96
		private const string MetricsSuffix = ".mtrx";

		// Token: 0x04000061 RID: 97
		private const string EventLoggerName = "eventLog";

		// Token: 0x04000062 RID: 98
		private const string FileTargetNameSuffix = ".fileTarget";

		// Token: 0x04000063 RID: 99
		private const string MetricsLoggerName = "metrics";

		// Token: 0x04000064 RID: 100
		private static int _configCount = 0;

		// Token: 0x04000065 RID: 101
		private static FileTarget _eventLogFileTarget;

		// Token: 0x04000066 RID: 102
		private static readonly Logger Log = LogManager.GetLogger("eventLog");

		// Token: 0x04000067 RID: 103
		private static readonly Logger Metrics = LogManager.GetLogger("metrics");

		// Token: 0x04000068 RID: 104
		private static readonly bool _metricsEnabled = string.Equals(Logger.ConfigValueOrDefault("Logger.Metrics", bool.FalseString), bool.TrueString, StringComparison.InvariantCultureIgnoreCase);

		// Token: 0x04000069 RID: 105
		private static readonly int MetricsMinLogMs;

		// Token: 0x0200004F RID: 79
		public enum Level
		{
			// Token: 0x04000129 RID: 297
			Off,
			// Token: 0x0400012A RID: 298
			Fatal,
			// Token: 0x0400012B RID: 299
			Error,
			// Token: 0x0400012C RID: 300
			Warn,
			// Token: 0x0400012D RID: 301
			Info,
			// Token: 0x0400012E RID: 302
			Debug,
			// Token: 0x0400012F RID: 303
			Trace
		}

		// Token: 0x02000050 RID: 80
		private sealed class LogShutdown : IDisposable
		{
			// Token: 0x060001CF RID: 463 RVA: 0x000068B2 File Offset: 0x00004AB2
			public LogShutdown(AsyncTargetWrapper asyncEventLog, AsyncTargetWrapper asyncMetricsLog = null)
			{
				if (asyncEventLog == null)
				{
					throw new ArgumentNullException();
				}
				this._asyncEventLog = asyncEventLog;
				this._asyncMetricsLog = asyncMetricsLog;
			}

			// Token: 0x060001D0 RID: 464 RVA: 0x000068D1 File Offset: 0x00004AD1
			public void Dispose()
			{
				Logger.Info("Logger shutting down", Array.Empty<object>());
				LogManager.Flush();
				LogManager.Shutdown();
				this._asyncEventLog.Dispose();
				if (this._asyncMetricsLog != null)
				{
					this._asyncMetricsLog.Dispose();
				}
			}

			// Token: 0x04000130 RID: 304
			private readonly AsyncTargetWrapper _asyncEventLog;

			// Token: 0x04000131 RID: 305
			private readonly AsyncTargetWrapper _asyncMetricsLog;
		}

		// Token: 0x02000051 RID: 81
		private sealed class LogRegion : IDisposable
		{
			// Token: 0x060001D1 RID: 465 RVA: 0x0000690A File Offset: 0x00004B0A
			public LogRegion(string label)
			{
				this._label = label;
				Logger.Info("----| {0} |----------------", new object[] { this._label });
			}

			// Token: 0x060001D2 RID: 466 RVA: 0x00006932 File Offset: 0x00004B32
			public void Dispose()
			{
				Logger.Info("----| {0} | END |----------", new object[] { this._label });
			}

			// Token: 0x04000132 RID: 306
			private readonly string _label;
		}

		// Token: 0x02000052 RID: 82
		[LayoutRenderer("DailyDate")]
		public class DailyDateLayoutRenderer : LayoutRenderer
		{
			// Token: 0x060001D3 RID: 467 RVA: 0x0000694D File Offset: 0x00004B4D
			public static void UpdateFileName()
			{
				Logger.DailyDateLayoutRenderer._isNewNameNeeded = true;
			}

			// Token: 0x060001D4 RID: 468 RVA: 0x00006958 File Offset: 0x00004B58
			private string GetCurrentName()
			{
				if (this._currentName == null || DateTime.Now.Date != this._currentDate || Logger.DailyDateLayoutRenderer._isNewNameNeeded)
				{
					this._currentDate = DateTime.Now.Date;
					this._currentName = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
					Logger.DailyDateLayoutRenderer._isNewNameNeeded = false;
				}
				return this._currentName;
			}

			// Token: 0x060001D5 RID: 469 RVA: 0x000069C5 File Offset: 0x00004BC5
			protected override void Append(StringBuilder builder, LogEventInfo logEvent)
			{
				builder.Append(this.GetCurrentName());
			}

			// Token: 0x04000133 RID: 307
			private string _currentName;

			// Token: 0x04000134 RID: 308
			private DateTime _currentDate;

			// Token: 0x04000135 RID: 309
			private static bool _isNewNameNeeded;
		}

		// Token: 0x02000053 RID: 83
		[LayoutRenderer("ArchiveDate")]
		public class ArchiveDateLayoutRenderer : LayoutRenderer
		{
			// Token: 0x060001D8 RID: 472 RVA: 0x000069DC File Offset: 0x00004BDC
			private string GetCurrentName()
			{
				Logger.DailyDateLayoutRenderer.UpdateFileName();
				return DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
			}

			// Token: 0x060001D9 RID: 473 RVA: 0x00006A00 File Offset: 0x00004C00
			protected override void Append(StringBuilder builder, LogEventInfo logEvent)
			{
				builder.Append(this.GetCurrentName());
			}
		}
	}
}
