using System;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Utils;

namespace Microsoft.Identity.Client.Internal.Logger
{
	// Token: 0x02000252 RID: 594
	internal sealed class DurationLogHelper : IDisposable
	{
		// Token: 0x060017EF RID: 6127 RVA: 0x0005008C File Offset: 0x0004E28C
		public DurationLogHelper(ILoggerAdapter logger, string measuredBlockName, LogLevel logLevel = LogLevel.Verbose)
		{
			this._logger = logger;
			this._measuredBlockName = measuredBlockName;
			this._logLevel = logLevel;
			this._startMilliseconds = StopwatchService.CurrentElapsedMilliseconds;
			this._logger.Log(LogLevel.Verbose, string.Empty, "Starting " + measuredBlockName);
		}

		// Token: 0x060017F0 RID: 6128 RVA: 0x000500DB File Offset: 0x0004E2DB
		public void Dispose()
		{
			this._logger.Log(LogLevel.Verbose, string.Empty, string.Format("Finished {0} in {1} ms", this._measuredBlockName, StopwatchService.CurrentElapsedMilliseconds - this._startMilliseconds));
		}

		// Token: 0x04000A82 RID: 2690
		private readonly ILoggerAdapter _logger;

		// Token: 0x04000A83 RID: 2691
		private readonly string _measuredBlockName;

		// Token: 0x04000A84 RID: 2692
		private readonly LogLevel _logLevel;

		// Token: 0x04000A85 RID: 2693
		private readonly long _startMilliseconds;
	}
}
