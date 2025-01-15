using System;
using System.Net;
using System.Net.Http;

namespace Microsoft.AnalysisServices.AdomdClient.Security
{
	// Token: 0x0200015C RID: 348
	internal sealed class TransparentUserContext : UserContext
	{
		// Token: 0x060010ED RID: 4333 RVA: 0x0003AFFC File Offset: 0x000391FC
		protected override void ExecuteInUserContextImpl(Action action)
		{
			action();
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x0003B004 File Offset: 0x00039204
		protected override TResult ExecuteInUserContextImpl<TResult>(Func<TResult> action)
		{
			return action();
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x0003B00C File Offset: 0x0003920C
		protected override void UpdateHttpRequestImpl(HttpWebRequest request)
		{
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x0003B00E File Offset: 0x0003920E
		protected override void UpdateHttpRequestImpl(HttpRequestMessage request)
		{
		}
	}
}
