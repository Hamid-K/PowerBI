using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.DI.RoaringBitmap.Utilities;

namespace Microsoft.DI.RoaringBitmap
{
	// Token: 0x02000008 RID: 8
	public class RoaringBitmap<T> : IRoaringBitmap<T>
	{
		// Token: 0x06000034 RID: 52 RVA: 0x00002B54 File Offset: 0x00000D54
		public RoaringBitmap()
		{
			this.msb32BitsToBitmap = new IRoaring32Bitmap[4];
			this.msb32BitKeys = new uint?[4];
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002B88 File Offset: 0x00000D88
		public ulong Cardinality
		{
			get
			{
				if (this.msb32BitsToBitmap == null || this.roaring64BitmapsListSize == 0U)
				{
					return 0UL;
				}
				ulong num = 0UL;
				foreach (IRoaring32Bitmap roaring32Bitmap in this.msb32BitsToBitmap)
				{
					if (roaring32Bitmap != null)
					{
						num += roaring32Bitmap.Cardinality;
					}
				}
				return num;
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002BD4 File Offset: 0x00000DD4
		public void Add(IEnumerable<T> indexList)
		{
			if (indexList == null || !indexList.Any<T>())
			{
				return;
			}
			foreach (T t in indexList)
			{
				this.Add(t);
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002C28 File Offset: 0x00000E28
		public void Remove(IEnumerable<T> indexList)
		{
			foreach (T t in indexList)
			{
				Pair<uint> highLow32Bits = RoaringBitmap<T>.GetHighLow32Bits(t);
				int num = this.Get32BitRoaringBitmapIndex(highLow32Bits.High);
				if (num < 0)
				{
					break;
				}
				if (this.msb32BitsToBitmap[num] != null)
				{
					this.msb32BitsToBitmap[num].Remove(highLow32Bits.Low);
					if (this.msb32BitsToBitmap[num].IsEmpty())
					{
						Array.Copy(this.msb32BitKeys, (long)(num + 1), this.msb32BitKeys, (long)num, (long)((ulong)this.roaring64BitmapsListSize - (ulong)((long)num) - 1UL));
						this.msb32BitKeys[(int)(this.roaring64BitmapsListSize - 1U)] = new uint?(uint.MaxValue);
						Array.Copy(this.msb32BitsToBitmap, (long)(num + 1), this.msb32BitsToBitmap, (long)(num - 1), (long)((ulong)this.roaring64BitmapsListSize - (ulong)((long)num) - 1UL));
						Array.Clear(this.msb32BitsToBitmap, (int)(this.roaring64BitmapsListSize - 1U), 1);
						this.roaring64BitmapsListSize -= 1U;
						this.ResetVisited();
					}
				}
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002D4C File Offset: 0x00000F4C
		public IEnumerable<T> Values()
		{
			List<T> list = new List<T>();
			int num = 0;
			foreach (IRoaring32Bitmap roaring32Bitmap in this.msb32BitsToBitmap)
			{
				if (roaring32Bitmap != null)
				{
					list.AddRange(roaring32Bitmap.Values<T>(this.msb32BitKeys[num].Value));
				}
				num++;
			}
			return list;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002DA4 File Offset: 0x00000FA4
		public void Serialize(Stream stream)
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(stream, Encoding.UTF8, true))
			{
				binaryWriter.Write(Convert.ToInt64(this.roaring64BitmapsListSize));
				for (ulong num = 0UL; num < (ulong)this.roaring64BitmapsListSize; num += 1UL)
				{
					checked
					{
						binaryWriter.Write((int)this.msb32BitKeys[(int)((IntPtr)num)].Value);
						this.msb32BitsToBitmap[(int)((IntPtr)num)].Serialize(stream);
					}
				}
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002E28 File Offset: 0x00001028
		public void Deserialize(Stream stream)
		{
			if (stream == null)
			{
				return;
			}
			using (BinaryReader binaryReader = new BinaryReader(stream, Encoding.UTF8, true))
			{
				this.roaring64BitmapsListSize = Convert.ToUInt32(binaryReader.ReadUInt64());
				this.msb32BitKeys = new uint?[this.roaring64BitmapsListSize];
				for (ulong num = 0UL; num < (ulong)this.roaring64BitmapsListSize; num += 1UL)
				{
					checked
					{
						this.msb32BitKeys[(int)((IntPtr)num)] = new uint?(binaryReader.ReadUInt32());
						this.msb32BitsToBitmap[(int)((IntPtr)num)] = new Roaring32Bitmap();
						this.msb32BitsToBitmap[(int)((IntPtr)num)].Deserialize(stream);
					}
				}
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002ED0 File Offset: 0x000010D0
		public bool Contains(T index)
		{
			if (this.Cardinality < 1UL)
			{
				return false;
			}
			Pair<uint> highLow32Bits = RoaringBitmap<T>.GetHighLow32Bits(index);
			int num = this.Get32BitRoaringBitmapIndex(highLow32Bits.High);
			if (num < 0)
			{
				return false;
			}
			IRoaring32Bitmap roaring32Bitmap = this.msb32BitsToBitmap[num];
			return roaring32Bitmap != null && roaring32Bitmap.Contains(highLow32Bits.Low);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002F20 File Offset: 0x00001120
		private static Pair<uint> GetHighLow32Bits(T index)
		{
			ulong num = Convert.ToUInt64(index, CultureInfo.InvariantCulture);
			uint maxValue = uint.MaxValue;
			return Pair.New<uint>(Convert.ToUInt32(num >> 32), Convert.ToUInt32(num & (ulong)maxValue));
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002F58 File Offset: 0x00001158
		private void Add(T index)
		{
			Pair<uint> highLow32Bits = RoaringBitmap<T>.GetHighLow32Bits(index);
			if ((long)this.msb32BitsToBitmap.Length <= (long)((ulong)(this.roaring64BitmapsListSize + 1U)))
			{
				Array.Resize<IRoaring32Bitmap>(ref this.msb32BitsToBitmap, (int)(this.roaring64BitmapsListSize + 1U));
				Array.Resize<uint?>(ref this.msb32BitKeys, (int)(this.roaring64BitmapsListSize + 1U));
			}
			int num = this.Get32BitRoaringBitmapIndex(highLow32Bits.High);
			if (num < 0)
			{
				num = -num - 1;
				Array.Copy(this.msb32BitKeys, (long)num, this.msb32BitKeys, (long)(num + 1), (long)((ulong)this.roaring64BitmapsListSize - (ulong)((long)num)));
				Array.Copy(this.msb32BitsToBitmap, (long)num, this.msb32BitsToBitmap, (long)(num + 1), (long)((ulong)this.roaring64BitmapsListSize - (ulong)((long)num)));
				this.msb32BitKeys[num] = new uint?(highLow32Bits.High);
				this.msb32BitsToBitmap[num] = new Roaring32Bitmap();
				this.roaring64BitmapsListSize += 1U;
				this.visited32BitIndex = new int?(num);
			}
			IRoaring32Bitmap roaring32Bitmap = this.msb32BitsToBitmap[num];
			if (roaring32Bitmap != null)
			{
				roaring32Bitmap.Add(highLow32Bits.Low);
			}
			this.visited32BitMsb = (int)highLow32Bits.High;
			this.visited32BitIndex = new int?(num);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003074 File Offset: 0x00001274
		private int Get32BitRoaringBitmapIndex(uint msb32Bit)
		{
			int num = this.visited32BitIndex.GetValueOrDefault(-1);
			if ((long)this.visited32BitMsb != (long)((ulong)msb32Bit))
			{
				num = Array.BinarySearch<uint?>(this.msb32BitKeys, 0, (int)this.roaring64BitmapsListSize, new uint?(msb32Bit));
			}
			return num;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000030B3 File Offset: 0x000012B3
		private void ResetVisited()
		{
			this.visited32BitIndex = null;
			this.visited32BitMsb = -1;
		}

		// Token: 0x04000010 RID: 16
		private const int InitialMaxSize = 4;

		// Token: 0x04000011 RID: 17
		private IRoaring32Bitmap[] msb32BitsToBitmap;

		// Token: 0x04000012 RID: 18
		private uint?[] msb32BitKeys = new uint?[0];

		// Token: 0x04000013 RID: 19
		private uint roaring64BitmapsListSize;

		// Token: 0x04000014 RID: 20
		private int? visited32BitIndex;

		// Token: 0x04000015 RID: 21
		private int visited32BitMsb = -1;
	}
}
