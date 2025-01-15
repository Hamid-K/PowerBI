using System;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading;
using System.Web.Http.Properties;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x02000130 RID: 304
	internal class BufferedMediaTypeFormatterTracer : BufferedMediaTypeFormatter, IFormatterTracer, IDecorator<BufferedMediaTypeFormatter>
	{
		// Token: 0x0600080E RID: 2062 RVA: 0x00014589 File Offset: 0x00012789
		public BufferedMediaTypeFormatterTracer(BufferedMediaTypeFormatter innerFormatter, ITraceWriter traceWriter, HttpRequestMessage request)
			: base(innerFormatter)
		{
			this._inner = innerFormatter;
			this._innerTracer = new MediaTypeFormatterTracer(innerFormatter, traceWriter, request);
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x0600080F RID: 2063 RVA: 0x000145A7 File Offset: 0x000127A7
		HttpRequestMessage IFormatterTracer.Request
		{
			get
			{
				return this._innerTracer.Request;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x000145B4 File Offset: 0x000127B4
		public BufferedMediaTypeFormatter Inner
		{
			get
			{
				return this._inner;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000811 RID: 2065 RVA: 0x000145BC File Offset: 0x000127BC
		public MediaTypeFormatter InnerFormatter
		{
			get
			{
				return this._innerTracer.InnerFormatter;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000812 RID: 2066 RVA: 0x000145C9 File Offset: 0x000127C9
		// (set) Token: 0x06000813 RID: 2067 RVA: 0x000145D6 File Offset: 0x000127D6
		public override IRequiredMemberSelector RequiredMemberSelector
		{
			get
			{
				return this._innerTracer.RequiredMemberSelector;
			}
			set
			{
				this._innerTracer.RequiredMemberSelector = value;
			}
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x000145E4 File Offset: 0x000127E4
		public override bool CanReadType(Type type)
		{
			return this._innerTracer.CanReadType(type);
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x000145F2 File Offset: 0x000127F2
		public override bool CanWriteType(Type type)
		{
			return this._innerTracer.CanWriteType(type);
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x00014600 File Offset: 0x00012800
		public override MediaTypeFormatter GetPerRequestFormatterInstance(Type type, HttpRequestMessage request, MediaTypeHeaderValue mediaType)
		{
			return this._innerTracer.GetPerRequestFormatterInstance(type, request, mediaType);
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x00014610 File Offset: 0x00012810
		public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
		{
			this._innerTracer.SetDefaultContentHeaders(type, headers, mediaType);
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00014620 File Offset: 0x00012820
		public override object ReadFromStream(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
		{
			return this.ReadFromStreamCore(type, readStream, content, formatterLogger, null);
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x00014641 File Offset: 0x00012841
		public override object ReadFromStream(Type type, Stream stream, HttpContent content, IFormatterLogger formatterLogger, CancellationToken cancellationToken)
		{
			return this.ReadFromStreamCore(type, stream, content, formatterLogger, new CancellationToken?(cancellationToken));
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x00014658 File Offset: 0x00012858
		private object ReadFromStreamCore(Type type, Stream stream, HttpContent content, IFormatterLogger formatterLogger, CancellationToken? cancellationToken = null)
		{
			BufferedMediaTypeFormatter innerFormatter = this.InnerFormatter as BufferedMediaTypeFormatter;
			HttpContentHeaders httpContentHeaders = ((content == null) ? null : content.Headers);
			MediaTypeHeaderValue contentType = ((httpContentHeaders == null) ? null : httpContentHeaders.ContentType);
			object value = null;
			this._innerTracer.TraceWriter.TraceBeginEnd(this._innerTracer.Request, TraceCategories.FormattingCategory, TraceLevel.Info, this._innerTracer.InnerFormatter.GetType().Name, "ReadFromStream", delegate(TraceRecord tr)
			{
				tr.Message = Error.Format(SRResources.TraceReadFromStreamMessage, new object[]
				{
					type.Name,
					(contentType == null) ? SRResources.TraceNoneObjectMessage : contentType.ToString()
				});
			}, delegate
			{
				if (cancellationToken != null)
				{
					value = innerFormatter.ReadFromStream(type, stream, content, formatterLogger, cancellationToken.Value);
					return;
				}
				value = innerFormatter.ReadFromStream(type, stream, content, formatterLogger);
			}, delegate(TraceRecord tr)
			{
				tr.Message = Error.Format(SRResources.TraceReadFromStreamValueMessage, new object[] { FormattingUtilities.ValueToString(value, CultureInfo.CurrentCulture) });
			}, null);
			return value;
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x00014738 File Offset: 0x00012938
		public override void WriteToStream(Type type, object value, Stream writeStream, HttpContent content)
		{
			this.WriteToStreamCore(type, value, writeStream, content, null);
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x00014759 File Offset: 0x00012959
		public override void WriteToStream(Type type, object value, Stream writeStream, HttpContent content, CancellationToken cancellationToken)
		{
			this.WriteToStreamCore(type, value, writeStream, content, new CancellationToken?(cancellationToken));
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x00014770 File Offset: 0x00012970
		private void WriteToStreamCore(Type type, object value, Stream writeStream, HttpContent content, CancellationToken? cancellationToken = null)
		{
			BufferedMediaTypeFormatter innerFormatter = this.InnerFormatter as BufferedMediaTypeFormatter;
			HttpContentHeaders httpContentHeaders = ((content == null) ? null : content.Headers);
			MediaTypeHeaderValue contentType = ((httpContentHeaders == null) ? null : httpContentHeaders.ContentType);
			this._innerTracer.TraceWriter.TraceBeginEnd(this._innerTracer.Request, TraceCategories.FormattingCategory, TraceLevel.Info, this._innerTracer.InnerFormatter.GetType().Name, "WriteToStream", delegate(TraceRecord tr)
			{
				tr.Message = Error.Format(SRResources.TraceWriteToStreamMessage, new object[]
				{
					FormattingUtilities.ValueToString(value, CultureInfo.CurrentCulture),
					type.Name,
					(contentType == null) ? SRResources.TraceNoneObjectMessage : contentType.ToString()
				});
			}, delegate
			{
				if (cancellationToken != null)
				{
					innerFormatter.WriteToStream(type, value, writeStream, content, cancellationToken.Value);
					return;
				}
				innerFormatter.WriteToStream(type, value, writeStream, content);
			}, null, null);
		}

		// Token: 0x0400022E RID: 558
		private const string OnReadFromStreamMethodName = "ReadFromStream";

		// Token: 0x0400022F RID: 559
		private const string OnWriteToStreamMethodName = "WriteToStream";

		// Token: 0x04000230 RID: 560
		private readonly BufferedMediaTypeFormatter _inner;

		// Token: 0x04000231 RID: 561
		private MediaTypeFormatterTracer _innerTracer;
	}
}
