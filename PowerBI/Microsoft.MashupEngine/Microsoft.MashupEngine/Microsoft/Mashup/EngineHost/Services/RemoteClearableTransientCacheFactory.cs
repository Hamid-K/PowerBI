using System;
using System.IO;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A5A RID: 6746
	internal class RemoteClearableTransientCacheFactory : IRemoteServiceFactory
	{
		// Token: 0x0600AA63 RID: 43619 RVA: 0x00232BD6 File Offset: 0x00230DD6
		public IRemoteServiceStub CreateStub(IEngineHost engineHost, IMessenger messenger, BinaryWriter proxyInitArgs)
		{
			return EmptyStub.Instance;
		}

		// Token: 0x0600AA64 RID: 43620 RVA: 0x00232BDD File Offset: 0x00230DDD
		public IRemoteServiceProxy CreateProxy(IEngineHost engineHost, IMessenger messenger, BinaryReader proxyInitArgs)
		{
			return new RemoteClearableTransientCacheFactory.Proxy(new ClearableTransientCache());
		}

		// Token: 0x02001A5B RID: 6747
		private sealed class Proxy : IRemoteServiceProxy, IEngineHost, IDisposable
		{
			// Token: 0x0600AA66 RID: 43622 RVA: 0x00232BE9 File Offset: 0x00230DE9
			public Proxy(ClearableTransientCache clearableTransientCache)
			{
				this.clearableTransientCache = clearableTransientCache;
			}

			// Token: 0x0600AA67 RID: 43623 RVA: 0x00232BF8 File Offset: 0x00230DF8
			T IEngineHost.QueryService<T>()
			{
				if (typeof(T) == typeof(IClearableTransientCache))
				{
					return (T)((object)this.clearableTransientCache);
				}
				return default(T);
			}

			// Token: 0x0600AA68 RID: 43624 RVA: 0x00232C35 File Offset: 0x00230E35
			public void Dispose()
			{
				if (this.clearableTransientCache != null)
				{
					this.clearableTransientCache.Dispose();
					this.clearableTransientCache = null;
				}
			}

			// Token: 0x04005878 RID: 22648
			private ClearableTransientCache clearableTransientCache;
		}
	}
}
