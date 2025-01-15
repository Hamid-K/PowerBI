using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Filters;
using System.Web.Http.Metadata;
using System.Web.Http.ModelBinding;
using System.Web.Http.Properties;
using System.Web.Http.Results;
using System.Web.Http.Routing;
using System.Web.Http.Validation;
using Newtonsoft.Json;

namespace System.Web.Http
{
	// Token: 0x02000038 RID: 56
	public abstract class ApiController : IHttpController, IDisposable
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00005264 File Offset: 0x00003464
		// (set) Token: 0x0600016C RID: 364 RVA: 0x00005271 File Offset: 0x00003471
		public HttpConfiguration Configuration
		{
			get
			{
				return this.ControllerContext.Configuration;
			}
			set
			{
				this.ControllerContext.Configuration = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600016D RID: 365 RVA: 0x0000527F File Offset: 0x0000347F
		// (set) Token: 0x0600016E RID: 366 RVA: 0x000052B4 File Offset: 0x000034B4
		public HttpControllerContext ControllerContext
		{
			get
			{
				if (this.ActionContext.ControllerContext == null)
				{
					this.ActionContext.ControllerContext = new HttpControllerContext
					{
						RequestContext = new RequestBackedHttpRequestContext()
					};
				}
				return this.ActionContext.ControllerContext;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this.ActionContext.ControllerContext = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600016F RID: 367 RVA: 0x000052CB File Offset: 0x000034CB
		// (set) Token: 0x06000170 RID: 368 RVA: 0x000052D3 File Offset: 0x000034D3
		public HttpActionContext ActionContext
		{
			get
			{
				return this._actionContext;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._actionContext = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000171 RID: 369 RVA: 0x000052E5 File Offset: 0x000034E5
		public ModelStateDictionary ModelState
		{
			get
			{
				return this.ActionContext.ModelState;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000172 RID: 370 RVA: 0x000052F2 File Offset: 0x000034F2
		// (set) Token: 0x06000173 RID: 371 RVA: 0x00005300 File Offset: 0x00003500
		public HttpRequestMessage Request
		{
			get
			{
				return this.ControllerContext.Request;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				HttpRequestContext requestContext = value.GetRequestContext();
				HttpRequestContext requestContext2 = this.RequestContext;
				if (requestContext != null && requestContext != requestContext2)
				{
					throw new InvalidOperationException(SRResources.RequestContextConflict);
				}
				this.ControllerContext.Request = value;
				value.SetRequestContext(requestContext2);
				RequestBackedHttpRequestContext requestBackedHttpRequestContext = requestContext2 as RequestBackedHttpRequestContext;
				if (requestBackedHttpRequestContext != null)
				{
					requestBackedHttpRequestContext.Request = value;
				}
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000174 RID: 372 RVA: 0x0000535A File Offset: 0x0000355A
		// (set) Token: 0x06000175 RID: 373 RVA: 0x00005368 File Offset: 0x00003568
		public HttpRequestContext RequestContext
		{
			get
			{
				return this.ControllerContext.RequestContext;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				HttpRequestContext requestContext = this.ControllerContext.RequestContext;
				HttpRequestMessage request = this.Request;
				if (request != null)
				{
					HttpRequestContext requestContext2 = request.GetRequestContext();
					if (requestContext2 != null && requestContext2 != requestContext && requestContext2 != value)
					{
						throw new InvalidOperationException(SRResources.RequestContextConflict);
					}
					request.SetRequestContext(value);
				}
				this.ControllerContext.RequestContext = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000176 RID: 374 RVA: 0x000053C4 File Offset: 0x000035C4
		// (set) Token: 0x06000177 RID: 375 RVA: 0x000053D1 File Offset: 0x000035D1
		public UrlHelper Url
		{
			get
			{
				return this.RequestContext.Url;
			}
			set
			{
				this.RequestContext.Url = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000178 RID: 376 RVA: 0x000053DF File Offset: 0x000035DF
		// (set) Token: 0x06000179 RID: 377 RVA: 0x000053EC File Offset: 0x000035EC
		public IPrincipal User
		{
			get
			{
				return this.RequestContext.Principal;
			}
			set
			{
				this.RequestContext.Principal = value;
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000053FC File Offset: 0x000035FC
		public virtual Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken)
		{
			if (this._initialized)
			{
				throw Error.InvalidOperation(SRResources.CannotSupportSingletonInstance, new object[]
				{
					typeof(ApiController).Name,
					typeof(IHttpControllerActivator).Name
				});
			}
			this.Initialize(controllerContext);
			if (this.Request != null)
			{
				this.Request.RegisterForDispose(this);
			}
			ServicesContainer services = controllerContext.ControllerDescriptor.Configuration.Services;
			HttpActionDescriptor httpActionDescriptor = services.GetActionSelector().SelectAction(controllerContext);
			this.ActionContext.ActionDescriptor = httpActionDescriptor;
			if (this.Request != null)
			{
				this.Request.SetActionDescriptor(httpActionDescriptor);
			}
			FilterGrouping filterGrouping = httpActionDescriptor.GetFilterGrouping();
			IActionFilter[] actionFilters = filterGrouping.ActionFilters;
			IAuthenticationFilter[] authenticationFilters = filterGrouping.AuthenticationFilters;
			IAuthorizationFilter[] authorizationFilters = filterGrouping.AuthorizationFilters;
			IExceptionFilter[] exceptionFilters = filterGrouping.ExceptionFilters;
			IHttpActionResult httpActionResult = new ActionFilterResult(httpActionDescriptor.ActionBinding, this.ActionContext, services, actionFilters);
			if (authorizationFilters.Length != 0)
			{
				httpActionResult = new AuthorizationFilterResult(this.ActionContext, authorizationFilters, httpActionResult);
			}
			if (authenticationFilters.Length != 0)
			{
				httpActionResult = new AuthenticationFilterResult(this.ActionContext, this, authenticationFilters, httpActionResult);
			}
			if (exceptionFilters.Length != 0)
			{
				IExceptionLogger logger = ExceptionServices.GetLogger(services);
				IExceptionHandler handler = ExceptionServices.GetHandler(services);
				httpActionResult = new ExceptionFilterResult(this.ActionContext, exceptionFilters, logger, handler, httpActionResult);
			}
			return httpActionResult.ExecuteAsync(cancellationToken);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00005534 File Offset: 0x00003734
		public void Validate<TEntity>(TEntity entity)
		{
			this.Validate<TEntity>(entity, string.Empty);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00005544 File Offset: 0x00003744
		public void Validate<TEntity>(TEntity entity, string keyPrefix)
		{
			if (this.Configuration == null)
			{
				throw Error.InvalidOperation(SRResources.TypePropertyMustNotBeNull, new object[]
				{
					typeof(ApiController).Name,
					"Configuration"
				});
			}
			IBodyModelValidator bodyModelValidator = this.Configuration.Services.GetBodyModelValidator();
			if (bodyModelValidator != null)
			{
				ModelMetadataProvider modelMetadataProvider = this.Configuration.Services.GetModelMetadataProvider();
				bodyModelValidator.Validate(entity, typeof(TEntity), modelMetadataProvider, this.ActionContext, keyPrefix);
			}
		}

		// Token: 0x0600017D RID: 381 RVA: 0x000055C8 File Offset: 0x000037C8
		protected internal virtual BadRequestResult BadRequest()
		{
			return new BadRequestResult(this);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x000055D0 File Offset: 0x000037D0
		protected internal virtual BadRequestErrorMessageResult BadRequest(string message)
		{
			return new BadRequestErrorMessageResult(message, this);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x000055D9 File Offset: 0x000037D9
		protected internal virtual InvalidModelStateResult BadRequest(ModelStateDictionary modelState)
		{
			return new InvalidModelStateResult(modelState, this);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x000055E2 File Offset: 0x000037E2
		protected internal virtual ConflictResult Conflict()
		{
			return new ConflictResult(this);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x000055EA File Offset: 0x000037EA
		protected internal virtual NegotiatedContentResult<T> Content<T>(HttpStatusCode statusCode, T value)
		{
			return new NegotiatedContentResult<T>(statusCode, value, this);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x000055F4 File Offset: 0x000037F4
		protected internal FormattedContentResult<T> Content<T>(HttpStatusCode statusCode, T value, MediaTypeFormatter formatter)
		{
			return this.Content<T>(statusCode, value, formatter, null);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00005600 File Offset: 0x00003800
		protected internal FormattedContentResult<T> Content<T>(HttpStatusCode statusCode, T value, MediaTypeFormatter formatter, string mediaType)
		{
			return this.Content<T>(statusCode, value, formatter, new MediaTypeHeaderValue(mediaType));
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00005612 File Offset: 0x00003812
		protected internal virtual FormattedContentResult<T> Content<T>(HttpStatusCode statusCode, T value, MediaTypeFormatter formatter, MediaTypeHeaderValue mediaType)
		{
			return new FormattedContentResult<T>(statusCode, value, formatter, mediaType, this);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000561F File Offset: 0x0000381F
		protected internal CreatedNegotiatedContentResult<T> Created<T>(string location, T content)
		{
			if (location == null)
			{
				throw new ArgumentNullException("location");
			}
			return this.Created<T>(new Uri(location, UriKind.RelativeOrAbsolute), content);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000563D File Offset: 0x0000383D
		protected internal virtual CreatedNegotiatedContentResult<T> Created<T>(Uri location, T content)
		{
			return new CreatedNegotiatedContentResult<T>(location, content, this);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00005647 File Offset: 0x00003847
		protected internal CreatedAtRouteNegotiatedContentResult<T> CreatedAtRoute<T>(string routeName, object routeValues, T content)
		{
			return this.CreatedAtRoute<T>(routeName, new HttpRouteValueDictionary(routeValues), content);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00005657 File Offset: 0x00003857
		protected internal virtual CreatedAtRouteNegotiatedContentResult<T> CreatedAtRoute<T>(string routeName, IDictionary<string, object> routeValues, T content)
		{
			return new CreatedAtRouteNegotiatedContentResult<T>(routeName, routeValues, content, this);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00005662 File Offset: 0x00003862
		protected internal virtual InternalServerErrorResult InternalServerError()
		{
			return new InternalServerErrorResult(this);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000566A File Offset: 0x0000386A
		protected internal virtual ExceptionResult InternalServerError(Exception exception)
		{
			return new ExceptionResult(exception, this);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00005673 File Offset: 0x00003873
		protected internal JsonResult<T> Json<T>(T content)
		{
			return this.Json<T>(content, new JsonSerializerSettings());
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00005681 File Offset: 0x00003881
		protected internal JsonResult<T> Json<T>(T content, JsonSerializerSettings serializerSettings)
		{
			return this.Json<T>(content, serializerSettings, new UTF8Encoding(false, true));
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00005692 File Offset: 0x00003892
		protected internal virtual JsonResult<T> Json<T>(T content, JsonSerializerSettings serializerSettings, Encoding encoding)
		{
			return new JsonResult<T>(content, serializerSettings, encoding, this);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000569D File Offset: 0x0000389D
		protected internal virtual NotFoundResult NotFound()
		{
			return new NotFoundResult(this);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x000056A5 File Offset: 0x000038A5
		protected internal virtual OkResult Ok()
		{
			return new OkResult(this);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x000056AD File Offset: 0x000038AD
		protected internal virtual OkNegotiatedContentResult<T> Ok<T>(T content)
		{
			return new OkNegotiatedContentResult<T>(content, this);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000056B6 File Offset: 0x000038B6
		protected internal virtual RedirectResult Redirect(string location)
		{
			if (location == null)
			{
				throw new ArgumentNullException("location");
			}
			return this.Redirect(new Uri(location));
		}

		// Token: 0x06000192 RID: 402 RVA: 0x000056D2 File Offset: 0x000038D2
		protected internal virtual RedirectResult Redirect(Uri location)
		{
			return new RedirectResult(location, this);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x000056DB File Offset: 0x000038DB
		protected internal RedirectToRouteResult RedirectToRoute(string routeName, object routeValues)
		{
			return this.RedirectToRoute(routeName, new HttpRouteValueDictionary(routeValues));
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000056EA File Offset: 0x000038EA
		protected internal virtual RedirectToRouteResult RedirectToRoute(string routeName, IDictionary<string, object> routeValues)
		{
			return new RedirectToRouteResult(routeName, routeValues, this);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000056F4 File Offset: 0x000038F4
		protected internal virtual ResponseMessageResult ResponseMessage(HttpResponseMessage response)
		{
			return new ResponseMessageResult(response);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x000056FC File Offset: 0x000038FC
		protected internal virtual StatusCodeResult StatusCode(HttpStatusCode status)
		{
			return new StatusCodeResult(status, this);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00005705 File Offset: 0x00003905
		protected internal UnauthorizedResult Unauthorized(params AuthenticationHeaderValue[] challenges)
		{
			return this.Unauthorized(challenges);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000570E File Offset: 0x0000390E
		protected internal virtual UnauthorizedResult Unauthorized(IEnumerable<AuthenticationHeaderValue> challenges)
		{
			return new UnauthorizedResult(challenges, this);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00005717 File Offset: 0x00003917
		protected virtual void Initialize(HttpControllerContext controllerContext)
		{
			if (controllerContext == null)
			{
				throw Error.ArgumentNull("controllerContext");
			}
			this._initialized = true;
			this.ControllerContext = controllerContext;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00005735 File Offset: 0x00003935
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00005744 File Offset: 0x00003944
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x04000051 RID: 81
		private HttpActionContext _actionContext = new HttpActionContext();

		// Token: 0x04000052 RID: 82
		private bool _initialized;
	}
}
