using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Json;
using Microsoft.OData.UriParser;

namespace Microsoft.OData
{
	// Token: 0x02000006 RID: 6
	public static class ContainerBuilderExtensions
	{
		// Token: 0x06000024 RID: 36 RVA: 0x000024F9 File Offset: 0x000006F9
		public static IContainerBuilder AddService<TService, TImplementation>(this IContainerBuilder builder, ServiceLifetime lifetime) where TService : class where TImplementation : class, TService
		{
			return builder.AddService(lifetime, typeof(TService), typeof(TImplementation));
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002516 File Offset: 0x00000716
		public static IContainerBuilder AddService(this IContainerBuilder builder, ServiceLifetime lifetime, Type serviceType)
		{
			return builder.AddService(lifetime, serviceType, serviceType);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002521 File Offset: 0x00000721
		public static IContainerBuilder AddService<TService>(this IContainerBuilder builder, ServiceLifetime lifetime) where TService : class
		{
			return builder.AddService(lifetime, typeof(TService));
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002534 File Offset: 0x00000734
		public static IContainerBuilder AddService<TService>(this IContainerBuilder builder, ServiceLifetime lifetime, Func<IServiceProvider, TService> implementationFactory) where TService : class
		{
			return builder.AddService(lifetime, typeof(TService), implementationFactory);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002548 File Offset: 0x00000748
		public static IContainerBuilder AddServicePrototype<TService>(this IContainerBuilder builder, TService instance)
		{
			return builder.AddService(ServiceLifetime.Singleton, (IServiceProvider sp) => new ServicePrototype<TService>(instance));
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002575 File Offset: 0x00000775
		public static IContainerBuilder AddDefaultODataServices(this IContainerBuilder builder)
		{
			return builder.AddDefaultODataServices(ODataVersion.V4);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002580 File Offset: 0x00000780
		public static IContainerBuilder AddDefaultODataServices(this IContainerBuilder builder, ODataVersion odataVersion)
		{
			builder.AddService(ServiceLifetime.Singleton);
			builder.AddService(ServiceLifetime.Singleton, (IServiceProvider sp) => new DefaultJsonWriterFactory());
			builder.AddService(ServiceLifetime.Singleton, (IServiceProvider sp) => ODataMediaTypeResolver.GetMediaTypeResolver(null));
			builder.AddService(ServiceLifetime.Scoped);
			builder.AddServicePrototype(new ODataMessageReaderSettings());
			builder.AddService(ServiceLifetime.Scoped, (IServiceProvider sp) => sp.GetServicePrototype<ODataMessageReaderSettings>().Clone());
			builder.AddServicePrototype(new ODataMessageWriterSettings());
			builder.AddService(ServiceLifetime.Scoped, (IServiceProvider sp) => sp.GetServicePrototype<ODataMessageWriterSettings>().Clone());
			builder.AddService(ServiceLifetime.Singleton, (IServiceProvider sp) => ODataPayloadValueConverter.GetPayloadValueConverter(null));
			builder.AddService(ServiceLifetime.Singleton, (IServiceProvider sp) => EdmCoreModel.Instance);
			builder.AddService(ServiceLifetime.Singleton, (IServiceProvider sp) => ODataUriResolver.GetUriResolver(null));
			builder.AddService(ServiceLifetime.Scoped);
			builder.AddService(ServiceLifetime.Scoped);
			builder.AddServicePrototype(new ODataSimplifiedOptions(new ODataVersion?(odataVersion)));
			builder.AddService(ServiceLifetime.Scoped, (IServiceProvider sp) => sp.GetServicePrototype<ODataSimplifiedOptions>().Clone());
			return builder;
		}
	}
}
