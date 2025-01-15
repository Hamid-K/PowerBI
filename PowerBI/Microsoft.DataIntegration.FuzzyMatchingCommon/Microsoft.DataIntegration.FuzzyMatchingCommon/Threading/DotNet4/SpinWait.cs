using System;
using System.Runtime.ConstrainedExecution;
using System.Security.Permissions;
using System.Threading;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Threading.Internal.DotNet4;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Threading.DotNet4
{
	// Token: 0x02000029 RID: 41
	[ReliabilityContract(3, 2)]
	[HostProtection(6, Synchronization = true, ExternalThreading = true)]
	internal struct SpinWait
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000EC RID: 236 RVA: 0x0000FEDD File Offset: 0x0000E0DD
		public int Count
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000ED RID: 237 RVA: 0x0000FEE5 File Offset: 0x0000E0E5
		public bool NextSpinWillYield
		{
			get
			{
				return Platform.IsSingleProcessor || this.m_count >= 85;
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000FF00 File Offset: 0x0000E100
		public void SpinOnce()
		{
			if (this.NextSpinWillYield)
			{
				int num = ((this.m_count >= 85) ? (this.m_count - 85) : this.m_count);
				if (num % 10 == 4)
				{
					Thread.Sleep(0);
				}
				else if (num % 10 == 9)
				{
					Thread.Sleep(1);
				}
				else
				{
					Platform.Yield();
				}
			}
			else
			{
				Thread.SpinWait((int)((float)this.m_count * 2.4705882f) + 1);
			}
			this.m_count = ((this.m_count == int.MaxValue) ? 85 : (this.m_count + 1));
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000FF8B File Offset: 0x0000E18B
		public void Reset()
		{
			this.m_count = 0;
		}

		// Token: 0x04000026 RID: 38
		internal const int YIELD_THRESHOLD = 85;

		// Token: 0x04000027 RID: 39
		internal const int SLEEP_0_EVERY_HOW_MANY_TIMES = 5;

		// Token: 0x04000028 RID: 40
		internal const int SLEEP_1_EVERY_HOW_MANY_TIMES = 10;

		// Token: 0x04000029 RID: 41
		internal const int MAX_SPIN_INTERVAL = 210;

		// Token: 0x0400002A RID: 42
		private int m_count;
	}
}
