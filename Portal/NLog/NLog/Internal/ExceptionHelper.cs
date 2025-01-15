using System;
using System.Threading;
using NLog.Common;

namespace NLog.Internal
{
	// Token: 0x02000116 RID: 278
	internal static class ExceptionHelper
	{
		// Token: 0x06000EA6 RID: 3750 RVA: 0x00024534 File Offset: 0x00022734
		public static void MarkAsLoggedToInternalLogger(this Exception exception)
		{
			if (exception != null)
			{
				exception.Data["NLog.ExceptionLoggedToInternalLogger"] = true;
			}
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x00024550 File Offset: 0x00022750
		public static bool IsLoggedToInternalLogger(this Exception exception)
		{
			return exception != null && ((exception.Data["NLog.ExceptionLoggedToInternalLogger"] as bool?) ?? false);
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x00024590 File Offset: 0x00022790
		public static bool MustBeRethrown(this Exception exception)
		{
			if (exception.MustBeRethrownImmediately())
			{
				return true;
			}
			bool flag = exception is NLogConfigurationException;
			if (!exception.IsLoggedToInternalLogger())
			{
				LogLevel logLevel = (flag ? LogLevel.Warn : LogLevel.Error);
				InternalLogger.Log(exception, logLevel, "Error has been raised.");
			}
			if (!flag)
			{
				return LogManager.ThrowExceptions;
			}
			bool? throwConfigExceptions = LogManager.ThrowConfigExceptions;
			if (throwConfigExceptions == null)
			{
				return LogManager.ThrowExceptions;
			}
			return throwConfigExceptions.GetValueOrDefault();
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x000245FA File Offset: 0x000227FA
		public static bool MustBeRethrownImmediately(this Exception exception)
		{
			return exception is StackOverflowException || exception is ThreadAbortException || exception is OutOfMemoryException;
		}

		// Token: 0x040003EC RID: 1004
		private const string LoggedKey = "NLog.ExceptionLoggedToInternalLogger";
	}
}
