using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using Microsoft.Identity.Client.Utils;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000144 RID: 324
	public class AuthenticationInfoParameters
	{
		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06001025 RID: 4133 RVA: 0x0003AA18 File Offset: 0x00038C18
		// (set) Token: 0x06001026 RID: 4134 RVA: 0x0003AA20 File Offset: 0x00038C20
		public string NextNonce { get; private set; }

		// Token: 0x1700032C RID: 812
		public string this[string key]
		{
			get
			{
				return this.RawParameters[key];
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06001028 RID: 4136 RVA: 0x0003AA37 File Offset: 0x00038C37
		// (set) Token: 0x06001029 RID: 4137 RVA: 0x0003AA3F File Offset: 0x00038C3F
		internal IDictionary<string, string> RawParameters { get; private set; }

		// Token: 0x0600102A RID: 4138 RVA: 0x0003AA48 File Offset: 0x00038C48
		public static AuthenticationInfoParameters CreateFromResponseHeaders(HttpResponseHeaders httpResponseHeaders)
		{
			AuthenticationInfoParameters authenticationInfoParameters = new AuthenticationInfoParameters();
			AuthenticationInfoParameters authenticationInfoParameters2;
			try
			{
				IEnumerable<string> value = httpResponseHeaders.SingleOrDefault((KeyValuePair<string, IEnumerable<string>> header) => header.Key == "Authentication-Info").Value;
				if (value != null)
				{
					string text = value.FirstOrDefault<string>();
					string[] array = text.Split(new char[] { ' ' }, 2);
					IDictionary<string, string> dictionary;
					if (array.Length != 2)
					{
						dictionary = new Dictionary<string, string>();
						dictionary.Add(new KeyValuePair<string, string>("Authentication-Info", text));
					}
					else
					{
						dictionary = (from v in CoreHelpers.SplitWithQuotes(array[1], ',')
							select AuthenticationHeaderParser.CreateKeyValuePair(v.Trim(), "Authentication-Info")).ToDictionary((KeyValuePair<string, string> pair) => pair.Key, (KeyValuePair<string, string> pair) => pair.Value, StringComparer.OrdinalIgnoreCase);
						string text2;
						if (dictionary.TryGetValue("nextnonce", out text2))
						{
							authenticationInfoParameters.NextNonce = text2;
						}
					}
					authenticationInfoParameters.RawParameters = dictionary;
				}
				authenticationInfoParameters2 = authenticationInfoParameters;
			}
			catch (Exception ex) when (!(ex is MsalClientException))
			{
				throw new MsalClientException("unable_to_parse_authentication_header", string.Format("{0}Response Headers: {1} See inner exception for details.", "MSAL is unable to parse the authentication header returned from the resource endpoint. This can be a result of a malformed header returned in either the WWW-Authenticate or the Authentication-Info collections acquired from the provided endpoint.", httpResponseHeaders), ex);
			}
			return authenticationInfoParameters2;
		}

		// Token: 0x040004DF RID: 1247
		private const string AuthenticationInfoKey = "Authentication-Info";
	}
}
