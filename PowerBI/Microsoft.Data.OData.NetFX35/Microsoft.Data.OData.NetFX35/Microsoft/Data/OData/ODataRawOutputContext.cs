using System;
using System.IO;
using System.Text;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData
{
	// Token: 0x020001CC RID: 460
	internal sealed class ODataRawOutputContext : ODataOutputContext
	{
		// Token: 0x06000D8D RID: 3469 RVA: 0x000303C0 File Offset: 0x0002E5C0
		internal ODataRawOutputContext(ODataFormat format, Stream messageStream, Encoding encoding, ODataMessageWriterSettings messageWriterSettings, bool writingResponse, bool synchronous, IEdmModel model, IODataUrlResolver urlResolver)
			: base(format, messageWriterSettings, writingResponse, synchronous, model, urlResolver)
		{
			try
			{
				this.messageOutputStream = messageStream;
				this.encoding = encoding;
				if (synchronous)
				{
					this.outputStream = messageStream;
				}
				else
				{
					this.asynchronousOutputStream = new AsyncBufferedStream(messageStream);
					this.outputStream = this.asynchronousOutputStream;
				}
			}
			catch
			{
				messageStream.Dispose();
				throw;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000D8E RID: 3470 RVA: 0x0003042C File Offset: 0x0002E62C
		internal Stream OutputStream
		{
			get
			{
				return this.outputStream;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000D8F RID: 3471 RVA: 0x00030434 File Offset: 0x0002E634
		internal TextWriter TextWriter
		{
			get
			{
				return this.rawValueWriter.TextWriter;
			}
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x00030441 File Offset: 0x0002E641
		internal void Flush()
		{
			if (this.rawValueWriter != null)
			{
				this.rawValueWriter.Flush();
			}
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x00030456 File Offset: 0x0002E656
		internal override void WriteInStreamError(ODataError error, bool includeDebugInformation)
		{
			if (this.outputInStreamErrorListener != null)
			{
				this.outputInStreamErrorListener.OnInStreamError();
			}
			throw new ODataException(Strings.ODataMessageWriter_CannotWriteInStreamErrorForRawValues);
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x00030475 File Offset: 0x0002E675
		internal override ODataBatchWriter CreateODataBatchWriter(string batchBoundary)
		{
			return this.CreateODataBatchWriterImplementation(batchBoundary);
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x0003047E File Offset: 0x0002E67E
		internal override void WriteValue(object value)
		{
			this.WriteValueImplementation(value);
			this.Flush();
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x0003048D File Offset: 0x0002E68D
		internal void InitializeRawValueWriter()
		{
			this.rawValueWriter = new RawValueWriter(base.MessageWriterSettings, this.outputStream, this.encoding);
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x000304AC File Offset: 0x0002E6AC
		internal void CloseWriter()
		{
			this.rawValueWriter.Dispose();
			this.rawValueWriter = null;
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x000304C0 File Offset: 0x0002E6C0
		internal void VerifyNotDisposed()
		{
			if (this.messageOutputStream == null)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x000304DB File Offset: 0x0002E6DB
		internal void FlushBuffers()
		{
			if (this.asynchronousOutputStream != null)
			{
				this.asynchronousOutputStream.FlushSync();
			}
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x000304F0 File Offset: 0x0002E6F0
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			try
			{
				if (this.messageOutputStream != null)
				{
					if (this.rawValueWriter != null)
					{
						this.rawValueWriter.Flush();
					}
					if (this.asynchronousOutputStream != null)
					{
						this.asynchronousOutputStream.FlushSync();
						this.asynchronousOutputStream.Dispose();
					}
					this.messageOutputStream.Dispose();
				}
			}
			finally
			{
				this.messageOutputStream = null;
				this.asynchronousOutputStream = null;
				this.outputStream = null;
				this.rawValueWriter = null;
			}
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x00030578 File Offset: 0x0002E778
		private void WriteValueImplementation(object value)
		{
			byte[] array = value as byte[];
			if (array != null)
			{
				this.OutputStream.Write(array, 0, array.Length);
				return;
			}
			this.InitializeRawValueWriter();
			this.rawValueWriter.Start();
			this.rawValueWriter.WriteRawValue(value);
			this.rawValueWriter.End();
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x000305C8 File Offset: 0x0002E7C8
		private ODataBatchWriter CreateODataBatchWriterImplementation(string batchBoundary)
		{
			this.encoding = this.encoding ?? MediaTypeUtils.EncodingUtf8NoPreamble;
			ODataBatchWriter odataBatchWriter = new ODataBatchWriter(this, batchBoundary);
			this.outputInStreamErrorListener = odataBatchWriter;
			return odataBatchWriter;
		}

		// Token: 0x040004B3 RID: 1203
		private Encoding encoding;

		// Token: 0x040004B4 RID: 1204
		private Stream messageOutputStream;

		// Token: 0x040004B5 RID: 1205
		private AsyncBufferedStream asynchronousOutputStream;

		// Token: 0x040004B6 RID: 1206
		private Stream outputStream;

		// Token: 0x040004B7 RID: 1207
		private IODataOutputInStreamErrorListener outputInStreamErrorListener;

		// Token: 0x040004B8 RID: 1208
		private RawValueWriter rawValueWriter;
	}
}
