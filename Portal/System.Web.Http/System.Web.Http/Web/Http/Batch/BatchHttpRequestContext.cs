using System;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;

namespace System.Web.Http.Batch
{
	// Token: 0x02000110 RID: 272
	internal class BatchHttpRequestContext : HttpRequestContext
	{
		// Token: 0x06000735 RID: 1845 RVA: 0x00011E57 File Offset: 0x00010057
		public BatchHttpRequestContext(HttpRequestContext batchContext)
		{
			if (batchContext == null)
			{
				throw new ArgumentNullException("batchContext");
			}
			this._batchContext = batchContext;
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x00011E74 File Offset: 0x00010074
		public HttpRequestContext BatchContext
		{
			get
			{
				return this._batchContext;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x00011E7C File Offset: 0x0001007C
		// (set) Token: 0x06000738 RID: 1848 RVA: 0x00011E89 File Offset: 0x00010089
		public override X509Certificate2 ClientCertificate
		{
			get
			{
				return this._batchContext.ClientCertificate;
			}
			set
			{
				this._batchContext.ClientCertificate = value;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x00011E97 File Offset: 0x00010097
		// (set) Token: 0x0600073A RID: 1850 RVA: 0x00011E9F File Offset: 0x0001009F
		public override HttpConfiguration Configuration
		{
			get
			{
				return base.Configuration;
			}
			set
			{
				base.Configuration = value;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x00011EA8 File Offset: 0x000100A8
		// (set) Token: 0x0600073C RID: 1852 RVA: 0x00011EB5 File Offset: 0x000100B5
		public override bool IncludeErrorDetail
		{
			get
			{
				return this._batchContext.IncludeErrorDetail;
			}
			set
			{
				this._batchContext.IncludeErrorDetail = value;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x0600073D RID: 1853 RVA: 0x00011EC3 File Offset: 0x000100C3
		// (set) Token: 0x0600073E RID: 1854 RVA: 0x00011ED0 File Offset: 0x000100D0
		public override bool IsLocal
		{
			get
			{
				return this._batchContext.IsLocal;
			}
			set
			{
				this._batchContext.IsLocal = value;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x0600073F RID: 1855 RVA: 0x00011EDE File Offset: 0x000100DE
		// (set) Token: 0x06000740 RID: 1856 RVA: 0x00011EEB File Offset: 0x000100EB
		public override IPrincipal Principal
		{
			get
			{
				return this._batchContext.Principal;
			}
			set
			{
				this._batchContext.Principal = value;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000741 RID: 1857 RVA: 0x00011EF9 File Offset: 0x000100F9
		// (set) Token: 0x06000742 RID: 1858 RVA: 0x00011F01 File Offset: 0x00010101
		public override IHttpRouteData RouteData
		{
			get
			{
				return base.RouteData;
			}
			set
			{
				base.RouteData = value;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x00011F0A File Offset: 0x0001010A
		// (set) Token: 0x06000744 RID: 1860 RVA: 0x00011F12 File Offset: 0x00010112
		public override UrlHelper Url
		{
			get
			{
				return base.Url;
			}
			set
			{
				base.Url = value;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x00011F1B File Offset: 0x0001011B
		// (set) Token: 0x06000746 RID: 1862 RVA: 0x00011F28 File Offset: 0x00010128
		public override string VirtualPathRoot
		{
			get
			{
				return this._batchContext.VirtualPathRoot;
			}
			set
			{
				this._batchContext.VirtualPathRoot = value;
			}
		}

		// Token: 0x040001D6 RID: 470
		private readonly HttpRequestContext _batchContext;
	}
}
