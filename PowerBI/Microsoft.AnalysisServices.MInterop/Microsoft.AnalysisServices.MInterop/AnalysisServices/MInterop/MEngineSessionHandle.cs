using System;
using System.Collections.Concurrent;
using System.Linq;
using Microsoft.AnalysisServices.PlatformHost;
using Microsoft.Data.Mashup.Preview;
using Microsoft.Mashup.OAuth;

namespace Microsoft.AnalysisServices.MInterop
{
	// Token: 0x02000021 RID: 33
	internal sealed class MEngineSessionHandle : IMEngineSessionHandle
	{
		// Token: 0x06000081 RID: 129 RVA: 0x0000417A File Offset: 0x0000237A
		internal MEngineSessionHandle(string sessionId)
		{
			this.impl = new MEngineSessionHandleImpl(sessionId);
			this.refreshedTokens = new ConcurrentDictionary<string, TokenCredential>();
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004199 File Offset: 0x00002399
		public MashupSessionHandle GetHandle()
		{
			return this.impl.GetHandle();
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000041A6 File Offset: 0x000023A6
		public void Close()
		{
			if (this.impl != null)
			{
				this.impl.Dispose();
				this.impl = null;
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000041C4 File Offset: 0x000023C4
		public string GetUpdatedDSRCredential(string resourcePath, string dataSourceCredential)
		{
			TokenCredential refreshedTokenCredentialsForDataSource = this.GetRefreshedTokenCredentialsForDataSource(resourcePath);
			if (refreshedTokenCredentialsForDataSource == null)
			{
				return dataSourceCredential;
			}
			Credential credential = Credential.FromJson(dataSourceCredential, "");
			credential.AuthenticationProperties["AccessToken"] = refreshedTokenCredentialsForDataSource.AccessToken;
			credential.AuthenticationProperties["Expires"] = refreshedTokenCredentialsForDataSource.Expires;
			credential.AuthenticationProperties["RefreshToken"] = refreshedTokenCredentialsForDataSource.RefreshToken;
			IEngineTracer engineTracer = MInteropHelperImpl.EngineTracer;
			if (engineTracer != null)
			{
				engineTracer.LogPrivateMessage(string.Format("Fetched updated credentials for data source {0}, refresh token available: {1}, expiry: {2}.", resourcePath.MarkAsCustomerContent(), !string.IsNullOrEmpty(refreshedTokenCredentialsForDataSource.RefreshToken), refreshedTokenCredentialsForDataSource.Expires));
			}
			return credential.ToJson();
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000426C File Offset: 0x0000246C
		internal TokenCredential GetRefreshedTokenCredentialsForDataSource(string dataSourceKey)
		{
			if (this.refreshedTokens.Keys.Any((string k) => k == dataSourceKey))
			{
				return this.refreshedTokens[dataSourceKey];
			}
			return null;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000042B7 File Offset: 0x000024B7
		internal void UpdateRefreshToken(string dataSourceKey, TokenCredential token)
		{
			this.refreshedTokens[dataSourceKey] = token;
		}

		// Token: 0x040000B5 RID: 181
		private MEngineSessionHandleImpl impl;

		// Token: 0x040000B6 RID: 182
		private ConcurrentDictionary<string, TokenCredential> refreshedTokens;
	}
}
