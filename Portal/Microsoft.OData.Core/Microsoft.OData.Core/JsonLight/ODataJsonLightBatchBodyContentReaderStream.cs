using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.OData.Json;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000226 RID: 550
	internal sealed class ODataJsonLightBatchBodyContentReaderStream : MemoryStream
	{
		// Token: 0x06001805 RID: 6149 RVA: 0x00044A9F File Offset: 0x00042C9F
		internal ODataJsonLightBatchBodyContentReaderStream(IODataStreamListener listener)
		{
			this.listener = listener;
		}

		// Token: 0x06001806 RID: 6150 RVA: 0x00044AB0 File Offset: 0x00042CB0
		internal bool PopulateBodyContent(IJsonReader jsonReader, string contentTypeHeader)
		{
			bool flag = false;
			ODataJsonLightBatchBodyContentReaderStream.BatchPayloadBodyContentType? batchPayloadBodyContentType = ODataJsonLightBatchBodyContentReaderStream.DetectBatchPayloadBodyContentType(jsonReader, contentTypeHeader);
			if (batchPayloadBodyContentType != null)
			{
				if (batchPayloadBodyContentType != null)
				{
					switch (batchPayloadBodyContentType.GetValueOrDefault())
					{
					case ODataJsonLightBatchBodyContentReaderStream.BatchPayloadBodyContentType.Json:
						this.WriteJsonContent(jsonReader);
						break;
					case ODataJsonLightBatchBodyContentReaderStream.BatchPayloadBodyContentType.Textual:
					{
						string text = string.Format(CultureInfo.InvariantCulture, "\"{0}\"", new object[] { jsonReader.ReadStringValue() });
						this.WriteBytes(Encoding.UTF8.GetBytes(text));
						break;
					}
					case ODataJsonLightBatchBodyContentReaderStream.BatchPayloadBodyContentType.Binary:
					{
						string text2 = jsonReader.ReadStringValue();
						this.WriteBinaryContent(text2);
						break;
					}
					default:
						goto IL_0096;
					}
					return true;
				}
				IL_0096:
				throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "unknown / undefined type, new type that needs to be supported: {0}? ", new object[] { batchPayloadBodyContentType }));
			}
			this.cachedBodyContent = jsonReader.ReadStringValue();
			return flag;
		}

		// Token: 0x06001807 RID: 6151 RVA: 0x00044B7C File Offset: 0x00042D7C
		internal void PopulateCachedBodyContent(string contentTypeHeader)
		{
			ODataMediaType mediaType = ODataJsonLightBatchBodyContentReaderStream.GetMediaType(contentTypeHeader);
			if (mediaType != null && mediaType.Type.Equals("text"))
			{
				string text = string.Format(CultureInfo.InvariantCulture, "\"{0}\"", new object[] { this.cachedBodyContent });
				this.WriteBytes(Encoding.UTF8.GetBytes(text));
				return;
			}
			this.WriteBinaryContent(this.cachedBodyContent);
		}

		// Token: 0x06001808 RID: 6152 RVA: 0x00044BE2 File Offset: 0x00042DE2
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.listener != null)
			{
				this.listener.StreamDisposed();
				this.listener = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x00044C08 File Offset: 0x00042E08
		private static ODataJsonLightBatchBodyContentReaderStream.BatchPayloadBodyContentType? DetectBatchPayloadBodyContentType(IJsonReader jsonReader, string contentTypeHeader)
		{
			ODataJsonLightBatchBodyContentReaderStream.BatchPayloadBodyContentType? batchPayloadBodyContentType = null;
			ODataMediaType mediaType = ODataJsonLightBatchBodyContentReaderStream.GetMediaType(contentTypeHeader);
			if (jsonReader.NodeType == JsonNodeType.StartObject)
			{
				batchPayloadBodyContentType = new ODataJsonLightBatchBodyContentReaderStream.BatchPayloadBodyContentType?(ODataJsonLightBatchBodyContentReaderStream.BatchPayloadBodyContentType.Json);
			}
			else if (jsonReader.NodeType == JsonNodeType.PrimitiveValue && (mediaType != null || contentTypeHeader != null))
			{
				if (mediaType != null && mediaType.Type.Equals("text"))
				{
					batchPayloadBodyContentType = new ODataJsonLightBatchBodyContentReaderStream.BatchPayloadBodyContentType?(ODataJsonLightBatchBodyContentReaderStream.BatchPayloadBodyContentType.Textual);
				}
				else
				{
					batchPayloadBodyContentType = new ODataJsonLightBatchBodyContentReaderStream.BatchPayloadBodyContentType?(ODataJsonLightBatchBodyContentReaderStream.BatchPayloadBodyContentType.Binary);
				}
			}
			return batchPayloadBodyContentType;
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x00044C70 File Offset: 0x00042E70
		private static ODataMediaType GetMediaType(string contentTypeHeader)
		{
			if (string.IsNullOrEmpty(contentTypeHeader))
			{
				return null;
			}
			contentTypeHeader = contentTypeHeader.Trim();
			int num = contentTypeHeader.IndexOf(';');
			string text = ((num != -1) ? contentTypeHeader.Substring(0, num) : contentTypeHeader);
			int num2 = text.IndexOf('/');
			string text2 = null;
			string text3;
			if (num2 != -1)
			{
				text3 = text.Substring(0, num2);
				text2 = text.Substring(num2 + 1);
			}
			else
			{
				text3 = text;
			}
			return new ODataMediaType(text3, text2);
		}

		// Token: 0x0600180B RID: 6155 RVA: 0x00044CDC File Offset: 0x00042EDC
		private void WriteJsonContent(IJsonReader reader)
		{
			IJsonWriter jsonWriter = new JsonWriter(new StreamWriter(this), true);
			ODataJsonLightBatchBodyContentReaderStream.WriteCurrentJsonObject(reader, jsonWriter);
			this.Flush();
			this.Position = 0L;
		}

		// Token: 0x0600180C RID: 6156 RVA: 0x00044D0C File Offset: 0x00042F0C
		private void WriteBinaryContent(string encodedContent)
		{
			byte[] array = Convert.FromBase64String(encodedContent.Replace('-', '+').Replace('_', '/'));
			this.WriteBytes(array);
		}

		// Token: 0x0600180D RID: 6157 RVA: 0x00044D39 File Offset: 0x00042F39
		private void WriteBytes(byte[] bytes)
		{
			this.Write(bytes, 0, bytes.Length);
			this.Flush();
			this.Position = 0L;
		}

		// Token: 0x0600180E RID: 6158 RVA: 0x00044D54 File Offset: 0x00042F54
		private static void WriteCurrentJsonObject(IJsonReader reader, IJsonWriter jsonWriter)
		{
			Stack<JsonNodeType> stack = new Stack<JsonNodeType>();
			for (;;)
			{
				switch (reader.NodeType)
				{
				case JsonNodeType.StartObject:
					stack.Push(reader.NodeType);
					jsonWriter.StartObjectScope();
					goto IL_00D6;
				case JsonNodeType.EndObject:
					stack.Pop();
					jsonWriter.EndObjectScope();
					goto IL_00D6;
				case JsonNodeType.StartArray:
					stack.Push(reader.NodeType);
					jsonWriter.StartArrayScope();
					goto IL_00D6;
				case JsonNodeType.EndArray:
					stack.Pop();
					jsonWriter.EndArrayScope();
					goto IL_00D6;
				case JsonNodeType.Property:
					jsonWriter.WriteName(reader.Value.ToString());
					goto IL_00D6;
				case JsonNodeType.PrimitiveValue:
					if (reader.Value != null)
					{
						jsonWriter.WritePrimitiveValue(reader.Value);
						goto IL_00D6;
					}
					jsonWriter.WriteValue(null);
					goto IL_00D6;
				}
				break;
				IL_00D6:
				reader.ReadNext();
				if (stack.Count == 0)
				{
					goto Block_3;
				}
			}
			throw new ODataException(string.Format(CultureInfo.InvariantCulture, "Unexpected reader.NodeType: {0}.", new object[] { reader.NodeType }));
			Block_3:
			jsonWriter.Flush();
		}

		// Token: 0x04000ABB RID: 2747
		private IODataStreamListener listener;

		// Token: 0x04000ABC RID: 2748
		private string cachedBodyContent;

		// Token: 0x020003E1 RID: 993
		private enum BatchPayloadBodyContentType
		{
			// Token: 0x04000F70 RID: 3952
			Json,
			// Token: 0x04000F71 RID: 3953
			Textual,
			// Token: 0x04000F72 RID: 3954
			Binary
		}
	}
}
