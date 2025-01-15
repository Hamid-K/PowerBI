using System;
using System.Buffers;
using System.Buffers.Text;
using System.Runtime.CompilerServices;
using System.Text.Encodings.Web;

namespace System.Text.Json
{
	// Token: 0x0200005A RID: 90
	internal static class JsonWriterHelper
	{
		// Token: 0x06000534 RID: 1332 RVA: 0x00015A20 File Offset: 0x00013C20
		public unsafe static void WriteIndentation(Span<byte> buffer, int indent)
		{
			if (indent < 8)
			{
				int i = 0;
				while (i < indent)
				{
					*buffer[i++] = 32;
					*buffer[i++] = 32;
				}
				return;
			}
			buffer.Slice(0, indent).Fill(32);
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00015A6B File Offset: 0x00013C6B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ValidateProperty(ReadOnlySpan<byte> propertyName)
		{
			if (propertyName.Length > 166666666)
			{
				ThrowHelper.ThrowArgumentException_PropertyNameTooLarge(propertyName.Length);
			}
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00015A87 File Offset: 0x00013C87
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ValidateValue(ReadOnlySpan<byte> value)
		{
			if (value.Length > 166666666)
			{
				ThrowHelper.ThrowArgumentException_ValueTooLarge((long)value.Length);
			}
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00015AA4 File Offset: 0x00013CA4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ValidateDouble(double value)
		{
			if (!JsonHelpers.IsFinite(value))
			{
				ThrowHelper.ThrowArgumentException_ValueNotSupported();
			}
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00015AB3 File Offset: 0x00013CB3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ValidateSingle(float value)
		{
			if (!JsonHelpers.IsFinite(value))
			{
				ThrowHelper.ThrowArgumentException_ValueNotSupported();
			}
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00015AC2 File Offset: 0x00013CC2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ValidateProperty(ReadOnlySpan<char> propertyName)
		{
			if (propertyName.Length > 166666666)
			{
				ThrowHelper.ThrowArgumentException_PropertyNameTooLarge(propertyName.Length);
			}
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00015ADE File Offset: 0x00013CDE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ValidateValue(ReadOnlySpan<char> value)
		{
			if (value.Length > 166666666)
			{
				ThrowHelper.ThrowArgumentException_ValueTooLarge((long)value.Length);
			}
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00015AFB File Offset: 0x00013CFB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ValidatePropertyAndValue(ReadOnlySpan<char> propertyName, ReadOnlySpan<byte> value)
		{
			if (propertyName.Length > 166666666 || value.Length > 166666666)
			{
				ThrowHelper.ThrowArgumentException(propertyName, value);
			}
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00015B20 File Offset: 0x00013D20
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ValidatePropertyAndValue(ReadOnlySpan<byte> propertyName, ReadOnlySpan<char> value)
		{
			if (propertyName.Length > 166666666 || value.Length > 166666666)
			{
				ThrowHelper.ThrowArgumentException(propertyName, value);
			}
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00015B45 File Offset: 0x00013D45
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ValidatePropertyAndValue(ReadOnlySpan<byte> propertyName, ReadOnlySpan<byte> value)
		{
			if (propertyName.Length > 166666666 || value.Length > 166666666)
			{
				ThrowHelper.ThrowArgumentException(propertyName, value);
			}
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00015B6A File Offset: 0x00013D6A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ValidatePropertyAndValue(ReadOnlySpan<char> propertyName, ReadOnlySpan<char> value)
		{
			if (propertyName.Length > 166666666 || value.Length > 166666666)
			{
				ThrowHelper.ThrowArgumentException(propertyName, value);
			}
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00015B8F File Offset: 0x00013D8F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ValidatePropertyNameLength(ReadOnlySpan<char> propertyName)
		{
			if (propertyName.Length > 166666666)
			{
				ThrowHelper.ThrowPropertyNameTooLargeArgumentException(propertyName.Length);
			}
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00015BAB File Offset: 0x00013DAB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ValidatePropertyNameLength(ReadOnlySpan<byte> propertyName)
		{
			if (propertyName.Length > 166666666)
			{
				ThrowHelper.ThrowPropertyNameTooLargeArgumentException(propertyName.Length);
			}
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00015BC8 File Offset: 0x00013DC8
		internal unsafe static void ValidateNumber(ReadOnlySpan<byte> utf8FormattedNumber)
		{
			int num = 0;
			if (*utf8FormattedNumber[num] == 45)
			{
				num++;
				if (utf8FormattedNumber.Length <= num)
				{
					throw new ArgumentException(SR.RequiredDigitNotFoundEndOfData, "utf8FormattedNumber");
				}
			}
			if (*utf8FormattedNumber[num] == 48)
			{
				num++;
			}
			else
			{
				while (num < utf8FormattedNumber.Length && JsonHelpers.IsDigit(*utf8FormattedNumber[num]))
				{
					num++;
				}
			}
			if (num == utf8FormattedNumber.Length)
			{
				return;
			}
			byte b = *utf8FormattedNumber[num];
			if (b == 46)
			{
				num++;
				if (utf8FormattedNumber.Length <= num)
				{
					throw new ArgumentException(SR.RequiredDigitNotFoundEndOfData, "utf8FormattedNumber");
				}
				while (num < utf8FormattedNumber.Length && JsonHelpers.IsDigit(*utf8FormattedNumber[num]))
				{
					num++;
				}
				if (num == utf8FormattedNumber.Length)
				{
					return;
				}
				b = *utf8FormattedNumber[num];
			}
			if (b != 101 && b != 69)
			{
				throw new ArgumentException(SR.Format(SR.ExpectedEndOfDigitNotFound, ThrowHelper.GetPrintableString(b)), "utf8FormattedNumber");
			}
			num++;
			if (utf8FormattedNumber.Length <= num)
			{
				throw new ArgumentException(SR.RequiredDigitNotFoundEndOfData, "utf8FormattedNumber");
			}
			b = *utf8FormattedNumber[num];
			if (b == 43 || b == 45)
			{
				num++;
			}
			if (utf8FormattedNumber.Length <= num)
			{
				throw new ArgumentException(SR.RequiredDigitNotFoundEndOfData, "utf8FormattedNumber");
			}
			while (num < utf8FormattedNumber.Length && JsonHelpers.IsDigit(*utf8FormattedNumber[num]))
			{
				num++;
			}
			if (num != utf8FormattedNumber.Length)
			{
				throw new ArgumentException(SR.Format(SR.ExpectedEndOfDigitNotFound, ThrowHelper.GetPrintableString(*utf8FormattedNumber[num])), "utf8FormattedNumber");
			}
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00015D60 File Offset: 0x00013F60
		public unsafe static bool IsValidUtf8String(ReadOnlySpan<byte> bytes)
		{
			bool flag;
			try
			{
				if (!bytes.IsEmpty)
				{
					try
					{
						fixed (byte* ptr = bytes.GetPinnableReference())
						{
							byte* ptr2 = ptr;
							JsonWriterHelper.s_utf8Encoding.GetCharCount(ptr2, bytes.Length);
						}
					}
					finally
					{
						byte* ptr = null;
					}
				}
				flag = true;
			}
			catch (DecoderFallbackException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00015DC0 File Offset: 0x00013FC0
		internal unsafe static OperationStatus ToUtf8(ReadOnlySpan<char> source, Span<byte> destination, out int written)
		{
			written = 0;
			OperationStatus operationStatus;
			try
			{
				if (!source.IsEmpty)
				{
					try
					{
						fixed (char* ptr = source.GetPinnableReference())
						{
							char* ptr2 = ptr;
							try
							{
								fixed (byte* ptr3 = destination.GetPinnableReference())
								{
									byte* ptr4 = ptr3;
									written = JsonWriterHelper.s_utf8Encoding.GetBytes(ptr2, source.Length, ptr4, destination.Length);
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
				operationStatus = OperationStatus.Done;
			}
			catch (EncoderFallbackException)
			{
				operationStatus = OperationStatus.InvalidData;
			}
			catch (ArgumentException)
			{
				operationStatus = OperationStatus.DestinationTooSmall;
			}
			return operationStatus;
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00015E60 File Offset: 0x00014060
		public unsafe static void WriteDateTimeTrimmed(Span<byte> buffer, DateTime value, out int bytesWritten)
		{
			Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)33], 33);
			Span<byte> span2 = span;
			bool flag = Utf8Formatter.TryFormat(value, span2, out bytesWritten, JsonWriterHelper.s_dateTimeStandardFormat);
			JsonWriterHelper.TrimDateTimeOffset(span2.Slice(0, bytesWritten), out bytesWritten);
			span2.Slice(0, bytesWritten).CopyTo(buffer);
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00015EB0 File Offset: 0x000140B0
		public unsafe static void WriteDateTimeOffsetTrimmed(Span<byte> buffer, DateTimeOffset value, out int bytesWritten)
		{
			Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)33], 33);
			Span<byte> span2 = span;
			bool flag = Utf8Formatter.TryFormat(value, span2, out bytesWritten, JsonWriterHelper.s_dateTimeStandardFormat);
			JsonWriterHelper.TrimDateTimeOffset(span2.Slice(0, bytesWritten), out bytesWritten);
			span2.Slice(0, bytesWritten).CopyTo(buffer);
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00015F00 File Offset: 0x00014100
		public unsafe static void TrimDateTimeOffset(Span<byte> buffer, out int bytesWritten)
		{
			if (*buffer[26] != 48)
			{
				bytesWritten = buffer.Length;
				return;
			}
			int num;
			if (*buffer[25] == 48)
			{
				if (*buffer[24] == 48)
				{
					if (*buffer[23] == 48)
					{
						if (*buffer[22] == 48)
						{
							if (*buffer[21] == 48)
							{
								if (*buffer[20] == 48)
								{
									num = 19;
								}
								else
								{
									num = 21;
								}
							}
							else
							{
								num = 22;
							}
						}
						else
						{
							num = 23;
						}
					}
					else
					{
						num = 24;
					}
				}
				else
				{
					num = 25;
				}
			}
			else
			{
				num = 26;
			}
			if (buffer.Length == 27)
			{
				bytesWritten = num;
				return;
			}
			if (buffer.Length == 33)
			{
				*buffer[num] = *buffer[27];
				*buffer[num + 1] = *buffer[28];
				*buffer[num + 2] = *buffer[29];
				*buffer[num + 3] = *buffer[30];
				*buffer[num + 4] = *buffer[31];
				*buffer[num + 5] = *buffer[32];
				bytesWritten = num + 6;
				return;
			}
			*buffer[num] = 90;
			bytesWritten = num + 1;
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x0001604B File Offset: 0x0001424B
		private unsafe static ReadOnlySpan<byte> AllowList
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.EFE627BE173681E4F55F4133AB4C1782E26D1080CB80CDB6BFAAC81416A2714E), 256);
			}
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0001605C File Offset: 0x0001425C
		private unsafe static bool NeedsEscaping(byte value)
		{
			return *JsonWriterHelper.AllowList[(int)value] == 0;
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0001607C File Offset: 0x0001427C
		private unsafe static bool NeedsEscapingNoBoundsCheck(char value)
		{
			return *JsonWriterHelper.AllowList[(int)value] == 0;
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0001609B File Offset: 0x0001429B
		public static int NeedsEscaping(ReadOnlySpan<byte> value, JavaScriptEncoder encoder)
		{
			return (encoder ?? JavaScriptEncoder.Default).FindFirstCharacterToEncodeUtf8(value);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x000160B0 File Offset: 0x000142B0
		public unsafe static int NeedsEscaping(ReadOnlySpan<char> value, JavaScriptEncoder encoder)
		{
			if (value.IsEmpty)
			{
				return -1;
			}
			fixed (char* pinnableReference = value.GetPinnableReference())
			{
				char* ptr = pinnableReference;
				return (encoder ?? JavaScriptEncoder.Default).FindFirstCharacterToEncode(ptr, value.Length);
			}
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x000160EA File Offset: 0x000142EA
		public static int GetMaxEscapedLength(int textLength, int firstIndexToEscape)
		{
			return firstIndexToEscape + 6 * (textLength - firstIndexToEscape);
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x000160F4 File Offset: 0x000142F4
		private static void EscapeString(ReadOnlySpan<byte> value, Span<byte> destination, JavaScriptEncoder encoder, ref int written)
		{
			int num;
			int num2;
			OperationStatus operationStatus = encoder.EncodeUtf8(value, destination, out num, out num2, true);
			if (operationStatus != OperationStatus.Done)
			{
				ThrowHelper.ThrowArgumentException_InvalidUTF8(value.Slice(num2));
			}
			written += num2;
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00016128 File Offset: 0x00014328
		public unsafe static void EscapeString(ReadOnlySpan<byte> value, Span<byte> destination, int indexOfFirstByteToEscape, JavaScriptEncoder encoder, out int written)
		{
			value.Slice(0, indexOfFirstByteToEscape).CopyTo(destination);
			written = indexOfFirstByteToEscape;
			if (encoder != null)
			{
				destination = destination.Slice(indexOfFirstByteToEscape);
				value = value.Slice(indexOfFirstByteToEscape);
				JsonWriterHelper.EscapeString(value, destination, encoder, ref written);
				return;
			}
			while (indexOfFirstByteToEscape < value.Length)
			{
				byte b = *value[indexOfFirstByteToEscape];
				if (!JsonWriterHelper.IsAsciiValue(b))
				{
					destination = destination.Slice(written);
					value = value.Slice(indexOfFirstByteToEscape);
					JsonWriterHelper.EscapeString(value, destination, JavaScriptEncoder.Default, ref written);
					return;
				}
				if (JsonWriterHelper.NeedsEscaping(b))
				{
					JsonWriterHelper.EscapeNextBytes(b, destination, ref written);
					indexOfFirstByteToEscape++;
				}
				else
				{
					*destination[written] = b;
					written++;
					indexOfFirstByteToEscape++;
				}
			}
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x000161E4 File Offset: 0x000143E4
		private unsafe static void EscapeNextBytes(byte value, Span<byte> destination, ref int written)
		{
			int num = written;
			written = num + 1;
			*destination[num] = 92;
			switch (value)
			{
			case 8:
				num = written;
				written = num + 1;
				*destination[num] = 98;
				return;
			case 9:
				num = written;
				written = num + 1;
				*destination[num] = 116;
				return;
			case 10:
				num = written;
				written = num + 1;
				*destination[num] = 110;
				return;
			case 11:
				break;
			case 12:
				num = written;
				written = num + 1;
				*destination[num] = 102;
				return;
			case 13:
				num = written;
				written = num + 1;
				*destination[num] = 114;
				return;
			default:
				if (value == 34)
				{
					num = written;
					written = num + 1;
					*destination[num] = 117;
					num = written;
					written = num + 1;
					*destination[num] = 48;
					num = written;
					written = num + 1;
					*destination[num] = 48;
					num = written;
					written = num + 1;
					*destination[num] = 50;
					num = written;
					written = num + 1;
					*destination[num] = 50;
					return;
				}
				if (value == 92)
				{
					num = written;
					written = num + 1;
					*destination[num] = 92;
					return;
				}
				break;
			}
			num = written;
			written = num + 1;
			*destination[num] = 117;
			int num2;
			bool flag = Utf8Formatter.TryFormat(value, destination.Slice(written), out num2, JsonWriterHelper.s_hexStandardFormat);
			written += num2;
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0001633E File Offset: 0x0001453E
		private static bool IsAsciiValue(byte value)
		{
			return value <= 127;
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00016348 File Offset: 0x00014548
		private static bool IsAsciiValue(char value)
		{
			return value <= '\u007f';
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00016354 File Offset: 0x00014554
		private unsafe static void EscapeString(ReadOnlySpan<char> value, Span<char> destination, JavaScriptEncoder encoder, ref int written)
		{
			int num;
			int num2;
			OperationStatus operationStatus = encoder.Encode(value, destination, out num, out num2, true);
			if (operationStatus != OperationStatus.Done)
			{
				ThrowHelper.ThrowArgumentException_InvalidUTF16((int)(*value[num2]));
			}
			written += num2;
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00016388 File Offset: 0x00014588
		public unsafe static void EscapeString(ReadOnlySpan<char> value, Span<char> destination, int indexOfFirstByteToEscape, JavaScriptEncoder encoder, out int written)
		{
			value.Slice(0, indexOfFirstByteToEscape).CopyTo(destination);
			written = indexOfFirstByteToEscape;
			if (encoder != null)
			{
				destination = destination.Slice(indexOfFirstByteToEscape);
				value = value.Slice(indexOfFirstByteToEscape);
				JsonWriterHelper.EscapeString(value, destination, encoder, ref written);
				return;
			}
			while (indexOfFirstByteToEscape < value.Length)
			{
				char c = (char)(*value[indexOfFirstByteToEscape]);
				if (!JsonWriterHelper.IsAsciiValue(c))
				{
					destination = destination.Slice(written);
					value = value.Slice(indexOfFirstByteToEscape);
					JsonWriterHelper.EscapeString(value, destination, JavaScriptEncoder.Default, ref written);
					return;
				}
				if (JsonWriterHelper.NeedsEscapingNoBoundsCheck(c))
				{
					JsonWriterHelper.EscapeNextChars(c, destination, ref written);
					indexOfFirstByteToEscape++;
				}
				else
				{
					*destination[written] = c;
					written++;
					indexOfFirstByteToEscape++;
				}
			}
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00016444 File Offset: 0x00014644
		private unsafe static void EscapeNextChars(char value, Span<char> destination, ref int written)
		{
			int num = written;
			written = num + 1;
			*destination[num] = '\\';
			byte b = (byte)value;
			switch (b)
			{
			case 8:
				num = written;
				written = num + 1;
				*destination[num] = 'b';
				return;
			case 9:
				num = written;
				written = num + 1;
				*destination[num] = 't';
				return;
			case 10:
				num = written;
				written = num + 1;
				*destination[num] = 'n';
				return;
			case 11:
				break;
			case 12:
				num = written;
				written = num + 1;
				*destination[num] = 'f';
				return;
			case 13:
				num = written;
				written = num + 1;
				*destination[num] = 'r';
				return;
			default:
				if (b == 34)
				{
					num = written;
					written = num + 1;
					*destination[num] = 'u';
					num = written;
					written = num + 1;
					*destination[num] = '0';
					num = written;
					written = num + 1;
					*destination[num] = '0';
					num = written;
					written = num + 1;
					*destination[num] = '2';
					num = written;
					written = num + 1;
					*destination[num] = '2';
					return;
				}
				if (b == 92)
				{
					num = written;
					written = num + 1;
					*destination[num] = '\\';
					return;
				}
				break;
			}
			num = written;
			written = num + 1;
			*destination[num] = 'u';
			written = JsonWriterHelper.WriteHex((int)value, destination, written);
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00016590 File Offset: 0x00014790
		private unsafe static int WriteHex(int value, Span<char> destination, int written)
		{
			*destination[written++] = HexConverter.ToCharUpper(value >> 12);
			*destination[written++] = HexConverter.ToCharUpper(value >> 8);
			*destination[written++] = HexConverter.ToCharUpper(value >> 4);
			*destination[written++] = HexConverter.ToCharUpper(value);
			return written;
		}

		// Token: 0x04000266 RID: 614
		private static readonly UTF8Encoding s_utf8Encoding = new UTF8Encoding(false, true);

		// Token: 0x04000267 RID: 615
		private static readonly StandardFormat s_dateTimeStandardFormat = new StandardFormat('O', byte.MaxValue);

		// Token: 0x04000268 RID: 616
		public const int LastAsciiCharacter = 127;

		// Token: 0x04000269 RID: 617
		private static readonly StandardFormat s_hexStandardFormat = new StandardFormat('X', 4);
	}
}
