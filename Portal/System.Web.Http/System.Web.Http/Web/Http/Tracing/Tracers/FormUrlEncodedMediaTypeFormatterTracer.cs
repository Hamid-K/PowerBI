using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x02000138 RID: 312
	internal class FormUrlEncodedMediaTypeFormatterTracer : FormUrlEncodedMediaTypeFormatter, IFormatterTracer, IDecorator<FormUrlEncodedMediaTypeFormatter>
	{
		// Token: 0x06000849 RID: 2121 RVA: 0x00014F98 File Offset: 0x00013198
		public FormUrlEncodedMediaTypeFormatterTracer(FormUrlEncodedMediaTypeFormatter innerFormatter, ITraceWriter traceWriter, HttpRequestMessage request)
			: base(innerFormatter)
		{
			this._inner = innerFormatter;
			this._innerTracer = new MediaTypeFormatterTracer(innerFormatter, traceWriter, request);
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x0600084A RID: 2122 RVA: 0x00014FB6 File Offset: 0x000131B6
		HttpRequestMessage IFormatterTracer.Request
		{
			get
			{
				return this._innerTracer.Request;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x0600084B RID: 2123 RVA: 0x00014FC3 File Offset: 0x000131C3
		public FormUrlEncodedMediaTypeFormatter Inner
		{
			get
			{
				return this._inner;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x0600084C RID: 2124 RVA: 0x00014FCB File Offset: 0x000131CB
		public MediaTypeFormatter InnerFormatter
		{
			get
			{
				return this._innerTracer.InnerFormatter;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x0600084D RID: 2125 RVA: 0x00014FD8 File Offset: 0x000131D8
		// (set) Token: 0x0600084E RID: 2126 RVA: 0x00014FE5 File Offset: 0x000131E5
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

		// Token: 0x0600084F RID: 2127 RVA: 0x00014FF3 File Offset: 0x000131F3
		public override bool CanReadType(Type type)
		{
			return this._innerTracer.CanReadType(type);
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x00015001 File Offset: 0x00013201
		public override bool CanWriteType(Type type)
		{
			return this._innerTracer.CanWriteType(type);
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0001500F File Offset: 0x0001320F
		public override MediaTypeFormatter GetPerRequestFormatterInstance(Type type, HttpRequestMessage request, MediaTypeHeaderValue mediaType)
		{
			return this._innerTracer.GetPerRequestFormatterInstance(type, request, mediaType);
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0001501F File Offset: 0x0001321F
		public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger, CancellationToken cancellationToken)
		{
			return this._innerTracer.ReadFromStreamAsync(type, readStream, content, formatterLogger, cancellationToken);
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x00015033 File Offset: 0x00013233
		public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
		{
			return this._innerTracer.ReadFromStreamAsync(type, readStream, content, formatterLogger);
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x00015045 File Offset: 0x00013245
		public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext, CancellationToken cancellationToken)
		{
			return this._innerTracer.WriteToStreamAsync(type, value, writeStream, content, transportContext, cancellationToken);
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0001505B File Offset: 0x0001325B
		public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext)
		{
			return this._innerTracer.WriteToStreamAsync(type, value, writeStream, content, transportContext);
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0001506F File Offset: 0x0001326F
		public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
		{
			this._innerTracer.SetDefaultContentHeaders(type, headers, mediaType);
		}

		// Token: 0x04000243 RID: 579
		private readonly FormUrlEncodedMediaTypeFormatter _inner;

		// Token: 0x04000244 RID: 580
		private MediaTypeFormatterTracer _innerTracer;
	}
}
