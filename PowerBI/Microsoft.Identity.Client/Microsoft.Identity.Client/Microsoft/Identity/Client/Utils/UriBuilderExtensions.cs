using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.Identity.Client.Utils
{
	// Token: 0x020001D5 RID: 469
	internal static class UriBuilderExtensions
	{
		// Token: 0x0600146F RID: 5231 RVA: 0x0004567C File Offset: 0x0004387C
		public static void AppendQueryParameters(this UriBuilder builder, string queryParams)
		{
			if (builder == null || string.IsNullOrEmpty(queryParams))
			{
				return;
			}
			if (builder.Query.Length > 1)
			{
				builder.Query = builder.Query.Substring(1) + "&" + queryParams;
				return;
			}
			builder.Query = queryParams;
		}

		// Token: 0x06001470 RID: 5232 RVA: 0x000456C8 File Offset: 0x000438C8
		public static void AppendQueryParameters(this UriBuilder builder, IDictionary<string, string> queryParams)
		{
			List<string> list = new List<string>();
			foreach (KeyValuePair<string, string> keyValuePair in queryParams)
			{
				list.Add(keyValuePair.Key + "=" + keyValuePair.Value);
			}
			builder.AppendQueryParameters(string.Join("&", list));
		}

		// Token: 0x06001471 RID: 5233 RVA: 0x00045740 File Offset: 0x00043940
		public static void AppendOrReplaceQueryParameter(this UriBuilder builder, string key, string value)
		{
			if (builder == null || string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
			{
				return;
			}
			Dictionary<string, string> dictionary = CoreHelpers.ParseKeyValueList(builder.Query.Substring(1), '&', true, null);
			dictionary[key] = value;
			builder.Query = dictionary.ToQueryParameter();
		}

		// Token: 0x06001472 RID: 5234 RVA: 0x0004578C File Offset: 0x0004398C
		public static string GetHttpsUriWithOptionalPort(string host, string tenant, string path, int port)
		{
			UriBuilder uriBuilder = new UriBuilder("https", host);
			uriBuilder.Path = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", tenant, path);
			if (port != 443)
			{
				uriBuilder.Port = port;
			}
			return uriBuilder.Uri.AbsoluteUri;
		}

		// Token: 0x06001473 RID: 5235 RVA: 0x000457D6 File Offset: 0x000439D6
		public static string GetHttpsUriWithOptionalPort(string uri, int port)
		{
			if (port != 443)
			{
				return new UriBuilder(uri)
				{
					Port = port
				}.Uri.AbsoluteUri;
			}
			return uri;
		}

		// Token: 0x04000856 RID: 2134
		private const int DefaultHttpsPort = 443;
	}
}
