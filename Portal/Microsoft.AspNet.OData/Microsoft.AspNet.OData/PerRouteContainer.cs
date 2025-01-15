using System;
using System.Collections.Concurrent;
using System.Web.Http;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Extensions;

namespace Microsoft.AspNet.OData
{
	// Token: 0x02000006 RID: 6
	public class PerRouteContainer : PerRouteContainerBase
	{
		// Token: 0x06000019 RID: 25 RVA: 0x0000255F File Offset: 0x0000075F
		public PerRouteContainer(HttpConfiguration configuration)
		{
			this.configuration = configuration;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002570 File Offset: 0x00000770
		protected override IServiceProvider GetContainer(string routeName)
		{
			if (string.IsNullOrEmpty(routeName))
			{
				return this.configuration.GetNonODataRootContainer();
			}
			IServiceProvider serviceProvider;
			if (this.GetRootContainerMappings().TryGetValue(routeName, out serviceProvider))
			{
				return serviceProvider;
			}
			throw Error.InvalidOperation(SRResources.NullContainer, new object[0]);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000025B3 File Offset: 0x000007B3
		protected override void SetContainer(string routeName, IServiceProvider rootContainer)
		{
			if (rootContainer == null)
			{
				throw Error.InvalidOperation(SRResources.NullContainer, new object[0]);
			}
			if (string.IsNullOrEmpty(routeName))
			{
				this.configuration.SetNonODataRootContainer(rootContainer);
				return;
			}
			this.GetRootContainerMappings()[routeName] = rootContainer;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000025EB File Offset: 0x000007EB
		private ConcurrentDictionary<string, IServiceProvider> GetRootContainerMappings()
		{
			return (ConcurrentDictionary<string, IServiceProvider>)this.configuration.Properties.GetOrAdd("Microsoft.AspNet.OData.RootContainerMappingsKey", (object key) => new ConcurrentDictionary<string, IServiceProvider>());
		}

		// Token: 0x04000006 RID: 6
		private const string RootContainerMappingsKey = "Microsoft.AspNet.OData.RootContainerMappingsKey";

		// Token: 0x04000007 RID: 7
		private readonly HttpConfiguration configuration;
	}
}
