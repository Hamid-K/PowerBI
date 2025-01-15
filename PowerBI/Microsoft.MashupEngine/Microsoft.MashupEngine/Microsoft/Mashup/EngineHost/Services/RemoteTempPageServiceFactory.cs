using System;
using System.IO;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001B1C RID: 6940
	internal class RemoteTempPageServiceFactory : IRemoteServiceFactory
	{
		// Token: 0x0600ADEF RID: 44527 RVA: 0x00232BD6 File Offset: 0x00230DD6
		public IRemoteServiceStub CreateStub(IEngineHost engineHost, IMessenger messenger, BinaryWriter proxyInitArgs)
		{
			return EmptyStub.Instance;
		}

		// Token: 0x0600ADF0 RID: 44528 RVA: 0x0023A960 File Offset: 0x00238B60
		public IRemoteServiceProxy CreateProxy(IEngineHost engineHost, IMessenger messenger, BinaryReader proxyInitArgs)
		{
			return new RemoteTempPageServiceFactory.Proxy();
		}

		// Token: 0x02001B1D RID: 6941
		private sealed class Proxy : IRemoteServiceProxy, IEngineHost, IDisposable
		{
			// Token: 0x0600ADF2 RID: 44530 RVA: 0x0023A967 File Offset: 0x00238B67
			public Proxy()
			{
				this.tempPageService = new TempPageService();
			}

			// Token: 0x0600ADF3 RID: 44531 RVA: 0x0023A97C File Offset: 0x00238B7C
			T IEngineHost.QueryService<T>()
			{
				if (typeof(T) == typeof(ITempPageService))
				{
					return (T)((object)this.tempPageService);
				}
				return default(T);
			}

			// Token: 0x0600ADF4 RID: 44532 RVA: 0x0023A9B9 File Offset: 0x00238BB9
			public void Dispose()
			{
				this.tempPageService.Dispose();
			}

			// Token: 0x040059C0 RID: 22976
			private readonly TempPageService tempPageService;
		}
	}
}
