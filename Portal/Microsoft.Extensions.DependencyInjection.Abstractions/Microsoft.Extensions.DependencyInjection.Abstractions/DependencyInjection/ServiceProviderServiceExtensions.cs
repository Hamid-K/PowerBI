using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection.Abstractions;

namespace Microsoft.Extensions.DependencyInjection
{
	// Token: 0x0200000C RID: 12
	public static class ServiceProviderServiceExtensions
	{
		// Token: 0x06000046 RID: 70 RVA: 0x00002825 File Offset: 0x00000A25
		public static T GetService<T>(this IServiceProvider provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			return (T)((object)provider.GetService(typeof(T)));
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0000284C File Offset: 0x00000A4C
		public static object GetRequiredService(this IServiceProvider provider, Type serviceType)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (serviceType == null)
			{
				throw new ArgumentNullException("serviceType");
			}
			ISupportRequiredService supportRequiredService = provider as ISupportRequiredService;
			if (supportRequiredService != null)
			{
				return supportRequiredService.GetRequiredService(serviceType);
			}
			object service = provider.GetService(serviceType);
			if (service == null)
			{
				throw new InvalidOperationException(Resources.FormatNoServiceRegistered(serviceType));
			}
			return service;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000289D File Offset: 0x00000A9D
		public static T GetRequiredService<T>(this IServiceProvider provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			return (T)((object)provider.GetRequiredService(typeof(T)));
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000028C2 File Offset: 0x00000AC2
		public static IEnumerable<T> GetServices<T>(this IServiceProvider provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			return provider.GetRequiredService<IEnumerable<T>>();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000028D8 File Offset: 0x00000AD8
		public static IEnumerable<object> GetServices(this IServiceProvider provider, Type serviceType)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (serviceType == null)
			{
				throw new ArgumentNullException("serviceType");
			}
			Type type = typeof(IEnumerable<>).MakeGenericType(new Type[] { serviceType });
			return (IEnumerable<object>)provider.GetRequiredService(type);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002927 File Offset: 0x00000B27
		public static IServiceScope CreateScope(this IServiceProvider provider)
		{
			return provider.GetRequiredService<IServiceScopeFactory>().CreateScope();
		}
	}
}
