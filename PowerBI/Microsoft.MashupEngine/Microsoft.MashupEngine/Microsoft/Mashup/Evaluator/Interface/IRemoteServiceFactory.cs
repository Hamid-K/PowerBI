using System;
using System.IO;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E2A RID: 7722
	public interface IRemoteServiceFactory
	{
		// Token: 0x0600BE2D RID: 48685
		IRemoteServiceStub CreateStub(IEngineHost engineHost, IMessenger messenger, BinaryWriter proxyInitArgs);

		// Token: 0x0600BE2E RID: 48686
		IRemoteServiceProxy CreateProxy(IEngineHost engineHost, IMessenger messenger, BinaryReader proxyInitArgs);
	}
}
