using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace NLog.Fluent
{
	// Token: 0x0200016D RID: 365
	public static class Log
	{
		// Token: 0x06001115 RID: 4373 RVA: 0x0002C715 File Offset: 0x0002A915
		public static LogBuilder Level(LogLevel logLevel, [CallerFilePath] string callerFilePath = null)
		{
			return Log.Create(logLevel, callerFilePath);
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x0002C71E File Offset: 0x0002A91E
		public static LogBuilder Trace([CallerFilePath] string callerFilePath = null)
		{
			return Log.Create(LogLevel.Trace, callerFilePath);
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x0002C72B File Offset: 0x0002A92B
		public static LogBuilder Debug([CallerFilePath] string callerFilePath = null)
		{
			return Log.Create(LogLevel.Debug, callerFilePath);
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x0002C738 File Offset: 0x0002A938
		public static LogBuilder Info([CallerFilePath] string callerFilePath = null)
		{
			return Log.Create(LogLevel.Info, callerFilePath);
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x0002C745 File Offset: 0x0002A945
		public static LogBuilder Warn([CallerFilePath] string callerFilePath = null)
		{
			return Log.Create(LogLevel.Warn, callerFilePath);
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x0002C752 File Offset: 0x0002A952
		public static LogBuilder Error([CallerFilePath] string callerFilePath = null)
		{
			return Log.Create(LogLevel.Error, callerFilePath);
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x0002C75F File Offset: 0x0002A95F
		public static LogBuilder Fatal([CallerFilePath] string callerFilePath = null)
		{
			return Log.Create(LogLevel.Fatal, callerFilePath);
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x0002C76C File Offset: 0x0002A96C
		private static LogBuilder Create(LogLevel logLevel, string callerFilePath)
		{
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(callerFilePath ?? string.Empty);
			ILogger logger2;
			if (!string.IsNullOrWhiteSpace(fileNameWithoutExtension))
			{
				ILogger logger = LogManager.GetLogger(fileNameWithoutExtension);
				logger2 = logger;
			}
			else
			{
				logger2 = Log._logger;
			}
			LogBuilder logBuilder = new LogBuilder(logger2, logLevel);
			if (callerFilePath != null)
			{
				logBuilder.Property("CallerFilePath", callerFilePath);
			}
			return logBuilder;
		}

		// Token: 0x0400049A RID: 1178
		private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
	}
}
