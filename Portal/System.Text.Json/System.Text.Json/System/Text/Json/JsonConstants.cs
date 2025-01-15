using System;

namespace System.Text.Json
{
	// Token: 0x0200002F RID: 47
	internal static class JsonConstants
	{
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000227 RID: 551 RVA: 0x000053FA File Offset: 0x000035FA
		public unsafe static ReadOnlySpan<byte> Utf8Bom
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.F1945CD6C19E56B3C1C78943EF5EC18116907A4CA1EFC40A57D48AB1DB7ADFC5), 3);
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00005407 File Offset: 0x00003607
		public unsafe static ReadOnlySpan<byte> TrueValue
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.DEBC2F07DB78D52D2DEF07B7BC620D7042367501D9439A62BA09B559A98E0957), 4);
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00005414 File Offset: 0x00003614
		public unsafe static ReadOnlySpan<byte> FalseValue
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.98151954F217A510702D236DE168CC35D0AB2F99C4479CC9B07EEEDE7EF73A66), 5);
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00005421 File Offset: 0x00003621
		public unsafe static ReadOnlySpan<byte> NullValue
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.97CCCB1B1197F11C6EDBB0D93975220592EF8FAF618C8770A131E4F7DFE567CC), 4);
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0000542E File Offset: 0x0000362E
		public unsafe static ReadOnlySpan<byte> NaNValue
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.5AC07401E4F83150BC0552D68E463CFF181D33C5DBEE917B7C8F7AD508FF81C4), 3);
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600022C RID: 556 RVA: 0x0000543B File Offset: 0x0000363B
		public unsafe static ReadOnlySpan<byte> PositiveInfinityValue
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.13DEB4F81E4D9ED6197EEFBA2DF8CE10439D53D30FBA199F61461E159C624CED), 8);
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600022D RID: 557 RVA: 0x00005448 File Offset: 0x00003648
		public unsafe static ReadOnlySpan<byte> NegativeInfinityValue
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.DD8F6C9CBF007866E6C260785EFAAE6C32A8228CE7CE3CDE5F87B28CDFCD2AC3), 9);
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00005456 File Offset: 0x00003656
		public unsafe static ReadOnlySpan<byte> Delimiters
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.2D3ECB0435B638C234311ED9076E86553842EFB85DA106574F2ED7DA85605804), 8);
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600022F RID: 559 RVA: 0x00005463 File Offset: 0x00003663
		public unsafe static ReadOnlySpan<byte> EscapableChars
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.D99A2E8B9B20C0C331F6C7128C2EA35622C429B103209CADD52E77B45309B8EE), 8);
			}
		}

		// Token: 0x040000D1 RID: 209
		public const string DoubleFormatString = "G17";

		// Token: 0x040000D2 RID: 210
		public const string SingleFormatString = "G9";

		// Token: 0x040000D3 RID: 211
		public const int StackallocByteThreshold = 256;

		// Token: 0x040000D4 RID: 212
		public const int StackallocCharThreshold = 128;

		// Token: 0x040000D5 RID: 213
		public const byte OpenBrace = 123;

		// Token: 0x040000D6 RID: 214
		public const byte CloseBrace = 125;

		// Token: 0x040000D7 RID: 215
		public const byte OpenBracket = 91;

		// Token: 0x040000D8 RID: 216
		public const byte CloseBracket = 93;

		// Token: 0x040000D9 RID: 217
		public const byte Space = 32;

		// Token: 0x040000DA RID: 218
		public const byte CarriageReturn = 13;

		// Token: 0x040000DB RID: 219
		public const byte LineFeed = 10;

		// Token: 0x040000DC RID: 220
		public const byte Tab = 9;

		// Token: 0x040000DD RID: 221
		public const byte ListSeparator = 44;

		// Token: 0x040000DE RID: 222
		public const byte KeyValueSeparator = 58;

		// Token: 0x040000DF RID: 223
		public const byte Quote = 34;

		// Token: 0x040000E0 RID: 224
		public const byte BackSlash = 92;

		// Token: 0x040000E1 RID: 225
		public const byte Slash = 47;

		// Token: 0x040000E2 RID: 226
		public const byte BackSpace = 8;

		// Token: 0x040000E3 RID: 227
		public const byte FormFeed = 12;

		// Token: 0x040000E4 RID: 228
		public const byte Asterisk = 42;

		// Token: 0x040000E5 RID: 229
		public const byte Colon = 58;

		// Token: 0x040000E6 RID: 230
		public const byte Period = 46;

		// Token: 0x040000E7 RID: 231
		public const byte Plus = 43;

		// Token: 0x040000E8 RID: 232
		public const byte Hyphen = 45;

		// Token: 0x040000E9 RID: 233
		public const byte UtcOffsetToken = 90;

		// Token: 0x040000EA RID: 234
		public const byte TimePrefix = 84;

		// Token: 0x040000EB RID: 235
		public const byte StartingByteOfNonStandardSeparator = 226;

		// Token: 0x040000EC RID: 236
		public const int SpacesPerIndent = 2;

		// Token: 0x040000ED RID: 237
		public const int RemoveFlagsBitMask = 2147483647;

		// Token: 0x040000EE RID: 238
		public const int MaxExpansionFactorWhileEscaping = 6;

		// Token: 0x040000EF RID: 239
		public const int MaxExpansionFactorWhileTranscoding = 3;

		// Token: 0x040000F0 RID: 240
		public const long ArrayPoolMaxSizeBeforeUsingNormalAlloc = 1048576L;

		// Token: 0x040000F1 RID: 241
		public const int MaxUtf16RawValueLength = 715827882;

		// Token: 0x040000F2 RID: 242
		public const int MaxEscapedTokenSize = 1000000000;

		// Token: 0x040000F3 RID: 243
		public const int MaxUnescapedTokenSize = 166666666;

		// Token: 0x040000F4 RID: 244
		public const int MaxCharacterTokenSize = 166666666;

		// Token: 0x040000F5 RID: 245
		public const int MaximumFormatBooleanLength = 5;

		// Token: 0x040000F6 RID: 246
		public const int MaximumFormatInt64Length = 20;

		// Token: 0x040000F7 RID: 247
		public const int MaximumFormatUInt64Length = 20;

		// Token: 0x040000F8 RID: 248
		public const int MaximumFormatDoubleLength = 128;

		// Token: 0x040000F9 RID: 249
		public const int MaximumFormatSingleLength = 128;

		// Token: 0x040000FA RID: 250
		public const int MaximumFormatDecimalLength = 31;

		// Token: 0x040000FB RID: 251
		public const int MaximumFormatGuidLength = 36;

		// Token: 0x040000FC RID: 252
		public const int MaximumEscapedGuidLength = 216;

		// Token: 0x040000FD RID: 253
		public const int MaximumFormatDateTimeLength = 27;

		// Token: 0x040000FE RID: 254
		public const int MaximumFormatDateTimeOffsetLength = 33;

		// Token: 0x040000FF RID: 255
		public const int MaxDateTimeUtcOffsetHours = 14;

		// Token: 0x04000100 RID: 256
		public const int DateTimeNumFractionDigits = 7;

		// Token: 0x04000101 RID: 257
		public const int MaxDateTimeFraction = 9999999;

		// Token: 0x04000102 RID: 258
		public const int DateTimeParseNumFractionDigits = 16;

		// Token: 0x04000103 RID: 259
		public const int MaximumDateTimeOffsetParseLength = 42;

		// Token: 0x04000104 RID: 260
		public const int MinimumDateTimeParseLength = 10;

		// Token: 0x04000105 RID: 261
		public const int MaximumEscapedDateTimeOffsetParseLength = 252;

		// Token: 0x04000106 RID: 262
		public const int MaximumLiteralLength = 5;

		// Token: 0x04000107 RID: 263
		public const char HighSurrogateStart = '\ud800';

		// Token: 0x04000108 RID: 264
		public const char HighSurrogateEnd = '\udbff';

		// Token: 0x04000109 RID: 265
		public const char LowSurrogateStart = '\udc00';

		// Token: 0x0400010A RID: 266
		public const char LowSurrogateEnd = '\udfff';

		// Token: 0x0400010B RID: 267
		public const int UnicodePlane01StartValue = 65536;

		// Token: 0x0400010C RID: 268
		public const int HighSurrogateStartValue = 55296;

		// Token: 0x0400010D RID: 269
		public const int HighSurrogateEndValue = 56319;

		// Token: 0x0400010E RID: 270
		public const int LowSurrogateStartValue = 56320;

		// Token: 0x0400010F RID: 271
		public const int LowSurrogateEndValue = 57343;

		// Token: 0x04000110 RID: 272
		public const int BitShiftBy10 = 1024;

		// Token: 0x04000111 RID: 273
		public const int UnboxedParameterCountThreshold = 4;
	}
}
