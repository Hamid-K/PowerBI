using System;
using System.Collections.Generic;
using System.Web.Http;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData.Routing.Conventions
{
	// Token: 0x02000099 RID: 153
	public static class ODataRoutingConventions
	{
		// Token: 0x06000543 RID: 1347 RVA: 0x00011928 File Offset: 0x0000FB28
		public static IList<IODataRoutingConvention> CreateDefaultWithAttributeRouting(string routeName, HttpConfiguration configuration)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			if (routeName == null)
			{
				throw Error.ArgumentNull("routeName");
			}
			IList<IODataRoutingConvention> list = ODataRoutingConventions.CreateDefault();
			AttributeRoutingConvention attributeRoutingConvention = new AttributeRoutingConvention(routeName, configuration);
			list.Insert(0, attributeRoutingConvention);
			return list;
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00011968 File Offset: 0x0000FB68
		public static IList<IODataRoutingConvention> CreateDefault()
		{
			return new List<IODataRoutingConvention>
			{
				new MetadataRoutingConvention(),
				new EntitySetRoutingConvention(),
				new SingletonRoutingConvention(),
				new EntityRoutingConvention(),
				new NavigationRoutingConvention(),
				new PropertyRoutingConvention(),
				new DynamicPropertyRoutingConvention(),
				new RefRoutingConvention(),
				new ActionRoutingConvention(),
				new FunctionRoutingConvention(),
				new UnmappedRequestRoutingConvention()
			};
		}
	}
}
