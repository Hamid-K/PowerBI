using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.OData.Core
{
	// Token: 0x02000139 RID: 313
	public sealed class ODataAsynchronousResponseMessage : IODataResponseMessage
	{
		// Token: 0x06000BE4 RID: 3044 RVA: 0x0002CF31 File Offset: 0x0002B131
		private ODataAsynchronousResponseMessage(Stream stream, int statusCode, IDictionary<string, string> headers, Action<ODataAsynchronousResponseMessage> writeEnvelope, bool writing)
		{
			this.stream = stream;
			this.statusCode = statusCode;
			this.headers = headers;
			this.writeEnvelope = writeEnvelope;
			this.writing = writing;
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x0002CF5E File Offset: 0x0002B15E
		// (set) Token: 0x06000BE6 RID: 3046 RVA: 0x0002CF66 File Offset: 0x0002B166
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

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000BE7 RID: 3047 RVA: 0x0002CF75 File Offset: 0x0002B175
		public IEnumerable<KeyValuePair<string, string>> Headers
		{
			get
			{
				return this.headers;
			}
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x0002CF80 File Offset: 0x0002B180
		public string GetHeader(string headerName)
		{
			string text;
			if (this.headers != null && this.headers.TryGetValue(headerName, ref text))
			{
				return text;
			}
			return null;
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x0002CFA8 File Offset: 0x0002B1A8
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

		// Token: 0x06000BEA RID: 3050 RVA: 0x0002CFF9 File Offset: 0x0002B1F9
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

		// Token: 0x06000BEB RID: 3051 RVA: 0x0002D02C File Offset: 0x0002B22C
		internal static ODataAsynchronousResponseMessage CreateMessageForWriting(Stream outputStream, Action<ODataAsynchronousResponseMessage> writeEnvelope)
		{
			return new ODataAsynchronousResponseMessage(outputStream, 0, null, writeEnvelope, true);
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x0002D038 File Offset: 0x0002B238
		internal static ODataAsynchronousResponseMessage CreateMessageForReading(Stream stream, int statusCode, IDictionary<string, string> headers)
		{
			return new ODataAsynchronousResponseMessage(stream, statusCode, headers, null, false);
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x0002D044 File Offset: 0x0002B244
		private void VerifyCanSetHeaderAndStatusCode()
		{
			if (!this.writing)
			{
				throw new ODataException(Strings.ODataAsyncResponseMessage_MustNotModifyMessage);
			}
		}

		// Token: 0x040004F6 RID: 1270
		private readonly bool writing;

		// Token: 0x040004F7 RID: 1271
		private readonly Stream stream;

		// Token: 0x040004F8 RID: 1272
		private readonly Action<ODataAsynchronousResponseMessage> writeEnvelope;

		// Token: 0x040004F9 RID: 1273
		private bool envelopeWritten;

		// Token: 0x040004FA RID: 1274
		private int statusCode;

		// Token: 0x040004FB RID: 1275
		private IDictionary<string, string> headers;
	}
}
