using System;

namespace Microsoft.Internal
{
	// Token: 0x020001B5 RID: 437
	internal class MurmurHash3_128
	{
		// Token: 0x06000841 RID: 2113 RVA: 0x0000FA34 File Offset: 0x0000DC34
		public MurmurHash3_128(ulong seed = 0UL)
		{
			this.h1 = seed;
			this.h2 = seed;
			this.buffer = new byte[65536];
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x0000FA5C File Offset: 0x0000DC5C
		public void AddByte(byte b)
		{
			if (this.bufferCount < 0)
			{
				throw new InvalidOperationException();
			}
			byte[] array = this.buffer;
			int num = this.bufferCount;
			this.bufferCount = num + 1;
			array[num] = b;
			if (this.bufferCount >= 16)
			{
				this.ProcessBuffer();
			}
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x0000FAA1 File Offset: 0x0000DCA1
		public void AddBytes(byte[] buffer)
		{
			this.AddBytes(buffer, 0, buffer.Length);
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x0000FAB0 File Offset: 0x0000DCB0
		public void AddBytes(byte[] buffer, int offset, int count)
		{
			if (this.bufferCount < 0)
			{
				throw new InvalidOperationException();
			}
			while (count > 0)
			{
				int num = Math.Min(65536 - this.bufferCount, count);
				Array.Copy(buffer, offset, this.buffer, this.bufferCount, num);
				this.bufferCount += num;
				this.ProcessBuffer();
				offset += num;
				count -= num;
			}
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0000FB18 File Offset: 0x0000DD18
		public unsafe void Finish(out ulong lowPart, out ulong highPart)
		{
			if (this.bufferCount > 0)
			{
				byte[] array;
				byte* ptr;
				if ((array = this.buffer) == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				this.AddTail(ptr, this.bufferCount);
				array = null;
			}
			this.bufferCount = -1;
			lowPart = this.h1;
			highPart = this.h2;
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0000FB74 File Offset: 0x0000DD74
		private unsafe void ProcessBuffer()
		{
			int num = this.bufferCount / 16;
			if (num > 0)
			{
				byte[] array;
				byte* ptr;
				if ((array = this.buffer) == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				ulong* ptr2 = (ulong*)ptr;
				this.AddBlocks(ptr2, num);
				array = null;
				int num2 = num * 16;
				this.bufferCount -= num2;
				if (this.bufferCount > 0)
				{
					Array.Copy(this.buffer, num2, this.buffer, 0, this.bufferCount);
				}
				this.length += (uint)num2;
			}
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0000FC00 File Offset: 0x0000DE00
		private unsafe void AddBlocks(ulong* blocks, int count)
		{
			for (int i = 0; i < count; i++)
			{
				ulong num = blocks[2 * i];
				ulong num2 = blocks[2 * i + 1];
				num *= 9782798678568883157UL;
				num = MurmurHash3_128.RotateLeft(num, 31);
				num *= 5545529020109919103UL;
				this.h1 ^= num;
				this.h1 = MurmurHash3_128.RotateLeft(this.h1, 27);
				this.h1 += this.h2;
				this.h1 = this.h1 * 5UL + 1390208809UL;
				num2 *= 5545529020109919103UL;
				num2 = MurmurHash3_128.RotateLeft(num2, 33);
				num2 *= 9782798678568883157UL;
				this.h2 ^= num2;
				this.h2 = MurmurHash3_128.RotateLeft(this.h2, 31);
				this.h2 += this.h1;
				this.h2 = this.h2 * 5UL + 944331445UL;
			}
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0000FD0C File Offset: 0x0000DF0C
		private unsafe void AddTail(byte* tail, int len)
		{
			ulong num = 0UL;
			ulong num2 = 0UL;
			this.length += (uint)len;
			switch (len & 15)
			{
			case 1:
				goto IL_012C;
			case 2:
				goto IL_0122;
			case 3:
				goto IL_0117;
			case 4:
				goto IL_010C;
			case 5:
				goto IL_0101;
			case 6:
				goto IL_00F6;
			case 7:
				goto IL_00EB;
			case 8:
				goto IL_00E0;
			case 9:
				goto IL_00A9;
			case 10:
				goto IL_009E;
			case 11:
				goto IL_0092;
			case 12:
				goto IL_0086;
			case 13:
				goto IL_007A;
			case 14:
				break;
			case 15:
				num2 ^= (ulong)tail[14] << 48;
				break;
			default:
				goto IL_0161;
			}
			num2 ^= (ulong)tail[13] << 40;
			IL_007A:
			num2 ^= (ulong)tail[12] << 32;
			IL_0086:
			num2 ^= (ulong)tail[11] << 24;
			IL_0092:
			num2 ^= (ulong)tail[10] << 16;
			IL_009E:
			num2 ^= (ulong)tail[9] << 8;
			IL_00A9:
			num2 ^= (ulong)tail[8];
			num2 *= 5545529020109919103UL;
			num2 = MurmurHash3_128.RotateLeft(num2, 33);
			num2 *= 9782798678568883157UL;
			this.h2 ^= num2;
			IL_00E0:
			num ^= (ulong)tail[7] << 56;
			IL_00EB:
			num ^= (ulong)tail[6] << 48;
			IL_00F6:
			num ^= (ulong)tail[5] << 40;
			IL_0101:
			num ^= (ulong)tail[4] << 32;
			IL_010C:
			num ^= (ulong)tail[3] << 24;
			IL_0117:
			num ^= (ulong)tail[2] << 16;
			IL_0122:
			num ^= (ulong)tail[1] << 8;
			IL_012C:
			num ^= (ulong)(*tail);
			num *= 9782798678568883157UL;
			num = MurmurHash3_128.RotateLeft(num, 31);
			num *= 5545529020109919103UL;
			this.h1 ^= num;
			IL_0161:
			this.h1 ^= (ulong)this.length;
			this.h2 ^= (ulong)this.length;
			this.h1 += this.h2;
			this.h2 += this.h1;
			this.h1 = MurmurHash3_128.Mix(this.h1);
			this.h2 = MurmurHash3_128.Mix(this.h2);
			this.h1 += this.h2;
			this.h2 += this.h1;
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0000FF10 File Offset: 0x0000E110
		private static ulong RotateLeft(ulong x, int r)
		{
			return (x << r) | (x >> 64 - r);
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0000FF22 File Offset: 0x0000E122
		private static ulong Mix(ulong k)
		{
			k ^= k >> 33;
			k *= 18397679294719823053UL;
			k ^= k >> 33;
			k *= 14181476777654086739UL;
			k ^= k >> 33;
			return k;
		}

		// Token: 0x040004B6 RID: 1206
		private const ulong C1 = 9782798678568883157UL;

		// Token: 0x040004B7 RID: 1207
		private const ulong C2 = 5545529020109919103UL;

		// Token: 0x040004B8 RID: 1208
		private const int BufferSize = 65536;

		// Token: 0x040004B9 RID: 1209
		private const int BlockSize = 16;

		// Token: 0x040004BA RID: 1210
		private readonly byte[] buffer;

		// Token: 0x040004BB RID: 1211
		private int bufferCount;

		// Token: 0x040004BC RID: 1212
		private ulong h1;

		// Token: 0x040004BD RID: 1213
		private ulong h2;

		// Token: 0x040004BE RID: 1214
		private uint length;
	}
}
