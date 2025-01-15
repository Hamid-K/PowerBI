using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.DI.RoaringBitmap.Utilities;

namespace Microsoft.DI.RoaringBitmap.Containers
{
	// Token: 0x0200000D RID: 13
	internal class ArrayContainer : IContainer
	{
		// Token: 0x06000049 RID: 73 RVA: 0x0000322F File Offset: 0x0000142F
		public ArrayContainer()
		{
			this.Lsb16BitIndexList = new ushort[4];
			this.Lsb16BitIndexList.Fill(ushort.MaxValue);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003253 File Offset: 0x00001453
		public ArrayContainer(int capacity)
		{
			this.Lsb16BitIndexList = new ushort[capacity];
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003267 File Offset: 0x00001467
		public ArrayContainer(ushort[] lsb16BitIndexList)
		{
			this.Initialize(lsb16BitIndexList, lsb16BitIndexList.Length);
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00003279 File Offset: 0x00001479
		public static int DefaultMaxSize
		{
			get
			{
				return 4096;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00003280 File Offset: 0x00001480
		public int ArraySizeInBytes
		{
			get
			{
				return this.Cardinality * 2;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600004E RID: 78 RVA: 0x0000328A File Offset: 0x0000148A
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00003292 File Offset: 0x00001492
		public int Cardinality { get; set; }

		// Token: 0x06000050 RID: 80 RVA: 0x0000329B File Offset: 0x0000149B
		public static int SerializedSizeInBytes(int cardinality)
		{
			return cardinality * 2 + 2;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000032A4 File Offset: 0x000014A4
		public void Serialize(BinaryWriter binaryWriter)
		{
			for (int i = 0; i < this.Cardinality; i++)
			{
				binaryWriter.Write(this.Lsb16BitIndexList[i]);
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000032D0 File Offset: 0x000014D0
		public void Deserialize(BinaryReader binaryReader, int cardinality)
		{
			ushort[] array = new ushort[cardinality];
			for (int i = 0; i < cardinality; i++)
			{
				array[i] = binaryReader.ReadUInt16();
			}
			this.Initialize(array, cardinality);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003301 File Offset: 0x00001501
		public bool Contains(char lsb16BitIndex)
		{
			return this.Cardinality >= 1 && Array.BinarySearch<ushort>(this.Lsb16BitIndexList, 0, this.Cardinality, (ushort)lsb16BitIndex) >= 0;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003328 File Offset: 0x00001528
		public IEnumerable<uint> Values()
		{
			List<uint> list = new List<uint>();
			uint num = 0U;
			while ((ulong)num < (ulong)((long)this.Cardinality))
			{
				list.Add((uint)this.Lsb16BitIndexList[(int)num]);
				num += 1U;
			}
			return list;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000335D File Offset: 0x0000155D
		public IContainer ToBitmapContainer()
		{
			BitmapContainer bitmapContainer = new BitmapContainer();
			bitmapContainer.ArrayToBitmap(this);
			return bitmapContainer;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000336C File Offset: 0x0000156C
		public IContainer Add(char index)
		{
			if (this.Cardinality >= ArrayContainer.DefaultMaxSize)
			{
				return this.ToBitmapContainer().Add(index);
			}
			if (this.Cardinality == 0)
			{
				this.SetIndex(index);
			}
			else if (this.Cardinality > 0 && index > (char)this.Lsb16BitIndexList[this.Cardinality - 1])
			{
				if (this.Cardinality <= this.Lsb16BitIndexList.Length - 1)
				{
					this.SetIndex(index);
				}
				else
				{
					this.IncreaseCapacity();
					this.SetIndex(index);
				}
			}
			else
			{
				int num = Array.BinarySearch<ushort>(this.Lsb16BitIndexList, 0, this.Cardinality, (ushort)index);
				if (num < 0)
				{
					num = -num - 1;
					if (this.Cardinality >= this.Lsb16BitIndexList.Length)
					{
						this.IncreaseCapacity();
					}
					Array.Copy(this.Lsb16BitIndexList, num, this.Lsb16BitIndexList, num + 1, this.Cardinality - num);
					this.Lsb16BitIndexList[num] = (ushort)index;
					int num2 = this.Cardinality + 1;
					this.Cardinality = num2;
				}
			}
			return this;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003456 File Offset: 0x00001656
		public void BitMapToArrayContainer(BitmapContainer bitmap)
		{
			this.Cardinality = bitmap.Cardinality;
			bitmap.ToArray(this.Lsb16BitIndexList);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003470 File Offset: 0x00001670
		public void IncreaseCapacity()
		{
			int num = ((this.Lsb16BitIndexList.Length == 0) ? 4 : ((this.Lsb16BitIndexList.Length < 64) ? (this.Lsb16BitIndexList.Length * 2) : ((this.Lsb16BitIndexList.Length < 1067) ? (this.Lsb16BitIndexList.Length * 3 / 2) : (this.Lsb16BitIndexList.Length * 5 / 4))));
			if (num > ArrayContainer.DefaultMaxSize)
			{
				num = ArrayContainer.DefaultMaxSize;
			}
			if (num > ArrayContainer.DefaultMaxSize - ArrayContainer.DefaultMaxSize / 16)
			{
				num = ArrayContainer.DefaultMaxSize;
			}
			Array.Resize<ushort>(ref this.Lsb16BitIndexList, num);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000034FC File Offset: 0x000016FC
		public bool IsEmpty()
		{
			return this.Cardinality == 0;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003508 File Offset: 0x00001708
		public IContainer Remove(char index)
		{
			int num = Array.BinarySearch<ushort>(this.Lsb16BitIndexList, 0, this.Cardinality, (ushort)index);
			if (num >= 0)
			{
				this.RemoveindexAtIndex(num);
			}
			return this;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003538 File Offset: 0x00001738
		public IContainer RunOptimize()
		{
			int num = this.NumberOfRuns();
			if (this.ArraySizeInBytes > RunContainer.SerializedSizeInBytes(num))
			{
				return new RunContainer(this, num);
			}
			return this;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003564 File Offset: 0x00001764
		public int NumberOfRuns()
		{
			if (this.Cardinality == 0)
			{
				return 0;
			}
			int num = 1;
			int num2 = (int)this.Lsb16BitIndexList[0];
			for (int i = 1; i < this.Cardinality; i++)
			{
				int num3 = (int)this.Lsb16BitIndexList[i];
				if (num2 + 1 != num3)
				{
					num++;
				}
				num2 = num3;
			}
			return num;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000035AD File Offset: 0x000017AD
		public ushort GetIndexAtPosition(char containerIndex)
		{
			return this.Lsb16BitIndexList[(int)containerIndex];
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000035B8 File Offset: 0x000017B8
		private void SetIndex(char index)
		{
			this.Lsb16BitIndexList[this.Cardinality] = (ushort)index;
			int cardinality = this.Cardinality;
			this.Cardinality = cardinality + 1;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000035E4 File Offset: 0x000017E4
		private void RemoveindexAtIndex(int containerIndex)
		{
			Array.Copy(this.Lsb16BitIndexList, containerIndex + 1, this.Lsb16BitIndexList, containerIndex, this.Cardinality - containerIndex - 1);
			int cardinality = this.Cardinality;
			this.Cardinality = cardinality - 1;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003620 File Offset: 0x00001820
		private void Initialize(ushort[] lsb16BitIndexList, int cardinality)
		{
			this.Cardinality = cardinality;
			this.Lsb16BitIndexList = lsb16BitIndexList;
		}

		// Token: 0x04000020 RID: 32
		public ushort[] Lsb16BitIndexList;

		// Token: 0x04000021 RID: 33
		private const int DefaultInitialSize = 4;
	}
}
