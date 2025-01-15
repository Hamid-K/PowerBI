using System;
using System.Net;
using System.Net.Http;
using Microsoft.AnalysisServices.AdomdClient.Utilities;

namespace Microsoft.AnalysisServices.AdomdClient.Security
{
	// Token: 0x0200015D RID: 349
	internal abstract class UserContext : Disposable
	{
		// Token: 0x060010F2 RID: 4338 RVA: 0x0003B018 File Offset: 0x00039218
		public virtual bool TryGetCredentials(out ICredentials credentials, out string groupName)
		{
			credentials = null;
			groupName = null;
			return false;
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x0003B021 File Offset: 0x00039221
		public void ExecuteInUserContext(Action action)
		{
			base.ThrowIfAlreadyDisposed();
			this.ExecuteInUserContextImpl(action);
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x0003B030 File Offset: 0x00039230
		public TResult ExecuteInUserContext<TResult>(Func<TResult> action)
		{
			base.ThrowIfAlreadyDisposed();
			return this.ExecuteInUserContextImpl<TResult>(action);
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x0003B03F File Offset: 0x0003923F
		public void UpdateHttpRequest(HttpWebRequest request)
		{
			base.ThrowIfAlreadyDisposed();
			this.UpdateHttpRequestImpl(request);
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x0003B04E File Offset: 0x0003924E
		public void UpdateHttpRequest(HttpRequestMessage request)
		{
			base.ThrowIfAlreadyDisposed();
			this.UpdateHttpRequestImpl(request);
		}

		// Token: 0x060010F7 RID: 4343
		protected abstract void ExecuteInUserContextImpl(Action action);

		// Token: 0x060010F8 RID: 4344
		protected abstract TResult ExecuteInUserContextImpl<TResult>(Func<TResult> action);

		// Token: 0x060010F9 RID: 4345
		protected abstract void UpdateHttpRequestImpl(HttpWebRequest request);

		// Token: 0x060010FA RID: 4346
		protected abstract void UpdateHttpRequestImpl(HttpRequestMessage request);
	}
}
