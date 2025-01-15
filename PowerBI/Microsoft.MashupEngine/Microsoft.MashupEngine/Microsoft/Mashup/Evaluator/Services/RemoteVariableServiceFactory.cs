using System;
using System.IO;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator.Services
{
	// Token: 0x02001D9F RID: 7583
	public sealed class RemoteVariableServiceFactory : IRemoteServiceFactory
	{
		// Token: 0x0600BC19 RID: 48153 RVA: 0x002611E9 File Offset: 0x0025F3E9
		public IRemoteServiceStub CreateStub(IEngineHost engineHost, IMessenger messenger, BinaryWriter proxyInitArgs)
		{
			return new RemoteVariableServiceFactory.Stub(new RemoteVariableService(messenger, engineHost, engineHost.QueryService<IVariableService>()));
		}

		// Token: 0x0600BC1A RID: 48154 RVA: 0x002611FD File Offset: 0x0025F3FD
		public IRemoteServiceProxy CreateProxy(IEngineHost engineHost, IMessenger messenger, BinaryReader proxyInitArgs)
		{
			return new RemoteVariableServiceFactory.Proxy(new RemoteVariableService(messenger, engineHost, new VariableService()));
		}

		// Token: 0x02001DA0 RID: 7584
		private sealed class Proxy : IRemoteServiceProxy, IEngineHost, IDisposable
		{
			// Token: 0x0600BC1C RID: 48156 RVA: 0x00261210 File Offset: 0x0025F410
			public Proxy(RemoteVariableService service)
			{
				this.service = service;
			}

			// Token: 0x0600BC1D RID: 48157 RVA: 0x00261220 File Offset: 0x0025F420
			T IEngineHost.QueryService<T>()
			{
				if (typeof(T) == typeof(IVariableService))
				{
					return (T)((object)this.service);
				}
				return default(T);
			}

			// Token: 0x0600BC1E RID: 48158 RVA: 0x0026125D File Offset: 0x0025F45D
			public void Dispose()
			{
				if (this.service != null)
				{
					this.service.Dispose();
					this.service = null;
				}
			}

			// Token: 0x04005FBE RID: 24510
			private RemoteVariableService service;
		}

		// Token: 0x02001DA1 RID: 7585
		private sealed class Stub : IRemoteServiceStub, IDisposable
		{
			// Token: 0x0600BC1F RID: 48159 RVA: 0x00261279 File Offset: 0x0025F479
			public Stub(RemoteVariableService service)
			{
				this.service = service;
			}

			// Token: 0x0600BC20 RID: 48160 RVA: 0x00261288 File Offset: 0x0025F488
			public void Dispose()
			{
				if (this.service != null)
				{
					this.service.Dispose();
					this.service = null;
				}
			}

			// Token: 0x04005FBF RID: 24511
			private RemoteVariableService service;
		}
	}
}
