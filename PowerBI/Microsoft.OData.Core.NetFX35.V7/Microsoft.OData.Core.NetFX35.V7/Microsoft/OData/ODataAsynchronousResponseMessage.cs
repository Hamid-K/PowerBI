using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.OData
{
	// Token: 0x0200002B RID: 43
	public sealed class ODataAsynchronousResponseMessage : IContainerProvider, IODataResponseMessage
	{
		// Token: 0x06000119 RID: 281 RVA: 0x000054F1 File Offset: 0x000036F1
		private ODataAsynchronousResponseMessage(Stream stream, int statusCode, IDictionary<string, string> headers, Action<ODataAsynchronousResponseMessage> writeEnvelope, bool writing, IServiceProvider container)
		{
			this.stream = stream;
			this.statusCode = statusCode;
			this.headers = headers;
			this.writeEnvelope = writeEnvelope;
			this.writing = writing;
			this.container = container;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00005526 File Offset: 0x00003726
		// (set) Token: 0x0600011B RID: 283 RVA: 0x0000552E File Offset: 0x0000372E
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

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600011C RID: 284 RVA: 0x0000553D File Offset: 0x0000373D
		public IEnumerable<KeyValuePair<string, string>> Headers
		{
			get
			{
				return this.headers;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00005545 File Offset: 0x00003745
		public IServiceProvider Container
		{
			get
			{
				return this.container;
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00005550 File Offset: 0x00003750
		public string GetHeader(string headerName)
		{
			string text;
			if (this.headers != null && this.headers.TryGetValue(headerName, ref text))
			{
				return text;
			}
			return null;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00005578 File Offset: 0x00003778
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

		// Token: 0x06000120 RID: 288 RVA: 0x000055C9 File Offset: 0x000037C9
		public Stream GetStream()
		{
			if (this.writing && !this.envelopeWritten)
			{
				if (this.writeEnvelope != null)
				{
					this.writeEnvelope.Invoke(this);
				}
				this.envelopeWritten = true;
			}
			return this.stream;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000055FC File Offset: 0x000037FC
		internal static ODataAsynchronousResponseMessage CreateMessageForWriting(Stream outputStream, Action<ODataAsynchronousResponseMessage> writeEnvelope, IServiceProvider container)
		{
			return new ODataAsynchronousResponseMessage(outputStream, 0, null, writeEnvelope, true, container);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00005609 File Offset: 0x00003809
		internal static ODataAsynchronousResponseMessage CreateMessageForReading(Stream stream, int statusCode, IDictionary<string, string> headers, IServiceProvider container)
		{
			return new ODataAsynchronousResponseMessage(stream, statusCode, headers, null, false, container);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00005616 File Offset: 0x00003816
		private void VerifyCanSetHeaderAndStatusCode()
		{
			if (!this.writing)
			{
				throw new ODataException(Strings.ODataAsyncResponseMessage_MustNotModifyMessage);
			}
		}

		// Token: 0x040000C6 RID: 198
		private readonly bool writing;

		// Token: 0x040000C7 RID: 199
		private readonly Stream stream;

		// Token: 0x040000C8 RID: 200
		private readonly Action<ODataAsynchronousResponseMessage> writeEnvelope;

		// Token: 0x040000C9 RID: 201
		private readonly IServiceProvider container;

		// Token: 0x040000CA RID: 202
		private bool envelopeWritten;

		// Token: 0x040000CB RID: 203
		private int statusCode;

		// Token: 0x040000CC RID: 204
		private IDictionary<string, string> headers;
	}
}
