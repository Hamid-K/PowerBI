using System;
using System.IO;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator.Services
{
	// Token: 0x02001D8A RID: 7562
	public class RemoteDocumentEvaluationConfigServiceFactory : IRemoteServiceFactory
	{
		// Token: 0x0600BBCD RID: 48077 RVA: 0x00260150 File Offset: 0x0025E350
		public IRemoteServiceStub CreateStub(IEngineHost engineHost, IMessenger messenger, BinaryWriter proxyInitArgs)
		{
			IDocumentEvaluationConfigService documentEvaluationConfigService = engineHost.QueryService<IDocumentEvaluationConfigService>();
			proxyInitArgs.WriteDocumentEvaluationConfig(documentEvaluationConfigService.Config);
			return EmptyStub.Instance;
		}

		// Token: 0x0600BBCE RID: 48078 RVA: 0x00260175 File Offset: 0x0025E375
		public IRemoteServiceProxy CreateProxy(IEngineHost engineHost, IMessenger messenger, BinaryReader proxyInitArgs)
		{
			return new EngineHostServiceProxy(new SimpleEngineHost<IDocumentEvaluationConfigService>(new DocumentEvaluationConfigService(proxyInitArgs.ReadDocumentEvaluationConfig())));
		}
	}
}
