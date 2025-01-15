using System;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001AB4 RID: 6836
	internal class RemoteMipService : IRemoteServiceFactory
	{
		// Token: 0x0600AC0F RID: 44047 RVA: 0x002367F4 File Offset: 0x002349F4
		public IRemoteServiceStub CreateStub(IEngineHost engineHost, IMessenger messenger, BinaryWriter proxyInitArgs)
		{
			IMipConfigService mipConfigService = engineHost.QueryService<IMipConfigService>();
			if (mipConfigService == null)
			{
				proxyInitArgs.WriteNullableString(null);
			}
			else
			{
				proxyInitArgs.WriteNullableString(mipConfigService.SdkPath);
				proxyInitArgs.Write(mipConfigService.ApplicationId);
			}
			return EmptyStub.Instance;
		}

		// Token: 0x0600AC10 RID: 44048 RVA: 0x00236834 File Offset: 0x00234A34
		public IRemoteServiceProxy CreateProxy(IEngineHost engineHost, IMessenger messenger, BinaryReader proxyInitArgs)
		{
			string text = proxyInitArgs.ReadNullableString();
			if (text != null)
			{
				string text2 = proxyInitArgs.ReadString();
				MipService.Initialize(text, text2);
				return new RemoteMipService.Proxy(engineHost);
			}
			return EmptyProxy.Instance;
		}

		// Token: 0x02001AB5 RID: 6837
		private class Proxy : IRemoteServiceProxy, IEngineHost, IDisposable
		{
			// Token: 0x0600AC12 RID: 44050 RVA: 0x00236865 File Offset: 0x00234A65
			public Proxy(IEngineHost engineHost)
			{
				this.engineHost = new MutableEngineHost();
				MipService.TryAddService(engineHost, this.engineHost);
			}

			// Token: 0x0600AC13 RID: 44051 RVA: 0x00236888 File Offset: 0x00234A88
			public T QueryService<T>() where T : class
			{
				if (typeof(T) == typeof(IMipService) || typeof(T) == typeof(IMipConfigService))
				{
					return this.engineHost.QueryService<T>();
				}
				return default(T);
			}

			// Token: 0x0600AC14 RID: 44052 RVA: 0x002368E0 File Offset: 0x00234AE0
			public void Dispose()
			{
				IDisposable disposable = this.engineHost.QueryService<IMipService>() as IDisposable;
				if (disposable == null)
				{
					return;
				}
				disposable.Dispose();
			}

			// Token: 0x0400590E RID: 22798
			private readonly MutableEngineHost engineHost;
		}
	}
}
