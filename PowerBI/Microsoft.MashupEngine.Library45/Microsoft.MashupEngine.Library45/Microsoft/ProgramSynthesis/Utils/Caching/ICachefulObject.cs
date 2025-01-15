using System;

namespace Microsoft.ProgramSynthesis.Utils.Caching
{
	// Token: 0x0200062E RID: 1582
	public interface ICachefulObject
	{
		// Token: 0x06002261 RID: 8801
		ICachefulObject CloneWithCurrentCacheState();

		// Token: 0x06002262 RID: 8802
		void ClearCaches();
	}
}
