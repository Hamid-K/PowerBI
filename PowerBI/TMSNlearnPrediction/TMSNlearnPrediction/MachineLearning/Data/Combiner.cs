using System;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000271 RID: 625
	public abstract class Combiner<T>
	{
		// Token: 0x06000DCE RID: 3534
		public abstract bool IsDefault(T value);

		// Token: 0x06000DCF RID: 3535
		public abstract void Combine(ref T dst, T src);
	}
}
