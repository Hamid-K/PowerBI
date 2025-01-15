using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Web.Http;

namespace System.Net.Http.Formatting
{
	// Token: 0x0200004B RID: 75
	public class RequestHeaderMapping : MediaTypeMapping
	{
		// Token: 0x060002E6 RID: 742 RVA: 0x00009F2D File Offset: 0x0000812D
		public RequestHeaderMapping(string headerName, string headerValue, StringComparison valueComparison, bool isValueSubstring, string mediaType)
			: base(mediaType)
		{
			this.Initialize(headerName, headerValue, valueComparison, isValueSubstring);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00009F42 File Offset: 0x00008142
		public RequestHeaderMapping(string headerName, string headerValue, StringComparison valueComparison, bool isValueSubstring, MediaTypeHeaderValue mediaType)
			: base(mediaType)
		{
			this.Initialize(headerName, headerValue, valueComparison, isValueSubstring);
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x00009F57 File Offset: 0x00008157
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x00009F5F File Offset: 0x0000815F
		public string HeaderName { get; private set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002EA RID: 746 RVA: 0x00009F68 File Offset: 0x00008168
		// (set) Token: 0x060002EB RID: 747 RVA: 0x00009F70 File Offset: 0x00008170
		public string HeaderValue { get; private set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002EC RID: 748 RVA: 0x00009F79 File Offset: 0x00008179
		// (set) Token: 0x060002ED RID: 749 RVA: 0x00009F81 File Offset: 0x00008181
		public StringComparison HeaderValueComparison { get; private set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002EE RID: 750 RVA: 0x00009F8A File Offset: 0x0000818A
		// (set) Token: 0x060002EF RID: 751 RVA: 0x00009F92 File Offset: 0x00008192
		public bool IsValueSubstring { get; private set; }

		// Token: 0x060002F0 RID: 752 RVA: 0x00009F9B File Offset: 0x0000819B
		public override double TryMatchMediaType(HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			return RequestHeaderMapping.MatchHeaderValue(request, this.HeaderName, this.HeaderValue, this.HeaderValueComparison, this.IsValueSubstring);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00009FCC File Offset: 0x000081CC
		private static double MatchHeaderValue(HttpRequestMessage request, string headerName, string headerValue, StringComparison valueComparison, bool isValueSubstring)
		{
			IEnumerable<string> enumerable;
			if (request.Headers.TryGetValues(headerName, out enumerable))
			{
				foreach (string text in enumerable)
				{
					if (isValueSubstring)
					{
						if (text.IndexOf(headerValue, valueComparison) != -1)
						{
							return 1.0;
						}
					}
					else if (text.Equals(headerValue, valueComparison))
					{
						return 1.0;
					}
				}
			}
			return 0.0;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000A05C File Offset: 0x0000825C
		private void Initialize(string headerName, string headerValue, StringComparison valueComparison, bool isValueSubstring)
		{
			if (string.IsNullOrWhiteSpace(headerName))
			{
				throw Error.ArgumentNull("headerName");
			}
			if (string.IsNullOrWhiteSpace(headerValue))
			{
				throw Error.ArgumentNull("headerValue");
			}
			StringComparisonHelper.Validate(valueComparison, "valueComparison");
			this.HeaderName = headerName;
			this.HeaderValue = headerValue;
			this.HeaderValueComparison = valueComparison;
			this.IsValueSubstring = isValueSubstring;
		}
	}
}
