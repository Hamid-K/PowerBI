using System;
using System.IO;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001B28 RID: 6952
	internal class RemoteUniqueIdServiceFactory : IRemoteServiceFactory
	{
		// Token: 0x0600AE1C RID: 44572 RVA: 0x00232BD6 File Offset: 0x00230DD6
		public IRemoteServiceStub CreateStub(IEngineHost engineHost, IMessenger messenger, BinaryWriter proxyInitArgs)
		{
			return EmptyStub.Instance;
		}

		// Token: 0x0600AE1D RID: 44573 RVA: 0x0023AD66 File Offset: 0x00238F66
		public IRemoteServiceProxy CreateProxy(IEngineHost engineHost, IMessenger messenger, BinaryReader proxyInitArgs)
		{
			return new EngineHostServiceProxy(new SimpleEngineHost<IUniqueIdService>(new UniqueIdService()));
		}
	}
}
