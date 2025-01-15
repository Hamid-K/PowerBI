using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.Linq;
using Microsoft.IdentityModel.Abstractions;

namespace Microsoft.IdentityModel.Logging
{
	// Token: 0x0200000A RID: 10
	public class LogHelper
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000028F9 File Offset: 0x00000AF9
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002900 File Offset: 0x00000B00
		public static IIdentityLogger Logger { get; set; } = NullIdentityModelLogger.Instance;

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002908 File Offset: 0x00000B08
		// (set) Token: 0x06000045 RID: 69 RVA: 0x0000290F File Offset: 0x00000B0F
		internal static bool HeaderWritten
		{
			get
			{
				return LogHelper._isHeaderWritten;
			}
			set
			{
				LogHelper._isHeaderWritten = value;
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002917 File Offset: 0x00000B17
		public static bool IsEnabled(EventLogLevel level)
		{
			return LogHelper.Logger.IsEnabled(level) || IdentityModelEventSource.Logger.IsEnabled(LogHelper.EventLogLevelToEventLevel(level), EventKeywords.All);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0000293A File Offset: 0x00000B3A
		public static ArgumentNullException LogArgumentNullException(string argument)
		{
			return LogHelper.LogArgumentException<ArgumentNullException>(EventLevel.Error, argument, "IDX10000: The parameter '{0}' cannot be a 'null' or an empty object. ", new object[] { argument });
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002952 File Offset: 0x00000B52
		public static T LogException<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(string message) where T : Exception
		{
			return LogHelper.LogException<T>(EventLevel.Error, null, message, null);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000295D File Offset: 0x00000B5D
		public static T LogArgumentException<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(string argumentName, string message) where T : ArgumentException
		{
			return LogHelper.LogArgumentException<T>(EventLevel.Error, argumentName, null, message, null);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002969 File Offset: 0x00000B69
		public static T LogException<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(string format, params object[] args) where T : Exception
		{
			return LogHelper.LogException<T>(EventLevel.Error, null, format, args);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002974 File Offset: 0x00000B74
		public static T LogArgumentException<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(string argumentName, string format, params object[] args) where T : ArgumentException
		{
			return LogHelper.LogArgumentException<T>(EventLevel.Error, argumentName, null, format, args);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002980 File Offset: 0x00000B80
		public static T LogException<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(Exception innerException, string message) where T : Exception
		{
			return LogHelper.LogException<T>(EventLevel.Error, innerException, message, null);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000298B File Offset: 0x00000B8B
		public static T LogArgumentException<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(string argumentName, Exception innerException, string message) where T : ArgumentException
		{
			return LogHelper.LogArgumentException<T>(EventLevel.Error, argumentName, innerException, message, null);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002997 File Offset: 0x00000B97
		public static T LogException<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(Exception innerException, string format, params object[] args) where T : Exception
		{
			return LogHelper.LogException<T>(EventLevel.Error, innerException, format, args);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000029A2 File Offset: 0x00000BA2
		public static T LogArgumentException<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(string argumentName, Exception innerException, string format, params object[] args) where T : ArgumentException
		{
			return LogHelper.LogArgumentException<T>(EventLevel.Error, argumentName, innerException, format, args);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000029AE File Offset: 0x00000BAE
		public static T LogException<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(EventLevel eventLevel, string message) where T : Exception
		{
			return LogHelper.LogException<T>(eventLevel, null, message, null);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000029B9 File Offset: 0x00000BB9
		public static T LogArgumentException<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(EventLevel eventLevel, string argumentName, string message) where T : ArgumentException
		{
			return LogHelper.LogArgumentException<T>(eventLevel, argumentName, null, message, null);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000029C5 File Offset: 0x00000BC5
		public static T LogException<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(EventLevel eventLevel, string format, params object[] args) where T : Exception
		{
			return LogHelper.LogException<T>(eventLevel, null, format, args);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000029D0 File Offset: 0x00000BD0
		public static T LogArgumentException<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(EventLevel eventLevel, string argumentName, string format, params object[] args) where T : ArgumentException
		{
			return LogHelper.LogArgumentException<T>(eventLevel, argumentName, null, format, args);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000029DC File Offset: 0x00000BDC
		public static T LogException<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(EventLevel eventLevel, Exception innerException, string message) where T : Exception
		{
			return LogHelper.LogException<T>(eventLevel, innerException, message, null);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000029E7 File Offset: 0x00000BE7
		public static T LogArgumentException<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(EventLevel eventLevel, string argumentName, Exception innerException, string message) where T : ArgumentException
		{
			return LogHelper.LogArgumentException<T>(eventLevel, argumentName, innerException, message, null);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000029F3 File Offset: 0x00000BF3
		public static T LogException<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(EventLevel eventLevel, Exception innerException, string format, params object[] args) where T : Exception
		{
			return LogHelper.LogExceptionImpl<T>(eventLevel, null, innerException, format, args);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000029FF File Offset: 0x00000BFF
		public static T LogArgumentException<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(EventLevel eventLevel, string argumentName, Exception innerException, string format, params object[] args) where T : ArgumentException
		{
			return LogHelper.LogExceptionImpl<T>(eventLevel, argumentName, innerException, format, args);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002A0C File Offset: 0x00000C0C
		public static Exception LogExceptionMessage(Exception exception)
		{
			return LogHelper.LogExceptionMessage(EventLevel.Error, exception);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002A18 File Offset: 0x00000C18
		public static Exception LogExceptionMessage(EventLevel eventLevel, Exception exception)
		{
			if (exception == null)
			{
				return null;
			}
			if (IdentityModelEventSource.Logger.IsEnabled() && IdentityModelEventSource.Logger.LogLevel >= eventLevel)
			{
				IdentityModelEventSource.Logger.Write(eventLevel, exception.InnerException, exception.Message);
			}
			EventLogLevel eventLogLevel = (EventLogLevel)(Enum.IsDefined(typeof(EventLogLevel), (int)eventLevel) ? eventLevel : EventLevel.Error);
			if (LogHelper.Logger.IsEnabled(eventLogLevel))
			{
				LogHelper.Logger.Log(LogHelper.WriteEntry((EventLogLevel)eventLevel, exception.InnerException, exception.Message, null));
			}
			return exception;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002AA4 File Offset: 0x00000CA4
		public static void LogInformation(string message, params object[] args)
		{
			if (IdentityModelEventSource.Logger.IsEnabled() && IdentityModelEventSource.Logger.LogLevel >= EventLevel.Informational)
			{
				IdentityModelEventSource.Logger.WriteInformation(message, args);
			}
			if (Enum.IsDefined(typeof(EventLogLevel), 4) && LogHelper.Logger.IsEnabled(EventLogLevel.Informational))
			{
				LogHelper.Logger.Log(LogHelper.WriteEntry(EventLogLevel.Informational, null, message, args));
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002B10 File Offset: 0x00000D10
		public static void LogVerbose(string message, params object[] args)
		{
			if (IdentityModelEventSource.Logger.IsEnabled())
			{
				IdentityModelEventSource.Logger.WriteVerbose(message, args);
			}
			if (Enum.IsDefined(typeof(EventLogLevel), 5) && LogHelper.Logger.IsEnabled(EventLogLevel.Verbose))
			{
				LogHelper.Logger.Log(LogHelper.WriteEntry(EventLogLevel.Verbose, null, message, args));
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002B6C File Offset: 0x00000D6C
		public static void LogWarning(string message, params object[] args)
		{
			if (IdentityModelEventSource.Logger.IsEnabled())
			{
				IdentityModelEventSource.Logger.WriteWarning(message, args);
			}
			if (Enum.IsDefined(typeof(EventLogLevel), 3) && LogHelper.Logger.IsEnabled(EventLogLevel.Warning))
			{
				LogHelper.Logger.Log(LogHelper.WriteEntry(EventLogLevel.Warning, null, message, args));
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002BC8 File Offset: 0x00000DC8
		private static T LogExceptionImpl<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(EventLevel eventLevel, string argumentName, Exception innerException, string format, params object[] args) where T : Exception
		{
			string text;
			if (args != null)
			{
				text = string.Format(CultureInfo.InvariantCulture, format, args);
			}
			else
			{
				text = format;
			}
			if (IdentityModelEventSource.Logger.IsEnabled() && IdentityModelEventSource.Logger.LogLevel >= eventLevel)
			{
				IdentityModelEventSource.Logger.Write(eventLevel, innerException, text);
			}
			EventLogLevel eventLogLevel = (EventLogLevel)(Enum.IsDefined(typeof(EventLogLevel), (int)eventLevel) ? eventLevel : EventLevel.Error);
			if (LogHelper.Logger.IsEnabled(eventLogLevel))
			{
				LogHelper.Logger.Log(LogHelper.WriteEntry((EventLogLevel)eventLevel, innerException, text, null));
			}
			if (innerException != null)
			{
				if (string.IsNullOrEmpty(argumentName))
				{
					return (T)((object)Activator.CreateInstance(typeof(T), new object[] { text, innerException }));
				}
				return (T)((object)Activator.CreateInstance(typeof(T), new object[] { argumentName, text, innerException }));
			}
			else
			{
				if (string.IsNullOrEmpty(argumentName))
				{
					return (T)((object)Activator.CreateInstance(typeof(T), new object[] { text }));
				}
				return (T)((object)Activator.CreateInstance(typeof(T), new object[] { argumentName, text }));
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002CED File Offset: 0x00000EED
		private static EventLogLevel EventLevelToEventLogLevel(EventLevel eventLevel)
		{
			if (eventLevel > EventLevel.Verbose)
			{
				return EventLogLevel.Error;
			}
			return (EventLogLevel)eventLevel;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002CF6 File Offset: 0x00000EF6
		private static EventLevel EventLogLevelToEventLevel(EventLogLevel eventLevel)
		{
			if (eventLevel > EventLogLevel.Verbose)
			{
				return EventLevel.Error;
			}
			return (EventLevel)eventLevel;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002D00 File Offset: 0x00000F00
		public static string FormatInvariant(string format, params object[] args)
		{
			if (format == null)
			{
				return string.Empty;
			}
			if (args == null)
			{
				return format;
			}
			if (!IdentityModelEventSource.ShowPII)
			{
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				object[] array = args.Select(new Func<object, string>(LogHelper.RemovePII)).ToArray<string>();
				return string.Format(invariantCulture, format, array);
			}
			return string.Format(CultureInfo.InvariantCulture, format, args.Select(new Func<object, object>(LogHelper.SanitizeSecurityArtifact)).ToArray<object>());
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002D6C File Offset: 0x00000F6C
		private static object SanitizeSecurityArtifact(object arg)
		{
			if (arg == null)
			{
				return "null";
			}
			if (IdentityModelEventSource.LogCompleteSecurityArtifact && arg is ISafeLogSecurityArtifact)
			{
				return (arg as ISafeLogSecurityArtifact).UnsafeToString();
			}
			if (arg is ISafeLogSecurityArtifact)
			{
				return string.Format(CultureInfo.InvariantCulture, IdentityModelEventSource.HiddenSecurityArtifactString, ((arg != null) ? arg.GetType().ToString() : null) ?? "Null");
			}
			return arg;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002DD0 File Offset: 0x00000FD0
		private static string RemovePII(object arg)
		{
			Exception ex = arg as Exception;
			if (ex != null && LogHelper.IsCustomException(ex))
			{
				return ex.ToString();
			}
			if (arg is NonPII)
			{
				return arg.ToString();
			}
			return string.Format(CultureInfo.InvariantCulture, IdentityModelEventSource.HiddenPIIString, ((arg != null) ? arg.GetType().ToString() : null) ?? "Null");
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002E2E File Offset: 0x0000102E
		internal static bool IsCustomException(Exception ex)
		{
			return ex.GetType().FullName.StartsWith("Microsoft.IdentityModel.", StringComparison.Ordinal);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002E46 File Offset: 0x00001046
		public static object MarkAsNonPII(object arg)
		{
			return new NonPII(arg);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002E53 File Offset: 0x00001053
		public static object MarkAsSecurityArtifact(object arg, Func<object, string> callback)
		{
			return new SecurityArtifact(arg, callback);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002E61 File Offset: 0x00001061
		public static object MarkAsSecurityArtifact(object arg, Func<object, string> callback, Func<object, string> callbackUnsafe)
		{
			return new SecurityArtifact(arg, callback, callbackUnsafe);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002E70 File Offset: 0x00001070
		public static object MarkAsUnsafeSecurityArtifact(object arg, Func<object, string> callbackUnsafe)
		{
			return new SecurityArtifact(arg, new Func<object, string>(SecurityArtifact.UnknownSafeTokenCallback), callbackUnsafe);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002E8C File Offset: 0x0000108C
		private static LogEntry WriteEntry(EventLogLevel eventLogLevel, Exception innerException, string message, params object[] args)
		{
			if (string.IsNullOrEmpty(message))
			{
				return null;
			}
			if (innerException != null)
			{
				if (!IdentityModelEventSource.ShowPII && !LogHelper.IsCustomException(innerException))
				{
					message = string.Format(CultureInfo.InvariantCulture, "Message: {0}, InnerException: {1}. ", message, innerException.GetType());
				}
				else
				{
					message = string.Format(CultureInfo.InvariantCulture, "Message: {0}, InnerException: {1}. ", message, innerException.Message);
				}
			}
			message = ((args == null) ? message : LogHelper.FormatInvariant(message, args));
			LogEntry logEntry = new LogEntry();
			logEntry.EventLogLevel = eventLogLevel;
			if (!LogHelper._isHeaderWritten)
			{
				string text = string.Format(CultureInfo.InvariantCulture, "Microsoft.IdentityModel Version: {0}. Date {1}. {2}", typeof(IdentityModelEventSource).Assembly.GetName().Version.ToString(), DateTime.UtcNow, IdentityModelEventSource.ShowPII ? LogHelper._piiOnLogMessage : LogHelper._piiOffLogMessage);
				logEntry.Message = text + Environment.NewLine + message;
				LogHelper._isHeaderWritten = true;
			}
			else
			{
				logEntry.Message = message;
			}
			return logEntry;
		}

		// Token: 0x04000030 RID: 48
		private static bool _isHeaderWritten = false;

		// Token: 0x04000031 RID: 49
		private static string _piiOffLogMessage = "PII logging is OFF. See https://aka.ms/IdentityModel/PII for details. ";

		// Token: 0x04000032 RID: 50
		private static string _piiOnLogMessage = "PII logging is ON, do not use in production. See https://aka.ms/IdentityModel/PII for details. ";
	}
}
