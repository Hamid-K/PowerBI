using System;
using System.Globalization;
using System.Text;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002FE RID: 766
	internal static class CsHash32
	{
		// Token: 0x06001C6F RID: 7279 RVA: 0x00055DB0 File Offset: 0x00053FB0
		public static void ComputeByte2(byte[] data, uint uSeed1, uint uSeed2, out uint uHash1, out uint uHash2)
		{
			uint num3;
			uint num2;
			uint num = (num2 = (num3 = (uint)((ulong)(-559038737) + (ulong)((long)data.Length) + (ulong)uSeed1)));
			num3 += uSeed2;
			int num4 = 0;
			int i;
			for (i = data.Length; i > 12; i -= 12)
			{
				num2 += BitConverter.ToUInt32(data, num4);
				num += BitConverter.ToUInt32(data, num4 + 4);
				num3 += BitConverter.ToUInt32(data, num4 + 8);
				num2 -= num3;
				num2 ^= (num3 << 4) | (num3 >> 28);
				num3 += num;
				num -= num2;
				num ^= (num2 << 6) | (num2 >> 26);
				num2 += num3;
				num3 -= num;
				num3 ^= (num << 8) | (num >> 24);
				num += num2;
				num2 -= num3;
				num2 ^= (num3 << 16) | (num3 >> 16);
				num3 += num;
				num -= num2;
				num ^= (num2 << 19) | (num2 >> 13);
				num2 += num3;
				num3 -= num;
				num3 ^= (num << 4) | (num >> 28);
				num += num2;
				num4 += 12;
			}
			switch (i)
			{
			case 0:
				uHash1 = num3;
				uHash2 = num;
				return;
			case 1:
				goto IL_01AF;
			case 2:
				goto IL_01A5;
			case 3:
				num2 += (uint)((uint)data[num4 + 2] << 16);
				goto IL_01A5;
			case 4:
				goto IL_018E;
			case 5:
				goto IL_0186;
			case 6:
				goto IL_017C;
			case 7:
				num += (uint)((uint)data[num4 + 6] << 16);
				goto IL_017C;
			case 8:
				goto IL_0159;
			case 9:
				goto IL_0151;
			case 10:
				break;
			case 11:
				num3 += (uint)((uint)data[num4 + 10] << 16);
				break;
			case 12:
				num2 += BitConverter.ToUInt32(data, num4);
				num += BitConverter.ToUInt32(data, num4 + 4);
				num3 += BitConverter.ToUInt32(data, num4 + 8);
				goto IL_01BF;
			default:
				goto IL_01BF;
			}
			num3 += (uint)((uint)data[num4 + 9] << 8);
			IL_0151:
			num3 += (uint)data[num4 + 8];
			IL_0159:
			num += BitConverter.ToUInt32(data, num4 + 4);
			num2 += BitConverter.ToUInt32(data, num4);
			goto IL_01BF;
			IL_017C:
			num += (uint)((uint)data[num4 + 5] << 8);
			IL_0186:
			num += (uint)data[num4 + 4];
			IL_018E:
			num2 += BitConverter.ToUInt32(data, num4);
			goto IL_01BF;
			IL_01A5:
			num2 += (uint)((uint)data[num4 + 1] << 8);
			IL_01AF:
			num2 += (uint)data[num4];
			IL_01BF:
			num3 ^= num;
			num3 -= (num << 14) | (num >> 18);
			num2 ^= num3;
			num2 -= (num3 << 11) | (num3 >> 21);
			num ^= num2;
			num -= (num2 << 25) | (num2 >> 7);
			num3 ^= num;
			num3 -= (num << 16) | (num >> 16);
			num2 ^= num3;
			num2 -= (num3 << 4) | (num3 >> 28);
			num ^= num2;
			num -= (num2 << 14) | (num2 >> 18);
			num3 ^= num;
			num3 -= (num << 24) | (num >> 8);
			uHash1 = num3;
			uHash2 = num;
		}

		// Token: 0x06001C70 RID: 7280 RVA: 0x00055FF0 File Offset: 0x000541F0
		public static uint ComputeByte(byte[] data, uint uSeed)
		{
			uint num = 0U;
			CsHash32.ComputeByte2(data, uSeed, num, out uSeed, out num);
			return uSeed;
		}

		// Token: 0x06001C71 RID: 7281 RVA: 0x0005600C File Offset: 0x0005420C
		public static void ComputeString2(string str, uint uSeed1, uint uSeed2, out uint uHash1, out uint uHash2)
		{
			CsHash32.ComputeByte2(Encoding.UTF8.GetBytes(str.ToUpper(CultureInfo.InvariantCulture)), uSeed1, uSeed2, out uHash1, out uHash2);
		}

		// Token: 0x06001C72 RID: 7282 RVA: 0x00056030 File Offset: 0x00054230
		public static uint ComputeString(string str, uint uSeed, bool ignoreCase)
		{
			uint num = 0U;
			if (ignoreCase)
			{
				CsHash32.ComputeByte2(Encoding.UTF8.GetBytes(str.ToUpper(CultureInfo.InvariantCulture)), uSeed, num, out uSeed, out num);
			}
			else
			{
				CsHash32.ComputeByte2(Encoding.UTF8.GetBytes(str), uSeed, num, out uSeed, out num);
			}
			return uSeed;
		}

		// Token: 0x06001C73 RID: 7283 RVA: 0x0005607C File Offset: 0x0005427C
		public static uint Guid(Guid guid)
		{
			byte[] array = guid.ToByteArray();
			uint num = BitConverter.ToUInt32(array, 0);
			uint num2 = BitConverter.ToUInt32(array, 4);
			uint num3 = BitConverter.ToUInt32(array, 8);
			num -= num3;
			num ^= (num3 << 4) | (num3 >> 28);
			num3 += num2;
			num2 -= num;
			num2 ^= (num << 6) | (num >> 26);
			num += num3;
			num3 -= num2;
			num3 ^= (num2 << 8) | (num2 >> 24);
			num2 += num;
			num -= num3;
			num ^= (num3 << 16) | (num3 >> 16);
			num3 += num2;
			num2 -= num;
			num2 ^= (num << 19) | (num >> 13);
			num += num3;
			num3 -= num2;
			num3 ^= (num2 << 4) | (num2 >> 28);
			num2 += num;
			num ^= BitConverter.ToUInt32(array, 12);
			num3 ^= num2;
			num3 -= (num2 << 14) | (num2 >> 18);
			num ^= num3;
			num -= (num3 << 11) | (num3 >> 21);
			num2 ^= num;
			num2 -= (num << 25) | (num >> 7);
			num3 ^= num2;
			num3 -= (num2 << 16) | (num2 >> 16);
			num ^= num3;
			num -= (num3 << 4) | (num3 >> 28);
			num2 ^= num;
			num2 -= (num << 14) | (num >> 18);
			num3 ^= num2;
			return num3 - ((num2 << 24) | (num2 >> 8));
		}
	}
}
