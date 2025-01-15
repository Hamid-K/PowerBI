using System;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing
{
	// Token: 0x02000093 RID: 147
	internal class DiagnoisticsEventCounters
	{
		// Token: 0x060004AF RID: 1199 RVA: 0x000142C1 File Offset: 0x000124C1
		internal DiagnoisticsEventCounters(int execCountInitial = 0)
		{
			this.execCount = execCountInitial;
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x000142DD File Offset: 0x000124DD
		internal int ExecCount
		{
			get
			{
				return this.execCount;
			}
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x000142E7 File Offset: 0x000124E7
		internal int Increment()
		{
			this.syncRoot.ExecuteSpinWaitLock(delegate
			{
				if (2147483647 > this.execCount)
				{
					this.execCount++;
				}
			});
			return this.execCount;
		}

		// Token: 0x040001D1 RID: 465
		private readonly object syncRoot = new object();

		// Token: 0x040001D2 RID: 466
		private volatile int execCount;
	}
}
