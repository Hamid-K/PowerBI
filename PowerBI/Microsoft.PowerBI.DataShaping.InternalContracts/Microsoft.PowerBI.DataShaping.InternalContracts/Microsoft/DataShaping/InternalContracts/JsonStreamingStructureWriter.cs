using System;
using System.IO;
using Microsoft.DataShaping.Common.Json;
using Microsoft.DataShaping.ServiceContracts;

namespace Microsoft.DataShaping.InternalContracts
{
	// Token: 0x0200001D RID: 29
	internal sealed class JsonStreamingStructureWriter : IStreamingStructureEncodedWriter, IStreamingStructureWriter, IDisposable
	{
		// Token: 0x06000068 RID: 104 RVA: 0x000030E0 File Offset: 0x000012E0
		internal JsonStreamingStructureWriter(Stream stream)
		{
			this._stream = stream;
			this._writer = new JsonWriter(new StreamWriter(stream)
			{
				AutoFlush = true
			}, false);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003115 File Offset: 0x00001315
		public void BeginObject()
		{
			this._writer.StartObjectScope();
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003122 File Offset: 0x00001322
		public void EndObject()
		{
			this._writer.EndObjectScope();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x0000312F File Offset: 0x0000132F
		public void BeginArray()
		{
			this._writer.StartArrayScope();
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000313C File Offset: 0x0000133C
		public void EndArray()
		{
			this._writer.EndArrayScope();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003149 File Offset: 0x00001349
		public void BeginProperty(string name)
		{
			this._writer.WriteName(name);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003157 File Offset: 0x00001357
		public void WriteTypeEncodedProperty(string name, object value)
		{
			this.BeginProperty(name);
			this.WriteTypeEncodedValue(value);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003167 File Offset: 0x00001367
		public void WriteSimpleEncodedProperty(string name, object value)
		{
			this.BeginProperty(name);
			this.WriteSimpleEncodedValue(value);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003177 File Offset: 0x00001377
		public void WriteProperty(string name, string value)
		{
			this.BeginProperty(name);
			this.WriteValue(value);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003187 File Offset: 0x00001387
		public void WriteJsonEncodedProperty(string name, string value)
		{
			this.BeginProperty(name);
			this.WriteJsonEncodedValue(value);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003197 File Offset: 0x00001397
		public void WriteJsonEncodedStringProperty(string name, string value)
		{
			this.BeginProperty(name);
			this.WriteJsonEncodedString(value);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000031A7 File Offset: 0x000013A7
		public void WriteProperty(string name, bool value)
		{
			this.BeginProperty(name);
			this.WriteValue(value);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000031B7 File Offset: 0x000013B7
		public void WriteProperty(string name, int value)
		{
			this.BeginProperty(name);
			this.WriteValue(value);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000031C7 File Offset: 0x000013C7
		public void WriteProperty(string name, DateTimeOffset value)
		{
			this.BeginProperty(name);
			this.WriteValue(value);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000031D7 File Offset: 0x000013D7
		public void WriteTypeEncodedValue(object value)
		{
			this._writer.WriteTypeEncodedValue(value);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000031E5 File Offset: 0x000013E5
		public void WriteSimpleEncodedValue(object value)
		{
			this._writer.WriteSimpleEncodedValue(value);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000031F3 File Offset: 0x000013F3
		public void WriteJsonEncodedValue(string value)
		{
			this._writer.WriteJsonEncodedValue(value);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003201 File Offset: 0x00001401
		public void WriteJsonEncodedString(string value)
		{
			this._writer.WriteJsonEncodedString(value);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000320F File Offset: 0x0000140F
		public void WriteValue(string value)
		{
			this._writer.WriteValue(value);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000321D File Offset: 0x0000141D
		public void WriteValue(bool value)
		{
			this._writer.WriteValue(value);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000322B File Offset: 0x0000142B
		public void WriteValue(int value)
		{
			this._writer.WriteValue(value);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003239 File Offset: 0x00001439
		public void WriteValue(long value)
		{
			this._writer.WriteValue(value);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003247 File Offset: 0x00001447
		public void WriteValue(double value)
		{
			this._writer.WriteValue(value);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003255 File Offset: 0x00001455
		public void WriteValue(decimal value)
		{
			this._writer.WriteValue(value);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003263 File Offset: 0x00001463
		public void WriteValue(DateTimeOffset value)
		{
			this._writer.WriteValue(value);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003271 File Offset: 0x00001471
		public void WriteValue(DateTime value)
		{
			this._writer.WriteValue(value);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003284 File Offset: 0x00001484
		public void Flush()
		{
			this._writer.Flush();
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003291 File Offset: 0x00001491
		public void Dispose()
		{
			if (this._writer != null)
			{
				this._writer.Close();
				this._writer = null;
				this._stream = null;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000084 RID: 132 RVA: 0x000032B4 File Offset: 0x000014B4
		public long BytesWritten
		{
			get
			{
				return BytesWrittenStreamExtractor.GetBytesWrittenLength(this._stream);
			}
		}

		// Token: 0x04000051 RID: 81
		private JsonWriter _writer;

		// Token: 0x04000052 RID: 82
		private Stream _stream;
	}
}
