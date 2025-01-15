using System;
using System.Collections.Specialized;
using System.Net.Http.Headers;
using System.Net.Http.Properties;
using System.Web.Http;

namespace System.Net.Http.Formatting
{
	// Token: 0x0200004A RID: 74
	public class QueryStringMapping : MediaTypeMapping
	{
		// Token: 0x060002DB RID: 731 RVA: 0x00009DCD File Offset: 0x00007FCD
		public QueryStringMapping(string queryStringParameterName, string queryStringParameterValue, string mediaType)
			: base(mediaType)
		{
			this.Initialize(queryStringParameterName, queryStringParameterValue);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00009DDE File Offset: 0x00007FDE
		public QueryStringMapping(string queryStringParameterName, string queryStringParameterValue, MediaTypeHeaderValue mediaType)
			: base(mediaType)
		{
			this.Initialize(queryStringParameterName, queryStringParameterValue);
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002DD RID: 733 RVA: 0x00009DEF File Offset: 0x00007FEF
		// (set) Token: 0x060002DE RID: 734 RVA: 0x00009DF7 File Offset: 0x00007FF7
		public string QueryStringParameterName { get; private set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002DF RID: 735 RVA: 0x00009E00 File Offset: 0x00008000
		// (set) Token: 0x060002E0 RID: 736 RVA: 0x00009E08 File Offset: 0x00008008
		public string QueryStringParameterValue { get; private set; }

		// Token: 0x060002E1 RID: 737 RVA: 0x00009E14 File Offset: 0x00008014
		public override double TryMatchMediaType(HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			NameValueCollection queryString = QueryStringMapping.GetQueryString(request.RequestUri);
			if (!this.DoesQueryStringMatch(queryString))
			{
				return 0.0;
			}
			return 1.0;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00009E57 File Offset: 0x00008057
		private static NameValueCollection GetQueryString(Uri uri)
		{
			if (uri == null)
			{
				throw Error.InvalidOperation(Resources.NonNullUriRequiredForMediaTypeMapping, new object[] { QueryStringMapping._queryStringMappingType.Name });
			}
			return new FormDataCollection(uri).ReadAsNameValueCollection();
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00009E8B File Offset: 0x0000808B
		private void Initialize(string queryStringParameterName, string queryStringParameterValue)
		{
			if (string.IsNullOrWhiteSpace(queryStringParameterName))
			{
				throw Error.ArgumentNull("queryStringParameterName");
			}
			if (string.IsNullOrWhiteSpace(queryStringParameterValue))
			{
				throw Error.ArgumentNull("queryStringParameterValue");
			}
			this.QueryStringParameterName = queryStringParameterName.Trim();
			this.QueryStringParameterValue = queryStringParameterValue.Trim();
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00009ECC File Offset: 0x000080CC
		private bool DoesQueryStringMatch(NameValueCollection queryString)
		{
			if (queryString != null)
			{
				foreach (string text in queryString.AllKeys)
				{
					if (string.Equals(text, this.QueryStringParameterName, StringComparison.OrdinalIgnoreCase) && string.Equals(queryString[text], this.QueryStringParameterValue, StringComparison.OrdinalIgnoreCase))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x040000D6 RID: 214
		private static readonly Type _queryStringMappingType = typeof(QueryStringMapping);
	}
}
