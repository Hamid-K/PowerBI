using System;
using System.Net;
using System.Net.Http;

namespace Microsoft.AnalysisServices.AdomdClient.Security
{
	// Token: 0x0200015C RID: 348
	internal sealed class TransparentUserContext : UserContext
	{
		// Token: 0x060010E0 RID: 4320 RVA: 0x0003ACCC File Offset: 0x00038ECC
		protected override void ExecuteInUserContextImpl(Action action)
		{
			action();
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x0003ACD4 File Offset: 0x00038ED4
		protected override TResult ExecuteInUserContextImpl<TResult>(Func<TResult> action)
		{
			return action();
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x0003ACDC File Offset: 0x00038EDC
		protected override void UpdateHttpRequestImpl(HttpWebRequest request)
		{
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x0003ACDE File Offset: 0x00038EDE
		protected override void UpdateHttpRequestImpl(HttpRequestMessage request)
		{
		}
	}
}
