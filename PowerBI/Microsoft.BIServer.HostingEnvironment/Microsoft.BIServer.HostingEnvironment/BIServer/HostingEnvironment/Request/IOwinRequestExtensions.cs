using System;
using System.Linq;
using Microsoft.Owin;

namespace Microsoft.BIServer.HostingEnvironment.Request
{
	// Token: 0x02000029 RID: 41
	public static class IOwinRequestExtensions
	{
		// Token: 0x06000122 RID: 290 RVA: 0x00004B9C File Offset: 0x00002D9C
		public static string GetHeaderValue(this IOwinRequest request, string headerName)
		{
			string text = null;
			if (request.Headers.ContainsKey(headerName))
			{
				text = request.Headers.GetValues(headerName).FirstOrDefault<string>();
			}
			return text ?? string.Empty;
		}
	}
}
