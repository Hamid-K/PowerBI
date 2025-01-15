using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.Filters;
using Microsoft.AspNet.OData.Adapters;
using Microsoft.AspNet.OData.Batch;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNet.OData.Interfaces;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNet.OData.Routing.Conventions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Extensions
{
	// Token: 0x020001C3 RID: 451
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class HttpConfigurationExtensions
	{
		// Token: 0x06000EBE RID: 3774 RVA: 0x0003CE60 File Offset: 0x0003B060
		public static void AddODataQueryFilter(this HttpConfiguration configuration)
		{
			configuration.AddODataQueryFilter(new EnableQueryAttribute());
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x0003CE70 File Offset: 0x0003B070
		public static void SetDefaultQuerySettings(this HttpConfiguration configuration, DefaultQuerySettings defaultQuerySettings)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			if (defaultQuerySettings == null)
			{
				throw Error.ArgumentNull("defaultQuerySettings");
			}
			if (defaultQuerySettings.MaxTop != null)
			{
				int? maxTop = defaultQuerySettings.MaxTop;
				int num = 0;
				if (!((maxTop.GetValueOrDefault() > num) & (maxTop != null)))
				{
					goto IL_0059;
				}
			}
			ModelBoundQuerySettings.DefaultModelBoundQuerySettings.MaxTop = defaultQuerySettings.MaxTop;
			IL_0059:
			configuration.Properties["Microsoft.AspNet.OData.DefaultQuerySettings"] = defaultQuerySettings;
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x0003CEE8 File Offset: 0x0003B0E8
		public static DefaultQuerySettings GetDefaultQuerySettings(this HttpConfiguration configuration)
		{
			object obj;
			if (!configuration.Properties.TryGetValue("Microsoft.AspNet.OData.DefaultQuerySettings", out obj))
			{
				DefaultQuerySettings defaultQuerySettings = new DefaultQuerySettings();
				configuration.SetDefaultQuerySettings(defaultQuerySettings);
				return defaultQuerySettings;
			}
			return obj as DefaultQuerySettings;
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x0003CF20 File Offset: 0x0003B120
		public static HttpConfiguration MaxTop(this HttpConfiguration configuration, int? maxTopValue)
		{
			configuration.GetDefaultQuerySettings().MaxTop = maxTopValue;
			if (maxTopValue != null)
			{
				int? num = maxTopValue;
				int num2 = 0;
				if (!((num.GetValueOrDefault() > num2) & (num != null)))
				{
					return configuration;
				}
			}
			ModelBoundQuerySettings.DefaultModelBoundQuerySettings.MaxTop = maxTopValue;
			return configuration;
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x0003CF66 File Offset: 0x0003B166
		public static HttpConfiguration Expand(this HttpConfiguration configuration, QueryOptionSetting setting)
		{
			configuration.GetDefaultQuerySettings().EnableExpand = setting == QueryOptionSetting.Allowed;
			return configuration;
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x0003CF78 File Offset: 0x0003B178
		public static HttpConfiguration Expand(this HttpConfiguration configuration)
		{
			configuration.GetDefaultQuerySettings().EnableExpand = true;
			return configuration;
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x0003CF87 File Offset: 0x0003B187
		public static HttpConfiguration Select(this HttpConfiguration configuration, QueryOptionSetting setting)
		{
			configuration.GetDefaultQuerySettings().EnableSelect = setting == QueryOptionSetting.Allowed;
			return configuration;
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x0003CF99 File Offset: 0x0003B199
		public static HttpConfiguration Select(this HttpConfiguration configuration)
		{
			configuration.GetDefaultQuerySettings().EnableSelect = true;
			return configuration;
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x0003CFA8 File Offset: 0x0003B1A8
		public static HttpConfiguration Filter(this HttpConfiguration configuration, QueryOptionSetting setting)
		{
			configuration.GetDefaultQuerySettings().EnableFilter = setting == QueryOptionSetting.Allowed;
			return configuration;
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x0003CFBA File Offset: 0x0003B1BA
		public static HttpConfiguration Filter(this HttpConfiguration configuration)
		{
			configuration.GetDefaultQuerySettings().EnableFilter = true;
			return configuration;
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x0003CFC9 File Offset: 0x0003B1C9
		public static HttpConfiguration OrderBy(this HttpConfiguration configuration, QueryOptionSetting setting)
		{
			configuration.GetDefaultQuerySettings().EnableOrderBy = setting == QueryOptionSetting.Allowed;
			return configuration;
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x0003CFDB File Offset: 0x0003B1DB
		public static HttpConfiguration OrderBy(this HttpConfiguration configuration)
		{
			configuration.GetDefaultQuerySettings().EnableOrderBy = true;
			return configuration;
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x0003CFEA File Offset: 0x0003B1EA
		public static HttpConfiguration SkipToken(this HttpConfiguration configuration, QueryOptionSetting setting)
		{
			configuration.GetDefaultQuerySettings().EnableSkipToken = setting == QueryOptionSetting.Allowed;
			return configuration;
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x0003CFFC File Offset: 0x0003B1FC
		public static HttpConfiguration SkipToken(this HttpConfiguration configuration)
		{
			configuration.GetDefaultQuerySettings().EnableSkipToken = true;
			return configuration;
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x0003D00B File Offset: 0x0003B20B
		public static HttpConfiguration Count(this HttpConfiguration configuration, QueryOptionSetting setting)
		{
			configuration.GetDefaultQuerySettings().EnableCount = setting == QueryOptionSetting.Allowed;
			return configuration;
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x0003D01D File Offset: 0x0003B21D
		public static HttpConfiguration Count(this HttpConfiguration configuration)
		{
			configuration.GetDefaultQuerySettings().EnableCount = true;
			return configuration;
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x0003D02C File Offset: 0x0003B22C
		public static void AddODataQueryFilter(this HttpConfiguration configuration, IActionFilter queryFilter)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			configuration.Services.Add(typeof(IFilterProvider), new QueryFilterProvider(queryFilter));
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x0003D058 File Offset: 0x0003B258
		public static IETagHandler GetETagHandler(this HttpConfiguration configuration)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			object obj;
			if (!configuration.Properties.TryGetValue("Microsoft.AspNet.OData.ETagHandler", out obj))
			{
				IETagHandler ietagHandler = new DefaultODataETagHandler();
				configuration.SetETagHandler(ietagHandler);
				return ietagHandler;
			}
			if (obj == null)
			{
				throw Error.InvalidOperation(SRResources.NullETagHandler, new object[0]);
			}
			IETagHandler ietagHandler2 = obj as IETagHandler;
			if (ietagHandler2 == null)
			{
				throw Error.InvalidOperation(SRResources.InvalidETagHandler, new object[] { obj.GetType() });
			}
			return ietagHandler2;
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x0003D0CF File Offset: 0x0003B2CF
		public static void SetETagHandler(this HttpConfiguration configuration, IETagHandler handler)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			if (handler == null)
			{
				throw Error.ArgumentNull("handler");
			}
			configuration.Properties["Microsoft.AspNet.OData.ETagHandler"] = handler;
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x0003D100 File Offset: 0x0003B300
		public static TimeZoneInfo GetTimeZoneInfo(this HttpConfiguration configuration)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			object obj;
			TimeZoneInfo timeZoneInfo;
			if (!configuration.Properties.TryGetValue("Microsoft.AspNet.OData.TimeZoneInfo", out obj))
			{
				timeZoneInfo = TimeZoneInfo.Local;
				configuration.SetTimeZoneInfo(timeZoneInfo);
				return timeZoneInfo;
			}
			timeZoneInfo = obj as TimeZoneInfo;
			if (timeZoneInfo == null)
			{
				throw Error.InvalidOperation(SRResources.InvalidTimeZoneInfo, new object[]
				{
					obj.GetType(),
					typeof(TimeZoneInfo)
				});
			}
			return timeZoneInfo;
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x0003D170 File Offset: 0x0003B370
		public static void SetTimeZoneInfo(this HttpConfiguration configuration, TimeZoneInfo timeZoneInfo)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			if (timeZoneInfo == null)
			{
				throw Error.ArgumentNull("timeZoneInfo");
			}
			configuration.Properties["Microsoft.AspNet.OData.TimeZoneInfo"] = timeZoneInfo;
			TimeZoneInfoHelper.TimeZone = timeZoneInfo;
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x0003D1A5 File Offset: 0x0003B3A5
		public static void EnableContinueOnErrorHeader(this HttpConfiguration configuration)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			configuration.Properties["Microsoft.AspNet.OData.ContinueOnErrorKey"] = true;
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x0003D1CC File Offset: 0x0003B3CC
		internal static bool HasEnabledContinueOnErrorHeader(this HttpConfiguration configuration)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			object obj;
			return configuration.Properties.TryGetValue("Microsoft.AspNet.OData.ContinueOnErrorKey", out obj) && (bool)obj;
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x0003D203 File Offset: 0x0003B403
		public static void SetSerializeNullDynamicProperty(this HttpConfiguration configuration, bool serialize)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			configuration.Properties["Microsoft.AspNet.OData.NullDynamicPropertyKey"] = serialize;
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x0003D229 File Offset: 0x0003B429
		public static void SetUrlKeyDelimiter(this HttpConfiguration configuration, ODataUrlKeyDelimiter urlKeyDelimiter)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			configuration.Properties["Microsoft.AspNet.OData.UrlKeyDelimiterKey"] = urlKeyDelimiter;
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x0003D24A File Offset: 0x0003B44A
		public static void SetCompatibilityOptions(this HttpConfiguration configuration, CompatibilityOptions options)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			configuration.Properties["Microsoft.AspNet.OData.CompatibilityOptionsKey"] = options;
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x0003D270 File Offset: 0x0003B470
		internal static bool HasEnabledNullDynamicProperty(this HttpConfiguration configuration)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			object obj;
			return configuration.Properties.TryGetValue("Microsoft.AspNet.OData.NullDynamicPropertyKey", out obj) && (bool)obj;
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x0003D2A8 File Offset: 0x0003B4A8
		internal static ODataUrlKeyDelimiter GetUrlKeyDelimiter(this HttpConfiguration configuration)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			object obj;
			if (configuration.Properties.TryGetValue("Microsoft.AspNet.OData.UrlKeyDelimiterKey", out obj))
			{
				return obj as ODataUrlKeyDelimiter;
			}
			configuration.Properties["Microsoft.AspNet.OData.UrlKeyDelimiterKey"] = null;
			return null;
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x0003D2F0 File Offset: 0x0003B4F0
		internal static CompatibilityOptions GetCompatibilityOptions(this HttpConfiguration configuration)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			object obj;
			if (configuration.Properties.TryGetValue("Microsoft.AspNet.OData.CompatibilityOptionsKey", out obj))
			{
				return (CompatibilityOptions)obj;
			}
			configuration.Properties["Microsoft.AspNet.OData.CompatibilityOptionsKey"] = CompatibilityOptions.None;
			return CompatibilityOptions.None;
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x0003D33D File Offset: 0x0003B53D
		public static HttpConfiguration UseCustomContainerBuilder(this HttpConfiguration configuration, Func<IContainerBuilder> builderFactory)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			if (builderFactory == null)
			{
				throw Error.ArgumentNull("builderFactory");
			}
			configuration.Properties["Microsoft.AspNet.OData.ContainerBuilderFactoryKey"] = builderFactory;
			return configuration;
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x0003D36D File Offset: 0x0003B56D
		public static void EnableDependencyInjection(this HttpConfiguration configuration)
		{
			configuration.EnableDependencyInjection(null);
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x0003D378 File Offset: 0x0003B578
		public static void EnableDependencyInjection(this HttpConfiguration configuration, Action<IContainerBuilder> configureAction)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			if (configuration.Properties.ContainsKey("Microsoft.AspNet.OData.NonODataRootContainerKey"))
			{
				throw Error.InvalidOperation(SRResources.CannotReEnableDependencyInjection, new object[0]);
			}
			configuration.GetPerRouteContainer().CreateODataRootContainer(null, HttpConfigurationExtensions.ConfigureDefaultServices(configuration, configureAction));
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x0003D3CC File Offset: 0x0003B5CC
		public static ODataRoute MapODataServiceRoute(this HttpConfiguration configuration, string routeName, string routePrefix, Action<IContainerBuilder> configureAction)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			if (routeName == null)
			{
				throw Error.ArgumentNull("routeName");
			}
			IServiceProvider serviceProvider = configuration.CreateODataRootContainer(routeName, configureAction);
			IODataPathHandler requiredService = ServiceProviderServiceExtensions.GetRequiredService<IODataPathHandler>(serviceProvider);
			if (requiredService != null && requiredService.UrlKeyDelimiter == null)
			{
				ODataUrlKeyDelimiter urlKeyDelimiter = configuration.GetUrlKeyDelimiter();
				requiredService.UrlKeyDelimiter = urlKeyDelimiter;
			}
			ODataPathRouteConstraint odataPathRouteConstraint = new ODataPathRouteConstraint(routeName);
			ServiceProviderServiceExtensions.GetServices<IODataRoutingConvention>(serviceProvider);
			HttpRouteCollection routes = configuration.Routes;
			routePrefix = HttpConfigurationExtensions.RemoveTrailingSlash(routePrefix);
			HttpMessageHandler service = ServiceProviderServiceExtensions.GetService<HttpMessageHandler>(serviceProvider);
			ODataRoute odataRoute;
			if (service != null)
			{
				odataRoute = new ODataRoute(routePrefix, odataPathRouteConstraint, null, null, null, service);
			}
			else
			{
				ODataBatchHandler service2 = ServiceProviderServiceExtensions.GetService<ODataBatchHandler>(serviceProvider);
				if (service2 != null)
				{
					service2.ODataRouteName = routeName;
					string text = (string.IsNullOrEmpty(routePrefix) ? ODataRouteConstants.Batch : (routePrefix + "/" + ODataRouteConstants.Batch));
					HttpRouteCollectionExtensions.MapHttpBatchRoute(routes, routeName + "Batch", text, service2);
				}
				odataRoute = new ODataRoute(routePrefix, odataPathRouteConstraint);
			}
			routes.Add(routeName, odataRoute);
			return odataRoute;
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x0003D4B8 File Offset: 0x0003B6B8
		public static ODataRoute MapODataServiceRoute(this HttpConfiguration configuration, string routeName, string routePrefix, IEdmModel model)
		{
			Func<IServiceProvider, IEdmModel> <>9__1;
			Func<IServiceProvider, IEnumerable<IODataRoutingConvention>> <>9__2;
			return configuration.MapODataServiceRoute(routeName, routePrefix, delegate(IContainerBuilder builder)
			{
				ServiceLifetime serviceLifetime = ServiceLifetime.Singleton;
				Func<IServiceProvider, IEdmModel> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = (IServiceProvider sp) => model);
				}
				IContainerBuilder containerBuilder = builder.AddService(serviceLifetime, func);
				ServiceLifetime serviceLifetime2 = ServiceLifetime.Singleton;
				Func<IServiceProvider, IEnumerable<IODataRoutingConvention>> func2;
				if ((func2 = <>9__2) == null)
				{
					func2 = (<>9__2 = (IServiceProvider sp) => ODataRoutingConventions.CreateDefaultWithAttributeRouting(routeName, configuration));
				}
				containerBuilder.AddService(serviceLifetime2, func2);
			});
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x0003D500 File Offset: 0x0003B700
		public static ODataRoute MapODataServiceRoute(this HttpConfiguration configuration, string routeName, string routePrefix, IEdmModel model, ODataBatchHandler batchHandler)
		{
			Func<IServiceProvider, IEdmModel> <>9__1;
			Func<IServiceProvider, ODataBatchHandler> <>9__2;
			Func<IServiceProvider, IEnumerable<IODataRoutingConvention>> <>9__3;
			return configuration.MapODataServiceRoute(routeName, routePrefix, delegate(IContainerBuilder builder)
			{
				ServiceLifetime serviceLifetime = ServiceLifetime.Singleton;
				Func<IServiceProvider, IEdmModel> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = (IServiceProvider sp) => model);
				}
				IContainerBuilder containerBuilder = builder.AddService(serviceLifetime, func);
				ServiceLifetime serviceLifetime2 = ServiceLifetime.Singleton;
				Func<IServiceProvider, ODataBatchHandler> func2;
				if ((func2 = <>9__2) == null)
				{
					func2 = (<>9__2 = (IServiceProvider sp) => batchHandler);
				}
				IContainerBuilder containerBuilder2 = containerBuilder.AddService(serviceLifetime2, func2);
				ServiceLifetime serviceLifetime3 = ServiceLifetime.Singleton;
				Func<IServiceProvider, IEnumerable<IODataRoutingConvention>> func3;
				if ((func3 = <>9__3) == null)
				{
					func3 = (<>9__3 = (IServiceProvider sp) => ODataRoutingConventions.CreateDefaultWithAttributeRouting(routeName, configuration));
				}
				containerBuilder2.AddService(serviceLifetime3, func3);
			});
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x0003D550 File Offset: 0x0003B750
		public static ODataRoute MapODataServiceRoute(this HttpConfiguration configuration, string routeName, string routePrefix, IEdmModel model, HttpMessageHandler defaultHandler)
		{
			Func<IServiceProvider, IEdmModel> <>9__1;
			Func<IServiceProvider, HttpMessageHandler> <>9__2;
			Func<IServiceProvider, IEnumerable<IODataRoutingConvention>> <>9__3;
			return configuration.MapODataServiceRoute(routeName, routePrefix, delegate(IContainerBuilder builder)
			{
				ServiceLifetime serviceLifetime = ServiceLifetime.Singleton;
				Func<IServiceProvider, IEdmModel> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = (IServiceProvider sp) => model);
				}
				IContainerBuilder containerBuilder = builder.AddService(serviceLifetime, func);
				ServiceLifetime serviceLifetime2 = ServiceLifetime.Singleton;
				Func<IServiceProvider, HttpMessageHandler> func2;
				if ((func2 = <>9__2) == null)
				{
					func2 = (<>9__2 = (IServiceProvider sp) => defaultHandler);
				}
				IContainerBuilder containerBuilder2 = containerBuilder.AddService(serviceLifetime2, func2);
				ServiceLifetime serviceLifetime3 = ServiceLifetime.Singleton;
				Func<IServiceProvider, IEnumerable<IODataRoutingConvention>> func3;
				if ((func3 = <>9__3) == null)
				{
					func3 = (<>9__3 = (IServiceProvider sp) => ODataRoutingConventions.CreateDefaultWithAttributeRouting(routeName, configuration));
				}
				containerBuilder2.AddService(serviceLifetime3, func3);
			});
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x0003D5A0 File Offset: 0x0003B7A0
		public static ODataRoute MapODataServiceRoute(this HttpConfiguration configuration, string routeName, string routePrefix, IEdmModel model, IODataPathHandler pathHandler, IEnumerable<IODataRoutingConvention> routingConventions)
		{
			Func<IServiceProvider, IEdmModel> <>9__1;
			Func<IServiceProvider, IODataPathHandler> <>9__2;
			Func<IServiceProvider, IEnumerable<IODataRoutingConvention>> <>9__3;
			return configuration.MapODataServiceRoute(routeName, routePrefix, delegate(IContainerBuilder builder)
			{
				ServiceLifetime serviceLifetime = ServiceLifetime.Singleton;
				Func<IServiceProvider, IEdmModel> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = (IServiceProvider sp) => model);
				}
				IContainerBuilder containerBuilder = builder.AddService(serviceLifetime, func);
				ServiceLifetime serviceLifetime2 = ServiceLifetime.Singleton;
				Func<IServiceProvider, IODataPathHandler> func2;
				if ((func2 = <>9__2) == null)
				{
					func2 = (<>9__2 = (IServiceProvider sp) => pathHandler);
				}
				IContainerBuilder containerBuilder2 = containerBuilder.AddService(serviceLifetime2, func2);
				ServiceLifetime serviceLifetime3 = ServiceLifetime.Singleton;
				Func<IServiceProvider, IEnumerable<IODataRoutingConvention>> func3;
				if ((func3 = <>9__3) == null)
				{
					func3 = (<>9__3 = (IServiceProvider sp) => routingConventions.ToList<IODataRoutingConvention>().AsEnumerable<IODataRoutingConvention>());
				}
				containerBuilder2.AddService(serviceLifetime3, func3);
			});
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x0003D5E0 File Offset: 0x0003B7E0
		public static ODataRoute MapODataServiceRoute(this HttpConfiguration configuration, string routeName, string routePrefix, IEdmModel model, IODataPathHandler pathHandler, IEnumerable<IODataRoutingConvention> routingConventions, ODataBatchHandler batchHandler)
		{
			Func<IServiceProvider, IEdmModel> <>9__1;
			Func<IServiceProvider, IODataPathHandler> <>9__2;
			Func<IServiceProvider, IEnumerable<IODataRoutingConvention>> <>9__3;
			Func<IServiceProvider, ODataBatchHandler> <>9__4;
			return configuration.MapODataServiceRoute(routeName, routePrefix, delegate(IContainerBuilder builder)
			{
				ServiceLifetime serviceLifetime = ServiceLifetime.Singleton;
				Func<IServiceProvider, IEdmModel> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = (IServiceProvider sp) => model);
				}
				IContainerBuilder containerBuilder = builder.AddService(serviceLifetime, func);
				ServiceLifetime serviceLifetime2 = ServiceLifetime.Singleton;
				Func<IServiceProvider, IODataPathHandler> func2;
				if ((func2 = <>9__2) == null)
				{
					func2 = (<>9__2 = (IServiceProvider sp) => pathHandler);
				}
				IContainerBuilder containerBuilder2 = containerBuilder.AddService(serviceLifetime2, func2);
				ServiceLifetime serviceLifetime3 = ServiceLifetime.Singleton;
				Func<IServiceProvider, IEnumerable<IODataRoutingConvention>> func3;
				if ((func3 = <>9__3) == null)
				{
					func3 = (<>9__3 = (IServiceProvider sp) => routingConventions.ToList<IODataRoutingConvention>().AsEnumerable<IODataRoutingConvention>());
				}
				IContainerBuilder containerBuilder3 = containerBuilder2.AddService(serviceLifetime3, func3);
				ServiceLifetime serviceLifetime4 = ServiceLifetime.Singleton;
				Func<IServiceProvider, ODataBatchHandler> func4;
				if ((func4 = <>9__4) == null)
				{
					func4 = (<>9__4 = (IServiceProvider sp) => batchHandler);
				}
				containerBuilder3.AddService(serviceLifetime4, func4);
			});
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x0003D628 File Offset: 0x0003B828
		public static ODataRoute MapODataServiceRoute(this HttpConfiguration configuration, string routeName, string routePrefix, IEdmModel model, IODataPathHandler pathHandler, IEnumerable<IODataRoutingConvention> routingConventions, HttpMessageHandler defaultHandler)
		{
			Func<IServiceProvider, IEdmModel> <>9__1;
			Func<IServiceProvider, IODataPathHandler> <>9__2;
			Func<IServiceProvider, IEnumerable<IODataRoutingConvention>> <>9__3;
			Func<IServiceProvider, HttpMessageHandler> <>9__4;
			return configuration.MapODataServiceRoute(routeName, routePrefix, delegate(IContainerBuilder builder)
			{
				ServiceLifetime serviceLifetime = ServiceLifetime.Singleton;
				Func<IServiceProvider, IEdmModel> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = (IServiceProvider sp) => model);
				}
				IContainerBuilder containerBuilder = builder.AddService(serviceLifetime, func);
				ServiceLifetime serviceLifetime2 = ServiceLifetime.Singleton;
				Func<IServiceProvider, IODataPathHandler> func2;
				if ((func2 = <>9__2) == null)
				{
					func2 = (<>9__2 = (IServiceProvider sp) => pathHandler);
				}
				IContainerBuilder containerBuilder2 = containerBuilder.AddService(serviceLifetime2, func2);
				ServiceLifetime serviceLifetime3 = ServiceLifetime.Singleton;
				Func<IServiceProvider, IEnumerable<IODataRoutingConvention>> func3;
				if ((func3 = <>9__3) == null)
				{
					func3 = (<>9__3 = (IServiceProvider sp) => routingConventions.ToList<IODataRoutingConvention>().AsEnumerable<IODataRoutingConvention>());
				}
				IContainerBuilder containerBuilder3 = containerBuilder2.AddService(serviceLifetime3, func3);
				ServiceLifetime serviceLifetime4 = ServiceLifetime.Singleton;
				Func<IServiceProvider, HttpMessageHandler> func4;
				if ((func4 = <>9__4) == null)
				{
					func4 = (<>9__4 = (IServiceProvider sp) => defaultHandler);
				}
				containerBuilder3.AddService(serviceLifetime4, func4);
			});
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x0003D670 File Offset: 0x0003B870
		private static string RemoveTrailingSlash(string routePrefix)
		{
			if (!string.IsNullOrEmpty(routePrefix))
			{
				int num = routePrefix.Length - 1;
				if (routePrefix[num] == '/')
				{
					routePrefix = routePrefix.Substring(0, routePrefix.Length - 1);
				}
			}
			return routePrefix;
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x0003D6AB File Offset: 0x0003B8AB
		internal static IServiceProvider CreateODataRootContainer(this HttpConfiguration configuration, string routeName, Action<IContainerBuilder> configureAction)
		{
			return configuration.GetPerRouteContainer().CreateODataRootContainer(routeName, HttpConfigurationExtensions.ConfigureDefaultServices(configuration, configureAction));
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x0003D6C0 File Offset: 0x0003B8C0
		internal static IPerRouteContainer GetPerRouteContainer(this HttpConfiguration configuration)
		{
			return (IPerRouteContainer)configuration.Properties.GetOrAdd("Microsoft.AspNet.OData.PerRouteContainerKey", delegate(object key)
			{
				IPerRouteContainer perRouteContainer = new PerRouteContainer(configuration);
				object obj;
				if (configuration.Properties.TryGetValue("Microsoft.AspNet.OData.ContainerBuilderFactoryKey", out obj))
				{
					Func<IContainerBuilder> func = (Func<IContainerBuilder>)obj;
					perRouteContainer.BuilderFactory = func;
				}
				return perRouteContainer;
			});
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x0003D700 File Offset: 0x0003B900
		internal static IServiceProvider GetODataRootContainer(this HttpConfiguration configuration, string routeName)
		{
			return configuration.GetPerRouteContainer().GetODataRootContainer(routeName);
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x0003D710 File Offset: 0x0003B910
		internal static IServiceProvider GetNonODataRootContainer(this HttpConfiguration configuration)
		{
			object obj;
			if (configuration.Properties.TryGetValue("Microsoft.AspNet.OData.NonODataRootContainerKey", out obj))
			{
				return (IServiceProvider)obj;
			}
			throw Error.InvalidOperation(SRResources.NoNonODataHttpRouteRegistered, new object[0]);
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x0003D748 File Offset: 0x0003B948
		internal static void SetNonODataRootContainer(this HttpConfiguration configuration, IServiceProvider rootContainer)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			if (rootContainer == null)
			{
				throw Error.ArgumentNull("rootContainer");
			}
			if (configuration.Properties.ContainsKey("Microsoft.AspNet.OData.NonODataRootContainerKey"))
			{
				throw Error.InvalidOperation(SRResources.CannotReEnableDependencyInjection, new object[0]);
			}
			configuration.Properties["Microsoft.AspNet.OData.NonODataRootContainerKey"] = rootContainer;
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x0003D7A5 File Offset: 0x0003B9A5
		private static Action<IContainerBuilder> ConfigureDefaultServices(HttpConfiguration configuration, Action<IContainerBuilder> configureAction)
		{
			Func<IServiceProvider, HttpConfiguration> <>9__2;
			Func<IServiceProvider, DefaultQuerySettings> <>9__3;
			return delegate(IContainerBuilder builder)
			{
				IAssembliesResolver resolver = ServicesExtensions.GetAssembliesResolver(configuration.Services) ?? new DefaultAssembliesResolver();
				builder.AddService(ServiceLifetime.Singleton, (IServiceProvider sp) => resolver);
				builder.AddService(ServiceLifetime.Transient);
				ServiceLifetime serviceLifetime = ServiceLifetime.Singleton;
				Func<IServiceProvider, HttpConfiguration> func;
				if ((func = <>9__2) == null)
				{
					func = (<>9__2 = (IServiceProvider sp) => configuration);
				}
				builder.AddService(serviceLifetime, func);
				ServiceLifetime serviceLifetime2 = ServiceLifetime.Singleton;
				Func<IServiceProvider, DefaultQuerySettings> func2;
				if ((func2 = <>9__3) == null)
				{
					func2 = (<>9__3 = (IServiceProvider sp) => configuration.GetDefaultQuerySettings());
				}
				builder.AddService(serviceLifetime2, func2);
				builder.AddDefaultWebApiServices();
				if (configureAction != null)
				{
					configureAction(builder);
				}
			};
		}

		// Token: 0x04000418 RID: 1048
		private const string ETagHandlerKey = "Microsoft.AspNet.OData.ETagHandler";

		// Token: 0x04000419 RID: 1049
		private const string TimeZoneInfoKey = "Microsoft.AspNet.OData.TimeZoneInfo";

		// Token: 0x0400041A RID: 1050
		private const string UrlKeyDelimiterKey = "Microsoft.AspNet.OData.UrlKeyDelimiterKey";

		// Token: 0x0400041B RID: 1051
		private const string ContinueOnErrorKey = "Microsoft.AspNet.OData.ContinueOnErrorKey";

		// Token: 0x0400041C RID: 1052
		private const string NullDynamicPropertyKey = "Microsoft.AspNet.OData.NullDynamicPropertyKey";

		// Token: 0x0400041D RID: 1053
		private const string ContainerBuilderFactoryKey = "Microsoft.AspNet.OData.ContainerBuilderFactoryKey";

		// Token: 0x0400041E RID: 1054
		private const string PerRouteContainerKey = "Microsoft.AspNet.OData.PerRouteContainerKey";

		// Token: 0x0400041F RID: 1055
		private const string DefaultQuerySettingsKey = "Microsoft.AspNet.OData.DefaultQuerySettings";

		// Token: 0x04000420 RID: 1056
		private const string NonODataRootContainerKey = "Microsoft.AspNet.OData.NonODataRootContainerKey";

		// Token: 0x04000421 RID: 1057
		private const string CompatibilityOptionsKey = "Microsoft.AspNet.OData.CompatibilityOptionsKey";
	}
}
