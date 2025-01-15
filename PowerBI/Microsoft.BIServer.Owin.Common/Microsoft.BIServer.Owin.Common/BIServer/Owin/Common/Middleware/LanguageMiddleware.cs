using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.BIServer.Owin.Common.Services;
using Microsoft.Owin;

namespace Microsoft.BIServer.Owin.Common.Middleware
{
	// Token: 0x02000018 RID: 24
	public sealed class LanguageMiddleware : OwinMiddleware
	{
		// Token: 0x06000068 RID: 104 RVA: 0x00002E31 File Offset: 0x00001031
		public LanguageMiddleware(OwinMiddleware next)
			: base(next)
		{
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002FA8 File Offset: 0x000011A8
		public override Task Invoke(IOwinContext context)
		{
			CultureInfo clientCultureInfo = LanguageUtils.GetClientCultureInfo(context);
			string text = string.Format("{0},{1};q=1", clientCultureInfo.Name, clientCultureInfo.Parent.Name);
			OwinSynchronizationContext.SetLanguageInfo(clientCultureInfo, text);
			return base.Next.Invoke(context);
		}
	}
}
