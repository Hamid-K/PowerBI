using System;
using System.Net.Http;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Formatter.Deserialization;
using Microsoft.AspNet.OData.Formatter.Serialization;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Query.Expressions;
using Microsoft.AspNet.OData.Query.Validators;
using Microsoft.AspNet.OData.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData;

namespace Microsoft.AspNet.OData.Extensions
{
	// Token: 0x020001C9 RID: 457
	internal static class ContainerBuilderExtensions
	{
		// Token: 0x06000F21 RID: 3873 RVA: 0x0003E3F4 File Offset: 0x0003C5F4
		public static IContainerBuilder AddDefaultWebApiServices(this IContainerBuilder builder)
		{
			if (builder == null)
			{
				throw Error.ArgumentNull("builder");
			}
			builder.AddService(ServiceLifetime.Singleton);
			builder.AddServicePrototype(new ODataMessageReaderSettings
			{
				EnableMessageStreamDisposal = false,
				MessageQuotas = new ODataMessageQuotas
				{
					MaxReceivedMessageSize = long.MaxValue
				}
			});
			builder.AddServicePrototype(new ODataMessageWriterSettings
			{
				EnableMessageStreamDisposal = false,
				MessageQuotas = new ODataMessageQuotas
				{
					MaxReceivedMessageSize = long.MaxValue
				}
			});
			builder.AddService(ServiceLifetime.Singleton);
			builder.AddService(ServiceLifetime.Scoped);
			builder.AddService(ServiceLifetime.Singleton);
			builder.AddService(ServiceLifetime.Singleton);
			builder.AddService(ServiceLifetime.Singleton);
			builder.AddService(ServiceLifetime.Singleton);
			builder.AddService(ServiceLifetime.Singleton);
			builder.AddService(ServiceLifetime.Singleton);
			builder.AddService(ServiceLifetime.Singleton);
			builder.AddService(ServiceLifetime.Singleton);
			builder.AddService(ServiceLifetime.Singleton);
			builder.AddService(ServiceLifetime.Singleton);
			builder.AddService(ServiceLifetime.Singleton);
			builder.AddService(ServiceLifetime.Singleton);
			builder.AddService(ServiceLifetime.Singleton);
			builder.AddService(ServiceLifetime.Singleton);
			builder.AddService(ServiceLifetime.Singleton);
			builder.AddService(ServiceLifetime.Singleton);
			builder.AddService(ServiceLifetime.Singleton);
			builder.AddService(ServiceLifetime.Singleton);
			builder.AddService(ServiceLifetime.Singleton);
			builder.AddService(ServiceLifetime.Singleton);
			builder.AddService(ServiceLifetime.Singleton);
			builder.AddService(ServiceLifetime.Singleton);
			builder.AddService(ServiceLifetime.Singleton);
			builder.AddService(ServiceLifetime.Singleton);
			builder.AddService(ServiceLifetime.Singleton);
			builder.AddService(ServiceLifetime.Singleton);
			builder.AddService(ServiceLifetime.Singleton);
			builder.AddService(ServiceLifetime.Singleton);
			builder.AddService(ServiceLifetime.Scoped);
			builder.AddService(ServiceLifetime.Transient);
			builder.AddService(ServiceLifetime.Scoped);
			builder.AddService(ServiceLifetime.Scoped, (IServiceProvider sp) => ServiceProviderServiceExtensions.GetRequiredService<HttpRequestScope>(sp).HttpRequest);
			return builder;
		}
	}
}
