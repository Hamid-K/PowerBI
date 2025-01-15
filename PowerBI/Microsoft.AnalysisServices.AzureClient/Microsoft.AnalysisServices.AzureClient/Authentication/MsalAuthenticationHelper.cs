using System;
using System.Threading;
using System.Windows.Forms;
using Microsoft.AnalysisServices.AzureClient.Utilities;
using Microsoft.Identity.Client;

namespace Microsoft.AnalysisServices.AzureClient.Authentication
{
	// Token: 0x02000020 RID: 32
	internal static class MsalAuthenticationHelper
	{
		// Token: 0x060000E7 RID: 231 RVA: 0x000047DF File Offset: 0x000029DF
		public static AcquireTokenInteractiveParameterBuilder WithCustomPlatformUi(this AcquireTokenInteractiveParameterBuilder builder)
		{
			return builder;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000047E2 File Offset: 0x000029E2
		public static IDisposable CreateMsalInteractiveAuthenticationScope()
		{
			return new MsalAuthenticationHelper.InteractiveAuthenticationScope();
		}

		// Token: 0x02000058 RID: 88
		private sealed class InteractiveAuthenticationScope : Disposable
		{
			// Token: 0x06000272 RID: 626 RVA: 0x0000BB2C File Offset: 0x00009D2C
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

			// Token: 0x06000273 RID: 627 RVA: 0x0000BB54 File Offset: 0x00009D54
			protected override void Dispose(bool disposing)
			{
				if (disposing && this.context != null)
				{
					SynchronizationContext.SetSynchronizationContext(this.context);
				}
				base.Dispose(disposing);
			}

			// Token: 0x040001B2 RID: 434
			private readonly SynchronizationContext context;
		}
	}
}
