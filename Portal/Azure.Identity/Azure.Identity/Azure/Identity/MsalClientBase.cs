using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;
using Microsoft.Identity.Client;

namespace Azure.Identity
{
	// Token: 0x02000075 RID: 117
	internal abstract class MsalClientBase<TClient> where TClient : IClientApplicationBase
	{
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x0000BAAB File Offset: 0x00009CAB
		protected internal bool IsSupportLoggingEnabled { get; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0000BAB3 File Offset: 0x00009CB3
		protected internal bool DisableInstanceDiscovery { get; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x0000BABB File Offset: 0x00009CBB
		protected internal CredentialPipeline Pipeline { get; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x0000BAC3 File Offset: 0x00009CC3
		internal string TenantId { get; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x0000BACB File Offset: 0x00009CCB
		internal string ClientId { get; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x0000BAD3 File Offset: 0x00009CD3
		internal Uri AuthorityHost { get; }

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000BADB File Offset: 0x00009CDB
		protected MsalClientBase()
		{
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000BAF8 File Offset: 0x00009CF8
		protected MsalClientBase(CredentialPipeline pipeline, string tenantId, string clientId, TokenCredentialOptions options)
		{
			Validations.ValidateAuthorityHost((options != null) ? options.AuthorityHost : null);
			this.AuthorityHost = ((options != null) ? options.AuthorityHost : null) ?? AzureAuthorityHosts.GetDefault();
			bool? flag;
			if (options == null)
			{
				flag = null;
			}
			else
			{
				TokenCredentialDiagnosticsOptions diagnostics = options.Diagnostics;
				flag = ((diagnostics != null) ? new bool?(diagnostics.IsAccountIdentifierLoggingEnabled) : null);
			}
			bool? flag2 = flag;
			this._logAccountDetails = flag2.GetValueOrDefault();
			ISupportsDisableInstanceDiscovery supportsDisableInstanceDiscovery = options as ISupportsDisableInstanceDiscovery;
			this.DisableInstanceDiscovery = supportsDisableInstanceDiscovery != null && supportsDisableInstanceDiscovery.DisableInstanceDiscovery;
			ISupportsTokenCachePersistenceOptions supportsTokenCachePersistenceOptions = options as ISupportsTokenCachePersistenceOptions;
			this._tokenCachePersistenceOptions = ((supportsTokenCachePersistenceOptions != null) ? supportsTokenCachePersistenceOptions.TokenCachePersistenceOptions : null);
			this.IsSupportLoggingEnabled = options != null && options.IsUnsafeSupportLoggingEnabled;
			this.Pipeline = pipeline;
			this.TenantId = tenantId;
			this.ClientId = clientId;
			this._clientAsyncLock = new AsyncLockWithValue<ValueTuple<TClient, TokenCache>>();
			this._clientWithCaeAsyncLock = new AsyncLockWithValue<ValueTuple<TClient, TokenCache>>();
		}

		// Token: 0x060003F6 RID: 1014
		protected abstract ValueTask<TClient> CreateClientAsync(bool enableCae, bool async, CancellationToken cancellationToken);

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000BC00 File Offset: 0x00009E00
		protected ValueTask<TClient> GetClientAsync(bool enableCae, bool async, CancellationToken cancellationToken)
		{
			MsalClientBase<TClient>.<GetClientAsync>d__26 <GetClientAsync>d__;
			<GetClientAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder<TClient>.Create();
			<GetClientAsync>d__.<>4__this = this;
			<GetClientAsync>d__.enableCae = enableCae;
			<GetClientAsync>d__.async = async;
			<GetClientAsync>d__.cancellationToken = cancellationToken;
			<GetClientAsync>d__.<>1__state = -1;
			<GetClientAsync>d__.<>t__builder.Start<MsalClientBase<TClient>.<GetClientAsync>d__26>(ref <GetClientAsync>d__);
			return <GetClientAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000BC5B File Offset: 0x00009E5B
		protected void LogMsal(LogLevel level, string message, bool isPii)
		{
			if (!isPii || this.IsSupportLoggingEnabled)
			{
				AzureIdentityEventSource.Singleton.LogMsal(level, message);
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000BC74 File Offset: 0x00009E74
		protected void LogAccountDetails(AuthenticationResult result)
		{
			if (this._logAccountDetails)
			{
				ValueTuple<string, string, string, string> valueTuple = TokenHelper.ParseAccountInfoFromToken(result.AccessToken);
				AzureIdentityEventSource singleton = AzureIdentityEventSource.Singleton;
				string item = valueTuple.Item1;
				string text = valueTuple.Item2 ?? result.TenantId;
				string text2;
				if ((text2 = valueTuple.Item3) == null)
				{
					IAccount account = result.Account;
					text2 = ((account != null) ? account.Username : null);
				}
				singleton.AuthenticatedAccountDetails(item, text, text2, valueTuple.Item4 ?? result.UniqueId);
			}
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000BCE4 File Offset: 0x00009EE4
		internal async ValueTask<TokenCache> GetTokenCache(bool enableCae)
		{
			AsyncLockWithValue<ValueTuple<TClient, TokenCache>>.LockOrValue lockOrValue;
			if (enableCae)
			{
				lockOrValue = await this._clientWithCaeAsyncLock.GetLockOrValueAsync(true, default(CancellationToken)).ConfigureAwait(false);
			}
			else
			{
				lockOrValue = await this._clientAsyncLock.GetLockOrValueAsync(true, default(CancellationToken)).ConfigureAwait(false);
			}
			TokenCache tokenCache;
			using (AsyncLockWithValue<ValueTuple<TClient, TokenCache>>.LockOrValue lockOrValue2 = lockOrValue)
			{
				tokenCache = (lockOrValue2.HasValue ? lockOrValue2.Value.Item2 : null);
			}
			return tokenCache;
		}

		// Token: 0x04000244 RID: 580
		[TupleElementNames(new string[] { "Client", "Cache" })]
		private readonly AsyncLockWithValue<ValueTuple<TClient, TokenCache>> _clientAsyncLock;

		// Token: 0x04000245 RID: 581
		[TupleElementNames(new string[] { "Client", "Cache" })]
		private readonly AsyncLockWithValue<ValueTuple<TClient, TokenCache>> _clientWithCaeAsyncLock;

		// Token: 0x04000246 RID: 582
		private readonly bool _logAccountDetails;

		// Token: 0x04000247 RID: 583
		private readonly TokenCachePersistenceOptions _tokenCachePersistenceOptions;

		// Token: 0x0400024A RID: 586
		protected string[] cp1Capabilities = new string[] { "CP1" };
	}
}
