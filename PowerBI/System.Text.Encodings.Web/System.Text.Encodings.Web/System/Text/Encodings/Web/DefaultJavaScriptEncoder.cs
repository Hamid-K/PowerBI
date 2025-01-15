using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Text.Unicode;

namespace System.Text.Encodings.Web
{
	// Token: 0x02000026 RID: 38
	internal sealed class DefaultJavaScriptEncoder : JavaScriptEncoder
	{
		// Token: 0x06000159 RID: 345 RVA: 0x000053C1 File Offset: 0x000035C1
		internal DefaultJavaScriptEncoder(TextEncoderSettings settings)
			: this(settings, false)
		{
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000053CC File Offset: 0x000035CC
		private unsafe DefaultJavaScriptEncoder(TextEncoderSettings settings, bool allowMinimalJsonEscaping)
		{
			if (settings == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.settings);
			}
			OptimizedInboxTextEncoder optimizedInboxTextEncoder;
			if (allowMinimalJsonEscaping)
			{
				ScalarEscaperBase singletonMinimallyEscaped = DefaultJavaScriptEncoder.EscaperImplementation.SingletonMinimallyEscaped;
				readonly ref AllowedBmpCodePointsBitmap allowedCodePointsBitmap = ref settings.GetAllowedCodePointsBitmap();
				IntPtr intPtr = stackalloc byte[(UIntPtr)4];
				*intPtr = 34;
				*(intPtr + 2) = 92;
				Span<char> span = new Span<char>(intPtr, 2);
				optimizedInboxTextEncoder = new OptimizedInboxTextEncoder(singletonMinimallyEscaped, in allowedCodePointsBitmap, false, span);
			}
			else
			{
				ScalarEscaperBase singleton = DefaultJavaScriptEncoder.EscaperImplementation.Singleton;
				readonly ref AllowedBmpCodePointsBitmap allowedCodePointsBitmap2 = ref settings.GetAllowedCodePointsBitmap();
				IntPtr intPtr2 = stackalloc byte[(UIntPtr)4];
				*intPtr2 = 92;
				*(intPtr2 + 2) = 96;
				Span<char> span = new Span<char>(intPtr2, 2);
				optimizedInboxTextEncoder = new OptimizedInboxTextEncoder(singleton, in allowedCodePointsBitmap2, true, span);
			}
			this._innerEncoder = optimizedInboxTextEncoder;
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600015B RID: 347 RVA: 0x0000545A File Offset: 0x0000365A
		public override int MaxOutputCharactersPerInputCharacter
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000545D File Offset: 0x0000365D
		private protected override OperationStatus EncodeCore(ReadOnlySpan<char> source, Span<char> destination, out int charsConsumed, out int charsWritten, bool isFinalBlock)
		{
			return this._innerEncoder.Encode(source, destination, out charsConsumed, out charsWritten, isFinalBlock);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00005471 File Offset: 0x00003671
		private protected override OperationStatus EncodeUtf8Core(ReadOnlySpan<byte> utf8Source, Span<byte> utf8Destination, out int bytesConsumed, out int bytesWritten, bool isFinalBlock)
		{
			return this._innerEncoder.EncodeUtf8(utf8Source, utf8Destination, out bytesConsumed, out bytesWritten, isFinalBlock);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00005485 File Offset: 0x00003685
		private protected override int FindFirstCharacterToEncode(ReadOnlySpan<char> text)
		{
			return this._innerEncoder.GetIndexOfFirstCharToEncode(text);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00005493 File Offset: 0x00003693
		public unsafe override int FindFirstCharacterToEncode(char* text, int textLength)
		{
			return this._innerEncoder.FindFirstCharacterToEncode(text, textLength);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x000054A2 File Offset: 0x000036A2
		public override int FindFirstCharacterToEncodeUtf8(ReadOnlySpan<byte> utf8Text)
		{
			return this._innerEncoder.GetIndexOfFirstByteToEncode(utf8Text);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000054B0 File Offset: 0x000036B0
		public unsafe override bool TryEncodeUnicodeScalar(int unicodeScalar, char* buffer, int bufferLength, out int numberOfCharactersWritten)
		{
			return this._innerEncoder.TryEncodeUnicodeScalar(unicodeScalar, buffer, bufferLength, out numberOfCharactersWritten);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000054C2 File Offset: 0x000036C2
		public override bool WillEncode(int unicodeScalar)
		{
			return !this._innerEncoder.IsScalarValueAllowed(new Rune(unicodeScalar));
		}

		// Token: 0x040000D0 RID: 208
		internal static readonly DefaultJavaScriptEncoder BasicLatinSingleton = new DefaultJavaScriptEncoder(new TextEncoderSettings(new UnicodeRange[] { UnicodeRanges.BasicLatin }));

		// Token: 0x040000D1 RID: 209
		internal static readonly DefaultJavaScriptEncoder UnsafeRelaxedEscapingSingleton = new DefaultJavaScriptEncoder(new TextEncoderSettings(new UnicodeRange[] { UnicodeRanges.All }), true);

		// Token: 0x040000D2 RID: 210
		private readonly OptimizedInboxTextEncoder _innerEncoder;

		// Token: 0x02000039 RID: 57
		private sealed class EscaperImplementation : ScalarEscaperBase
		{
			// Token: 0x060001BA RID: 442 RVA: 0x000066F0 File Offset: 0x000048F0
			private EscaperImplementation(bool allowMinimalEscaping)
			{
				this._preescapedMap.InsertAsciiChar('\b', 98);
				this._preescapedMap.InsertAsciiChar('\t', 116);
				this._preescapedMap.InsertAsciiChar('\n', 110);
				this._preescapedMap.InsertAsciiChar('\f', 102);
				this._preescapedMap.InsertAsciiChar('\r', 114);
				this._preescapedMap.InsertAsciiChar('\\', 92);
				if (allowMinimalEscaping)
				{
					this._preescapedMap.InsertAsciiChar('"', 34);
				}
			}

			// Token: 0x060001BB RID: 443 RVA: 0x00006770 File Offset: 0x00004970
			internal unsafe override int EncodeUtf8(Rune value, Span<byte> destination)
			{
				byte b;
				if (!this._preescapedMap.TryLookup(value, out b))
				{
					return DefaultJavaScriptEncoder.EscaperImplementation.<EncodeUtf8>g__TryEncodeScalarAsHex|4_0(this, value, destination);
				}
				if (SpanUtility.IsValidIndex<byte>(destination, 1))
				{
					*destination[0] = 92;
					*destination[1] = b;
					return 2;
				}
				return -1;
			}

			// Token: 0x060001BC RID: 444 RVA: 0x000067B8 File Offset: 0x000049B8
			internal unsafe override int EncodeUtf16(Rune value, Span<char> destination)
			{
				byte b;
				if (!this._preescapedMap.TryLookup(value, out b))
				{
					return DefaultJavaScriptEncoder.EscaperImplementation.<EncodeUtf16>g__TryEncodeScalarAsHex|5_0(this, value, destination);
				}
				if (SpanUtility.IsValidIndex<char>(destination, 1))
				{
					*destination[0] = '\\';
					*destination[1] = (char)b;
					return 2;
				}
				return -1;
			}

			// Token: 0x060001BE RID: 446 RVA: 0x00006818 File Offset: 0x00004A18
			[CompilerGenerated]
			internal unsafe static int <EncodeUtf8>g__TryEncodeScalarAsHex|4_0(object @this, Rune value, Span<byte> destination)
			{
				if (value.IsBmp)
				{
					if (SpanUtility.IsValidIndex<byte>(destination, 5))
					{
						*destination[0] = 92;
						*destination[1] = 117;
						HexConverter.ToBytesBuffer((byte)value.Value, destination, 4, HexConverter.Casing.Upper);
						HexConverter.ToBytesBuffer((byte)((uint)value.Value >> 8), destination, 2, HexConverter.Casing.Upper);
						return 6;
					}
				}
				else
				{
					char c;
					char c2;
					UnicodeHelpers.GetUtf16SurrogatePairFromAstralScalarValue((uint)value.Value, out c, out c2);
					if (SpanUtility.IsValidIndex<byte>(destination, 11))
					{
						*destination[0] = 92;
						*destination[1] = 117;
						HexConverter.ToBytesBuffer((byte)c, destination, 4, HexConverter.Casing.Upper);
						HexConverter.ToBytesBuffer((byte)(c >> 8), destination, 2, HexConverter.Casing.Upper);
						*destination[6] = 92;
						*destination[7] = 117;
						HexConverter.ToBytesBuffer((byte)c2, destination, 10, HexConverter.Casing.Upper);
						HexConverter.ToBytesBuffer((byte)(c2 >> 8), destination, 8, HexConverter.Casing.Upper);
						return 12;
					}
				}
				return -1;
			}

			// Token: 0x060001BF RID: 447 RVA: 0x000068EC File Offset: 0x00004AEC
			[CompilerGenerated]
			internal unsafe static int <EncodeUtf16>g__TryEncodeScalarAsHex|5_0(object @this, Rune value, Span<char> destination)
			{
				if (value.IsBmp)
				{
					if (SpanUtility.IsValidIndex<char>(destination, 5))
					{
						*destination[0] = '\\';
						*destination[1] = 'u';
						HexConverter.ToCharsBuffer((byte)value.Value, destination, 4, HexConverter.Casing.Upper);
						HexConverter.ToCharsBuffer((byte)((uint)value.Value >> 8), destination, 2, HexConverter.Casing.Upper);
						return 6;
					}
				}
				else
				{
					char c;
					char c2;
					UnicodeHelpers.GetUtf16SurrogatePairFromAstralScalarValue((uint)value.Value, out c, out c2);
					if (SpanUtility.IsValidIndex<char>(destination, 11))
					{
						*destination[0] = '\\';
						*destination[1] = 'u';
						HexConverter.ToCharsBuffer((byte)c, destination, 4, HexConverter.Casing.Upper);
						HexConverter.ToCharsBuffer((byte)(c >> 8), destination, 2, HexConverter.Casing.Upper);
						*destination[6] = '\\';
						*destination[7] = 'u';
						HexConverter.ToCharsBuffer((byte)c2, destination, 10, HexConverter.Casing.Upper);
						HexConverter.ToCharsBuffer((byte)(c2 >> 8), destination, 8, HexConverter.Casing.Upper);
						return 12;
					}
				}
				return -1;
			}

			// Token: 0x040000EC RID: 236
			internal static readonly DefaultJavaScriptEncoder.EscaperImplementation Singleton = new DefaultJavaScriptEncoder.EscaperImplementation(false);

			// Token: 0x040000ED RID: 237
			internal static readonly DefaultJavaScriptEncoder.EscaperImplementation SingletonMinimallyEscaped = new DefaultJavaScriptEncoder.EscaperImplementation(true);

			// Token: 0x040000EE RID: 238
			private readonly AsciiByteMap _preescapedMap;
		}
	}
}
