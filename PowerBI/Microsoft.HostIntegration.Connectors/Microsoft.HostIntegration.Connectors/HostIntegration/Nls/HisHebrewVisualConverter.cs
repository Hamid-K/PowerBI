using System;

namespace Microsoft.HostIntegration.Nls
{
	// Token: 0x02000630 RID: 1584
	internal class HisHebrewVisualConverter : HisBiDiConverter
	{
		// Token: 0x06003588 RID: 13704 RVA: 0x000B38AF File Offset: 0x000B1AAF
		internal HisHebrewVisualConverter(HisEncoding enc)
			: base(enc)
		{
		}

		// Token: 0x06003589 RID: 13705 RVA: 0x000B38D0 File Offset: 0x000B1AD0
		protected override int GlypIndexToEbcdic(char[] glyphsFromText, int srcIndex, int totalGlyphs, ref byte[] ebcdicBytes, int destIndex)
		{
			int i = srcIndex;
			int num = destIndex;
			while (i < totalGlyphs)
			{
				if (glyphsFromText[i] >= 'ʠ' && glyphsFromText[i] <= 'ˀ')
				{
					ebcdicBytes[num++] = this.hebrewrange[(int)(glyphsFromText[i] - 'ʠ')];
				}
				else
				{
					int num2 = 0;
					bool flag = false;
					while (num2 < 256 && !flag)
					{
						flag = glyphsFromText[i] == this.memoryGlyphs[num2++];
					}
					if (flag)
					{
						num2--;
						ebcdicBytes[num++] = (byte)num2;
					}
					else
					{
						ebcdicBytes[num++] = 111;
					}
				}
				i++;
			}
			return num - destIndex;
		}

		// Token: 0x04001E97 RID: 7831
		private const ushort START = 672;

		// Token: 0x04001E98 RID: 7832
		private const ushort END = 704;

		// Token: 0x04001E99 RID: 7833
		private byte[] hebrewrange = new byte[]
		{
			65, 66, 67, 68, 69, 70, 71, 72, 73, 81,
			82, 83, 84, 85, 86, 87, 88, 89, 98, 99,
			100, 101, 102, 103, 104, 105, 113, 111, 111, 111,
			111, 111, 158
		};
	}
}
