using System;
using System.Diagnostics;
using System.IO;

namespace Microsoft.MachineLearning.Internal.Utilities
{
	// Token: 0x020004AB RID: 1195
	public sealed class SequencePool
	{
		// Token: 0x17000269 RID: 617
		// (get) Token: 0x060018AE RID: 6318 RVA: 0x0008BEEF File Offset: 0x0008A0EF
		public int Count
		{
			get
			{
				return this._idLim;
			}
		}

		// Token: 0x060018AF RID: 6319 RVA: 0x0008BEF8 File Offset: 0x0008A0F8
		public SequencePool()
		{
			this._mask = 31;
			this._buckets = Utils.CreateArray<int>(this._mask + 1, -1);
			this._next = new int[10];
			this._start = new int[11];
			this._hash = new uint[10];
			this._bytes = new byte[40];
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x0008BF5C File Offset: 0x0008A15C
		public SequencePool(BinaryReader reader)
		{
			this._idLim = reader.ReadInt32();
			Contracts.CheckDecode(0 <= this._idLim && this._idLim < int.MaxValue);
			this._start = Utils.ReadIntArray(reader, this._idLim + 1);
			Contracts.CheckDecode(Utils.Size<int>(this._start) > 0 && this._start[0] == 0);
			Contracts.CheckDecode(this._start[this._idLim] >= 0);
			this._bytes = Utils.ReadByteArray(reader, this._start[this._idLim]);
			if (this._idLim < 10)
			{
				Array.Resize<int>(ref this._start, 11);
			}
			if (Utils.Size<byte>(this._bytes) < 40)
			{
				Array.Resize<byte>(ref this._bytes, 40);
			}
			int num = Utils.IbitHigh((uint)Math.Max(this._idLim, 31));
			if (num < 31)
			{
				num++;
			}
			this._mask = (1 << num) - 1;
			this._buckets = Utils.CreateArray<int>(this._mask + 1, -1);
			this._hash = new uint[Math.Max(this._idLim, 10)];
			this._next = new int[Math.Max(this._idLim, 10)];
			uint[] array = null;
			int num2 = this._start[this._idLim];
			for (int i = 0; i < this._idLim; i++)
			{
				Contracts.CheckDecode(this._start[i] <= this._start[i + 1] && this._start[i + 1] <= num2);
				int num3 = SequencePool.Leb128ToUIntArray(this._bytes, this._start[i], this._start[i + 1], ref array);
				this._hash[i] = Hashing.HashSequence(array, 0, num3);
				int bucketIndex = this.GetBucketIndex(this._hash[i]);
				this._next[i] = this._buckets[bucketIndex];
				this._buckets[bucketIndex] = i;
			}
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x0008C150 File Offset: 0x0008A350
		public void Save(BinaryWriter writer)
		{
			writer.Write(this._idLim);
			Utils.WriteIntsNoCount(writer, this._start, this._idLim + 1);
			Utils.WriteBytesNoCount(writer, this._bytes, this._start[this._idLim]);
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x0008C18B File Offset: 0x0008A38B
		[Conditional("DEBUG")]
		private void AssertValid()
		{
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x0008C18D File Offset: 0x0008A38D
		private int GetFirstIdInBucket(uint hash)
		{
			return this._buckets[(int)(hash & (uint)this._mask)];
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x0008C19E File Offset: 0x0008A39E
		private int GetBucketIndex(uint hash)
		{
			return (int)(hash & (uint)this._mask);
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x0008C1A8 File Offset: 0x0008A3A8
		private int GetCore(uint[] sequence, int min, int lim, out uint hash)
		{
			hash = Hashing.HashSequence(sequence, min, lim);
			for (int i = this.GetFirstIdInBucket(hash); i >= 0; i = this._next[i])
			{
				if (this._hash[i] == hash)
				{
					int num = this._start[i];
					int num2 = this._start[i + 1];
					for (int j = min; j < lim; j++)
					{
						if (num >= num2)
						{
							goto IL_006F;
						}
						uint num3;
						SequencePool.TryDecodeOne(this._bytes, ref num, this._start[i + 1], out num3);
						if (sequence[j] != num3)
						{
							goto IL_006F;
						}
					}
					if (num == num2)
					{
						return i;
					}
				}
				IL_006F:;
			}
			return -1;
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x0008C234 File Offset: 0x0008A434
		public bool TryAdd(uint[] sequence, int min, int lim, out int id)
		{
			Contracts.Check(0 <= min && min <= lim && lim <= Utils.Size<uint>(sequence));
			uint num;
			id = this.GetCore(sequence, min, lim, out num);
			if (id >= 0)
			{
				return false;
			}
			id = this._idLim;
			this.AddCore(sequence, min, lim, num);
			return true;
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x0008C288 File Offset: 0x0008A488
		public int Get(uint[] sequence, int min, int lim)
		{
			Contracts.Check(0 <= min && min <= lim && lim <= Utils.Size<uint>(sequence));
			uint num;
			return this.GetCore(sequence, min, lim, out num);
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x0008C2BC File Offset: 0x0008A4BC
		private void AddCore(uint[] sequence, int min, int lim, uint hash)
		{
			if (this._idLim + 1 >= this._start.Length)
			{
				int num = checked(this._start.Length + this._start.Length / 2);
				Array.Resize<int>(ref this._start, num);
			}
			checked
			{
				if (this._idLim >= this._next.Length)
				{
					int num2 = this._next.Length + this._next.Length / 2;
					Array.Resize<uint>(ref this._hash, num2);
					Array.Resize<int>(ref this._next, num2);
				}
				int num3 = 5 * (lim - min);
				int num4 = this._start[this._idLim];
				if (num4 > unchecked(this._bytes.Length - num3))
				{
					int num5 = Math.Max(this._bytes.Length + this._bytes.Length / 2, num4 + num3);
					Array.Resize<byte>(ref this._bytes, num5);
				}
				int bucketIndex = this.GetBucketIndex(hash);
				this._next[this._idLim] = this._buckets[bucketIndex];
				this._hash[this._idLim] = hash;
				this._buckets[bucketIndex] = this._idLim;
			}
			this._idLim++;
			this._start[this._idLim] = this._start[this._idLim - 1];
			SequencePool.UIntArrayToLeb128(sequence, min, lim, this._bytes, ref this._start[this._idLim]);
			if (this._idLim >= this._buckets.Length)
			{
				this.GrowTable();
			}
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x0008C420 File Offset: 0x0008A620
		private void GrowTable()
		{
			int num = checked(2 * this._buckets.Length);
			this._buckets = Utils.CreateArray<int>(num, -1);
			this._mask = num - 1;
			for (int i = 0; i < this._idLim; i++)
			{
				int bucketIndex = this.GetBucketIndex(this._hash[i]);
				this._next[i] = this._buckets[bucketIndex];
				this._buckets[bucketIndex] = i;
			}
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x0008C487 File Offset: 0x0008A687
		public int GetById(int id, ref uint[] sequence)
		{
			Contracts.Check(0 <= id && id < this._idLim);
			return SequencePool.Leb128ToUIntArray(this._bytes, this._start[id], this._start[id + 1], ref sequence);
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x0008C4BC File Offset: 0x0008A6BC
		private static void UIntArrayToLeb128(uint[] values, int min, int lim, byte[] bytes, ref int ib)
		{
			for (int i = min; i < lim; i++)
			{
				uint num;
				for (num = values[i]; num >= 128U; num >>= 7)
				{
					bytes[ib++] = (byte)(num | 128U);
				}
				bytes[ib++] = (byte)num;
			}
		}

		// Token: 0x060018BC RID: 6332 RVA: 0x0008C50C File Offset: 0x0008A70C
		private static bool TryDecodeOne(byte[] bytes, ref int ib, int ibLim, out uint value)
		{
			value = 0U;
			int num = 0;
			while (ib < ibLim)
			{
				uint num2 = (uint)bytes[ib];
				if (num == 28 && num2 > 15U)
				{
					return false;
				}
				value |= (num2 & 127U) << num;
				num += 7;
				if ((num2 & 128U) == 0U)
				{
					ib++;
					return true;
				}
				ib++;
			}
			return false;
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x0008C560 File Offset: 0x0008A760
		private static int Leb128ToUIntArray(byte[] bytes, int min, int lim, ref uint[] sequence)
		{
			int i = min;
			int num = 0;
			while (i < lim)
			{
				if (Utils.Size<uint>(sequence) <= num)
				{
					Array.Resize<uint>(ref sequence, lim - min);
				}
				Contracts.CheckDecode(SequencePool.TryDecodeOne(bytes, ref i, lim, out sequence[num]));
				num++;
			}
			return num;
		}

		// Token: 0x04000ED4 RID: 3796
		private int _mask;

		// Token: 0x04000ED5 RID: 3797
		private int[] _buckets;

		// Token: 0x04000ED6 RID: 3798
		private int _idLim;

		// Token: 0x04000ED7 RID: 3799
		private int[] _next;

		// Token: 0x04000ED8 RID: 3800
		private int[] _start;

		// Token: 0x04000ED9 RID: 3801
		private uint[] _hash;

		// Token: 0x04000EDA RID: 3802
		private byte[] _bytes;
	}
}
