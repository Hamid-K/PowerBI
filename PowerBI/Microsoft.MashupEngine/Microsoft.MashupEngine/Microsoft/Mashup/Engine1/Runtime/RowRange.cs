using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001609 RID: 5641
	public struct RowRange
	{
		// Token: 0x1700253C RID: 9532
		// (get) Token: 0x06008DDC RID: 36316 RVA: 0x001DA596 File Offset: 0x001D8796
		public static RowRange All
		{
			get
			{
				return new RowRange(RowCount.Zero, RowCount.Infinite);
			}
		}

		// Token: 0x1700253D RID: 9533
		// (get) Token: 0x06008DDD RID: 36317 RVA: 0x001DA5A7 File Offset: 0x001D87A7
		public static RowRange None
		{
			get
			{
				return new RowRange(RowCount.Zero, RowCount.Zero);
			}
		}

		// Token: 0x06008DDE RID: 36318 RVA: 0x001DA5B8 File Offset: 0x001D87B8
		private RowRange(RowCount skip, RowCount take)
		{
			this.skip = skip;
			this.take = take;
		}

		// Token: 0x1700253E RID: 9534
		// (get) Token: 0x06008DDF RID: 36319 RVA: 0x001DA5C8 File Offset: 0x001D87C8
		public RowCount SkipCount
		{
			get
			{
				return this.skip;
			}
		}

		// Token: 0x1700253F RID: 9535
		// (get) Token: 0x06008DE0 RID: 36320 RVA: 0x001DA5D0 File Offset: 0x001D87D0
		public RowCount TakeCount
		{
			get
			{
				return this.take;
			}
		}

		// Token: 0x17002540 RID: 9536
		// (get) Token: 0x06008DE1 RID: 36321 RVA: 0x001DA5D8 File Offset: 0x001D87D8
		public bool IsAll
		{
			get
			{
				return this.skip.IsZero && this.take.IsInfinite;
			}
		}

		// Token: 0x17002541 RID: 9537
		// (get) Token: 0x06008DE2 RID: 36322 RVA: 0x001DA5F4 File Offset: 0x001D87F4
		public bool IsNone
		{
			get
			{
				return this.skip.IsInfinite || this.take.IsZero;
			}
		}

		// Token: 0x06008DE3 RID: 36323 RVA: 0x001DA610 File Offset: 0x001D8810
		public RowRange Skip(RowCount count)
		{
			if (count.IsInfinite)
			{
				return RowRange.None;
			}
			if (this.skip.IsInfinite)
			{
				return RowRange.None;
			}
			if (this.skip.Value > RowCount.MaxValue - count.Value)
			{
				throw ValueException.SkipCountTooLarge((double)this.skip.Value + (double)count.Value);
			}
			RowCount rowCount = new RowCount(this.skip.Value + count.Value);
			if (this.take.IsInfinite)
			{
				return new RowRange(rowCount, this.take);
			}
			if (count.Value > this.take.Value)
			{
				return RowRange.None;
			}
			RowCount rowCount2 = new RowCount(this.take.Value - count.Value);
			return new RowRange(rowCount, rowCount2);
		}

		// Token: 0x06008DE4 RID: 36324 RVA: 0x001DA6E4 File Offset: 0x001D88E4
		public RowRange Take(RowCount count)
		{
			if (count.IsInfinite)
			{
				return new RowRange(this.skip, this.take);
			}
			if (this.take.IsInfinite)
			{
				return new RowRange(this.skip, count);
			}
			return new RowRange(this.skip, new RowCount(Math.Min(count.Value, this.take.Value)));
		}

		// Token: 0x04004D30 RID: 19760
		private RowCount skip;

		// Token: 0x04004D31 RID: 19761
		private RowCount take;
	}
}
