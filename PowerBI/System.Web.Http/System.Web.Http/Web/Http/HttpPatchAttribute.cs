using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace System.Web.Http
{
	// Token: 0x02000021 RID: 33
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class HttpPatchAttribute : Attribute, IActionHttpMethodProvider
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00003E5F File Offset: 0x0000205F
		public Collection<HttpMethod> HttpMethods
		{
			get
			{
				return HttpPatchAttribute._supportedMethods;
			}
		}

		// Token: 0x04000024 RID: 36
		private static readonly Collection<HttpMethod> _supportedMethods = new Collection<HttpMethod>(new HttpMethod[]
		{
			new HttpMethod("PATCH")
		});
	}
}
