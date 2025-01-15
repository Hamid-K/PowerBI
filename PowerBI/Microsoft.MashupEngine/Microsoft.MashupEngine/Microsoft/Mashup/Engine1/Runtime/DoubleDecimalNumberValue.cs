using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200159E RID: 5534
	internal class DoubleDecimalNumberValue : DecimalNumberValue
	{
		// Token: 0x06008AD8 RID: 35544 RVA: 0x001D38A2 File Offset: 0x001D1AA2
		public DoubleDecimalNumberValue(decimal value, double doubleValue)
			: base(value)
		{
			this.doubleValue = doubleValue;
		}

		// Token: 0x06008AD9 RID: 35545 RVA: 0x001D38B2 File Offset: 0x001D1AB2
		public override double ToDouble()
		{
			return this.doubleValue;
		}

		// Token: 0x06008ADA RID: 35546 RVA: 0x001D38BA File Offset: 0x001D1ABA
		public override Value Negate()
		{
			return NumberValue.New(-base.Value, -this.doubleValue);
		}

		// Token: 0x04004C20 RID: 19488
		private readonly double doubleValue;
	}
}
