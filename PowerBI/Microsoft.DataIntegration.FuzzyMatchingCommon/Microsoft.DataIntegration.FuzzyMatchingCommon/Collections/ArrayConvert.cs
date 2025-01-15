using System;
using System.Runtime.InteropServices;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x0200009D RID: 157
	[StructLayout(2)]
	internal struct ArrayConvert
	{
		// Token: 0x060006B0 RID: 1712 RVA: 0x00023D4C File Offset: 0x00021F4C
		public static byte[] GetBytes(int[] ints)
		{
			ArrayConvert arrayConvert = default(ArrayConvert);
			arrayConvert.ints = ints;
			arrayConvert.length.val = ints.Length * 4;
			return arrayConvert.bytes;
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00023D80 File Offset: 0x00021F80
		public static byte[] GetBytes(float[] floats)
		{
			ArrayConvert arrayConvert = default(ArrayConvert);
			arrayConvert.floats = floats;
			arrayConvert.length.val = floats.Length * 4;
			return arrayConvert.bytes;
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00023DB4 File Offset: 0x00021FB4
		public static float[] GetFloats(byte[] bytes)
		{
			ArrayConvert arrayConvert = default(ArrayConvert);
			arrayConvert.bytes = bytes;
			arrayConvert.length.val = bytes.Length / 4;
			return arrayConvert.floats;
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00023DE8 File Offset: 0x00021FE8
		public static byte[] GetTop4BytesFrom(object obj)
		{
			ArrayConvert arrayConvert = default(ArrayConvert);
			arrayConvert.obj = obj;
			return new byte[]
			{
				arrayConvert.top4bytes.b0,
				arrayConvert.top4bytes.b1,
				arrayConvert.top4bytes.b2,
				arrayConvert.top4bytes.b3
			};
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x00023E44 File Offset: 0x00022044
		public static byte[] GetBytesFrom(object obj, int size)
		{
			ArrayConvert arrayConvert = default(ArrayConvert);
			arrayConvert.obj = obj;
			arrayConvert.length.val = size;
			return arrayConvert.bytes;
		}

		// Token: 0x04000154 RID: 340
		[FieldOffset(0)]
		private byte[] bytes;

		// Token: 0x04000155 RID: 341
		[FieldOffset(0)]
		private object obj;

		// Token: 0x04000156 RID: 342
		[FieldOffset(0)]
		private float[] floats;

		// Token: 0x04000157 RID: 343
		[FieldOffset(0)]
		private int[] ints;

		// Token: 0x04000158 RID: 344
		[FieldOffset(0)]
		private ArrayConvert.ArrayLength length;

		// Token: 0x04000159 RID: 345
		[FieldOffset(0)]
		private ArrayConvert.Top4Bytes top4bytes;

		// Token: 0x02000136 RID: 310
		private class ArrayLength
		{
			// Token: 0x0400031E RID: 798
			public int val;
		}

		// Token: 0x02000137 RID: 311
		private class Top4Bytes
		{
			// Token: 0x0400031F RID: 799
			public byte b0;

			// Token: 0x04000320 RID: 800
			public byte b1;

			// Token: 0x04000321 RID: 801
			public byte b2;

			// Token: 0x04000322 RID: 802
			public byte b3;
		}
	}
}
