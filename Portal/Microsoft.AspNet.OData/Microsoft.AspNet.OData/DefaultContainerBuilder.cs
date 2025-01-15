using System;
using System.Reflection;
using Microsoft.AspNet.OData.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData;

namespace Microsoft.AspNet.OData
{
	// Token: 0x0200001F RID: 31
	public class DefaultContainerBuilder : IContainerBuilder
	{
		// Token: 0x060000BF RID: 191 RVA: 0x0000437C File Offset: 0x0000257C
		public virtual IContainerBuilder AddService(ServiceLifetime lifetime, Type serviceType, Type implementationType)
		{
			if (serviceType == null)
			{
				throw Error.ArgumentNull("serviceType");
			}
			if (implementationType == null)
			{
				throw Error.ArgumentNull("implementationType");
			}
			this.services.Add(new ServiceDescriptor(serviceType, implementationType, DefaultContainerBuilder.TranslateServiceLifetime(lifetime)));
			return this;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000043CA File Offset: 0x000025CA
		public IContainerBuilder AddService(ServiceLifetime lifetime, Type serviceType, Func<IServiceProvider, object> implementationFactory)
		{
			if (serviceType == null)
			{
				throw Error.ArgumentNull("serviceType");
			}
			if (implementationFactory == null)
			{
				throw Error.ArgumentNull("implementationFactory");
			}
			this.services.Add(new ServiceDescriptor(serviceType, implementationFactory, DefaultContainerBuilder.TranslateServiceLifetime(lifetime)));
			return this;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004408 File Offset: 0x00002608
		public virtual IServiceProvider BuildContainer()
		{
			return (IServiceProvider)this.services.GetType().GetTypeInfo().Assembly.GetType(typeof(ServiceCollectionContainerBuilderExtensions).GetTypeInfo().FullName).GetTypeInfo().GetMethod("BuildServiceProvider", new Type[] { typeof(IServiceCollection) })
				.Invoke(null, new object[] { this.services });
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000447F File Offset: 0x0000267F
		private static ServiceLifetime TranslateServiceLifetime(ServiceLifetime lifetime)
		{
			if (lifetime == ServiceLifetime.Singleton)
			{
				return 0;
			}
			if (lifetime == ServiceLifetime.Scoped)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x0400002B RID: 43
		private readonly IServiceCollection services = new ServiceCollection();
	}
}
