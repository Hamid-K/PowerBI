using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core
{
	// Token: 0x02000197 RID: 407
	internal sealed class ODataRawOutputContext : ODataOutputContext
	{
		// Token: 0x06000F43 RID: 3907 RVA: 0x0003542C File Offset: 0x0003362C
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

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000F44 RID: 3908 RVA: 0x00035498 File Offset: 0x00033698
		internal Stream OutputStream
		{
			get
			{
				return this.outputStream;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000F45 RID: 3909 RVA: 0x000354A0 File Offset: 0x000336A0
		internal TextWriter TextWriter
		{
			get
			{
				return this.rawValueWriter.TextWriter;
			}
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x000354AD File Offset: 0x000336AD
		internal void Flush()
		{
			if (this.rawValueWriter != null)
			{
				this.rawValueWriter.Flush();
			}
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x000354C2 File Offset: 0x000336C2
		internal override void WriteInStreamError(ODataError error, bool includeDebugInformation)
		{
			if (this.outputInStreamErrorListener != null)
			{
				this.outputInStreamErrorListener.OnInStreamError();
			}
			throw new ODataException(Strings.ODataMessageWriter_CannotWriteInStreamErrorForRawValues);
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x000354E1 File Offset: 0x000336E1
		internal override ODataBatchWriter CreateODataBatchWriter(string batchBoundary)
		{
			return this.CreateODataBatchWriterImplementation(batchBoundary);
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x000354EA File Offset: 0x000336EA
		internal override ODataAsynchronousWriter CreateODataAsynchronousWriter()
		{
			return this.CreateODataAsynchronousWriterImplementation();
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x000354F2 File Offset: 0x000336F2
		internal override void WriteValue(object value)
		{
			this.WriteValueImplementation(value);
			this.Flush();
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x00035501 File Offset: 0x00033701
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "We create a NonDisposingStream which doesn't need to be disposed, even though it's IDisposable.")]
		internal void InitializeRawValueWriter()
		{
			this.rawValueWriter = new RawValueWriter(base.MessageWriterSettings, this.outputStream, this.encoding);
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x00035520 File Offset: 0x00033720
		internal void CloseWriter()
		{
			this.rawValueWriter.Dispose();
			this.rawValueWriter = null;
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x00035534 File Offset: 0x00033734
		internal void VerifyNotDisposed()
		{
			if (this.messageOutputStream == null)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x0003554F File Offset: 0x0003374F
		internal void FlushBuffers()
		{
			if (this.asynchronousOutputStream != null)
			{
				this.asynchronousOutputStream.FlushSync();
			}
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x00035564 File Offset: 0x00033764
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

		// Token: 0x06000F50 RID: 3920 RVA: 0x000355EC File Offset: 0x000337EC
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

		// Token: 0x06000F51 RID: 3921 RVA: 0x0003564C File Offset: 0x0003384C
		private ODataBatchWriter CreateODataBatchWriterImplementation(string batchBoundary)
		{
			this.encoding = this.encoding ?? MediaTypeUtils.EncodingUtf8NoPreamble;
			ODataBatchWriter odataBatchWriter = new ODataBatchWriter(this, batchBoundary);
			this.outputInStreamErrorListener = odataBatchWriter;
			return odataBatchWriter;
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x00035680 File Offset: 0x00033880
		private ODataAsynchronousWriter CreateODataAsynchronousWriterImplementation()
		{
			this.encoding = this.encoding ?? MediaTypeUtils.EncodingUtf8NoPreamble;
			ODataAsynchronousWriter odataAsynchronousWriter = new ODataAsynchronousWriter(this);
			this.outputInStreamErrorListener = odataAsynchronousWriter;
			return odataAsynchronousWriter;
		}

		// Token: 0x040006AE RID: 1710
		private Encoding encoding;

		// Token: 0x040006AF RID: 1711
		private Stream messageOutputStream;

		// Token: 0x040006B0 RID: 1712
		private AsyncBufferedStream asynchronousOutputStream;

		// Token: 0x040006B1 RID: 1713
		private Stream outputStream;

		// Token: 0x040006B2 RID: 1714
		private IODataOutputInStreamErrorListener outputInStreamErrorListener;

		// Token: 0x040006B3 RID: 1715
		private RawValueWriter rawValueWriter;
	}
}
