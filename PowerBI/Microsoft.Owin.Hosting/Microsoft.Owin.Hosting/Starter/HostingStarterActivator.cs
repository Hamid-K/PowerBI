using System;
using Microsoft.Owin.Hosting.Services;

namespace Microsoft.Owin.Hosting.Starter
{
	// Token: 0x02000013 RID: 19
	public class HostingStarterActivator : IHostingStarterActivator
	{
		// Token: 0x06000065 RID: 101 RVA: 0x000035EE File Offset: 0x000017EE
		public HostingStarterActivator(IServiceProvider services)
		{
			this._services = services;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003600 File Offset: 0x00001800
		public virtual IHostingStarter Activate(Type type)
		{
			object starter = ActivatorUtilities.GetServiceOrCreateInstance(this._services, type);
			return (IHostingStarter)starter;
		}

		// Token: 0x04000031 RID: 49
		private readonly IServiceProvider _services;
	}
}
