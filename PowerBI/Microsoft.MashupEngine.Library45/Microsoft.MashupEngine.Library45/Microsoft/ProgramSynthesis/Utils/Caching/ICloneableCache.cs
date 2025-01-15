using System;

namespace Microsoft.ProgramSynthesis.Utils.Caching
{
	// Token: 0x02000630 RID: 1584
	public interface ICloneableCache : ICachefulObject
	{
		// Token: 0x06002264 RID: 8804
		ICloneableCache ShallowClone();

		// Token: 0x06002265 RID: 8805
		ICloneableCache DeepClone();

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06002266 RID: 8806
		int Count { get; }

		// Token: 0x06002267 RID: 8807
		void Clear();
	}
}
