using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http.Properties;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x02000136 RID: 310
	internal class ContentNegotiatorTracer : IContentNegotiator, IDecorator<IContentNegotiator>
	{
		// Token: 0x0600083E RID: 2110 RVA: 0x00014D61 File Offset: 0x00012F61
		public ContentNegotiatorTracer(IContentNegotiator innerNegotiator, ITraceWriter traceWriter)
		{
			this._innerNegotiator = innerNegotiator;
			this._traceWriter = traceWriter;
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x0600083F RID: 2111 RVA: 0x00014D77 File Offset: 0x00012F77
		public IContentNegotiator Inner
		{
			get
			{
				return this._innerNegotiator;
			}
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00014D80 File Offset: 0x00012F80
		public ContentNegotiationResult Negotiate(Type type, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)
		{
			ContentNegotiationResult result = null;
			this._traceWriter.TraceBeginEnd(request, TraceCategories.FormattingCategory, TraceLevel.Info, this._innerNegotiator.GetType().Name, "Negotiate", delegate(TraceRecord tr)
			{
				tr.Message = Error.Format(SRResources.TraceNegotiateFormatter, new object[]
				{
					type.Name,
					FormattingUtilities.FormattersToString(formatters)
				});
			}, delegate
			{
				result = this._innerNegotiator.Negotiate(type, request, formatters);
			}, delegate(TraceRecord tr)
			{
				tr.Message = Error.Format(SRResources.TraceSelectedFormatter, new object[]
				{
					(result == null) ? SRResources.TraceNoneObjectMessage : MediaTypeFormatterTracer.ActualMediaTypeFormatter(result.Formatter).GetType().Name,
					(result == null || result.MediaType == null) ? SRResources.TraceNoneObjectMessage : result.MediaType.ToString()
				});
			}, null);
			if (result != null)
			{
				result.Formatter = MediaTypeFormatterTracer.CreateTracer(result.Formatter, this._traceWriter, request);
			}
			return result;
		}

		// Token: 0x0400023D RID: 573
		private const string NegotiateMethodName = "Negotiate";

		// Token: 0x0400023E RID: 574
		private readonly IContentNegotiator _innerNegotiator;

		// Token: 0x0400023F RID: 575
		private readonly ITraceWriter _traceWriter;
	}
}
