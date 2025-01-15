using System;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001B1A RID: 6938
	internal class RemoteTempDirectoryServiceFactory : IRemoteServiceFactory
	{
		// Token: 0x0600ADE9 RID: 44521 RVA: 0x0023A87C File Offset: 0x00238A7C
		public IRemoteServiceStub CreateStub(IEngineHost engineHost, IMessenger messenger, BinaryWriter proxyInitArgs)
		{
			ITempDirectoryConfig tempDirectoryConfig = engineHost.QueryService<ITempDirectoryConfig>();
			proxyInitArgs.WriteString(tempDirectoryConfig.TempDirectoryPath);
			proxyInitArgs.WriteInt64(tempDirectoryConfig.TempDirectoryMaxSize);
			return EmptyStub.Instance;
		}

		// Token: 0x0600ADEA RID: 44522 RVA: 0x0023A8B0 File Offset: 0x00238AB0
		public IRemoteServiceProxy CreateProxy(IEngineHost engineHost, IMessenger messenger, BinaryReader proxyInitArgs)
		{
			string text = proxyInitArgs.ReadString();
			long num = proxyInitArgs.ReadInt64();
			TempDirectoryConfig tempDirectoryConfig = new TempDirectoryConfig(text, num);
			ITempDirectoryService tempDirectoryService = new EvaluationTempDirectory(tempDirectoryConfig, engineHost.GetEvaluationConstants());
			return new RemoteTempDirectoryServiceFactory.Proxy(tempDirectoryConfig, tempDirectoryService);
		}

		// Token: 0x02001B1B RID: 6939
		private sealed class Proxy : IRemoteServiceProxy, IEngineHost, IDisposable
		{
			// Token: 0x0600ADEC RID: 44524 RVA: 0x0023A8E3 File Offset: 0x00238AE3
			public Proxy(ITempDirectoryConfig tempDirectoryConfig, ITempDirectoryService tempDirectoryService)
			{
				this.tempDirectoryConfig = tempDirectoryConfig;
				this.tempDirectoryService = tempDirectoryService;
			}

			// Token: 0x0600ADED RID: 44525 RVA: 0x0023A8FC File Offset: 0x00238AFC
			T IEngineHost.QueryService<T>()
			{
				if (typeof(T) == typeof(ITempDirectoryService))
				{
					return (T)((object)this.tempDirectoryService);
				}
				if (typeof(T) == typeof(ITempDirectoryConfig))
				{
					return (T)((object)this.tempDirectoryConfig);
				}
				return default(T);
			}

			// Token: 0x0600ADEE RID: 44526 RVA: 0x0000336E File Offset: 0x0000156E
			public void Dispose()
			{
			}

			// Token: 0x040059BE RID: 22974
			private ITempDirectoryConfig tempDirectoryConfig;

			// Token: 0x040059BF RID: 22975
			private ITempDirectoryService tempDirectoryService;
		}
	}
}
