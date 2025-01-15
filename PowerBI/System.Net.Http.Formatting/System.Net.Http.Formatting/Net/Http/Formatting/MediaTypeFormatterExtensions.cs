using System;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Web.Http;

namespace System.Net.Http.Formatting
{
	// Token: 0x02000046 RID: 70
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class MediaTypeFormatterExtensions
	{
		// Token: 0x060002C8 RID: 712 RVA: 0x00009A70 File Offset: 0x00007C70
		public static void AddQueryStringMapping(this MediaTypeFormatter formatter, string queryStringParameterName, string queryStringParameterValue, MediaTypeHeaderValue mediaType)
		{
			if (formatter == null)
			{
				throw Error.ArgumentNull("formatter");
			}
			QueryStringMapping queryStringMapping = new QueryStringMapping(queryStringParameterName, queryStringParameterValue, mediaType);
			formatter.MediaTypeMappings.Add(queryStringMapping);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00009AA0 File Offset: 0x00007CA0
		public static void AddQueryStringMapping(this MediaTypeFormatter formatter, string queryStringParameterName, string queryStringParameterValue, string mediaType)
		{
			if (formatter == null)
			{
				throw Error.ArgumentNull("formatter");
			}
			QueryStringMapping queryStringMapping = new QueryStringMapping(queryStringParameterName, queryStringParameterValue, mediaType);
			formatter.MediaTypeMappings.Add(queryStringMapping);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00009AD0 File Offset: 0x00007CD0
		public static void AddRequestHeaderMapping(this MediaTypeFormatter formatter, string headerName, string headerValue, StringComparison valueComparison, bool isValueSubstring, MediaTypeHeaderValue mediaType)
		{
			if (formatter == null)
			{
				throw Error.ArgumentNull("formatter");
			}
			RequestHeaderMapping requestHeaderMapping = new RequestHeaderMapping(headerName, headerValue, valueComparison, isValueSubstring, mediaType);
			formatter.MediaTypeMappings.Add(requestHeaderMapping);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00009B04 File Offset: 0x00007D04
		public static void AddRequestHeaderMapping(this MediaTypeFormatter formatter, string headerName, string headerValue, StringComparison valueComparison, bool isValueSubstring, string mediaType)
		{
			if (formatter == null)
			{
				throw Error.ArgumentNull("formatter");
			}
			RequestHeaderMapping requestHeaderMapping = new RequestHeaderMapping(headerName, headerValue, valueComparison, isValueSubstring, mediaType);
			formatter.MediaTypeMappings.Add(requestHeaderMapping);
		}
	}
}
