using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNet.OData.Routing.Conventions;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.BIServer.HostingEnvironment.HostingInfo;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;
using Microsoft.ReportingServices.Portal.Interfaces.WebApi;
using Microsoft.ReportingServices.Portal.ODataWebApi.V1.Controllers;
using Microsoft.ReportingServices.Portal.ODataWebApi.V1.Filters;
using Microsoft.ReportingServices.Portal.ODataWebApi.V2.Controllers;
using Microsoft.ReportingServices.Portal.ODataWebApi.V2.Filters;
using Model;
using Owin;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.WebApi
{
	// Token: 0x0200000B RID: 11
	internal sealed class ODataWebApiOwinConfig
	{
		// Token: 0x06000020 RID: 32 RVA: 0x0000279C File Offset: 0x0000099C
		internal static void RegisterODataWebApiV1(IAppBuilder app, ILogger logger, IAssembliesResolverFactory assembliesResolverFactory, IHttpControllerActivator controllerActivator)
		{
			try
			{
				HttpConfiguration httpConfiguration = new HttpConfiguration();
				httpConfiguration.Count().Filter().OrderBy()
					.Expand()
					.Select()
					.MaxTop(null);
				httpConfiguration.AddODataQueryFilter();
				httpConfiguration.Filters.Add(new Microsoft.ReportingServices.Portal.ODataWebApi.V1.Filters.PortalExceptionFilter
				{
					Logger = logger
				});
				if (HostingState.Current.IsConfigSwitchEnabled(ConfigSwitches.EnableRemoteErrors, false))
				{
					httpConfiguration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
				}
				else
				{
					httpConfiguration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Never;
				}
				IEdmModel edmModel = ODataWebApiOwinConfig.GetEdmModelV1();
				Func<IServiceProvider, IEnumerable<IODataRoutingConvention>> <>9__2;
				Func<IServiceProvider, IEdmModel> <>9__1;
				httpConfiguration.MapODataServiceRoute("odataV1", "api/v1.0", delegate(IContainerBuilder builder)
				{
					ServiceLifetime serviceLifetime = ServiceLifetime.Singleton;
					Func<IServiceProvider, IEdmModel> func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = (IServiceProvider s) => edmModel);
					}
					IContainerBuilder containerBuilder = builder.AddService(serviceLifetime, func);
					ServiceLifetime serviceLifetime2 = ServiceLifetime.Singleton;
					Func<IServiceProvider, IEnumerable<IODataRoutingConvention>> func2;
					if ((func2 = <>9__2) == null)
					{
						func2 = (<>9__2 = (IServiceProvider s) => ODataWebApiOwinConfig.GetRoutingConventionsV1(logger));
					}
					containerBuilder.AddService(serviceLifetime2, func2).AddService(ServiceLifetime.Singleton, (IServiceProvider s) => new DefaultODataPathHandler()).AddService(ServiceLifetime.Singleton, (IServiceProvider s) => new ODataUriResolver
					{
						EnableCaseInsensitive = true
					});
				});
				httpConfiguration.Services.Replace(typeof(IHttpControllerActivator), controllerActivator);
				httpConfiguration.Services.Replace(typeof(IAssembliesResolver), assembliesResolverFactory.Create(new Assembly[]
				{
					typeof(MetadataController).Assembly,
					typeof(UnboundFunctionRoutingConvention).Assembly
				}));
				httpConfiguration.Services.Replace(typeof(IHttpControllerSelector), new NamespaceHttpControllerSelector(httpConfiguration));
				httpConfiguration.EnsureInitialized();
				httpConfiguration.EnableDependencyInjection();
				app.UseWebApi(httpConfiguration);
			}
			catch (Exception ex)
			{
				logger.Trace(TraceLevel.Error, new Func<string>(ex.ToString));
				throw;
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002928 File Offset: 0x00000B28
		internal static void RegisterODataWebApiV2(IAppBuilder app, ILogger logger, IAssembliesResolverFactory assembliesResolverFactory, IHttpControllerActivator controllerActivator)
		{
			try
			{
				HttpConfiguration config = new HttpConfiguration();
				config.Count().Filter().OrderBy()
					.Expand()
					.Select()
					.MaxTop(null);
				config.AddODataQueryFilter();
				config.Filters.Add(new Microsoft.ReportingServices.Portal.ODataWebApi.V2.Filters.PortalExceptionFilter
				{
					Logger = logger
				});
				if (HostingState.Current.IsConfigSwitchEnabled(ConfigSwitches.EnableRemoteErrors, false))
				{
					config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
				}
				else
				{
					config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Never;
				}
				IEdmModel edmModel = ODataWebApiOwinConfig.GetEdmModelV2();
				Func<IServiceProvider, IEdmModel> <>9__1;
				Func<IServiceProvider, IEnumerable<IODataRoutingConvention>> <>9__2;
				Func<IServiceProvider, ODataUriResolver> <>9__4;
				config.MapODataServiceRoute("odataV2", "api/v2.0", delegate(IContainerBuilder builder)
				{
					ServiceLifetime serviceLifetime = ServiceLifetime.Singleton;
					Func<IServiceProvider, IEdmModel> func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = (IServiceProvider s) => edmModel);
					}
					IContainerBuilder containerBuilder = builder.AddService(serviceLifetime, func);
					ServiceLifetime serviceLifetime2 = ServiceLifetime.Singleton;
					Func<IServiceProvider, IEnumerable<IODataRoutingConvention>> func2;
					if ((func2 = <>9__2) == null)
					{
						func2 = (<>9__2 = (IServiceProvider s) => ODataWebApiOwinConfig.GetRoutingConventionsV2(config, edmModel, logger));
					}
					IContainerBuilder containerBuilder2 = containerBuilder.AddService(serviceLifetime2, func2).AddService(ServiceLifetime.Singleton, (IServiceProvider s) => new PathAndSlashEscapeODataPathHandler());
					ServiceLifetime serviceLifetime3 = ServiceLifetime.Singleton;
					Func<IServiceProvider, ODataUriResolver> func3;
					if ((func3 = <>9__4) == null)
					{
						func3 = (<>9__4 = (IServiceProvider s) => new AlternateKeysODataUriResolver(edmModel)
						{
							EnableCaseInsensitive = true
						});
					}
					containerBuilder2.AddService(serviceLifetime3, func3);
				});
				config.Services.Replace(typeof(IHttpControllerActivator), controllerActivator);
				config.Services.Replace(typeof(IAssembliesResolver), assembliesResolverFactory.Create(new Assembly[]
				{
					typeof(MetadataController).Assembly,
					typeof(UnboundFunctionRoutingConvention).Assembly
				}));
				config.Services.Replace(typeof(IHttpControllerSelector), new NamespaceHttpControllerSelector(config));
				config.EnsureInitialized();
				config.EnableDependencyInjection();
				app.UseWebApi(config);
			}
			catch (Exception ex)
			{
				logger.Trace(TraceLevel.Error, new Func<string>(ex.ToString));
				throw;
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002AF8 File Offset: 0x00000CF8
		private static IEnumerable<IODataRoutingConvention> GetRoutingConventionsV1(ILogger logger)
		{
			IList<IODataRoutingConvention> list = ODataRoutingConventions.CreateDefault();
			list.Add(new MethodBasedRoutingConvention(logger));
			list.Add(new UnboundFunctionRoutingConvention(logger));
			return list;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002B17 File Offset: 0x00000D17
		private static IEnumerable<IODataRoutingConvention> GetRoutingConventionsV2(HttpConfiguration config, IEdmModel model, ILogger logger)
		{
			IList<IODataRoutingConvention> list = ODataRoutingConventions.CreateDefaultWithAttributeRouting("odataV2", config);
			list.Add(new MethodBasedRoutingConvention(logger));
			list.Add(new UnboundFunctionRoutingConvention(logger));
			return list;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002B3C File Offset: 0x00000D3C
		private static IEdmModel GetEdmModelV1()
		{
			ODataConventionModelBuilder odataConventionModelBuilder = new ODataConventionModelBuilder();
			odataConventionModelBuilder.Namespace = "Model";
			EnumTypeConfiguration<KpiDataItemType> enumTypeConfiguration = odataConventionModelBuilder.EnumType<KpiDataItemType>();
			enumTypeConfiguration.Member(KpiDataItemType.Shared);
			enumTypeConfiguration.Member(KpiDataItemType.Static);
			ComplexTypeConfiguration<KpiDataItem> complexTypeConfiguration = odataConventionModelBuilder.ComplexType<KpiDataItem>();
			complexTypeConfiguration.EnumProperty<KpiDataItemType>((KpiDataItem c) => c.Type);
			complexTypeConfiguration.Abstract();
			ComplexTypeConfiguration<KpiSharedDataItem> complexTypeConfiguration2 = odataConventionModelBuilder.ComplexType<KpiSharedDataItem>();
			complexTypeConfiguration2.Ignore<KpiDataItemType>((KpiSharedDataItem c) => c.Type);
			complexTypeConfiguration2.Property<Guid>((KpiSharedDataItem c) => c.Id);
			complexTypeConfiguration2.Property((KpiSharedDataItem c) => c.Path);
			complexTypeConfiguration2.CollectionProperty<DataSetParameter>((KpiSharedDataItem c) => c.Parameters);
			complexTypeConfiguration2.EnumProperty<KpiSharedDataItemAggregation>((KpiSharedDataItem c) => c.Aggregation);
			complexTypeConfiguration2.DerivesFrom<KpiDataItem>();
			ComplexTypeConfiguration<KpiStaticDataItem> complexTypeConfiguration3 = odataConventionModelBuilder.ComplexType<KpiStaticDataItem>();
			complexTypeConfiguration3.Ignore<KpiDataItemType>((KpiStaticDataItem c) => c.Type);
			complexTypeConfiguration3.Property((KpiStaticDataItem c) => c.Value);
			complexTypeConfiguration3.DerivesFrom<KpiDataItem>();
			EnumTypeConfiguration<DrillthroughTargetType> enumTypeConfiguration2 = odataConventionModelBuilder.EnumType<DrillthroughTargetType>();
			enumTypeConfiguration2.Member(DrillthroughTargetType.CatalogItem);
			enumTypeConfiguration2.Member(DrillthroughTargetType.Url);
			ComplexTypeConfiguration<DrillthroughTarget> complexTypeConfiguration4 = odataConventionModelBuilder.ComplexType<DrillthroughTarget>();
			complexTypeConfiguration4.EnumProperty<DrillthroughTargetType>((DrillthroughTarget c) => c.Type);
			complexTypeConfiguration4.Abstract();
			ComplexTypeConfiguration<CatalogItemDrillthroughTarget> complexTypeConfiguration5 = odataConventionModelBuilder.ComplexType<CatalogItemDrillthroughTarget>();
			complexTypeConfiguration5.Ignore<DrillthroughTargetType>((CatalogItemDrillthroughTarget c) => c.Type);
			complexTypeConfiguration5.EnumProperty<CatalogItemType>((CatalogItemDrillthroughTarget c) => c.CatalogItemType);
			complexTypeConfiguration5.Property((CatalogItemDrillthroughTarget c) => c.Path);
			complexTypeConfiguration5.Property<Guid>((CatalogItemDrillthroughTarget c) => c.Id);
			complexTypeConfiguration5.CollectionProperty<CatalogItemParameter>((CatalogItemDrillthroughTarget c) => c.Parameters);
			ComplexTypeConfiguration<UrlDrillthroughTarget> complexTypeConfiguration6 = odataConventionModelBuilder.ComplexType<UrlDrillthroughTarget>();
			complexTypeConfiguration6.Ignore<DrillthroughTargetType>((UrlDrillthroughTarget c) => c.Type);
			complexTypeConfiguration6.Property((UrlDrillthroughTarget c) => c.Url);
			complexTypeConfiguration6.Property<bool>((UrlDrillthroughTarget c) => c.DirectNavigation);
			Microsoft.ReportingServices.Portal.ODataWebApi.V1.Controllers.SubscriptionsController.RegisterModel(odataConventionModelBuilder);
			CacheRefreshPlanController.RegisterModel(odataConventionModelBuilder);
			Microsoft.ReportingServices.Portal.ODataWebApi.V1.Controllers.CatalogItemsController.RegisterModel(odataConventionModelBuilder);
			Microsoft.ReportingServices.Portal.ODataWebApi.V1.Controllers.CommentsController.RegisterModel(odataConventionModelBuilder);
			Microsoft.ReportingServices.Portal.ODataWebApi.V1.Controllers.SchedulesController.RegisterModel(odataConventionModelBuilder);
			ReportServerInfoController.RegisterModel(odataConventionModelBuilder);
			Microsoft.ReportingServices.Portal.ODataWebApi.V1.Controllers.SystemResourcesController.RegisterModel(odataConventionModelBuilder);
			Microsoft.ReportingServices.Portal.ODataWebApi.V1.Controllers.NotificationsController.RegisterModel(odataConventionModelBuilder);
			odataConventionModelBuilder.Singleton<User>("Me");
			odataConventionModelBuilder.Singleton<Telemetry>("Telemetry");
			odataConventionModelBuilder.Function("CatalogItemByPath").ReturnsFromEntitySet<CatalogItem>("CatalogItems").Parameter<string>("path");
			odataConventionModelBuilder.Function("FavoriteItems").ReturnsCollectionFromEntitySet<CatalogItem>("CatalogItems");
			odataConventionModelBuilder.Function("RestrictedFeatures").ReturnsCollection<string>();
			odataConventionModelBuilder.Function("ServiceState").Returns<ServiceState>();
			odataConventionModelBuilder.Function("AllowedActions").ReturnsCollection<string>().Parameter<string>("path");
			FunctionConfiguration functionConfiguration = odataConventionModelBuilder.Function("SafeGetSystemResourceContent").Returns<byte[]>();
			functionConfiguration.Parameter<string>("type");
			functionConfiguration.Parameter<string>("key");
			return odataConventionModelBuilder.GetEdmModel();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00003130 File Offset: 0x00001330
		internal static IEdmModel GetEdmModelV2()
		{
			ODataConventionModelBuilder odataConventionModelBuilder = new ODataConventionModelBuilder();
			odataConventionModelBuilder.Namespace = "Model";
			EnumTypeConfiguration<KpiDataItemType> enumTypeConfiguration = odataConventionModelBuilder.EnumType<KpiDataItemType>();
			enumTypeConfiguration.Member(KpiDataItemType.Shared);
			enumTypeConfiguration.Member(KpiDataItemType.Static);
			ComplexTypeConfiguration<KpiDataItem> complexTypeConfiguration = odataConventionModelBuilder.ComplexType<KpiDataItem>();
			complexTypeConfiguration.EnumProperty<KpiDataItemType>((KpiDataItem c) => c.Type);
			complexTypeConfiguration.Abstract();
			ComplexTypeConfiguration<KpiSharedDataItem> complexTypeConfiguration2 = odataConventionModelBuilder.ComplexType<KpiSharedDataItem>();
			complexTypeConfiguration2.Ignore<KpiDataItemType>((KpiSharedDataItem c) => c.Type);
			complexTypeConfiguration2.Property<Guid>((KpiSharedDataItem c) => c.Id);
			complexTypeConfiguration2.Property((KpiSharedDataItem c) => c.Path);
			complexTypeConfiguration2.CollectionProperty<DataSetParameter>((KpiSharedDataItem c) => c.Parameters);
			complexTypeConfiguration2.EnumProperty<KpiSharedDataItemAggregation>((KpiSharedDataItem c) => c.Aggregation);
			complexTypeConfiguration2.DerivesFrom<KpiDataItem>();
			ComplexTypeConfiguration<KpiStaticDataItem> complexTypeConfiguration3 = odataConventionModelBuilder.ComplexType<KpiStaticDataItem>();
			complexTypeConfiguration3.Ignore<KpiDataItemType>((KpiStaticDataItem c) => c.Type);
			complexTypeConfiguration3.Property((KpiStaticDataItem c) => c.Value);
			complexTypeConfiguration3.DerivesFrom<KpiDataItem>();
			EnumTypeConfiguration<DrillthroughTargetType> enumTypeConfiguration2 = odataConventionModelBuilder.EnumType<DrillthroughTargetType>();
			enumTypeConfiguration2.Member(DrillthroughTargetType.CatalogItem);
			enumTypeConfiguration2.Member(DrillthroughTargetType.Url);
			ComplexTypeConfiguration<DrillthroughTarget> complexTypeConfiguration4 = odataConventionModelBuilder.ComplexType<DrillthroughTarget>();
			complexTypeConfiguration4.EnumProperty<DrillthroughTargetType>((DrillthroughTarget c) => c.Type);
			complexTypeConfiguration4.Abstract();
			ComplexTypeConfiguration<CatalogItemDrillthroughTarget> complexTypeConfiguration5 = odataConventionModelBuilder.ComplexType<CatalogItemDrillthroughTarget>();
			complexTypeConfiguration5.Ignore<DrillthroughTargetType>((CatalogItemDrillthroughTarget c) => c.Type);
			complexTypeConfiguration5.EnumProperty<CatalogItemType>((CatalogItemDrillthroughTarget c) => c.CatalogItemType);
			complexTypeConfiguration5.Property((CatalogItemDrillthroughTarget c) => c.Path);
			complexTypeConfiguration5.Property<Guid>((CatalogItemDrillthroughTarget c) => c.Id);
			complexTypeConfiguration5.CollectionProperty<CatalogItemParameter>((CatalogItemDrillthroughTarget c) => c.Parameters);
			ComplexTypeConfiguration<UrlDrillthroughTarget> complexTypeConfiguration6 = odataConventionModelBuilder.ComplexType<UrlDrillthroughTarget>();
			complexTypeConfiguration6.Ignore<DrillthroughTargetType>((UrlDrillthroughTarget c) => c.Type);
			complexTypeConfiguration6.Property((UrlDrillthroughTarget c) => c.Url);
			complexTypeConfiguration6.Property<bool>((UrlDrillthroughTarget c) => c.DirectNavigation);
			AlertSubscriptionsController.RegisterModel(odataConventionModelBuilder);
			CacheRefreshPlansController.RegisterModel(odataConventionModelBuilder);
			Microsoft.ReportingServices.Portal.ODataWebApi.V2.Controllers.CatalogItemsController.RegisterModel(odataConventionModelBuilder);
			Microsoft.ReportingServices.Portal.ODataWebApi.V2.Controllers.CommentsController.RegisterModel(odataConventionModelBuilder);
			ComponentsController.RegisterModel(odataConventionModelBuilder);
			DataSetsController.RegisterModel(odataConventionModelBuilder);
			DataSourcesController.RegisterModel(odataConventionModelBuilder);
			ExcelWorkbooksController.RegisterModel(odataConventionModelBuilder);
			ExtensionsController.RegisterModel(odataConventionModelBuilder);
			FavoriteItemsController.RegisterModel(odataConventionModelBuilder);
			FoldersController.RegisterModel(odataConventionModelBuilder);
			KpisController.RegisterModel(odataConventionModelBuilder);
			LinkedReportsController.RegisterModel(odataConventionModelBuilder);
			Microsoft.ReportingServices.Portal.ODataWebApi.V2.Controllers.NotificationsController.RegisterModel(odataConventionModelBuilder);
			PowerBIReportsController.RegisterModel(odataConventionModelBuilder);
			ReportsController.RegisterModel(odataConventionModelBuilder);
			SystemController.RegisterModel(odataConventionModelBuilder);
			ResourcesController.RegisterModel(odataConventionModelBuilder);
			Microsoft.ReportingServices.Portal.ODataWebApi.V2.Controllers.SchedulesController.RegisterModel(odataConventionModelBuilder);
			Microsoft.ReportingServices.Portal.ODataWebApi.V2.Controllers.SubscriptionsController.RegisterModel(odataConventionModelBuilder);
			Microsoft.ReportingServices.Portal.ODataWebApi.V2.Controllers.SystemResourcesController.RegisterModel(odataConventionModelBuilder);
			Microsoft.ReportingServices.Portal.ODataWebApi.V2.Controllers.UnboundFunctionController.RegisterModel(odataConventionModelBuilder);
			UserSettingsController.RegisterModel(odataConventionModelBuilder);
			odataConventionModelBuilder.Singleton<User>("Me");
			odataConventionModelBuilder.Singleton<Telemetry>("Telemetry");
			IEdmModel edmModel = odataConventionModelBuilder.GetEdmModel();
			IEdmEntityType edmEntityType = edmModel.FindDeclaredEntitySet("CatalogItems").EntityType();
			IEdmProperty edmProperty = edmEntityType.FindProperty("Path");
			((EdmModel)edmModel).AddAlternateKeyAnnotation(edmEntityType, new Dictionary<string, IEdmProperty> { { "Path", edmProperty } });
			return edmModel;
		}
	}
}
