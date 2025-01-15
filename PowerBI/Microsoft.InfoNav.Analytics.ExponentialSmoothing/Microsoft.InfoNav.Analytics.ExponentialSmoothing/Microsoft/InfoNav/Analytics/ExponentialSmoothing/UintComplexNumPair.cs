using System;

namespace Microsoft.InfoNav.Analytics.ExponentialSmoothing
{
	// Token: 0x0200000F RID: 15
	internal sealed class UintComplexNumPair
	{
		// Token: 0x06000067 RID: 103 RVA: 0x00004E3A File Offset: 0x0000303A
		internal UintComplexNumPair(uint first, double second)
		{
			this.m_first = first;
			this.m_second = second;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00004E50 File Offset: 0x00003050
		internal uint First
		{
			get
			{
				return this.m_first;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00004E58 File Offset: 0x00003058
		internal double Second
		{
			get
			{
				return this.m_second;
			}
		}

		// Token: 0x0400006A RID: 106
		private readonly uint m_first;

		// Token: 0x0400006B RID: 107
		private readonly double m_second;
	}
}
