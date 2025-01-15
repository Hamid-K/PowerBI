using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200160C RID: 5644
	internal abstract class SelectingAccumulator : ChainedAccumulator
	{
		// Token: 0x06008DEC RID: 36332 RVA: 0x001DA77A File Offset: 0x001D897A
		public SelectingAccumulator(IAccumulator accumulator)
			: base(accumulator)
		{
		}

		// Token: 0x06008DED RID: 36333 RVA: 0x001DA783 File Offset: 0x001D8983
		public override void AccumulateNext(IValueReference next)
		{
			if (this.Select(next))
			{
				this.accumulator.AccumulateNext(next);
			}
		}

		// Token: 0x06008DEE RID: 36334
		protected abstract bool Select(IValueReference valueReference);
	}
}
