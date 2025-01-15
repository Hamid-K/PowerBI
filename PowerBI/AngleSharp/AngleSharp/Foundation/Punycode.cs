using System;
using System.Text;
using AngleSharp.Extensions;

namespace AngleSharp.Foundation
{
	// Token: 0x020000E1 RID: 225
	internal static class Punycode
	{
		// Token: 0x0600068C RID: 1676 RVA: 0x00031384 File Offset: 0x0002F584
		public static string Encode(string text)
		{
			if (text.Length == 0)
			{
				return text;
			}
			StringBuilder stringBuilder = new StringBuilder(text.Length);
			int i = 0;
			int num = 0;
			int num2 = 0;
			while (i < text.Length)
			{
				i = text.IndexOfAny(Punycode.possibleDots, num);
				if (i < 0)
				{
					i = text.Length;
				}
				if (i == num)
				{
					break;
				}
				stringBuilder.Append(Punycode.acePrefix);
				int j = 0;
				for (int k = num; k < i; k++)
				{
					if (text[k] < '\u0080')
					{
						stringBuilder.Append(Punycode.EncodeBasic(text[k]));
						j++;
					}
					else if (char.IsSurrogatePair(text, k))
					{
						k++;
					}
				}
				int num3 = j;
				if (num3 == i - num)
				{
					stringBuilder.Remove(num2, Punycode.acePrefix.Length);
				}
				else
				{
					if (text.Length - num >= Punycode.acePrefix.Length && text.Substring(num, Punycode.acePrefix.Length).Equals(Punycode.acePrefix, StringComparison.OrdinalIgnoreCase))
					{
						break;
					}
					int num4 = 0;
					if (num3 > 0)
					{
						stringBuilder.Append('-');
					}
					int num5 = 128;
					int num6 = 0;
					int num7 = 72;
					while (j < i - num)
					{
						int num8 = 134217727;
						int num9;
						for (int l = num; l < i; l += (Punycode.IsSupplementary(num9) ? 2 : 1))
						{
							num9 = text.ConvertToUtf32(l);
							if (num9 >= num5 && num9 < num8)
							{
								num8 = num9;
							}
						}
						num6 += (num8 - num5) * (j - num4 + 1);
						num5 = num8;
						for (int l = num; l < i; l += (Punycode.IsSupplementary(num9) ? 2 : 1))
						{
							num9 = text.ConvertToUtf32(l);
							if (num9 < num5)
							{
								num6++;
							}
							else if (num9 == num5)
							{
								int num10 = num6;
								int num11 = 36;
								for (;;)
								{
									int num12 = ((num11 <= num7) ? 1 : ((num11 >= num7 + 26) ? 26 : (num11 - num7)));
									if (num10 < num12)
									{
										break;
									}
									stringBuilder.Append(Punycode.EncodeDigit(num12 + (num10 - num12) % (36 - num12)));
									num10 = (num10 - num12) / (36 - num12);
									num11 += 36;
								}
								stringBuilder.Append(Punycode.EncodeDigit(num10));
								num7 = Punycode.AdaptChar(num6, j - num4 + 1, j == num3);
								num6 = 0;
								j++;
								if (Punycode.IsSupplementary(num8))
								{
									j++;
									num4++;
								}
							}
						}
						num6++;
						num5++;
					}
				}
				if (stringBuilder.Length - num2 > 63)
				{
					throw new ArgumentException();
				}
				if (i != text.Length)
				{
					stringBuilder.Append(Punycode.possibleDots[0]);
				}
				num = i + 1;
				num2 = stringBuilder.Length;
			}
			int num13 = (Punycode.IsDot(text[text.Length - 1]) ? 0 : 1);
			int num14 = 255 - num13;
			if (stringBuilder.Length > num14)
			{
				stringBuilder.Remove(num14, stringBuilder.Length - num14);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00031685 File Offset: 0x0002F885
		private static bool IsSupplementary(int test)
		{
			return test >= 65536;
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x00031694 File Offset: 0x0002F894
		private static bool IsDot(char c)
		{
			for (int i = 0; i < Punycode.possibleDots.Length; i++)
			{
				if (Punycode.possibleDots[i] == c)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x000316C0 File Offset: 0x0002F8C0
		private static char EncodeDigit(int digit)
		{
			if (digit > 25)
			{
				return (char)(digit + 22);
			}
			return (char)(digit + 97);
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x000316D2 File Offset: 0x0002F8D2
		private static char EncodeBasic(char character)
		{
			if (char.IsUpper(character))
			{
				character += ' ';
			}
			return character;
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x000316E4 File Offset: 0x0002F8E4
		private static int AdaptChar(int delta, int numPoints, bool firstTime)
		{
			delta = (firstTime ? (delta / 700) : (delta / 2));
			delta += delta / numPoints;
			uint num = 0U;
			while (delta > 455)
			{
				delta /= 35;
				num += 36U;
			}
			return (int)((ulong)num + (ulong)((long)(36 * delta / (delta + 38))));
		}

		// Token: 0x04000603 RID: 1539
		private const int PunycodeBase = 36;

		// Token: 0x04000604 RID: 1540
		private const int Tmin = 1;

		// Token: 0x04000605 RID: 1541
		private const int Tmax = 26;

		// Token: 0x04000606 RID: 1542
		private static readonly string acePrefix = "xn--";

		// Token: 0x04000607 RID: 1543
		private static readonly char[] possibleDots = new char[] { '.', '。', '．', '｡' };
	}
}
