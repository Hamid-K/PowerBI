using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.PlatformsCommon.Factories;
using Microsoft.Identity.Client.Utils;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000143 RID: 323
	public class AuthenticationHeaderParser
	{
		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06001018 RID: 4120 RVA: 0x0003A85D File Offset: 0x00038A5D
		// (set) Token: 0x06001019 RID: 4121 RVA: 0x0003A865 File Offset: 0x00038A65
		public IReadOnlyList<WwwAuthenticateParameters> WwwAuthenticateParameters { get; private set; }

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x0600101A RID: 4122 RVA: 0x0003A86E File Offset: 0x00038A6E
		// (set) Token: 0x0600101B RID: 4123 RVA: 0x0003A876 File Offset: 0x00038A76
		public AuthenticationInfoParameters AuthenticationInfoParameters { get; private set; }

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x0600101C RID: 4124 RVA: 0x0003A87F File Offset: 0x00038A7F
		// (set) Token: 0x0600101D RID: 4125 RVA: 0x0003A887 File Offset: 0x00038A87
		public string PopNonce { get; private set; }

		// Token: 0x0600101E RID: 4126 RVA: 0x0003A890 File Offset: 0x00038A90
		public static Task<AuthenticationHeaderParser> ParseAuthenticationHeadersAsync(string resourceUri, CancellationToken cancellationToken = default(CancellationToken))
		{
			return AuthenticationHeaderParser.ParseAuthenticationHeadersAsync(resourceUri, AuthenticationHeaderParser.GetHttpClient(), cancellationToken);
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x0003A8A0 File Offset: 0x00038AA0
		public static async Task<AuthenticationHeaderParser> ParseAuthenticationHeadersAsync(string resourceUri, HttpClient httpClient, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (httpClient == null)
			{
				throw new ArgumentNullException("httpClient");
			}
			if (string.IsNullOrWhiteSpace(resourceUri))
			{
				throw new ArgumentNullException("resourceUri");
			}
			return AuthenticationHeaderParser.ParseAuthenticationHeaders((await httpClient.GetAsync(resourceUri, cancellationToken).ConfigureAwait(false)).Headers);
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x0003A8F4 File Offset: 0x00038AF4
		public static AuthenticationHeaderParser ParseAuthenticationHeaders(HttpResponseHeaders httpResponseHeaders)
		{
			AuthenticationHeaderParser authenticationHeaderParser = new AuthenticationHeaderParser();
			AuthenticationInfoParameters authenticationInfoParameters = new AuthenticationInfoParameters();
			string text = null;
			if (httpResponseHeaders.WwwAuthenticate.Count != 0)
			{
				IReadOnlyList<WwwAuthenticateParameters> readOnlyList = Microsoft.Identity.Client.WwwAuthenticateParameters.CreateFromAuthenticationHeaders(httpResponseHeaders);
				WwwAuthenticateParameters wwwAuthenticateParameters = readOnlyList.SingleOrDefault((WwwAuthenticateParameters parameter) => string.Equals(parameter.AuthenticationScheme, "PoP", StringComparison.Ordinal));
				text = ((wwwAuthenticateParameters != null) ? wwwAuthenticateParameters.Nonce : null);
				authenticationHeaderParser.WwwAuthenticateParameters = readOnlyList;
			}
			else
			{
				authenticationHeaderParser.WwwAuthenticateParameters = new List<WwwAuthenticateParameters>();
				authenticationInfoParameters = AuthenticationInfoParameters.CreateFromResponseHeaders(httpResponseHeaders);
				authenticationHeaderParser.AuthenticationInfoParameters = authenticationInfoParameters;
			}
			authenticationHeaderParser.PopNonce = text ?? authenticationInfoParameters.NextNonce;
			return authenticationHeaderParser;
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x0003A989 File Offset: 0x00038B89
		internal static HttpClient GetHttpClient()
		{
			return AuthenticationHeaderParser._httpClientFactory.Value.GetHttpClient();
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x0003A99C File Offset: 0x00038B9C
		internal static KeyValuePair<string, string> CreateKeyValuePair(string paramValue, string authScheme)
		{
			string[] array = (from s in CoreHelpers.SplitWithQuotes(paramValue, '=')
				select s.Trim().Trim(new char[] { '"' })).ToArray<string>();
			if (array.Length < 2)
			{
				return new KeyValuePair<string, string>(authScheme, paramValue);
			}
			return new KeyValuePair<string, string>(array[0], array[1]);
		}

		// Token: 0x040004DB RID: 1243
		private static readonly Lazy<IMsalHttpClientFactory> _httpClientFactory = new Lazy<IMsalHttpClientFactory>(() => PlatformProxyFactory.CreatePlatformProxy(null).CreateDefaultHttpClientFactory());
	}
}
