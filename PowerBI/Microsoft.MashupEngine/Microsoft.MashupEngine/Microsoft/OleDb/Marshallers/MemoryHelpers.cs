using System;

namespace Microsoft.OleDb.Marshallers
{
	// Token: 0x02001FD3 RID: 8147
	public static class MemoryHelpers
	{
		// Token: 0x0600C709 RID: 50953 RVA: 0x0027A4F9 File Offset: 0x002786F9
		public static void ZeroMemory(IntPtr destination, int lengthBytes)
		{
			MemoryHelpers.Memset(destination, lengthBytes, 0);
		}

		// Token: 0x0600C70A RID: 50954 RVA: 0x0027A504 File Offset: 0x00278704
		public unsafe static void Memset(IntPtr destination, int lengthBytes, byte value)
		{
			byte* ptr = (byte*)(void*)destination;
			int i = lengthBytes;
			if ((long)i >= MemoryHelpers.QuickLengthThreshold && IntPtr.Size <= 8)
			{
				long* ptr2 = ((long)destination / 8L + 7L) & -8L;
				long num = (long)((ulong)value);
				if (num != 0L)
				{
					num = (num << 8) | num;
					num = (num << 16) | num;
					num = (num << 32) | num;
				}
				while (ptr != (byte*)ptr2)
				{
					*ptr = value;
					i--;
					ptr++;
				}
				while (i >= 8)
				{
					*ptr2 = num;
					i -= 8;
					ptr2++;
				}
				ptr = (byte*)ptr2;
			}
			while (i > 0)
			{
				*ptr = value;
				i--;
				ptr++;
			}
		}

		// Token: 0x0600C70B RID: 50955 RVA: 0x0027A58C File Offset: 0x0027878C
		public unsafe static bool Memcmp(byte* bytes1, byte* bytes2, uint length)
		{
			if ((ulong)length >= (ulong)MemoryHelpers.QuickLengthThreshold && IntPtr.Size <= 8 && (bytes1 & 7L) == (bytes2 & 7L))
			{
				long* ptr = ((long*)bytes1 + 7L / 8L) & -8L;
				long* ptr2 = ((long*)bytes2 + 7L / 8L) & -8L;
				while (bytes1 != (byte*)ptr)
				{
					if (*bytes1 != *bytes2)
					{
						return false;
					}
					length -= 1U;
					bytes1++;
					bytes2++;
				}
				while (length >= 8U)
				{
					if (*ptr != *ptr2)
					{
						return false;
					}
					length -= 8U;
					ptr++;
					ptr2++;
				}
				bytes1 = (byte*)ptr;
				bytes2 = (byte*)ptr2;
			}
			while (length > 0U)
			{
				if (*bytes1 != *bytes2)
				{
					return false;
				}
				length -= 1U;
				bytes1++;
				bytes2++;
			}
			return true;
		}

		// Token: 0x0600C70C RID: 50956 RVA: 0x0027A62B File Offset: 0x0027882B
		public unsafe static bool StrcmpW(char* str1, char* str2)
		{
			while (*str1 == *str2)
			{
				if (*str1 == '\0')
				{
					return true;
				}
				str1++;
				str2++;
			}
			return false;
		}

		// Token: 0x0400658A RID: 25994
		private static long QuickLengthThreshold = 32L;
	}
}
