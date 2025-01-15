using System;

namespace Microsoft.InfoNav.Analytics.ExponentialSmoothing
{
	// Token: 0x0200000D RID: 13
	internal sealed class PeriodPower
	{
		// Token: 0x06000055 RID: 85 RVA: 0x000049B6 File Offset: 0x00002BB6
		internal PeriodPower(uint period, double spectrumPower, double corr)
		{
			this.m_period = period;
			this.m_spectrumPower = spectrumPower;
			this.m_correlation = corr;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000056 RID: 86 RVA: 0x000049D3 File Offset: 0x00002BD3
		internal uint Period
		{
			get
			{
				return this.m_period;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000057 RID: 87 RVA: 0x000049DB File Offset: 0x00002BDB
		// (set) Token: 0x06000058 RID: 88 RVA: 0x000049E3 File Offset: 0x00002BE3
		internal double SpectrumPower
		{
			get
			{
				return this.m_spectrumPower;
			}
			set
			{
				this.m_spectrumPower = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000059 RID: 89 RVA: 0x000049EC File Offset: 0x00002BEC
		// (set) Token: 0x0600005A RID: 90 RVA: 0x000049F4 File Offset: 0x00002BF4
		internal double Correlation
		{
			get
			{
				return this.m_correlation;
			}
			set
			{
				this.m_correlation = value;
			}
		}

		// Token: 0x04000060 RID: 96
		private uint m_period;

		// Token: 0x04000061 RID: 97
		private double m_spectrumPower;

		// Token: 0x04000062 RID: 98
		private double m_correlation;
	}
}
