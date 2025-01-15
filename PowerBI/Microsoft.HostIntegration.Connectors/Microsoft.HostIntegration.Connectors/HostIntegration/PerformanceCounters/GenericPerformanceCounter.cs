using System;
using System.Diagnostics;

namespace Microsoft.HostIntegration.PerformanceCounters
{
	// Token: 0x0200078E RID: 1934
	public class GenericPerformanceCounter : IDisposable
	{
		// Token: 0x06003E73 RID: 15987 RVA: 0x000D18F4 File Offset: 0x000CFAF4
		internal GenericPerformanceCounter(PerformanceCounterInformation performanceCounterInformation, string instanceName)
		{
			this.performanceCounterInformation = performanceCounterInformation;
			try
			{
				if (!GenericPerformanceCounter.FailedToGetCounters)
				{
					this.counter = this.CreateCounter(performanceCounterInformation.CategoryName, performanceCounterInformation.CounterName, instanceName, false);
					if (this.performanceCounterInformation.AutomaticallySupportCommonInstance)
					{
						this.commonCounter = this.CreateCounter(performanceCounterInformation.CategoryName, performanceCounterInformation.CounterName, null, true);
					}
				}
			}
			catch (Exception)
			{
				GenericPerformanceCounter.FailedToGetCounters = true;
			}
		}

		// Token: 0x06003E74 RID: 15988 RVA: 0x000D1974 File Offset: 0x000CFB74
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003E75 RID: 15989 RVA: 0x000036A9 File Offset: 0x000018A9
		private void Dispose(bool disposing)
		{
		}

		// Token: 0x06003E76 RID: 15990 RVA: 0x000D1984 File Offset: 0x000CFB84
		protected PerformanceCounter CreateCounter(string category, string name, string instance, bool common)
		{
			PerformanceCounter performanceCounter = new PerformanceCounter();
			performanceCounter.CategoryName = category;
			performanceCounter.CounterName = name;
			performanceCounter.InstanceName = (common ? "Common" : instance);
			performanceCounter.ReadOnly = false;
			performanceCounter.InstanceLifetime = (common ? PerformanceCounterInstanceLifetime.Global : PerformanceCounterInstanceLifetime.Process);
			performanceCounter.Increment();
			performanceCounter.Decrement();
			return performanceCounter;
		}

		// Token: 0x0400250D RID: 9485
		internal static bool FailedToGetCounters;

		// Token: 0x0400250E RID: 9486
		internal PerformanceCounterInformation performanceCounterInformation;

		// Token: 0x0400250F RID: 9487
		internal PerformanceCounter counter;

		// Token: 0x04002510 RID: 9488
		internal PerformanceCounter commonCounter;
	}
}
