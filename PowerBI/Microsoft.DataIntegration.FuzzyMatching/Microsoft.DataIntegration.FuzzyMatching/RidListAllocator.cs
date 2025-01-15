using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.DataIntegration.FuzzyMatchingCommon;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;
using Microsoft.DataIntegration.FuzzyMatchingCommon.IO;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200006F RID: 111
	[Serializable]
	internal sealed class RidListAllocator : IRawSerializable, ISerializable, IMemoryUsage
	{
		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x00015D88 File Offset: 0x00013F88
		// (set) Token: 0x060004AF RID: 1199 RVA: 0x00015D90 File Offset: 0x00013F90
		bool IRawSerializable.EnableRawSerialization { get; set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x00015D99 File Offset: 0x00013F99
		// (set) Token: 0x060004B1 RID: 1201 RVA: 0x00015DA1 File Offset: 0x00013FA1
		int IRawSerializable.RawSerializationID { get; set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x00015DAC File Offset: 0x00013FAC
		public long MemoryUsage
		{
			get
			{
				long num = this.m_sig2reference.MemoryUsage + this.m_largeRidListMemoryUsage;
				for (int i = 0; i < this.m_smallRidLists.Length; i++)
				{
					num += this.m_smallRidLists[i].MemoryUsage;
				}
				return num;
			}
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x00015DF0 File Offset: 0x00013FF0
		public RidListAllocator()
		{
			this.m_smallRidLists = new RidListBlockList[3];
			for (int i = 1; i <= 3; i++)
			{
				this.m_smallRidLists[i - 1] = new RidListBlockList(i);
			}
			this.m_largeRidLists = new List<RidList>();
			this.m_largeRidLists.Add(RidList.Empty);
			this.m_sig2reference = new FastIntToIntHash();
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00015E5C File Offset: 0x0001405C
		private RidListAllocator(SerializationInfo info, StreamingContext context)
		{
			((IRawSerializable)this).EnableRawSerialization = (bool)info.GetValue("EnableRawSerialization", typeof(bool));
			((IRawSerializable)this).RawSerializationID = (int)info.GetValue("RawSerializationID", typeof(int));
			this.m_smallRidLists = (RidListBlockList[])info.GetValue("m_smallRidLists", typeof(RidListBlockList[]));
			this.m_sig2reference = (FastIntToIntHash)info.GetValue("m_sig2reference", typeof(FastIntToIntHash));
			this.m_largeRidListMemoryUsage = (long)info.GetValue("m_largeRidListMemoryUsage", typeof(long));
			if (!((IRawSerializable)this).EnableRawSerialization)
			{
				this.m_largeRidLists = (List<RidList>)info.GetValue("m_largeRidLists", typeof(List<RidList>));
				this.m_largeRidListAllocator = (BlockedSegmentArray<int>)info.GetValue("m_largeRidListAllocator", typeof(BlockedSegmentArray<int>));
			}
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00015F64 File Offset: 0x00014164
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("EnableRawSerialization", ((IRawSerializable)this).EnableRawSerialization);
			info.AddValue("RawSerializationID", ((IRawSerializable)this).RawSerializationID);
			info.AddValue("m_smallRidLists", this.m_smallRidLists);
			info.AddValue("m_sig2reference", this.m_sig2reference);
			info.AddValue("m_largeRidListMemoryUsage", this.m_largeRidListMemoryUsage);
			if (!((IRawSerializable)this).EnableRawSerialization)
			{
				info.AddValue("m_largeRidLists", this.m_largeRidLists);
				info.AddValue("m_largeRidListAllocator", this.m_largeRidListAllocator);
			}
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00015FF0 File Offset: 0x000141F0
		void IRawSerializable.Serialize(Stream s)
		{
			StreamUtilities.WriteInt64(s, s.Position);
			StreamUtilities.WriteInt32(s, this.m_largeRidLists.Count);
			foreach (RidList ridList in this.m_largeRidLists)
			{
				StreamUtilities.WriteInt32(s, ridList.Count);
				for (int i = 0; i < ridList.Count; i++)
				{
					StreamUtilities.WriteInt32(s, ridList.Array[i]);
				}
			}
			StreamUtilities.WriteInt64(s, s.Position);
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00016090 File Offset: 0x00014290
		void IRawSerializable.Deserialize(Stream s)
		{
			if (s.Position != StreamUtilities.ReadInt64(s))
			{
				throw new SerializationException("Unexpected stream position encountered when deserializing.");
			}
			int num = StreamUtilities.ReadInt32(s);
			this.m_largeRidLists = new List<RidList>(num);
			this.m_largeRidListAllocator.Reset();
			for (int i = 0; i < num; i++)
			{
				int num2 = StreamUtilities.ReadInt32(s);
				if (num2 == 0)
				{
					this.m_largeRidLists.Add(RidList.Empty);
				}
				else
				{
					RidList ridList = new RidList(this.m_largeRidListAllocator.New(num2));
					for (int j = 0; j < num2; j++)
					{
						ridList.Array[ridList.Offset + j] = StreamUtilities.ReadInt32(s);
					}
					this.m_largeRidLists.Add(ridList);
				}
			}
			if (s.Position != StreamUtilities.ReadInt64(s))
			{
				throw new SerializationException("Unexpected stream position encountered when deserializing.");
			}
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0001615C File Offset: 0x0001435C
		public bool TryGetRidList(int sig, ref int[] ridBuffer, out RidList ridList)
		{
			int num;
			if (!this.m_sig2reference.TryGetValue(sig, out num))
			{
				ridList = RidList.Empty;
				return false;
			}
			int num2 = (num >> 30) & 3;
			int num3 = num & 1073741823;
			if (num2 == 0)
			{
				ridList = this.m_largeRidLists[num3];
			}
			else if (1 == num2)
			{
				ridBuffer[0] = num & 1073741823;
				ridList = new RidList(ridBuffer, 0, 1);
			}
			else
			{
				int[] array;
				int num4;
				this.m_smallRidLists[num2 - 1].GetRidList(num3, out array, out num4);
				ridList = new RidList(array, num4, num2);
			}
			return true;
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x000161F0 File Offset: 0x000143F0
		public bool RemoveRid(int sig, int rid)
		{
			int num = 0;
			int num2;
			if (!this.m_sig2reference.TryGetValue(sig, out num2))
			{
				throw new ArgumentOutOfRangeException("Did not find any RIDs for the specified signature!");
			}
			int num3 = (num2 >> 30) & 3;
			int num4 = num2 & 1073741823;
			if (num3 == 0)
			{
				RidList ridList = this.m_largeRidLists[num4];
				for (int i = ridList.Offset; i < ridList.Offset + ridList.Count; i++)
				{
					if (ridList.Array[i] == rid)
					{
						num++;
					}
					else if (num > 0)
					{
						ridList.Array[i - num] = ridList.Array[i];
					}
				}
				if (num == 0)
				{
					throw new ArgumentOutOfRangeException("Did not find RecordId in the RidList!");
				}
				ridList.Count -= num;
				if (ridList.Count >= 4)
				{
					Array.Resize<int>(ref ridList.Array, ridList.Count);
					this.m_largeRidLists[num4] = ridList;
				}
				else
				{
					this.m_sig2reference.Remove(sig);
					this.m_largeRidListMemoryUsage -= (long)(4 * ridList.Array.Length + 16 + 16 + 4 + 4);
					this.m_largeRidLists[num4] = RidList.Empty;
					if (this.m_freeLargeRidListIndexes == null)
					{
						this.m_freeLargeRidListIndexes = new Stack<int>();
					}
					this.m_freeLargeRidListIndexes.Push(num4);
					for (int j = ridList.Offset; j < ridList.Offset + ridList.Count; j++)
					{
						this.AddRid(ridList[j], rid);
					}
				}
				return ridList.Count == 0;
			}
			else if (1 == num3)
			{
				if ((num2 & 1073741823) != rid)
				{
					throw new ArgumentOutOfRangeException("Did not find RecordId in the RidList!");
				}
				this.m_sig2reference.Remove(sig);
				return true;
			}
			else
			{
				int[] array;
				int num5;
				this.m_smallRidLists[num3 - 1].GetRidList(num4, out array, out num5);
				for (int k = num5; k < num5 + num3; k++)
				{
					if (array[k] == rid)
					{
						num++;
					}
					else if (num > 0)
					{
						array[k - num] = array[k];
					}
				}
				if (num == 0)
				{
					throw new ArgumentOutOfRangeException("Did not find RecordId in the RidList!");
				}
				this.m_sig2reference.Remove(sig);
				for (int l = num5; l < num5 + num3 - num; l++)
				{
					this.AddRid(sig, array[l]);
				}
				this.m_smallRidLists[num3 - 1].FreeRidList(num4);
				return num3 - num == 0;
			}
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0001643C File Offset: 0x0001463C
		public bool AddRid(int sig, int rid)
		{
			int num;
			if (!this.m_sig2reference.TryGetValue(sig, out num))
			{
				this.m_sig2reference[sig] = rid | 1073741824;
				return true;
			}
			int num2 = (num >> 30) & 3;
			int num3 = num & 1073741823;
			if (num2 == 0)
			{
				RidList ridList = this.m_largeRidLists[num3];
				if (ridList.Count == ridList.Array.Length)
				{
					this.m_largeRidListMemoryUsage -= (long)(4 * ridList.Array.Length);
					Array.Resize<int>(ref ridList.Array, ridList.Array.Length * 2);
					this.m_largeRidListMemoryUsage += (long)(4 * ridList.Array.Length);
				}
				int[] array = ridList.Array;
				int count = ridList.Count;
				ridList.Count = count + 1;
				array[count] = rid;
				this.m_largeRidLists[num3] = ridList;
			}
			else
			{
				int num4;
				if (1 == num2)
				{
					int[] array2;
					int num5;
					this.m_smallRidLists[num2].AllocRidList(out num4, out array2, out num5);
					array2[num5++] = num & 1073741823;
					array2[num5++] = rid;
					num4 |= num2 + 1 << 30;
				}
				else
				{
					int[] array3;
					int num6;
					this.m_smallRidLists[num2 - 1].GetRidList(num3, out array3, out num6);
					int[] array2;
					int num7;
					if (2 == num2)
					{
						this.m_smallRidLists[num2].AllocRidList(out num4, out array2, out num7);
						num4 |= num2 + 1 << 30;
					}
					else
					{
						RidList ridList2 = default(RidList);
						ridList2.Array = new int[4];
						ridList2.Count = 4;
						this.m_largeRidLists.Add(ridList2);
						this.m_largeRidListMemoryUsage += 56L;
						num4 = this.m_largeRidLists.Count - 1;
						array2 = ridList2.Array;
						num7 = 0;
					}
					for (int i = 0; i < num2; i++)
					{
						array2[num7++] = array3[num6++];
					}
					array2[num7] = rid;
					this.m_smallRidLists[num2 - 1].FreeRidList(num3);
				}
				this.m_sig2reference[sig] = num4;
			}
			return false;
		}

		// Token: 0x04000197 RID: 407
		private const int LenBits = 2;

		// Token: 0x04000198 RID: 408
		private const int RefBits = 30;

		// Token: 0x04000199 RID: 409
		private const int RidListReferenceMask = 1073741823;

		// Token: 0x0400019A RID: 410
		private const int RidListLengthMask = 3;

		// Token: 0x0400019B RID: 411
		internal RidListBlockList[] m_smallRidLists;

		// Token: 0x0400019C RID: 412
		internal List<RidList> m_largeRidLists;

		// Token: 0x0400019D RID: 413
		private long m_largeRidListMemoryUsage;

		// Token: 0x0400019E RID: 414
		private BlockedSegmentArray<int> m_largeRidListAllocator = new BlockedSegmentArray<int>();

		// Token: 0x0400019F RID: 415
		internal FastIntToIntHash m_sig2reference;

		// Token: 0x040001A0 RID: 416
		private Stack<int> m_freeLargeRidListIndexes;
	}
}
