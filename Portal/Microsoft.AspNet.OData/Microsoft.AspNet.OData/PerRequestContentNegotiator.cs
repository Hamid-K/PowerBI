using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData
{
	// Token: 0x0200000B RID: 11
	internal class PerRequestContentNegotiator : IContentNegotiator
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00002AED File Offset: 0x00000CED
		public PerRequestContentNegotiator(IContentNegotiator innerContentNegotiator)
		{
			if (innerContentNegotiator == null)
			{
				throw Error.ArgumentNull("innerContentNegotiator");
			}
			this._innerContentNegotiator = innerContentNegotiator;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002B0C File Offset: 0x00000D0C
		public ContentNegotiationResult Negotiate(Type type, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)
		{
			MediaTypeHeaderValue mediaTypeHeaderValue = ((request.Content == null) ? null : request.Content.Headers.ContentType);
			List<MediaTypeFormatter> list = new List<MediaTypeFormatter>();
			foreach (MediaTypeFormatter mediaTypeFormatter in formatters)
			{
				if (mediaTypeFormatter != null)
				{
					list.Add(mediaTypeFormatter.GetPerRequestFormatterInstance(type, request, mediaTypeHeaderValue));
				}
			}
			return this._innerContentNegotiator.Negotiate(type, request, list);
		}

		// Token: 0x04000008 RID: 8
		private IContentNegotiator _innerContentNegotiator;
	}
}
