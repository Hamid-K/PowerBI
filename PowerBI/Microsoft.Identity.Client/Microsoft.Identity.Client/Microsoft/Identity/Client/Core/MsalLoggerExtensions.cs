using System;
using Microsoft.Identity.Client.Internal.Logger;

namespace Microsoft.Identity.Client.Core
{
	// Token: 0x0200022C RID: 556
	internal static class MsalLoggerExtensions
	{
		// Token: 0x060016C0 RID: 5824 RVA: 0x0004B570 File Offset: 0x00049770
		public static void Always(this ILoggerAdapter logger, string message)
		{
			logger.Log(LogLevel.Always, string.Empty, message);
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x0004B57F File Offset: 0x0004977F
		public static void AlwaysPii(this ILoggerAdapter logger, string messageWithPii, string messageScrubbed)
		{
			logger.Log(LogLevel.Always, messageWithPii, messageScrubbed);
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x0004B58A File Offset: 0x0004978A
		public static void Error(this ILoggerAdapter logger, string message)
		{
			logger.Log(LogLevel.Error, string.Empty, message);
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x0004B599 File Offset: 0x00049799
		public static void ErrorPiiWithPrefix(this ILoggerAdapter logger, Exception exWithPii, string prefix)
		{
			logger.Log(LogLevel.Error, prefix + ((exWithPii != null) ? exWithPii.ToString() : null), prefix + LoggerHelper.GetPiiScrubbedExceptionDetails(exWithPii));
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x0004B5C1 File Offset: 0x000497C1
		public static void ErrorPii(this ILoggerAdapter logger, string messageWithPii, string messageScrubbed)
		{
			logger.Log(LogLevel.Error, messageWithPii, messageScrubbed);
		}

		// Token: 0x060016C5 RID: 5829 RVA: 0x0004B5CC File Offset: 0x000497CC
		public static void ErrorPii(this ILoggerAdapter logger, Exception exWithPii)
		{
			logger.Log(LogLevel.Error, exWithPii.ToString(), LoggerHelper.GetPiiScrubbedExceptionDetails(exWithPii));
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x0004B5E1 File Offset: 0x000497E1
		public static void Warning(this ILoggerAdapter logger, string message)
		{
			logger.Log(LogLevel.Warning, string.Empty, message);
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x0004B5F0 File Offset: 0x000497F0
		public static void WarningPii(this ILoggerAdapter logger, string messageWithPii, string messageScrubbed)
		{
			logger.Log(LogLevel.Warning, messageWithPii, messageScrubbed);
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x0004B5FB File Offset: 0x000497FB
		public static void WarningPii(this ILoggerAdapter logger, Exception exWithPii)
		{
			logger.Log(LogLevel.Warning, exWithPii.ToString(), LoggerHelper.GetPiiScrubbedExceptionDetails(exWithPii));
		}

		// Token: 0x060016C9 RID: 5833 RVA: 0x0004B610 File Offset: 0x00049810
		public static void WarningPiiWithPrefix(this ILoggerAdapter logger, Exception exWithPii, string prefix)
		{
			logger.Log(LogLevel.Warning, prefix + ((exWithPii != null) ? exWithPii.ToString() : null), prefix + LoggerHelper.GetPiiScrubbedExceptionDetails(exWithPii));
		}

		// Token: 0x060016CA RID: 5834 RVA: 0x0004B638 File Offset: 0x00049838
		public static void Info(this ILoggerAdapter logger, string message)
		{
			logger.Log(LogLevel.Info, string.Empty, message);
		}

		// Token: 0x060016CB RID: 5835 RVA: 0x0004B647 File Offset: 0x00049847
		public static void Info(this ILoggerAdapter logger, Func<string> messageProducer)
		{
			if (logger.IsLoggingEnabled(LogLevel.Info))
			{
				logger.Log(LogLevel.Info, string.Empty, messageProducer());
			}
		}

		// Token: 0x060016CC RID: 5836 RVA: 0x0004B664 File Offset: 0x00049864
		public static void InfoPii(this ILoggerAdapter logger, string messageWithPii, string messageScrubbed)
		{
			logger.Log(LogLevel.Info, messageWithPii, messageScrubbed);
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x0004B66F File Offset: 0x0004986F
		public static void InfoPii(this ILoggerAdapter logger, Func<string> messageWithPiiProducer, Func<string> messageScrubbedProducer)
		{
			if (logger.IsLoggingEnabled(LogLevel.Info))
			{
				logger.Log(LogLevel.Info, messageWithPiiProducer(), messageScrubbedProducer());
			}
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x0004B68D File Offset: 0x0004988D
		public static void InfoPii(this ILoggerAdapter logger, Exception exWithPii)
		{
			logger.Log(LogLevel.Info, (exWithPii != null) ? exWithPii.ToString() : null, LoggerHelper.GetPiiScrubbedExceptionDetails(exWithPii));
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x0004B6A8 File Offset: 0x000498A8
		public static void Verbose(this ILoggerAdapter logger, Func<string> messageProducer)
		{
			if (logger.IsLoggingEnabled(LogLevel.Verbose))
			{
				logger.Log(LogLevel.Verbose, string.Empty, messageProducer());
			}
		}

		// Token: 0x060016D0 RID: 5840 RVA: 0x0004B6C5 File Offset: 0x000498C5
		public static void VerbosePii(this ILoggerAdapter logger, Func<string> messageWithPiiProducer, Func<string> messageScrubbedProducer)
		{
			if (logger.IsLoggingEnabled(LogLevel.Verbose))
			{
				logger.Log(LogLevel.Verbose, messageWithPiiProducer(), messageScrubbedProducer());
			}
		}
	}
}
