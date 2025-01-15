using System;
using System.IO;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator.Services
{
	// Token: 0x02001D8C RID: 7564
	public class RemoteTraitTrackingServiceFactory : IRemoteServiceFactory
	{
		// Token: 0x0600BBD3 RID: 48083 RVA: 0x00232BD6 File Offset: 0x00230DD6
		public IRemoteServiceStub CreateStub(IEngineHost engineHost, IMessenger messenger, BinaryWriter proxyInitArgs)
		{
			return EmptyStub.Instance;
		}

		// Token: 0x0600BBD4 RID: 48084 RVA: 0x002601EB File Offset: 0x0025E3EB
		public IRemoteServiceProxy CreateProxy(IEngineHost engineHost, IMessenger messenger, BinaryReader proxyInitArgs)
		{
			return new EngineHostServiceProxy(new SimpleEngineHost<ITraitTrackingService>(new TraitTrackingService(engineHost.QueryService<IEngine>())));
		}
	}
}
