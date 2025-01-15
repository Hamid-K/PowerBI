using System;
using System.Buffers;
using System.Buffers.Text;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Text.Json
{
	// Token: 0x02000045 RID: 69
	internal static class JsonReaderHelper
	{
		// Token: 0x06000340 RID: 832 RVA: 0x00009C72 File Offset: 0x00007E72
		public static bool ContainsSpecialCharacters(this ReadOnlySpan<char> text)
		{
			return text.IndexOfAny(". '/\"[]()\t\n\r\f\b\\\u0085\u2028\u2029".AsSpan()) >= 0;
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00009C8C File Offset: 0x00007E8C
		public static global::System.ValueTuple<int, int> CountNewLines(ReadOnlySpan<byte> data)
		{
			int num = data.LastIndexOf(10);
			int num2 = 0;
			if (num >= 0)
			{
				num2 = 1;
				data = data.Slice(0, num);
				int num3;
				while ((num3 = data.IndexOf(10)) >= 0)
				{
					num2++;
					data = data.Slice(num3 + 1);
				}
			}
			return new global::System.ValueTuple<int, int>(num2, num);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00009CDC File Offset: 0x00007EDC
		internal static JsonValueKind ToValueKind(this JsonTokenType tokenType)
		{
			switch (tokenType)
			{
			case JsonTokenType.None:
				return JsonValueKind.Undefined;
			case JsonTokenType.StartObject:
				return JsonValueKind.Object;
			case JsonTokenType.StartArray:
				return JsonValueKind.Array;
			case JsonTokenType.String:
			case JsonTokenType.Number:
			case JsonTokenType.True:
			case JsonTokenType.False:
			case JsonTokenType.Null:
				return (JsonValueKind)(tokenType - JsonTokenType.EndArray);
			}
			return JsonValueKind.Undefined;
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00009D2D File Offset: 0x00007F2D
		public static bool IsTokenTypePrimitive(JsonTokenType tokenType)
		{
			return tokenType - JsonTokenType.String <= 4;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00009D39 File Offset: 0x00007F39
		public static bool IsHexDigit(byte nextByte)
		{
			return HexConverter.IsHexChar((int)nextByte);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00009D44 File Offset: 0x00007F44
		public unsafe static bool TryGetEscapedDateTime(ReadOnlySpan<byte> source, out DateTime value)
		{
			Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)252], 252);
			Span<byte> span2 = span;
			int num;
			JsonReaderHelper.Unescape(source, span2, out num);
			span2 = span2.Slice(0, num);
			DateTime dateTime;
			if (JsonHelpers.IsValidUnescapedDateTimeOffsetParseLength(span2.Length) && JsonHelpers.TryParseAsISO(span2, out dateTime))
			{
				value = dateTime;
				return true;
			}
			value = default(DateTime);
			return false;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00009DA8 File Offset: 0x00007FA8
		public unsafe static bool TryGetEscapedDateTimeOffset(ReadOnlySpan<byte> source, out DateTimeOffset value)
		{
			Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)252], 252);
			Span<byte> span2 = span;
			int num;
			JsonReaderHelper.Unescape(source, span2, out num);
			span2 = span2.Slice(0, num);
			DateTimeOffset dateTimeOffset;
			if (JsonHelpers.IsValidUnescapedDateTimeOffsetParseLength(span2.Length) && JsonHelpers.TryParseAsISO(span2, out dateTimeOffset))
			{
				value = dateTimeOffset;
				return true;
			}
			value = default(DateTimeOffset);
			return false;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00009E0C File Offset: 0x0000800C
		public unsafe static bool TryGetEscapedGuid(ReadOnlySpan<byte> source, out Guid value)
		{
			Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)216], 216);
			Span<byte> span2 = span;
			int num;
			JsonReaderHelper.Unescape(source, span2, out num);
			span2 = span2.Slice(0, num);
			Guid guid;
			int num2;
			if (span2.Length == 36 && Utf8Parser.TryParse(span2, out guid, out num2, 'D'))
			{
				value = guid;
				return true;
			}
			value = default(Guid);
			return false;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00009E70 File Offset: 0x00008070
		public static bool TryGetFloatingPointConstant(ReadOnlySpan<byte> span, out float value)
		{
			if (span.Length == 3)
			{
				if (span.SequenceEqual(JsonConstants.NaNValue))
				{
					value = float.NaN;
					return true;
				}
			}
			else if (span.Length == 8)
			{
				if (span.SequenceEqual(JsonConstants.PositiveInfinityValue))
				{
					value = float.PositiveInfinity;
					return true;
				}
			}
			else if (span.Length == 9 && span.SequenceEqual(JsonConstants.NegativeInfinityValue))
			{
				value = float.NegativeInfinity;
				return true;
			}
			value = 0f;
			return false;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00009EE8 File Offset: 0x000080E8
		public static bool TryGetFloatingPointConstant(ReadOnlySpan<byte> span, out double value)
		{
			if (span.Length == 3)
			{
				if (span.SequenceEqual(JsonConstants.NaNValue))
				{
					value = double.NaN;
					return true;
				}
			}
			else if (span.Length == 8)
			{
				if (span.SequenceEqual(JsonConstants.PositiveInfinityValue))
				{
					value = double.PositiveInfinity;
					return true;
				}
			}
			else if (span.Length == 9 && span.SequenceEqual(JsonConstants.NegativeInfinityValue))
			{
				value = double.NegativeInfinity;
				return true;
			}
			value = 0.0;
			return false;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00009F70 File Offset: 0x00008170
		public unsafe static bool TryGetUnescapedBase64Bytes(ReadOnlySpan<byte> utf8Source, [NotNullWhen(true)] out byte[] bytes)
		{
			byte[] array = null;
			Span<byte> span2;
			if (utf8Source.Length <= 256)
			{
				Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)256], 256);
				span2 = span;
			}
			else
			{
				span2 = (array = ArrayPool<byte>.Shared.Rent(utf8Source.Length));
			}
			Span<byte> span3 = span2;
			int num;
			JsonReaderHelper.Unescape(utf8Source, span3, out num);
			span3 = span3.Slice(0, num);
			bool flag = JsonReaderHelper.TryDecodeBase64InPlace(span3, out bytes);
			if (array != null)
			{
				span3.Clear();
				ArrayPool<byte>.Shared.Return(array, false);
			}
			return flag;
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00009FF8 File Offset: 0x000081F8
		public unsafe static string GetUnescapedString(ReadOnlySpan<byte> utf8Source)
		{
			int length = utf8Source.Length;
			byte[] array = null;
			Span<byte> span2;
			if (length <= 256)
			{
				Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)256], 256);
				span2 = span;
			}
			else
			{
				span2 = (array = ArrayPool<byte>.Shared.Rent(length));
			}
			Span<byte> span3 = span2;
			int num;
			JsonReaderHelper.Unescape(utf8Source, span3, out num);
			span3 = span3.Slice(0, num);
			string text = JsonReaderHelper.TranscodeHelper(span3);
			if (array != null)
			{
				span3.Clear();
				ArrayPool<byte>.Shared.Return(array, false);
			}
			return text;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000A080 File Offset: 0x00008280
		public unsafe static ReadOnlySpan<byte> GetUnescapedSpan(ReadOnlySpan<byte> utf8Source)
		{
			int length = utf8Source.Length;
			byte[] array = null;
			Span<byte> span2;
			if (length <= 256)
			{
				Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)256], 256);
				span2 = span;
			}
			else
			{
				span2 = (array = ArrayPool<byte>.Shared.Rent(length));
			}
			Span<byte> span3 = span2;
			int num;
			JsonReaderHelper.Unescape(utf8Source, span3, out num);
			ReadOnlySpan<byte> readOnlySpan = span3.Slice(0, num).ToArray();
			if (array != null)
			{
				new Span<byte>(array, 0, num).Clear();
				ArrayPool<byte>.Shared.Return(array, false);
			}
			return readOnlySpan;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000A114 File Offset: 0x00008314
		public unsafe static bool UnescapeAndCompare(ReadOnlySpan<byte> utf8Source, ReadOnlySpan<byte> other)
		{
			byte[] array = null;
			Span<byte> span2;
			if (utf8Source.Length <= 256)
			{
				Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)256], 256);
				span2 = span;
			}
			else
			{
				span2 = (array = ArrayPool<byte>.Shared.Rent(utf8Source.Length));
			}
			Span<byte> span3 = span2;
			int num;
			JsonReaderHelper.Unescape(utf8Source, span3, 0, out num);
			span3 = span3.Slice(0, num);
			bool flag = other.SequenceEqual(span3);
			if (array != null)
			{
				span3.Clear();
				ArrayPool<byte>.Shared.Return(array, false);
			}
			return flag;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000A1A0 File Offset: 0x000083A0
		public unsafe static bool UnescapeAndCompare(ReadOnlySequence<byte> utf8Source, ReadOnlySpan<byte> other)
		{
			byte[] array = null;
			byte[] array2 = null;
			int num = checked((int)utf8Source.Length);
			Span<byte> span2;
			if (num <= 256)
			{
				Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)256], 256);
				span2 = span;
			}
			else
			{
				span2 = (array2 = ArrayPool<byte>.Shared.Rent(num));
			}
			Span<byte> span3 = span2;
			Span<byte> span4;
			if (num <= 256)
			{
				Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)256], 256);
				span4 = span;
			}
			else
			{
				span4 = (array = ArrayPool<byte>.Shared.Rent(num));
			}
			Span<byte> span5 = span4;
			(in utf8Source).CopyTo(span5);
			span5 = span5.Slice(0, num);
			int num2;
			JsonReaderHelper.Unescape(span5, span3, 0, out num2);
			span3 = span3.Slice(0, num2);
			bool flag = other.SequenceEqual(span3);
			if (array2 != null)
			{
				span3.Clear();
				ArrayPool<byte>.Shared.Return(array2, false);
				span5.Clear();
				ArrayPool<byte>.Shared.Return(array, false);
			}
			return flag;
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000A298 File Offset: 0x00008498
		public static bool TryDecodeBase64InPlace(Span<byte> utf8Unescaped, [NotNullWhen(true)] out byte[] bytes)
		{
			int num;
			OperationStatus operationStatus = Base64.DecodeFromUtf8InPlace(utf8Unescaped, out num);
			if (operationStatus != OperationStatus.Done)
			{
				bytes = null;
				return false;
			}
			bytes = utf8Unescaped.Slice(0, num).ToArray();
			return true;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000A2CC File Offset: 0x000084CC
		public unsafe static bool TryDecodeBase64(ReadOnlySpan<byte> utf8Unescaped, [NotNullWhen(true)] out byte[] bytes)
		{
			byte[] array = null;
			Span<byte> span2;
			if (utf8Unescaped.Length <= 256)
			{
				Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)256], 256);
				span2 = span;
			}
			else
			{
				span2 = (array = ArrayPool<byte>.Shared.Rent(utf8Unescaped.Length));
			}
			Span<byte> span3 = span2;
			int num;
			int num2;
			OperationStatus operationStatus = Base64.DecodeFromUtf8(utf8Unescaped, span3, out num, out num2, true);
			if (operationStatus != OperationStatus.Done)
			{
				bytes = null;
				if (array != null)
				{
					span3.Clear();
					ArrayPool<byte>.Shared.Return(array, false);
				}
				return false;
			}
			bytes = span3.Slice(0, num2).ToArray();
			if (array != null)
			{
				span3.Clear();
				ArrayPool<byte>.Shared.Return(array, false);
			}
			return true;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000A378 File Offset: 0x00008578
		public unsafe static string TranscodeHelper(ReadOnlySpan<byte> utf8Unescaped)
		{
			string text;
			try
			{
				if (utf8Unescaped.IsEmpty)
				{
					text = string.Empty;
				}
				else
				{
					try
					{
						fixed (byte* ptr = utf8Unescaped.GetPinnableReference())
						{
							byte* ptr2 = ptr;
							text = JsonReaderHelper.s_utf8Encoding.GetString(ptr2, utf8Unescaped.Length);
						}
					}
					finally
					{
						byte* ptr = null;
					}
				}
			}
			catch (DecoderFallbackException ex)
			{
				throw ThrowHelper.GetInvalidOperationException_ReadInvalidUTF8(ex);
			}
			return text;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000A3E0 File Offset: 0x000085E0
		public unsafe static int TranscodeHelper(ReadOnlySpan<byte> utf8Unescaped, Span<char> destination)
		{
			int num;
			try
			{
				if (utf8Unescaped.IsEmpty)
				{
					num = 0;
				}
				else
				{
					try
					{
						fixed (byte* ptr = utf8Unescaped.GetPinnableReference())
						{
							byte* ptr2 = ptr;
							try
							{
								fixed (char* ptr3 = destination.GetPinnableReference())
								{
									char* ptr4 = ptr3;
									num = JsonReaderHelper.s_utf8Encoding.GetChars(ptr2, utf8Unescaped.Length, ptr4, destination.Length);
								}
							}
							finally
							{
								char* ptr3 = null;
							}
						}
					}
					finally
					{
						byte* ptr = null;
					}
				}
			}
			catch (DecoderFallbackException ex)
			{
				throw ThrowHelper.GetInvalidOperationException_ReadInvalidUTF8(ex);
			}
			catch (ArgumentException)
			{
				destination.Clear();
				throw;
			}
			return num;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000A484 File Offset: 0x00008684
		public unsafe static void ValidateUtf8(ReadOnlySpan<byte> utf8Buffer)
		{
			try
			{
				if (!utf8Buffer.IsEmpty)
				{
					try
					{
						fixed (byte* ptr = utf8Buffer.GetPinnableReference())
						{
							byte* ptr2 = ptr;
							JsonReaderHelper.s_utf8Encoding.GetCharCount(ptr2, utf8Buffer.Length);
						}
					}
					finally
					{
						byte* ptr = null;
					}
				}
			}
			catch (DecoderFallbackException ex)
			{
				throw ThrowHelper.GetInvalidOperationException_ReadInvalidUTF8(ex);
			}
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000A4E8 File Offset: 0x000086E8
		internal unsafe static int GetUtf8ByteCount(ReadOnlySpan<char> text)
		{
			int num;
			try
			{
				if (text.IsEmpty)
				{
					num = 0;
				}
				else
				{
					try
					{
						fixed (char* ptr = text.GetPinnableReference())
						{
							char* ptr2 = ptr;
							num = JsonReaderHelper.s_utf8Encoding.GetByteCount(ptr2, text.Length);
						}
					}
					finally
					{
						char* ptr = null;
					}
				}
			}
			catch (EncoderFallbackException ex)
			{
				throw ThrowHelper.GetArgumentException_ReadInvalidUTF16(ex);
			}
			return num;
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000A54C File Offset: 0x0000874C
		internal unsafe static int GetUtf8FromText(ReadOnlySpan<char> text, Span<byte> dest)
		{
			int num;
			try
			{
				if (text.IsEmpty)
				{
					num = 0;
				}
				else
				{
					try
					{
						fixed (char* ptr = text.GetPinnableReference())
						{
							char* ptr2 = ptr;
							try
							{
								fixed (byte* ptr3 = dest.GetPinnableReference())
								{
									byte* ptr4 = ptr3;
									num = JsonReaderHelper.s_utf8Encoding.GetBytes(ptr2, text.Length, ptr4, dest.Length);
								}
							}
							finally
							{
								byte* ptr3 = null;
							}
						}
					}
					finally
					{
						char* ptr = null;
					}
				}
			}
			catch (EncoderFallbackException ex)
			{
				throw ThrowHelper.GetArgumentException_ReadInvalidUTF16(ex);
			}
			return num;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000A5D8 File Offset: 0x000087D8
		internal unsafe static string GetTextFromUtf8(ReadOnlySpan<byte> utf8Text)
		{
			if (utf8Text.IsEmpty)
			{
				return string.Empty;
			}
			fixed (byte* pinnableReference = utf8Text.GetPinnableReference())
			{
				byte* ptr = pinnableReference;
				return JsonReaderHelper.s_utf8Encoding.GetString(ptr, utf8Text.Length);
			}
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000A614 File Offset: 0x00008814
		internal static void Unescape(ReadOnlySpan<byte> source, Span<byte> destination, out int written)
		{
			int num = source.IndexOf(92);
			bool flag = JsonReaderHelper.TryUnescape(source, destination, num, out written);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000A634 File Offset: 0x00008834
		internal static void Unescape(ReadOnlySpan<byte> source, Span<byte> destination, int idx, out int written)
		{
			bool flag = JsonReaderHelper.TryUnescape(source, destination, idx, out written);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000A64C File Offset: 0x0000884C
		internal static bool TryUnescape(ReadOnlySpan<byte> source, Span<byte> destination, out int written)
		{
			int num = source.IndexOf(92);
			return JsonReaderHelper.TryUnescape(source, destination, num, out written);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000A66C File Offset: 0x0000886C
		private unsafe static bool TryUnescape(ReadOnlySpan<byte> source, Span<byte> destination, int idx, out int written)
		{
			if (!source.Slice(0, idx).TryCopyTo(destination))
			{
				written = 0;
			}
			else
			{
				written = idx;
				while (written != destination.Length)
				{
					byte b = *source[++idx];
					if (b <= 98)
					{
						if (b <= 47)
						{
							if (b != 34)
							{
								if (b != 47)
								{
									goto IL_0179;
								}
								int num = written;
								written = num + 1;
								*destination[num] = 47;
							}
							else
							{
								int num = written;
								written = num + 1;
								*destination[num] = 34;
							}
						}
						else if (b != 92)
						{
							if (b != 98)
							{
								goto IL_0179;
							}
							int num = written;
							written = num + 1;
							*destination[num] = 8;
						}
						else
						{
							int num = written;
							written = num + 1;
							*destination[num] = 92;
						}
					}
					else if (b <= 110)
					{
						if (b != 102)
						{
							if (b != 110)
							{
								goto IL_0179;
							}
							int num = written;
							written = num + 1;
							*destination[num] = 10;
						}
						else
						{
							int num = written;
							written = num + 1;
							*destination[num] = 12;
						}
					}
					else if (b != 114)
					{
						if (b != 116)
						{
							goto IL_0179;
						}
						int num = written;
						written = num + 1;
						*destination[num] = 9;
					}
					else
					{
						int num = written;
						written = num + 1;
						*destination[num] = 13;
					}
					IL_025B:
					if (++idx != source.Length)
					{
						if (*source[idx] == 92)
						{
							continue;
						}
						ReadOnlySpan<byte> readOnlySpan = source.Slice(idx);
						int num2 = readOnlySpan.IndexOf(92);
						if (num2 < 0)
						{
							num2 = readOnlySpan.Length;
						}
						if (written + num2 >= destination.Length)
						{
							break;
						}
						switch (num2)
						{
						case 1:
						{
							int num = written;
							written = num + 1;
							*destination[num] = *source[idx++];
							break;
						}
						case 2:
						{
							int num = written;
							written = num + 1;
							*destination[num] = *source[idx++];
							num = written;
							written = num + 1;
							*destination[num] = *source[idx++];
							break;
						}
						case 3:
						{
							int num = written;
							written = num + 1;
							*destination[num] = *source[idx++];
							num = written;
							written = num + 1;
							*destination[num] = *source[idx++];
							num = written;
							written = num + 1;
							*destination[num] = *source[idx++];
							break;
						}
						default:
							readOnlySpan.Slice(0, num2).CopyTo(destination.Slice(written));
							written += num2;
							idx += num2;
							break;
						}
						if (idx != source.Length)
						{
							continue;
						}
					}
					return true;
					IL_0179:
					int num3;
					int num4;
					bool flag = Utf8Parser.TryParse(source.Slice(idx + 1, 4), out num3, out num4, 'x');
					idx += 4;
					if (JsonHelpers.IsInRangeInclusive((uint)num3, 55296U, 57343U))
					{
						if (num3 >= 56320)
						{
							ThrowHelper.ThrowInvalidOperationException_ReadInvalidUTF16(num3);
						}
						if (source.Length < idx + 7 || *source[idx + 1] != 92 || *source[idx + 2] != 117)
						{
							ThrowHelper.ThrowInvalidOperationException_ReadIncompleteUTF16();
						}
						int num5;
						flag = Utf8Parser.TryParse(source.Slice(idx + 3, 4), out num5, out num4, 'x');
						idx += 6;
						if (!JsonHelpers.IsInRangeInclusive((uint)num5, 56320U, 57343U))
						{
							ThrowHelper.ThrowInvalidOperationException_ReadInvalidUTF16(num5);
						}
						num3 = 1024 * (num3 - 55296) + (num5 - 56320) + 65536;
					}
					int num6;
					bool flag2 = JsonReaderHelper.TryEncodeToUtf8Bytes((uint)num3, destination.Slice(written), out num6);
					if (flag2)
					{
						written += num6;
						goto IL_025B;
					}
					break;
				}
			}
			return false;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000AA54 File Offset: 0x00008C54
		private unsafe static bool TryEncodeToUtf8Bytes(uint scalar, Span<byte> utf8Destination, out int bytesWritten)
		{
			if (scalar < 128U)
			{
				if (utf8Destination.Length < 1)
				{
					bytesWritten = 0;
					return false;
				}
				*utf8Destination[0] = (byte)scalar;
				bytesWritten = 1;
			}
			else if (scalar < 2048U)
			{
				if (utf8Destination.Length < 2)
				{
					bytesWritten = 0;
					return false;
				}
				*utf8Destination[0] = (byte)(192U | (scalar >> 6));
				*utf8Destination[1] = (byte)(128U | (scalar & 63U));
				bytesWritten = 2;
			}
			else if (scalar < 65536U)
			{
				if (utf8Destination.Length < 3)
				{
					bytesWritten = 0;
					return false;
				}
				*utf8Destination[0] = (byte)(224U | (scalar >> 12));
				*utf8Destination[1] = (byte)(128U | ((scalar >> 6) & 63U));
				*utf8Destination[2] = (byte)(128U | (scalar & 63U));
				bytesWritten = 3;
			}
			else
			{
				if (utf8Destination.Length < 4)
				{
					bytesWritten = 0;
					return false;
				}
				*utf8Destination[0] = (byte)(240U | (scalar >> 18));
				*utf8Destination[1] = (byte)(128U | ((scalar >> 12) & 63U));
				*utf8Destination[2] = (byte)(128U | ((scalar >> 6) & 63U));
				*utf8Destination[3] = (byte)(128U | (scalar & 63U));
				bytesWritten = 4;
			}
			return true;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000AB94 File Offset: 0x00008D94
		public unsafe static int IndexOfQuoteOrAnyControlOrBackSlash(this ReadOnlySpan<byte> span)
		{
			ref byte reference = ref MemoryMarshal.GetReference<byte>(span);
			int length = span.Length;
			IntPtr intPtr = (IntPtr)0;
			IntPtr intPtr2 = (IntPtr)length;
			if (Vector.IsHardwareAccelerated && length >= Vector<byte>.Count * 2)
			{
				int num = Unsafe.AsPointer<byte>(ref reference) & (Vector<byte>.Count - 1);
				intPtr2 = (IntPtr)((Vector<byte>.Count - num) & (Vector<byte>.Count - 1));
			}
			Vector<byte> vector5;
			for (;;)
			{
				if ((void*)intPtr2 < 8)
				{
					if ((void*)intPtr2 >= 4)
					{
						intPtr2 -= 4;
						uint num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref reference, intPtr));
						if (34U == num2 || 92U == num2 || 32U > num2)
						{
							goto IL_03B2;
						}
						num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref reference, intPtr + 1));
						if (34U == num2 || 92U == num2 || 32U > num2)
						{
							goto IL_03BA;
						}
						num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref reference, intPtr + 2));
						if (34U == num2 || 92U == num2 || 32U > num2)
						{
							goto IL_03C8;
						}
						num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref reference, intPtr + 3));
						if (34U == num2 || 92U == num2 || 32U > num2)
						{
							goto IL_03D6;
						}
						intPtr += 4;
					}
					while ((void*)intPtr2 != null)
					{
						intPtr2 -= 1;
						uint num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref reference, intPtr));
						if (34U == num2 || 92U == num2 || 32U > num2)
						{
							goto IL_03B2;
						}
						intPtr += 1;
					}
					if (!Vector.IsHardwareAccelerated || (void*)intPtr >= length)
					{
						return -1;
					}
					intPtr2 = (IntPtr)((length - (void*)intPtr) & ~(Vector<byte>.Count - 1));
					Vector<byte> vector = new Vector<byte>(34);
					Vector<byte> vector2 = new Vector<byte>(92);
					Vector<byte> vector3 = new Vector<byte>(32);
					while ((void*)intPtr2 != (void*)intPtr)
					{
						Vector<byte> vector4 = Unsafe.ReadUnaligned<Vector<byte>>(Unsafe.AddByteOffset<byte>(ref reference, intPtr));
						vector5 = Vector.BitwiseOr<byte>(Vector.BitwiseOr<byte>(Vector.Equals<byte>(vector4, vector), Vector.Equals<byte>(vector4, vector2)), Vector.LessThan<byte>(vector4, vector3));
						if (!Vector<byte>.Zero.Equals(vector5))
						{
							goto IL_0374;
						}
						intPtr += Vector<byte>.Count;
					}
					if ((void*)intPtr >= length)
					{
						return -1;
					}
					intPtr2 = (IntPtr)(length - (void*)intPtr);
				}
				else
				{
					intPtr2 -= 8;
					uint num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref reference, intPtr));
					if (34U == num2 || 92U == num2 || 32U > num2)
					{
						goto IL_03B2;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref reference, intPtr + 1));
					if (34U == num2 || 92U == num2 || 32U > num2)
					{
						goto IL_03BA;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref reference, intPtr + 2));
					if (34U == num2 || 92U == num2 || 32U > num2)
					{
						goto IL_03C8;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref reference, intPtr + 3));
					if (34U == num2 || 92U == num2 || 32U > num2)
					{
						goto IL_03D6;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref reference, intPtr + 4));
					if (34U == num2 || 92U == num2 || 32U > num2)
					{
						goto IL_03E4;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref reference, intPtr + 5));
					if (34U == num2 || 92U == num2 || 32U > num2)
					{
						goto IL_03F2;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref reference, intPtr + 6));
					if (34U == num2 || 92U == num2 || 32U > num2)
					{
						goto IL_0400;
					}
					num2 = (uint)(*Unsafe.AddByteOffset<byte>(ref reference, intPtr + 7));
					if (34U == num2 || 92U == num2 || 32U > num2)
					{
						goto IL_040E;
					}
					intPtr += 8;
				}
			}
			IL_0374:
			return (void*)intPtr + JsonReaderHelper.LocateFirstFoundByte(vector5);
			IL_03B2:
			return (void*)intPtr;
			IL_03BA:
			return (void*)(intPtr + 1);
			IL_03C8:
			return (void*)(intPtr + 2);
			IL_03D6:
			return (void*)(intPtr + 3);
			IL_03E4:
			return (void*)(intPtr + 4);
			IL_03F2:
			return (void*)(intPtr + 5);
			IL_0400:
			return (void*)(intPtr + 6);
			IL_040E:
			return (void*)(intPtr + 7);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000AFBC File Offset: 0x000091BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int LocateFirstFoundByte(Vector<byte> match)
		{
			Vector<ulong> vector = Vector.AsVectorUInt64<byte>(match);
			ulong num = 0UL;
			int i;
			for (i = 0; i < Vector<ulong>.Count; i++)
			{
				num = vector[i];
				if (num != 0UL)
				{
					break;
				}
			}
			return i * 8 + JsonReaderHelper.LocateFirstFoundByte(num);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000AFFC File Offset: 0x000091FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int LocateFirstFoundByte(ulong match)
		{
			ulong num = match ^ (match - 1UL);
			return (int)(num * 283686952306184UL >> 57);
		}

		// Token: 0x0400015A RID: 346
		private const string SpecialCharacters = ". '/\"[]()\t\n\r\f\b\\\u0085\u2028\u2029";

		// Token: 0x0400015B RID: 347
		public static readonly UTF8Encoding s_utf8Encoding = new UTF8Encoding(false, true);

		// Token: 0x0400015C RID: 348
		private const ulong XorPowerOfTwoToHighByte = 283686952306184UL;
	}
}
