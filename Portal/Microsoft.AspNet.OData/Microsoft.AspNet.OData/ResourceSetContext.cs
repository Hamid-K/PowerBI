using System;
using System.Collections;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;
using Microsoft.AspNet.OData.Adapters;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Formatter.Serialization;
using Microsoft.AspNet.OData.Interfaces;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData
{
	// Token: 0x0200000D RID: 13
	public class ResourceSetContext
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002F12 File Offset: 0x00001112
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00002F1A File Offset: 0x0000111A
		public HttpRequestMessage Request
		{
			get
			{
				return this._request;
			}
			set
			{
				this._request = value;
				this.InternalRequest = ((this._request != null) ? new WebApiRequestMessage(this._request) : null);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002F3F File Offset: 0x0000113F
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00002F47 File Offset: 0x00001147
		public HttpRequestContext RequestContext { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002F50 File Offset: 0x00001150
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00002F58 File Offset: 0x00001158
		public UrlHelper Url
		{
			get
			{
				return this._urlHelper;
			}
			set
			{
				this._urlHelper = value;
				this.InternalUrlHelper = ((this._urlHelper != null) ? new WebApiUrlHelper(this._urlHelper) : null);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002F7D File Offset: 0x0000117D
		public IEdmModel EdmModel
		{
			get
			{
				return this.Request.GetModel();
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002F8A File Offset: 0x0000118A
		internal static ResourceSetContext Create(ODataSerializerContext writeContext, IEnumerable resourceSetInstance)
		{
			return new ResourceSetContext
			{
				Request = writeContext.Request,
				EntitySetBase = (writeContext.NavigationSource as IEdmEntitySetBase),
				Url = writeContext.Url,
				ResourceSetInstance = resourceSetInstance
			};
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002FC1 File Offset: 0x000011C1
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00002FC9 File Offset: 0x000011C9
		public IEdmEntitySetBase EntitySetBase { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002FD2 File Offset: 0x000011D2
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00002FDA File Offset: 0x000011DA
		public object ResourceSetInstance { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002FE3 File Offset: 0x000011E3
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00002FEB File Offset: 0x000011EB
		internal IWebApiRequestMessage InternalRequest { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002FF4 File Offset: 0x000011F4
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00002FFC File Offset: 0x000011FC
		internal IWebApiUrlHelper InternalUrlHelper { get; private set; }

		// Token: 0x0400000E RID: 14
		private HttpRequestMessage _request;

		// Token: 0x0400000F RID: 15
		private UrlHelper _urlHelper;
	}
}
