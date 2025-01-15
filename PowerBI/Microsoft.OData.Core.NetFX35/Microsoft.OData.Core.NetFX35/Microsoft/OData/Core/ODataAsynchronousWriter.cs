using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.OData.Core
{
	// Token: 0x0200013A RID: 314
	public sealed class ODataAsynchronousWriter : IODataOutputInStreamErrorListener
	{
		// Token: 0x06000BEE RID: 3054 RVA: 0x0002D059 File Offset: 0x0002B259
		internal ODataAsynchronousWriter(ODataRawOutputContext rawOutputContext)
		{
			this.rawOutputContext = rawOutputContext;
			this.rawOutputContext.InitializeRawValueWriter();
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x0002D073 File Offset: 0x0002B273
		public ODataAsynchronousResponseMessage CreateResponseMessage()
		{
			this.VerifyCanCreateResponseMessage(true);
			return this.CreateResponseMessageImplementation();
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x0002D082 File Offset: 0x0002B282
		public void Flush()
		{
			this.VerifyCanFlush(true);
			this.rawOutputContext.Flush();
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x0002D096 File Offset: 0x0002B296
		void IODataOutputInStreamErrorListener.OnInStreamError()
		{
			this.rawOutputContext.VerifyNotDisposed();
			this.rawOutputContext.TextWriter.Flush();
			throw new ODataException(Strings.ODataAsyncWriter_CannotWriteInStreamErrorForAsync);
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x0002D0BD File Offset: 0x0002B2BD
		private void ValidateWriterNotDisposed()
		{
			this.rawOutputContext.VerifyNotDisposed();
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x0002D0CA File Offset: 0x0002B2CA
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall && !this.rawOutputContext.Synchronous)
			{
				throw new ODataException(Strings.ODataAsyncWriter_SyncCallOnAsyncWriter);
			}
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x0002D0E7 File Offset: 0x0002B2E7
		private void VerifyCanFlush(bool synchronousCall)
		{
			this.rawOutputContext.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x0002D0FB File Offset: 0x0002B2FB
		private void VerifyCanCreateResponseMessage(bool synchronousCall)
		{
			this.ValidateWriterNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			if (!this.rawOutputContext.WritingResponse)
			{
				throw new ODataException(Strings.ODataAsyncWriter_CannotCreateResponseWhenNotWritingResponse);
			}
			if (this.responseMessageCreated)
			{
				throw new ODataException(Strings.ODataAsyncWriter_CannotCreateResponseMoreThanOnce);
			}
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x0002D138 File Offset: 0x0002B338
		private ODataAsynchronousResponseMessage CreateResponseMessageImplementation()
		{
			ODataAsynchronousResponseMessage odataAsynchronousResponseMessage = ODataAsynchronousResponseMessage.CreateMessageForWriting(this.rawOutputContext.OutputStream, new Action<ODataAsynchronousResponseMessage>(this.WriteInnerEnvelope));
			this.responseMessageCreated = true;
			return odataAsynchronousResponseMessage;
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x0002D16C File Offset: 0x0002B36C
		private void WriteInnerEnvelope(ODataAsynchronousResponseMessage responseMessage)
		{
			string statusMessage = HttpUtils.GetStatusMessage(responseMessage.StatusCode);
			this.rawOutputContext.TextWriter.WriteLine("{0} {1} {2}", "HTTP/1.1", responseMessage.StatusCode, statusMessage);
			if (responseMessage.Headers != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in responseMessage.Headers)
				{
					string key = keyValuePair.Key;
					string value = keyValuePair.Value;
					this.rawOutputContext.TextWriter.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0}: {1}", new object[] { key, value }));
				}
			}
			this.rawOutputContext.TextWriter.WriteLine();
			this.rawOutputContext.TextWriter.Flush();
		}

		// Token: 0x040004FC RID: 1276
		private readonly ODataRawOutputContext rawOutputContext;

		// Token: 0x040004FD RID: 1277
		private bool responseMessageCreated;
	}
}
