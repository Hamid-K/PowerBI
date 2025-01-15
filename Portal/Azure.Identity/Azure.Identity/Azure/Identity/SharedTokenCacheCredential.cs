using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Core.Pipeline;
using Microsoft.Identity.Client;

namespace Azure.Identity
{
	// Token: 0x0200004E RID: 78
	public class SharedTokenCacheCredential : TokenCredential
	{
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x000088F2 File Offset: 0x00006AF2
		internal string TenantId { get; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x000088FA File Offset: 0x00006AFA
		internal string Username { get; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x00008902 File Offset: 0x00006B02
		internal MsalPublicClient Client { get; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002AA RID: 682 RVA: 0x0000890A File Offset: 0x00006B0A
		internal TenantIdResolverBase TenantIdResolver { get; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002AB RID: 683 RVA: 0x00008912 File Offset: 0x00006B12
		internal bool UseOperatingSystemAccount { get; }

		// Token: 0x060002AC RID: 684 RVA: 0x0000891A File Offset: 0x00006B1A
		public SharedTokenCacheCredential()
			: this(null, null, null, null, null)
		{
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00008927 File Offset: 0x00006B27
		public SharedTokenCacheCredential(SharedTokenCacheCredentialOptions options)
			: this((options != null) ? options.TenantId : null, (options != null) ? options.Username : null, options, null, null)
		{
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000894A File Offset: 0x00006B4A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public SharedTokenCacheCredential(string username, TokenCredentialOptions options = null)
			: this(null, username, options, null, null)
		{
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00008957 File Offset: 0x00006B57
		internal SharedTokenCacheCredential(string tenantId, string username, TokenCredentialOptions options, CredentialPipeline pipeline)
			: this(tenantId, username, options, pipeline, null)
		{
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00008968 File Offset: 0x00006B68
		internal SharedTokenCacheCredential(string tenantId, string username, TokenCredentialOptions options, CredentialPipeline pipeline, MsalPublicClient client)
		{
			this.TenantId = tenantId;
			this.Username = username;
			SharedTokenCacheCredentialOptions sharedTokenCacheCredentialOptions = options as SharedTokenCacheCredentialOptions;
			this._skipTenantValidation = sharedTokenCacheCredentialOptions != null && sharedTokenCacheCredentialOptions.EnableGuestTenantAuthentication;
			this._record = ((sharedTokenCacheCredentialOptions != null) ? sharedTokenCacheCredentialOptions.AuthenticationRecord : null);
			this._pipeline = pipeline ?? CredentialPipeline.GetInstance(options, false);
			this.Client = client ?? new MsalPublicClient(this._pipeline, tenantId, ((sharedTokenCacheCredentialOptions != null) ? sharedTokenCacheCredentialOptions.ClientId : null) ?? "04b07795-8ddb-461a-bbee-02f9e1bf7b46", null, options ?? SharedTokenCacheCredential.s_DefaultCacheOptions);
			this._accountAsyncLock = new AsyncLockWithValue<IAccount>();
			this.TenantIdResolver = ((options != null) ? options.TenantIdResolver : null) ?? TenantIdResolverBase.Default;
			IMsalPublicClientInitializerOptions msalPublicClientInitializerOptions = options as IMsalPublicClientInitializerOptions;
			this.UseOperatingSystemAccount = msalPublicClientInitializerOptions != null && msalPublicClientInitializerOptions.UseDefaultBrokerAccount;
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00008A3D File Offset: 0x00006C3D
		public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.GetTokenImplAsync(false, requestContext, cancellationToken).EnsureCompleted<AccessToken>();
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00008A50 File Offset: 0x00006C50
		public override async ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await this.GetTokenImplAsync(true, requestContext, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00008AA4 File Offset: 0x00006CA4
		private ValueTask<AccessToken> GetTokenImplAsync(bool async, TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			SharedTokenCacheCredential.<GetTokenImplAsync>d__31 <GetTokenImplAsync>d__;
			<GetTokenImplAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder<AccessToken>.Create();
			<GetTokenImplAsync>d__.<>4__this = this;
			<GetTokenImplAsync>d__.async = async;
			<GetTokenImplAsync>d__.requestContext = requestContext;
			<GetTokenImplAsync>d__.cancellationToken = cancellationToken;
			<GetTokenImplAsync>d__.<>1__state = -1;
			<GetTokenImplAsync>d__.<>t__builder.Start<SharedTokenCacheCredential.<GetTokenImplAsync>d__31>(ref <GetTokenImplAsync>d__);
			return <GetTokenImplAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00008B00 File Offset: 0x00006D00
		private ValueTask<IAccount> GetAccountAsync(string tenantId, bool enableCae, bool async, CancellationToken cancellationToken)
		{
			SharedTokenCacheCredential.<GetAccountAsync>d__32 <GetAccountAsync>d__;
			<GetAccountAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder<IAccount>.Create();
			<GetAccountAsync>d__.<>4__this = this;
			<GetAccountAsync>d__.tenantId = tenantId;
			<GetAccountAsync>d__.enableCae = enableCae;
			<GetAccountAsync>d__.async = async;
			<GetAccountAsync>d__.cancellationToken = cancellationToken;
			<GetAccountAsync>d__.<>1__state = -1;
			<GetAccountAsync>d__.<>t__builder.Start<SharedTokenCacheCredential.<GetAccountAsync>d__32>(ref <GetAccountAsync>d__);
			return <GetAccountAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00008B64 File Offset: 0x00006D64
		private string GetCredentialUnavailableMessage(List<IAccount> filteredAccounts)
		{
			if (string.IsNullOrEmpty(this.Username) && string.IsNullOrEmpty(this.TenantId))
			{
				return string.Format(CultureInfo.InvariantCulture, "SharedTokenCacheCredential authentication unavailable. Multiple accounts were found in the cache. Use username and tenant id to disambiguate.", Array.Empty<object>());
			}
			string text = (string.IsNullOrEmpty(this.Username) ? string.Empty : (" username: " + this.Username));
			string text2 = (string.IsNullOrEmpty(this.TenantId) ? string.Empty : (" tenantId: " + this.TenantId));
			if (filteredAccounts.Count == 0)
			{
				return string.Format(CultureInfo.InvariantCulture, "SharedTokenCacheCredential authentication unavailable. No account matching the specified{0}{1} was found in the cache.", text, text2);
			}
			return string.Format(CultureInfo.InvariantCulture, "SharedTokenCacheCredential authentication unavailable. Multiple accounts matching the specified{0}{1} were found in the cache.", text, text2);
		}

		// Token: 0x040001A8 RID: 424
		internal const string NoAccountsInCacheMessage = "SharedTokenCacheCredential authentication unavailable. No accounts were found in the cache.";

		// Token: 0x040001A9 RID: 425
		internal const string MultipleAccountsInCacheMessage = "SharedTokenCacheCredential authentication unavailable. Multiple accounts were found in the cache. Use username and tenant id to disambiguate.";

		// Token: 0x040001AA RID: 426
		internal const string NoMatchingAccountsInCacheMessage = "SharedTokenCacheCredential authentication unavailable. No account matching the specified{0}{1} was found in the cache.";

		// Token: 0x040001AB RID: 427
		internal const string MultipleMatchingAccountsInCacheMessage = "SharedTokenCacheCredential authentication unavailable. Multiple accounts matching the specified{0}{1} were found in the cache.";

		// Token: 0x040001AC RID: 428
		private static readonly SharedTokenCacheCredentialOptions s_DefaultCacheOptions = new SharedTokenCacheCredentialOptions();

		// Token: 0x040001AD RID: 429
		private readonly CredentialPipeline _pipeline;

		// Token: 0x040001AE RID: 430
		private readonly bool _skipTenantValidation;

		// Token: 0x040001AF RID: 431
		private readonly AuthenticationRecord _record;

		// Token: 0x040001B0 RID: 432
		private readonly AsyncLockWithValue<IAccount> _accountAsyncLock;
	}
}
