using System;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A73 RID: 6771
	internal class RemoteCurrentTimeServiceFactory : IRemoteServiceFactory
	{
		// Token: 0x0600AAC9 RID: 43721 RVA: 0x00233C1C File Offset: 0x00231E1C
		public IRemoteServiceStub CreateStub(IEngineHost engineHost, IMessenger messenger, BinaryWriter proxyInitArgs)
		{
			CurrentTimeService currentTimeService = engineHost.QueryService<ICurrentTimeService>() as CurrentTimeService;
			if (currentTimeService == null || currentTimeService.BaseUtcNow == null)
			{
				proxyInitArgs.Write(false);
			}
			else
			{
				proxyInitArgs.Write(true);
				proxyInitArgs.WriteDateTime(currentTimeService.BaseUtcNow.Value);
			}
			return EmptyStub.Instance;
		}

		// Token: 0x0600AACA RID: 43722 RVA: 0x00233C74 File Offset: 0x00231E74
		public IRemoteServiceProxy CreateProxy(IEngineHost engineHost, IMessenger messenger, BinaryReader proxyInitArgs)
		{
			DateTime? dateTime = null;
			if (proxyInitArgs.ReadBoolean())
			{
				dateTime = new DateTime?(proxyInitArgs.ReadDateTime());
			}
			return new EngineHostServiceProxy(new SimpleEngineHost<ICurrentTimeService>(new CurrentTimeService(dateTime)));
		}
	}
}
