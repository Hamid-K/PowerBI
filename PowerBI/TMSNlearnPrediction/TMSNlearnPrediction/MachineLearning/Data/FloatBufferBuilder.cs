using System;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020002E3 RID: 739
	public sealed class FloatBufferBuilder : BufferBuilder<float>
	{
		// Token: 0x060010C6 RID: 4294 RVA: 0x0005DDBD File Offset: 0x0005BFBD
		public FloatBufferBuilder()
			: base(FloatAdder.Instance)
		{
		}
	}
}
