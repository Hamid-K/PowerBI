using System;
using System.Threading;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000281 RID: 641
	internal class OMCacheNodeProperties
	{
		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06001640 RID: 5696 RVA: 0x000447C4 File Offset: 0x000429C4
		// (set) Token: 0x06001641 RID: 5697 RVA: 0x000447CC File Offset: 0x000429CC
		public ObjectType OType
		{
			get
			{
				return this._oType;
			}
			set
			{
				this._oType = value;
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06001642 RID: 5698 RVA: 0x000447D5 File Offset: 0x000429D5
		// (set) Token: 0x06001643 RID: 5699 RVA: 0x000447DD File Offset: 0x000429DD
		public bool IsEvictable
		{
			get
			{
				return this._isEvictable;
			}
			set
			{
				this._isEvictable = value;
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06001644 RID: 5700 RVA: 0x000447E6 File Offset: 0x000429E6
		// (set) Token: 0x06001645 RID: 5701 RVA: 0x000447EE File Offset: 0x000429EE
		public bool IsExpirable
		{
			get
			{
				return this._isExpirable;
			}
			set
			{
				this._isExpirable = value;
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06001646 RID: 5702 RVA: 0x000447F7 File Offset: 0x000429F7
		// (set) Token: 0x06001647 RID: 5703 RVA: 0x000447FF File Offset: 0x000429FF
		public int EvictionInterval
		{
			get
			{
				return this._evictionInterval;
			}
			set
			{
				this._evictionInterval = value;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06001648 RID: 5704 RVA: 0x00044808 File Offset: 0x00042A08
		// (set) Token: 0x06001649 RID: 5705 RVA: 0x00044810 File Offset: 0x00042A10
		public ThreadPriority EvictionPriority
		{
			get
			{
				return this._evictionPriority;
			}
			set
			{
				this._evictionPriority = value;
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x0600164A RID: 5706 RVA: 0x00044819 File Offset: 0x00042A19
		// (set) Token: 0x0600164B RID: 5707 RVA: 0x00044821 File Offset: 0x00042A21
		public int HighWaterMarkPercentage
		{
			get
			{
				return this._highWaterMarkPercentage;
			}
			set
			{
				this._highWaterMarkPercentage = value;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x0600164C RID: 5708 RVA: 0x0004482A File Offset: 0x00042A2A
		public long HighWaterMark
		{
			get
			{
				return (long)this._highWaterMarkPercentage * this._maxSize / 100L;
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x0600164D RID: 5709 RVA: 0x0004483E File Offset: 0x00042A3E
		// (set) Token: 0x0600164E RID: 5710 RVA: 0x00044846 File Offset: 0x00042A46
		public int LowWaterMarkPercentage
		{
			get
			{
				return this._lowWaterMarkPercentage;
			}
			set
			{
				this._lowWaterMarkPercentage = value;
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x0600164F RID: 5711 RVA: 0x0004484F File Offset: 0x00042A4F
		public long LowWaterMark
		{
			get
			{
				return (long)this._lowWaterMarkPercentage * this._maxSize / 100L;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06001650 RID: 5712 RVA: 0x00044863 File Offset: 0x00042A63
		// (set) Token: 0x06001651 RID: 5713 RVA: 0x0004486B File Offset: 0x00042A6B
		public long MaxSize
		{
			get
			{
				return this._maxSize;
			}
			set
			{
				this._maxSize = value;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06001652 RID: 5714 RVA: 0x00044874 File Offset: 0x00042A74
		public bool PerfMonCounterRequired
		{
			get
			{
				return this._perfMonCounterRequired;
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06001653 RID: 5715 RVA: 0x0004487C File Offset: 0x00042A7C
		// (set) Token: 0x06001654 RID: 5716 RVA: 0x00044884 File Offset: 0x00042A84
		public int MaxNamedCacheCount
		{
			get
			{
				return this._maxNamedCacheCount;
			}
			set
			{
				this._maxNamedCacheCount = value;
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06001655 RID: 5717 RVA: 0x0004488D File Offset: 0x00042A8D
		// (set) Token: 0x06001656 RID: 5718 RVA: 0x00044895 File Offset: 0x00042A95
		public EvictionType EvictionType
		{
			get
			{
				return this._evictionType;
			}
			set
			{
				this._evictionType = value;
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06001657 RID: 5719 RVA: 0x0004489E File Offset: 0x00042A9E
		// (set) Token: 0x06001658 RID: 5720 RVA: 0x000448A6 File Offset: 0x00042AA6
		public ExpirationType ExpirationType
		{
			get
			{
				return this._expirationType;
			}
			set
			{
				this._expirationType = value;
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06001659 RID: 5721 RVA: 0x000448AF File Offset: 0x00042AAF
		// (set) Token: 0x0600165A RID: 5722 RVA: 0x000448B7 File Offset: 0x00042AB7
		public int QueuesCount
		{
			get
			{
				return this._noOfQueues;
			}
			set
			{
				this._noOfQueues = value;
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x0600165B RID: 5723 RVA: 0x000448C0 File Offset: 0x00042AC0
		// (set) Token: 0x0600165C RID: 5724 RVA: 0x000448C8 File Offset: 0x00042AC8
		public ConsistencyType ConsistencyType
		{
			get
			{
				return this._consistencyType;
			}
			set
			{
				this._consistencyType = value;
			}
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x000448D1 File Offset: 0x00042AD1
		public OMCacheNodeProperties()
		{
			this.InitializeHostDefaults(null);
			this.InitializeNamedCacheDefaults();
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x00044903 File Offset: 0x00042B03
		public OMCacheNodeProperties(int maxNamedCacheCount)
		{
			this._maxNamedCacheCount = maxNamedCacheCount;
			this.InitializeHostDefaults(null);
			this.InitializeNamedCacheDefaults();
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x0004493C File Offset: 0x00042B3C
		public OMCacheNodeProperties(IHostConfiguration props, int maxNC, bool perfCounterRequired)
		{
			if (maxNC > 0)
			{
				this._maxNamedCacheCount = maxNC;
			}
			else
			{
				this._maxNamedCacheCount = 128;
			}
			this._perfMonCounterRequired = perfCounterRequired;
			this.InitializeHostDefaults(props);
			this.InitializeNamedCacheDefaults();
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x00044998 File Offset: 0x00042B98
		private void InitializeHostDefaults(IHostConfiguration props)
		{
			IHostConfiguration hostConfiguration;
			if (props != null)
			{
				hostConfiguration = props;
			}
			else
			{
				hostConfiguration = ServiceConfigurationManager.GetHostDefaults();
			}
			this._isEvictable = true;
			this._evictionInterval = hostConfiguration.EvictionInterval * 1000;
			this._highWaterMarkPercentage = (int)hostConfiguration.HighWaterMarkPercentage;
			this._lowWaterMarkPercentage = (int)hostConfiguration.LowWaterMarkPercentage;
			this._maxSize = hostConfiguration.Size << 20;
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x000449F8 File Offset: 0x00042BF8
		private void InitializeNamedCacheDefaults()
		{
			INamedCacheConfiguration namedCacheDefault = ServiceConfigurationManager.GetNamedCacheDefault();
			this._isExpirable = namedCacheDefault.IsExpirable;
			this._consistencyType = namedCacheDefault.Consistency;
			this._evictionType = namedCacheDefault.EvictionType;
			this._expirationType = namedCacheDefault.ExpirationType;
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x00044A3C File Offset: 0x00042C3C
		public OMCacheNodeProperties(OMCacheNodeProperties props)
		{
			this._isEvictable = props._isEvictable;
			this._evictionInterval = props._evictionInterval;
			this._evictionType = props._evictionType;
			this._expirationType = props._expirationType;
			this._isExpirable = props._isExpirable;
			this._highWaterMarkPercentage = props._highWaterMarkPercentage;
			this._lowWaterMarkPercentage = props._lowWaterMarkPercentage;
			this._maxNamedCacheCount = props._maxNamedCacheCount;
			this._maxSize = props._maxSize;
			this._oType = props._oType;
			this._noOfQueues = props._noOfQueues;
			this._consistencyType = props._consistencyType;
			this._perfMonCounterRequired = props._perfMonCounterRequired;
		}

		// Token: 0x04000C89 RID: 3209
		private static int MAXNAMEDCACHECOUNT = 128;

		// Token: 0x04000C8A RID: 3210
		private static int QUEUESCOUNT = ConfigManager.CND_QUEUESCOUNT;

		// Token: 0x04000C8B RID: 3211
		private ObjectType _oType = ObjectType.SerializedBufferRefType;

		// Token: 0x04000C8C RID: 3212
		private bool _isEvictable;

		// Token: 0x04000C8D RID: 3213
		private bool _isExpirable;

		// Token: 0x04000C8E RID: 3214
		private int _evictionInterval;

		// Token: 0x04000C8F RID: 3215
		private ThreadPriority _evictionPriority;

		// Token: 0x04000C90 RID: 3216
		private int _highWaterMarkPercentage;

		// Token: 0x04000C91 RID: 3217
		private int _lowWaterMarkPercentage;

		// Token: 0x04000C92 RID: 3218
		private long _maxSize;

		// Token: 0x04000C93 RID: 3219
		private bool _perfMonCounterRequired;

		// Token: 0x04000C94 RID: 3220
		private int _maxNamedCacheCount = OMCacheNodeProperties.MAXNAMEDCACHECOUNT;

		// Token: 0x04000C95 RID: 3221
		private EvictionType _evictionType;

		// Token: 0x04000C96 RID: 3222
		private ExpirationType _expirationType;

		// Token: 0x04000C97 RID: 3223
		private int _noOfQueues = OMCacheNodeProperties.QUEUESCOUNT;

		// Token: 0x04000C98 RID: 3224
		private ConsistencyType _consistencyType;
	}
}
