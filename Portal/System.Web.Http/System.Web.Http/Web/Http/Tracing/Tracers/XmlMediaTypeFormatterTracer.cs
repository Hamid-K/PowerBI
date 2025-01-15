using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Services;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x0200013E RID: 318
	internal class XmlMediaTypeFormatterTracer : XmlMediaTypeFormatter, IFormatterTracer, IDecorator<XmlMediaTypeFormatter>
	{
		// Token: 0x06000895 RID: 2197 RVA: 0x00015869 File Offset: 0x00013A69
		public XmlMediaTypeFormatterTracer(XmlMediaTypeFormatter innerFormatter, ITraceWriter traceWriter, HttpRequestMessage request)
			: base(innerFormatter)
		{
			this._inner = innerFormatter;
			this._innerTracer = new MediaTypeFormatterTracer(innerFormatter, traceWriter, request);
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000896 RID: 2198 RVA: 0x00015887 File Offset: 0x00013A87
		HttpRequestMessage IFormatterTracer.Request
		{
			get
			{
				return this._innerTracer.Request;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000897 RID: 2199 RVA: 0x00015894 File Offset: 0x00013A94
		public XmlMediaTypeFormatter Inner
		{
			get
			{
				return this._inner;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000898 RID: 2200 RVA: 0x0001589C File Offset: 0x00013A9C
		public MediaTypeFormatter InnerFormatter
		{
			get
			{
				return this._innerTracer.InnerFormatter;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x000158A9 File Offset: 0x00013AA9
		// (set) Token: 0x0600089A RID: 2202 RVA: 0x000158B6 File Offset: 0x00013AB6
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

		// Token: 0x0600089B RID: 2203 RVA: 0x000158C4 File Offset: 0x00013AC4
		public override bool CanReadType(Type type)
		{
			return this._innerTracer.CanReadType(type);
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x000158D2 File Offset: 0x00013AD2
		public override bool CanWriteType(Type type)
		{
			return this._innerTracer.CanWriteType(type);
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x000158E0 File Offset: 0x00013AE0
		public override MediaTypeFormatter GetPerRequestFormatterInstance(Type type, HttpRequestMessage request, MediaTypeHeaderValue mediaType)
		{
			return this._innerTracer.GetPerRequestFormatterInstance(type, request, mediaType);
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x000158F0 File Offset: 0x00013AF0
		public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger, CancellationToken cancellationToken)
		{
			return this._innerTracer.ReadFromStreamAsync(type, readStream, content, formatterLogger, cancellationToken);
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x00015904 File Offset: 0x00013B04
		public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
		{
			return this._innerTracer.ReadFromStreamAsync(type, readStream, content, formatterLogger);
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x00015916 File Offset: 0x00013B16
		public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext, CancellationToken cancellationToken)
		{
			return this._innerTracer.WriteToStreamAsync(type, value, writeStream, content, transportContext, cancellationToken);
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0001592C File Offset: 0x00013B2C
		public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext)
		{
			return this._innerTracer.WriteToStreamAsync(type, value, writeStream, content, transportContext);
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x00015940 File Offset: 0x00013B40
		public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
		{
			this._innerTracer.SetDefaultContentHeaders(type, headers, mediaType);
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x00015950 File Offset: 0x00013B50
		public override XmlSerializer CreateXmlSerializer(Type type)
		{
			return this._inner.CreateXmlSerializer(type);
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x0001595E File Offset: 0x00013B5E
		public override DataContractSerializer CreateDataContractSerializer(Type type)
		{
			return this._inner.CreateDataContractSerializer(type);
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x0001596C File Offset: 0x00013B6C
		protected override XmlReader CreateXmlReader(Stream readStream, HttpContent content)
		{
			return this._inner.InvokeCreateXmlReader(readStream, content);
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0001597B File Offset: 0x00013B7B
		protected override XmlWriter CreateXmlWriter(Stream writeStream, HttpContent content)
		{
			return this._inner.InvokeCreateXmlWriter(writeStream, content);
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0001598A File Offset: 0x00013B8A
		protected override object GetDeserializer(Type type, HttpContent content)
		{
			return this._inner.InvokeGetDeserializer(type, content);
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x00015999 File Offset: 0x00013B99
		protected override object GetSerializer(Type type, object value, HttpContent content)
		{
			return this._inner.InvokeGetSerializer(type, value, content);
		}

		// Token: 0x04000255 RID: 597
		private readonly XmlMediaTypeFormatter _inner;

		// Token: 0x04000256 RID: 598
		private readonly MediaTypeFormatterTracer _innerTracer;
	}
}
