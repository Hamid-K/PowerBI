using System;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000B1 RID: 177
	internal sealed class AverageDurationCounterMonitor : IDisposable
	{
		// Token: 0x060005A6 RID: 1446 RVA: 0x000110AB File Offset: 0x0000F2AB
		public AverageDurationCounterMonitor(ICounter counter)
		{
			this.m_counter = counter;
			this.m_start = DateTime.UtcNow;
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x000110C8 File Offset: 0x0000F2C8
		void IDisposable.Dispose()
		{
			if (this.m_counter != null)
			{
				long num = (long)(DateTime.UtcNow - this.m_start).TotalMilliseconds;
				this.m_counter.IncrementBy(num);
			}
		}

		// Token: 0x04000329 RID: 809
		private ICounter m_counter;

		// Token: 0x0400032A RID: 810
		private DateTime m_start;
	}
}
