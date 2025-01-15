using System;
using System.Collections.Generic;

namespace Microsoft.OData
{
	// Token: 0x020000CE RID: 206
	internal static class ServiceProviderExtensions
	{
		// Token: 0x060007D5 RID: 2005 RVA: 0x00015CFF File Offset: 0x00013EFF
		public static T GetService<T>(this IServiceProvider container)
		{
			return (T)((object)container.GetService(typeof(T)));
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x00015D18 File Offset: 0x00013F18
		public static object GetRequiredService(this IServiceProvider container, Type serviceType)
		{
			object service = container.GetService(serviceType);
			if (service == null)
			{
				throw new ODataException(Strings.ServiceProviderExtensions_NoServiceRegistered(serviceType));
			}
			return service;
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x00015D3D File Offset: 0x00013F3D
		public static T GetRequiredService<T>(this IServiceProvider container)
		{
			return (T)((object)container.GetRequiredService(typeof(T)));
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x00015D54 File Offset: 0x00013F54
		public static IEnumerable<T> GetServices<T>(this IServiceProvider container)
		{
			return container.GetRequiredService<IEnumerable<T>>();
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x00015D5C File Offset: 0x00013F5C
		public static IEnumerable<object> GetServices(this IServiceProvider container, Type serviceType)
		{
			Type type = typeof(IEnumerable).MakeGenericType(new Type[] { serviceType });
			return (IEnumerable<object>)container.GetRequiredService(type);
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x00015D8F File Offset: 0x00013F8F
		public static TService GetServicePrototype<TService>(this IServiceProvider container)
		{
			return container.GetRequiredService<ServicePrototype<TService>>().Instance;
		}
	}
}
