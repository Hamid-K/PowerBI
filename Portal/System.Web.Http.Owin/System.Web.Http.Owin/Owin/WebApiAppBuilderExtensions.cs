using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Hosting;
using System.Web.Http.Owin;

namespace Owin
{
	// Token: 0x02000002 RID: 2
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class WebApiAppBuilderExtensions
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static IAppBuilder UseWebApi(this IAppBuilder builder, HttpConfiguration configuration)
		{
			if (builder == null)
			{
				throw new ArgumentNullException("builder");
			}
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			HttpServer httpServer = new HttpServer(configuration);
			IAppBuilder appBuilder;
			try
			{
				HttpMessageHandlerOptions httpMessageHandlerOptions = WebApiAppBuilderExtensions.CreateOptions(builder, httpServer, configuration);
				appBuilder = builder.UseMessageHandler(httpMessageHandlerOptions);
			}
			catch
			{
				httpServer.Dispose();
				throw;
			}
			return appBuilder;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020B0 File Offset: 0x000002B0
		public static IAppBuilder UseWebApi(this IAppBuilder builder, HttpServer httpServer)
		{
			if (builder == null)
			{
				throw new ArgumentNullException("builder");
			}
			if (httpServer == null)
			{
				throw new ArgumentNullException("httpServer");
			}
			HttpConfiguration configuration = httpServer.Configuration;
			HttpMessageHandlerOptions httpMessageHandlerOptions = WebApiAppBuilderExtensions.CreateOptions(builder, httpServer, configuration);
			return builder.UseMessageHandler(httpMessageHandlerOptions);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020F0 File Offset: 0x000002F0
		private static IAppBuilder UseMessageHandler(this IAppBuilder builder, HttpMessageHandlerOptions options)
		{
			return builder.Use(typeof(HttpMessageHandlerAdapter), new object[] { options });
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000210C File Offset: 0x0000030C
		private static HttpMessageHandlerOptions CreateOptions(IAppBuilder builder, HttpServer server, HttpConfiguration configuration)
		{
			ServicesContainer services = configuration.Services;
			IHostBufferPolicySelector hostBufferPolicySelector = services.GetHostBufferPolicySelector() ?? WebApiAppBuilderExtensions._defaultBufferPolicySelector;
			IExceptionLogger logger = ExceptionServices.GetLogger(services);
			IExceptionHandler handler = ExceptionServices.GetHandler(services);
			return new HttpMessageHandlerOptions
			{
				MessageHandler = server,
				BufferPolicySelector = hostBufferPolicySelector,
				ExceptionLogger = logger,
				ExceptionHandler = handler,
				AppDisposing = builder.GetOnAppDisposingProperty()
			};
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000216C File Offset: 0x0000036C
		internal static CancellationToken GetOnAppDisposingProperty(this IAppBuilder builder)
		{
			IDictionary<string, object> properties = builder.Properties;
			if (properties == null)
			{
				return CancellationToken.None;
			}
			object obj;
			if (!properties.TryGetValue("host.OnAppDisposing", out obj))
			{
				return CancellationToken.None;
			}
			CancellationToken? cancellationToken = obj as CancellationToken?;
			if (cancellationToken == null)
			{
				return CancellationToken.None;
			}
			return cancellationToken.Value;
		}

		// Token: 0x04000001 RID: 1
		private static readonly IHostBufferPolicySelector _defaultBufferPolicySelector = new OwinBufferPolicySelector();
	}
}
