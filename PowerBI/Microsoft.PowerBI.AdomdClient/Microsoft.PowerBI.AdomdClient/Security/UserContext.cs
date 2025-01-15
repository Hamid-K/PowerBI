using System;
using System.Net;
using System.Net.Http;
using Microsoft.AnalysisServices.AdomdClient.Utilities;

namespace Microsoft.AnalysisServices.AdomdClient.Security
{
	// Token: 0x0200015D RID: 349
	internal abstract class UserContext : Disposable
	{
		// Token: 0x060010E5 RID: 4325 RVA: 0x0003ACE8 File Offset: 0x00038EE8
		public virtual bool TryGetCredentials(out ICredentials credentials, out string groupName)
		{
			credentials = null;
			groupName = null;
			return false;
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x0003ACF1 File Offset: 0x00038EF1
		public void ExecuteInUserContext(Action action)
		{
			base.ThrowIfAlreadyDisposed();
			this.ExecuteInUserContextImpl(action);
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x0003AD00 File Offset: 0x00038F00
		public TResult ExecuteInUserContext<TResult>(Func<TResult> action)
		{
			base.ThrowIfAlreadyDisposed();
			return this.ExecuteInUserContextImpl<TResult>(action);
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x0003AD0F File Offset: 0x00038F0F
		public void UpdateHttpRequest(HttpWebRequest request)
		{
			base.ThrowIfAlreadyDisposed();
			this.UpdateHttpRequestImpl(request);
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x0003AD1E File Offset: 0x00038F1E
		public void UpdateHttpRequest(HttpRequestMessage request)
		{
			base.ThrowIfAlreadyDisposed();
			this.UpdateHttpRequestImpl(request);
		}

		// Token: 0x060010EA RID: 4330
		protected abstract void ExecuteInUserContextImpl(Action action);

		// Token: 0x060010EB RID: 4331
		protected abstract TResult ExecuteInUserContextImpl<TResult>(Func<TResult> action);

		// Token: 0x060010EC RID: 4332
		protected abstract void UpdateHttpRequestImpl(HttpWebRequest request);

		// Token: 0x060010ED RID: 4333
		protected abstract void UpdateHttpRequestImpl(HttpRequestMessage request);
	}
}
