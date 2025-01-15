using System;
using System.Linq;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Builder.Conventions
{
	// Token: 0x02000152 RID: 338
	internal class SelfLinksGenerationConvention : INavigationSourceConvention, IConvention
	{
		// Token: 0x06000C4A RID: 3146 RVA: 0x000300F4 File Offset: 0x0002E2F4
		public void Apply(NavigationSourceConfiguration configuration, ODataModelBuilder model)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			EntitySetConfiguration entitySetConfiguration = configuration as EntitySetConfiguration;
			if (entitySetConfiguration != null && entitySetConfiguration.GetFeedSelfLink() == null)
			{
				entitySetConfiguration.HasFeedSelfLink(delegate(ResourceSetContext feedContext)
				{
					string text = feedContext.InternalUrlHelper.CreateODataLink(new ODataPathSegment[]
					{
						new EntitySetSegment(feedContext.EntitySetBase as IEdmEntitySet)
					});
					if (text == null)
					{
						return null;
					}
					return new Uri(text);
				});
			}
			if (configuration.GetIdLink() == null)
			{
				configuration.HasIdLink(new SelfLinkBuilder<Uri>((ResourceContext entityContext) => entityContext.GenerateSelfLink(false), true));
			}
			if (configuration.GetEditLink() == null)
			{
				if (model.DerivedTypes(configuration.EntityType).OfType<EntityTypeConfiguration>().Any((EntityTypeConfiguration e) => e.NavigationProperties.Any<NavigationPropertyConfiguration>()))
				{
					configuration.HasEditLink(new SelfLinkBuilder<Uri>((ResourceContext entityContext) => entityContext.GenerateSelfLink(true), true));
					return;
				}
				configuration.HasEditLink(new SelfLinkBuilder<Uri>((ResourceContext entityContext) => entityContext.GenerateSelfLink(false), true));
			}
		}
	}
}
