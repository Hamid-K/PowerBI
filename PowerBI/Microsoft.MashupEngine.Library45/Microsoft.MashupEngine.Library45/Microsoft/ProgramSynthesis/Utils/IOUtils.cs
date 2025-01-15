using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x0200049C RID: 1180
	public static class IOUtils
	{
		// Token: 0x06001A8A RID: 6794 RVA: 0x0004FFA4 File Offset: 0x0004E1A4
		public static int RepeatRead(this Stream s, byte[] buffer, int offset, int count)
		{
			int num = 0;
			int num2 = count;
			int num3;
			while (num2 > 0 && (num3 = s.Read(buffer, offset + num, num2)) != 0)
			{
				num += num3;
				num2 -= num3;
			}
			return num;
		}

		// Token: 0x06001A8B RID: 6795 RVA: 0x0004FFD4 File Offset: 0x0004E1D4
		public static byte[] RepeatReadAndAllocate(this Stream s, int count)
		{
			IOUtils.<>c__DisplayClass1_0 CS$<>8__locals1;
			CS$<>8__locals1.count = count;
			byte[] array = new byte[IOUtils.<RepeatReadAndAllocate>g__RoundSize|1_0(4096, ref CS$<>8__locals1)];
			int num = 0;
			for (;;)
			{
				int num2 = array.Length - num;
				int num3 = s.RepeatRead(array, num, num2);
				num += num3;
				if (num3 < num2)
				{
					break;
				}
				if (num == CS$<>8__locals1.count)
				{
					return array;
				}
				Array.Resize<byte>(ref array, IOUtils.<RepeatReadAndAllocate>g__RoundSize|1_0(2 * array.Length, ref CS$<>8__locals1));
			}
			Array.Resize<byte>(ref array, num);
			return array;
		}

		// Token: 0x06001A8C RID: 6796 RVA: 0x00050041 File Offset: 0x0004E241
		[CompilerGenerated]
		internal static int <RepeatReadAndAllocate>g__RoundSize|1_0(int candidate, ref IOUtils.<>c__DisplayClass1_0 A_1)
		{
			if ((double)candidate * 1.5 <= (double)A_1.count)
			{
				return candidate;
			}
			return A_1.count;
		}
	}
}
