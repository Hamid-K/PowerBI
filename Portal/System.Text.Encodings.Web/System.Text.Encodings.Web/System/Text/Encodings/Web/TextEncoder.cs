using System;
using System.Buffers;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.Unicode;

namespace System.Text.Encodings.Web
{
	// Token: 0x0200002C RID: 44
	public abstract class TextEncoder
	{
		// Token: 0x06000184 RID: 388
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public unsafe abstract bool TryEncodeUnicodeScalar(int unicodeScalar, char* buffer, int bufferLength, out int numberOfCharactersWritten);

		// Token: 0x06000185 RID: 389 RVA: 0x00005934 File Offset: 0x00003B34
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe bool TryEncodeUnicodeScalar(uint unicodeScalar, Span<char> buffer, out int charsWritten)
		{
			fixed (char* reference = MemoryMarshal.GetReference<char>(buffer))
			{
				char* ptr = reference;
				return this.TryEncodeUnicodeScalar((int)unicodeScalar, ptr, buffer.Length, out charsWritten);
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000595C File Offset: 0x00003B5C
		private unsafe bool TryEncodeUnicodeScalarUtf8(uint unicodeScalar, Span<char> utf16ScratchBuffer, Span<byte> utf8Destination, out int bytesWritten)
		{
			int num;
			if (!this.TryEncodeUnicodeScalar(unicodeScalar, utf16ScratchBuffer, out num))
			{
				TextEncoder.ThrowArgumentException_MaxOutputCharsPerInputChar();
			}
			utf16ScratchBuffer = utf16ScratchBuffer.Slice(0, num);
			int num2 = 0;
			IL_0078:
			while (!utf16ScratchBuffer.IsEmpty)
			{
				Rune rune;
				int num3;
				if (Rune.DecodeFromUtf16(utf16ScratchBuffer, out rune, out num3) != OperationStatus.Done)
				{
					TextEncoder.ThrowArgumentException_MaxOutputCharsPerInputChar();
				}
				uint num4 = (uint)UnicodeHelpers.GetUtf8RepresentationForScalarValue((uint)rune.Value);
				while (SpanUtility.IsValidIndex<byte>(utf8Destination, num2))
				{
					*utf8Destination[num2++] = (byte)num4;
					if ((num4 >>= 8) == 0U)
					{
						utf16ScratchBuffer = utf16ScratchBuffer.Slice(num3);
						goto IL_0078;
					}
				}
				bytesWritten = 0;
				return false;
			}
			bytesWritten = num2;
			return true;
		}

		// Token: 0x06000187 RID: 391
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public unsafe abstract int FindFirstCharacterToEncode(char* text, int textLength);

		// Token: 0x06000188 RID: 392
		[EditorBrowsable(EditorBrowsableState.Never)]
		public abstract bool WillEncode(int unicodeScalar);

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000189 RID: 393
		[EditorBrowsable(EditorBrowsableState.Never)]
		public abstract int MaxOutputCharactersPerInputCharacter { get; }

		// Token: 0x0600018A RID: 394 RVA: 0x000059F0 File Offset: 0x00003BF0
		[NullableContext(1)]
		public virtual string Encode(string value)
		{
			if (value == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
			}
			int num = this.FindFirstCharacterToEncode(value.AsSpan());
			if (num < 0)
			{
				return value;
			}
			return this.EncodeToNewString(value.AsSpan(), num);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00005A28 File Offset: 0x00003C28
		private unsafe string EncodeToNewString(ReadOnlySpan<char> value, int indexOfFirstCharToEncode)
		{
			ReadOnlySpan<char> readOnlySpan = value.Slice(indexOfFirstCharToEncode);
			Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)2048], 1024);
			ValueStringBuilder valueStringBuilder = new ValueStringBuilder(span);
			valueStringBuilder.Append(value.Slice(0, indexOfFirstCharToEncode));
			int num = Math.Max(this.MaxOutputCharactersPerInputCharacter, 1024);
			do
			{
				Span<char> span2 = valueStringBuilder.AppendSpan(Math.Max(readOnlySpan.Length, num));
				int num2;
				int num3;
				this.EncodeCore(readOnlySpan, span2, out num2, out num3, true);
				if (num3 == 0 || num3 > span2.Length)
				{
					TextEncoder.ThrowArgumentException_MaxOutputCharsPerInputChar();
				}
				readOnlySpan = readOnlySpan.Slice(num2);
				valueStringBuilder.Length -= span2.Length - num3;
			}
			while (!readOnlySpan.IsEmpty);
			return valueStringBuilder.ToString();
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00005AEB File Offset: 0x00003CEB
		[NullableContext(1)]
		public void Encode(TextWriter output, string value)
		{
			this.Encode(output, value, 0, value.Length);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00005AFC File Offset: 0x00003CFC
		[NullableContext(1)]
		public virtual void Encode(TextWriter output, string value, int startIndex, int characterCount)
		{
			if (output == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.output);
			}
			if (value == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
			}
			TextEncoder.ValidateRanges(startIndex, characterCount, value.Length);
			int num = this.FindFirstCharacterToEncode(value.AsSpan(startIndex, characterCount));
			if (num < 0)
			{
				num = characterCount;
			}
			output.WritePartialString(value, startIndex, num);
			if (num != characterCount)
			{
				this.EncodeCore(output, value.AsSpan(startIndex + num, characterCount - num));
			}
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00005B64 File Offset: 0x00003D64
		[NullableContext(1)]
		public virtual void Encode(TextWriter output, char[] value, int startIndex, int characterCount)
		{
			if (output == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.output);
			}
			if (value == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
			}
			TextEncoder.ValidateRanges(startIndex, characterCount, value.Length);
			int num = this.FindFirstCharacterToEncode(value.AsSpan(startIndex, characterCount));
			if (num < 0)
			{
				num = characterCount;
			}
			output.Write(value, startIndex, num);
			if (num != characterCount)
			{
				this.EncodeCore(output, value.AsSpan(startIndex + num, characterCount - num));
			}
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00005BD4 File Offset: 0x00003DD4
		public virtual OperationStatus EncodeUtf8(ReadOnlySpan<byte> utf8Source, Span<byte> utf8Destination, out int bytesConsumed, out int bytesWritten, bool isFinalBlock = true)
		{
			ReadOnlySpan<byte> readOnlySpan = utf8Source;
			if (utf8Destination.Length < utf8Source.Length)
			{
				readOnlySpan = utf8Source.Slice(0, utf8Destination.Length);
			}
			int num = this.FindFirstCharacterToEncodeUtf8(readOnlySpan);
			if (num < 0)
			{
				num = readOnlySpan.Length;
			}
			utf8Source.Slice(0, num).CopyTo(utf8Destination);
			if (num == utf8Source.Length)
			{
				bytesConsumed = utf8Source.Length;
				bytesWritten = utf8Source.Length;
				return OperationStatus.Done;
			}
			int num2;
			int num3;
			OperationStatus operationStatus = this.EncodeUtf8Core(utf8Source.Slice(num), utf8Destination.Slice(num), out num2, out num3, isFinalBlock);
			bytesConsumed = num + num2;
			bytesWritten = num + num3;
			return operationStatus;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00005C74 File Offset: 0x00003E74
		private protected unsafe virtual OperationStatus EncodeUtf8Core(ReadOnlySpan<byte> utf8Source, Span<byte> utf8Destination, out int bytesConsumed, out int bytesWritten, bool isFinalBlock)
		{
			int length = utf8Source.Length;
			int length2 = utf8Destination.Length;
			Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)48], 24);
			Span<char> span2 = span;
			IL_00CE:
			OperationStatus operationStatus2;
			while (!utf8Source.IsEmpty)
			{
				Rune rune;
				int num;
				OperationStatus operationStatus = Rune.DecodeFromUtf8(utf8Source, out rune, out num);
				if (operationStatus != OperationStatus.Done)
				{
					if (!isFinalBlock && operationStatus == OperationStatus.NeedMoreData)
					{
						operationStatus2 = OperationStatus.NeedMoreData;
						goto IL_00DC;
					}
				}
				else if (!this.WillEncode(rune.Value))
				{
					uint num2 = (uint)UnicodeHelpers.GetUtf8RepresentationForScalarValue((uint)rune.Value);
					int i = 0;
					while (i < utf8Destination.Length)
					{
						*utf8Destination[i++] = (byte)num2;
						if ((num2 >>= 8) == 0U)
						{
							utf8Source = utf8Source.Slice(num);
							utf8Destination = utf8Destination.Slice(i);
							goto IL_00CE;
						}
					}
					goto IL_00F9;
				}
				int num3;
				if (this.TryEncodeUnicodeScalarUtf8((uint)rune.Value, span2, utf8Destination, out num3))
				{
					utf8Source = utf8Source.Slice(num);
					utf8Destination = utf8Destination.Slice(num3);
					continue;
				}
				goto IL_00F9;
				IL_00DC:
				bytesConsumed = length - utf8Source.Length;
				bytesWritten = length2 - utf8Destination.Length;
				return operationStatus2;
				IL_00F9:
				operationStatus2 = OperationStatus.DestinationTooSmall;
				goto IL_00DC;
			}
			operationStatus2 = OperationStatus.Done;
			goto IL_00DC;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00005D80 File Offset: 0x00003F80
		public virtual OperationStatus Encode(ReadOnlySpan<char> source, Span<char> destination, out int charsConsumed, out int charsWritten, bool isFinalBlock = true)
		{
			ReadOnlySpan<char> readOnlySpan = source;
			if (destination.Length < source.Length)
			{
				readOnlySpan = source.Slice(0, destination.Length);
			}
			int num = this.FindFirstCharacterToEncode(readOnlySpan);
			if (num < 0)
			{
				num = readOnlySpan.Length;
			}
			source.Slice(0, num).CopyTo(destination);
			if (num == source.Length)
			{
				charsConsumed = source.Length;
				charsWritten = source.Length;
				return OperationStatus.Done;
			}
			int num2;
			int num3;
			OperationStatus operationStatus = this.EncodeCore(source.Slice(num), destination.Slice(num), out num2, out num3, isFinalBlock);
			charsConsumed = num + num2;
			charsWritten = num + num3;
			return operationStatus;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00005E20 File Offset: 0x00004020
		private protected virtual OperationStatus EncodeCore(ReadOnlySpan<char> source, Span<char> destination, out int charsConsumed, out int charsWritten, bool isFinalBlock)
		{
			int length = source.Length;
			int length2 = destination.Length;
			OperationStatus operationStatus2;
			while (!source.IsEmpty)
			{
				Rune rune;
				int num;
				OperationStatus operationStatus = Rune.DecodeFromUtf16(source, out rune, out num);
				if (operationStatus != OperationStatus.Done)
				{
					if (!isFinalBlock && operationStatus == OperationStatus.NeedMoreData)
					{
						operationStatus2 = OperationStatus.NeedMoreData;
						goto IL_0090;
					}
				}
				else if (!this.WillEncode(rune.Value))
				{
					int num2;
					if (rune.TryEncodeToUtf16(destination, out num2))
					{
						source = source.Slice(num);
						destination = destination.Slice(num);
						continue;
					}
					goto IL_00AD;
				}
				int num3;
				if (this.TryEncodeUnicodeScalar((uint)rune.Value, destination, out num3))
				{
					source = source.Slice(num);
					destination = destination.Slice(num3);
					continue;
				}
				goto IL_00AD;
				IL_0090:
				charsConsumed = length - source.Length;
				charsWritten = length2 - destination.Length;
				return operationStatus2;
				IL_00AD:
				operationStatus2 = OperationStatus.DestinationTooSmall;
				goto IL_0090;
			}
			operationStatus2 = OperationStatus.Done;
			goto IL_0090;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00005EE0 File Offset: 0x000040E0
		private void EncodeCore(TextWriter output, ReadOnlySpan<char> value)
		{
			int num = Math.Max(this.MaxOutputCharactersPerInputCharacter, 1024);
			char[] array = ArrayPool<char>.Shared.Rent(Math.Max(value.Length, num));
			Span<char> span = array;
			do
			{
				int num2;
				int num3;
				this.EncodeCore(value, span, out num2, out num3, true);
				if (num3 == 0 || num3 > span.Length)
				{
					TextEncoder.ThrowArgumentException_MaxOutputCharsPerInputChar();
				}
				output.Write(array, 0, num3);
				value = value.Slice(num2);
			}
			while (!value.IsEmpty);
			ArrayPool<char>.Shared.Return(array, false);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00005F68 File Offset: 0x00004168
		private protected unsafe virtual int FindFirstCharacterToEncode(ReadOnlySpan<char> text)
		{
			fixed (char* reference = MemoryMarshal.GetReference<char>(text))
			{
				char* ptr = reference;
				return this.FindFirstCharacterToEncode(ptr, text.Length);
			}
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00005F90 File Offset: 0x00004190
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual int FindFirstCharacterToEncodeUtf8(ReadOnlySpan<byte> utf8Text)
		{
			int length = utf8Text.Length;
			Rune rune;
			int num;
			while (!utf8Text.IsEmpty && Rune.DecodeFromUtf8(utf8Text, out rune, out num) == OperationStatus.Done && !this.WillEncode(rune.Value))
			{
				utf8Text = utf8Text.Slice(num);
			}
			if (!utf8Text.IsEmpty)
			{
				return length - utf8Text.Length;
			}
			return -1;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00005FEC File Offset: 0x000041EC
		internal unsafe static bool TryCopyCharacters(string source, Span<char> destination, out int numberOfCharactersWritten)
		{
			if (destination.Length < source.Length)
			{
				numberOfCharactersWritten = 0;
				return false;
			}
			for (int i = 0; i < source.Length; i++)
			{
				*destination[i] = source[i];
			}
			numberOfCharactersWritten = source.Length;
			return true;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00006037 File Offset: 0x00004237
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal unsafe static bool TryWriteScalarAsChar(int unicodeScalar, Span<char> destination, out int numberOfCharactersWritten)
		{
			if (destination.IsEmpty)
			{
				numberOfCharactersWritten = 0;
				return false;
			}
			*destination[0] = (char)unicodeScalar;
			numberOfCharactersWritten = 1;
			return true;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00006056 File Offset: 0x00004256
		private static void ValidateRanges(int startIndex, int characterCount, int actualInputLength)
		{
			if (startIndex < 0 || startIndex > actualInputLength)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			if (characterCount < 0 || characterCount > actualInputLength - startIndex)
			{
				throw new ArgumentOutOfRangeException("characterCount");
			}
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00006080 File Offset: 0x00004280
		[DoesNotReturn]
		private static void ThrowArgumentException_MaxOutputCharsPerInputChar()
		{
			throw new ArgumentException(SR.TextEncoderDoesNotImplementMaxOutputCharsPerInputChar);
		}

		// Token: 0x040000D5 RID: 213
		private const int EncodeStartingOutputBufferSize = 1024;
	}
}
