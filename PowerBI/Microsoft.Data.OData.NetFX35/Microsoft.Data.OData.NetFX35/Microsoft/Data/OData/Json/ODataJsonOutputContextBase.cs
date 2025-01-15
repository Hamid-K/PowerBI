using System;
using System.IO;
using System.Text;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Json
{
	// Token: 0x02000177 RID: 375
	internal abstract class ODataJsonOutputContextBase : ODataOutputContext
	{
		// Token: 0x06000A5E RID: 2654 RVA: 0x00022B89 File Offset: 0x00020D89
		protected internal ODataJsonOutputContextBase(ODataFormat format, TextWriter textWriter, ODataMessageWriterSettings messageWriterSettings, IEdmModel model)
			: base(format, messageWriterSettings, false, true, model, null)
		{
			this.textWriter = textWriter;
			this.jsonWriter = new JsonWriter(this.textWriter, messageWriterSettings.Indent, format);
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x00022BB8 File Offset: 0x00020DB8
		protected internal ODataJsonOutputContextBase(ODataFormat format, Stream messageStream, Encoding encoding, ODataMessageWriterSettings messageWriterSettings, bool writingResponse, bool synchronous, IEdmModel model, IODataUrlResolver urlResolver)
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
				this.jsonWriter = new JsonWriter(this.textWriter, messageWriterSettings.Indent, format);
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

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000A60 RID: 2656 RVA: 0x00022C44 File Offset: 0x00020E44
		internal IJsonWriter JsonWriter
		{
			get
			{
				return this.jsonWriter;
			}
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x00022C4C File Offset: 0x00020E4C
		internal void VerifyNotDisposed()
		{
			if (this.messageOutputStream == null)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x00022C67 File Offset: 0x00020E67
		internal void Flush()
		{
			this.jsonWriter.Flush();
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x00022C74 File Offset: 0x00020E74
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
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
		}

		// Token: 0x040003F3 RID: 1011
		protected IODataOutputInStreamErrorListener outputInStreamErrorListener;

		// Token: 0x040003F4 RID: 1012
		private Stream messageOutputStream;

		// Token: 0x040003F5 RID: 1013
		private AsyncBufferedStream asynchronousOutputStream;

		// Token: 0x040003F6 RID: 1014
		private TextWriter textWriter;

		// Token: 0x040003F7 RID: 1015
		private IJsonWriter jsonWriter;
	}
}
