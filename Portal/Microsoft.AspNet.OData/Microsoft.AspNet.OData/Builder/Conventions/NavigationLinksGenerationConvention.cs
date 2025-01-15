using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Builder.Conventions
{
	// Token: 0x02000151 RID: 337
	internal class NavigationLinksGenerationConvention : INavigationSourceConvention, IConvention
	{
		// Token: 0x06000C48 RID: 3144 RVA: 0x0002FF84 File Offset: 0x0002E184
		public void Apply(NavigationSourceConfiguration configuration, ODataModelBuilder model)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			foreach (StructuralTypeConfiguration structuralTypeConfiguration in configuration.EntityType.ThisAndBaseTypes())
			{
				foreach (NavigationPropertyConfiguration navigationPropertyConfiguration in ((EntityTypeConfiguration)structuralTypeConfiguration).NavigationProperties)
				{
					if (configuration.GetNavigationPropertyLink(navigationPropertyConfiguration) == null)
					{
						configuration.HasNavigationPropertyLink(navigationPropertyConfiguration, new NavigationLinkBuilder((ResourceContext entityContext, IEdmNavigationProperty navigationProperty) => entityContext.GenerateNavigationPropertyLink(navigationProperty, false), true));
					}
				}
			}
			foreach (EntityTypeConfiguration entityTypeConfiguration in model.DerivedTypes(configuration.EntityType))
			{
				foreach (NavigationPropertyConfiguration navigationPropertyConfiguration2 in entityTypeConfiguration.NavigationProperties)
				{
					if (configuration.GetNavigationPropertyLink(navigationPropertyConfiguration2) == null)
					{
						configuration.HasNavigationPropertyLink(navigationPropertyConfiguration2, new NavigationLinkBuilder((ResourceContext entityContext, IEdmNavigationProperty navigationProperty) => entityContext.GenerateNavigationPropertyLink(navigationProperty, true), true));
					}
				}
			}
		}
	}
}
