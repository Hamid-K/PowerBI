using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Instance;
using Microsoft.Identity.Client.Utils;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000181 RID: 385
	public class WwwAuthenticateParameters
	{
		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x0600127C RID: 4732 RVA: 0x0003EE48 File Offset: 0x0003D048
		// (set) Token: 0x0600127D RID: 4733 RVA: 0x0003EE50 File Offset: 0x0003D050
		[Obsolete("The client apps should know which App ID URI it requests scopes for.", true)]
		public string Resource { get; set; }

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x0600127E RID: 4734 RVA: 0x0003EE59 File Offset: 0x0003D059
		// (set) Token: 0x0600127F RID: 4735 RVA: 0x0003EE61 File Offset: 0x0003D061
		[Obsolete("The client apps should know which scopes to request for.", true)]
		public IEnumerable<string> Scopes { get; set; }

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06001280 RID: 4736 RVA: 0x0003EE6A File Offset: 0x0003D06A
		// (set) Token: 0x06001281 RID: 4737 RVA: 0x0003EE72 File Offset: 0x0003D072
		public string Authority { get; set; }

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06001282 RID: 4738 RVA: 0x0003EE7B File Offset: 0x0003D07B
		// (set) Token: 0x06001283 RID: 4739 RVA: 0x0003EE83 File Offset: 0x0003D083
		public string Claims { get; set; }

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06001284 RID: 4740 RVA: 0x0003EE8C File Offset: 0x0003D08C
		// (set) Token: 0x06001285 RID: 4741 RVA: 0x0003EE94 File Offset: 0x0003D094
		public string Error { get; set; }

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06001286 RID: 4742 RVA: 0x0003EE9D File Offset: 0x0003D09D
		// (set) Token: 0x06001287 RID: 4743 RVA: 0x0003EEA5 File Offset: 0x0003D0A5
		public string AuthenticationScheme { get; private set; }

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06001288 RID: 4744 RVA: 0x0003EEAE File Offset: 0x0003D0AE
		// (set) Token: 0x06001289 RID: 4745 RVA: 0x0003EEB6 File Offset: 0x0003D0B6
		public string Nonce { get; private set; }

		// Token: 0x170003CE RID: 974
		public string this[string key]
		{
			get
			{
				return this.RawParameters[key];
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x0600128B RID: 4747 RVA: 0x0003EECD File Offset: 0x0003D0CD
		// (set) Token: 0x0600128C RID: 4748 RVA: 0x0003EED5 File Offset: 0x0003D0D5
		internal IDictionary<string, string> RawParameters { get; private set; }

		// Token: 0x0600128D RID: 4749 RVA: 0x0003EEDE File Offset: 0x0003D0DE
		public string GetTenantId()
		{
			Authority authority = Microsoft.Identity.Client.Instance.Authority.CreateAuthority(this.Authority, true);
			if (authority == null)
			{
				return null;
			}
			return authority.TenantId;
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x0003EEF8 File Offset: 0x0003D0F8
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This api is now obsolete and has been replaced with CreateFromAuthenticationResponseAsync(...)")]
		public static Task<WwwAuthenticateParameters> CreateFromResourceResponseAsync(string resourceUri)
		{
			return WwwAuthenticateParameters.CreateFromResourceResponseAsync(resourceUri, default(CancellationToken));
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x0003EF14 File Offset: 0x0003D114
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This api is now obsolete and has been replaced with CreateFromAuthenticationResponseAsync(...)")]
		public static Task<WwwAuthenticateParameters> CreateFromResourceResponseAsync(string resourceUri, CancellationToken cancellationToken = default(CancellationToken))
		{
			return WwwAuthenticateParameters.CreateFromResourceResponseAsync(AuthenticationHeaderParser.GetHttpClient(), resourceUri, cancellationToken);
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x0003EF24 File Offset: 0x0003D124
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This api is now obsolete and has been replaced with replaced with CreateFromAuthenticationResponseAsync(HttpResponseHeaders, string)")]
		public static async Task<WwwAuthenticateParameters> CreateFromResourceResponseAsync(HttpClient httpClient, string resourceUri, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (httpClient == null)
			{
				throw new ArgumentNullException("httpClient");
			}
			if (string.IsNullOrWhiteSpace(resourceUri))
			{
				throw new ArgumentNullException("resourceUri");
			}
			return WwwAuthenticateParameters.CreateFromResponseHeaders((await httpClient.GetAsync(resourceUri, cancellationToken).ConfigureAwait(false)).Headers, "Bearer");
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x0003EF78 File Offset: 0x0003D178
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This api is now obsolete and has been replaced with CreateFromAuthenticationHeaders(...)")]
		public static WwwAuthenticateParameters CreateFromResponseHeaders(HttpResponseHeaders httpResponseHeaders, string scheme = "Bearer")
		{
			if (httpResponseHeaders.WwwAuthenticate.Any<AuthenticationHeaderValue>())
			{
				AuthenticationHeaderValue authenticationHeaderValue = httpResponseHeaders.WwwAuthenticate.FirstOrDefault((AuthenticationHeaderValue v) => string.Equals(v.Scheme, scheme, StringComparison.OrdinalIgnoreCase));
				if (authenticationHeaderValue != null)
				{
					WwwAuthenticateParameters wwwAuthenticateParameters = WwwAuthenticateParameters.CreateFromWwwAuthenticateHeaderValue(authenticationHeaderValue.Parameter);
					wwwAuthenticateParameters.AuthenticationScheme = scheme;
					return wwwAuthenticateParameters;
				}
			}
			return WwwAuthenticateParameters.CreateWwwAuthenticateParameters(new Dictionary<string, string>(), string.Empty);
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x0003EFE1 File Offset: 0x0003D1E1
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This api is now obsolete and should not be used.")]
		public static WwwAuthenticateParameters CreateFromWwwAuthenticateHeaderValue(string wwwAuthenticateValue)
		{
			return WwwAuthenticateParameters.CreateFromWwwAuthenticationHeaderValue(wwwAuthenticateValue, string.Empty);
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x0003EFEE File Offset: 0x0003D1EE
		public static Task<WwwAuthenticateParameters> CreateFromAuthenticationResponseAsync(string resourceUri, string scheme, CancellationToken cancellationToken = default(CancellationToken))
		{
			return WwwAuthenticateParameters.CreateFromAuthenticationResponseAsync(resourceUri, scheme, AuthenticationHeaderParser.GetHttpClient(), cancellationToken);
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x0003F000 File Offset: 0x0003D200
		public static async Task<WwwAuthenticateParameters> CreateFromAuthenticationResponseAsync(string resourceUri, string scheme, HttpClient httpClient, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (httpClient == null)
			{
				throw new ArgumentNullException("httpClient");
			}
			if (string.IsNullOrWhiteSpace(resourceUri))
			{
				throw new ArgumentNullException("resourceUri");
			}
			return WwwAuthenticateParameters.CreateFromAuthenticationHeaders((await httpClient.GetAsync(resourceUri, cancellationToken).ConfigureAwait(false)).Headers, scheme);
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x0003F05C File Offset: 0x0003D25C
		public static WwwAuthenticateParameters CreateFromAuthenticationHeaders(HttpResponseHeaders httpResponseHeaders, string scheme)
		{
			AuthenticationHeaderValue authenticationHeaderValue = httpResponseHeaders.WwwAuthenticate.FirstOrDefault((AuthenticationHeaderValue v) => string.Equals(v.Scheme, scheme, StringComparison.OrdinalIgnoreCase));
			if (authenticationHeaderValue != null)
			{
				string parameter = authenticationHeaderValue.Parameter;
				try
				{
					return WwwAuthenticateParameters.CreateFromWwwAuthenticationHeaderValue(parameter, scheme);
				}
				catch (Exception ex)
				{
					if (ex is MsalException)
					{
						throw;
					}
					throw new MsalClientException("unable_to_parse_authentication_header", "MSAL is unable to parse the authentication header returned from the resource endpoint. This can be a result of a malformed header returned in either the WWW-Authenticate or the Authentication-Info collections acquired from the provided endpoint." + string.Format("Response Headers: {0} See inner exception for details.", httpResponseHeaders), ex);
				}
			}
			return WwwAuthenticateParameters.CreateWwwAuthenticateParameters(new Dictionary<string, string>(), string.Empty);
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x0003F0F8 File Offset: 0x0003D2F8
		public static Task<IReadOnlyList<WwwAuthenticateParameters>> CreateFromAuthenticationResponseAsync(string resourceUri, CancellationToken cancellationToken = default(CancellationToken))
		{
			return WwwAuthenticateParameters.CreateFromAuthenticationResponseAsync(resourceUri, AuthenticationHeaderParser.GetHttpClient(), cancellationToken);
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x0003F108 File Offset: 0x0003D308
		public static async Task<IReadOnlyList<WwwAuthenticateParameters>> CreateFromAuthenticationResponseAsync(string resourceUri, HttpClient httpClient, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (httpClient == null)
			{
				throw new ArgumentNullException("httpClient");
			}
			if (string.IsNullOrWhiteSpace(resourceUri))
			{
				throw new ArgumentNullException("resourceUri");
			}
			return WwwAuthenticateParameters.CreateFromAuthenticationHeaders((await httpClient.GetAsync(resourceUri, cancellationToken).ConfigureAwait(false)).Headers);
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x0003F15C File Offset: 0x0003D35C
		public static IReadOnlyList<WwwAuthenticateParameters> CreateFromAuthenticationHeaders(HttpResponseHeaders httpResponseHeaders)
		{
			List<WwwAuthenticateParameters> list = new List<WwwAuthenticateParameters>();
			foreach (AuthenticationHeaderValue authenticationHeaderValue in httpResponseHeaders.WwwAuthenticate)
			{
				try
				{
					WwwAuthenticateParameters wwwAuthenticateParameters = WwwAuthenticateParameters.CreateFromWwwAuthenticationHeaderValue(authenticationHeaderValue.Parameter, authenticationHeaderValue.Scheme);
					list.Add(wwwAuthenticateParameters);
				}
				catch (Exception ex) when (!(ex is MsalException))
				{
					throw new MsalClientException("unable_to_parse_authentication_header", "MSAL is unable to parse the authentication header returned from the resource endpoint. This can be a result of a malformed header returned in either the WWW-Authenticate or the Authentication-Info collections acquired from the provided endpoint. See inner exception for details.", ex);
				}
			}
			return list;
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x0003F204 File Offset: 0x0003D404
		public static string GetClaimChallengeFromResponseHeaders(HttpResponseHeaders httpResponseHeaders, string scheme = "Bearer")
		{
			WwwAuthenticateParameters wwwAuthenticateParameters = WwwAuthenticateParameters.CreateFromAuthenticationHeaders(httpResponseHeaders, scheme);
			if (wwwAuthenticateParameters.Claims != null && string.Equals(wwwAuthenticateParameters.Error, "insufficient_claims", StringComparison.OrdinalIgnoreCase))
			{
				return wwwAuthenticateParameters.Claims;
			}
			return null;
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x0003F23C File Offset: 0x0003D43C
		private static WwwAuthenticateParameters CreateFromWwwAuthenticationHeaderValue(string wwwAuthenticateValue, string scheme)
		{
			IDictionary<string, string> dictionary = null;
			if (!string.IsNullOrWhiteSpace(wwwAuthenticateValue))
			{
				dictionary = (from v in WwwAuthenticateParameters.GetParsedAuthValueElements(wwwAuthenticateValue)
					select AuthenticationHeaderParser.CreateKeyValuePair(v.Trim(), scheme)).ToDictionary((KeyValuePair<string, string> pair) => pair.Key, (KeyValuePair<string, string> pair) => pair.Value, StringComparer.OrdinalIgnoreCase);
			}
			return WwwAuthenticateParameters.CreateWwwAuthenticateParameters(dictionary, scheme);
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x0003F2CC File Offset: 0x0003D4CC
		private static IEnumerable<string> GetParsedAuthValueElements(string wwwAuthenticateValue)
		{
			char[] charsToTrim = new char[] { ',', ' ' };
			IReadOnlyList<string> readOnlyList = CoreHelpers.SplitWithQuotes(wwwAuthenticateValue, ' ');
			if (WwwAuthenticateParameters.s_knownAuthenticationSchemes.Contains(readOnlyList[0]))
			{
				readOnlyList = readOnlyList.Skip(1).ToList<string>();
			}
			return readOnlyList.Select((string authValue) => authValue.TrimEnd(charsToTrim));
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x0003F330 File Offset: 0x0003D530
		internal static WwwAuthenticateParameters CreateWwwAuthenticateParameters(IDictionary<string, string> values, string scheme)
		{
			WwwAuthenticateParameters wwwAuthenticateParameters = new WwwAuthenticateParameters();
			wwwAuthenticateParameters.AuthenticationScheme = scheme;
			if (values == null)
			{
				wwwAuthenticateParameters.RawParameters = new Dictionary<string, string>();
				return wwwAuthenticateParameters;
			}
			wwwAuthenticateParameters.RawParameters = values;
			string text;
			if (values.TryGetValue("authorization_uri", out text))
			{
				wwwAuthenticateParameters.Authority = text.Replace("/v2.0", string.Empty).Replace("/oauth2/authorize", string.Empty);
			}
			if (string.IsNullOrEmpty(wwwAuthenticateParameters.Authority) && values.TryGetValue("authorization", out text))
			{
				wwwAuthenticateParameters.Authority = text.Replace("/v2.0", string.Empty).Replace("/oauth2/authorize", string.Empty);
			}
			if (string.IsNullOrEmpty(wwwAuthenticateParameters.Authority) && values.TryGetValue("authority", out text))
			{
				wwwAuthenticateParameters.Authority = text.TrimEnd(new char[] { '/' });
			}
			if (values.TryGetValue("claims", out text))
			{
				wwwAuthenticateParameters.Claims = WwwAuthenticateParameters.GetJsonFragment(text);
			}
			if (values.TryGetValue("error", out text))
			{
				wwwAuthenticateParameters.Error = text;
			}
			if (values.TryGetValue("nonce", out text))
			{
				wwwAuthenticateParameters.Nonce = text;
			}
			return wwwAuthenticateParameters;
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x0003F454 File Offset: 0x0003D654
		private static string GetJsonFragment(string inputString)
		{
			if (!string.IsNullOrEmpty(inputString) && inputString.Length % 4 == 0)
			{
				Func<char, bool> func;
				if ((func = WwwAuthenticateParameters.<>O.<0>__IsWhiteSpace) == null)
				{
					func = (WwwAuthenticateParameters.<>O.<0>__IsWhiteSpace = new Func<char, bool>(char.IsWhiteSpace));
				}
				if (!inputString.Any(func))
				{
					string text;
					try
					{
						byte[] array = Convert.FromBase64String(inputString);
						text = Encoding.UTF8.GetString(array);
					}
					catch
					{
						text = inputString;
					}
					return text;
				}
			}
			return inputString;
		}

		// Token: 0x040006E6 RID: 1766
		private static readonly ISet<string> s_knownAuthenticationSchemes = new HashSet<string>(new string[] { "Bearer", "PoP" }, StringComparer.OrdinalIgnoreCase);

		// Token: 0x02000403 RID: 1027
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040011E0 RID: 4576
			public static Func<char, bool> <0>__IsWhiteSpace;
		}
	}
}
