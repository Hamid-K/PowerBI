using System;
using System.Net;
using System.Net.Http;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Properties;

namespace System.Web.Http.Dispatcher
{
	// Token: 0x02000085 RID: 133
	public class HttpControllerDispatcher : HttpMessageHandler
	{
		// Token: 0x06000350 RID: 848 RVA: 0x00009DBD File Offset: 0x00007FBD
		public HttpControllerDispatcher(HttpConfiguration configuration)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			this._configuration = configuration;
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000351 RID: 849 RVA: 0x00009DDA File Offset: 0x00007FDA
		public HttpConfiguration Configuration
		{
			get
			{
				return this._configuration;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000352 RID: 850 RVA: 0x00009DE2 File Offset: 0x00007FE2
		// (set) Token: 0x06000353 RID: 851 RVA: 0x00009E03 File Offset: 0x00008003
		internal IExceptionLogger ExceptionLogger
		{
			get
			{
				if (this._exceptionLogger == null)
				{
					this._exceptionLogger = ExceptionServices.GetLogger(this._configuration);
				}
				return this._exceptionLogger;
			}
			set
			{
				this._exceptionLogger = value;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000354 RID: 852 RVA: 0x00009E0C File Offset: 0x0000800C
		// (set) Token: 0x06000355 RID: 853 RVA: 0x00009E2D File Offset: 0x0000802D
		internal IExceptionHandler ExceptionHandler
		{
			get
			{
				if (this._exceptionHandler == null)
				{
					this._exceptionHandler = ExceptionServices.GetHandler(this._configuration);
				}
				return this._exceptionHandler;
			}
			set
			{
				this._exceptionHandler = value;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000356 RID: 854 RVA: 0x00009E36 File Offset: 0x00008036
		private IHttpControllerSelector ControllerSelector
		{
			get
			{
				if (this._controllerSelector == null)
				{
					this._controllerSelector = this._configuration.Services.GetHttpControllerSelector();
				}
				return this._controllerSelector;
			}
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00009E5C File Offset: 0x0000805C
		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			HttpControllerContext controllerContext = null;
			ExceptionDispatchInfo exceptionInfo;
			try
			{
				HttpControllerDescriptor httpControllerDescriptor = this.ControllerSelector.SelectController(request);
				if (httpControllerDescriptor == null)
				{
					return request.CreateErrorResponse(HttpStatusCode.NotFound, Error.Format(SRResources.ResourceNotFound, new object[] { request.RequestUri }), SRResources.NoControllerSelected);
				}
				IHttpController httpController = httpControllerDescriptor.CreateController(request);
				if (httpController == null)
				{
					return request.CreateErrorResponse(HttpStatusCode.NotFound, Error.Format(SRResources.ResourceNotFound, new object[] { request.RequestUri }), SRResources.NoControllerCreated);
				}
				controllerContext = HttpControllerDispatcher.CreateControllerContext(request, httpControllerDescriptor, httpController);
				return await httpController.ExecuteAsync(controllerContext, cancellationToken);
			}
			catch (OperationCanceledException)
			{
				throw;
			}
			catch (HttpResponseException ex)
			{
				return ex.Response;
			}
			catch (Exception ex2)
			{
				exceptionInfo = ExceptionDispatchInfo.Capture(ex2);
			}
			ExceptionContext exceptionContext = new ExceptionContext(exceptionInfo.SourceException, ExceptionCatchBlocks.HttpControllerDispatcher, request)
			{
				ControllerContext = controllerContext
			};
			await this.ExceptionLogger.LogAsync(exceptionContext, cancellationToken);
			HttpResponseMessage httpResponseMessage = await this.ExceptionHandler.HandleAsync(exceptionContext, cancellationToken);
			if (httpResponseMessage == null)
			{
				exceptionInfo.Throw();
			}
			return httpResponseMessage;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00009EB4 File Offset: 0x000080B4
		private static HttpControllerContext CreateControllerContext(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, IHttpController controller)
		{
			HttpConfiguration configuration = controllerDescriptor.Configuration;
			HttpConfiguration configuration2 = request.GetConfiguration();
			if (configuration2 == null)
			{
				request.SetConfiguration(configuration);
			}
			else if (configuration2 != configuration)
			{
				request.SetConfiguration(configuration);
			}
			HttpRequestContext httpRequestContext = request.GetRequestContext();
			if (httpRequestContext == null)
			{
				httpRequestContext = new RequestBackedHttpRequestContext(request)
				{
					Configuration = configuration
				};
				request.SetRequestContext(httpRequestContext);
			}
			return new HttpControllerContext(httpRequestContext, request, controllerDescriptor, controller);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00009F0E File Offset: 0x0000810E
		private static HttpConfiguration EnsureNonNull(HttpConfiguration configuration)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			return configuration;
		}

		// Token: 0x040000BB RID: 187
		private readonly HttpConfiguration _configuration;

		// Token: 0x040000BC RID: 188
		private IExceptionLogger _exceptionLogger;

		// Token: 0x040000BD RID: 189
		private IExceptionHandler _exceptionHandler;

		// Token: 0x040000BE RID: 190
		private IHttpControllerSelector _controllerSelector;
	}
}
