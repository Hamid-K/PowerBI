using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using Microsoft.OData.Metadata;

namespace Microsoft.OData
{
	// Token: 0x0200008D RID: 141
	internal sealed class ODataRawOutputContext : ODataOutputContext
	{
		// Token: 0x0600056C RID: 1388 RVA: 0x0000F0C0 File Offset: 0x0000D2C0
		internal ODataRawOutputContext(ODataFormat format, ODataMessageInfo messageInfo, ODataMessageWriterSettings messageWriterSettings)
			: base(format, messageInfo, messageWriterSettings)
		{
			try
			{
				this.messageOutputStream = messageInfo.MessageStream;
				this.encoding = messageInfo.Encoding;
				if (base.Synchronous)
				{
					this.outputStream = this.messageOutputStream;
				}
				else
				{
					this.asynchronousOutputStream = new AsyncBufferedStream(this.messageOutputStream);
					this.outputStream = this.asynchronousOutputStream;
				}
			}
			catch
			{
				this.messageOutputStream.Dispose();
				throw;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x0000F144 File Offset: 0x0000D344
		internal Stream OutputStream
		{
			get
			{
				return this.outputStream;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x0000F14C File Offset: 0x0000D34C
		internal TextWriter TextWriter
		{
			get
			{
				return this.rawValueWriter.TextWriter;
			}
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0000F159 File Offset: 0x0000D359
		internal void Flush()
		{
			if (this.rawValueWriter != null)
			{
				this.rawValueWriter.Flush();
			}
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0000F16E File Offset: 0x0000D36E
		internal override void WriteInStreamError(ODataError error, bool includeDebugInformation)
		{
			if (this.outputInStreamErrorListener != null)
			{
				this.outputInStreamErrorListener.OnInStreamError();
			}
			throw new ODataException(Strings.ODataMessageWriter_CannotWriteInStreamErrorForRawValues);
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0000F18D File Offset: 0x0000D38D
		internal override ODataBatchWriter CreateODataBatchWriter(string batchBoundary)
		{
			return this.CreateODataBatchWriterImplementation(batchBoundary);
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0000F196 File Offset: 0x0000D396
		internal override ODataAsynchronousWriter CreateODataAsynchronousWriter()
		{
			return this.CreateODataAsynchronousWriterImplementation();
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0000F19E File Offset: 0x0000D39E
		internal override void WriteValue(object value)
		{
			this.WriteValueImplementation(value);
			this.Flush();
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0000F1AD File Offset: 0x0000D3AD
		internal void InitializeRawValueWriter()
		{
			this.rawValueWriter = new RawValueWriter(base.MessageWriterSettings, this.outputStream, this.encoding);
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0000F1CC File Offset: 0x0000D3CC
		internal void CloseWriter()
		{
			this.rawValueWriter.Dispose();
			this.rawValueWriter = null;
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0000F1E0 File Offset: 0x0000D3E0
		internal void VerifyNotDisposed()
		{
			if (this.messageOutputStream == null)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0000F1FB File Offset: 0x0000D3FB
		internal void FlushBuffers()
		{
			if (this.asynchronousOutputStream != null)
			{
				this.asynchronousOutputStream.FlushSync();
			}
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0000F210 File Offset: 0x0000D410
		[SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "rawValueWriter", Justification = "We intentionally don't dispose rawValueWriter, we instead dispose the underlying stream manually.")]
		protected override void Dispose(bool disposing)
		{
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
			base.Dispose(disposing);
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0000F298 File Offset: 0x0000D498
		private void WriteValueImplementation(object value)
		{
			byte[] array = value as byte[];
			if (array != null)
			{
				this.OutputStream.Write(array, 0, array.Length);
				return;
			}
			value = base.Model.ConvertToUnderlyingTypeIfUIntValue(value, null);
			this.InitializeRawValueWriter();
			this.rawValueWriter.Start();
			this.rawValueWriter.WriteRawValue(value);
			this.rawValueWriter.End();
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0000F2F8 File Offset: 0x0000D4F8
		private ODataBatchWriter CreateODataBatchWriterImplementation(string batchBoundary)
		{
			this.encoding = this.encoding ?? MediaTypeUtils.EncodingUtf8NoPreamble;
			ODataBatchWriter odataBatchWriter = new ODataBatchWriter(this, batchBoundary);
			this.outputInStreamErrorListener = odataBatchWriter;
			return odataBatchWriter;
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0000F32C File Offset: 0x0000D52C
		private ODataAsynchronousWriter CreateODataAsynchronousWriterImplementation()
		{
			this.encoding = this.encoding ?? MediaTypeUtils.EncodingUtf8NoPreamble;
			ODataAsynchronousWriter odataAsynchronousWriter = new ODataAsynchronousWriter(this);
			this.outputInStreamErrorListener = odataAsynchronousWriter;
			return odataAsynchronousWriter;
		}

		// Token: 0x0400029D RID: 669
		private Encoding encoding;

		// Token: 0x0400029E RID: 670
		private Stream messageOutputStream;

		// Token: 0x0400029F RID: 671
		private AsyncBufferedStream asynchronousOutputStream;

		// Token: 0x040002A0 RID: 672
		private Stream outputStream;

		// Token: 0x040002A1 RID: 673
		private IODataOutputInStreamErrorListener outputInStreamErrorListener;

		// Token: 0x040002A2 RID: 674
		private RawValueWriter rawValueWriter;
	}
}
