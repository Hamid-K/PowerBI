using System;
using System.Net;
using System.Net.Http;

namespace Microsoft.AnalysisServices.Security
{
	// Token: 0x02000152 RID: 338
	internal sealed class TransparentUserContext : UserContext
	{
		// Token: 0x0600117C RID: 4476 RVA: 0x0003D98C File Offset: 0x0003BB8C
		protected override void ExecuteInUserContextImpl(Action action)
		{
			action();
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x0003D994 File Offset: 0x0003BB94
		protected override TResult ExecuteInUserContextImpl<TResult>(Func<TResult> action)
		{
			return action();
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x0003D99C File Offset: 0x0003BB9C
		protected override void UpdateHttpRequestImpl(HttpWebRequest request)
		{
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x0003D99E File Offset: 0x0003BB9E
		protected override void UpdateHttpRequestImpl(HttpRequestMessage request)
		{
		}
	}
}
