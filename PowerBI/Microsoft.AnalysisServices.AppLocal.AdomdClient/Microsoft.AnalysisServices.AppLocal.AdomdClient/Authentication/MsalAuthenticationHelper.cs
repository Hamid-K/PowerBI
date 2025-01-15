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
		// Token: 0x06000F25 RID: 3877 RVA: 0x00033751 File Offset: 0x00031951
		public static AcquireTokenInteractiveParameterBuilder WithCustomPlatformUi(this AcquireTokenInteractiveParameterBuilder builder)
		{
			return builder;
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x00033754 File Offset: 0x00031954
		public static IDisposable CreateMsalInteractiveAuthenticationScope()
		{
			return new MsalAuthenticationHelper.InteractiveAuthenticationScope();
		}

		// Token: 0x020001DE RID: 478
		private sealed class InteractiveAuthenticationScope : Disposable
		{
			// Token: 0x06001434 RID: 5172 RVA: 0x00046304 File Offset: 0x00044504
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

			// Token: 0x06001435 RID: 5173 RVA: 0x0004632C File Offset: 0x0004452C
			protected override void Dispose(bool disposing)
			{
				if (disposing && this.context != null)
				{
					SynchronizationContext.SetSynchronizationContext(this.context);
				}
				base.Dispose(disposing);
			}

			// Token: 0x04000E57 RID: 3671
			private readonly SynchronizationContext context;
		}
	}
}
