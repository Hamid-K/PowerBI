using System;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;
using Microsoft.Mashup.Evaluator.Services;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001AFC RID: 6908
	internal class RemoteRedirectPolicyServiceFactory : IRemoteServiceFactory
	{
		// Token: 0x0600AD4A RID: 44362 RVA: 0x002395E4 File Offset: 0x002377E4
		public IRemoteServiceStub CreateStub(IEngineHost engineHost, IMessenger messenger, BinaryWriter proxyInitArgs)
		{
			IRedirectPolicyService redirectPolicyService = engineHost.QueryService<IRedirectPolicyService>();
			proxyInitArgs.WriteBool(redirectPolicyService != null && redirectPolicyService.Legacy);
			return EmptyStub.Instance;
		}

		// Token: 0x0600AD4B RID: 44363 RVA: 0x0023960F File Offset: 0x0023780F
		public IRemoteServiceProxy CreateProxy(IEngineHost engineHost, IMessenger messenger, BinaryReader proxyInitArgs)
		{
			return new EngineHostServiceProxy(new SimpleEngineHost<IRedirectPolicyService>(new RedirectPolicyService(proxyInitArgs.ReadBool())));
		}
	}
}
