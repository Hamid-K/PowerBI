using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.DataIntegration.FuzzyMatchingCommon;
using Microsoft.DataIntegration.FuzzyMatchingCommon.IO;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200006C RID: 108
	[Serializable]
	internal class MainMemoryRidListStore : IRawSerializable, ISerializable, IMemoryLimit, IMemoryUsage
	{
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x00015293 File Offset: 0x00013493
		// (set) Token: 0x0600048A RID: 1162 RVA: 0x0001529B File Offset: 0x0001349B
		bool IRawSerializable.EnableRawSerialization { get; set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x000152A4 File Offset: 0x000134A4
		// (set) Token: 0x0600048C RID: 1164 RVA: 0x000152AC File Offset: 0x000134AC
		int IRawSerializable.RawSerializationID { get; set; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x000152B8 File Offset: 0x000134B8
		public long MemoryUsage
		{
			get
			{
				long num = 0L;
				for (int i = 0; i < this.m_lookups.Count; i++)
				{
					for (int j = 0; j < this.m_lookups[i].RidListLookups.Length; j++)
					{
						num += this.m_lookups[i].RidListLookups[j].MemoryUsage;
					}
				}
				return num;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x00015318 File Offset: 0x00013518
		// (set) Token: 0x0600048F RID: 1167 RVA: 0x00015320 File Offset: 0x00013520
		public long MemoryLimit
		{
			get
			{
				return this.m_memoryLimit;
			}
			set
			{
				this.m_memoryLimit = value;
			}
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0001532C File Offset: 0x0001352C
		public MainMemoryRidListStore(FuzzyLookupDefinition indexDefinition, StateManagerStatistics statistics)
		{
			this.Statistics = statistics;
			this.Reset();
			foreach (Lookup lookup in indexDefinition.Lookups)
			{
				IOneDimSignatureGenerator oneDimSignatureGenerator = (IOneDimSignatureGenerator)lookup.SignatureGenerator.CreateInstance();
				if (oneDimSignatureGenerator is IMultiDimSignatureGenerator)
				{
					this.CreateLookup((oneDimSignatureGenerator as IMultiDimSignatureGenerator).NumHashtables);
				}
				else
				{
					this.CreateLookup(1);
				}
			}
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x000153E0 File Offset: 0x000135E0
		private MainMemoryRidListStore(SerializationInfo info, StreamingContext context)
		{
			((IRawSerializable)this).EnableRawSerialization = (bool)info.GetValue("EnableRawSerialization", typeof(bool));
			((IRawSerializable)this).RawSerializationID = (int)info.GetValue("RawSerializationID", typeof(int));
			this.Statistics = (StateManagerStatistics)info.GetValue("Statistics", typeof(StateManagerStatistics));
			this.m_lookups = (List<MainMemoryRidListStore.LookupInfo>)info.GetValue("m_lookups", typeof(List<MainMemoryRidListStore.LookupInfo>));
			this.ThrowOnMemoryLimitExceeded = (bool)info.GetValue("ThrowOnMemoryLimitExceeded", typeof(bool));
			this.m_memoryLimit = (long)info.GetValue("m_memoryLimit", typeof(long));
			this.m_tempRidBuffer = new int[1];
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x000154E4 File Offset: 0x000136E4
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("EnableRawSerialization", ((IRawSerializable)this).EnableRawSerialization);
			info.AddValue("RawSerializationID", ((IRawSerializable)this).RawSerializationID);
			info.AddValue("Statistics", this.Statistics);
			info.AddValue("m_lookups", this.m_lookups);
			info.AddValue("ThrowOnMemoryLimitExceeded", this.ThrowOnMemoryLimitExceeded);
			info.AddValue("m_memoryLimit", this.m_memoryLimit);
			bool enableRawSerialization = ((IRawSerializable)this).EnableRawSerialization;
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0001555E File Offset: 0x0001375E
		void IRawSerializable.Serialize(Stream s)
		{
			StreamUtilities.WriteInt64(s, s.Position);
			StreamUtilities.WriteInt64(s, s.Position);
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00015578 File Offset: 0x00013778
		void IRawSerializable.Deserialize(Stream s)
		{
			if (s.Position != StreamUtilities.ReadInt64(s))
			{
				throw new SerializationException("Unexpected stream position encountered when deserializing.");
			}
			if (s.Position != StreamUtilities.ReadInt64(s))
			{
				throw new SerializationException("Unexpected stream position encountered when deserializing.");
			}
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x000155AC File Offset: 0x000137AC
		public void Reset()
		{
			this.m_lookups = new List<MainMemoryRidListStore.LookupInfo>();
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x000155BC File Offset: 0x000137BC
		public int CreateLookup(int numLshHashtables)
		{
			int count = this.m_lookups.Count;
			MainMemoryRidListStore.LookupInfo lookupInfo = new MainMemoryRidListStore.LookupInfo(count);
			this.m_lookups.Add(lookupInfo);
			lookupInfo.RidListLookups = new RidListAllocator[numLshHashtables + 1];
			for (int i = 0; i < numLshHashtables + 1; i++)
			{
				lookupInfo.RidListLookups[i] = new RidListAllocator();
			}
			return count;
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00015614 File Offset: 0x00013814
		public void AddRidSignatures(int recordId, int lookupId, int hashTableIndex, IEnumerable<int> signatures)
		{
			foreach (int num in signatures)
			{
				if (this.m_lookups[lookupId].RidListLookups[hashTableIndex + 1].AddRid(num, recordId))
				{
					this.Statistics.DistinctSignaturesIndexed += 1L;
				}
				this.Statistics.NonDistinctSignaturesIndexed += 1L;
			}
			if (this.ThrowOnMemoryLimitExceeded && recordId % 1000 == 0 && this.MemoryUsage > this.MemoryLimit)
			{
				throw new OutOfMemoryException("FuzzyLookup state manager has reached the memory limit and cannot continue.  Increase the FuzzyLookup.MemoryLimit property or provide a SQL connection string in the FuzzyLookup constructor to allow spillover onto disk.");
			}
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x000156C8 File Offset: 0x000138C8
		public void RemoveRidSignatures(int recordId, int lookupId, int hashTableIndex, IEnumerable<int> signatures)
		{
			foreach (int num in signatures)
			{
				if (this.m_lookups[lookupId].RidListLookups[hashTableIndex + 1].RemoveRid(num, recordId))
				{
					this.Statistics.DistinctSignaturesIndexed -= 1L;
				}
				this.Statistics.NonDistinctSignaturesIndexed -= 1L;
			}
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00015750 File Offset: 0x00013950
		public bool TryGetSignatureRidList(ISession session, int lookupId, int hashTableIndex, int signature, ref int[] ridBuffer, out RidList ridList)
		{
			return this.m_lookups[lookupId].RidListLookups[hashTableIndex + 1].TryGetRidList(signature, ref ridBuffer, out ridList);
		}

		// Token: 0x04000185 RID: 389
		private StateManagerStatistics Statistics;

		// Token: 0x04000186 RID: 390
		private List<MainMemoryRidListStore.LookupInfo> m_lookups;

		// Token: 0x04000187 RID: 391
		private int[] m_tempRidBuffer = new int[1];

		// Token: 0x04000188 RID: 392
		private long m_memoryLimit = long.MaxValue;

		// Token: 0x04000189 RID: 393
		public bool ThrowOnMemoryLimitExceeded = true;

		// Token: 0x0200015B RID: 347
		[Serializable]
		internal sealed class LookupInfo
		{
			// Token: 0x06000CCB RID: 3275 RVA: 0x000371FA File Offset: 0x000353FA
			public LookupInfo(int id)
			{
				this.Id = id;
			}

			// Token: 0x040005AE RID: 1454
			public int Id;

			// Token: 0x040005AF RID: 1455
			public RidListAllocator[] RidListLookups;
		}
	}
}
