using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.OData.Metadata;

namespace Microsoft.OData
{
	// Token: 0x020000B1 RID: 177
	internal class ODataRawOutputContext : ODataOutputContext
	{
		// Token: 0x060007A5 RID: 1957 RVA: 0x000127B4 File Offset: 0x000109B4
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

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060007A6 RID: 1958 RVA: 0x00012838 File Offset: 0x00010A38
		internal Stream OutputStream
		{
			get
			{
				return this.outputStream;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x00012840 File Offset: 0x00010A40
		internal TextWriter TextWriter
		{
			get
			{
				return this.rawValueWriter.TextWriter;
			}
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x0001284D File Offset: 0x00010A4D
		internal void Flush()
		{
			if (this.rawValueWriter != null)
			{
				this.rawValueWriter.Flush();
			}
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x00012862 File Offset: 0x00010A62
		internal Task FlushAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperationReturningTask(delegate
			{
				if (this.rawValueWriter != null)
				{
					this.rawValueWriter.Flush();
				}
				return this.asynchronousOutputStream.FlushAsync();
			}).FollowOnSuccessWithTask((Task asyncBufferedStreamFlushTask) => this.messageOutputStream.FlushAsync());
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x00012886 File Offset: 0x00010A86
		internal override void WriteInStreamError(ODataError error, bool includeDebugInformation)
		{
			if (this.outputInStreamErrorListener != null)
			{
				this.outputInStreamErrorListener.OnInStreamError();
			}
			throw new ODataException(Strings.ODataMessageWriter_CannotWriteInStreamErrorForRawValues);
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x00012886 File Offset: 0x00010A86
		internal override Task WriteInStreamErrorAsync(ODataError error, bool includeDebugInformation)
		{
			if (this.outputInStreamErrorListener != null)
			{
				this.outputInStreamErrorListener.OnInStreamError();
			}
			throw new ODataException(Strings.ODataMessageWriter_CannotWriteInStreamErrorForRawValues);
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x000128A5 File Offset: 0x00010AA5
		internal override ODataAsynchronousWriter CreateODataAsynchronousWriter()
		{
			return this.CreateODataAsynchronousWriterImplementation();
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x000128AD File Offset: 0x00010AAD
		internal override Task<ODataAsynchronousWriter> CreateODataAsynchronousWriterAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<ODataAsynchronousWriter>(() => this.CreateODataAsynchronousWriterImplementation());
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x000128C0 File Offset: 0x00010AC0
		internal override void WriteValue(object value)
		{
			this.WriteValueImplementation(value);
			this.Flush();
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x000128D0 File Offset: 0x00010AD0
		internal override Task WriteValueAsync(object value)
		{
			return TaskUtils.GetTaskForSynchronousOperationReturningTask(delegate
			{
				this.WriteValueImplementation(value);
				return this.FlushAsync();
			});
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x00012902 File Offset: 0x00010B02
		internal void InitializeRawValueWriter()
		{
			this.rawValueWriter = new RawValueWriter(base.MessageWriterSettings, this.outputStream, this.encoding);
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x00012921 File Offset: 0x00010B21
		internal void CloseWriter()
		{
			this.rawValueWriter.Dispose();
			this.rawValueWriter = null;
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x00012935 File Offset: 0x00010B35
		internal void VerifyNotDisposed()
		{
			if (this.messageOutputStream == null)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x00012950 File Offset: 0x00010B50
		internal void FlushBuffers()
		{
			if (this.asynchronousOutputStream != null)
			{
				this.asynchronousOutputStream.FlushSync();
			}
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x00012965 File Offset: 0x00010B65
		internal Task FlushBuffersAsync()
		{
			if (this.asynchronousOutputStream != null)
			{
				return this.asynchronousOutputStream.FlushAsync();
			}
			return TaskUtils.CompletedTask;
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x00012980 File Offset: 0x00010B80
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

		// Token: 0x060007B6 RID: 1974 RVA: 0x00012A08 File Offset: 0x00010C08
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

		// Token: 0x060007B7 RID: 1975 RVA: 0x00012A68 File Offset: 0x00010C68
		private ODataAsynchronousWriter CreateODataAsynchronousWriterImplementation()
		{
			this.encoding = this.encoding ?? MediaTypeUtils.EncodingUtf8NoPreamble;
			ODataAsynchronousWriter odataAsynchronousWriter = new ODataAsynchronousWriter(this);
			this.outputInStreamErrorListener = odataAsynchronousWriter;
			return odataAsynchronousWriter;
		}

		// Token: 0x04000300 RID: 768
		protected Encoding encoding;

		// Token: 0x04000301 RID: 769
		protected IODataOutputInStreamErrorListener outputInStreamErrorListener;

		// Token: 0x04000302 RID: 770
		private Stream messageOutputStream;

		// Token: 0x04000303 RID: 771
		private AsyncBufferedStream asynchronousOutputStream;

		// Token: 0x04000304 RID: 772
		private Stream outputStream;

		// Token: 0x04000305 RID: 773
		private RawValueWriter rawValueWriter;
	}
}
