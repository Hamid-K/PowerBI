using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.BIServer.Configuration;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.Owin;

namespace Microsoft.BIServer.Owin.Common.Middleware
{
	// Token: 0x0200001B RID: 27
	public sealed class CustomHeadersMiddleware : OwinMiddleware
	{
		// Token: 0x0600007B RID: 123 RVA: 0x000032A8 File Offset: 0x000014A8
		public CustomHeadersMiddleware(OwinMiddleware next)
			: base(next)
		{
			CustomHeaderHelper customHeaderHelper = new CustomHeaderHelper();
			this._headerRules = customHeaderHelper.GetHeaderRules();
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000032CE File Offset: 0x000014CE
		public CustomHeadersMiddleware(OwinMiddleware next, Dictionary<IPathMatcher, Header> headerRules)
			: base(next)
		{
			this._headerRules = headerRules;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000032E0 File Offset: 0x000014E0
		public override Task Invoke(IOwinContext context)
		{
			using (ScopeMeter.Use(new string[]
			{
				"owin",
				base.GetType().Name
			}))
			{
				Dictionary<IPathMatcher, Header> headerRules = this._headerRules;
				if (headerRules != null && headerRules.Count > 0)
				{
					this.ApplyCustomHeaders(context, this._headerRules);
				}
			}
			return base.Next.Invoke(context);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000335C File Offset: 0x0000155C
		private void ApplyCustomHeaders(IOwinContext context, Dictionary<IPathMatcher, Header> headerRules)
		{
			foreach (KeyValuePair<IPathMatcher, Header> keyValuePair in headerRules)
			{
				string text = context.Request.Uri.ToString();
				try
				{
					bool flag = keyValuePair.Key.IsMatch(text, context.Request.Query.ToList<KeyValuePair<string, string[]>>());
					if (flag)
					{
						context.Response.Headers.Append(keyValuePair.Value.Name, keyValuePair.Value.Value);
					}
					Logger.Verbose(string.Format("Processing custom header rule {0} with pattern: {1}, for request {2}, isMatch: {3}", new object[]
					{
						keyValuePair.Value.Name,
						keyValuePair.Value.Pattern,
						text,
						flag
					}), Array.Empty<object>());
				}
				catch (Exception ex)
				{
					Logger.Error(string.Format("Error setting custom header rule {0} for request {1}, error: {2}", keyValuePair.Value.Name, text, ex.Message), Array.Empty<object>());
				}
			}
		}

		// Token: 0x0400004E RID: 78
		private readonly Dictionary<IPathMatcher, Header> _headerRules;
	}
}
