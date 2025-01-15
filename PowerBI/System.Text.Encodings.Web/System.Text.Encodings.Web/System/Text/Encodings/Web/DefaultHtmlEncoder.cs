using System;
using System.Buffers;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text.Unicode;

namespace System.Text.Encodings.Web
{
	// Token: 0x0200002A RID: 42
	internal sealed class DefaultHtmlEncoder : HtmlEncoder
	{
		// Token: 0x06000175 RID: 373 RVA: 0x00005830 File Offset: 0x00003A30
		internal DefaultHtmlEncoder(TextEncoderSettings settings)
		{
			if (settings == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.settings);
			}
			this._innerEncoder = new OptimizedInboxTextEncoder(DefaultHtmlEncoder.EscaperImplementation.Singleton, settings.GetAllowedCodePointsBitmap(), true, default(ReadOnlySpan<char>));
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000176 RID: 374 RVA: 0x0000586C File Offset: 0x00003A6C
		public override int MaxOutputCharactersPerInputCharacter
		{
			get
			{
				return 8;
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000586F File Offset: 0x00003A6F
		private protected override OperationStatus EncodeCore(ReadOnlySpan<char> source, Span<char> destination, out int charsConsumed, out int charsWritten, bool isFinalBlock)
		{
			return this._innerEncoder.Encode(source, destination, out charsConsumed, out charsWritten, isFinalBlock);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00005883 File Offset: 0x00003A83
		private protected override OperationStatus EncodeUtf8Core(ReadOnlySpan<byte> utf8Source, Span<byte> utf8Destination, out int bytesConsumed, out int bytesWritten, bool isFinalBlock)
		{
			return this._innerEncoder.EncodeUtf8(utf8Source, utf8Destination, out bytesConsumed, out bytesWritten, isFinalBlock);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00005897 File Offset: 0x00003A97
		private protected override int FindFirstCharacterToEncode(ReadOnlySpan<char> text)
		{
			return this._innerEncoder.GetIndexOfFirstCharToEncode(text);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000058A5 File Offset: 0x00003AA5
		public unsafe override int FindFirstCharacterToEncode(char* text, int textLength)
		{
			return this._innerEncoder.FindFirstCharacterToEncode(text, textLength);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x000058B4 File Offset: 0x00003AB4
		public override int FindFirstCharacterToEncodeUtf8(ReadOnlySpan<byte> utf8Text)
		{
			return this._innerEncoder.GetIndexOfFirstByteToEncode(utf8Text);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x000058C2 File Offset: 0x00003AC2
		public unsafe override bool TryEncodeUnicodeScalar(int unicodeScalar, char* buffer, int bufferLength, out int numberOfCharactersWritten)
		{
			return this._innerEncoder.TryEncodeUnicodeScalar(unicodeScalar, buffer, bufferLength, out numberOfCharactersWritten);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x000058D4 File Offset: 0x00003AD4
		public override bool WillEncode(int unicodeScalar)
		{
			return !this._innerEncoder.IsScalarValueAllowed(new Rune(unicodeScalar));
		}

		// Token: 0x040000D3 RID: 211
		internal static readonly DefaultHtmlEncoder BasicLatinSingleton = new DefaultHtmlEncoder(new TextEncoderSettings(new UnicodeRange[] { UnicodeRanges.BasicLatin }));

		// Token: 0x040000D4 RID: 212
		private readonly OptimizedInboxTextEncoder _innerEncoder;

		// Token: 0x0200003A RID: 58
		private sealed class EscaperImplementation : ScalarEscaperBase
		{
			// Token: 0x060001C0 RID: 448 RVA: 0x000069BF File Offset: 0x00004BBF
			private EscaperImplementation()
			{
			}

			// Token: 0x060001C1 RID: 449 RVA: 0x000069C8 File Offset: 0x00004BC8
			internal override int EncodeUtf8(Rune value, Span<byte> destination)
			{
				if (value.Value == 60)
				{
					if (SpanUtility.TryWriteBytes(destination, 38, 108, 116, 59))
					{
						return 4;
					}
				}
				else if (value.Value == 62)
				{
					if (SpanUtility.TryWriteBytes(destination, 38, 103, 116, 59))
					{
						return 4;
					}
				}
				else if (value.Value == 38)
				{
					if (SpanUtility.TryWriteBytes(destination, 38, 97, 109, 112, 59))
					{
						return 5;
					}
				}
				else
				{
					if (value.Value != 34)
					{
						return DefaultHtmlEncoder.EscaperImplementation.<EncodeUtf8>g__TryEncodeScalarAsHex|2_0(this, (uint)value.Value, destination);
					}
					if (SpanUtility.TryWriteBytes(destination, 38, 113, 117, 111, 116, 59))
					{
						return 6;
					}
				}
				return -1;
			}

			// Token: 0x060001C2 RID: 450 RVA: 0x00006A60 File Offset: 0x00004C60
			internal override int EncodeUtf16(Rune value, Span<char> destination)
			{
				if (value.Value == 60)
				{
					if (SpanUtility.TryWriteChars(destination, '&', 'l', 't', ';'))
					{
						return 4;
					}
				}
				else if (value.Value == 62)
				{
					if (SpanUtility.TryWriteChars(destination, '&', 'g', 't', ';'))
					{
						return 4;
					}
				}
				else if (value.Value == 38)
				{
					if (SpanUtility.TryWriteChars(destination, '&', 'a', 'm', 'p', ';'))
					{
						return 5;
					}
				}
				else
				{
					if (value.Value != 34)
					{
						return DefaultHtmlEncoder.EscaperImplementation.<EncodeUtf16>g__TryEncodeScalarAsHex|3_0(this, (uint)value.Value, destination);
					}
					if (SpanUtility.TryWriteChars(destination, '&', 'q', 'u', 'o', 't', ';'))
					{
						return 6;
					}
				}
				return -1;
			}

			// Token: 0x060001C4 RID: 452 RVA: 0x00006B04 File Offset: 0x00004D04
			[CompilerGenerated]
			internal unsafe static int <EncodeUtf8>g__TryEncodeScalarAsHex|2_0(object @this, uint scalarValue, Span<byte> destination)
			{
				int num = BitOperations.Log2(scalarValue) / 4 + 4;
				if (SpanUtility.IsValidIndex<byte>(destination, num))
				{
					*destination[num] = 59;
					SpanUtility.TryWriteBytes(destination, 38, 35, 120, 48);
					destination = destination.Slice(3, num - 3);
					int num2 = destination.Length - 1;
					while (SpanUtility.IsValidIndex<byte>(destination, num2))
					{
						char c = HexConverter.ToCharUpper((int)scalarValue);
						*destination[num2] = (byte)c;
						scalarValue >>= 4;
						num2--;
					}
					return destination.Length + 4;
				}
				return -1;
			}

			// Token: 0x060001C5 RID: 453 RVA: 0x00006B88 File Offset: 0x00004D88
			[CompilerGenerated]
			internal unsafe static int <EncodeUtf16>g__TryEncodeScalarAsHex|3_0(object @this, uint scalarValue, Span<char> destination)
			{
				int num = BitOperations.Log2(scalarValue) / 4 + 4;
				if (SpanUtility.IsValidIndex<char>(destination, num))
				{
					*destination[num] = ';';
					SpanUtility.TryWriteChars(destination, '&', '#', 'x', '0');
					destination = destination.Slice(3, num - 3);
					int num2 = destination.Length - 1;
					while (SpanUtility.IsValidIndex<char>(destination, num2))
					{
						char c = HexConverter.ToCharUpper((int)scalarValue);
						*destination[num2] = c;
						scalarValue >>= 4;
						num2--;
					}
					return destination.Length + 4;
				}
				return -1;
			}

			// Token: 0x040000EF RID: 239
			internal static readonly DefaultHtmlEncoder.EscaperImplementation Singleton = new DefaultHtmlEncoder.EscaperImplementation();
		}
	}
}
