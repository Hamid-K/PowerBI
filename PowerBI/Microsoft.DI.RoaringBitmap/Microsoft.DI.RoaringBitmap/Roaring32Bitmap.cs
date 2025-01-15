using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.DI.RoaringBitmap.Containers;
using Microsoft.DI.RoaringBitmap.Utilities;

namespace Microsoft.DI.RoaringBitmap
{
	// Token: 0x02000006 RID: 6
	internal class Roaring32Bitmap : IRoaring32Bitmap
	{
		// Token: 0x06000014 RID: 20 RVA: 0x000020AB File Offset: 0x000002AB
		public Roaring32Bitmap()
		{
			this.roaringBitmapKeyContainerArray = new RoaringBitmapKeyContainerArray();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000020C5 File Offset: 0x000002C5
		internal Roaring32Bitmap(RoaringBitmapKeyContainerArray roaringBitmapKeyContainerArray)
		{
			this.roaringBitmapKeyContainerArray = roaringBitmapKeyContainerArray;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000020DB File Offset: 0x000002DB
		public ulong Cardinality
		{
			get
			{
				return this.roaringBitmapKeyContainerArray.Cardinality;
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000020E8 File Offset: 0x000002E8
		public void Serialize(Stream stream)
		{
			if (this == null)
			{
				return;
			}
			bool flag = this.RunOptimize();
			this.roaringBitmapKeyContainerArray.Serialize(ref stream, flag);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000210E File Offset: 0x0000030E
		public void Deserialize(Stream stream)
		{
			if (this.roaringBitmapKeyContainerArray == null)
			{
				this.roaringBitmapKeyContainerArray = new RoaringBitmapKeyContainerArray();
			}
			this.roaringBitmapKeyContainerArray.Deserialize(stream);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000212F File Offset: 0x0000032F
		public int SerializedSizeInBytes()
		{
			return this.roaringBitmapKeyContainerArray.SerializedSizeInBytes();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000213C File Offset: 0x0000033C
		public void Add(IEnumerable<uint> indexList)
		{
			foreach (uint num in indexList)
			{
				this.Add(num);
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002184 File Offset: 0x00000384
		public void Add(uint index)
		{
			Pair<char> highLowSignificantBits = Utility.GetHighLowSignificantBits(index);
			int containerIndex = this.GetContainerIndex(highLowSignificantBits.High);
			if (this.visitedContainerIndex != null && this.visitedContainer != null)
			{
				int num = containerIndex;
				int? num2 = this.visitedContainerIndex;
				if ((num == num2.GetValueOrDefault()) & (num2 != null))
				{
					IContainer container = this.visitedContainer.Add(highLowSignificantBits.Low);
					this.roaringBitmapKeyContainerArray.SetContainerAtIndex((uint)this.visitedContainerIndex.Value, container);
					this.visitedContainer = container;
					return;
				}
			}
			this.visitedContainer = null;
			this.visitedContainerIndex = new int?(this.AddIndexAndSetContainer(highLowSignificantBits, containerIndex, ref this.visitedContainer));
			this.visitedContainerMsb = (int)highLowSignificantBits.High;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002238 File Offset: 0x00000438
		public bool Contains(uint index)
		{
			if (this.Cardinality < 1UL)
			{
				return false;
			}
			Pair<char> highLowSignificantBits = Utility.GetHighLowSignificantBits(index);
			int containerIndex = this.GetContainerIndex(highLowSignificantBits.High);
			return containerIndex >= 0 && this.roaringBitmapKeyContainerArray.GetContainer((ushort)Convert.ToChar(containerIndex)).Contains(highLowSignificantBits.Low);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002289 File Offset: 0x00000489
		public IEnumerable<T> Values<T>(uint msbToAdd = 0U)
		{
			return this.roaringBitmapKeyContainerArray.Values<T>(msbToAdd);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002298 File Offset: 0x00000498
		public void Remove(uint index)
		{
			Pair<char> highLowSignificantBits = Utility.GetHighLowSignificantBits(index);
			int containerIndex = this.GetContainerIndex(highLowSignificantBits.High);
			if (containerIndex < 0)
			{
				return;
			}
			this.roaringBitmapKeyContainerArray.SetContainerAtIndex((uint)Convert.ToChar(containerIndex), this.roaringBitmapKeyContainerArray.GetContainer((ushort)Convert.ToChar(containerIndex)).Remove(highLowSignificantBits.Low));
			if (this.roaringBitmapKeyContainerArray.GetContainer((ushort)Convert.ToChar(containerIndex)).IsEmpty())
			{
				this.roaringBitmapKeyContainerArray.RemoveWithContainerAtIndex(containerIndex);
				this.size--;
				this.visitedContainerIndex = null;
				this.visitedContainer = null;
				this.visitedContainerMsb = -1;
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002339 File Offset: 0x00000539
		public bool IsEmpty()
		{
			return this.roaringBitmapKeyContainerArray.GetSize() == 0;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000234C File Offset: 0x0000054C
		public bool RunOptimize()
		{
			bool flag = false;
			int num = this.roaringBitmapKeyContainerArray.GetSize();
			ushort num2 = 0;
			while ((int)num2 < num)
			{
				IContainer container = this.roaringBitmapKeyContainerArray.GetContainer(num2).RunOptimize();
				if (container is RunContainer)
				{
					flag = true;
				}
				this.roaringBitmapKeyContainerArray.SetContainerAtIndex((uint)num2, container);
				num2 += 1;
			}
			return flag;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000023A0 File Offset: 0x000005A0
		private int GetContainerIndex(char msb16Bit)
		{
			int num = this.visitedContainerIndex.GetValueOrDefault(-1);
			if (this.visitedContainerMsb != (int)msb16Bit)
			{
				num = this.roaringBitmapKeyContainerArray.GetContainerIndex(msb16Bit);
			}
			return num;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000023D4 File Offset: 0x000005D4
		private int AddIndexAndSetContainer(Pair<char> split, int containerIndex, ref IContainer container)
		{
			if (containerIndex >= 0)
			{
				container = this.roaringBitmapKeyContainerArray.GetContainer((ushort)Convert.ToChar(containerIndex));
				IContainer container2 = container.Add(split.Low);
				this.roaringBitmapKeyContainerArray.SetContainerAtIndex((uint)containerIndex, container2);
				return containerIndex;
			}
			containerIndex = (int)((ushort)(-containerIndex - 1));
			container = new ArrayContainer().Add(split.Low);
			this.roaringBitmapKeyContainerArray.InsertAtIndex((uint)((ushort)containerIndex), split.High, container);
			this.size++;
			return containerIndex;
		}

		// Token: 0x04000003 RID: 3
		private RoaringBitmapKeyContainerArray roaringBitmapKeyContainerArray;

		// Token: 0x04000004 RID: 4
		private int size;

		// Token: 0x04000005 RID: 5
		private int? visitedContainerIndex;

		// Token: 0x04000006 RID: 6
		private IContainer visitedContainer;

		// Token: 0x04000007 RID: 7
		private int visitedContainerMsb = -1;
	}
}
