using System;
using System.Threading;
using System.Windows.Forms;
using Microsoft.AnalysisServices.AdomdClient.Utilities;
using Microsoft.Identity.Client;

namespace Microsoft.AnalysisServices.AdomdClient.Authentication
{
	// Token: 0x02000107 RID: 263
	internal static class MsalAuthenticationHelper
	{
		// Token: 0x06000F18 RID: 3864 RVA: 0x00033421 File Offset: 0x00031621
		public static AcquireTokenInteractiveParameterBuilder WithCustomPlatformUi(this AcquireTokenInteractiveParameterBuilder builder)
		{
			return builder;
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x00033424 File Offset: 0x00031624
		public static IDisposable CreateMsalInteractiveAuthenticationScope()
		{
			return new MsalAuthenticationHelper.InteractiveAuthenticationScope();
		}

		// Token: 0x020001DE RID: 478
		private sealed class InteractiveAuthenticationScope : Disposable
		{
			// Token: 0x06001427 RID: 5159 RVA: 0x00045DC8 File Offset: 0x00043FC8
			public InteractiveAuthenticationScope()
			{
				if (Application.MessageLoop)
				{
					this.context = SynchronizationContext.Current;
					if (this.context != null)
					{
						SynchronizationContext.SetSynchronizationContext(null);
					}
				}
			}

			// Token: 0x06001428 RID: 5160 RVA: 0x00045DF0 File Offset: 0x00043FF0
			protected override void Dispose(bool disposing)
			{
				if (disposing && this.context != null)
				{
					SynchronizationContext.SetSynchronizationContext(this.context);
				}
				base.Dispose(disposing);
			}

			// Token: 0x04000E46 RID: 3654
			private readonly SynchronizationContext context;
		}
	}
}
