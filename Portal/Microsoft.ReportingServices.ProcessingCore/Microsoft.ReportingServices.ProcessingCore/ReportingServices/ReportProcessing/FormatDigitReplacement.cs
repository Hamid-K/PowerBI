using System;
using System.Globalization;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200060B RID: 1547
	internal sealed class FormatDigitReplacement
	{
		// Token: 0x06005545 RID: 21829 RVA: 0x0016774F File Offset: 0x0016594F
		private FormatDigitReplacement()
		{
		}

		// Token: 0x06005546 RID: 21830 RVA: 0x00167757 File Offset: 0x00165957
		internal static char SimpleDigitFromNumeralShape(char asciiDigit, int numeralShape)
		{
			if (asciiDigit < '0' || asciiDigit > '9')
			{
				return asciiDigit;
			}
			if (asciiDigit == '0')
			{
				return FormatDigitReplacement.SimpleDigitMapping[numeralShape][0];
			}
			return FormatDigitReplacement.SimpleDigitMapping[numeralShape][1] + asciiDigit - '1';
		}

		// Token: 0x06005547 RID: 21831 RVA: 0x00167784 File Offset: 0x00165984
		private static string SimpleTranslateNumber(string numberValue, int numeralShape, char numberDecimalSeparator)
		{
			if (numeralShape < 0 || numeralShape > 14)
			{
				return numberValue;
			}
			char[] array = new char[numberValue.Length];
			for (int i = 0; i < numberValue.Length; i++)
			{
				char c = numberValue[i];
				if (c != numberDecimalSeparator)
				{
					array[i] = FormatDigitReplacement.SimpleDigitFromNumeralShape(c, numeralShape);
				}
				else
				{
					array[i] = c;
				}
			}
			return new string(array);
		}

		// Token: 0x06005548 RID: 21832 RVA: 0x001677DB File Offset: 0x001659DB
		private static void SkipNonDigits(string number, ref int index)
		{
			while (index < number.Length)
			{
				if (number[index] >= '0' && number[index] <= '9')
				{
					return;
				}
				index++;
			}
		}

		// Token: 0x06005549 RID: 21833 RVA: 0x0016780C File Offset: 0x00165A0C
		private static string ComplexTranslateNumber(string number, int numeralShape, char numberDecimalSeparator, int numVariant)
		{
			if (numeralShape < 15 || numeralShape > 27)
			{
				return number;
			}
			int i = 0;
			int num = 0;
			int num2 = 0;
			char[] array = new char[2 * number.Length];
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			uint[][] array2;
			if (numeralShape <= 17)
			{
				array2 = FormatDigitReplacement.DBNum_Japanese;
			}
			else if (numeralShape <= 20)
			{
				flag2 = true;
				array2 = FormatDigitReplacement.DBNum_SimplChinese;
			}
			else if (numeralShape <= 23)
			{
				flag2 = true;
				array2 = FormatDigitReplacement.DBNum_TradChinese;
			}
			else
			{
				array2 = FormatDigitReplacement.DBNum_Korean;
				if (numVariant == 0 || numVariant == 3)
				{
					flag3 = true;
				}
			}
			if (numVariant == 1)
			{
				flag3 = true;
			}
			while (i < number.Length && (number[i] < '0' || number[i] > '9'))
			{
				array[num] = number[i];
				i++;
				num++;
			}
			int num3 = i;
			while (num3 < number.Length && number[num3] != numberDecimalSeparator)
			{
				if (number[num3] >= '0' && number[num3] <= '9')
				{
					num2++;
				}
				num3++;
			}
			int num4;
			if (num2 > 12)
			{
				if (num2 > 16)
				{
					while (12 < num2)
					{
						FormatDigitReplacement.SkipNonDigits(number, ref i);
						array[num] = (char)array2[(int)(number[i] - '0')][numVariant];
						i++;
						num++;
						num2--;
					}
				}
				else
				{
					num4 = 16;
					int num5 = 12;
					do
					{
						if (num4 > num2)
						{
							num4--;
							num5--;
						}
						else
						{
							FormatDigitReplacement.SkipNonDigits(number, ref i);
							if (number[i] != '0')
							{
								if (flag2 || flag3 || number[i] > '1' || num4 % 4 == 1)
								{
									if (flag2 && flag)
									{
										array[num] = (char)array2[0][numVariant];
										num++;
										flag = false;
									}
									array[num] = (char)array2[(int)(number[i] - '0')][numVariant];
									num++;
								}
								if (num5 >= 10)
								{
									array[num] = (char)array2[num5][numVariant];
									num++;
								}
							}
							else
							{
								flag = true;
							}
							num4--;
							num5--;
							num2--;
							i++;
						}
					}
					while (num4 > 12);
				}
				array[num] = (char)array2[15][numVariant];
				num++;
			}
			num4 = 12;
			do
			{
				int num5 = 12;
				bool flag4 = false;
				flag = false;
				do
				{
					if (num4 > num2)
					{
						num4--;
						num5--;
					}
					else
					{
						FormatDigitReplacement.SkipNonDigits(number, ref i);
						if (number[i] != '0' || num == 0)
						{
							if (flag2 || flag3 || number[i] > '1' || num4 % 4 == 1)
							{
								if (flag2 && flag)
								{
									array[num] = (char)array2[0][numVariant];
									num++;
									flag = false;
								}
								array[num] = (char)array2[(int)(number[i] - '0')][numVariant];
								num++;
							}
							if (num5 >= 10)
							{
								array[num] = (char)array2[num5][numVariant];
								num++;
							}
							flag4 = true;
						}
						else
						{
							flag = true;
						}
						num4--;
						num5--;
						num2--;
						i++;
					}
				}
				while (num4 % 4 > 0);
				if (flag4 && num2 / 4 > 0)
				{
					if (num2 == 8)
					{
						array[num] = (char)array2[14][numVariant];
						num++;
					}
					else if (num2 == 4)
					{
						array[num] = (char)array2[13][numVariant];
						num++;
					}
				}
			}
			while (num2 > 0);
			if (i < number.Length && number[i] == numberDecimalSeparator)
			{
				array[num] = number[i];
				i++;
				num++;
				while (i < number.Length)
				{
					if (number[i] < '0' || number[i] > '9')
					{
						array[num] = number[i];
					}
					else
					{
						array[num] = (char)array2[(int)(number[i] - '0')][numVariant];
					}
					i++;
					num++;
				}
			}
			return new string(array).TrimEnd(new char[1]);
		}

		// Token: 0x0600554A RID: 21834 RVA: 0x00167B78 File Offset: 0x00165D78
		private static int GetNumeralShape(int numeralVariant, CultureInfo numeralLanguage)
		{
			if (numeralLanguage == null)
			{
				return 0;
			}
			if (numeralVariant < 3)
			{
				return 0;
			}
			int lcid = numeralLanguage.LCID;
			if (numeralVariant == 7 && (lcid & 255) == 18)
			{
				return 27;
			}
			if (numeralVariant == 4)
			{
				if ((lcid & 255) == 18)
				{
					return 24;
				}
				if ((lcid & 255) == 17)
				{
					return 15;
				}
				if ((lcid & 255) == 4)
				{
					if (lcid == 31748)
					{
						return 21;
					}
					return 18;
				}
			}
			else if (numeralVariant == 5)
			{
				if ((lcid & 255) == 18)
				{
					return 25;
				}
				if ((lcid & 255) == 17)
				{
					return 16;
				}
				if ((lcid & 255) == 4)
				{
					if (lcid == 31748)
					{
						return 22;
					}
					return 19;
				}
			}
			else if (numeralVariant == 6)
			{
				if ((lcid & 255) == 18)
				{
					return 26;
				}
				if ((lcid & 255) == 17)
				{
					return 17;
				}
				if ((lcid & 255) == 4)
				{
					if (lcid == 31748)
					{
						return 23;
					}
					return 20;
				}
			}
			else if (numeralVariant == 3)
			{
				if (lcid == 1108)
				{
					return 13;
				}
				if (lcid == 1105)
				{
					return 14;
				}
				if (lcid == 1096)
				{
					return 7;
				}
				if (lcid == 1093)
				{
					return 4;
				}
				if ((lcid & 255) == 1)
				{
					return 1;
				}
				if ((lcid & 255) == 32 || (lcid & 255) == 41)
				{
					return 2;
				}
				if ((lcid & 255) == 57 || (lcid & 255) == 87 || (lcid & 255) == 78 || (lcid & 255) == 79)
				{
					return 3;
				}
				if ((lcid & 255) == 70)
				{
					return 5;
				}
				if ((lcid & 255) == 71)
				{
					return 6;
				}
				if ((lcid & 255) == 73)
				{
					return 8;
				}
				if ((lcid & 255) == 74)
				{
					return 9;
				}
				if ((lcid & 255) == 75)
				{
					return 10;
				}
				if ((lcid & 255) == 62)
				{
					return 11;
				}
				if ((lcid & 255) == 30)
				{
					return 12;
				}
			}
			return 0;
		}

		// Token: 0x0600554B RID: 21835 RVA: 0x00167D40 File Offset: 0x00165F40
		internal static string FormatNumeralVariant(string number, int numeralVariant, CultureInfo numeralLanguage, string numberDecimalSeparator, out bool numberTranslated)
		{
			numberTranslated = true;
			if (number == null || number == string.Empty)
			{
				return number;
			}
			int numeralShape = FormatDigitReplacement.GetNumeralShape(numeralVariant, numeralLanguage);
			if (numeralShape == 0)
			{
				numberTranslated = false;
				return number;
			}
			char c = '.';
			if (numberDecimalSeparator != null && numberDecimalSeparator != string.Empty)
			{
				c = numberDecimalSeparator[0];
			}
			string text;
			if (numeralVariant >= 4)
			{
				text = FormatDigitReplacement.ComplexTranslateNumber(number, numeralShape, c, numeralVariant - 4);
			}
			else
			{
				text = FormatDigitReplacement.SimpleTranslateNumber(number, numeralShape, c);
			}
			return text;
		}

		// Token: 0x04002D25 RID: 11557
		private const int DbnumHundred = 11;

		// Token: 0x04002D26 RID: 11558
		private const int DbnumThousand = 12;

		// Token: 0x04002D27 RID: 11559
		private const int DbnumTenThousand = 13;

		// Token: 0x04002D28 RID: 11560
		private const int DbnumHundredMillion = 14;

		// Token: 0x04002D29 RID: 11561
		private const int DbnumTrillion = 15;

		// Token: 0x04002D2A RID: 11562
		private const int NUM_ASCII = 0;

		// Token: 0x04002D2B RID: 11563
		private const int NUM_ARABIC_INDIC = 1;

		// Token: 0x04002D2C RID: 11564
		private const int NUM_EXTENDED_ARABIC_INDIC = 2;

		// Token: 0x04002D2D RID: 11565
		private const int NUM_DEVANAGARI = 3;

		// Token: 0x04002D2E RID: 11566
		private const int NUM_BENGALI = 4;

		// Token: 0x04002D2F RID: 11567
		private const int NUM_GURMUKHI = 5;

		// Token: 0x04002D30 RID: 11568
		private const int NUM_GUJARATI = 6;

		// Token: 0x04002D31 RID: 11569
		private const int NUM_ORIYA = 7;

		// Token: 0x04002D32 RID: 11570
		private const int NUM_TAMIL = 8;

		// Token: 0x04002D33 RID: 11571
		private const int NUM_TELUGU = 9;

		// Token: 0x04002D34 RID: 11572
		private const int NUM_KANNADA = 10;

		// Token: 0x04002D35 RID: 11573
		private const int NUM_MALAYALAM = 11;

		// Token: 0x04002D36 RID: 11574
		private const int NUM_THAI = 12;

		// Token: 0x04002D37 RID: 11575
		private const int NUM_LAO = 13;

		// Token: 0x04002D38 RID: 11576
		private const int NUM_TIBETAN = 14;

		// Token: 0x04002D39 RID: 11577
		private const int NUM_JAPANESE1 = 15;

		// Token: 0x04002D3A RID: 11578
		private const int NUM_JAPANESE2 = 16;

		// Token: 0x04002D3B RID: 11579
		private const int NUM_JAPANESE3 = 17;

		// Token: 0x04002D3C RID: 11580
		private const int NUM_CHINESE_SIMP1 = 18;

		// Token: 0x04002D3D RID: 11581
		private const int NUM_CHINESE_SIMP2 = 19;

		// Token: 0x04002D3E RID: 11582
		private const int NUM_CHINESE_SIMP3 = 20;

		// Token: 0x04002D3F RID: 11583
		private const int NUM_CHINESE_TRAD1 = 21;

		// Token: 0x04002D40 RID: 11584
		private const int NUM_CHINESE_TRAD2 = 22;

		// Token: 0x04002D41 RID: 11585
		private const int NUM_CHINESE_TRAD3 = 23;

		// Token: 0x04002D42 RID: 11586
		private const int NUM_KOREAN1 = 24;

		// Token: 0x04002D43 RID: 11587
		private const int NUM_KOREAN2 = 25;

		// Token: 0x04002D44 RID: 11588
		private const int NUM_KOREAN3 = 26;

		// Token: 0x04002D45 RID: 11589
		private const int NUM_KOREAN4 = 27;

		// Token: 0x04002D46 RID: 11590
		internal static uint[][] DBNum_Japanese = new uint[][]
		{
			new uint[] { 12295U, 12295U, 65296U, 0U },
			new uint[] { 19968U, 22769U, 65297U, 0U },
			new uint[] { 20108U, 24336U, 65298U, 0U },
			new uint[] { 19977U, 21442U, 65299U, 0U },
			new uint[] { 22235U, 22235U, 65300U, 0U },
			new uint[] { 20116U, 20237U, 65301U, 0U },
			new uint[] { 20845U, 20845U, 65302U, 0U },
			new uint[] { 19971U, 19971U, 65303U, 0U },
			new uint[] { 20843U, 20843U, 65304U, 0U },
			new uint[] { 20061U, 20061U, 65305U, 0U },
			new uint[] { 21313U, 25342U, 21313U, 0U },
			new uint[] { 30334U, 30334U, 30334U, 0U },
			new uint[] { 21315U, 38433U, 21315U, 0U },
			new uint[] { 19975U, 33836U, 19975U, 0U },
			new uint[] { 20740U, 20740U, 20740U, 0U },
			new uint[] { 20806U, 20806U, 20806U, 0U }
		};

		// Token: 0x04002D47 RID: 11591
		internal static uint[][] DBNum_Korean = new uint[][]
		{
			new uint[] { 65296U, 63922U, 65296U, 50689U },
			new uint[] { 19968U, 22777U, 65297U, 51068U },
			new uint[] { 20108U, 36019U, 65298U, 51060U },
			new uint[] { 19977U, 63851U, 65299U, 49340U },
			new uint[] { 22235U, 22235U, 65300U, 49324U },
			new uint[] { 20116U, 20237U, 65301U, 50724U },
			new uint[] { 63953U, 63953U, 65302U, 50977U },
			new uint[] { 19971U, 19971U, 65303U, 52832U },
			new uint[] { 20843U, 20843U, 65304U, 54036U },
			new uint[] { 20061U, 20061U, 65305U, 44396U },
			new uint[] { 21313U, 63859U, 21313U, 49901U },
			new uint[] { 30334U, 30334U, 30334U, 48177U },
			new uint[] { 21315U, 38433U, 21315U, 52380U },
			new uint[] { 19975U, 33836U, 19975U, 47564U },
			new uint[] { 20740U, 20740U, 20740U, 50613U },
			new uint[] { 20806U, 20806U, 20806U, 51312U }
		};

		// Token: 0x04002D48 RID: 11592
		internal static uint[][] DBNum_SimplChinese = new uint[][]
		{
			new uint[] { 9675U, 38646U, 65296U, 0U },
			new uint[] { 19968U, 22777U, 65297U, 0U },
			new uint[] { 20108U, 36144U, 65298U, 0U },
			new uint[] { 19977U, 21441U, 65299U, 0U },
			new uint[] { 22235U, 32902U, 65300U, 0U },
			new uint[] { 20116U, 20237U, 65301U, 0U },
			new uint[] { 20845U, 38470U, 65302U, 0U },
			new uint[] { 19971U, 26578U, 65303U, 0U },
			new uint[] { 20843U, 25420U, 65304U, 0U },
			new uint[] { 20061U, 29590U, 65305U, 0U },
			new uint[] { 21313U, 25342U, 21313U, 0U },
			new uint[] { 30334U, 20336U, 30334U, 0U },
			new uint[] { 21315U, 20191U, 21315U, 0U },
			new uint[] { 19975U, 19975U, 19975U, 0U },
			new uint[] { 20159U, 20159U, 20159U, 0U },
			new uint[] { 20806U, 20806U, 20806U, 0U }
		};

		// Token: 0x04002D49 RID: 11593
		internal static uint[][] DBNum_TradChinese = new uint[][]
		{
			new uint[] { 9675U, 38646U, 65296U, 0U },
			new uint[] { 19968U, 22777U, 65297U, 0U },
			new uint[] { 20108U, 36019U, 65298U, 0U },
			new uint[] { 19977U, 21443U, 65299U, 0U },
			new uint[] { 22235U, 32902U, 65300U, 0U },
			new uint[] { 20116U, 20237U, 65301U, 0U },
			new uint[] { 20845U, 38520U, 65302U, 0U },
			new uint[] { 19971U, 26578U, 65303U, 0U },
			new uint[] { 20843U, 25420U, 65304U, 0U },
			new uint[] { 20061U, 29590U, 65305U, 0U },
			new uint[] { 21313U, 25342U, 21313U, 0U },
			new uint[] { 30334U, 20336U, 30334U, 0U },
			new uint[] { 21315U, 20191U, 21315U, 0U },
			new uint[] { 33836U, 33836U, 33836U, 0U },
			new uint[] { 20740U, 20740U, 20740U, 0U },
			new uint[] { 20806U, 20806U, 20806U, 0U }
		};

		// Token: 0x04002D4A RID: 11594
		internal static char[][] SimpleDigitMapping = new char[][]
		{
			new char[] { '0', '1' },
			new char[] { '٠', '١' },
			new char[] { '۰', '۱' },
			new char[] { '०', '१' },
			new char[] { '০', '১' },
			new char[] { '੦', '੧' },
			new char[] { '૦', '૧' },
			new char[] { '୦', '୧' },
			new char[] { '0', '௧' },
			new char[] { '౦', '౧' },
			new char[] { '೦', '೧' },
			new char[] { '൦', '൧' },
			new char[] { '๐', '๑' },
			new char[] { '໐', '໑' },
			new char[] { '༠', '༡' }
		};
	}
}
