using System;

namespace Microsoft.HostIntegration.Nls
{
	// Token: 0x02000613 RID: 1555
	internal class HisArabicVisualConverter : HisBiDiConverter
	{
		// Token: 0x06003495 RID: 13461 RVA: 0x000AF0E6 File Offset: 0x000AD2E6
		internal HisArabicVisualConverter(HisEncoding enc)
			: base(enc)
		{
		}

		// Token: 0x06003496 RID: 13462 RVA: 0x000AF108 File Offset: 0x000AD308
		protected override int GlypIndexToEbcdic(char[] glyphsFromText, int srcIndex, int totalGlyphs, ref byte[] ebcdicBytes, int destIndex)
		{
			int i = srcIndex;
			int num = destIndex;
			int num2 = 0;
			byte[] array = ebcdicBytes;
			while (i < totalGlyphs)
			{
				if (glyphsFromText[i] >= 'Ϳ' && glyphsFromText[i] <= 'ϼ')
				{
					byte b = this.arabicrange[(int)(glyphsFromText[i] - 'Ϳ')];
					if (b <= 139)
					{
						if (b == 119 || b == 128 || b == 139)
						{
							goto IL_007E;
						}
					}
					else
					{
						if (b == 141)
						{
							goto IL_007E;
						}
						if (b != 186)
						{
							if (b - 218 <= 1)
							{
								if (glyphsFromText[i] == 'Ή' || glyphsFromText[i] == 'Ί')
								{
									array[num++] = 70;
									num2++;
								}
							}
						}
						else if (glyphsFromText[i] == 'Ϳ')
						{
							array[num++] = 191;
							array[num++] = 186;
							num2 += 2;
						}
					}
					IL_00D6:
					array[num++] = this.arabicrange[(int)(glyphsFromText[i] - 'Ϳ')];
					goto IL_01BC;
					IL_007E:
					array[num++] = 69;
					num2++;
					goto IL_00D6;
				}
				uint num3 = 0U;
				bool flag = false;
				while (num3 < 256U && !flag)
				{
					flag = glyphsFromText[i] == this.memoryGlyphs[(int)num3++];
				}
				if (flag)
				{
					num3 -= 1U;
					array[num++] = (byte)num3;
				}
				else
				{
					int num4 = (int)glyphsFromText[i];
					if (num4 <= 760)
					{
						if (num4 - 753 > 5 && num4 != 760)
						{
							goto IL_01B3;
						}
					}
					else if (num4 - 839 > 1 && num4 - 842 > 1)
					{
						if (num4 != 2927)
						{
							goto IL_01B3;
						}
						array[num++] = 191;
						array[num++] = 186;
						array[num++] = 186;
						array[num++] = 86;
						num2 += 4;
						goto IL_01BC;
					}
					num2--;
					goto IL_01BC;
					IL_01B3:
					array[num++] = 111;
				}
				IL_01BC:
				i++;
			}
			if (num2 > 0)
			{
				int num5 = 0;
				while (num5 < num2 && array[num5 + destIndex] == 64)
				{
					num5++;
				}
				if (num5 == num2)
				{
					Array.Copy(array, num2 + destIndex, array, destIndex, num - destIndex - num2);
					num -= num2;
				}
			}
			return num - destIndex;
		}

		// Token: 0x04001D9D RID: 7581
		private const ushort START = 895;

		// Token: 0x04001D9E RID: 7582
		private const ushort END = 1020;

		// Token: 0x04001D9F RID: 7583
		private byte[] arabicrange = new byte[]
		{
			186, 70, 71, 72, 73, 81, 82, 82, 86, 86,
			218, 219, 85, 85, 86, 87, 88, 88, 89, 89,
			98, 98, 99, 99, 100, 100, 101, 101, 102, 102,
			103, 103, 104, 104, 105, 105, 112, 112, 113, 113,
			114, 114, 115, 115, 116, 116, 117, 117, 118, 118,
			119, 119, 120, 120, 128, 128, 138, 138, 139, 139,
			140, 140, 141, 141, 142, 142, 143, 143, 143, 143,
			144, 144, 144, 144, 154, 155, 156, 157, 158, 159,
			160, 170, 171, 171, 172, 172, 173, 173, 174, 174,
			175, 175, 176, 176, 177, 177, 186, 186, 187, 187,
			188, 188, 189, 189, 190, 190, 191, 191, 203, 205,
			207, 207, 218, 219, 220, 221, 222, 222, 178, 179,
			180, 181, 184, 185, 184, 185
		};
	}
}
