using System;
using System.IO;

namespace Microsoft.ReportingServices.LegacyOfficeRender
{
	// Token: 0x02000003 RID: 3
	internal sealed class OfficeImageHasher
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002128 File Offset: 0x00000328
		internal OfficeImageHasher(byte[] inputBuffer)
		{
			this.Mdinit(inputBuffer);
			this.Calc();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002174 File Offset: 0x00000374
		internal OfficeImageHasher(Stream inputStream)
		{
			this.Mdinit(inputStream);
			this.Calc();
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000021C0 File Offset: 0x000003C0
		internal byte[] Hash
		{
			get
			{
				byte[] array = new byte[16];
				Array.Copy(BitConverter.GetBytes(this.m_a), 0, array, 0, 4);
				Array.Copy(BitConverter.GetBytes(this.m_b), 0, array, 4, 4);
				Array.Copy(BitConverter.GetBytes(this.m_c), 0, array, 8, 4);
				Array.Copy(BitConverter.GetBytes(this.m_d), 0, array, 12, 4);
				return array;
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002228 File Offset: 0x00000428
		internal void Mdinit(byte[] inputBuffer)
		{
			long num = (long)(inputBuffer.Length * 8);
			int num2 = inputBuffer.Length % 64;
			int num3;
			if (num2 < 56)
			{
				num3 = 64 - num2;
			}
			else
			{
				num3 = 64 - num2 + 64;
			}
			int num4 = inputBuffer.Length + num3;
			byte[] array = new byte[num4];
			Array.Copy(inputBuffer, array, inputBuffer.Length);
			array[inputBuffer.Length] = 128;
			for (int i = 0; i < 8; i++)
			{
				array[num4 - 8 + i] = (byte)(num & 255L);
				num >>= 8;
			}
			this.m_numwords = num4 / 4;
			this.m_dd = new int[this.m_numwords];
			for (int i = 0; i < num4; i += 4)
			{
				this.m_dd[i / 4] = (int)(array[i] & byte.MaxValue) + ((int)(array[i + 1] & byte.MaxValue) << 8) + ((int)(array[i + 2] & byte.MaxValue) << 16) + ((int)(array[i + 3] & byte.MaxValue) << 24);
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000230C File Offset: 0x0000050C
		internal void Mdinit(Stream inputStream)
		{
			long num = inputStream.Length * 8L;
			int num2 = (int)(inputStream.Length % 64L);
			int num3;
			if (num2 < 56)
			{
				num3 = 64 - num2;
			}
			else
			{
				num3 = 64 - num2 + 64;
			}
			int num4 = (int)inputStream.Length + num3;
			byte[] array = new byte[num4];
			inputStream.Read(array, 0, (int)inputStream.Length);
			array[(int)inputStream.Length] = 128;
			for (int i = 0; i < 8; i++)
			{
				array[num4 - 8 + i] = (byte)(num & 255L);
				num >>= 8;
			}
			this.m_numwords = num4 / 4;
			this.m_dd = new int[this.m_numwords];
			for (int i = 0; i < num4; i += 4)
			{
				this.m_dd[i / 4] = (int)(array[i] & byte.MaxValue) + ((int)(array[i + 1] & byte.MaxValue) << 8) + ((int)(array[i + 2] & byte.MaxValue) << 16) + ((int)(array[i + 3] & byte.MaxValue) << 24);
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002404 File Offset: 0x00000604
		internal void Calc()
		{
			for (int i = 0; i < this.m_numwords / 16; i++)
			{
				int a = this.m_a;
				int b = this.m_b;
				int c = this.m_c;
				int d = this.m_d;
				this.Round1(i);
				this.Round2(i);
				this.Round3(i);
				this.m_a += a;
				this.m_b += b;
				this.m_c += c;
				this.m_d += d;
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002495 File Offset: 0x00000695
		internal static int F(int x, int y, int z)
		{
			return (x & y) | (~x & z);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000249F File Offset: 0x0000069F
		internal static int G(int x, int y, int z)
		{
			return (x & y) | (x & z) | (y & z);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000024AC File Offset: 0x000006AC
		internal static int H(int x, int y, int z)
		{
			return x ^ y ^ z;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000024B4 File Offset: 0x000006B4
		internal void Round1(int blk)
		{
			int num = 16 * blk;
			this.m_a = OfficeImageHasher.Rotintlft(this.m_a + OfficeImageHasher.F(this.m_b, this.m_c, this.m_d) + this.m_dd[num], 3);
			this.m_d = OfficeImageHasher.Rotintlft(this.m_d + OfficeImageHasher.F(this.m_a, this.m_b, this.m_c) + this.m_dd[1 + num], 7);
			this.m_c = OfficeImageHasher.Rotintlft(this.m_c + OfficeImageHasher.F(this.m_d, this.m_a, this.m_b) + this.m_dd[2 + num], 11);
			this.m_b = OfficeImageHasher.Rotintlft(this.m_b + OfficeImageHasher.F(this.m_c, this.m_d, this.m_a) + this.m_dd[3 + num], 19);
			this.m_a = OfficeImageHasher.Rotintlft(this.m_a + OfficeImageHasher.F(this.m_b, this.m_c, this.m_d) + this.m_dd[4 + num], 3);
			this.m_d = OfficeImageHasher.Rotintlft(this.m_d + OfficeImageHasher.F(this.m_a, this.m_b, this.m_c) + this.m_dd[5 + num], 7);
			this.m_c = OfficeImageHasher.Rotintlft(this.m_c + OfficeImageHasher.F(this.m_d, this.m_a, this.m_b) + this.m_dd[6 + num], 11);
			this.m_b = OfficeImageHasher.Rotintlft(this.m_b + OfficeImageHasher.F(this.m_c, this.m_d, this.m_a) + this.m_dd[7 + num], 19);
			this.m_a = OfficeImageHasher.Rotintlft(this.m_a + OfficeImageHasher.F(this.m_b, this.m_c, this.m_d) + this.m_dd[8 + num], 3);
			this.m_d = OfficeImageHasher.Rotintlft(this.m_d + OfficeImageHasher.F(this.m_a, this.m_b, this.m_c) + this.m_dd[9 + num], 7);
			this.m_c = OfficeImageHasher.Rotintlft(this.m_c + OfficeImageHasher.F(this.m_d, this.m_a, this.m_b) + this.m_dd[10 + num], 11);
			this.m_b = OfficeImageHasher.Rotintlft(this.m_b + OfficeImageHasher.F(this.m_c, this.m_d, this.m_a) + this.m_dd[11 + num], 19);
			this.m_a = OfficeImageHasher.Rotintlft(this.m_a + OfficeImageHasher.F(this.m_b, this.m_c, this.m_d) + this.m_dd[12 + num], 3);
			this.m_d = OfficeImageHasher.Rotintlft(this.m_d + OfficeImageHasher.F(this.m_a, this.m_b, this.m_c) + this.m_dd[13 + num], 7);
			this.m_c = OfficeImageHasher.Rotintlft(this.m_c + OfficeImageHasher.F(this.m_d, this.m_a, this.m_b) + this.m_dd[14 + num], 11);
			this.m_b = OfficeImageHasher.Rotintlft(this.m_b + OfficeImageHasher.F(this.m_c, this.m_d, this.m_a) + this.m_dd[15 + num], 19);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002824 File Offset: 0x00000A24
		internal void Round2(int blk)
		{
			int num = 16 * blk;
			this.m_a = OfficeImageHasher.Rotintlft(this.m_a + OfficeImageHasher.G(this.m_b, this.m_c, this.m_d) + this.m_dd[num] + 1518500249, 3);
			this.m_d = OfficeImageHasher.Rotintlft(this.m_d + OfficeImageHasher.G(this.m_a, this.m_b, this.m_c) + this.m_dd[4 + num] + 1518500249, 5);
			this.m_c = OfficeImageHasher.Rotintlft(this.m_c + OfficeImageHasher.G(this.m_d, this.m_a, this.m_b) + this.m_dd[8 + num] + 1518500249, 9);
			this.m_b = OfficeImageHasher.Rotintlft(this.m_b + OfficeImageHasher.G(this.m_c, this.m_d, this.m_a) + this.m_dd[12 + num] + 1518500249, 13);
			this.m_a = OfficeImageHasher.Rotintlft(this.m_a + OfficeImageHasher.G(this.m_b, this.m_c, this.m_d) + this.m_dd[1 + num] + 1518500249, 3);
			this.m_d = OfficeImageHasher.Rotintlft(this.m_d + OfficeImageHasher.G(this.m_a, this.m_b, this.m_c) + this.m_dd[5 + num] + 1518500249, 5);
			this.m_c = OfficeImageHasher.Rotintlft(this.m_c + OfficeImageHasher.G(this.m_d, this.m_a, this.m_b) + this.m_dd[9 + num] + 1518500249, 9);
			this.m_b = OfficeImageHasher.Rotintlft(this.m_b + OfficeImageHasher.G(this.m_c, this.m_d, this.m_a) + this.m_dd[13 + num] + 1518500249, 13);
			this.m_a = OfficeImageHasher.Rotintlft(this.m_a + OfficeImageHasher.G(this.m_b, this.m_c, this.m_d) + this.m_dd[2 + num] + 1518500249, 3);
			this.m_d = OfficeImageHasher.Rotintlft(this.m_d + OfficeImageHasher.G(this.m_a, this.m_b, this.m_c) + this.m_dd[6 + num] + 1518500249, 5);
			this.m_c = OfficeImageHasher.Rotintlft(this.m_c + OfficeImageHasher.G(this.m_d, this.m_a, this.m_b) + this.m_dd[10 + num] + 1518500249, 9);
			this.m_b = OfficeImageHasher.Rotintlft(this.m_b + OfficeImageHasher.G(this.m_c, this.m_d, this.m_a) + this.m_dd[14 + num] + 1518500249, 13);
			this.m_a = OfficeImageHasher.Rotintlft(this.m_a + OfficeImageHasher.G(this.m_b, this.m_c, this.m_d) + this.m_dd[3 + num] + 1518500249, 3);
			this.m_d = OfficeImageHasher.Rotintlft(this.m_d + OfficeImageHasher.G(this.m_a, this.m_b, this.m_c) + this.m_dd[7 + num] + 1518500249, 5);
			this.m_c = OfficeImageHasher.Rotintlft(this.m_c + OfficeImageHasher.G(this.m_d, this.m_a, this.m_b) + this.m_dd[11 + num] + 1518500249, 9);
			this.m_b = OfficeImageHasher.Rotintlft(this.m_b + OfficeImageHasher.G(this.m_c, this.m_d, this.m_a) + this.m_dd[15 + num] + 1518500249, 13);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002BF4 File Offset: 0x00000DF4
		internal void Round3(int blk)
		{
			int num = 16 * blk;
			this.m_a = OfficeImageHasher.Rotintlft(this.m_a + OfficeImageHasher.H(this.m_b, this.m_c, this.m_d) + this.m_dd[num] + 1859775393, 3);
			this.m_d = OfficeImageHasher.Rotintlft(this.m_d + OfficeImageHasher.H(this.m_a, this.m_b, this.m_c) + this.m_dd[8 + num] + 1859775393, 9);
			this.m_c = OfficeImageHasher.Rotintlft(this.m_c + OfficeImageHasher.H(this.m_d, this.m_a, this.m_b) + this.m_dd[4 + num] + 1859775393, 11);
			this.m_b = OfficeImageHasher.Rotintlft(this.m_b + OfficeImageHasher.H(this.m_c, this.m_d, this.m_a) + this.m_dd[12 + num] + 1859775393, 15);
			this.m_a = OfficeImageHasher.Rotintlft(this.m_a + OfficeImageHasher.H(this.m_b, this.m_c, this.m_d) + this.m_dd[2 + num] + 1859775393, 3);
			this.m_d = OfficeImageHasher.Rotintlft(this.m_d + OfficeImageHasher.H(this.m_a, this.m_b, this.m_c) + this.m_dd[10 + num] + 1859775393, 9);
			this.m_c = OfficeImageHasher.Rotintlft(this.m_c + OfficeImageHasher.H(this.m_d, this.m_a, this.m_b) + this.m_dd[6 + num] + 1859775393, 11);
			this.m_b = OfficeImageHasher.Rotintlft(this.m_b + OfficeImageHasher.H(this.m_c, this.m_d, this.m_a) + this.m_dd[14 + num] + 1859775393, 15);
			this.m_a = OfficeImageHasher.Rotintlft(this.m_a + OfficeImageHasher.H(this.m_b, this.m_c, this.m_d) + this.m_dd[1 + num] + 1859775393, 3);
			this.m_d = OfficeImageHasher.Rotintlft(this.m_d + OfficeImageHasher.H(this.m_a, this.m_b, this.m_c) + this.m_dd[9 + num] + 1859775393, 9);
			this.m_c = OfficeImageHasher.Rotintlft(this.m_c + OfficeImageHasher.H(this.m_d, this.m_a, this.m_b) + this.m_dd[5 + num] + 1859775393, 11);
			this.m_b = OfficeImageHasher.Rotintlft(this.m_b + OfficeImageHasher.H(this.m_c, this.m_d, this.m_a) + this.m_dd[13 + num] + 1859775393, 15);
			this.m_a = OfficeImageHasher.Rotintlft(this.m_a + OfficeImageHasher.H(this.m_b, this.m_c, this.m_d) + this.m_dd[3 + num] + 1859775393, 3);
			this.m_d = OfficeImageHasher.Rotintlft(this.m_d + OfficeImageHasher.H(this.m_a, this.m_b, this.m_c) + this.m_dd[11 + num] + 1859775393, 9);
			this.m_c = OfficeImageHasher.Rotintlft(this.m_c + OfficeImageHasher.H(this.m_d, this.m_a, this.m_b) + this.m_dd[7 + num] + 1859775393, 11);
			this.m_b = OfficeImageHasher.Rotintlft(this.m_b + OfficeImageHasher.H(this.m_c, this.m_d, this.m_a) + this.m_dd[15 + num] + 1859775393, 15);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002FC7 File Offset: 0x000011C7
		internal int[] Getregs()
		{
			return new int[] { this.m_a, this.m_b, this.m_c, this.m_d };
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002FF3 File Offset: 0x000011F3
		internal static int Rotintlft(int val, int numbits)
		{
			return (val << numbits) | (int)((uint)val >> 32 - numbits);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00003005 File Offset: 0x00001205
		public override string ToString()
		{
			return OfficeImageHasher.Tohex(this.m_a) + OfficeImageHasher.Tohex(this.m_b) + OfficeImageHasher.Tohex(this.m_c) + OfficeImageHasher.Tohex(this.m_d);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00003038 File Offset: 0x00001238
		internal static string Tohex(int i)
		{
			string text = "";
			for (int j = 0; j < 4; j++)
			{
				text = text + Convert.ToString((i >> 4) & 15, 16) + Convert.ToString(i & 15, 16);
				i >>= 8;
			}
			return text;
		}

		// Token: 0x04000001 RID: 1
		private int m_a = 1732584193;

		// Token: 0x04000002 RID: 2
		private int m_b = -271733879;

		// Token: 0x04000003 RID: 3
		private int m_c = -1732584194;

		// Token: 0x04000004 RID: 4
		private int m_d = 271733878;

		// Token: 0x04000005 RID: 5
		private int[] m_dd;

		// Token: 0x04000006 RID: 6
		private int m_numwords;
	}
}
