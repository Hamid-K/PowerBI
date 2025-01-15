using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Services;
using Newtonsoft.Json;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x0200013A RID: 314
	internal class JsonMediaTypeFormatterTracer : JsonMediaTypeFormatter, IFormatterTracer, IDecorator<JsonMediaTypeFormatter>
	{
		// Token: 0x06000861 RID: 2145 RVA: 0x0001518D File Offset: 0x0001338D
		public JsonMediaTypeFormatterTracer(JsonMediaTypeFormatter innerFormatter, ITraceWriter traceWriter, HttpRequestMessage request)
			: base(innerFormatter)
		{
			this._inner = innerFormatter;
			this._innerTracer = new MediaTypeFormatterTracer(innerFormatter, traceWriter, request);
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000862 RID: 2146 RVA: 0x000151AB File Offset: 0x000133AB
		HttpRequestMessage IFormatterTracer.Request
		{
			get
			{
				return this._innerTracer.Request;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000863 RID: 2147 RVA: 0x000151B8 File Offset: 0x000133B8
		public JsonMediaTypeFormatter Inner
		{
			get
			{
				return this._inner;
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000864 RID: 2148 RVA: 0x000151C0 File Offset: 0x000133C0
		public MediaTypeFormatter InnerFormatter
		{
			get
			{
				return this._innerTracer.InnerFormatter;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000865 RID: 2149 RVA: 0x000151CD File Offset: 0x000133CD
		// (set) Token: 0x06000866 RID: 2150 RVA: 0x000151DA File Offset: 0x000133DA
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

		// Token: 0x06000867 RID: 2151 RVA: 0x000151E8 File Offset: 0x000133E8
		public override bool CanReadType(Type type)
		{
			return this._innerTracer.CanReadType(type);
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x000151F6 File Offset: 0x000133F6
		public override bool CanWriteType(Type type)
		{
			return this._innerTracer.CanWriteType(type);
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x00015204 File Offset: 0x00013404
		public override MediaTypeFormatter GetPerRequestFormatterInstance(Type type, HttpRequestMessage request, MediaTypeHeaderValue mediaType)
		{
			return this._innerTracer.GetPerRequestFormatterInstance(type, request, mediaType);
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x00015214 File Offset: 0x00013414
		public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
		{
			return this._innerTracer.ReadFromStreamAsync(type, readStream, content, formatterLogger);
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x00015226 File Offset: 0x00013426
		public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger, CancellationToken cancellationToken)
		{
			return this._innerTracer.ReadFromStreamAsync(type, readStream, content, formatterLogger, cancellationToken);
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0001523A File Offset: 0x0001343A
		public override object ReadFromStream(Type type, Stream readStream, Encoding effectiveEncoding, IFormatterLogger formatterLogger)
		{
			return this._inner.ReadFromStream(type, readStream, effectiveEncoding, formatterLogger);
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x0001524C File Offset: 0x0001344C
		public override JsonReader CreateJsonReader(Type type, Stream readStream, Encoding effectiveEncoding)
		{
			return this._inner.CreateJsonReader(type, readStream, effectiveEncoding);
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x0001525C File Offset: 0x0001345C
		public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext)
		{
			return this._innerTracer.WriteToStreamAsync(type, value, writeStream, content, transportContext);
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x00015270 File Offset: 0x00013470
		public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext, CancellationToken cancellationToken)
		{
			return this._innerTracer.WriteToStreamAsync(type, value, writeStream, content, transportContext, cancellationToken);
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x00015286 File Offset: 0x00013486
		public override void WriteToStream(Type type, object value, Stream writeStream, Encoding effectiveEncoding)
		{
			this._inner.WriteToStream(type, value, writeStream, effectiveEncoding);
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x00015298 File Offset: 0x00013498
		public override JsonWriter CreateJsonWriter(Type type, Stream writeStream, Encoding effectiveEncoding)
		{
			return this._inner.CreateJsonWriter(type, writeStream, effectiveEncoding);
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x000152A8 File Offset: 0x000134A8
		public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
		{
			this._innerTracer.SetDefaultContentHeaders(type, headers, mediaType);
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x000152B8 File Offset: 0x000134B8
		public override JsonSerializer CreateJsonSerializer()
		{
			return this._inner.CreateJsonSerializer();
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x000152C5 File Offset: 0x000134C5
		public override DataContractJsonSerializer CreateDataContractSerializer(Type type)
		{
			return this._inner.CreateDataContractSerializer(type);
		}

		// Token: 0x04000248 RID: 584
		private readonly JsonMediaTypeFormatter _inner;

		// Token: 0x04000249 RID: 585
		private MediaTypeFormatterTracer _innerTracer;
	}
}
