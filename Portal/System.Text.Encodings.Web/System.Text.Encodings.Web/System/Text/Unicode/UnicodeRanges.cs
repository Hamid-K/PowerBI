using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Text.Unicode
{
	// Token: 0x02000021 RID: 33
	[NullableContext(1)]
	[Nullable(0)]
	public static class UnicodeRanges
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00003672 File Offset: 0x00001872
		public static UnicodeRange None
		{
			get
			{
				return UnicodeRanges._none ?? UnicodeRanges.CreateEmptyRange(ref UnicodeRanges._none);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00003687 File Offset: 0x00001887
		public static UnicodeRange All
		{
			get
			{
				return UnicodeRanges._all ?? UnicodeRanges.CreateRange(ref UnicodeRanges._all, '\0', char.MaxValue);
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000036A2 File Offset: 0x000018A2
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static UnicodeRange CreateEmptyRange([NotNull] ref UnicodeRange range)
		{
			Volatile.Write<UnicodeRange>(ref range, new UnicodeRange(0, 0));
			return range;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000036B3 File Offset: 0x000018B3
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static UnicodeRange CreateRange([NotNull] ref UnicodeRange range, char first, char last)
		{
			Volatile.Write<UnicodeRange>(ref range, UnicodeRange.Create(first, last));
			return range;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600009D RID: 157 RVA: 0x000036C4 File Offset: 0x000018C4
		public static UnicodeRange BasicLatin
		{
			get
			{
				return UnicodeRanges._u0000 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u0000, '\0', '\u007f');
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600009E RID: 158 RVA: 0x000036DC File Offset: 0x000018DC
		public static UnicodeRange Latin1Supplement
		{
			get
			{
				return UnicodeRanges._u0080 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u0080, '\u0080', 'ÿ');
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600009F RID: 159 RVA: 0x000036FB File Offset: 0x000018FB
		public static UnicodeRange LatinExtendedA
		{
			get
			{
				return UnicodeRanges._u0100 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u0100, 'Ā', 'ſ');
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x0000371A File Offset: 0x0000191A
		public static UnicodeRange LatinExtendedB
		{
			get
			{
				return UnicodeRanges._u0180 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u0180, 'ƀ', 'ɏ');
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00003739 File Offset: 0x00001939
		public static UnicodeRange IpaExtensions
		{
			get
			{
				return UnicodeRanges._u0250 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u0250, 'ɐ', 'ʯ');
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00003758 File Offset: 0x00001958
		public static UnicodeRange SpacingModifierLetters
		{
			get
			{
				return UnicodeRanges._u02B0 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u02B0, 'ʰ', '\u02ff');
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00003777 File Offset: 0x00001977
		public static UnicodeRange CombiningDiacriticalMarks
		{
			get
			{
				return UnicodeRanges._u0300 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u0300, '\u0300', '\u036f');
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00003796 File Offset: 0x00001996
		public static UnicodeRange GreekandCoptic
		{
			get
			{
				return UnicodeRanges._u0370 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u0370, 'Ͱ', 'Ͽ');
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x000037B5 File Offset: 0x000019B5
		public static UnicodeRange Cyrillic
		{
			get
			{
				return UnicodeRanges._u0400 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u0400, 'Ѐ', 'ӿ');
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x000037D4 File Offset: 0x000019D4
		public static UnicodeRange CyrillicSupplement
		{
			get
			{
				return UnicodeRanges._u0500 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u0500, 'Ԁ', 'ԯ');
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x000037F3 File Offset: 0x000019F3
		public static UnicodeRange Armenian
		{
			get
			{
				return UnicodeRanges._u0530 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u0530, '\u0530', '֏');
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00003812 File Offset: 0x00001A12
		public static UnicodeRange Hebrew
		{
			get
			{
				return UnicodeRanges._u0590 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u0590, '\u0590', '\u05ff');
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00003831 File Offset: 0x00001A31
		public static UnicodeRange Arabic
		{
			get
			{
				return UnicodeRanges._u0600 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u0600, '\u0600', 'ۿ');
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00003850 File Offset: 0x00001A50
		public static UnicodeRange Syriac
		{
			get
			{
				return UnicodeRanges._u0700 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u0700, '܀', 'ݏ');
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000AB RID: 171 RVA: 0x0000386F File Offset: 0x00001A6F
		public static UnicodeRange ArabicSupplement
		{
			get
			{
				return UnicodeRanges._u0750 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u0750, 'ݐ', 'ݿ');
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000AC RID: 172 RVA: 0x0000388E File Offset: 0x00001A8E
		public static UnicodeRange Thaana
		{
			get
			{
				return UnicodeRanges._u0780 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u0780, 'ހ', '\u07bf');
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000AD RID: 173 RVA: 0x000038AD File Offset: 0x00001AAD
		public static UnicodeRange NKo
		{
			get
			{
				return UnicodeRanges._u07C0 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u07C0, '߀', '߿');
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000AE RID: 174 RVA: 0x000038CC File Offset: 0x00001ACC
		public static UnicodeRange Samaritan
		{
			get
			{
				return UnicodeRanges._u0800 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u0800, 'ࠀ', '\u083f');
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000AF RID: 175 RVA: 0x000038EB File Offset: 0x00001AEB
		public static UnicodeRange Mandaic
		{
			get
			{
				return UnicodeRanges._u0840 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u0840, 'ࡀ', '\u085f');
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x0000390A File Offset: 0x00001B0A
		public static UnicodeRange SyriacSupplement
		{
			get
			{
				return UnicodeRanges._u0860 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u0860, 'ࡠ', '\u086f');
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00003929 File Offset: 0x00001B29
		public static UnicodeRange ArabicExtendedB
		{
			get
			{
				return UnicodeRanges._u0870 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u0870, 'ࡰ', '\u089f');
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00003948 File Offset: 0x00001B48
		public static UnicodeRange ArabicExtendedA
		{
			get
			{
				return UnicodeRanges._u08A0 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u08A0, 'ࢠ', '\u08ff');
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00003967 File Offset: 0x00001B67
		public static UnicodeRange Devanagari
		{
			get
			{
				return UnicodeRanges._u0900 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u0900, '\u0900', 'ॿ');
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00003986 File Offset: 0x00001B86
		public static UnicodeRange Bengali
		{
			get
			{
				return UnicodeRanges._u0980 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u0980, 'ঀ', '\u09ff');
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x000039A5 File Offset: 0x00001BA5
		public static UnicodeRange Gurmukhi
		{
			get
			{
				return UnicodeRanges._u0A00 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u0A00, '\u0a00', '\u0a7f');
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x000039C4 File Offset: 0x00001BC4
		public static UnicodeRange Gujarati
		{
			get
			{
				return UnicodeRanges._u0A80 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u0A80, '\u0a80', '\u0aff');
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x000039E3 File Offset: 0x00001BE3
		public static UnicodeRange Oriya
		{
			get
			{
				return UnicodeRanges._u0B00 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u0B00, '\u0b00', '\u0b7f');
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00003A02 File Offset: 0x00001C02
		public static UnicodeRange Tamil
		{
			get
			{
				return UnicodeRanges._u0B80 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u0B80, '\u0b80', '\u0bff');
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00003A21 File Offset: 0x00001C21
		public static UnicodeRange Telugu
		{
			get
			{
				return UnicodeRanges._u0C00 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u0C00, '\u0c00', '౿');
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00003A40 File Offset: 0x00001C40
		public static UnicodeRange Kannada
		{
			get
			{
				return UnicodeRanges._u0C80 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u0C80, 'ಀ', '\u0cff');
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00003A5F File Offset: 0x00001C5F
		public static UnicodeRange Malayalam
		{
			get
			{
				return UnicodeRanges._u0D00 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u0D00, '\u0d00', 'ൿ');
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00003A7E File Offset: 0x00001C7E
		public static UnicodeRange Sinhala
		{
			get
			{
				return UnicodeRanges._u0D80 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u0D80, '\u0d80', '\u0dff');
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00003A9D File Offset: 0x00001C9D
		public static UnicodeRange Thai
		{
			get
			{
				return UnicodeRanges._u0E00 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u0E00, '\u0e00', '\u0e7f');
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00003ABC File Offset: 0x00001CBC
		public static UnicodeRange Lao
		{
			get
			{
				return UnicodeRanges._u0E80 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u0E80, '\u0e80', '\u0eff');
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00003ADB File Offset: 0x00001CDB
		public static UnicodeRange Tibetan
		{
			get
			{
				return UnicodeRanges._u0F00 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u0F00, 'ༀ', '\u0fff');
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00003AFA File Offset: 0x00001CFA
		public static UnicodeRange Myanmar
		{
			get
			{
				return UnicodeRanges._u1000 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u1000, 'က', '႟');
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00003B19 File Offset: 0x00001D19
		public static UnicodeRange Georgian
		{
			get
			{
				return UnicodeRanges._u10A0 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u10A0, 'Ⴀ', 'ჿ');
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00003B38 File Offset: 0x00001D38
		public static UnicodeRange HangulJamo
		{
			get
			{
				return UnicodeRanges._u1100 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u1100, 'ᄀ', 'ᇿ');
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00003B57 File Offset: 0x00001D57
		public static UnicodeRange Ethiopic
		{
			get
			{
				return UnicodeRanges._u1200 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u1200, 'ሀ', '\u137f');
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00003B76 File Offset: 0x00001D76
		public static UnicodeRange EthiopicSupplement
		{
			get
			{
				return UnicodeRanges._u1380 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u1380, 'ᎀ', '\u139f');
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00003B95 File Offset: 0x00001D95
		public static UnicodeRange Cherokee
		{
			get
			{
				return UnicodeRanges._u13A0 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u13A0, 'Ꭰ', '\u13ff');
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00003BB4 File Offset: 0x00001DB4
		public static UnicodeRange UnifiedCanadianAboriginalSyllabics
		{
			get
			{
				return UnicodeRanges._u1400 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u1400, '᐀', 'ᙿ');
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00003BD3 File Offset: 0x00001DD3
		public static UnicodeRange Ogham
		{
			get
			{
				return UnicodeRanges._u1680 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u1680, '\u1680', '\u169f');
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00003BF2 File Offset: 0x00001DF2
		public static UnicodeRange Runic
		{
			get
			{
				return UnicodeRanges._u16A0 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u16A0, 'ᚠ', '\u16ff');
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00003C11 File Offset: 0x00001E11
		public static UnicodeRange Tagalog
		{
			get
			{
				return UnicodeRanges._u1700 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u1700, 'ᜀ', 'ᜟ');
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00003C30 File Offset: 0x00001E30
		public static UnicodeRange Hanunoo
		{
			get
			{
				return UnicodeRanges._u1720 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u1720, 'ᜠ', '\u173f');
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00003C4F File Offset: 0x00001E4F
		public static UnicodeRange Buhid
		{
			get
			{
				return UnicodeRanges._u1740 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u1740, 'ᝀ', '\u175f');
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00003C6E File Offset: 0x00001E6E
		public static UnicodeRange Tagbanwa
		{
			get
			{
				return UnicodeRanges._u1760 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u1760, 'ᝠ', '\u177f');
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00003C8D File Offset: 0x00001E8D
		public static UnicodeRange Khmer
		{
			get
			{
				return UnicodeRanges._u1780 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u1780, 'ក', '\u17ff');
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00003CAC File Offset: 0x00001EAC
		public static UnicodeRange Mongolian
		{
			get
			{
				return UnicodeRanges._u1800 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u1800, '᠀', '\u18af');
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00003CCB File Offset: 0x00001ECB
		public static UnicodeRange UnifiedCanadianAboriginalSyllabicsExtended
		{
			get
			{
				return UnicodeRanges._u18B0 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u18B0, 'ᢰ', '\u18ff');
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00003CEA File Offset: 0x00001EEA
		public static UnicodeRange Limbu
		{
			get
			{
				return UnicodeRanges._u1900 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u1900, 'ᤀ', '᥏');
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00003D09 File Offset: 0x00001F09
		public static UnicodeRange TaiLe
		{
			get
			{
				return UnicodeRanges._u1950 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u1950, 'ᥐ', '\u197f');
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00003D28 File Offset: 0x00001F28
		public static UnicodeRange NewTaiLue
		{
			get
			{
				return UnicodeRanges._u1980 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u1980, 'ᦀ', '᧟');
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00003D47 File Offset: 0x00001F47
		public static UnicodeRange KhmerSymbols
		{
			get
			{
				return UnicodeRanges._u19E0 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u19E0, '᧠', '᧿');
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00003D66 File Offset: 0x00001F66
		public static UnicodeRange Buginese
		{
			get
			{
				return UnicodeRanges._u1A00 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u1A00, 'ᨀ', '᨟');
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00003D85 File Offset: 0x00001F85
		public static UnicodeRange TaiTham
		{
			get
			{
				return UnicodeRanges._u1A20 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u1A20, 'ᨠ', '\u1aaf');
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00003DA4 File Offset: 0x00001FA4
		public static UnicodeRange CombiningDiacriticalMarksExtended
		{
			get
			{
				return UnicodeRanges._u1AB0 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u1AB0, '\u1ab0', '\u1aff');
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00003DC3 File Offset: 0x00001FC3
		public static UnicodeRange Balinese
		{
			get
			{
				return UnicodeRanges._u1B00 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u1B00, '\u1b00', '\u1b7f');
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00003DE2 File Offset: 0x00001FE2
		public static UnicodeRange Sundanese
		{
			get
			{
				return UnicodeRanges._u1B80 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u1B80, '\u1b80', 'ᮿ');
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00003E01 File Offset: 0x00002001
		public static UnicodeRange Batak
		{
			get
			{
				return UnicodeRanges._u1BC0 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u1BC0, 'ᯀ', '᯿');
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00003E20 File Offset: 0x00002020
		public static UnicodeRange Lepcha
		{
			get
			{
				return UnicodeRanges._u1C00 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u1C00, 'ᰀ', 'ᱏ');
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00003E3F File Offset: 0x0000203F
		public static UnicodeRange OlChiki
		{
			get
			{
				return UnicodeRanges._u1C50 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u1C50, '᱐', '᱿');
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00003E5E File Offset: 0x0000205E
		public static UnicodeRange CyrillicExtendedC
		{
			get
			{
				return UnicodeRanges._u1C80 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u1C80, 'ᲀ', '\u1c8f');
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00003E7D File Offset: 0x0000207D
		public static UnicodeRange GeorgianExtended
		{
			get
			{
				return UnicodeRanges._u1C90 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u1C90, 'Ა', 'Ჿ');
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00003E9C File Offset: 0x0000209C
		public static UnicodeRange SundaneseSupplement
		{
			get
			{
				return UnicodeRanges._u1CC0 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u1CC0, '᳀', '\u1ccf');
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00003EBB File Offset: 0x000020BB
		public static UnicodeRange VedicExtensions
		{
			get
			{
				return UnicodeRanges._u1CD0 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u1CD0, '\u1cd0', '\u1cff');
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00003EDA File Offset: 0x000020DA
		public static UnicodeRange PhoneticExtensions
		{
			get
			{
				return UnicodeRanges._u1D00 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u1D00, 'ᴀ', 'ᵿ');
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00003EF9 File Offset: 0x000020F9
		public static UnicodeRange PhoneticExtensionsSupplement
		{
			get
			{
				return UnicodeRanges._u1D80 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u1D80, 'ᶀ', 'ᶿ');
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00003F18 File Offset: 0x00002118
		public static UnicodeRange CombiningDiacriticalMarksSupplement
		{
			get
			{
				return UnicodeRanges._u1DC0 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u1DC0, '\u1dc0', '\u1dff');
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00003F37 File Offset: 0x00002137
		public static UnicodeRange LatinExtendedAdditional
		{
			get
			{
				return UnicodeRanges._u1E00 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u1E00, 'Ḁ', 'ỿ');
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00003F56 File Offset: 0x00002156
		public static UnicodeRange GreekExtended
		{
			get
			{
				return UnicodeRanges._u1F00 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u1F00, 'ἀ', '\u1fff');
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00003F75 File Offset: 0x00002175
		public static UnicodeRange GeneralPunctuation
		{
			get
			{
				return UnicodeRanges._u2000 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u2000, '\u2000', '\u206f');
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00003F94 File Offset: 0x00002194
		public static UnicodeRange SuperscriptsandSubscripts
		{
			get
			{
				return UnicodeRanges._u2070 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u2070, '⁰', '\u209f');
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00003FB3 File Offset: 0x000021B3
		public static UnicodeRange CurrencySymbols
		{
			get
			{
				return UnicodeRanges._u20A0 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u20A0, '₠', '\u20cf');
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00003FD2 File Offset: 0x000021D2
		public static UnicodeRange CombiningDiacriticalMarksforSymbols
		{
			get
			{
				return UnicodeRanges._u20D0 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u20D0, '\u20d0', '\u20ff');
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00003FF1 File Offset: 0x000021F1
		public static UnicodeRange LetterlikeSymbols
		{
			get
			{
				return UnicodeRanges._u2100 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u2100, '℀', '⅏');
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00004010 File Offset: 0x00002210
		public static UnicodeRange NumberForms
		{
			get
			{
				return UnicodeRanges._u2150 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u2150, '⅐', '\u218f');
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060000EB RID: 235 RVA: 0x0000402F File Offset: 0x0000222F
		public static UnicodeRange Arrows
		{
			get
			{
				return UnicodeRanges._u2190 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u2190, '←', '⇿');
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060000EC RID: 236 RVA: 0x0000404E File Offset: 0x0000224E
		public static UnicodeRange MathematicalOperators
		{
			get
			{
				return UnicodeRanges._u2200 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u2200, '∀', '⋿');
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060000ED RID: 237 RVA: 0x0000406D File Offset: 0x0000226D
		public static UnicodeRange MiscellaneousTechnical
		{
			get
			{
				return UnicodeRanges._u2300 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u2300, '⌀', '⏿');
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060000EE RID: 238 RVA: 0x0000408C File Offset: 0x0000228C
		public static UnicodeRange ControlPictures
		{
			get
			{
				return UnicodeRanges._u2400 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u2400, '␀', '\u243f');
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060000EF RID: 239 RVA: 0x000040AB File Offset: 0x000022AB
		public static UnicodeRange OpticalCharacterRecognition
		{
			get
			{
				return UnicodeRanges._u2440 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u2440, '⑀', '\u245f');
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x000040CA File Offset: 0x000022CA
		public static UnicodeRange EnclosedAlphanumerics
		{
			get
			{
				return UnicodeRanges._u2460 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u2460, '①', '⓿');
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x000040E9 File Offset: 0x000022E9
		public static UnicodeRange BoxDrawing
		{
			get
			{
				return UnicodeRanges._u2500 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u2500, '─', '╿');
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00004108 File Offset: 0x00002308
		public static UnicodeRange BlockElements
		{
			get
			{
				return UnicodeRanges._u2580 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u2580, '▀', '▟');
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00004127 File Offset: 0x00002327
		public static UnicodeRange GeometricShapes
		{
			get
			{
				return UnicodeRanges._u25A0 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u25A0, '■', '◿');
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00004146 File Offset: 0x00002346
		public static UnicodeRange MiscellaneousSymbols
		{
			get
			{
				return UnicodeRanges._u2600 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u2600, '☀', '⛿');
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00004165 File Offset: 0x00002365
		public static UnicodeRange Dingbats
		{
			get
			{
				return UnicodeRanges._u2700 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u2700, '✀', '➿');
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00004184 File Offset: 0x00002384
		public static UnicodeRange MiscellaneousMathematicalSymbolsA
		{
			get
			{
				return UnicodeRanges._u27C0 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u27C0, '⟀', '⟯');
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x000041A3 File Offset: 0x000023A3
		public static UnicodeRange SupplementalArrowsA
		{
			get
			{
				return UnicodeRanges._u27F0 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u27F0, '⟰', '⟿');
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x000041C2 File Offset: 0x000023C2
		public static UnicodeRange BraillePatterns
		{
			get
			{
				return UnicodeRanges._u2800 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u2800, '⠀', '⣿');
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x000041E1 File Offset: 0x000023E1
		public static UnicodeRange SupplementalArrowsB
		{
			get
			{
				return UnicodeRanges._u2900 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u2900, '⤀', '⥿');
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00004200 File Offset: 0x00002400
		public static UnicodeRange MiscellaneousMathematicalSymbolsB
		{
			get
			{
				return UnicodeRanges._u2980 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u2980, '⦀', '⧿');
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060000FB RID: 251 RVA: 0x0000421F File Offset: 0x0000241F
		public static UnicodeRange SupplementalMathematicalOperators
		{
			get
			{
				return UnicodeRanges._u2A00 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u2A00, '⨀', '⫿');
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060000FC RID: 252 RVA: 0x0000423E File Offset: 0x0000243E
		public static UnicodeRange MiscellaneousSymbolsandArrows
		{
			get
			{
				return UnicodeRanges._u2B00 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u2B00, '⬀', '⯿');
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060000FD RID: 253 RVA: 0x0000425D File Offset: 0x0000245D
		public static UnicodeRange Glagolitic
		{
			get
			{
				return UnicodeRanges._u2C00 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u2C00, 'Ⰰ', 'ⱟ');
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060000FE RID: 254 RVA: 0x0000427C File Offset: 0x0000247C
		public static UnicodeRange LatinExtendedC
		{
			get
			{
				return UnicodeRanges._u2C60 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u2C60, 'Ⱡ', 'Ɀ');
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060000FF RID: 255 RVA: 0x0000429B File Offset: 0x0000249B
		public static UnicodeRange Coptic
		{
			get
			{
				return UnicodeRanges._u2C80 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u2C80, 'Ⲁ', '⳿');
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000100 RID: 256 RVA: 0x000042BA File Offset: 0x000024BA
		public static UnicodeRange GeorgianSupplement
		{
			get
			{
				return UnicodeRanges._u2D00 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u2D00, 'ⴀ', '\u2d2f');
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000101 RID: 257 RVA: 0x000042D9 File Offset: 0x000024D9
		public static UnicodeRange Tifinagh
		{
			get
			{
				return UnicodeRanges._u2D30 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u2D30, 'ⴰ', '\u2d7f');
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000102 RID: 258 RVA: 0x000042F8 File Offset: 0x000024F8
		public static UnicodeRange EthiopicExtended
		{
			get
			{
				return UnicodeRanges._u2D80 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u2D80, 'ⶀ', '\u2ddf');
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00004317 File Offset: 0x00002517
		public static UnicodeRange CyrillicExtendedA
		{
			get
			{
				return UnicodeRanges._u2DE0 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u2DE0, '\u2de0', '\u2dff');
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00004336 File Offset: 0x00002536
		public static UnicodeRange SupplementalPunctuation
		{
			get
			{
				return UnicodeRanges._u2E00 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u2E00, '⸀', '\u2e7f');
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00004355 File Offset: 0x00002555
		public static UnicodeRange CjkRadicalsSupplement
		{
			get
			{
				return UnicodeRanges._u2E80 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u2E80, '⺀', '\u2eff');
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00004374 File Offset: 0x00002574
		public static UnicodeRange KangxiRadicals
		{
			get
			{
				return UnicodeRanges._u2F00 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u2F00, '⼀', '\u2fdf');
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00004393 File Offset: 0x00002593
		public static UnicodeRange IdeographicDescriptionCharacters
		{
			get
			{
				return UnicodeRanges._u2FF0 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u2FF0, '⿰', '\u2fff');
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000108 RID: 264 RVA: 0x000043B2 File Offset: 0x000025B2
		public static UnicodeRange CjkSymbolsandPunctuation
		{
			get
			{
				return UnicodeRanges._u3000 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u3000, '\u3000', '〿');
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000109 RID: 265 RVA: 0x000043D1 File Offset: 0x000025D1
		public static UnicodeRange Hiragana
		{
			get
			{
				return UnicodeRanges._u3040 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u3040, '\u3040', 'ゟ');
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600010A RID: 266 RVA: 0x000043F0 File Offset: 0x000025F0
		public static UnicodeRange Katakana
		{
			get
			{
				return UnicodeRanges._u30A0 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u30A0, '゠', 'ヿ');
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600010B RID: 267 RVA: 0x0000440F File Offset: 0x0000260F
		public static UnicodeRange Bopomofo
		{
			get
			{
				return UnicodeRanges._u3100 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u3100, '\u3100', 'ㄯ');
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600010C RID: 268 RVA: 0x0000442E File Offset: 0x0000262E
		public static UnicodeRange HangulCompatibilityJamo
		{
			get
			{
				return UnicodeRanges._u3130 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u3130, '\u3130', '\u318f');
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600010D RID: 269 RVA: 0x0000444D File Offset: 0x0000264D
		public static UnicodeRange Kanbun
		{
			get
			{
				return UnicodeRanges._u3190 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u3190, '㆐', '㆟');
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600010E RID: 270 RVA: 0x0000446C File Offset: 0x0000266C
		public static UnicodeRange BopomofoExtended
		{
			get
			{
				return UnicodeRanges._u31A0 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u31A0, 'ㆠ', 'ㆿ');
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600010F RID: 271 RVA: 0x0000448B File Offset: 0x0000268B
		public static UnicodeRange CjkStrokes
		{
			get
			{
				return UnicodeRanges._u31C0 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u31C0, '㇀', '\u31ef');
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000110 RID: 272 RVA: 0x000044AA File Offset: 0x000026AA
		public static UnicodeRange KatakanaPhoneticExtensions
		{
			get
			{
				return UnicodeRanges._u31F0 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u31F0, 'ㇰ', 'ㇿ');
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000111 RID: 273 RVA: 0x000044C9 File Offset: 0x000026C9
		public static UnicodeRange EnclosedCjkLettersandMonths
		{
			get
			{
				return UnicodeRanges._u3200 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u3200, '㈀', '㋿');
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000112 RID: 274 RVA: 0x000044E8 File Offset: 0x000026E8
		public static UnicodeRange CjkCompatibility
		{
			get
			{
				return UnicodeRanges._u3300 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u3300, '㌀', '㏿');
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00004507 File Offset: 0x00002707
		public static UnicodeRange CjkUnifiedIdeographsExtensionA
		{
			get
			{
				return UnicodeRanges._u3400 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u3400, '㐀', '䶿');
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00004526 File Offset: 0x00002726
		public static UnicodeRange YijingHexagramSymbols
		{
			get
			{
				return UnicodeRanges._u4DC0 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u4DC0, '䷀', '䷿');
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00004545 File Offset: 0x00002745
		public static UnicodeRange CjkUnifiedIdeographs
		{
			get
			{
				return UnicodeRanges._u4E00 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._u4E00, '一', '鿿');
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00004564 File Offset: 0x00002764
		public static UnicodeRange YiSyllables
		{
			get
			{
				return UnicodeRanges._uA000 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._uA000, 'ꀀ', '\ua48f');
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00004583 File Offset: 0x00002783
		public static UnicodeRange YiRadicals
		{
			get
			{
				return UnicodeRanges._uA490 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._uA490, '꒐', '\ua4cf');
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000118 RID: 280 RVA: 0x000045A2 File Offset: 0x000027A2
		public static UnicodeRange Lisu
		{
			get
			{
				return UnicodeRanges._uA4D0 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._uA4D0, 'ꓐ', '꓿');
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000119 RID: 281 RVA: 0x000045C1 File Offset: 0x000027C1
		public static UnicodeRange Vai
		{
			get
			{
				return UnicodeRanges._uA500 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._uA500, 'ꔀ', '\ua63f');
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600011A RID: 282 RVA: 0x000045E0 File Offset: 0x000027E0
		public static UnicodeRange CyrillicExtendedB
		{
			get
			{
				return UnicodeRanges._uA640 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._uA640, 'Ꙁ', '\ua69f');
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600011B RID: 283 RVA: 0x000045FF File Offset: 0x000027FF
		public static UnicodeRange Bamum
		{
			get
			{
				return UnicodeRanges._uA6A0 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._uA6A0, 'ꚠ', '\ua6ff');
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600011C RID: 284 RVA: 0x0000461E File Offset: 0x0000281E
		public static UnicodeRange ModifierToneLetters
		{
			get
			{
				return UnicodeRanges._uA700 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._uA700, '\ua700', 'ꜟ');
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600011D RID: 285 RVA: 0x0000463D File Offset: 0x0000283D
		public static UnicodeRange LatinExtendedD
		{
			get
			{
				return UnicodeRanges._uA720 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._uA720, '\ua720', 'ꟿ');
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600011E RID: 286 RVA: 0x0000465C File Offset: 0x0000285C
		public static UnicodeRange SylotiNagri
		{
			get
			{
				return UnicodeRanges._uA800 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._uA800, 'ꠀ', '\ua82f');
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600011F RID: 287 RVA: 0x0000467B File Offset: 0x0000287B
		public static UnicodeRange CommonIndicNumberForms
		{
			get
			{
				return UnicodeRanges._uA830 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._uA830, '꠰', '\ua83f');
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000120 RID: 288 RVA: 0x0000469A File Offset: 0x0000289A
		public static UnicodeRange Phagspa
		{
			get
			{
				return UnicodeRanges._uA840 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._uA840, 'ꡀ', '\ua87f');
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000121 RID: 289 RVA: 0x000046B9 File Offset: 0x000028B9
		public static UnicodeRange Saurashtra
		{
			get
			{
				return UnicodeRanges._uA880 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._uA880, '\ua880', '\ua8df');
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000122 RID: 290 RVA: 0x000046D8 File Offset: 0x000028D8
		public static UnicodeRange DevanagariExtended
		{
			get
			{
				return UnicodeRanges._uA8E0 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._uA8E0, '\ua8e0', '\ua8ff');
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000123 RID: 291 RVA: 0x000046F7 File Offset: 0x000028F7
		public static UnicodeRange KayahLi
		{
			get
			{
				return UnicodeRanges._uA900 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._uA900, '꤀', '꤯');
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00004716 File Offset: 0x00002916
		public static UnicodeRange Rejang
		{
			get
			{
				return UnicodeRanges._uA930 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._uA930, 'ꤰ', '꥟');
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00004735 File Offset: 0x00002935
		public static UnicodeRange HangulJamoExtendedA
		{
			get
			{
				return UnicodeRanges._uA960 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._uA960, 'ꥠ', '\ua97f');
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00004754 File Offset: 0x00002954
		public static UnicodeRange Javanese
		{
			get
			{
				return UnicodeRanges._uA980 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._uA980, '\ua980', '꧟');
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00004773 File Offset: 0x00002973
		public static UnicodeRange MyanmarExtendedB
		{
			get
			{
				return UnicodeRanges._uA9E0 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._uA9E0, 'ꧠ', '\ua9ff');
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00004792 File Offset: 0x00002992
		public static UnicodeRange Cham
		{
			get
			{
				return UnicodeRanges._uAA00 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._uAA00, 'ꨀ', '꩟');
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000129 RID: 297 RVA: 0x000047B1 File Offset: 0x000029B1
		public static UnicodeRange MyanmarExtendedA
		{
			get
			{
				return UnicodeRanges._uAA60 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._uAA60, 'ꩠ', 'ꩿ');
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600012A RID: 298 RVA: 0x000047D0 File Offset: 0x000029D0
		public static UnicodeRange TaiViet
		{
			get
			{
				return UnicodeRanges._uAA80 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._uAA80, 'ꪀ', '꫟');
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600012B RID: 299 RVA: 0x000047EF File Offset: 0x000029EF
		public static UnicodeRange MeeteiMayekExtensions
		{
			get
			{
				return UnicodeRanges._uAAE0 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._uAAE0, 'ꫠ', '\uaaff');
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600012C RID: 300 RVA: 0x0000480E File Offset: 0x00002A0E
		public static UnicodeRange EthiopicExtendedA
		{
			get
			{
				return UnicodeRanges._uAB00 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._uAB00, '\uab00', '\uab2f');
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600012D RID: 301 RVA: 0x0000482D File Offset: 0x00002A2D
		public static UnicodeRange LatinExtendedE
		{
			get
			{
				return UnicodeRanges._uAB30 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._uAB30, 'ꬰ', '\uab6f');
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600012E RID: 302 RVA: 0x0000484C File Offset: 0x00002A4C
		public static UnicodeRange CherokeeSupplement
		{
			get
			{
				return UnicodeRanges._uAB70 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._uAB70, 'ꭰ', 'ꮿ');
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600012F RID: 303 RVA: 0x0000486B File Offset: 0x00002A6B
		public static UnicodeRange MeeteiMayek
		{
			get
			{
				return UnicodeRanges._uABC0 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._uABC0, 'ꯀ', '\uabff');
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000130 RID: 304 RVA: 0x0000488A File Offset: 0x00002A8A
		public static UnicodeRange HangulSyllables
		{
			get
			{
				return UnicodeRanges._uAC00 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._uAC00, '가', '\ud7af');
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000131 RID: 305 RVA: 0x000048A9 File Offset: 0x00002AA9
		public static UnicodeRange HangulJamoExtendedB
		{
			get
			{
				return UnicodeRanges._uD7B0 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._uD7B0, 'ힰ', '\ud7ff');
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000132 RID: 306 RVA: 0x000048C8 File Offset: 0x00002AC8
		public static UnicodeRange CjkCompatibilityIdeographs
		{
			get
			{
				return UnicodeRanges._uF900 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._uF900, '豈', '\ufaff');
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000133 RID: 307 RVA: 0x000048E7 File Offset: 0x00002AE7
		public static UnicodeRange AlphabeticPresentationForms
		{
			get
			{
				return UnicodeRanges._uFB00 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._uFB00, 'ﬀ', 'ﭏ');
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00004906 File Offset: 0x00002B06
		public static UnicodeRange ArabicPresentationFormsA
		{
			get
			{
				return UnicodeRanges._uFB50 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._uFB50, 'ﭐ', '﷿');
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00004925 File Offset: 0x00002B25
		public static UnicodeRange VariationSelectors
		{
			get
			{
				return UnicodeRanges._uFE00 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._uFE00, '\ufe00', '\ufe0f');
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00004944 File Offset: 0x00002B44
		public static UnicodeRange VerticalForms
		{
			get
			{
				return UnicodeRanges._uFE10 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._uFE10, '︐', '\ufe1f');
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00004963 File Offset: 0x00002B63
		public static UnicodeRange CombiningHalfMarks
		{
			get
			{
				return UnicodeRanges._uFE20 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._uFE20, '\ufe20', '\ufe2f');
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00004982 File Offset: 0x00002B82
		public static UnicodeRange CjkCompatibilityForms
		{
			get
			{
				return UnicodeRanges._uFE30 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._uFE30, '︰', '\ufe4f');
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000139 RID: 313 RVA: 0x000049A1 File Offset: 0x00002BA1
		public static UnicodeRange SmallFormVariants
		{
			get
			{
				return UnicodeRanges._uFE50 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._uFE50, '﹐', '\ufe6f');
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600013A RID: 314 RVA: 0x000049C0 File Offset: 0x00002BC0
		public static UnicodeRange ArabicPresentationFormsB
		{
			get
			{
				return UnicodeRanges._uFE70 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._uFE70, 'ﹰ', '\ufeff');
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600013B RID: 315 RVA: 0x000049DF File Offset: 0x00002BDF
		public static UnicodeRange HalfwidthandFullwidthForms
		{
			get
			{
				return UnicodeRanges._uFF00 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._uFF00, '\uff00', '\uffef');
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600013C RID: 316 RVA: 0x000049FE File Offset: 0x00002BFE
		public static UnicodeRange Specials
		{
			get
			{
				return UnicodeRanges._uFFF0 ?? UnicodeRanges.CreateRange(ref UnicodeRanges._uFFF0, '\ufff0', char.MaxValue);
			}
		}

		// Token: 0x04000024 RID: 36
		private static UnicodeRange _none;

		// Token: 0x04000025 RID: 37
		private static UnicodeRange _all;

		// Token: 0x04000026 RID: 38
		private static UnicodeRange _u0000;

		// Token: 0x04000027 RID: 39
		private static UnicodeRange _u0080;

		// Token: 0x04000028 RID: 40
		private static UnicodeRange _u0100;

		// Token: 0x04000029 RID: 41
		private static UnicodeRange _u0180;

		// Token: 0x0400002A RID: 42
		private static UnicodeRange _u0250;

		// Token: 0x0400002B RID: 43
		private static UnicodeRange _u02B0;

		// Token: 0x0400002C RID: 44
		private static UnicodeRange _u0300;

		// Token: 0x0400002D RID: 45
		private static UnicodeRange _u0370;

		// Token: 0x0400002E RID: 46
		private static UnicodeRange _u0400;

		// Token: 0x0400002F RID: 47
		private static UnicodeRange _u0500;

		// Token: 0x04000030 RID: 48
		private static UnicodeRange _u0530;

		// Token: 0x04000031 RID: 49
		private static UnicodeRange _u0590;

		// Token: 0x04000032 RID: 50
		private static UnicodeRange _u0600;

		// Token: 0x04000033 RID: 51
		private static UnicodeRange _u0700;

		// Token: 0x04000034 RID: 52
		private static UnicodeRange _u0750;

		// Token: 0x04000035 RID: 53
		private static UnicodeRange _u0780;

		// Token: 0x04000036 RID: 54
		private static UnicodeRange _u07C0;

		// Token: 0x04000037 RID: 55
		private static UnicodeRange _u0800;

		// Token: 0x04000038 RID: 56
		private static UnicodeRange _u0840;

		// Token: 0x04000039 RID: 57
		private static UnicodeRange _u0860;

		// Token: 0x0400003A RID: 58
		private static UnicodeRange _u0870;

		// Token: 0x0400003B RID: 59
		private static UnicodeRange _u08A0;

		// Token: 0x0400003C RID: 60
		private static UnicodeRange _u0900;

		// Token: 0x0400003D RID: 61
		private static UnicodeRange _u0980;

		// Token: 0x0400003E RID: 62
		private static UnicodeRange _u0A00;

		// Token: 0x0400003F RID: 63
		private static UnicodeRange _u0A80;

		// Token: 0x04000040 RID: 64
		private static UnicodeRange _u0B00;

		// Token: 0x04000041 RID: 65
		private static UnicodeRange _u0B80;

		// Token: 0x04000042 RID: 66
		private static UnicodeRange _u0C00;

		// Token: 0x04000043 RID: 67
		private static UnicodeRange _u0C80;

		// Token: 0x04000044 RID: 68
		private static UnicodeRange _u0D00;

		// Token: 0x04000045 RID: 69
		private static UnicodeRange _u0D80;

		// Token: 0x04000046 RID: 70
		private static UnicodeRange _u0E00;

		// Token: 0x04000047 RID: 71
		private static UnicodeRange _u0E80;

		// Token: 0x04000048 RID: 72
		private static UnicodeRange _u0F00;

		// Token: 0x04000049 RID: 73
		private static UnicodeRange _u1000;

		// Token: 0x0400004A RID: 74
		private static UnicodeRange _u10A0;

		// Token: 0x0400004B RID: 75
		private static UnicodeRange _u1100;

		// Token: 0x0400004C RID: 76
		private static UnicodeRange _u1200;

		// Token: 0x0400004D RID: 77
		private static UnicodeRange _u1380;

		// Token: 0x0400004E RID: 78
		private static UnicodeRange _u13A0;

		// Token: 0x0400004F RID: 79
		private static UnicodeRange _u1400;

		// Token: 0x04000050 RID: 80
		private static UnicodeRange _u1680;

		// Token: 0x04000051 RID: 81
		private static UnicodeRange _u16A0;

		// Token: 0x04000052 RID: 82
		private static UnicodeRange _u1700;

		// Token: 0x04000053 RID: 83
		private static UnicodeRange _u1720;

		// Token: 0x04000054 RID: 84
		private static UnicodeRange _u1740;

		// Token: 0x04000055 RID: 85
		private static UnicodeRange _u1760;

		// Token: 0x04000056 RID: 86
		private static UnicodeRange _u1780;

		// Token: 0x04000057 RID: 87
		private static UnicodeRange _u1800;

		// Token: 0x04000058 RID: 88
		private static UnicodeRange _u18B0;

		// Token: 0x04000059 RID: 89
		private static UnicodeRange _u1900;

		// Token: 0x0400005A RID: 90
		private static UnicodeRange _u1950;

		// Token: 0x0400005B RID: 91
		private static UnicodeRange _u1980;

		// Token: 0x0400005C RID: 92
		private static UnicodeRange _u19E0;

		// Token: 0x0400005D RID: 93
		private static UnicodeRange _u1A00;

		// Token: 0x0400005E RID: 94
		private static UnicodeRange _u1A20;

		// Token: 0x0400005F RID: 95
		private static UnicodeRange _u1AB0;

		// Token: 0x04000060 RID: 96
		private static UnicodeRange _u1B00;

		// Token: 0x04000061 RID: 97
		private static UnicodeRange _u1B80;

		// Token: 0x04000062 RID: 98
		private static UnicodeRange _u1BC0;

		// Token: 0x04000063 RID: 99
		private static UnicodeRange _u1C00;

		// Token: 0x04000064 RID: 100
		private static UnicodeRange _u1C50;

		// Token: 0x04000065 RID: 101
		private static UnicodeRange _u1C80;

		// Token: 0x04000066 RID: 102
		private static UnicodeRange _u1C90;

		// Token: 0x04000067 RID: 103
		private static UnicodeRange _u1CC0;

		// Token: 0x04000068 RID: 104
		private static UnicodeRange _u1CD0;

		// Token: 0x04000069 RID: 105
		private static UnicodeRange _u1D00;

		// Token: 0x0400006A RID: 106
		private static UnicodeRange _u1D80;

		// Token: 0x0400006B RID: 107
		private static UnicodeRange _u1DC0;

		// Token: 0x0400006C RID: 108
		private static UnicodeRange _u1E00;

		// Token: 0x0400006D RID: 109
		private static UnicodeRange _u1F00;

		// Token: 0x0400006E RID: 110
		private static UnicodeRange _u2000;

		// Token: 0x0400006F RID: 111
		private static UnicodeRange _u2070;

		// Token: 0x04000070 RID: 112
		private static UnicodeRange _u20A0;

		// Token: 0x04000071 RID: 113
		private static UnicodeRange _u20D0;

		// Token: 0x04000072 RID: 114
		private static UnicodeRange _u2100;

		// Token: 0x04000073 RID: 115
		private static UnicodeRange _u2150;

		// Token: 0x04000074 RID: 116
		private static UnicodeRange _u2190;

		// Token: 0x04000075 RID: 117
		private static UnicodeRange _u2200;

		// Token: 0x04000076 RID: 118
		private static UnicodeRange _u2300;

		// Token: 0x04000077 RID: 119
		private static UnicodeRange _u2400;

		// Token: 0x04000078 RID: 120
		private static UnicodeRange _u2440;

		// Token: 0x04000079 RID: 121
		private static UnicodeRange _u2460;

		// Token: 0x0400007A RID: 122
		private static UnicodeRange _u2500;

		// Token: 0x0400007B RID: 123
		private static UnicodeRange _u2580;

		// Token: 0x0400007C RID: 124
		private static UnicodeRange _u25A0;

		// Token: 0x0400007D RID: 125
		private static UnicodeRange _u2600;

		// Token: 0x0400007E RID: 126
		private static UnicodeRange _u2700;

		// Token: 0x0400007F RID: 127
		private static UnicodeRange _u27C0;

		// Token: 0x04000080 RID: 128
		private static UnicodeRange _u27F0;

		// Token: 0x04000081 RID: 129
		private static UnicodeRange _u2800;

		// Token: 0x04000082 RID: 130
		private static UnicodeRange _u2900;

		// Token: 0x04000083 RID: 131
		private static UnicodeRange _u2980;

		// Token: 0x04000084 RID: 132
		private static UnicodeRange _u2A00;

		// Token: 0x04000085 RID: 133
		private static UnicodeRange _u2B00;

		// Token: 0x04000086 RID: 134
		private static UnicodeRange _u2C00;

		// Token: 0x04000087 RID: 135
		private static UnicodeRange _u2C60;

		// Token: 0x04000088 RID: 136
		private static UnicodeRange _u2C80;

		// Token: 0x04000089 RID: 137
		private static UnicodeRange _u2D00;

		// Token: 0x0400008A RID: 138
		private static UnicodeRange _u2D30;

		// Token: 0x0400008B RID: 139
		private static UnicodeRange _u2D80;

		// Token: 0x0400008C RID: 140
		private static UnicodeRange _u2DE0;

		// Token: 0x0400008D RID: 141
		private static UnicodeRange _u2E00;

		// Token: 0x0400008E RID: 142
		private static UnicodeRange _u2E80;

		// Token: 0x0400008F RID: 143
		private static UnicodeRange _u2F00;

		// Token: 0x04000090 RID: 144
		private static UnicodeRange _u2FF0;

		// Token: 0x04000091 RID: 145
		private static UnicodeRange _u3000;

		// Token: 0x04000092 RID: 146
		private static UnicodeRange _u3040;

		// Token: 0x04000093 RID: 147
		private static UnicodeRange _u30A0;

		// Token: 0x04000094 RID: 148
		private static UnicodeRange _u3100;

		// Token: 0x04000095 RID: 149
		private static UnicodeRange _u3130;

		// Token: 0x04000096 RID: 150
		private static UnicodeRange _u3190;

		// Token: 0x04000097 RID: 151
		private static UnicodeRange _u31A0;

		// Token: 0x04000098 RID: 152
		private static UnicodeRange _u31C0;

		// Token: 0x04000099 RID: 153
		private static UnicodeRange _u31F0;

		// Token: 0x0400009A RID: 154
		private static UnicodeRange _u3200;

		// Token: 0x0400009B RID: 155
		private static UnicodeRange _u3300;

		// Token: 0x0400009C RID: 156
		private static UnicodeRange _u3400;

		// Token: 0x0400009D RID: 157
		private static UnicodeRange _u4DC0;

		// Token: 0x0400009E RID: 158
		private static UnicodeRange _u4E00;

		// Token: 0x0400009F RID: 159
		private static UnicodeRange _uA000;

		// Token: 0x040000A0 RID: 160
		private static UnicodeRange _uA490;

		// Token: 0x040000A1 RID: 161
		private static UnicodeRange _uA4D0;

		// Token: 0x040000A2 RID: 162
		private static UnicodeRange _uA500;

		// Token: 0x040000A3 RID: 163
		private static UnicodeRange _uA640;

		// Token: 0x040000A4 RID: 164
		private static UnicodeRange _uA6A0;

		// Token: 0x040000A5 RID: 165
		private static UnicodeRange _uA700;

		// Token: 0x040000A6 RID: 166
		private static UnicodeRange _uA720;

		// Token: 0x040000A7 RID: 167
		private static UnicodeRange _uA800;

		// Token: 0x040000A8 RID: 168
		private static UnicodeRange _uA830;

		// Token: 0x040000A9 RID: 169
		private static UnicodeRange _uA840;

		// Token: 0x040000AA RID: 170
		private static UnicodeRange _uA880;

		// Token: 0x040000AB RID: 171
		private static UnicodeRange _uA8E0;

		// Token: 0x040000AC RID: 172
		private static UnicodeRange _uA900;

		// Token: 0x040000AD RID: 173
		private static UnicodeRange _uA930;

		// Token: 0x040000AE RID: 174
		private static UnicodeRange _uA960;

		// Token: 0x040000AF RID: 175
		private static UnicodeRange _uA980;

		// Token: 0x040000B0 RID: 176
		private static UnicodeRange _uA9E0;

		// Token: 0x040000B1 RID: 177
		private static UnicodeRange _uAA00;

		// Token: 0x040000B2 RID: 178
		private static UnicodeRange _uAA60;

		// Token: 0x040000B3 RID: 179
		private static UnicodeRange _uAA80;

		// Token: 0x040000B4 RID: 180
		private static UnicodeRange _uAAE0;

		// Token: 0x040000B5 RID: 181
		private static UnicodeRange _uAB00;

		// Token: 0x040000B6 RID: 182
		private static UnicodeRange _uAB30;

		// Token: 0x040000B7 RID: 183
		private static UnicodeRange _uAB70;

		// Token: 0x040000B8 RID: 184
		private static UnicodeRange _uABC0;

		// Token: 0x040000B9 RID: 185
		private static UnicodeRange _uAC00;

		// Token: 0x040000BA RID: 186
		private static UnicodeRange _uD7B0;

		// Token: 0x040000BB RID: 187
		private static UnicodeRange _uF900;

		// Token: 0x040000BC RID: 188
		private static UnicodeRange _uFB00;

		// Token: 0x040000BD RID: 189
		private static UnicodeRange _uFB50;

		// Token: 0x040000BE RID: 190
		private static UnicodeRange _uFE00;

		// Token: 0x040000BF RID: 191
		private static UnicodeRange _uFE10;

		// Token: 0x040000C0 RID: 192
		private static UnicodeRange _uFE20;

		// Token: 0x040000C1 RID: 193
		private static UnicodeRange _uFE30;

		// Token: 0x040000C2 RID: 194
		private static UnicodeRange _uFE50;

		// Token: 0x040000C3 RID: 195
		private static UnicodeRange _uFE70;

		// Token: 0x040000C4 RID: 196
		private static UnicodeRange _uFF00;

		// Token: 0x040000C5 RID: 197
		private static UnicodeRange _uFFF0;
	}
}
