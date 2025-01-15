using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020015E0 RID: 5600
	internal class RangeListValue : BufferedListValue
	{
		// Token: 0x06008CCA RID: 36042 RVA: 0x001D8375 File Offset: 0x001D6575
		public RangeListValue(int lower, int count)
		{
			this.lower = lower;
			this.count = Math.Max(0, count);
		}

		// Token: 0x170024E7 RID: 9447
		// (get) Token: 0x06008CCB RID: 36043 RVA: 0x001D8391 File Offset: 0x001D6591
		public override int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x170024E8 RID: 9448
		public override Value this[int index]
		{
			get
			{
				if (index >= 0 && index < this.count)
				{
					return NumberValue.New((long)this.lower + (long)index);
				}
				return base[index];
			}
		}

		// Token: 0x06008CCD RID: 36045 RVA: 0x001AC6DA File Offset: 0x001AA8DA
		public override IValueReference GetReference(int index)
		{
			return this[index];
		}

		// Token: 0x04004CC8 RID: 19656
		private int lower;

		// Token: 0x04004CC9 RID: 19657
		private int count;
	}
}
