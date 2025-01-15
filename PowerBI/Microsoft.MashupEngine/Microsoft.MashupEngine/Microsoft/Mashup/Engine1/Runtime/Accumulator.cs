using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001252 RID: 4690
	public abstract class Accumulator
	{
		// Token: 0x06007B9C RID: 31644
		public abstract void Add(Value value);

		// Token: 0x06007B9D RID: 31645
		public abstract void Subtract(Value value);

		// Token: 0x06007B9E RID: 31646
		public abstract void Divide(Value value);

		// Token: 0x06007B9F RID: 31647
		public abstract Value ToValue();
	}
}
