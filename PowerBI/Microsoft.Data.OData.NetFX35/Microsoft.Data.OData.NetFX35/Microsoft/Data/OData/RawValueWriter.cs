using System;
using System.IO;
using System.Text;

namespace Microsoft.Data.OData
{
	// Token: 0x0200015E RID: 350
	internal sealed class RawValueWriter : IDisposable
	{
		// Token: 0x0600094D RID: 2381 RVA: 0x0001D0B4 File Offset: 0x0001B2B4
		internal RawValueWriter(ODataMessageWriterSettings settings, Stream stream, Encoding encoding)
		{
			this.settings = settings;
			this.stream = stream;
			this.encoding = encoding;
			this.InitializeTextWriter();
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x0600094E RID: 2382 RVA: 0x0001D0D7 File Offset: 0x0001B2D7
		internal TextWriter TextWriter
		{
			get
			{
				return this.textWriter;
			}
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x0001D0DF File Offset: 0x0001B2DF
		public void Dispose()
		{
			this.textWriter.Dispose();
			this.textWriter = null;
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x0001D0F3 File Offset: 0x0001B2F3
		internal void Start()
		{
			if (this.settings.HasJsonPaddingFunction())
			{
				this.textWriter.Write(this.settings.JsonPCallback);
				this.textWriter.Write("(");
			}
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x0001D128 File Offset: 0x0001B328
		internal void End()
		{
			if (this.settings.HasJsonPaddingFunction())
			{
				this.textWriter.Write(")");
			}
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x0001D148 File Offset: 0x0001B348
		internal void WriteRawValue(object value)
		{
			string text;
			if (!AtomValueUtils.TryConvertPrimitiveToString(value, out text))
			{
				throw new ODataException(Strings.ODataUtils_CannotConvertValueToRawPrimitive(value.GetType().FullName));
			}
			this.textWriter.Write(text);
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x0001D181 File Offset: 0x0001B381
		internal void Flush()
		{
			if (this.TextWriter != null)
			{
				this.TextWriter.Flush();
			}
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x0001D198 File Offset: 0x0001B398
		private void InitializeTextWriter()
		{
			Stream stream;
			if (MessageStreamWrapper.IsNonDisposingStream(this.stream) || this.stream is AsyncBufferedStream)
			{
				stream = this.stream;
			}
			else
			{
				stream = MessageStreamWrapper.CreateNonDisposingStream(this.stream);
			}
			this.textWriter = new StreamWriter(stream, this.encoding);
		}

		// Token: 0x04000381 RID: 897
		private readonly ODataMessageWriterSettings settings;

		// Token: 0x04000382 RID: 898
		private readonly Stream stream;

		// Token: 0x04000383 RID: 899
		private readonly Encoding encoding;

		// Token: 0x04000384 RID: 900
		private TextWriter textWriter;
	}
}
