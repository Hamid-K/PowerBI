using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000BF RID: 191
	[Serializable]
	internal class MemoryPressureMonitorProperties : ConfigurationElement, ISerializable
	{
		// Token: 0x060004B8 RID: 1208 RVA: 0x00015607 File Offset: 0x00013807
		internal MemoryPressureMonitorProperties()
		{
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x00015FAF File Offset: 0x000141AF
		// (set) Token: 0x060004BA RID: 1210 RVA: 0x00015FC1 File Offset: 0x000141C1
		[ConfigurationProperty("isEnabled", IsRequired = false, DefaultValue = true)]
		internal bool IsEnabled
		{
			get
			{
				return (bool)base["isEnabled"];
			}
			set
			{
				base["isEnabled"] = value;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x00015FD4 File Offset: 0x000141D4
		// (set) Token: 0x060004BC RID: 1212 RVA: 0x00015FE6 File Offset: 0x000141E6
		[ConfigurationProperty("lowMemoryPercent", IsRequired = false, DefaultValue = 15)]
		[IntegerValidator(MinValue = 0, MaxValue = 50)]
		internal int LowMemoryPercent
		{
			get
			{
				return (int)base["lowMemoryPercent"];
			}
			set
			{
				base["lowMemoryPercent"] = value;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x00015FF9 File Offset: 0x000141F9
		// (set) Token: 0x060004BE RID: 1214 RVA: 0x0001600B File Offset: 0x0001420B
		[IntegerValidator(MinValue = 0, MaxValue = 50)]
		[ConfigurationProperty("lowMemoryReleasePercent", IsRequired = false, DefaultValue = 7)]
		internal int LowMemoryReleasePercent
		{
			get
			{
				return (int)base["lowMemoryReleasePercent"];
			}
			set
			{
				base["lowMemoryReleasePercent"] = value;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x0001601E File Offset: 0x0001421E
		// (set) Token: 0x060004C0 RID: 1216 RVA: 0x00016030 File Offset: 0x00014230
		[ConfigurationProperty("interval", IsRequired = false, DefaultValue = 1000)]
		[IntegerValidator(MinValue = 1000)]
		internal int Interval
		{
			get
			{
				return (int)base["interval"];
			}
			set
			{
				base["interval"] = value;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x00016043 File Offset: 0x00014243
		// (set) Token: 0x060004C2 RID: 1218 RVA: 0x00016055 File Offset: 0x00014255
		[ConfigurationProperty("percentItemInFinalizerQueue", IsRequired = false, DefaultValue = 5)]
		[IntegerValidator(MinValue = 1, MaxValue = 50)]
		internal int PercentItemInFinalizerQueue
		{
			get
			{
				return (int)base["percentItemInFinalizerQueue"];
			}
			set
			{
				base["percentItemInFinalizerQueue"] = value;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x00016068 File Offset: 0x00014268
		// (set) Token: 0x060004C4 RID: 1220 RVA: 0x0001607A File Offset: 0x0001427A
		[IntegerValidator(MinValue = 0, MaxValue = 50)]
		[ConfigurationProperty("internalThrottleLowPercent", IsRequired = false, DefaultValue = 0)]
		internal int InternalThrottleLowPercent
		{
			get
			{
				return (int)base["internalThrottleLowPercent"];
			}
			set
			{
				base["internalThrottleLowPercent"] = value;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x0001608D File Offset: 0x0001428D
		// (set) Token: 0x060004C6 RID: 1222 RVA: 0x0001609F File Offset: 0x0001429F
		[IntegerValidator(MinValue = 0, MaxValue = 50)]
		[ConfigurationProperty("internalThrottleHighPercent", IsRequired = false, DefaultValue = 0)]
		internal int InternalThrottleHighPercent
		{
			get
			{
				return (int)base["internalThrottleHighPercent"];
			}
			set
			{
				base["internalThrottleHighPercent"] = value;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x000160B2 File Offset: 0x000142B2
		// (set) Token: 0x060004C8 RID: 1224 RVA: 0x000160C4 File Offset: 0x000142C4
		[ConfigurationProperty("throttleLowPercent", IsRequired = false, DefaultValue = 0)]
		[IntegerValidator(MinValue = 0, MaxValue = 50)]
		internal int ThrottleLowPercent
		{
			get
			{
				return (int)base["throttleLowPercent"];
			}
			set
			{
				base["throttleLowPercent"] = value;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x000160D7 File Offset: 0x000142D7
		// (set) Token: 0x060004CA RID: 1226 RVA: 0x000160E9 File Offset: 0x000142E9
		[IntegerValidator(MinValue = 0, MaxValue = 50)]
		[ConfigurationProperty("throttleHighPercent", IsRequired = false, DefaultValue = 0)]
		internal int ThrottleHighPercent
		{
			get
			{
				return (int)base["throttleHighPercent"];
			}
			set
			{
				base["throttleHighPercent"] = value;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x000160FC File Offset: 0x000142FC
		// (set) Token: 0x060004CC RID: 1228 RVA: 0x0001610E File Offset: 0x0001430E
		[ConfigurationProperty("throttleGCInterval", IsRequired = false, DefaultValue = 300)]
		[IntegerValidator(MinValue = 0, MaxValue = 3600)]
		internal int ThrottleGCInterval
		{
			get
			{
				return (int)base["throttleGCInterval"];
			}
			set
			{
				base["throttleGCInterval"] = value;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x00016121 File Offset: 0x00014321
		// (set) Token: 0x060004CE RID: 1230 RVA: 0x00016133 File Offset: 0x00014333
		[IntegerValidator(MinValue = 0)]
		[ConfigurationProperty("syncGCInterval", IsRequired = false, DefaultValue = 0)]
		internal int SyncGCInterval
		{
			get
			{
				return (int)base["syncGCInterval"];
			}
			set
			{
				base["syncGCInterval"] = value;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x00016146 File Offset: 0x00014346
		// (set) Token: 0x060004D0 RID: 1232 RVA: 0x00016158 File Offset: 0x00014358
		[ConfigurationProperty("memoryManagerEnabled", DefaultValue = true, IsRequired = false)]
		internal bool IsMemoryManagerEnabled
		{
			get
			{
				return (bool)base["memoryManagerEnabled"];
			}
			set
			{
				base["memoryManagerEnabled"] = value;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x0001616B File Offset: 0x0001436B
		// (set) Token: 0x060004D2 RID: 1234 RVA: 0x0001617D File Offset: 0x0001437D
		[LongValidator(MaxValue = 4194304L)]
		[ConfigurationProperty("bufferSize", DefaultValue = 1024L, IsRequired = false)]
		internal long BufferSize
		{
			get
			{
				return (long)base["bufferSize"];
			}
			set
			{
				base["bufferSize"] = value;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x00016190 File Offset: 0x00014390
		// (set) Token: 0x060004D4 RID: 1236 RVA: 0x000161A2 File Offset: 0x000143A2
		[ConfigurationProperty("minObjectPoolSize", DefaultValue = 0L, IsRequired = false)]
		internal long MinObjectPoolSize
		{
			get
			{
				return (long)base["minObjectPoolSize"];
			}
			set
			{
				base["minObjectPoolSize"] = value;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x000161B5 File Offset: 0x000143B5
		// (set) Token: 0x060004D6 RID: 1238 RVA: 0x000161C7 File Offset: 0x000143C7
		[ConfigurationProperty("maxObjectPoolSize", DefaultValue = 0L, IsRequired = false)]
		internal long MaxObjectPoolSize
		{
			get
			{
				return (long)base["maxObjectPoolSize"];
			}
			set
			{
				base["maxObjectPoolSize"] = value;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x000161DA File Offset: 0x000143DA
		// (set) Token: 0x060004D8 RID: 1240 RVA: 0x000161EC File Offset: 0x000143EC
		[ConfigurationProperty("averageCacheItemSize", DefaultValue = 0L, IsRequired = false)]
		internal long AverageCacheItemSizeInBytes
		{
			get
			{
				return (long)base["averageCacheItemSize"];
			}
			set
			{
				base["averageCacheItemSize"] = value;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x000161FF File Offset: 0x000143FF
		// (set) Token: 0x060004DA RID: 1242 RVA: 0x00016211 File Offset: 0x00014411
		[ConfigurationProperty("cacheUserDataSizePerNode", DefaultValue = -1L, IsRequired = false)]
		internal long CacheUserDataSizePerNode
		{
			get
			{
				return (long)base["cacheUserDataSizePerNode"];
			}
			set
			{
				base["cacheUserDataSizePerNode"] = value;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x00016224 File Offset: 0x00014424
		// (set) Token: 0x060004DC RID: 1244 RVA: 0x00016236 File Offset: 0x00014436
		[IntegerValidator(MinValue = 1)]
		[ConfigurationProperty("memoryManagerPauseThreshold", DefaultValue = 15, IsRequired = false)]
		internal int MemoryManagerPauseThreshold
		{
			get
			{
				return (int)base["memoryManagerPauseThreshold"];
			}
			set
			{
				base["memoryManagerPauseThreshold"] = value;
			}
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0001624C File Offset: 0x0001444C
		protected MemoryPressureMonitorProperties(SerializationInfo info, StreamingContext context)
		{
			this.IsEnabled = info.GetBoolean("isEnabled");
			this.LowMemoryPercent = (int)info.GetValue("lowMemoryPercent", typeof(int));
			this.LowMemoryReleasePercent = (int)info.GetValue("lowMemoryReleasePercent", typeof(int));
			this.ThrottleLowPercent = (int)info.GetValue("throttleLowPercent", typeof(int));
			this.ThrottleHighPercent = (int)info.GetValue("throttleHighPercent", typeof(int));
			this.ThrottleGCInterval = (int)info.GetValue("throttleGCInterval", typeof(int));
			this.Interval = (int)info.GetValue("interval", typeof(int));
			this.SyncGCInterval = (int)info.GetValue("syncGCInterval", typeof(int));
			try
			{
				this.IsMemoryManagerEnabled = (bool)info.GetValue("memoryManagerEnabled", typeof(bool));
				this.BufferSize = (long)info.GetValue("bufferSize", typeof(long));
				this.MinObjectPoolSize = (long)info.GetValue("minObjectPoolSize", typeof(long));
				this.MaxObjectPoolSize = (long)info.GetValue("maxObjectPoolSize", typeof(long));
			}
			catch (SerializationException)
			{
				this.IsMemoryManagerEnabled = true;
				this.BufferSize = 1024L;
				this.MinObjectPoolSize = 0L;
				this.MaxObjectPoolSize = 0L;
			}
			try
			{
				this.InternalThrottleLowPercent = (int)info.GetValue("internalThrottleLowPercent", typeof(int));
				this.InternalThrottleHighPercent = (int)info.GetValue("internalThrottleHighPercent", typeof(int));
				this.PercentItemInFinalizerQueue = (int)info.GetValue("percentItemInFinalizerQueue", typeof(int));
			}
			catch (SerializationException)
			{
				this.InternalThrottleLowPercent = 0;
				this.InternalThrottleHighPercent = 0;
				this.PercentItemInFinalizerQueue = 5;
			}
			try
			{
				this.MemoryManagerPauseThreshold = info.GetInt32("memoryManagerPauseThreshold");
			}
			catch (SerializationException)
			{
				this.MemoryManagerPauseThreshold = 15;
			}
			try
			{
				this.CacheUserDataSizePerNode = (long)info.GetValue("cacheUserDataSizePerNode", typeof(long));
			}
			catch (SerializationException)
			{
				this.CacheUserDataSizePerNode = -1L;
			}
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x000164F4 File Offset: 0x000146F4
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("isEnabled", this.IsEnabled);
			info.AddValue("lowMemoryPercent", this.LowMemoryPercent);
			info.AddValue("lowMemoryReleasePercent", this.LowMemoryReleasePercent);
			info.AddValue("throttleLowPercent", this.ThrottleLowPercent);
			info.AddValue("throttleHighPercent", this.ThrottleHighPercent);
			info.AddValue("throttleGCInterval", this.ThrottleGCInterval);
			info.AddValue("interval", this.Interval);
			info.AddValue("syncGCInterval", this.SyncGCInterval);
			info.AddValue("memoryManagerEnabled", this.IsMemoryManagerEnabled);
			info.AddValue("bufferSize", this.BufferSize);
			info.AddValue("minObjectPoolSize", this.MinObjectPoolSize);
			info.AddValue("maxObjectPoolSize", this.MaxObjectPoolSize);
			info.AddValue("internalThrottleLowPercent", this.InternalThrottleLowPercent);
			info.AddValue("internalThrottleHighPercent", this.InternalThrottleHighPercent);
			info.AddValue("percentItemInFinalizerQueue", this.PercentItemInFinalizerQueue);
			info.AddValue("memoryManagerPauseThreshold", this.MemoryManagerPauseThreshold);
			info.AddValue("cacheUserDataSizePerNode", this.CacheUserDataSizePerNode);
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00016624 File Offset: 0x00014824
		public MemoryPressureMonitorChange ComputeDifferences(MemoryPressureMonitorProperties other)
		{
			MemoryPressureMonitorChange memoryPressureMonitorChange = default(MemoryPressureMonitorChange);
			memoryPressureMonitorChange[MemoryPressureMonitorChanges.IsEnabledChange] = this.IsEnabled != other.IsEnabled;
			memoryPressureMonitorChange[MemoryPressureMonitorChanges.LowMemoryPercentChange] = this.LowMemoryPercent != other.LowMemoryPercent;
			memoryPressureMonitorChange[MemoryPressureMonitorChanges.LowMemoryReleasePercentChange] = this.LowMemoryReleasePercent != other.LowMemoryReleasePercent;
			memoryPressureMonitorChange[MemoryPressureMonitorChanges.IntervalChange] = this.Interval != other.Interval;
			memoryPressureMonitorChange[MemoryPressureMonitorChanges.ThrottleLowPercentChange] = this.ThrottleLowPercent != other.ThrottleLowPercent;
			memoryPressureMonitorChange[MemoryPressureMonitorChanges.ThrottleHighPercentChange] = this.ThrottleHighPercent != other.ThrottleHighPercent;
			memoryPressureMonitorChange[MemoryPressureMonitorChanges.ThrottleGCIntervalChange] = this.ThrottleGCInterval != other.ThrottleGCInterval;
			memoryPressureMonitorChange[MemoryPressureMonitorChanges.SyncGCIntervalChange] = this.SyncGCInterval != other.SyncGCInterval;
			memoryPressureMonitorChange[MemoryPressureMonitorChanges.IsMemoryManagerEnabledChange] = this.IsMemoryManagerEnabled != other.IsMemoryManagerEnabled;
			memoryPressureMonitorChange[MemoryPressureMonitorChanges.BufferSizeChange] = this.BufferSize != other.BufferSize;
			memoryPressureMonitorChange[MemoryPressureMonitorChanges.MinObjectPoolSizeChange] = this.MinObjectPoolSize != other.MinObjectPoolSize;
			memoryPressureMonitorChange[MemoryPressureMonitorChanges.MaxObjectPoolSizeChange] = this.MaxObjectPoolSize != other.MaxObjectPoolSize;
			memoryPressureMonitorChange[MemoryPressureMonitorChanges.InternalThrottleHighPercentChange] = this.InternalThrottleHighPercent != other.InternalThrottleHighPercent;
			memoryPressureMonitorChange[MemoryPressureMonitorChanges.InternalThrottleLowPercentChange] = this.InternalThrottleLowPercent != other.InternalThrottleLowPercent;
			memoryPressureMonitorChange[MemoryPressureMonitorChanges.PercentItemInFinalizerQueue] = this.PercentItemInFinalizerQueue != other.PercentItemInFinalizerQueue;
			memoryPressureMonitorChange[MemoryPressureMonitorChanges.MemoryManagerPauseThreshold] = this.MemoryManagerPauseThreshold != other.MemoryManagerPauseThreshold;
			memoryPressureMonitorChange[MemoryPressureMonitorChanges.AverageCacheItemSizeInBytes] = this.AverageCacheItemSizeInBytes != other.AverageCacheItemSizeInBytes;
			memoryPressureMonitorChange[MemoryPressureMonitorChanges.CacheUserDataSizePerNodeChange] = this.CacheUserDataSizePerNode != other.CacheUserDataSizePerNode;
			return memoryPressureMonitorChange;
		}

		// Token: 0x0400036A RID: 874
		internal const long DefaultMinObjectPoolSize = 0L;

		// Token: 0x0400036B RID: 875
		internal const long DefaultMaxObjectPoolSize = 0L;

		// Token: 0x0400036C RID: 876
		internal const long DefaultAverageCacheItemSize = 0L;

		// Token: 0x0400036D RID: 877
		internal const string IS_ENABLED = "isEnabled";

		// Token: 0x0400036E RID: 878
		internal const string LOW_MEM_PERCENT = "lowMemoryPercent";

		// Token: 0x0400036F RID: 879
		internal const string LOW_MEM_RELEASE_PERCENT = "lowMemoryReleasePercent";

		// Token: 0x04000370 RID: 880
		internal const string THROTTLE_LOW_PERCENT = "throttleLowPercent";

		// Token: 0x04000371 RID: 881
		internal const string THROTTLE_HIGH_PERCENT = "throttleHighPercent";

		// Token: 0x04000372 RID: 882
		internal const string INTERNAL_THROTTLE_LOW_PERCENT = "internalThrottleLowPercent";

		// Token: 0x04000373 RID: 883
		internal const string INTERNAL_THROTTLE_HIGH_PERCENT = "internalThrottleHighPercent";

		// Token: 0x04000374 RID: 884
		internal const string THROTTLE_GC_INTERVAL = "throttleGCInterval";

		// Token: 0x04000375 RID: 885
		internal const string INTERVAL = "interval";

		// Token: 0x04000376 RID: 886
		internal const string SYNC_GC_INTERVAL = "syncGCInterval";

		// Token: 0x04000377 RID: 887
		internal const string ISMEMORYMANAGERENABLED = "memoryManagerEnabled";

		// Token: 0x04000378 RID: 888
		internal const string BUFFERSIZE = "bufferSize";

		// Token: 0x04000379 RID: 889
		internal const string MINOBJECTPOOLSIZE = "minObjectPoolSize";

		// Token: 0x0400037A RID: 890
		internal const string MAXOBJECTPOOLSIZE = "maxObjectPoolSize";

		// Token: 0x0400037B RID: 891
		internal const string PERCENT_ITEM_IN_FINALIZER_QUEUE = "percentItemInFinalizerQueue";

		// Token: 0x0400037C RID: 892
		internal const string MEMORYMANAGER_PAUSE_THRESHOLD = "memoryManagerPauseThreshold";

		// Token: 0x0400037D RID: 893
		internal const string AVERAGECACHEITEMSIZE = "averageCacheItemSize";

		// Token: 0x0400037E RID: 894
		internal const string CACHEUSERDATASIZEPERNODE = "cacheUserDataSizePerNode";
	}
}
