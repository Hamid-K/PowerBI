using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.Core
{
	// Token: 0x02000042 RID: 66
	[NullableContext(1)]
	[Nullable(0)]
	public static class DelegatedTokenCredential
	{
		// Token: 0x060001DB RID: 475 RVA: 0x00005F1E File Offset: 0x0000411E
		public static TokenCredential Create(Func<TokenRequestContext, CancellationToken, AccessToken> getToken, [Nullable(new byte[] { 1, 0 })] Func<TokenRequestContext, CancellationToken, ValueTask<AccessToken>> getTokenAsync)
		{
			return new DelegatedTokenCredential.StaticTokenCredential(getToken, getTokenAsync);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00005F27 File Offset: 0x00004127
		public static TokenCredential Create(Func<TokenRequestContext, CancellationToken, AccessToken> getToken)
		{
			return new DelegatedTokenCredential.StaticTokenCredential(getToken);
		}

		// Token: 0x020000E0 RID: 224
		[Nullable(0)]
		private class StaticTokenCredential : TokenCredential
		{
			// Token: 0x0600070E RID: 1806 RVA: 0x0001822F File Offset: 0x0001642F
			internal StaticTokenCredential(Func<TokenRequestContext, CancellationToken, AccessToken> getToken, [Nullable(new byte[] { 1, 0 })] Func<TokenRequestContext, CancellationToken, ValueTask<AccessToken>> getTokenAsync)
			{
				this._getToken = getToken;
				this._getTokenAsync = getTokenAsync;
			}

			// Token: 0x0600070F RID: 1807 RVA: 0x00018245 File Offset: 0x00016445
			internal StaticTokenCredential(Func<TokenRequestContext, CancellationToken, AccessToken> getToken)
			{
				this._getToken = getToken;
				this._getTokenAsync = (TokenRequestContext context, CancellationToken token) => new ValueTask<AccessToken>(this._getToken(context, token));
			}

			// Token: 0x06000710 RID: 1808 RVA: 0x00018266 File Offset: 0x00016466
			[NullableContext(0)]
			public override ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken)
			{
				return this._getTokenAsync(requestContext, cancellationToken);
			}

			// Token: 0x06000711 RID: 1809 RVA: 0x00018275 File Offset: 0x00016475
			public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken)
			{
				return this._getToken(requestContext, cancellationToken);
			}

			// Token: 0x040002FF RID: 767
			private readonly Func<TokenRequestContext, CancellationToken, AccessToken> _getToken;

			// Token: 0x04000300 RID: 768
			[Nullable(new byte[] { 1, 0 })]
			private readonly Func<TokenRequestContext, CancellationToken, ValueTask<AccessToken>> _getTokenAsync;
		}
	}
}
