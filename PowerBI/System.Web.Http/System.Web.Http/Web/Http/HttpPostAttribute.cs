using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace System.Web.Http
{
	// Token: 0x0200002D RID: 45
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class HttpPostAttribute : Attribute, IActionHttpMethodProvider
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000111 RID: 273 RVA: 0x000049F4 File Offset: 0x00002BF4
		public Collection<HttpMethod> HttpMethods
		{
			get
			{
				return HttpPostAttribute._supportedMethods;
			}
		}

		// Token: 0x0400003A RID: 58
		private static readonly Collection<HttpMethod> _supportedMethods = new Collection<HttpMethod>(new HttpMethod[] { HttpMethod.Post });
	}
}
