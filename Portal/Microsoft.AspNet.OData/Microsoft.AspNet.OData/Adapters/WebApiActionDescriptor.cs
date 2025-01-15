using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Controllers;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Interfaces;

namespace Microsoft.AspNet.OData.Adapters
{
	// Token: 0x020001D8 RID: 472
	internal class WebApiActionDescriptor : IWebApiActionDescriptor
	{
		// Token: 0x06000F7D RID: 3965 RVA: 0x0003F560 File Offset: 0x0003D760
		public WebApiActionDescriptor(HttpActionDescriptor actionDescriptor)
		{
			if (actionDescriptor == null)
			{
				throw Error.ArgumentNull("actionDescriptor");
			}
			this.innerDescriptor = actionDescriptor;
			if (actionDescriptor.SupportedHttpMethods != null)
			{
				this.supportedHttpMethods = new List<ODataRequestMethod>();
				foreach (HttpMethod httpMethod in actionDescriptor.SupportedHttpMethods)
				{
					bool flag = true;
					ODataRequestMethod odataRequestMethod = ODataRequestMethod.Unknown;
					if (Enum.TryParse<ODataRequestMethod>(httpMethod.Method, flag, out odataRequestMethod))
					{
						this.supportedHttpMethods.Add(odataRequestMethod);
					}
				}
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000F7E RID: 3966 RVA: 0x0003F5F4 File Offset: 0x0003D7F4
		public string ControllerName
		{
			get
			{
				if (this.innerDescriptor.ControllerDescriptor == null)
				{
					return null;
				}
				return this.innerDescriptor.ControllerDescriptor.ControllerName;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000F7F RID: 3967 RVA: 0x0003F615 File Offset: 0x0003D815
		public string ActionName
		{
			get
			{
				return this.innerDescriptor.ActionName;
			}
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x0003F622 File Offset: 0x0003D822
		public IEnumerable<T> GetCustomAttributes<T>(bool inherit) where T : Attribute
		{
			return this.innerDescriptor.GetCustomAttributes<T>(inherit);
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x0003F630 File Offset: 0x0003D830
		public bool IsHttpMethodSupported(ODataRequestMethod method)
		{
			return this.supportedHttpMethods == null || this.supportedHttpMethods.Contains(method);
		}

		// Token: 0x04000447 RID: 1095
		private IList<ODataRequestMethod> supportedHttpMethods;

		// Token: 0x04000448 RID: 1096
		private HttpActionDescriptor innerDescriptor;
	}
}
