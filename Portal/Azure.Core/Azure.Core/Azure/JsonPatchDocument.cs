using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading;
using Azure.Core.JsonPatch;
using Azure.Core.Serialization;

namespace Azure
{
	// Token: 0x02000023 RID: 35
	[NullableContext(1)]
	[Nullable(0)]
	public class JsonPatchDocument
	{
		// Token: 0x0600006E RID: 110 RVA: 0x0000289C File Offset: 0x00000A9C
		public JsonPatchDocument()
			: this(default(ReadOnlyMemory<byte>))
		{
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000028B8 File Offset: 0x00000AB8
		public JsonPatchDocument(ObjectSerializer serializer)
			: this(default(ReadOnlyMemory<byte>), serializer)
		{
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000028D5 File Offset: 0x00000AD5
		[NullableContext(0)]
		public JsonPatchDocument(ReadOnlyMemory<byte> rawDocument)
			: this(rawDocument, new JsonObjectSerializer())
		{
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000028E3 File Offset: 0x00000AE3
		[NullableContext(0)]
		public JsonPatchDocument(ReadOnlyMemory<byte> rawDocument, [Nullable(1)] ObjectSerializer serializer)
		{
			this._operations = new Collection<JsonPatchOperation>();
			this._rawDocument = rawDocument;
			if (serializer == null)
			{
				throw new ArgumentNullException("serializer");
			}
			this._serializer = serializer;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002913 File Offset: 0x00000B13
		public void AppendAddRaw(string path, string rawJsonValue)
		{
			this._operations.Add(new JsonPatchOperation(JsonPatchOperationKind.Add, path, null, rawJsonValue));
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000292D File Offset: 0x00000B2D
		public void AppendAdd<[Nullable(2)] T>(string path, T value)
		{
			this._operations.Add(new JsonPatchOperation(JsonPatchOperationKind.Add, path, null, this.Serialize<T>(value)));
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000294D File Offset: 0x00000B4D
		public void AppendReplaceRaw(string path, string rawJsonValue)
		{
			this._operations.Add(new JsonPatchOperation(JsonPatchOperationKind.Replace, path, null, rawJsonValue));
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002967 File Offset: 0x00000B67
		public void AppendReplace<[Nullable(2)] T>(string path, T value)
		{
			this._operations.Add(new JsonPatchOperation(JsonPatchOperationKind.Replace, path, null, this.Serialize<T>(value)));
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002987 File Offset: 0x00000B87
		public void AppendCopy(string from, string path)
		{
			this._operations.Add(new JsonPatchOperation(JsonPatchOperationKind.Copy, path, from, null));
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000029A1 File Offset: 0x00000BA1
		public void AppendMove(string from, string path)
		{
			this._operations.Add(new JsonPatchOperation(JsonPatchOperationKind.Move, path, from, null));
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000029BB File Offset: 0x00000BBB
		public void AppendRemove(string path)
		{
			this._operations.Add(new JsonPatchOperation(JsonPatchOperationKind.Remove, path, null, null));
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000029D5 File Offset: 0x00000BD5
		public void AppendTestRaw(string path, string rawJsonValue)
		{
			this._operations.Add(new JsonPatchOperation(JsonPatchOperationKind.Test, path, null, rawJsonValue));
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000029EF File Offset: 0x00000BEF
		public void AppendTest<[Nullable(2)] T>(string path, T value)
		{
			this._operations.Add(new JsonPatchOperation(JsonPatchOperationKind.Test, path, null, this.Serialize<T>(value)));
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002A10 File Offset: 0x00000C10
		[NullableContext(0)]
		public ReadOnlyMemory<byte> ToBytes()
		{
			if (!this._rawDocument.IsEmpty && this._operations.Count == 0)
			{
				return this._rawDocument;
			}
			ReadOnlyMemory<byte> readOnlyMemory;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(memoryStream, default(JsonWriterOptions)))
				{
					this.WriteTo(utf8JsonWriter);
				}
				readOnlyMemory = MemoryExtensions.AsMemory<byte>(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
			}
			return readOnlyMemory;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002AAC File Offset: 0x00000CAC
		public override string ToString()
		{
			return Encoding.UTF8.GetString(this.ToBytes().ToArray());
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002AD4 File Offset: 0x00000CD4
		private void WriteTo(Utf8JsonWriter writer)
		{
			writer.WriteStartArray();
			if (!this._rawDocument.IsEmpty)
			{
				using (JsonDocument jsonDocument = JsonDocument.Parse(this._rawDocument, default(JsonDocumentOptions)))
				{
					foreach (JsonElement jsonElement in jsonDocument.RootElement.EnumerateArray())
					{
						jsonElement.WriteTo(writer);
					}
				}
			}
			foreach (JsonPatchOperation jsonPatchOperation in this._operations)
			{
				writer.WriteStartObject();
				writer.WriteString("op", jsonPatchOperation.Kind.ToString());
				if (jsonPatchOperation.From != null)
				{
					writer.WriteString("from", jsonPatchOperation.From);
				}
				writer.WriteString("path", jsonPatchOperation.Path);
				if (jsonPatchOperation.RawJsonValue != null)
				{
					using (JsonDocument jsonDocument2 = JsonDocument.Parse(jsonPatchOperation.RawJsonValue, default(JsonDocumentOptions)))
					{
						writer.WritePropertyName("value");
						jsonDocument2.WriteTo(writer);
					}
				}
				writer.WriteEndObject();
			}
			writer.WriteEndArray();
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002C64 File Offset: 0x00000E64
		private string Serialize<[Nullable(2)] T>(T value)
		{
			string @string;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				this._serializer.Serialize(memoryStream, value, typeof(T), default(CancellationToken));
				@string = Encoding.UTF8.GetString(memoryStream.ToArray());
			}
			return @string;
		}

		// Token: 0x04000040 RID: 64
		[Nullable(0)]
		private readonly ReadOnlyMemory<byte> _rawDocument;

		// Token: 0x04000041 RID: 65
		private readonly ObjectSerializer _serializer;

		// Token: 0x04000042 RID: 66
		private readonly Collection<JsonPatchOperation> _operations;
	}
}
