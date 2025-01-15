using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Core.Pipeline;

namespace Azure.Identity
{
	// Token: 0x02000040 RID: 64
	public class DefaultAzureCredential : TokenCredential
	{
		// Token: 0x060001C9 RID: 457 RVA: 0x00006E2E File Offset: 0x0000502E
		internal DefaultAzureCredential()
			: this(false)
		{
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00006E37 File Offset: 0x00005037
		public DefaultAzureCredential(bool includeInteractiveCredentials = false)
		{
			object obj;
			if (!includeInteractiveCredentials)
			{
				obj = null;
			}
			else
			{
				(obj = new DefaultAzureCredentialOptions()).ExcludeInteractiveBrowserCredential = false;
			}
			this..ctor(obj);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00006E51 File Offset: 0x00005051
		public DefaultAzureCredential(DefaultAzureCredentialOptions options)
			: this(new DefaultAzureCredentialFactory(DefaultAzureCredential.ValidateAuthorityHostOption(options)))
		{
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00006E64 File Offset: 0x00005064
		internal DefaultAzureCredential(DefaultAzureCredentialFactory factory)
		{
			this._pipeline = factory.Pipeline;
			this._sources = factory.CreateCredentialChain();
			this._credentialLock = new AsyncLockWithValue<TokenCredential>();
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00006E8F File Offset: 0x0000508F
		public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.GetTokenImplAsync(false, requestContext, cancellationToken).EnsureCompleted<AccessToken>();
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00006EA0 File Offset: 0x000050A0
		public override async ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await this.GetTokenImplAsync(true, requestContext, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00006EF4 File Offset: 0x000050F4
		private ValueTask<AccessToken> GetTokenImplAsync(bool async, TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			DefaultAzureCredential.<GetTokenImplAsync>d__12 <GetTokenImplAsync>d__;
			<GetTokenImplAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder<AccessToken>.Create();
			<GetTokenImplAsync>d__.<>4__this = this;
			<GetTokenImplAsync>d__.async = async;
			<GetTokenImplAsync>d__.requestContext = requestContext;
			<GetTokenImplAsync>d__.cancellationToken = cancellationToken;
			<GetTokenImplAsync>d__.<>1__state = -1;
			<GetTokenImplAsync>d__.<>t__builder.Start<DefaultAzureCredential.<GetTokenImplAsync>d__12>(ref <GetTokenImplAsync>d__);
			return <GetTokenImplAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00006F50 File Offset: 0x00005150
		private static async ValueTask<AccessToken> GetTokenFromCredentialAsync(TokenCredential credential, TokenRequestContext requestContext, bool async, CancellationToken cancellationToken)
		{
			AccessToken accessToken2;
			try
			{
				AccessToken accessToken;
				if (async)
				{
					accessToken = await credential.GetTokenAsync(requestContext, cancellationToken).ConfigureAwait(false);
				}
				else
				{
					accessToken = credential.GetToken(requestContext, cancellationToken);
				}
				accessToken2 = accessToken;
			}
			catch (Exception ex) when (!(ex is CredentialUnavailableException))
			{
				throw new AuthenticationFailedException("DefaultAzureCredential authentication failed due to an unhandled exception: ", ex);
			}
			return accessToken2;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00006FAC File Offset: 0x000051AC
		[return: TupleElementNames(new string[] { "Token", "Credential" })]
		private static async ValueTask<ValueTuple<AccessToken, TokenCredential>> GetTokenFromSourcesAsync(TokenCredential[] sources, TokenRequestContext requestContext, bool async, CancellationToken cancellationToken)
		{
			List<CredentialUnavailableException> exceptions = new List<CredentialUnavailableException>();
			int i = 0;
			while (i < sources.Length && sources[i] != null)
			{
				try
				{
					AccessToken accessToken;
					if (async)
					{
						accessToken = await sources[i].GetTokenAsync(requestContext, cancellationToken).ConfigureAwait(false);
					}
					else
					{
						accessToken = sources[i].GetToken(requestContext, cancellationToken);
					}
					return new ValueTuple<AccessToken, TokenCredential>(accessToken, sources[i]);
				}
				catch (CredentialUnavailableException ex)
				{
					exceptions.Add(ex);
				}
				i++;
			}
			throw CredentialUnavailableException.CreateAggregateException("DefaultAzureCredential failed to retrieve a token from the included credentials. See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/defaultazurecredential/troubleshoot", exceptions);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00007007 File Offset: 0x00005207
		private static DefaultAzureCredentialOptions ValidateAuthorityHostOption(DefaultAzureCredentialOptions options)
		{
			Validations.ValidateAuthorityHost(((options != null) ? options.AuthorityHost : null) ?? AzureAuthorityHosts.GetDefault());
			return options;
		}

		// Token: 0x0400013E RID: 318
		private const string Troubleshooting = "See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/defaultazurecredential/troubleshoot";

		// Token: 0x0400013F RID: 319
		private const string DefaultExceptionMessage = "DefaultAzureCredential failed to retrieve a token from the included credentials. See the troubleshooting guide for more information. https://aka.ms/azsdk/net/identity/defaultazurecredential/troubleshoot";

		// Token: 0x04000140 RID: 320
		private const string UnhandledExceptionMessage = "DefaultAzureCredential authentication failed due to an unhandled exception: ";

		// Token: 0x04000141 RID: 321
		private readonly CredentialPipeline _pipeline;

		// Token: 0x04000142 RID: 322
		private readonly AsyncLockWithValue<TokenCredential> _credentialLock;

		// Token: 0x04000143 RID: 323
		internal TokenCredential[] _sources;
	}
}
