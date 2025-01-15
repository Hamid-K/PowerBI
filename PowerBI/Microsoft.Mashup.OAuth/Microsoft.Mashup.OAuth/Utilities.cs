using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;

namespace Microsoft.Mashup.OAuth
{
	// Token: 0x0200002A RID: 42
	internal static class Utilities
	{
		// Token: 0x06000134 RID: 308 RVA: 0x00006084 File Offset: 0x00004284
		internal static string GetExpiresAtString(string expiresIn)
		{
			DateTime dateTime;
			if (Utilities.TryGetExpiresAt(expiresIn, out dateTime))
			{
				return dateTime.ToString("R", CultureInfo.InvariantCulture);
			}
			return null;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000060B0 File Offset: 0x000042B0
		internal static bool ExpiresInLessThan(string expiresIn, TimeSpan minimumHours)
		{
			double num;
			if (!string.IsNullOrEmpty(expiresIn) && double.TryParse(expiresIn, NumberStyles.Any, CultureInfo.InvariantCulture, out num))
			{
				try
				{
					return TimeSpan.FromSeconds(num) < minimumHours;
				}
				catch (ArgumentOutOfRangeException)
				{
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00006100 File Offset: 0x00004300
		internal static string GetScope(string[] scopes)
		{
			if (scopes == null)
			{
				throw new ArgumentNullException("scopes");
			}
			return string.Join(" ", scopes);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000611C File Offset: 0x0000431C
		internal static bool TryGetExpiresIn(string expiresAt, out TimeSpan expiresIn)
		{
			DateTime dateTime;
			if (expiresAt != null && DateTime.TryParse(expiresAt, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out dateTime))
			{
				expiresIn = dateTime - DateTime.UtcNow;
				return true;
			}
			expiresIn = default(TimeSpan);
			return false;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00006158 File Offset: 0x00004358
		internal static Uri GetLoginUri(Uri authorizationUri, Dictionary<string, string> onAuthenticate, string state, string displayMode)
		{
			if (string.IsNullOrEmpty(state))
			{
				throw new ArgumentNullException("state");
			}
			UriBuilder uriBuilder = new UriBuilder(authorizationUri);
			if (onAuthenticate != null)
			{
				NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(uriBuilder.Query ?? string.Empty);
				nameValueCollection["state"] = state;
				nameValueCollection["display"] = displayMode ?? string.Empty;
				foreach (KeyValuePair<string, string> keyValuePair in onAuthenticate)
				{
					nameValueCollection[keyValuePair.Key] = keyValuePair.Value;
				}
				uriBuilder.Query = nameValueCollection.ToString();
			}
			return uriBuilder.Uri;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000621C File Offset: 0x0000441C
		internal static Uri AddQueryParametersToUri(Uri uri, Dictionary<string, string> queryParameters)
		{
			UriBuilder uriBuilder = new UriBuilder(uri);
			if (queryParameters != null)
			{
				NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(uriBuilder.Query ?? string.Empty);
				foreach (KeyValuePair<string, string> keyValuePair in queryParameters)
				{
					nameValueCollection[keyValuePair.Key] = keyValuePair.Value;
				}
				uriBuilder.Query = nameValueCollection.ToString();
			}
			return uriBuilder.Uri;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000062A8 File Offset: 0x000044A8
		internal static string GetAuthority(string url)
		{
			return Uri.UnescapeDataString(new Uri(url).GetLeftPart(UriPartial.Authority));
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000062BC File Offset: 0x000044BC
		internal static string GetExpiresInString(DateTime expiresAt)
		{
			return (expiresAt.ToUniversalTime() - DateTime.UtcNow).TotalSeconds.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000062EF File Offset: 0x000044EF
		internal static string PoolRefreshToken(string credentialRefreshToken, string responseRefreshToken)
		{
			if (responseRefreshToken != null && responseRefreshToken != credentialRefreshToken)
			{
				return responseRefreshToken;
			}
			return credentialRefreshToken;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00006300 File Offset: 0x00004500
		internal static OAuthToken FinishLogin(OAuthServices services, Uri uri, NameValueCollection values)
		{
			byte[] array;
			WebHeaderCollection webHeaderCollection;
			Exception ex;
			if (Utilities.TryGetPostResponse(services, uri, values, out array, out webHeaderCollection, out ex))
			{
				OAuthToken oauthTokenFromJsonResponseBytes = Utilities.GetOAuthTokenFromJsonResponseBytes(services, webHeaderCollection, array);
				if (oauthTokenFromJsonResponseBytes != null)
				{
					return oauthTokenFromJsonResponseBytes;
				}
			}
			throw new OAuthException(OAuthStrings.InvalidToken, ex);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00006338 File Offset: 0x00004538
		internal static OAuthToken RefreshJsonToken(OAuthServices services, string refreshToken, Uri uri, NameValueCollection values)
		{
			if (refreshToken != null)
			{
				values["refresh_token"] = refreshToken;
				byte[] array;
				WebHeaderCollection webHeaderCollection;
				Exception ex;
				if (Utilities.TryGetPostResponse(services, uri, values, out array, out webHeaderCollection, out ex))
				{
					OAuthToken oauthTokenFromJsonResponseBytes = Utilities.GetOAuthTokenFromJsonResponseBytes(services, webHeaderCollection, array);
					if (oauthTokenFromJsonResponseBytes != null)
					{
						oauthTokenFromJsonResponseBytes.RefreshToken = Utilities.PoolRefreshToken(refreshToken, oauthTokenFromJsonResponseBytes.RefreshToken);
						return oauthTokenFromJsonResponseBytes;
					}
				}
				else
				{
					OAuthError oauthErrorFromResponseBytes = Utilities.GetOAuthErrorFromResponseBytes<OAuthError>(array);
					if (oauthErrorFromResponseBytes != null)
					{
						throw new OAuthWebException(oauthErrorFromResponseBytes, array, ex);
					}
					throw new OAuthException(OAuthStrings.RefreshFailed, ex);
				}
			}
			OAuthException ex2 = new OAuthException(OAuthStrings.RefreshFailed);
			ex2.Data["PowerBINonFatalError_ErrorDescription"] = "MissingRefreshToken";
			throw ex2;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x000063C8 File Offset: 0x000045C8
		internal static NameValueCollection RefreshToken(OAuthServices services, TokenCredential credential, Uri uri, NameValueCollection values)
		{
			if (credential == null)
			{
				throw new ArgumentNullException("credential");
			}
			if (credential.RefreshToken == null)
			{
				throw new OAuthException(OAuthStrings.RefreshFailed);
			}
			values["refresh_token"] = credential.RefreshToken;
			byte[] array;
			WebHeaderCollection webHeaderCollection;
			Exception ex;
			if (Utilities.TryGetPostResponse(services, uri, values, out array, out webHeaderCollection, out ex))
			{
				NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(HttpUtility.UrlDecode(Encoding.UTF8.GetString(array)));
				nameValueCollection["refresh_token"] = Utilities.PoolRefreshToken(credential.RefreshToken, nameValueCollection["refresh_token"]);
				return nameValueCollection;
			}
			OAuthError oauthErrorFromResponseBytes = Utilities.GetOAuthErrorFromResponseBytes<OAuthError>(array);
			if (oauthErrorFromResponseBytes != null)
			{
				throw new OAuthWebException(oauthErrorFromResponseBytes, array, ex);
			}
			throw new OAuthException(OAuthStrings.RefreshFailed, ex);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00006474 File Offset: 0x00004674
		internal static NameValueCollection ValidateResponse(OAuthServices services, string queryOrFragment, string state)
		{
			NameValueCollection nameValueCollection = Utilities.ParseQueryString(queryOrFragment);
			string text = nameValueCollection["state"];
			if (text == null || !string.Equals(text, state, StringComparison.Ordinal))
			{
				services.Write("OAuth/Utilities/ValidateResponse", TraceEventType.Error, new object[] { "QueryOrFragment", queryOrFragment, "State", state });
				throw new OAuthException("invalid_state", OAuthStrings.InvalidOAuthState, null);
			}
			string text2 = nameValueCollection["error"];
			if (text2 != null)
			{
				string text3 = nameValueCollection["error_description"];
				if (text3 == null && text2 == "access_denied")
				{
					text3 = OAuthStrings.Error_AccessDenied;
				}
				services.Write("OAuth/Utilities/ValidateResponse", TraceEventType.Error, new object[] { "Error", text2 });
				throw new OAuthException(OAuthStrings.RedirectError(text2, text3));
			}
			if (!string.IsNullOrEmpty(nameValueCollection["code"]) || !string.IsNullOrEmpty(nameValueCollection["access_token"]))
			{
				return nameValueCollection;
			}
			services.Write("OAuth/Utilities/ValidateResponse", TraceEventType.Error, new object[] { "QueryOrFragment", queryOrFragment });
			throw new OAuthException("invalid_response", OAuthStrings.InvalidOAuthResponse, null);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00006595 File Offset: 0x00004795
		internal static void ValidateFinishLoginArguments(Uri callbackUri, string state)
		{
			if (callbackUri == null)
			{
				throw new ArgumentNullException("callbackUri");
			}
			if (string.IsNullOrEmpty(state))
			{
				throw new ArgumentNullException("state");
			}
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000065BE File Offset: 0x000047BE
		internal static void ValidateRefreshArguments(TokenCredential credential)
		{
			if (credential == null)
			{
				throw new ArgumentNullException("credential");
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x000065CE File Offset: 0x000047CE
		internal static T DecodeJsonObject<T>(Stream stream) where T : class
		{
			return new DataContractJsonSerializer(typeof(T)).ReadObject(stream) as T;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000065F0 File Offset: 0x000047F0
		private static bool TryGetPostResponse(OAuthServices services, Uri uri, NameValueCollection values, out byte[] response, out WebHeaderCollection headers, out Exception exception)
		{
			response = null;
			headers = null;
			exception = null;
			if (uri == null)
			{
				services.Write("OAuth/Utilities/TryGetPostResponse", TraceEventType.Error, new object[] { "Message", "URI is null" });
				return false;
			}
			try
			{
				WebRequest webRequest = services.CreateRequest(uri);
				webRequest.Method = "POST";
				webRequest.ContentType = "application/x-www-form-urlencoded";
				using (Stream requestStream = webRequest.GetRequestStream())
				{
					StringBuilder stringBuilder = new StringBuilder();
					string[] allKeys = values.AllKeys;
					for (int i = 0; i < allKeys.Length; i++)
					{
						if (i > 0)
						{
							stringBuilder.Append('&');
						}
						stringBuilder.Append(HttpUtility.UrlEncode(allKeys[i]));
						stringBuilder.Append('=');
						stringBuilder.Append(HttpUtility.UrlEncode(values[allKeys[i]]));
					}
					byte[] bytes = Encoding.UTF8.GetBytes(stringBuilder.ToString());
					requestStream.Write(bytes, 0, bytes.Length);
				}
				using (WebResponse response2 = webRequest.GetResponse())
				{
					response = Utilities.GetResponseBody(response2);
					headers = response2.Headers;
					return true;
				}
			}
			catch (WebException ex)
			{
				exception = ex;
				if (ex.Response != null)
				{
					using (WebResponse response3 = ex.Response)
					{
						response = Utilities.GetResponseBody(response3);
						headers = response3.Headers;
						services.Write("OAuth/Utilities/TryGetPostResponse", TraceEventType.Error, new object[]
						{
							"WebException",
							ex,
							"Status",
							services.GetResponseStatus(response3),
							"Scheme",
							uri.Scheme,
							"Host",
							uri.Host,
							"LocalPath",
							uri.LocalPath,
							"InterestingHeaders",
							services.GetInterestingHeaders(headers),
							"Response",
							Utilities.Contents(response)
						});
						goto IL_01FA;
					}
				}
				services.Write("OAuth/Utilities/TryGetPostResponse", ex);
				IL_01FA:;
			}
			catch (IOException ex2)
			{
				exception = ex2;
				services.Write("OAuth/Utilities/TryGetPostResponse", TraceEventType.Error, new object[] { "Exception", ex2 });
			}
			return false;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00006868 File Offset: 0x00004A68
		internal static byte[] GetResponseBody(WebResponse webResponse)
		{
			byte[] array2;
			using (Stream responseStream = webResponse.GetResponseStream())
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					byte[] array = new byte[4096];
					for (;;)
					{
						int num = responseStream.Read(array, 0, array.Length);
						if (num == 0)
						{
							break;
						}
						memoryStream.Write(array, 0, num);
					}
					array2 = memoryStream.ToArray();
				}
			}
			return array2;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000068E4 File Offset: 0x00004AE4
		private static OAuthToken GetOAuthTokenFromJsonResponseBytes(OAuthServices tracingService, WebHeaderCollection headers, byte[] responseBytes)
		{
			OAuthToken oauthToken2;
			try
			{
				using (MemoryStream memoryStream = new MemoryStream(responseBytes))
				{
					OAuthToken oauthToken = new DataContractJsonSerializer(typeof(OAuthToken)).ReadObject(memoryStream) as OAuthToken;
					if (oauthToken.ExpiresIn != null)
					{
						oauthToken.Expires = Utilities.GetExpiresAtString(oauthToken.ExpiresIn);
					}
					oauthToken2 = oauthToken;
				}
			}
			catch (SerializationException ex)
			{
				tracingService.Write("OAuth/Utilities/GetOAuthTokenFromJsonResponseBytes", TraceEventType.Error, new object[] { "Headers", headers, "Exception", ex });
				throw new OAuthException(OAuthStrings.InvalidToken + " " + tracingService.GetInterestingHeaders(headers), ex);
			}
			return oauthToken2;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x000069A4 File Offset: 0x00004BA4
		internal static T GetOAuthErrorFromResponseBytes<T>(byte[] responseBytes) where T : OAuthError
		{
			T t;
			if (responseBytes == null)
			{
				t = default(T);
				return t;
			}
			try
			{
				using (MemoryStream memoryStream = new MemoryStream(responseBytes))
				{
					t = new DataContractJsonSerializer(typeof(T)).ReadObject(memoryStream) as T;
				}
			}
			catch (SerializationException)
			{
				t = default(T);
			}
			return t;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00006A1C File Offset: 0x00004C1C
		internal static string Contents(byte[] bytes)
		{
			string text;
			try
			{
				text = Encoding.UTF8.GetString(bytes);
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				text = Convert.ToBase64String(bytes);
			}
			return text.Substring(0, Math.Min(text.Length, 500));
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00006A70 File Offset: 0x00004C70
		private static NameValueCollection ParseQueryString(string queryString)
		{
			queryString = queryString ?? string.Empty;
			return HttpUtility.ParseQueryString(queryString.TrimStart(Constants.QueryDelimiters()));
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00006A90 File Offset: 0x00004C90
		private static bool TryGetExpiresAt(string expiresIn, out DateTime dateTime)
		{
			double num;
			if (!string.IsNullOrEmpty(expiresIn) && double.TryParse(expiresIn, NumberStyles.Any, CultureInfo.InvariantCulture, out num))
			{
				try
				{
					dateTime = DateTime.UtcNow.AddSeconds(num);
					return true;
				}
				catch (ArgumentOutOfRangeException)
				{
				}
			}
			dateTime = default(DateTime);
			return false;
		}
	}
}
