using System;
using System.Globalization;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001608 RID: 5640
	public struct RowCount
	{
		// Token: 0x17002534 RID: 9524
		// (get) Token: 0x06008DCD RID: 36301 RVA: 0x001DA447 File Offset: 0x001D8647
		public static RowCount Zero
		{
			get
			{
				return new RowCount(0L);
			}
		}

		// Token: 0x17002535 RID: 9525
		// (get) Token: 0x06008DCE RID: 36302 RVA: 0x001DA450 File Offset: 0x001D8650
		public static RowCount One
		{
			get
			{
				return new RowCount(1L);
			}
		}

		// Token: 0x17002536 RID: 9526
		// (get) Token: 0x06008DCF RID: 36303 RVA: 0x001DA459 File Offset: 0x001D8659
		public static RowCount Two
		{
			get
			{
				return new RowCount(2L);
			}
		}

		// Token: 0x17002537 RID: 9527
		// (get) Token: 0x06008DD0 RID: 36304 RVA: 0x001DA462 File Offset: 0x001D8662
		public static RowCount Infinite
		{
			get
			{
				return new RowCount(long.MaxValue);
			}
		}

		// Token: 0x17002538 RID: 9528
		// (get) Token: 0x06008DD1 RID: 36305 RVA: 0x001CEC19 File Offset: 0x001CCE19
		public static long MaxValue
		{
			get
			{
				return 4503599627370496L;
			}
		}

		// Token: 0x06008DD2 RID: 36306 RVA: 0x001DA472 File Offset: 0x001D8672
		public RowCount(long value)
		{
			this.value = value;
		}

		// Token: 0x17002539 RID: 9529
		// (get) Token: 0x06008DD3 RID: 36307 RVA: 0x001DA47B File Offset: 0x001D867B
		public long Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x1700253A RID: 9530
		// (get) Token: 0x06008DD4 RID: 36308 RVA: 0x001DA483 File Offset: 0x001D8683
		public bool IsInfinite
		{
			get
			{
				return this.value == long.MaxValue;
			}
		}

		// Token: 0x1700253B RID: 9531
		// (get) Token: 0x06008DD5 RID: 36309 RVA: 0x001DA496 File Offset: 0x001D8696
		public bool IsZero
		{
			get
			{
				return this.value == 0L;
			}
		}

		// Token: 0x06008DD6 RID: 36310 RVA: 0x001DA4A2 File Offset: 0x001D86A2
		public static RowCount operator +(RowCount rowCount1, RowCount rowCount2)
		{
			if (rowCount1.IsInfinite || rowCount2.IsZero)
			{
				return rowCount1;
			}
			if (rowCount2.IsInfinite || rowCount1.IsZero)
			{
				return rowCount2;
			}
			return new RowCount(rowCount1.value + rowCount2.value);
		}

		// Token: 0x06008DD7 RID: 36311 RVA: 0x001DA4DE File Offset: 0x001D86DE
		public static RowCount operator --(RowCount rowCount)
		{
			if (rowCount.IsInfinite)
			{
				return rowCount;
			}
			return new RowCount(rowCount.value - 1L);
		}

		// Token: 0x06008DD8 RID: 36312 RVA: 0x001DA4F9 File Offset: 0x001D86F9
		public static RowCount operator ++(RowCount rowCount)
		{
			if (rowCount.IsInfinite)
			{
				return rowCount;
			}
			return new RowCount(rowCount.value + 1L);
		}

		// Token: 0x06008DD9 RID: 36313 RVA: 0x001DA514 File Offset: 0x001D8714
		public static bool operator <(RowCount rowCount1, RowCount rowCount2)
		{
			if (rowCount2.IsInfinite)
			{
				return !rowCount1.IsInfinite;
			}
			return !rowCount1.IsInfinite && rowCount1.Value < rowCount2.Value;
		}

		// Token: 0x06008DDA RID: 36314 RVA: 0x001DA545 File Offset: 0x001D8745
		public static bool operator >(RowCount rowCount1, RowCount rowCount2)
		{
			if (rowCount1.IsInfinite)
			{
				return !rowCount2.IsInfinite;
			}
			return !rowCount2.IsInfinite && rowCount1.Value > rowCount2.Value;
		}

		// Token: 0x06008DDB RID: 36315 RVA: 0x001DA576 File Offset: 0x001D8776
		public override string ToString()
		{
			if (this.IsInfinite)
			{
				return "Infinite";
			}
			return this.value.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x04004D2F RID: 19759
		private long value;
	}
}
