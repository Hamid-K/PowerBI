using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001594 RID: 5524
	public abstract class NumberComparer : IComparer<NumberValue>, IEqualityComparer<NumberValue>
	{
		// Token: 0x06008A03 RID: 35331
		public abstract int Compare(NumberValue x, NumberValue y);

		// Token: 0x06008A04 RID: 35332
		public abstract bool Equals(NumberValue x, NumberValue y);

		// Token: 0x06008A05 RID: 35333
		public abstract int GetHashCode(NumberValue obj);

		// Token: 0x04004C01 RID: 19457
		public static readonly NumberComparer Double = new NumberComparer.DoubleNumberComparer();

		// Token: 0x04004C02 RID: 19458
		public static readonly NumberComparer Decimal = new NumberComparer.DecimalNumberComparer();

		// Token: 0x02001595 RID: 5525
		private class DoubleNumberComparer : NumberComparer
		{
			// Token: 0x06008A08 RID: 35336 RVA: 0x001D19D0 File Offset: 0x001CFBD0
			public override int Compare(NumberValue x, NumberValue y)
			{
				return x.AsDouble.CompareTo(y.AsDouble);
			}

			// Token: 0x06008A09 RID: 35337 RVA: 0x001D19F1 File Offset: 0x001CFBF1
			public override bool Equals(NumberValue x, NumberValue y)
			{
				return x.AsDouble == y.AsDouble;
			}

			// Token: 0x06008A0A RID: 35338 RVA: 0x001D1A04 File Offset: 0x001CFC04
			public override int GetHashCode(NumberValue value)
			{
				return value.AsDouble.GetHashCode();
			}
		}

		// Token: 0x02001596 RID: 5526
		private class DecimalNumberComparer : NumberComparer
		{
			// Token: 0x06008A0C RID: 35340 RVA: 0x001D1A28 File Offset: 0x001CFC28
			public override int Compare(NumberValue x, NumberValue y)
			{
				int num;
				try
				{
					num = x.AsDecimal.CompareTo(y.AsDecimal);
				}
				catch (OverflowException)
				{
					num = x.AsDouble.CompareTo(y.AsDouble);
				}
				return num;
			}

			// Token: 0x06008A0D RID: 35341 RVA: 0x001D1A78 File Offset: 0x001CFC78
			public override bool Equals(NumberValue x, NumberValue y)
			{
				bool flag;
				try
				{
					flag = x.AsDecimal == y.AsDecimal;
				}
				catch (OverflowException)
				{
					flag = x.AsDouble == y.AsDouble;
				}
				return flag;
			}

			// Token: 0x06008A0E RID: 35342 RVA: 0x001D1ABC File Offset: 0x001CFCBC
			public override int GetHashCode(NumberValue value)
			{
				int num;
				try
				{
					num = value.AsDecimal.GetHashCode();
				}
				catch (OverflowException)
				{
					num = value.AsDouble.GetHashCode();
				}
				return num;
			}
		}
	}
}
