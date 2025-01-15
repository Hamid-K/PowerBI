using System;
using System.Globalization;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200159C RID: 5532
	public class DoubleNumberValue : NumberValue
	{
		// Token: 0x06008AAE RID: 35502 RVA: 0x001D33A8 File Offset: 0x001D15A8
		public DoubleNumberValue(double value)
		{
			this.value = value;
		}

		// Token: 0x17002496 RID: 9366
		// (get) Token: 0x06008AAF RID: 35503 RVA: 0x001D33B7 File Offset: 0x001D15B7
		public double Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17002497 RID: 9367
		// (get) Token: 0x06008AB0 RID: 35504 RVA: 0x00002139 File Offset: 0x00000339
		public override NumberKind NumberKind
		{
			get
			{
				return NumberKind.Double;
			}
		}

		// Token: 0x06008AB1 RID: 35505 RVA: 0x001D33BF File Offset: 0x001D15BF
		public override Value Add(Value value, Precision precision)
		{
			return value.AddR(this, precision);
		}

		// Token: 0x06008AB2 RID: 35506 RVA: 0x001D33C9 File Offset: 0x001D15C9
		public override Value AddR(Int32NumberValue value, Precision precision)
		{
			return precision.Add((double)value.Value, this.value);
		}

		// Token: 0x06008AB3 RID: 35507 RVA: 0x001D33DE File Offset: 0x001D15DE
		public override Value AddR(DoubleNumberValue value, Precision precision)
		{
			return precision.Add(value.value, this.value);
		}

		// Token: 0x06008AB4 RID: 35508 RVA: 0x001D33F2 File Offset: 0x001D15F2
		public override Value Subtract(Value value, Precision precision)
		{
			return value.SubtractR(this, precision);
		}

		// Token: 0x06008AB5 RID: 35509 RVA: 0x001D33FC File Offset: 0x001D15FC
		public override Value SubtractR(Int32NumberValue value, Precision precision)
		{
			return precision.Subtract((double)value.Value, this.value);
		}

		// Token: 0x06008AB6 RID: 35510 RVA: 0x001D3411 File Offset: 0x001D1611
		public override Value SubtractR(DoubleNumberValue value, Precision precision)
		{
			return precision.Subtract(value.Value, this.value);
		}

		// Token: 0x06008AB7 RID: 35511 RVA: 0x001D33B7 File Offset: 0x001D15B7
		public override double ToDouble()
		{
			return this.value;
		}

		// Token: 0x06008AB8 RID: 35512 RVA: 0x001D3428 File Offset: 0x001D1628
		public override decimal ToDecimal()
		{
			decimal num;
			if (this.TryGetDecimal(out num))
			{
				return num;
			}
			throw ValueException.NumberOutOfRange<Message0>(Strings.NumberOutOfRangeDecimal, this, null);
		}

		// Token: 0x06008AB9 RID: 35513 RVA: 0x001D344D File Offset: 0x001D164D
		public override object ToObject()
		{
			return this.value;
		}

		// Token: 0x06008ABA RID: 35514 RVA: 0x001D345C File Offset: 0x001D165C
		public override bool TryGetInt64(out long value)
		{
			if (!double.IsNaN(this.value) && !double.IsInfinity(this.value))
			{
				long num = (long)this.value;
				if ((double)num == this.value)
				{
					value = num;
					return true;
				}
			}
			value = 0L;
			return false;
		}

		// Token: 0x06008ABB RID: 35515 RVA: 0x001D34A0 File Offset: 0x001D16A0
		public override bool TryGetDecimal(out decimal value)
		{
			bool flag;
			try
			{
				if (this.IsInteger)
				{
					try
					{
						value = checked((long)this.value);
						return true;
					}
					catch (OverflowException)
					{
					}
				}
				value = (decimal)this.value;
				flag = true;
			}
			catch (OverflowException)
			{
				value = 0m;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06008ABC RID: 35516 RVA: 0x001D350C File Offset: 0x001D170C
		public override NumberValue Abs()
		{
			if (this.value >= 0.0)
			{
				return this;
			}
			return NumberValue.New(-this.value);
		}

		// Token: 0x06008ABD RID: 35517 RVA: 0x001D352D File Offset: 0x001D172D
		public override Value Negate()
		{
			return NumberValue.New(-this.value);
		}

		// Token: 0x06008ABE RID: 35518 RVA: 0x001D353B File Offset: 0x001D173B
		public override NumberValue Ceiling(int digits)
		{
			if (digits == 0)
			{
				return NumberValue.New(Math.Ceiling(this.value));
			}
			return base.ScaledOperation(digits, new Func<decimal, decimal>(Math.Ceiling), true);
		}

		// Token: 0x06008ABF RID: 35519 RVA: 0x001D3565 File Offset: 0x001D1765
		public override NumberValue Floor(int digits)
		{
			if (digits == 0)
			{
				return NumberValue.New(Math.Floor(this.value));
			}
			return base.ScaledOperation(digits, new Func<decimal, decimal>(Math.Floor), false);
		}

		// Token: 0x06008AC0 RID: 35520 RVA: 0x001D358F File Offset: 0x001D178F
		public override NumberValue Truncate(int digits)
		{
			if (digits == 0)
			{
				return NumberValue.New(Math.Truncate(this.value));
			}
			return base.ScaledOperation(digits, new Func<decimal, decimal>(Math.Truncate), false);
		}

		// Token: 0x06008AC1 RID: 35521 RVA: 0x001D35BC File Offset: 0x001D17BC
		public override string ToSource()
		{
			if (double.IsNaN(this.value))
			{
				return "#nan";
			}
			if (double.IsNegativeInfinity(this.value))
			{
				return "-#infinity";
			}
			if (double.IsPositiveInfinity(this.value))
			{
				return "#infinity";
			}
			return this.ToString();
		}

		// Token: 0x06008AC2 RID: 35522 RVA: 0x001D3608 File Offset: 0x001D1808
		public override string ToString()
		{
			if ((this.value > 999999999999999.0 || this.value < -999999999999999.0) && this.IsInteger)
			{
				try
				{
					return this.ToDecimal().ToString("G", CultureInfo.InvariantCulture);
				}
				catch (ValueException)
				{
				}
			}
			return this.value.ToString("R", CultureInfo.InvariantCulture);
		}

		// Token: 0x06008AC3 RID: 35523 RVA: 0x001D3688 File Offset: 0x001D1888
		public override string ToString(string format, IFormatProvider provider)
		{
			if ((this.value > 999999999999999.0 || this.value < -999999999999999.0) && this.IsInteger)
			{
				try
				{
					return this.ToDecimal().ToString("G", provider);
				}
				catch (ValueException)
				{
				}
			}
			return this.value.ToString(format, provider);
		}

		// Token: 0x17002498 RID: 9368
		// (get) Token: 0x06008AC4 RID: 35524 RVA: 0x001D36FC File Offset: 0x001D18FC
		private bool IsInteger
		{
			get
			{
				return Math.Floor(this.value) == this.value;
			}
		}

		// Token: 0x04004C1E RID: 19486
		private readonly double value;
	}
}
