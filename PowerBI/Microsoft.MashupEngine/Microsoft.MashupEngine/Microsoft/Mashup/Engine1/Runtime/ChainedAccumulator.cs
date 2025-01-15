using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200129C RID: 4764
	internal abstract class ChainedAccumulator : IAccumulator
	{
		// Token: 0x06007D1F RID: 32031 RVA: 0x001AD2C0 File Offset: 0x001AB4C0
		public ChainedAccumulator(IAccumulator accumulator)
		{
			this.accumulator = accumulator;
		}

		// Token: 0x17002207 RID: 8711
		// (get) Token: 0x06007D20 RID: 32032 RVA: 0x001AD2CF File Offset: 0x001AB4CF
		public IValueReference Current
		{
			get
			{
				return this.accumulator.Current;
			}
		}

		// Token: 0x06007D21 RID: 32033
		public abstract void AccumulateNext(IValueReference next);

		// Token: 0x040044F4 RID: 17652
		protected readonly IAccumulator accumulator;
	}
}
