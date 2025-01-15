using System;
using System.Runtime.CompilerServices;

namespace Microsoft.InfoNav.Analytics.TimeSeriesPreProcessing
{
	// Token: 0x0200000A RID: 10
	internal class ForecastMinMaxStepDetector
	{
		// Token: 0x0600001E RID: 30 RVA: 0x00003010 File Offset: 0x00001210
		internal ForecastMinMaxStepDetector()
		{
			this.m_xnumMinNonZeroValue = 0.0;
			this.m_xnumMaxValue = 0.0;
			this.m_cFoundZeroValues = 0U;
			this.m_fUnsorted = false;
			this.Reset();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000304A File Offset: 0x0000124A
		internal void Reset()
		{
			this.m_xnumMinNonZeroValue = double.MaxValue;
			this.m_xnumMaxValue = 0.0;
			this.m_cFoundZeroValues = 0U;
			this.m_fUnsorted = false;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00003078 File Offset: 0x00001278
		internal bool FEmpty()
		{
			return this.m_xnumMinNonZeroValue == double.MaxValue && this.m_cFoundZeroValues == 0U;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00003096 File Offset: 0x00001296
		internal bool FNotSorted()
		{
			return this.m_fUnsorted;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000030A0 File Offset: 0x000012A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void UpdateLimitsFromValues(double pxnumfirst, double pxnumSecond)
		{
			double num = pxnumSecond - pxnumfirst;
			if (num < this.m_xnumMinNonZeroValue)
			{
				if (num <= 0.0)
				{
					this.m_cFoundZeroValues += 1U;
					if (num < 0.0)
					{
						this.m_fUnsorted = true;
					}
				}
				else
				{
					this.m_xnumMinNonZeroValue = num;
				}
			}
			if (num > this.m_xnumMaxValue)
			{
				this.m_xnumMaxValue = num;
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00003100 File Offset: 0x00001300
		internal double GetMinValue(bool fIgnoreZeroes)
		{
			if (this.m_cFoundZeroValues <= 0U || fIgnoreZeroes)
			{
				return this.m_xnumMinNonZeroValue;
			}
			return 0.0;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000311E File Offset: 0x0000131E
		internal double GetMaxValue()
		{
			return this.m_xnumMaxValue;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00003126 File Offset: 0x00001326
		internal bool FSingleValue()
		{
			return this.m_xnumMaxValue == this.m_xnumMinNonZeroValue || (this.m_cFoundZeroValues > 0U && this.m_xnumMaxValue == 0.0);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00003154 File Offset: 0x00001354
		internal uint CGetFoundZeroes()
		{
			return this.m_cFoundZeroValues;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000315C File Offset: 0x0000135C
		internal void MinMaxCopy(ForecastMinMaxStepDetector pfmmsdOther)
		{
			if (pfmmsdOther != null)
			{
				this.m_xnumMinNonZeroValue = pfmmsdOther.m_xnumMinNonZeroValue;
				this.m_xnumMaxValue = pfmmsdOther.m_xnumMaxValue;
				this.m_cFoundZeroValues = pfmmsdOther.m_cFoundZeroValues;
				this.m_fUnsorted = pfmmsdOther.m_fUnsorted;
				return;
			}
			this.Reset();
		}

		// Token: 0x04000072 RID: 114
		private const double XNUM_RESET_MAX_VALUE = 1.7976931348623157E+308;

		// Token: 0x04000073 RID: 115
		protected double m_xnumMinNonZeroValue;

		// Token: 0x04000074 RID: 116
		protected double m_xnumMaxValue;

		// Token: 0x04000075 RID: 117
		protected uint m_cFoundZeroValues;

		// Token: 0x04000076 RID: 118
		protected bool m_fUnsorted;
	}
}
