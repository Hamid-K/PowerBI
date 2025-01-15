using System;
using System.IO;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001B1E RID: 6942
	internal class RemoteThreadPoolServiceFactory : IRemoteServiceFactory
	{
		// Token: 0x0600ADF5 RID: 44533 RVA: 0x00232BD6 File Offset: 0x00230DD6
		public IRemoteServiceStub CreateStub(IEngineHost engineHost, IMessenger messenger, BinaryWriter proxyInitArgs)
		{
			return EmptyStub.Instance;
		}

		// Token: 0x0600ADF6 RID: 44534 RVA: 0x0023A9C6 File Offset: 0x00238BC6
		public IRemoteServiceProxy CreateProxy(IEngineHost engineHost, IMessenger messenger, BinaryReader proxyInitArgs)
		{
			return new EngineHostServiceProxy(new SimpleEngineHost<IThreadPoolService>(new ThreadPoolService()));
		}
	}
}
