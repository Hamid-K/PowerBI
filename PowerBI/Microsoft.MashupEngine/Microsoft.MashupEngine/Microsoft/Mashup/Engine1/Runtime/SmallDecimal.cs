using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001618 RID: 5656
	internal struct SmallDecimal
	{
		// Token: 0x06008E17 RID: 36375 RVA: 0x001DAF46 File Offset: 0x001D9146
		public SmallDecimal(int numerator)
		{
			this = new SmallDecimal(numerator, 1);
		}

		// Token: 0x06008E18 RID: 36376 RVA: 0x001DAF50 File Offset: 0x001D9150
		public SmallDecimal(int numerator, int denominator)
		{
			this.numerator = numerator;
			this.denominator = denominator;
		}

		// Token: 0x1700254B RID: 9547
		// (get) Token: 0x06008E19 RID: 36377 RVA: 0x001DAF60 File Offset: 0x001D9160
		public int Numerator
		{
			get
			{
				return this.numerator;
			}
		}

		// Token: 0x1700254C RID: 9548
		// (get) Token: 0x06008E1A RID: 36378 RVA: 0x001DAF68 File Offset: 0x001D9168
		public int Denominator
		{
			get
			{
				return this.denominator;
			}
		}

		// Token: 0x04004D4F RID: 19791
		private int numerator;

		// Token: 0x04004D50 RID: 19792
		private int denominator;
	}
}
