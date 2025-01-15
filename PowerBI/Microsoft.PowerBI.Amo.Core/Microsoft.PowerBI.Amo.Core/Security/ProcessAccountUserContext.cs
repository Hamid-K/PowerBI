using System;
using System.Net;
using System.Net.Http;
using System.Security.Principal;

namespace Microsoft.AnalysisServices.Security
{
	// Token: 0x02000154 RID: 340
	internal sealed class ProcessAccountUserContext : UserContext
	{
		// Token: 0x0600118B RID: 4491 RVA: 0x0003D9F8 File Offset: 0x0003BBF8
		protected override void ExecuteInUserContextImpl(Action action)
		{
			using (WindowsIdentity.Impersonate(IntPtr.Zero))
			{
				action();
			}
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x0003DA34 File Offset: 0x0003BC34
		protected override TResult ExecuteInUserContextImpl<TResult>(Func<TResult> action)
		{
			TResult tresult;
			using (WindowsIdentity.Impersonate(IntPtr.Zero))
			{
				tresult = action();
			}
			return tresult;
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x0003DA70 File Offset: 0x0003BC70
		protected override void UpdateHttpRequestImpl(HttpWebRequest request)
		{
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x0003DA72 File Offset: 0x0003BC72
		protected override void UpdateHttpRequestImpl(HttpRequestMessage request)
		{
		}
	}
}
