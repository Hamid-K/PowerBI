using System;
using Microsoft.Owin.Hosting.Services;

namespace Microsoft.Owin.Hosting.ServerFactory
{
	// Token: 0x02000020 RID: 32
	public class ServerFactoryActivator : IServerFactoryActivator
	{
		// Token: 0x06000092 RID: 146 RVA: 0x00003D7C File Offset: 0x00001F7C
		public ServerFactoryActivator(IServiceProvider services)
		{
			this._services = services;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003D8B File Offset: 0x00001F8B
		public virtual object Activate(Type type)
		{
			return ActivatorUtilities.GetServiceOrCreateInstance(this._services, type);
		}

		// Token: 0x04000037 RID: 55
		private readonly IServiceProvider _services;
	}
}
