using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Http.Controllers;
using System.Web.Http.Properties;

namespace System.Web.Http.Routing
{
	// Token: 0x0200014C RID: 332
	public class DefaultDirectRouteProvider : IDirectRouteProvider
	{
		// Token: 0x06000907 RID: 2311 RVA: 0x00016B60 File Offset: 0x00014D60
		public virtual IReadOnlyList<RouteEntry> GetDirectRoutes(HttpControllerDescriptor controllerDescriptor, IReadOnlyList<HttpActionDescriptor> actionDescriptors, IInlineConstraintResolver constraintResolver)
		{
			List<RouteEntry> list = new List<RouteEntry>();
			List<HttpActionDescriptor> list2 = new List<HttpActionDescriptor>();
			foreach (HttpActionDescriptor httpActionDescriptor in actionDescriptors)
			{
				IReadOnlyList<IDirectRouteFactory> actionRouteFactories = this.GetActionRouteFactories(httpActionDescriptor);
				if (actionRouteFactories != null && actionRouteFactories.Count > 0)
				{
					IReadOnlyCollection<RouteEntry> actionDirectRoutes = this.GetActionDirectRoutes(httpActionDescriptor, actionRouteFactories, constraintResolver);
					if (actionDirectRoutes != null)
					{
						list.AddRange(actionDirectRoutes);
					}
				}
				else
				{
					list2.Add(httpActionDescriptor);
				}
			}
			if (list2.Count > 0)
			{
				IReadOnlyList<IDirectRouteFactory> controllerRouteFactories = this.GetControllerRouteFactories(controllerDescriptor);
				if (controllerRouteFactories != null && controllerRouteFactories.Count > 0)
				{
					IReadOnlyCollection<RouteEntry> controllerDirectRoutes = this.GetControllerDirectRoutes(controllerDescriptor, list2, controllerRouteFactories, constraintResolver);
					if (controllerDirectRoutes != null)
					{
						list.AddRange(controllerDirectRoutes);
					}
				}
			}
			return list;
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x00016C20 File Offset: 0x00014E20
		protected virtual IReadOnlyList<IDirectRouteFactory> GetControllerRouteFactories(HttpControllerDescriptor controllerDescriptor)
		{
			Collection<IDirectRouteFactory> customAttributes = controllerDescriptor.GetCustomAttributes<IDirectRouteFactory>(false);
			Collection<IHttpRouteInfoProvider> customAttributes2 = controllerDescriptor.GetCustomAttributes<IHttpRouteInfoProvider>(false);
			List<IDirectRouteFactory> list = new List<IDirectRouteFactory>();
			list.AddRange(customAttributes);
			foreach (IHttpRouteInfoProvider httpRouteInfoProvider in customAttributes2)
			{
				if (!(httpRouteInfoProvider is IDirectRouteFactory))
				{
					list.Add(new RouteInfoDirectRouteFactory(httpRouteInfoProvider));
				}
			}
			return list;
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x00016C94 File Offset: 0x00014E94
		protected virtual IReadOnlyList<IDirectRouteFactory> GetActionRouteFactories(HttpActionDescriptor actionDescriptor)
		{
			ReflectedHttpActionDescriptor reflectedHttpActionDescriptor = actionDescriptor as ReflectedHttpActionDescriptor;
			if (reflectedHttpActionDescriptor != null && reflectedHttpActionDescriptor.MethodInfo != null && reflectedHttpActionDescriptor.MethodInfo.DeclaringType != actionDescriptor.ControllerDescriptor.ControllerType)
			{
				return null;
			}
			Collection<IDirectRouteFactory> customAttributes = actionDescriptor.GetCustomAttributes<IDirectRouteFactory>(false);
			Collection<IHttpRouteInfoProvider> customAttributes2 = actionDescriptor.GetCustomAttributes<IHttpRouteInfoProvider>(false);
			List<IDirectRouteFactory> list = new List<IDirectRouteFactory>();
			list.AddRange(customAttributes);
			foreach (IHttpRouteInfoProvider httpRouteInfoProvider in customAttributes2)
			{
				if (!(httpRouteInfoProvider is IDirectRouteFactory))
				{
					list.Add(new RouteInfoDirectRouteFactory(httpRouteInfoProvider));
				}
			}
			return list;
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x00016D40 File Offset: 0x00014F40
		protected virtual IReadOnlyList<RouteEntry> GetControllerDirectRoutes(HttpControllerDescriptor controllerDescriptor, IReadOnlyList<HttpActionDescriptor> actionDescriptors, IReadOnlyList<IDirectRouteFactory> factories, IInlineConstraintResolver constraintResolver)
		{
			return DefaultDirectRouteProvider.CreateRouteEntries(this.GetRoutePrefix(controllerDescriptor), factories, actionDescriptors, constraintResolver, false);
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x00016D53 File Offset: 0x00014F53
		protected virtual IReadOnlyList<RouteEntry> GetActionDirectRoutes(HttpActionDescriptor actionDescriptor, IReadOnlyList<IDirectRouteFactory> factories, IInlineConstraintResolver constraintResolver)
		{
			return DefaultDirectRouteProvider.CreateRouteEntries(this.GetRoutePrefix(actionDescriptor.ControllerDescriptor), factories, new HttpActionDescriptor[] { actionDescriptor }, constraintResolver, true);
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x00016D74 File Offset: 0x00014F74
		protected virtual string GetRoutePrefix(HttpControllerDescriptor controllerDescriptor)
		{
			Collection<IRoutePrefix> customAttributes = controllerDescriptor.GetCustomAttributes<IRoutePrefix>(false);
			if (customAttributes == null)
			{
				return null;
			}
			if (customAttributes.Count > 1)
			{
				throw new InvalidOperationException(Error.Format(SRResources.RoutePrefix_CannotSupportMultiRoutePrefix, new object[] { controllerDescriptor.ControllerType.FullName }));
			}
			if (customAttributes.Count == 1)
			{
				IRoutePrefix routePrefix = customAttributes[0];
				if (routePrefix != null)
				{
					string prefix = routePrefix.Prefix;
					if (prefix == null)
					{
						throw new InvalidOperationException(Error.Format(SRResources.RoutePrefix_PrefixCannotBeNull, new object[] { controllerDescriptor.ControllerType.FullName }));
					}
					if (prefix.EndsWith("/", StringComparison.Ordinal))
					{
						throw Error.InvalidOperation(SRResources.AttributeRoutes_InvalidPrefix, new object[] { prefix, controllerDescriptor.ControllerName });
					}
					return prefix;
				}
			}
			return null;
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x00016E2C File Offset: 0x0001502C
		private static IReadOnlyList<RouteEntry> CreateRouteEntries(string prefix, IReadOnlyCollection<IDirectRouteFactory> factories, IReadOnlyCollection<HttpActionDescriptor> actions, IInlineConstraintResolver constraintResolver, bool targetIsAction)
		{
			List<RouteEntry> list = new List<RouteEntry>();
			foreach (IDirectRouteFactory directRouteFactory in factories)
			{
				RouteEntry routeEntry = DefaultDirectRouteProvider.CreateRouteEntry(prefix, directRouteFactory, actions, constraintResolver, targetIsAction);
				list.Add(routeEntry);
			}
			return list;
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x00016E88 File Offset: 0x00015088
		private static RouteEntry CreateRouteEntry(string prefix, IDirectRouteFactory factory, IReadOnlyCollection<HttpActionDescriptor> actions, IInlineConstraintResolver constraintResolver, bool targetIsAction)
		{
			DirectRouteFactoryContext directRouteFactoryContext = new DirectRouteFactoryContext(prefix, actions, constraintResolver, targetIsAction);
			RouteEntry routeEntry = factory.CreateRoute(directRouteFactoryContext);
			if (routeEntry == null)
			{
				throw Error.InvalidOperation(SRResources.TypeMethodMustNotReturnNull, new object[]
				{
					typeof(IDirectRouteFactory).Name,
					"CreateRoute"
				});
			}
			DirectRouteBuilder.ValidateRouteEntry(routeEntry);
			return routeEntry;
		}
	}
}
