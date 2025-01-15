using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002BF RID: 703
	public class TimePeriodControl : IDisposable
	{
		// Token: 0x060012E7 RID: 4839 RVA: 0x000418F4 File Offset: 0x0003FAF4
		public TimePeriodControl(int period)
		{
			ExtendedDiagnostics.EnsureArgumentIsNotNegative(period, "period");
			this.m_period = (uint)period;
			uint num = TimePeriodControl.NativeMethods.timeBeginPeriod(this.m_period);
			if (num != 0U)
			{
				throw new IOException("Failed to change the timer resolution", Convert.ToInt32(num));
			}
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x0004193C File Offset: 0x0003FB3C
		~TimePeriodControl()
		{
			this.Dispose(false);
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x0004196C File Offset: 0x0003FB6C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x0004197B File Offset: 0x0003FB7B
		protected virtual void Dispose(bool disposing)
		{
			TimePeriodControl.NativeMethods.timeEndPeriod(this.m_period);
		}

		// Token: 0x0400070D RID: 1805
		private uint m_period;

		// Token: 0x0200077E RID: 1918
		private static class NativeMethods
		{
			// Token: 0x06003085 RID: 12421
			[DllImport("winmm.dll")]
			internal static extern uint timeBeginPeriod(uint period);

			// Token: 0x06003086 RID: 12422
			[DllImport("winmm.dll")]
			internal static extern uint timeEndPeriod(uint period);

			// Token: 0x04001630 RID: 5680
			internal const uint TIMERR_NOERROR = 0U;
		}
	}
}
