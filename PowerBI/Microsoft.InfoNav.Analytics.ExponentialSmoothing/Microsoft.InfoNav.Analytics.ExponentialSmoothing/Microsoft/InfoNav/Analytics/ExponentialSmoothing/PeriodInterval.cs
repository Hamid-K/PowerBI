using System;

namespace Microsoft.InfoNav.Analytics.ExponentialSmoothing
{
	// Token: 0x0200000C RID: 12
	internal sealed class PeriodInterval
	{
		// Token: 0x0600004E RID: 78 RVA: 0x00004966 File Offset: 0x00002B66
		internal PeriodInterval(uint period, uint min, uint max)
		{
			this.m_period = period;
			this.m_min = min;
			this.m_max = max;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00004983 File Offset: 0x00002B83
		// (set) Token: 0x06000050 RID: 80 RVA: 0x0000498B File Offset: 0x00002B8B
		internal uint Period
		{
			get
			{
				return this.m_period;
			}
			set
			{
				this.m_period = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00004994 File Offset: 0x00002B94
		// (set) Token: 0x06000052 RID: 82 RVA: 0x0000499C File Offset: 0x00002B9C
		internal uint Min
		{
			get
			{
				return this.m_min;
			}
			set
			{
				this.m_min = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000053 RID: 83 RVA: 0x000049A5 File Offset: 0x00002BA5
		// (set) Token: 0x06000054 RID: 84 RVA: 0x000049AD File Offset: 0x00002BAD
		internal uint Max
		{
			get
			{
				return this.m_max;
			}
			set
			{
				this.m_max = value;
			}
		}

		// Token: 0x0400005D RID: 93
		private uint m_period;

		// Token: 0x0400005E RID: 94
		private uint m_min;

		// Token: 0x0400005F RID: 95
		private uint m_max;
	}
}
