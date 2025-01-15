using System;
using System.Buffers;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;

namespace Azure.Identity
{
	// Token: 0x02000088 RID: 136
	internal class TokenExchangeManagedIdentitySource : ManagedIdentitySource
	{
		// Token: 0x0600047A RID: 1146 RVA: 0x0000DBFF File Offset: 0x0000BDFF
		private TokenExchangeManagedIdentitySource(CredentialPipeline pipeline, string tenantId, string clientId, string tokenFilePath)
			: base(pipeline)
		{
			this._tokenFileCache = new TokenExchangeManagedIdentitySource.TokenFileCache(tokenFilePath);
			this._clientAssertionCredential = new ClientAssertionCredential(tenantId, clientId, new Func<CancellationToken, Task<string>>(this._tokenFileCache.GetTokenFileContentsAsync), new ClientAssertionCredentialOptions
			{
				Pipeline = pipeline
			});
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0000DC40 File Offset: 0x0000BE40
		public static ManagedIdentitySource TryCreate(ManagedIdentityClientOptions options)
		{
			string azureFederatedTokenFile = EnvironmentVariables.AzureFederatedTokenFile;
			string tenantId = EnvironmentVariables.TenantId;
			string text = options.ClientId ?? EnvironmentVariables.ClientId;
			if (options.ExcludeTokenExchangeManagedIdentitySource || string.IsNullOrEmpty(azureFederatedTokenFile) || string.IsNullOrEmpty(tenantId) || string.IsNullOrEmpty(text))
			{
				return null;
			}
			return new TokenExchangeManagedIdentitySource(options.Pipeline, tenantId, text, azureFederatedTokenFile);
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0000DC9C File Offset: 0x0000BE9C
		public override async ValueTask<AccessToken> AuthenticateAsync(bool async, TokenRequestContext context, CancellationToken cancellationToken)
		{
			AccessToken accessToken;
			if (async)
			{
				accessToken = await this._clientAssertionCredential.GetTokenAsync(context, cancellationToken).ConfigureAwait(false);
			}
			else
			{
				accessToken = this._clientAssertionCredential.GetToken(context, cancellationToken);
			}
			return accessToken;
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0000DCF7 File Offset: 0x0000BEF7
		protected override Request CreateRequest(string[] scopes)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0000DCFE File Offset: 0x0000BEFE
		internal static Task<string> ReadAllTextAsync(string path, CancellationToken cancellationToken = default(CancellationToken))
		{
			return TokenExchangeManagedIdentitySource.ReadAllTextAsync(path, Encoding.UTF8, cancellationToken);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0000DD0C File Offset: 0x0000BF0C
		internal static Task<string> ReadAllTextAsync(string path, Encoding encoding, CancellationToken cancellationToken = default(CancellationToken))
		{
			Argument.AssertNotNullOrEmpty(path, "path");
			Argument.AssertNotNull<Encoding>(encoding, "encoding");
			if (!cancellationToken.IsCancellationRequested)
			{
				return TokenExchangeManagedIdentitySource.InternalReadAllTextAsync(path, encoding, cancellationToken);
			}
			return Task.FromCanceled<string>(cancellationToken);
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0000DD3C File Offset: 0x0000BF3C
		private static async Task<string> InternalReadAllTextAsync(string path, Encoding encoding, CancellationToken cancellationToken)
		{
			char[] buffer = null;
			StreamReader sr = TokenExchangeManagedIdentitySource.AsyncStreamReader(path, encoding);
			string text;
			try
			{
				cancellationToken.ThrowIfCancellationRequested();
				buffer = ArrayPool<char>.Shared.Rent(sr.CurrentEncoding.GetMaxCharCount(TokenExchangeManagedIdentitySource.DefaultBufferSize));
				StringBuilder sb = new StringBuilder();
				int totalRead = 0;
				for (;;)
				{
					int num = await sr.ReadAsync(buffer, totalRead, TokenExchangeManagedIdentitySource.DefaultBufferSize - totalRead).ConfigureAwait(false);
					if (num == 0)
					{
						break;
					}
					sb.Append(buffer, 0, num);
					totalRead += num;
				}
				text = sb.ToString();
			}
			finally
			{
				sr.Dispose();
				if (buffer != null)
				{
					ArrayPool<char>.Shared.Return(buffer, false);
				}
			}
			return text;
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0000DD8F File Offset: 0x0000BF8F
		private static StreamReader AsyncStreamReader(string path, Encoding encoding)
		{
			return new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, TokenExchangeManagedIdentitySource.DefaultBufferSize, FileOptions.Asynchronous | FileOptions.SequentialScan), encoding, true);
		}

		// Token: 0x0400028A RID: 650
		private TokenExchangeManagedIdentitySource.TokenFileCache _tokenFileCache;

		// Token: 0x0400028B RID: 651
		private ClientAssertionCredential _clientAssertionCredential;

		// Token: 0x0400028C RID: 652
		private static readonly int DefaultBufferSize = 4096;

		// Token: 0x02000130 RID: 304
		private class TokenFileCache
		{
			// Token: 0x0600062E RID: 1582 RVA: 0x0001BE22 File Offset: 0x0001A022
			public TokenFileCache(string tokenFilePath)
			{
				this._tokenFilePath = tokenFilePath;
			}

			// Token: 0x0600062F RID: 1583 RVA: 0x0001BE3C File Offset: 0x0001A03C
			public async Task<string> GetTokenFileContentsAsync(CancellationToken cancellationToken)
			{
				if (this._refreshOn <= DateTimeOffset.UtcNow)
				{
					try
					{
						await TokenExchangeManagedIdentitySource.TokenFileCache.semaphore.WaitAsync(cancellationToken).ConfigureAwait(false);
						if (this._refreshOn <= DateTimeOffset.UtcNow)
						{
							this._tokenFileContents = await TokenExchangeManagedIdentitySource.ReadAllTextAsync(this._tokenFilePath, default(CancellationToken)).ConfigureAwait(false);
							this._refreshOn = DateTimeOffset.UtcNow.AddMinutes(5.0);
						}
					}
					finally
					{
						TokenExchangeManagedIdentitySource.TokenFileCache.semaphore.Release();
					}
				}
				return this._tokenFileContents;
			}

			// Token: 0x04000698 RID: 1688
			private static SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

			// Token: 0x04000699 RID: 1689
			private readonly string _tokenFilePath;

			// Token: 0x0400069A RID: 1690
			private string _tokenFileContents;

			// Token: 0x0400069B RID: 1691
			private DateTimeOffset _refreshOn = DateTimeOffset.MinValue;
		}
	}
}
