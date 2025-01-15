using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Microsoft.OData
{
	// Token: 0x02000053 RID: 83
	public sealed class ODataAsynchronousResponseMessage : IContainerProvider, IODataResponseMessageAsync, IODataResponseMessage
	{
		// Token: 0x06000292 RID: 658 RVA: 0x00008085 File Offset: 0x00006285
		private ODataAsynchronousResponseMessage(Stream stream, int statusCode, IDictionary<string, string> headers, Action<ODataAsynchronousResponseMessage> writeEnvelope, bool writing, IServiceProvider container)
		{
			this.stream = stream;
			this.statusCode = statusCode;
			this.headers = headers;
			this.writeEnvelope = writeEnvelope;
			this.writing = writing;
			this.container = container;
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000293 RID: 659 RVA: 0x000080BA File Offset: 0x000062BA
		// (set) Token: 0x06000294 RID: 660 RVA: 0x000080C2 File Offset: 0x000062C2
		public int StatusCode
		{
			get
			{
				return this.statusCode;
			}
			set
			{
				this.VerifyCanSetHeaderAndStatusCode();
				this.statusCode = value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000295 RID: 661 RVA: 0x000080D1 File Offset: 0x000062D1
		public IEnumerable<KeyValuePair<string, string>> Headers
		{
			get
			{
				return this.headers;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000296 RID: 662 RVA: 0x000080D9 File Offset: 0x000062D9
		public IServiceProvider Container
		{
			get
			{
				return this.container;
			}
		}

		// Token: 0x06000297 RID: 663 RVA: 0x000080E4 File Offset: 0x000062E4
		public string GetHeader(string headerName)
		{
			string text;
			if (this.headers != null && this.headers.TryGetValue(headerName, out text))
			{
				return text;
			}
			return null;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000810C File Offset: 0x0000630C
		public void SetHeader(string headerName, string headerValue)
		{
			this.VerifyCanSetHeaderAndStatusCode();
			if (headerValue == null)
			{
				if (this.headers != null)
				{
					this.headers.Remove(headerName);
					return;
				}
			}
			else
			{
				if (this.headers == null)
				{
					this.headers = new Dictionary<string, string>(StringComparer.Ordinal);
				}
				this.headers[headerName] = headerValue;
			}
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000815D File Offset: 0x0000635D
		public Stream GetStream()
		{
			if (this.writing && !this.envelopeWritten)
			{
				if (this.writeEnvelope != null)
				{
					this.writeEnvelope(this);
				}
				this.envelopeWritten = true;
			}
			return this.stream;
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00008190 File Offset: 0x00006390
		public Task<Stream> GetStreamAsync()
		{
			return Task<Stream>.Factory.StartNew(new Func<Stream>(this.GetStream));
		}

		// Token: 0x0600029B RID: 667 RVA: 0x000081A9 File Offset: 0x000063A9
		internal static ODataAsynchronousResponseMessage CreateMessageForWriting(Stream outputStream, Action<ODataAsynchronousResponseMessage> writeEnvelope, IServiceProvider container)
		{
			return new ODataAsynchronousResponseMessage(outputStream, 0, null, writeEnvelope, true, container);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x000081B6 File Offset: 0x000063B6
		internal static ODataAsynchronousResponseMessage CreateMessageForReading(Stream stream, int statusCode, IDictionary<string, string> headers, IServiceProvider container)
		{
			return new ODataAsynchronousResponseMessage(stream, statusCode, headers, null, false, container);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x000081C3 File Offset: 0x000063C3
		private void VerifyCanSetHeaderAndStatusCode()
		{
			if (!this.writing)
			{
				throw new ODataException(Strings.ODataAsyncResponseMessage_MustNotModifyMessage);
			}
		}

		// Token: 0x04000130 RID: 304
		private readonly bool writing;

		// Token: 0x04000131 RID: 305
		private readonly Stream stream;

		// Token: 0x04000132 RID: 306
		private readonly Action<ODataAsynchronousResponseMessage> writeEnvelope;

		// Token: 0x04000133 RID: 307
		private readonly IServiceProvider container;

		// Token: 0x04000134 RID: 308
		private bool envelopeWritten;

		// Token: 0x04000135 RID: 309
		private int statusCode;

		// Token: 0x04000136 RID: 310
		private IDictionary<string, string> headers;
	}
}
