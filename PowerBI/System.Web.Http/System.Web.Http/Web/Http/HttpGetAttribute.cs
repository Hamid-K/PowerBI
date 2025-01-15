using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace System.Web.Http
{
	// Token: 0x0200002C RID: 44
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class HttpGetAttribute : Attribute, IActionHttpMethodProvider
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600010E RID: 270 RVA: 0x000049D3 File Offset: 0x00002BD3
		public Collection<HttpMethod> HttpMethods
		{
			get
			{
				return HttpGetAttribute._supportedMethods;
			}
		}

		// Token: 0x04000039 RID: 57
		private static readonly Collection<HttpMethod> _supportedMethods = new Collection<HttpMethod>(new HttpMethod[] { HttpMethod.Get });
	}
}
