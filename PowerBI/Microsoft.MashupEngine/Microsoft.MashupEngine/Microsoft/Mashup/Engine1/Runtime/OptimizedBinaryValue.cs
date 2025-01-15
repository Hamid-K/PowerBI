using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001287 RID: 4743
	internal sealed class OptimizedBinaryValue : DelegatingBinaryValue, IOptimizedValue
	{
		// Token: 0x06007CA9 RID: 31913 RVA: 0x001AC146 File Offset: 0x001AA346
		public OptimizedBinaryValue(BinaryValue binary)
			: base(binary)
		{
		}

		// Token: 0x06007CAA RID: 31914 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public override BinaryValue Optimize()
		{
			return this;
		}
	}
}
