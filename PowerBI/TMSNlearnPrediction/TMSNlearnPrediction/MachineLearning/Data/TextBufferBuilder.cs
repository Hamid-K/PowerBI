using System;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020002E4 RID: 740
	public sealed class TextBufferBuilder : BufferBuilder<DvText>
	{
		// Token: 0x060010C7 RID: 4295 RVA: 0x0005DDCA File Offset: 0x0005BFCA
		public TextBufferBuilder()
			: base(TextCombiner.Instance)
		{
		}
	}
}
