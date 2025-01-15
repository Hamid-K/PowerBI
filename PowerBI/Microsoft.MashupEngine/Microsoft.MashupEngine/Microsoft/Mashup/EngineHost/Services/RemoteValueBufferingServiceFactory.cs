using System;
using System.IO;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001B29 RID: 6953
	internal class RemoteValueBufferingServiceFactory : IRemoteServiceFactory
	{
		// Token: 0x0600AE1F RID: 44575 RVA: 0x00232BD6 File Offset: 0x00230DD6
		public IRemoteServiceStub CreateStub(IEngineHost engineHost, IMessenger messenger, BinaryWriter proxyInitArgs)
		{
			return EmptyStub.Instance;
		}

		// Token: 0x0600AE20 RID: 44576 RVA: 0x0023AD78 File Offset: 0x00238F78
		public IRemoteServiceProxy CreateProxy(IEngineHost engineHost, IMessenger messenger, BinaryReader proxyInitArgs)
		{
			IEngine engine = engineHost.QueryService<IEngine>();
			return new EngineHostServiceProxy(new SimpleEngineHost<IValueBufferingService>(new ValueBufferService(engineHost, engine)));
		}
	}
}
