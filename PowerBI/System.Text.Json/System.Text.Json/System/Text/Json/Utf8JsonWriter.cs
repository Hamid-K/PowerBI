using System;
using System.Buffers;
using System.Buffers.Text;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Text.Json
{
	// Token: 0x0200005C RID: 92
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	public sealed class Utf8JsonWriter : IDisposable, IAsyncDisposable
	{
		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x000166BF File Offset: 0x000148BF
		// (set) Token: 0x06000561 RID: 1377 RVA: 0x000166C7 File Offset: 0x000148C7
		public int BytesPending { get; private set; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x000166D0 File Offset: 0x000148D0
		// (set) Token: 0x06000563 RID: 1379 RVA: 0x000166D8 File Offset: 0x000148D8
		public long BytesCommitted { get; private set; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x000166E1 File Offset: 0x000148E1
		public JsonWriterOptions Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x000166E9 File Offset: 0x000148E9
		private int Indentation
		{
			get
			{
				return this.CurrentDepth * 2;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x000166F3 File Offset: 0x000148F3
		internal JsonTokenType TokenType
		{
			get
			{
				return this._tokenType;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x000166FB File Offset: 0x000148FB
		public int CurrentDepth
		{
			get
			{
				return this._currentDepth & int.MaxValue;
			}
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00016709 File Offset: 0x00014909
		private Utf8JsonWriter()
		{
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00016711 File Offset: 0x00014911
		[NullableContext(1)]
		public Utf8JsonWriter(IBufferWriter<byte> bufferWriter, JsonWriterOptions options = default(JsonWriterOptions))
		{
			if (bufferWriter == null)
			{
				ThrowHelper.ThrowArgumentNullException("bufferWriter");
			}
			this._output = bufferWriter;
			this._options = options;
			if (this._options.MaxDepth == 0)
			{
				this._options.MaxDepth = 1000;
			}
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00016754 File Offset: 0x00014954
		[NullableContext(1)]
		public Utf8JsonWriter(Stream utf8Json, JsonWriterOptions options = default(JsonWriterOptions))
		{
			if (utf8Json == null)
			{
				ThrowHelper.ThrowArgumentNullException("utf8Json");
			}
			if (!utf8Json.CanWrite)
			{
				throw new ArgumentException(SR.StreamNotWritable);
			}
			this._stream = utf8Json;
			this._options = options;
			if (this._options.MaxDepth == 0)
			{
				this._options.MaxDepth = 1000;
			}
			this._arrayBufferWriter = new ArrayBufferWriter<byte>();
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x000167BD File Offset: 0x000149BD
		public void Reset()
		{
			this.CheckNotDisposed();
			ArrayBufferWriter<byte> arrayBufferWriter = this._arrayBufferWriter;
			if (arrayBufferWriter != null)
			{
				arrayBufferWriter.Clear();
			}
			this.ResetHelper();
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x000167DC File Offset: 0x000149DC
		[NullableContext(1)]
		public void Reset(Stream utf8Json)
		{
			this.CheckNotDisposed();
			if (utf8Json == null)
			{
				throw new ArgumentNullException("utf8Json");
			}
			if (!utf8Json.CanWrite)
			{
				throw new ArgumentException(SR.StreamNotWritable);
			}
			this._stream = utf8Json;
			if (this._arrayBufferWriter == null)
			{
				this._arrayBufferWriter = new ArrayBufferWriter<byte>();
			}
			else
			{
				this._arrayBufferWriter.Clear();
			}
			this._output = null;
			this.ResetHelper();
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00016844 File Offset: 0x00014A44
		[NullableContext(1)]
		public void Reset(IBufferWriter<byte> bufferWriter)
		{
			this.CheckNotDisposed();
			if (bufferWriter == null)
			{
				throw new ArgumentNullException("bufferWriter");
			}
			this._output = bufferWriter;
			this._stream = null;
			this._arrayBufferWriter = null;
			this.ResetHelper();
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00016876 File Offset: 0x00014A76
		internal void ResetAllStateForCacheReuse()
		{
			this.ResetHelper();
			this._stream = null;
			this._arrayBufferWriter = null;
			this._output = null;
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00016893 File Offset: 0x00014A93
		internal void Reset(IBufferWriter<byte> bufferWriter, JsonWriterOptions options)
		{
			this._output = bufferWriter;
			this._options = options;
			if (this._options.MaxDepth == 0)
			{
				this._options.MaxDepth = 1000;
			}
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x000168C0 File Offset: 0x00014AC0
		internal static Utf8JsonWriter CreateEmptyInstanceForCaching()
		{
			return new Utf8JsonWriter();
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x000168C8 File Offset: 0x00014AC8
		private void ResetHelper()
		{
			this.BytesPending = 0;
			this.BytesCommitted = 0L;
			this._memory = default(Memory<byte>);
			this._inObject = false;
			this._tokenType = JsonTokenType.None;
			this._commentAfterNoneOrPropertyName = false;
			this._currentDepth = 0;
			this._bitStack = default(BitStack);
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00016918 File Offset: 0x00014B18
		private void CheckNotDisposed()
		{
			if (this._stream == null && this._output == null)
			{
				ThrowHelper.ThrowObjectDisposedException_Utf8JsonWriter();
			}
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00016930 File Offset: 0x00014B30
		public void Flush()
		{
			this.CheckNotDisposed();
			this._memory = default(Memory<byte>);
			if (this._stream != null)
			{
				if (this.BytesPending != 0)
				{
					this._arrayBufferWriter.Advance(this.BytesPending);
					this.BytesPending = 0;
					ArraySegment<byte> arraySegment;
					bool flag = MemoryMarshal.TryGetArray<byte>(this._arrayBufferWriter.WrittenMemory, out arraySegment);
					this._stream.Write(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
					this.BytesCommitted += (long)this._arrayBufferWriter.WrittenCount;
					this._arrayBufferWriter.Clear();
				}
				this._stream.Flush();
				return;
			}
			if (this.BytesPending != 0)
			{
				this._output.Advance(this.BytesPending);
				this.BytesCommitted += (long)this.BytesPending;
				this.BytesPending = 0;
			}
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00016A11 File Offset: 0x00014C11
		public void Dispose()
		{
			if (this._stream == null && this._output == null)
			{
				return;
			}
			this.Flush();
			this.ResetHelper();
			this._stream = null;
			this._arrayBufferWriter = null;
			this._output = null;
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00016A48 File Offset: 0x00014C48
		public async ValueTask DisposeAsync()
		{
			if (this._stream != null || this._output != null)
			{
				await this.FlushAsync(default(CancellationToken)).ConfigureAwait(false);
				this.ResetHelper();
				this._stream = null;
				this._arrayBufferWriter = null;
				this._output = null;
			}
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00016A8C File Offset: 0x00014C8C
		[NullableContext(1)]
		public async Task FlushAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			this.CheckNotDisposed();
			this._memory = default(Memory<byte>);
			if (this._stream != null)
			{
				if (this.BytesPending != 0)
				{
					this._arrayBufferWriter.Advance(this.BytesPending);
					this.BytesPending = 0;
					ArraySegment<byte> arraySegment;
					bool flag = MemoryMarshal.TryGetArray<byte>(this._arrayBufferWriter.WrittenMemory, out arraySegment);
					await this._stream.WriteAsync(arraySegment.Array, arraySegment.Offset, arraySegment.Count, cancellationToken).ConfigureAwait(false);
					this.BytesCommitted += (long)this._arrayBufferWriter.WrittenCount;
					this._arrayBufferWriter.Clear();
				}
				await this._stream.FlushAsync(cancellationToken).ConfigureAwait(false);
			}
			else if (this.BytesPending != 0)
			{
				this._output.Advance(this.BytesPending);
				this.BytesCommitted += (long)this.BytesPending;
				this.BytesPending = 0;
			}
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00016AD7 File Offset: 0x00014CD7
		public void WriteStartArray()
		{
			this.WriteStart(91);
			this._tokenType = JsonTokenType.StartArray;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00016AE8 File Offset: 0x00014CE8
		public void WriteStartObject()
		{
			this.WriteStart(123);
			this._tokenType = JsonTokenType.StartObject;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00016AFC File Offset: 0x00014CFC
		private void WriteStart(byte token)
		{
			if (this.CurrentDepth >= this._options.MaxDepth)
			{
				ThrowHelper.ThrowInvalidOperationException(ExceptionResource.DepthTooLarge, this._currentDepth, this._options.MaxDepth, 0, JsonTokenType.None);
			}
			if (this._options.IndentedOrNotSkipValidation)
			{
				this.WriteStartSlow(token);
			}
			else
			{
				this.WriteStartMinimized(token);
			}
			this._currentDepth &= int.MaxValue;
			this._currentDepth++;
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00016B74 File Offset: 0x00014D74
		private unsafe void WriteStartMinimized(byte token)
		{
			if (this._memory.Length - this.BytesPending < 2)
			{
				this.Grow(2);
			}
			Span<byte> span = this._memory.Span;
			int num;
			if (this._currentDepth < 0)
			{
				num = this.BytesPending;
				this.BytesPending = num + 1;
				*span[num] = 44;
			}
			num = this.BytesPending;
			this.BytesPending = num + 1;
			*span[num] = token;
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00016BE8 File Offset: 0x00014DE8
		private void WriteStartSlow(byte token)
		{
			if (this._options.Indented)
			{
				if (!this._options.SkipValidation)
				{
					this.ValidateStart();
					this.UpdateBitStackOnStart(token);
				}
				this.WriteStartIndented(token);
				return;
			}
			this.ValidateStart();
			this.UpdateBitStackOnStart(token);
			this.WriteStartMinimized(token);
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00016C38 File Offset: 0x00014E38
		private void ValidateStart()
		{
			if (this._inObject)
			{
				if (this._tokenType != JsonTokenType.PropertyName)
				{
					ThrowHelper.ThrowInvalidOperationException(ExceptionResource.CannotStartObjectArrayWithoutProperty, 0, this._options.MaxDepth, 0, this._tokenType);
					return;
				}
			}
			else if (this.CurrentDepth == 0 && this._tokenType != JsonTokenType.None)
			{
				ThrowHelper.ThrowInvalidOperationException(ExceptionResource.CannotStartObjectArrayAfterPrimitiveOrClose, 0, this._options.MaxDepth, 0, this._tokenType);
			}
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00016C9C File Offset: 0x00014E9C
		private unsafe void WriteStartIndented(byte token)
		{
			int indentation = this.Indentation;
			int num = indentation + 1;
			int num2 = num + 3;
			if (this._memory.Length - this.BytesPending < num2)
			{
				this.Grow(num2);
			}
			Span<byte> span = this._memory.Span;
			int num3;
			if (this._currentDepth < 0)
			{
				num3 = this.BytesPending;
				this.BytesPending = num3 + 1;
				*span[num3] = 44;
			}
			JsonTokenType tokenType = this._tokenType;
			if ((tokenType != JsonTokenType.PropertyName && tokenType != JsonTokenType.None) || this._commentAfterNoneOrPropertyName)
			{
				this.WriteNewLine(span);
				JsonWriterHelper.WriteIndentation(span.Slice(this.BytesPending), indentation);
				this.BytesPending += indentation;
			}
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = token;
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00016D66 File Offset: 0x00014F66
		public void WriteStartArray(JsonEncodedText propertyName)
		{
			this.WriteStartHelper(propertyName.EncodedUtf8Bytes, 91);
			this._tokenType = JsonTokenType.StartArray;
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00016D7E File Offset: 0x00014F7E
		public void WriteStartObject(JsonEncodedText propertyName)
		{
			this.WriteStartHelper(propertyName.EncodedUtf8Bytes, 123);
			this._tokenType = JsonTokenType.StartObject;
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00016D96 File Offset: 0x00014F96
		private void WriteStartHelper(ReadOnlySpan<byte> utf8PropertyName, byte token)
		{
			this.ValidateDepth();
			this.WriteStartByOptions(utf8PropertyName, token);
			this._currentDepth &= int.MaxValue;
			this._currentDepth++;
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00016DC6 File Offset: 0x00014FC6
		public void WriteStartArray(ReadOnlySpan<byte> utf8PropertyName)
		{
			this.ValidatePropertyNameAndDepth(utf8PropertyName);
			this.WriteStartEscape(utf8PropertyName, 91);
			this._currentDepth &= int.MaxValue;
			this._currentDepth++;
			this._tokenType = JsonTokenType.StartArray;
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00016DFF File Offset: 0x00014FFF
		public void WriteStartObject(ReadOnlySpan<byte> utf8PropertyName)
		{
			this.ValidatePropertyNameAndDepth(utf8PropertyName);
			this.WriteStartEscape(utf8PropertyName, 123);
			this._currentDepth &= int.MaxValue;
			this._currentDepth++;
			this._tokenType = JsonTokenType.StartObject;
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00016E38 File Offset: 0x00015038
		private void WriteStartEscape(ReadOnlySpan<byte> utf8PropertyName, byte token)
		{
			int num = JsonWriterHelper.NeedsEscaping(utf8PropertyName, this._options.Encoder);
			if (num != -1)
			{
				this.WriteStartEscapeProperty(utf8PropertyName, token, num);
				return;
			}
			this.WriteStartByOptions(utf8PropertyName, token);
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00016E6D File Offset: 0x0001506D
		private void WriteStartByOptions(ReadOnlySpan<byte> utf8PropertyName, byte token)
		{
			this.ValidateWritingProperty(token);
			if (this._options.Indented)
			{
				this.WritePropertyNameIndented(utf8PropertyName, token);
				return;
			}
			this.WritePropertyNameMinimized(utf8PropertyName, token);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00016E94 File Offset: 0x00015094
		private unsafe void WriteStartEscapeProperty(ReadOnlySpan<byte> utf8PropertyName, byte token, int firstEscapeIndexProp)
		{
			byte[] array = null;
			int maxEscapedLength = JsonWriterHelper.GetMaxEscapedLength(utf8PropertyName.Length, firstEscapeIndexProp);
			Span<byte> span2;
			if (maxEscapedLength <= 256)
			{
				Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)256], 256);
				span2 = span;
			}
			else
			{
				span2 = (array = ArrayPool<byte>.Shared.Rent(maxEscapedLength));
			}
			Span<byte> span3 = span2;
			int num;
			JsonWriterHelper.EscapeString(utf8PropertyName, span3, firstEscapeIndexProp, this._options.Encoder, out num);
			this.WriteStartByOptions(span3.Slice(0, num), token);
			if (array != null)
			{
				ArrayPool<byte>.Shared.Return(array, false);
			}
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00016F23 File Offset: 0x00015123
		[NullableContext(1)]
		public void WriteStartArray(string propertyName)
		{
			if (propertyName == null)
			{
				ThrowHelper.ThrowArgumentNullException("propertyName");
			}
			this.WriteStartArray(propertyName.AsSpan());
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00016F3E File Offset: 0x0001513E
		[NullableContext(1)]
		public void WriteStartObject(string propertyName)
		{
			if (propertyName == null)
			{
				ThrowHelper.ThrowArgumentNullException("propertyName");
			}
			this.WriteStartObject(propertyName.AsSpan());
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00016F59 File Offset: 0x00015159
		public void WriteStartArray(ReadOnlySpan<char> propertyName)
		{
			this.ValidatePropertyNameAndDepth(propertyName);
			this.WriteStartEscape(propertyName, 91);
			this._currentDepth &= int.MaxValue;
			this._currentDepth++;
			this._tokenType = JsonTokenType.StartArray;
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00016F92 File Offset: 0x00015192
		public void WriteStartObject(ReadOnlySpan<char> propertyName)
		{
			this.ValidatePropertyNameAndDepth(propertyName);
			this.WriteStartEscape(propertyName, 123);
			this._currentDepth &= int.MaxValue;
			this._currentDepth++;
			this._tokenType = JsonTokenType.StartObject;
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00016FCC File Offset: 0x000151CC
		private void WriteStartEscape(ReadOnlySpan<char> propertyName, byte token)
		{
			int num = JsonWriterHelper.NeedsEscaping(propertyName, this._options.Encoder);
			if (num != -1)
			{
				this.WriteStartEscapeProperty(propertyName, token, num);
				return;
			}
			this.WriteStartByOptions(propertyName, token);
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00017001 File Offset: 0x00015201
		private void WriteStartByOptions(ReadOnlySpan<char> propertyName, byte token)
		{
			this.ValidateWritingProperty(token);
			if (this._options.Indented)
			{
				this.WritePropertyNameIndented(propertyName, token);
				return;
			}
			this.WritePropertyNameMinimized(propertyName, token);
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00017028 File Offset: 0x00015228
		private unsafe void WriteStartEscapeProperty(ReadOnlySpan<char> propertyName, byte token, int firstEscapeIndexProp)
		{
			char[] array = null;
			int maxEscapedLength = JsonWriterHelper.GetMaxEscapedLength(propertyName.Length, firstEscapeIndexProp);
			Span<char> span2;
			if (maxEscapedLength <= 128)
			{
				Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)256], 128);
				span2 = span;
			}
			else
			{
				span2 = (array = ArrayPool<char>.Shared.Rent(maxEscapedLength));
			}
			Span<char> span3 = span2;
			int num;
			JsonWriterHelper.EscapeString(propertyName, span3, firstEscapeIndexProp, this._options.Encoder, out num);
			this.WriteStartByOptions(span3.Slice(0, num), token);
			if (array != null)
			{
				ArrayPool<char>.Shared.Return(array, false);
			}
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x000170B7 File Offset: 0x000152B7
		public void WriteEndArray()
		{
			this.WriteEnd(93);
			this._tokenType = JsonTokenType.EndArray;
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x000170C8 File Offset: 0x000152C8
		public void WriteEndObject()
		{
			this.WriteEnd(125);
			this._tokenType = JsonTokenType.EndObject;
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x000170D9 File Offset: 0x000152D9
		private void WriteEnd(byte token)
		{
			if (this._options.IndentedOrNotSkipValidation)
			{
				this.WriteEndSlow(token);
			}
			else
			{
				this.WriteEndMinimized(token);
			}
			this.SetFlagToAddListSeparatorBeforeNextItem();
			if (this.CurrentDepth != 0)
			{
				this._currentDepth--;
			}
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00017114 File Offset: 0x00015314
		private unsafe void WriteEndMinimized(byte token)
		{
			if (this._memory.Length - this.BytesPending < 1)
			{
				this.Grow(1);
			}
			Span<byte> span = this._memory.Span;
			int bytesPending = this.BytesPending;
			this.BytesPending = bytesPending + 1;
			*span[bytesPending] = token;
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x00017163 File Offset: 0x00015363
		private void WriteEndSlow(byte token)
		{
			if (this._options.Indented)
			{
				if (!this._options.SkipValidation)
				{
					this.ValidateEnd(token);
				}
				this.WriteEndIndented(token);
				return;
			}
			this.ValidateEnd(token);
			this.WriteEndMinimized(token);
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0001719C File Offset: 0x0001539C
		private void ValidateEnd(byte token)
		{
			if (this._bitStack.CurrentDepth <= 0 || this._tokenType == JsonTokenType.PropertyName)
			{
				ThrowHelper.ThrowInvalidOperationException(ExceptionResource.MismatchedObjectArray, 0, this._options.MaxDepth, token, this._tokenType);
			}
			if (token == 93)
			{
				if (this._inObject)
				{
					ThrowHelper.ThrowInvalidOperationException(ExceptionResource.MismatchedObjectArray, 0, this._options.MaxDepth, token, this._tokenType);
				}
			}
			else if (!this._inObject)
			{
				ThrowHelper.ThrowInvalidOperationException(ExceptionResource.MismatchedObjectArray, 0, this._options.MaxDepth, token, this._tokenType);
			}
			this._inObject = this._bitStack.Pop();
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00017238 File Offset: 0x00015438
		private unsafe void WriteEndIndented(byte token)
		{
			if (this._tokenType == JsonTokenType.StartObject || this._tokenType == JsonTokenType.StartArray)
			{
				this.WriteEndMinimized(token);
				return;
			}
			int num = this.Indentation;
			if (num != 0)
			{
				num -= 2;
			}
			int num2 = num + 3;
			if (this._memory.Length - this.BytesPending < num2)
			{
				this.Grow(num2);
			}
			Span<byte> span = this._memory.Span;
			this.WriteNewLine(span);
			JsonWriterHelper.WriteIndentation(span.Slice(this.BytesPending), num);
			this.BytesPending += num;
			int bytesPending = this.BytesPending;
			this.BytesPending = bytesPending + 1;
			*span[bytesPending] = token;
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x000172DC File Offset: 0x000154DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe void WriteNewLine(Span<byte> output)
		{
			int num;
			if (Utf8JsonWriter.s_newLineLength == 2)
			{
				num = this.BytesPending;
				this.BytesPending = num + 1;
				*output[num] = 13;
			}
			num = this.BytesPending;
			this.BytesPending = num + 1;
			*output[num] = 10;
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00017327 File Offset: 0x00015527
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void UpdateBitStackOnStart(byte token)
		{
			if (token == 91)
			{
				this._bitStack.PushFalse();
				this._inObject = false;
				return;
			}
			this._bitStack.PushTrue();
			this._inObject = true;
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00017354 File Offset: 0x00015554
		private void Grow(int requiredSize)
		{
			if (this._memory.Length == 0)
			{
				this.FirstCallToGetMemory(requiredSize);
				return;
			}
			int num = Math.Max(4096, requiredSize);
			if (this._stream != null)
			{
				int num2 = this.BytesPending + num;
				JsonHelpers.ValidateInt32MaxArrayLength((uint)num2);
				this._memory = this._arrayBufferWriter.GetMemory(num2);
				return;
			}
			this._output.Advance(this.BytesPending);
			this.BytesCommitted += (long)this.BytesPending;
			this.BytesPending = 0;
			this._memory = this._output.GetMemory(num);
			if (this._memory.Length < num)
			{
				ThrowHelper.ThrowInvalidOperationException_NeedLargerSpan();
			}
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00017400 File Offset: 0x00015600
		private void FirstCallToGetMemory(int requiredSize)
		{
			int num = Math.Max(256, requiredSize);
			if (this._stream != null)
			{
				this._memory = this._arrayBufferWriter.GetMemory(num);
				return;
			}
			this._memory = this._output.GetMemory(num);
			if (this._memory.Length < num)
			{
				ThrowHelper.ThrowInvalidOperationException_NeedLargerSpan();
			}
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00017459 File Offset: 0x00015659
		private void SetFlagToAddListSeparatorBeforeNextItem()
		{
			this._currentDepth |= int.MinValue;
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x0001746D File Offset: 0x0001566D
		[Nullable(1)]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string DebuggerDisplay
		{
			get
			{
				return string.Format("BytesCommitted = {0} BytesPending = {1} CurrentDepth = {2}", this.BytesCommitted, this.BytesPending, this.CurrentDepth);
			}
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0001749C File Offset: 0x0001569C
		public void WriteBase64String(JsonEncodedText propertyName, ReadOnlySpan<byte> bytes)
		{
			ReadOnlySpan<byte> encodedUtf8Bytes = propertyName.EncodedUtf8Bytes;
			this.WriteBase64ByOptions(encodedUtf8Bytes, bytes);
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.String;
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x000174C6 File Offset: 0x000156C6
		public void WriteBase64String([Nullable(1)] string propertyName, ReadOnlySpan<byte> bytes)
		{
			if (propertyName == null)
			{
				ThrowHelper.ThrowArgumentNullException("propertyName");
			}
			this.WriteBase64String(propertyName.AsSpan(), bytes);
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x000174E2 File Offset: 0x000156E2
		public void WriteBase64String(ReadOnlySpan<char> propertyName, ReadOnlySpan<byte> bytes)
		{
			JsonWriterHelper.ValidatePropertyNameLength(propertyName);
			this.WriteBase64Escape(propertyName, bytes);
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.String;
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x000174FF File Offset: 0x000156FF
		public void WriteBase64String(ReadOnlySpan<byte> utf8PropertyName, ReadOnlySpan<byte> bytes)
		{
			JsonWriterHelper.ValidatePropertyNameLength(utf8PropertyName);
			this.WriteBase64Escape(utf8PropertyName, bytes);
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.String;
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x0001751C File Offset: 0x0001571C
		private void WriteBase64Escape(ReadOnlySpan<char> propertyName, ReadOnlySpan<byte> bytes)
		{
			int num = JsonWriterHelper.NeedsEscaping(propertyName, this._options.Encoder);
			if (num != -1)
			{
				this.WriteBase64EscapeProperty(propertyName, bytes, num);
				return;
			}
			this.WriteBase64ByOptions(propertyName, bytes);
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x00017554 File Offset: 0x00015754
		private void WriteBase64Escape(ReadOnlySpan<byte> utf8PropertyName, ReadOnlySpan<byte> bytes)
		{
			int num = JsonWriterHelper.NeedsEscaping(utf8PropertyName, this._options.Encoder);
			if (num != -1)
			{
				this.WriteBase64EscapeProperty(utf8PropertyName, bytes, num);
				return;
			}
			this.WriteBase64ByOptions(utf8PropertyName, bytes);
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0001758C File Offset: 0x0001578C
		private unsafe void WriteBase64EscapeProperty(ReadOnlySpan<char> propertyName, ReadOnlySpan<byte> bytes, int firstEscapeIndexProp)
		{
			char[] array = null;
			int maxEscapedLength = JsonWriterHelper.GetMaxEscapedLength(propertyName.Length, firstEscapeIndexProp);
			Span<char> span2;
			if (maxEscapedLength <= 128)
			{
				Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)256], 128);
				span2 = span;
			}
			else
			{
				span2 = (array = ArrayPool<char>.Shared.Rent(maxEscapedLength));
			}
			Span<char> span3 = span2;
			int num;
			JsonWriterHelper.EscapeString(propertyName, span3, firstEscapeIndexProp, this._options.Encoder, out num);
			this.WriteBase64ByOptions(span3.Slice(0, num), bytes);
			if (array != null)
			{
				ArrayPool<char>.Shared.Return(array, false);
			}
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0001761C File Offset: 0x0001581C
		private unsafe void WriteBase64EscapeProperty(ReadOnlySpan<byte> utf8PropertyName, ReadOnlySpan<byte> bytes, int firstEscapeIndexProp)
		{
			byte[] array = null;
			int maxEscapedLength = JsonWriterHelper.GetMaxEscapedLength(utf8PropertyName.Length, firstEscapeIndexProp);
			Span<byte> span2;
			if (maxEscapedLength <= 256)
			{
				Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)256], 256);
				span2 = span;
			}
			else
			{
				span2 = (array = ArrayPool<byte>.Shared.Rent(maxEscapedLength));
			}
			Span<byte> span3 = span2;
			int num;
			JsonWriterHelper.EscapeString(utf8PropertyName, span3, firstEscapeIndexProp, this._options.Encoder, out num);
			this.WriteBase64ByOptions(span3.Slice(0, num), bytes);
			if (array != null)
			{
				ArrayPool<byte>.Shared.Return(array, false);
			}
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x000176AB File Offset: 0x000158AB
		private void WriteBase64ByOptions(ReadOnlySpan<char> propertyName, ReadOnlySpan<byte> bytes)
		{
			this.ValidateWritingProperty();
			if (this._options.Indented)
			{
				this.WriteBase64Indented(propertyName, bytes);
				return;
			}
			this.WriteBase64Minimized(propertyName, bytes);
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x000176D1 File Offset: 0x000158D1
		private void WriteBase64ByOptions(ReadOnlySpan<byte> utf8PropertyName, ReadOnlySpan<byte> bytes)
		{
			this.ValidateWritingProperty();
			if (this._options.Indented)
			{
				this.WriteBase64Indented(utf8PropertyName, bytes);
				return;
			}
			this.WriteBase64Minimized(utf8PropertyName, bytes);
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x000176F8 File Offset: 0x000158F8
		private unsafe void WriteBase64Minimized(ReadOnlySpan<char> escapedPropertyName, ReadOnlySpan<byte> bytes)
		{
			int maxEncodedToUtf8Length = Base64.GetMaxEncodedToUtf8Length(bytes.Length);
			int num = escapedPropertyName.Length * 3 + maxEncodedToUtf8Length + 6;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			int num2;
			if (this._currentDepth < 0)
			{
				num2 = this.BytesPending;
				this.BytesPending = num2 + 1;
				*span[num2] = 44;
			}
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			this.TranscodeAndWrite(escapedPropertyName, span);
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 58;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			this.Base64EncodeAndWrite(bytes, span, maxEncodedToUtf8Length);
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00017804 File Offset: 0x00015A04
		private unsafe void WriteBase64Minimized(ReadOnlySpan<byte> escapedPropertyName, ReadOnlySpan<byte> bytes)
		{
			int maxEncodedToUtf8Length = Base64.GetMaxEncodedToUtf8Length(bytes.Length);
			int num = escapedPropertyName.Length + maxEncodedToUtf8Length + 6;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			int num2;
			if (this._currentDepth < 0)
			{
				num2 = this.BytesPending;
				this.BytesPending = num2 + 1;
				*span[num2] = 44;
			}
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			escapedPropertyName.CopyTo(span.Slice(this.BytesPending));
			this.BytesPending += escapedPropertyName.Length;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 58;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			this.Base64EncodeAndWrite(bytes, span, maxEncodedToUtf8Length);
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00017930 File Offset: 0x00015B30
		private unsafe void WriteBase64Indented(ReadOnlySpan<char> escapedPropertyName, ReadOnlySpan<byte> bytes)
		{
			int indentation = this.Indentation;
			int maxEncodedToUtf8Length = Base64.GetMaxEncodedToUtf8Length(bytes.Length);
			int num = indentation + escapedPropertyName.Length * 3 + maxEncodedToUtf8Length + 7 + Utf8JsonWriter.s_newLineLength;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			int num2;
			if (this._currentDepth < 0)
			{
				num2 = this.BytesPending;
				this.BytesPending = num2 + 1;
				*span[num2] = 44;
			}
			if (this._tokenType != JsonTokenType.None)
			{
				this.WriteNewLine(span);
			}
			JsonWriterHelper.WriteIndentation(span.Slice(this.BytesPending), indentation);
			this.BytesPending += indentation;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			this.TranscodeAndWrite(escapedPropertyName, span);
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 58;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 32;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			this.Base64EncodeAndWrite(bytes, span, maxEncodedToUtf8Length);
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00017AAC File Offset: 0x00015CAC
		private unsafe void WriteBase64Indented(ReadOnlySpan<byte> escapedPropertyName, ReadOnlySpan<byte> bytes)
		{
			int indentation = this.Indentation;
			int maxEncodedToUtf8Length = Base64.GetMaxEncodedToUtf8Length(bytes.Length);
			int num = indentation + escapedPropertyName.Length + maxEncodedToUtf8Length + 7 + Utf8JsonWriter.s_newLineLength;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			int num2;
			if (this._currentDepth < 0)
			{
				num2 = this.BytesPending;
				this.BytesPending = num2 + 1;
				*span[num2] = 44;
			}
			if (this._tokenType != JsonTokenType.None)
			{
				this.WriteNewLine(span);
			}
			JsonWriterHelper.WriteIndentation(span.Slice(this.BytesPending), indentation);
			this.BytesPending += indentation;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			escapedPropertyName.CopyTo(span.Slice(this.BytesPending));
			this.BytesPending += escapedPropertyName.Length;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 58;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 32;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			this.Base64EncodeAndWrite(bytes, span, maxEncodedToUtf8Length);
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00017C48 File Offset: 0x00015E48
		public void WriteString(JsonEncodedText propertyName, DateTime value)
		{
			ReadOnlySpan<byte> encodedUtf8Bytes = propertyName.EncodedUtf8Bytes;
			this.WriteStringByOptions(encodedUtf8Bytes, value);
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.String;
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00017C72 File Offset: 0x00015E72
		[NullableContext(1)]
		public void WriteString(string propertyName, DateTime value)
		{
			if (propertyName == null)
			{
				ThrowHelper.ThrowArgumentNullException("propertyName");
			}
			this.WriteString(propertyName.AsSpan(), value);
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00017C8E File Offset: 0x00015E8E
		public void WriteString(ReadOnlySpan<char> propertyName, DateTime value)
		{
			JsonWriterHelper.ValidateProperty(propertyName);
			this.WriteStringEscape(propertyName, value);
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.String;
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00017CAB File Offset: 0x00015EAB
		public void WriteString(ReadOnlySpan<byte> utf8PropertyName, DateTime value)
		{
			JsonWriterHelper.ValidateProperty(utf8PropertyName);
			this.WriteStringEscape(utf8PropertyName, value);
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.String;
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00017CC8 File Offset: 0x00015EC8
		private void WriteStringEscape(ReadOnlySpan<char> propertyName, DateTime value)
		{
			int num = JsonWriterHelper.NeedsEscaping(propertyName, this._options.Encoder);
			if (num != -1)
			{
				this.WriteStringEscapeProperty(propertyName, value, num);
				return;
			}
			this.WriteStringByOptions(propertyName, value);
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00017D00 File Offset: 0x00015F00
		private void WriteStringEscape(ReadOnlySpan<byte> utf8PropertyName, DateTime value)
		{
			int num = JsonWriterHelper.NeedsEscaping(utf8PropertyName, this._options.Encoder);
			if (num != -1)
			{
				this.WriteStringEscapeProperty(utf8PropertyName, value, num);
				return;
			}
			this.WriteStringByOptions(utf8PropertyName, value);
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00017D38 File Offset: 0x00015F38
		private unsafe void WriteStringEscapeProperty(ReadOnlySpan<char> propertyName, DateTime value, int firstEscapeIndexProp)
		{
			char[] array = null;
			int maxEscapedLength = JsonWriterHelper.GetMaxEscapedLength(propertyName.Length, firstEscapeIndexProp);
			Span<char> span2;
			if (maxEscapedLength <= 128)
			{
				Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)256], 128);
				span2 = span;
			}
			else
			{
				span2 = (array = ArrayPool<char>.Shared.Rent(maxEscapedLength));
			}
			Span<char> span3 = span2;
			int num;
			JsonWriterHelper.EscapeString(propertyName, span3, firstEscapeIndexProp, this._options.Encoder, out num);
			this.WriteStringByOptions(span3.Slice(0, num), value);
			if (array != null)
			{
				ArrayPool<char>.Shared.Return(array, false);
			}
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00017DC8 File Offset: 0x00015FC8
		private unsafe void WriteStringEscapeProperty(ReadOnlySpan<byte> utf8PropertyName, DateTime value, int firstEscapeIndexProp)
		{
			byte[] array = null;
			int maxEscapedLength = JsonWriterHelper.GetMaxEscapedLength(utf8PropertyName.Length, firstEscapeIndexProp);
			Span<byte> span2;
			if (maxEscapedLength <= 256)
			{
				Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)256], 256);
				span2 = span;
			}
			else
			{
				span2 = (array = ArrayPool<byte>.Shared.Rent(maxEscapedLength));
			}
			Span<byte> span3 = span2;
			int num;
			JsonWriterHelper.EscapeString(utf8PropertyName, span3, firstEscapeIndexProp, this._options.Encoder, out num);
			this.WriteStringByOptions(span3.Slice(0, num), value);
			if (array != null)
			{
				ArrayPool<byte>.Shared.Return(array, false);
			}
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00017E57 File Offset: 0x00016057
		private void WriteStringByOptions(ReadOnlySpan<char> propertyName, DateTime value)
		{
			this.ValidateWritingProperty();
			if (this._options.Indented)
			{
				this.WriteStringIndented(propertyName, value);
				return;
			}
			this.WriteStringMinimized(propertyName, value);
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00017E7D File Offset: 0x0001607D
		private void WriteStringByOptions(ReadOnlySpan<byte> utf8PropertyName, DateTime value)
		{
			this.ValidateWritingProperty();
			if (this._options.Indented)
			{
				this.WriteStringIndented(utf8PropertyName, value);
				return;
			}
			this.WriteStringMinimized(utf8PropertyName, value);
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00017EA4 File Offset: 0x000160A4
		private unsafe void WriteStringMinimized(ReadOnlySpan<char> escapedPropertyName, DateTime value)
		{
			int num = escapedPropertyName.Length * 3 + 33 + 6;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			int num2;
			if (this._currentDepth < 0)
			{
				num2 = this.BytesPending;
				this.BytesPending = num2 + 1;
				*span[num2] = 44;
			}
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			this.TranscodeAndWrite(escapedPropertyName, span);
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 58;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			int num3;
			JsonWriterHelper.WriteDateTimeTrimmed(span.Slice(this.BytesPending), value, out num3);
			this.BytesPending += num3;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x00017FC0 File Offset: 0x000161C0
		private unsafe void WriteStringMinimized(ReadOnlySpan<byte> escapedPropertyName, DateTime value)
		{
			int num = escapedPropertyName.Length + 33 + 5;
			int num2 = num + 1;
			if (this._memory.Length - this.BytesPending < num2)
			{
				this.Grow(num2);
			}
			Span<byte> span = this._memory.Span;
			int num3;
			if (this._currentDepth < 0)
			{
				num3 = this.BytesPending;
				this.BytesPending = num3 + 1;
				*span[num3] = 44;
			}
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			escapedPropertyName.CopyTo(span.Slice(this.BytesPending));
			this.BytesPending += escapedPropertyName.Length;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 58;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			int num4;
			JsonWriterHelper.WriteDateTimeTrimmed(span.Slice(this.BytesPending), value, out num4);
			this.BytesPending += num4;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00018110 File Offset: 0x00016310
		private unsafe void WriteStringIndented(ReadOnlySpan<char> escapedPropertyName, DateTime value)
		{
			int indentation = this.Indentation;
			int num = indentation + escapedPropertyName.Length * 3 + 33 + 7 + Utf8JsonWriter.s_newLineLength;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			int num2;
			if (this._currentDepth < 0)
			{
				num2 = this.BytesPending;
				this.BytesPending = num2 + 1;
				*span[num2] = 44;
			}
			if (this._tokenType != JsonTokenType.None)
			{
				this.WriteNewLine(span);
			}
			JsonWriterHelper.WriteIndentation(span.Slice(this.BytesPending), indentation);
			this.BytesPending += indentation;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			this.TranscodeAndWrite(escapedPropertyName, span);
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 58;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 32;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			int num3;
			JsonWriterHelper.WriteDateTimeTrimmed(span.Slice(this.BytesPending), value, out num3);
			this.BytesPending += num3;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0001829C File Offset: 0x0001649C
		private unsafe void WriteStringIndented(ReadOnlySpan<byte> escapedPropertyName, DateTime value)
		{
			int indentation = this.Indentation;
			int num = indentation + escapedPropertyName.Length + 33 + 6;
			int num2 = num + 1 + Utf8JsonWriter.s_newLineLength;
			if (this._memory.Length - this.BytesPending < num2)
			{
				this.Grow(num2);
			}
			Span<byte> span = this._memory.Span;
			int num3;
			if (this._currentDepth < 0)
			{
				num3 = this.BytesPending;
				this.BytesPending = num3 + 1;
				*span[num3] = 44;
			}
			if (this._tokenType != JsonTokenType.None)
			{
				this.WriteNewLine(span);
			}
			JsonWriterHelper.WriteIndentation(span.Slice(this.BytesPending), indentation);
			this.BytesPending += indentation;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			escapedPropertyName.CopyTo(span.Slice(this.BytesPending));
			this.BytesPending += escapedPropertyName.Length;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 58;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 32;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			int num4;
			JsonWriterHelper.WriteDateTimeTrimmed(span.Slice(this.BytesPending), value, out num4);
			this.BytesPending += num4;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00018448 File Offset: 0x00016648
		internal unsafe void WritePropertyName(DateTime value)
		{
			Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)33], 33);
			Span<byte> span2 = span;
			int num;
			JsonWriterHelper.WriteDateTimeTrimmed(span2, value, out num);
			this.WritePropertyNameUnescaped(span2.Slice(0, num));
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00018484 File Offset: 0x00016684
		public void WriteString(JsonEncodedText propertyName, DateTimeOffset value)
		{
			ReadOnlySpan<byte> encodedUtf8Bytes = propertyName.EncodedUtf8Bytes;
			this.WriteStringByOptions(encodedUtf8Bytes, value);
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.String;
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x000184AE File Offset: 0x000166AE
		[NullableContext(1)]
		public void WriteString(string propertyName, DateTimeOffset value)
		{
			if (propertyName == null)
			{
				ThrowHelper.ThrowArgumentNullException("propertyName");
			}
			this.WriteString(propertyName.AsSpan(), value);
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x000184CA File Offset: 0x000166CA
		public void WriteString(ReadOnlySpan<char> propertyName, DateTimeOffset value)
		{
			JsonWriterHelper.ValidateProperty(propertyName);
			this.WriteStringEscape(propertyName, value);
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.String;
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x000184E7 File Offset: 0x000166E7
		public void WriteString(ReadOnlySpan<byte> utf8PropertyName, DateTimeOffset value)
		{
			JsonWriterHelper.ValidateProperty(utf8PropertyName);
			this.WriteStringEscape(utf8PropertyName, value);
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.String;
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x00018504 File Offset: 0x00016704
		private void WriteStringEscape(ReadOnlySpan<char> propertyName, DateTimeOffset value)
		{
			int num = JsonWriterHelper.NeedsEscaping(propertyName, this._options.Encoder);
			if (num != -1)
			{
				this.WriteStringEscapeProperty(propertyName, value, num);
				return;
			}
			this.WriteStringByOptions(propertyName, value);
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0001853C File Offset: 0x0001673C
		private void WriteStringEscape(ReadOnlySpan<byte> utf8PropertyName, DateTimeOffset value)
		{
			int num = JsonWriterHelper.NeedsEscaping(utf8PropertyName, this._options.Encoder);
			if (num != -1)
			{
				this.WriteStringEscapeProperty(utf8PropertyName, value, num);
				return;
			}
			this.WriteStringByOptions(utf8PropertyName, value);
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00018574 File Offset: 0x00016774
		private unsafe void WriteStringEscapeProperty(ReadOnlySpan<char> propertyName, DateTimeOffset value, int firstEscapeIndexProp)
		{
			char[] array = null;
			int maxEscapedLength = JsonWriterHelper.GetMaxEscapedLength(propertyName.Length, firstEscapeIndexProp);
			Span<char> span2;
			if (maxEscapedLength <= 128)
			{
				Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)256], 128);
				span2 = span;
			}
			else
			{
				span2 = (array = ArrayPool<char>.Shared.Rent(maxEscapedLength));
			}
			Span<char> span3 = span2;
			int num;
			JsonWriterHelper.EscapeString(propertyName, span3, firstEscapeIndexProp, this._options.Encoder, out num);
			this.WriteStringByOptions(span3.Slice(0, num), value);
			if (array != null)
			{
				ArrayPool<char>.Shared.Return(array, false);
			}
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00018604 File Offset: 0x00016804
		private unsafe void WriteStringEscapeProperty(ReadOnlySpan<byte> utf8PropertyName, DateTimeOffset value, int firstEscapeIndexProp)
		{
			byte[] array = null;
			int maxEscapedLength = JsonWriterHelper.GetMaxEscapedLength(utf8PropertyName.Length, firstEscapeIndexProp);
			Span<byte> span2;
			if (maxEscapedLength <= 256)
			{
				Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)256], 256);
				span2 = span;
			}
			else
			{
				span2 = (array = ArrayPool<byte>.Shared.Rent(maxEscapedLength));
			}
			Span<byte> span3 = span2;
			int num;
			JsonWriterHelper.EscapeString(utf8PropertyName, span3, firstEscapeIndexProp, this._options.Encoder, out num);
			this.WriteStringByOptions(span3.Slice(0, num), value);
			if (array != null)
			{
				ArrayPool<byte>.Shared.Return(array, false);
			}
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00018693 File Offset: 0x00016893
		private void WriteStringByOptions(ReadOnlySpan<char> propertyName, DateTimeOffset value)
		{
			this.ValidateWritingProperty();
			if (this._options.Indented)
			{
				this.WriteStringIndented(propertyName, value);
				return;
			}
			this.WriteStringMinimized(propertyName, value);
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x000186B9 File Offset: 0x000168B9
		private void WriteStringByOptions(ReadOnlySpan<byte> utf8PropertyName, DateTimeOffset value)
		{
			this.ValidateWritingProperty();
			if (this._options.Indented)
			{
				this.WriteStringIndented(utf8PropertyName, value);
				return;
			}
			this.WriteStringMinimized(utf8PropertyName, value);
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x000186E0 File Offset: 0x000168E0
		private unsafe void WriteStringMinimized(ReadOnlySpan<char> escapedPropertyName, DateTimeOffset value)
		{
			int num = escapedPropertyName.Length * 3 + 33 + 6;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			int num2;
			if (this._currentDepth < 0)
			{
				num2 = this.BytesPending;
				this.BytesPending = num2 + 1;
				*span[num2] = 44;
			}
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			this.TranscodeAndWrite(escapedPropertyName, span);
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 58;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			int num3;
			JsonWriterHelper.WriteDateTimeOffsetTrimmed(span.Slice(this.BytesPending), value, out num3);
			this.BytesPending += num3;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x000187FC File Offset: 0x000169FC
		private unsafe void WriteStringMinimized(ReadOnlySpan<byte> escapedPropertyName, DateTimeOffset value)
		{
			int num = escapedPropertyName.Length + 33 + 5;
			int num2 = num + 1;
			if (this._memory.Length - this.BytesPending < num2)
			{
				this.Grow(num2);
			}
			Span<byte> span = this._memory.Span;
			int num3;
			if (this._currentDepth < 0)
			{
				num3 = this.BytesPending;
				this.BytesPending = num3 + 1;
				*span[num3] = 44;
			}
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			escapedPropertyName.CopyTo(span.Slice(this.BytesPending));
			this.BytesPending += escapedPropertyName.Length;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 58;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			int num4;
			JsonWriterHelper.WriteDateTimeOffsetTrimmed(span.Slice(this.BytesPending), value, out num4);
			this.BytesPending += num4;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0001894C File Offset: 0x00016B4C
		private unsafe void WriteStringIndented(ReadOnlySpan<char> escapedPropertyName, DateTimeOffset value)
		{
			int indentation = this.Indentation;
			int num = indentation + escapedPropertyName.Length * 3 + 33 + 7 + Utf8JsonWriter.s_newLineLength;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			int num2;
			if (this._currentDepth < 0)
			{
				num2 = this.BytesPending;
				this.BytesPending = num2 + 1;
				*span[num2] = 44;
			}
			if (this._tokenType != JsonTokenType.None)
			{
				this.WriteNewLine(span);
			}
			JsonWriterHelper.WriteIndentation(span.Slice(this.BytesPending), indentation);
			this.BytesPending += indentation;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			this.TranscodeAndWrite(escapedPropertyName, span);
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 58;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 32;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			int num3;
			JsonWriterHelper.WriteDateTimeOffsetTrimmed(span.Slice(this.BytesPending), value, out num3);
			this.BytesPending += num3;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00018AD8 File Offset: 0x00016CD8
		private unsafe void WriteStringIndented(ReadOnlySpan<byte> escapedPropertyName, DateTimeOffset value)
		{
			int indentation = this.Indentation;
			int num = indentation + escapedPropertyName.Length + 33 + 6;
			int num2 = num + 1 + Utf8JsonWriter.s_newLineLength;
			if (this._memory.Length - this.BytesPending < num2)
			{
				this.Grow(num2);
			}
			Span<byte> span = this._memory.Span;
			int num3;
			if (this._currentDepth < 0)
			{
				num3 = this.BytesPending;
				this.BytesPending = num3 + 1;
				*span[num3] = 44;
			}
			if (this._tokenType != JsonTokenType.None)
			{
				this.WriteNewLine(span);
			}
			JsonWriterHelper.WriteIndentation(span.Slice(this.BytesPending), indentation);
			this.BytesPending += indentation;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			escapedPropertyName.CopyTo(span.Slice(this.BytesPending));
			this.BytesPending += escapedPropertyName.Length;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 58;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 32;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			int num4;
			JsonWriterHelper.WriteDateTimeOffsetTrimmed(span.Slice(this.BytesPending), value, out num4);
			this.BytesPending += num4;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00018C84 File Offset: 0x00016E84
		internal unsafe void WritePropertyName(DateTimeOffset value)
		{
			Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)33], 33);
			Span<byte> span2 = span;
			int num;
			JsonWriterHelper.WriteDateTimeOffsetTrimmed(span2, value, out num);
			this.WritePropertyNameUnescaped(span2.Slice(0, num));
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00018CC0 File Offset: 0x00016EC0
		public void WriteNumber(JsonEncodedText propertyName, decimal value)
		{
			ReadOnlySpan<byte> encodedUtf8Bytes = propertyName.EncodedUtf8Bytes;
			this.WriteNumberByOptions(encodedUtf8Bytes, value);
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.Number;
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00018CEA File Offset: 0x00016EEA
		[NullableContext(1)]
		public void WriteNumber(string propertyName, decimal value)
		{
			if (propertyName == null)
			{
				ThrowHelper.ThrowArgumentNullException("propertyName");
			}
			this.WriteNumber(propertyName.AsSpan(), value);
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00018D06 File Offset: 0x00016F06
		public void WriteNumber(ReadOnlySpan<char> propertyName, decimal value)
		{
			JsonWriterHelper.ValidateProperty(propertyName);
			this.WriteNumberEscape(propertyName, value);
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.Number;
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00018D23 File Offset: 0x00016F23
		public void WriteNumber(ReadOnlySpan<byte> utf8PropertyName, decimal value)
		{
			JsonWriterHelper.ValidateProperty(utf8PropertyName);
			this.WriteNumberEscape(utf8PropertyName, value);
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.Number;
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00018D40 File Offset: 0x00016F40
		private void WriteNumberEscape(ReadOnlySpan<char> propertyName, decimal value)
		{
			int num = JsonWriterHelper.NeedsEscaping(propertyName, this._options.Encoder);
			if (num != -1)
			{
				this.WriteNumberEscapeProperty(propertyName, value, num);
				return;
			}
			this.WriteNumberByOptions(propertyName, value);
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00018D78 File Offset: 0x00016F78
		private void WriteNumberEscape(ReadOnlySpan<byte> utf8PropertyName, decimal value)
		{
			int num = JsonWriterHelper.NeedsEscaping(utf8PropertyName, this._options.Encoder);
			if (num != -1)
			{
				this.WriteNumberEscapeProperty(utf8PropertyName, value, num);
				return;
			}
			this.WriteNumberByOptions(utf8PropertyName, value);
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00018DB0 File Offset: 0x00016FB0
		private unsafe void WriteNumberEscapeProperty(ReadOnlySpan<char> propertyName, decimal value, int firstEscapeIndexProp)
		{
			char[] array = null;
			int maxEscapedLength = JsonWriterHelper.GetMaxEscapedLength(propertyName.Length, firstEscapeIndexProp);
			Span<char> span2;
			if (maxEscapedLength <= 128)
			{
				Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)256], 128);
				span2 = span;
			}
			else
			{
				span2 = (array = ArrayPool<char>.Shared.Rent(maxEscapedLength));
			}
			Span<char> span3 = span2;
			int num;
			JsonWriterHelper.EscapeString(propertyName, span3, firstEscapeIndexProp, this._options.Encoder, out num);
			this.WriteNumberByOptions(span3.Slice(0, num), value);
			if (array != null)
			{
				ArrayPool<char>.Shared.Return(array, false);
			}
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x00018E40 File Offset: 0x00017040
		private unsafe void WriteNumberEscapeProperty(ReadOnlySpan<byte> utf8PropertyName, decimal value, int firstEscapeIndexProp)
		{
			byte[] array = null;
			int maxEscapedLength = JsonWriterHelper.GetMaxEscapedLength(utf8PropertyName.Length, firstEscapeIndexProp);
			Span<byte> span2;
			if (maxEscapedLength <= 256)
			{
				Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)256], 256);
				span2 = span;
			}
			else
			{
				span2 = (array = ArrayPool<byte>.Shared.Rent(maxEscapedLength));
			}
			Span<byte> span3 = span2;
			int num;
			JsonWriterHelper.EscapeString(utf8PropertyName, span3, firstEscapeIndexProp, this._options.Encoder, out num);
			this.WriteNumberByOptions(span3.Slice(0, num), value);
			if (array != null)
			{
				ArrayPool<byte>.Shared.Return(array, false);
			}
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00018ECF File Offset: 0x000170CF
		private void WriteNumberByOptions(ReadOnlySpan<char> propertyName, decimal value)
		{
			this.ValidateWritingProperty();
			if (this._options.Indented)
			{
				this.WriteNumberIndented(propertyName, value);
				return;
			}
			this.WriteNumberMinimized(propertyName, value);
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00018EF5 File Offset: 0x000170F5
		private void WriteNumberByOptions(ReadOnlySpan<byte> utf8PropertyName, decimal value)
		{
			this.ValidateWritingProperty();
			if (this._options.Indented)
			{
				this.WriteNumberIndented(utf8PropertyName, value);
				return;
			}
			this.WriteNumberMinimized(utf8PropertyName, value);
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00018F1C File Offset: 0x0001711C
		private unsafe void WriteNumberMinimized(ReadOnlySpan<char> escapedPropertyName, decimal value)
		{
			int num = escapedPropertyName.Length * 3 + 31 + 4;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			int num2;
			if (this._currentDepth < 0)
			{
				num2 = this.BytesPending;
				this.BytesPending = num2 + 1;
				*span[num2] = 44;
			}
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			this.TranscodeAndWrite(escapedPropertyName, span);
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 58;
			int num3;
			bool flag = Utf8Formatter.TryFormat(value, span.Slice(this.BytesPending), out num3, default(StandardFormat));
			this.BytesPending += num3;
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00019018 File Offset: 0x00017218
		private unsafe void WriteNumberMinimized(ReadOnlySpan<byte> escapedPropertyName, decimal value)
		{
			int num = escapedPropertyName.Length + 31 + 3;
			int num2 = num + 1;
			if (this._memory.Length - this.BytesPending < num2)
			{
				this.Grow(num2);
			}
			Span<byte> span = this._memory.Span;
			int num3;
			if (this._currentDepth < 0)
			{
				num3 = this.BytesPending;
				this.BytesPending = num3 + 1;
				*span[num3] = 44;
			}
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			escapedPropertyName.CopyTo(span.Slice(this.BytesPending));
			this.BytesPending += escapedPropertyName.Length;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 58;
			int num4;
			bool flag = Utf8Formatter.TryFormat(value, span.Slice(this.BytesPending), out num4, default(StandardFormat));
			this.BytesPending += num4;
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00019138 File Offset: 0x00017338
		private unsafe void WriteNumberIndented(ReadOnlySpan<char> escapedPropertyName, decimal value)
		{
			int indentation = this.Indentation;
			int num = indentation + escapedPropertyName.Length * 3 + 31 + 5 + Utf8JsonWriter.s_newLineLength;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			int num2;
			if (this._currentDepth < 0)
			{
				num2 = this.BytesPending;
				this.BytesPending = num2 + 1;
				*span[num2] = 44;
			}
			if (this._tokenType != JsonTokenType.None)
			{
				this.WriteNewLine(span);
			}
			JsonWriterHelper.WriteIndentation(span.Slice(this.BytesPending), indentation);
			this.BytesPending += indentation;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			this.TranscodeAndWrite(escapedPropertyName, span);
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 58;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 32;
			int num3;
			bool flag = Utf8Formatter.TryFormat(value, span.Slice(this.BytesPending), out num3, default(StandardFormat));
			this.BytesPending += num3;
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00019294 File Offset: 0x00017494
		private unsafe void WriteNumberIndented(ReadOnlySpan<byte> escapedPropertyName, decimal value)
		{
			int indentation = this.Indentation;
			int num = indentation + escapedPropertyName.Length + 31 + 4;
			int num2 = num + 1 + Utf8JsonWriter.s_newLineLength;
			if (this._memory.Length - this.BytesPending < num2)
			{
				this.Grow(num2);
			}
			Span<byte> span = this._memory.Span;
			int num3;
			if (this._currentDepth < 0)
			{
				num3 = this.BytesPending;
				this.BytesPending = num3 + 1;
				*span[num3] = 44;
			}
			if (this._tokenType != JsonTokenType.None)
			{
				this.WriteNewLine(span);
			}
			JsonWriterHelper.WriteIndentation(span.Slice(this.BytesPending), indentation);
			this.BytesPending += indentation;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			escapedPropertyName.CopyTo(span.Slice(this.BytesPending));
			this.BytesPending += escapedPropertyName.Length;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 58;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 32;
			int num4;
			bool flag = Utf8Formatter.TryFormat(value, span.Slice(this.BytesPending), out num4, default(StandardFormat));
			this.BytesPending += num4;
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00019410 File Offset: 0x00017610
		internal unsafe void WritePropertyName(decimal value)
		{
			Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)31], 31);
			Span<byte> span2 = span;
			int num;
			bool flag = Utf8Formatter.TryFormat(value, span2, out num, default(StandardFormat));
			this.WritePropertyNameUnescaped(span2.Slice(0, num));
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00019454 File Offset: 0x00017654
		public void WriteNumber(JsonEncodedText propertyName, double value)
		{
			ReadOnlySpan<byte> encodedUtf8Bytes = propertyName.EncodedUtf8Bytes;
			JsonWriterHelper.ValidateDouble(value);
			this.WriteNumberByOptions(encodedUtf8Bytes, value);
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.Number;
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x00019484 File Offset: 0x00017684
		[NullableContext(1)]
		public void WriteNumber(string propertyName, double value)
		{
			if (propertyName == null)
			{
				ThrowHelper.ThrowArgumentNullException("propertyName");
			}
			this.WriteNumber(propertyName.AsSpan(), value);
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x000194A0 File Offset: 0x000176A0
		public void WriteNumber(ReadOnlySpan<char> propertyName, double value)
		{
			JsonWriterHelper.ValidateProperty(propertyName);
			JsonWriterHelper.ValidateDouble(value);
			this.WriteNumberEscape(propertyName, value);
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.Number;
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x000194C3 File Offset: 0x000176C3
		public void WriteNumber(ReadOnlySpan<byte> utf8PropertyName, double value)
		{
			JsonWriterHelper.ValidateProperty(utf8PropertyName);
			JsonWriterHelper.ValidateDouble(value);
			this.WriteNumberEscape(utf8PropertyName, value);
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.Number;
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x000194E8 File Offset: 0x000176E8
		private void WriteNumberEscape(ReadOnlySpan<char> propertyName, double value)
		{
			int num = JsonWriterHelper.NeedsEscaping(propertyName, this._options.Encoder);
			if (num != -1)
			{
				this.WriteNumberEscapeProperty(propertyName, value, num);
				return;
			}
			this.WriteNumberByOptions(propertyName, value);
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00019520 File Offset: 0x00017720
		private void WriteNumberEscape(ReadOnlySpan<byte> utf8PropertyName, double value)
		{
			int num = JsonWriterHelper.NeedsEscaping(utf8PropertyName, this._options.Encoder);
			if (num != -1)
			{
				this.WriteNumberEscapeProperty(utf8PropertyName, value, num);
				return;
			}
			this.WriteNumberByOptions(utf8PropertyName, value);
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x00019558 File Offset: 0x00017758
		private unsafe void WriteNumberEscapeProperty(ReadOnlySpan<char> propertyName, double value, int firstEscapeIndexProp)
		{
			char[] array = null;
			int maxEscapedLength = JsonWriterHelper.GetMaxEscapedLength(propertyName.Length, firstEscapeIndexProp);
			Span<char> span2;
			if (maxEscapedLength <= 128)
			{
				Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)256], 128);
				span2 = span;
			}
			else
			{
				span2 = (array = ArrayPool<char>.Shared.Rent(maxEscapedLength));
			}
			Span<char> span3 = span2;
			int num;
			JsonWriterHelper.EscapeString(propertyName, span3, firstEscapeIndexProp, this._options.Encoder, out num);
			this.WriteNumberByOptions(span3.Slice(0, num), value);
			if (array != null)
			{
				ArrayPool<char>.Shared.Return(array, false);
			}
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x000195E8 File Offset: 0x000177E8
		private unsafe void WriteNumberEscapeProperty(ReadOnlySpan<byte> utf8PropertyName, double value, int firstEscapeIndexProp)
		{
			byte[] array = null;
			int maxEscapedLength = JsonWriterHelper.GetMaxEscapedLength(utf8PropertyName.Length, firstEscapeIndexProp);
			Span<byte> span2;
			if (maxEscapedLength <= 256)
			{
				Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)256], 256);
				span2 = span;
			}
			else
			{
				span2 = (array = ArrayPool<byte>.Shared.Rent(maxEscapedLength));
			}
			Span<byte> span3 = span2;
			int num;
			JsonWriterHelper.EscapeString(utf8PropertyName, span3, firstEscapeIndexProp, this._options.Encoder, out num);
			this.WriteNumberByOptions(span3.Slice(0, num), value);
			if (array != null)
			{
				ArrayPool<byte>.Shared.Return(array, false);
			}
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00019677 File Offset: 0x00017877
		private void WriteNumberByOptions(ReadOnlySpan<char> propertyName, double value)
		{
			this.ValidateWritingProperty();
			if (this._options.Indented)
			{
				this.WriteNumberIndented(propertyName, value);
				return;
			}
			this.WriteNumberMinimized(propertyName, value);
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0001969D File Offset: 0x0001789D
		private void WriteNumberByOptions(ReadOnlySpan<byte> utf8PropertyName, double value)
		{
			this.ValidateWritingProperty();
			if (this._options.Indented)
			{
				this.WriteNumberIndented(utf8PropertyName, value);
				return;
			}
			this.WriteNumberMinimized(utf8PropertyName, value);
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x000196C4 File Offset: 0x000178C4
		private unsafe void WriteNumberMinimized(ReadOnlySpan<char> escapedPropertyName, double value)
		{
			int num = escapedPropertyName.Length * 3 + 128 + 4;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			int num2;
			if (this._currentDepth < 0)
			{
				num2 = this.BytesPending;
				this.BytesPending = num2 + 1;
				*span[num2] = 44;
			}
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			this.TranscodeAndWrite(escapedPropertyName, span);
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 58;
			int num3;
			bool flag = Utf8JsonWriter.TryFormatDouble(value, span.Slice(this.BytesPending), out num3);
			this.BytesPending += num3;
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x000197B8 File Offset: 0x000179B8
		private unsafe void WriteNumberMinimized(ReadOnlySpan<byte> escapedPropertyName, double value)
		{
			int num = escapedPropertyName.Length + 128 + 3;
			int num2 = num + 1;
			if (this._memory.Length - this.BytesPending < num2)
			{
				this.Grow(num2);
			}
			Span<byte> span = this._memory.Span;
			int num3;
			if (this._currentDepth < 0)
			{
				num3 = this.BytesPending;
				this.BytesPending = num3 + 1;
				*span[num3] = 44;
			}
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			escapedPropertyName.CopyTo(span.Slice(this.BytesPending));
			this.BytesPending += escapedPropertyName.Length;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 58;
			int num4;
			bool flag = Utf8JsonWriter.TryFormatDouble(value, span.Slice(this.BytesPending), out num4);
			this.BytesPending += num4;
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x000198D0 File Offset: 0x00017AD0
		private unsafe void WriteNumberIndented(ReadOnlySpan<char> escapedPropertyName, double value)
		{
			int indentation = this.Indentation;
			int num = indentation + escapedPropertyName.Length * 3 + 128 + 5 + Utf8JsonWriter.s_newLineLength;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			int num2;
			if (this._currentDepth < 0)
			{
				num2 = this.BytesPending;
				this.BytesPending = num2 + 1;
				*span[num2] = 44;
			}
			if (this._tokenType != JsonTokenType.None)
			{
				this.WriteNewLine(span);
			}
			JsonWriterHelper.WriteIndentation(span.Slice(this.BytesPending), indentation);
			this.BytesPending += indentation;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			this.TranscodeAndWrite(escapedPropertyName, span);
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 58;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 32;
			int num3;
			bool flag = Utf8JsonWriter.TryFormatDouble(value, span.Slice(this.BytesPending), out num3);
			this.BytesPending += num3;
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00019A24 File Offset: 0x00017C24
		private unsafe void WriteNumberIndented(ReadOnlySpan<byte> escapedPropertyName, double value)
		{
			int indentation = this.Indentation;
			int num = indentation + escapedPropertyName.Length + 128 + 4;
			int num2 = num + 1 + Utf8JsonWriter.s_newLineLength;
			if (this._memory.Length - this.BytesPending < num2)
			{
				this.Grow(num2);
			}
			Span<byte> span = this._memory.Span;
			int num3;
			if (this._currentDepth < 0)
			{
				num3 = this.BytesPending;
				this.BytesPending = num3 + 1;
				*span[num3] = 44;
			}
			if (this._tokenType != JsonTokenType.None)
			{
				this.WriteNewLine(span);
			}
			JsonWriterHelper.WriteIndentation(span.Slice(this.BytesPending), indentation);
			this.BytesPending += indentation;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			escapedPropertyName.CopyTo(span.Slice(this.BytesPending));
			this.BytesPending += escapedPropertyName.Length;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 58;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 32;
			int num4;
			bool flag = Utf8JsonWriter.TryFormatDouble(value, span.Slice(this.BytesPending), out num4);
			this.BytesPending += num4;
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00019B9C File Offset: 0x00017D9C
		internal unsafe void WritePropertyName(double value)
		{
			JsonWriterHelper.ValidateDouble(value);
			Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)128], 128);
			Span<byte> span2 = span;
			int num;
			bool flag = Utf8JsonWriter.TryFormatDouble(value, span2, out num);
			this.WritePropertyNameUnescaped(span2.Slice(0, num));
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00019BE4 File Offset: 0x00017DE4
		public void WriteNumber(JsonEncodedText propertyName, float value)
		{
			ReadOnlySpan<byte> encodedUtf8Bytes = propertyName.EncodedUtf8Bytes;
			JsonWriterHelper.ValidateSingle(value);
			this.WriteNumberByOptions(encodedUtf8Bytes, value);
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.Number;
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x00019C14 File Offset: 0x00017E14
		[NullableContext(1)]
		public void WriteNumber(string propertyName, float value)
		{
			if (propertyName == null)
			{
				ThrowHelper.ThrowArgumentNullException("propertyName");
			}
			this.WriteNumber(propertyName.AsSpan(), value);
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00019C30 File Offset: 0x00017E30
		public void WriteNumber(ReadOnlySpan<char> propertyName, float value)
		{
			JsonWriterHelper.ValidateProperty(propertyName);
			JsonWriterHelper.ValidateSingle(value);
			this.WriteNumberEscape(propertyName, value);
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.Number;
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00019C53 File Offset: 0x00017E53
		public void WriteNumber(ReadOnlySpan<byte> utf8PropertyName, float value)
		{
			JsonWriterHelper.ValidateProperty(utf8PropertyName);
			JsonWriterHelper.ValidateSingle(value);
			this.WriteNumberEscape(utf8PropertyName, value);
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.Number;
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00019C78 File Offset: 0x00017E78
		private void WriteNumberEscape(ReadOnlySpan<char> propertyName, float value)
		{
			int num = JsonWriterHelper.NeedsEscaping(propertyName, this._options.Encoder);
			if (num != -1)
			{
				this.WriteNumberEscapeProperty(propertyName, value, num);
				return;
			}
			this.WriteNumberByOptions(propertyName, value);
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00019CB0 File Offset: 0x00017EB0
		private void WriteNumberEscape(ReadOnlySpan<byte> utf8PropertyName, float value)
		{
			int num = JsonWriterHelper.NeedsEscaping(utf8PropertyName, this._options.Encoder);
			if (num != -1)
			{
				this.WriteNumberEscapeProperty(utf8PropertyName, value, num);
				return;
			}
			this.WriteNumberByOptions(utf8PropertyName, value);
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00019CE8 File Offset: 0x00017EE8
		private unsafe void WriteNumberEscapeProperty(ReadOnlySpan<char> propertyName, float value, int firstEscapeIndexProp)
		{
			char[] array = null;
			int maxEscapedLength = JsonWriterHelper.GetMaxEscapedLength(propertyName.Length, firstEscapeIndexProp);
			Span<char> span2;
			if (maxEscapedLength <= 128)
			{
				Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)256], 128);
				span2 = span;
			}
			else
			{
				span2 = (array = ArrayPool<char>.Shared.Rent(maxEscapedLength));
			}
			Span<char> span3 = span2;
			int num;
			JsonWriterHelper.EscapeString(propertyName, span3, firstEscapeIndexProp, this._options.Encoder, out num);
			this.WriteNumberByOptions(span3.Slice(0, num), value);
			if (array != null)
			{
				ArrayPool<char>.Shared.Return(array, false);
			}
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00019D78 File Offset: 0x00017F78
		private unsafe void WriteNumberEscapeProperty(ReadOnlySpan<byte> utf8PropertyName, float value, int firstEscapeIndexProp)
		{
			byte[] array = null;
			int maxEscapedLength = JsonWriterHelper.GetMaxEscapedLength(utf8PropertyName.Length, firstEscapeIndexProp);
			Span<byte> span2;
			if (maxEscapedLength <= 256)
			{
				Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)256], 256);
				span2 = span;
			}
			else
			{
				span2 = (array = ArrayPool<byte>.Shared.Rent(maxEscapedLength));
			}
			Span<byte> span3 = span2;
			int num;
			JsonWriterHelper.EscapeString(utf8PropertyName, span3, firstEscapeIndexProp, this._options.Encoder, out num);
			this.WriteNumberByOptions(span3.Slice(0, num), value);
			if (array != null)
			{
				ArrayPool<byte>.Shared.Return(array, false);
			}
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00019E07 File Offset: 0x00018007
		private void WriteNumberByOptions(ReadOnlySpan<char> propertyName, float value)
		{
			this.ValidateWritingProperty();
			if (this._options.Indented)
			{
				this.WriteNumberIndented(propertyName, value);
				return;
			}
			this.WriteNumberMinimized(propertyName, value);
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x00019E2D File Offset: 0x0001802D
		private void WriteNumberByOptions(ReadOnlySpan<byte> utf8PropertyName, float value)
		{
			this.ValidateWritingProperty();
			if (this._options.Indented)
			{
				this.WriteNumberIndented(utf8PropertyName, value);
				return;
			}
			this.WriteNumberMinimized(utf8PropertyName, value);
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00019E54 File Offset: 0x00018054
		private unsafe void WriteNumberMinimized(ReadOnlySpan<char> escapedPropertyName, float value)
		{
			int num = escapedPropertyName.Length * 3 + 128 + 4;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			int num2;
			if (this._currentDepth < 0)
			{
				num2 = this.BytesPending;
				this.BytesPending = num2 + 1;
				*span[num2] = 44;
			}
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			this.TranscodeAndWrite(escapedPropertyName, span);
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 58;
			int num3;
			bool flag = Utf8JsonWriter.TryFormatSingle(value, span.Slice(this.BytesPending), out num3);
			this.BytesPending += num3;
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00019F48 File Offset: 0x00018148
		private unsafe void WriteNumberMinimized(ReadOnlySpan<byte> escapedPropertyName, float value)
		{
			int num = escapedPropertyName.Length + 128 + 3;
			int num2 = num + 1;
			if (this._memory.Length - this.BytesPending < num2)
			{
				this.Grow(num2);
			}
			Span<byte> span = this._memory.Span;
			int num3;
			if (this._currentDepth < 0)
			{
				num3 = this.BytesPending;
				this.BytesPending = num3 + 1;
				*span[num3] = 44;
			}
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			escapedPropertyName.CopyTo(span.Slice(this.BytesPending));
			this.BytesPending += escapedPropertyName.Length;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 58;
			int num4;
			bool flag = Utf8JsonWriter.TryFormatSingle(value, span.Slice(this.BytesPending), out num4);
			this.BytesPending += num4;
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0001A060 File Offset: 0x00018260
		private unsafe void WriteNumberIndented(ReadOnlySpan<char> escapedPropertyName, float value)
		{
			int indentation = this.Indentation;
			int num = indentation + escapedPropertyName.Length * 3 + 128 + 5 + Utf8JsonWriter.s_newLineLength;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			int num2;
			if (this._currentDepth < 0)
			{
				num2 = this.BytesPending;
				this.BytesPending = num2 + 1;
				*span[num2] = 44;
			}
			if (this._tokenType != JsonTokenType.None)
			{
				this.WriteNewLine(span);
			}
			JsonWriterHelper.WriteIndentation(span.Slice(this.BytesPending), indentation);
			this.BytesPending += indentation;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			this.TranscodeAndWrite(escapedPropertyName, span);
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 58;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 32;
			int num3;
			bool flag = Utf8JsonWriter.TryFormatSingle(value, span.Slice(this.BytesPending), out num3);
			this.BytesPending += num3;
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0001A1B4 File Offset: 0x000183B4
		private unsafe void WriteNumberIndented(ReadOnlySpan<byte> escapedPropertyName, float value)
		{
			int indentation = this.Indentation;
			int num = indentation + escapedPropertyName.Length + 128 + 4;
			int num2 = num + 1 + Utf8JsonWriter.s_newLineLength;
			if (this._memory.Length - this.BytesPending < num2)
			{
				this.Grow(num2);
			}
			Span<byte> span = this._memory.Span;
			int num3;
			if (this._currentDepth < 0)
			{
				num3 = this.BytesPending;
				this.BytesPending = num3 + 1;
				*span[num3] = 44;
			}
			if (this._tokenType != JsonTokenType.None)
			{
				this.WriteNewLine(span);
			}
			JsonWriterHelper.WriteIndentation(span.Slice(this.BytesPending), indentation);
			this.BytesPending += indentation;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			escapedPropertyName.CopyTo(span.Slice(this.BytesPending));
			this.BytesPending += escapedPropertyName.Length;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 58;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 32;
			int num4;
			bool flag = Utf8JsonWriter.TryFormatSingle(value, span.Slice(this.BytesPending), out num4);
			this.BytesPending += num4;
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0001A32C File Offset: 0x0001852C
		internal unsafe void WritePropertyName(float value)
		{
			Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)128], 128);
			Span<byte> span2 = span;
			int num;
			bool flag = Utf8JsonWriter.TryFormatSingle(value, span2, out num);
			this.WritePropertyNameUnescaped(span2.Slice(0, num));
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0001A36C File Offset: 0x0001856C
		internal void WriteNumber(ReadOnlySpan<char> propertyName, ReadOnlySpan<byte> utf8FormattedNumber)
		{
			JsonWriterHelper.ValidateProperty(propertyName);
			JsonWriterHelper.ValidateValue(utf8FormattedNumber);
			JsonWriterHelper.ValidateNumber(utf8FormattedNumber);
			this.WriteNumberEscape(propertyName, utf8FormattedNumber);
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.Number;
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0001A395 File Offset: 0x00018595
		internal void WriteNumber(ReadOnlySpan<byte> utf8PropertyName, ReadOnlySpan<byte> utf8FormattedNumber)
		{
			JsonWriterHelper.ValidateProperty(utf8PropertyName);
			JsonWriterHelper.ValidateValue(utf8FormattedNumber);
			JsonWriterHelper.ValidateNumber(utf8FormattedNumber);
			this.WriteNumberEscape(utf8PropertyName, utf8FormattedNumber);
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.Number;
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0001A3BE File Offset: 0x000185BE
		internal void WriteNumber(JsonEncodedText propertyName, ReadOnlySpan<byte> utf8FormattedNumber)
		{
			JsonWriterHelper.ValidateValue(utf8FormattedNumber);
			JsonWriterHelper.ValidateNumber(utf8FormattedNumber);
			this.WriteNumberByOptions(propertyName.EncodedUtf8Bytes, utf8FormattedNumber);
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.Number;
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0001A3E8 File Offset: 0x000185E8
		private void WriteNumberEscape(ReadOnlySpan<char> propertyName, ReadOnlySpan<byte> value)
		{
			int num = JsonWriterHelper.NeedsEscaping(propertyName, this._options.Encoder);
			if (num != -1)
			{
				this.WriteNumberEscapeProperty(propertyName, value, num);
				return;
			}
			this.WriteNumberByOptions(propertyName, value);
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0001A420 File Offset: 0x00018620
		private void WriteNumberEscape(ReadOnlySpan<byte> utf8PropertyName, ReadOnlySpan<byte> value)
		{
			int num = JsonWriterHelper.NeedsEscaping(utf8PropertyName, this._options.Encoder);
			if (num != -1)
			{
				this.WriteNumberEscapeProperty(utf8PropertyName, value, num);
				return;
			}
			this.WriteNumberByOptions(utf8PropertyName, value);
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0001A458 File Offset: 0x00018658
		private unsafe void WriteNumberEscapeProperty(ReadOnlySpan<char> propertyName, ReadOnlySpan<byte> value, int firstEscapeIndexProp)
		{
			char[] array = null;
			int maxEscapedLength = JsonWriterHelper.GetMaxEscapedLength(propertyName.Length, firstEscapeIndexProp);
			Span<char> span2;
			if (maxEscapedLength <= 128)
			{
				Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)256], 128);
				span2 = span;
			}
			else
			{
				span2 = (array = ArrayPool<char>.Shared.Rent(maxEscapedLength));
			}
			Span<char> span3 = span2;
			int num;
			JsonWriterHelper.EscapeString(propertyName, span3, firstEscapeIndexProp, this._options.Encoder, out num);
			this.WriteNumberByOptions(span3.Slice(0, num), value);
			if (array != null)
			{
				ArrayPool<char>.Shared.Return(array, false);
			}
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0001A4E8 File Offset: 0x000186E8
		private unsafe void WriteNumberEscapeProperty(ReadOnlySpan<byte> utf8PropertyName, ReadOnlySpan<byte> value, int firstEscapeIndexProp)
		{
			byte[] array = null;
			int maxEscapedLength = JsonWriterHelper.GetMaxEscapedLength(utf8PropertyName.Length, firstEscapeIndexProp);
			Span<byte> span2;
			if (maxEscapedLength <= 256)
			{
				Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)256], 256);
				span2 = span;
			}
			else
			{
				span2 = (array = ArrayPool<byte>.Shared.Rent(maxEscapedLength));
			}
			Span<byte> span3 = span2;
			int num;
			JsonWriterHelper.EscapeString(utf8PropertyName, span3, firstEscapeIndexProp, this._options.Encoder, out num);
			this.WriteNumberByOptions(span3.Slice(0, num), value);
			if (array != null)
			{
				ArrayPool<byte>.Shared.Return(array, false);
			}
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0001A577 File Offset: 0x00018777
		private void WriteNumberByOptions(ReadOnlySpan<char> propertyName, ReadOnlySpan<byte> value)
		{
			this.ValidateWritingProperty();
			if (this._options.Indented)
			{
				this.WriteLiteralIndented(propertyName, value);
				return;
			}
			this.WriteLiteralMinimized(propertyName, value);
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0001A59D File Offset: 0x0001879D
		private void WriteNumberByOptions(ReadOnlySpan<byte> utf8PropertyName, ReadOnlySpan<byte> value)
		{
			this.ValidateWritingProperty();
			if (this._options.Indented)
			{
				this.WriteLiteralIndented(utf8PropertyName, value);
				return;
			}
			this.WriteLiteralMinimized(utf8PropertyName, value);
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0001A5C4 File Offset: 0x000187C4
		public void WriteString(JsonEncodedText propertyName, Guid value)
		{
			ReadOnlySpan<byte> encodedUtf8Bytes = propertyName.EncodedUtf8Bytes;
			this.WriteStringByOptions(encodedUtf8Bytes, value);
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.String;
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0001A5EE File Offset: 0x000187EE
		[NullableContext(1)]
		public void WriteString(string propertyName, Guid value)
		{
			if (propertyName == null)
			{
				ThrowHelper.ThrowArgumentNullException("propertyName");
			}
			this.WriteString(propertyName.AsSpan(), value);
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0001A60A File Offset: 0x0001880A
		public void WriteString(ReadOnlySpan<char> propertyName, Guid value)
		{
			JsonWriterHelper.ValidateProperty(propertyName);
			this.WriteStringEscape(propertyName, value);
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.String;
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0001A627 File Offset: 0x00018827
		public void WriteString(ReadOnlySpan<byte> utf8PropertyName, Guid value)
		{
			JsonWriterHelper.ValidateProperty(utf8PropertyName);
			this.WriteStringEscape(utf8PropertyName, value);
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.String;
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0001A644 File Offset: 0x00018844
		private void WriteStringEscape(ReadOnlySpan<char> propertyName, Guid value)
		{
			int num = JsonWriterHelper.NeedsEscaping(propertyName, this._options.Encoder);
			if (num != -1)
			{
				this.WriteStringEscapeProperty(propertyName, value, num);
				return;
			}
			this.WriteStringByOptions(propertyName, value);
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0001A67C File Offset: 0x0001887C
		private void WriteStringEscape(ReadOnlySpan<byte> utf8PropertyName, Guid value)
		{
			int num = JsonWriterHelper.NeedsEscaping(utf8PropertyName, this._options.Encoder);
			if (num != -1)
			{
				this.WriteStringEscapeProperty(utf8PropertyName, value, num);
				return;
			}
			this.WriteStringByOptions(utf8PropertyName, value);
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0001A6B4 File Offset: 0x000188B4
		private unsafe void WriteStringEscapeProperty(ReadOnlySpan<char> propertyName, Guid value, int firstEscapeIndexProp)
		{
			char[] array = null;
			int maxEscapedLength = JsonWriterHelper.GetMaxEscapedLength(propertyName.Length, firstEscapeIndexProp);
			Span<char> span2;
			if (maxEscapedLength <= 128)
			{
				Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)256], 128);
				span2 = span;
			}
			else
			{
				span2 = (array = ArrayPool<char>.Shared.Rent(maxEscapedLength));
			}
			Span<char> span3 = span2;
			int num;
			JsonWriterHelper.EscapeString(propertyName, span3, firstEscapeIndexProp, this._options.Encoder, out num);
			this.WriteStringByOptions(span3.Slice(0, num), value);
			if (array != null)
			{
				ArrayPool<char>.Shared.Return(array, false);
			}
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0001A744 File Offset: 0x00018944
		private unsafe void WriteStringEscapeProperty(ReadOnlySpan<byte> utf8PropertyName, Guid value, int firstEscapeIndexProp)
		{
			byte[] array = null;
			int maxEscapedLength = JsonWriterHelper.GetMaxEscapedLength(utf8PropertyName.Length, firstEscapeIndexProp);
			Span<byte> span2;
			if (maxEscapedLength <= 256)
			{
				Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)256], 256);
				span2 = span;
			}
			else
			{
				span2 = (array = ArrayPool<byte>.Shared.Rent(maxEscapedLength));
			}
			Span<byte> span3 = span2;
			int num;
			JsonWriterHelper.EscapeString(utf8PropertyName, span3, firstEscapeIndexProp, this._options.Encoder, out num);
			this.WriteStringByOptions(span3.Slice(0, num), value);
			if (array != null)
			{
				ArrayPool<byte>.Shared.Return(array, false);
			}
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0001A7D3 File Offset: 0x000189D3
		private void WriteStringByOptions(ReadOnlySpan<char> propertyName, Guid value)
		{
			this.ValidateWritingProperty();
			if (this._options.Indented)
			{
				this.WriteStringIndented(propertyName, value);
				return;
			}
			this.WriteStringMinimized(propertyName, value);
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x0001A7F9 File Offset: 0x000189F9
		private void WriteStringByOptions(ReadOnlySpan<byte> utf8PropertyName, Guid value)
		{
			this.ValidateWritingProperty();
			if (this._options.Indented)
			{
				this.WriteStringIndented(utf8PropertyName, value);
				return;
			}
			this.WriteStringMinimized(utf8PropertyName, value);
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0001A820 File Offset: 0x00018A20
		private unsafe void WriteStringMinimized(ReadOnlySpan<char> escapedPropertyName, Guid value)
		{
			int num = escapedPropertyName.Length * 3 + 36 + 6;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			int num2;
			if (this._currentDepth < 0)
			{
				num2 = this.BytesPending;
				this.BytesPending = num2 + 1;
				*span[num2] = 44;
			}
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			this.TranscodeAndWrite(escapedPropertyName, span);
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 58;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			int num3;
			bool flag = Utf8Formatter.TryFormat(value, span.Slice(this.BytesPending), out num3, default(StandardFormat));
			this.BytesPending += num3;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0001A958 File Offset: 0x00018B58
		private unsafe void WriteStringMinimized(ReadOnlySpan<byte> escapedPropertyName, Guid value)
		{
			int num = escapedPropertyName.Length + 36 + 5;
			int num2 = num + 1;
			if (this._memory.Length - this.BytesPending < num2)
			{
				this.Grow(num2);
			}
			Span<byte> span = this._memory.Span;
			int num3;
			if (this._currentDepth < 0)
			{
				num3 = this.BytesPending;
				this.BytesPending = num3 + 1;
				*span[num3] = 44;
			}
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			escapedPropertyName.CopyTo(span.Slice(this.BytesPending));
			this.BytesPending += escapedPropertyName.Length;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 58;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			int num4;
			bool flag = Utf8Formatter.TryFormat(value, span.Slice(this.BytesPending), out num4, default(StandardFormat));
			this.BytesPending += num4;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0001AAB4 File Offset: 0x00018CB4
		private unsafe void WriteStringIndented(ReadOnlySpan<char> escapedPropertyName, Guid value)
		{
			int indentation = this.Indentation;
			int num = indentation + escapedPropertyName.Length * 3 + 36 + 7 + Utf8JsonWriter.s_newLineLength;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			int num2;
			if (this._currentDepth < 0)
			{
				num2 = this.BytesPending;
				this.BytesPending = num2 + 1;
				*span[num2] = 44;
			}
			if (this._tokenType != JsonTokenType.None)
			{
				this.WriteNewLine(span);
			}
			JsonWriterHelper.WriteIndentation(span.Slice(this.BytesPending), indentation);
			this.BytesPending += indentation;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			this.TranscodeAndWrite(escapedPropertyName, span);
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 58;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 32;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			int num3;
			bool flag = Utf8Formatter.TryFormat(value, span.Slice(this.BytesPending), out num3, default(StandardFormat));
			this.BytesPending += num3;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0001AC4C File Offset: 0x00018E4C
		private unsafe void WriteStringIndented(ReadOnlySpan<byte> escapedPropertyName, Guid value)
		{
			int indentation = this.Indentation;
			int num = indentation + escapedPropertyName.Length + 36 + 6;
			int num2 = num + 1 + Utf8JsonWriter.s_newLineLength;
			if (this._memory.Length - this.BytesPending < num2)
			{
				this.Grow(num2);
			}
			Span<byte> span = this._memory.Span;
			int num3;
			if (this._currentDepth < 0)
			{
				num3 = this.BytesPending;
				this.BytesPending = num3 + 1;
				*span[num3] = 44;
			}
			if (this._tokenType != JsonTokenType.None)
			{
				this.WriteNewLine(span);
			}
			JsonWriterHelper.WriteIndentation(span.Slice(this.BytesPending), indentation);
			this.BytesPending += indentation;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			escapedPropertyName.CopyTo(span.Slice(this.BytesPending));
			this.BytesPending += escapedPropertyName.Length;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 58;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 32;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			int num4;
			bool flag = Utf8Formatter.TryFormat(value, span.Slice(this.BytesPending), out num4, default(StandardFormat));
			this.BytesPending += num4;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0001AE04 File Offset: 0x00019004
		internal unsafe void WritePropertyName(Guid value)
		{
			Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)36], 36);
			Span<byte> span2 = span;
			int num;
			bool flag = Utf8Formatter.TryFormat(value, span2, out num, default(StandardFormat));
			this.WritePropertyNameUnescaped(span2.Slice(0, num));
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0001AE48 File Offset: 0x00019048
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void ValidatePropertyNameAndDepth(ReadOnlySpan<char> propertyName)
		{
			if (propertyName.Length > 166666666 || this.CurrentDepth >= this._options.MaxDepth)
			{
				ThrowHelper.ThrowInvalidOperationOrArgumentException(propertyName, this._currentDepth, this._options.MaxDepth);
			}
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0001AE82 File Offset: 0x00019082
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void ValidatePropertyNameAndDepth(ReadOnlySpan<byte> utf8PropertyName)
		{
			if (utf8PropertyName.Length > 166666666 || this.CurrentDepth >= this._options.MaxDepth)
			{
				ThrowHelper.ThrowInvalidOperationOrArgumentException(utf8PropertyName, this._currentDepth, this._options.MaxDepth);
			}
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0001AEBC File Offset: 0x000190BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void ValidateDepth()
		{
			if (this.CurrentDepth >= this._options.MaxDepth)
			{
				ThrowHelper.ThrowInvalidOperationException(this._currentDepth, this._options.MaxDepth);
			}
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x0001AEE7 File Offset: 0x000190E7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void ValidateWritingProperty()
		{
			if (!this._options.SkipValidation && (!this._inObject || this._tokenType == JsonTokenType.PropertyName))
			{
				ThrowHelper.ThrowInvalidOperationException(ExceptionResource.CannotWritePropertyWithinArray, 0, this._options.MaxDepth, 0, this._tokenType);
			}
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x0001AF24 File Offset: 0x00019124
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void ValidateWritingProperty(byte token)
		{
			if (!this._options.SkipValidation)
			{
				if (!this._inObject || this._tokenType == JsonTokenType.PropertyName)
				{
					ThrowHelper.ThrowInvalidOperationException(ExceptionResource.CannotWritePropertyWithinArray, 0, this._options.MaxDepth, 0, this._tokenType);
				}
				this.UpdateBitStackOnStart(token);
			}
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x0001AF70 File Offset: 0x00019170
		private unsafe void WritePropertyNameMinimized(ReadOnlySpan<byte> escapedPropertyName, byte token)
		{
			int num = escapedPropertyName.Length + 4;
			int num2 = num + 1;
			if (this._memory.Length - this.BytesPending < num2)
			{
				this.Grow(num2);
			}
			Span<byte> span = this._memory.Span;
			int num3;
			if (this._currentDepth < 0)
			{
				num3 = this.BytesPending;
				this.BytesPending = num3 + 1;
				*span[num3] = 44;
			}
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			escapedPropertyName.CopyTo(span.Slice(this.BytesPending));
			this.BytesPending += escapedPropertyName.Length;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 58;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = token;
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x0001B06C File Offset: 0x0001926C
		private unsafe void WritePropertyNameIndented(ReadOnlySpan<byte> escapedPropertyName, byte token)
		{
			int indentation = this.Indentation;
			int num = indentation + escapedPropertyName.Length + 5;
			int num2 = num + 1 + Utf8JsonWriter.s_newLineLength;
			if (this._memory.Length - this.BytesPending < num2)
			{
				this.Grow(num2);
			}
			Span<byte> span = this._memory.Span;
			int num3;
			if (this._currentDepth < 0)
			{
				num3 = this.BytesPending;
				this.BytesPending = num3 + 1;
				*span[num3] = 44;
			}
			if (this._tokenType != JsonTokenType.None)
			{
				this.WriteNewLine(span);
			}
			JsonWriterHelper.WriteIndentation(span.Slice(this.BytesPending), indentation);
			this.BytesPending += indentation;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			escapedPropertyName.CopyTo(span.Slice(this.BytesPending));
			this.BytesPending += escapedPropertyName.Length;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 58;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 32;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = token;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x0001B1D4 File Offset: 0x000193D4
		private unsafe void WritePropertyNameMinimized(ReadOnlySpan<char> escapedPropertyName, byte token)
		{
			int num = escapedPropertyName.Length * 3 + 5;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			int num2;
			if (this._currentDepth < 0)
			{
				num2 = this.BytesPending;
				this.BytesPending = num2 + 1;
				*span[num2] = 44;
			}
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			this.TranscodeAndWrite(escapedPropertyName, span);
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 58;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = token;
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x0001B2AC File Offset: 0x000194AC
		private unsafe void WritePropertyNameIndented(ReadOnlySpan<char> escapedPropertyName, byte token)
		{
			int indentation = this.Indentation;
			int num = indentation + escapedPropertyName.Length * 3 + 6 + Utf8JsonWriter.s_newLineLength;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			int num2;
			if (this._currentDepth < 0)
			{
				num2 = this.BytesPending;
				this.BytesPending = num2 + 1;
				*span[num2] = 44;
			}
			if (this._tokenType != JsonTokenType.None)
			{
				this.WriteNewLine(span);
			}
			JsonWriterHelper.WriteIndentation(span.Slice(this.BytesPending), indentation);
			this.BytesPending += indentation;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			this.TranscodeAndWrite(escapedPropertyName, span);
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 58;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 32;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = token;
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x0001B3E0 File Offset: 0x000195E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void TranscodeAndWrite(ReadOnlySpan<char> escapedPropertyName, Span<byte> output)
		{
			int num;
			OperationStatus operationStatus = JsonWriterHelper.ToUtf8(escapedPropertyName, output.Slice(this.BytesPending), out num);
			this.BytesPending += num;
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x0001B411 File Offset: 0x00019611
		public void WriteNull(JsonEncodedText propertyName)
		{
			this.WriteLiteralHelper(propertyName.EncodedUtf8Bytes, JsonConstants.NullValue);
			this._tokenType = JsonTokenType.Null;
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x0001B430 File Offset: 0x00019630
		internal void WriteNullSection(ReadOnlySpan<byte> escapedPropertyNameSection)
		{
			if (this._options.Indented)
			{
				ReadOnlySpan<byte> readOnlySpan = escapedPropertyNameSection.Slice(1, escapedPropertyNameSection.Length - 3);
				this.WriteLiteralHelper(readOnlySpan, JsonConstants.NullValue);
				this._tokenType = JsonTokenType.Null;
				return;
			}
			ReadOnlySpan<byte> nullValue = JsonConstants.NullValue;
			this.WriteLiteralSection(escapedPropertyNameSection, nullValue);
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.Null;
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0001B48D File Offset: 0x0001968D
		private void WriteLiteralHelper(ReadOnlySpan<byte> utf8PropertyName, ReadOnlySpan<byte> value)
		{
			this.WriteLiteralByOptions(utf8PropertyName, value);
			this.SetFlagToAddListSeparatorBeforeNextItem();
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x0001B49D File Offset: 0x0001969D
		[NullableContext(1)]
		public void WriteNull(string propertyName)
		{
			if (propertyName == null)
			{
				ThrowHelper.ThrowArgumentNullException("propertyName");
			}
			this.WriteNull(propertyName.AsSpan());
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0001B4B8 File Offset: 0x000196B8
		public void WriteNull(ReadOnlySpan<char> propertyName)
		{
			JsonWriterHelper.ValidateProperty(propertyName);
			ReadOnlySpan<byte> nullValue = JsonConstants.NullValue;
			this.WriteLiteralEscape(propertyName, nullValue);
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.Null;
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x0001B4E8 File Offset: 0x000196E8
		public void WriteNull(ReadOnlySpan<byte> utf8PropertyName)
		{
			JsonWriterHelper.ValidateProperty(utf8PropertyName);
			ReadOnlySpan<byte> nullValue = JsonConstants.NullValue;
			this.WriteLiteralEscape(utf8PropertyName, nullValue);
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.Null;
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0001B517 File Offset: 0x00019717
		public void WriteBoolean(JsonEncodedText propertyName, bool value)
		{
			if (value)
			{
				this.WriteLiteralHelper(propertyName.EncodedUtf8Bytes, JsonConstants.TrueValue);
				this._tokenType = JsonTokenType.True;
				return;
			}
			this.WriteLiteralHelper(propertyName.EncodedUtf8Bytes, JsonConstants.FalseValue);
			this._tokenType = JsonTokenType.False;
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0001B551 File Offset: 0x00019751
		[NullableContext(1)]
		public void WriteBoolean(string propertyName, bool value)
		{
			if (propertyName == null)
			{
				ThrowHelper.ThrowArgumentNullException("propertyName");
			}
			this.WriteBoolean(propertyName.AsSpan(), value);
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0001B570 File Offset: 0x00019770
		public void WriteBoolean(ReadOnlySpan<char> propertyName, bool value)
		{
			JsonWriterHelper.ValidateProperty(propertyName);
			ReadOnlySpan<byte> readOnlySpan = (value ? JsonConstants.TrueValue : JsonConstants.FalseValue);
			this.WriteLiteralEscape(propertyName, readOnlySpan);
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = (value ? JsonTokenType.True : JsonTokenType.False);
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0001B5B0 File Offset: 0x000197B0
		public void WriteBoolean(ReadOnlySpan<byte> utf8PropertyName, bool value)
		{
			JsonWriterHelper.ValidateProperty(utf8PropertyName);
			ReadOnlySpan<byte> readOnlySpan = (value ? JsonConstants.TrueValue : JsonConstants.FalseValue);
			this.WriteLiteralEscape(utf8PropertyName, readOnlySpan);
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = (value ? JsonTokenType.True : JsonTokenType.False);
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0001B5F0 File Offset: 0x000197F0
		private void WriteLiteralEscape(ReadOnlySpan<char> propertyName, ReadOnlySpan<byte> value)
		{
			int num = JsonWriterHelper.NeedsEscaping(propertyName, this._options.Encoder);
			if (num != -1)
			{
				this.WriteLiteralEscapeProperty(propertyName, value, num);
				return;
			}
			this.WriteLiteralByOptions(propertyName, value);
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x0001B628 File Offset: 0x00019828
		private void WriteLiteralEscape(ReadOnlySpan<byte> utf8PropertyName, ReadOnlySpan<byte> value)
		{
			int num = JsonWriterHelper.NeedsEscaping(utf8PropertyName, this._options.Encoder);
			if (num != -1)
			{
				this.WriteLiteralEscapeProperty(utf8PropertyName, value, num);
				return;
			}
			this.WriteLiteralByOptions(utf8PropertyName, value);
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0001B660 File Offset: 0x00019860
		private unsafe void WriteLiteralEscapeProperty(ReadOnlySpan<char> propertyName, ReadOnlySpan<byte> value, int firstEscapeIndexProp)
		{
			char[] array = null;
			int maxEscapedLength = JsonWriterHelper.GetMaxEscapedLength(propertyName.Length, firstEscapeIndexProp);
			Span<char> span2;
			if (maxEscapedLength <= 128)
			{
				Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)256], 128);
				span2 = span;
			}
			else
			{
				span2 = (array = ArrayPool<char>.Shared.Rent(maxEscapedLength));
			}
			Span<char> span3 = span2;
			int num;
			JsonWriterHelper.EscapeString(propertyName, span3, firstEscapeIndexProp, this._options.Encoder, out num);
			this.WriteLiteralByOptions(span3.Slice(0, num), value);
			if (array != null)
			{
				ArrayPool<char>.Shared.Return(array, false);
			}
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0001B6F0 File Offset: 0x000198F0
		private unsafe void WriteLiteralEscapeProperty(ReadOnlySpan<byte> utf8PropertyName, ReadOnlySpan<byte> value, int firstEscapeIndexProp)
		{
			byte[] array = null;
			int maxEscapedLength = JsonWriterHelper.GetMaxEscapedLength(utf8PropertyName.Length, firstEscapeIndexProp);
			Span<byte> span2;
			if (maxEscapedLength <= 256)
			{
				Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)256], 256);
				span2 = span;
			}
			else
			{
				span2 = (array = ArrayPool<byte>.Shared.Rent(maxEscapedLength));
			}
			Span<byte> span3 = span2;
			int num;
			JsonWriterHelper.EscapeString(utf8PropertyName, span3, firstEscapeIndexProp, this._options.Encoder, out num);
			this.WriteLiteralByOptions(span3.Slice(0, num), value);
			if (array != null)
			{
				ArrayPool<byte>.Shared.Return(array, false);
			}
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0001B77F File Offset: 0x0001997F
		private void WriteLiteralByOptions(ReadOnlySpan<char> propertyName, ReadOnlySpan<byte> value)
		{
			this.ValidateWritingProperty();
			if (this._options.Indented)
			{
				this.WriteLiteralIndented(propertyName, value);
				return;
			}
			this.WriteLiteralMinimized(propertyName, value);
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x0001B7A5 File Offset: 0x000199A5
		private void WriteLiteralByOptions(ReadOnlySpan<byte> utf8PropertyName, ReadOnlySpan<byte> value)
		{
			this.ValidateWritingProperty();
			if (this._options.Indented)
			{
				this.WriteLiteralIndented(utf8PropertyName, value);
				return;
			}
			this.WriteLiteralMinimized(utf8PropertyName, value);
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0001B7CC File Offset: 0x000199CC
		private unsafe void WriteLiteralMinimized(ReadOnlySpan<char> escapedPropertyName, ReadOnlySpan<byte> value)
		{
			int num = escapedPropertyName.Length * 3 + value.Length + 4;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			int num2;
			if (this._currentDepth < 0)
			{
				num2 = this.BytesPending;
				this.BytesPending = num2 + 1;
				*span[num2] = 44;
			}
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			this.TranscodeAndWrite(escapedPropertyName, span);
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 58;
			value.CopyTo(span.Slice(this.BytesPending));
			this.BytesPending += value.Length;
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0001B8BC File Offset: 0x00019ABC
		private unsafe void WriteLiteralMinimized(ReadOnlySpan<byte> escapedPropertyName, ReadOnlySpan<byte> value)
		{
			int num = escapedPropertyName.Length + value.Length + 3;
			int num2 = num + 1;
			if (this._memory.Length - this.BytesPending < num2)
			{
				this.Grow(num2);
			}
			Span<byte> span = this._memory.Span;
			int num3;
			if (this._currentDepth < 0)
			{
				num3 = this.BytesPending;
				this.BytesPending = num3 + 1;
				*span[num3] = 44;
			}
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			escapedPropertyName.CopyTo(span.Slice(this.BytesPending));
			this.BytesPending += escapedPropertyName.Length;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 58;
			value.CopyTo(span.Slice(this.BytesPending));
			this.BytesPending += value.Length;
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0001B9CC File Offset: 0x00019BCC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe void WriteLiteralSection(ReadOnlySpan<byte> escapedPropertyNameSection, ReadOnlySpan<byte> value)
		{
			int num = escapedPropertyNameSection.Length + value.Length;
			int num2 = num + 1;
			if (this._memory.Length - this.BytesPending < num2)
			{
				this.Grow(num2);
			}
			Span<byte> span = this._memory.Span;
			if (this._currentDepth < 0)
			{
				int bytesPending = this.BytesPending;
				this.BytesPending = bytesPending + 1;
				*span[bytesPending] = 44;
			}
			escapedPropertyNameSection.CopyTo(span.Slice(this.BytesPending));
			this.BytesPending += escapedPropertyNameSection.Length;
			value.CopyTo(span.Slice(this.BytesPending));
			this.BytesPending += value.Length;
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x0001BA8C File Offset: 0x00019C8C
		private unsafe void WriteLiteralIndented(ReadOnlySpan<char> escapedPropertyName, ReadOnlySpan<byte> value)
		{
			int indentation = this.Indentation;
			int num = indentation + escapedPropertyName.Length * 3 + value.Length + 5 + Utf8JsonWriter.s_newLineLength;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			int num2;
			if (this._currentDepth < 0)
			{
				num2 = this.BytesPending;
				this.BytesPending = num2 + 1;
				*span[num2] = 44;
			}
			if (this._tokenType != JsonTokenType.None)
			{
				this.WriteNewLine(span);
			}
			JsonWriterHelper.WriteIndentation(span.Slice(this.BytesPending), indentation);
			this.BytesPending += indentation;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			this.TranscodeAndWrite(escapedPropertyName, span);
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 58;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 32;
			value.CopyTo(span.Slice(this.BytesPending));
			this.BytesPending += value.Length;
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x0001BBD4 File Offset: 0x00019DD4
		private unsafe void WriteLiteralIndented(ReadOnlySpan<byte> escapedPropertyName, ReadOnlySpan<byte> value)
		{
			int indentation = this.Indentation;
			int num = indentation + escapedPropertyName.Length + value.Length + 4;
			int num2 = num + 1 + Utf8JsonWriter.s_newLineLength;
			if (this._memory.Length - this.BytesPending < num2)
			{
				this.Grow(num2);
			}
			Span<byte> span = this._memory.Span;
			int num3;
			if (this._currentDepth < 0)
			{
				num3 = this.BytesPending;
				this.BytesPending = num3 + 1;
				*span[num3] = 44;
			}
			if (this._tokenType != JsonTokenType.None)
			{
				this.WriteNewLine(span);
			}
			JsonWriterHelper.WriteIndentation(span.Slice(this.BytesPending), indentation);
			this.BytesPending += indentation;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			escapedPropertyName.CopyTo(span.Slice(this.BytesPending));
			this.BytesPending += escapedPropertyName.Length;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 58;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 32;
			value.CopyTo(span.Slice(this.BytesPending));
			this.BytesPending += value.Length;
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x0001BD50 File Offset: 0x00019F50
		internal unsafe void WritePropertyName(bool value)
		{
			Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)5], 5);
			Span<byte> span2 = span;
			int num;
			bool flag = Utf8Formatter.TryFormat(value, span2, out num, default(StandardFormat));
			this.WritePropertyNameUnescaped(span2.Slice(0, num));
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0001BD94 File Offset: 0x00019F94
		public void WriteNumber(JsonEncodedText propertyName, long value)
		{
			ReadOnlySpan<byte> encodedUtf8Bytes = propertyName.EncodedUtf8Bytes;
			this.WriteNumberByOptions(encodedUtf8Bytes, value);
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.Number;
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0001BDBE File Offset: 0x00019FBE
		[NullableContext(1)]
		public void WriteNumber(string propertyName, long value)
		{
			if (propertyName == null)
			{
				ThrowHelper.ThrowArgumentNullException("propertyName");
			}
			this.WriteNumber(propertyName.AsSpan(), value);
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0001BDDA File Offset: 0x00019FDA
		public void WriteNumber(ReadOnlySpan<char> propertyName, long value)
		{
			JsonWriterHelper.ValidateProperty(propertyName);
			this.WriteNumberEscape(propertyName, value);
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.Number;
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0001BDF7 File Offset: 0x00019FF7
		public void WriteNumber(ReadOnlySpan<byte> utf8PropertyName, long value)
		{
			JsonWriterHelper.ValidateProperty(utf8PropertyName);
			this.WriteNumberEscape(utf8PropertyName, value);
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.Number;
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0001BE14 File Offset: 0x0001A014
		public void WriteNumber(JsonEncodedText propertyName, int value)
		{
			this.WriteNumber(propertyName, (long)value);
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x0001BE1F File Offset: 0x0001A01F
		[NullableContext(1)]
		public void WriteNumber(string propertyName, int value)
		{
			if (propertyName == null)
			{
				ThrowHelper.ThrowArgumentNullException("propertyName");
			}
			this.WriteNumber(propertyName.AsSpan(), (long)value);
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0001BE3C File Offset: 0x0001A03C
		public void WriteNumber(ReadOnlySpan<char> propertyName, int value)
		{
			this.WriteNumber(propertyName, (long)value);
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0001BE47 File Offset: 0x0001A047
		public void WriteNumber(ReadOnlySpan<byte> utf8PropertyName, int value)
		{
			this.WriteNumber(utf8PropertyName, (long)value);
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0001BE54 File Offset: 0x0001A054
		private void WriteNumberEscape(ReadOnlySpan<char> propertyName, long value)
		{
			int num = JsonWriterHelper.NeedsEscaping(propertyName, this._options.Encoder);
			if (num != -1)
			{
				this.WriteNumberEscapeProperty(propertyName, value, num);
				return;
			}
			this.WriteNumberByOptions(propertyName, value);
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0001BE8C File Offset: 0x0001A08C
		private void WriteNumberEscape(ReadOnlySpan<byte> utf8PropertyName, long value)
		{
			int num = JsonWriterHelper.NeedsEscaping(utf8PropertyName, this._options.Encoder);
			if (num != -1)
			{
				this.WriteNumberEscapeProperty(utf8PropertyName, value, num);
				return;
			}
			this.WriteNumberByOptions(utf8PropertyName, value);
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0001BEC4 File Offset: 0x0001A0C4
		private unsafe void WriteNumberEscapeProperty(ReadOnlySpan<char> propertyName, long value, int firstEscapeIndexProp)
		{
			char[] array = null;
			int maxEscapedLength = JsonWriterHelper.GetMaxEscapedLength(propertyName.Length, firstEscapeIndexProp);
			Span<char> span2;
			if (maxEscapedLength <= 128)
			{
				Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)256], 128);
				span2 = span;
			}
			else
			{
				span2 = (array = ArrayPool<char>.Shared.Rent(maxEscapedLength));
			}
			Span<char> span3 = span2;
			int num;
			JsonWriterHelper.EscapeString(propertyName, span3, firstEscapeIndexProp, this._options.Encoder, out num);
			this.WriteNumberByOptions(span3.Slice(0, num), value);
			if (array != null)
			{
				ArrayPool<char>.Shared.Return(array, false);
			}
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0001BF54 File Offset: 0x0001A154
		private unsafe void WriteNumberEscapeProperty(ReadOnlySpan<byte> utf8PropertyName, long value, int firstEscapeIndexProp)
		{
			byte[] array = null;
			int maxEscapedLength = JsonWriterHelper.GetMaxEscapedLength(utf8PropertyName.Length, firstEscapeIndexProp);
			Span<byte> span2;
			if (maxEscapedLength <= 256)
			{
				Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)256], 256);
				span2 = span;
			}
			else
			{
				span2 = (array = ArrayPool<byte>.Shared.Rent(maxEscapedLength));
			}
			Span<byte> span3 = span2;
			int num;
			JsonWriterHelper.EscapeString(utf8PropertyName, span3, firstEscapeIndexProp, this._options.Encoder, out num);
			this.WriteNumberByOptions(span3.Slice(0, num), value);
			if (array != null)
			{
				ArrayPool<byte>.Shared.Return(array, false);
			}
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0001BFE3 File Offset: 0x0001A1E3
		private void WriteNumberByOptions(ReadOnlySpan<char> propertyName, long value)
		{
			this.ValidateWritingProperty();
			if (this._options.Indented)
			{
				this.WriteNumberIndented(propertyName, value);
				return;
			}
			this.WriteNumberMinimized(propertyName, value);
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0001C009 File Offset: 0x0001A209
		private void WriteNumberByOptions(ReadOnlySpan<byte> utf8PropertyName, long value)
		{
			this.ValidateWritingProperty();
			if (this._options.Indented)
			{
				this.WriteNumberIndented(utf8PropertyName, value);
				return;
			}
			this.WriteNumberMinimized(utf8PropertyName, value);
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0001C030 File Offset: 0x0001A230
		private unsafe void WriteNumberMinimized(ReadOnlySpan<char> escapedPropertyName, long value)
		{
			int num = escapedPropertyName.Length * 3 + 20 + 4;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			int num2;
			if (this._currentDepth < 0)
			{
				num2 = this.BytesPending;
				this.BytesPending = num2 + 1;
				*span[num2] = 44;
			}
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			this.TranscodeAndWrite(escapedPropertyName, span);
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 58;
			int num3;
			bool flag = Utf8Formatter.TryFormat(value, span.Slice(this.BytesPending), out num3, default(StandardFormat));
			this.BytesPending += num3;
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0001C12C File Offset: 0x0001A32C
		private unsafe void WriteNumberMinimized(ReadOnlySpan<byte> escapedPropertyName, long value)
		{
			int num = escapedPropertyName.Length + 20 + 3;
			int num2 = num + 1;
			if (this._memory.Length - this.BytesPending < num2)
			{
				this.Grow(num2);
			}
			Span<byte> span = this._memory.Span;
			int num3;
			if (this._currentDepth < 0)
			{
				num3 = this.BytesPending;
				this.BytesPending = num3 + 1;
				*span[num3] = 44;
			}
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			escapedPropertyName.CopyTo(span.Slice(this.BytesPending));
			this.BytesPending += escapedPropertyName.Length;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 58;
			int num4;
			bool flag = Utf8Formatter.TryFormat(value, span.Slice(this.BytesPending), out num4, default(StandardFormat));
			this.BytesPending += num4;
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0001C24C File Offset: 0x0001A44C
		private unsafe void WriteNumberIndented(ReadOnlySpan<char> escapedPropertyName, long value)
		{
			int indentation = this.Indentation;
			int num = indentation + escapedPropertyName.Length * 3 + 20 + 5 + Utf8JsonWriter.s_newLineLength;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			int num2;
			if (this._currentDepth < 0)
			{
				num2 = this.BytesPending;
				this.BytesPending = num2 + 1;
				*span[num2] = 44;
			}
			if (this._tokenType != JsonTokenType.None)
			{
				this.WriteNewLine(span);
			}
			JsonWriterHelper.WriteIndentation(span.Slice(this.BytesPending), indentation);
			this.BytesPending += indentation;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			this.TranscodeAndWrite(escapedPropertyName, span);
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 58;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 32;
			int num3;
			bool flag = Utf8Formatter.TryFormat(value, span.Slice(this.BytesPending), out num3, default(StandardFormat));
			this.BytesPending += num3;
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x0001C3A8 File Offset: 0x0001A5A8
		private unsafe void WriteNumberIndented(ReadOnlySpan<byte> escapedPropertyName, long value)
		{
			int indentation = this.Indentation;
			int num = indentation + escapedPropertyName.Length + 20 + 4;
			int num2 = num + 1 + Utf8JsonWriter.s_newLineLength;
			if (this._memory.Length - this.BytesPending < num2)
			{
				this.Grow(num2);
			}
			Span<byte> span = this._memory.Span;
			int num3;
			if (this._currentDepth < 0)
			{
				num3 = this.BytesPending;
				this.BytesPending = num3 + 1;
				*span[num3] = 44;
			}
			if (this._tokenType != JsonTokenType.None)
			{
				this.WriteNewLine(span);
			}
			JsonWriterHelper.WriteIndentation(span.Slice(this.BytesPending), indentation);
			this.BytesPending += indentation;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			escapedPropertyName.CopyTo(span.Slice(this.BytesPending));
			this.BytesPending += escapedPropertyName.Length;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 58;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 32;
			int num4;
			bool flag = Utf8Formatter.TryFormat(value, span.Slice(this.BytesPending), out num4, default(StandardFormat));
			this.BytesPending += num4;
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0001C524 File Offset: 0x0001A724
		internal void WritePropertyName(int value)
		{
			this.WritePropertyName((long)value);
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0001C530 File Offset: 0x0001A730
		internal unsafe void WritePropertyName(long value)
		{
			Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)20], 20);
			Span<byte> span2 = span;
			int num;
			bool flag = Utf8Formatter.TryFormat(value, span2, out num, default(StandardFormat));
			this.WritePropertyNameUnescaped(span2.Slice(0, num));
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0001C574 File Offset: 0x0001A774
		public void WritePropertyName(JsonEncodedText propertyName)
		{
			this.WritePropertyNameHelper(propertyName.EncodedUtf8Bytes);
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0001C584 File Offset: 0x0001A784
		internal void WritePropertyNameSection(ReadOnlySpan<byte> escapedPropertyNameSection)
		{
			if (this._options.Indented)
			{
				ReadOnlySpan<byte> readOnlySpan = escapedPropertyNameSection.Slice(1, escapedPropertyNameSection.Length - 3);
				this.WritePropertyNameHelper(readOnlySpan);
				return;
			}
			this.WriteStringPropertyNameSection(escapedPropertyNameSection);
			this._currentDepth &= int.MaxValue;
			this._tokenType = JsonTokenType.PropertyName;
			this._commentAfterNoneOrPropertyName = false;
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0001C5DF File Offset: 0x0001A7DF
		private void WritePropertyNameHelper(ReadOnlySpan<byte> utf8PropertyName)
		{
			this.WriteStringByOptionsPropertyName(utf8PropertyName);
			this._currentDepth &= int.MaxValue;
			this._tokenType = JsonTokenType.PropertyName;
			this._commentAfterNoneOrPropertyName = false;
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0001C608 File Offset: 0x0001A808
		[NullableContext(1)]
		public void WritePropertyName(string propertyName)
		{
			if (propertyName == null)
			{
				ThrowHelper.ThrowArgumentNullException("propertyName");
			}
			this.WritePropertyName(propertyName.AsSpan());
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0001C624 File Offset: 0x0001A824
		public void WritePropertyName(ReadOnlySpan<char> propertyName)
		{
			JsonWriterHelper.ValidateProperty(propertyName);
			int num = JsonWriterHelper.NeedsEscaping(propertyName, this._options.Encoder);
			if (num != -1)
			{
				this.WriteStringEscapeProperty(propertyName, num);
			}
			else
			{
				this.WriteStringByOptionsPropertyName(propertyName);
			}
			this._currentDepth &= int.MaxValue;
			this._tokenType = JsonTokenType.PropertyName;
			this._commentAfterNoneOrPropertyName = false;
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0001C680 File Offset: 0x0001A880
		private unsafe void WriteStringEscapeProperty([ScopedRef] ReadOnlySpan<char> propertyName, int firstEscapeIndexProp)
		{
			char[] array = null;
			if (firstEscapeIndexProp != -1)
			{
				int maxEscapedLength = JsonWriterHelper.GetMaxEscapedLength(propertyName.Length, firstEscapeIndexProp);
				Span<char> span;
				if (maxEscapedLength > 128)
				{
					array = ArrayPool<char>.Shared.Rent(maxEscapedLength);
					span = array;
				}
				else
				{
					Span<char> span2 = new Span<char>(stackalloc byte[(UIntPtr)256], 128);
					span = span2;
				}
				int num;
				JsonWriterHelper.EscapeString(propertyName, span, firstEscapeIndexProp, this._options.Encoder, out num);
				propertyName = span.Slice(0, num);
			}
			this.WriteStringByOptionsPropertyName(propertyName);
			if (array != null)
			{
				ArrayPool<char>.Shared.Return(array, false);
			}
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0001C710 File Offset: 0x0001A910
		private void WriteStringByOptionsPropertyName(ReadOnlySpan<char> propertyName)
		{
			this.ValidateWritingProperty();
			if (this._options.Indented)
			{
				this.WriteStringIndentedPropertyName(propertyName);
				return;
			}
			this.WriteStringMinimizedPropertyName(propertyName);
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0001C734 File Offset: 0x0001A934
		private unsafe void WriteStringMinimizedPropertyName(ReadOnlySpan<char> escapedPropertyName)
		{
			int num = escapedPropertyName.Length * 3 + 4;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			int num2;
			if (this._currentDepth < 0)
			{
				num2 = this.BytesPending;
				this.BytesPending = num2 + 1;
				*span[num2] = 44;
			}
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			this.TranscodeAndWrite(escapedPropertyName, span);
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 58;
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0001C7F4 File Offset: 0x0001A9F4
		private unsafe void WriteStringIndentedPropertyName(ReadOnlySpan<char> escapedPropertyName)
		{
			int indentation = this.Indentation;
			int num = indentation + escapedPropertyName.Length * 3 + 5 + Utf8JsonWriter.s_newLineLength;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			int num2;
			if (this._currentDepth < 0)
			{
				num2 = this.BytesPending;
				this.BytesPending = num2 + 1;
				*span[num2] = 44;
			}
			if (this._tokenType != JsonTokenType.None)
			{
				this.WriteNewLine(span);
			}
			JsonWriterHelper.WriteIndentation(span.Slice(this.BytesPending), indentation);
			this.BytesPending += indentation;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			this.TranscodeAndWrite(escapedPropertyName, span);
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 58;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 32;
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0001C90C File Offset: 0x0001AB0C
		public void WritePropertyName(ReadOnlySpan<byte> utf8PropertyName)
		{
			JsonWriterHelper.ValidateProperty(utf8PropertyName);
			int num = JsonWriterHelper.NeedsEscaping(utf8PropertyName, this._options.Encoder);
			if (num != -1)
			{
				this.WriteStringEscapeProperty(utf8PropertyName, num);
			}
			else
			{
				this.WriteStringByOptionsPropertyName(utf8PropertyName);
			}
			this._currentDepth &= int.MaxValue;
			this._tokenType = JsonTokenType.PropertyName;
			this._commentAfterNoneOrPropertyName = false;
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0001C966 File Offset: 0x0001AB66
		private void WritePropertyNameUnescaped(ReadOnlySpan<byte> utf8PropertyName)
		{
			JsonWriterHelper.ValidateProperty(utf8PropertyName);
			this.WriteStringByOptionsPropertyName(utf8PropertyName);
			this._currentDepth &= int.MaxValue;
			this._tokenType = JsonTokenType.PropertyName;
			this._commentAfterNoneOrPropertyName = false;
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0001C998 File Offset: 0x0001AB98
		private unsafe void WriteStringEscapeProperty([ScopedRef] ReadOnlySpan<byte> utf8PropertyName, int firstEscapeIndexProp)
		{
			byte[] array = null;
			if (firstEscapeIndexProp != -1)
			{
				int maxEscapedLength = JsonWriterHelper.GetMaxEscapedLength(utf8PropertyName.Length, firstEscapeIndexProp);
				Span<byte> span;
				if (maxEscapedLength > 256)
				{
					array = ArrayPool<byte>.Shared.Rent(maxEscapedLength);
					span = array;
				}
				else
				{
					Span<byte> span2 = new Span<byte>(stackalloc byte[(UIntPtr)256], 256);
					span = span2;
				}
				int num;
				JsonWriterHelper.EscapeString(utf8PropertyName, span, firstEscapeIndexProp, this._options.Encoder, out num);
				utf8PropertyName = span.Slice(0, num);
			}
			this.WriteStringByOptionsPropertyName(utf8PropertyName);
			if (array != null)
			{
				ArrayPool<byte>.Shared.Return(array, false);
			}
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0001CA28 File Offset: 0x0001AC28
		private void WriteStringByOptionsPropertyName(ReadOnlySpan<byte> utf8PropertyName)
		{
			this.ValidateWritingProperty();
			if (this._options.Indented)
			{
				this.WriteStringIndentedPropertyName(utf8PropertyName);
				return;
			}
			this.WriteStringMinimizedPropertyName(utf8PropertyName);
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0001CA4C File Offset: 0x0001AC4C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe void WriteStringMinimizedPropertyName(ReadOnlySpan<byte> escapedPropertyName)
		{
			int num = escapedPropertyName.Length + 3;
			int num2 = num + 1;
			if (this._memory.Length - this.BytesPending < num2)
			{
				this.Grow(num2);
			}
			Span<byte> span = this._memory.Span;
			int num3;
			if (this._currentDepth < 0)
			{
				num3 = this.BytesPending;
				this.BytesPending = num3 + 1;
				*span[num3] = 44;
			}
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			escapedPropertyName.CopyTo(span.Slice(this.BytesPending));
			this.BytesPending += escapedPropertyName.Length;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 58;
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0001CB2C File Offset: 0x0001AD2C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe void WriteStringPropertyNameSection(ReadOnlySpan<byte> escapedPropertyNameSection)
		{
			int num = escapedPropertyNameSection.Length + 1;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			if (this._currentDepth < 0)
			{
				int bytesPending = this.BytesPending;
				this.BytesPending = bytesPending + 1;
				*span[bytesPending] = 44;
			}
			escapedPropertyNameSection.CopyTo(span.Slice(this.BytesPending));
			this.BytesPending += escapedPropertyNameSection.Length;
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x0001CBB8 File Offset: 0x0001ADB8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe void WriteStringIndentedPropertyName(ReadOnlySpan<byte> escapedPropertyName)
		{
			int indentation = this.Indentation;
			int num = indentation + escapedPropertyName.Length + 4;
			int num2 = num + 1 + Utf8JsonWriter.s_newLineLength;
			if (this._memory.Length - this.BytesPending < num2)
			{
				this.Grow(num2);
			}
			Span<byte> span = this._memory.Span;
			int num3;
			if (this._currentDepth < 0)
			{
				num3 = this.BytesPending;
				this.BytesPending = num3 + 1;
				*span[num3] = 44;
			}
			if (this._tokenType != JsonTokenType.None)
			{
				this.WriteNewLine(span);
			}
			JsonWriterHelper.WriteIndentation(span.Slice(this.BytesPending), indentation);
			this.BytesPending += indentation;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			escapedPropertyName.CopyTo(span.Slice(this.BytesPending));
			this.BytesPending += escapedPropertyName.Length;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 58;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 32;
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x0001CD01 File Offset: 0x0001AF01
		public void WriteString(JsonEncodedText propertyName, JsonEncodedText value)
		{
			this.WriteStringHelper(propertyName.EncodedUtf8Bytes, value.EncodedUtf8Bytes);
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x0001CD17 File Offset: 0x0001AF17
		private void WriteStringHelper(ReadOnlySpan<byte> utf8PropertyName, ReadOnlySpan<byte> utf8Value)
		{
			this.WriteStringByOptions(utf8PropertyName, utf8Value);
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.String;
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0001CD2E File Offset: 0x0001AF2E
		[NullableContext(1)]
		public void WriteString(string propertyName, JsonEncodedText value)
		{
			if (propertyName == null)
			{
				ThrowHelper.ThrowArgumentNullException("propertyName");
			}
			this.WriteString(propertyName.AsSpan(), value);
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x0001CD4A File Offset: 0x0001AF4A
		[NullableContext(1)]
		public void WriteString(string propertyName, [Nullable(2)] string value)
		{
			if (propertyName == null)
			{
				ThrowHelper.ThrowArgumentNullException("propertyName");
			}
			if (value == null)
			{
				this.WriteNull(propertyName.AsSpan());
				return;
			}
			this.WriteString(propertyName.AsSpan(), value.AsSpan());
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x0001CD7B File Offset: 0x0001AF7B
		public void WriteString(ReadOnlySpan<char> propertyName, ReadOnlySpan<char> value)
		{
			JsonWriterHelper.ValidatePropertyAndValue(propertyName, value);
			this.WriteStringEscape(propertyName, value);
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.String;
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x0001CD99 File Offset: 0x0001AF99
		public void WriteString(ReadOnlySpan<byte> utf8PropertyName, ReadOnlySpan<byte> utf8Value)
		{
			JsonWriterHelper.ValidatePropertyAndValue(utf8PropertyName, utf8Value);
			this.WriteStringEscape(utf8PropertyName, utf8Value);
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.String;
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x0001CDB7 File Offset: 0x0001AFB7
		[NullableContext(2)]
		public void WriteString(JsonEncodedText propertyName, string value)
		{
			if (value == null)
			{
				this.WriteNull(propertyName);
				return;
			}
			this.WriteString(propertyName, value.AsSpan());
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x0001CDD1 File Offset: 0x0001AFD1
		public void WriteString(JsonEncodedText propertyName, ReadOnlySpan<char> value)
		{
			this.WriteStringHelperEscapeValue(propertyName.EncodedUtf8Bytes, value);
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x0001CDE4 File Offset: 0x0001AFE4
		private void WriteStringHelperEscapeValue(ReadOnlySpan<byte> utf8PropertyName, ReadOnlySpan<char> value)
		{
			JsonWriterHelper.ValidateValue(value);
			int num = JsonWriterHelper.NeedsEscaping(value, this._options.Encoder);
			if (num != -1)
			{
				this.WriteStringEscapeValueOnly(utf8PropertyName, value, num);
			}
			else
			{
				this.WriteStringByOptions(utf8PropertyName, value);
			}
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.String;
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x0001CE2D File Offset: 0x0001B02D
		public void WriteString([Nullable(1)] string propertyName, ReadOnlySpan<char> value)
		{
			if (propertyName == null)
			{
				ThrowHelper.ThrowArgumentNullException("propertyName");
			}
			this.WriteString(propertyName.AsSpan(), value);
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0001CE49 File Offset: 0x0001B049
		public void WriteString(ReadOnlySpan<byte> utf8PropertyName, ReadOnlySpan<char> value)
		{
			JsonWriterHelper.ValidatePropertyAndValue(utf8PropertyName, value);
			this.WriteStringEscape(utf8PropertyName, value);
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.String;
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x0001CE67 File Offset: 0x0001B067
		public void WriteString(JsonEncodedText propertyName, ReadOnlySpan<byte> utf8Value)
		{
			this.WriteStringHelperEscapeValue(propertyName.EncodedUtf8Bytes, utf8Value);
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x0001CE78 File Offset: 0x0001B078
		private void WriteStringHelperEscapeValue(ReadOnlySpan<byte> utf8PropertyName, ReadOnlySpan<byte> utf8Value)
		{
			JsonWriterHelper.ValidateValue(utf8Value);
			int num = JsonWriterHelper.NeedsEscaping(utf8Value, this._options.Encoder);
			if (num != -1)
			{
				this.WriteStringEscapeValueOnly(utf8PropertyName, utf8Value, num);
			}
			else
			{
				this.WriteStringByOptions(utf8PropertyName, utf8Value);
			}
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.String;
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0001CEC1 File Offset: 0x0001B0C1
		public void WriteString([Nullable(1)] string propertyName, ReadOnlySpan<byte> utf8Value)
		{
			if (propertyName == null)
			{
				ThrowHelper.ThrowArgumentNullException("propertyName");
			}
			this.WriteString(propertyName.AsSpan(), utf8Value);
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x0001CEDD File Offset: 0x0001B0DD
		public void WriteString(ReadOnlySpan<char> propertyName, ReadOnlySpan<byte> utf8Value)
		{
			JsonWriterHelper.ValidatePropertyAndValue(propertyName, utf8Value);
			this.WriteStringEscape(propertyName, utf8Value);
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.String;
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x0001CEFB File Offset: 0x0001B0FB
		public void WriteString(ReadOnlySpan<char> propertyName, JsonEncodedText value)
		{
			this.WriteStringHelperEscapeProperty(propertyName, value.EncodedUtf8Bytes);
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x0001CF0C File Offset: 0x0001B10C
		private void WriteStringHelperEscapeProperty(ReadOnlySpan<char> propertyName, ReadOnlySpan<byte> utf8Value)
		{
			JsonWriterHelper.ValidateProperty(propertyName);
			int num = JsonWriterHelper.NeedsEscaping(propertyName, this._options.Encoder);
			if (num != -1)
			{
				this.WriteStringEscapePropertyOnly(propertyName, utf8Value, num);
			}
			else
			{
				this.WriteStringByOptions(propertyName, utf8Value);
			}
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.String;
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x0001CF55 File Offset: 0x0001B155
		public void WriteString(ReadOnlySpan<char> propertyName, [Nullable(2)] string value)
		{
			if (value == null)
			{
				this.WriteNull(propertyName);
				return;
			}
			this.WriteString(propertyName, value.AsSpan());
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x0001CF6F File Offset: 0x0001B16F
		public void WriteString(ReadOnlySpan<byte> utf8PropertyName, JsonEncodedText value)
		{
			this.WriteStringHelperEscapeProperty(utf8PropertyName, value.EncodedUtf8Bytes);
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x0001CF80 File Offset: 0x0001B180
		private void WriteStringHelperEscapeProperty(ReadOnlySpan<byte> utf8PropertyName, ReadOnlySpan<byte> utf8Value)
		{
			JsonWriterHelper.ValidateProperty(utf8PropertyName);
			int num = JsonWriterHelper.NeedsEscaping(utf8PropertyName, this._options.Encoder);
			if (num != -1)
			{
				this.WriteStringEscapePropertyOnly(utf8PropertyName, utf8Value, num);
			}
			else
			{
				this.WriteStringByOptions(utf8PropertyName, utf8Value);
			}
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.String;
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0001CFC9 File Offset: 0x0001B1C9
		public void WriteString(ReadOnlySpan<byte> utf8PropertyName, [Nullable(2)] string value)
		{
			if (value == null)
			{
				this.WriteNull(utf8PropertyName);
				return;
			}
			this.WriteString(utf8PropertyName, value.AsSpan());
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x0001CFE4 File Offset: 0x0001B1E4
		private unsafe void WriteStringEscapeValueOnly(ReadOnlySpan<byte> escapedPropertyName, ReadOnlySpan<byte> utf8Value, int firstEscapeIndex)
		{
			byte[] array = null;
			int maxEscapedLength = JsonWriterHelper.GetMaxEscapedLength(utf8Value.Length, firstEscapeIndex);
			Span<byte> span2;
			if (maxEscapedLength <= 256)
			{
				Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)256], 256);
				span2 = span;
			}
			else
			{
				span2 = (array = ArrayPool<byte>.Shared.Rent(maxEscapedLength));
			}
			Span<byte> span3 = span2;
			int num;
			JsonWriterHelper.EscapeString(utf8Value, span3, firstEscapeIndex, this._options.Encoder, out num);
			this.WriteStringByOptions(escapedPropertyName, span3.Slice(0, num));
			if (array != null)
			{
				ArrayPool<byte>.Shared.Return(array, false);
			}
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0001D074 File Offset: 0x0001B274
		private unsafe void WriteStringEscapeValueOnly(ReadOnlySpan<byte> escapedPropertyName, ReadOnlySpan<char> value, int firstEscapeIndex)
		{
			char[] array = null;
			int maxEscapedLength = JsonWriterHelper.GetMaxEscapedLength(value.Length, firstEscapeIndex);
			Span<char> span2;
			if (maxEscapedLength <= 128)
			{
				Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)256], 128);
				span2 = span;
			}
			else
			{
				span2 = (array = ArrayPool<char>.Shared.Rent(maxEscapedLength));
			}
			Span<char> span3 = span2;
			int num;
			JsonWriterHelper.EscapeString(value, span3, firstEscapeIndex, this._options.Encoder, out num);
			this.WriteStringByOptions(escapedPropertyName, span3.Slice(0, num));
			if (array != null)
			{
				ArrayPool<char>.Shared.Return(array, false);
			}
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0001D104 File Offset: 0x0001B304
		private unsafe void WriteStringEscapePropertyOnly(ReadOnlySpan<char> propertyName, ReadOnlySpan<byte> escapedValue, int firstEscapeIndex)
		{
			char[] array = null;
			int maxEscapedLength = JsonWriterHelper.GetMaxEscapedLength(propertyName.Length, firstEscapeIndex);
			Span<char> span2;
			if (maxEscapedLength <= 128)
			{
				Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)256], 128);
				span2 = span;
			}
			else
			{
				span2 = (array = ArrayPool<char>.Shared.Rent(maxEscapedLength));
			}
			Span<char> span3 = span2;
			int num;
			JsonWriterHelper.EscapeString(propertyName, span3, firstEscapeIndex, this._options.Encoder, out num);
			this.WriteStringByOptions(span3.Slice(0, num), escapedValue);
			if (array != null)
			{
				ArrayPool<char>.Shared.Return(array, false);
			}
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0001D194 File Offset: 0x0001B394
		private unsafe void WriteStringEscapePropertyOnly(ReadOnlySpan<byte> utf8PropertyName, ReadOnlySpan<byte> escapedValue, int firstEscapeIndex)
		{
			byte[] array = null;
			int maxEscapedLength = JsonWriterHelper.GetMaxEscapedLength(utf8PropertyName.Length, firstEscapeIndex);
			Span<byte> span2;
			if (maxEscapedLength <= 256)
			{
				Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)256], 256);
				span2 = span;
			}
			else
			{
				span2 = (array = ArrayPool<byte>.Shared.Rent(maxEscapedLength));
			}
			Span<byte> span3 = span2;
			int num;
			JsonWriterHelper.EscapeString(utf8PropertyName, span3, firstEscapeIndex, this._options.Encoder, out num);
			this.WriteStringByOptions(span3.Slice(0, num), escapedValue);
			if (array != null)
			{
				ArrayPool<byte>.Shared.Return(array, false);
			}
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0001D224 File Offset: 0x0001B424
		private void WriteStringEscape(ReadOnlySpan<char> propertyName, ReadOnlySpan<char> value)
		{
			int num = JsonWriterHelper.NeedsEscaping(value, this._options.Encoder);
			int num2 = JsonWriterHelper.NeedsEscaping(propertyName, this._options.Encoder);
			if (num + num2 != -2)
			{
				this.WriteStringEscapePropertyOrValue(propertyName, value, num2, num);
				return;
			}
			this.WriteStringByOptions(propertyName, value);
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x0001D270 File Offset: 0x0001B470
		private void WriteStringEscape(ReadOnlySpan<byte> utf8PropertyName, ReadOnlySpan<byte> utf8Value)
		{
			int num = JsonWriterHelper.NeedsEscaping(utf8Value, this._options.Encoder);
			int num2 = JsonWriterHelper.NeedsEscaping(utf8PropertyName, this._options.Encoder);
			if (num + num2 != -2)
			{
				this.WriteStringEscapePropertyOrValue(utf8PropertyName, utf8Value, num2, num);
				return;
			}
			this.WriteStringByOptions(utf8PropertyName, utf8Value);
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0001D2BC File Offset: 0x0001B4BC
		private void WriteStringEscape(ReadOnlySpan<char> propertyName, ReadOnlySpan<byte> utf8Value)
		{
			int num = JsonWriterHelper.NeedsEscaping(utf8Value, this._options.Encoder);
			int num2 = JsonWriterHelper.NeedsEscaping(propertyName, this._options.Encoder);
			if (num + num2 != -2)
			{
				this.WriteStringEscapePropertyOrValue(propertyName, utf8Value, num2, num);
				return;
			}
			this.WriteStringByOptions(propertyName, utf8Value);
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0001D308 File Offset: 0x0001B508
		private void WriteStringEscape(ReadOnlySpan<byte> utf8PropertyName, ReadOnlySpan<char> value)
		{
			int num = JsonWriterHelper.NeedsEscaping(value, this._options.Encoder);
			int num2 = JsonWriterHelper.NeedsEscaping(utf8PropertyName, this._options.Encoder);
			if (num + num2 != -2)
			{
				this.WriteStringEscapePropertyOrValue(utf8PropertyName, value, num2, num);
				return;
			}
			this.WriteStringByOptions(utf8PropertyName, value);
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0001D354 File Offset: 0x0001B554
		private unsafe void WriteStringEscapePropertyOrValue([ScopedRef] ReadOnlySpan<char> propertyName, [ScopedRef] ReadOnlySpan<char> value, int firstEscapeIndexProp, int firstEscapeIndexVal)
		{
			char[] array = null;
			char[] array2 = null;
			if (firstEscapeIndexVal != -1)
			{
				int maxEscapedLength = JsonWriterHelper.GetMaxEscapedLength(value.Length, firstEscapeIndexVal);
				Span<char> span;
				if (maxEscapedLength > 128)
				{
					array = ArrayPool<char>.Shared.Rent(maxEscapedLength);
					span = array;
				}
				else
				{
					Span<char> span2 = new Span<char>(stackalloc byte[(UIntPtr)256], 128);
					span = span2;
				}
				int num;
				JsonWriterHelper.EscapeString(value, span, firstEscapeIndexVal, this._options.Encoder, out num);
				value = span.Slice(0, num);
			}
			if (firstEscapeIndexProp != -1)
			{
				int maxEscapedLength2 = JsonWriterHelper.GetMaxEscapedLength(propertyName.Length, firstEscapeIndexProp);
				Span<char> span3;
				if (maxEscapedLength2 > 128)
				{
					array2 = ArrayPool<char>.Shared.Rent(maxEscapedLength2);
					span3 = array2;
				}
				else
				{
					Span<char> span2 = new Span<char>(stackalloc byte[(UIntPtr)256], 128);
					span3 = span2;
				}
				int num2;
				JsonWriterHelper.EscapeString(propertyName, span3, firstEscapeIndexProp, this._options.Encoder, out num2);
				propertyName = span3.Slice(0, num2);
			}
			this.WriteStringByOptions(propertyName, value);
			if (array != null)
			{
				ArrayPool<char>.Shared.Return(array, false);
			}
			if (array2 != null)
			{
				ArrayPool<char>.Shared.Return(array2, false);
			}
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0001D46C File Offset: 0x0001B66C
		private unsafe void WriteStringEscapePropertyOrValue([ScopedRef] ReadOnlySpan<byte> utf8PropertyName, [ScopedRef] ReadOnlySpan<byte> utf8Value, int firstEscapeIndexProp, int firstEscapeIndexVal)
		{
			byte[] array = null;
			byte[] array2 = null;
			if (firstEscapeIndexVal != -1)
			{
				int maxEscapedLength = JsonWriterHelper.GetMaxEscapedLength(utf8Value.Length, firstEscapeIndexVal);
				Span<byte> span;
				if (maxEscapedLength > 256)
				{
					array = ArrayPool<byte>.Shared.Rent(maxEscapedLength);
					span = array;
				}
				else
				{
					Span<byte> span2 = new Span<byte>(stackalloc byte[(UIntPtr)256], 256);
					span = span2;
				}
				int num;
				JsonWriterHelper.EscapeString(utf8Value, span, firstEscapeIndexVal, this._options.Encoder, out num);
				utf8Value = span.Slice(0, num);
			}
			if (firstEscapeIndexProp != -1)
			{
				int maxEscapedLength2 = JsonWriterHelper.GetMaxEscapedLength(utf8PropertyName.Length, firstEscapeIndexProp);
				Span<byte> span3;
				if (maxEscapedLength2 > 256)
				{
					array2 = ArrayPool<byte>.Shared.Rent(maxEscapedLength2);
					span3 = array2;
				}
				else
				{
					Span<byte> span2 = new Span<byte>(stackalloc byte[(UIntPtr)256], 256);
					span3 = span2;
				}
				int num2;
				JsonWriterHelper.EscapeString(utf8PropertyName, span3, firstEscapeIndexProp, this._options.Encoder, out num2);
				utf8PropertyName = span3.Slice(0, num2);
			}
			this.WriteStringByOptions(utf8PropertyName, utf8Value);
			if (array != null)
			{
				ArrayPool<byte>.Shared.Return(array, false);
			}
			if (array2 != null)
			{
				ArrayPool<byte>.Shared.Return(array2, false);
			}
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x0001D584 File Offset: 0x0001B784
		private unsafe void WriteStringEscapePropertyOrValue([ScopedRef] ReadOnlySpan<char> propertyName, [ScopedRef] ReadOnlySpan<byte> utf8Value, int firstEscapeIndexProp, int firstEscapeIndexVal)
		{
			byte[] array = null;
			char[] array2 = null;
			if (firstEscapeIndexVal != -1)
			{
				int maxEscapedLength = JsonWriterHelper.GetMaxEscapedLength(utf8Value.Length, firstEscapeIndexVal);
				Span<byte> span;
				if (maxEscapedLength > 256)
				{
					array = ArrayPool<byte>.Shared.Rent(maxEscapedLength);
					span = array;
				}
				else
				{
					Span<byte> span2 = new Span<byte>(stackalloc byte[(UIntPtr)256], 256);
					span = span2;
				}
				int num;
				JsonWriterHelper.EscapeString(utf8Value, span, firstEscapeIndexVal, this._options.Encoder, out num);
				utf8Value = span.Slice(0, num);
			}
			if (firstEscapeIndexProp != -1)
			{
				int maxEscapedLength2 = JsonWriterHelper.GetMaxEscapedLength(propertyName.Length, firstEscapeIndexProp);
				Span<char> span3;
				if (maxEscapedLength2 > 128)
				{
					array2 = ArrayPool<char>.Shared.Rent(maxEscapedLength2);
					span3 = array2;
				}
				else
				{
					Span<char> span4 = new Span<char>(stackalloc byte[(UIntPtr)256], 128);
					span3 = span4;
				}
				int num2;
				JsonWriterHelper.EscapeString(propertyName, span3, firstEscapeIndexProp, this._options.Encoder, out num2);
				propertyName = span3.Slice(0, num2);
			}
			this.WriteStringByOptions(propertyName, utf8Value);
			if (array != null)
			{
				ArrayPool<byte>.Shared.Return(array, false);
			}
			if (array2 != null)
			{
				ArrayPool<char>.Shared.Return(array2, false);
			}
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0001D69C File Offset: 0x0001B89C
		private unsafe void WriteStringEscapePropertyOrValue([ScopedRef] ReadOnlySpan<byte> utf8PropertyName, [ScopedRef] ReadOnlySpan<char> value, int firstEscapeIndexProp, int firstEscapeIndexVal)
		{
			char[] array = null;
			byte[] array2 = null;
			if (firstEscapeIndexVal != -1)
			{
				int maxEscapedLength = JsonWriterHelper.GetMaxEscapedLength(value.Length, firstEscapeIndexVal);
				Span<char> span;
				if (maxEscapedLength > 128)
				{
					array = ArrayPool<char>.Shared.Rent(maxEscapedLength);
					span = array;
				}
				else
				{
					Span<char> span2 = new Span<char>(stackalloc byte[(UIntPtr)256], 128);
					span = span2;
				}
				int num;
				JsonWriterHelper.EscapeString(value, span, firstEscapeIndexVal, this._options.Encoder, out num);
				value = span.Slice(0, num);
			}
			if (firstEscapeIndexProp != -1)
			{
				int maxEscapedLength2 = JsonWriterHelper.GetMaxEscapedLength(utf8PropertyName.Length, firstEscapeIndexProp);
				Span<byte> span3;
				if (maxEscapedLength2 > 256)
				{
					array2 = ArrayPool<byte>.Shared.Rent(maxEscapedLength2);
					span3 = array2;
				}
				else
				{
					Span<byte> span4 = new Span<byte>(stackalloc byte[(UIntPtr)256], 256);
					span3 = span4;
				}
				int num2;
				JsonWriterHelper.EscapeString(utf8PropertyName, span3, firstEscapeIndexProp, this._options.Encoder, out num2);
				utf8PropertyName = span3.Slice(0, num2);
			}
			this.WriteStringByOptions(utf8PropertyName, value);
			if (array != null)
			{
				ArrayPool<char>.Shared.Return(array, false);
			}
			if (array2 != null)
			{
				ArrayPool<byte>.Shared.Return(array2, false);
			}
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x0001D7B4 File Offset: 0x0001B9B4
		private void WriteStringByOptions(ReadOnlySpan<char> propertyName, ReadOnlySpan<char> value)
		{
			this.ValidateWritingProperty();
			if (this._options.Indented)
			{
				this.WriteStringIndented(propertyName, value);
				return;
			}
			this.WriteStringMinimized(propertyName, value);
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x0001D7DA File Offset: 0x0001B9DA
		private void WriteStringByOptions(ReadOnlySpan<byte> utf8PropertyName, ReadOnlySpan<byte> utf8Value)
		{
			this.ValidateWritingProperty();
			if (this._options.Indented)
			{
				this.WriteStringIndented(utf8PropertyName, utf8Value);
				return;
			}
			this.WriteStringMinimized(utf8PropertyName, utf8Value);
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0001D800 File Offset: 0x0001BA00
		private void WriteStringByOptions(ReadOnlySpan<char> propertyName, ReadOnlySpan<byte> utf8Value)
		{
			this.ValidateWritingProperty();
			if (this._options.Indented)
			{
				this.WriteStringIndented(propertyName, utf8Value);
				return;
			}
			this.WriteStringMinimized(propertyName, utf8Value);
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0001D826 File Offset: 0x0001BA26
		private void WriteStringByOptions(ReadOnlySpan<byte> utf8PropertyName, ReadOnlySpan<char> value)
		{
			this.ValidateWritingProperty();
			if (this._options.Indented)
			{
				this.WriteStringIndented(utf8PropertyName, value);
				return;
			}
			this.WriteStringMinimized(utf8PropertyName, value);
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0001D84C File Offset: 0x0001BA4C
		private unsafe void WriteStringMinimized(ReadOnlySpan<char> escapedPropertyName, ReadOnlySpan<char> escapedValue)
		{
			int num = (escapedPropertyName.Length + escapedValue.Length) * 3 + 6;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			int num2;
			if (this._currentDepth < 0)
			{
				num2 = this.BytesPending;
				this.BytesPending = num2 + 1;
				*span[num2] = 44;
			}
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			this.TranscodeAndWrite(escapedPropertyName, span);
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 58;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			this.TranscodeAndWrite(escapedValue, span);
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0001D950 File Offset: 0x0001BB50
		private unsafe void WriteStringMinimized(ReadOnlySpan<byte> escapedPropertyName, ReadOnlySpan<byte> escapedValue)
		{
			int num = escapedPropertyName.Length + escapedValue.Length + 5;
			int num2 = num + 1;
			if (this._memory.Length - this.BytesPending < num2)
			{
				this.Grow(num2);
			}
			Span<byte> span = this._memory.Span;
			int num3;
			if (this._currentDepth < 0)
			{
				num3 = this.BytesPending;
				this.BytesPending = num3 + 1;
				*span[num3] = 44;
			}
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			escapedPropertyName.CopyTo(span.Slice(this.BytesPending));
			this.BytesPending += escapedPropertyName.Length;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 58;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			escapedValue.CopyTo(span.Slice(this.BytesPending));
			this.BytesPending += escapedValue.Length;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0001DA98 File Offset: 0x0001BC98
		private unsafe void WriteStringMinimized(ReadOnlySpan<char> escapedPropertyName, ReadOnlySpan<byte> escapedValue)
		{
			int num = escapedPropertyName.Length * 3 + escapedValue.Length + 6;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			int num2;
			if (this._currentDepth < 0)
			{
				num2 = this.BytesPending;
				this.BytesPending = num2 + 1;
				*span[num2] = 44;
			}
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			this.TranscodeAndWrite(escapedPropertyName, span);
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 58;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			escapedValue.CopyTo(span.Slice(this.BytesPending));
			this.BytesPending += escapedValue.Length;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0001DBBC File Offset: 0x0001BDBC
		private unsafe void WriteStringMinimized(ReadOnlySpan<byte> escapedPropertyName, ReadOnlySpan<char> escapedValue)
		{
			int num = escapedValue.Length * 3 + escapedPropertyName.Length + 6;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			int num2;
			if (this._currentDepth < 0)
			{
				num2 = this.BytesPending;
				this.BytesPending = num2 + 1;
				*span[num2] = 44;
			}
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			escapedPropertyName.CopyTo(span.Slice(this.BytesPending));
			this.BytesPending += escapedPropertyName.Length;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 58;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			this.TranscodeAndWrite(escapedValue, span);
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0001DCE0 File Offset: 0x0001BEE0
		private unsafe void WriteStringIndented(ReadOnlySpan<char> escapedPropertyName, ReadOnlySpan<char> escapedValue)
		{
			int indentation = this.Indentation;
			int num = indentation + (escapedPropertyName.Length + escapedValue.Length) * 3 + 7 + Utf8JsonWriter.s_newLineLength;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			int num2;
			if (this._currentDepth < 0)
			{
				num2 = this.BytesPending;
				this.BytesPending = num2 + 1;
				*span[num2] = 44;
			}
			if (this._tokenType != JsonTokenType.None)
			{
				this.WriteNewLine(span);
			}
			JsonWriterHelper.WriteIndentation(span.Slice(this.BytesPending), indentation);
			this.BytesPending += indentation;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			this.TranscodeAndWrite(escapedPropertyName, span);
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 58;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 32;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			this.TranscodeAndWrite(escapedValue, span);
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0001DE40 File Offset: 0x0001C040
		private unsafe void WriteStringIndented(ReadOnlySpan<byte> escapedPropertyName, ReadOnlySpan<byte> escapedValue)
		{
			int indentation = this.Indentation;
			int num = indentation + escapedPropertyName.Length + escapedValue.Length + 6;
			int num2 = num + 1 + Utf8JsonWriter.s_newLineLength;
			if (this._memory.Length - this.BytesPending < num2)
			{
				this.Grow(num2);
			}
			Span<byte> span = this._memory.Span;
			int num3;
			if (this._currentDepth < 0)
			{
				num3 = this.BytesPending;
				this.BytesPending = num3 + 1;
				*span[num3] = 44;
			}
			if (this._tokenType != JsonTokenType.None)
			{
				this.WriteNewLine(span);
			}
			JsonWriterHelper.WriteIndentation(span.Slice(this.BytesPending), indentation);
			this.BytesPending += indentation;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			escapedPropertyName.CopyTo(span.Slice(this.BytesPending));
			this.BytesPending += escapedPropertyName.Length;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 58;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 32;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			escapedValue.CopyTo(span.Slice(this.BytesPending));
			this.BytesPending += escapedValue.Length;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0001DFF8 File Offset: 0x0001C1F8
		private unsafe void WriteStringIndented(ReadOnlySpan<char> escapedPropertyName, ReadOnlySpan<byte> escapedValue)
		{
			int indentation = this.Indentation;
			int num = indentation + escapedPropertyName.Length * 3 + escapedValue.Length + 7 + Utf8JsonWriter.s_newLineLength;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			int num2;
			if (this._currentDepth < 0)
			{
				num2 = this.BytesPending;
				this.BytesPending = num2 + 1;
				*span[num2] = 44;
			}
			if (this._tokenType != JsonTokenType.None)
			{
				this.WriteNewLine(span);
			}
			JsonWriterHelper.WriteIndentation(span.Slice(this.BytesPending), indentation);
			this.BytesPending += indentation;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			this.TranscodeAndWrite(escapedPropertyName, span);
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 58;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 32;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			escapedValue.CopyTo(span.Slice(this.BytesPending));
			this.BytesPending += escapedValue.Length;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0001E178 File Offset: 0x0001C378
		private unsafe void WriteStringIndented(ReadOnlySpan<byte> escapedPropertyName, ReadOnlySpan<char> escapedValue)
		{
			int indentation = this.Indentation;
			int num = indentation + escapedValue.Length * 3 + escapedPropertyName.Length + 7 + Utf8JsonWriter.s_newLineLength;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			int num2;
			if (this._currentDepth < 0)
			{
				num2 = this.BytesPending;
				this.BytesPending = num2 + 1;
				*span[num2] = 44;
			}
			if (this._tokenType != JsonTokenType.None)
			{
				this.WriteNewLine(span);
			}
			JsonWriterHelper.WriteIndentation(span.Slice(this.BytesPending), indentation);
			this.BytesPending += indentation;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			escapedPropertyName.CopyTo(span.Slice(this.BytesPending));
			this.BytesPending += escapedPropertyName.Length;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 58;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 32;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			this.TranscodeAndWrite(escapedValue, span);
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0001E2F8 File Offset: 0x0001C4F8
		[CLSCompliant(false)]
		public void WriteNumber(JsonEncodedText propertyName, ulong value)
		{
			ReadOnlySpan<byte> encodedUtf8Bytes = propertyName.EncodedUtf8Bytes;
			this.WriteNumberByOptions(encodedUtf8Bytes, value);
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.Number;
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0001E322 File Offset: 0x0001C522
		[NullableContext(1)]
		[CLSCompliant(false)]
		public void WriteNumber(string propertyName, ulong value)
		{
			if (propertyName == null)
			{
				ThrowHelper.ThrowArgumentNullException("propertyName");
			}
			this.WriteNumber(propertyName.AsSpan(), value);
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0001E33E File Offset: 0x0001C53E
		[CLSCompliant(false)]
		public void WriteNumber(ReadOnlySpan<char> propertyName, ulong value)
		{
			JsonWriterHelper.ValidateProperty(propertyName);
			this.WriteNumberEscape(propertyName, value);
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.Number;
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0001E35B File Offset: 0x0001C55B
		[CLSCompliant(false)]
		public void WriteNumber(ReadOnlySpan<byte> utf8PropertyName, ulong value)
		{
			JsonWriterHelper.ValidateProperty(utf8PropertyName);
			this.WriteNumberEscape(utf8PropertyName, value);
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.Number;
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0001E378 File Offset: 0x0001C578
		[CLSCompliant(false)]
		public void WriteNumber(JsonEncodedText propertyName, uint value)
		{
			this.WriteNumber(propertyName, (ulong)value);
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x0001E383 File Offset: 0x0001C583
		[NullableContext(1)]
		[CLSCompliant(false)]
		public void WriteNumber(string propertyName, uint value)
		{
			if (propertyName == null)
			{
				ThrowHelper.ThrowArgumentNullException("propertyName");
			}
			this.WriteNumber(propertyName.AsSpan(), (ulong)value);
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0001E3A0 File Offset: 0x0001C5A0
		[CLSCompliant(false)]
		public void WriteNumber(ReadOnlySpan<char> propertyName, uint value)
		{
			this.WriteNumber(propertyName, (ulong)value);
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0001E3AB File Offset: 0x0001C5AB
		[CLSCompliant(false)]
		public void WriteNumber(ReadOnlySpan<byte> utf8PropertyName, uint value)
		{
			this.WriteNumber(utf8PropertyName, (ulong)value);
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0001E3B8 File Offset: 0x0001C5B8
		private void WriteNumberEscape(ReadOnlySpan<char> propertyName, ulong value)
		{
			int num = JsonWriterHelper.NeedsEscaping(propertyName, this._options.Encoder);
			if (num != -1)
			{
				this.WriteNumberEscapeProperty(propertyName, value, num);
				return;
			}
			this.WriteNumberByOptions(propertyName, value);
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0001E3F0 File Offset: 0x0001C5F0
		private void WriteNumberEscape(ReadOnlySpan<byte> utf8PropertyName, ulong value)
		{
			int num = JsonWriterHelper.NeedsEscaping(utf8PropertyName, this._options.Encoder);
			if (num != -1)
			{
				this.WriteNumberEscapeProperty(utf8PropertyName, value, num);
				return;
			}
			this.WriteNumberByOptions(utf8PropertyName, value);
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0001E428 File Offset: 0x0001C628
		private unsafe void WriteNumberEscapeProperty(ReadOnlySpan<char> propertyName, ulong value, int firstEscapeIndexProp)
		{
			char[] array = null;
			int maxEscapedLength = JsonWriterHelper.GetMaxEscapedLength(propertyName.Length, firstEscapeIndexProp);
			Span<char> span2;
			if (maxEscapedLength <= 128)
			{
				Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)256], 128);
				span2 = span;
			}
			else
			{
				span2 = (array = ArrayPool<char>.Shared.Rent(maxEscapedLength));
			}
			Span<char> span3 = span2;
			int num;
			JsonWriterHelper.EscapeString(propertyName, span3, firstEscapeIndexProp, this._options.Encoder, out num);
			this.WriteNumberByOptions(span3.Slice(0, num), value);
			if (array != null)
			{
				ArrayPool<char>.Shared.Return(array, false);
			}
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0001E4B8 File Offset: 0x0001C6B8
		private unsafe void WriteNumberEscapeProperty(ReadOnlySpan<byte> utf8PropertyName, ulong value, int firstEscapeIndexProp)
		{
			byte[] array = null;
			int maxEscapedLength = JsonWriterHelper.GetMaxEscapedLength(utf8PropertyName.Length, firstEscapeIndexProp);
			Span<byte> span2;
			if (maxEscapedLength <= 256)
			{
				Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)256], 256);
				span2 = span;
			}
			else
			{
				span2 = (array = ArrayPool<byte>.Shared.Rent(maxEscapedLength));
			}
			Span<byte> span3 = span2;
			int num;
			JsonWriterHelper.EscapeString(utf8PropertyName, span3, firstEscapeIndexProp, this._options.Encoder, out num);
			this.WriteNumberByOptions(span3.Slice(0, num), value);
			if (array != null)
			{
				ArrayPool<byte>.Shared.Return(array, false);
			}
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0001E547 File Offset: 0x0001C747
		private void WriteNumberByOptions(ReadOnlySpan<char> propertyName, ulong value)
		{
			this.ValidateWritingProperty();
			if (this._options.Indented)
			{
				this.WriteNumberIndented(propertyName, value);
				return;
			}
			this.WriteNumberMinimized(propertyName, value);
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0001E56D File Offset: 0x0001C76D
		private void WriteNumberByOptions(ReadOnlySpan<byte> utf8PropertyName, ulong value)
		{
			this.ValidateWritingProperty();
			if (this._options.Indented)
			{
				this.WriteNumberIndented(utf8PropertyName, value);
				return;
			}
			this.WriteNumberMinimized(utf8PropertyName, value);
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0001E594 File Offset: 0x0001C794
		private unsafe void WriteNumberMinimized(ReadOnlySpan<char> escapedPropertyName, ulong value)
		{
			int num = escapedPropertyName.Length * 3 + 20 + 4;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			int num2;
			if (this._currentDepth < 0)
			{
				num2 = this.BytesPending;
				this.BytesPending = num2 + 1;
				*span[num2] = 44;
			}
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			this.TranscodeAndWrite(escapedPropertyName, span);
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 58;
			int num3;
			bool flag = Utf8Formatter.TryFormat(value, span.Slice(this.BytesPending), out num3, default(StandardFormat));
			this.BytesPending += num3;
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0001E690 File Offset: 0x0001C890
		private unsafe void WriteNumberMinimized(ReadOnlySpan<byte> escapedPropertyName, ulong value)
		{
			int num = escapedPropertyName.Length + 20 + 3;
			int num2 = num + 1;
			if (this._memory.Length - this.BytesPending < num2)
			{
				this.Grow(num2);
			}
			Span<byte> span = this._memory.Span;
			int num3;
			if (this._currentDepth < 0)
			{
				num3 = this.BytesPending;
				this.BytesPending = num3 + 1;
				*span[num3] = 44;
			}
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			escapedPropertyName.CopyTo(span.Slice(this.BytesPending));
			this.BytesPending += escapedPropertyName.Length;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 58;
			int num4;
			bool flag = Utf8Formatter.TryFormat(value, span.Slice(this.BytesPending), out num4, default(StandardFormat));
			this.BytesPending += num4;
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0001E7B0 File Offset: 0x0001C9B0
		private unsafe void WriteNumberIndented(ReadOnlySpan<char> escapedPropertyName, ulong value)
		{
			int indentation = this.Indentation;
			int num = indentation + escapedPropertyName.Length * 3 + 20 + 5 + Utf8JsonWriter.s_newLineLength;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			int num2;
			if (this._currentDepth < 0)
			{
				num2 = this.BytesPending;
				this.BytesPending = num2 + 1;
				*span[num2] = 44;
			}
			if (this._tokenType != JsonTokenType.None)
			{
				this.WriteNewLine(span);
			}
			JsonWriterHelper.WriteIndentation(span.Slice(this.BytesPending), indentation);
			this.BytesPending += indentation;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			this.TranscodeAndWrite(escapedPropertyName, span);
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 58;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 32;
			int num3;
			bool flag = Utf8Formatter.TryFormat(value, span.Slice(this.BytesPending), out num3, default(StandardFormat));
			this.BytesPending += num3;
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0001E90C File Offset: 0x0001CB0C
		private unsafe void WriteNumberIndented(ReadOnlySpan<byte> escapedPropertyName, ulong value)
		{
			int indentation = this.Indentation;
			int num = indentation + escapedPropertyName.Length + 20 + 4;
			int num2 = num + 1 + Utf8JsonWriter.s_newLineLength;
			if (this._memory.Length - this.BytesPending < num2)
			{
				this.Grow(num2);
			}
			Span<byte> span = this._memory.Span;
			int num3;
			if (this._currentDepth < 0)
			{
				num3 = this.BytesPending;
				this.BytesPending = num3 + 1;
				*span[num3] = 44;
			}
			if (this._tokenType != JsonTokenType.None)
			{
				this.WriteNewLine(span);
			}
			JsonWriterHelper.WriteIndentation(span.Slice(this.BytesPending), indentation);
			this.BytesPending += indentation;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			escapedPropertyName.CopyTo(span.Slice(this.BytesPending));
			this.BytesPending += escapedPropertyName.Length;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 58;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 32;
			int num4;
			bool flag = Utf8Formatter.TryFormat(value, span.Slice(this.BytesPending), out num4, default(StandardFormat));
			this.BytesPending += num4;
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x0001EA88 File Offset: 0x0001CC88
		internal void WritePropertyName(uint value)
		{
			this.WritePropertyName((ulong)value);
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0001EA94 File Offset: 0x0001CC94
		internal unsafe void WritePropertyName(ulong value)
		{
			Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)20], 20);
			Span<byte> span2 = span;
			int num;
			bool flag = Utf8Formatter.TryFormat(value, span2, out num, default(StandardFormat));
			this.WritePropertyNameUnescaped(span2.Slice(0, num));
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0001EAD8 File Offset: 0x0001CCD8
		public void WriteBase64StringValue(ReadOnlySpan<byte> bytes)
		{
			this.WriteBase64ByOptions(bytes);
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.String;
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0001EAEE File Offset: 0x0001CCEE
		private void WriteBase64ByOptions(ReadOnlySpan<byte> bytes)
		{
			if (!this._options.SkipValidation)
			{
				this.ValidateWritingValue();
			}
			if (this._options.Indented)
			{
				this.WriteBase64Indented(bytes);
				return;
			}
			this.WriteBase64Minimized(bytes);
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0001EB20 File Offset: 0x0001CD20
		private unsafe void WriteBase64Minimized(ReadOnlySpan<byte> bytes)
		{
			if (bytes.Length > 1610612733)
			{
				ThrowHelper.ThrowArgumentException_ValueTooLarge((long)bytes.Length);
			}
			int maxEncodedToUtf8Length = Base64.GetMaxEncodedToUtf8Length(bytes.Length);
			int num = maxEncodedToUtf8Length + 3;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			int num2;
			if (this._currentDepth < 0)
			{
				num2 = this.BytesPending;
				this.BytesPending = num2 + 1;
				*span[num2] = 44;
			}
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			this.Base64EncodeAndWrite(bytes, span, maxEncodedToUtf8Length);
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0001EBE4 File Offset: 0x0001CDE4
		private unsafe void WriteBase64Indented(ReadOnlySpan<byte> bytes)
		{
			int indentation = this.Indentation;
			int num = indentation + 3 + Utf8JsonWriter.s_newLineLength;
			int num2 = 1610612733 - num;
			if (bytes.Length > num2)
			{
				ThrowHelper.ThrowArgumentException_ValueTooLarge((long)bytes.Length);
			}
			int maxEncodedToUtf8Length = Base64.GetMaxEncodedToUtf8Length(bytes.Length);
			int num3 = maxEncodedToUtf8Length + num;
			if (this._memory.Length - this.BytesPending < num3)
			{
				this.Grow(num3);
			}
			Span<byte> span = this._memory.Span;
			int num4;
			if (this._currentDepth < 0)
			{
				num4 = this.BytesPending;
				this.BytesPending = num4 + 1;
				*span[num4] = 44;
			}
			if (this._tokenType != JsonTokenType.PropertyName)
			{
				if (this._tokenType != JsonTokenType.None)
				{
					this.WriteNewLine(span);
				}
				JsonWriterHelper.WriteIndentation(span.Slice(this.BytesPending), indentation);
				this.BytesPending += indentation;
			}
			num4 = this.BytesPending;
			this.BytesPending = num4 + 1;
			*span[num4] = 34;
			this.Base64EncodeAndWrite(bytes, span, maxEncodedToUtf8Length);
			num4 = this.BytesPending;
			this.BytesPending = num4 + 1;
			*span[num4] = 34;
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x0001ED05 File Offset: 0x0001CF05
		private unsafe static ReadOnlySpan<byte> SingleLineCommentDelimiterUtf8
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.BCCD0BA7F848775347BA9B64603DDEAC80563D9BFC8FB67AF20A7FB468DF6FD4), 2);
			}
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0001ED12 File Offset: 0x0001CF12
		[NullableContext(1)]
		public void WriteCommentValue(string value)
		{
			if (value == null)
			{
				ThrowHelper.ThrowArgumentNullException("value");
			}
			this.WriteCommentValue(value.AsSpan());
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0001ED30 File Offset: 0x0001CF30
		public void WriteCommentValue(ReadOnlySpan<char> value)
		{
			JsonWriterHelper.ValidateValue(value);
			if (value.IndexOf(Utf8JsonWriter.s_singleLineCommentDelimiter) != -1)
			{
				ThrowHelper.ThrowArgumentException_InvalidCommentValue();
			}
			this.WriteCommentByOptions(value);
			JsonTokenType tokenType = this._tokenType;
			bool flag = tokenType == JsonTokenType.None || tokenType == JsonTokenType.PropertyName;
			if (flag)
			{
				this._commentAfterNoneOrPropertyName = true;
			}
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0001ED80 File Offset: 0x0001CF80
		private void WriteCommentByOptions(ReadOnlySpan<char> value)
		{
			if (this._options.Indented)
			{
				this.WriteCommentIndented(value);
				return;
			}
			this.WriteCommentMinimized(value);
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0001EDA0 File Offset: 0x0001CFA0
		private unsafe void WriteCommentMinimized(ReadOnlySpan<char> value)
		{
			int num = value.Length * 3 + 4;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			int num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 47;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 42;
			int num3;
			OperationStatus operationStatus = JsonWriterHelper.ToUtf8(value, span.Slice(this.BytesPending), out num3);
			if (operationStatus == OperationStatus.InvalidData)
			{
				ThrowHelper.ThrowArgumentException_InvalidUTF16((int)(*value[num3]));
			}
			this.BytesPending += num3;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 42;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 47;
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0001EE90 File Offset: 0x0001D090
		private unsafe void WriteCommentIndented(ReadOnlySpan<char> value)
		{
			int indentation = this.Indentation;
			int num = indentation + value.Length * 3 + 4 + Utf8JsonWriter.s_newLineLength;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			if (this._tokenType != JsonTokenType.None || this._commentAfterNoneOrPropertyName)
			{
				this.WriteNewLine(span);
				JsonWriterHelper.WriteIndentation(span.Slice(this.BytesPending), indentation);
				this.BytesPending += indentation;
			}
			int num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 47;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 42;
			int num3;
			OperationStatus operationStatus = JsonWriterHelper.ToUtf8(value, span.Slice(this.BytesPending), out num3);
			if (operationStatus == OperationStatus.InvalidData)
			{
				ThrowHelper.ThrowArgumentException_InvalidUTF16((int)(*value[num3]));
			}
			this.BytesPending += num3;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 42;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 47;
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0001EFC8 File Offset: 0x0001D1C8
		public void WriteCommentValue(ReadOnlySpan<byte> utf8Value)
		{
			JsonWriterHelper.ValidateValue(utf8Value);
			if (utf8Value.IndexOf(Utf8JsonWriter.SingleLineCommentDelimiterUtf8) != -1)
			{
				ThrowHelper.ThrowArgumentException_InvalidCommentValue();
			}
			if (!JsonWriterHelper.IsValidUtf8String(utf8Value))
			{
				ThrowHelper.ThrowArgumentException_InvalidUTF8(utf8Value);
			}
			this.WriteCommentByOptions(utf8Value);
			JsonTokenType tokenType = this._tokenType;
			bool flag = tokenType == JsonTokenType.None || tokenType == JsonTokenType.PropertyName;
			if (flag)
			{
				this._commentAfterNoneOrPropertyName = true;
			}
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0001F021 File Offset: 0x0001D221
		private void WriteCommentByOptions(ReadOnlySpan<byte> utf8Value)
		{
			if (this._options.Indented)
			{
				this.WriteCommentIndented(utf8Value);
				return;
			}
			this.WriteCommentMinimized(utf8Value);
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0001F040 File Offset: 0x0001D240
		private unsafe void WriteCommentMinimized(ReadOnlySpan<byte> utf8Value)
		{
			int num = utf8Value.Length + 4;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			int num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 47;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 42;
			utf8Value.CopyTo(span.Slice(this.BytesPending));
			this.BytesPending += utf8Value.Length;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 42;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 47;
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0001F114 File Offset: 0x0001D314
		private unsafe void WriteCommentIndented(ReadOnlySpan<byte> utf8Value)
		{
			int indentation = this.Indentation;
			int num = indentation + utf8Value.Length + 4;
			int num2 = num + Utf8JsonWriter.s_newLineLength;
			if (this._memory.Length - this.BytesPending < num2)
			{
				this.Grow(num2);
			}
			Span<byte> span = this._memory.Span;
			if (this._tokenType != JsonTokenType.None || this._commentAfterNoneOrPropertyName)
			{
				this.WriteNewLine(span);
				JsonWriterHelper.WriteIndentation(span.Slice(this.BytesPending), indentation);
				this.BytesPending += indentation;
			}
			int num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 47;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 42;
			utf8Value.CopyTo(span.Slice(this.BytesPending));
			this.BytesPending += utf8Value.Length;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 42;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 47;
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0001F23C File Offset: 0x0001D43C
		public void WriteStringValue(DateTime value)
		{
			if (!this._options.SkipValidation)
			{
				this.ValidateWritingValue();
			}
			if (this._options.Indented)
			{
				this.WriteStringValueIndented(value);
			}
			else
			{
				this.WriteStringValueMinimized(value);
			}
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.String;
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0001F27C File Offset: 0x0001D47C
		private unsafe void WriteStringValueMinimized(DateTime value)
		{
			int num = 36;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			int num2;
			if (this._currentDepth < 0)
			{
				num2 = this.BytesPending;
				this.BytesPending = num2 + 1;
				*span[num2] = 44;
			}
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			int num3;
			JsonWriterHelper.WriteDateTimeTrimmed(span.Slice(this.BytesPending), value, out num3);
			this.BytesPending += num3;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0001F334 File Offset: 0x0001D534
		private unsafe void WriteStringValueIndented(DateTime value)
		{
			int indentation = this.Indentation;
			int num = indentation + 33 + 3 + Utf8JsonWriter.s_newLineLength;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			int num2;
			if (this._currentDepth < 0)
			{
				num2 = this.BytesPending;
				this.BytesPending = num2 + 1;
				*span[num2] = 44;
			}
			if (this._tokenType != JsonTokenType.PropertyName)
			{
				if (this._tokenType != JsonTokenType.None)
				{
					this.WriteNewLine(span);
				}
				JsonWriterHelper.WriteIndentation(span.Slice(this.BytesPending), indentation);
				this.BytesPending += indentation;
			}
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			int num3;
			JsonWriterHelper.WriteDateTimeTrimmed(span.Slice(this.BytesPending), value, out num3);
			this.BytesPending += num3;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0001F43C File Offset: 0x0001D63C
		public void WriteStringValue(DateTimeOffset value)
		{
			if (!this._options.SkipValidation)
			{
				this.ValidateWritingValue();
			}
			if (this._options.Indented)
			{
				this.WriteStringValueIndented(value);
			}
			else
			{
				this.WriteStringValueMinimized(value);
			}
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.String;
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0001F47C File Offset: 0x0001D67C
		private unsafe void WriteStringValueMinimized(DateTimeOffset value)
		{
			int num = 36;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			int num2;
			if (this._currentDepth < 0)
			{
				num2 = this.BytesPending;
				this.BytesPending = num2 + 1;
				*span[num2] = 44;
			}
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			int num3;
			JsonWriterHelper.WriteDateTimeOffsetTrimmed(span.Slice(this.BytesPending), value, out num3);
			this.BytesPending += num3;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0001F534 File Offset: 0x0001D734
		private unsafe void WriteStringValueIndented(DateTimeOffset value)
		{
			int indentation = this.Indentation;
			int num = indentation + 33 + 3 + Utf8JsonWriter.s_newLineLength;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			int num2;
			if (this._currentDepth < 0)
			{
				num2 = this.BytesPending;
				this.BytesPending = num2 + 1;
				*span[num2] = 44;
			}
			if (this._tokenType != JsonTokenType.PropertyName)
			{
				if (this._tokenType != JsonTokenType.None)
				{
					this.WriteNewLine(span);
				}
				JsonWriterHelper.WriteIndentation(span.Slice(this.BytesPending), indentation);
				this.BytesPending += indentation;
			}
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			int num3;
			JsonWriterHelper.WriteDateTimeOffsetTrimmed(span.Slice(this.BytesPending), value, out num3);
			this.BytesPending += num3;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0001F63C File Offset: 0x0001D83C
		public void WriteNumberValue(decimal value)
		{
			if (!this._options.SkipValidation)
			{
				this.ValidateWritingValue();
			}
			if (this._options.Indented)
			{
				this.WriteNumberValueIndented(value);
			}
			else
			{
				this.WriteNumberValueMinimized(value);
			}
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.Number;
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0001F67C File Offset: 0x0001D87C
		private unsafe void WriteNumberValueMinimized(decimal value)
		{
			int num = 32;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			if (this._currentDepth < 0)
			{
				int bytesPending = this.BytesPending;
				this.BytesPending = bytesPending + 1;
				*span[bytesPending] = 44;
			}
			int num2;
			bool flag = Utf8Formatter.TryFormat(value, span.Slice(this.BytesPending), out num2, default(StandardFormat));
			this.BytesPending += num2;
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0001F70C File Offset: 0x0001D90C
		private unsafe void WriteNumberValueIndented(decimal value)
		{
			int indentation = this.Indentation;
			int num = indentation + 31 + 1 + Utf8JsonWriter.s_newLineLength;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			if (this._currentDepth < 0)
			{
				int bytesPending = this.BytesPending;
				this.BytesPending = bytesPending + 1;
				*span[bytesPending] = 44;
			}
			if (this._tokenType != JsonTokenType.PropertyName)
			{
				if (this._tokenType != JsonTokenType.None)
				{
					this.WriteNewLine(span);
				}
				JsonWriterHelper.WriteIndentation(span.Slice(this.BytesPending), indentation);
				this.BytesPending += indentation;
			}
			int num2;
			bool flag = Utf8Formatter.TryFormat(value, span.Slice(this.BytesPending), out num2, default(StandardFormat));
			this.BytesPending += num2;
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0001F7E4 File Offset: 0x0001D9E4
		internal unsafe void WriteNumberValueAsString(decimal value)
		{
			Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)31], 31);
			Span<byte> span2 = span;
			int num;
			bool flag = Utf8Formatter.TryFormat(value, span2, out num, default(StandardFormat));
			this.WriteNumberValueAsStringUnescaped(span2.Slice(0, num));
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0001F828 File Offset: 0x0001DA28
		public void WriteNumberValue(double value)
		{
			JsonWriterHelper.ValidateDouble(value);
			if (!this._options.SkipValidation)
			{
				this.ValidateWritingValue();
			}
			if (this._options.Indented)
			{
				this.WriteNumberValueIndented(value);
			}
			else
			{
				this.WriteNumberValueMinimized(value);
			}
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.Number;
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x0001F878 File Offset: 0x0001DA78
		private unsafe void WriteNumberValueMinimized(double value)
		{
			int num = 129;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			if (this._currentDepth < 0)
			{
				int bytesPending = this.BytesPending;
				this.BytesPending = bytesPending + 1;
				*span[bytesPending] = 44;
			}
			int num2;
			bool flag = Utf8JsonWriter.TryFormatDouble(value, span.Slice(this.BytesPending), out num2);
			this.BytesPending += num2;
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0001F900 File Offset: 0x0001DB00
		private unsafe void WriteNumberValueIndented(double value)
		{
			int indentation = this.Indentation;
			int num = indentation + 128 + 1 + Utf8JsonWriter.s_newLineLength;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			if (this._currentDepth < 0)
			{
				int bytesPending = this.BytesPending;
				this.BytesPending = bytesPending + 1;
				*span[bytesPending] = 44;
			}
			if (this._tokenType != JsonTokenType.PropertyName)
			{
				if (this._tokenType != JsonTokenType.None)
				{
					this.WriteNewLine(span);
				}
				JsonWriterHelper.WriteIndentation(span.Slice(this.BytesPending), indentation);
				this.BytesPending += indentation;
			}
			int num2;
			bool flag = Utf8JsonWriter.TryFormatDouble(value, span.Slice(this.BytesPending), out num2);
			this.BytesPending += num2;
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0001F9D4 File Offset: 0x0001DBD4
		private static bool TryFormatDouble(double value, Span<byte> destination, out int bytesWritten)
		{
			string text = value.ToString("G17", CultureInfo.InvariantCulture);
			if (text.Length > destination.Length)
			{
				bytesWritten = 0;
				return false;
			}
			bool flag;
			try
			{
				byte[] bytes = Encoding.UTF8.GetBytes(text);
				if (bytes.Length > destination.Length)
				{
					bytesWritten = 0;
					flag = false;
				}
				else
				{
					bytes.CopyTo(destination);
					bytesWritten = bytes.Length;
					flag = true;
				}
			}
			catch
			{
				bytesWritten = 0;
				flag = false;
			}
			return flag;
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0001FA50 File Offset: 0x0001DC50
		internal unsafe void WriteNumberValueAsString(double value)
		{
			Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)128], 128);
			Span<byte> span2 = span;
			int num;
			bool flag = Utf8JsonWriter.TryFormatDouble(value, span2, out num);
			this.WriteNumberValueAsStringUnescaped(span2.Slice(0, num));
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0001FA90 File Offset: 0x0001DC90
		internal void WriteFloatingPointConstant(double value)
		{
			if (double.IsNaN(value))
			{
				this.WriteNumberValueAsStringUnescaped(JsonConstants.NaNValue);
				return;
			}
			if (double.IsPositiveInfinity(value))
			{
				this.WriteNumberValueAsStringUnescaped(JsonConstants.PositiveInfinityValue);
				return;
			}
			if (double.IsNegativeInfinity(value))
			{
				this.WriteNumberValueAsStringUnescaped(JsonConstants.NegativeInfinityValue);
				return;
			}
			this.WriteNumberValue(value);
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0001FAE0 File Offset: 0x0001DCE0
		public void WriteNumberValue(float value)
		{
			JsonWriterHelper.ValidateSingle(value);
			if (!this._options.SkipValidation)
			{
				this.ValidateWritingValue();
			}
			if (this._options.Indented)
			{
				this.WriteNumberValueIndented(value);
			}
			else
			{
				this.WriteNumberValueMinimized(value);
			}
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.Number;
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0001FB30 File Offset: 0x0001DD30
		private unsafe void WriteNumberValueMinimized(float value)
		{
			int num = 129;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			if (this._currentDepth < 0)
			{
				int bytesPending = this.BytesPending;
				this.BytesPending = bytesPending + 1;
				*span[bytesPending] = 44;
			}
			int num2;
			bool flag = Utf8JsonWriter.TryFormatSingle(value, span.Slice(this.BytesPending), out num2);
			this.BytesPending += num2;
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0001FBB8 File Offset: 0x0001DDB8
		private unsafe void WriteNumberValueIndented(float value)
		{
			int indentation = this.Indentation;
			int num = indentation + 128 + 1 + Utf8JsonWriter.s_newLineLength;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			if (this._currentDepth < 0)
			{
				int bytesPending = this.BytesPending;
				this.BytesPending = bytesPending + 1;
				*span[bytesPending] = 44;
			}
			if (this._tokenType != JsonTokenType.PropertyName)
			{
				if (this._tokenType != JsonTokenType.None)
				{
					this.WriteNewLine(span);
				}
				JsonWriterHelper.WriteIndentation(span.Slice(this.BytesPending), indentation);
				this.BytesPending += indentation;
			}
			int num2;
			bool flag = Utf8JsonWriter.TryFormatSingle(value, span.Slice(this.BytesPending), out num2);
			this.BytesPending += num2;
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x0001FC8C File Offset: 0x0001DE8C
		private static bool TryFormatSingle(float value, Span<byte> destination, out int bytesWritten)
		{
			string text = value.ToString("G9", CultureInfo.InvariantCulture);
			if (text.Length > destination.Length)
			{
				bytesWritten = 0;
				return false;
			}
			bool flag;
			try
			{
				byte[] bytes = Encoding.UTF8.GetBytes(text);
				if (bytes.Length > destination.Length)
				{
					bytesWritten = 0;
					flag = false;
				}
				else
				{
					bytes.CopyTo(destination);
					bytesWritten = bytes.Length;
					flag = true;
				}
			}
			catch
			{
				bytesWritten = 0;
				flag = false;
			}
			return flag;
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0001FD08 File Offset: 0x0001DF08
		internal unsafe void WriteNumberValueAsString(float value)
		{
			Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)128], 128);
			Span<byte> span2 = span;
			int num;
			bool flag = Utf8JsonWriter.TryFormatSingle(value, span2, out num);
			this.WriteNumberValueAsStringUnescaped(span2.Slice(0, num));
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x0001FD48 File Offset: 0x0001DF48
		internal void WriteFloatingPointConstant(float value)
		{
			if (float.IsNaN(value))
			{
				this.WriteNumberValueAsStringUnescaped(JsonConstants.NaNValue);
				return;
			}
			if (float.IsPositiveInfinity(value))
			{
				this.WriteNumberValueAsStringUnescaped(JsonConstants.PositiveInfinityValue);
				return;
			}
			if (float.IsNegativeInfinity(value))
			{
				this.WriteNumberValueAsStringUnescaped(JsonConstants.NegativeInfinityValue);
				return;
			}
			this.WriteNumberValue(value);
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x0001FD98 File Offset: 0x0001DF98
		internal void WriteNumberValue(ReadOnlySpan<byte> utf8FormattedNumber)
		{
			JsonWriterHelper.ValidateValue(utf8FormattedNumber);
			JsonWriterHelper.ValidateNumber(utf8FormattedNumber);
			if (!this._options.SkipValidation)
			{
				this.ValidateWritingValue();
			}
			if (this._options.Indented)
			{
				this.WriteNumberValueIndented(utf8FormattedNumber);
			}
			else
			{
				this.WriteNumberValueMinimized(utf8FormattedNumber);
			}
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.Number;
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0001FDF0 File Offset: 0x0001DFF0
		private unsafe void WriteNumberValueMinimized(ReadOnlySpan<byte> utf8Value)
		{
			int num = utf8Value.Length + 1;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			if (this._currentDepth < 0)
			{
				int bytesPending = this.BytesPending;
				this.BytesPending = bytesPending + 1;
				*span[bytesPending] = 44;
			}
			utf8Value.CopyTo(span.Slice(this.BytesPending));
			this.BytesPending += utf8Value.Length;
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0001FE7C File Offset: 0x0001E07C
		private unsafe void WriteNumberValueIndented(ReadOnlySpan<byte> utf8Value)
		{
			int indentation = this.Indentation;
			int num = indentation + utf8Value.Length + 1 + Utf8JsonWriter.s_newLineLength;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			if (this._currentDepth < 0)
			{
				int bytesPending = this.BytesPending;
				this.BytesPending = bytesPending + 1;
				*span[bytesPending] = 44;
			}
			if (this._tokenType != JsonTokenType.PropertyName)
			{
				if (this._tokenType != JsonTokenType.None)
				{
					this.WriteNewLine(span);
				}
				JsonWriterHelper.WriteIndentation(span.Slice(this.BytesPending), indentation);
				this.BytesPending += indentation;
			}
			utf8Value.CopyTo(span.Slice(this.BytesPending));
			this.BytesPending += utf8Value.Length;
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0001FF4F File Offset: 0x0001E14F
		public void WriteStringValue(Guid value)
		{
			if (!this._options.SkipValidation)
			{
				this.ValidateWritingValue();
			}
			if (this._options.Indented)
			{
				this.WriteStringValueIndented(value);
			}
			else
			{
				this.WriteStringValueMinimized(value);
			}
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.String;
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0001FF90 File Offset: 0x0001E190
		private unsafe void WriteStringValueMinimized(Guid value)
		{
			int num = 39;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			int num2;
			if (this._currentDepth < 0)
			{
				num2 = this.BytesPending;
				this.BytesPending = num2 + 1;
				*span[num2] = 44;
			}
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			int num3;
			bool flag = Utf8Formatter.TryFormat(value, span.Slice(this.BytesPending), out num3, default(StandardFormat));
			this.BytesPending += num3;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0002005C File Offset: 0x0001E25C
		private unsafe void WriteStringValueIndented(Guid value)
		{
			int indentation = this.Indentation;
			int num = indentation + 36 + 3 + Utf8JsonWriter.s_newLineLength;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			int num2;
			if (this._currentDepth < 0)
			{
				num2 = this.BytesPending;
				this.BytesPending = num2 + 1;
				*span[num2] = 44;
			}
			if (this._tokenType != JsonTokenType.PropertyName)
			{
				if (this._tokenType != JsonTokenType.None)
				{
					this.WriteNewLine(span);
				}
				JsonWriterHelper.WriteIndentation(span.Slice(this.BytesPending), indentation);
				this.BytesPending += indentation;
			}
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			int num3;
			bool flag = Utf8Formatter.TryFormat(value, span.Slice(this.BytesPending), out num3, default(StandardFormat));
			this.BytesPending += num3;
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x00020170 File Offset: 0x0001E370
		private void ValidateWritingValue()
		{
			if (this._inObject)
			{
				if (this._tokenType != JsonTokenType.PropertyName)
				{
					ThrowHelper.ThrowInvalidOperationException(ExceptionResource.CannotWriteValueWithinObject, 0, this._options.MaxDepth, 0, this._tokenType);
					return;
				}
			}
			else if (this.CurrentDepth == 0 && this._tokenType != JsonTokenType.None)
			{
				ThrowHelper.ThrowInvalidOperationException(ExceptionResource.CannotWriteValueAfterPrimitiveOrClose, 0, this._options.MaxDepth, 0, this._tokenType);
			}
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x000201D4 File Offset: 0x0001E3D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe void Base64EncodeAndWrite(ReadOnlySpan<byte> bytes, Span<byte> output, int encodingLength)
		{
			byte[] array = null;
			Span<byte> span2;
			if (encodingLength <= 256)
			{
				Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)256], 256);
				span2 = span;
			}
			else
			{
				span2 = (array = ArrayPool<byte>.Shared.Rent(encodingLength));
			}
			Span<byte> span3 = span2;
			int num;
			int num2;
			OperationStatus operationStatus = Base64.EncodeToUtf8(bytes, span3, out num, out num2, true);
			span3 = span3.Slice(0, num2);
			Span<byte> span4 = output.Slice(this.BytesPending);
			span3.Slice(0, num2).CopyTo(span4);
			this.BytesPending += num2;
			if (array != null)
			{
				ArrayPool<byte>.Shared.Return(array, false);
			}
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x00020276 File Offset: 0x0001E476
		public void WriteNullValue()
		{
			this.WriteLiteralByOptions(JsonConstants.NullValue);
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.Null;
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x00020291 File Offset: 0x0001E491
		public void WriteBooleanValue(bool value)
		{
			if (value)
			{
				this.WriteLiteralByOptions(JsonConstants.TrueValue);
				this._tokenType = JsonTokenType.True;
			}
			else
			{
				this.WriteLiteralByOptions(JsonConstants.FalseValue);
				this._tokenType = JsonTokenType.False;
			}
			this.SetFlagToAddListSeparatorBeforeNextItem();
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x000202C4 File Offset: 0x0001E4C4
		private void WriteLiteralByOptions(ReadOnlySpan<byte> utf8Value)
		{
			if (!this._options.SkipValidation)
			{
				this.ValidateWritingValue();
			}
			if (this._options.Indented)
			{
				this.WriteLiteralIndented(utf8Value);
				return;
			}
			this.WriteLiteralMinimized(utf8Value);
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x000202F8 File Offset: 0x0001E4F8
		private unsafe void WriteLiteralMinimized(ReadOnlySpan<byte> utf8Value)
		{
			int num = utf8Value.Length + 1;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			if (this._currentDepth < 0)
			{
				int bytesPending = this.BytesPending;
				this.BytesPending = bytesPending + 1;
				*span[bytesPending] = 44;
			}
			utf8Value.CopyTo(span.Slice(this.BytesPending));
			this.BytesPending += utf8Value.Length;
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x00020384 File Offset: 0x0001E584
		private unsafe void WriteLiteralIndented(ReadOnlySpan<byte> utf8Value)
		{
			int indentation = this.Indentation;
			int num = indentation + utf8Value.Length + 1 + Utf8JsonWriter.s_newLineLength;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			if (this._currentDepth < 0)
			{
				int bytesPending = this.BytesPending;
				this.BytesPending = bytesPending + 1;
				*span[bytesPending] = 44;
			}
			if (this._tokenType != JsonTokenType.PropertyName)
			{
				if (this._tokenType != JsonTokenType.None)
				{
					this.WriteNewLine(span);
				}
				JsonWriterHelper.WriteIndentation(span.Slice(this.BytesPending), indentation);
				this.BytesPending += indentation;
			}
			utf8Value.CopyTo(span.Slice(this.BytesPending));
			this.BytesPending += utf8Value.Length;
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x00020457 File Offset: 0x0001E657
		[NullableContext(1)]
		public void WriteRawValue([StringSyntax("Json")] string json, bool skipInputValidation = false)
		{
			if (!this._options.SkipValidation)
			{
				this.ValidateWritingValue();
			}
			if (json == null)
			{
				throw new ArgumentNullException("json");
			}
			this.TranscodeAndWriteRawValue(json.AsSpan(), skipInputValidation);
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x00020487 File Offset: 0x0001E687
		public void WriteRawValue([StringSyntax("Json")] ReadOnlySpan<char> json, bool skipInputValidation = false)
		{
			if (!this._options.SkipValidation)
			{
				this.ValidateWritingValue();
			}
			this.TranscodeAndWriteRawValue(json, skipInputValidation);
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x000204A4 File Offset: 0x0001E6A4
		public void WriteRawValue(ReadOnlySpan<byte> utf8Json, bool skipInputValidation = false)
		{
			if (!this._options.SkipValidation)
			{
				this.ValidateWritingValue();
			}
			if (utf8Json.Length == 2147483647)
			{
				ThrowHelper.ThrowArgumentException_ValueTooLarge(2147483647L);
			}
			this.WriteRawValueCore(utf8Json, skipInputValidation);
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x000204DC File Offset: 0x0001E6DC
		public unsafe void WriteRawValue(ReadOnlySequence<byte> utf8Json, bool skipInputValidation = false)
		{
			if (!this._options.SkipValidation)
			{
				this.ValidateWritingValue();
			}
			long length = utf8Json.Length;
			if (length == 0L)
			{
				ThrowHelper.ThrowArgumentException(SR.ExpectedJsonTokens);
			}
			if (length >= 2147483647L)
			{
				ThrowHelper.ThrowArgumentException_ValueTooLarge(length);
			}
			if (skipInputValidation)
			{
				this._tokenType = JsonTokenType.String;
			}
			else
			{
				Utf8JsonReader utf8JsonReader = new Utf8JsonReader(utf8Json, default(JsonReaderOptions));
				while (utf8JsonReader.Read())
				{
				}
				this._tokenType = utf8JsonReader.TokenType;
			}
			int num = (int)length;
			int num2 = num + 1;
			if (this._memory.Length - this.BytesPending < num2)
			{
				this.Grow(num2);
			}
			Span<byte> span = this._memory.Span;
			if (this._currentDepth < 0)
			{
				int bytesPending = this.BytesPending;
				this.BytesPending = bytesPending + 1;
				*span[bytesPending] = 44;
			}
			(in utf8Json).CopyTo(span.Slice(this.BytesPending));
			this.BytesPending += num;
			this.SetFlagToAddListSeparatorBeforeNextItem();
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x000205D4 File Offset: 0x0001E7D4
		private void TranscodeAndWriteRawValue(ReadOnlySpan<char> json, bool skipInputValidation)
		{
			if (json.Length > 715827882)
			{
				ThrowHelper.ThrowArgumentException_ValueTooLarge((long)json.Length);
			}
			byte[] array = null;
			Span<byte> span = (((long)json.Length <= 349525L) ? (array = ArrayPool<byte>.Shared.Rent(json.Length * 3)) : new byte[JsonReaderHelper.GetUtf8ByteCount(json)]);
			try
			{
				int utf8FromText = JsonReaderHelper.GetUtf8FromText(json, span);
				span = span.Slice(0, utf8FromText);
				this.WriteRawValueCore(span, skipInputValidation);
			}
			finally
			{
				if (array != null)
				{
					span.Clear();
					ArrayPool<byte>.Shared.Return(array, false);
				}
			}
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x00020680 File Offset: 0x0001E880
		private unsafe void WriteRawValueCore(ReadOnlySpan<byte> utf8Json, bool skipInputValidation)
		{
			int length = utf8Json.Length;
			if (length == 0)
			{
				ThrowHelper.ThrowArgumentException(SR.ExpectedJsonTokens);
			}
			if (skipInputValidation)
			{
				this._tokenType = JsonTokenType.String;
			}
			else
			{
				Utf8JsonReader utf8JsonReader = new Utf8JsonReader(utf8Json, default(JsonReaderOptions));
				while (utf8JsonReader.Read())
				{
				}
				this._tokenType = utf8JsonReader.TokenType;
			}
			int num = length + 1;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			if (this._currentDepth < 0)
			{
				int bytesPending = this.BytesPending;
				this.BytesPending = bytesPending + 1;
				*span[bytesPending] = 44;
			}
			utf8Json.CopyTo(span.Slice(this.BytesPending));
			this.BytesPending += length;
			this.SetFlagToAddListSeparatorBeforeNextItem();
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x00020751 File Offset: 0x0001E951
		public void WriteNumberValue(int value)
		{
			this.WriteNumberValue((long)value);
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0002075B File Offset: 0x0001E95B
		public void WriteNumberValue(long value)
		{
			if (!this._options.SkipValidation)
			{
				this.ValidateWritingValue();
			}
			if (this._options.Indented)
			{
				this.WriteNumberValueIndented(value);
			}
			else
			{
				this.WriteNumberValueMinimized(value);
			}
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.Number;
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0002079C File Offset: 0x0001E99C
		private unsafe void WriteNumberValueMinimized(long value)
		{
			int num = 21;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			if (this._currentDepth < 0)
			{
				int bytesPending = this.BytesPending;
				this.BytesPending = bytesPending + 1;
				*span[bytesPending] = 44;
			}
			int num2;
			bool flag = Utf8Formatter.TryFormat(value, span.Slice(this.BytesPending), out num2, default(StandardFormat));
			this.BytesPending += num2;
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x0002082C File Offset: 0x0001EA2C
		private unsafe void WriteNumberValueIndented(long value)
		{
			int indentation = this.Indentation;
			int num = indentation + 20 + 1 + Utf8JsonWriter.s_newLineLength;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			if (this._currentDepth < 0)
			{
				int bytesPending = this.BytesPending;
				this.BytesPending = bytesPending + 1;
				*span[bytesPending] = 44;
			}
			if (this._tokenType != JsonTokenType.PropertyName)
			{
				if (this._tokenType != JsonTokenType.None)
				{
					this.WriteNewLine(span);
				}
				JsonWriterHelper.WriteIndentation(span.Slice(this.BytesPending), indentation);
				this.BytesPending += indentation;
			}
			int num2;
			bool flag = Utf8Formatter.TryFormat(value, span.Slice(this.BytesPending), out num2, default(StandardFormat));
			this.BytesPending += num2;
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00020904 File Offset: 0x0001EB04
		internal unsafe void WriteNumberValueAsString(long value)
		{
			Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)20], 20);
			Span<byte> span2 = span;
			int num;
			bool flag = Utf8Formatter.TryFormat(value, span2, out num, default(StandardFormat));
			this.WriteNumberValueAsStringUnescaped(span2.Slice(0, num));
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x00020948 File Offset: 0x0001EB48
		public void WriteStringValue(JsonEncodedText value)
		{
			ReadOnlySpan<byte> encodedUtf8Bytes = value.EncodedUtf8Bytes;
			this.WriteStringByOptions(encodedUtf8Bytes);
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.String;
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x00020971 File Offset: 0x0001EB71
		[NullableContext(2)]
		public void WriteStringValue(string value)
		{
			if (value == null)
			{
				this.WriteNullValue();
				return;
			}
			this.WriteStringValue(value.AsSpan());
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x00020989 File Offset: 0x0001EB89
		public void WriteStringValue(ReadOnlySpan<char> value)
		{
			JsonWriterHelper.ValidateValue(value);
			this.WriteStringEscape(value);
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.String;
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x000209A8 File Offset: 0x0001EBA8
		private void WriteStringEscape(ReadOnlySpan<char> value)
		{
			int num = JsonWriterHelper.NeedsEscaping(value, this._options.Encoder);
			if (num != -1)
			{
				this.WriteStringEscapeValue(value, num);
				return;
			}
			this.WriteStringByOptions(value);
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x000209DB File Offset: 0x0001EBDB
		private void WriteStringByOptions(ReadOnlySpan<char> value)
		{
			if (!this._options.SkipValidation)
			{
				this.ValidateWritingValue();
			}
			if (this._options.Indented)
			{
				this.WriteStringIndented(value);
				return;
			}
			this.WriteStringMinimized(value);
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00020A0C File Offset: 0x0001EC0C
		private unsafe void WriteStringMinimized(ReadOnlySpan<char> escapedValue)
		{
			int num = escapedValue.Length * 3 + 3;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			int num2;
			if (this._currentDepth < 0)
			{
				num2 = this.BytesPending;
				this.BytesPending = num2 + 1;
				*span[num2] = 44;
			}
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			this.TranscodeAndWrite(escapedValue, span);
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x00020AB0 File Offset: 0x0001ECB0
		private unsafe void WriteStringIndented(ReadOnlySpan<char> escapedValue)
		{
			int indentation = this.Indentation;
			int num = indentation + escapedValue.Length * 3 + 3 + Utf8JsonWriter.s_newLineLength;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			int num2;
			if (this._currentDepth < 0)
			{
				num2 = this.BytesPending;
				this.BytesPending = num2 + 1;
				*span[num2] = 44;
			}
			if (this._tokenType != JsonTokenType.PropertyName)
			{
				if (this._tokenType != JsonTokenType.None)
				{
					this.WriteNewLine(span);
				}
				JsonWriterHelper.WriteIndentation(span.Slice(this.BytesPending), indentation);
				this.BytesPending += indentation;
			}
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
			this.TranscodeAndWrite(escapedValue, span);
			num2 = this.BytesPending;
			this.BytesPending = num2 + 1;
			*span[num2] = 34;
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x00020B9C File Offset: 0x0001ED9C
		private unsafe void WriteStringEscapeValue(ReadOnlySpan<char> value, int firstEscapeIndexVal)
		{
			char[] array = null;
			int maxEscapedLength = JsonWriterHelper.GetMaxEscapedLength(value.Length, firstEscapeIndexVal);
			Span<char> span2;
			if (maxEscapedLength <= 128)
			{
				Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)256], 128);
				span2 = span;
			}
			else
			{
				span2 = (array = ArrayPool<char>.Shared.Rent(maxEscapedLength));
			}
			Span<char> span3 = span2;
			int num;
			JsonWriterHelper.EscapeString(value, span3, firstEscapeIndexVal, this._options.Encoder, out num);
			this.WriteStringByOptions(span3.Slice(0, num));
			if (array != null)
			{
				ArrayPool<char>.Shared.Return(array, false);
			}
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x00020C2A File Offset: 0x0001EE2A
		public void WriteStringValue(ReadOnlySpan<byte> utf8Value)
		{
			JsonWriterHelper.ValidateValue(utf8Value);
			this.WriteStringEscape(utf8Value);
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.String;
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x00020C48 File Offset: 0x0001EE48
		private void WriteStringEscape(ReadOnlySpan<byte> utf8Value)
		{
			int num = JsonWriterHelper.NeedsEscaping(utf8Value, this._options.Encoder);
			if (num != -1)
			{
				this.WriteStringEscapeValue(utf8Value, num);
				return;
			}
			this.WriteStringByOptions(utf8Value);
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x00020C7B File Offset: 0x0001EE7B
		private void WriteStringByOptions(ReadOnlySpan<byte> utf8Value)
		{
			if (!this._options.SkipValidation)
			{
				this.ValidateWritingValue();
			}
			if (this._options.Indented)
			{
				this.WriteStringIndented(utf8Value);
				return;
			}
			this.WriteStringMinimized(utf8Value);
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x00020CAC File Offset: 0x0001EEAC
		private unsafe void WriteStringMinimized(ReadOnlySpan<byte> escapedValue)
		{
			int num = escapedValue.Length + 2;
			int num2 = num + 1;
			if (this._memory.Length - this.BytesPending < num2)
			{
				this.Grow(num2);
			}
			Span<byte> span = this._memory.Span;
			int num3;
			if (this._currentDepth < 0)
			{
				num3 = this.BytesPending;
				this.BytesPending = num3 + 1;
				*span[num3] = 44;
			}
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			escapedValue.CopyTo(span.Slice(this.BytesPending));
			this.BytesPending += escapedValue.Length;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x00020D74 File Offset: 0x0001EF74
		private unsafe void WriteStringIndented(ReadOnlySpan<byte> escapedValue)
		{
			int indentation = this.Indentation;
			int num = indentation + escapedValue.Length + 2;
			int num2 = num + 1 + Utf8JsonWriter.s_newLineLength;
			if (this._memory.Length - this.BytesPending < num2)
			{
				this.Grow(num2);
			}
			Span<byte> span = this._memory.Span;
			int num3;
			if (this._currentDepth < 0)
			{
				num3 = this.BytesPending;
				this.BytesPending = num3 + 1;
				*span[num3] = 44;
			}
			if (this._tokenType != JsonTokenType.PropertyName)
			{
				if (this._tokenType != JsonTokenType.None)
				{
					this.WriteNewLine(span);
				}
				JsonWriterHelper.WriteIndentation(span.Slice(this.BytesPending), indentation);
				this.BytesPending += indentation;
			}
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
			escapedValue.CopyTo(span.Slice(this.BytesPending));
			this.BytesPending += escapedValue.Length;
			num3 = this.BytesPending;
			this.BytesPending = num3 + 1;
			*span[num3] = 34;
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00020E8C File Offset: 0x0001F08C
		private unsafe void WriteStringEscapeValue(ReadOnlySpan<byte> utf8Value, int firstEscapeIndexVal)
		{
			byte[] array = null;
			int maxEscapedLength = JsonWriterHelper.GetMaxEscapedLength(utf8Value.Length, firstEscapeIndexVal);
			Span<byte> span2;
			if (maxEscapedLength <= 256)
			{
				Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)256], 256);
				span2 = span;
			}
			else
			{
				span2 = (array = ArrayPool<byte>.Shared.Rent(maxEscapedLength));
			}
			Span<byte> span3 = span2;
			int num;
			JsonWriterHelper.EscapeString(utf8Value, span3, firstEscapeIndexVal, this._options.Encoder, out num);
			this.WriteStringByOptions(span3.Slice(0, num));
			if (array != null)
			{
				ArrayPool<byte>.Shared.Return(array, false);
			}
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x00020F1A File Offset: 0x0001F11A
		internal void WriteNumberValueAsStringUnescaped(ReadOnlySpan<byte> utf8Value)
		{
			this.WriteStringByOptions(utf8Value);
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.String;
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00020F30 File Offset: 0x0001F130
		[CLSCompliant(false)]
		public void WriteNumberValue(uint value)
		{
			this.WriteNumberValue((ulong)value);
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x00020F3A File Offset: 0x0001F13A
		[CLSCompliant(false)]
		public void WriteNumberValue(ulong value)
		{
			if (!this._options.SkipValidation)
			{
				this.ValidateWritingValue();
			}
			if (this._options.Indented)
			{
				this.WriteNumberValueIndented(value);
			}
			else
			{
				this.WriteNumberValueMinimized(value);
			}
			this.SetFlagToAddListSeparatorBeforeNextItem();
			this._tokenType = JsonTokenType.Number;
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00020F7C File Offset: 0x0001F17C
		private unsafe void WriteNumberValueMinimized(ulong value)
		{
			int num = 21;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			if (this._currentDepth < 0)
			{
				int bytesPending = this.BytesPending;
				this.BytesPending = bytesPending + 1;
				*span[bytesPending] = 44;
			}
			int num2;
			bool flag = Utf8Formatter.TryFormat(value, span.Slice(this.BytesPending), out num2, default(StandardFormat));
			this.BytesPending += num2;
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0002100C File Offset: 0x0001F20C
		private unsafe void WriteNumberValueIndented(ulong value)
		{
			int indentation = this.Indentation;
			int num = indentation + 20 + 1 + Utf8JsonWriter.s_newLineLength;
			if (this._memory.Length - this.BytesPending < num)
			{
				this.Grow(num);
			}
			Span<byte> span = this._memory.Span;
			if (this._currentDepth < 0)
			{
				int bytesPending = this.BytesPending;
				this.BytesPending = bytesPending + 1;
				*span[bytesPending] = 44;
			}
			if (this._tokenType != JsonTokenType.PropertyName)
			{
				if (this._tokenType != JsonTokenType.None)
				{
					this.WriteNewLine(span);
				}
				JsonWriterHelper.WriteIndentation(span.Slice(this.BytesPending), indentation);
				this.BytesPending += indentation;
			}
			int num2;
			bool flag = Utf8Formatter.TryFormat(value, span.Slice(this.BytesPending), out num2, default(StandardFormat));
			this.BytesPending += num2;
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x000210E4 File Offset: 0x0001F2E4
		internal unsafe void WriteNumberValueAsString(ulong value)
		{
			Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)20], 20);
			Span<byte> span2 = span;
			int num;
			bool flag = Utf8Formatter.TryFormat(value, span2, out num, default(StandardFormat));
			this.WriteNumberValueAsStringUnescaped(span2.Slice(0, num));
		}

		// Token: 0x04000270 RID: 624
		private static readonly int s_newLineLength = Environment.NewLine.Length;

		// Token: 0x04000271 RID: 625
		private const int DefaultGrowthSize = 4096;

		// Token: 0x04000272 RID: 626
		private const int InitialGrowthSize = 256;

		// Token: 0x04000273 RID: 627
		private IBufferWriter<byte> _output;

		// Token: 0x04000274 RID: 628
		private Stream _stream;

		// Token: 0x04000275 RID: 629
		private ArrayBufferWriter<byte> _arrayBufferWriter;

		// Token: 0x04000276 RID: 630
		private Memory<byte> _memory;

		// Token: 0x04000277 RID: 631
		private bool _inObject;

		// Token: 0x04000278 RID: 632
		private bool _commentAfterNoneOrPropertyName;

		// Token: 0x04000279 RID: 633
		private JsonTokenType _tokenType;

		// Token: 0x0400027A RID: 634
		private BitStack _bitStack;

		// Token: 0x0400027B RID: 635
		private int _currentDepth;

		// Token: 0x0400027C RID: 636
		private JsonWriterOptions _options;

		// Token: 0x0400027F RID: 639
		private static readonly char[] s_singleLineCommentDelimiter = new char[] { '*', '/' };
	}
}
