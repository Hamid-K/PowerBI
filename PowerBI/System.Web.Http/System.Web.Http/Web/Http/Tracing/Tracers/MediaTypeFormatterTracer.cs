using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Properties;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x0200013B RID: 315
	internal class MediaTypeFormatterTracer : MediaTypeFormatter, IFormatterTracer, IDecorator<MediaTypeFormatter>
	{
		// Token: 0x06000875 RID: 2165 RVA: 0x000152D3 File Offset: 0x000134D3
		public MediaTypeFormatterTracer(MediaTypeFormatter innerFormatter, ITraceWriter traceWriter, HttpRequestMessage request)
			: base(innerFormatter)
		{
			this.InnerFormatter = innerFormatter;
			this.TraceWriter = traceWriter;
			this.Request = request;
			this._inner = innerFormatter;
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000876 RID: 2166 RVA: 0x000152F8 File Offset: 0x000134F8
		public MediaTypeFormatter Inner
		{
			get
			{
				return this._inner;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000877 RID: 2167 RVA: 0x00015300 File Offset: 0x00013500
		// (set) Token: 0x06000878 RID: 2168 RVA: 0x00015308 File Offset: 0x00013508
		public MediaTypeFormatter InnerFormatter { get; private set; }

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000879 RID: 2169 RVA: 0x00015311 File Offset: 0x00013511
		// (set) Token: 0x0600087A RID: 2170 RVA: 0x00015319 File Offset: 0x00013519
		public ITraceWriter TraceWriter { get; set; }

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x0600087B RID: 2171 RVA: 0x00015322 File Offset: 0x00013522
		// (set) Token: 0x0600087C RID: 2172 RVA: 0x0001532A File Offset: 0x0001352A
		public HttpRequestMessage Request { get; set; }

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x0600087D RID: 2173 RVA: 0x00015333 File Offset: 0x00013533
		// (set) Token: 0x0600087E RID: 2174 RVA: 0x00015340 File Offset: 0x00013540
		public override IRequiredMemberSelector RequiredMemberSelector
		{
			get
			{
				return this.InnerFormatter.RequiredMemberSelector;
			}
			set
			{
				this.InnerFormatter.RequiredMemberSelector = value;
			}
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00015350 File Offset: 0x00013550
		public static MediaTypeFormatter ActualMediaTypeFormatter(MediaTypeFormatter formatter)
		{
			IFormatterTracer formatterTracer = formatter as IFormatterTracer;
			if (formatterTracer != null)
			{
				return formatterTracer.InnerFormatter;
			}
			return formatter;
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x00015370 File Offset: 0x00013570
		public static MediaTypeFormatter CreateTracer(MediaTypeFormatter formatter, ITraceWriter traceWriter, HttpRequestMessage request)
		{
			IFormatterTracer formatterTracer = formatter as IFormatterTracer;
			if (formatterTracer != null)
			{
				if (formatterTracer.Request == request)
				{
					return formatter;
				}
				formatter = formatterTracer.InnerFormatter;
			}
			XmlMediaTypeFormatter xmlMediaTypeFormatter = formatter as XmlMediaTypeFormatter;
			JsonMediaTypeFormatter jsonMediaTypeFormatter = formatter as JsonMediaTypeFormatter;
			FormUrlEncodedMediaTypeFormatter formUrlEncodedMediaTypeFormatter = formatter as FormUrlEncodedMediaTypeFormatter;
			BufferedMediaTypeFormatter bufferedMediaTypeFormatter = formatter as BufferedMediaTypeFormatter;
			MediaTypeFormatter mediaTypeFormatter;
			if (xmlMediaTypeFormatter != null)
			{
				mediaTypeFormatter = new XmlMediaTypeFormatterTracer(xmlMediaTypeFormatter, traceWriter, request);
			}
			else if (jsonMediaTypeFormatter != null)
			{
				mediaTypeFormatter = new JsonMediaTypeFormatterTracer(jsonMediaTypeFormatter, traceWriter, request);
			}
			else if (formUrlEncodedMediaTypeFormatter != null)
			{
				mediaTypeFormatter = new FormUrlEncodedMediaTypeFormatterTracer(formUrlEncodedMediaTypeFormatter, traceWriter, request);
			}
			else if (bufferedMediaTypeFormatter != null)
			{
				mediaTypeFormatter = new BufferedMediaTypeFormatterTracer(bufferedMediaTypeFormatter, traceWriter, request);
			}
			else
			{
				mediaTypeFormatter = new MediaTypeFormatterTracer(formatter, traceWriter, request);
			}
			return mediaTypeFormatter;
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00015400 File Offset: 0x00013600
		public override MediaTypeFormatter GetPerRequestFormatterInstance(Type type, HttpRequestMessage request, MediaTypeHeaderValue mediaType)
		{
			MediaTypeFormatter formatter = null;
			this.TraceWriter.TraceBeginEnd(request, TraceCategories.FormattingCategory, TraceLevel.Info, this.InnerFormatter.GetType().Name, "GetPerRequestFormatterInstance", delegate(TraceRecord tr)
			{
				tr.Message = Error.Format(SRResources.TraceGetPerRequestFormatterMessage, new object[]
				{
					this.InnerFormatter.GetType().Name,
					type.Name,
					mediaType
				});
			}, delegate
			{
				formatter = this.InnerFormatter.GetPerRequestFormatterInstance(type, request, mediaType);
			}, delegate(TraceRecord tr)
			{
				if (formatter == null)
				{
					tr.Message = SRResources.TraceGetPerRequestNullFormatterEndMessage;
					return;
				}
				string text = ((MediaTypeFormatterTracer.ActualMediaTypeFormatter(formatter) == this.InnerFormatter) ? SRResources.TraceGetPerRequestFormatterEndMessage : SRResources.TraceGetPerRequestFormatterEndMessageNew);
				tr.Message = Error.Format(text, new object[] { formatter.GetType().Name });
			}, null);
			if (formatter != null && !(formatter is IFormatterTracer))
			{
				formatter = MediaTypeFormatterTracer.CreateTracer(formatter, this.TraceWriter, request);
			}
			return formatter;
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x000154BF File Offset: 0x000136BF
		public override bool CanReadType(Type type)
		{
			return this.InnerFormatter.CanReadType(type);
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x000154CD File Offset: 0x000136CD
		public override bool CanWriteType(Type type)
		{
			return this.InnerFormatter.CanWriteType(type);
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x000154DB File Offset: 0x000136DB
		public override bool Equals(object obj)
		{
			return this.InnerFormatter.Equals(obj);
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x000154E9 File Offset: 0x000136E9
		public override int GetHashCode()
		{
			return this.InnerFormatter.GetHashCode();
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x000154F6 File Offset: 0x000136F6
		public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
		{
			this.InnerFormatter.SetDefaultContentHeaders(type, headers, mediaType);
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00015506 File Offset: 0x00013706
		public override string ToString()
		{
			return this.InnerFormatter.ToString();
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00015514 File Offset: 0x00013714
		public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
		{
			return this.ReadFromStreamAsyncCore(type, readStream, content, formatterLogger, null);
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x00015535 File Offset: 0x00013735
		public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger, CancellationToken cancellationToken)
		{
			return this.ReadFromStreamAsyncCore(type, readStream, content, formatterLogger, new CancellationToken?(cancellationToken));
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x0001554C File Offset: 0x0001374C
		private Task<object> ReadFromStreamAsyncCore(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger, CancellationToken? cancellationToken)
		{
			HttpContentHeaders httpContentHeaders = ((content == null) ? null : content.Headers);
			MediaTypeHeaderValue contentType = ((httpContentHeaders == null) ? null : httpContentHeaders.ContentType);
			IFormatterLogger formatterLoggerTraceWrapper = ((formatterLogger == null) ? null : new FormatterLoggerTraceWrapper(formatterLogger, this.TraceWriter, this.Request, this.InnerFormatter.GetType().Name, "ReadFromStreamAsync"));
			return this.TraceWriter.TraceBeginEndAsync(this.Request, TraceCategories.FormattingCategory, TraceLevel.Info, this.InnerFormatter.GetType().Name, "ReadFromStreamAsync", delegate(TraceRecord tr)
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
					return this.InnerFormatter.ReadFromStreamAsync(type, readStream, content, formatterLoggerTraceWrapper, cancellationToken.Value);
				}
				return this.InnerFormatter.ReadFromStreamAsync(type, readStream, content, formatterLoggerTraceWrapper);
			}, delegate(TraceRecord tr, object value)
			{
				tr.Message = Error.Format(SRResources.TraceReadFromStreamValueMessage, new object[] { FormattingUtilities.ValueToString(value, CultureInfo.CurrentCulture) });
			}, null);
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x00015648 File Offset: 0x00013848
		public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext)
		{
			return this.WriteToStreamAsyncCore(type, value, writeStream, content, transportContext, null);
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x0001566B File Offset: 0x0001386B
		public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext, CancellationToken cancellationToken)
		{
			return this.WriteToStreamAsyncCore(type, value, writeStream, content, transportContext, new CancellationToken?(cancellationToken));
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x00015684 File Offset: 0x00013884
		private Task WriteToStreamAsyncCore(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext, CancellationToken? cancellationToken = null)
		{
			HttpContentHeaders httpContentHeaders = ((content == null) ? null : content.Headers);
			MediaTypeHeaderValue contentType = ((httpContentHeaders == null) ? null : httpContentHeaders.ContentType);
			return this.TraceWriter.TraceBeginEndAsync(this.Request, TraceCategories.FormattingCategory, TraceLevel.Info, this.InnerFormatter.GetType().Name, "WriteToStreamAsync", delegate(TraceRecord tr)
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
					return this.InnerFormatter.WriteToStreamAsync(type, value, writeStream, content, transportContext, cancellationToken.Value);
				}
				return this.InnerFormatter.WriteToStreamAsync(type, value, writeStream, content, transportContext);
			}, null, null);
		}

		// Token: 0x0400024A RID: 586
		private const string ReadFromStreamAsyncMethodName = "ReadFromStreamAsync";

		// Token: 0x0400024B RID: 587
		private const string WriteToStreamAsyncMethodName = "WriteToStreamAsync";

		// Token: 0x0400024C RID: 588
		private const string GetPerRequestFormatterInstanceMethodName = "GetPerRequestFormatterInstance";

		// Token: 0x0400024D RID: 589
		private readonly MediaTypeFormatter _inner;
	}
}
