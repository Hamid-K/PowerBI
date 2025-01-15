using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001676 RID: 5750
	internal abstract class TransformingAccumulator : ChainedAccumulator
	{
		// Token: 0x0600918D RID: 37261 RVA: 0x001DA77A File Offset: 0x001D897A
		public TransformingAccumulator(IAccumulator accumulator)
			: base(accumulator)
		{
		}

		// Token: 0x0600918E RID: 37262 RVA: 0x001E37FC File Offset: 0x001E19FC
		public override void AccumulateNext(IValueReference next)
		{
			this.accumulator.AccumulateNext(this.Transform(next));
		}

		// Token: 0x0600918F RID: 37263
		protected abstract IValueReference Transform(IValueReference valueReference);
	}
}
