using System;
using System.IO;
using Microsoft.DataShaping.Common.Json;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer
{
	// Token: 0x020000BF RID: 191
	internal sealed class JsonDataShapeQueryWriter : DataShapeQueryWriter
	{
		// Token: 0x060004F7 RID: 1271 RVA: 0x0000A6F4 File Offset: 0x000088F4
		internal JsonDataShapeQueryWriter(Stream stream)
		{
			StreamWriter streamWriter = new StreamWriter(stream);
			this.m_jsonWriter = new JsonWriter(streamWriter, true);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0000A71B File Offset: 0x0000891B
		internal JsonDataShapeQueryWriter(JsonWriter jsonWriter)
		{
			this.m_jsonWriter = jsonWriter;
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0000A72A File Offset: 0x0000892A
		protected override void WriteStartDocument()
		{
			this.m_jsonWriter.StartObjectScope();
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0000A737 File Offset: 0x00008937
		protected override void WriteEndDocument()
		{
			this.m_jsonWriter.EndObjectScope();
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0000A744 File Offset: 0x00008944
		protected override void WriteStartObject(string localName, bool isInCollection)
		{
			if (!isInCollection)
			{
				this.m_jsonWriter.WriteName(localName);
			}
			this.m_jsonWriter.StartObjectScope();
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0000A760 File Offset: 0x00008960
		protected override void WriteEndObject()
		{
			this.m_jsonWriter.EndObjectScope();
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0000A76D File Offset: 0x0000896D
		protected override void WriteStartArray(string localName)
		{
			this.m_jsonWriter.WriteName(localName);
			this.m_jsonWriter.StartArrayScope();
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0000A786 File Offset: 0x00008986
		protected override void WriteStartArray()
		{
			this.m_jsonWriter.StartArrayScope();
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0000A793 File Offset: 0x00008993
		protected override void WriteEndArray()
		{
			this.m_jsonWriter.EndArrayScope();
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0000A7A0 File Offset: 0x000089A0
		protected override void WriteVariantValue(string localName, object value, bool isInCollection)
		{
			if (!isInCollection)
			{
				this.m_jsonWriter.WriteName(localName);
			}
			this.m_jsonWriter.WriteTypeEncodedValue(value);
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0000A7BD File Offset: 0x000089BD
		protected override void WriteValue(string localName, bool value, bool isInCollection)
		{
			if (!isInCollection)
			{
				this.m_jsonWriter.WriteName(localName);
			}
			this.m_jsonWriter.WriteValue(value);
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0000A7DA File Offset: 0x000089DA
		protected override void WriteValue(string localName, int value, bool isInCollection)
		{
			if (!isInCollection)
			{
				this.m_jsonWriter.WriteName(localName);
			}
			this.m_jsonWriter.WriteValue(value);
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0000A7F7 File Offset: 0x000089F7
		protected override void WriteValue(string localName, long value, bool isInCollection)
		{
			if (!isInCollection)
			{
				this.m_jsonWriter.WriteName(localName);
			}
			this.m_jsonWriter.WriteValue(value);
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0000A814 File Offset: 0x00008A14
		protected override void WriteValue(string localName, string value, bool isInCollection)
		{
			if (!isInCollection)
			{
				this.m_jsonWriter.WriteName(localName);
			}
			this.m_jsonWriter.WriteValue(value);
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0000A831 File Offset: 0x00008A31
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.m_jsonWriter != null)
			{
				this.m_jsonWriter.Flush();
				this.m_jsonWriter = null;
			}
		}

		// Token: 0x0400021D RID: 541
		private JsonWriter m_jsonWriter;
	}
}
