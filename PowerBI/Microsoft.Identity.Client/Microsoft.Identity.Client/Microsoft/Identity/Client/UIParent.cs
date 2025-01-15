using System;
using System.ComponentModel;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000168 RID: 360
	[Obsolete("In MSAL.NET 3.x, you should directly pass the Activity (on Xamarin.Android), or Window (on .NET Framework and UWP) using AcquireTokenInteractiveParameterBuilder.WithParentActivityOrWindowSee https://aka.ms/msal-net-3-breaking-changes. ", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class UIParent
	{
		// Token: 0x0600119A RID: 4506 RVA: 0x0003BF62 File Offset: 0x0003A162
		[Obsolete("See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public UIParent()
		{
			throw new NotImplementedException("See https://aka.ms/msal-net-3-breaking-changes. ");
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x0003BF74 File Offset: 0x0003A174
		[Obsolete("See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public UIParent(object parent, bool useEmbeddedWebView)
		{
			throw new NotImplementedException("See https://aka.ms/msal-net-3-breaking-changes. ");
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x0003BF86 File Offset: 0x0003A186
		[Obsolete("See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static bool IsSystemWebviewAvailable()
		{
			throw new NotImplementedException("See https://aka.ms/msal-net-3-breaking-changes. ");
		}
	}
}
