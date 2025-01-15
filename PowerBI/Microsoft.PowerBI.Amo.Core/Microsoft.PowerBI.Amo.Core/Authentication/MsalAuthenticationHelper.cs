using System;
using System.Threading;
using System.Windows.Forms;
using Microsoft.AnalysisServices.Utilities;
using Microsoft.Identity.Client;

namespace Microsoft.AnalysisServices.Authentication
{
	// Token: 0x020000FC RID: 252
	internal static class MsalAuthenticationHelper
	{
		// Token: 0x06000FB4 RID: 4020 RVA: 0x00036065 File Offset: 0x00034265
		public static AcquireTokenInteractiveParameterBuilder WithCustomPlatformUi(this AcquireTokenInteractiveParameterBuilder builder)
		{
			return builder;
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x00036068 File Offset: 0x00034268
		public static IDisposable CreateMsalInteractiveAuthenticationScope()
		{
			return new MsalAuthenticationHelper.InteractiveAuthenticationScope();
		}

		// Token: 0x020001BB RID: 443
		private sealed class InteractiveAuthenticationScope : Disposable
		{
			// Token: 0x0600138F RID: 5007 RVA: 0x00044524 File Offset: 0x00042724
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

			// Token: 0x06001390 RID: 5008 RVA: 0x0004454C File Offset: 0x0004274C
			protected override void Dispose(bool disposing)
			{
				if (disposing && this.context != null)
				{
					SynchronizationContext.SetSynchronizationContext(this.context);
				}
				base.Dispose(disposing);
			}

			// Token: 0x04001112 RID: 4370
			private readonly SynchronizationContext context;
		}
	}
}
