using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Properties;

namespace System.Web.Http.Routing
{
	// Token: 0x0200014B RID: 331
	internal static class AttributeRoutingMapper
	{
		// Token: 0x06000904 RID: 2308 RVA: 0x0001688C File Offset: 0x00014A8C
		public static void MapAttributeRoutes(HttpConfiguration configuration, IInlineConstraintResolver constraintResolver, IDirectRouteProvider directRouteProvider)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			if (constraintResolver == null)
			{
				throw new ArgumentNullException("constraintResolver");
			}
			if (directRouteProvider == null)
			{
				throw new ArgumentNullException("directRouteProvider");
			}
			RouteCollectionRoute aggregateRoute = new RouteCollectionRoute();
			configuration.Routes.Add("MS_attributerouteWebApi", aggregateRoute);
			Action<HttpConfiguration> previousInitializer = configuration.Initializer;
			configuration.Initializer = delegate(HttpConfiguration config)
			{
				previousInitializer(config);
				SubRouteCollection subRoutes = null;
				Func<SubRouteCollection> func = delegate
				{
					subRoutes = new SubRouteCollection();
					AttributeRoutingMapper.AddRouteEntries(subRoutes, configuration, constraintResolver, directRouteProvider);
					return subRoutes;
				};
				aggregateRoute.EnsureInitialized(func);
				if (subRoutes != null)
				{
					AttributeRoutingMapper.AddGenerationHooksForSubRoutes(config.Routes, subRoutes.Entries);
				}
			};
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x0001693C File Offset: 0x00014B3C
		private static void AddGenerationHooksForSubRoutes(HttpRouteCollection routeTable, IEnumerable<RouteEntry> entries)
		{
			foreach (RouteEntry routeEntry in entries)
			{
				string name = routeEntry.Name;
				if (name != null)
				{
					IHttpRoute httpRoute = new LinkGenerationRoute(routeEntry.Route);
					routeTable.Add(name, httpRoute);
				}
			}
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x0001699C File Offset: 0x00014B9C
		private static void AddRouteEntries(SubRouteCollection collector, HttpConfiguration configuration, IInlineConstraintResolver constraintResolver, IDirectRouteProvider directRouteProvider)
		{
			IDictionary<string, HttpControllerDescriptor> controllerMapping = configuration.Services.GetHttpControllerSelector().GetControllerMapping();
			if (controllerMapping != null)
			{
				foreach (HttpControllerDescriptor httpControllerDescriptor in controllerMapping.Values)
				{
					ILookup<string, HttpActionDescriptor> actionMapping = httpControllerDescriptor.Configuration.Services.GetActionSelector().GetActionMapping(httpControllerDescriptor);
					if (actionMapping != null)
					{
						List<HttpActionDescriptor> list = actionMapping.SelectMany((IGrouping<string, HttpActionDescriptor> g) => g).ToList<HttpActionDescriptor>();
						IReadOnlyCollection<RouteEntry> directRoutes = directRouteProvider.GetDirectRoutes(httpControllerDescriptor, list, constraintResolver);
						if (directRoutes == null)
						{
							throw Error.InvalidOperation(SRResources.TypeMethodMustNotReturnNull, new object[]
							{
								typeof(IDirectRouteProvider).Name,
								"GetDirectRoutes"
							});
						}
						foreach (RouteEntry routeEntry in directRoutes)
						{
							if (routeEntry == null)
							{
								throw Error.InvalidOperation(SRResources.TypeMethodMustNotReturnNull, new object[]
								{
									typeof(IDirectRouteProvider).Name,
									"GetDirectRoutes"
								});
							}
							DirectRouteBuilder.ValidateRouteEntry(routeEntry);
							HttpControllerDescriptor targetControllerDescriptor = routeEntry.Route.GetTargetControllerDescriptor();
							if (targetControllerDescriptor == null)
							{
								HttpActionDescriptor[] targetActionDescriptors = routeEntry.Route.GetTargetActionDescriptors();
								for (int i = 0; i < targetActionDescriptors.Length; i++)
								{
									targetActionDescriptors[i].SetIsAttributeRouted(true);
								}
							}
							else
							{
								targetControllerDescriptor.SetIsAttributeRouted(true);
							}
						}
						collector.AddRange(directRoutes);
					}
				}
			}
		}

		// Token: 0x0400026F RID: 623
		private const string AttributeRouteName = "MS_attributerouteWebApi";
	}
}
