using System;
using Microsoft.Owin.Infrastructure;

namespace Microsoft.Owin.Helpers
{
	// Token: 0x0200003F RID: 63
	public static class WebHelpers
	{
		// Token: 0x0600024A RID: 586 RVA: 0x00006678 File Offset: 0x00004878
		public static IFormCollection ParseForm(string text)
		{
			return OwinHelpers.GetForm(text);
		}
	}
}
