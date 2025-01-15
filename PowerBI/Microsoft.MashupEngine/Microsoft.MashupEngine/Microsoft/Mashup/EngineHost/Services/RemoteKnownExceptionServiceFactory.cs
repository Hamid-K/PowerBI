using System;
using System.IO;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001AA2 RID: 6818
	internal class RemoteKnownExceptionServiceFactory : IRemoteServiceFactory
	{
		// Token: 0x0600ABB5 RID: 43957 RVA: 0x00232BD6 File Offset: 0x00230DD6
		public IRemoteServiceStub CreateStub(IEngineHost engineHost, IMessenger messenger, BinaryWriter proxyInitArgs)
		{
			return EmptyStub.Instance;
		}

		// Token: 0x0600ABB6 RID: 43958 RVA: 0x00235E6E File Offset: 0x0023406E
		public IRemoteServiceProxy CreateProxy(IEngineHost engineHost, IMessenger messenger, BinaryReader proxyInitArgs)
		{
			return new EngineHostServiceProxy(new SimpleEngineHost<IKnownExceptionService>(KnownExceptionService.Instance));
		}
	}
}
