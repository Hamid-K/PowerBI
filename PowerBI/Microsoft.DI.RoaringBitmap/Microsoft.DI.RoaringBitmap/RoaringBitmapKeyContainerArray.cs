using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.DI.RoaringBitmap.Containers;
using Microsoft.DI.RoaringBitmap.Utilities;

namespace Microsoft.DI.RoaringBitmap
{
	// Token: 0x02000007 RID: 7
	internal class RoaringBitmapKeyContainerArray
	{
		// Token: 0x06000023 RID: 35 RVA: 0x00002455 File Offset: 0x00000655
		public RoaringBitmapKeyContainerArray()
		{
			this.msbKeys = new char[4];
			this.containersArray = new IContainer[4];
			this.msbKeys.Fill(char.MaxValue);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002485 File Offset: 0x00000685
		public RoaringBitmapKeyContainerArray(int roaringBitmapsListSize, char[] msbKeys, IContainer[] containersArray)
		{
			this.Initialize(roaringBitmapsListSize, msbKeys, containersArray);
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002498 File Offset: 0x00000698
		public ulong Cardinality
		{
			get
			{
				if (this.containersArray == null)
				{
					return 0UL;
				}
				ulong num = 0UL;
				foreach (IContainer container in this.containersArray)
				{
					if (container != null)
					{
						num += (ulong)((long)container.Cardinality);
					}
				}
				return num;
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000024DC File Offset: 0x000006DC
		public void Serialize(ref Stream stream, bool hasRun)
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(stream, Encoding.UTF8, true))
			{
				int size = this.GetSize();
				char[] array = this.msbKeys;
				IContainer[] array2 = this.containersArray;
				int num2;
				if (hasRun)
				{
					binaryWriter.Write(12347 | (size - 1 << 16));
					byte[] array3 = new byte[(size + 7) / 8];
					for (int i = 0; i < size; i++)
					{
						if (this.containersArray[i] is RunContainer)
						{
							byte[] array4 = array3;
							int num = i / 8;
							array4[num] |= (byte)(1 << i % 8);
						}
					}
					binaryWriter.Write(array3);
					if (this.GetSize() < 4)
					{
						num2 = 4 + 4 * this.GetSize() + array3.Length;
					}
					else
					{
						num2 = 4 + 8 * this.GetSize() + array3.Length;
					}
				}
				else
				{
					binaryWriter.Write(12346);
					binaryWriter.Write(size);
					num2 = 8 + 4 * size + 4 * size;
				}
				for (int j = 0; j < size; j++)
				{
					binaryWriter.Write(Convert.ToUInt16(array[j]));
					binaryWriter.Write(Convert.ToUInt16(array2[j].Cardinality - 1));
				}
				if (!hasRun || size >= 4)
				{
					for (int k = 0; k < size; k++)
					{
						binaryWriter.Write(num2);
						num2 += array2[k].ArraySizeInBytes;
					}
				}
				for (int l = 0; l < size; l++)
				{
					array2[l].Serialize(binaryWriter);
				}
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002668 File Offset: 0x00000868
		public void Deserialize(Stream stream)
		{
			if (stream == null)
			{
				return;
			}
			using (BinaryReader binaryReader = new BinaryReader(stream, Encoding.UTF8, true))
			{
				int num = binaryReader.ReadInt32();
				bool flag = (num & 65535) == 12347;
				if (!flag && num != 12346)
				{
					throw new InvalidDataException("No RoaringBitmap file.");
				}
				int num2 = (((num & 65535) == 12347) ? ((num >> 16) + 1) : binaryReader.ReadInt32());
				if (num2 > 65536)
				{
					throw new InvalidOperationException("Size too large");
				}
				byte[] array = null;
				if ((num & 65535) == 12347)
				{
					array = binaryReader.ReadBytes((num2 + 7) / 8);
				}
				char[] array2 = new char[num2];
				int[] array3 = new int[num2];
				bool[] array4 = new bool[num2];
				for (int i = 0; i < num2; i++)
				{
					array2[i] = (char)binaryReader.ReadUInt16();
					array3[i] = (int)(1 + (ushort.MaxValue & binaryReader.ReadUInt16()));
					array4[i] = array3[i] > ArrayContainer.DefaultMaxSize;
					if (array != null && ((int)array[i / 8] & (1 << i % 8)) != 0)
					{
						array4[i] = false;
					}
				}
				if (!flag || num2 >= 4)
				{
					binaryReader.ReadBytes(num2 * 4);
				}
				IContainer[] array5 = new IContainer[num2];
				for (int j = 0; j < num2; j++)
				{
					if (array4[j])
					{
						array5[j] = new BitmapContainer();
						array5[j].Deserialize(binaryReader, array3[j]);
					}
					else if (array != null && ((int)array[j / 8] & (1 << j % 8)) != 0)
					{
						array5[j] = new RunContainer();
						array5[j].Deserialize(binaryReader, array3[j]);
					}
					else
					{
						array5[j] = new ArrayContainer();
						array5[j].Deserialize(binaryReader, array3[j]);
					}
				}
				this.Initialize(num2, array2, array5);
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002844 File Offset: 0x00000A44
		public int GetContainerIndex(char keyIndex)
		{
			return Array.BinarySearch<char>(this.msbKeys, 0, this.roaringBitmapsListSize, keyIndex);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002859 File Offset: 0x00000A59
		public IContainer GetContainer(ushort keyIndex)
		{
			return this.containersArray[(int)keyIndex];
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002863 File Offset: 0x00000A63
		public int GetSize()
		{
			return this.roaringBitmapsListSize;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000286B File Offset: 0x00000A6B
		public void SetContainerAtIndex(uint indexPosition, IContainer container)
		{
			this.containersArray[(int)indexPosition] = container;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002878 File Offset: 0x00000A78
		public void InsertAtIndex(uint indexPosition, char newMsbIndex, IContainer newContainer)
		{
			this.IncreaseCapacity(1);
			Array.Copy(this.msbKeys, (long)((ulong)indexPosition), this.msbKeys, (long)((ulong)(indexPosition + 1U)), (long)this.roaringBitmapsListSize - (long)((ulong)indexPosition));
			this.msbKeys[(int)indexPosition] = newMsbIndex;
			Array.Copy(this.containersArray, (long)((ulong)indexPosition), this.containersArray, (long)((ulong)(indexPosition + 1U)), (long)this.roaringBitmapsListSize - (long)((ulong)indexPosition));
			this.containersArray[(int)indexPosition] = newContainer;
			this.roaringBitmapsListSize++;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000028F0 File Offset: 0x00000AF0
		public void RemoveWithContainerAtIndex(int index)
		{
			Array.Copy(this.msbKeys, index + 1, this.msbKeys, index, this.roaringBitmapsListSize - index - 1);
			this.msbKeys[this.roaringBitmapsListSize - 1] = char.MaxValue;
			Array.Copy(this.containersArray, index + 1, this.containersArray, index, this.roaringBitmapsListSize - index - 1);
			Array.Clear(this.containersArray, this.roaringBitmapsListSize - 1, 1);
			this.roaringBitmapsListSize--;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002974 File Offset: 0x00000B74
		public IEnumerable<T> Values<T>(uint msbToAdd = 0U)
		{
			List<T> list = new List<T>(65535);
			ulong num = (ulong)msbToAdd << 32;
			uint num2 = 0U;
			while ((ulong)num2 < (ulong)((long)this.roaringBitmapsListSize))
			{
				if (this.containersArray[(int)num2] != null)
				{
					IEnumerable<uint> enumerable = this.containersArray[(int)num2].Values();
					List<T> list2 = new List<T>(65535);
					foreach (uint num3 in enumerable)
					{
						uint num4 = (uint)((uint)this.msbKeys[(int)num2] << 16) | num3;
						T t = (T)((object)(num | (ulong)num4));
						list2.Add(t);
					}
					list.AddRange(list2);
				}
				num2 += 1U;
			}
			return list;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002A34 File Offset: 0x00000C34
		public int SerializedSizeInBytes()
		{
			int num = this.HeaderSizeForSerialization();
			for (int i = 0; i < this.GetSize(); i++)
			{
				num += this.containersArray[i].ArraySizeInBytes;
			}
			return num;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002A6C File Offset: 0x00000C6C
		private int HeaderSizeForSerialization()
		{
			int size = this.GetSize();
			if (!this.HasRunContainer())
			{
				return 8 + 8 * size;
			}
			if (size < 4)
			{
				return 4 + (size + 7) / 8 + 4 * size;
			}
			return 4 + (size + 7) / 8 + 8 * size;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002AAC File Offset: 0x00000CAC
		private void IncreaseCapacity(int increaseAtleast)
		{
			if (this.roaringBitmapsListSize + increaseAtleast > this.msbKeys.Length)
			{
				int num;
				if (this.msbKeys.Length < 1024)
				{
					num = 2 * (this.roaringBitmapsListSize + increaseAtleast);
				}
				else
				{
					num = 5 * (this.roaringBitmapsListSize + increaseAtleast) / 4;
				}
				Array.Resize<char>(ref this.msbKeys, num);
				Array.Resize<IContainer>(ref this.containersArray, num);
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002B0C File Offset: 0x00000D0C
		private void Initialize(int roaringBitmapsListSize, char[] msbKeys, IContainer[] containersArray)
		{
			this.roaringBitmapsListSize = roaringBitmapsListSize;
			this.msbKeys = msbKeys;
			this.containersArray = containersArray;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002B24 File Offset: 0x00000D24
		private bool HasRunContainer()
		{
			for (int i = 0; i < this.GetSize(); i++)
			{
				if (this.containersArray[i] is RunContainer)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000008 RID: 8
		private const int SerialCookieNoRuncontainer = 12346;

		// Token: 0x04000009 RID: 9
		private const int SerialCookie = 12347;

		// Token: 0x0400000A RID: 10
		private const int NoOffsetThreshold = 4;

		// Token: 0x0400000B RID: 11
		private const int DefaultInitialSize = 4;

		// Token: 0x0400000C RID: 12
		private const int InitialCapacity = 4;

		// Token: 0x0400000D RID: 13
		private int roaringBitmapsListSize;

		// Token: 0x0400000E RID: 14
		private char[] msbKeys;

		// Token: 0x0400000F RID: 15
		private IContainer[] containersArray;
	}
}
