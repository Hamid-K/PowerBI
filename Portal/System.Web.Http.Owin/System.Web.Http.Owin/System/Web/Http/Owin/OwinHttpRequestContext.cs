using System;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;
using Microsoft.Owin;

namespace System.Web.Http.Owin
{
	// Token: 0x02000011 RID: 17
	internal class OwinHttpRequestContext : HttpRequestContext
	{
		// Token: 0x06000098 RID: 152 RVA: 0x00003423 File Offset: 0x00001623
		public OwinHttpRequestContext(IOwinContext context, HttpRequestMessage request)
		{
			this._context = context;
			this._request = request;
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00003439 File Offset: 0x00001639
		public IOwinContext Context
		{
			get
			{
				return this._context;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00003441 File Offset: 0x00001641
		public HttpRequestMessage Request
		{
			get
			{
				return this._request;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00003449 File Offset: 0x00001649
		// (set) Token: 0x0600009C RID: 156 RVA: 0x00003476 File Offset: 0x00001676
		public override X509Certificate2 ClientCertificate
		{
			get
			{
				if (!this._clientCertificateSet)
				{
					this._clientCertificate = this._context.Get<X509Certificate2>("ssl.ClientCertificate");
					this._clientCertificateSet = true;
				}
				return this._clientCertificate;
			}
			set
			{
				this._clientCertificate = value;
				this._clientCertificateSet = true;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00003488 File Offset: 0x00001688
		// (set) Token: 0x0600009E RID: 158 RVA: 0x000034ED File Offset: 0x000016ED
		public override bool IncludeErrorDetail
		{
			get
			{
				if (!this._includeErrorDetailSet)
				{
					HttpConfiguration configuration = this.Configuration;
					IncludeErrorDetailPolicy includeErrorDetailPolicy;
					if (configuration != null)
					{
						includeErrorDetailPolicy = configuration.IncludeErrorDetailPolicy;
					}
					else
					{
						includeErrorDetailPolicy = IncludeErrorDetailPolicy.Default;
					}
					bool flag;
					switch (includeErrorDetailPolicy)
					{
					case IncludeErrorDetailPolicy.Default:
					case IncludeErrorDetailPolicy.LocalOnly:
						flag = this.IsLocal;
						goto IL_0044;
					case IncludeErrorDetailPolicy.Always:
						flag = true;
						goto IL_0044;
					}
					flag = false;
					IL_0044:
					this._includeErrorDetail = flag;
					this._includeErrorDetailSet = true;
				}
				return this._includeErrorDetail;
			}
			set
			{
				this._includeErrorDetail = value;
				this._includeErrorDetailSet = true;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600009F RID: 159 RVA: 0x000034FD File Offset: 0x000016FD
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x0000352A File Offset: 0x0000172A
		public override bool IsLocal
		{
			get
			{
				if (!this._isLocalSet)
				{
					this._isLocal = this._context.Get<bool>("server.IsLocal");
					this._isLocalSet = true;
				}
				return this._isLocal;
			}
			set
			{
				this._isLocal = value;
				this._isLocalSet = true;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x0000353A File Offset: 0x0000173A
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x0000354C File Offset: 0x0000174C
		public override IPrincipal Principal
		{
			get
			{
				return this._context.Request.User;
			}
			set
			{
				this._context.Request.User = value;
				Thread.CurrentPrincipal = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00003565 File Offset: 0x00001765
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x0000358D File Offset: 0x0000178D
		public override UrlHelper Url
		{
			get
			{
				if (!this._urlSet)
				{
					this._url = new UrlHelper(this._request);
					this._urlSet = true;
				}
				return this._url;
			}
			set
			{
				this._url = value;
				this._urlSet = true;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x000035A0 File Offset: 0x000017A0
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x000035F1 File Offset: 0x000017F1
		public override string VirtualPathRoot
		{
			get
			{
				if (!this._virtualPathRootSet)
				{
					string value = this._context.Request.PathBase.Value;
					this._virtualPathRoot = (string.IsNullOrEmpty(value) ? "/" : value);
					this._virtualPathRootSet = true;
				}
				return this._virtualPathRoot;
			}
			set
			{
				this._virtualPathRoot = value;
				this._virtualPathRootSet = true;
			}
		}

		// Token: 0x04000021 RID: 33
		private readonly IOwinContext _context;

		// Token: 0x04000022 RID: 34
		private readonly HttpRequestMessage _request;

		// Token: 0x04000023 RID: 35
		private X509Certificate2 _clientCertificate;

		// Token: 0x04000024 RID: 36
		private bool _clientCertificateSet;

		// Token: 0x04000025 RID: 37
		private bool _includeErrorDetail;

		// Token: 0x04000026 RID: 38
		private bool _includeErrorDetailSet;

		// Token: 0x04000027 RID: 39
		private bool _isLocal;

		// Token: 0x04000028 RID: 40
		private bool _isLocalSet;

		// Token: 0x04000029 RID: 41
		private UrlHelper _url;

		// Token: 0x0400002A RID: 42
		private bool _urlSet;

		// Token: 0x0400002B RID: 43
		private string _virtualPathRoot;

		// Token: 0x0400002C RID: 44
		private bool _virtualPathRootSet;
	}
}
