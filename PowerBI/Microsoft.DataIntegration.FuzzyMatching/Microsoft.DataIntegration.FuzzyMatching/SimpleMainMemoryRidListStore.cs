using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.DataIntegration.FuzzyMatchingCommon;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;
using Microsoft.DataIntegration.FuzzyMatchingCommon.IO;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200007B RID: 123
	[Serializable]
	internal sealed class SimpleMainMemoryRidListStore : IRawSerializable, ISerializable, IMemoryLimit, IMemoryUsage
	{
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x00016CBA File Offset: 0x00014EBA
		// (set) Token: 0x060004EB RID: 1259 RVA: 0x00016CC2 File Offset: 0x00014EC2
		bool IRawSerializable.EnableRawSerialization { get; set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x00016CCB File Offset: 0x00014ECB
		// (set) Token: 0x060004ED RID: 1261 RVA: 0x00016CD3 File Offset: 0x00014ED3
		int IRawSerializable.RawSerializationID { get; set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x00016CDC File Offset: 0x00014EDC
		public int RidListCount
		{
			get
			{
				return this.m_ridLists.Count;
			}
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00016CEC File Offset: 0x00014EEC
		public SimpleMainMemoryRidListStore(FuzzyLookupDefinition indexDefinition, StateManagerStatistics statistics)
		{
			this.m_statistics = statistics;
			this.m_ridLists = new MruCachedDictionary<SimpleMainMemoryRidListStore.RidListKey, RidList>(new Func<SimpleMainMemoryRidListStore.RidListKey, RidList, long>(this.GetMemoryUsage), new Func<SimpleMainMemoryRidListStore.RidListKey, RidList, bool>(this.AllowRemove), null, 101);
			this.m_ridLists.RawSerializationDelegate = new Action<Stream, SimpleMainMemoryRidListStore.RidListKey, RidList>(SimpleMainMemoryRidListStore.SerializeRidList);
			this.m_ridLists.RawDeserializationDelegate = new MruCachedDictionary<SimpleMainMemoryRidListStore.RidListKey, RidList>.DeserializationDelegate(SimpleMainMemoryRidListStore.DeserializeRidList);
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00016D70 File Offset: 0x00014F70
		protected SimpleMainMemoryRidListStore(SerializationInfo info, StreamingContext context)
		{
			((IRawSerializable)this).EnableRawSerialization = (bool)info.GetValue("EnableRawSerialization", typeof(bool));
			((IRawSerializable)this).RawSerializationID = (int)info.GetValue("RawSerializationID", typeof(int));
			this.m_statistics = (StateManagerStatistics)info.GetValue("Statistics", typeof(StateManagerStatistics));
			this.m_memoryLimit = (long)info.GetValue("m_memoryLimit", typeof(long));
			this.SerializeRidLists = (bool)info.GetValue("SerializeRidLists", typeof(bool));
			this.AllRidListsInMemory = (bool)info.GetValue("AllRidListsInMemory", typeof(bool));
			if (this.SerializeRidLists)
			{
				this.m_ridLists = (MruCachedDictionary<SimpleMainMemoryRidListStore.RidListKey, RidList>)info.GetValue("m_ridLists", typeof(MruCachedDictionary<SimpleMainMemoryRidListStore.RidListKey, RidList>));
				return;
			}
			this.m_ridLists = new MruCachedDictionary<SimpleMainMemoryRidListStore.RidListKey, RidList>(new Func<SimpleMainMemoryRidListStore.RidListKey, RidList, long>(this.GetMemoryUsage), new Func<SimpleMainMemoryRidListStore.RidListKey, RidList, bool>(this.AllowRemove), null, 101);
			this.m_ridLists.MemoryLimit = this.m_memoryLimit;
			this.m_ridLists.RawSerializationDelegate = new Action<Stream, SimpleMainMemoryRidListStore.RidListKey, RidList>(SimpleMainMemoryRidListStore.SerializeRidList);
			this.m_ridLists.RawDeserializationDelegate = new MruCachedDictionary<SimpleMainMemoryRidListStore.RidListKey, RidList>.DeserializationDelegate(SimpleMainMemoryRidListStore.DeserializeRidList);
			this.AllRidListsInMemory = false;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00016EF0 File Offset: 0x000150F0
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("EnableRawSerialization", ((IRawSerializable)this).EnableRawSerialization);
			info.AddValue("RawSerializationID", ((IRawSerializable)this).RawSerializationID);
			info.AddValue("Statistics", this.m_statistics);
			info.AddValue("m_memoryLimit", this.m_memoryLimit);
			info.AddValue("SerializeRidLists", this.SerializeRidLists);
			info.AddValue("AllRidListsInMemory", this.AllRidListsInMemory);
			if (this.SerializeRidLists)
			{
				info.AddValue("m_ridLists", this.m_ridLists);
			}
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00016F7C File Offset: 0x0001517C
		private static void SerializeRidList(Stream s, SimpleMainMemoryRidListStore.RidListKey key, RidList ridList)
		{
			key.Write(s);
			ridList.Write(s);
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00016F8E File Offset: 0x0001518E
		private static void DeserializeRidList(Stream s, out SimpleMainMemoryRidListStore.RidListKey key, out RidList ridList)
		{
			key = SimpleMainMemoryRidListStore.RidListKey.Read(s);
			ridList = RidList.Read(s);
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00016FA8 File Offset: 0x000151A8
		void IRawSerializable.Serialize(Stream s)
		{
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00016FAA File Offset: 0x000151AA
		void IRawSerializable.Deserialize(Stream s)
		{
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00016FAC File Offset: 0x000151AC
		private bool AllowRemove(SimpleMainMemoryRidListStore.RidListKey key, RidList value)
		{
			this.AllRidListsInMemory = false;
			return true;
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00016FB8 File Offset: 0x000151B8
		private long GetMemoryUsage(SimpleMainMemoryRidListStore.RidListKey key, RidList value)
		{
			int num = 60;
			if (value.Array != null)
			{
				num += value.Array.Length * 4;
			}
			return (long)num;
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x00016FDF File Offset: 0x000151DF
		public long MemoryUsage
		{
			get
			{
				return this.m_ridLists.MemoryUsage;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x00016FEC File Offset: 0x000151EC
		// (set) Token: 0x060004FA RID: 1274 RVA: 0x00016FF4 File Offset: 0x000151F4
		public long MemoryLimit
		{
			get
			{
				return this.m_memoryLimit;
			}
			set
			{
				this.m_memoryLimit = value;
				this.m_ridLists.MemoryLimit = value;
			}
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0001700C File Offset: 0x0001520C
		public void AddRidList(int lookupId, int hashTableIndex, int signature, RidList ridList)
		{
			SimpleMainMemoryRidListStore.RidListKey ridListKey = new SimpleMainMemoryRidListStore.RidListKey(lookupId, hashTableIndex, signature);
			this.m_ridLists.Add(ridListKey, ridList);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00017034 File Offset: 0x00015234
		public void AddRidSignatures(int recordId, int lookupId, int hashTableIndex, IEnumerable<int> signatures)
		{
			SimpleMainMemoryRidListStore.RidListKey ridListKey = new SimpleMainMemoryRidListStore.RidListKey(lookupId, hashTableIndex, 0);
			foreach (int num in signatures)
			{
				ridListKey.m_signature = num;
				RidList ridList;
				if (!this.m_ridLists.TryGetValue(ridListKey, out ridList))
				{
					this.m_statistics.DistinctSignaturesIndexed += 1L;
					this.m_ridLists.Add(ridListKey, new RidList(new int[] { recordId }, 0, 1));
				}
				else
				{
					Array.Resize<int>(ref ridList.Array, ridList.Array.Length + 1);
					ridList.Array[ridList.Array.Length - 1] = recordId;
					ridList.Count++;
					this.m_ridLists[ridListKey] = ridList;
				}
				this.m_statistics.NonDistinctSignaturesIndexed += 1L;
			}
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00017128 File Offset: 0x00015328
		public void RemoveRidSignatures(int recordId, int lookupId, int hashTableIndex, IEnumerable<int> signatures)
		{
			SimpleMainMemoryRidListStore.RidListKey ridListKey = new SimpleMainMemoryRidListStore.RidListKey(lookupId, hashTableIndex, 0);
			foreach (int num in signatures)
			{
				RidList ridList;
				if (!this.m_ridLists.TryGetValue(ridListKey, out ridList))
				{
					throw new Exception("The signature ridlist for the the signature was not found!");
				}
				bool flag = false;
				for (int i = 0; i < ridList.Count; i++)
				{
					if (ridList.Array[i] == recordId)
					{
						this.m_statistics.NonDistinctSignaturesIndexed -= 1L;
						if (ridList.Count == 1)
						{
							this.m_statistics.DistinctSignaturesIndexed -= 1L;
							this.m_ridLists.Remove(ridListKey);
						}
						else
						{
							if (i < ridList.Count - 1)
							{
								Array.Copy(ridList.Array, i + 1, ridList.Array, i, ridList.Count - 1);
							}
							ridList.Count--;
							this.m_ridLists.Remove(ridListKey);
							this.m_ridLists.Add(ridListKey, ridList);
						}
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					throw new Exception("The specified rid was not found in the signature ridlist!");
				}
			}
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00017270 File Offset: 0x00015470
		public bool TryGetSignatureRidList(ISession session, int lookupId, int hashTableIndex, int signature, ref int[] ridBuffer, out RidList ridList)
		{
			SimpleMainMemoryRidListStore.RidListKey ridListKey = new SimpleMainMemoryRidListStore.RidListKey(lookupId, hashTableIndex, signature);
			return this.m_ridLists.TryGetValue(ridListKey, out ridList);
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00017296 File Offset: 0x00015496
		public void Clear()
		{
			this.m_ridLists.Clear();
			this.AllRidListsInMemory = false;
		}

		// Token: 0x040001A8 RID: 424
		private StateManagerStatistics m_statistics;

		// Token: 0x040001A9 RID: 425
		private long m_memoryLimit = long.MaxValue;

		// Token: 0x040001AA RID: 426
		private MruCachedDictionary<SimpleMainMemoryRidListStore.RidListKey, RidList> m_ridLists;

		// Token: 0x040001AD RID: 429
		public bool SerializeRidLists;

		// Token: 0x040001AE RID: 430
		public bool AllRidListsInMemory = true;

		// Token: 0x0200015F RID: 351
		private struct RidListKey
		{
			// Token: 0x06000CCF RID: 3279 RVA: 0x0003722A File Offset: 0x0003542A
			public RidListKey(int lookupId, int hashTableIndex, int signature)
			{
				this.m_lookupId = lookupId;
				this.m_hashTableIndex = hashTableIndex;
				this.m_signature = signature;
			}

			// Token: 0x06000CD0 RID: 3280 RVA: 0x00037241 File Offset: 0x00035441
			internal void Write(Stream s)
			{
				StreamUtilities.WriteInt32(s, this.m_lookupId);
				StreamUtilities.WriteInt32(s, this.m_hashTableIndex);
				StreamUtilities.WriteInt32(s, this.m_signature);
			}

			// Token: 0x06000CD1 RID: 3281 RVA: 0x00037267 File Offset: 0x00035467
			internal static SimpleMainMemoryRidListStore.RidListKey Read(Stream s)
			{
				return new SimpleMainMemoryRidListStore.RidListKey(StreamUtilities.ReadInt32(s), StreamUtilities.ReadInt32(s), StreamUtilities.ReadInt32(s));
			}

			// Token: 0x040005B7 RID: 1463
			public int m_lookupId;

			// Token: 0x040005B8 RID: 1464
			public int m_hashTableIndex;

			// Token: 0x040005B9 RID: 1465
			public int m_signature;
		}
	}
}
