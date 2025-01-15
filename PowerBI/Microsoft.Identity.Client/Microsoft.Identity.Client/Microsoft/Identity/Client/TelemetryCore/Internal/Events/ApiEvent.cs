using System;
using Microsoft.Identity.Client.AuthScheme;
using Microsoft.Identity.Client.Cache;
using Microsoft.Identity.Client.Region;

namespace Microsoft.Identity.Client.TelemetryCore.Internal.Events
{
	// Token: 0x020001E8 RID: 488
	internal class ApiEvent
	{
		// Token: 0x060014B6 RID: 5302 RVA: 0x00045CFD File Offset: 0x00043EFD
		public ApiEvent(Guid correlationId)
		{
			this.CorrelationId = correlationId;
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x060014B7 RID: 5303 RVA: 0x00045D0C File Offset: 0x00043F0C
		// (set) Token: 0x060014B8 RID: 5304 RVA: 0x00045D14 File Offset: 0x00043F14
		public Guid CorrelationId { get; set; }

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x060014B9 RID: 5305 RVA: 0x00045D1D File Offset: 0x00043F1D
		// (set) Token: 0x060014BA RID: 5306 RVA: 0x00045D25 File Offset: 0x00043F25
		public ApiEvent.ApiIds ApiId { get; set; }

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x060014BB RID: 5307 RVA: 0x00045D2E File Offset: 0x00043F2E
		public string ApiIdString
		{
			get
			{
				return this.ApiId.ToString("D");
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x060014BC RID: 5308 RVA: 0x00045D45 File Offset: 0x00043F45
		// (set) Token: 0x060014BD RID: 5309 RVA: 0x00045D4D File Offset: 0x00043F4D
		public string TokenEndpoint { get; set; }

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x060014BE RID: 5310 RVA: 0x00045D56 File Offset: 0x00043F56
		// (set) Token: 0x060014BF RID: 5311 RVA: 0x00045D5E File Offset: 0x00043F5E
		public bool IsAccessTokenCacheHit { get; set; }

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x060014C0 RID: 5312 RVA: 0x00045D67 File Offset: 0x00043F67
		// (set) Token: 0x060014C1 RID: 5313 RVA: 0x00045D6F File Offset: 0x00043F6F
		public string ApiErrorCode { get; set; }

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x060014C2 RID: 5314 RVA: 0x00045D78 File Offset: 0x00043F78
		// (set) Token: 0x060014C3 RID: 5315 RVA: 0x00045D80 File Offset: 0x00043F80
		public string RegionUsed { get; set; }

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x060014C4 RID: 5316 RVA: 0x00045D89 File Offset: 0x00043F89
		// (set) Token: 0x060014C5 RID: 5317 RVA: 0x00045D96 File Offset: 0x00043F96
		public RegionAutodetectionSource RegionAutodetectionSource
		{
			get
			{
				return this._regionAutodetectionSource.GetValueOrDefault();
			}
			set
			{
				this._regionAutodetectionSource = new RegionAutodetectionSource?(value);
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x060014C6 RID: 5318 RVA: 0x00045DA4 File Offset: 0x00043FA4
		public string RegionAutodetectionSourceString
		{
			get
			{
				if (this._regionAutodetectionSource == null)
				{
					return null;
				}
				return this._regionAutodetectionSource.Value.ToString("D");
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x060014C7 RID: 5319 RVA: 0x00045DCF File Offset: 0x00043FCF
		// (set) Token: 0x060014C8 RID: 5320 RVA: 0x00045DDC File Offset: 0x00043FDC
		public RegionOutcome RegionOutcome
		{
			get
			{
				return this._regionOutcome.GetValueOrDefault();
			}
			set
			{
				this._regionOutcome = new RegionOutcome?(value);
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x060014C9 RID: 5321 RVA: 0x00045DEA File Offset: 0x00043FEA
		public string RegionOutcomeString
		{
			get
			{
				if (this._regionOutcome == null)
				{
					return null;
				}
				return this._regionOutcome.Value.ToString("D");
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x060014CA RID: 5322 RVA: 0x00045E15 File Offset: 0x00044015
		// (set) Token: 0x060014CB RID: 5323 RVA: 0x00045E1D File Offset: 0x0004401D
		public string AutoDetectedRegion { get; internal set; }

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x060014CC RID: 5324 RVA: 0x00045E26 File Offset: 0x00044026
		// (set) Token: 0x060014CD RID: 5325 RVA: 0x00045E2E File Offset: 0x0004402E
		public string RegionDiscoveryFailureReason { get; set; }

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x060014CE RID: 5326 RVA: 0x00045E37 File Offset: 0x00044037
		// (set) Token: 0x060014CF RID: 5327 RVA: 0x00045E3F File Offset: 0x0004403F
		public bool IsTokenCacheSerialized { get; set; }

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x060014D0 RID: 5328 RVA: 0x00045E48 File Offset: 0x00044048
		public char IsTokenCacheSerializedString
		{
			get
			{
				if (!this.IsTokenCacheSerialized)
				{
					return '0';
				}
				return '1';
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x060014D1 RID: 5329 RVA: 0x00045E57 File Offset: 0x00044057
		// (set) Token: 0x060014D2 RID: 5330 RVA: 0x00045E5F File Offset: 0x0004405F
		public bool IsLegacyCacheEnabled { get; set; }

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x060014D3 RID: 5331 RVA: 0x00045E68 File Offset: 0x00044068
		public char IsLegacyCacheEnabledString
		{
			get
			{
				if (!this.IsLegacyCacheEnabled)
				{
					return '0';
				}
				return '1';
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x060014D4 RID: 5332 RVA: 0x00045E77 File Offset: 0x00044077
		// (set) Token: 0x060014D5 RID: 5333 RVA: 0x00045E84 File Offset: 0x00044084
		public CacheRefreshReason CacheInfo
		{
			get
			{
				return this._cacheInfo.GetValueOrDefault();
			}
			set
			{
				this._cacheInfo = new CacheRefreshReason?(value);
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x060014D6 RID: 5334 RVA: 0x00045E92 File Offset: 0x00044092
		public string CacheInfoString
		{
			get
			{
				if (this._cacheInfo == null)
				{
					return null;
				}
				return this._cacheInfo.Value.ToString("D");
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x060014D7 RID: 5335 RVA: 0x00045EBD File Offset: 0x000440BD
		// (set) Token: 0x060014D8 RID: 5336 RVA: 0x00045EC5 File Offset: 0x000440C5
		public long DurationInHttpInMs { get; set; }

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x060014D9 RID: 5337 RVA: 0x00045ECE File Offset: 0x000440CE
		// (set) Token: 0x060014DA RID: 5338 RVA: 0x00045ED6 File Offset: 0x000440D6
		public long DurationInCacheInMs { get; set; }

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x060014DB RID: 5339 RVA: 0x00045EDF File Offset: 0x000440DF
		// (set) Token: 0x060014DC RID: 5340 RVA: 0x00045EE7 File Offset: 0x000440E7
		public TokenType? TokenType { get; set; }

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x060014DD RID: 5341 RVA: 0x00045EF0 File Offset: 0x000440F0
		public string TokenTypeString
		{
			get
			{
				if (this.TokenType == null)
				{
					return null;
				}
				return this.TokenType.Value.ToString("D");
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x060014DE RID: 5342 RVA: 0x00045F2C File Offset: 0x0004412C
		// (set) Token: 0x060014DF RID: 5343 RVA: 0x00045F34 File Offset: 0x00044134
		public AssertionType AssertionType { get; set; }

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x060014E0 RID: 5344 RVA: 0x00045F3D File Offset: 0x0004413D
		// (set) Token: 0x060014E1 RID: 5345 RVA: 0x00045F45 File Offset: 0x00044145
		public CacheLevel CacheLevel { get; set; }

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x060014E2 RID: 5346 RVA: 0x00045F4E File Offset: 0x0004414E
		// (set) Token: 0x060014E3 RID: 5347 RVA: 0x00045F56 File Offset: 0x00044156
		public string MsalRuntimeTelemetry { get; set; }

		// Token: 0x060014E4 RID: 5348 RVA: 0x00045F5F File Offset: 0x0004415F
		public static bool IsLongRunningObo(ApiEvent.ApiIds apiId)
		{
			return apiId == ApiEvent.ApiIds.InitiateLongRunningObo || apiId == ApiEvent.ApiIds.AcquireTokenInLongRunningObo;
		}

		// Token: 0x060014E5 RID: 5349 RVA: 0x00045F73 File Offset: 0x00044173
		public static bool IsOnBehalfOfRequest(ApiEvent.ApiIds apiId)
		{
			return apiId == ApiEvent.ApiIds.AcquireTokenOnBehalfOf || ApiEvent.IsLongRunningObo(apiId);
		}

		// Token: 0x040008A0 RID: 2208
		private RegionAutodetectionSource? _regionAutodetectionSource;

		// Token: 0x040008A1 RID: 2209
		private RegionOutcome? _regionOutcome;

		// Token: 0x040008A6 RID: 2214
		private CacheRefreshReason? _cacheInfo;

		// Token: 0x02000469 RID: 1129
		public enum ApiIds
		{
			// Token: 0x04001392 RID: 5010
			None,
			// Token: 0x04001393 RID: 5011
			AcquireTokenByAuthorizationCode = 1000,
			// Token: 0x04001394 RID: 5012
			AcquireTokenByRefreshToken,
			// Token: 0x04001395 RID: 5013
			AcquireTokenByIntegratedWindowsAuth,
			// Token: 0x04001396 RID: 5014
			AcquireTokenByUsernamePassword,
			// Token: 0x04001397 RID: 5015
			AcquireTokenForClient,
			// Token: 0x04001398 RID: 5016
			AcquireTokenInteractive,
			// Token: 0x04001399 RID: 5017
			AcquireTokenOnBehalfOf,
			// Token: 0x0400139A RID: 5018
			AcquireTokenSilent,
			// Token: 0x0400139B RID: 5019
			AcquireTokenByDeviceCode,
			// Token: 0x0400139C RID: 5020
			GetAuthorizationRequestUrl,
			// Token: 0x0400139D RID: 5021
			GetAccounts,
			// Token: 0x0400139E RID: 5022
			GetAccountById,
			// Token: 0x0400139F RID: 5023
			GetAccountsByUserFlow,
			// Token: 0x040013A0 RID: 5024
			RemoveAccount,
			// Token: 0x040013A1 RID: 5025
			RemoveOboTokens,
			// Token: 0x040013A2 RID: 5026
			AcquireTokenForSystemAssignedManagedIdentity,
			// Token: 0x040013A3 RID: 5027
			AcquireTokenForUserAssignedManagedIdentity,
			// Token: 0x040013A4 RID: 5028
			InitiateLongRunningObo,
			// Token: 0x040013A5 RID: 5029
			AcquireTokenInLongRunningObo
		}
	}
}
