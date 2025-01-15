using System;
using System.IO;
using Microsoft.Lucia.Common;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Json
{
	// Token: 0x0200002F RID: 47
	public sealed class JsonLinesReader : IDisposable
	{
		// Token: 0x060000BD RID: 189 RVA: 0x0000374C File Offset: 0x0000194C
		private JsonLinesReader(TextReader textReader, [Nullable] JsonSerializer serializer)
		{
			this._textReader = textReader;
			this._serializer = serializer ?? JsonSerializer.Create();
			this._serializer.MetadataPropertyHandling = MetadataPropertyHandling.Ignore;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003777 File Offset: 0x00001977
		public static JsonLinesReader Create(Stream stream, [Nullable] JsonSerializer serializer = null, bool leaveOpen = false)
		{
			return JsonLinesReader.Create(StreamReaderFactory.Create(stream, leaveOpen), serializer);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003786 File Offset: 0x00001986
		public static JsonLinesReader Create(TextReader reader, [Nullable] JsonSerializer serializer = null)
		{
			return new JsonLinesReader(reader, serializer);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000378F File Offset: 0x0000198F
		public void Dispose()
		{
			this._textReader.Dispose();
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x0000379C File Offset: 0x0000199C
		public T ReadLineOrDefault<T>()
		{
			T t;
			if (!this.TryReadLine<T>(out t))
			{
				return default(T);
			}
			return t;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000037C0 File Offset: 0x000019C0
		public bool TryReadLine<T>(out T item)
		{
			string text = this.ReadLineRaw();
			if (text == null)
			{
				item = default(T);
				return false;
			}
			item = (T)((object)this._serializer.Deserialize(new StringReader(text), typeof(T)));
			return true;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003807 File Offset: 0x00001A07
		public string ReadLineRaw()
		{
			return this._textReader.ReadLine();
		}

		// Token: 0x04000051 RID: 81
		private readonly TextReader _textReader;

		// Token: 0x04000052 RID: 82
		private readonly JsonSerializer _serializer;
	}
}
