using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Identity.Client.Utils;
using Microsoft.Identity.Json;

namespace Microsoft.Identity.Client.UI
{
	// Token: 0x020001D8 RID: 472
	[JsonObject]
	[Preserve(AllMembers = true)]
	internal class AuthorizationResult
	{
		// Token: 0x06001478 RID: 5240 RVA: 0x00045854 File Offset: 0x00043A54
		public static AuthorizationResult FromUri(string webAuthenticationResult)
		{
			if (string.IsNullOrWhiteSpace(webAuthenticationResult))
			{
				return AuthorizationResult.FromStatus(AuthorizationStatus.UnknownError, "authentication_failed", "The authorization server returned an invalid response. ");
			}
			string query = new Uri(webAuthenticationResult).Query;
			if (string.IsNullOrWhiteSpace(query))
			{
				return AuthorizationResult.FromStatus(AuthorizationStatus.UnknownError, "authentication_failed", "The authorization server returned an invalid response. ");
			}
			return AuthorizationResult.FromParsedValues(CoreHelpers.ParseKeyValueList(query.Substring(1), '&', true, null), webAuthenticationResult);
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x000458B5 File Offset: 0x00043AB5
		public static AuthorizationResult FromPostData(byte[] postData)
		{
			if (postData == null)
			{
				return AuthorizationResult.FromStatus(AuthorizationStatus.UnknownError, "authentication_failed", "The authorization server returned an invalid response. ");
			}
			return AuthorizationResult.FromParsedValues(CoreHelpers.ParseKeyValueList(Encoding.Default.GetString(postData).TrimEnd(new char[1]), '&', true, null), null);
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x000458F0 File Offset: 0x00043AF0
		private static AuthorizationResult FromParsedValues(Dictionary<string, string> parameters, string url = null)
		{
			string text;
			if (!parameters.TryGetValue("error", out text))
			{
				AuthorizationResult authorizationResult = new AuthorizationResult
				{
					Status = AuthorizationStatus.Success
				};
				string text2;
				if (parameters.TryGetValue("state", out text2))
				{
					authorizationResult.State = text2;
				}
				string text3;
				if (parameters.TryGetValue("cloud_instance_host_name", out text3))
				{
					authorizationResult.CloudInstanceHost = text3;
				}
				string text4;
				if (parameters.TryGetValue("client_info", out text4))
				{
					authorizationResult.ClientInfo = text4;
				}
				string text5;
				if (parameters.TryGetValue("code", out text5))
				{
					authorizationResult.Code = text5;
				}
				else
				{
					if (string.IsNullOrEmpty(url) || !url.StartsWith("msauth://", StringComparison.OrdinalIgnoreCase))
					{
						return AuthorizationResult.FromStatus(AuthorizationStatus.UnknownError, "authentication_failed", "The authorization server returned an invalid response. ");
					}
					authorizationResult.Code = url;
				}
				return authorizationResult;
			}
			string text6;
			if (parameters.TryGetValue("error_subcode", out text6) && "cancel".Equals(text6, StringComparison.OrdinalIgnoreCase))
			{
				return AuthorizationResult.FromStatus(AuthorizationStatus.UserCancel);
			}
			string text7;
			return AuthorizationResult.FromStatus(AuthorizationStatus.ProtocolError, text, parameters.TryGetValue("error_description", out text7) ? text7 : null);
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x000459E8 File Offset: 0x00043BE8
		internal static AuthorizationResult FromStatus(AuthorizationStatus status)
		{
			if (status == AuthorizationStatus.Success)
			{
				throw new InvalidOperationException("Use the FromUri builder");
			}
			AuthorizationResult authorizationResult = new AuthorizationResult
			{
				Status = status
			};
			if (status == AuthorizationStatus.UserCancel)
			{
				authorizationResult.Error = "authentication_canceled";
				authorizationResult.ErrorDescription = "User canceled authentication. ";
			}
			else if (status == AuthorizationStatus.UnknownError)
			{
				authorizationResult.Error = "unknown_error";
				authorizationResult.ErrorDescription = "Unknown error";
			}
			return authorizationResult;
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x00045A47 File Offset: 0x00043C47
		public static AuthorizationResult FromStatus(AuthorizationStatus status, string error, string errorDescription)
		{
			return new AuthorizationResult
			{
				Status = status,
				Error = error,
				ErrorDescription = errorDescription
			};
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x0600147D RID: 5245 RVA: 0x00045A63 File Offset: 0x00043C63
		// (set) Token: 0x0600147E RID: 5246 RVA: 0x00045A6B File Offset: 0x00043C6B
		public AuthorizationStatus Status { get; private set; }

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x0600147F RID: 5247 RVA: 0x00045A74 File Offset: 0x00043C74
		// (set) Token: 0x06001480 RID: 5248 RVA: 0x00045A7C File Offset: 0x00043C7C
		[JsonProperty]
		public string Code { get; set; }

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06001481 RID: 5249 RVA: 0x00045A85 File Offset: 0x00043C85
		// (set) Token: 0x06001482 RID: 5250 RVA: 0x00045A8D File Offset: 0x00043C8D
		[JsonProperty]
		public string Error { get; set; }

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06001483 RID: 5251 RVA: 0x00045A96 File Offset: 0x00043C96
		// (set) Token: 0x06001484 RID: 5252 RVA: 0x00045A9E File Offset: 0x00043C9E
		[JsonProperty]
		public string ErrorDescription { get; set; }

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06001485 RID: 5253 RVA: 0x00045AA7 File Offset: 0x00043CA7
		// (set) Token: 0x06001486 RID: 5254 RVA: 0x00045AAF File Offset: 0x00043CAF
		[JsonProperty]
		public string CloudInstanceHost { get; set; }

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06001487 RID: 5255 RVA: 0x00045AB8 File Offset: 0x00043CB8
		// (set) Token: 0x06001488 RID: 5256 RVA: 0x00045AC0 File Offset: 0x00043CC0
		public string ClientInfo { get; set; }

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06001489 RID: 5257 RVA: 0x00045AC9 File Offset: 0x00043CC9
		// (set) Token: 0x0600148A RID: 5258 RVA: 0x00045AD1 File Offset: 0x00043CD1
		public string State { get; set; }
	}
}
