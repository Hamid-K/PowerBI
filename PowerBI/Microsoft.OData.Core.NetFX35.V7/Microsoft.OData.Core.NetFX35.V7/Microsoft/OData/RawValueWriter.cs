using System;
using System.IO;
using System.Text;
using Microsoft.OData.Json;
using Microsoft.Spatial;

namespace Microsoft.OData
{
	// Token: 0x020000A7 RID: 167
	internal sealed class RawValueWriter : IDisposable
	{
		// Token: 0x0600067A RID: 1658 RVA: 0x00011E49 File Offset: 0x00010049
		internal RawValueWriter(ODataMessageWriterSettings settings, Stream stream, Encoding encoding)
		{
			this.settings = settings;
			this.stream = stream;
			this.encoding = encoding;
			this.InitializeTextWriter();
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x00011E6C File Offset: 0x0001006C
		internal TextWriter TextWriter
		{
			get
			{
				return this.textWriter;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x00011E74 File Offset: 0x00010074
		internal JsonWriter JsonWriter
		{
			get
			{
				return this.jsonWriter;
			}
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x00011E7C File Offset: 0x0001007C
		public void Dispose()
		{
			this.textWriter.Dispose();
			this.textWriter = null;
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00011E90 File Offset: 0x00010090
		internal void Start()
		{
			if (this.settings.HasJsonPaddingFunction())
			{
				this.textWriter.Write(this.settings.JsonPCallback);
				this.textWriter.Write("(");
			}
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00011EC5 File Offset: 0x000100C5
		internal void End()
		{
			if (this.settings.HasJsonPaddingFunction())
			{
				this.textWriter.Write(")");
			}
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x00011EE4 File Offset: 0x000100E4
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
				PrimitiveConverter.Instance.WriteJsonLight(value, this.jsonWriter);
				return;
			}
			string text;
			if (ODataRawValueUtils.TryConvertPrimitiveToString(value, out text))
			{
				this.textWriter.Write(text);
				return;
			}
			throw new ODataException(Strings.ODataUtils_CannotConvertValueToRawString(value.GetType().FullName));
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x00011F5B File Offset: 0x0001015B
		internal void Flush()
		{
			if (this.TextWriter != null)
			{
				this.TextWriter.Flush();
			}
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x00011F70 File Offset: 0x00010170
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
			this.jsonWriter = new JsonWriter(this.textWriter, false);
		}

		// Token: 0x040002E0 RID: 736
		private readonly ODataMessageWriterSettings settings;

		// Token: 0x040002E1 RID: 737
		private readonly Stream stream;

		// Token: 0x040002E2 RID: 738
		private readonly Encoding encoding;

		// Token: 0x040002E3 RID: 739
		private TextWriter textWriter;

		// Token: 0x040002E4 RID: 740
		private JsonWriter jsonWriter;
	}
}
