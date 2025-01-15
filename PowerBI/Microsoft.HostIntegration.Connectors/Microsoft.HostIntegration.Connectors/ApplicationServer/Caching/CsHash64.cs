using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002FF RID: 767
	internal static class CsHash64
	{
		// Token: 0x06001C74 RID: 7284 RVA: 0x00056198 File Offset: 0x00054398
		public static ulong ComputeByte(byte[] data, ulong uSeed)
		{
			uint num;
			uint num2;
			CsHash32.ComputeByte2(data, (uint)uSeed, (uint)(uSeed >> 32), out num, out num2);
			return (ulong)num | ((ulong)num2 << 32);
		}

		// Token: 0x06001C75 RID: 7285 RVA: 0x000561C0 File Offset: 0x000543C0
		public static ulong ComputeString(string str, ulong uSeed)
		{
			uint num;
			uint num2;
			CsHash32.ComputeString2(str, (uint)uSeed, (uint)(uSeed >> 32), out num, out num2);
			return (ulong)num | ((ulong)num2 << 32);
		}

		// Token: 0x06001C76 RID: 7286 RVA: 0x000561E8 File Offset: 0x000543E8
		public static ulong Guid(Guid guid)
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
			num3 -= (num2 << 24) | (num2 >> 8);
			return (ulong)num3 | ((ulong)num2 << 32);
		}
	}
}
