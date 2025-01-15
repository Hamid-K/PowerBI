using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.DI.RoaringBitmap.Utilities;

namespace Microsoft.DI.RoaringBitmap.Containers
{
	// Token: 0x0200000E RID: 14
	internal class BitmapContainer : IContainer
	{
		// Token: 0x06000061 RID: 97 RVA: 0x00003630 File Offset: 0x00001830
		public BitmapContainer()
		{
			this.Cardinality = 0;
			this.Bitmap = new ulong[1024];
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003650 File Offset: 0x00001850
		public BitmapContainer(int cardinality, char[] values)
		{
			this.Bitmap = new ulong[1024];
			for (int i = 0; i < cardinality; i++)
			{
				char c = values[i];
				this.Bitmap[(int)(c >> 6)] |= 1UL << (int)c;
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000369C File Offset: 0x0000189C
		public BitmapContainer(int cardinality, ulong[] bitmap)
		{
			this.Initialize(cardinality, bitmap);
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000064 RID: 100 RVA: 0x000036AC File Offset: 0x000018AC
		public int ArraySizeInBytes
		{
			get
			{
				return 8192;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000065 RID: 101 RVA: 0x000036B3 File Offset: 0x000018B3
		// (set) Token: 0x06000066 RID: 102 RVA: 0x000036BB File Offset: 0x000018BB
		public ulong[] Bitmap { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000067 RID: 103 RVA: 0x000036C4 File Offset: 0x000018C4
		// (set) Token: 0x06000068 RID: 104 RVA: 0x000036CC File Offset: 0x000018CC
		public int Cardinality { get; set; }

		// Token: 0x06000069 RID: 105 RVA: 0x000036D5 File Offset: 0x000018D5
		public static int SerializedSizeInBytes()
		{
			return 8192;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000036DC File Offset: 0x000018DC
		public void Serialize(BinaryWriter binaryWriter)
		{
			for (int i = 0; i < 1024; i++)
			{
				binaryWriter.Write(this.Bitmap[i]);
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003708 File Offset: 0x00001908
		public void Deserialize(BinaryReader binaryReader, int cardinality)
		{
			if (binaryReader == null)
			{
				throw new ArgumentNullException("binaryReader");
			}
			ulong[] array = new ulong[1024];
			for (int i = 0; i < 1024; i++)
			{
				array[i] = binaryReader.ReadUInt64();
			}
			this.Initialize(cardinality, array);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003750 File Offset: 0x00001950
		public IContainer Add(char index)
		{
			ulong num = this.Bitmap[(int)(index >> 6)];
			ulong num2 = num | (1UL << (int)index);
			this.Bitmap[(int)(index >> 6)] = num2;
			if (num != num2)
			{
				int num3 = this.Cardinality + 1;
				this.Cardinality = num3;
			}
			return this;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003790 File Offset: 0x00001990
		public bool IsEmpty()
		{
			return this.Cardinality == 0;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000379C File Offset: 0x0000199C
		public IEnumerable<uint> Values()
		{
			List<uint> list = new List<uint>();
			uint num = 0U;
			while (num < 4294967295U && list.Count < this.Cardinality)
			{
				if ((this.Bitmap[(int)(num >> 6)] & (1UL << (int)num)) > 0UL)
				{
					list.Add(num);
				}
				num += 1U;
			}
			return list;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000037E7 File Offset: 0x000019E7
		public bool Contains(char lsbIndex)
		{
			return this.Cardinality >= 1 && (this.Bitmap[(int)(lsbIndex >> 6)] & (1UL << (int)lsbIndex)) > 0UL;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000380C File Offset: 0x00001A0C
		public IContainer Remove(char index)
		{
			char c = index >> 6;
			ulong num = this.Bitmap[(int)c];
			ulong num2 = 1UL << (int)index;
			if (this.Cardinality == ArrayContainer.DefaultMaxSize + 1 && (num & num2) != 0UL)
			{
				int num3 = this.Cardinality - 1;
				this.Cardinality = num3;
				this.Bitmap[(int)c] = num & ~num2;
				return this.ToArrayContainer();
			}
			ulong num4 = num & ~num2;
			this.Cardinality -= ((num4 != num) ? 1 : 0);
			this.Bitmap[(int)c] = num4;
			return this;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000388C File Offset: 0x00001A8C
		public void ToArray(ushort[] array)
		{
			int num = 0;
			int num2 = 0;
			foreach (ulong num3 in this.Bitmap)
			{
				while (num3 != 0UL)
				{
					array[num] = (ushort)(num2 + Utility.TrailingZeroCount(num3));
					num3 &= num3 - 1UL;
					num++;
				}
				num2 += 64;
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000038E0 File Offset: 0x00001AE0
		public void ArrayToBitmap(ArrayContainer arrayContainer)
		{
			this.Cardinality = arrayContainer.Cardinality;
			char c = '\0';
			while ((int)c < this.Cardinality)
			{
				ushort indexAtPosition = arrayContainer.GetIndexAtPosition(c);
				this.Bitmap[(int)(indexAtPosition / 64)] |= 1UL << (int)indexAtPosition;
				c += '\u0001';
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003930 File Offset: 0x00001B30
		public int NumberOfRunsLowerBound(int mustNotExceed)
		{
			int num = 0;
			int num2 = 0;
			while (num2 + BitmapContainer.BlockSize <= this.Bitmap.Length)
			{
				for (int i = num2; i < num2 + BitmapContainer.BlockSize; i++)
				{
					ulong num3 = this.Bitmap[i];
					num += Utility.PopCount(~num3 & (num3 << 1));
				}
				if (num > mustNotExceed)
				{
					return num;
				}
				num2 += BitmapContainer.BlockSize;
			}
			return num;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000398C File Offset: 0x00001B8C
		public IContainer RunOptimize()
		{
			int num = this.NumberOfRunsLowerBound(BitmapContainer.MaxRuns);
			if (RunContainer.SerializedSizeInBytes(num) >= this.ArraySizeInBytes)
			{
				return this;
			}
			num += this.NumberOfRunsAdjustment();
			int num2 = RunContainer.SerializedSizeInBytes(num);
			if (this.ArraySizeInBytes > num2)
			{
				return new RunContainer(this, num);
			}
			return this;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000039D8 File Offset: 0x00001BD8
		public int NumberOfRunsAdjustment()
		{
			int num = 0;
			ulong num2 = this.Bitmap[0];
			ulong num3;
			for (int i = 0; i < this.Bitmap.Length - 1; i++)
			{
				num3 = num2;
				num2 = this.Bitmap[i + 1];
				num += (int)((num3 >> 63) & ~num2);
			}
			num3 = num2;
			if ((num3 & 9223372036854775808UL) != 0UL)
			{
				num++;
			}
			return num;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003A33 File Offset: 0x00001C33
		public int NumberOfRuns()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003A3C File Offset: 0x00001C3C
		private ArrayContainer ToArrayContainer()
		{
			ArrayContainer arrayContainer = new ArrayContainer(this.Cardinality);
			if (this.Cardinality != 0)
			{
				arrayContainer.BitMapToArrayContainer(this);
			}
			if (arrayContainer.Cardinality != this.Cardinality)
			{
				throw new Exception("Could not create Array container from Bitmap container correctly");
			}
			return arrayContainer;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003A7E File Offset: 0x00001C7E
		private void Initialize(int cardinality, ulong[] Bitmap)
		{
			this.Cardinality = cardinality;
			this.Bitmap = Bitmap;
		}

		// Token: 0x04000023 RID: 35
		public const int MaxCapacity = 65536;

		// Token: 0x04000024 RID: 36
		private const int BitmapLength = 1024;

		// Token: 0x04000025 RID: 37
		private const int Size = 8192;

		// Token: 0x04000026 RID: 38
		private static readonly int BlockSize = 128;

		// Token: 0x04000027 RID: 39
		private static readonly int MaxRuns = 2047;
	}
}
