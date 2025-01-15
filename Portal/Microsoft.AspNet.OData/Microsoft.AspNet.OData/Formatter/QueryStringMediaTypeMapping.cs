using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData.Formatter
{
	// Token: 0x02000180 RID: 384
	public class QueryStringMediaTypeMapping : MediaTypeMapping
	{
		// Token: 0x06000CBD RID: 3261 RVA: 0x00032249 File Offset: 0x00030449
		public QueryStringMediaTypeMapping(string queryStringParameterName, MediaTypeHeaderValue mediaType)
			: base(mediaType)
		{
			if (queryStringParameterName == null)
			{
				throw Error.ArgumentNull("queryStringParameterName");
			}
			this.QueryStringParameterName = queryStringParameterName;
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x00032268 File Offset: 0x00030468
		public override double TryMatchMediaType(HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			FormDataCollection queryString = QueryStringMediaTypeMapping.GetQueryString(request.RequestUri);
			return (double)(this.DoesQueryStringMatch(queryString) ? 1 : 0);
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x0003229D File Offset: 0x0003049D
		private static FormDataCollection GetQueryString(Uri uri)
		{
			if (uri == null)
			{
				throw Error.InvalidOperation(SRResources.NonNullUriRequiredForMediaTypeMapping, new object[] { typeof(QueryStringMediaTypeMapping).Name });
			}
			return new FormDataCollection(uri);
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x000322D1 File Offset: 0x000304D1
		public QueryStringMediaTypeMapping(string queryStringParameterName, string mediaType)
			: base(mediaType)
		{
			if (queryStringParameterName == null)
			{
				throw Error.ArgumentNull("queryStringParameterName");
			}
			this.QueryStringParameterName = queryStringParameterName;
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000CC1 RID: 3265 RVA: 0x000322EF File Offset: 0x000304EF
		// (set) Token: 0x06000CC2 RID: 3266 RVA: 0x000322F7 File Offset: 0x000304F7
		public string QueryStringParameterName { get; private set; }

		// Token: 0x06000CC3 RID: 3267 RVA: 0x00032300 File Offset: 0x00030500
		private bool DoesQueryStringMatch(IEnumerable<KeyValuePair<string, string>> queryString)
		{
			if (queryString != null)
			{
				string value = queryString.Where((KeyValuePair<string, string> kvp) => kvp.Key == this.QueryStringParameterName).FirstOrDefault<KeyValuePair<string, string>>().Value;
				MediaTypeHeaderValue mediaTypeHeaderValue;
				if (value != null && MediaTypeHeaderValue.TryParse(value, out mediaTypeHeaderValue) && base.MediaType.Equals(mediaTypeHeaderValue))
				{
					return true;
				}
			}
			return false;
		}
	}
}
