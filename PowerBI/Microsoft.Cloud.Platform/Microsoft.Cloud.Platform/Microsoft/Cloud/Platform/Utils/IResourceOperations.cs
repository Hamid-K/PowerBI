using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000243 RID: 579
	public interface IResourceOperations<T>
	{
		// Token: 0x06000EE1 RID: 3809
		T CreateResource(object state);

		// Token: 0x06000EE2 RID: 3810
		void DestroyResource(T resource);

		// Token: 0x06000EE3 RID: 3811
		bool IsHealtyResource(T resource);

		// Token: 0x06000EE4 RID: 3812
		bool HandleResourceFailure(T resource, Exception e);
	}
}
