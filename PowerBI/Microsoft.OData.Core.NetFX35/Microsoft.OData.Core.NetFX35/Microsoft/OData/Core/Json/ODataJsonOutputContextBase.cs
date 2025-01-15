using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.Json
{
	// Token: 0x020000E4 RID: 228
	internal abstract class ODataJsonOutputContextBase : ODataOutputContext
	{
		// Token: 0x060008A0 RID: 2208 RVA: 0x0001FE74 File Offset: 0x0001E074
		protected internal ODataJsonOutputContextBase(ODataFormat format, TextWriter textWriter, ODataMessageWriterSettings messageWriterSettings, IEdmModel model)
			: base(format, messageWriterSettings, false, true, model, null)
		{
			this.textWriter = textWriter;
			this.jsonWriter = new JsonWriter(this.textWriter, messageWriterSettings.Indent, format, true);
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0001FEA4 File Offset: 0x0001E0A4
		[SuppressMessage("DataWeb.Usage", "AC0014", Justification = "Throws every time")]
		protected internal ODataJsonOutputContextBase(ODataFormat format, Stream messageStream, Encoding encoding, ODataMessageWriterSettings messageWriterSettings, bool writingResponse, bool synchronous, bool isIeee754Compatible, IEdmModel model, IODataUrlResolver urlResolver)
			: base(format, messageWriterSettings, writingResponse, synchronous, model, urlResolver)
		{
			try
			{
				this.messageOutputStream = messageStream;
				Stream stream;
				if (synchronous)
				{
					stream = messageStream;
				}
				else
				{
					this.asynchronousOutputStream = new AsyncBufferedStream(messageStream);
					stream = this.asynchronousOutputStream;
				}
				this.textWriter = new StreamWriter(stream, encoding);
				this.jsonWriter = new JsonWriter(this.textWriter, messageWriterSettings.Indent, format, isIeee754Compatible);
			}
			catch (Exception ex)
			{
				if (ExceptionUtils.IsCatchableExceptionType(ex) && messageStream != null)
				{
					messageStream.Dispose();
				}
				throw;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060008A2 RID: 2210 RVA: 0x0001FF34 File Offset: 0x0001E134
		internal IJsonWriter JsonWriter
		{
			get
			{
				return this.jsonWriter;
			}
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0001FF3C File Offset: 0x0001E13C
		internal void VerifyNotDisposed()
		{
			if (this.messageOutputStream == null)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x0001FF57 File Offset: 0x0001E157
		internal void Flush()
		{
			this.jsonWriter.Flush();
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x0001FF64 File Offset: 0x0001E164
		[SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "textWriter", Justification = "We don't dispose the jsonWriter or textWriter, instead we dispose the underlying stream directly.")]
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (this.messageOutputStream != null)
				{
					this.jsonWriter.Flush();
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
				this.textWriter = null;
				this.jsonWriter = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x0400038B RID: 907
		protected IODataOutputInStreamErrorListener outputInStreamErrorListener;

		// Token: 0x0400038C RID: 908
		private Stream messageOutputStream;

		// Token: 0x0400038D RID: 909
		private AsyncBufferedStream asynchronousOutputStream;

		// Token: 0x0400038E RID: 910
		private TextWriter textWriter;

		// Token: 0x0400038F RID: 911
		private IJsonWriter jsonWriter;
	}
}
