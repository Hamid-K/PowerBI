using System;

namespace Microsoft.ReportingServices.LegacyOfficeRender
{
	// Token: 0x02000002 RID: 2
	internal class LittleEndian
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		internal static void PutInt(byte[] data, int offset, int val)
		{
			LittleEndian.PutNumber(data, offset, (long)val, 4);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000205C File Offset: 0x0000025C
		internal static void PutInt(byte[] data, int val)
		{
			LittleEndian.PutInt(data, 0, val);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002066 File Offset: 0x00000266
		internal static void PutUShort(byte[] data, int offset, ushort val)
		{
			LittleEndian.PutNumber(data, offset, (long)((ulong)val), 2);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002072 File Offset: 0x00000272
		internal static void PutUShort(byte[] data, ushort val)
		{
			LittleEndian.PutUShort(data, 0, val);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000207C File Offset: 0x0000027C
		internal static void PutShort(byte[] data, int offset, short val)
		{
			LittleEndian.PutNumber(data, offset, (long)val, 2);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002088 File Offset: 0x00000288
		internal static void PutShort(byte[] data, short val)
		{
			LittleEndian.PutShort(data, 0, val);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002092 File Offset: 0x00000292
		internal static int getInt(byte[] data)
		{
			return LittleEndian.getInt(data, 0);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000209B File Offset: 0x0000029B
		internal static int getInt(byte[] data, int offset)
		{
			return (int)LittleEndian.getNumber(data, offset, 4);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020A6 File Offset: 0x000002A6
		internal static ushort getUShort(byte[] data, int offset)
		{
			return (ushort)LittleEndian.getNumber(data, offset, 2);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000020B1 File Offset: 0x000002B1
		internal static short getShort(byte[] data, int offset)
		{
			return (short)LittleEndian.getNumber(data, offset, 2);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020BC File Offset: 0x000002BC
		private static long getNumber(byte[] data, int offset, int size)
		{
			long num = 0L;
			for (int i = offset + size - 1; i >= offset; i--)
			{
				num <<= 8;
				num |= (long)((ulong)(byte.MaxValue & data[i]));
			}
			return num;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000020F0 File Offset: 0x000002F0
		private static void PutNumber(byte[] data, int offset, long val, int size)
		{
			int num = size + offset;
			long num2 = val;
			for (int i = offset; i < num; i++)
			{
				data[i] = (byte)(num2 & 255L);
				num2 >>= 8;
			}
		}
	}
}
