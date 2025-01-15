using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.ModelBinding;

namespace System.Web.Http.Controllers
{
	// Token: 0x020000F7 RID: 247
	public class HttpActionContext
	{
		// Token: 0x06000668 RID: 1640 RVA: 0x0001029C File Offset: 0x0000E49C
		public HttpActionContext(HttpControllerContext controllerContext, HttpActionDescriptor actionDescriptor)
		{
			if (controllerContext == null)
			{
				throw Error.ArgumentNull("controllerContext");
			}
			if (actionDescriptor == null)
			{
				throw Error.ArgumentNull("actionDescriptor");
			}
			this._controllerContext = controllerContext;
			this._actionDescriptor = actionDescriptor;
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x000102EF File Offset: 0x0000E4EF
		public HttpActionContext()
		{
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x0001030D File Offset: 0x0000E50D
		// (set) Token: 0x0600066B RID: 1643 RVA: 0x00010315 File Offset: 0x0000E515
		public HttpControllerContext ControllerContext
		{
			get
			{
				return this._controllerContext;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._controllerContext = value;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x0600066C RID: 1644 RVA: 0x00010327 File Offset: 0x0000E527
		// (set) Token: 0x0600066D RID: 1645 RVA: 0x0001032F File Offset: 0x0000E52F
		public HttpActionDescriptor ActionDescriptor
		{
			get
			{
				return this._actionDescriptor;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._actionDescriptor = value;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x00010341 File Offset: 0x0000E541
		public ModelStateDictionary ModelState
		{
			get
			{
				return this._modelState;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x00010349 File Offset: 0x0000E549
		public Dictionary<string, object> ActionArguments
		{
			get
			{
				return this._operationArguments;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x00010351 File Offset: 0x0000E551
		// (set) Token: 0x06000671 RID: 1649 RVA: 0x00010359 File Offset: 0x0000E559
		public HttpResponseMessage Response { get; set; }

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000672 RID: 1650 RVA: 0x00010362 File Offset: 0x0000E562
		public HttpRequestMessage Request
		{
			get
			{
				if (this._controllerContext == null)
				{
					return null;
				}
				return this._controllerContext.Request;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x00010379 File Offset: 0x0000E579
		public HttpRequestContext RequestContext
		{
			get
			{
				if (this._controllerContext == null)
				{
					return null;
				}
				return this._controllerContext.RequestContext;
			}
		}

		// Token: 0x04000199 RID: 409
		private readonly ModelStateDictionary _modelState = new ModelStateDictionary();

		// Token: 0x0400019A RID: 410
		private readonly Dictionary<string, object> _operationArguments = new Dictionary<string, object>();

		// Token: 0x0400019B RID: 411
		private HttpActionDescriptor _actionDescriptor;

		// Token: 0x0400019C RID: 412
		private HttpControllerContext _controllerContext;
	}
}
