using System;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.RequestTracing;

namespace Microsoft.Mashup.Engine1.Library.Http
{
	// Token: 0x02000A9C RID: 2716
	internal class TracingHttpWebResponse : DelegatingHttpWebResponse
	{
		// Token: 0x06004C0D RID: 19469 RVA: 0x000FB9C4 File Offset: 0x000F9BC4
		public TracingHttpWebResponse(MashupHttpWebResponse response, IEngineHost host, IResource resource, Guid sessionId)
			: base(response)
		{
			this.trace = RequestTracingService.CreateTrace(host, resource, sessionId, "Response");
		}

		// Token: 0x06004C0E RID: 19470 RVA: 0x000FB9E1 File Offset: 0x000F9BE1
		public override Stream GetResponseStream()
		{
			this.traceStream = this.trace.GetContentStream();
			this.WriteHttpResponseHeaders(this.traceStream);
			return new TracingHttpWebResponse.TracingReadStream(base.GetResponseStream(), this);
		}

		// Token: 0x06004C0F RID: 19471 RVA: 0x000FBA0C File Offset: 0x000F9C0C
		public override void Close()
		{
			if (this.trace != null && this.traceStream == null)
			{
				using (Stream contentStream = this.trace.GetContentStream())
				{
					this.WriteHttpResponseHeaders(contentStream);
				}
			}
			this.DisposeTrace();
			base.Close();
		}

		// Token: 0x06004C10 RID: 19472 RVA: 0x000FBA64 File Offset: 0x000F9C64
		private void DisposeTrace()
		{
			if (this.traceStream != null)
			{
				this.traceStream.Dispose();
				this.traceStream = null;
			}
			if (this.trace != null)
			{
				this.trace.Dispose();
				this.trace = null;
			}
		}

		// Token: 0x06004C11 RID: 19473 RVA: 0x000FBA9C File Offset: 0x000F9C9C
		public void WriteHttpResponseHeaders(Stream stream)
		{
			TextWriter textWriter = new StreamWriter(stream);
			textWriter.Write("HTTP/{0} {1} {2}\r\n", this.ProtocolVersion, (int)this.StatusCode, this.StatusDescription);
			foreach (string text in this.Headers.AllKeys)
			{
				textWriter.Write("{0}: {1}\r\n", text, this.Headers[text]);
			}
			textWriter.Write("\r\n");
			textWriter.Flush();
		}

		// Token: 0x0400285E RID: 10334
		private IRequestTrace trace;

		// Token: 0x0400285F RID: 10335
		private Stream traceStream;

		// Token: 0x02000A9D RID: 2717
		private class TracingReadStream : DelegatingStream
		{
			// Token: 0x06004C12 RID: 19474 RVA: 0x000FBB19 File Offset: 0x000F9D19
			public TracingReadStream(Stream sourceStream, TracingHttpWebResponse response)
				: base(sourceStream)
			{
				this.response = response;
			}

			// Token: 0x06004C13 RID: 19475 RVA: 0x000FBB2C File Offset: 0x000F9D2C
			public override int Read(byte[] buffer, int offset, int count)
			{
				int num2;
				try
				{
					int num = base.Read(buffer, offset, count);
					this.response.traceStream.Write(buffer, offset, num);
					num2 = num;
				}
				catch (Exception)
				{
					this.response.DisposeTrace();
					throw;
				}
				return num2;
			}

			// Token: 0x06004C14 RID: 19476 RVA: 0x000FBB7C File Offset: 0x000F9D7C
			public override int ReadByte()
			{
				int num2;
				try
				{
					int num = base.ReadByte();
					if (num >= 0)
					{
						this.response.traceStream.WriteByte((byte)num);
					}
					num2 = num;
				}
				catch (Exception)
				{
					this.response.DisposeTrace();
					throw;
				}
				return num2;
			}

			// Token: 0x06004C15 RID: 19477 RVA: 0x000FBBCC File Offset: 0x000F9DCC
			public override void Close()
			{
				if (this.response != null)
				{
					this.response.Close();
					this.response = null;
				}
				base.Close();
			}

			// Token: 0x06004C16 RID: 19478 RVA: 0x000FBBEE File Offset: 0x000F9DEE
			protected override void Dispose(bool disposing)
			{
				if (disposing && this.response != null)
				{
					this.response.Close();
					this.response = null;
				}
				base.Dispose(disposing);
			}

			// Token: 0x04002860 RID: 10336
			private TracingHttpWebResponse response;
		}
	}
}
