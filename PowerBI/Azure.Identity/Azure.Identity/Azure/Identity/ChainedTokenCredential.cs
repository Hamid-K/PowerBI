using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Core.Pipeline;

namespace Azure.Identity
{
	// Token: 0x02000039 RID: 57
	public class ChainedTokenCredential : TokenCredential
	{
		// Token: 0x06000183 RID: 387 RVA: 0x00006412 File Offset: 0x00004612
		internal ChainedTokenCredential()
		{
			this._sources = Array.Empty<TokenCredential>();
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00006428 File Offset: 0x00004628
		public ChainedTokenCredential(params TokenCredential[] sources)
		{
			if (sources == null)
			{
				throw new ArgumentNullException("sources");
			}
			if (sources.Length == 0)
			{
				throw new ArgumentException("sources must not be empty", "sources");
			}
			for (int i = 0; i < sources.Length; i++)
			{
				if (sources[i] == null)
				{
					throw new ArgumentException("sources must not contain null", "sources");
				}
			}
			this._sources = sources;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00006487 File Offset: 0x00004687
		public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.GetTokenImplAsync(false, requestContext, cancellationToken).EnsureCompleted<AccessToken>();
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00006498 File Offset: 0x00004698
		public override async ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await this.GetTokenImplAsync(true, requestContext, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000064EC File Offset: 0x000046EC
		private async ValueTask<AccessToken> GetTokenImplAsync(bool async, TokenRequestContext requestContext, CancellationToken cancellationToken)
		{
			ScopeGroupHandler groupScopeHandler = new ScopeGroupHandler(null);
			try
			{
				List<CredentialUnavailableException> exceptions = new List<CredentialUnavailableException>();
				foreach (TokenCredential tokenCredential in this._sources)
				{
					try
					{
						AccessToken accessToken;
						if (async)
						{
							accessToken = await tokenCredential.GetTokenAsync(requestContext, cancellationToken).ConfigureAwait(false);
						}
						else
						{
							accessToken = tokenCredential.GetToken(requestContext, cancellationToken);
						}
						ValueTask<AccessToken> valueTask = accessToken;
						ScopeGroupHandler scopeGroupHandler = groupScopeHandler;
						string text = null;
						DiagnosticScope diagnosticScope = default(DiagnosticScope);
						scopeGroupHandler.Dispose(text, in diagnosticScope);
						return valueTask;
					}
					catch (CredentialUnavailableException ex)
					{
						exceptions.Add(ex);
					}
					catch (Exception ex2) when (!cancellationToken.IsCancellationRequested)
					{
						throw new AuthenticationFailedException("The ChainedTokenCredential failed due to an unhandled exception: " + ex2.Message, ex2);
					}
				}
				TokenCredential[] array = null;
				throw CredentialUnavailableException.CreateAggregateException("The ChainedTokenCredential failed to retrieve a token from the included credentials.", exceptions);
			}
			catch (Exception ex3)
			{
				ScopeGroupHandler scopeGroupHandler2 = groupScopeHandler;
				string text2 = null;
				DiagnosticScope diagnosticScope = default(DiagnosticScope);
				scopeGroupHandler2.Fail(text2, in diagnosticScope, ex3);
				throw;
			}
			AccessToken accessToken2;
			return accessToken2;
		}

		// Token: 0x0400011A RID: 282
		private const string AggregateAllUnavailableErrorMessage = "The ChainedTokenCredential failed to retrieve a token from the included credentials.";

		// Token: 0x0400011B RID: 283
		private const string AuthenticationFailedErrorMessage = "The ChainedTokenCredential failed due to an unhandled exception: ";

		// Token: 0x0400011C RID: 284
		private readonly TokenCredential[] _sources;
	}
}
