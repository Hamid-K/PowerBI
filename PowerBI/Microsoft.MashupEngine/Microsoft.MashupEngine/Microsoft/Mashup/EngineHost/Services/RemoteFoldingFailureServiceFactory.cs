using System;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A92 RID: 6802
	internal class RemoteFoldingFailureServiceFactory : IRemoteServiceFactory
	{
		// Token: 0x0600AB6F RID: 43887 RVA: 0x00235470 File Offset: 0x00233670
		public IRemoteServiceStub CreateStub(IEngineHost engineHost, IMessenger messenger, BinaryWriter proxyInitArgs)
		{
			IFoldingFailureService foldingFailureService = engineHost.QueryService<IFoldingFailureService>();
			proxyInitArgs.WriteBool(foldingFailureService.ThrowOnFoldingFailure);
			proxyInitArgs.WriteBool(foldingFailureService.ThrowOnVolatileFunctions);
			return EmptyStub.Instance;
		}

		// Token: 0x0600AB70 RID: 43888 RVA: 0x002354A4 File Offset: 0x002336A4
		public IRemoteServiceProxy CreateProxy(IEngineHost engineHost, IMessenger messenger, BinaryReader proxyInitArgs)
		{
			bool flag = proxyInitArgs.ReadBoolean();
			bool flag2 = proxyInitArgs.ReadBoolean();
			return new EngineHostServiceProxy(new SimpleEngineHost<IFoldingFailureService>(new FoldingFailureService(flag, flag2)));
		}
	}
}
