using System;

namespace Microsoft.Extensions.DependencyInjection
{
	// Token: 0x02000004 RID: 4
	public interface IServiceProviderFactory<TContainerBuilder>
	{
		// Token: 0x06000006 RID: 6
		TContainerBuilder CreateBuilder(IServiceCollection services);

		// Token: 0x06000007 RID: 7
		IServiceProvider CreateServiceProvider(TContainerBuilder containerBuilder);
	}
}
