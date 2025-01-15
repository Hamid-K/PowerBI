using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001F8 RID: 504
	public class CrashEventArgs : EventArgs
	{
		// Token: 0x06000D65 RID: 3429 RVA: 0x0002EECA File Offset: 0x0002D0CA
		public CrashEventArgs(CrashScenario scenario, Exception exception)
		{
			this.CrashScenario = scenario;
			this.ExceptionObject = exception;
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000D66 RID: 3430 RVA: 0x0002EEE0 File Offset: 0x0002D0E0
		// (set) Token: 0x06000D67 RID: 3431 RVA: 0x0002EEE8 File Offset: 0x0002D0E8
		public CrashScenario CrashScenario { get; private set; }

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000D68 RID: 3432 RVA: 0x0002EEF1 File Offset: 0x0002D0F1
		// (set) Token: 0x06000D69 RID: 3433 RVA: 0x0002EEF9 File Offset: 0x0002D0F9
		public Exception ExceptionObject { get; private set; }
	}
}
