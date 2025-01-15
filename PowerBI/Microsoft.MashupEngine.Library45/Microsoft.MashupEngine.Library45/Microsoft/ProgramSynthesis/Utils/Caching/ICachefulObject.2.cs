using System;

namespace Microsoft.ProgramSynthesis.Utils.Caching
{
	// Token: 0x0200062F RID: 1583
	public interface ICachefulObject<out TDerived> : ICachefulObject where TDerived : ICachefulObject<TDerived>
	{
		// Token: 0x06002263 RID: 8803
		TDerived CloneWithCurrentCacheState();
	}
}
