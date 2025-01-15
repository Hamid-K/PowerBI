using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace System.Web.Http
{
	// Token: 0x0200001F RID: 31
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class HttpOptionsAttribute : Attribute, IActionHttpMethodProvider
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00003E1D File Offset: 0x0000201D
		public Collection<HttpMethod> HttpMethods
		{
			get
			{
				return HttpOptionsAttribute._supportedMethods;
			}
		}

		// Token: 0x04000022 RID: 34
		private static readonly Collection<HttpMethod> _supportedMethods = new Collection<HttpMethod>(new HttpMethod[] { HttpMethod.Options });
	}
}
