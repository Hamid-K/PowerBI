using System;
using System.Net;
using System.Net.Http;
using System.Security.Principal;

namespace Microsoft.AnalysisServices.AdomdClient.Security
{
	// Token: 0x0200015E RID: 350
	internal sealed class ProcessAccountUserContext : UserContext
	{
		// Token: 0x060010EF RID: 4335 RVA: 0x0003AD38 File Offset: 0x00038F38
		protected override void ExecuteInUserContextImpl(Action action)
		{
			using (WindowsIdentity.Impersonate(IntPtr.Zero))
			{
				action();
			}
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x0003AD74 File Offset: 0x00038F74
		protected override TResult ExecuteInUserContextImpl<TResult>(Func<TResult> action)
		{
			TResult tresult;
			using (WindowsIdentity.Impersonate(IntPtr.Zero))
			{
				tresult = action();
			}
			return tresult;
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x0003ADB0 File Offset: 0x00038FB0
		protected override void UpdateHttpRequestImpl(HttpWebRequest request)
		{
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x0003ADB2 File Offset: 0x00038FB2
		protected override void UpdateHttpRequestImpl(HttpRequestMessage request)
		{
		}
	}
}
