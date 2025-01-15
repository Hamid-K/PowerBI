using System;
using System.IO;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200002D RID: 45
	public static class StreamSupport
	{
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00005BC9 File Offset: 0x00003DC9
		public static int MemoryBufferLimit
		{
			get
			{
				return StreamSupport.__MemoryBufferLimit;
			}
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00005BD0 File Offset: 0x00003DD0
		public static byte[] ReadToEndNotUsingLength(Stream s, int initialBufferSize)
		{
			bool flag;
			return StreamSupport.ReadToEndNotUsingLength(s, initialBufferSize, StreamSupport.__MaxAllowedBytesUnlimited, out flag);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00005BEC File Offset: 0x00003DEC
		public static byte[] ReadToEndNotUsingLength(Stream s, int initialBufferSize, int maxAllowedBytes, out bool exceededMaxSize)
		{
			exceededMaxSize = false;
			if (s == null)
			{
				return null;
			}
			if (initialBufferSize <= 0)
			{
				initialBufferSize = 1;
			}
			byte[] array = new byte[initialBufferSize];
			int num = 0;
			for (;;)
			{
				int num2 = s.Read(array, num, array.Length - num);
				num += num2;
				if (num > maxAllowedBytes)
				{
					break;
				}
				if (num2 != 0 && num >= array.Length)
				{
					byte[] array2 = new byte[array.Length * 2];
					Array.Copy(array, 0, array2, 0, num);
					array = array2;
				}
				if (num2 == 0)
				{
					goto Block_6;
				}
			}
			exceededMaxSize = true;
			return null;
			Block_6:
			if (num == array.Length)
			{
				return array;
			}
			byte[] array3 = new byte[num];
			Array.Copy(array, 0, array3, 0, num);
			return array3;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00005C70 File Offset: 0x00003E70
		public static byte[] ReadToEndUsingLength(Stream s)
		{
			if (s == null)
			{
				return null;
			}
			byte[] array = new byte[s.Length];
			int num = 0;
			int num2;
			do
			{
				num2 = s.Read(array, num, array.Length - num);
				num += num2;
			}
			while (num2 != 0);
			if (num != array.Length)
			{
				throw new NotSupportedException("Stream must be at position 0 when calling this function!");
			}
			return array;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00005CBC File Offset: 0x00003EBC
		public static long CopyStreamUsingBuffer(Stream from, Stream to, int bufferSize)
		{
			if (bufferSize <= 0)
			{
				bufferSize = 1024;
			}
			if (bufferSize > StreamSupport.MemoryBufferLimit)
			{
				RSTrace.CatalogTrace.Assert(false, "Buffers size is non optimal size for copying streams");
			}
			byte[] array = new byte[bufferSize];
			long num = 0L;
			int num2;
			do
			{
				num2 = from.Read(array, 0, array.Length);
				if (num2 > 0)
				{
					to.Write(array, 0, num2);
					num += (long)num2;
				}
			}
			while (num2 > 0);
			return num;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00005D1C File Offset: 0x00003F1C
		public static long CopyFromStreamUsingBuffer(Stream from, Stream to, long bytesToCopy, int bufferSize)
		{
			long num = bytesToCopy;
			if (bufferSize <= 0)
			{
				bufferSize = 1024;
			}
			if (bufferSize > StreamSupport.MemoryBufferLimit)
			{
				RSTrace.CatalogTrace.Assert(false, "Buffers size is non optimal size for copying streams");
			}
			byte[] array = new byte[bufferSize];
			int num2 = array.Length;
			long num3 = 0L;
			int num4;
			do
			{
				if ((long)num2 > num)
				{
					num2 = (int)num;
				}
				num4 = from.Read(array, 0, num2);
				if (num4 > 0)
				{
					to.Write(array, 0, num4);
					num3 += (long)num4;
					num -= (long)num4;
				}
			}
			while (num4 > 0);
			if (num != 0L)
			{
				RSTrace.ChunkTracer.Assert(false, "unexpected number of bytes to copy ({0})", new object[] { num });
			}
			return num3;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00005DB4 File Offset: 0x00003FB4
		public static int ReadToCountOrEnd(byte[] buffer, int offset, int count, StreamSupport.StreamRead streamReadDelegate)
		{
			int num = 0;
			int num2 = offset;
			int num3 = count;
			for (;;)
			{
				int num4 = streamReadDelegate(buffer, num2, num3);
				if (num4 == -1)
				{
					break;
				}
				num += num4;
				if (num >= count || num4 <= 0)
				{
					break;
				}
				num2 += num4;
				num3 -= num4;
			}
			return num;
		}

		// Token: 0x040000A8 RID: 168
		private static readonly int __MemoryBufferLimit = 81920;

		// Token: 0x040000A9 RID: 169
		private static readonly int __MaxAllowedBytesUnlimited = int.MaxValue;

		// Token: 0x020000E1 RID: 225
		// (Invoke) Token: 0x060007A3 RID: 1955
		public delegate int StreamRead(byte[] buffer, int offset, int count);
	}
}
