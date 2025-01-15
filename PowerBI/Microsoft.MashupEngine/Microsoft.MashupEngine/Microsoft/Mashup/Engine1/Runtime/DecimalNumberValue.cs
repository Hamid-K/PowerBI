using System;
using System.Globalization;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200159D RID: 5533
	internal class DecimalNumberValue : NumberValue
	{
		// Token: 0x06008AC5 RID: 35525 RVA: 0x001D3711 File Offset: 0x001D1911
		public DecimalNumberValue(decimal value)
		{
			this.value = value;
		}

		// Token: 0x17002499 RID: 9369
		// (get) Token: 0x06008AC6 RID: 35526 RVA: 0x001D3720 File Offset: 0x001D1920
		public decimal Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x1700249A RID: 9370
		// (get) Token: 0x06008AC7 RID: 35527 RVA: 0x000023C4 File Offset: 0x000005C4
		public override NumberKind NumberKind
		{
			get
			{
				return NumberKind.Decimal;
			}
		}

		// Token: 0x06008AC8 RID: 35528 RVA: 0x001D3728 File Offset: 0x001D1928
		public override double ToDouble()
		{
			return (double)this.value;
		}

		// Token: 0x06008AC9 RID: 35529 RVA: 0x001D3720 File Offset: 0x001D1920
		public override decimal ToDecimal()
		{
			return this.value;
		}

		// Token: 0x06008ACA RID: 35530 RVA: 0x001D3736 File Offset: 0x001D1936
		public override Value AddR(Int32NumberValue value, Precision precision)
		{
			return precision.Add(value, this);
		}

		// Token: 0x06008ACB RID: 35531 RVA: 0x001D3736 File Offset: 0x001D1936
		public override Value AddR(DoubleNumberValue value, Precision precision)
		{
			return precision.Add(value, this);
		}

		// Token: 0x06008ACC RID: 35532 RVA: 0x001D3740 File Offset: 0x001D1940
		public override Value SubtractR(Int32NumberValue value, Precision precision)
		{
			return precision.Subtract(value, this);
		}

		// Token: 0x06008ACD RID: 35533 RVA: 0x001D3740 File Offset: 0x001D1940
		public override Value SubtractR(DoubleNumberValue value, Precision precision)
		{
			return precision.Subtract(value, this);
		}

		// Token: 0x06008ACE RID: 35534 RVA: 0x001D374A File Offset: 0x001D194A
		public override object ToObject()
		{
			return this.value;
		}

		// Token: 0x06008ACF RID: 35535 RVA: 0x001D3758 File Offset: 0x001D1958
		public override bool TryGetInt64(out long value)
		{
			if (this.value >= -9223372036854775808m && this.value <= 9223372036854775807m)
			{
				long num = (long)this.value;
				if (num == this.value)
				{
					value = num;
					return true;
				}
			}
			value = 0L;
			return false;
		}

		// Token: 0x06008AD0 RID: 35536 RVA: 0x001D37C4 File Offset: 0x001D19C4
		public override bool TryGetDecimal(out decimal value)
		{
			value = this.value;
			return true;
		}

		// Token: 0x06008AD1 RID: 35537 RVA: 0x001D37D3 File Offset: 0x001D19D3
		public override NumberValue Abs()
		{
			if (this.value >= 0m)
			{
				return this;
			}
			return this.Negate().AsNumber;
		}

		// Token: 0x06008AD2 RID: 35538 RVA: 0x001D37F4 File Offset: 0x001D19F4
		public override Value Negate()
		{
			return NumberValue.New(-this.value);
		}

		// Token: 0x06008AD3 RID: 35539 RVA: 0x001D3806 File Offset: 0x001D1A06
		public override NumberValue Ceiling(int digits)
		{
			return base.ScaledOperation(digits, new Func<decimal, decimal>(Math.Ceiling), true);
		}

		// Token: 0x06008AD4 RID: 35540 RVA: 0x001D381C File Offset: 0x001D1A1C
		public override NumberValue Floor(int digits)
		{
			return base.ScaledOperation(digits, new Func<decimal, decimal>(Math.Floor), false);
		}

		// Token: 0x06008AD5 RID: 35541 RVA: 0x001D3832 File Offset: 0x001D1A32
		public override NumberValue Truncate(int digits)
		{
			return base.ScaledOperation(digits, new Func<decimal, decimal>(Math.Truncate), false);
		}

		// Token: 0x06008AD6 RID: 35542 RVA: 0x001D3848 File Offset: 0x001D1A48
		public override string ToString()
		{
			return this.value.ToString("G", CultureInfo.InvariantCulture);
		}

		// Token: 0x06008AD7 RID: 35543 RVA: 0x001D3870 File Offset: 0x001D1A70
		public override string ToString(string format, IFormatProvider provider)
		{
			if (format.Equals("R", StringComparison.OrdinalIgnoreCase))
			{
				format = "G";
			}
			return this.value.ToString(format, provider);
		}

		// Token: 0x04004C1F RID: 19487
		private readonly decimal value;
	}
}
