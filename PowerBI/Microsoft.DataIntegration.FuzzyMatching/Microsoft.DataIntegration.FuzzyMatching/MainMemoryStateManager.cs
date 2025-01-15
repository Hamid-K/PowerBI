using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.DataIntegration.FuzzyMatchingCommon;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Diagnostics;
using Microsoft.DataIntegration.FuzzyMatchingCommon.IO;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000067 RID: 103
	[Serializable]
	public class MainMemoryStateManager : IFuzzyLookupStateManager, IRecordContextCache, IInvertedIndexUpdate, IRecordWithIdUpdate, IRecordWithIdLookup, IInvertedIndexLookup, IMemoryUsage, IMemoryLimit, IFuzzyLookupStateManagerInitialize, IRawSerializable, ISerializable, ISessionable
	{
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x00013334 File Offset: 0x00011534
		public IStatistics Statistics
		{
			get
			{
				return this.m_statistics;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x0001333C File Offset: 0x0001153C
		// (set) Token: 0x06000428 RID: 1064 RVA: 0x00013344 File Offset: 0x00011544
		bool IRawSerializable.EnableRawSerialization { get; set; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x0001334D File Offset: 0x0001154D
		// (set) Token: 0x0600042A RID: 1066 RVA: 0x00013355 File Offset: 0x00011555
		int IRawSerializable.RawSerializationID { get; set; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x0001335E File Offset: 0x0001155E
		// (set) Token: 0x0600042C RID: 1068 RVA: 0x00013366 File Offset: 0x00011566
		public bool EnableReferenceContextCaching { get; set; }

		// Token: 0x0600042D RID: 1069 RVA: 0x0001336F File Offset: 0x0001156F
		public MainMemoryStateManager()
		{
			this.m_statistics = new StateManagerStatistics();
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x000133A8 File Offset: 0x000115A8
		public virtual void Initialize(FuzzyLookupDefinition indexDefinition, IConnectionManager connectionManager)
		{
			this.m_indexDefintion = indexDefinition;
			this.m_ridListStore = new MainMemoryRidListStore(indexDefinition, this.m_statistics);
			this.m_refRecordStore = new MainMemoryRecordStore2(indexDefinition.RecordBinding.Schema, indexDefinition.KeyColumns.ConvertAll<string>((Column c) => c.Name).ToArray());
			this.m_refContextCache = new MruCachedDictionary<long, RecordContext>(Int64EqualityComparerWithRobustHashing.Instance, new Func<long, RecordContext, long>(MainMemoryStateManager.GetRefContextMemoryUsage), null, null, 1);
			this.m_refContextCache.RawSerializationDelegate = new Action<Stream, long, RecordContext>(this.m_recordContextSerialiationHelper.SerializeRecordContext);
			this.m_refContextCache.RawDeserializationDelegate = new MruCachedDictionary<long, RecordContext>.DeserializationDelegate(this.m_recordContextSerialiationHelper.DeserializeRecordContext);
			this.SetMemoryLimit(this.m_memoryLimit);
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0001347C File Offset: 0x0001167C
		protected MainMemoryStateManager(SerializationInfo info, StreamingContext context)
		{
			((IRawSerializable)this).EnableRawSerialization = (bool)info.GetValue("EnableRawSerialization", typeof(bool));
			((IRawSerializable)this).RawSerializationID = (int)info.GetValue("RawSerializationID", typeof(int));
			this.m_statistics = (StateManagerStatistics)info.GetValue("Statistics", typeof(StateManagerStatistics));
			this.m_ridListStore = (MainMemoryRidListStore)info.GetValue("m_ridListStore", typeof(MainMemoryRidListStore));
			this.m_refRecordStore = (IRecordStore)info.GetValue("m_refRecordStore", typeof(IRecordStore));
			this.m_memoryLimit = (long)info.GetValue("m_memoryLimit", typeof(long));
			this.m_indexDefintion = (FuzzyLookupDefinition)info.GetValue("m_indexDefintion", typeof(FuzzyLookupDefinition));
			this.m_recordContextSerialiationHelper = (MainMemoryStateManager.RecordContextSerializerHelper)info.GetValue("m_recordContextSerialiationHelper", typeof(MainMemoryStateManager.RecordContextSerializerHelper));
			this.m_refContextCache = (MruCachedDictionary<long, RecordContext>)info.GetValue("m_refContextCache", typeof(MruCachedDictionary<long, RecordContext>));
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x000135D4 File Offset: 0x000117D4
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			this.GetObjectData(info, context);
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x000135E0 File Offset: 0x000117E0
		protected void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("EnableRawSerialization", ((IRawSerializable)this).EnableRawSerialization);
			info.AddValue("RawSerializationID", ((IRawSerializable)this).RawSerializationID);
			info.AddValue("Statistics", this.m_statistics);
			info.AddValue("m_ridListStore", this.m_ridListStore);
			info.AddValue("m_refRecordStore", this.m_refRecordStore);
			info.AddValue("m_memoryLimit", this.m_memoryLimit);
			info.AddValue("m_indexDefintion", this.m_indexDefintion);
			if (this.m_memoryLimit == 9223372036854775807L)
			{
				this.m_recordContextSerialiationHelper.m_deserializationTranMatchAllocator = new BlockedSegmentArray<WeightedTransformationMatch>();
				this.m_recordContextSerialiationHelper.m_deserializationIntAllocator = new BlockedSegmentArray<int>();
				this.m_recordContextSerialiationHelper.m_deserializationByteAllocator = new BlockedSegmentArray<byte>();
			}
			else
			{
				this.m_recordContextSerialiationHelper.m_deserializationTranMatchAllocator = HeapSegmentAllocator<WeightedTransformationMatch>.Instance;
				this.m_recordContextSerialiationHelper.m_deserializationIntAllocator = HeapSegmentAllocator<int>.Instance;
				this.m_recordContextSerialiationHelper.m_deserializationByteAllocator = HeapSegmentAllocator<byte>.Instance;
			}
			info.AddValue("m_recordContextSerialiationHelper", this.m_recordContextSerialiationHelper);
			info.AddValue("m_refContextCache", this.m_refContextCache);
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x000136F9 File Offset: 0x000118F9
		public virtual void Serialize(Stream s)
		{
			StreamUtilities.WriteInt64(s, s.Position);
			StreamUtilities.WriteInt64(s, s.Position);
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00013713 File Offset: 0x00011913
		public virtual void Deserialize(Stream s)
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

		// Token: 0x06000434 RID: 1076 RVA: 0x00013747 File Offset: 0x00011947
		private static long GetRefContextMemoryUsage(long k, RecordContext seq)
		{
			return 16L + seq.MemoryUsage;
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x00013753 File Offset: 0x00011953
		IStatistics IFuzzyLookupStateManager.Statistics
		{
			get
			{
				return this.m_statistics;
			}
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0001375B File Offset: 0x0001195B
		public virtual IUpdateContext BeginUpdate(DataTable schemaTable)
		{
			return new MainMemoryStateManager.UpdateContext
			{
				RefRecordStoreUpdateContext = this.m_refRecordStore.BeginUpdate(schemaTable)
			};
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00013774 File Offset: 0x00011974
		public virtual void EndUpdate(IUpdateContext updateContext)
		{
			this.m_refRecordStore.EndUpdate(updateContext);
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x00013782 File Offset: 0x00011982
		// (set) Token: 0x06000439 RID: 1081 RVA: 0x0001378A File Offset: 0x0001198A
		public long MemoryLimit
		{
			get
			{
				return this.m_memoryLimit;
			}
			set
			{
				this.SetMemoryLimit(value);
			}
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00013794 File Offset: 0x00011994
		protected virtual void SetMemoryLimit(long memoryLimit)
		{
			this.m_memoryLimit = memoryLimit;
			if (this.m_indexDefintion != null)
			{
				int num = 2;
				foreach (Lookup lookup in this.m_indexDefintion.Lookups)
				{
					if (this.EnableReferenceContextCaching)
					{
						num++;
						break;
					}
				}
				this.m_ridListStore.MemoryLimit = this.m_memoryLimit / (long)num;
				this.m_refRecordStore.MemoryLimit = this.m_memoryLimit / (long)num;
				this.m_refContextCache.MemoryLimit = this.m_memoryLimit / (long)num;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x00013844 File Offset: 0x00011A44
		public long MemoryUsage
		{
			get
			{
				return this.m_ridListStore.MemoryUsage + this.m_refRecordStore.MemoryUsage + this.m_refContextCache.MemoryUsage;
			}
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00013869 File Offset: 0x00011A69
		public virtual void AddRidSignatures(IUpdateContext _updateContext, int rid, int lookupId, int hashTableIndex, IEnumerable<int> signatures)
		{
			this.m_ridListStore.AddRidSignatures(rid, lookupId, hashTableIndex, signatures);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0001387C File Offset: 0x00011A7C
		public virtual void RemoveRidSignatures(IUpdateContext _updateContext, int rid, int lookupId, int hashTableIndex, IEnumerable<int> signatures)
		{
			this.m_ridListStore.RemoveRidSignatures(rid, lookupId, hashTableIndex, signatures);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0001388F File Offset: 0x00011A8F
		public virtual bool TryGetSignatureRidList(ISession session, int lookupId, int hashTableIndex, int signature, ref int[] ridBuffer, out RidList ridList)
		{
			return this.m_ridListStore.TryGetSignatureRidList(session, lookupId, hashTableIndex, signature, ref ridBuffer, out ridList);
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x000138A5 File Offset: 0x00011AA5
		public virtual void AddRecord(IUpdateContext _updateContext, IDataRecord record, out int recordId)
		{
			recordId = this.m_refRecordStore.AddRecord(record);
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x000138B5 File Offset: 0x00011AB5
		public virtual bool TryRemoveRecord(IUpdateContext _updateContext, IDataRecord record, out int rid)
		{
			return this.m_refRecordStore.TryRemoveRecord(record, out rid);
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x000138C4 File Offset: 0x00011AC4
		public ISession CreateSession()
		{
			return new MainMemoryStateManager.Session
			{
				m_recordStoreSession = this.m_refRecordStore.CreateSession()
			};
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x000138DC File Offset: 0x00011ADC
		public virtual bool TryGetRecord(ISession session, int rid, ref object[] values)
		{
			MainMemoryStateManager.Session session2 = (MainMemoryStateManager.Session)session;
			return this.m_refRecordStore.TryGetRecord(session2.m_recordStoreSession, rid, ref values);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00013903 File Offset: 0x00011B03
		public virtual bool TryGetRecordContext(ISession session, int rid, int lookupId, out RecordContext recordContext)
		{
			if (this.EnableReferenceContextCaching)
			{
				return this.m_refContextCache.TryGetValue(Utilities.Int32ToInt64(rid, lookupId), out recordContext);
			}
			recordContext = null;
			return false;
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00013927 File Offset: 0x00011B27
		public virtual void CacheRecordContext(int rid, int lookupId, RecordContext recordContext)
		{
			if (this.EnableReferenceContextCaching)
			{
				this.m_refContextCache.Add(Utilities.Int32ToInt64(rid, lookupId), recordContext);
			}
		}

		// Token: 0x0400015A RID: 346
		internal StateManagerStatistics m_statistics = new StateManagerStatistics();

		// Token: 0x0400015B RID: 347
		internal MainMemoryRidListStore m_ridListStore;

		// Token: 0x0400015C RID: 348
		internal IRecordStore m_refRecordStore;

		// Token: 0x0400015D RID: 349
		internal long m_memoryLimit = long.MaxValue;

		// Token: 0x0400015E RID: 350
		internal FuzzyLookupDefinition m_indexDefintion;

		// Token: 0x0400015F RID: 351
		internal MruCachedDictionary<long, RecordContext> m_refContextCache;

		// Token: 0x04000160 RID: 352
		internal MainMemoryStateManager.RecordContextSerializerHelper m_recordContextSerialiationHelper = new MainMemoryStateManager.RecordContextSerializerHelper();

		// Token: 0x02000154 RID: 340
		[Serializable]
		internal class RecordContextSerializerHelper
		{
			// Token: 0x06000CBE RID: 3262 RVA: 0x00037146 File Offset: 0x00035346
			public void SerializeRecordContext(Stream s, long key, RecordContext context)
			{
				StreamUtilities.WriteInt64(s, key);
				context.Write(new BinaryWriter(s));
			}

			// Token: 0x06000CBF RID: 3263 RVA: 0x0003715B File Offset: 0x0003535B
			public void DeserializeRecordContext(Stream s, out long key, out RecordContext context)
			{
				key = StreamUtilities.ReadInt64(s);
				context = new RecordContext(new BinaryReader(s), this.m_deserializationIntAllocator, this.m_deserializationByteAllocator);
			}

			// Token: 0x0400059F RID: 1439
			public ISegmentAllocator<WeightedTransformationMatch> m_deserializationTranMatchAllocator;

			// Token: 0x040005A0 RID: 1440
			public ISegmentAllocator<int> m_deserializationIntAllocator;

			// Token: 0x040005A1 RID: 1441
			public ISegmentAllocator<byte> m_deserializationByteAllocator;
		}

		// Token: 0x02000155 RID: 341
		private class UpdateContext : IUpdateContext
		{
			// Token: 0x040005A2 RID: 1442
			public IUpdateContext RefRecordStoreUpdateContext;
		}

		// Token: 0x02000156 RID: 342
		private class Session : ISession
		{
			// Token: 0x06000CC2 RID: 3266 RVA: 0x0003718E File Offset: 0x0003538E
			public void Reset()
			{
				if (this.m_recordStoreSession != null)
				{
					this.m_recordStoreSession.Reset();
				}
			}

			// Token: 0x040005A3 RID: 1443
			public ISession m_recordStoreSession;
		}
	}
}
