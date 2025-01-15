using System;
using System.Collections.Generic;
using System.Linq;
using dotless.Core.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
	// Token: 0x02000002 RID: 2
	public static class ServiceCollectionExtensions
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static IServiceCollection Decorate<TService, TDecorator>(this IServiceCollection services) where TDecorator : TService
		{
			if (services == null)
			{
				throw new ArgumentNullException("services");
			}
			return services.DecorateDescriptors(typeof(TService), (ServiceDescriptor x) => x.Decorate(typeof(TDecorator)));
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000208F File Offset: 0x0000028F
		private static IServiceCollection DecorateDescriptors(this IServiceCollection services, Type serviceType, Func<ServiceDescriptor, ServiceDescriptor> decorator)
		{
			if (services.TryDecorateDescriptors(serviceType, decorator))
			{
				return services;
			}
			throw new MissingTypeRegistrationException(serviceType);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020A4 File Offset: 0x000002A4
		private static bool TryDecorateDescriptors(this IServiceCollection services, Type serviceType, Func<ServiceDescriptor, ServiceDescriptor> decorator)
		{
			ICollection<ServiceDescriptor> collection;
			if (!services.TryGetDescriptors(serviceType, out collection))
			{
				return false;
			}
			foreach (ServiceDescriptor serviceDescriptor in collection)
			{
				int num = services.IndexOf(serviceDescriptor);
				services.Insert(num, decorator(serviceDescriptor));
				services.Remove(serviceDescriptor);
			}
			return true;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002114 File Offset: 0x00000314
		private static bool TryGetDescriptors(this IServiceCollection services, Type serviceType, out ICollection<ServiceDescriptor> descriptors)
		{
			ICollection<ServiceDescriptor> collection;
			descriptors = (collection = services.Where((ServiceDescriptor service) => service.ServiceType == serviceType).ToArray<ServiceDescriptor>());
			return collection.Any<ServiceDescriptor>();
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002150 File Offset: 0x00000350
		private static ServiceDescriptor Decorate(this ServiceDescriptor descriptor, Type decoratorType)
		{
			return descriptor.WithFactory((IServiceProvider provider) => provider.CreateInstance(decoratorType, new object[] { provider.GetInstance(descriptor) }));
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002188 File Offset: 0x00000388
		private static ServiceDescriptor WithFactory(this ServiceDescriptor descriptor, Func<IServiceProvider, object> factory)
		{
			return ServiceDescriptor.Describe(descriptor.ServiceType, factory, descriptor.Lifetime);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000219C File Offset: 0x0000039C
		private static object GetInstance(this IServiceProvider provider, ServiceDescriptor descriptor)
		{
			if (descriptor.ImplementationInstance != null)
			{
				return descriptor.ImplementationInstance;
			}
			if (descriptor.ImplementationType != null)
			{
				return provider.GetServiceOrCreateInstance(descriptor.ImplementationType);
			}
			return descriptor.ImplementationFactory(provider);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021D4 File Offset: 0x000003D4
		private static object GetServiceOrCreateInstance(this IServiceProvider provider, Type type)
		{
			return ActivatorUtilities.GetServiceOrCreateInstance(provider, type);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021DD File Offset: 0x000003DD
		private static object CreateInstance(this IServiceProvider provider, Type type, params object[] arguments)
		{
			return ActivatorUtilities.CreateInstance(provider, type, arguments);
		}
	}
}
