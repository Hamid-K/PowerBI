using System;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A93 RID: 6803
	internal class RemoteGarbageCollectionService : IRemoteServiceFactory
	{
		// Token: 0x0600AB72 RID: 43890 RVA: 0x002354D0 File Offset: 0x002336D0
		public IRemoteServiceStub CreateStub(IEngineHost engineHost, IMessenger messenger, BinaryWriter proxyInitArgs)
		{
			bool flag = engineHost.QueryService<GarbageCollectionService>() != null;
			proxyInitArgs.WriteBool(flag);
			return EmptyStub.Instance;
		}

		// Token: 0x0600AB73 RID: 43891 RVA: 0x002354F3 File Offset: 0x002336F3
		public IRemoteServiceProxy CreateProxy(IEngineHost engineHost, IMessenger messenger, BinaryReader proxyInitArgs)
		{
			if (proxyInitArgs.ReadBool())
			{
				return new RemoteGarbageCollectionService.Proxy(engineHost.QueryService<IEvaluationConstants>());
			}
			return EmptyProxy.Instance;
		}

		// Token: 0x02001A94 RID: 6804
		private class Proxy : IRemoteServiceProxy, IEngineHost, IDisposable
		{
			// Token: 0x0600AB75 RID: 43893 RVA: 0x0023550E File Offset: 0x0023370E
			public Proxy(IEvaluationConstants evaluationConstants)
			{
				this.gcService = new GarbageCollectionService(evaluationConstants);
			}

			// Token: 0x0600AB76 RID: 43894 RVA: 0x00235524 File Offset: 0x00233724
			public T QueryService<T>() where T : class
			{
				if (typeof(T) == typeof(GarbageCollectionService))
				{
					return (T)((object)this.gcService);
				}
				return default(T);
			}

			// Token: 0x0600AB77 RID: 43895 RVA: 0x00235561 File Offset: 0x00233761
			public void Dispose()
			{
				this.gcService.Dispose();
			}

			// Token: 0x040058DD RID: 22749
			private readonly GarbageCollectionService gcService;
		}
	}
}
