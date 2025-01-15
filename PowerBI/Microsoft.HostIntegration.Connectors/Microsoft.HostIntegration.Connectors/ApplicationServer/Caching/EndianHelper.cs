using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001C1 RID: 449
	internal static class EndianHelper
	{
		// Token: 0x06000ED3 RID: 3795 RVA: 0x000324C0 File Offset: 0x000306C0
		public static ushort ReverseBytes(ushort value)
		{
			return (ushort)(((int)(value & 255) << 8) | (int)((uint)(value & 65280) >> 8));
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x000324D6 File Offset: 0x000306D6
		public static short ReverseBytes(short value)
		{
			return (short)((((long)value & 255L) << 8) | (((long)value & 65280L) >> 8));
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x000324F0 File Offset: 0x000306F0
		public static uint ReverseBytes(uint value)
		{
			return ((value & 255U) << 24) | ((value & 65280U) << 8) | ((value & 16711680U) >> 8) | ((value & 4278190080U) >> 24);
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x0003251B File Offset: 0x0003071B
		public static int ReverseBytes(int value)
		{
			return (int)((((long)value & 255L) << 24) | (((long)value & 65280L) << 8) | (((long)value & 16711680L) >> 8) | (((long)value & (long)((ulong)(-16777216))) >> 24));
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x00032550 File Offset: 0x00030750
		public static ulong ReverseBytes(ulong value)
		{
			return ((value & 255UL) << 56) | ((value & 65280UL) << 40) | ((value & 16711680UL) << 24) | ((value & (ulong)(-16777216)) << 8) | ((value & 1095216660480UL) >> 8) | ((value & 280375465082880UL) >> 24) | ((value & 71776119061217280UL) >> 40) | ((value & 18374686479671623680UL) >> 56);
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x000325C6 File Offset: 0x000307C6
		public static long ReverseBytes(long value)
		{
			return (long)EndianHelper.ReverseBytes((ulong)value);
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x000325CE File Offset: 0x000307CE
		public static double ReverseBytes(double value)
		{
			return BitConverter.Int64BitsToDouble(EndianHelper.ReverseBytes(BitConverter.DoubleToInt64Bits(value)));
		}

		// Token: 0x04000A2A RID: 2602
		public const bool IsNetworkLittleEndian = false;

		// Token: 0x04000A2B RID: 2603
		public static readonly bool IsHostLittleEndian = BitConverter.IsLittleEndian;
	}
}
