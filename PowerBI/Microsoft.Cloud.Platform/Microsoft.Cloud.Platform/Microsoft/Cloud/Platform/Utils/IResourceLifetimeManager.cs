using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000241 RID: 577
	public interface IResourceLifetimeManager<TKey, TValue> : IResourceLifetimeManager, IDisposable
	{
		// Token: 0x06000EDD RID: 3805
		IResourceHandle<TValue> Get(TKey key, object createParams);

		// Token: 0x06000EDE RID: 3806
		void Release(IResourceHandle<TValue> resource);

		// Token: 0x06000EDF RID: 3807
		void ReportFaulted(IResourceHandle<TValue> resource, Exception e);
	}
}
