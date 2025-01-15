using System;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Web.Http.Routing;

namespace System.Web.Http.Controllers
{
	// Token: 0x020000F2 RID: 242
	internal sealed class RequestBackedHttpRequestContext : HttpRequestContext
	{
		// Token: 0x06000632 RID: 1586 RVA: 0x0000FE5E File Offset: 0x0000E05E
		public RequestBackedHttpRequestContext()
		{
			this.Principal = Thread.CurrentPrincipal;
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0000FE71 File Offset: 0x0000E071
		public RequestBackedHttpRequestContext(HttpRequestMessage request)
			: this()
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			this._request = request;
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x0000FE8E File Offset: 0x0000E08E
		// (set) Token: 0x06000635 RID: 1589 RVA: 0x0000FE96 File Offset: 0x0000E096
		public HttpRequestMessage Request
		{
			get
			{
				return this._request;
			}
			set
			{
				this._request = value;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x0000FE9F File Offset: 0x0000E09F
		// (set) Token: 0x06000637 RID: 1591 RVA: 0x0000FEC5 File Offset: 0x0000E0C5
		public override X509Certificate2 ClientCertificate
		{
			get
			{
				if (this._certificateSet)
				{
					return this._certificate;
				}
				if (this._request != null)
				{
					return this._request.LegacyGetClientCertificate();
				}
				return null;
			}
			set
			{
				this._certificate = value;
				this._certificateSet = true;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x0000FED5 File Offset: 0x0000E0D5
		// (set) Token: 0x06000639 RID: 1593 RVA: 0x0000FEFB File Offset: 0x0000E0FB
		public override HttpConfiguration Configuration
		{
			get
			{
				if (this._configurationSet)
				{
					return this._configuration;
				}
				if (this._request != null)
				{
					return this._request.LegacyGetConfiguration();
				}
				return null;
			}
			set
			{
				this._configuration = value;
				this._configurationSet = true;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x0000FF0B File Offset: 0x0000E10B
		// (set) Token: 0x0600063B RID: 1595 RVA: 0x0000FF31 File Offset: 0x0000E131
		public override bool IncludeErrorDetail
		{
			get
			{
				if (this._includeErrorDetailSet)
				{
					return this._includeErrorDetail;
				}
				return this._request != null && this._request.LegacyShouldIncludeErrorDetail();
			}
			set
			{
				this._includeErrorDetail = value;
				this._includeErrorDetailSet = true;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x0000FF41 File Offset: 0x0000E141
		// (set) Token: 0x0600063D RID: 1597 RVA: 0x0000FF67 File Offset: 0x0000E167
		public override bool IsLocal
		{
			get
			{
				if (this._isLocalSet)
				{
					return this._isLocal;
				}
				return this._request != null && this._request.LegacyIsLocal();
			}
			set
			{
				this._isLocal = value;
				this._isLocalSet = true;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x0000FF77 File Offset: 0x0000E177
		// (set) Token: 0x0600063F RID: 1599 RVA: 0x0000FF9D File Offset: 0x0000E19D
		public override IHttpRouteData RouteData
		{
			get
			{
				if (this._routeDataSet)
				{
					return this._routeData;
				}
				if (this._request != null)
				{
					return this._request.LegacyGetRouteData();
				}
				return null;
			}
			set
			{
				this._routeData = value;
				this._routeDataSet = true;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x0000FFAD File Offset: 0x0000E1AD
		// (set) Token: 0x06000641 RID: 1601 RVA: 0x0000FFD3 File Offset: 0x0000E1D3
		public override UrlHelper Url
		{
			get
			{
				if (this._urlSet)
				{
					return this._url;
				}
				if (this._request != null)
				{
					return new UrlHelper(this._request);
				}
				return null;
			}
			set
			{
				this._url = value;
				this._urlSet = true;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x0000FFE4 File Offset: 0x0000E1E4
		// (set) Token: 0x06000643 RID: 1603 RVA: 0x00010012 File Offset: 0x0000E212
		public override string VirtualPathRoot
		{
			get
			{
				if (this._virtualPathRootSet)
				{
					return this._virtualPathRoot;
				}
				HttpConfiguration configuration = this.Configuration;
				if (configuration != null)
				{
					return configuration.VirtualPathRoot;
				}
				return null;
			}
			set
			{
				this._virtualPathRoot = value;
				this._virtualPathRootSet = true;
			}
		}

		// Token: 0x0400017E RID: 382
		private HttpRequestMessage _request;

		// Token: 0x0400017F RID: 383
		private X509Certificate2 _certificate;

		// Token: 0x04000180 RID: 384
		private bool _certificateSet;

		// Token: 0x04000181 RID: 385
		private HttpConfiguration _configuration;

		// Token: 0x04000182 RID: 386
		private bool _configurationSet;

		// Token: 0x04000183 RID: 387
		private bool _includeErrorDetail;

		// Token: 0x04000184 RID: 388
		private bool _includeErrorDetailSet;

		// Token: 0x04000185 RID: 389
		private bool _isLocal;

		// Token: 0x04000186 RID: 390
		private bool _isLocalSet;

		// Token: 0x04000187 RID: 391
		private IHttpRouteData _routeData;

		// Token: 0x04000188 RID: 392
		private bool _routeDataSet;

		// Token: 0x04000189 RID: 393
		private UrlHelper _url;

		// Token: 0x0400018A RID: 394
		private bool _urlSet;

		// Token: 0x0400018B RID: 395
		private string _virtualPathRoot;

		// Token: 0x0400018C RID: 396
		private bool _virtualPathRootSet;
	}
}
