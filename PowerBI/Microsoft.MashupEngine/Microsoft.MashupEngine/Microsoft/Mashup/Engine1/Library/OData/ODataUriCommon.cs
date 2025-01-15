using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Text;
using System.Web;
using Microsoft.Mashup.Engine1.Library.Uris;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData
{
	// Token: 0x02000762 RID: 1890
	internal static class ODataUriCommon
	{
		// Token: 0x060037A6 RID: 14246 RVA: 0x000B2074 File Offset: 0x000B0274
		public static Uri AddCount(Uri uri)
		{
			UriBuilder uriBuilder = new UriBuilder(uri);
			uriBuilder.Path = ((uriBuilder.Path != null) ? uriBuilder.Path : string.Empty) + "/$count";
			return ODataUriNormalizer.Normalize(uriBuilder.Uri);
		}

		// Token: 0x060037A7 RID: 14247 RVA: 0x000B20B8 File Offset: 0x000B02B8
		public static Uri ConvertToUri(TextValue uriValue)
		{
			return ODataUriNormalizer.Normalize(UriHelper.CreateAbsoluteUriFromValue(uriValue));
		}

		// Token: 0x060037A8 RID: 14248 RVA: 0x000B20C8 File Offset: 0x000B02C8
		public static Uri RemoveSkipAndTake(Uri baseUri, out int skipCount, out int? takeCount)
		{
			bool flag = false;
			NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(baseUri.Query);
			string[] values = nameValueCollection.GetValues("$skip");
			if (values == null)
			{
				skipCount = 0;
			}
			else
			{
				flag = true;
				skipCount = int.Parse(values[0], CultureInfo.InvariantCulture);
				nameValueCollection.Remove("$skip");
			}
			string[] values2 = nameValueCollection.GetValues("$top");
			if (values2 == null)
			{
				takeCount = null;
			}
			else
			{
				flag = true;
				takeCount = new int?(int.Parse(values2[0], CultureInfo.InvariantCulture));
				nameValueCollection.Remove("$top");
			}
			if (flag)
			{
				return new UriBuilder(baseUri)
				{
					Query = ODataUriCommon.NameValueToString(nameValueCollection)
				}.Uri;
			}
			return baseUri;
		}

		// Token: 0x060037A9 RID: 14249 RVA: 0x000B216C File Offset: 0x000B036C
		public static string NameValueToString(NameValueCollection queries)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			for (int i = 0; i < queries.Count; i++)
			{
				string text = Uri.EscapeDataString(queries.GetKey(i));
				string[] values = queries.GetValues(i);
				if (values != null && values.Length != 0)
				{
					for (int j = 0; j < values.Length; j++)
					{
						if (!flag)
						{
							stringBuilder.Append("&");
						}
						flag = false;
						stringBuilder.Append(text);
						stringBuilder.Append("=");
						stringBuilder.Append(Uri.EscapeDataString(values[j]));
					}
				}
				else
				{
					if (!flag)
					{
						stringBuilder.Append("&");
					}
					flag = false;
					stringBuilder.Append(text);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060037AA RID: 14250 RVA: 0x000B2224 File Offset: 0x000B0424
		public static Uri RemovePathAndQuery(Uri uri)
		{
			UriBuilder uriBuilder = new UriBuilder(uri);
			return new UriBuilder(uriBuilder.Scheme, uriBuilder.Host, uriBuilder.Port).Uri;
		}

		// Token: 0x060037AB RID: 14251 RVA: 0x000B2254 File Offset: 0x000B0454
		public static void ValidateHttpAbsolute(Uri uri)
		{
			if (!uri.IsAbsoluteUri || (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
			{
				throw ODataCommonErrors.InvalidServiceUri(uri);
			}
		}
	}
}
