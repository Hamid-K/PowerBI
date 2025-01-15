using System;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x02000402 RID: 1026
	public interface ICachedEquatable<in T> where T : class, IEquatable<T>
	{
		// Token: 0x06001744 RID: 5956
		bool NonCachedEquals(T other);
	}
}
