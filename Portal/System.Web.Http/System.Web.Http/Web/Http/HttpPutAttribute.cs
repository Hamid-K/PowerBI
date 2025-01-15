using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace System.Web.Http
{
	// Token: 0x02000029 RID: 41
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class HttpPutAttribute : Attribute, IActionHttpMethodProvider
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00004751 File Offset: 0x00002951
		public Collection<HttpMethod> HttpMethods
		{
			get
			{
				return HttpPutAttribute._supportedMethods;
			}
		}

		// Token: 0x04000031 RID: 49
		private static readonly Collection<HttpMethod> _supportedMethods = new Collection<HttpMethod>(new HttpMethod[] { HttpMethod.Put });
	}
}
