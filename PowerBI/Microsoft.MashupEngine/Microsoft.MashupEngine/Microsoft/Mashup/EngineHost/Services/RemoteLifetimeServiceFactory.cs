using System;
using System.IO;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001AB2 RID: 6834
	internal class RemoteLifetimeServiceFactory : IRemoteServiceFactory
	{
		// Token: 0x0600AC09 RID: 44041 RVA: 0x00232BD6 File Offset: 0x00230DD6
		public IRemoteServiceStub CreateStub(IEngineHost engineHost, IMessenger messenger, BinaryWriter proxyInitArgs)
		{
			return EmptyStub.Instance;
		}

		// Token: 0x0600AC0A RID: 44042 RVA: 0x0023678B File Offset: 0x0023498B
		public IRemoteServiceProxy CreateProxy(IEngineHost engineHost, IMessenger messenger, BinaryReader proxyInitArgs)
		{
			return new RemoteLifetimeServiceFactory.Proxy();
		}

		// Token: 0x02001AB3 RID: 6835
		private sealed class Proxy : IRemoteServiceProxy, IEngineHost, IDisposable
		{
			// Token: 0x0600AC0C RID: 44044 RVA: 0x00236792 File Offset: 0x00234992
			public Proxy()
			{
				this.lifetimeService = new LifetimeService();
			}

			// Token: 0x0600AC0D RID: 44045 RVA: 0x002367A8 File Offset: 0x002349A8
			T IEngineHost.QueryService<T>()
			{
				if (typeof(T) == typeof(ILifetimeService))
				{
					return (T)((object)this.lifetimeService);
				}
				return default(T);
			}

			// Token: 0x0600AC0E RID: 44046 RVA: 0x002367E5 File Offset: 0x002349E5
			public void Dispose()
			{
				this.lifetimeService.Dispose();
			}

			// Token: 0x0400590D RID: 22797
			private readonly LifetimeService lifetimeService;
		}
	}
}
