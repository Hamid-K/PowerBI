using System;

namespace NLog.Config
{
	// Token: 0x02000192 RID: 402
	public class LoggingConfigurationReloadedEventArgs : EventArgs
	{
		// Token: 0x0600126E RID: 4718 RVA: 0x00031F68 File Offset: 0x00030168
		public LoggingConfigurationReloadedEventArgs(bool succeeded)
		{
			this.Succeeded = succeeded;
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x00031F77 File Offset: 0x00030177
		public LoggingConfigurationReloadedEventArgs(bool succeeded, Exception exception)
		{
			this.Succeeded = succeeded;
			this.Exception = exception;
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06001270 RID: 4720 RVA: 0x00031F8D File Offset: 0x0003018D
		// (set) Token: 0x06001271 RID: 4721 RVA: 0x00031F95 File Offset: 0x00030195
		public bool Succeeded { get; private set; }

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06001272 RID: 4722 RVA: 0x00031F9E File Offset: 0x0003019E
		// (set) Token: 0x06001273 RID: 4723 RVA: 0x00031FA6 File Offset: 0x000301A6
		public Exception Exception { get; private set; }
	}
}
