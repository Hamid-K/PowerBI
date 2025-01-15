using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.OData
{
	// Token: 0x0200002C RID: 44
	public sealed class ODataAsynchronousWriter : IODataOutputInStreamErrorListener
	{
		// Token: 0x06000124 RID: 292 RVA: 0x0000562B File Offset: 0x0000382B
		internal ODataAsynchronousWriter(ODataRawOutputContext rawOutputContext)
		{
			this.rawOutputContext = rawOutputContext;
			this.container = rawOutputContext.Container;
			this.rawOutputContext.InitializeRawValueWriter();
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00005651 File Offset: 0x00003851
		public ODataAsynchronousResponseMessage CreateResponseMessage()
		{
			this.VerifyCanCreateResponseMessage(true);
			return this.CreateResponseMessageImplementation();
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00005660 File Offset: 0x00003860
		public void Flush()
		{
			this.VerifyCanFlush(true);
			this.rawOutputContext.Flush();
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00005674 File Offset: 0x00003874
		void IODataOutputInStreamErrorListener.OnInStreamError()
		{
			this.rawOutputContext.VerifyNotDisposed();
			this.rawOutputContext.TextWriter.Flush();
			throw new ODataException(Strings.ODataAsyncWriter_CannotWriteInStreamErrorForAsync);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000569B File Offset: 0x0000389B
		private void ValidateWriterNotDisposed()
		{
			this.rawOutputContext.VerifyNotDisposed();
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000056A8 File Offset: 0x000038A8
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall && !this.rawOutputContext.Synchronous)
			{
				throw new ODataException(Strings.ODataAsyncWriter_SyncCallOnAsyncWriter);
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x000056C5 File Offset: 0x000038C5
		private void VerifyCanFlush(bool synchronousCall)
		{
			this.rawOutputContext.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000056D9 File Offset: 0x000038D9
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

		// Token: 0x0600012C RID: 300 RVA: 0x00005714 File Offset: 0x00003914
		private ODataAsynchronousResponseMessage CreateResponseMessageImplementation()
		{
			ODataAsynchronousResponseMessage odataAsynchronousResponseMessage = ODataAsynchronousResponseMessage.CreateMessageForWriting(this.rawOutputContext.OutputStream, new Action<ODataAsynchronousResponseMessage>(this.WriteInnerEnvelope), this.container);
			this.responseMessageCreated = true;
			return odataAsynchronousResponseMessage;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000574C File Offset: 0x0000394C
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

		// Token: 0x040000CD RID: 205
		private readonly ODataRawOutputContext rawOutputContext;

		// Token: 0x040000CE RID: 206
		private readonly IServiceProvider container;

		// Token: 0x040000CF RID: 207
		private bool responseMessageCreated;
	}
}
