using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020015E1 RID: 5601
	internal sealed class RecordAccumulable : IAccumulable
	{
		// Token: 0x06008CCE RID: 36046 RVA: 0x001D83BF File Offset: 0x001D65BF
		public RecordAccumulable(Keys keys, IEnumerable<IAccumulable> accumulables)
		{
			this.keys = keys;
			this.accumulables = accumulables;
		}

		// Token: 0x06008CCF RID: 36047 RVA: 0x001D83D5 File Offset: 0x001D65D5
		public IAccumulator CreateAccumulator()
		{
			return new RecordAccumulable.RecordAccumulator(this.keys, this.accumulables.Select((IAccumulable a) => a.CreateAccumulator()).ToArray<IAccumulator>());
		}

		// Token: 0x04004CCA RID: 19658
		private readonly Keys keys;

		// Token: 0x04004CCB RID: 19659
		private readonly IEnumerable<IAccumulable> accumulables;

		// Token: 0x020015E2 RID: 5602
		private sealed class RecordAccumulator : IAccumulator
		{
			// Token: 0x06008CD0 RID: 36048 RVA: 0x001D8411 File Offset: 0x001D6611
			public RecordAccumulator(Keys keys, IAccumulator[] accumulators)
			{
				this.keys = keys;
				this.accumulators = accumulators;
			}

			// Token: 0x170024E9 RID: 9449
			// (get) Token: 0x06008CD1 RID: 36049 RVA: 0x001D8428 File Offset: 0x001D6628
			public IValueReference Current
			{
				get
				{
					IValueReference[] array = new IValueReference[this.accumulators.Length];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = this.accumulators[i].Current;
					}
					return RecordValue.New(this.keys, array);
				}
			}

			// Token: 0x06008CD2 RID: 36050 RVA: 0x001D8470 File Offset: 0x001D6670
			public void AccumulateNext(IValueReference next)
			{
				IAccumulator[] array = this.accumulators;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].AccumulateNext(next);
				}
			}

			// Token: 0x04004CCC RID: 19660
			private readonly Keys keys;

			// Token: 0x04004CCD RID: 19661
			private readonly IAccumulator[] accumulators;
		}
	}
}
