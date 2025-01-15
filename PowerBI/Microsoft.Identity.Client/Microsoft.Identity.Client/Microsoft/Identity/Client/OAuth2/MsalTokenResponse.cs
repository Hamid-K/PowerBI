using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Extensibility;
using Microsoft.Identity.Client.Http;
using Microsoft.Identity.Client.ManagedIdentity;
using Microsoft.Identity.Client.Utils;
using Microsoft.Identity.Json;
using Microsoft.Identity.Json.Linq;

namespace Microsoft.Identity.Client.OAuth2
{
	// Token: 0x02000205 RID: 517
	[JsonObject]
	[Preserve(AllMembers = true)]
	internal class MsalTokenResponse : OAuth2ResponseBase
	{
		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x060015C6 RID: 5574 RVA: 0x00047EF3 File Offset: 0x000460F3
		// (set) Token: 0x060015C7 RID: 5575 RVA: 0x00047EFB File Offset: 0x000460FB
		[JsonExtensionData]
		public Dictionary<string, JToken> ExtensionData { get; set; }

		// Token: 0x060015C8 RID: 5576 RVA: 0x00047F04 File Offset: 0x00046104
		public IReadOnlyDictionary<string, string> CreateExtensionDataStringMap()
		{
			if (this.ExtensionData == null || this.ExtensionData.Count == 0)
			{
				return CollectionHelpers.GetEmptyDictionary<string, string>();
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			foreach (KeyValuePair<string, JToken> keyValuePair in this.ExtensionData)
			{
				if (keyValuePair.Value.Type == JTokenType.String || keyValuePair.Value.Type == JTokenType.Uri || keyValuePair.Value.Type == JTokenType.Boolean || keyValuePair.Value.Type == JTokenType.Date || keyValuePair.Value.Type == JTokenType.Float || keyValuePair.Value.Type == JTokenType.Guid || keyValuePair.Value.Type == JTokenType.Integer || keyValuePair.Value.Type == JTokenType.TimeSpan || keyValuePair.Value.Type == JTokenType.Null)
				{
					dictionary.Add(keyValuePair.Key, keyValuePair.Value.ToString());
				}
			}
			return dictionary;
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x060015C9 RID: 5577 RVA: 0x00048024 File Offset: 0x00046224
		// (set) Token: 0x060015CA RID: 5578 RVA: 0x0004802C File Offset: 0x0004622C
		[JsonProperty("token_type")]
		public string TokenType { get; set; }

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x060015CB RID: 5579 RVA: 0x00048035 File Offset: 0x00046235
		// (set) Token: 0x060015CC RID: 5580 RVA: 0x0004803D File Offset: 0x0004623D
		[JsonProperty("access_token")]
		public string AccessToken { get; set; }

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x060015CD RID: 5581 RVA: 0x00048046 File Offset: 0x00046246
		// (set) Token: 0x060015CE RID: 5582 RVA: 0x0004804E File Offset: 0x0004624E
		[JsonProperty("refresh_token")]
		public string RefreshToken { get; set; }

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x060015CF RID: 5583 RVA: 0x00048057 File Offset: 0x00046257
		// (set) Token: 0x060015D0 RID: 5584 RVA: 0x0004805F File Offset: 0x0004625F
		[JsonProperty("scope")]
		public string Scope { get; set; }

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x060015D1 RID: 5585 RVA: 0x00048068 File Offset: 0x00046268
		// (set) Token: 0x060015D2 RID: 5586 RVA: 0x00048070 File Offset: 0x00046270
		[JsonProperty("client_info")]
		public string ClientInfo { get; set; }

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x060015D3 RID: 5587 RVA: 0x00048079 File Offset: 0x00046279
		// (set) Token: 0x060015D4 RID: 5588 RVA: 0x00048081 File Offset: 0x00046281
		[JsonProperty("id_token")]
		public string IdToken { get; set; }

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x060015D5 RID: 5589 RVA: 0x0004808A File Offset: 0x0004628A
		// (set) Token: 0x060015D6 RID: 5590 RVA: 0x00048092 File Offset: 0x00046292
		[JsonProperty("expires_in")]
		public long ExpiresIn { get; set; }

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x060015D7 RID: 5591 RVA: 0x0004809B File Offset: 0x0004629B
		// (set) Token: 0x060015D8 RID: 5592 RVA: 0x000480A3 File Offset: 0x000462A3
		[JsonProperty("ext_expires_in")]
		public long ExtendedExpiresIn { get; set; }

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x060015D9 RID: 5593 RVA: 0x000480AC File Offset: 0x000462AC
		// (set) Token: 0x060015DA RID: 5594 RVA: 0x000480B4 File Offset: 0x000462B4
		[JsonProperty("refresh_in")]
		public long? RefreshIn { get; set; }

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x060015DB RID: 5595 RVA: 0x000480BD File Offset: 0x000462BD
		// (set) Token: 0x060015DC RID: 5596 RVA: 0x000480C5 File Offset: 0x000462C5
		[JsonProperty("foci")]
		public string FamilyId { get; set; }

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x060015DD RID: 5597 RVA: 0x000480CE File Offset: 0x000462CE
		// (set) Token: 0x060015DE RID: 5598 RVA: 0x000480D6 File Offset: 0x000462D6
		[JsonProperty("spa_code")]
		public string SpaAuthCode { get; set; }

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x060015DF RID: 5599 RVA: 0x000480DF File Offset: 0x000462DF
		// (set) Token: 0x060015E0 RID: 5600 RVA: 0x000480E7 File Offset: 0x000462E7
		[JsonProperty("authority")]
		public string AuthorityUrl { get; set; }

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x060015E1 RID: 5601 RVA: 0x000480F0 File Offset: 0x000462F0
		// (set) Token: 0x060015E2 RID: 5602 RVA: 0x000480F8 File Offset: 0x000462F8
		public string TenantId { get; set; }

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x060015E3 RID: 5603 RVA: 0x00048101 File Offset: 0x00046301
		// (set) Token: 0x060015E4 RID: 5604 RVA: 0x00048109 File Offset: 0x00046309
		public string Upn { get; set; }

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x060015E5 RID: 5605 RVA: 0x00048112 File Offset: 0x00046312
		// (set) Token: 0x060015E6 RID: 5606 RVA: 0x0004811A File Offset: 0x0004631A
		public string AccountUserId { get; set; }

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x060015E7 RID: 5607 RVA: 0x00048123 File Offset: 0x00046323
		// (set) Token: 0x060015E8 RID: 5608 RVA: 0x0004812B File Offset: 0x0004632B
		public string WamAccountId { get; set; }

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x060015E9 RID: 5609 RVA: 0x00048134 File Offset: 0x00046334
		// (set) Token: 0x060015EA RID: 5610 RVA: 0x0004813C File Offset: 0x0004633C
		public TokenSource TokenSource { get; set; }

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x060015EB RID: 5611 RVA: 0x00048145 File Offset: 0x00046345
		// (set) Token: 0x060015EC RID: 5612 RVA: 0x0004814D File Offset: 0x0004634D
		public HttpResponse HttpResponse { get; set; }

		// Token: 0x060015ED RID: 5613 RVA: 0x00048158 File Offset: 0x00046358
		internal static MsalTokenResponse CreateFromiOSBrokerResponse(Dictionary<string, string> responseDictionary)
		{
			string text;
			if (responseDictionary.TryGetValue("broker_error_code", out text))
			{
				string text3;
				string text2 = (responseDictionary.TryGetValue("error_metadata", out text3) ? text3 : null);
				Dictionary<string, string> dictionary = null;
				if (text2 != null)
				{
					dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Uri.UnescapeDataString(text2));
				}
				string text4 = null;
				if (dictionary != null)
				{
					dictionary.TryGetValue("home_account_id", out text4);
				}
				string text5;
				string text6;
				string text7;
				return new MsalTokenResponse
				{
					Error = text,
					ErrorDescription = (responseDictionary.TryGetValue("error_description", out text5) ? CoreHelpers.UrlDecode(text5) : string.Empty),
					SubError = (responseDictionary.TryGetValue("suberror", out text6) ? text6 : string.Empty),
					AccountUserId = ((text4 != null) ? AccountId.ParseFromString(text4).ObjectId : null),
					TenantId = ((text4 != null) ? AccountId.ParseFromString(text4).TenantId : null),
					Upn = ((dictionary != null && dictionary.ContainsKey("username")) ? dictionary["username"] : null),
					CorrelationId = (responseDictionary.TryGetValue("correlation_id", out text7) ? text7 : null)
				};
			}
			string text8;
			string text9;
			string text10;
			MsalTokenResponse msalTokenResponse = new MsalTokenResponse
			{
				AccessToken = responseDictionary["access_token"],
				RefreshToken = (responseDictionary.TryGetValue("refresh_token", out text8) ? text8 : null),
				IdToken = responseDictionary["id_token"],
				TokenType = "Bearer",
				CorrelationId = responseDictionary["correlation_id"],
				Scope = responseDictionary["scope"],
				ExpiresIn = (responseDictionary.TryGetValue("expires_on", out text9) ? DateTimeHelpers.GetDurationFromNowInSeconds(text9) : 0L),
				ClientInfo = (responseDictionary.TryGetValue("client_info", out text10) ? text10 : null),
				TokenSource = TokenSource.Broker
			};
			string text11;
			if (responseDictionary.TryGetValue("refresh_in", out text11))
			{
				msalTokenResponse.RefreshIn = new long?(long.Parse(text11, CultureInfo.InvariantCulture));
			}
			return msalTokenResponse;
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x00048350 File Offset: 0x00046550
		internal static MsalTokenResponse CreateFromManagedIdentityResponse(ManagedIdentityResponse managedIdentityResponse)
		{
			MsalTokenResponse.ValidateManagedIdentityResult(managedIdentityResponse);
			long durationFromNowInSeconds = DateTimeHelpers.GetDurationFromNowInSeconds(managedIdentityResponse.ExpiresOn);
			return new MsalTokenResponse
			{
				AccessToken = managedIdentityResponse.AccessToken,
				ExpiresIn = durationFromNowInSeconds,
				TokenType = managedIdentityResponse.TokenType,
				TokenSource = TokenSource.IdentityProvider,
				RefreshIn = MsalTokenResponse.InferManagedIdentityRefreshInValue(durationFromNowInSeconds)
			};
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x000483A8 File Offset: 0x000465A8
		private static long? InferManagedIdentityRefreshInValue(long expiresIn)
		{
			if (expiresIn > 7200L)
			{
				return new long?(expiresIn / 2L);
			}
			return null;
		}

		// Token: 0x060015F0 RID: 5616 RVA: 0x000483D1 File Offset: 0x000465D1
		private static void ValidateManagedIdentityResult(ManagedIdentityResponse response)
		{
			if (string.IsNullOrEmpty(response.AccessToken))
			{
				MsalTokenResponse.HandleInvalidExternalValueError("AccessToken");
			}
			if (DateTimeHelpers.GetDurationFromNowInSeconds(response.ExpiresOn) <= 0L)
			{
				MsalTokenResponse.HandleInvalidExternalValueError("ExpiresOn");
			}
		}

		// Token: 0x060015F1 RID: 5617 RVA: 0x00048404 File Offset: 0x00046604
		internal static MsalTokenResponse CreateFromAppProviderResponse(AppTokenProviderResult tokenProviderResponse)
		{
			MsalTokenResponse.ValidateTokenProviderResult(tokenProviderResponse);
			MsalTokenResponse msalTokenResponse = new MsalTokenResponse();
			msalTokenResponse.AccessToken = tokenProviderResponse.AccessToken;
			msalTokenResponse.RefreshToken = null;
			msalTokenResponse.IdToken = null;
			msalTokenResponse.TokenType = "Bearer";
			msalTokenResponse.ExpiresIn = tokenProviderResponse.ExpiresInSeconds;
			msalTokenResponse.ClientInfo = null;
			msalTokenResponse.TokenSource = TokenSource.IdentityProvider;
			msalTokenResponse.TenantId = null;
			long? refreshInSeconds = tokenProviderResponse.RefreshInSeconds;
			msalTokenResponse.RefreshIn = ((refreshInSeconds != null) ? refreshInSeconds : MsalTokenResponse.EstimateRefreshIn(tokenProviderResponse.ExpiresInSeconds));
			return msalTokenResponse;
		}

		// Token: 0x060015F2 RID: 5618 RVA: 0x00048488 File Offset: 0x00046688
		private static long? EstimateRefreshIn(long expiresInSeconds)
		{
			if (expiresInSeconds >= 7200L)
			{
				return new long?(expiresInSeconds / 2L);
			}
			return null;
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x000484B1 File Offset: 0x000466B1
		private static void ValidateTokenProviderResult(AppTokenProviderResult TokenProviderResult)
		{
			if (string.IsNullOrEmpty(TokenProviderResult.AccessToken))
			{
				MsalTokenResponse.HandleInvalidExternalValueError("AccessToken");
			}
			if (TokenProviderResult.ExpiresInSeconds == 0L || TokenProviderResult.ExpiresInSeconds < 0L)
			{
				MsalTokenResponse.HandleInvalidExternalValueError("ExpiresInSeconds");
			}
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x000484E6 File Offset: 0x000466E6
		private static void HandleInvalidExternalValueError(string nameOfValue)
		{
			throw new MsalClientException("invalid_token_provider_response_value", MsalErrorMessage.InvalidTokenProviderResponseValue(nameOfValue));
		}

		// Token: 0x060015F5 RID: 5621 RVA: 0x000484F8 File Offset: 0x000466F8
		internal static MsalTokenResponse CreateFromAndroidBrokerResponse(string jsonResponse, string correlationId)
		{
			JObject jobject = JsonHelper.ParseIntoJsonObject(jsonResponse);
			JToken jtoken = jobject["broker_error_code"];
			string text = ((jtoken != null) ? jtoken.ToString() : null);
			if (!string.IsNullOrEmpty(text))
			{
				MsalTokenResponse msalTokenResponse = new MsalTokenResponse();
				msalTokenResponse.Error = text;
				JToken jtoken2 = jobject["broker_error_message"];
				msalTokenResponse.ErrorDescription = ((jtoken2 != null) ? jtoken2.ToString() : null);
				JToken jtoken3 = jobject["authority"];
				msalTokenResponse.AuthorityUrl = ((jtoken3 != null) ? jtoken3.ToString() : null);
				JToken jtoken4 = jobject["tenant_id"];
				msalTokenResponse.TenantId = ((jtoken4 != null) ? jtoken4.ToString() : null);
				JToken jtoken5 = jobject["username"];
				msalTokenResponse.Upn = ((jtoken5 != null) ? jtoken5.ToString() : null);
				JToken jtoken6 = jobject["local_account_id"];
				msalTokenResponse.AccountUserId = ((jtoken6 != null) ? jtoken6.ToString() : null);
				return msalTokenResponse;
			}
			MsalTokenResponse msalTokenResponse2 = new MsalTokenResponse();
			msalTokenResponse2.AccessToken = jobject["access_token"].ToString();
			msalTokenResponse2.IdToken = jobject["id_token"].ToString();
			msalTokenResponse2.CorrelationId = correlationId;
			msalTokenResponse2.Scope = jobject["scopes"].ToString();
			msalTokenResponse2.ExpiresIn = DateTimeHelpers.GetDurationFromNowInSeconds(jobject["expires_on"].ToString());
			msalTokenResponse2.ExtendedExpiresIn = DateTimeHelpers.GetDurationFromNowInSeconds(jobject["ext_expires_on"].ToString());
			msalTokenResponse2.ClientInfo = jobject["client_info"].ToString();
			JToken jtoken7 = jobject["token_type"];
			msalTokenResponse2.TokenType = ((jtoken7 != null) ? jtoken7.ToString() : null) ?? "Bearer";
			msalTokenResponse2.TokenSource = TokenSource.Broker;
			JToken jtoken8 = jobject["authority"];
			msalTokenResponse2.AuthorityUrl = ((jtoken8 != null) ? jtoken8.ToString() : null);
			JToken jtoken9 = jobject["tenant_id"];
			msalTokenResponse2.TenantId = ((jtoken9 != null) ? jtoken9.ToString() : null);
			JToken jtoken10 = jobject["username"];
			msalTokenResponse2.Upn = ((jtoken10 != null) ? jtoken10.ToString() : null);
			JToken jtoken11 = jobject["local_account_id"];
			msalTokenResponse2.AccountUserId = ((jtoken11 != null) ? jtoken11.ToString() : null);
			return msalTokenResponse2;
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x00048708 File Offset: 0x00046908
		public void Log(ILoggerAdapter logger, LogLevel logLevel)
		{
			if (logger.IsLoggingEnabled(logLevel))
			{
				string text = string.Format("\r\n[MsalTokenResponse]\r\nError: {0}\r\nErrorDescription: {1}\r\nScopes: {2}\r\nExpiresIn: {3}\r\nRefreshIn: {4}\r\nAccessToken returned: {5}\r\nAccessToken Type: {6}\r\nRefreshToken returned: {7}\r\nIdToken returned: {8}\r\nClientInfo: {9}\r\nFamilyId: {10}\r\nWamAccountId exists: {11}", new object[]
				{
					base.Error,
					base.ErrorDescription,
					this.Scope,
					this.ExpiresIn,
					this.RefreshIn,
					!string.IsNullOrEmpty(this.AccessToken),
					this.TokenType,
					!string.IsNullOrEmpty(this.RefreshToken),
					!string.IsNullOrEmpty(this.IdToken),
					this.ClientInfo,
					this.FamilyId,
					!string.IsNullOrEmpty(this.WamAccountId)
				});
				string text2 = string.Format("\r\n[MsalTokenResponse]\r\nError: {0}\r\nErrorDescription: {1}\r\nScopes: {2}\r\nExpiresIn: {3}\r\nRefreshIn: {4}\r\nAccessToken returned: {5}\r\nAccessToken Type: {6}\r\nRefreshToken returned: {7}\r\nIdToken returned: {8}\r\nClientInfo returned: {9}\r\nFamilyId: {10}\r\nWamAccountId exists: {11}", new object[]
				{
					base.Error,
					base.ErrorDescription,
					this.Scope,
					this.ExpiresIn,
					this.RefreshIn,
					!string.IsNullOrEmpty(this.AccessToken),
					this.TokenType,
					!string.IsNullOrEmpty(this.RefreshToken),
					!string.IsNullOrEmpty(this.IdToken),
					!string.IsNullOrEmpty(this.ClientInfo),
					this.FamilyId,
					!string.IsNullOrEmpty(this.WamAccountId)
				});
				logger.Log(logLevel, text, text2);
			}
		}

		// Token: 0x04000906 RID: 2310
		private const string iOSBrokerErrorMetadata = "error_metadata";

		// Token: 0x04000907 RID: 2311
		private const string iOSBrokerHomeAccountId = "home_account_id";
	}
}
