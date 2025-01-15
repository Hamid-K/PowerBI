using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Cors;

namespace Microsoft.Owin.Cors
{
	// Token: 0x02000002 RID: 2
	public class CorsMiddleware
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public CorsMiddleware(Func<IDictionary<string, object>, Task> next, CorsOptions options)
		{
			if (next == null)
			{
				throw new ArgumentNullException("next");
			}
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			this._next = next;
			this._corsPolicyProvider = options.PolicyProvider ?? new CorsPolicyProvider();
			this._corsEngine = options.CorsEngine ?? new CorsEngine();
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020B0 File Offset: 0x000002B0
		public async Task Invoke(IDictionary<string, object> environment)
		{
			IOwinContext context = new OwinContext(environment);
			CorsRequestContext corsRequestContext = CorsMiddleware.GetCorsRequestContext(context);
			CorsPolicy policy = null;
			if (corsRequestContext != null)
			{
				CorsPolicy corsPolicy = await this._corsPolicyProvider.GetCorsPolicyAsync(context.Request);
				policy = corsPolicy;
			}
			if (policy != null && corsRequestContext != null)
			{
				if (corsRequestContext.IsPreflight)
				{
					await this.HandleCorsPreflightRequestAsync(context, policy, corsRequestContext);
				}
				else
				{
					await this.HandleCorsRequestAsync(context, policy, corsRequestContext);
				}
			}
			else
			{
				await this._next(environment);
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020FC File Offset: 0x000002FC
		private Task HandleCorsRequestAsync(IOwinContext context, CorsPolicy policy, CorsRequestContext corsRequestContext)
		{
			CorsResult result;
			if (this.TryEvaluateCorsPolicy(policy, corsRequestContext, out result))
			{
				CorsMiddleware.WriteCorsHeaders(context, result);
			}
			return this._next(context.Environment);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002130 File Offset: 0x00000330
		private Task HandleCorsPreflightRequestAsync(IOwinContext context, CorsPolicy policy, CorsRequestContext corsRequestContext)
		{
			CorsResult result;
			if (!string.IsNullOrEmpty(corsRequestContext.AccessControlRequestMethod) && this.TryEvaluateCorsPolicy(policy, corsRequestContext, out result))
			{
				context.Response.StatusCode = 200;
				CorsMiddleware.WriteCorsHeaders(context, result);
			}
			else
			{
				context.Response.StatusCode = 400;
			}
			return Task.FromResult<int>(0);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002185 File Offset: 0x00000385
		private bool TryEvaluateCorsPolicy(CorsPolicy policy, CorsRequestContext corsRequestContext, out CorsResult result)
		{
			result = this._corsEngine.EvaluatePolicy(corsRequestContext, policy);
			return result != null && result.IsValid;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000021A4 File Offset: 0x000003A4
		private static void WriteCorsHeaders(IOwinContext context, CorsResult result)
		{
			IDictionary<string, string> corsHeaders = result.ToResponseHeaders();
			if (corsHeaders != null)
			{
				foreach (KeyValuePair<string, string> header in corsHeaders)
				{
					context.Response.Headers.Set(header.Key, header.Value);
				}
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002210 File Offset: 0x00000410
		private static CorsRequestContext GetCorsRequestContext(IOwinContext context)
		{
			string origin = context.Request.Headers.Get(CorsConstants.Origin);
			if (string.IsNullOrEmpty(origin))
			{
				return null;
			}
			CorsRequestContext requestContext = new CorsRequestContext
			{
				RequestUri = context.Request.Uri,
				HttpMethod = context.Request.Method,
				Host = context.Request.Host.Value,
				Origin = origin,
				AccessControlRequestMethod = context.Request.Headers.Get(CorsConstants.AccessControlRequestMethod)
			};
			IList<string> headerValues = context.Request.Headers.GetCommaSeparatedValues(CorsConstants.AccessControlRequestHeaders);
			if (headerValues != null)
			{
				foreach (string header in headerValues)
				{
					requestContext.AccessControlRequestHeaders.Add(header);
				}
			}
			return requestContext;
		}

		// Token: 0x04000001 RID: 1
		private readonly Func<IDictionary<string, object>, Task> _next;

		// Token: 0x04000002 RID: 2
		private readonly ICorsPolicyProvider _corsPolicyProvider;

		// Token: 0x04000003 RID: 3
		private readonly ICorsEngine _corsEngine;
	}
}
