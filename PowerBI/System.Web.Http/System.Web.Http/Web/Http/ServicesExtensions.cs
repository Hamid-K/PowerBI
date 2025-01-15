using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http.Controllers;
using System.Web.Http.Description;
using System.Web.Http.Dispatcher;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Filters;
using System.Web.Http.Hosting;
using System.Web.Http.Metadata;
using System.Web.Http.ModelBinding;
using System.Web.Http.Properties;
using System.Web.Http.Tracing;
using System.Web.Http.Validation;
using System.Web.Http.ValueProviders;

namespace System.Web.Http
{
	// Token: 0x02000032 RID: 50
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class ServicesExtensions
	{
		// Token: 0x06000131 RID: 305 RVA: 0x00004CE1 File Offset: 0x00002EE1
		public static IEnumerable<ModelBinderProvider> GetModelBinderProviders(this ServicesContainer services)
		{
			return services.GetServices<ModelBinderProvider>();
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00004CE9 File Offset: 0x00002EE9
		public static ModelMetadataProvider GetModelMetadataProvider(this ServicesContainer services)
		{
			return services.GetServiceOrThrow<ModelMetadataProvider>();
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00004CF1 File Offset: 0x00002EF1
		public static IEnumerable<ModelValidatorProvider> GetModelValidatorProviders(this ServicesContainer services)
		{
			return services.GetServices<ModelValidatorProvider>();
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00004CF9 File Offset: 0x00002EF9
		internal static IModelValidatorCache GetModelValidatorCache(this ServicesContainer services)
		{
			return services.GetService<IModelValidatorCache>();
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00004D01 File Offset: 0x00002F01
		public static IContentNegotiator GetContentNegotiator(this ServicesContainer services)
		{
			return services.GetService<IContentNegotiator>();
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00004D09 File Offset: 0x00002F09
		public static IHttpControllerActivator GetHttpControllerActivator(this ServicesContainer services)
		{
			return services.GetServiceOrThrow<IHttpControllerActivator>();
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00004D11 File Offset: 0x00002F11
		public static IHttpActionSelector GetActionSelector(this ServicesContainer services)
		{
			return services.GetServiceOrThrow<IHttpActionSelector>();
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00004D19 File Offset: 0x00002F19
		public static IHttpActionInvoker GetActionInvoker(this ServicesContainer services)
		{
			return services.GetServiceOrThrow<IHttpActionInvoker>();
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00004D21 File Offset: 0x00002F21
		public static IActionValueBinder GetActionValueBinder(this ServicesContainer services)
		{
			return services.GetService<IActionValueBinder>();
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00004D29 File Offset: 0x00002F29
		public static IEnumerable<ValueProviderFactory> GetValueProviderFactories(this ServicesContainer services)
		{
			return services.GetServices<ValueProviderFactory>();
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00004D31 File Offset: 0x00002F31
		public static IBodyModelValidator GetBodyModelValidator(this ServicesContainer services)
		{
			return services.GetService<IBodyModelValidator>();
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00004D39 File Offset: 0x00002F39
		public static IHostBufferPolicySelector GetHostBufferPolicySelector(this ServicesContainer services)
		{
			return services.GetService<IHostBufferPolicySelector>();
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00004D41 File Offset: 0x00002F41
		public static IHttpControllerSelector GetHttpControllerSelector(this ServicesContainer services)
		{
			return services.GetServiceOrThrow<IHttpControllerSelector>();
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00004D49 File Offset: 0x00002F49
		public static IAssembliesResolver GetAssembliesResolver(this ServicesContainer services)
		{
			return services.GetServiceOrThrow<IAssembliesResolver>();
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00004D51 File Offset: 0x00002F51
		public static IHttpControllerTypeResolver GetHttpControllerTypeResolver(this ServicesContainer services)
		{
			return services.GetServiceOrThrow<IHttpControllerTypeResolver>();
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00004D59 File Offset: 0x00002F59
		public static IApiExplorer GetApiExplorer(this ServicesContainer services)
		{
			return services.GetServiceOrThrow<IApiExplorer>();
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00004D61 File Offset: 0x00002F61
		public static IDocumentationProvider GetDocumentationProvider(this ServicesContainer services)
		{
			return services.GetService<IDocumentationProvider>();
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00004D69 File Offset: 0x00002F69
		public static IExceptionHandler GetExceptionHandler(this ServicesContainer services)
		{
			return services.GetService<IExceptionHandler>();
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00004D71 File Offset: 0x00002F71
		public static IEnumerable<IExceptionLogger> GetExceptionLoggers(this ServicesContainer services)
		{
			return services.GetServices<IExceptionLogger>();
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00004D79 File Offset: 0x00002F79
		public static IEnumerable<IFilterProvider> GetFilterProviders(this ServicesContainer services)
		{
			return services.GetServices<IFilterProvider>();
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00004D81 File Offset: 0x00002F81
		public static ITraceManager GetTraceManager(this ServicesContainer services)
		{
			return services.GetService<ITraceManager>();
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00004D89 File Offset: 0x00002F89
		public static ITraceWriter GetTraceWriter(this ServicesContainer services)
		{
			return services.GetService<ITraceWriter>();
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00004D91 File Offset: 0x00002F91
		internal static IEnumerable<TService> GetServices<TService>(this ServicesContainer services)
		{
			if (services == null)
			{
				throw Error.ArgumentNull("services");
			}
			return services.GetServices(typeof(TService)).Cast<TService>();
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00004DB6 File Offset: 0x00002FB6
		private static TService GetService<TService>(this ServicesContainer services)
		{
			if (services == null)
			{
				throw Error.ArgumentNull("services");
			}
			return (TService)((object)services.GetService(typeof(TService)));
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00004DDC File Offset: 0x00002FDC
		private static T GetServiceOrThrow<T>(this ServicesContainer services)
		{
			T service = services.GetService<T>();
			if (service == null)
			{
				throw Error.InvalidOperation(SRResources.DependencyResolverNoService, new object[] { typeof(T).FullName });
			}
			return service;
		}
	}
}
