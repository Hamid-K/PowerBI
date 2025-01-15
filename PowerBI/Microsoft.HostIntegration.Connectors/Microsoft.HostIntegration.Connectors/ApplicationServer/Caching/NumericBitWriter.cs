using System;
using System.Diagnostics;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000298 RID: 664
	internal sealed class NumericBitWriter
	{
		// Token: 0x0600182E RID: 6190 RVA: 0x000497C8 File Offset: 0x000479C8
		private NumericBitWriter(bool isLittleEndian)
		{
			this._isLittleEndian = isLittleEndian;
		}

		// Token: 0x0600182F RID: 6191 RVA: 0x000497D7 File Offset: 0x000479D7
		public void FillBytes(byte number, byte[] array, int offset)
		{
			array[offset] = number;
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x000497DD File Offset: 0x000479DD
		public void FillBytes(sbyte number, byte[] array, int offset)
		{
			array[offset] = (byte)number;
		}

		// Token: 0x06001831 RID: 6193 RVA: 0x000497E4 File Offset: 0x000479E4
		public void FillBytes(char ch, byte[] array, int offset)
		{
			this.FillBytes((short)ch, array, offset);
		}

		// Token: 0x06001832 RID: 6194 RVA: 0x000497F0 File Offset: 0x000479F0
		public void FillBytes(short number, byte[] array, int offset)
		{
			if (this._isLittleEndian)
			{
				array[offset] = (byte)(number & 255);
				array[offset + 1] = (byte)((number >> 8) & 255);
				return;
			}
			array[offset + 1] = (byte)(number & 255);
			array[offset] = (byte)((number >> 8) & 255);
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x000497E4 File Offset: 0x000479E4
		public void FillBytes(ushort number, byte[] array, int offset)
		{
			this.FillBytes((short)number, array, offset);
		}

		// Token: 0x06001834 RID: 6196 RVA: 0x00049830 File Offset: 0x00047A30
		public void FillBytes(int number, byte[] array, int offset)
		{
			if (this._isLittleEndian)
			{
				array[offset] = (byte)(number & 255);
				array[offset + 1] = (byte)((number >> 8) & 255);
				array[offset + 2] = (byte)((number >> 16) & 255);
				array[offset + 3] = (byte)((number >> 24) & 255);
				return;
			}
			array[offset + 3] = (byte)(number & 255);
			array[offset + 2] = (byte)((number >> 8) & 255);
			array[offset + 1] = (byte)((number >> 16) & 255);
			array[offset] = (byte)((number >> 24) & 255);
		}

		// Token: 0x06001835 RID: 6197 RVA: 0x000498BA File Offset: 0x00047ABA
		public void FillBytes(uint number, byte[] array, int offset)
		{
			this.FillBytes((int)number, array, offset);
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x000498C8 File Offset: 0x00047AC8
		public void FillBytes(long number, byte[] array, int offset)
		{
			if (this._isLittleEndian)
			{
				array[offset] = (byte)(number & 255L);
				array[offset + 1] = (byte)((number >> 8) & 255L);
				array[offset + 2] = (byte)((number >> 16) & 255L);
				array[offset + 3] = (byte)((number >> 24) & 255L);
				array[offset + 4] = (byte)((number >> 32) & 255L);
				array[offset + 5] = (byte)((number >> 40) & 255L);
				array[offset + 6] = (byte)((number >> 48) & 255L);
				array[offset + 7] = (byte)((number >> 56) & 255L);
				return;
			}
			array[offset + 7] = (byte)(number & 255L);
			array[offset + 6] = (byte)((number >> 8) & 255L);
			array[offset + 5] = (byte)((number >> 16) & 255L);
			array[offset + 4] = (byte)((number >> 24) & 255L);
			array[offset + 3] = (byte)((number >> 32) & 255L);
			array[offset + 2] = (byte)((number >> 40) & 255L);
			array[offset + 1] = (byte)((number >> 48) & 255L);
			array[offset] = (byte)((number >> 56) & 255L);
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x000499E5 File Offset: 0x00047BE5
		public void FillBytes(ulong number, byte[] array, int offset)
		{
			this.FillBytes((long)number, array, offset);
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x000499F0 File Offset: 0x00047BF0
		public void FillBytes(float number, byte[] array, int offset)
		{
			BitConverter.GetBytes(number).CopyTo(array, offset);
			if (this._isLittleEndian != BitConverter.IsLittleEndian)
			{
				NumericBitWriter.FlipArray(array, offset, 4);
			}
		}

		// Token: 0x06001839 RID: 6201 RVA: 0x00049A14 File Offset: 0x00047C14
		public void FillBytes(double number, byte[] array, int offset)
		{
			long num = BitConverter.DoubleToInt64Bits(number);
			this.FillBytes(num, array, offset);
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x00049A34 File Offset: 0x00047C34
		private static void FlipArray(byte[] array, int offset, int count)
		{
			for (int i = 0; i < count / 2; i++)
			{
				byte b = array[offset + i];
				array[offset + i] = array[offset + count - i - 1];
				array[offset + count - i - 1] = b;
			}
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x000036A9 File Offset: 0x000018A9
		[Conditional("DEBUG")]
		private static void Validate(byte[] array, int offset, int length)
		{
		}

		// Token: 0x04000D58 RID: 3416
		public static readonly NumericBitWriter LittleEndian = new NumericBitWriter(true);

		// Token: 0x04000D59 RID: 3417
		public static readonly NumericBitWriter BigEndian = new NumericBitWriter(false);

		// Token: 0x04000D5A RID: 3418
		public static readonly NumericBitWriter NetworkOrder = NumericBitWriter.BigEndian;

		// Token: 0x04000D5B RID: 3419
		public static readonly NumericBitWriter HostOrder = (BitConverter.IsLittleEndian ? NumericBitWriter.LittleEndian : NumericBitWriter.BigEndian);

		// Token: 0x04000D5C RID: 3420
		private readonly bool _isLittleEndian;
	}
}
