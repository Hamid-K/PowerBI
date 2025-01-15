using System;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using JetBrains.Annotations;
using NLog.Internal;
using NLog.Time;

namespace NLog.Common
{
	// Token: 0x020001BC RID: 444
	public static class InternalLogger
	{
		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x0600138E RID: 5006 RVA: 0x0003552C File Offset: 0x0003372C
		public static bool IsTraceEnabled
		{
			get
			{
				return LogLevel.Trace >= InternalLogger.LogLevel;
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x0600138F RID: 5007 RVA: 0x0003553D File Offset: 0x0003373D
		public static bool IsDebugEnabled
		{
			get
			{
				return LogLevel.Debug >= InternalLogger.LogLevel;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06001390 RID: 5008 RVA: 0x0003554E File Offset: 0x0003374E
		public static bool IsInfoEnabled
		{
			get
			{
				return LogLevel.Info >= InternalLogger.LogLevel;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06001391 RID: 5009 RVA: 0x0003555F File Offset: 0x0003375F
		public static bool IsWarnEnabled
		{
			get
			{
				return LogLevel.Warn >= InternalLogger.LogLevel;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06001392 RID: 5010 RVA: 0x00035570 File Offset: 0x00033770
		public static bool IsErrorEnabled
		{
			get
			{
				return LogLevel.Error >= InternalLogger.LogLevel;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06001393 RID: 5011 RVA: 0x00035581 File Offset: 0x00033781
		public static bool IsFatalEnabled
		{
			get
			{
				return LogLevel.Fatal >= InternalLogger.LogLevel;
			}
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x00035592 File Offset: 0x00033792
		[StringFormatMethod("message")]
		public static void Trace([Localizable(false)] string message, params object[] args)
		{
			InternalLogger.Write(null, LogLevel.Trace, message, args);
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x000355A1 File Offset: 0x000337A1
		public static void Trace([Localizable(false)] string message)
		{
			InternalLogger.Write(null, LogLevel.Trace, message, null);
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x000355B0 File Offset: 0x000337B0
		public static void Trace([Localizable(false)] Func<string> messageFunc)
		{
			if (InternalLogger.IsTraceEnabled)
			{
				InternalLogger.Write(null, LogLevel.Trace, messageFunc(), null);
			}
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x000355CB File Offset: 0x000337CB
		[StringFormatMethod("message")]
		public static void Trace(Exception ex, [Localizable(false)] string message, params object[] args)
		{
			InternalLogger.Write(ex, LogLevel.Trace, message, args);
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x000355DA File Offset: 0x000337DA
		[StringFormatMethod("message")]
		public static void Trace<TArgument1>([Localizable(false)] string message, TArgument1 arg0)
		{
			if (InternalLogger.IsTraceEnabled)
			{
				InternalLogger.Log(null, LogLevel.Trace, message, new object[] { arg0 });
			}
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x000355FE File Offset: 0x000337FE
		[StringFormatMethod("message")]
		public static void Trace<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 arg0, TArgument2 arg1)
		{
			if (InternalLogger.IsTraceEnabled)
			{
				InternalLogger.Log(null, LogLevel.Trace, message, new object[] { arg0, arg1 });
			}
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x0003562B File Offset: 0x0003382B
		[StringFormatMethod("message")]
		public static void Trace<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 arg0, TArgument2 arg1, TArgument3 arg2)
		{
			if (InternalLogger.IsTraceEnabled)
			{
				InternalLogger.Log(null, LogLevel.Trace, message, new object[] { arg0, arg1, arg2 });
			}
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x00035661 File Offset: 0x00033861
		public static void Trace(Exception ex, [Localizable(false)] string message)
		{
			InternalLogger.Write(ex, LogLevel.Trace, message, null);
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x00035670 File Offset: 0x00033870
		public static void Trace(Exception ex, [Localizable(false)] Func<string> messageFunc)
		{
			if (InternalLogger.IsTraceEnabled)
			{
				InternalLogger.Write(ex, LogLevel.Trace, messageFunc(), null);
			}
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x0003568B File Offset: 0x0003388B
		[StringFormatMethod("message")]
		public static void Debug([Localizable(false)] string message, params object[] args)
		{
			InternalLogger.Write(null, LogLevel.Debug, message, args);
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x0003569A File Offset: 0x0003389A
		public static void Debug([Localizable(false)] string message)
		{
			InternalLogger.Write(null, LogLevel.Debug, message, null);
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x000356A9 File Offset: 0x000338A9
		public static void Debug([Localizable(false)] Func<string> messageFunc)
		{
			if (InternalLogger.IsDebugEnabled)
			{
				InternalLogger.Write(null, LogLevel.Debug, messageFunc(), null);
			}
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x000356C4 File Offset: 0x000338C4
		[StringFormatMethod("message")]
		public static void Debug(Exception ex, [Localizable(false)] string message, params object[] args)
		{
			InternalLogger.Write(ex, LogLevel.Debug, message, args);
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x000356D3 File Offset: 0x000338D3
		[StringFormatMethod("message")]
		public static void Debug<TArgument1>([Localizable(false)] string message, TArgument1 arg0)
		{
			if (InternalLogger.IsDebugEnabled)
			{
				InternalLogger.Log(null, LogLevel.Debug, message, new object[] { arg0 });
			}
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x000356F7 File Offset: 0x000338F7
		[StringFormatMethod("message")]
		public static void Debug<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 arg0, TArgument2 arg1)
		{
			if (InternalLogger.IsDebugEnabled)
			{
				InternalLogger.Log(null, LogLevel.Debug, message, new object[] { arg0, arg1 });
			}
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x00035724 File Offset: 0x00033924
		[StringFormatMethod("message")]
		public static void Debug<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 arg0, TArgument2 arg1, TArgument3 arg2)
		{
			if (InternalLogger.IsDebugEnabled)
			{
				InternalLogger.Log(null, LogLevel.Debug, message, new object[] { arg0, arg1, arg2 });
			}
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x0003575A File Offset: 0x0003395A
		public static void Debug(Exception ex, [Localizable(false)] string message)
		{
			InternalLogger.Write(ex, LogLevel.Debug, message, null);
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x00035769 File Offset: 0x00033969
		public static void Debug(Exception ex, [Localizable(false)] Func<string> messageFunc)
		{
			if (InternalLogger.IsDebugEnabled)
			{
				InternalLogger.Write(ex, LogLevel.Debug, messageFunc(), null);
			}
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x00035784 File Offset: 0x00033984
		[StringFormatMethod("message")]
		public static void Info([Localizable(false)] string message, params object[] args)
		{
			InternalLogger.Write(null, LogLevel.Info, message, args);
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x00035793 File Offset: 0x00033993
		public static void Info([Localizable(false)] string message)
		{
			InternalLogger.Write(null, LogLevel.Info, message, null);
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x000357A2 File Offset: 0x000339A2
		public static void Info([Localizable(false)] Func<string> messageFunc)
		{
			if (InternalLogger.IsInfoEnabled)
			{
				InternalLogger.Write(null, LogLevel.Info, messageFunc(), null);
			}
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x000357BD File Offset: 0x000339BD
		[StringFormatMethod("message")]
		public static void Info(Exception ex, [Localizable(false)] string message, params object[] args)
		{
			InternalLogger.Write(ex, LogLevel.Info, message, args);
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x000357CC File Offset: 0x000339CC
		[StringFormatMethod("message")]
		public static void Info<TArgument1>([Localizable(false)] string message, TArgument1 arg0)
		{
			if (InternalLogger.IsInfoEnabled)
			{
				InternalLogger.Log(null, LogLevel.Info, message, new object[] { arg0 });
			}
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x000357F0 File Offset: 0x000339F0
		[StringFormatMethod("message")]
		public static void Info<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 arg0, TArgument2 arg1)
		{
			if (InternalLogger.IsInfoEnabled)
			{
				InternalLogger.Log(null, LogLevel.Info, message, new object[] { arg0, arg1 });
			}
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x0003581D File Offset: 0x00033A1D
		[StringFormatMethod("message")]
		public static void Info<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 arg0, TArgument2 arg1, TArgument3 arg2)
		{
			if (InternalLogger.IsInfoEnabled)
			{
				InternalLogger.Log(null, LogLevel.Info, message, new object[] { arg0, arg1, arg2 });
			}
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x00035853 File Offset: 0x00033A53
		public static void Info(Exception ex, [Localizable(false)] string message)
		{
			InternalLogger.Write(ex, LogLevel.Info, message, null);
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x00035862 File Offset: 0x00033A62
		public static void Info(Exception ex, [Localizable(false)] Func<string> messageFunc)
		{
			if (InternalLogger.IsInfoEnabled)
			{
				InternalLogger.Write(ex, LogLevel.Info, messageFunc(), null);
			}
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x0003587D File Offset: 0x00033A7D
		[StringFormatMethod("message")]
		public static void Warn([Localizable(false)] string message, params object[] args)
		{
			InternalLogger.Write(null, LogLevel.Warn, message, args);
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x0003588C File Offset: 0x00033A8C
		public static void Warn([Localizable(false)] string message)
		{
			InternalLogger.Write(null, LogLevel.Warn, message, null);
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x0003589B File Offset: 0x00033A9B
		public static void Warn([Localizable(false)] Func<string> messageFunc)
		{
			if (InternalLogger.IsWarnEnabled)
			{
				InternalLogger.Write(null, LogLevel.Warn, messageFunc(), null);
			}
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x000358B6 File Offset: 0x00033AB6
		[StringFormatMethod("message")]
		public static void Warn(Exception ex, [Localizable(false)] string message, params object[] args)
		{
			InternalLogger.Write(ex, LogLevel.Warn, message, args);
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x000358C5 File Offset: 0x00033AC5
		[StringFormatMethod("message")]
		public static void Warn<TArgument1>([Localizable(false)] string message, TArgument1 arg0)
		{
			if (InternalLogger.IsWarnEnabled)
			{
				InternalLogger.Log(null, LogLevel.Warn, message, new object[] { arg0 });
			}
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x000358E9 File Offset: 0x00033AE9
		[StringFormatMethod("message")]
		public static void Warn<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 arg0, TArgument2 arg1)
		{
			if (InternalLogger.IsWarnEnabled)
			{
				InternalLogger.Log(null, LogLevel.Warn, message, new object[] { arg0, arg1 });
			}
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x00035916 File Offset: 0x00033B16
		[StringFormatMethod("message")]
		public static void Warn<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 arg0, TArgument2 arg1, TArgument3 arg2)
		{
			if (InternalLogger.IsWarnEnabled)
			{
				InternalLogger.Log(null, LogLevel.Warn, message, new object[] { arg0, arg1, arg2 });
			}
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x0003594C File Offset: 0x00033B4C
		public static void Warn(Exception ex, [Localizable(false)] string message)
		{
			InternalLogger.Write(ex, LogLevel.Warn, message, null);
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x0003595B File Offset: 0x00033B5B
		public static void Warn(Exception ex, [Localizable(false)] Func<string> messageFunc)
		{
			if (InternalLogger.IsWarnEnabled)
			{
				InternalLogger.Write(ex, LogLevel.Warn, messageFunc(), null);
			}
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x00035976 File Offset: 0x00033B76
		[StringFormatMethod("message")]
		public static void Error([Localizable(false)] string message, params object[] args)
		{
			InternalLogger.Write(null, LogLevel.Error, message, args);
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x00035985 File Offset: 0x00033B85
		public static void Error([Localizable(false)] string message)
		{
			InternalLogger.Write(null, LogLevel.Error, message, null);
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x00035994 File Offset: 0x00033B94
		public static void Error([Localizable(false)] Func<string> messageFunc)
		{
			if (InternalLogger.IsErrorEnabled)
			{
				InternalLogger.Write(null, LogLevel.Error, messageFunc(), null);
			}
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x000359AF File Offset: 0x00033BAF
		[StringFormatMethod("message")]
		public static void Error(Exception ex, [Localizable(false)] string message, params object[] args)
		{
			InternalLogger.Write(ex, LogLevel.Error, message, args);
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x000359BE File Offset: 0x00033BBE
		[StringFormatMethod("message")]
		public static void Error<TArgument1>([Localizable(false)] string message, TArgument1 arg0)
		{
			if (InternalLogger.IsErrorEnabled)
			{
				InternalLogger.Log(null, LogLevel.Error, message, new object[] { arg0 });
			}
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x000359E2 File Offset: 0x00033BE2
		[StringFormatMethod("message")]
		public static void Error<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 arg0, TArgument2 arg1)
		{
			if (InternalLogger.IsErrorEnabled)
			{
				InternalLogger.Log(null, LogLevel.Error, message, new object[] { arg0, arg1 });
			}
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x00035A0F File Offset: 0x00033C0F
		[StringFormatMethod("message")]
		public static void Error<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 arg0, TArgument2 arg1, TArgument3 arg2)
		{
			if (InternalLogger.IsErrorEnabled)
			{
				InternalLogger.Log(null, LogLevel.Error, message, new object[] { arg0, arg1, arg2 });
			}
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x00035A45 File Offset: 0x00033C45
		public static void Error(Exception ex, [Localizable(false)] string message)
		{
			InternalLogger.Write(ex, LogLevel.Error, message, null);
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x00035A54 File Offset: 0x00033C54
		public static void Error(Exception ex, [Localizable(false)] Func<string> messageFunc)
		{
			if (InternalLogger.IsErrorEnabled)
			{
				InternalLogger.Write(ex, LogLevel.Error, messageFunc(), null);
			}
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x00035A6F File Offset: 0x00033C6F
		[StringFormatMethod("message")]
		public static void Fatal([Localizable(false)] string message, params object[] args)
		{
			InternalLogger.Write(null, LogLevel.Fatal, message, args);
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x00035A7E File Offset: 0x00033C7E
		public static void Fatal([Localizable(false)] string message)
		{
			InternalLogger.Write(null, LogLevel.Fatal, message, null);
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x00035A8D File Offset: 0x00033C8D
		public static void Fatal([Localizable(false)] Func<string> messageFunc)
		{
			if (InternalLogger.IsFatalEnabled)
			{
				InternalLogger.Write(null, LogLevel.Fatal, messageFunc(), null);
			}
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x00035AA8 File Offset: 0x00033CA8
		[StringFormatMethod("message")]
		public static void Fatal(Exception ex, [Localizable(false)] string message, params object[] args)
		{
			InternalLogger.Write(ex, LogLevel.Fatal, message, args);
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x00035AB7 File Offset: 0x00033CB7
		[StringFormatMethod("message")]
		public static void Fatal<TArgument1>([Localizable(false)] string message, TArgument1 arg0)
		{
			if (InternalLogger.IsFatalEnabled)
			{
				InternalLogger.Log(null, LogLevel.Fatal, message, new object[] { arg0 });
			}
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x00035ADB File Offset: 0x00033CDB
		[StringFormatMethod("message")]
		public static void Fatal<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 arg0, TArgument2 arg1)
		{
			if (InternalLogger.IsFatalEnabled)
			{
				InternalLogger.Log(null, LogLevel.Fatal, message, new object[] { arg0, arg1 });
			}
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x00035B08 File Offset: 0x00033D08
		[StringFormatMethod("message")]
		public static void Fatal<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 arg0, TArgument2 arg1, TArgument3 arg2)
		{
			if (InternalLogger.IsFatalEnabled)
			{
				InternalLogger.Log(null, LogLevel.Fatal, message, new object[] { arg0, arg1, arg2 });
			}
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x00035B3E File Offset: 0x00033D3E
		public static void Fatal(Exception ex, [Localizable(false)] string message)
		{
			InternalLogger.Write(ex, LogLevel.Fatal, message, null);
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x00035B4D File Offset: 0x00033D4D
		public static void Fatal(Exception ex, [Localizable(false)] Func<string> messageFunc)
		{
			if (InternalLogger.IsFatalEnabled)
			{
				InternalLogger.Write(ex, LogLevel.Fatal, messageFunc(), null);
			}
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x00035B68 File Offset: 0x00033D68
		static InternalLogger()
		{
			InternalLogger.Reset();
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x00035B7C File Offset: 0x00033D7C
		public static void Reset()
		{
			InternalLogger.LogToConsole = InternalLogger.GetSetting<bool>("nlog.internalLogToConsole", "NLOG_INTERNAL_LOG_TO_CONSOLE", false);
			InternalLogger.LogToConsoleError = InternalLogger.GetSetting<bool>("nlog.internalLogToConsoleError", "NLOG_INTERNAL_LOG_TO_CONSOLE_ERROR", false);
			InternalLogger.LogLevel = InternalLogger.GetSetting("nlog.internalLogLevel", "NLOG_INTERNAL_LOG_LEVEL", LogLevel.Info);
			InternalLogger.LogFile = InternalLogger.GetSetting<string>("nlog.internalLogFile", "NLOG_INTERNAL_LOG_FILE", string.Empty);
			InternalLogger.LogToTrace = InternalLogger.GetSetting<bool>("nlog.internalLogToTrace", "NLOG_INTERNAL_LOG_TO_TRACE", false);
			InternalLogger.IncludeTimestamp = InternalLogger.GetSetting<bool>("nlog.internalLogIncludeTimestamp", "NLOG_INTERNAL_INCLUDE_TIMESTAMP", true);
			InternalLogger.Info("NLog internal logger initialized.");
			InternalLogger.ExceptionThrowWhenWriting = false;
			InternalLogger.LogWriter = null;
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x060013CC RID: 5068 RVA: 0x00035C25 File Offset: 0x00033E25
		// (set) Token: 0x060013CD RID: 5069 RVA: 0x00035C2C File Offset: 0x00033E2C
		public static LogLevel LogLevel
		{
			get
			{
				return InternalLogger._logLevel;
			}
			set
			{
				InternalLogger._logLevel = value ?? LogLevel.Info;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x060013CE RID: 5070 RVA: 0x00035C3D File Offset: 0x00033E3D
		// (set) Token: 0x060013CF RID: 5071 RVA: 0x00035C44 File Offset: 0x00033E44
		public static bool LogToConsole { get; set; }

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x060013D0 RID: 5072 RVA: 0x00035C4C File Offset: 0x00033E4C
		// (set) Token: 0x060013D1 RID: 5073 RVA: 0x00035C53 File Offset: 0x00033E53
		public static bool LogToConsoleError { get; set; }

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x060013D2 RID: 5074 RVA: 0x00035C5B File Offset: 0x00033E5B
		// (set) Token: 0x060013D3 RID: 5075 RVA: 0x00035C62 File Offset: 0x00033E62
		public static bool LogToTrace { get; set; }

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x060013D4 RID: 5076 RVA: 0x00035C6A File Offset: 0x00033E6A
		// (set) Token: 0x060013D5 RID: 5077 RVA: 0x00035C71 File Offset: 0x00033E71
		public static string LogFile
		{
			get
			{
				return InternalLogger._logFile;
			}
			set
			{
				InternalLogger._logFile = value;
				if (!string.IsNullOrEmpty(InternalLogger._logFile))
				{
					InternalLogger.CreateDirectoriesIfNeeded(InternalLogger._logFile);
				}
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x060013D6 RID: 5078 RVA: 0x00035C8F File Offset: 0x00033E8F
		// (set) Token: 0x060013D7 RID: 5079 RVA: 0x00035C96 File Offset: 0x00033E96
		public static TextWriter LogWriter { get; set; }

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x060013D8 RID: 5080 RVA: 0x00035C9E File Offset: 0x00033E9E
		// (set) Token: 0x060013D9 RID: 5081 RVA: 0x00035CA5 File Offset: 0x00033EA5
		public static bool IncludeTimestamp { get; set; }

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x060013DA RID: 5082 RVA: 0x00035CAD File Offset: 0x00033EAD
		// (set) Token: 0x060013DB RID: 5083 RVA: 0x00035CB4 File Offset: 0x00033EB4
		internal static bool ExceptionThrowWhenWriting { get; private set; }

		// Token: 0x060013DC RID: 5084 RVA: 0x00035CBC File Offset: 0x00033EBC
		[StringFormatMethod("message")]
		public static void Log(LogLevel level, [Localizable(false)] string message, params object[] args)
		{
			InternalLogger.Write(null, level, message, args);
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x00035CC7 File Offset: 0x00033EC7
		public static void Log(LogLevel level, [Localizable(false)] string message)
		{
			InternalLogger.Write(null, level, message, null);
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x00035CD2 File Offset: 0x00033ED2
		public static void Log(LogLevel level, [Localizable(false)] Func<string> messageFunc)
		{
			if (!InternalLogger.IsLogLevelDisabled(level))
			{
				InternalLogger.Write(null, level, messageFunc(), null);
			}
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x00035CEA File Offset: 0x00033EEA
		public static void Log(Exception ex, LogLevel level, [Localizable(false)] Func<string> messageFunc)
		{
			if (!InternalLogger.IsLogLevelDisabled(level))
			{
				InternalLogger.Write(ex, level, messageFunc(), null);
			}
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x00035D02 File Offset: 0x00033F02
		[StringFormatMethod("message")]
		public static void Log(Exception ex, LogLevel level, [Localizable(false)] string message, params object[] args)
		{
			InternalLogger.Write(ex, level, message, args);
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x00035D0D File Offset: 0x00033F0D
		public static void Log(Exception ex, LogLevel level, [Localizable(false)] string message)
		{
			InternalLogger.Write(ex, level, message, null);
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x00035D18 File Offset: 0x00033F18
		private static void Write([CanBeNull] Exception ex, LogLevel level, string message, [CanBeNull] object[] args)
		{
			if (InternalLogger.IsLogLevelDisabled(level))
			{
				return;
			}
			if (InternalLogger.IsSeriousException(ex))
			{
				return;
			}
			if (!InternalLogger.HasActiveLoggers())
			{
				return;
			}
			try
			{
				string text = InternalLogger.FormatMessage(ex, level, message, args);
				InternalLogger.WriteToLogFile(text);
				InternalLogger.WriteToTextWriter(text);
				InternalLogger.WriteToConsole(text);
				InternalLogger.WriteToErrorConsole(text);
				InternalLogger.WriteToTrace(text);
			}
			catch (Exception ex2)
			{
				InternalLogger.ExceptionThrowWhenWriting = true;
				if (ex2.MustBeRethrownImmediately())
				{
					throw;
				}
			}
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x00035D8C File Offset: 0x00033F8C
		private static string FormatMessage([CanBeNull] Exception ex, LogLevel level, string message, [CanBeNull] object[] args)
		{
			string text = ((args == null) ? message : string.Format(CultureInfo.InvariantCulture, message, args));
			StringBuilder stringBuilder = new StringBuilder(text.Length + "yyyy-MM-dd HH:mm:ss.ffff".Length + ((ex != null) ? ex.ToString().Length : 0) + 25);
			if (InternalLogger.IncludeTimestamp)
			{
				stringBuilder.Append(TimeSource.Current.Time.ToString("yyyy-MM-dd HH:mm:ss.ffff", CultureInfo.InvariantCulture)).Append(" ");
			}
			stringBuilder.Append(level).Append(" ").Append(text);
			if (ex != null)
			{
				ex.MarkAsLoggedToInternalLogger();
				stringBuilder.Append(" ").Append("Exception: ").Append(ex);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x00035E4F File Offset: 0x0003404F
		private static bool IsSeriousException(Exception exception)
		{
			return exception != null && exception.MustBeRethrownImmediately();
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x00035E5C File Offset: 0x0003405C
		private static bool IsLogLevelDisabled(LogLevel logLevel)
		{
			return InternalLogger._logLevel == LogLevel.Off || logLevel < InternalLogger._logLevel;
		}

		// Token: 0x060013E6 RID: 5094 RVA: 0x00035E77 File Offset: 0x00034077
		internal static bool HasActiveLoggers()
		{
			return !string.IsNullOrEmpty(InternalLogger.LogFile) || InternalLogger.LogToConsole || InternalLogger.LogToConsoleError || InternalLogger.LogToTrace || InternalLogger.LogWriter != null;
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x00035EA4 File Offset: 0x000340A4
		private static void WriteToLogFile(string message)
		{
			string logFile = InternalLogger.LogFile;
			if (string.IsNullOrEmpty(logFile))
			{
				return;
			}
			object lockObject = InternalLogger.LockObject;
			lock (lockObject)
			{
				using (StreamWriter streamWriter = File.AppendText(logFile))
				{
					streamWriter.WriteLine(message);
				}
			}
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x00035F10 File Offset: 0x00034110
		private static void WriteToTextWriter(string message)
		{
			TextWriter logWriter = InternalLogger.LogWriter;
			if (logWriter == null)
			{
				return;
			}
			object lockObject = InternalLogger.LockObject;
			lock (lockObject)
			{
				logWriter.WriteLine(message);
			}
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x00035F5C File Offset: 0x0003415C
		private static void WriteToConsole(string message)
		{
			if (!InternalLogger.LogToConsole)
			{
				return;
			}
			object lockObject = InternalLogger.LockObject;
			lock (lockObject)
			{
				Console.WriteLine(message);
			}
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x00035FA4 File Offset: 0x000341A4
		private static void WriteToErrorConsole(string message)
		{
			if (!InternalLogger.LogToConsoleError)
			{
				return;
			}
			object lockObject = InternalLogger.LockObject;
			lock (lockObject)
			{
				Console.Error.WriteLine(message);
			}
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x00035FF0 File Offset: 0x000341F0
		private static void WriteToTrace(string message)
		{
			if (!InternalLogger.LogToTrace)
			{
				return;
			}
			global::System.Diagnostics.Trace.WriteLine(message, "NLog");
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x00036008 File Offset: 0x00034208
		public static void LogAssemblyVersion(Assembly assembly)
		{
			try
			{
				FileVersionInfo fileVersionInfo = ((!string.IsNullOrEmpty(assembly.Location)) ? FileVersionInfo.GetVersionInfo(assembly.Location) : null);
				InternalLogger.Info<string, string, string>("{0}. File version: {1}. Product version: {2}.", assembly.FullName, (fileVersionInfo != null) ? fileVersionInfo.FileVersion : null, (fileVersionInfo != null) ? fileVersionInfo.ProductVersion : null);
			}
			catch (Exception ex)
			{
				InternalLogger.Error(ex, "Error logging version of assembly {0}.", new object[] { assembly.FullName });
			}
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x00036088 File Offset: 0x00034288
		private static string GetAppSettings(string configName)
		{
			try
			{
				return global::System.Configuration.ConfigurationManager.AppSettings[configName];
			}
			catch (Exception ex)
			{
				if (ex.MustBeRethrownImmediately())
				{
					throw;
				}
			}
			return null;
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x000360C4 File Offset: 0x000342C4
		private static string GetSettingString(string configName, string envName)
		{
			try
			{
				string appSettings = InternalLogger.GetAppSettings(configName);
				if (appSettings != null)
				{
					return appSettings;
				}
			}
			catch (Exception ex)
			{
				if (ex.MustBeRethrownImmediately())
				{
					throw;
				}
			}
			try
			{
				string safeEnvironmentVariable = EnvironmentHelper.GetSafeEnvironmentVariable(envName);
				if (!string.IsNullOrEmpty(safeEnvironmentVariable))
				{
					return safeEnvironmentVariable;
				}
			}
			catch (Exception ex2)
			{
				if (ex2.MustBeRethrownImmediately())
				{
					throw;
				}
			}
			return null;
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x0003612C File Offset: 0x0003432C
		private static LogLevel GetSetting(string configName, string envName, LogLevel defaultValue)
		{
			string settingString = InternalLogger.GetSettingString(configName, envName);
			if (settingString == null)
			{
				return defaultValue;
			}
			LogLevel logLevel;
			try
			{
				logLevel = LogLevel.FromString(settingString);
			}
			catch (Exception ex)
			{
				if (ex.MustBeRethrownImmediately())
				{
					throw;
				}
				logLevel = defaultValue;
			}
			return logLevel;
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x00036170 File Offset: 0x00034370
		private static T GetSetting<T>(string configName, string envName, T defaultValue)
		{
			string settingString = InternalLogger.GetSettingString(configName, envName);
			if (settingString == null)
			{
				return defaultValue;
			}
			T t;
			try
			{
				t = (T)((object)Convert.ChangeType(settingString, typeof(T), CultureInfo.InvariantCulture));
			}
			catch (Exception ex)
			{
				if (ex.MustBeRethrownImmediately())
				{
					throw;
				}
				t = defaultValue;
			}
			return t;
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x000361C8 File Offset: 0x000343C8
		private static void CreateDirectoriesIfNeeded(string filename)
		{
			try
			{
				if (!(InternalLogger.LogLevel == LogLevel.Off))
				{
					string directoryName = Path.GetDirectoryName(filename);
					if (!string.IsNullOrEmpty(directoryName))
					{
						Directory.CreateDirectory(directoryName);
					}
				}
			}
			catch (Exception ex)
			{
				InternalLogger.Error(ex, "Cannot create needed directories to '{0}'.", new object[] { filename });
				if (ex.MustBeRethrownImmediately())
				{
					throw;
				}
			}
		}

		// Token: 0x0400054F RID: 1359
		private static readonly object LockObject = new object();

		// Token: 0x04000550 RID: 1360
		private static string _logFile;

		// Token: 0x04000551 RID: 1361
		private static LogLevel _logLevel;
	}
}
