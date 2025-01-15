using System;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020002E2 RID: 738
	public sealed class VBufferBuilder<T> : BufferBuilder<T>
	{
		// Token: 0x060010C5 RID: 4293 RVA: 0x0005DDB4 File Offset: 0x0005BFB4
		public VBufferBuilder(Combiner<T> comb)
			: base(comb)
		{
		}
	}
}
