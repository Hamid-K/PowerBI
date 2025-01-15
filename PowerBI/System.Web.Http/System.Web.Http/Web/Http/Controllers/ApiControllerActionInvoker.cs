using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Properties;

namespace System.Web.Http.Controllers
{
	// Token: 0x02000102 RID: 258
	public class ApiControllerActionInvoker : IHttpActionInvoker
	{
		// Token: 0x060006D1 RID: 1745 RVA: 0x000111EB File Offset: 0x0000F3EB
		public virtual Task<HttpResponseMessage> InvokeActionAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
		{
			if (actionContext == null)
			{
				throw Error.ArgumentNull("actionContext");
			}
			return ApiControllerActionInvoker.InvokeActionAsyncCore(actionContext, cancellationToken);
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x00011204 File Offset: 0x0000F404
		private static async Task<HttpResponseMessage> InvokeActionAsyncCore(HttpActionContext actionContext, CancellationToken cancellationToken)
		{
			HttpActionDescriptor actionDescriptor = actionContext.ActionDescriptor;
			HttpControllerContext controllerContext = actionContext.ControllerContext;
			HttpResponseMessage httpResponseMessage2;
			try
			{
				object obj = await actionDescriptor.ExecuteAsync(controllerContext, actionContext.ActionArguments, cancellationToken);
				bool flag = typeof(IHttpActionResult).IsAssignableFrom(actionDescriptor.ReturnType);
				if (obj == null && flag)
				{
					throw Error.InvalidOperation(SRResources.ApiControllerActionInvoker_NullHttpActionResult, new object[0]);
				}
				if (flag || actionDescriptor.ReturnType == typeof(object))
				{
					IHttpActionResult httpActionResult = obj as IHttpActionResult;
					if (httpActionResult == null && flag)
					{
						throw Error.InvalidOperation(SRResources.ApiControllerActionInvoker_InvalidHttpActionResult, new object[] { obj.GetType() });
					}
					if (httpActionResult != null)
					{
						HttpResponseMessage httpResponseMessage = await httpActionResult.ExecuteAsync(cancellationToken);
						if (httpResponseMessage == null)
						{
							throw Error.InvalidOperation(SRResources.ResponseMessageResultConverter_NullHttpResponseMessage, new object[0]);
						}
						httpResponseMessage.EnsureResponseHasRequest(actionContext.Request);
						return httpResponseMessage;
					}
				}
				httpResponseMessage2 = actionDescriptor.ResultConverter.Convert(controllerContext, obj);
			}
			catch (HttpResponseException ex)
			{
				HttpResponseMessage response = ex.Response;
				response.EnsureResponseHasRequest(actionContext.Request);
				httpResponseMessage2 = response;
			}
			return httpResponseMessage2;
		}
	}
}
