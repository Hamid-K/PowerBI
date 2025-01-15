using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Microsoft.Owin;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.BIServer.Owin.Common
{
	// Token: 0x02000008 RID: 8
	public sealed class RsRequestContext : IRSRequestContext
	{
		// Token: 0x06000013 RID: 19 RVA: 0x00002234 File Offset: 0x00000434
		public RsRequestContext(IOwinContext context, IIdentity identity)
		{
			this._owinContext = context;
			this._identity = identity;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000224A File Offset: 0x0000044A
		public RsRequestContext(IOwinContext context)
			: this(context, null)
		{
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002254 File Offset: 0x00000454
		public IOwinContext OwinContext
		{
			get
			{
				return this._owinContext;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000225C File Offset: 0x0000045C
		public IDictionary<string, string> Cookies
		{
			get
			{
				if (this._cookies == null)
				{
					RequestCookieCollection requestCookieCollection = this._owinContext.Request.Cookies ?? new RequestCookieCollection(new Dictionary<string, string>());
					this._cookies = requestCookieCollection.ToDictionary((KeyValuePair<string, string> kvp) => kvp.Key, (KeyValuePair<string, string> kvp) => kvp.Value);
				}
				return this._cookies;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000022E0 File Offset: 0x000004E0
		public IDictionary<string, string[]> Headers
		{
			get
			{
				if (this._headers == null)
				{
					IDictionary<string, string[]> headers = this._owinContext.Request.Headers;
					this._headers = headers ?? new Dictionary<string, string[]>();
				}
				return this._headers;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000018 RID: 24 RVA: 0x0000231C File Offset: 0x0000051C
		public IIdentity User
		{
			get
			{
				return this._identity;
			}
		}

		// Token: 0x04000030 RID: 48
		private readonly IOwinContext _owinContext;

		// Token: 0x04000031 RID: 49
		private readonly IIdentity _identity;

		// Token: 0x04000032 RID: 50
		private IDictionary<string, string> _cookies;

		// Token: 0x04000033 RID: 51
		private IDictionary<string, string[]> _headers;
	}
}
