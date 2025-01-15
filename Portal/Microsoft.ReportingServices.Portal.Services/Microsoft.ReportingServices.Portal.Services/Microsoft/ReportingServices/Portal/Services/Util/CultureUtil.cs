using System;
using System.Globalization;
using System.Threading;
using Microsoft.BIServer.Owin.Common.Services;

namespace Microsoft.ReportingServices.Portal.Services.Util
{
	// Token: 0x02000025 RID: 37
	internal static class CultureUtil
	{
		// Token: 0x060001D5 RID: 469 RVA: 0x0000CF44 File Offset: 0x0000B144
		internal static void ExecuteInCulture(string culture, Action action)
		{
			OwinSynchronizationContext owinSynchronizationContext = SynchronizationContext.Current as OwinSynchronizationContext;
			if (owinSynchronizationContext == null)
			{
				action();
				return;
			}
			var <>f__AnonymousType = new
			{
				Culture = owinSynchronizationContext.ThreadCultureInfo,
				AcceptLanguages = owinSynchronizationContext.AcceptLanguages
			};
			try
			{
				OwinSynchronizationContext.SetLanguageInfo(new CultureInfo(culture), culture);
				action();
			}
			finally
			{
				OwinSynchronizationContext.SetLanguageInfo(<>f__AnonymousType.Culture, <>f__AnonymousType.AcceptLanguages);
			}
		}
	}
}
