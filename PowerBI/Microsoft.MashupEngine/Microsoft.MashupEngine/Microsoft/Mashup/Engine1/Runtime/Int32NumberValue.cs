using System;
using System.Globalization;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200159B RID: 5531
	public class Int32NumberValue : NumberValue
	{
		// Token: 0x06008A97 RID: 35479 RVA: 0x001D31AA File Offset: 0x001D13AA
		public Int32NumberValue(int value)
		{
			this.value = value;
		}

		// Token: 0x17002494 RID: 9364
		// (get) Token: 0x06008A98 RID: 35480 RVA: 0x001D31B9 File Offset: 0x001D13B9
		public int Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17002495 RID: 9365
		// (get) Token: 0x06008A99 RID: 35481 RVA: 0x00002105 File Offset: 0x00000305
		public override NumberKind NumberKind
		{
			get
			{
				return NumberKind.Int32;
			}
		}

		// Token: 0x06008A9A RID: 35482 RVA: 0x001D31C1 File Offset: 0x001D13C1
		public override double ToDouble()
		{
			return (double)this.value;
		}

		// Token: 0x06008A9B RID: 35483 RVA: 0x001D31CA File Offset: 0x001D13CA
		public override decimal ToDecimal()
		{
			return this.value;
		}

		// Token: 0x06008A9C RID: 35484 RVA: 0x001D31D7 File Offset: 0x001D13D7
		public override object ToObject()
		{
			return this.value;
		}

		// Token: 0x06008A9D RID: 35485 RVA: 0x001D31E4 File Offset: 0x001D13E4
		public override bool TryGetInt64(out long value)
		{
			value = (long)this.value;
			return true;
		}

		// Token: 0x06008A9E RID: 35486 RVA: 0x001D31F0 File Offset: 0x001D13F0
		public override bool TryGetDecimal(out decimal value)
		{
			value = this.value;
			return true;
		}

		// Token: 0x06008A9F RID: 35487 RVA: 0x001D3204 File Offset: 0x001D1404
		public override Value Add(Value value, Precision precision)
		{
			return value.AddR(this, precision);
		}

		// Token: 0x06008AA0 RID: 35488 RVA: 0x001D320E File Offset: 0x001D140E
		public override Value AddR(Int32NumberValue value, Precision precision)
		{
			return precision.Add(value.Value, this.value);
		}

		// Token: 0x06008AA1 RID: 35489 RVA: 0x001D3222 File Offset: 0x001D1422
		public override Value AddR(DoubleNumberValue value, Precision precision)
		{
			return precision.Add(value.Value, (double)this.value);
		}

		// Token: 0x06008AA2 RID: 35490 RVA: 0x001D3237 File Offset: 0x001D1437
		public override Value Subtract(Value value, Precision precision)
		{
			return value.SubtractR(this, precision);
		}

		// Token: 0x06008AA3 RID: 35491 RVA: 0x001D3241 File Offset: 0x001D1441
		public override Value SubtractR(Int32NumberValue value, Precision precision)
		{
			return precision.Subtract(value.Value, this.value);
		}

		// Token: 0x06008AA4 RID: 35492 RVA: 0x001D3255 File Offset: 0x001D1455
		public override Value SubtractR(DoubleNumberValue value, Precision precision)
		{
			return precision.Subtract(value.Value, (double)this.value);
		}

		// Token: 0x06008AA5 RID: 35493 RVA: 0x001D326A File Offset: 0x001D146A
		public override NumberValue Abs()
		{
			if (this.value >= 0)
			{
				return this;
			}
			return this.Negate().AsNumber;
		}

		// Token: 0x06008AA6 RID: 35494 RVA: 0x001D3282 File Offset: 0x001D1482
		public override Value Negate()
		{
			if (this.value == -2147483648)
			{
				return NumberValue.New(2147483648.0);
			}
			return NumberValue.New(-this.value);
		}

		// Token: 0x06008AA7 RID: 35495 RVA: 0x001D32AC File Offset: 0x001D14AC
		public override NumberValue Ceiling(int digits)
		{
			if (digits >= 0)
			{
				return this;
			}
			return base.ScaledOperation(digits, new Func<decimal, decimal>(Math.Ceiling), true);
		}

		// Token: 0x06008AA8 RID: 35496 RVA: 0x001D32C8 File Offset: 0x001D14C8
		public override NumberValue Floor(int digits)
		{
			if (digits >= 0)
			{
				return this;
			}
			return base.ScaledOperation(digits, new Func<decimal, decimal>(Math.Floor), false);
		}

		// Token: 0x06008AA9 RID: 35497 RVA: 0x001D32E4 File Offset: 0x001D14E4
		public override NumberValue Truncate(int digits)
		{
			if (digits >= 0)
			{
				return this;
			}
			return base.ScaledOperation(digits, new Func<decimal, decimal>(Math.Truncate), false);
		}

		// Token: 0x06008AAA RID: 35498 RVA: 0x001D3300 File Offset: 0x001D1500
		public override string ToString()
		{
			return this.value.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06008AAB RID: 35499 RVA: 0x001D3320 File Offset: 0x001D1520
		public override string ToString(string format, IFormatProvider provider)
		{
			if (format.Equals("R", StringComparison.OrdinalIgnoreCase))
			{
				format = "G";
			}
			return this.value.ToString(format, provider);
		}

		// Token: 0x06008AAC RID: 35500 RVA: 0x001D3352 File Offset: 0x001D1552
		public override Value ShiftLeft(Value value)
		{
			if (value.IsNull)
			{
				return Microsoft.Mashup.Engine1.Runtime.Value.Null;
			}
			return NumberValue.New((long)this.value << value.AsNumber.AsInteger32);
		}

		// Token: 0x06008AAD RID: 35501 RVA: 0x001D337D File Offset: 0x001D157D
		public override Value ShiftRight(Value value)
		{
			if (value.IsNull)
			{
				return Microsoft.Mashup.Engine1.Runtime.Value.Null;
			}
			return NumberValue.New((long)this.value >> value.AsNumber.AsInteger32);
		}

		// Token: 0x04004C1D RID: 19485
		private readonly int value;
	}
}
