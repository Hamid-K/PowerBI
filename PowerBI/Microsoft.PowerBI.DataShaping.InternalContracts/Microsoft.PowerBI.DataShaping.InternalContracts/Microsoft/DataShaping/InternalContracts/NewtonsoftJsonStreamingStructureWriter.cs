using System;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.DataShaping.ServiceContracts;
using Newtonsoft.Json;

namespace Microsoft.DataShaping.InternalContracts
{
	// Token: 0x0200001F RID: 31
	internal sealed class NewtonsoftJsonStreamingStructureWriter : IStreamingStructureWriter, IDisposable
	{
		// Token: 0x06000092 RID: 146 RVA: 0x00003330 File Offset: 0x00001530
		internal NewtonsoftJsonStreamingStructureWriter(Stream stream, int bufferSizeChars)
		{
			this._stream = stream;
			StreamWriter streamWriter = new StreamWriter(stream, NewtonsoftJsonStreamingStructureWriter.UTF8NoBOM, bufferSizeChars);
			this._writer = new JsonTextWriter(streamWriter);
			this._writer.DateFormatString = "s";
			this._writer.Culture = CultureInfo.InvariantCulture;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003383 File Offset: 0x00001583
		public void BeginObject()
		{
			this._writer.WriteStartObject();
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003390 File Offset: 0x00001590
		public void EndObject()
		{
			this._writer.WriteEndObject();
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000339D File Offset: 0x0000159D
		public void BeginArray()
		{
			this._writer.WriteStartArray();
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000033AA File Offset: 0x000015AA
		public void EndArray()
		{
			this._writer.WriteEndArray();
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000033B7 File Offset: 0x000015B7
		public void BeginProperty(string name)
		{
			this._writer.WritePropertyName(name);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000033C5 File Offset: 0x000015C5
		public void WriteValue(string value)
		{
			this._writer.WriteValue(value);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000033D3 File Offset: 0x000015D3
		public void WriteValue(bool value)
		{
			this._writer.WriteValue(value);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000033E1 File Offset: 0x000015E1
		public void WriteValue(int value)
		{
			this._writer.WriteValue(value);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000033EF File Offset: 0x000015EF
		public void WriteValue(long value)
		{
			this._writer.WriteValue(value);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000033FD File Offset: 0x000015FD
		public void WriteValue(double value)
		{
			this._writer.WriteValue(value);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000340B File Offset: 0x0000160B
		public void WriteValue(decimal value)
		{
			this._writer.WriteValue(value);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003419 File Offset: 0x00001619
		public void WriteValue(DateTimeOffset value)
		{
			this._writer.WriteValue(value);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003427 File Offset: 0x00001627
		public void WriteValue(DateTime value)
		{
			this._writer.WriteValue(value);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003435 File Offset: 0x00001635
		public void Flush()
		{
			this._writer.Flush();
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003442 File Offset: 0x00001642
		public void Dispose()
		{
			if (this._writer != null)
			{
				this._writer.Close();
				this._writer = null;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x0000345E File Offset: 0x0000165E
		public long BytesWritten
		{
			get
			{
				return BytesWrittenStreamExtractor.GetBytesWrittenLength(this._stream);
			}
		}

		// Token: 0x04000059 RID: 89
		private const string DateTimeFormat = "s";

		// Token: 0x0400005A RID: 90
		private static readonly Encoding UTF8NoBOM = new UTF8Encoding(false, true);

		// Token: 0x0400005B RID: 91
		private JsonTextWriter _writer;

		// Token: 0x0400005C RID: 92
		private Stream _stream;
	}
}
