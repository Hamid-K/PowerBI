using System;

namespace Microsoft.Mashup.Engine1.Library.Http
{
	// Token: 0x02000A98 RID: 2712
	public struct RetryHandlerResult
	{
		// Token: 0x06004BEB RID: 19435 RVA: 0x000FAE29 File Offset: 0x000F9029
		public RetryHandlerResult(RetryHandlerResultType type, TimeSpan delay, Exception exception)
		{
			this.Type = type;
			this.Delay = delay;
			this.Exception = exception;
		}

		// Token: 0x170017EE RID: 6126
		// (get) Token: 0x06004BEC RID: 19436 RVA: 0x000FAE40 File Offset: 0x000F9040
		public RetryHandlerResultType Type { get; }

		// Token: 0x170017EF RID: 6127
		// (get) Token: 0x06004BED RID: 19437 RVA: 0x000FAE48 File Offset: 0x000F9048
		public TimeSpan Delay { get; }

		// Token: 0x170017F0 RID: 6128
		// (get) Token: 0x06004BEE RID: 19438 RVA: 0x000FAE50 File Offset: 0x000F9050
		public Exception Exception { get; }

		// Token: 0x06004BEF RID: 19439 RVA: 0x000FAE58 File Offset: 0x000F9058
		public static RetryHandlerResult RetryAfterDelay(TimeSpan delay)
		{
			return new RetryHandlerResult(RetryHandlerResultType.RetryAfterCustomDelay, delay, null);
		}

		// Token: 0x06004BF0 RID: 19440 RVA: 0x000FAE64 File Offset: 0x000F9064
		public static RetryHandlerResult FailWithException(Exception exception)
		{
			return new RetryHandlerResult(RetryHandlerResultType.FailWithCustomException, default(TimeSpan), exception);
		}

		// Token: 0x04002847 RID: 10311
		public static readonly RetryHandlerResult RetryAfterDefaultDelay = new RetryHandlerResult(RetryHandlerResultType.RetryAfterDefaultDelay, default(TimeSpan), null);

		// Token: 0x04002848 RID: 10312
		public static readonly RetryHandlerResult FailWithOriginalException = new RetryHandlerResult(RetryHandlerResultType.FailWithOriginalException, default(TimeSpan), null);
	}
}
