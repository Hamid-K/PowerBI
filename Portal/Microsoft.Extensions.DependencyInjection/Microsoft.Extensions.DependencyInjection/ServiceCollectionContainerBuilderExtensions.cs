using System;

namespace Microsoft.Extensions.DependencyInjection
{
	// Token: 0x02000005 RID: 5
	public static class ServiceCollectionContainerBuilderExtensions
	{
		// Token: 0x06000014 RID: 20 RVA: 0x0000215C File Offset: 0x0000035C
		public static IServiceProvider BuildServiceProvider(this IServiceCollection services)
		{
			return services.BuildServiceProvider(false);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002165 File Offset: 0x00000365
		public static IServiceProvider BuildServiceProvider(this IServiceCollection services, bool validateScopes)
		{
			return new ServiceProvider(services, validateScopes);
		}
	}
}
