using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Microsoft.ProgramSynthesis.Utils.Logging
{
	// Token: 0x02000541 RID: 1345
	public class Logger
	{
		// Token: 0x06001E64 RID: 7780 RVA: 0x000592D8 File Offset: 0x000574D8
		private void WriteLog(Logger.LogLevel logLevel, string message, DateTime time, string source, int lineNumber)
		{
			this.WriteLog(logLevel, message, null, time, source, lineNumber);
		}

		// Token: 0x06001E65 RID: 7781 RVA: 0x000592E8 File Offset: 0x000574E8
		private void WriteLog(Logger.LogLevel logLevel, FormattableString message, DateTime time, string source, int lineNumber)
		{
			this.WriteLog(logLevel, FormattableString.Invariant(message), null, time, source, lineNumber);
		}

		// Token: 0x06001E66 RID: 7782 RVA: 0x000592FD File Offset: 0x000574FD
		private void WriteLog(Logger.LogLevel logLevel, FormattableString message, object details, DateTime time, string source, int lineNumber)
		{
			this.WriteLog(logLevel, FormattableString.Invariant(message), details, time, source, lineNumber);
		}

		// Token: 0x06001E67 RID: 7783 RVA: 0x00059314 File Offset: 0x00057514
		private void WriteLog(Logger.LogLevel logLevel, string message, object details, DateTime time, string source, int lineNumber)
		{
			if (!this.LogSourceInfo)
			{
				source = null;
				lineNumber = 0;
			}
			Logger.LogMessage logMessage = new Logger.LogMessage
			{
				LogLevel = logLevel,
				Message = message,
				Details = details,
				Time = time,
				Source = source,
				LineNumber = lineNumber
			};
			this.LogWriter(logMessage.Serialize());
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001E68 RID: 7784 RVA: 0x00059373 File Offset: 0x00057573
		// (set) Token: 0x06001E69 RID: 7785 RVA: 0x0005937B File Offset: 0x0005757B
		public Logger.LogLevel OutputLevel { get; set; } = Logger.LogLevel.None;

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001E6A RID: 7786 RVA: 0x00059384 File Offset: 0x00057584
		public bool LogSourceInfo { get; }

		// Token: 0x06001E6B RID: 7787 RVA: 0x0005938C File Offset: 0x0005758C
		public Logger(Action<string> logWriter, Logger.LogLevel outputLevel, bool logSourceInfo = true)
		{
			this.LogWriter = logWriter;
			this.OutputLevel = outputLevel;
			this.LogSourceInfo = logSourceInfo;
		}

		// Token: 0x06001E6C RID: 7788 RVA: 0x000593BC File Offset: 0x000575BC
		public static Action<string> FileWriter(string path)
		{
			Action<string> action;
			try
			{
				Directory.CreateDirectory(Path.GetDirectoryName(path));
				FileStream fileStream = File.Open(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
				StreamWriter streamWriter = new StreamWriter(fileStream, new UTF8Encoding(false))
				{
					AutoFlush = true
				};
				BlockingCollection<string> logQueue = new BlockingCollection<string>();
				Task.Run(delegate
				{
					for (;;)
					{
						string text = logQueue.Take();
						streamWriter.Write(text);
						streamWriter.Write(Environment.NewLine);
					}
				});
				action = new Action<string>(logQueue.Add);
			}
			catch (Exception)
			{
				action = delegate(string _)
				{
				};
			}
			return action;
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001E6D RID: 7789 RVA: 0x00059464 File Offset: 0x00057664
		// (set) Token: 0x06001E6E RID: 7790 RVA: 0x0005946C File Offset: 0x0005766C
		public Action<string> LogWriter { get; set; } = Logger.TraceWriter;

		// Token: 0x06001E6F RID: 7791 RVA: 0x00059475 File Offset: 0x00057675
		public bool ShouldLog(Logger.LogLevel logLevel)
		{
			return logLevel >= this.OutputLevel;
		}

		// Token: 0x06001E70 RID: 7792 RVA: 0x00059483 File Offset: 0x00057683
		public bool ShouldTrace()
		{
			return this.ShouldLog(Logger.LogLevel.All);
		}

		// Token: 0x06001E71 RID: 7793 RVA: 0x0005948C File Offset: 0x0005768C
		public bool ShouldDebug()
		{
			return this.ShouldLog(Logger.LogLevel.Debug);
		}

		// Token: 0x06001E72 RID: 7794 RVA: 0x00059495 File Offset: 0x00057695
		public bool ShouldInfo()
		{
			return this.ShouldLog(Logger.LogLevel.Info);
		}

		// Token: 0x06001E73 RID: 7795 RVA: 0x0005949E File Offset: 0x0005769E
		public bool ShouldWarn()
		{
			return this.ShouldLog(Logger.LogLevel.Warn);
		}

		// Token: 0x06001E74 RID: 7796 RVA: 0x000594A7 File Offset: 0x000576A7
		public bool ShouldError()
		{
			return this.ShouldLog(Logger.LogLevel.Error);
		}

		// Token: 0x06001E75 RID: 7797 RVA: 0x000594B0 File Offset: 0x000576B0
		public bool ShouldFatal()
		{
			return this.ShouldLog(Logger.LogLevel.Fatal);
		}

		// Token: 0x06001E76 RID: 7798 RVA: 0x000594B9 File Offset: 0x000576B9
		public void Log(Logger.LogLevel logLevel, string message, object details = null, [CallerFilePath] string source = "", [CallerLineNumber] int lineNumber = 0)
		{
			if (!this.ShouldLog(logLevel))
			{
				return;
			}
			this.WriteLog(logLevel, message, details, DateTime.Now, source, lineNumber);
		}

		// Token: 0x06001E77 RID: 7799 RVA: 0x000594D7 File Offset: 0x000576D7
		public void Log(Logger.LogLevel logLevel, FormattableString message, object details = null, [CallerFilePath] string source = "", [CallerLineNumber] int lineNumber = 0)
		{
			if (!this.ShouldLog(logLevel))
			{
				return;
			}
			this.WriteLog(logLevel, message, details, DateTime.Now, source, lineNumber);
		}

		// Token: 0x06001E78 RID: 7800 RVA: 0x000594F5 File Offset: 0x000576F5
		public void Trace(string message, object details = null, [CallerFilePath] string source = "", [CallerLineNumber] int lineNumber = 0)
		{
			this.Log(Logger.LogLevel.All, message, details, source, lineNumber);
		}

		// Token: 0x06001E79 RID: 7801 RVA: 0x00059503 File Offset: 0x00057703
		public void Trace(FormattableString message, object details = null, [CallerFilePath] string source = "", [CallerLineNumber] int lineNumber = 0)
		{
			this.Log(Logger.LogLevel.All, message, details, source, lineNumber);
		}

		// Token: 0x06001E7A RID: 7802 RVA: 0x00059511 File Offset: 0x00057711
		public void Debug(string message, object details = null, [CallerFilePath] string source = "", [CallerLineNumber] int lineNumber = 0)
		{
			this.Log(Logger.LogLevel.Debug, message, details, source, lineNumber);
		}

		// Token: 0x06001E7B RID: 7803 RVA: 0x0005951F File Offset: 0x0005771F
		public void Debug(FormattableString message, object details = null, [CallerFilePath] string source = "", [CallerLineNumber] int lineNumber = 0)
		{
			this.Log(Logger.LogLevel.Debug, message, details, source, lineNumber);
		}

		// Token: 0x06001E7C RID: 7804 RVA: 0x0005952D File Offset: 0x0005772D
		public void Info(string message, object details = null, [CallerFilePath] string source = "", [CallerLineNumber] int lineNumber = 0)
		{
			this.Log(Logger.LogLevel.Info, message, details, source, lineNumber);
		}

		// Token: 0x06001E7D RID: 7805 RVA: 0x0005953B File Offset: 0x0005773B
		public void Info(FormattableString message, object details = null, [CallerFilePath] string source = "", [CallerLineNumber] int lineNumber = 0)
		{
			this.Log(Logger.LogLevel.Info, message, details, source, lineNumber);
		}

		// Token: 0x06001E7E RID: 7806 RVA: 0x00059549 File Offset: 0x00057749
		public void Warn(string message, object details = null, [CallerFilePath] string source = "", [CallerLineNumber] int lineNumber = 0)
		{
			this.Log(Logger.LogLevel.Warn, message, details, source, lineNumber);
		}

		// Token: 0x06001E7F RID: 7807 RVA: 0x00059557 File Offset: 0x00057757
		public void Warn(FormattableString message, object details = null, [CallerFilePath] string source = "", [CallerLineNumber] int lineNumber = 0)
		{
			this.Log(Logger.LogLevel.Warn, message, details, source, lineNumber);
		}

		// Token: 0x06001E80 RID: 7808 RVA: 0x00059565 File Offset: 0x00057765
		public void Error(string message, object details = null, [CallerFilePath] string source = "", [CallerLineNumber] int lineNumber = 0)
		{
			this.Log(Logger.LogLevel.Error, message, details, source, lineNumber);
		}

		// Token: 0x06001E81 RID: 7809 RVA: 0x00059573 File Offset: 0x00057773
		public void Error(FormattableString message, object details = null, [CallerFilePath] string source = "", [CallerLineNumber] int lineNumber = 0)
		{
			this.Log(Logger.LogLevel.Error, message, details, source, lineNumber);
		}

		// Token: 0x06001E82 RID: 7810 RVA: 0x00059581 File Offset: 0x00057781
		public void Fatal(string message, object details = null, [CallerFilePath] string source = "", [CallerLineNumber] int lineNumber = 0)
		{
			this.Log(Logger.LogLevel.Fatal, message, details, source, lineNumber);
		}

		// Token: 0x06001E83 RID: 7811 RVA: 0x0005958F File Offset: 0x0005778F
		public void Fatal(FormattableString message, object details = null, [CallerFilePath] string source = "", [CallerLineNumber] int lineNumber = 0)
		{
			this.Log(Logger.LogLevel.Fatal, message, details, source, lineNumber);
		}

		// Token: 0x06001E84 RID: 7812 RVA: 0x0005959D File Offset: 0x0005779D
		public IStopwatchWrapper LogTiming(Logger.LogLevel logLevel, string message, [CallerFilePath] string source = "", [CallerLineNumber] int lineNumber = 0)
		{
			if (!this.ShouldLog(logLevel))
			{
				return null;
			}
			return new Logger.StopwatchWrapper(logLevel, message, source, lineNumber, this);
		}

		// Token: 0x06001E85 RID: 7813 RVA: 0x000595B5 File Offset: 0x000577B5
		public IStopwatchWrapper LogTiming(Logger.LogLevel logLevel, FormattableString message, [CallerFilePath] string source = "", [CallerLineNumber] int lineNumber = 0)
		{
			if (!this.ShouldLog(logLevel))
			{
				return null;
			}
			return new Logger.StopwatchWrapper(logLevel, FormattableString.Invariant(message), source, lineNumber, this);
		}

		// Token: 0x06001E86 RID: 7814 RVA: 0x000595D2 File Offset: 0x000577D2
		public IStopwatchWrapper TraceTiming(string message, [CallerFilePath] string source = "", [CallerLineNumber] int lineNumber = 0)
		{
			return this.LogTiming(Logger.LogLevel.All, message, source, lineNumber);
		}

		// Token: 0x06001E87 RID: 7815 RVA: 0x000595DE File Offset: 0x000577DE
		public IStopwatchWrapper TraceTiming(FormattableString message, [CallerFilePath] string source = "", [CallerLineNumber] int lineNumber = 0)
		{
			return this.LogTiming(Logger.LogLevel.All, message, source, lineNumber);
		}

		// Token: 0x06001E88 RID: 7816 RVA: 0x000595EA File Offset: 0x000577EA
		public IStopwatchWrapper DebugTiming(string message, [CallerFilePath] string source = "", [CallerLineNumber] int lineNumber = 0)
		{
			return this.LogTiming(Logger.LogLevel.Debug, message, source, lineNumber);
		}

		// Token: 0x06001E89 RID: 7817 RVA: 0x000595F6 File Offset: 0x000577F6
		public IStopwatchWrapper DebugTiming(FormattableString message, [CallerFilePath] string source = "", [CallerLineNumber] int lineNumber = 0)
		{
			return this.LogTiming(Logger.LogLevel.Debug, message, source, lineNumber);
		}

		// Token: 0x06001E8A RID: 7818 RVA: 0x00059602 File Offset: 0x00057802
		public IStopwatchWrapper InfoTiming(string message, [CallerFilePath] string source = "", [CallerLineNumber] int lineNumber = 0)
		{
			return this.LogTiming(Logger.LogLevel.Info, message, source, lineNumber);
		}

		// Token: 0x06001E8B RID: 7819 RVA: 0x0005960E File Offset: 0x0005780E
		public IStopwatchWrapper InfoTiming(FormattableString message, [CallerFilePath] string source = "", [CallerLineNumber] int lineNumber = 0)
		{
			return this.LogTiming(Logger.LogLevel.Info, message, source, lineNumber);
		}

		// Token: 0x04000EA6 RID: 3750
		public static readonly Action<string> TraceWriter = delegate(string log)
		{
			global::System.Diagnostics.Trace.WriteLine(log);
		};

		// Token: 0x04000EA7 RID: 3751
		public static readonly Logger Instance = new Logger(Logger.TraceWriter, Logger.LogLevel.None, true);

		// Token: 0x02000542 RID: 1346
		public enum LogLevel
		{
			// Token: 0x04000EAA RID: 3754
			All,
			// Token: 0x04000EAB RID: 3755
			Trace = 0,
			// Token: 0x04000EAC RID: 3756
			Debug,
			// Token: 0x04000EAD RID: 3757
			Info,
			// Token: 0x04000EAE RID: 3758
			Warn,
			// Token: 0x04000EAF RID: 3759
			Error,
			// Token: 0x04000EB0 RID: 3760
			Fatal,
			// Token: 0x04000EB1 RID: 3761
			None
		}

		// Token: 0x02000543 RID: 1347
		public class LogMessage
		{
			// Token: 0x1700054C RID: 1356
			// (get) Token: 0x06001E8D RID: 7821 RVA: 0x00059642 File Offset: 0x00057842
			// (set) Token: 0x06001E8E RID: 7822 RVA: 0x0005964A File Offset: 0x0005784A
			[JsonProperty(Order = 1)]
			[JsonConverter(typeof(StringEnumConverter))]
			public Logger.LogLevel LogLevel { get; set; }

			// Token: 0x1700054D RID: 1357
			// (get) Token: 0x06001E8F RID: 7823 RVA: 0x00059653 File Offset: 0x00057853
			// (set) Token: 0x06001E90 RID: 7824 RVA: 0x0005965B File Offset: 0x0005785B
			[JsonProperty(Order = 2)]
			public object Message { get; set; }

			// Token: 0x1700054E RID: 1358
			// (get) Token: 0x06001E91 RID: 7825 RVA: 0x00059664 File Offset: 0x00057864
			// (set) Token: 0x06001E92 RID: 7826 RVA: 0x0005966C File Offset: 0x0005786C
			[JsonProperty(Order = 3)]
			public object Details { get; set; }

			// Token: 0x1700054F RID: 1359
			// (get) Token: 0x06001E93 RID: 7827 RVA: 0x00059675 File Offset: 0x00057875
			// (set) Token: 0x06001E94 RID: 7828 RVA: 0x0005967D File Offset: 0x0005787D
			[JsonProperty(Order = 4)]
			public string Source { get; set; }

			// Token: 0x17000550 RID: 1360
			// (get) Token: 0x06001E95 RID: 7829 RVA: 0x00059686 File Offset: 0x00057886
			// (set) Token: 0x06001E96 RID: 7830 RVA: 0x0005968E File Offset: 0x0005788E
			[JsonProperty(Order = 5, DefaultValueHandling = DefaultValueHandling.Ignore)]
			public int LineNumber { get; set; }

			// Token: 0x17000551 RID: 1361
			// (get) Token: 0x06001E97 RID: 7831 RVA: 0x00059697 File Offset: 0x00057897
			// (set) Token: 0x06001E98 RID: 7832 RVA: 0x0005969F File Offset: 0x0005789F
			[JsonProperty(Order = 6)]
			public DateTime Time { get; set; }

			// Token: 0x06001E99 RID: 7833 RVA: 0x000596A8 File Offset: 0x000578A8
			public string Serialize()
			{
				return JsonConvert.SerializeObject(this, Logger.LogMessage.JsonSerializerSettings);
			}

			// Token: 0x04000EB2 RID: 3762
			private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
			{
				DateFormatString = "yyyy-MM-ddThh:mm:ss.fffffffzzz",
				Culture = CultureInfo.InvariantCulture,
				Formatting = Formatting.None,
				NullValueHandling = NullValueHandling.Ignore
			};
		}

		// Token: 0x02000544 RID: 1348
		internal class StopwatchWrapper : IStopwatchWrapper, IDisposable
		{
			// Token: 0x17000552 RID: 1362
			// (get) Token: 0x06001E9C RID: 7836 RVA: 0x000596E5 File Offset: 0x000578E5
			public TimeSpan Elapsed
			{
				get
				{
					return this._stopwatch.Elapsed;
				}
			}

			// Token: 0x06001E9D RID: 7837 RVA: 0x000596F4 File Offset: 0x000578F4
			public StopwatchWrapper(Logger.LogLevel logLevel, object message, string source, int lineNumber, Logger logger)
			{
				this._logLevel = logLevel;
				this._message = message;
				this._source = source;
				this._lineNumber = lineNumber;
				this._logger = logger ?? Logger.Instance;
				this._logger.WriteLog(this._logLevel, string.Format("{0} Started", this._message), DateTime.Now, this._source, this._lineNumber);
				this._stopwatch = Stopwatch.StartNew();
			}

			// Token: 0x06001E9E RID: 7838 RVA: 0x00059774 File Offset: 0x00057974
			public void Stop()
			{
				if (this._stopwatch == null)
				{
					return;
				}
				this._stopwatch.Stop();
				this._logger.WriteLog(this._logLevel, string.Format("{0} Elapsed: {1}", this._message, this._stopwatch.Elapsed), DateTime.Now, this._source, this._lineNumber);
				this._stopwatch = null;
			}

			// Token: 0x06001E9F RID: 7839 RVA: 0x000597DE File Offset: 0x000579DE
			public void Dispose()
			{
				this.Stop();
			}

			// Token: 0x04000EB9 RID: 3769
			private Stopwatch _stopwatch;

			// Token: 0x04000EBA RID: 3770
			private readonly Logger.LogLevel _logLevel;

			// Token: 0x04000EBB RID: 3771
			private readonly object _message;

			// Token: 0x04000EBC RID: 3772
			private readonly string _source;

			// Token: 0x04000EBD RID: 3773
			private readonly int _lineNumber;

			// Token: 0x04000EBE RID: 3774
			private readonly Logger _logger;
		}
	}
}
