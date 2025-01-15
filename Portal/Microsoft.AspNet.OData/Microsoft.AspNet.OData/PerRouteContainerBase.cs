using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData
{
	// Token: 0x0200001C RID: 28
	public abstract class PerRouteContainerBase : IPerRouteContainer
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x000041AE File Offset: 0x000023AE
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x000041B6 File Offset: 0x000023B6
		public Func<IContainerBuilder> BuilderFactory { get; set; }

		// Token: 0x060000B3 RID: 179 RVA: 0x000041C0 File Offset: 0x000023C0
		public IServiceProvider CreateODataRootContainer(string routeName, Action<IContainerBuilder> configureAction)
		{
			IServiceProvider serviceProvider = this.CreateODataRootContainer(configureAction);
			this.SetContainer(routeName, serviceProvider);
			return serviceProvider;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000041E0 File Offset: 0x000023E0
		public IServiceProvider CreateODataRootContainer(Action<IContainerBuilder> configureAction)
		{
			IContainerBuilder containerBuilder = this.CreateContainerBuilderWithCoreServices();
			if (configureAction != null)
			{
				configureAction(containerBuilder);
			}
			IServiceProvider serviceProvider = containerBuilder.BuildContainer();
			if (serviceProvider == null)
			{
				throw Error.InvalidOperation(SRResources.NullContainer, new object[0]);
			}
			return serviceProvider;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0000421A File Offset: 0x0000241A
		public bool HasODataRootContainer(string routeName)
		{
			return this.GetContainer(routeName) != null;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004228 File Offset: 0x00002428
		public IServiceProvider GetODataRootContainer(string routeName)
		{
			IServiceProvider container = this.GetContainer(routeName);
			if (container != null)
			{
				return container;
			}
			if (string.IsNullOrEmpty(routeName))
			{
				throw Error.InvalidOperation(SRResources.MissingNonODataContainer, new object[0]);
			}
			throw Error.InvalidOperation(SRResources.MissingODataContainer, new object[] { routeName });
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x0000426F File Offset: 0x0000246F
		internal void SetODataRootContainer(string routeName, IServiceProvider rootContainer)
		{
			this.SetContainer(routeName, rootContainer);
		}

		// Token: 0x060000B8 RID: 184
		protected abstract IServiceProvider GetContainer(string routeName);

		// Token: 0x060000B9 RID: 185
		protected abstract void SetContainer(string routeName, IServiceProvider rootContainer);

		// Token: 0x060000BA RID: 186 RVA: 0x0000427C File Offset: 0x0000247C
		protected IContainerBuilder CreateContainerBuilderWithCoreServices()
		{
			IContainerBuilder containerBuilder;
			if (this.BuilderFactory != null)
			{
				containerBuilder = this.BuilderFactory();
				if (containerBuilder == null)
				{
					throw Error.InvalidOperation(SRResources.NullContainerBuilder, new object[0]);
				}
			}
			else
			{
				containerBuilder = new DefaultContainerBuilder();
			}
			containerBuilder.AddDefaultODataServices();
			containerBuilder.AddService(ServiceLifetime.Singleton, typeof(ODataUriResolver), (IServiceProvider sp) => new UnqualifiedODataUriResolver
			{
				EnableCaseInsensitive = true
			});
			return containerBuilder;
		}
	}
}
