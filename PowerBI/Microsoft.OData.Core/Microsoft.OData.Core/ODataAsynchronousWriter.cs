using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Microsoft.OData
{
	// Token: 0x02000054 RID: 84
	public sealed class ODataAsynchronousWriter : IODataOutputInStreamErrorListener
	{
		// Token: 0x0600029E RID: 670 RVA: 0x000081D8 File Offset: 0x000063D8
		internal ODataAsynchronousWriter(ODataRawOutputContext rawOutputContext)
		{
			this.rawOutputContext = rawOutputContext;
			this.container = rawOutputContext.Container;
			this.rawOutputContext.InitializeRawValueWriter();
		}

		// Token: 0x0600029F RID: 671 RVA: 0x000081FE File Offset: 0x000063FE
		public ODataAsynchronousResponseMessage CreateResponseMessage()
		{
			this.VerifyCanCreateResponseMessage(true);
			return this.CreateResponseMessageImplementation();
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000820D File Offset: 0x0000640D
		public Task<ODataAsynchronousResponseMessage> CreateResponseMessageAsync()
		{
			this.VerifyCanCreateResponseMessage(false);
			return TaskUtils.GetTaskForSynchronousOperation<ODataAsynchronousResponseMessage>(() => this.CreateResponseMessageImplementation());
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00008227 File Offset: 0x00006427
		public void Flush()
		{
			this.VerifyCanFlush(true);
			this.rawOutputContext.Flush();
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000823B File Offset: 0x0000643B
		public Task FlushAsync()
		{
			this.VerifyCanFlush(false);
			return this.rawOutputContext.FlushAsync();
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000824F File Offset: 0x0000644F
		void IODataOutputInStreamErrorListener.OnInStreamError()
		{
			this.rawOutputContext.VerifyNotDisposed();
			this.rawOutputContext.TextWriter.Flush();
			throw new ODataException(Strings.ODataAsyncWriter_CannotWriteInStreamErrorForAsync);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00008276 File Offset: 0x00006476
		private void ValidateWriterNotDisposed()
		{
			this.rawOutputContext.VerifyNotDisposed();
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00008283 File Offset: 0x00006483
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall)
			{
				if (!this.rawOutputContext.Synchronous)
				{
					throw new ODataException(Strings.ODataAsyncWriter_SyncCallOnAsyncWriter);
				}
			}
			else if (this.rawOutputContext.Synchronous)
			{
				throw new ODataException(Strings.ODataAsyncWriter_AsyncCallOnSyncWriter);
			}
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x000082B8 File Offset: 0x000064B8
		private void VerifyCanFlush(bool synchronousCall)
		{
			this.rawOutputContext.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x000082CC File Offset: 0x000064CC
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

		// Token: 0x060002A8 RID: 680 RVA: 0x00008308 File Offset: 0x00006508
		private ODataAsynchronousResponseMessage CreateResponseMessageImplementation()
		{
			ODataAsynchronousResponseMessage odataAsynchronousResponseMessage = ODataAsynchronousResponseMessage.CreateMessageForWriting(this.rawOutputContext.OutputStream, new Action<ODataAsynchronousResponseMessage>(this.WriteInnerEnvelope), this.container);
			this.responseMessageCreated = true;
			return odataAsynchronousResponseMessage;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00008340 File Offset: 0x00006540
		private void WriteInnerEnvelope(ODataAsynchronousResponseMessage responseMessage)
		{
			string statusMessage = HttpUtils.GetStatusMessage(responseMessage.StatusCode);
			this.rawOutputContext.TextWriter.WriteLine("{0} {1} {2}", new object[] { "HTTP/1.1", responseMessage.StatusCode, statusMessage });
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

		// Token: 0x04000137 RID: 311
		private readonly ODataRawOutputContext rawOutputContext;

		// Token: 0x04000138 RID: 312
		private readonly IServiceProvider container;

		// Token: 0x04000139 RID: 313
		private bool responseMessageCreated;
	}
}
