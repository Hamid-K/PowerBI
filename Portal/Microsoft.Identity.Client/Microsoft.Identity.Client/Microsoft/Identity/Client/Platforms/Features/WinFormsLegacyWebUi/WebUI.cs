using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Http;
using Microsoft.Identity.Client.Internal;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs;
using Microsoft.Identity.Client.UI;
using Microsoft.Identity.Client.Utils;

namespace Microsoft.Identity.Client.Platforms.Features.WinFormsLegacyWebUi
{
	// Token: 0x020001B0 RID: 432
	internal abstract class WebUI : IWebUI
	{
		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06001376 RID: 4982 RVA: 0x00041539 File Offset: 0x0003F739
		// (set) Token: 0x06001377 RID: 4983 RVA: 0x00041541 File Offset: 0x0003F741
		private protected Uri RequestUri { protected get; private set; }

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06001378 RID: 4984 RVA: 0x0004154A File Offset: 0x0003F74A
		// (set) Token: 0x06001379 RID: 4985 RVA: 0x00041552 File Offset: 0x0003F752
		private protected Uri CallbackUri { protected get; private set; }

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x0600137A RID: 4986 RVA: 0x0004155B File Offset: 0x0003F75B
		// (set) Token: 0x0600137B RID: 4987 RVA: 0x00041563 File Offset: 0x0003F763
		public object OwnerWindow { get; set; }

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x0600137C RID: 4988 RVA: 0x0004156C File Offset: 0x0003F76C
		// (set) Token: 0x0600137D RID: 4989 RVA: 0x00041574 File Offset: 0x0003F774
		protected SynchronizationContext SynchronizationContext { get; set; }

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x0600137E RID: 4990 RVA: 0x0004157D File Offset: 0x0003F77D
		// (set) Token: 0x0600137F RID: 4991 RVA: 0x00041585 File Offset: 0x0003F785
		public RequestContext RequestContext { get; set; }

		// Token: 0x06001380 RID: 4992 RVA: 0x00041590 File Offset: 0x0003F790
		public async Task<AuthorizationResult> AcquireAuthorizationAsync(Uri authorizationUri, Uri redirectUri, RequestContext requestContext, CancellationToken cancellationToken)
		{
			AuthorizationResult authorizationResult = null;
			UriBuilder uriBuilder = new UriBuilder(authorizationUri);
			uriBuilder.AppendOrReplaceQueryParameter("response_mode", "form_post");
			authorizationUri = uriBuilder.Uri;
			Action action = delegate
			{
				authorizationResult = this.Authenticate(authorizationUri, redirectUri, cancellationToken);
			};
			Action<object> action2 = delegate(object tcs)
			{
				try
				{
					authorizationResult = this.Authenticate(authorizationUri, redirectUri, cancellationToken);
					((TaskCompletionSource<object>)tcs).TrySetResult(null);
				}
				catch (Exception ex4)
				{
					((TaskCompletionSource<object>)tcs).TrySetException(ex4);
				}
			};
			if (Thread.CurrentThread.GetApartmentState() == ApartmentState.MTA)
			{
				if (this.SynchronizationContext != null)
				{
					TaskCompletionSource<object> taskCompletionSource = new TaskCompletionSource<object>();
					this.SynchronizationContext.Post(new SendOrPostCallback(action2.Invoke), taskCompletionSource);
					await taskCompletionSource.Task.ConfigureAwait(false);
					goto IL_020D;
				}
				using (StaTaskScheduler staTaskScheduler = new StaTaskScheduler(1))
				{
					try
					{
						Task.Factory.StartNew(action, cancellationToken, TaskCreationOptions.None, staTaskScheduler).Wait(cancellationToken);
					}
					catch (AggregateException ex)
					{
						requestContext.Logger.ErrorPii(ex.InnerException);
						Exception ex2 = ex.InnerExceptions[0];
						AggregateException ex3 = ex2 as AggregateException;
						if (ex3 != null)
						{
							ex2 = ex3.InnerExceptions[0];
						}
						throw ex2;
					}
					goto IL_020D;
				}
			}
			action();
			IL_020D:
			return await Task.Factory.StartNew<AuthorizationResult>(() => authorizationResult, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x000415F4 File Offset: 0x0003F7F4
		internal AuthorizationResult Authenticate(Uri requestUri, Uri callbackUri, CancellationToken cancellationToken)
		{
			this.RequestUri = requestUri;
			this.CallbackUri = callbackUri;
			return this.OnAuthenticate(cancellationToken);
		}

		// Token: 0x06001382 RID: 4994
		protected abstract AuthorizationResult OnAuthenticate(CancellationToken cancellationToken);

		// Token: 0x06001383 RID: 4995 RVA: 0x0004160B File Offset: 0x0003F80B
		public Uri UpdateRedirectUri(Uri redirectUri)
		{
			RedirectUriHelper.Validate(redirectUri, false);
			return redirectUri;
		}
	}
}
