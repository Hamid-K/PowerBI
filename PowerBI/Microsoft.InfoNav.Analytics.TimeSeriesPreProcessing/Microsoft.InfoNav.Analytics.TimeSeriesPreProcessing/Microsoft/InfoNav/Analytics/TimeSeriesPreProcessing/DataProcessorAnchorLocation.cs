using System;
using System.Runtime.CompilerServices;

namespace Microsoft.InfoNav.Analytics.TimeSeriesPreProcessing
{
	// Token: 0x02000005 RID: 5
	internal sealed class DataProcessorAnchorLocation
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002068 File Offset: 0x00000268
		internal DataProcessorAnchorLocation(double pxnumTimeBase, double pxnumTimeLimit)
		{
			this.m_xnumTimeBase = pxnumTimeBase;
			this.m_xnumTimeLimit = pxnumTimeLimit;
			this.m_cMaxfoundAnchorSize = 0U;
			this.m_prgxnumTimelineLocs = new int[21];
			this.m_prgxnumTimelineValues = new double[21];
			int num = 0;
			while ((long)num < 21L)
			{
				this.m_prgxnumTimelineLocs[num] = -1;
				this.m_prgxnumTimelineValues[num] = 0.0;
				num++;
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D3 File Offset: 0x000002D3
		internal bool IsEmpty()
		{
			return this.m_cMaxfoundAnchorSize == 0U;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020DE File Offset: 0x000002DE
		internal void StoreAnchorCandidate(uint cAnchorSize, uint pxnumTimelineLoc, double pxnumTimelineValue)
		{
			if (cAnchorSize > 20U)
			{
				cAnchorSize = 20U;
			}
			if (this.m_prgxnumTimelineLocs[(int)cAnchorSize] == -1)
			{
				this.m_prgxnumTimelineLocs[(int)cAnchorSize] = (int)pxnumTimelineLoc;
				this.m_prgxnumTimelineValues[(int)cAnchorSize] = pxnumTimelineValue;
			}
			if (cAnchorSize > this.m_cMaxfoundAnchorSize)
			{
				this.m_cMaxfoundAnchorSize = cAnchorSize;
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002116 File Offset: 0x00000316
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal bool ShouldStore(uint cAnchorSize)
		{
			return cAnchorSize > this.m_cMaxfoundAnchorSize;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002124 File Offset: 0x00000324
		internal int PxnumGetBestAnchorTimelineHighEnd()
		{
			double num = 0.0;
			int num2 = -1;
			if (this.IsEmpty())
			{
				return -1;
			}
			double num3 = this.m_xnumTimeLimit - this.m_xnumTimeBase;
			for (uint num4 = this.m_cMaxfoundAnchorSize; num4 >= 2U; num4 -= 1U)
			{
				if (this.m_prgxnumTimelineLocs[(int)num4] != -1)
				{
					double num5 = (this.m_xnumTimeLimit - this.m_prgxnumTimelineValues[(int)num4]) / num3;
					double num6 = num4 * (1.0 - num5);
					if (num6 > num)
					{
						num = num6;
						num2 = this.m_prgxnumTimelineLocs[(int)num4];
					}
				}
			}
			return num2;
		}

		// Token: 0x04000029 RID: 41
		private readonly double m_xnumTimeBase;

		// Token: 0x0400002A RID: 42
		private readonly double m_xnumTimeLimit;

		// Token: 0x0400002B RID: 43
		private uint m_cMaxfoundAnchorSize;

		// Token: 0x0400002C RID: 44
		private readonly int[] m_prgxnumTimelineLocs;

		// Token: 0x0400002D RID: 45
		private readonly double[] m_prgxnumTimelineValues;
	}
}
