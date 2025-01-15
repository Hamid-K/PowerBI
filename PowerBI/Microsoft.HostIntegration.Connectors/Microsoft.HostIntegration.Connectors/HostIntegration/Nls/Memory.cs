using System;
using System.IO;

namespace Microsoft.HostIntegration.Nls
{
	// Token: 0x0200063F RID: 1599
	public class Memory
	{
		// Token: 0x060035BE RID: 13758 RVA: 0x000B5C64 File Offset: 0x000B3E64
		public static void ReadFileIntoArray(Stream stream, byte[] data, int bytesToRead)
		{
			int num = 0;
			while (bytesToRead > 0)
			{
				int num2 = stream.Read(data, num, bytesToRead);
				if (num2 <= 0)
				{
					throw new EndOfStreamException(string.Format("End of stream reached with {0} bytes left to read", bytesToRead));
				}
				bytesToRead -= num2;
				num += num2;
			}
		}

		// Token: 0x060035BF RID: 13759 RVA: 0x000B5CA8 File Offset: 0x000B3EA8
		public static bool ReadTable(Stream stream, byte[] data, int bytesToRead)
		{
			try
			{
				Memory.ReadFileIntoArray(stream, data, bytesToRead);
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}

		// Token: 0x060035C0 RID: 13760 RVA: 0x000B5CD8 File Offset: 0x000B3ED8
		public unsafe static void SetMemory(byte[] destination, long startIndex, byte setValue, long setCount)
		{
			fixed (byte* ptr = &destination[(int)(checked((IntPtr)startIndex))])
			{
				Memory.SetMemory(ptr, setValue, setCount);
			}
		}

		// Token: 0x060035C1 RID: 13761 RVA: 0x000B5CFC File Offset: 0x000B3EFC
		public unsafe static void SetMemory(byte* destination, byte setValue, long setCount)
		{
			long num = 0L;
			byte* ptr = destination;
			if (setCount > 24L)
			{
				ulong num2;
				ptr = (byte*)(&num2);
				for (num = 0L; num < 8L; num += 1L)
				{
					*ptr = setValue;
					ptr++;
				}
				ptr = destination;
				for (num = 0L; num < setCount / 8L; num += 1L)
				{
					*(long*)ptr = (long)num2;
					ptr += 8;
				}
			}
			for (num *= 8L; num < setCount; num += 1L)
			{
				*ptr = setValue;
				ptr++;
			}
		}

		// Token: 0x060035C2 RID: 13762 RVA: 0x000B5D60 File Offset: 0x000B3F60
		public unsafe static void SetMemory(char[] destination, long startIndex, char setValue, long setCount)
		{
			fixed (char* ptr = &destination[(int)(checked((IntPtr)startIndex))])
			{
				Memory.SetMemory(ptr, setValue, setCount);
			}
		}

		// Token: 0x060035C3 RID: 13763 RVA: 0x000B5D84 File Offset: 0x000B3F84
		public unsafe static void SetMemory(char* destination, char setValue, long setCount)
		{
			long num = 0L;
			char* ptr = destination;
			while (num < setCount)
			{
				*ptr = setValue;
				ptr++;
				num += 1L;
			}
		}

		// Token: 0x060035C4 RID: 13764 RVA: 0x000B5DA8 File Offset: 0x000B3FA8
		public unsafe static void MemoryCopy(byte[] source, int srcStartPosition, byte* destination, int copyCount)
		{
			fixed (byte* ptr = &source[srcStartPosition])
			{
				Memory.MemoryCopy(ptr, destination, (long)copyCount);
			}
		}

		// Token: 0x060035C5 RID: 13765 RVA: 0x000B5DCC File Offset: 0x000B3FCC
		public unsafe static void MemoryCopy(byte* source, byte[] destination, int destStartPosition, long copyCount)
		{
			fixed (byte* ptr = &destination[destStartPosition])
			{
				byte* ptr2 = ptr;
				Memory.MemoryCopy(source, ptr2, copyCount);
			}
		}

		// Token: 0x060035C6 RID: 13766 RVA: 0x000B5DF0 File Offset: 0x000B3FF0
		public unsafe static void MemoryCopy(byte[] source, int srcStartPosition, byte[] destination, int destStartPosition, long copyCount)
		{
			fixed (byte* ptr = &destination[destStartPosition])
			{
				byte* ptr2 = ptr;
				fixed (byte* ptr3 = &source[srcStartPosition])
				{
					Memory.MemoryCopy(ptr3, ptr2, copyCount);
				}
			}
		}

		// Token: 0x060035C7 RID: 13767 RVA: 0x000B5E20 File Offset: 0x000B4020
		public unsafe static void MemoryCopy(byte* source, byte* destination, long copyCount)
		{
			long num = 0L;
			ulong* ptr = (ulong*)destination;
			ulong* ptr2 = (ulong*)source;
			if (copyCount > 8L)
			{
				for (num = 0L; num < copyCount / 8L; num += 1L)
				{
					*ptr = *ptr2;
					ptr++;
					ptr2++;
				}
			}
			for (num *= 8L; num < copyCount; num += 1L)
			{
				destination[num] = source[num];
			}
		}

		// Token: 0x060035C8 RID: 13768 RVA: 0x000B5E78 File Offset: 0x000B4078
		public unsafe static int memcmp(byte[] source, int srcStartPosition, byte[] destination, int destStartPosition, long copyCount)
		{
			fixed (byte* ptr = &destination[destStartPosition])
			{
				byte* ptr2 = ptr;
				fixed (byte* ptr3 = &source[srcStartPosition])
				{
					return Memory.memcmp(ptr3, ptr2, copyCount);
				}
			}
		}

		// Token: 0x060035C9 RID: 13769 RVA: 0x000B5EA4 File Offset: 0x000B40A4
		public unsafe static int memcmp(byte* source, byte* destination, long copyCount)
		{
			long num = 0L;
			ulong* ptr = (ulong*)destination;
			ulong* ptr2 = (ulong*)source;
			if (copyCount > 8L)
			{
				for (num = 0L; num < copyCount / 8L; num += 1L)
				{
					if (*ptr != *ptr2)
					{
						while (num < copyCount)
						{
							if (destination[num] != source[num])
							{
								return (int)(source[num] - destination[num]);
							}
							num += 1L;
						}
					}
					ptr++;
					ptr2++;
				}
			}
			for (num *= 8L; num < copyCount; num += 1L)
			{
				if (destination[num] != source[num])
				{
					return (int)(source[num] - destination[num]);
				}
			}
			return 0;
		}
	}
}
