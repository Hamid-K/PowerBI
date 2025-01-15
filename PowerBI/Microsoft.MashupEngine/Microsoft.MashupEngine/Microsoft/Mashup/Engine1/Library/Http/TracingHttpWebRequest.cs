using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.RequestTracing;

namespace Microsoft.Mashup.Engine1.Library.Http
{
	// Token: 0x02000A9A RID: 2714
	internal class TracingHttpWebRequest : DelegatingHttpWebRequest
	{
		// Token: 0x170017F2 RID: 6130
		// (get) Token: 0x06004C02 RID: 19458 RVA: 0x000FB4C9 File Offset: 0x000F96C9
		private IRequestTrace NetworkTrace
		{
			get
			{
				if (this.trace == null)
				{
					this.trace = RequestTracingService.CreateTrace(this.host, this.resource, this.sessionId, "Request");
				}
				return this.trace;
			}
		}

		// Token: 0x170017F3 RID: 6131
		// (get) Token: 0x06004C03 RID: 19459 RVA: 0x000FB4FB File Offset: 0x000F96FB
		private Stream RequestTrace
		{
			get
			{
				if (this.requestTrace == null)
				{
					this.requestTrace = this.NetworkTrace.GetContentStream();
				}
				return this.requestTrace;
			}
		}

		// Token: 0x06004C04 RID: 19460 RVA: 0x000FB51C File Offset: 0x000F971C
		public TracingHttpWebRequest(MashupHttpWebRequest request, IEngineHost host, IResource resource)
			: base(request)
		{
			this.host = host;
			this.resource = resource;
			this.sessionId = Guid.NewGuid();
		}

		// Token: 0x06004C05 RID: 19461 RVA: 0x000FB540 File Offset: 0x000F9740
		public override WebResponse GetResponse()
		{
			if (this.requestTrace == null)
			{
				this.WriteHttpRequestHeaders(this.RequestTrace);
			}
			WebResponse webResponse;
			try
			{
				webResponse = new TracingHttpWebResponse((MashupHttpWebResponse)base.GetResponse(), this.host, this.resource, this.sessionId);
			}
			catch (WebException ex)
			{
				MashupHttpWebResponse mashupHttpWebResponse = ex.Response as MashupHttpWebResponse;
				if (mashupHttpWebResponse != null)
				{
					TracingHttpWebResponse tracingHttpWebResponse = new TracingHttpWebResponse(mashupHttpWebResponse, this.host, this.resource, this.sessionId);
					WebException ex2 = new WebException(ex.Message, ex.InnerException, ex.Status, tracingHttpWebResponse);
					foreach (object obj in ex.Data.Keys)
					{
						string text = (string)obj;
						ex2.Data[text] = ex.Data[text];
					}
					ex2.HelpLink = ex.HelpLink;
					ex2.Source = ex.Source;
					throw ex2;
				}
				throw;
			}
			finally
			{
				if (this.requestTrace != null)
				{
					this.requestTrace.Dispose();
					this.requestTrace = null;
				}
				if (this.trace != null)
				{
					this.trace.Dispose();
					this.trace = null;
				}
			}
			return webResponse;
		}

		// Token: 0x06004C06 RID: 19462 RVA: 0x000FB6AC File Offset: 0x000F98AC
		public override Stream GetRequestStream()
		{
			this.WriteHttpRequestHeaders(this.RequestTrace);
			return new TracingHttpWebRequest.TracingWriteStream(base.GetRequestStream(), this.RequestTrace);
		}

		// Token: 0x06004C07 RID: 19463 RVA: 0x000FB6CB File Offset: 0x000F98CB
		public override Stream GetRequestStream(out TransportContext context)
		{
			this.WriteHttpRequestHeaders(this.RequestTrace);
			return new TracingHttpWebRequest.TracingWriteStream(base.GetRequestStream(out context), this.RequestTrace);
		}

		// Token: 0x06004C08 RID: 19464 RVA: 0x000FB6EC File Offset: 0x000F98EC
		private void WriteHttpRequestHeaders(Stream stream)
		{
			TextWriter textWriter = new StreamWriter(stream);
			textWriter.Write("{0} {1} HTTP/{2}\r\n", this.Method, this.RequestUri.ToString(), this.ProtocolVersion);
			if (this.Accept != null)
			{
				textWriter.Write("Accept: {0}\r\n", this.Accept);
			}
			if (this.Connection != null || this.KeepAlive)
			{
				textWriter.Write("Connection:");
				if (this.Connection != null)
				{
					textWriter.Write(" {0}", this.Connection);
				}
				if (this.KeepAlive)
				{
					textWriter.Write(" Keep-alive");
				}
				textWriter.Write("\r\n");
			}
			if (this.ContentType != null)
			{
				textWriter.Write("Content-Type: {0}\r\n", this.ContentType);
			}
			if (this.ContentLength != -1L)
			{
				textWriter.Write("Content-Length: {0}\r\n", this.ContentLength);
			}
			if (this.Expect != null)
			{
				textWriter.Write("Expect: {0}\r\n", this.Expect);
			}
			if (this.IfModifiedSince > DateTime.MinValue)
			{
				textWriter.Write("If-Modified-Since: {0:r}\r\n", this.IfModifiedSince);
			}
			if (this.Referer != null)
			{
				textWriter.Write("Referer: {0}\r\n", this.Referer);
			}
			if (this.TransferEncoding != null)
			{
				textWriter.Write("Transfer-Encoding: {0}\r\n", this.TransferEncoding);
			}
			if (this.UserAgent != null)
			{
				textWriter.Write("User-Agent: {0}\r\n", this.UserAgent);
			}
			foreach (string text in this.Headers.AllKeys.Except(TracingHttpWebRequest.RestrictedKeys))
			{
				textWriter.Write("{0}: {1}\r\n", text, this.Headers[text]);
			}
			textWriter.Write("\r\n");
			textWriter.Flush();
		}

		// Token: 0x04002857 RID: 10327
		private static IEnumerable<string> RestrictedKeys = new string[] { "Accept", "Connection", "Content-Length", "Content-Type", "Expect", "If-Modified-Since", "Range", "Referer", "Transfer-Encoding", "User-Agent" };

		// Token: 0x04002858 RID: 10328
		private readonly IEngineHost host;

		// Token: 0x04002859 RID: 10329
		private readonly IResource resource;

		// Token: 0x0400285A RID: 10330
		private readonly Guid sessionId;

		// Token: 0x0400285B RID: 10331
		private IRequestTrace trace;

		// Token: 0x0400285C RID: 10332
		private Stream requestTrace;

		// Token: 0x02000A9B RID: 2715
		private class TracingWriteStream : DelegatingStream
		{
			// Token: 0x06004C0A RID: 19466 RVA: 0x000FB92E File Offset: 0x000F9B2E
			public TracingWriteStream(Stream sourceStream, Stream traceStream)
				: base(sourceStream)
			{
				this.traceStream = traceStream;
			}

			// Token: 0x06004C0B RID: 19467 RVA: 0x000FB940 File Offset: 0x000F9B40
			public override void Write(byte[] buffer, int offset, int count)
			{
				try
				{
					this.traceStream.Write(buffer, offset, count);
					base.Write(buffer, offset, count);
				}
				catch (Exception)
				{
					this.traceStream.Dispose();
					throw;
				}
			}

			// Token: 0x06004C0C RID: 19468 RVA: 0x000FB984 File Offset: 0x000F9B84
			public override void WriteByte(byte value)
			{
				try
				{
					this.traceStream.WriteByte(value);
					base.WriteByte(value);
				}
				catch (Exception)
				{
					this.traceStream.Dispose();
					throw;
				}
			}

			// Token: 0x0400285D RID: 10333
			private readonly Stream traceStream;
		}
	}
}
