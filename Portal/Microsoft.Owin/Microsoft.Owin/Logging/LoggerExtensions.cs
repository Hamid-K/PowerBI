using System;
using System.Diagnostics;
using System.Globalization;

namespace Microsoft.Owin.Logging
{
	// Token: 0x0200002E RID: 46
	public static class LoggerExtensions
	{
		// Token: 0x060001E1 RID: 481 RVA: 0x00004E09 File Offset: 0x00003009
		public static bool IsEnabled(this ILogger logger, TraceEventType eventType)
		{
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			return logger.WriteCore(eventType, 0, null, null, null);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00004E24 File Offset: 0x00003024
		public static void WriteVerbose(this ILogger logger, string data)
		{
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			logger.WriteCore(TraceEventType.Verbose, 0, data, null, LoggerExtensions.TheMessage);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00004E45 File Offset: 0x00003045
		public static void WriteInformation(this ILogger logger, string message)
		{
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			logger.WriteCore(TraceEventType.Information, 0, message, null, LoggerExtensions.TheMessage);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00004E68 File Offset: 0x00003068
		public static void WriteWarning(this ILogger logger, string message, params string[] args)
		{
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			logger.WriteCore(TraceEventType.Warning, 0, string.Format(CultureInfo.InvariantCulture, message, args), null, LoggerExtensions.TheMessage);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00004EA0 File Offset: 0x000030A0
		public static void WriteWarning(this ILogger logger, string message, Exception error)
		{
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			logger.WriteCore(TraceEventType.Warning, 0, message, error, LoggerExtensions.TheMessageAndError);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00004EC0 File Offset: 0x000030C0
		public static void WriteError(this ILogger logger, string message)
		{
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			logger.WriteCore(TraceEventType.Error, 0, message, null, LoggerExtensions.TheMessage);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00004EE0 File Offset: 0x000030E0
		public static void WriteError(this ILogger logger, string message, Exception error)
		{
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			logger.WriteCore(TraceEventType.Error, 0, message, error, LoggerExtensions.TheMessageAndError);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00004F00 File Offset: 0x00003100
		public static void WriteCritical(this ILogger logger, string message)
		{
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			logger.WriteCore(TraceEventType.Critical, 0, message, null, LoggerExtensions.TheMessage);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00004F20 File Offset: 0x00003120
		public static void WriteCritical(this ILogger logger, string message, Exception error)
		{
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			logger.WriteCore(TraceEventType.Critical, 0, message, error, LoggerExtensions.TheMessageAndError);
		}

		// Token: 0x0400005E RID: 94
		private static readonly Func<object, Exception, string> TheMessage = (object message, Exception error) => (string)message;

		// Token: 0x0400005F RID: 95
		private static readonly Func<object, Exception, string> TheMessageAndError = (object message, Exception error) => string.Format(CultureInfo.CurrentCulture, "{0}\r\n{1}", new object[] { message, error });
	}
}
