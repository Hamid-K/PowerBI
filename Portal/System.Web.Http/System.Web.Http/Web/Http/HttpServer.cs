using System;
using System.Net;
using System.Net.Http;
using System.Runtime.ExceptionServices;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Properties;

namespace System.Web.Http
{
	// Token: 0x0200002E RID: 46
	public class HttpServer : DelegatingHandler
	{
		// Token: 0x06000114 RID: 276 RVA: 0x00004A15 File Offset: 0x00002C15
		public HttpServer()
			: this(new HttpConfiguration())
		{
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00004A22 File Offset: 0x00002C22
		public HttpServer(HttpConfiguration configuration)
			: this(configuration, new HttpRoutingDispatcher(configuration))
		{
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00004A31 File Offset: 0x00002C31
		public HttpServer(HttpMessageHandler dispatcher)
			: this(new HttpConfiguration(), dispatcher)
		{
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00004A40 File Offset: 0x00002C40
		public HttpServer(HttpConfiguration configuration, HttpMessageHandler dispatcher)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			if (dispatcher == null)
			{
				throw Error.ArgumentNull("dispatcher");
			}
			IPrincipal currentPrincipal = Thread.CurrentPrincipal;
			this._dispatcher = dispatcher;
			this._configuration = configuration;
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00004A8E File Offset: 0x00002C8E
		public HttpMessageHandler Dispatcher
		{
			get
			{
				return this._dispatcher;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00004A96 File Offset: 0x00002C96
		public HttpConfiguration Configuration
		{
			get
			{
				return this._configuration;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00004A9E File Offset: 0x00002C9E
		// (set) Token: 0x0600011B RID: 283 RVA: 0x00004ABF File Offset: 0x00002CBF
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

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00004AC8 File Offset: 0x00002CC8
		// (set) Token: 0x0600011D RID: 285 RVA: 0x00004AE9 File Offset: 0x00002CE9
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

		// Token: 0x0600011E RID: 286 RVA: 0x00004AF2 File Offset: 0x00002CF2
		protected override void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				this._disposed = true;
				if (disposing)
				{
					this._configuration.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00004B18 File Offset: 0x00002D18
		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			HttpResponseMessage httpResponseMessage;
			if (this._disposed)
			{
				httpResponseMessage = request.CreateErrorResponse(HttpStatusCode.ServiceUnavailable, SRResources.HttpServerDisposed);
			}
			else
			{
				this.EnsureInitialized();
				SynchronizationContext synchronizationContext = SynchronizationContext.Current;
				if (synchronizationContext != null)
				{
					request.SetSynchronizationContext(synchronizationContext);
				}
				request.SetConfiguration(this._configuration);
				IPrincipal originalPrincipal = Thread.CurrentPrincipal;
				if (originalPrincipal == null)
				{
					Thread.CurrentPrincipal = HttpServer._anonymousPrincipal;
				}
				if (request.GetRequestContext() == null)
				{
					HttpRequestContext httpRequestContext = new RequestBackedHttpRequestContext(request);
					request.SetRequestContext(httpRequestContext);
				}
				try
				{
					ExceptionDispatchInfo exceptionInfo;
					try
					{
						return await base.SendAsync(request, cancellationToken);
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
					ExceptionContext exceptionContext = new ExceptionContext(exceptionInfo.SourceException, ExceptionCatchBlocks.HttpServer, request);
					await this.ExceptionLogger.LogAsync(exceptionContext, cancellationToken);
					HttpResponseMessage httpResponseMessage2 = await this.ExceptionHandler.HandleAsync(exceptionContext, cancellationToken);
					if (httpResponseMessage2 == null)
					{
						exceptionInfo.Throw();
					}
					httpResponseMessage = httpResponseMessage2;
				}
				finally
				{
					Thread.CurrentPrincipal = originalPrincipal;
				}
			}
			return httpResponseMessage;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00004B6D File Offset: 0x00002D6D
		private void EnsureInitialized()
		{
			LazyInitializer.EnsureInitialized<object>(ref this._initializationTarget, ref this._initialized, ref this._initializationLock, delegate
			{
				this.Initialize();
				return null;
			});
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00004B93 File Offset: 0x00002D93
		protected virtual void Initialize()
		{
			this._configuration.EnsureInitialized();
			base.InnerHandler = HttpClientFactory.CreatePipeline(this._dispatcher, this._configuration.MessageHandlers);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00004BBC File Offset: 0x00002DBC
		private static HttpConfiguration EnsureNonNull(HttpConfiguration configuration)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			return configuration;
		}

		// Token: 0x0400003B RID: 59
		private static readonly IPrincipal _anonymousPrincipal = new GenericPrincipal(new GenericIdentity(string.Empty), new string[0]);

		// Token: 0x0400003C RID: 60
		private readonly HttpConfiguration _configuration;

		// Token: 0x0400003D RID: 61
		private readonly HttpMessageHandler _dispatcher;

		// Token: 0x0400003E RID: 62
		private IExceptionLogger _exceptionLogger;

		// Token: 0x0400003F RID: 63
		private IExceptionHandler _exceptionHandler;

		// Token: 0x04000040 RID: 64
		private bool _disposed;

		// Token: 0x04000041 RID: 65
		private bool _initialized;

		// Token: 0x04000042 RID: 66
		private object _initializationLock = new object();

		// Token: 0x04000043 RID: 67
		private object _initializationTarget;
	}
}
