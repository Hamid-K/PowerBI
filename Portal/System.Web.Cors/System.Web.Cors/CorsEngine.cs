using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Cors.Properties;

namespace System.Web.Cors
{
	// Token: 0x02000005 RID: 5
	public class CorsEngine : ICorsEngine
	{
		// Token: 0x06000017 RID: 23 RVA: 0x000023D4 File Offset: 0x000005D4
		public virtual CorsResult EvaluatePolicy(CorsRequestContext requestContext, CorsPolicy policy)
		{
			if (requestContext == null)
			{
				throw new ArgumentNullException("requestContext");
			}
			if (policy == null)
			{
				throw new ArgumentNullException("policy");
			}
			CorsResult corsResult = new CorsResult();
			if (!this.TryValidateOrigin(requestContext, policy, corsResult))
			{
				return corsResult;
			}
			corsResult.SupportsCredentials = policy.SupportsCredentials;
			if (requestContext.IsPreflight)
			{
				if (!this.TryValidateMethod(requestContext, policy, corsResult))
				{
					return corsResult;
				}
				if (!this.TryValidateHeaders(requestContext, policy, corsResult))
				{
					return corsResult;
				}
				corsResult.PreflightMaxAge = policy.PreflightMaxAge;
			}
			else
			{
				CorsEngine.AddHeaderValues(corsResult.AllowedExposedHeaders, policy.ExposedHeaders);
			}
			return corsResult;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002460 File Offset: 0x00000660
		public virtual bool TryValidateMethod(CorsRequestContext requestContext, CorsPolicy policy, CorsResult result)
		{
			if (requestContext == null)
			{
				throw new ArgumentNullException("requestContext");
			}
			if (policy == null)
			{
				throw new ArgumentNullException("policy");
			}
			if (result == null)
			{
				throw new ArgumentNullException("result");
			}
			if (policy.AllowAnyMethod || policy.Methods.Contains(requestContext.AccessControlRequestMethod))
			{
				result.AllowedMethods.Add(requestContext.AccessControlRequestMethod);
			}
			else
			{
				result.ErrorMessages.Add(string.Format(CultureInfo.CurrentCulture, SRResources.MethodNotAllowed, new object[] { requestContext.AccessControlRequestMethod }));
			}
			return result.IsValid;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000024F4 File Offset: 0x000006F4
		public virtual bool TryValidateHeaders(CorsRequestContext requestContext, CorsPolicy policy, CorsResult result)
		{
			if (requestContext == null)
			{
				throw new ArgumentNullException("requestContext");
			}
			if (policy == null)
			{
				throw new ArgumentNullException("policy");
			}
			if (result == null)
			{
				throw new ArgumentNullException("result");
			}
			if (policy.AllowAnyHeader || requestContext.AccessControlRequestHeaders.IsSubsetOf(policy.Headers))
			{
				CorsEngine.AddHeaderValues(result.AllowedHeaders, requestContext.AccessControlRequestHeaders);
			}
			else
			{
				result.ErrorMessages.Add(string.Format(CultureInfo.CurrentCulture, SRResources.HeadersNotAllowed, new object[] { string.Join(",", requestContext.AccessControlRequestHeaders) }));
			}
			return result.IsValid;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002594 File Offset: 0x00000794
		public virtual bool TryValidateOrigin(CorsRequestContext requestContext, CorsPolicy policy, CorsResult result)
		{
			if (requestContext == null)
			{
				throw new ArgumentNullException("requestContext");
			}
			if (policy == null)
			{
				throw new ArgumentNullException("policy");
			}
			if (result == null)
			{
				throw new ArgumentNullException("result");
			}
			if (requestContext.Origin != null)
			{
				if (policy.AllowAnyOrigin)
				{
					if (policy.SupportsCredentials)
					{
						result.AllowedOrigin = requestContext.Origin;
					}
					else
					{
						result.AllowedOrigin = CorsConstants.AnyOrigin;
					}
				}
				else if (policy.Origins.Contains(requestContext.Origin))
				{
					result.AllowedOrigin = requestContext.Origin;
				}
				else
				{
					result.ErrorMessages.Add(string.Format(CultureInfo.CurrentCulture, SRResources.OriginNotAllowed, new object[] { requestContext.Origin }));
				}
			}
			else
			{
				result.ErrorMessages.Add(SRResources.NoOriginHeader);
			}
			return result.IsValid;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002660 File Offset: 0x00000860
		private static void AddHeaderValues(IList<string> target, IEnumerable<string> headerValues)
		{
			foreach (string text in headerValues)
			{
				target.Add(text);
			}
		}
	}
}
