using System;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dependencies;
using System.Web.Http.Dispatcher;
using Microsoft.ReportingServices.Portal.Interfaces.WebApi;
using Newtonsoft.Json;
using Owin;

namespace Microsoft.ReportingServices.Portal.WebApi
{
	// Token: 0x02000005 RID: 5
	internal sealed class WebApiOwinConfig
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public WebApiOwinConfig(IDependencyResolver dependencyResolver, IAssembliesResolverFactory assembliesResolverFactory)
		{
			if (dependencyResolver == null)
			{
				throw new ArgumentNullException("dependencyResolver");
			}
			if (assembliesResolverFactory == null)
			{
				throw new ArgumentNullException("assembliesResolverFactory");
			}
			this._dependencyResolver = dependencyResolver;
			this._assembliesResolverFactory = assembliesResolverFactory;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002084 File Offset: 0x00000284
		public void Register(IAppBuilder app)
		{
			HttpConfiguration httpConfiguration = new HttpConfiguration();
			httpConfiguration.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
			httpConfiguration.MapHttpAttributeRoutes();
			httpConfiguration.EnsureInitialized();
			httpConfiguration.DependencyResolver = this._dependencyResolver;
			httpConfiguration.Services.Replace(typeof(IAssembliesResolver), this._assembliesResolverFactory.Create(new Assembly[] { base.GetType().Assembly }));
			httpConfiguration.Services.Replace(typeof(IHttpControllerSelector), new DefaultHttpControllerSelector(httpConfiguration));
			app.UseWebApi(httpConfiguration);
		}

		// Token: 0x04000035 RID: 53
		private readonly IDependencyResolver _dependencyResolver;

		// Token: 0x04000036 RID: 54
		private readonly IAssembliesResolverFactory _assembliesResolverFactory;
	}
}
