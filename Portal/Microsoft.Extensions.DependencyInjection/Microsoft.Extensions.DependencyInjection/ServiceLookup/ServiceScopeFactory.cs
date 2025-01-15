using System;

namespace Microsoft.Extensions.DependencyInjection.ServiceLookup
{
	// Token: 0x0200001E RID: 30
	internal class ServiceScopeFactory : IServiceScopeFactory
	{
		// Token: 0x060000C0 RID: 192 RVA: 0x00003A91 File Offset: 0x00001C91
		public ServiceScopeFactory(ServiceProvider provider)
		{
			this._provider = provider;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003AA0 File Offset: 0x00001CA0
		public IServiceScope CreateScope()
		{
			return new ServiceScope(new ServiceProvider(this._provider));
		}

		// Token: 0x04000033 RID: 51
		private readonly ServiceProvider _provider;
	}
}
