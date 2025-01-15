using System;

namespace Microsoft.Cloud.Platform.Monitoring
{
	// Token: 0x0200008F RID: 143
	internal class WindowsEventLogIntervalConfig
	{
		// Token: 0x0600040B RID: 1035 RVA: 0x0000EBA8 File Offset: 0x0000CDA8
		public WindowsEventLogIntervalConfig(int windowsEventLogWriteInterval)
		{
			this.WindowsEventLogWriteInterval = windowsEventLogWriteInterval;
			this.Counter = 0;
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000EBC0 File Offset: 0x0000CDC0
		public void Tick()
		{
			int counter = this.Counter;
			this.Counter = counter + 1;
			if (this.Counter % this.WindowsEventLogWriteInterval == 0)
			{
				this.Counter = 0;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x0000EBF3 File Offset: 0x0000CDF3
		// (set) Token: 0x0600040E RID: 1038 RVA: 0x0000EBFB File Offset: 0x0000CDFB
		public int WindowsEventLogWriteInterval { get; private set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x0000EC04 File Offset: 0x0000CE04
		// (set) Token: 0x06000410 RID: 1040 RVA: 0x0000EC0C File Offset: 0x0000CE0C
		public int Counter { get; private set; }
	}
}
