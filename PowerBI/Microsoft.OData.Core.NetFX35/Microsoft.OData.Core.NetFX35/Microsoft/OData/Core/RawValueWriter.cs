using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using Microsoft.Spatial;

namespace Microsoft.OData.Core
{
	// Token: 0x020001AF RID: 431
	internal sealed class RawValueWriter : IDisposable
	{
		// Token: 0x06000FFF RID: 4095 RVA: 0x000376EE File Offset: 0x000358EE
		internal RawValueWriter(ODataMessageWriterSettings settings, Stream stream, Encoding encoding)
		{
			this.settings = settings;
			this.stream = stream;
			this.encoding = encoding;
			this.InitializeTextWriter();
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06001000 RID: 4096 RVA: 0x00037711 File Offset: 0x00035911
		internal TextWriter TextWriter
		{
			get
			{
				return this.textWriter;
			}
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x00037719 File Offset: 0x00035919
		public void Dispose()
		{
			this.textWriter.Dispose();
			this.textWriter = null;
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x0003772D File Offset: 0x0003592D
		internal void Start()
		{
			if (this.settings.HasJsonPaddingFunction())
			{
				this.textWriter.Write(this.settings.JsonPCallback);
				this.textWriter.Write("(");
			}
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x00037762 File Offset: 0x00035962
		internal void End()
		{
			if (this.settings.HasJsonPaddingFunction())
			{
				this.textWriter.Write(")");
			}
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x00037784 File Offset: 0x00035984
		internal void WriteRawValue(object value)
		{
			ODataEnumValue odataEnumValue = value as ODataEnumValue;
			if (odataEnumValue != null)
			{
				this.textWriter.Write(odataEnumValue.Value);
				return;
			}
			if (value is Geometry || value is Geography)
			{
				PrimitiveConverter.Instance.TryWriteAtom(value, this.textWriter);
				return;
			}
			string text;
			if (AtomValueUtils.TryConvertPrimitiveToString(value, out text))
			{
				this.textWriter.Write(text);
				return;
			}
			throw new ODataException(Strings.ODataUtils_CannotConvertValueToRawString(value.GetType().FullName));
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x000377FC File Offset: 0x000359FC
		internal void Flush()
		{
			if (this.TextWriter != null)
			{
				this.TextWriter.Flush();
			}
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x00037814 File Offset: 0x00035A14
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "We create a NonDisposingStream which doesn't need to be disposed, even though it's IDisposable.")]
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

		// Token: 0x04000749 RID: 1865
		private readonly ODataMessageWriterSettings settings;

		// Token: 0x0400074A RID: 1866
		private readonly Stream stream;

		// Token: 0x0400074B RID: 1867
		private readonly Encoding encoding;

		// Token: 0x0400074C RID: 1868
		private TextWriter textWriter;
	}
}
