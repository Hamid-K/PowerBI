using System;

namespace Microsoft.Extensions.DependencyInjection
{
	// Token: 0x02000003 RID: 3
	public class DefaultServiceProviderFactory : IServiceProviderFactory<IServiceCollection>
	{
		// Token: 0x06000002 RID: 2 RVA: 0x0000207E File Offset: 0x0000027E
		public IServiceCollection CreateBuilder(IServiceCollection services)
		{
			return services;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002081 File Offset: 0x00000281
		public IServiceProvider CreateServiceProvider(IServiceCollection containerBuilder)
		{
			return containerBuilder.BuildServiceProvider();
		}
	}
}
