using System;
using System.Buffers;
using System.Buffers.Text;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Text.Json
{
	// Token: 0x02000039 RID: 57
	public sealed class JsonDocument : IDisposable
	{
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000269 RID: 617 RVA: 0x00006629 File Offset: 0x00004829
		internal bool IsDisposable { get; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600026A RID: 618 RVA: 0x00006631 File Offset: 0x00004831
		public JsonElement RootElement
		{
			get
			{
				return new JsonElement(this, 0);
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000663A File Offset: 0x0000483A
		private JsonDocument(ReadOnlyMemory<byte> utf8Json, JsonDocument.MetadataDb parsedData, byte[] extraRentedArrayPoolBytes = null, PooledByteBufferWriter extraPooledByteBufferWriter = null, bool isDisposable = true)
		{
			this._utf8Json = utf8Json;
			this._parsedData = parsedData;
			this._extraRentedArrayPoolBytes = extraRentedArrayPoolBytes;
			this._extraPooledByteBufferWriter = extraPooledByteBufferWriter;
			this.IsDisposable = isDisposable;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00006668 File Offset: 0x00004868
		public void Dispose()
		{
			int length = this._utf8Json.Length;
			if (length == 0 || !this.IsDisposable)
			{
				return;
			}
			this._parsedData.Dispose();
			this._utf8Json = ReadOnlyMemory<byte>.Empty;
			if (this._extraRentedArrayPoolBytes != null)
			{
				byte[] array = Interlocked.Exchange<byte[]>(ref this._extraRentedArrayPoolBytes, null);
				if (array != null)
				{
					array.AsSpan(0, length).Clear();
					ArrayPool<byte>.Shared.Return(array, false);
					return;
				}
			}
			else if (this._extraPooledByteBufferWriter != null)
			{
				PooledByteBufferWriter pooledByteBufferWriter = Interlocked.Exchange<PooledByteBufferWriter>(ref this._extraPooledByteBufferWriter, null);
				if (pooledByteBufferWriter != null)
				{
					pooledByteBufferWriter.Dispose();
				}
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x000066F8 File Offset: 0x000048F8
		[NullableContext(1)]
		public void WriteTo(Utf8JsonWriter writer)
		{
			if (writer == null)
			{
				ThrowHelper.ThrowArgumentNullException("writer");
			}
			this.RootElement.WriteTo(writer);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00006721 File Offset: 0x00004921
		internal JsonTokenType GetJsonTokenType(int index)
		{
			this.CheckNotDisposed();
			return this._parsedData.GetJsonTokenType(index);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00006738 File Offset: 0x00004938
		internal int GetArrayLength(int index)
		{
			this.CheckNotDisposed();
			JsonDocument.DbRow dbRow = this._parsedData.Get(index);
			JsonDocument.CheckExpectedType(JsonTokenType.StartArray, dbRow.TokenType);
			return dbRow.SizeOrLength;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000676C File Offset: 0x0000496C
		internal JsonElement GetArrayIndexElement(int currentIndex, int arrayIndex)
		{
			this.CheckNotDisposed();
			JsonDocument.DbRow dbRow = this._parsedData.Get(currentIndex);
			JsonDocument.CheckExpectedType(JsonTokenType.StartArray, dbRow.TokenType);
			int sizeOrLength = dbRow.SizeOrLength;
			if (arrayIndex >= sizeOrLength)
			{
				throw new IndexOutOfRangeException();
			}
			if (!dbRow.HasComplexChildren)
			{
				return new JsonElement(this, currentIndex + (arrayIndex + 1) * 12);
			}
			int num = 0;
			for (int i = currentIndex + 12; i < this._parsedData.Length; i += 12)
			{
				if (arrayIndex == num)
				{
					return new JsonElement(this, i);
				}
				dbRow = this._parsedData.Get(i);
				if (!dbRow.IsSimpleValue)
				{
					i += 12 * dbRow.NumberOfRows;
				}
				num++;
			}
			throw new IndexOutOfRangeException();
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00006818 File Offset: 0x00004A18
		internal int GetEndIndex(int index, bool includeEndElement)
		{
			this.CheckNotDisposed();
			JsonDocument.DbRow dbRow = this._parsedData.Get(index);
			if (dbRow.IsSimpleValue)
			{
				return index + 12;
			}
			int num = index + 12 * dbRow.NumberOfRows;
			if (includeEndElement)
			{
				num += 12;
			}
			return num;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000685C File Offset: 0x00004A5C
		internal ReadOnlyMemory<byte> GetRootRawValue()
		{
			return this.GetRawValue(0, true);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00006868 File Offset: 0x00004A68
		internal ReadOnlyMemory<byte> GetRawValue(int index, bool includeQuotes)
		{
			this.CheckNotDisposed();
			JsonDocument.DbRow dbRow = this._parsedData.Get(index);
			if (!dbRow.IsSimpleValue)
			{
				int endIndex = this.GetEndIndex(index, false);
				int location = dbRow.Location;
				dbRow = this._parsedData.Get(endIndex);
				return this._utf8Json.Slice(location, dbRow.Location - location + dbRow.SizeOrLength);
			}
			if (includeQuotes && dbRow.TokenType == JsonTokenType.String)
			{
				return this._utf8Json.Slice(dbRow.Location - 1, dbRow.SizeOrLength + 2);
			}
			return this._utf8Json.Slice(dbRow.Location, dbRow.SizeOrLength);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00006914 File Offset: 0x00004B14
		private ReadOnlyMemory<byte> GetPropertyRawValue(int valueIndex)
		{
			this.CheckNotDisposed();
			int num = this._parsedData.Get(valueIndex - 12).Location - 1;
			JsonDocument.DbRow dbRow = this._parsedData.Get(valueIndex);
			int num2;
			if (dbRow.IsSimpleValue)
			{
				num2 = dbRow.Location + dbRow.SizeOrLength;
				if (dbRow.TokenType == JsonTokenType.String)
				{
					num2++;
				}
				return this._utf8Json.Slice(num, num2 - num);
			}
			int endIndex = this.GetEndIndex(valueIndex, false);
			dbRow = this._parsedData.Get(endIndex);
			num2 = dbRow.Location + dbRow.SizeOrLength;
			return this._utf8Json.Slice(num, num2 - num);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x000069BC File Offset: 0x00004BBC
		internal string GetString(int index, JsonTokenType expectedType)
		{
			this.CheckNotDisposed();
			JsonDocument.DbRow dbRow = this._parsedData.Get(index);
			JsonTokenType tokenType = dbRow.TokenType;
			if (tokenType == JsonTokenType.Null)
			{
				return null;
			}
			JsonDocument.CheckExpectedType(expectedType, tokenType);
			ReadOnlySpan<byte> readOnlySpan = this._utf8Json.Span.Slice(dbRow.Location, dbRow.SizeOrLength);
			if (!dbRow.HasComplexChildren)
			{
				return JsonReaderHelper.TranscodeHelper(readOnlySpan);
			}
			return JsonReaderHelper.GetUnescapedString(readOnlySpan);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00006A2C File Offset: 0x00004C2C
		internal unsafe bool TextEquals(int index, ReadOnlySpan<char> otherText, bool isPropertyName)
		{
			this.CheckNotDisposed();
			byte[] array = null;
			int num = checked(otherText.Length * 3);
			Span<byte> span2;
			if (num <= 256)
			{
				Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)256], 256);
				span2 = span;
			}
			else
			{
				span2 = (array = ArrayPool<byte>.Shared.Rent(num));
			}
			Span<byte> span3 = span2;
			int num2;
			OperationStatus operationStatus = JsonWriterHelper.ToUtf8(otherText, span3, out num2);
			bool flag = operationStatus != OperationStatus.InvalidData && this.TextEquals(index, span3.Slice(0, num2), isPropertyName, true);
			if (array != null)
			{
				span3.Slice(0, num2).Clear();
				ArrayPool<byte>.Shared.Return(array, false);
			}
			return flag;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00006AD8 File Offset: 0x00004CD8
		internal bool TextEquals(int index, ReadOnlySpan<byte> otherUtf8Text, bool isPropertyName, bool shouldUnescape)
		{
			this.CheckNotDisposed();
			int num = (isPropertyName ? (index - 12) : index);
			JsonDocument.DbRow dbRow = this._parsedData.Get(num);
			JsonDocument.CheckExpectedType(isPropertyName ? JsonTokenType.PropertyName : JsonTokenType.String, dbRow.TokenType);
			ReadOnlySpan<byte> readOnlySpan = this._utf8Json.Span.Slice(dbRow.Location, dbRow.SizeOrLength);
			if (otherUtf8Text.Length > readOnlySpan.Length || (!shouldUnescape && otherUtf8Text.Length != readOnlySpan.Length))
			{
				return false;
			}
			if (!dbRow.HasComplexChildren || !shouldUnescape)
			{
				return readOnlySpan.SequenceEqual(otherUtf8Text);
			}
			if (otherUtf8Text.Length < readOnlySpan.Length / 6)
			{
				return false;
			}
			int num2 = readOnlySpan.IndexOf(92);
			return otherUtf8Text.StartsWith(readOnlySpan.Slice(0, num2)) && JsonReaderHelper.UnescapeAndCompare(readOnlySpan.Slice(num2), otherUtf8Text.Slice(num2));
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00006BBB File Offset: 0x00004DBB
		internal string GetNameOfPropertyValue(int index)
		{
			return this.GetString(index - 12, JsonTokenType.PropertyName);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00006BC8 File Offset: 0x00004DC8
		internal bool TryGetValue(int index, [NotNullWhen(true)] out byte[] value)
		{
			this.CheckNotDisposed();
			JsonDocument.DbRow dbRow = this._parsedData.Get(index);
			JsonDocument.CheckExpectedType(JsonTokenType.String, dbRow.TokenType);
			ReadOnlySpan<byte> readOnlySpan = this._utf8Json.Span.Slice(dbRow.Location, dbRow.SizeOrLength);
			if (dbRow.HasComplexChildren)
			{
				return JsonReaderHelper.TryGetUnescapedBase64Bytes(readOnlySpan, out value);
			}
			return JsonReaderHelper.TryDecodeBase64(readOnlySpan, out value);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00006C30 File Offset: 0x00004E30
		internal bool TryGetValue(int index, out sbyte value)
		{
			this.CheckNotDisposed();
			JsonDocument.DbRow dbRow = this._parsedData.Get(index);
			JsonDocument.CheckExpectedType(JsonTokenType.Number, dbRow.TokenType);
			ReadOnlySpan<byte> readOnlySpan = this._utf8Json.Span.Slice(dbRow.Location, dbRow.SizeOrLength);
			sbyte b;
			int num;
			if (Utf8Parser.TryParse(readOnlySpan, out b, out num, '\0') && num == readOnlySpan.Length)
			{
				value = b;
				return true;
			}
			value = 0;
			return false;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00006CA0 File Offset: 0x00004EA0
		internal bool TryGetValue(int index, out byte value)
		{
			this.CheckNotDisposed();
			JsonDocument.DbRow dbRow = this._parsedData.Get(index);
			JsonDocument.CheckExpectedType(JsonTokenType.Number, dbRow.TokenType);
			ReadOnlySpan<byte> readOnlySpan = this._utf8Json.Span.Slice(dbRow.Location, dbRow.SizeOrLength);
			byte b;
			int num;
			if (Utf8Parser.TryParse(readOnlySpan, out b, out num, '\0') && num == readOnlySpan.Length)
			{
				value = b;
				return true;
			}
			value = 0;
			return false;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00006D10 File Offset: 0x00004F10
		internal bool TryGetValue(int index, out short value)
		{
			this.CheckNotDisposed();
			JsonDocument.DbRow dbRow = this._parsedData.Get(index);
			JsonDocument.CheckExpectedType(JsonTokenType.Number, dbRow.TokenType);
			ReadOnlySpan<byte> readOnlySpan = this._utf8Json.Span.Slice(dbRow.Location, dbRow.SizeOrLength);
			short num;
			int num2;
			if (Utf8Parser.TryParse(readOnlySpan, out num, out num2, '\0') && num2 == readOnlySpan.Length)
			{
				value = num;
				return true;
			}
			value = 0;
			return false;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00006D80 File Offset: 0x00004F80
		internal bool TryGetValue(int index, out ushort value)
		{
			this.CheckNotDisposed();
			JsonDocument.DbRow dbRow = this._parsedData.Get(index);
			JsonDocument.CheckExpectedType(JsonTokenType.Number, dbRow.TokenType);
			ReadOnlySpan<byte> readOnlySpan = this._utf8Json.Span.Slice(dbRow.Location, dbRow.SizeOrLength);
			ushort num;
			int num2;
			if (Utf8Parser.TryParse(readOnlySpan, out num, out num2, '\0') && num2 == readOnlySpan.Length)
			{
				value = num;
				return true;
			}
			value = 0;
			return false;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00006DF0 File Offset: 0x00004FF0
		internal bool TryGetValue(int index, out int value)
		{
			this.CheckNotDisposed();
			JsonDocument.DbRow dbRow = this._parsedData.Get(index);
			JsonDocument.CheckExpectedType(JsonTokenType.Number, dbRow.TokenType);
			ReadOnlySpan<byte> readOnlySpan = this._utf8Json.Span.Slice(dbRow.Location, dbRow.SizeOrLength);
			int num;
			int num2;
			if (Utf8Parser.TryParse(readOnlySpan, out num, out num2, '\0') && num2 == readOnlySpan.Length)
			{
				value = num;
				return true;
			}
			value = 0;
			return false;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00006E60 File Offset: 0x00005060
		internal bool TryGetValue(int index, out uint value)
		{
			this.CheckNotDisposed();
			JsonDocument.DbRow dbRow = this._parsedData.Get(index);
			JsonDocument.CheckExpectedType(JsonTokenType.Number, dbRow.TokenType);
			ReadOnlySpan<byte> readOnlySpan = this._utf8Json.Span.Slice(dbRow.Location, dbRow.SizeOrLength);
			uint num;
			int num2;
			if (Utf8Parser.TryParse(readOnlySpan, out num, out num2, '\0') && num2 == readOnlySpan.Length)
			{
				value = num;
				return true;
			}
			value = 0U;
			return false;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00006ED0 File Offset: 0x000050D0
		internal bool TryGetValue(int index, out long value)
		{
			this.CheckNotDisposed();
			JsonDocument.DbRow dbRow = this._parsedData.Get(index);
			JsonDocument.CheckExpectedType(JsonTokenType.Number, dbRow.TokenType);
			ReadOnlySpan<byte> readOnlySpan = this._utf8Json.Span.Slice(dbRow.Location, dbRow.SizeOrLength);
			long num;
			int num2;
			if (Utf8Parser.TryParse(readOnlySpan, out num, out num2, '\0') && num2 == readOnlySpan.Length)
			{
				value = num;
				return true;
			}
			value = 0L;
			return false;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00006F44 File Offset: 0x00005144
		internal bool TryGetValue(int index, out ulong value)
		{
			this.CheckNotDisposed();
			JsonDocument.DbRow dbRow = this._parsedData.Get(index);
			JsonDocument.CheckExpectedType(JsonTokenType.Number, dbRow.TokenType);
			ReadOnlySpan<byte> readOnlySpan = this._utf8Json.Span.Slice(dbRow.Location, dbRow.SizeOrLength);
			ulong num;
			int num2;
			if (Utf8Parser.TryParse(readOnlySpan, out num, out num2, '\0') && num2 == readOnlySpan.Length)
			{
				value = num;
				return true;
			}
			value = 0UL;
			return false;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00006FB8 File Offset: 0x000051B8
		internal bool TryGetValue(int index, out double value)
		{
			this.CheckNotDisposed();
			JsonDocument.DbRow dbRow = this._parsedData.Get(index);
			JsonDocument.CheckExpectedType(JsonTokenType.Number, dbRow.TokenType);
			ReadOnlySpan<byte> readOnlySpan = this._utf8Json.Span.Slice(dbRow.Location, dbRow.SizeOrLength);
			double num;
			int num2;
			if (Utf8Parser.TryParse(readOnlySpan, out num, out num2, '\0') && readOnlySpan.Length == num2)
			{
				value = num;
				return true;
			}
			value = 0.0;
			return false;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00007030 File Offset: 0x00005230
		internal bool TryGetValue(int index, out float value)
		{
			this.CheckNotDisposed();
			JsonDocument.DbRow dbRow = this._parsedData.Get(index);
			JsonDocument.CheckExpectedType(JsonTokenType.Number, dbRow.TokenType);
			ReadOnlySpan<byte> readOnlySpan = this._utf8Json.Span.Slice(dbRow.Location, dbRow.SizeOrLength);
			float num;
			int num2;
			if (Utf8Parser.TryParse(readOnlySpan, out num, out num2, '\0') && readOnlySpan.Length == num2)
			{
				value = num;
				return true;
			}
			value = 0f;
			return false;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x000070A4 File Offset: 0x000052A4
		internal bool TryGetValue(int index, out decimal value)
		{
			this.CheckNotDisposed();
			JsonDocument.DbRow dbRow = this._parsedData.Get(index);
			JsonDocument.CheckExpectedType(JsonTokenType.Number, dbRow.TokenType);
			ReadOnlySpan<byte> readOnlySpan = this._utf8Json.Span.Slice(dbRow.Location, dbRow.SizeOrLength);
			decimal num;
			int num2;
			if (Utf8Parser.TryParse(readOnlySpan, out num, out num2, '\0') && readOnlySpan.Length == num2)
			{
				value = num;
				return true;
			}
			value = 0m;
			return false;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000711C File Offset: 0x0000531C
		internal bool TryGetValue(int index, out DateTime value)
		{
			this.CheckNotDisposed();
			JsonDocument.DbRow dbRow = this._parsedData.Get(index);
			JsonDocument.CheckExpectedType(JsonTokenType.String, dbRow.TokenType);
			ReadOnlySpan<byte> readOnlySpan = this._utf8Json.Span.Slice(dbRow.Location, dbRow.SizeOrLength);
			if (!JsonHelpers.IsValidDateTimeOffsetParseLength(readOnlySpan.Length))
			{
				value = default(DateTime);
				return false;
			}
			if (dbRow.HasComplexChildren)
			{
				return JsonReaderHelper.TryGetEscapedDateTime(readOnlySpan, out value);
			}
			DateTime dateTime;
			if (JsonHelpers.TryParseAsISO(readOnlySpan, out dateTime))
			{
				value = dateTime;
				return true;
			}
			value = default(DateTime);
			return false;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x000071B0 File Offset: 0x000053B0
		internal bool TryGetValue(int index, out DateTimeOffset value)
		{
			this.CheckNotDisposed();
			JsonDocument.DbRow dbRow = this._parsedData.Get(index);
			JsonDocument.CheckExpectedType(JsonTokenType.String, dbRow.TokenType);
			ReadOnlySpan<byte> readOnlySpan = this._utf8Json.Span.Slice(dbRow.Location, dbRow.SizeOrLength);
			if (!JsonHelpers.IsValidDateTimeOffsetParseLength(readOnlySpan.Length))
			{
				value = default(DateTimeOffset);
				return false;
			}
			if (dbRow.HasComplexChildren)
			{
				return JsonReaderHelper.TryGetEscapedDateTimeOffset(readOnlySpan, out value);
			}
			DateTimeOffset dateTimeOffset;
			if (JsonHelpers.TryParseAsISO(readOnlySpan, out dateTimeOffset))
			{
				value = dateTimeOffset;
				return true;
			}
			value = default(DateTimeOffset);
			return false;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00007244 File Offset: 0x00005444
		internal bool TryGetValue(int index, out Guid value)
		{
			this.CheckNotDisposed();
			JsonDocument.DbRow dbRow = this._parsedData.Get(index);
			JsonDocument.CheckExpectedType(JsonTokenType.String, dbRow.TokenType);
			ReadOnlySpan<byte> readOnlySpan = this._utf8Json.Span.Slice(dbRow.Location, dbRow.SizeOrLength);
			if (readOnlySpan.Length > 216)
			{
				value = default(Guid);
				return false;
			}
			if (dbRow.HasComplexChildren)
			{
				return JsonReaderHelper.TryGetEscapedGuid(readOnlySpan, out value);
			}
			Guid guid;
			int num;
			if (readOnlySpan.Length == 36 && Utf8Parser.TryParse(readOnlySpan, out guid, out num, 'D'))
			{
				value = guid;
				return true;
			}
			value = default(Guid);
			return false;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x000072E8 File Offset: 0x000054E8
		internal string GetRawValueAsString(int index)
		{
			return JsonReaderHelper.TranscodeHelper(this.GetRawValue(index, true).Span);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000730C File Offset: 0x0000550C
		internal string GetPropertyRawValueAsString(int valueIndex)
		{
			return JsonReaderHelper.TranscodeHelper(this.GetPropertyRawValue(valueIndex).Span);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00007330 File Offset: 0x00005530
		internal JsonElement CloneElement(int index)
		{
			int endIndex = this.GetEndIndex(index, true);
			JsonDocument.MetadataDb metadataDb = this._parsedData.CopySegment(index, endIndex);
			ReadOnlyMemory<byte> readOnlyMemory = this.GetRawValue(index, true).ToArray();
			JsonDocument jsonDocument = new JsonDocument(readOnlyMemory, metadataDb, null, null, false);
			return jsonDocument.RootElement;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000737C File Offset: 0x0000557C
		internal void WriteElementTo(int index, Utf8JsonWriter writer)
		{
			this.CheckNotDisposed();
			JsonDocument.DbRow dbRow = this._parsedData.Get(index);
			switch (dbRow.TokenType)
			{
			case JsonTokenType.StartObject:
				writer.WriteStartObject();
				this.WriteComplexElement(index, writer);
				return;
			case JsonTokenType.EndObject:
			case JsonTokenType.EndArray:
			case JsonTokenType.PropertyName:
			case JsonTokenType.Comment:
				return;
			case JsonTokenType.StartArray:
				writer.WriteStartArray();
				this.WriteComplexElement(index, writer);
				return;
			case JsonTokenType.String:
				this.WriteString(in dbRow, writer);
				return;
			case JsonTokenType.Number:
				writer.WriteNumberValue(this._utf8Json.Slice(dbRow.Location, dbRow.SizeOrLength).Span);
				return;
			case JsonTokenType.True:
				writer.WriteBooleanValue(true);
				return;
			case JsonTokenType.False:
				writer.WriteBooleanValue(false);
				return;
			case JsonTokenType.Null:
				writer.WriteNullValue();
				return;
			default:
				return;
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00007440 File Offset: 0x00005640
		private void WriteComplexElement(int index, Utf8JsonWriter writer)
		{
			int endIndex = this.GetEndIndex(index, true);
			for (int i = index + 12; i < endIndex; i += 12)
			{
				JsonDocument.DbRow dbRow = this._parsedData.Get(i);
				switch (dbRow.TokenType)
				{
				case JsonTokenType.StartObject:
					writer.WriteStartObject();
					break;
				case JsonTokenType.EndObject:
					writer.WriteEndObject();
					break;
				case JsonTokenType.StartArray:
					writer.WriteStartArray();
					break;
				case JsonTokenType.EndArray:
					writer.WriteEndArray();
					break;
				case JsonTokenType.PropertyName:
					this.WritePropertyName(in dbRow, writer);
					break;
				case JsonTokenType.String:
					this.WriteString(in dbRow, writer);
					break;
				case JsonTokenType.Number:
					writer.WriteNumberValue(this._utf8Json.Slice(dbRow.Location, dbRow.SizeOrLength).Span);
					break;
				case JsonTokenType.True:
					writer.WriteBooleanValue(true);
					break;
				case JsonTokenType.False:
					writer.WriteBooleanValue(false);
					break;
				case JsonTokenType.Null:
					writer.WriteNullValue();
					break;
				}
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00007530 File Offset: 0x00005730
		private ReadOnlySpan<byte> UnescapeString(in JsonDocument.DbRow row, out ArraySegment<byte> rented)
		{
			int location = row.Location;
			int sizeOrLength = row.SizeOrLength;
			ReadOnlySpan<byte> span = this._utf8Json.Slice(location, sizeOrLength).Span;
			if (!row.HasComplexChildren)
			{
				rented = default(ArraySegment<byte>);
				return span;
			}
			byte[] array = ArrayPool<byte>.Shared.Rent(sizeOrLength);
			int num;
			JsonReaderHelper.Unescape(span, array, out num);
			rented = new ArraySegment<byte>(array, 0, num);
			return rented.AsSpan<byte>();
		}

		// Token: 0x0600028E RID: 654 RVA: 0x000075AC File Offset: 0x000057AC
		private static void ClearAndReturn(ArraySegment<byte> rented)
		{
			if (rented.Array != null)
			{
				rented.AsSpan<byte>().Clear();
				ArrayPool<byte>.Shared.Return(rented.Array, false);
			}
		}

		// Token: 0x0600028F RID: 655 RVA: 0x000075E4 File Offset: 0x000057E4
		private void WritePropertyName(in JsonDocument.DbRow row, Utf8JsonWriter writer)
		{
			ArraySegment<byte> arraySegment = default(ArraySegment<byte>);
			try
			{
				writer.WritePropertyName(this.UnescapeString(in row, out arraySegment));
			}
			finally
			{
				JsonDocument.ClearAndReturn(arraySegment);
			}
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00007624 File Offset: 0x00005824
		private void WriteString(in JsonDocument.DbRow row, Utf8JsonWriter writer)
		{
			ArraySegment<byte> arraySegment = default(ArraySegment<byte>);
			try
			{
				writer.WriteStringValue(this.UnescapeString(in row, out arraySegment));
			}
			finally
			{
				JsonDocument.ClearAndReturn(arraySegment);
			}
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00007664 File Offset: 0x00005864
		private static void Parse(ReadOnlySpan<byte> utf8JsonSpan, JsonReaderOptions readerOptions, ref JsonDocument.MetadataDb database, ref JsonDocument.StackRowStack stack)
		{
			bool flag = false;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			Utf8JsonReader utf8JsonReader = new Utf8JsonReader(utf8JsonSpan, true, new JsonReaderState(readerOptions));
			while (utf8JsonReader.Read())
			{
				JsonTokenType tokenType = utf8JsonReader.TokenType;
				int num4 = (int)utf8JsonReader.TokenStartIndex;
				if (tokenType == JsonTokenType.StartObject)
				{
					if (flag)
					{
						num++;
					}
					num3++;
					database.Append(tokenType, num4, -1);
					JsonDocument.StackRow stackRow = new JsonDocument.StackRow(num2 + 1, -1);
					stack.Push(stackRow);
					num2 = 0;
				}
				else if (tokenType == JsonTokenType.EndObject)
				{
					int num5 = database.FindIndexOfFirstUnsetSizeOrLength(JsonTokenType.StartObject);
					num3++;
					num2++;
					database.SetLength(num5, num2);
					int length = database.Length;
					database.Append(tokenType, num4, utf8JsonReader.ValueSpan.Length);
					database.SetNumberOfRows(num5, num2);
					database.SetNumberOfRows(length, num2);
					JsonDocument.StackRow stackRow2 = stack.Pop();
					num2 += stackRow2.SizeOrLength;
				}
				else if (tokenType == JsonTokenType.StartArray)
				{
					if (flag)
					{
						num++;
					}
					num2++;
					database.Append(tokenType, num4, -1);
					JsonDocument.StackRow stackRow3 = new JsonDocument.StackRow(num, num3 + 1);
					stack.Push(stackRow3);
					num = 0;
					num3 = 0;
				}
				else if (tokenType == JsonTokenType.EndArray)
				{
					int num6 = database.FindIndexOfFirstUnsetSizeOrLength(JsonTokenType.StartArray);
					num3++;
					num2++;
					database.SetLength(num6, num);
					database.SetNumberOfRows(num6, num3);
					if (num + 1 != num3)
					{
						database.SetHasComplexChildren(num6);
					}
					int length2 = database.Length;
					database.Append(tokenType, num4, utf8JsonReader.ValueSpan.Length);
					database.SetNumberOfRows(length2, num3);
					JsonDocument.StackRow stackRow4 = stack.Pop();
					num = stackRow4.SizeOrLength;
					num3 += stackRow4.NumberOfRows;
				}
				else if (tokenType == JsonTokenType.PropertyName)
				{
					num3++;
					num2++;
					database.Append(tokenType, num4 + 1, utf8JsonReader.ValueSpan.Length);
					if (utf8JsonReader.ValueIsEscaped)
					{
						database.SetHasComplexChildren(database.Length - 12);
					}
				}
				else
				{
					num3++;
					num2++;
					if (flag)
					{
						num++;
					}
					if (tokenType == JsonTokenType.String)
					{
						database.Append(tokenType, num4 + 1, utf8JsonReader.ValueSpan.Length);
						if (utf8JsonReader.ValueIsEscaped)
						{
							database.SetHasComplexChildren(database.Length - 12);
						}
					}
					else
					{
						database.Append(tokenType, num4, utf8JsonReader.ValueSpan.Length);
					}
				}
				flag = utf8JsonReader.IsInArray;
			}
			database.CompleteAllocations();
		}

		// Token: 0x06000292 RID: 658 RVA: 0x000078B7 File Offset: 0x00005AB7
		private void CheckNotDisposed()
		{
			if (this._utf8Json.IsEmpty)
			{
				ThrowHelper.ThrowObjectDisposedException_JsonDocument();
			}
		}

		// Token: 0x06000293 RID: 659 RVA: 0x000078CB File Offset: 0x00005ACB
		private static void CheckExpectedType(JsonTokenType expected, JsonTokenType actual)
		{
			if (expected != actual)
			{
				ThrowHelper.ThrowJsonElementWrongTypeException(expected, actual);
			}
		}

		// Token: 0x06000294 RID: 660 RVA: 0x000078D8 File Offset: 0x00005AD8
		private static void CheckSupportedOptions(JsonReaderOptions readerOptions, string paramName)
		{
			if (readerOptions.CommentHandling == JsonCommentHandling.Allow)
			{
				throw new ArgumentException(SR.JsonDocumentDoesNotSupportComments, paramName);
			}
		}

		// Token: 0x06000295 RID: 661 RVA: 0x000078F0 File Offset: 0x00005AF0
		[return: Nullable(1)]
		public static JsonDocument Parse(ReadOnlyMemory<byte> utf8Json, JsonDocumentOptions options = default(JsonDocumentOptions))
		{
			return JsonDocument.Parse(utf8Json, options.GetReaderOptions(), null, null);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00007904 File Offset: 0x00005B04
		[return: Nullable(1)]
		public static JsonDocument Parse(ReadOnlySequence<byte> utf8Json, JsonDocumentOptions options = default(JsonDocumentOptions))
		{
			JsonReaderOptions readerOptions = options.GetReaderOptions();
			if (utf8Json.IsSingleSegment)
			{
				return JsonDocument.Parse(utf8Json.First, readerOptions, null, null);
			}
			int num = checked((int)utf8Json.Length);
			byte[] array = ArrayPool<byte>.Shared.Rent(num);
			JsonDocument jsonDocument;
			try
			{
				(in utf8Json).CopyTo(array.AsSpan<byte>());
				jsonDocument = JsonDocument.Parse(array.AsMemory(0, num), readerOptions, array, null);
			}
			catch
			{
				array.AsSpan(0, num).Clear();
				ArrayPool<byte>.Shared.Return(array, false);
				throw;
			}
			return jsonDocument;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x000079A0 File Offset: 0x00005BA0
		[NullableContext(1)]
		public static JsonDocument Parse(Stream utf8Json, JsonDocumentOptions options = default(JsonDocumentOptions))
		{
			if (utf8Json == null)
			{
				ThrowHelper.ThrowArgumentNullException("utf8Json");
			}
			ArraySegment<byte> arraySegment = JsonDocument.ReadToEnd(utf8Json);
			JsonDocument jsonDocument;
			try
			{
				jsonDocument = JsonDocument.Parse(arraySegment.AsMemory<byte>(), options.GetReaderOptions(), arraySegment.Array, null);
			}
			catch
			{
				arraySegment.AsSpan<byte>().Clear();
				ArrayPool<byte>.Shared.Return(arraySegment.Array, false);
				throw;
			}
			return jsonDocument;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00007A18 File Offset: 0x00005C18
		internal static JsonDocument ParseRented(PooledByteBufferWriter utf8Json, JsonDocumentOptions options = default(JsonDocumentOptions))
		{
			return JsonDocument.Parse(utf8Json.WrittenMemory, options.GetReaderOptions(), null, utf8Json);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00007A30 File Offset: 0x00005C30
		internal static JsonDocument ParseValue(Stream utf8Json, JsonDocumentOptions options)
		{
			ArraySegment<byte> arraySegment = JsonDocument.ReadToEnd(utf8Json);
			byte[] array = new byte[arraySegment.Count];
			Buffer.BlockCopy(arraySegment.Array, 0, array, 0, arraySegment.Count);
			arraySegment.AsSpan<byte>().Clear();
			ArrayPool<byte>.Shared.Return(arraySegment.Array, false);
			return JsonDocument.ParseUnrented(array.AsMemory<byte>(), options.GetReaderOptions(), JsonTokenType.None);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00007AA0 File Offset: 0x00005CA0
		internal static JsonDocument ParseValue(ReadOnlySpan<byte> utf8Json, JsonDocumentOptions options)
		{
			byte[] array = new byte[utf8Json.Length];
			utf8Json.CopyTo(array);
			return JsonDocument.ParseUnrented(array.AsMemory<byte>(), options.GetReaderOptions(), JsonTokenType.None);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00007ADF File Offset: 0x00005CDF
		internal static JsonDocument ParseValue(string json, JsonDocumentOptions options)
		{
			return JsonDocument.ParseValue(json.AsMemory(), options);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00007AED File Offset: 0x00005CED
		[NullableContext(1)]
		public static Task<JsonDocument> ParseAsync(Stream utf8Json, JsonDocumentOptions options = default(JsonDocumentOptions), CancellationToken cancellationToken = default(CancellationToken))
		{
			if (utf8Json == null)
			{
				ThrowHelper.ThrowArgumentNullException("utf8Json");
			}
			return JsonDocument.ParseAsyncCore(utf8Json, options, cancellationToken);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00007B04 File Offset: 0x00005D04
		private static async Task<JsonDocument> ParseAsyncCore(Stream utf8Json, JsonDocumentOptions options = default(JsonDocumentOptions), CancellationToken cancellationToken = default(CancellationToken))
		{
			ArraySegment<byte> arraySegment = await JsonDocument.ReadToEndAsync(utf8Json, cancellationToken).ConfigureAwait(false);
			ArraySegment<byte> arraySegment2 = arraySegment;
			JsonDocument jsonDocument;
			try
			{
				jsonDocument = JsonDocument.Parse(arraySegment2.AsMemory<byte>(), options.GetReaderOptions(), arraySegment2.Array, null);
			}
			catch
			{
				arraySegment2.AsSpan<byte>().Clear();
				ArrayPool<byte>.Shared.Return(arraySegment2.Array, false);
				throw;
			}
			return jsonDocument;
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00007B58 File Offset: 0x00005D58
		internal static async Task<JsonDocument> ParseAsyncCoreUnrented(Stream utf8Json, JsonDocumentOptions options = default(JsonDocumentOptions), CancellationToken cancellationToken = default(CancellationToken))
		{
			ArraySegment<byte> arraySegment = await JsonDocument.ReadToEndAsync(utf8Json, cancellationToken).ConfigureAwait(false);
			ArraySegment<byte> arraySegment2 = arraySegment;
			byte[] array = new byte[arraySegment2.Count];
			Buffer.BlockCopy(arraySegment2.Array, 0, array, 0, arraySegment2.Count);
			arraySegment2.AsSpan<byte>().Clear();
			ArrayPool<byte>.Shared.Return(arraySegment2.Array, false);
			return JsonDocument.ParseUnrented(array.AsMemory<byte>(), options.GetReaderOptions(), JsonTokenType.None);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00007BAC File Offset: 0x00005DAC
		[return: Nullable(1)]
		public static JsonDocument Parse([StringSyntax("Json")] ReadOnlyMemory<char> json, JsonDocumentOptions options = default(JsonDocumentOptions))
		{
			ReadOnlySpan<char> span = json.Span;
			int utf8ByteCount = JsonReaderHelper.GetUtf8ByteCount(span);
			byte[] array = ArrayPool<byte>.Shared.Rent(utf8ByteCount);
			JsonDocument jsonDocument;
			try
			{
				int utf8FromText = JsonReaderHelper.GetUtf8FromText(span, array);
				jsonDocument = JsonDocument.Parse(array.AsMemory(0, utf8FromText), options.GetReaderOptions(), array, null);
			}
			catch
			{
				array.AsSpan(0, utf8ByteCount).Clear();
				ArrayPool<byte>.Shared.Return(array, false);
				throw;
			}
			return jsonDocument;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00007C34 File Offset: 0x00005E34
		internal static JsonDocument ParseValue(ReadOnlyMemory<char> json, JsonDocumentOptions options)
		{
			ReadOnlySpan<char> span = json.Span;
			int utf8ByteCount = JsonReaderHelper.GetUtf8ByteCount(span);
			byte[] array = ArrayPool<byte>.Shared.Rent(utf8ByteCount);
			byte[] array2;
			try
			{
				int utf8FromText = JsonReaderHelper.GetUtf8FromText(span, array);
				array2 = new byte[utf8FromText];
				Buffer.BlockCopy(array, 0, array2, 0, utf8FromText);
			}
			finally
			{
				array.AsSpan(0, utf8ByteCount).Clear();
				ArrayPool<byte>.Shared.Return(array, false);
			}
			return JsonDocument.ParseUnrented(array2.AsMemory<byte>(), options.GetReaderOptions(), JsonTokenType.None);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00007CC8 File Offset: 0x00005EC8
		[NullableContext(1)]
		public static JsonDocument Parse([StringSyntax("Json")] string json, JsonDocumentOptions options = default(JsonDocumentOptions))
		{
			if (json == null)
			{
				ThrowHelper.ThrowArgumentNullException("json");
			}
			return JsonDocument.Parse(json.AsMemory(), options);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00007CE3 File Offset: 0x00005EE3
		[NullableContext(2)]
		public static bool TryParseValue(ref Utf8JsonReader reader, [NotNullWhen(true)] out JsonDocument document)
		{
			return JsonDocument.TryParseValue(ref reader, out document, false, true);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00007CF0 File Offset: 0x00005EF0
		[NullableContext(1)]
		public static JsonDocument ParseValue(ref Utf8JsonReader reader)
		{
			JsonDocument jsonDocument;
			bool flag = JsonDocument.TryParseValue(ref reader, out jsonDocument, true, true);
			return jsonDocument;
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00007D0C File Offset: 0x00005F0C
		internal unsafe static bool TryParseValue(ref Utf8JsonReader reader, [NotNullWhen(true)] out JsonDocument document, bool shouldThrow, bool useArrayPools)
		{
			JsonReaderState currentState = reader.CurrentState;
			JsonDocument.CheckSupportedOptions(currentState.Options, "reader");
			Utf8JsonReader utf8JsonReader = reader;
			ReadOnlySpan<byte> readOnlySpan = default(ReadOnlySpan<byte>);
			ReadOnlySequence<byte> readOnlySequence = default(ReadOnlySequence<byte>);
			try
			{
				JsonTokenType tokenType = reader.TokenType;
				if ((tokenType == JsonTokenType.None || tokenType == JsonTokenType.PropertyName) && !reader.Read())
				{
					if (shouldThrow)
					{
						ThrowHelper.ThrowJsonReaderException(ref reader, ExceptionResource.ExpectedJsonTokens, 0, default(ReadOnlySpan<byte>));
					}
					reader = utf8JsonReader;
					document = null;
					return false;
				}
				switch (reader.TokenType)
				{
				case JsonTokenType.StartObject:
				case JsonTokenType.StartArray:
				{
					long tokenStartIndex = reader.TokenStartIndex;
					if (!reader.TrySkip())
					{
						if (shouldThrow)
						{
							ThrowHelper.ThrowJsonReaderException(ref reader, ExceptionResource.ExpectedJsonTokens, 0, default(ReadOnlySpan<byte>));
						}
						reader = utf8JsonReader;
						document = null;
						return false;
					}
					long num = reader.BytesConsumed - tokenStartIndex;
					ReadOnlySequence<byte> originalSequence = reader.OriginalSequence;
					if (originalSequence.IsEmpty)
					{
						readOnlySpan = checked(reader.OriginalSpan.Slice((int)tokenStartIndex, (int)num));
						goto IL_0251;
					}
					readOnlySequence = originalSequence.Slice(tokenStartIndex, num);
					goto IL_0251;
				}
				case JsonTokenType.String:
				{
					ReadOnlySequence<byte> originalSequence2 = reader.OriginalSequence;
					if (originalSequence2.IsEmpty)
					{
						int num2 = reader.ValueSpan.Length + 2;
						readOnlySpan = reader.OriginalSpan.Slice((int)reader.TokenStartIndex, num2);
						goto IL_0251;
					}
					long num3 = 2L;
					if (reader.HasValueSequence)
					{
						num3 += reader.ValueSequence.Length;
					}
					else
					{
						num3 += (long)reader.ValueSpan.Length;
					}
					readOnlySequence = originalSequence2.Slice(reader.TokenStartIndex, num3);
					goto IL_0251;
				}
				case JsonTokenType.Number:
					if (reader.HasValueSequence)
					{
						readOnlySequence = reader.ValueSequence;
						goto IL_0251;
					}
					readOnlySpan = reader.ValueSpan;
					goto IL_0251;
				case JsonTokenType.True:
				case JsonTokenType.False:
				case JsonTokenType.Null:
					if (!useArrayPools)
					{
						document = JsonDocument.CreateForLiteral(reader.TokenType);
						return true;
					}
					if (reader.HasValueSequence)
					{
						readOnlySequence = reader.ValueSequence;
						goto IL_0251;
					}
					readOnlySpan = reader.ValueSpan;
					goto IL_0251;
				}
				if (shouldThrow)
				{
					byte b = *reader.ValueSpan[0];
					ThrowHelper.ThrowJsonReaderException(ref reader, ExceptionResource.ExpectedStartOfValueNotFound, b, default(ReadOnlySpan<byte>));
				}
				reader = utf8JsonReader;
				document = null;
				return false;
				IL_0251:;
			}
			catch
			{
				reader = utf8JsonReader;
				throw;
			}
			int num4 = (readOnlySpan.IsEmpty ? checked((int)readOnlySequence.Length) : readOnlySpan.Length);
			if (useArrayPools)
			{
				byte[] array = ArrayPool<byte>.Shared.Rent(num4);
				Span<byte> span = array.AsSpan(0, num4);
				try
				{
					if (readOnlySpan.IsEmpty)
					{
						(in readOnlySequence).CopyTo(span);
					}
					else
					{
						readOnlySpan.CopyTo(span);
					}
					document = JsonDocument.Parse(array.AsMemory(0, num4), currentState.Options, array, null);
					return true;
				}
				catch
				{
					span.Clear();
					ArrayPool<byte>.Shared.Return(array, false);
					throw;
				}
			}
			byte[] array2;
			if (readOnlySpan.IsEmpty)
			{
				array2 = (in readOnlySequence).ToArray<byte>();
			}
			else
			{
				array2 = readOnlySpan.ToArray();
			}
			document = JsonDocument.ParseUnrented(array2, currentState.Options, reader.TokenType);
			return true;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00008078 File Offset: 0x00006278
		private static JsonDocument CreateForLiteral(JsonTokenType tokenType)
		{
			JsonDocument.<>c__DisplayClass73_0 CS$<>8__locals1;
			CS$<>8__locals1.tokenType = tokenType;
			JsonTokenType tokenType2 = CS$<>8__locals1.tokenType;
			if (tokenType2 == JsonTokenType.True)
			{
				if (JsonDocument.s_trueLiteral == null)
				{
					JsonDocument.s_trueLiteral = JsonDocument.<CreateForLiteral>g__Create|73_0(JsonConstants.TrueValue.ToArray(), ref CS$<>8__locals1);
				}
				return JsonDocument.s_trueLiteral;
			}
			if (tokenType2 == JsonTokenType.False)
			{
				if (JsonDocument.s_falseLiteral == null)
				{
					JsonDocument.s_falseLiteral = JsonDocument.<CreateForLiteral>g__Create|73_0(JsonConstants.FalseValue.ToArray(), ref CS$<>8__locals1);
				}
				return JsonDocument.s_falseLiteral;
			}
			if (JsonDocument.s_nullLiteral == null)
			{
				JsonDocument.s_nullLiteral = JsonDocument.<CreateForLiteral>g__Create|73_0(JsonConstants.NullValue.ToArray(), ref CS$<>8__locals1);
			}
			return JsonDocument.s_nullLiteral;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00008110 File Offset: 0x00006310
		private static JsonDocument Parse(ReadOnlyMemory<byte> utf8Json, JsonReaderOptions readerOptions, byte[] extraRentedArrayPoolBytes = null, PooledByteBufferWriter extraPooledByteBufferWriter = null)
		{
			ReadOnlySpan<byte> span = utf8Json.Span;
			JsonDocument.MetadataDb metadataDb = JsonDocument.MetadataDb.CreateRented(utf8Json.Length, false);
			JsonDocument.StackRowStack stackRowStack = new JsonDocument.StackRowStack(512);
			try
			{
				JsonDocument.Parse(span, readerOptions, ref metadataDb, ref stackRowStack);
			}
			catch
			{
				metadataDb.Dispose();
				throw;
			}
			finally
			{
				stackRowStack.Dispose();
			}
			return new JsonDocument(utf8Json, metadataDb, extraRentedArrayPoolBytes, extraPooledByteBufferWriter, true);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00008184 File Offset: 0x00006384
		private static JsonDocument ParseUnrented(ReadOnlyMemory<byte> utf8Json, JsonReaderOptions readerOptions, JsonTokenType tokenType = JsonTokenType.None)
		{
			ReadOnlySpan<byte> span = utf8Json.Span;
			JsonDocument.MetadataDb metadataDb;
			if (tokenType == JsonTokenType.String || tokenType == JsonTokenType.Number)
			{
				metadataDb = JsonDocument.MetadataDb.CreateLocked(utf8Json.Length);
				JsonDocument.StackRowStack stackRowStack = default(JsonDocument.StackRowStack);
				JsonDocument.Parse(span, readerOptions, ref metadataDb, ref stackRowStack);
			}
			else
			{
				metadataDb = JsonDocument.MetadataDb.CreateRented(utf8Json.Length, true);
				JsonDocument.StackRowStack stackRowStack2 = new JsonDocument.StackRowStack(512);
				try
				{
					JsonDocument.Parse(span, readerOptions, ref metadataDb, ref stackRowStack2);
				}
				finally
				{
					stackRowStack2.Dispose();
				}
			}
			return new JsonDocument(utf8Json, metadataDb, null, null, false);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000820C File Offset: 0x0000640C
		private static ArraySegment<byte> ReadToEnd(Stream stream)
		{
			int num = 0;
			byte[] array = null;
			ReadOnlySpan<byte> utf8Bom = JsonConstants.Utf8Bom;
			ArraySegment<byte> arraySegment;
			try
			{
				if (stream.CanSeek)
				{
					long num2 = Math.Max((long)utf8Bom.Length, stream.Length - stream.Position) + 1L;
					array = ArrayPool<byte>.Shared.Rent(checked((int)num2));
				}
				else
				{
					array = ArrayPool<byte>.Shared.Rent(4096);
				}
				int num3;
				do
				{
					num3 = stream.Read(array, num, utf8Bom.Length - num);
					num += num3;
				}
				while (num3 > 0 && num < utf8Bom.Length);
				if (num == utf8Bom.Length && utf8Bom.SequenceEqual(array.AsSpan(0, utf8Bom.Length)))
				{
					num = 0;
				}
				do
				{
					if (array.Length == num)
					{
						byte[] array2 = array;
						array = ArrayPool<byte>.Shared.Rent(checked(array2.Length * 2));
						Buffer.BlockCopy(array2, 0, array, 0, array2.Length);
						ArrayPool<byte>.Shared.Return(array2, true);
					}
					num3 = stream.Read(array, num, array.Length - num);
					num += num3;
				}
				while (num3 > 0);
				arraySegment = new ArraySegment<byte>(array, 0, num);
			}
			catch
			{
				if (array != null)
				{
					array.AsSpan(0, num).Clear();
					ArrayPool<byte>.Shared.Return(array, false);
				}
				throw;
			}
			return arraySegment;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00008344 File Offset: 0x00006544
		private static async Task<ArraySegment<byte>> ReadToEndAsync(Stream stream, CancellationToken cancellationToken)
		{
			int written = 0;
			byte[] rented = null;
			ArraySegment<byte> arraySegment;
			try
			{
				int utf8BomLength = JsonConstants.Utf8Bom.Length;
				if (stream.CanSeek)
				{
					long num = Math.Max((long)utf8BomLength, stream.Length - stream.Position) + 1L;
					rented = ArrayPool<byte>.Shared.Rent(checked((int)num));
				}
				else
				{
					rented = ArrayPool<byte>.Shared.Rent(4096);
				}
				int num3;
				do
				{
					int num2 = await stream.ReadAsync(rented, written, utf8BomLength - written, cancellationToken).ConfigureAwait(false);
					num3 = num2;
					written += num3;
				}
				while (num3 > 0 && written < utf8BomLength);
				if (written == utf8BomLength && JsonConstants.Utf8Bom.SequenceEqual(rented.AsSpan(0, utf8BomLength)))
				{
					written = 0;
				}
				do
				{
					if (rented.Length == written)
					{
						byte[] array = rented;
						rented = ArrayPool<byte>.Shared.Rent(array.Length * 2);
						Buffer.BlockCopy(array, 0, rented, 0, array.Length);
						ArrayPool<byte>.Shared.Return(array, true);
					}
					num3 = await stream.ReadAsync(rented, written, rented.Length - written, cancellationToken).ConfigureAwait(false);
					written += num3;
				}
				while (num3 > 0);
				arraySegment = new ArraySegment<byte>(rented, 0, written);
			}
			catch
			{
				if (rented != null)
				{
					rented.AsSpan(0, written).Clear();
					ArrayPool<byte>.Shared.Return(rented, false);
				}
				throw;
			}
			return arraySegment;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00008390 File Offset: 0x00006590
		internal unsafe bool TryGetNamedPropertyValue(int index, ReadOnlySpan<char> propertyName, out JsonElement value)
		{
			this.CheckNotDisposed();
			JsonDocument.DbRow dbRow = this._parsedData.Get(index);
			JsonDocument.CheckExpectedType(JsonTokenType.StartObject, dbRow.TokenType);
			if (dbRow.NumberOfRows == 1)
			{
				value = default(JsonElement);
				return false;
			}
			int maxByteCount = JsonReaderHelper.s_utf8Encoding.GetMaxByteCount(propertyName.Length);
			int num = index + 12;
			int num2 = checked(dbRow.NumberOfRows * 12 + index);
			if (maxByteCount < 256)
			{
				Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)256], 256);
				Span<byte> span2 = span;
				int utf8FromText = JsonReaderHelper.GetUtf8FromText(propertyName, span2);
				span2 = span2.Slice(0, utf8FromText);
				return this.TryGetNamedPropertyValue(num, num2, span2, out value);
			}
			int length = propertyName.Length;
			for (int i = num2 - 12; i > index; i -= 12)
			{
				int num3 = i;
				dbRow = this._parsedData.Get(i);
				if (dbRow.IsSimpleValue)
				{
					i -= 12;
				}
				else
				{
					i -= 12 * (dbRow.NumberOfRows + 1);
				}
				if (this._parsedData.Get(i).SizeOrLength >= length)
				{
					byte[] array = ArrayPool<byte>.Shared.Rent(maxByteCount);
					Span<byte> span3 = default(Span<byte>);
					try
					{
						int utf8FromText2 = JsonReaderHelper.GetUtf8FromText(propertyName, array);
						span3 = array.AsSpan(0, utf8FromText2);
						return this.TryGetNamedPropertyValue(num, num3 + 12, span3, out value);
					}
					finally
					{
						span3.Clear();
						ArrayPool<byte>.Shared.Return(array, false);
					}
				}
			}
			value = default(JsonElement);
			return false;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00008524 File Offset: 0x00006724
		internal bool TryGetNamedPropertyValue(int index, ReadOnlySpan<byte> propertyName, out JsonElement value)
		{
			this.CheckNotDisposed();
			JsonDocument.DbRow dbRow = this._parsedData.Get(index);
			JsonDocument.CheckExpectedType(JsonTokenType.StartObject, dbRow.TokenType);
			if (dbRow.NumberOfRows == 1)
			{
				value = default(JsonElement);
				return false;
			}
			int num = checked(dbRow.NumberOfRows * 12 + index);
			return this.TryGetNamedPropertyValue(index + 12, num, propertyName, out value);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00008580 File Offset: 0x00006780
		private unsafe bool TryGetNamedPropertyValue(int startIndex, int endIndex, ReadOnlySpan<byte> propertyName, out JsonElement value)
		{
			ReadOnlySpan<byte> span = this._utf8Json.Span;
			Span<byte> span2 = new Span<byte>(stackalloc byte[(UIntPtr)256], 256);
			Span<byte> span3 = span2;
			int i = endIndex - 12;
			while (i > startIndex)
			{
				JsonDocument.DbRow dbRow = this._parsedData.Get(i);
				if (dbRow.IsSimpleValue)
				{
					i -= 12;
				}
				else
				{
					i -= 12 * (dbRow.NumberOfRows + 1);
				}
				dbRow = this._parsedData.Get(i);
				ReadOnlySpan<byte> readOnlySpan = span.Slice(dbRow.Location, dbRow.SizeOrLength);
				if (!dbRow.HasComplexChildren)
				{
					goto IL_0172;
				}
				if (readOnlySpan.Length > propertyName.Length)
				{
					int num = readOnlySpan.IndexOf(92);
					if (propertyName.Length > num && readOnlySpan.Slice(0, num).SequenceEqual(propertyName.Slice(0, num)))
					{
						int num2 = readOnlySpan.Length - num;
						int num3 = 0;
						byte[] array = null;
						try
						{
							Span<byte> span4 = ((num2 <= span3.Length) ? span3 : (array = ArrayPool<byte>.Shared.Rent(num2)));
							JsonReaderHelper.Unescape(readOnlySpan.Slice(num), span4, 0, out num3);
							if (span4.Slice(0, num3).SequenceEqual(propertyName.Slice(num)))
							{
								value = new JsonElement(this, i + 12);
								return true;
							}
							goto IL_018F;
						}
						finally
						{
							if (array != null)
							{
								array.AsSpan(0, num3).Clear();
								ArrayPool<byte>.Shared.Return(array, false);
							}
						}
						goto IL_0172;
					}
				}
				IL_018F:
				i -= 12;
				continue;
				IL_0172:
				if (readOnlySpan.SequenceEqual(propertyName))
				{
					value = new JsonElement(this, i + 12);
					return true;
				}
				goto IL_018F;
			}
			value = default(JsonElement);
			return false;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00008744 File Offset: 0x00006944
		[CompilerGenerated]
		internal static JsonDocument <CreateForLiteral>g__Create|73_0(byte[] utf8Json, ref JsonDocument.<>c__DisplayClass73_0 A_1)
		{
			JsonDocument.MetadataDb metadataDb = JsonDocument.MetadataDb.CreateLocked(utf8Json.Length);
			metadataDb.Append(A_1.tokenType, 0, utf8Json.Length);
			return new JsonDocument(utf8Json, metadataDb, null, null, false);
		}

		// Token: 0x0400011D RID: 285
		private ReadOnlyMemory<byte> _utf8Json;

		// Token: 0x0400011E RID: 286
		private JsonDocument.MetadataDb _parsedData;

		// Token: 0x0400011F RID: 287
		private byte[] _extraRentedArrayPoolBytes;

		// Token: 0x04000120 RID: 288
		private PooledByteBufferWriter _extraPooledByteBufferWriter;

		// Token: 0x04000122 RID: 290
		private static JsonDocument s_nullLiteral;

		// Token: 0x04000123 RID: 291
		private static JsonDocument s_trueLiteral;

		// Token: 0x04000124 RID: 292
		private static JsonDocument s_falseLiteral;

		// Token: 0x04000125 RID: 293
		private const int UnseekableStreamInitialRentSize = 4096;

		// Token: 0x02000111 RID: 273
		internal readonly struct DbRow
		{
			// Token: 0x170002D0 RID: 720
			// (get) Token: 0x06000D2B RID: 3371 RVA: 0x000333BB File Offset: 0x000315BB
			internal int Location
			{
				get
				{
					return this._location;
				}
			}

			// Token: 0x170002D1 RID: 721
			// (get) Token: 0x06000D2C RID: 3372 RVA: 0x000333C3 File Offset: 0x000315C3
			internal int SizeOrLength
			{
				get
				{
					return this._sizeOrLengthUnion & int.MaxValue;
				}
			}

			// Token: 0x170002D2 RID: 722
			// (get) Token: 0x06000D2D RID: 3373 RVA: 0x000333D1 File Offset: 0x000315D1
			internal bool IsUnknownSize
			{
				get
				{
					return this._sizeOrLengthUnion == -1;
				}
			}

			// Token: 0x170002D3 RID: 723
			// (get) Token: 0x06000D2E RID: 3374 RVA: 0x000333DC File Offset: 0x000315DC
			internal bool HasComplexChildren
			{
				get
				{
					return this._sizeOrLengthUnion < 0;
				}
			}

			// Token: 0x170002D4 RID: 724
			// (get) Token: 0x06000D2F RID: 3375 RVA: 0x000333E7 File Offset: 0x000315E7
			internal int NumberOfRows
			{
				get
				{
					return this._numberOfRowsAndTypeUnion & 268435455;
				}
			}

			// Token: 0x170002D5 RID: 725
			// (get) Token: 0x06000D30 RID: 3376 RVA: 0x000333F5 File Offset: 0x000315F5
			internal JsonTokenType TokenType
			{
				get
				{
					return (JsonTokenType)((uint)this._numberOfRowsAndTypeUnion >> 28);
				}
			}

			// Token: 0x06000D31 RID: 3377 RVA: 0x00033401 File Offset: 0x00031601
			internal DbRow(JsonTokenType jsonTokenType, int location, int sizeOrLength)
			{
				this._location = location;
				this._sizeOrLengthUnion = sizeOrLength;
				this._numberOfRowsAndTypeUnion = (int)((int)jsonTokenType << 28);
			}

			// Token: 0x170002D6 RID: 726
			// (get) Token: 0x06000D32 RID: 3378 RVA: 0x0003341B File Offset: 0x0003161B
			internal bool IsSimpleValue
			{
				get
				{
					return this.TokenType >= JsonTokenType.PropertyName;
				}
			}

			// Token: 0x04000444 RID: 1092
			internal const int Size = 12;

			// Token: 0x04000445 RID: 1093
			private readonly int _location;

			// Token: 0x04000446 RID: 1094
			private readonly int _sizeOrLengthUnion;

			// Token: 0x04000447 RID: 1095
			private readonly int _numberOfRowsAndTypeUnion;

			// Token: 0x04000448 RID: 1096
			internal const int UnknownSize = -1;
		}

		// Token: 0x02000112 RID: 274
		private struct MetadataDb : IDisposable
		{
			// Token: 0x170002D7 RID: 727
			// (get) Token: 0x06000D33 RID: 3379 RVA: 0x00033429 File Offset: 0x00031629
			// (set) Token: 0x06000D34 RID: 3380 RVA: 0x00033431 File Offset: 0x00031631
			internal int Length { readonly get; private set; }

			// Token: 0x06000D35 RID: 3381 RVA: 0x0003343A File Offset: 0x0003163A
			private MetadataDb(byte[] initialDb, bool isLocked, bool convertToAlloc)
			{
				this._data = initialDb;
				this._isLocked = isLocked;
				this._convertToAlloc = convertToAlloc;
				this.Length = 0;
			}

			// Token: 0x06000D36 RID: 3382 RVA: 0x00033458 File Offset: 0x00031658
			internal MetadataDb(byte[] completeDb)
			{
				this._data = completeDb;
				this._isLocked = true;
				this._convertToAlloc = false;
				this.Length = completeDb.Length;
			}

			// Token: 0x06000D37 RID: 3383 RVA: 0x00033478 File Offset: 0x00031678
			internal static JsonDocument.MetadataDb CreateRented(int payloadLength, bool convertToAlloc)
			{
				int num = payloadLength + 12;
				if (num > 1048576 && num <= 4194304)
				{
					num = 1048576;
				}
				byte[] array = ArrayPool<byte>.Shared.Rent(num);
				return new JsonDocument.MetadataDb(array, false, convertToAlloc);
			}

			// Token: 0x06000D38 RID: 3384 RVA: 0x000334B4 File Offset: 0x000316B4
			internal static JsonDocument.MetadataDb CreateLocked(int payloadLength)
			{
				int num = payloadLength + 12;
				byte[] array = new byte[num];
				return new JsonDocument.MetadataDb(array, true, false);
			}

			// Token: 0x06000D39 RID: 3385 RVA: 0x000334D8 File Offset: 0x000316D8
			public void Dispose()
			{
				byte[] array = Interlocked.Exchange<byte[]>(ref this._data, null);
				if (array == null)
				{
					return;
				}
				ArrayPool<byte>.Shared.Return(array, false);
				this.Length = 0;
			}

			// Token: 0x06000D3A RID: 3386 RVA: 0x0003350C File Offset: 0x0003170C
			internal void CompleteAllocations()
			{
				if (!this._isLocked)
				{
					if (this._convertToAlloc)
					{
						byte[] data = this._data;
						this._data = this._data.AsSpan(0, this.Length).ToArray();
						this._isLocked = true;
						this._convertToAlloc = false;
						ArrayPool<byte>.Shared.Return(data, false);
						return;
					}
					if (this.Length <= this._data.Length / 2)
					{
						byte[] array = ArrayPool<byte>.Shared.Rent(this.Length);
						byte[] array2 = array;
						if (array.Length < this._data.Length)
						{
							Buffer.BlockCopy(this._data, 0, array, 0, this.Length);
							array2 = this._data;
							this._data = array;
						}
						ArrayPool<byte>.Shared.Return(array2, false);
					}
				}
			}

			// Token: 0x06000D3B RID: 3387 RVA: 0x000335D0 File Offset: 0x000317D0
			internal void Append(JsonTokenType tokenType, int startLocation, int length)
			{
				if (this.Length >= this._data.Length - 12)
				{
					this.Enlarge();
				}
				JsonDocument.DbRow dbRow = new JsonDocument.DbRow(tokenType, startLocation, length);
				MemoryMarshal.Write<JsonDocument.DbRow>(this._data.AsSpan(this.Length), ref dbRow);
				this.Length += 12;
			}

			// Token: 0x06000D3C RID: 3388 RVA: 0x00033628 File Offset: 0x00031828
			private void Enlarge()
			{
				byte[] data = this._data;
				int num = data.Length * 2;
				if (num > 2147483591)
				{
					num = 2147483591;
				}
				if (num == data.Length)
				{
					num = int.MaxValue;
				}
				this._data = ArrayPool<byte>.Shared.Rent(num);
				Buffer.BlockCopy(data, 0, this._data, 0, data.Length);
				ArrayPool<byte>.Shared.Return(data, false);
			}

			// Token: 0x06000D3D RID: 3389 RVA: 0x0003368A File Offset: 0x0003188A
			[Conditional("DEBUG")]
			private void AssertValidIndex(int index)
			{
			}

			// Token: 0x06000D3E RID: 3390 RVA: 0x0003368C File Offset: 0x0003188C
			internal void SetLength(int index, int length)
			{
				Span<byte> span = this._data.AsSpan(index + 4);
				MemoryMarshal.Write<int>(span, ref length);
			}

			// Token: 0x06000D3F RID: 3391 RVA: 0x000336B0 File Offset: 0x000318B0
			internal void SetNumberOfRows(int index, int numberOfRows)
			{
				Span<byte> span = this._data.AsSpan(index + 8);
				int num = MemoryMarshal.Read<int>(span);
				int num2 = (num & -268435456) | numberOfRows;
				MemoryMarshal.Write<int>(span, ref num2);
			}

			// Token: 0x06000D40 RID: 3392 RVA: 0x000336EC File Offset: 0x000318EC
			internal void SetHasComplexChildren(int index)
			{
				Span<byte> span = this._data.AsSpan(index + 4);
				int num = MemoryMarshal.Read<int>(span);
				int num2 = num | int.MinValue;
				MemoryMarshal.Write<int>(span, ref num2);
			}

			// Token: 0x06000D41 RID: 3393 RVA: 0x00033724 File Offset: 0x00031924
			internal int FindIndexOfFirstUnsetSizeOrLength(JsonTokenType lookupType)
			{
				return this.FindOpenElement(lookupType);
			}

			// Token: 0x06000D42 RID: 3394 RVA: 0x00033730 File Offset: 0x00031930
			private int FindOpenElement(JsonTokenType lookupType)
			{
				Span<byte> span = this._data.AsSpan(0, this.Length);
				for (int i = this.Length - 12; i >= 0; i -= 12)
				{
					JsonDocument.DbRow dbRow = MemoryMarshal.Read<JsonDocument.DbRow>(span.Slice(i));
					if (dbRow.IsUnknownSize && dbRow.TokenType == lookupType)
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x06000D43 RID: 3395 RVA: 0x0003378E File Offset: 0x0003198E
			internal JsonDocument.DbRow Get(int index)
			{
				return MemoryMarshal.Read<JsonDocument.DbRow>(this._data.AsSpan(index));
			}

			// Token: 0x06000D44 RID: 3396 RVA: 0x000337A8 File Offset: 0x000319A8
			internal JsonTokenType GetJsonTokenType(int index)
			{
				uint num = MemoryMarshal.Read<uint>(this._data.AsSpan(index + 8));
				return (JsonTokenType)(num >> 28);
			}

			// Token: 0x06000D45 RID: 3397 RVA: 0x000337D4 File Offset: 0x000319D4
			internal unsafe JsonDocument.MetadataDb CopySegment(int startIndex, int endIndex)
			{
				JsonDocument.DbRow dbRow = this.Get(startIndex);
				int num = endIndex - startIndex;
				byte[] array = new byte[num];
				this._data.AsSpan(startIndex, num).CopyTo(array);
				Span<int> span = MemoryMarshal.Cast<byte, int>(array);
				int num2 = *span[0];
				if (dbRow.TokenType == JsonTokenType.String)
				{
					num2--;
				}
				for (int i = (num - 12) / 4; i >= 0; i -= 3)
				{
					*span[i] -= num2;
				}
				return new JsonDocument.MetadataDb(array);
			}

			// Token: 0x04000449 RID: 1097
			private const int SizeOrLengthOffset = 4;

			// Token: 0x0400044A RID: 1098
			private const int NumberOfRowsOffset = 8;

			// Token: 0x0400044C RID: 1100
			private byte[] _data;

			// Token: 0x0400044D RID: 1101
			private bool _convertToAlloc;

			// Token: 0x0400044E RID: 1102
			private bool _isLocked;
		}

		// Token: 0x02000113 RID: 275
		private readonly struct StackRow
		{
			// Token: 0x06000D46 RID: 3398 RVA: 0x00033861 File Offset: 0x00031A61
			internal StackRow(int sizeOrLength = 0, int numberOfRows = -1)
			{
				this.SizeOrLength = sizeOrLength;
				this.NumberOfRows = numberOfRows;
			}

			// Token: 0x0400044F RID: 1103
			internal const int Size = 8;

			// Token: 0x04000450 RID: 1104
			internal readonly int SizeOrLength;

			// Token: 0x04000451 RID: 1105
			internal readonly int NumberOfRows;
		}

		// Token: 0x02000114 RID: 276
		private struct StackRowStack : IDisposable
		{
			// Token: 0x06000D47 RID: 3399 RVA: 0x00033871 File Offset: 0x00031A71
			public StackRowStack(int initialSize)
			{
				this._rentedBuffer = ArrayPool<byte>.Shared.Rent(initialSize);
				this._topOfStack = this._rentedBuffer.Length;
			}

			// Token: 0x06000D48 RID: 3400 RVA: 0x00033894 File Offset: 0x00031A94
			public void Dispose()
			{
				byte[] rentedBuffer = this._rentedBuffer;
				this._rentedBuffer = null;
				this._topOfStack = 0;
				if (rentedBuffer != null)
				{
					ArrayPool<byte>.Shared.Return(rentedBuffer, false);
				}
			}

			// Token: 0x06000D49 RID: 3401 RVA: 0x000338C5 File Offset: 0x00031AC5
			internal void Push(JsonDocument.StackRow row)
			{
				if (this._topOfStack < 8)
				{
					this.Enlarge();
				}
				this._topOfStack -= 8;
				MemoryMarshal.Write<JsonDocument.StackRow>(this._rentedBuffer.AsSpan(this._topOfStack), ref row);
			}

			// Token: 0x06000D4A RID: 3402 RVA: 0x000338FC File Offset: 0x00031AFC
			internal JsonDocument.StackRow Pop()
			{
				JsonDocument.StackRow stackRow = MemoryMarshal.Read<JsonDocument.StackRow>(this._rentedBuffer.AsSpan(this._topOfStack));
				this._topOfStack += 8;
				return stackRow;
			}

			// Token: 0x06000D4B RID: 3403 RVA: 0x00033934 File Offset: 0x00031B34
			private void Enlarge()
			{
				byte[] rentedBuffer = this._rentedBuffer;
				this._rentedBuffer = ArrayPool<byte>.Shared.Rent(rentedBuffer.Length * 2);
				Buffer.BlockCopy(rentedBuffer, this._topOfStack, this._rentedBuffer, this._rentedBuffer.Length - rentedBuffer.Length + this._topOfStack, rentedBuffer.Length - this._topOfStack);
				this._topOfStack += this._rentedBuffer.Length - rentedBuffer.Length;
				ArrayPool<byte>.Shared.Return(rentedBuffer, false);
			}

			// Token: 0x04000452 RID: 1106
			private byte[] _rentedBuffer;

			// Token: 0x04000453 RID: 1107
			private int _topOfStack;
		}
	}
}
