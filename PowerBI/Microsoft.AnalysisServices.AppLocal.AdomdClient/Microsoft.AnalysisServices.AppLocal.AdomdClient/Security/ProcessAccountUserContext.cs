using System;
using System.Net;
using System.Net.Http;
using System.Security.Principal;

namespace Microsoft.AnalysisServices.AdomdClient.Security
{
	// Token: 0x0200015E RID: 350
	internal sealed class ProcessAccountUserContext : UserContext
	{
		// Token: 0x060010FC RID: 4348 RVA: 0x0003B068 File Offset: 0x00039268
		protected override void ExecuteInUserContextImpl(Action action)
		{
			using (WindowsIdentity.Impersonate(IntPtr.Zero))
			{
				action();
			}
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x0003B0A4 File Offset: 0x000392A4
		protected override TResult ExecuteInUserContextImpl<TResult>(Func<TResult> action)
		{
			TResult tresult;
			using (WindowsIdentity.Impersonate(IntPtr.Zero))
			{
				tresult = action();
			}
			return tresult;
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x0003B0E0 File Offset: 0x000392E0
		protected override void UpdateHttpRequestImpl(HttpWebRequest request)
		{
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x0003B0E2 File Offset: 0x000392E2
		protected override void UpdateHttpRequestImpl(HttpRequestMessage request)
		{
		}
	}
}
