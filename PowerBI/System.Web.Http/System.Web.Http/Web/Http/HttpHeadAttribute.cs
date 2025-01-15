using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace System.Web.Http
{
	// Token: 0x02000020 RID: 32
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class HttpHeadAttribute : Attribute, IActionHttpMethodProvider
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00003E3E File Offset: 0x0000203E
		public Collection<HttpMethod> HttpMethods
		{
			get
			{
				return HttpHeadAttribute._supportedMethods;
			}
		}

		// Token: 0x04000023 RID: 35
		private static readonly Collection<HttpMethod> _supportedMethods = new Collection<HttpMethod>(new HttpMethod[] { HttpMethod.Head });
	}
}
