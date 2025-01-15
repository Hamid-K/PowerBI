using System;

namespace NLog.Fluent
{
	// Token: 0x0200016F RID: 367
	public static class LoggerExtensions
	{
		// Token: 0x06001132 RID: 4402 RVA: 0x0002CBB5 File Offset: 0x0002ADB5
		[CLSCompliant(false)]
		public static LogBuilder Log(this ILogger logger, LogLevel logLevel)
		{
			return new LogBuilder(logger, logLevel);
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x0002CBBE File Offset: 0x0002ADBE
		[CLSCompliant(false)]
		public static LogBuilder Trace(this ILogger logger)
		{
			return new LogBuilder(logger, LogLevel.Trace);
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x0002CBCB File Offset: 0x0002ADCB
		[CLSCompliant(false)]
		public static LogBuilder Debug(this ILogger logger)
		{
			return new LogBuilder(logger, LogLevel.Debug);
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x0002CBD8 File Offset: 0x0002ADD8
		[CLSCompliant(false)]
		public static LogBuilder Info(this ILogger logger)
		{
			return new LogBuilder(logger, LogLevel.Info);
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x0002CBE5 File Offset: 0x0002ADE5
		[CLSCompliant(false)]
		public static LogBuilder Warn(this ILogger logger)
		{
			return new LogBuilder(logger, LogLevel.Warn);
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x0002CBF2 File Offset: 0x0002ADF2
		[CLSCompliant(false)]
		public static LogBuilder Error(this ILogger logger)
		{
			return new LogBuilder(logger, LogLevel.Error);
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x0002CBFF File Offset: 0x0002ADFF
		[CLSCompliant(false)]
		public static LogBuilder Fatal(this ILogger logger)
		{
			return new LogBuilder(logger, LogLevel.Fatal);
		}
	}
}
