using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Owin.Builder;
using Owin;

namespace Microsoft.Owin.Infrastructure
{
	// Token: 0x0200003C RID: 60
	public static class SignatureConversions
	{
		// Token: 0x06000242 RID: 578 RVA: 0x00006477 File Offset: 0x00004677
		public static void AddConversions(IAppBuilder app)
		{
			app.AddSignatureConversion(new Func<Func<IDictionary<string, object>, Task>, OwinMiddleware>(SignatureConversions.Conversion1));
			app.AddSignatureConversion(new Func<OwinMiddleware, Func<IDictionary<string, object>, Task>>(SignatureConversions.Conversion2));
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000649D File Offset: 0x0000469D
		private static OwinMiddleware Conversion1(Func<IDictionary<string, object>, Task> next)
		{
			return new AppFuncTransition(next);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x000064A5 File Offset: 0x000046A5
		private static Func<IDictionary<string, object>, Task> Conversion2(OwinMiddleware next)
		{
			return new Func<IDictionary<string, object>, Task>(new OwinMiddlewareTransition(next).Invoke);
		}
	}
}
