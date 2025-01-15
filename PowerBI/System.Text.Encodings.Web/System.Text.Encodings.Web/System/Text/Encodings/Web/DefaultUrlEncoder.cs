using System;
using System.Buffers;
using System.Text.Unicode;

namespace System.Text.Encodings.Web
{
	// Token: 0x02000025 RID: 37
	internal sealed class DefaultUrlEncoder : UrlEncoder
	{
		// Token: 0x0600014F RID: 335 RVA: 0x00005184 File Offset: 0x00003384
		internal unsafe DefaultUrlEncoder(TextEncoderSettings settings)
		{
			if (settings == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.settings);
			}
			ScalarEscaperBase singleton = DefaultUrlEncoder.EscaperImplementation.Singleton;
			readonly ref AllowedBmpCodePointsBitmap allowedCodePointsBitmap = ref settings.GetAllowedCodePointsBitmap();
			IntPtr intPtr = stackalloc byte[(UIntPtr)62];
			*intPtr = 32;
			*(intPtr + 2) = 35;
			*(intPtr + (IntPtr)2 * 2) = 37;
			*(intPtr + (IntPtr)3 * 2) = 47;
			*(intPtr + (IntPtr)4 * 2) = 58;
			*(intPtr + (IntPtr)5 * 2) = 61;
			*(intPtr + (IntPtr)6 * 2) = 63;
			*(intPtr + (IntPtr)7 * 2) = 91;
			*(intPtr + (IntPtr)8 * 2) = 92;
			*(intPtr + (IntPtr)9 * 2) = 93;
			*(intPtr + (IntPtr)10 * 2) = 94;
			*(intPtr + (IntPtr)11 * 2) = 96;
			*(intPtr + (IntPtr)12 * 2) = 123;
			*(intPtr + (IntPtr)13 * 2) = 124;
			*(intPtr + (IntPtr)14 * 2) = 125;
			*(intPtr + (IntPtr)15 * 2) = (short)65520;
			*(intPtr + (IntPtr)16 * 2) = (short)65521;
			*(intPtr + (IntPtr)17 * 2) = (short)65522;
			*(intPtr + (IntPtr)18 * 2) = (short)65523;
			*(intPtr + (IntPtr)19 * 2) = (short)65524;
			*(intPtr + (IntPtr)20 * 2) = (short)65525;
			*(intPtr + (IntPtr)21 * 2) = (short)65526;
			*(intPtr + (IntPtr)22 * 2) = (short)65527;
			*(intPtr + (IntPtr)23 * 2) = (short)65528;
			*(intPtr + (IntPtr)24 * 2) = (short)65529;
			*(intPtr + (IntPtr)25 * 2) = (short)65530;
			*(intPtr + (IntPtr)26 * 2) = (short)65531;
			*(intPtr + (IntPtr)27 * 2) = (short)65532;
			*(intPtr + (IntPtr)28 * 2) = (short)65533;
			*(intPtr + (IntPtr)29 * 2) = (short)65534;
			*(intPtr + (IntPtr)30 * 2) = (short)65535;
			Span<char> span = new Span<char>(intPtr, 31);
			this._innerEncoder = new OptimizedInboxTextEncoder(singleton, in allowedCodePointsBitmap, true, span);
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00005323 File Offset: 0x00003523
		public override int MaxOutputCharactersPerInputCharacter
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00005327 File Offset: 0x00003527
		private protected override OperationStatus EncodeCore(ReadOnlySpan<char> source, Span<char> destination, out int charsConsumed, out int charsWritten, bool isFinalBlock)
		{
			return this._innerEncoder.Encode(source, destination, out charsConsumed, out charsWritten, isFinalBlock);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000533B File Offset: 0x0000353B
		private protected override OperationStatus EncodeUtf8Core(ReadOnlySpan<byte> utf8Source, Span<byte> utf8Destination, out int bytesConsumed, out int bytesWritten, bool isFinalBlock)
		{
			return this._innerEncoder.EncodeUtf8(utf8Source, utf8Destination, out bytesConsumed, out bytesWritten, isFinalBlock);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000534F File Offset: 0x0000354F
		private protected override int FindFirstCharacterToEncode(ReadOnlySpan<char> text)
		{
			return this._innerEncoder.GetIndexOfFirstCharToEncode(text);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000535D File Offset: 0x0000355D
		public unsafe override int FindFirstCharacterToEncode(char* text, int textLength)
		{
			return this._innerEncoder.FindFirstCharacterToEncode(text, textLength);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000536C File Offset: 0x0000356C
		public override int FindFirstCharacterToEncodeUtf8(ReadOnlySpan<byte> utf8Text)
		{
			return this._innerEncoder.GetIndexOfFirstByteToEncode(utf8Text);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000537A File Offset: 0x0000357A
		public unsafe override bool TryEncodeUnicodeScalar(int unicodeScalar, char* buffer, int bufferLength, out int numberOfCharactersWritten)
		{
			return this._innerEncoder.TryEncodeUnicodeScalar(unicodeScalar, buffer, bufferLength, out numberOfCharactersWritten);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000538C File Offset: 0x0000358C
		public override bool WillEncode(int unicodeScalar)
		{
			return !this._innerEncoder.IsScalarValueAllowed(new Rune(unicodeScalar));
		}

		// Token: 0x040000CE RID: 206
		internal static readonly DefaultUrlEncoder BasicLatinSingleton = new DefaultUrlEncoder(new TextEncoderSettings(new UnicodeRange[] { UnicodeRanges.BasicLatin }));

		// Token: 0x040000CF RID: 207
		private readonly OptimizedInboxTextEncoder _innerEncoder;

		// Token: 0x02000038 RID: 56
		private sealed class EscaperImplementation : ScalarEscaperBase
		{
			// Token: 0x060001B6 RID: 438 RVA: 0x0000656B File Offset: 0x0000476B
			private EscaperImplementation()
			{
			}

			// Token: 0x060001B7 RID: 439 RVA: 0x00006574 File Offset: 0x00004774
			internal unsafe override int EncodeUtf8(Rune value, Span<byte> destination)
			{
				uint num = (uint)UnicodeHelpers.GetUtf8RepresentationForScalarValue((uint)value.Value);
				if (SpanUtility.IsValidIndex<byte>(destination, 2))
				{
					*destination[0] = 37;
					HexConverter.ToBytesBuffer((byte)num, destination, 1, HexConverter.Casing.Upper);
					if ((num >>= 8) == 0U)
					{
						return 3;
					}
					if (SpanUtility.IsValidIndex<byte>(destination, 5))
					{
						*destination[3] = 37;
						HexConverter.ToBytesBuffer((byte)num, destination, 4, HexConverter.Casing.Upper);
						if ((num >>= 8) == 0U)
						{
							return 6;
						}
						if (SpanUtility.IsValidIndex<byte>(destination, 8))
						{
							*destination[6] = 37;
							HexConverter.ToBytesBuffer((byte)num, destination, 7, HexConverter.Casing.Upper);
							if ((num >>= 8) == 0U)
							{
								return 9;
							}
							if (SpanUtility.IsValidIndex<byte>(destination, 11))
							{
								*destination[9] = 37;
								HexConverter.ToBytesBuffer((byte)num, destination, 10, HexConverter.Casing.Upper);
								return 12;
							}
						}
					}
				}
				return -1;
			}

			// Token: 0x060001B8 RID: 440 RVA: 0x0000662C File Offset: 0x0000482C
			internal unsafe override int EncodeUtf16(Rune value, Span<char> destination)
			{
				uint num = (uint)UnicodeHelpers.GetUtf8RepresentationForScalarValue((uint)value.Value);
				if (SpanUtility.IsValidIndex<char>(destination, 2))
				{
					*destination[0] = '%';
					HexConverter.ToCharsBuffer((byte)num, destination, 1, HexConverter.Casing.Upper);
					if ((num >>= 8) == 0U)
					{
						return 3;
					}
					if (SpanUtility.IsValidIndex<char>(destination, 5))
					{
						*destination[3] = '%';
						HexConverter.ToCharsBuffer((byte)num, destination, 4, HexConverter.Casing.Upper);
						if ((num >>= 8) == 0U)
						{
							return 6;
						}
						if (SpanUtility.IsValidIndex<char>(destination, 8))
						{
							*destination[6] = '%';
							HexConverter.ToCharsBuffer((byte)num, destination, 7, HexConverter.Casing.Upper);
							if ((num >>= 8) == 0U)
							{
								return 9;
							}
							if (SpanUtility.IsValidIndex<char>(destination, 11))
							{
								*destination[9] = '%';
								HexConverter.ToCharsBuffer((byte)num, destination, 10, HexConverter.Casing.Upper);
								return 12;
							}
						}
					}
				}
				return -1;
			}

			// Token: 0x040000EB RID: 235
			internal static readonly DefaultUrlEncoder.EscaperImplementation Singleton = new DefaultUrlEncoder.EscaperImplementation();
		}
	}
}
