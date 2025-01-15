using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.DI.RoaringBitmap.Utilities;

namespace Microsoft.DI.RoaringBitmap.Containers
{
	// Token: 0x02000010 RID: 16
	internal class RunContainer : IContainer
	{
		// Token: 0x06000085 RID: 133 RVA: 0x00003AA4 File Offset: 0x00001CA4
		public RunContainer()
		{
			this.nbrRuns = 0;
			this.valueslength = new char[0];
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003ABF File Offset: 0x00001CBF
		public RunContainer(int firstOfRun, int lastOfRun)
		{
			this.nbrRuns = 1;
			this.valueslength = new char[]
			{
				(char)firstOfRun,
				(char)(lastOfRun - 1 - firstOfRun)
			};
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003AE8 File Offset: 0x00001CE8
		public RunContainer(char[] lengthsAndValues, int nbrRuns)
		{
			this.nbrRuns = nbrRuns;
			this.valueslength = lengthsAndValues;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003B00 File Offset: 0x00001D00
		public RunContainer(ArrayContainer arr, int nbrRuns)
		{
			this.nbrRuns = nbrRuns;
			this.valueslength = new char[2 * nbrRuns];
			if (nbrRuns == 0)
			{
				return;
			}
			int num = -2;
			int num2 = 0;
			int num3 = 0;
			for (int i = 0; i < arr.Cardinality; i++)
			{
				int num4 = (int)arr.Lsb16BitIndexList[i];
				if (num4 == num + 1)
				{
					num2++;
				}
				else
				{
					if (num3 > 0)
					{
						this.SetLength(num3 - 1, (char)num2);
					}
					this.SetValue(num3, (char)num4);
					num2 = 0;
					num3++;
				}
				num = num4;
			}
			this.SetLength(num3 - 1, (char)num2);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003B88 File Offset: 0x00001D88
		public RunContainer(BitmapContainer bc, int nbrRuns)
		{
			this.nbrRuns = nbrRuns;
			this.valueslength = new char[2 * nbrRuns];
			if (nbrRuns == 0)
			{
				return;
			}
			int num = 0;
			long num2 = (long)bc.Bitmap[0];
			int num3 = 0;
			int num4;
			int num6;
			for (;;)
			{
				if (num2 != 0L || num >= bc.Bitmap.Length - 1)
				{
					if (num2 == 0L)
					{
						break;
					}
					num4 = Utility.TrailingZeroCount((ulong)num2) + 64 * num;
					long num5 = num2 | (num2 - 1L);
					while (num5 == -1L && num < bc.Bitmap.Length - 1)
					{
						num5 = (long)bc.Bitmap[++num];
					}
					if (num5 == -1L)
					{
						goto Block_5;
					}
					num6 = Utility.TrailingZeroCount((ulong)(~(ulong)num5)) + num * 64;
					this.SetValue(num3, (char)num4);
					this.SetLength(num3, (char)(num6 - num4 - 1));
					num3++;
					num2 = num5 & (num5 + 1L);
				}
				else
				{
					num2 = (long)bc.Bitmap[++num];
				}
			}
			return;
			Block_5:
			num6 = 64 + num * 64;
			this.SetValue(num3, (char)num4);
			this.SetLength(num3, (char)(num6 - num4 - 1));
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00003C7C File Offset: 0x00001E7C
		public int Cardinality
		{
			get
			{
				int num = this.nbrRuns;
				for (int i = 0; i < this.nbrRuns; i++)
				{
					num += (int)this.GetLength(i);
				}
				return num;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00003CAC File Offset: 0x00001EAC
		public int ArraySizeInBytes
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003CAF File Offset: 0x00001EAF
		public static int SerializedSizeInBytes(int numberOfRuns)
		{
			return 2 + 4 * numberOfRuns;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003CB6 File Offset: 0x00001EB6
		public char GetValue(int index)
		{
			return this.valueslength[2 * index];
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003CC2 File Offset: 0x00001EC2
		public char GetLength(int index)
		{
			return this.valueslength[2 * index + 1];
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003CD0 File Offset: 0x00001ED0
		public IContainer Add(char index)
		{
			return this.ToBitmapOrArrayContainer().Add(index);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003CE0 File Offset: 0x00001EE0
		public bool Contains(char index)
		{
			int num = this.BinarySearch((int)index);
			if (num >= 0)
			{
				return true;
			}
			num = -num - 2;
			if (num != -1)
			{
				int num2 = (int)(index - this.GetValue(num));
				int length = (int)this.GetLength(num);
				return num2 <= length;
			}
			return false;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003D1D File Offset: 0x00001F1D
		public bool IsEmpty()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003D24 File Offset: 0x00001F24
		public IEnumerable<uint> Values()
		{
			List<uint> list = new List<uint>();
			for (int i = 0; i < this.nbrRuns; i++)
			{
				char value = this.GetValue(i);
				int num = (int)(value + this.GetLength(i));
				for (int j = (int)value; j <= num; j++)
				{
					list.Add((uint)((ushort)j));
				}
			}
			return list;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003D6D File Offset: 0x00001F6D
		public IContainer Remove(char index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003D74 File Offset: 0x00001F74
		public IContainer RunOptimize()
		{
			int num = RunContainer.SerializedSizeInBytes(this.nbrRuns);
			int num2 = BitmapContainer.SerializedSizeInBytes();
			int num3 = ArrayContainer.SerializedSizeInBytes(this.Cardinality);
			if (num <= Math.Min(num2, num3))
			{
				return this;
			}
			return this.ToBitmapOrArrayContainer();
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003DAF File Offset: 0x00001FAF
		public int NumberOfRuns()
		{
			return this.nbrRuns;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003DB8 File Offset: 0x00001FB8
		public void Serialize(BinaryWriter binaryWriter)
		{
			binaryWriter.Write((ushort)this.nbrRuns);
			for (int i = 0; i < 2 * this.nbrRuns; i++)
			{
				binaryWriter.Write((ushort)this.valueslength[i]);
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003DF4 File Offset: 0x00001FF4
		public void Deserialize(BinaryReader binaryReader, int cardinality)
		{
			this.nbrRuns = (int)binaryReader.ReadUInt16();
			if (this.valueslength.Length < 2 * this.nbrRuns)
			{
				this.valueslength = new char[2 * this.nbrRuns];
			}
			for (int i = 0; i < 2 * this.nbrRuns; i++)
			{
				this.valueslength[i] = (char)binaryReader.ReadInt16();
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003E54 File Offset: 0x00002054
		private static void SetBitmapRange(ulong[] bitmap, int start, int end)
		{
			if (start == end)
			{
				return;
			}
			int num = start / 64;
			int num2 = (end - 1) / 64;
			if (num == num2)
			{
				bitmap[num] |= (ulong.MaxValue << start) & (ulong.MaxValue >> -end);
				return;
			}
			bitmap[num] |= ulong.MaxValue << start;
			for (int i = num + 1; i < num2; i++)
			{
				bitmap[i] = ulong.MaxValue;
			}
			bitmap[num2] |= ulong.MaxValue >> -end;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003ECA File Offset: 0x000020CA
		private void SetLength(int index, char v)
		{
			this.valueslength[2 * index + 1] = v;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003ED9 File Offset: 0x000020D9
		private void SetValue(int index, char v)
		{
			this.valueslength[2 * index] = v;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003EE8 File Offset: 0x000020E8
		private IContainer ToBitmapOrArrayContainer()
		{
			int cardinality = this.Cardinality;
			if (cardinality <= ArrayContainer.DefaultMaxSize)
			{
				ArrayContainer arrayContainer = new ArrayContainer(cardinality);
				arrayContainer.Cardinality = 0;
				for (int i = 0; i < this.nbrRuns; i++)
				{
					char value = this.GetValue(i);
					int num = (int)(value + this.GetLength(i));
					for (int j = (int)value; j <= num; j++)
					{
						ushort[] lsb16BitIndexList = arrayContainer.Lsb16BitIndexList;
						ArrayContainer arrayContainer2 = arrayContainer;
						int cardinality2 = arrayContainer2.Cardinality;
						arrayContainer2.Cardinality = cardinality2 + 1;
						lsb16BitIndexList[cardinality2] = (ushort)j;
					}
				}
				return arrayContainer;
			}
			BitmapContainer bitmapContainer = new BitmapContainer();
			for (int k = 0; k < this.nbrRuns; k++)
			{
				int value2 = (int)this.GetValue(k);
				int num2 = value2 + (int)this.GetLength(k) + 1;
				RunContainer.SetBitmapRange(bitmapContainer.Bitmap, value2, num2);
			}
			bitmapContainer.Cardinality = cardinality;
			return bitmapContainer;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003FB0 File Offset: 0x000021B0
		private int BinarySearch(int index)
		{
			int i = 0;
			int num = this.valueslength.Length - 1;
			while (i <= num)
			{
				int num2 = (i + num) / 2;
				int num3 = (int)this.valueslength[2 * num2];
				if (num3 < index)
				{
					i = num2 + 1;
				}
				else
				{
					if (num3 <= index)
					{
						return num2;
					}
					num = num2 - 1;
				}
				if (i == num)
				{
					return i;
				}
			}
			return -(i + 1);
		}

		// Token: 0x0400002A RID: 42
		private char[] valueslength;

		// Token: 0x0400002B RID: 43
		private int nbrRuns;
	}
}
