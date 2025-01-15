using System;

namespace Microsoft.Reporting
{
	// Token: 0x020000C4 RID: 196
	internal static class CharacterUtils
	{
		// Token: 0x06000C6D RID: 3181 RVA: 0x00020A45 File Offset: 0x0001EC45
		public static CharacterUtils.UniCatFlags GetUniCatFlags(char ch)
		{
			return (CharacterUtils.UniCatFlags)(1 << (int)char.GetUnicodeCategory(ch));
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x00020A52 File Offset: 0x0001EC52
		public static bool IsLineTerminator(char ch)
		{
			if (ch <= '\r')
			{
				if (ch != '\n' && ch != '\r')
				{
					return false;
				}
			}
			else if (ch != '\u0085' && ch != '\u2028' && ch != '\u2029')
			{
				return false;
			}
			return true;
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x00020A80 File Offset: 0x0001EC80
		public static bool IsDigit(char ch)
		{
			return ch - '0' <= '\t';
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x00020A8D File Offset: 0x0001EC8D
		public static bool IsDigit(char ch, out uint val)
		{
			val = (uint)(ch - '0');
			return val <= 9U;
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x00020A9E File Offset: 0x0001EC9E
		public static uint GetDecVal(char ch)
		{
			return (uint)(ch - '0');
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x00020AA4 File Offset: 0x0001ECA4
		public static bool IsHexDigit(char ch)
		{
			return ch - '0' <= '\t' || ch - 'A' <= '\u0005' || ch - 'a' <= '\u0005';
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x00020AC3 File Offset: 0x0001ECC3
		public static uint GetHexVal(char ch)
		{
			if (ch >= 'a')
			{
				return (uint)(ch - 'W');
			}
			if (ch >= 'A')
			{
				return (uint)(ch - '7');
			}
			return (uint)(ch - '0');
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x00020ADD File Offset: 0x0001ECDD
		public static bool IsFormatCh(char ch)
		{
			return ch >= '\u0080' && (CharacterUtils.GetUniCatFlags(ch) & CharacterUtils.UniCatFlags.Format) > (CharacterUtils.UniCatFlags)0U;
		}

		// Token: 0x020002D1 RID: 721
		[Flags]
		public enum UniCatFlags : uint
		{
			// Token: 0x04001018 RID: 4120
			LowercaseLetter = 2U,
			// Token: 0x04001019 RID: 4121
			UppercaseLetter = 1U,
			// Token: 0x0400101A RID: 4122
			TitlecaseLetter = 4U,
			// Token: 0x0400101B RID: 4123
			ModifierLetter = 8U,
			// Token: 0x0400101C RID: 4124
			OtherLetter = 16U,
			// Token: 0x0400101D RID: 4125
			NonSpacingMark = 32U,
			// Token: 0x0400101E RID: 4126
			SpacingCombiningMark = 64U,
			// Token: 0x0400101F RID: 4127
			DecimalDigitNumber = 256U,
			// Token: 0x04001020 RID: 4128
			LetterNumber = 512U,
			// Token: 0x04001021 RID: 4129
			SpaceSeparator = 2048U,
			// Token: 0x04001022 RID: 4130
			LineSeparator = 4096U,
			// Token: 0x04001023 RID: 4131
			ParagraphSeparator = 8192U,
			// Token: 0x04001024 RID: 4132
			Format = 32768U,
			// Token: 0x04001025 RID: 4133
			Control = 16384U,
			// Token: 0x04001026 RID: 4134
			OtherNotAssigned = 536870912U,
			// Token: 0x04001027 RID: 4135
			PrivateUse = 131072U,
			// Token: 0x04001028 RID: 4136
			Surrogate = 65536U,
			// Token: 0x04001029 RID: 4137
			ConnectorPunctuation = 262144U,
			// Token: 0x0400102A RID: 4138
			IdentStartChar = 543U,
			// Token: 0x0400102B RID: 4139
			IdentPartChar = 295807U
		}
	}
}
