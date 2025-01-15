using System;
using System.IO;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001B1F RID: 6943
	internal class RemoteTimeZoneServiceFactory : IRemoteServiceFactory
	{
		// Token: 0x0600ADF8 RID: 44536 RVA: 0x00232BD6 File Offset: 0x00230DD6
		public IRemoteServiceStub CreateStub(IEngineHost engineHost, IMessenger messenger, BinaryWriter proxyInitArgs)
		{
			return EmptyStub.Instance;
		}

		// Token: 0x0600ADF9 RID: 44537 RVA: 0x0023A9D7 File Offset: 0x00238BD7
		public IRemoteServiceProxy CreateProxy(IEngineHost engineHost, IMessenger messenger, BinaryReader proxyInitArgs)
		{
			return new EngineHostServiceProxy(new SimpleEngineHost<ITimeZoneService>(MinimalEngineHost.LocalTimeZoneService));
		}
	}
}
