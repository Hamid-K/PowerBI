using System;

namespace NLog
{
	// Token: 0x02000008 RID: 8
	public static class ILoggerExtensions
	{
		// Token: 0x06000175 RID: 373 RVA: 0x000022FA File Offset: 0x000004FA
		[CLSCompliant(false)]
		public static void Log(this ILogger logger, LogLevel level, Exception exception, LogMessageGenerator messageFunc)
		{
			if (logger.IsEnabled(level))
			{
				logger.Log(level, exception, messageFunc(), null);
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00002314 File Offset: 0x00000514
		[CLSCompliant(false)]
		public static void Trace(this ILogger logger, Exception exception, LogMessageGenerator messageFunc)
		{
			if (logger.IsTraceEnabled)
			{
				if (messageFunc == null)
				{
					throw new ArgumentNullException("messageFunc");
				}
				logger.Trace(exception, messageFunc());
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00002339 File Offset: 0x00000539
		[CLSCompliant(false)]
		public static void Debug(this ILogger logger, Exception exception, LogMessageGenerator messageFunc)
		{
			if (logger.IsDebugEnabled)
			{
				if (messageFunc == null)
				{
					throw new ArgumentNullException("messageFunc");
				}
				logger.Debug(exception, messageFunc());
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000235E File Offset: 0x0000055E
		[CLSCompliant(false)]
		public static void Info(this ILogger logger, Exception exception, LogMessageGenerator messageFunc)
		{
			if (logger.IsInfoEnabled)
			{
				if (messageFunc == null)
				{
					throw new ArgumentNullException("messageFunc");
				}
				logger.Info(exception, messageFunc());
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00002383 File Offset: 0x00000583
		[CLSCompliant(false)]
		public static void Warn(this ILogger logger, Exception exception, LogMessageGenerator messageFunc)
		{
			if (logger.IsWarnEnabled)
			{
				if (messageFunc == null)
				{
					throw new ArgumentNullException("messageFunc");
				}
				logger.Warn(exception, messageFunc());
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000023A8 File Offset: 0x000005A8
		[CLSCompliant(false)]
		public static void Error(this ILogger logger, Exception exception, LogMessageGenerator messageFunc)
		{
			if (logger.IsErrorEnabled)
			{
				if (messageFunc == null)
				{
					throw new ArgumentNullException("messageFunc");
				}
				logger.Error(exception, messageFunc());
			}
		}

		// Token: 0x0600017B RID: 379 RVA: 0x000023CD File Offset: 0x000005CD
		[CLSCompliant(false)]
		public static void Fatal(this ILogger logger, Exception exception, LogMessageGenerator messageFunc)
		{
			if (logger.IsFatalEnabled)
			{
				if (messageFunc == null)
				{
					throw new ArgumentNullException("messageFunc");
				}
				logger.Fatal(exception, messageFunc());
			}
		}
	}
}
