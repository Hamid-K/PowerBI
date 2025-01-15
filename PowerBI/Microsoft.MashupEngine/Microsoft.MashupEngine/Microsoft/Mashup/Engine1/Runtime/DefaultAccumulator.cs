using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001253 RID: 4691
	internal class DefaultAccumulator : Accumulator
	{
		// Token: 0x06007BA1 RID: 31649 RVA: 0x001AA4FA File Offset: 0x001A86FA
		public DefaultAccumulator(Precision precision, Value zero)
		{
			this.precision = precision;
			this.value = zero;
		}

		// Token: 0x06007BA2 RID: 31650 RVA: 0x001AA510 File Offset: 0x001A8710
		public override void Add(Value value)
		{
			this.value = this.precision.Add(this.value, value);
		}

		// Token: 0x06007BA3 RID: 31651 RVA: 0x001AA52A File Offset: 0x001A872A
		public override void Subtract(Value value)
		{
			this.value = this.precision.Subtract(this.value, value);
		}

		// Token: 0x06007BA4 RID: 31652 RVA: 0x001AA544 File Offset: 0x001A8744
		public override void Divide(Value value)
		{
			this.value = this.precision.Divide(this.value, value);
		}

		// Token: 0x06007BA5 RID: 31653 RVA: 0x001AA55E File Offset: 0x001A875E
		public override Value ToValue()
		{
			return this.value;
		}

		// Token: 0x04004489 RID: 17545
		private Precision precision;

		// Token: 0x0400448A RID: 17546
		private Value value;
	}
}
