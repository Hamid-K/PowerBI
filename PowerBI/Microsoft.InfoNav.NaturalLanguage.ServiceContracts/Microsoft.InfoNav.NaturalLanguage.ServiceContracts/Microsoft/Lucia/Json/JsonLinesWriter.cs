using System;
using System.IO;
using Microsoft.Lucia.Common;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Json
{
	// Token: 0x02000030 RID: 48
	public sealed class JsonLinesWriter : IDisposable
	{
		// Token: 0x060000C4 RID: 196 RVA: 0x00003814 File Offset: 0x00001A14
		private JsonLinesWriter(TextWriter textWriter, [Nullable] JsonSerializer serializer)
		{
			this._textWriter = textWriter;
			this._serializer = serializer ?? JsonSerializer.Create();
			this._serializer.Formatting = Formatting.None;
			this._serializer.MetadataPropertyHandling = MetadataPropertyHandling.Ignore;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000384B File Offset: 0x00001A4B
		public static JsonLinesWriter Create(Stream stream, [Nullable] JsonSerializer serializer = null, bool leaveOpen = false)
		{
			return JsonLinesWriter.Create(StreamWriterFactory.Create(stream, leaveOpen), serializer);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000385A File Offset: 0x00001A5A
		public static JsonLinesWriter Create(TextWriter writer, [Nullable] JsonSerializer serializer = null)
		{
			return new JsonLinesWriter(writer, serializer);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003863 File Offset: 0x00001A63
		public void Dispose()
		{
			this._textWriter.Dispose();
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003870 File Offset: 0x00001A70
		public void WriteLine<T>(T item)
		{
			this._serializer.Serialize(this._textWriter, item, typeof(T));
			this._textWriter.Write('\n');
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000038A0 File Offset: 0x00001AA0
		public void WriteLineRaw(string json)
		{
			this._textWriter.Write(json);
			this._textWriter.Write('\n');
		}

		// Token: 0x04000053 RID: 83
		private readonly TextWriter _textWriter;

		// Token: 0x04000054 RID: 84
		private readonly JsonSerializer _serializer;
	}
}
