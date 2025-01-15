using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Microsoft.Internal;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Library.Uris;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Http
{
	// Token: 0x02000A61 RID: 2657
	internal abstract class HttpResourceCredentialDispatcher : ResourceCredentialDispatcher
	{
		// Token: 0x06004A1C RID: 18972 RVA: 0x000F6E79 File Offset: 0x000F5079
		public HttpResourceCredentialDispatcher(IEngineHost hostEnvironment)
		{
			this.hostEnvironment = hostEnvironment;
		}

		// Token: 0x17001760 RID: 5984
		// (get) Token: 0x06004A1D RID: 18973 RVA: 0x000F6E88 File Offset: 0x000F5088
		protected IEngineHost Host
		{
			get
			{
				return this.hostEnvironment;
			}
		}

		// Token: 0x06004A1E RID: 18974 RVA: 0x000F6E90 File Offset: 0x000F5090
		protected virtual bool Apply(ResourceCredentialCollection credentials)
		{
			this.resource = credentials.Resource;
			IResourceCredential resourceCredential = credentials.RemoveAdornments();
			return resourceCredential == null || base.Apply(resourceCredential);
		}

		// Token: 0x06004A1F RID: 18975 RVA: 0x000F6EBC File Offset: 0x000F50BC
		public static bool ApplyCredentialsToRequest(MashupHttpWebRequest request, ResourceCredentialCollection credentials, IEngineHost hostEnvironment, string oAuthResource)
		{
			if (oAuthResource == null)
			{
				oAuthResource = request.RequestUri.GetLeftPart(UriPartial.Authority);
			}
			return (credentials.Resource.IsAzureStorageResource() ? new HttpResourceCredentialDispatcher.AzureStorageRequestCredentialDispatcher(request, hostEnvironment, oAuthResource) : new HttpResourceCredentialDispatcher.WebRequestCredentialDispatcher(request, hostEnvironment, oAuthResource)).Apply(credentials);
		}

		// Token: 0x06004A20 RID: 18976 RVA: 0x000F6EF4 File Offset: 0x000F50F4
		public static bool ApplyCredentialsToUri(UriBuilder builder, string apiKeyName, ResourceCredentialCollection credentials, IEngineHost hostEnvironment)
		{
			return new HttpResourceCredentialDispatcher.UriCredentialDispatcher(builder, apiKeyName, hostEnvironment).Apply(credentials);
		}

		// Token: 0x06004A21 RID: 18977 RVA: 0x00002105 File Offset: 0x00000305
		protected sealed override bool Apply(FtpLoginAuthCredential credential)
		{
			return false;
		}

		// Token: 0x06004A22 RID: 18978 RVA: 0x00002105 File Offset: 0x00000305
		protected sealed override bool Apply(SqlAuthCredential credential)
		{
			return false;
		}

		// Token: 0x06004A23 RID: 18979 RVA: 0x00002105 File Offset: 0x00000305
		protected sealed override bool Apply(EncryptedConnectionAdornment credential)
		{
			return false;
		}

		// Token: 0x06004A24 RID: 18980 RVA: 0x00002105 File Offset: 0x00000305
		protected sealed override bool Apply(ConnectionStringAdornment credential)
		{
			return false;
		}

		// Token: 0x04002777 RID: 10103
		public const string SASAuthentication = "SAS";

		// Token: 0x04002778 RID: 10104
		public const string Token = "Token";

		// Token: 0x04002779 RID: 10105
		private readonly IEngineHost hostEnvironment;

		// Token: 0x0400277A RID: 10106
		protected IResource resource;

		// Token: 0x02000A62 RID: 2658
		private class UriCredentialDispatcher : HttpResourceCredentialDispatcher
		{
			// Token: 0x06004A25 RID: 18981 RVA: 0x000F6F04 File Offset: 0x000F5104
			public UriCredentialDispatcher(UriBuilder builder, string apiKeyName, IEngineHost hostEnvironment)
				: base(hostEnvironment)
			{
				this.apiKeyName = apiKeyName;
				this.builder = builder;
			}

			// Token: 0x06004A26 RID: 18982 RVA: 0x00002139 File Offset: 0x00000339
			protected override bool Apply(BasicAuthCredential credential)
			{
				return true;
			}

			// Token: 0x06004A27 RID: 18983 RVA: 0x00002139 File Offset: 0x00000339
			protected override bool Apply(FeedKeyCredential credential)
			{
				return true;
			}

			// Token: 0x06004A28 RID: 18984 RVA: 0x000F6F1B File Offset: 0x000F511B
			protected override bool Apply(WebApiKeyCredential credential)
			{
				this.AddQuery(this.apiKeyName, credential.ApiKeyValue);
				return true;
			}

			// Token: 0x06004A29 RID: 18985 RVA: 0x00002139 File Offset: 0x00000339
			protected override bool Apply(WindowsCredential credential)
			{
				return true;
			}

			// Token: 0x06004A2A RID: 18986 RVA: 0x00002139 File Offset: 0x00000339
			protected override bool Apply(OAuthCredential credential)
			{
				return true;
			}

			// Token: 0x06004A2B RID: 18987 RVA: 0x00002139 File Offset: 0x00000339
			protected override bool Apply(SharedKeyAuthCredential credential)
			{
				return true;
			}

			// Token: 0x06004A2C RID: 18988 RVA: 0x000F6F30 File Offset: 0x000F5130
			protected override bool Apply(ParameterizedCredential credential)
			{
				if (credential.Name == "SAS")
				{
					string kind = this.resource.Kind;
					string text;
					if ((kind == "AzureBlobs" || kind == "AzureDataLakeStorage") && credential.Values.TryGetValue("Token", out text))
					{
						UriHelper.AddQueryRecord(this.builder, HttpUtility.ParseQueryString(text));
						return true;
					}
				}
				return false;
			}

			// Token: 0x06004A2D RID: 18989 RVA: 0x000F6F9D File Offset: 0x000F519D
			private void AddQuery(string key, string value)
			{
				this.builder.Query = UriHelper.AddQueryPart(UriHelper.NormalizeUriComponent(this.builder.Query), key, value);
			}

			// Token: 0x0400277B RID: 10107
			private string apiKeyName;

			// Token: 0x0400277C RID: 10108
			private UriBuilder builder;
		}

		// Token: 0x02000A63 RID: 2659
		private class WebRequestCredentialDispatcher : HttpResourceCredentialDispatcher
		{
			// Token: 0x06004A2E RID: 18990 RVA: 0x000F6FC1 File Offset: 0x000F51C1
			public WebRequestCredentialDispatcher(MashupHttpWebRequest request, IEngineHost hostEnvironment, string oAuthResource)
				: base(hostEnvironment)
			{
				this.request = request;
				this.oAuthResource = oAuthResource;
			}

			// Token: 0x06004A2F RID: 18991 RVA: 0x000F6FD8 File Offset: 0x000F51D8
			protected override bool Apply(BasicAuthCredential credential)
			{
				this.ApplyBasicAuthCredential(credential);
				return true;
			}

			// Token: 0x06004A30 RID: 18992 RVA: 0x00002139 File Offset: 0x00000339
			protected override bool Apply(WebApiKeyCredential credential)
			{
				return true;
			}

			// Token: 0x06004A31 RID: 18993 RVA: 0x000F6FE4 File Offset: 0x000F51E4
			protected override bool Apply(WindowsCredential credential)
			{
				ICredentials networkCredential = credential.GetNetworkCredential(this.hostEnvironment, this.resource);
				NetworkCredential networkCredential2 = networkCredential as NetworkCredential;
				if (networkCredential2 != null && !string.IsNullOrEmpty(networkCredential2.UserName))
				{
					Uri uri = new UriBuilder(this.request.RequestUri.Scheme, this.request.RequestUri.Host, this.request.RequestUri.Port).Uri;
					this.request.PreAuthenticate = false;
					CredentialCache credentialCache = new CredentialCache();
					credentialCache.Add(uri, "Negotiate", networkCredential2);
					credentialCache.Add(uri, "Kerberos", networkCredential2);
					credentialCache.Add(uri, "NTLM", networkCredential2);
					this.request.Credentials = credentialCache;
				}
				else
				{
					this.request.PreAuthenticate = true;
					this.request.Credentials = networkCredential;
				}
				return true;
			}

			// Token: 0x06004A32 RID: 18994 RVA: 0x000F70BC File Offset: 0x000F52BC
			protected override bool Apply(SharedKeyAuthCredential credential)
			{
				string text = DateTime.UtcNow.ToString("R", CultureInfo.InvariantCulture);
				this.request.Headers["x-ms-date"] = text;
				string absoluteUri = this.request.RequestUri.AbsoluteUri;
				string text2 = "GET\n\n\n" + text + "\n";
				string text3 = absoluteUri.Remove(0, absoluteUri.IndexOf('/') + 2).Split(new char[] { '.' })[0];
				string pathAndQuery = this.request.RequestUri.PathAndQuery;
				text2 = text2 + "/" + text3 + pathAndQuery.Split(new char[] { '?' })[0];
				byte[] bytes = Encoding.UTF8.GetBytes(text2);
				HMAC hmac = CryptoAlgorithmFactory.CreateHMACSHA256Algorithm();
				byte[] array;
				if (!Base64Encoding.TryFromBase64String(credential.SharedKey, out array))
				{
					throw DataSourceException.NewInvalidCredentialsError(this.hostEnvironment, this.resource, Strings.Binary_InvalidEncoding, null, null);
				}
				hmac.Key = array;
				string text4 = Convert.ToBase64String(hmac.ComputeHash(bytes));
				string text5 = string.Format(CultureInfo.InvariantCulture, "{0} {1}:{2}", "SharedKey", text3, text4);
				this.request.Headers[HttpRequestHeader.Authorization] = text5;
				return true;
			}

			// Token: 0x06004A33 RID: 18995 RVA: 0x000F71F4 File Offset: 0x000F53F4
			protected override bool Apply(OAuthCredential credential)
			{
				if (!this.ApplySharepointCredential(credential))
				{
					this.ApplyOAuthCredential(credential, false);
				}
				return true;
			}

			// Token: 0x06004A34 RID: 18996 RVA: 0x000F7208 File Offset: 0x000F5408
			private bool ApplySharepointCredential(OAuthCredential credential)
			{
				CookieCollection cookieCollection;
				if (!CookieHelper.TryDeserializeCookies(credential.AccessToken, out cookieCollection))
				{
					return false;
				}
				string text = null;
				foreach (object obj in cookieCollection)
				{
					Cookie cookie = (Cookie)obj;
					if (cookie.Name == "FedAuth")
					{
						text = cookie.Value;
					}
				}
				if (text != null)
				{
					if (this.request.CookieContainer == null)
					{
						this.request.CookieContainer = new CookieContainer();
					}
					string leftPart = this.request.RequestUri.GetLeftPart(UriPartial.Authority);
					CookieCollection cookieCollection2 = new CookieCollection();
					foreach (object obj2 in cookieCollection)
					{
						Cookie cookie2 = (Cookie)obj2;
						cookieCollection2.Add(new Cookie(cookie2.Name, cookie2.Value));
					}
					this.request.CookieContainer.Add(new Uri(leftPart), cookieCollection2);
					return true;
				}
				return false;
			}

			// Token: 0x06004A35 RID: 18997 RVA: 0x000F6FD8 File Offset: 0x000F51D8
			protected override bool Apply(FeedKeyCredential credential)
			{
				this.ApplyBasicAuthCredential(credential);
				return true;
			}

			// Token: 0x06004A36 RID: 18998 RVA: 0x000F7338 File Offset: 0x000F5538
			private void ApplyBasicAuthCredential(BasicAuthCredential credential)
			{
				this.request.PreAuthenticate = true;
				if (this.request.Headers[HttpRequestHeader.Authorization] == null)
				{
					string text = credential.Username + ":" + credential.Password;
					string text2 = Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
					string text3 = "Basic " + text2;
					this.request.Headers[HttpRequestHeader.Authorization] = text3;
				}
			}

			// Token: 0x06004A37 RID: 18999 RVA: 0x000F73AC File Offset: 0x000F55AC
			private void ApplyOAuthCredential(OAuthCredential credential, bool useBase64Encoding = false)
			{
				credential = credential.RefreshTokenAsNeeded(this.hostEnvironment, this.resource, false);
				string text = credential.AccessTokenForResource(this.oAuthResource);
				if (text == null)
				{
					throw DataSourceException.NewAccessAuthorizationError(base.Host, this.resource, null, null, null);
				}
				string text2 = "Bearer " + text;
				this.request.PreAuthenticate = true;
				this.request.Headers[HttpRequestHeader.Authorization] = text2;
			}

			// Token: 0x0400277D RID: 10109
			protected readonly MashupHttpWebRequest request;

			// Token: 0x0400277E RID: 10110
			private readonly string oAuthResource;
		}

		// Token: 0x02000A64 RID: 2660
		private class AzureStorageRequestCredentialDispatcher : HttpResourceCredentialDispatcher.WebRequestCredentialDispatcher
		{
			// Token: 0x06004A38 RID: 19000 RVA: 0x000F741E File Offset: 0x000F561E
			public AzureStorageRequestCredentialDispatcher(MashupHttpWebRequest request, IEngineHost hostEnvironment, string oAuthResource)
				: base(request, hostEnvironment, oAuthResource)
			{
			}

			// Token: 0x17001761 RID: 5985
			// (get) Token: 0x06004A39 RID: 19001 RVA: 0x000F7429 File Offset: 0x000F5629
			private Tracer Tracer
			{
				get
				{
					if (this.tracer == null)
					{
						this.tracer = new Tracer(this.hostEnvironment, "Engine/IO/Web/Request/Credential", this.resource, null, null);
					}
					return this.tracer;
				}
			}

			// Token: 0x06004A3A RID: 19002 RVA: 0x000F7458 File Offset: 0x000F5658
			protected override bool Apply(ResourceCredentialCollection credentials)
			{
				ConnectionStringPropertiesAdornment connectionStringPropertiesAdornment;
				return base.Apply(credentials) && (!credentials.TryGetCredential(out connectionStringPropertiesAdornment) || this.Apply(connectionStringPropertiesAdornment));
			}

			// Token: 0x06004A3B RID: 19003 RVA: 0x000F7483 File Offset: 0x000F5683
			protected override bool Apply(ParameterizedCredential credential)
			{
				return credential.Name == "SAS" && this.resource.Kind != "HDInsight";
			}

			// Token: 0x06004A3C RID: 19004 RVA: 0x000F74AE File Offset: 0x000F56AE
			protected override bool Apply(FeedKeyCredential credential)
			{
				this.ApplySharedKey(credential);
				return true;
			}

			// Token: 0x06004A3D RID: 19005 RVA: 0x000F74B8 File Offset: 0x000F56B8
			private void ApplySharedKey(FeedKeyCredential credential)
			{
				string text = DateTime.UtcNow.ToString("R", CultureInfo.InvariantCulture);
				this.request.Headers["x-ms-date"] = text;
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(this.request.Method + "\n");
				stringBuilder.Append("\n");
				stringBuilder.Append("\n");
				if (this.request.ContentLength > 0L)
				{
					stringBuilder.Append(this.request.ContentLength);
				}
				stringBuilder.Append("\n");
				stringBuilder.Append("\n");
				stringBuilder.Append("\n");
				stringBuilder.Append("\n");
				stringBuilder.Append("\n");
				stringBuilder.Append(this.request.Headers["If-Match"] ?? string.Empty);
				stringBuilder.Append("\n");
				stringBuilder.Append(this.request.Headers["If-None-Match"] ?? string.Empty);
				stringBuilder.Append("\n");
				stringBuilder.Append("\n");
				stringBuilder.Append(this.request.Headers["Range"] ?? string.Empty);
				stringBuilder.Append("\n");
				if (this.request.Headers.Count > 0)
				{
					string[] allKeys = this.request.Headers.AllKeys;
					foreach (string text2 in HttpResourceCredentialDispatcher.AzureStorageRequestCredentialDispatcher.SharedKeyHeaders)
					{
						if (allKeys.Contains(text2))
						{
							stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}:{1}\n", text2, this.request.Headers[text2]);
						}
					}
				}
				string text3 = this.request.RequestUri.Host.Split(new char[] { '.' })[0];
				stringBuilder.Append('/');
				stringBuilder.Append(text3);
				stringBuilder.Append(this.request.RequestUri.AbsolutePath);
				string query = this.request.RequestUri.Query;
				if (!string.IsNullOrEmpty(this.request.RequestUri.Query))
				{
					stringBuilder.Append("\n");
					NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(query);
					List<string> list = new List<string>(nameValueCollection.Keys.Cast<string>());
					list.Sort(StringComparer.OrdinalIgnoreCase);
					foreach (string text4 in list)
					{
						stringBuilder.Append(text4.ToLowerInvariant());
						stringBuilder.Append(':');
						stringBuilder.Append(nameValueCollection[text4]);
						stringBuilder.Append("\n");
					}
				}
				byte[] bytes = Encoding.UTF8.GetBytes(stringBuilder.ToString().TrimEnd(new char[] { '\n' }));
				HMAC hmac = CryptoAlgorithmFactory.CreateHMACSHA256Algorithm();
				byte[] array;
				if (!Base64Encoding.TryFromBase64String(credential.FeedKey, out array))
				{
					throw DataSourceException.NewInvalidCredentialsError(this.hostEnvironment, this.resource, Strings.Binary_InvalidEncoding, null, null);
				}
				hmac.Key = array;
				string text5 = "SharedKey " + text3 + ":" + Convert.ToBase64String(hmac.ComputeHash(bytes));
				this.request.Headers[HttpRequestHeader.Authorization] = text5;
			}

			// Token: 0x06004A3E RID: 19006 RVA: 0x000F7848 File Offset: 0x000F5A48
			protected sealed override bool Apply(ConnectionStringPropertiesAdornment credential)
			{
				string text;
				bool flag = credential.Properties.TryGetValue("CapacityId", out text) && !string.IsNullOrEmpty(text);
				string text2;
				bool flag2 = credential.Properties.TryGetValue("ServiceAccessToken", out text2) && !string.IsNullOrEmpty(text2);
				if (flag && flag2)
				{
					this.request.Headers["x-ms-caller-capacity-id"] = text;
					this.request.Headers["x-ms-src-capacity-id"] = text;
					this.request.Headers["x-ms-s2s-actor-authorization"] = text2;
					using (IHostTrace hostTrace = this.Tracer.CreateTrace("/PrivateLink", TraceEventType.Information))
					{
						hostTrace.Add("CapacityId", text, false);
						hostTrace.Add("ServiceTokenLength", text2.Length, false);
					}
					return true;
				}
				return !flag && !flag2;
			}

			// Token: 0x0400277F RID: 10111
			private static readonly string[] SharedKeyHeaders = new string[] { "x-ms-blob-content-type", "x-ms-blob-type", "x-ms-client-request-id", "x-ms-date", "x-ms-range", "x-ms-rename-source", "x-ms-source-if-match", "x-ms-version" };

			// Token: 0x04002780 RID: 10112
			private Tracer tracer;
		}
	}
}
