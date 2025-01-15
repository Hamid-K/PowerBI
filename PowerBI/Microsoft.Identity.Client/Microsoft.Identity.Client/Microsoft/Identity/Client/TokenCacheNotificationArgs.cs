using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using Microsoft.Identity.Client.TelemetryCore.TelemetryClient;
using Microsoft.IdentityModel.Abstractions;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000160 RID: 352
	public sealed class TokenCacheNotificationArgs
	{
		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06001139 RID: 4409 RVA: 0x0003B94F File Offset: 0x00039B4F
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[Obsolete("Use Account instead (See https://aka.ms/msal-net-2-released)", true)]
		public IUser User
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x0003B958 File Offset: 0x00039B58
		public TokenCacheNotificationArgs(ITokenCacheSerializer tokenCache, string clientId, IAccount account, bool hasStateChanged, bool isApplicationCache, string suggestedCacheKey, bool hasTokens, DateTimeOffset? suggestedCacheExpiry, CancellationToken cancellationToken)
			: this(tokenCache, clientId, account, hasStateChanged, isApplicationCache, suggestedCacheKey, hasTokens, suggestedCacheExpiry, cancellationToken, default(Guid), null, null, null, false, null)
		{
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x0003B988 File Offset: 0x00039B88
		public TokenCacheNotificationArgs(ITokenCacheSerializer tokenCache, string clientId, IAccount account, bool hasStateChanged, bool isApplicationCache, string suggestedCacheKey, bool hasTokens, DateTimeOffset? suggestedCacheExpiry, CancellationToken cancellationToken, Guid correlationId)
			: this(tokenCache, clientId, account, hasStateChanged, isApplicationCache, suggestedCacheKey, hasTokens, suggestedCacheExpiry, cancellationToken, correlationId, null, null, null, false, null)
		{
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x0003B9B4 File Offset: 0x00039BB4
		public TokenCacheNotificationArgs(ITokenCacheSerializer tokenCache, string clientId, IAccount account, bool hasStateChanged, bool isApplicationCache, string suggestedCacheKey, bool hasTokens, DateTimeOffset? suggestedCacheExpiry, CancellationToken cancellationToken, Guid correlationId, IEnumerable<string> requestScopes, string requestTenantId)
		{
			this.TokenCache = tokenCache;
			this.ClientId = clientId;
			this.Account = account;
			this.HasStateChanged = hasStateChanged;
			this.IsApplicationCache = isApplicationCache;
			this.SuggestedCacheKey = suggestedCacheKey;
			this.HasTokens = hasTokens;
			this.CancellationToken = cancellationToken;
			this.CorrelationId = correlationId;
			this.RequestScopes = requestScopes;
			this.RequestTenantId = requestTenantId;
			this.SuggestedCacheExpiry = suggestedCacheExpiry;
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x0003BA24 File Offset: 0x00039C24
		public TokenCacheNotificationArgs(ITokenCacheSerializer tokenCache, string clientId, IAccount account, bool hasStateChanged, bool isApplicationCache, string suggestedCacheKey, bool hasTokens, DateTimeOffset? suggestedCacheExpiry, CancellationToken cancellationToken, Guid correlationId, IEnumerable<string> requestScopes, string requestTenantId, IIdentityLogger identityLogger, bool piiLoggingEnabled, TelemetryData telemetryData = null)
		{
			this.TokenCache = tokenCache;
			this.ClientId = clientId;
			this.Account = account;
			this.HasStateChanged = hasStateChanged;
			this.IsApplicationCache = isApplicationCache;
			this.SuggestedCacheKey = suggestedCacheKey;
			this.HasTokens = hasTokens;
			this.CancellationToken = cancellationToken;
			this.CorrelationId = correlationId;
			this.RequestScopes = requestScopes;
			this.RequestTenantId = requestTenantId;
			this.SuggestedCacheExpiry = suggestedCacheExpiry;
			this.IdentityLogger = identityLogger;
			this.PiiLoggingEnabled = piiLoggingEnabled;
			this.TelemetryData = telemetryData ?? new TelemetryData();
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x0600113E RID: 4414 RVA: 0x0003BAB5 File Offset: 0x00039CB5
		public ITokenCacheSerializer TokenCache { get; }

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x0600113F RID: 4415 RVA: 0x0003BABD File Offset: 0x00039CBD
		public string ClientId { get; }

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06001140 RID: 4416 RVA: 0x0003BAC5 File Offset: 0x00039CC5
		public IAccount Account { get; }

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06001141 RID: 4417 RVA: 0x0003BACD File Offset: 0x00039CCD
		// (set) Token: 0x06001142 RID: 4418 RVA: 0x0003BAD5 File Offset: 0x00039CD5
		public bool HasStateChanged { get; internal set; }

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06001143 RID: 4419 RVA: 0x0003BADE File Offset: 0x00039CDE
		public bool IsApplicationCache { get; }

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06001144 RID: 4420 RVA: 0x0003BAE6 File Offset: 0x00039CE6
		public string SuggestedCacheKey { get; }

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06001145 RID: 4421 RVA: 0x0003BAEE File Offset: 0x00039CEE
		public bool HasTokens { get; }

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06001146 RID: 4422 RVA: 0x0003BAF6 File Offset: 0x00039CF6
		public CancellationToken CancellationToken { get; }

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06001147 RID: 4423 RVA: 0x0003BAFE File Offset: 0x00039CFE
		public Guid CorrelationId { get; }

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06001148 RID: 4424 RVA: 0x0003BB06 File Offset: 0x00039D06
		public IEnumerable<string> RequestScopes { get; }

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06001149 RID: 4425 RVA: 0x0003BB0E File Offset: 0x00039D0E
		public string RequestTenantId { get; }

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x0600114A RID: 4426 RVA: 0x0003BB16 File Offset: 0x00039D16
		public DateTimeOffset? SuggestedCacheExpiry { get; }

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x0600114B RID: 4427 RVA: 0x0003BB1E File Offset: 0x00039D1E
		public IIdentityLogger IdentityLogger { get; }

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x0600114C RID: 4428 RVA: 0x0003BB26 File Offset: 0x00039D26
		public bool PiiLoggingEnabled { get; }

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x0600114D RID: 4429 RVA: 0x0003BB2E File Offset: 0x00039D2E
		public TelemetryData TelemetryData { get; }
	}
}
