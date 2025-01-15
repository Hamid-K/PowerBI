using System;

namespace Microsoft.Extensions.DependencyInjection
{
	// Token: 0x02000009 RID: 9
	public static class ServiceCollectionServiceExtensions
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002088 File Offset: 0x00000288
		public static IServiceCollection AddTransient(this IServiceCollection services, Type serviceType, Type implementationType)
		{
			if (services == null)
			{
				throw new ArgumentNullException("services");
			}
			if (serviceType == null)
			{
				throw new ArgumentNullException("serviceType");
			}
			if (implementationType == null)
			{
				throw new ArgumentNullException("implementationType");
			}
			return ServiceCollectionServiceExtensions.Add(services, serviceType, implementationType, ServiceLifetime.Transient);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000020BD File Offset: 0x000002BD
		public static IServiceCollection AddTransient(this IServiceCollection services, Type serviceType, Func<IServiceProvider, object> implementationFactory)
		{
			if (services == null)
			{
				throw new ArgumentNullException("services");
			}
			if (serviceType == null)
			{
				throw new ArgumentNullException("serviceType");
			}
			if (implementationFactory == null)
			{
				throw new ArgumentNullException("implementationFactory");
			}
			return ServiceCollectionServiceExtensions.Add(services, serviceType, implementationFactory, ServiceLifetime.Transient);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000020F2 File Offset: 0x000002F2
		public static IServiceCollection AddTransient<TService, TImplementation>(this IServiceCollection services) where TService : class where TImplementation : class, TService
		{
			if (services == null)
			{
				throw new ArgumentNullException("services");
			}
			return services.AddTransient(typeof(TService), typeof(TImplementation));
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000211C File Offset: 0x0000031C
		public static IServiceCollection AddTransient(this IServiceCollection services, Type serviceType)
		{
			if (services == null)
			{
				throw new ArgumentNullException("services");
			}
			if (serviceType == null)
			{
				throw new ArgumentNullException("serviceType");
			}
			return services.AddTransient(serviceType, serviceType);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002142 File Offset: 0x00000342
		public static IServiceCollection AddTransient<TService>(this IServiceCollection services) where TService : class
		{
			if (services == null)
			{
				throw new ArgumentNullException("services");
			}
			return services.AddTransient(typeof(TService));
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002162 File Offset: 0x00000362
		public static IServiceCollection AddTransient<TService>(this IServiceCollection services, Func<IServiceProvider, TService> implementationFactory) where TService : class
		{
			if (services == null)
			{
				throw new ArgumentNullException("services");
			}
			if (implementationFactory == null)
			{
				throw new ArgumentNullException("implementationFactory");
			}
			return services.AddTransient(typeof(TService), implementationFactory);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002191 File Offset: 0x00000391
		public static IServiceCollection AddTransient<TService, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TImplementation> implementationFactory) where TService : class where TImplementation : class, TService
		{
			if (services == null)
			{
				throw new ArgumentNullException("services");
			}
			if (implementationFactory == null)
			{
				throw new ArgumentNullException("implementationFactory");
			}
			return services.AddTransient(typeof(TService), implementationFactory);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000021C0 File Offset: 0x000003C0
		public static IServiceCollection AddScoped(this IServiceCollection services, Type serviceType, Type implementationType)
		{
			if (services == null)
			{
				throw new ArgumentNullException("services");
			}
			if (serviceType == null)
			{
				throw new ArgumentNullException("serviceType");
			}
			if (implementationType == null)
			{
				throw new ArgumentNullException("implementationType");
			}
			return ServiceCollectionServiceExtensions.Add(services, serviceType, implementationType, ServiceLifetime.Scoped);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000021F5 File Offset: 0x000003F5
		public static IServiceCollection AddScoped(this IServiceCollection services, Type serviceType, Func<IServiceProvider, object> implementationFactory)
		{
			if (services == null)
			{
				throw new ArgumentNullException("services");
			}
			if (serviceType == null)
			{
				throw new ArgumentNullException("serviceType");
			}
			if (implementationFactory == null)
			{
				throw new ArgumentNullException("implementationFactory");
			}
			return ServiceCollectionServiceExtensions.Add(services, serviceType, implementationFactory, ServiceLifetime.Scoped);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000222A File Offset: 0x0000042A
		public static IServiceCollection AddScoped<TService, TImplementation>(this IServiceCollection services) where TService : class where TImplementation : class, TService
		{
			if (services == null)
			{
				throw new ArgumentNullException("services");
			}
			return services.AddScoped(typeof(TService), typeof(TImplementation));
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002254 File Offset: 0x00000454
		public static IServiceCollection AddScoped(this IServiceCollection services, Type serviceType)
		{
			if (services == null)
			{
				throw new ArgumentNullException("services");
			}
			if (serviceType == null)
			{
				throw new ArgumentNullException("serviceType");
			}
			return services.AddScoped(serviceType, serviceType);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000227A File Offset: 0x0000047A
		public static IServiceCollection AddScoped<TService>(this IServiceCollection services) where TService : class
		{
			if (services == null)
			{
				throw new ArgumentNullException("services");
			}
			return services.AddScoped(typeof(TService));
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000229A File Offset: 0x0000049A
		public static IServiceCollection AddScoped<TService>(this IServiceCollection services, Func<IServiceProvider, TService> implementationFactory) where TService : class
		{
			if (services == null)
			{
				throw new ArgumentNullException("services");
			}
			if (implementationFactory == null)
			{
				throw new ArgumentNullException("implementationFactory");
			}
			return services.AddScoped(typeof(TService), implementationFactory);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000022C9 File Offset: 0x000004C9
		public static IServiceCollection AddScoped<TService, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TImplementation> implementationFactory) where TService : class where TImplementation : class, TService
		{
			if (services == null)
			{
				throw new ArgumentNullException("services");
			}
			if (implementationFactory == null)
			{
				throw new ArgumentNullException("implementationFactory");
			}
			return services.AddScoped(typeof(TService), implementationFactory);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000022F8 File Offset: 0x000004F8
		public static IServiceCollection AddSingleton(this IServiceCollection services, Type serviceType, Type implementationType)
		{
			if (services == null)
			{
				throw new ArgumentNullException("services");
			}
			if (serviceType == null)
			{
				throw new ArgumentNullException("serviceType");
			}
			if (implementationType == null)
			{
				throw new ArgumentNullException("implementationType");
			}
			return ServiceCollectionServiceExtensions.Add(services, serviceType, implementationType, ServiceLifetime.Singleton);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000232D File Offset: 0x0000052D
		public static IServiceCollection AddSingleton(this IServiceCollection services, Type serviceType, Func<IServiceProvider, object> implementationFactory)
		{
			if (services == null)
			{
				throw new ArgumentNullException("services");
			}
			if (serviceType == null)
			{
				throw new ArgumentNullException("serviceType");
			}
			if (implementationFactory == null)
			{
				throw new ArgumentNullException("implementationFactory");
			}
			return ServiceCollectionServiceExtensions.Add(services, serviceType, implementationFactory, ServiceLifetime.Singleton);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002362 File Offset: 0x00000562
		public static IServiceCollection AddSingleton<TService, TImplementation>(this IServiceCollection services) where TService : class where TImplementation : class, TService
		{
			if (services == null)
			{
				throw new ArgumentNullException("services");
			}
			return services.AddSingleton(typeof(TService), typeof(TImplementation));
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000238C File Offset: 0x0000058C
		public static IServiceCollection AddSingleton(this IServiceCollection services, Type serviceType)
		{
			if (services == null)
			{
				throw new ArgumentNullException("services");
			}
			if (serviceType == null)
			{
				throw new ArgumentNullException("serviceType");
			}
			return services.AddSingleton(serviceType, serviceType);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000023B2 File Offset: 0x000005B2
		public static IServiceCollection AddSingleton<TService>(this IServiceCollection services) where TService : class
		{
			if (services == null)
			{
				throw new ArgumentNullException("services");
			}
			return services.AddSingleton(typeof(TService));
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000023D2 File Offset: 0x000005D2
		public static IServiceCollection AddSingleton<TService>(this IServiceCollection services, Func<IServiceProvider, TService> implementationFactory) where TService : class
		{
			if (services == null)
			{
				throw new ArgumentNullException("services");
			}
			if (implementationFactory == null)
			{
				throw new ArgumentNullException("implementationFactory");
			}
			return services.AddSingleton(typeof(TService), implementationFactory);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002401 File Offset: 0x00000601
		public static IServiceCollection AddSingleton<TService, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TImplementation> implementationFactory) where TService : class where TImplementation : class, TService
		{
			if (services == null)
			{
				throw new ArgumentNullException("services");
			}
			if (implementationFactory == null)
			{
				throw new ArgumentNullException("implementationFactory");
			}
			return services.AddSingleton(typeof(TService), implementationFactory);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002430 File Offset: 0x00000630
		public static IServiceCollection AddSingleton(this IServiceCollection services, Type serviceType, object implementationInstance)
		{
			if (services == null)
			{
				throw new ArgumentNullException("services");
			}
			if (serviceType == null)
			{
				throw new ArgumentNullException("serviceType");
			}
			if (implementationInstance == null)
			{
				throw new ArgumentNullException("implementationInstance");
			}
			ServiceDescriptor serviceDescriptor = new ServiceDescriptor(serviceType, implementationInstance);
			services.Add(serviceDescriptor);
			return services;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002477 File Offset: 0x00000677
		public static IServiceCollection AddSingleton<TService>(this IServiceCollection services, TService implementationInstance) where TService : class
		{
			if (services == null)
			{
				throw new ArgumentNullException("services");
			}
			if (implementationInstance == null)
			{
				throw new ArgumentNullException("implementationInstance");
			}
			return services.AddSingleton(typeof(TService), implementationInstance);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000024B0 File Offset: 0x000006B0
		private static IServiceCollection Add(IServiceCollection collection, Type serviceType, Type implementationType, ServiceLifetime lifetime)
		{
			ServiceDescriptor serviceDescriptor = new ServiceDescriptor(serviceType, implementationType, lifetime);
			collection.Add(serviceDescriptor);
			return collection;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000024D0 File Offset: 0x000006D0
		private static IServiceCollection Add(IServiceCollection collection, Type serviceType, Func<IServiceProvider, object> implementationFactory, ServiceLifetime lifetime)
		{
			ServiceDescriptor serviceDescriptor = new ServiceDescriptor(serviceType, implementationFactory, lifetime);
			collection.Add(serviceDescriptor);
			return collection;
		}
	}
}
