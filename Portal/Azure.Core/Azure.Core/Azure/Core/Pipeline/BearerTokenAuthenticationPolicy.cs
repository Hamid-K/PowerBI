using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core.Diagnostics;

namespace Azure.Core.Pipeline
{
	// Token: 0x02000087 RID: 135
	[NullableContext(1)]
	[Nullable(0)]
	public class BearerTokenAuthenticationPolicy : HttpPipelinePolicy
	{
		// Token: 0x0600044A RID: 1098 RVA: 0x0000CD5A File Offset: 0x0000AF5A
		public BearerTokenAuthenticationPolicy(TokenCredential credential, string scope)
			: this(credential, new string[] { scope })
		{
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0000CD6D File Offset: 0x0000AF6D
		public BearerTokenAuthenticationPolicy(TokenCredential credential, IEnumerable<string> scopes)
			: this(credential, scopes, TimeSpan.FromMinutes(5.0), TimeSpan.FromSeconds(30.0))
		{
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0000CD93 File Offset: 0x0000AF93
		internal BearerTokenAuthenticationPolicy(TokenCredential credential, IEnumerable<string> scopes, TimeSpan tokenRefreshOffset, TimeSpan tokenRefreshRetryDelay)
		{
			Argument.AssertNotNull<TokenCredential>(credential, "credential");
			Argument.AssertNotNull<IEnumerable<string>>(scopes, "scopes");
			this._scopes = scopes.ToArray<string>();
			this._accessTokenCache = new BearerTokenAuthenticationPolicy.AccessTokenCache(credential, tokenRefreshOffset, tokenRefreshRetryDelay);
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0000CDCC File Offset: 0x0000AFCC
		public override ValueTask ProcessAsync(HttpMessage message, [Nullable(new byte[] { 0, 1 })] ReadOnlyMemory<HttpPipelinePolicy> pipeline)
		{
			return this.ProcessAsync(message, pipeline, true);
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0000CDD7 File Offset: 0x0000AFD7
		public override void Process(HttpMessage message, [Nullable(new byte[] { 0, 1 })] ReadOnlyMemory<HttpPipelinePolicy> pipeline)
		{
			this.ProcessAsync(message, pipeline, false).EnsureCompleted();
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0000CDE8 File Offset: 0x0000AFE8
		protected virtual ValueTask AuthorizeRequestAsync(HttpMessage message)
		{
			TokenRequestContext tokenRequestContext = new TokenRequestContext(this._scopes, message.Request.ClientRequestId);
			return this.AuthenticateAndAuthorizeRequestAsync(message, tokenRequestContext);
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0000CE18 File Offset: 0x0000B018
		protected virtual void AuthorizeRequest(HttpMessage message)
		{
			TokenRequestContext tokenRequestContext = new TokenRequestContext(this._scopes, message.Request.ClientRequestId);
			this.AuthenticateAndAuthorizeRequest(message, tokenRequestContext);
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0000CE48 File Offset: 0x0000B048
		[NullableContext(0)]
		protected virtual ValueTask<bool> AuthorizeRequestOnChallengeAsync([Nullable(1)] HttpMessage message)
		{
			return default(ValueTask<bool>);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0000CE5E File Offset: 0x0000B05E
		protected virtual bool AuthorizeRequestOnChallenge(HttpMessage message)
		{
			return false;
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0000CE64 File Offset: 0x0000B064
		private async ValueTask ProcessAsync(HttpMessage message, [Nullable(new byte[] { 0, 1 })] ReadOnlyMemory<HttpPipelinePolicy> pipeline, bool async)
		{
			if (message.Request.Uri.Scheme != Uri.UriSchemeHttps)
			{
				throw new InvalidOperationException("Bearer token authentication is not permitted for non TLS protected (https) endpoints.");
			}
			if (async)
			{
				await this.AuthorizeRequestAsync(message).ConfigureAwait(false);
				await HttpPipelinePolicy.ProcessNextAsync(message, pipeline).ConfigureAwait(false);
			}
			else
			{
				this.AuthorizeRequest(message);
				HttpPipelinePolicy.ProcessNext(message, pipeline);
			}
			if (message.Response.Status == 401 && message.Response.Headers.Contains(HttpHeader.Names.WwwAuthenticate))
			{
				if (async)
				{
					ConfiguredValueTaskAwaitable<bool>.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter = this.AuthorizeRequestOnChallengeAsync(message).ConfigureAwait(false).GetAwaiter();
					if (!configuredValueTaskAwaiter.IsCompleted)
					{
						await configuredValueTaskAwaiter;
						ConfiguredValueTaskAwaitable<bool>.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter2;
						configuredValueTaskAwaiter = configuredValueTaskAwaiter2;
						configuredValueTaskAwaiter2 = default(ConfiguredValueTaskAwaitable<bool>.ConfiguredValueTaskAwaiter);
					}
					if (configuredValueTaskAwaiter.GetResult())
					{
						await HttpPipelinePolicy.ProcessNextAsync(message, pipeline).ConfigureAwait(false);
					}
				}
				else if (this.AuthorizeRequestOnChallenge(message))
				{
					HttpPipelinePolicy.ProcessNext(message, pipeline);
				}
			}
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0000CEC0 File Offset: 0x0000B0C0
		protected async ValueTask AuthenticateAndAuthorizeRequestAsync(HttpMessage message, TokenRequestContext context)
		{
			string text = await this._accessTokenCache.GetHeaderValueAsync(message, context, true).ConfigureAwait(false);
			message.Request.Headers.SetValue(HttpHeader.Names.Authorization, text);
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0000CF14 File Offset: 0x0000B114
		protected void AuthenticateAndAuthorizeRequest(HttpMessage message, TokenRequestContext context)
		{
			string text = this._accessTokenCache.GetHeaderValueAsync(message, context, false).EnsureCompleted<string>();
			message.Request.Headers.SetValue(HttpHeader.Names.Authorization, text);
		}

		// Token: 0x040001C9 RID: 457
		private string[] _scopes;

		// Token: 0x040001CA RID: 458
		private readonly BearerTokenAuthenticationPolicy.AccessTokenCache _accessTokenCache;

		// Token: 0x02000111 RID: 273
		[Nullable(0)]
		private class AccessTokenCache
		{
			// Token: 0x060007A8 RID: 1960 RVA: 0x0001B9F1 File Offset: 0x00019BF1
			public AccessTokenCache(TokenCredential credential, TimeSpan tokenRefreshOffset, TimeSpan tokenRefreshRetryDelay)
			{
				this._credential = credential;
				this._tokenRefreshOffset = tokenRefreshOffset;
				this._tokenRefreshRetryDelay = tokenRefreshRetryDelay;
			}

			// Token: 0x060007A9 RID: 1961 RVA: 0x0001BA1C File Offset: 0x00019C1C
			[return: Nullable(new byte[] { 0, 1 })]
			public async ValueTask<string> GetHeaderValueAsync(HttpMessage message, TokenRequestContext context, bool async)
			{
				int maxCancellationRetries = 3;
				TaskCompletionSource<BearerTokenAuthenticationPolicy.AccessTokenCache.HeaderValueInfo> backgroundUpdateTcs;
				TaskCompletionSource<BearerTokenAuthenticationPolicy.AccessTokenCache.HeaderValueInfo> headerValueTcs;
				string text;
				for (;;)
				{
					global::System.ValueTuple<TaskCompletionSource<BearerTokenAuthenticationPolicy.AccessTokenCache.HeaderValueInfo>, TaskCompletionSource<BearerTokenAuthenticationPolicy.AccessTokenCache.HeaderValueInfo>, bool> taskCompletionSources = this.GetTaskCompletionSources(context);
					headerValueTcs = taskCompletionSources.Item1;
					backgroundUpdateTcs = taskCompletionSources.Item2;
					if (taskCompletionSources.Item3)
					{
						if (backgroundUpdateTcs != null)
						{
							break;
						}
						try
						{
							BearerTokenAuthenticationPolicy.AccessTokenCache.HeaderValueInfo headerValueInfo = await this.GetHeaderValueFromCredentialAsync(context, async, message.CancellationToken).ConfigureAwait(false);
							BearerTokenAuthenticationPolicy.AccessTokenCache.HeaderValueInfo info = headerValueInfo;
							headerValueTcs.SetResult(info);
						}
						catch (OperationCanceledException)
						{
							headerValueTcs.SetCanceled();
						}
						catch (Exception ex)
						{
							headerValueTcs.SetException(ex);
						}
					}
					Task<BearerTokenAuthenticationPolicy.AccessTokenCache.HeaderValueInfo> task = headerValueTcs.Task;
					try
					{
						if (!task.IsCompleted)
						{
							if (async)
							{
								await task.AwaitWithCancellation(message.CancellationToken);
							}
							else
							{
								try
								{
									task.Wait(message.CancellationToken);
								}
								catch (AggregateException)
								{
								}
							}
						}
						BearerTokenAuthenticationPolicy.AccessTokenCache.HeaderValueInfo info;
						if (async)
						{
							BearerTokenAuthenticationPolicy.AccessTokenCache.HeaderValueInfo headerValueInfo = await headerValueTcs.Task.ConfigureAwait(false);
							info = headerValueInfo;
						}
						else
						{
							info = headerValueTcs.Task.EnsureCompleted<BearerTokenAuthenticationPolicy.AccessTokenCache.HeaderValueInfo>();
						}
						text = info.HeaderValue;
					}
					catch (TaskCanceledException obj) when (!message.CancellationToken.IsCancellationRequested)
					{
						maxCancellationRetries--;
						if (!message.CancellationToken.CanBeCanceled && maxCancellationRetries <= 0)
						{
							throw;
						}
						continue;
					}
					return text;
				}
				if (CS$<>8__locals2.CS$<>8__locals1.async)
				{
					BearerTokenAuthenticationPolicy.AccessTokenCache.HeaderValueInfo headerValueInfo = await headerValueTcs.Task.ConfigureAwait(false);
					CS$<>8__locals2.info = headerValueInfo;
				}
				else
				{
					CS$<>8__locals2.info = headerValueTcs.Task.EnsureCompleted<BearerTokenAuthenticationPolicy.AccessTokenCache.HeaderValueInfo>();
				}
				Task.Run<ValueTask>(() => CS$<>8__locals2.CS$<>8__locals1.<>4__this.GetHeaderValueFromCredentialInBackgroundAsync(CS$<>8__locals2.CS$<>8__locals1.backgroundUpdateTcs, CS$<>8__locals2.info, CS$<>8__locals2.CS$<>8__locals1.context, CS$<>8__locals2.CS$<>8__locals1.async));
				text = CS$<>8__locals2.info.HeaderValue;
				return text;
			}

			// Token: 0x060007AA RID: 1962 RVA: 0x0001BA78 File Offset: 0x00019C78
			[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "InfoTcs", "BackgroundUpdateTcs", "GetTokenFromCredential" })]
			[return: Nullable(new byte[] { 0, 1, 2 })]
			private global::System.ValueTuple<TaskCompletionSource<BearerTokenAuthenticationPolicy.AccessTokenCache.HeaderValueInfo>, TaskCompletionSource<BearerTokenAuthenticationPolicy.AccessTokenCache.HeaderValueInfo>, bool> GetTaskCompletionSources(TokenRequestContext context)
			{
				BearerTokenAuthenticationPolicy.AccessTokenCache.TokenRequestState state = this._state;
				if (state != null && state.InfoTcs.Task.IsCompleted && !state.RequestRequiresNewToken(context))
				{
					DateTimeOffset utcNow = DateTimeOffset.UtcNow;
					if (!state.BackgroundTokenAcquiredSuccessfully(utcNow) && !state.AccessTokenFailedOrExpired(utcNow) && !state.TokenNeedsBackgroundRefresh(utcNow))
					{
						return new global::System.ValueTuple<TaskCompletionSource<BearerTokenAuthenticationPolicy.AccessTokenCache.HeaderValueInfo>, TaskCompletionSource<BearerTokenAuthenticationPolicy.AccessTokenCache.HeaderValueInfo>, bool>(state.InfoTcs, null, false);
					}
				}
				object syncObj = this._syncObj;
				global::System.ValueTuple<TaskCompletionSource<BearerTokenAuthenticationPolicy.AccessTokenCache.HeaderValueInfo>, TaskCompletionSource<BearerTokenAuthenticationPolicy.AccessTokenCache.HeaderValueInfo>, bool> valueTuple;
				lock (syncObj)
				{
					if (this._state == null || this._state.RequestRequiresNewToken(context))
					{
						this._state = new BearerTokenAuthenticationPolicy.AccessTokenCache.TokenRequestState(context, new TaskCompletionSource<BearerTokenAuthenticationPolicy.AccessTokenCache.HeaderValueInfo>(TaskCreationOptions.RunContinuationsAsynchronously), null);
						valueTuple = new global::System.ValueTuple<TaskCompletionSource<BearerTokenAuthenticationPolicy.AccessTokenCache.HeaderValueInfo>, TaskCompletionSource<BearerTokenAuthenticationPolicy.AccessTokenCache.HeaderValueInfo>, bool>(this._state.InfoTcs, this._state.BackgroundUpdateTcs, true);
					}
					else if (!this._state.InfoTcs.Task.IsCompleted)
					{
						if (this._state.BackgroundUpdateTcs != null)
						{
							this._state = new BearerTokenAuthenticationPolicy.AccessTokenCache.TokenRequestState(this._state.CurrentContext, this._state.InfoTcs, null);
						}
						valueTuple = new global::System.ValueTuple<TaskCompletionSource<BearerTokenAuthenticationPolicy.AccessTokenCache.HeaderValueInfo>, TaskCompletionSource<BearerTokenAuthenticationPolicy.AccessTokenCache.HeaderValueInfo>, bool>(this._state.InfoTcs, this._state.BackgroundUpdateTcs, false);
					}
					else
					{
						DateTimeOffset utcNow2 = DateTimeOffset.UtcNow;
						if (this._state.BackgroundTokenAcquiredSuccessfully(utcNow2))
						{
							this._state = new BearerTokenAuthenticationPolicy.AccessTokenCache.TokenRequestState(this._state.CurrentContext, this._state.BackgroundUpdateTcs, null);
						}
						if (this._state.AccessTokenFailedOrExpired(utcNow2))
						{
							this._state = new BearerTokenAuthenticationPolicy.AccessTokenCache.TokenRequestState(this._state.CurrentContext, new TaskCompletionSource<BearerTokenAuthenticationPolicy.AccessTokenCache.HeaderValueInfo>(TaskCreationOptions.RunContinuationsAsynchronously), this._state.BackgroundUpdateTcs);
							valueTuple = new global::System.ValueTuple<TaskCompletionSource<BearerTokenAuthenticationPolicy.AccessTokenCache.HeaderValueInfo>, TaskCompletionSource<BearerTokenAuthenticationPolicy.AccessTokenCache.HeaderValueInfo>, bool>(this._state.InfoTcs, null, true);
						}
						else if (this._state.TokenNeedsBackgroundRefresh(utcNow2))
						{
							this._state = new BearerTokenAuthenticationPolicy.AccessTokenCache.TokenRequestState(this._state.CurrentContext, this._state.InfoTcs, new TaskCompletionSource<BearerTokenAuthenticationPolicy.AccessTokenCache.HeaderValueInfo>(TaskCreationOptions.RunContinuationsAsynchronously));
							valueTuple = new global::System.ValueTuple<TaskCompletionSource<BearerTokenAuthenticationPolicy.AccessTokenCache.HeaderValueInfo>, TaskCompletionSource<BearerTokenAuthenticationPolicy.AccessTokenCache.HeaderValueInfo>, bool>(this._state.InfoTcs, this._state.BackgroundUpdateTcs, true);
						}
						else
						{
							valueTuple = new global::System.ValueTuple<TaskCompletionSource<BearerTokenAuthenticationPolicy.AccessTokenCache.HeaderValueInfo>, TaskCompletionSource<BearerTokenAuthenticationPolicy.AccessTokenCache.HeaderValueInfo>, bool>(this._state.InfoTcs, null, false);
						}
					}
				}
				return valueTuple;
			}

			// Token: 0x060007AB RID: 1963 RVA: 0x0001BCBC File Offset: 0x00019EBC
			private async ValueTask GetHeaderValueFromCredentialInBackgroundAsync(TaskCompletionSource<BearerTokenAuthenticationPolicy.AccessTokenCache.HeaderValueInfo> backgroundUpdateTcs, BearerTokenAuthenticationPolicy.AccessTokenCache.HeaderValueInfo info, TokenRequestContext context, bool async)
			{
				CancellationTokenSource cts = new CancellationTokenSource(this._tokenRefreshRetryDelay);
				try
				{
					BearerTokenAuthenticationPolicy.AccessTokenCache.HeaderValueInfo headerValueInfo = await this.GetHeaderValueFromCredentialAsync(context, async, cts.Token).ConfigureAwait(false);
					backgroundUpdateTcs.SetResult(headerValueInfo);
				}
				catch (OperationCanceledException ex) when (cts.IsCancellationRequested)
				{
					backgroundUpdateTcs.SetResult(new BearerTokenAuthenticationPolicy.AccessTokenCache.HeaderValueInfo(info.HeaderValue, info.ExpiresOn, DateTimeOffset.UtcNow));
					AzureCoreEventSource.Singleton.BackgroundRefreshFailed(context.ParentRequestId ?? string.Empty, ex.ToString());
				}
				catch (Exception ex2)
				{
					backgroundUpdateTcs.SetResult(new BearerTokenAuthenticationPolicy.AccessTokenCache.HeaderValueInfo(info.HeaderValue, info.ExpiresOn, DateTimeOffset.UtcNow + this._tokenRefreshRetryDelay));
					AzureCoreEventSource.Singleton.BackgroundRefreshFailed(context.ParentRequestId ?? string.Empty, ex2.ToString());
				}
				finally
				{
					cts.Dispose();
				}
			}

			// Token: 0x060007AC RID: 1964 RVA: 0x0001BD20 File Offset: 0x00019F20
			[NullableContext(0)]
			private async ValueTask<BearerTokenAuthenticationPolicy.AccessTokenCache.HeaderValueInfo> GetHeaderValueFromCredentialAsync(TokenRequestContext context, bool async, CancellationToken cancellationToken)
			{
				AccessToken accessToken;
				if (async)
				{
					accessToken = await this._credential.GetTokenAsync(context, cancellationToken).ConfigureAwait(false);
				}
				else
				{
					accessToken = this._credential.GetToken(context, cancellationToken);
				}
				AccessToken accessToken2 = accessToken;
				return new BearerTokenAuthenticationPolicy.AccessTokenCache.HeaderValueInfo("Bearer " + accessToken2.Token, accessToken2.ExpiresOn, accessToken2.ExpiresOn - this._tokenRefreshOffset);
			}

			// Token: 0x040003F4 RID: 1012
			private readonly object _syncObj = new object();

			// Token: 0x040003F5 RID: 1013
			private readonly TokenCredential _credential;

			// Token: 0x040003F6 RID: 1014
			private readonly TimeSpan _tokenRefreshOffset;

			// Token: 0x040003F7 RID: 1015
			private readonly TimeSpan _tokenRefreshRetryDelay;

			// Token: 0x040003F8 RID: 1016
			[Nullable(2)]
			private BearerTokenAuthenticationPolicy.AccessTokenCache.TokenRequestState _state;

			// Token: 0x02000164 RID: 356
			[Nullable(0)]
			private readonly struct HeaderValueInfo
			{
				// Token: 0x170001F8 RID: 504
				// (get) Token: 0x0600090F RID: 2319 RVA: 0x00022A9A File Offset: 0x00020C9A
				public string HeaderValue { get; }

				// Token: 0x170001F9 RID: 505
				// (get) Token: 0x06000910 RID: 2320 RVA: 0x00022AA2 File Offset: 0x00020CA2
				public DateTimeOffset ExpiresOn { get; }

				// Token: 0x170001FA RID: 506
				// (get) Token: 0x06000911 RID: 2321 RVA: 0x00022AAA File Offset: 0x00020CAA
				public DateTimeOffset RefreshOn { get; }

				// Token: 0x06000912 RID: 2322 RVA: 0x00022AB2 File Offset: 0x00020CB2
				public HeaderValueInfo(string headerValue, DateTimeOffset expiresOn, DateTimeOffset refreshOn)
				{
					this.HeaderValue = headerValue;
					this.ExpiresOn = expiresOn;
					this.RefreshOn = refreshOn;
				}
			}

			// Token: 0x02000165 RID: 357
			[Nullable(0)]
			private class TokenRequestState
			{
				// Token: 0x170001FB RID: 507
				// (get) Token: 0x06000913 RID: 2323 RVA: 0x00022AC9 File Offset: 0x00020CC9
				public TokenRequestContext CurrentContext { get; }

				// Token: 0x170001FC RID: 508
				// (get) Token: 0x06000914 RID: 2324 RVA: 0x00022AD1 File Offset: 0x00020CD1
				public TaskCompletionSource<BearerTokenAuthenticationPolicy.AccessTokenCache.HeaderValueInfo> InfoTcs { get; }

				// Token: 0x170001FD RID: 509
				// (get) Token: 0x06000915 RID: 2325 RVA: 0x00022AD9 File Offset: 0x00020CD9
				[Nullable(2)]
				public TaskCompletionSource<BearerTokenAuthenticationPolicy.AccessTokenCache.HeaderValueInfo> BackgroundUpdateTcs
				{
					[NullableContext(2)]
					get;
				}

				// Token: 0x06000916 RID: 2326 RVA: 0x00022AE1 File Offset: 0x00020CE1
				public TokenRequestState(TokenRequestContext currentContext, TaskCompletionSource<BearerTokenAuthenticationPolicy.AccessTokenCache.HeaderValueInfo> infoTcs, [Nullable(2)] TaskCompletionSource<BearerTokenAuthenticationPolicy.AccessTokenCache.HeaderValueInfo> backgroundUpdateTcs)
				{
					this.CurrentContext = currentContext;
					this.InfoTcs = infoTcs;
					this.BackgroundUpdateTcs = backgroundUpdateTcs;
				}

				// Token: 0x06000917 RID: 2327 RVA: 0x00022B00 File Offset: 0x00020D00
				public bool RequestRequiresNewToken(TokenRequestContext context)
				{
					return (context.Scopes != null && !MemoryExtensions.SequenceEqual<string>(MemoryExtensions.AsSpan<string>(context.Scopes), MemoryExtensions.AsSpan<string>(this.CurrentContext.Scopes))) || (context.Claims != null && !string.Equals(context.Claims, this.CurrentContext.Claims)) || (context.TenantId != null && !string.Equals(context.TenantId, this.CurrentContext.TenantId));
				}

				// Token: 0x06000918 RID: 2328 RVA: 0x00022B90 File Offset: 0x00020D90
				public bool BackgroundTokenAcquiredSuccessfully(DateTimeOffset now)
				{
					return this.BackgroundUpdateTcs != null && this.BackgroundUpdateTcs.Task.Status == TaskStatus.RanToCompletion && this.BackgroundUpdateTcs.Task.Result.ExpiresOn > now;
				}

				// Token: 0x06000919 RID: 2329 RVA: 0x00022BD8 File Offset: 0x00020DD8
				public bool AccessTokenFailedOrExpired(DateTimeOffset now)
				{
					return this.InfoTcs.Task.Status != TaskStatus.RanToCompletion || now >= this.InfoTcs.Task.Result.ExpiresOn;
				}

				// Token: 0x0600091A RID: 2330 RVA: 0x00022C18 File Offset: 0x00020E18
				public bool TokenNeedsBackgroundRefresh(DateTimeOffset now)
				{
					return now >= this.InfoTcs.Task.Result.RefreshOn && this.BackgroundUpdateTcs == null;
				}
			}
		}
	}
}
