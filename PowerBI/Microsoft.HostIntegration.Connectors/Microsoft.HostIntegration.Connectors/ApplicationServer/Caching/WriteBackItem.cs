using System;
using System.Text;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001EE RID: 494
	internal class WriteBackItem : ADMCacheItem
	{
		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06001031 RID: 4145 RVA: 0x00036850 File Offset: 0x00034A50
		// (set) Token: 0x06001032 RID: 4146 RVA: 0x00036858 File Offset: 0x00034A58
		public int WriteFailCount
		{
			get
			{
				return this._writeFailCount;
			}
			internal set
			{
				this._writeFailCount = value;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06001033 RID: 4147 RVA: 0x00036861 File Offset: 0x00034A61
		// (set) Token: 0x06001034 RID: 4148 RVA: 0x00036869 File Offset: 0x00034A69
		public long FirstWrite
		{
			get
			{
				return this._firstWrite;
			}
			internal set
			{
				this._firstWrite = value;
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06001035 RID: 4149 RVA: 0x00036872 File Offset: 0x00034A72
		// (set) Token: 0x06001036 RID: 4150 RVA: 0x0003687A File Offset: 0x00034A7A
		public AOMCacheItem OmItem
		{
			get
			{
				return this._omItem;
			}
			internal set
			{
				this._omItem = value;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06001037 RID: 4151 RVA: 0x00036883 File Offset: 0x00034A83
		// (set) Token: 0x06001038 RID: 4152 RVA: 0x0003688B File Offset: 0x00034A8B
		public StoreOperation Operation
		{
			get
			{
				return this._operation;
			}
			internal set
			{
				this._operation = value;
			}
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x00036894 File Offset: 0x00034A94
		internal WriteBackItem()
		{
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x0003689C File Offset: 0x00034A9C
		internal WriteBackItem(AOMCacheItem item, StoreOperation operation, int writeFailCount, long firstWrite)
		{
			this.Init(item, operation, writeFailCount, firstWrite);
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x000368AF File Offset: 0x00034AAF
		internal WriteBackItem(WriteBackItem item)
			: base(item)
		{
			this.CopyFrom(item);
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x000368BF File Offset: 0x00034ABF
		internal void Init(AOMCacheItem item, StoreOperation operation, int writeFailCount, long firstWrite)
		{
			this.Operation = operation;
			this.WriteFailCount = writeFailCount;
			this.FirstWrite = firstWrite;
			this.OmItem = item;
			base.TimeToLive = long.MaxValue;
			base.ExtensionTimeout = 0L;
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x000368F5 File Offset: 0x00034AF5
		internal void CopyFrom(WriteBackItem item)
		{
			this.Init(item.OmItem, item.Operation, item.WriteFailCount, item.FirstWrite);
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x00036915 File Offset: 0x00034B15
		internal new void Clean()
		{
			this._writeFailCount = 0;
			this._firstWrite = 0L;
			this._omItem = null;
			this._operation = StoreOperation.None;
			base.Clean();
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x0003693A File Offset: 0x00034B3A
		internal bool IsInUse()
		{
			return this._operation != StoreOperation.None;
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x00036948 File Offset: 0x00034B48
		internal WBCheckPointItem GetCheckPointItem(bool success)
		{
			return new WBCheckPointItem(DataCacheItemKey.Create(this._omItem), base.Version, success);
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x00036964 File Offset: 0x00034B64
		public override bool Equals(object obj)
		{
			if (object.ReferenceEquals(this, obj))
			{
				return true;
			}
			WriteBackLookupKey writeBackLookupKey = obj as WriteBackLookupKey;
			if (writeBackLookupKey != null)
			{
				return object.ReferenceEquals(this._omItem, writeBackLookupKey.OmItem);
			}
			WriteBackItem writeBackItem = obj as WriteBackItem;
			string text;
			string text2;
			if (writeBackItem != null)
			{
				text = writeBackItem.GetOMItemKey();
				text2 = writeBackItem.GetRegion();
			}
			else
			{
				DataCacheItemKey dataCacheItemKey = (DataCacheItemKey)obj;
				text = dataCacheItemKey.Key;
				text2 = dataCacheItemKey.Region;
			}
			return DataCacheItemKey.Equals(this.GetOMItemKey(), this.GetRegion(), text, text2);
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x000369DD File Offset: 0x00034BDD
		public override int GetHashCode()
		{
			return WriteBackItem.ComputeHashCode(this._omItem);
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x000369EA File Offset: 0x00034BEA
		internal static int ComputeHashCode(AOMCacheItem item)
		{
			return DataCacheItemKey.GetHashCode(item.HashCode, item.RegionName);
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x00036A00 File Offset: 0x00034C00
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("WriteBackItem[");
			stringBuilder.Append("StoreOperation:");
			stringBuilder.Append(this._operation);
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append(this._omItem.ToString());
			return stringBuilder.ToString();
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x00036A61 File Offset: 0x00034C61
		internal string GetOMItemKey()
		{
			return this._omItem.Key.StringValue;
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x00036A73 File Offset: 0x00034C73
		internal string GetRegion()
		{
			return this._omItem.RegionName;
		}

		// Token: 0x04000AB3 RID: 2739
		private int _writeFailCount;

		// Token: 0x04000AB4 RID: 2740
		private long _firstWrite;

		// Token: 0x04000AB5 RID: 2741
		private AOMCacheItem _omItem;

		// Token: 0x04000AB6 RID: 2742
		private StoreOperation _operation;

		// Token: 0x020001EF RID: 495
		internal class FakeRegion : IOMRegion
		{
			// Token: 0x06001047 RID: 4167 RVA: 0x00036A80 File Offset: 0x00034C80
			internal FakeRegion(string regionName, string cacheName)
			{
				this._regionName = regionName;
				this._cacheName = cacheName;
			}

			// Token: 0x17000380 RID: 896
			// (get) Token: 0x06001048 RID: 4168 RVA: 0x00036A96 File Offset: 0x00034C96
			public string RegionName
			{
				get
				{
					return this._regionName;
				}
			}

			// Token: 0x17000381 RID: 897
			// (get) Token: 0x06001049 RID: 4169 RVA: 0x00036A9E File Offset: 0x00034C9E
			public string CacheName
			{
				get
				{
					return this._cacheName;
				}
			}

			// Token: 0x17000382 RID: 898
			// (get) Token: 0x0600104A RID: 4170 RVA: 0x000189CC File Offset: 0x00016BCC
			public object State
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000383 RID: 899
			// (get) Token: 0x0600104B RID: 4171 RVA: 0x000189CC File Offset: 0x00016BCC
			public OMRegionStats Stats
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000384 RID: 900
			// (get) Token: 0x0600104C RID: 4172 RVA: 0x00003CAB File Offset: 0x00001EAB
			public IMemoryManager MemoryManager
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17000385 RID: 901
			// (get) Token: 0x0600104D RID: 4173 RVA: 0x00006F04 File Offset: 0x00005104
			public bool IsDeleted
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000386 RID: 902
			// (get) Token: 0x0600104E RID: 4174 RVA: 0x00036AA6 File Offset: 0x00034CA6
			public ExpirationType ExpirationType
			{
				get
				{
					return ExpirationType.NotProvided;
				}
			}

			// Token: 0x04000AB7 RID: 2743
			private string _regionName;

			// Token: 0x04000AB8 RID: 2744
			private string _cacheName;
		}
	}
}
