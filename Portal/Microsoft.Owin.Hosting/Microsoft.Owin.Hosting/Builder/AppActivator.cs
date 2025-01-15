using System;
using Microsoft.Owin.Hosting.Services;

namespace Microsoft.Owin.Hosting.Builder
{
	// Token: 0x0200002A RID: 42
	public class AppActivator : IAppActivator
	{
		// Token: 0x060000C7 RID: 199 RVA: 0x00004AC0 File Offset: 0x00002CC0
		public AppActivator(IServiceProvider services)
		{
			this._services = services;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004ACF File Offset: 0x00002CCF
		public virtual object Activate(Type type)
		{
			return ActivatorUtilities.GetServiceOrCreateInstance(this._services, type);
		}

		// Token: 0x0400004A RID: 74
		private readonly IServiceProvider _services;
	}
}
