using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001262 RID: 4706
	internal static class BinaryFormat
	{
		// Token: 0x06007BF9 RID: 31737 RVA: 0x001AAE94 File Offset: 0x001A9094
		public static short Int16FromLittleEndian(int byte0, int byte1)
		{
			return (short)((byte1 << 8) | byte0);
		}

		// Token: 0x06007BFA RID: 31738 RVA: 0x001AAE9C File Offset: 0x001A909C
		public static void Int16ToLittleEndian(int value, out byte byte0, out byte byte1)
		{
			byte0 = BinaryFormat.GetByte(value, 0);
			byte1 = BinaryFormat.GetByte(value, 1);
		}

		// Token: 0x06007BFB RID: 31739 RVA: 0x001AAEB0 File Offset: 0x001A90B0
		public static short Int16FromBigEndian(int byte0, int byte1)
		{
			return (short)((byte0 << 8) | byte1);
		}

		// Token: 0x06007BFC RID: 31740 RVA: 0x001AAEB8 File Offset: 0x001A90B8
		public static void Int16ToBigEndian(int value, out byte byte0, out byte byte1)
		{
			byte0 = BinaryFormat.GetByte(value, 1);
			byte1 = BinaryFormat.GetByte(value, 0);
		}

		// Token: 0x06007BFD RID: 31741 RVA: 0x001AAECC File Offset: 0x001A90CC
		public static int Int32FromLittleEndian(int byte0, int byte1, int byte2, int byte3)
		{
			return (byte3 << 24) | (byte2 << 16) | (byte1 << 8) | byte0;
		}

		// Token: 0x06007BFE RID: 31742 RVA: 0x001AAEDD File Offset: 0x001A90DD
		public static void Int32ToLittleEndian(int value, out byte byte0, out byte byte1, out byte byte2, out byte byte3)
		{
			byte0 = BinaryFormat.GetByte(value, 0);
			byte1 = BinaryFormat.GetByte(value, 1);
			byte2 = BinaryFormat.GetByte(value, 2);
			byte3 = BinaryFormat.GetByte(value, 3);
		}

		// Token: 0x06007BFF RID: 31743 RVA: 0x001AAF04 File Offset: 0x001A9104
		public static int Int32FromBigEndian(int byte0, int byte1, int byte2, int byte3)
		{
			return (byte0 << 24) | (byte1 << 16) | (byte2 << 8) | byte3;
		}

		// Token: 0x06007C00 RID: 31744 RVA: 0x001AAF15 File Offset: 0x001A9115
		public static void Int32ToBigEndian(int value, out byte byte0, out byte byte1, out byte byte2, out byte byte3)
		{
			byte0 = BinaryFormat.GetByte(value, 3);
			byte1 = BinaryFormat.GetByte(value, 2);
			byte2 = BinaryFormat.GetByte(value, 1);
			byte3 = BinaryFormat.GetByte(value, 0);
		}

		// Token: 0x06007C01 RID: 31745 RVA: 0x001AAF3C File Offset: 0x001A913C
		public static long Int64FromLittleEndian(int int0, int int1)
		{
			return (long)(((ulong)int1 << 32) | (ulong)int0);
		}

		// Token: 0x06007C02 RID: 31746 RVA: 0x001AAF46 File Offset: 0x001A9146
		public static long Int64FromLittleEndian(long byte0, long byte1, long byte2, long byte3, long byte4, long byte5, long byte6, long byte7)
		{
			return (byte7 << 56) | (byte6 << 48) | (byte5 << 40) | (byte4 << 32) | (byte3 << 24) | (byte2 << 16) | (byte1 << 8) | byte0;
		}

		// Token: 0x06007C03 RID: 31747 RVA: 0x001AAF70 File Offset: 0x001A9170
		public static void Int64ToLittleEndian(long value, out byte byte0, out byte byte1, out byte byte2, out byte byte3, out byte byte4, out byte byte5, out byte byte6, out byte byte7)
		{
			byte0 = BinaryFormat.GetByte(value, 0);
			byte1 = BinaryFormat.GetByte(value, 1);
			byte2 = BinaryFormat.GetByte(value, 2);
			byte3 = BinaryFormat.GetByte(value, 3);
			byte4 = BinaryFormat.GetByte(value, 4);
			byte5 = BinaryFormat.GetByte(value, 5);
			byte6 = BinaryFormat.GetByte(value, 6);
			byte7 = BinaryFormat.GetByte(value, 7);
		}

		// Token: 0x06007C04 RID: 31748 RVA: 0x00110574 File Offset: 0x0010E774
		public static long Int64FromBigEndian(int int0, int int1)
		{
			return (long)(((ulong)int0 << 32) | (ulong)int1);
		}

		// Token: 0x06007C05 RID: 31749 RVA: 0x001AAFCA File Offset: 0x001A91CA
		public static long Int64FromBigEndian(long byte0, long byte1, long byte2, long byte3, long byte4, long byte5, long byte6, long byte7)
		{
			return (byte0 << 56) | (byte1 << 48) | (byte2 << 40) | (byte3 << 32) | (byte4 << 24) | (byte5 << 16) | (byte6 << 8) | byte7;
		}

		// Token: 0x06007C06 RID: 31750 RVA: 0x001AAFF4 File Offset: 0x001A91F4
		public static void Int64ToBigEndian(long value, out byte byte0, out byte byte1, out byte byte2, out byte byte3, out byte byte4, out byte byte5, out byte byte6, out byte byte7)
		{
			byte0 = BinaryFormat.GetByte(value, 7);
			byte1 = BinaryFormat.GetByte(value, 6);
			byte2 = BinaryFormat.GetByte(value, 5);
			byte3 = BinaryFormat.GetByte(value, 4);
			byte4 = BinaryFormat.GetByte(value, 3);
			byte5 = BinaryFormat.GetByte(value, 2);
			byte6 = BinaryFormat.GetByte(value, 1);
			byte7 = BinaryFormat.GetByte(value, 0);
		}

		// Token: 0x06007C07 RID: 31751 RVA: 0x001AB04E File Offset: 0x001A924E
		private static byte GetByte(int value, int index)
		{
			return (byte)(((uint)value >> 8 * index) & 255U);
		}

		// Token: 0x06007C08 RID: 31752 RVA: 0x001AB05F File Offset: 0x001A925F
		private static byte GetByte(long value, int index)
		{
			return (byte)(((ulong)value >> 8 * index) & 255UL);
		}
	}
}
