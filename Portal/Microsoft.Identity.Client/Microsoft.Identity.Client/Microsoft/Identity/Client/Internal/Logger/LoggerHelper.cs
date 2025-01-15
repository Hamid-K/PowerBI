using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.PlatformsCommon.Factories;
using Microsoft.IdentityModel.Abstractions;

namespace Microsoft.Identity.Client.Internal.Logger
{
	// Token: 0x02000255 RID: 597
	internal class LoggerHelper
	{
		// Token: 0x060017FF RID: 6143 RVA: 0x00050348 File Offset: 0x0004E548
		public static string GetClientInfo(string clientName, string clientVersion)
		{
			if (string.IsNullOrEmpty(clientName) || "UnknownClient".Equals(clientName))
			{
				return string.Empty;
			}
			if (string.IsNullOrEmpty(clientVersion))
			{
				return " (" + clientName + ")";
			}
			return string.Concat(new string[] { " (", clientName, ": ", clientVersion, ")" });
		}

		// Token: 0x06001800 RID: 6144 RVA: 0x000503B4 File Offset: 0x0004E5B4
		public static ILoggerAdapter CreateLogger(Guid correlationId, ApplicationConfiguration config)
		{
			if (config.IdentityLogger != null)
			{
				return IdentityLoggerAdapter.Create(correlationId, config);
			}
			if (config.LoggingCallback == null)
			{
				return LoggerHelper.s_nullLogger.Value;
			}
			return CallbackIdentityLoggerAdapter.Create(correlationId, config, false);
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06001801 RID: 6145 RVA: 0x000503E1 File Offset: 0x0004E5E1
		public static ILoggerAdapter NullLogger
		{
			get
			{
				return LoggerHelper.s_nullLogger.Value;
			}
		}

		// Token: 0x06001802 RID: 6146 RVA: 0x000503F0 File Offset: 0x0004E5F0
		public static string FormatLogMessage(string message, bool piiEnabled, string correlationId, string clientInformation)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0} MSAL {1} {2} {3} {4} [{5}{6}]{7} {8}", new object[]
			{
				piiEnabled,
				LoggerHelper.s_msalVersionLazy.Value,
				LoggerHelper.s_skuLazy.Value,
				LoggerHelper.s_runtimeVersionLazy.Value,
				LoggerHelper.s_osLazy.Value,
				DateTime.UtcNow.ToString("u"),
				correlationId,
				clientInformation,
				message
			});
		}

		// Token: 0x06001803 RID: 6147 RVA: 0x00050474 File Offset: 0x0004E674
		internal static string GetPiiScrubbedExceptionDetails(Exception ex)
		{
			if (ex == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "Exception type: {0}", ex.GetType()));
			MsalException ex2 = ex as MsalException;
			if (ex2 != null)
			{
				stringBuilder.AppendLine(", ErrorCode: " + ex2.ErrorCode);
			}
			MsalServiceException ex3 = ex as MsalServiceException;
			if (ex3 != null)
			{
				stringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "HTTP StatusCode {0}", ex3.StatusCode));
				stringBuilder.AppendLine("CorrelationId " + ex3.CorrelationId);
				string[] errorCodes = ex3.ErrorCodes;
				if (errorCodes != null && errorCodes.Length > 0)
				{
					stringBuilder.AppendLine("Microsoft Entra ID Error Code AADSTS" + string.Join(" ", ex3.ErrorCodes));
				}
			}
			if (ex.InnerException != null)
			{
				stringBuilder.AppendLine("---> Inner Exception Details");
				stringBuilder.AppendLine(LoggerHelper.GetPiiScrubbedExceptionDetails(ex.InnerException));
				stringBuilder.AppendLine("=== End of inner exception stack trace ===");
			}
			if (ex is MsalClaimsChallengeException)
			{
				stringBuilder.AppendLine("The returned error contains a claims challenge. For additional info on how to handle claims related to multifactor authentication, Conditional Access, and incremental consent, see https://aka.ms/msal-conditional-access-claims. If you are using the On-Behalf-Of flow, see https://aka.ms/msal-conditional-access-claims-obo for details.");
			}
			stringBuilder.AppendLine("To see full exception details, enable PII Logging. See https://aka.ms/msal-net-logging");
			if (ex.StackTrace != null)
			{
				stringBuilder.AppendLine(ex.StackTrace);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001804 RID: 6148 RVA: 0x000505AD File Offset: 0x0004E7AD
		public static DurationLogHelper LogBlockDuration(ILoggerAdapter logger, string measuredBlockName, LogLevel logLevel = LogLevel.Verbose)
		{
			return new DurationLogHelper(logger, measuredBlockName, logLevel);
		}

		// Token: 0x06001805 RID: 6149 RVA: 0x000505B8 File Offset: 0x0004E7B8
		public static DurationLogHelper LogMethodDuration(ILoggerAdapter logger, LogLevel logLevel = LogLevel.Verbose, [CallerMemberName] string methodName = null, [CallerFilePath] string filePath = null)
		{
			string text = ((!string.IsNullOrEmpty(filePath)) ? Path.GetFileNameWithoutExtension(filePath) : "");
			return new DurationLogHelper(logger, text + ":" + methodName, logLevel);
		}

		// Token: 0x06001806 RID: 6150 RVA: 0x000505EE File Offset: 0x0004E7EE
		public static EventLogLevel GetEventLogLevel(LogLevel logLevel)
		{
			if (logLevel == LogLevel.Always)
			{
				return EventLogLevel.LogAlways;
			}
			return (EventLogLevel)(logLevel + 2);
		}

		// Token: 0x06001807 RID: 6151 RVA: 0x000505F9 File Offset: 0x0004E7F9
		public static string GetMessageToLog(string messageWithPii, string messageScrubbed, bool piiLoggingEnabled)
		{
			if (string.IsNullOrWhiteSpace(messageWithPii) || !piiLoggingEnabled)
			{
				return messageScrubbed;
			}
			return messageWithPii;
		}

		// Token: 0x04000A90 RID: 2704
		private static Lazy<string> s_msalVersionLazy = new Lazy<string>(new Func<string>(MsalIdHelper.GetMsalVersion));

		// Token: 0x04000A91 RID: 2705
		private static Lazy<string> s_runtimeVersionLazy = new Lazy<string>(() => PlatformProxyFactory.CreatePlatformProxy(null).GetRuntimeVersion());

		// Token: 0x04000A92 RID: 2706
		private static readonly Lazy<ILoggerAdapter> s_nullLogger = new Lazy<ILoggerAdapter>(() => new NullLogger());

		// Token: 0x04000A93 RID: 2707
		private static Lazy<string> s_osLazy = new Lazy<string>(delegate
		{
			string text;
			if (MsalIdHelper.GetMsalIdParameters(null).TryGetValue("x-client-OS", out text))
			{
				return text;
			}
			return "Unknown OS";
		});

		// Token: 0x04000A94 RID: 2708
		private static Lazy<string> s_skuLazy = new Lazy<string>(delegate
		{
			string text2;
			if (MsalIdHelper.GetMsalIdParameters(null).TryGetValue("x-client-SKU", out text2))
			{
				return text2;
			}
			return "Unknown SKU";
		});
	}
}
