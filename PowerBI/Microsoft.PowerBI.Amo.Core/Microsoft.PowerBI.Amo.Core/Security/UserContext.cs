using System;
using System.Net;
using System.Net.Http;
using Microsoft.AnalysisServices.Utilities;

namespace Microsoft.AnalysisServices.Security
{
	// Token: 0x02000153 RID: 339
	internal abstract class UserContext : Disposable
	{
		// Token: 0x06001181 RID: 4481 RVA: 0x0003D9A8 File Offset: 0x0003BBA8
		public virtual bool TryGetCredentials(out ICredentials credentials, out string groupName)
		{
			credentials = null;
			groupName = null;
			return false;
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x0003D9B1 File Offset: 0x0003BBB1
		public void ExecuteInUserContext(Action action)
		{
			base.ThrowIfAlreadyDisposed();
			this.ExecuteInUserContextImpl(action);
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x0003D9C0 File Offset: 0x0003BBC0
		public TResult ExecuteInUserContext<TResult>(Func<TResult> action)
		{
			base.ThrowIfAlreadyDisposed();
			return this.ExecuteInUserContextImpl<TResult>(action);
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x0003D9CF File Offset: 0x0003BBCF
		public void UpdateHttpRequest(HttpWebRequest request)
		{
			base.ThrowIfAlreadyDisposed();
			this.UpdateHttpRequestImpl(request);
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x0003D9DE File Offset: 0x0003BBDE
		public void UpdateHttpRequest(HttpRequestMessage request)
		{
			base.ThrowIfAlreadyDisposed();
			this.UpdateHttpRequestImpl(request);
		}

		// Token: 0x06001186 RID: 4486
		protected abstract void ExecuteInUserContextImpl(Action action);

		// Token: 0x06001187 RID: 4487
		protected abstract TResult ExecuteInUserContextImpl<TResult>(Func<TResult> action);

		// Token: 0x06001188 RID: 4488
		protected abstract void UpdateHttpRequestImpl(HttpWebRequest request);

		// Token: 0x06001189 RID: 4489
		protected abstract void UpdateHttpRequestImpl(HttpRequestMessage request);
	}
}
