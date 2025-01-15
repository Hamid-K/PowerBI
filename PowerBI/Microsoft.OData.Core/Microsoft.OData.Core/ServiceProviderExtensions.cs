using System;
using System.Collections.Generic;

namespace Microsoft.OData
{
	// Token: 0x020000CE RID: 206
	internal static class ServiceProviderExtensions
	{
		// Token: 0x0600098D RID: 2445 RVA: 0x000182B7 File Offset: 0x000164B7
		public static T GetService<T>(this IServiceProvider container)
		{
			return (T)((object)container.GetService(typeof(T)));
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x000182D0 File Offset: 0x000164D0
		public static object GetRequiredService(this IServiceProvider container, Type serviceType)
		{
			object service = container.GetService(serviceType);
			if (service == null)
			{
				throw new ODataException(Strings.ServiceProviderExtensions_NoServiceRegistered(serviceType));
			}
			return service;
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x000182F5 File Offset: 0x000164F5
		public static T GetRequiredService<T>(this IServiceProvider container)
		{
			return (T)((object)container.GetRequiredService(typeof(T)));
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0001830C File Offset: 0x0001650C
		public static IEnumerable<T> GetServices<T>(this IServiceProvider container)
		{
			return container.GetRequiredService<IEnumerable<T>>();
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x00018314 File Offset: 0x00016514
		public static IEnumerable<object> GetServices(this IServiceProvider container, Type serviceType)
		{
			Type type = typeof(IEnumerable<>).MakeGenericType(new Type[] { serviceType });
			return (IEnumerable<object>)container.GetRequiredService(type);
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x00018347 File Offset: 0x00016547
		public static TService GetServicePrototype<TService>(this IServiceProvider container)
		{
			return container.GetRequiredService<ServicePrototype<TService>>().Instance;
		}
	}
}
