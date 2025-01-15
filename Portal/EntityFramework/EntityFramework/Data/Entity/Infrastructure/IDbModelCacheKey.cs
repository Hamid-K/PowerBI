using System;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200024D RID: 589
	public interface IDbModelCacheKey
	{
		// Token: 0x06001EB7 RID: 7863
		bool Equals(object other);

		// Token: 0x06001EB8 RID: 7864
		int GetHashCode();
	}
}
