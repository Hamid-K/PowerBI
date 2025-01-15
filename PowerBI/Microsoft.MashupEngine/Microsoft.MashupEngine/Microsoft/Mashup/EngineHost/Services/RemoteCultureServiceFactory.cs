using System;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A72 RID: 6770
	internal class RemoteCultureServiceFactory : IRemoteServiceFactory
	{
		// Token: 0x0600AAC6 RID: 43718 RVA: 0x00233BC8 File Offset: 0x00231DC8
		public IRemoteServiceStub CreateStub(IEngineHost engineHost, IMessenger messenger, BinaryWriter proxyInitArgs)
		{
			ICultureService cultureService = engineHost.QueryService<ICultureService>();
			proxyInitArgs.WriteString(cultureService.DefaultCulture.Name);
			return EmptyStub.Instance;
		}

		// Token: 0x0600AAC7 RID: 43719 RVA: 0x00233BF4 File Offset: 0x00231DF4
		public IRemoteServiceProxy CreateProxy(IEngineHost engineHost, IMessenger messenger, BinaryReader proxyInitArgs)
		{
			string text = proxyInitArgs.ReadString();
			return new EngineHostServiceProxy(new SimpleEngineHost<ICultureService>(new CultureService(engineHost, text)));
		}
	}
}
