using System;
using System.IO;
using System.Text;
using Microsoft.OData.Json;
using Microsoft.Spatial;

namespace Microsoft.OData
{
	// Token: 0x020000C5 RID: 197
	internal sealed class RawValueWriter : IDisposable
	{
		// Token: 0x06000930 RID: 2352 RVA: 0x00016709 File Offset: 0x00014909
		internal RawValueWriter(ODataMessageWriterSettings settings, Stream stream, Encoding encoding)
		{
			this.settings = settings;
			this.stream = stream;
			this.encoding = encoding;
			this.InitializeTextWriter();
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000931 RID: 2353 RVA: 0x0001672C File Offset: 0x0001492C
		internal TextWriter TextWriter
		{
			get
			{
				return this.textWriter;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000932 RID: 2354 RVA: 0x00016734 File Offset: 0x00014934
		internal JsonWriter JsonWriter
		{
			get
			{
				return this.jsonWriter;
			}
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0001673C File Offset: 0x0001493C
		public void Dispose()
		{
			this.textWriter.Dispose();
			this.textWriter = null;
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x00016750 File Offset: 0x00014950
		internal void Start()
		{
			if (this.settings.HasJsonPaddingFunction())
			{
				this.textWriter.Write(this.settings.JsonPCallback);
				this.textWriter.Write("(");
			}
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x00016785 File Offset: 0x00014985
		internal void End()
		{
			if (this.settings.HasJsonPaddingFunction())
			{
				this.textWriter.Write(")");
			}
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x000167A4 File Offset: 0x000149A4
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

		// Token: 0x06000937 RID: 2359 RVA: 0x0001681B File Offset: 0x00014A1B
		internal void Flush()
		{
			if (this.TextWriter != null)
			{
				this.TextWriter.Flush();
			}
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x00016830 File Offset: 0x00014A30
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

		// Token: 0x04000340 RID: 832
		private readonly ODataMessageWriterSettings settings;

		// Token: 0x04000341 RID: 833
		private readonly Stream stream;

		// Token: 0x04000342 RID: 834
		private readonly Encoding encoding;

		// Token: 0x04000343 RID: 835
		private TextWriter textWriter;

		// Token: 0x04000344 RID: 836
		private JsonWriter jsonWriter;
	}
}
