using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Microsoft.HostIntegration.PerformanceCounters
{
	// Token: 0x02000792 RID: 1938
	public class AverageTimeCounter : GenericPerformanceCounter
	{
		// Token: 0x06003E7E RID: 15998
		[DllImport("Kernel32.dll")]
		private static extern void QueryPerformanceCounter(ref long ticks);

		// Token: 0x06003E7F RID: 15999
		[DllImport("Kernel32.dll")]
		private static extern void QueryPerformanceFrequency(ref long frequency);

		// Token: 0x06003E80 RID: 16000 RVA: 0x000D1A6F File Offset: 0x000CFC6F
		static AverageTimeCounter()
		{
			AverageTimeCounter.QueryPerformanceFrequency(ref AverageTimeCounter.frequency);
			AverageTimeCounter.frequencyPerMilliseconds = (float)AverageTimeCounter.frequency / 1000f;
		}

		// Token: 0x06003E81 RID: 16001 RVA: 0x000D1A8C File Offset: 0x000CFC8C
		internal AverageTimeCounter(PerformanceCounterInformation performanceCounterInformation, string instanceName)
			: base(performanceCounterInformation, instanceName)
		{
			try
			{
				if (!GenericPerformanceCounter.FailedToGetCounters)
				{
					this.baseCounter = base.CreateCounter(performanceCounterInformation.CategoryName, performanceCounterInformation.CounterNameBase, instanceName, false);
					if (this.performanceCounterInformation.AutomaticallySupportCommonInstance)
					{
						this.commonBaseCounter = base.CreateCounter(performanceCounterInformation.CategoryName, performanceCounterInformation.CounterNameBase, null, true);
					}
				}
			}
			catch (Exception)
			{
				GenericPerformanceCounter.FailedToGetCounters = true;
			}
		}

		// Token: 0x06003E82 RID: 16002 RVA: 0x000D1B04 File Offset: 0x000CFD04
		public void StartOperation()
		{
			if (this.started)
			{
				throw new Exception("FATAL: StartOperation called again without an intermediate StopOperationAndReport");
			}
			AverageTimeCounter.QueryPerformanceCounter(ref this.startTime);
			this.started = true;
			this.ended = false;
		}

		// Token: 0x06003E83 RID: 16003 RVA: 0x000D1B34 File Offset: 0x000CFD34
		public void StopOperationAndReport()
		{
			if (!this.started)
			{
				throw new Exception("FATAL: StopOperationAndReport called without a Start");
			}
			AverageTimeCounter.QueryPerformanceCounter(ref this.endTime);
			this.started = false;
			this.ended = true;
			if (this.counter == null)
			{
				return;
			}
			this.counter.IncrementBy(this.endTime - this.startTime);
			this.baseCounter.Increment();
			if (this.commonCounter == null)
			{
				return;
			}
			this.commonCounter.IncrementBy(this.endTime - this.startTime);
			this.commonBaseCounter.Increment();
		}

		// Token: 0x17000EC4 RID: 3780
		// (get) Token: 0x06003E84 RID: 16004 RVA: 0x000D1BCC File Offset: 0x000CFDCC
		public float OperationDuration
		{
			get
			{
				if (!this.started && !this.ended)
				{
					throw new Exception("FATAL: OperationDuration called without a Start");
				}
				if (!this.ended)
				{
					AverageTimeCounter.QueryPerformanceCounter(ref this.endTime);
				}
				return (float)(this.endTime - this.startTime) / AverageTimeCounter.frequencyPerMilliseconds;
			}
		}

		// Token: 0x04002511 RID: 9489
		private long startTime;

		// Token: 0x04002512 RID: 9490
		private static long frequency;

		// Token: 0x04002513 RID: 9491
		private static float frequencyPerMilliseconds;

		// Token: 0x04002514 RID: 9492
		private bool started;

		// Token: 0x04002515 RID: 9493
		private bool ended;

		// Token: 0x04002516 RID: 9494
		private long endTime;

		// Token: 0x04002517 RID: 9495
		private PerformanceCounter baseCounter;

		// Token: 0x04002518 RID: 9496
		private PerformanceCounter commonBaseCounter;
	}
}
