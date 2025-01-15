using System;
using System.Collections.Generic;
using Microsoft.Owin;

namespace System.Web.Http.Owin
{
	// Token: 0x02000013 RID: 19
	internal static class OwinResponseExtensions
	{
		// Token: 0x060000A9 RID: 169 RVA: 0x000036CC File Offset: 0x000018CC
		public static void DisableBuffering(this IOwinResponse response)
		{
			if (response == null)
			{
				throw new ArgumentNullException("response");
			}
			IDictionary<string, object> environment = response.Environment;
			if (environment == null)
			{
				return;
			}
			Action action;
			if (!environment.TryGetValue("server.DisableResponseBuffering", out action))
			{
				return;
			}
			action();
		}

		// Token: 0x0400002F RID: 47
		private const string DisableResponseBufferingKey = "server.DisableResponseBuffering";
	}
}
