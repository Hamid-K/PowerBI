using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace System.Web.Http
{
	// Token: 0x02000012 RID: 18
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class AcceptVerbsAttribute : Attribute, IActionHttpMethodProvider
	{
		// Token: 0x06000086 RID: 134 RVA: 0x00003A18 File Offset: 0x00001C18
		public AcceptVerbsAttribute(string method)
			: this(new string[] { method })
		{
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003A2C File Offset: 0x00001C2C
		public AcceptVerbsAttribute(params string[] methods)
		{
			Collection<HttpMethod> collection;
			if (methods == null)
			{
				collection = new Collection<HttpMethod>(new HttpMethod[0]);
			}
			else
			{
				collection = new Collection<HttpMethod>(methods.Select((string method) => HttpMethodHelper.GetHttpMethod(method)).ToArray<HttpMethod>());
			}
			this._httpMethods = collection;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003A84 File Offset: 0x00001C84
		internal AcceptVerbsAttribute(params HttpMethod[] methods)
		{
			this._httpMethods = new Collection<HttpMethod>(methods);
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00003A98 File Offset: 0x00001C98
		public Collection<HttpMethod> HttpMethods
		{
			get
			{
				return this._httpMethods;
			}
		}

		// Token: 0x04000012 RID: 18
		private readonly Collection<HttpMethod> _httpMethods;
	}
}
