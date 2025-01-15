using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace System.Web.Http
{
	// Token: 0x0200002A RID: 42
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class HttpDeleteAttribute : Attribute, IActionHttpMethodProvider
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00004772 File Offset: 0x00002972
		public Collection<HttpMethod> HttpMethods
		{
			get
			{
				return HttpDeleteAttribute._supportedMethods;
			}
		}

		// Token: 0x04000032 RID: 50
		private static readonly Collection<HttpMethod> _supportedMethods = new Collection<HttpMethod>(new HttpMethod[] { HttpMethod.Delete });
	}
}
