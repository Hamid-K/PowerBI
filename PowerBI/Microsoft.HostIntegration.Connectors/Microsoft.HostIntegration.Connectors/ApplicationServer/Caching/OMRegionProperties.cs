using System;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000289 RID: 649
	[DataContract]
	[Serializable]
	internal class OMRegionProperties
	{
		// Token: 0x06001788 RID: 6024 RVA: 0x00047AEC File Offset: 0x00045CEC
		public OMRegionProperties(int indexLevel)
		{
			this._indexLevel = indexLevel;
			this._consistencyType = ConsistencyType.StrongConsistency;
			this._oType = ObjectType.SerializedBufferRefType;
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x00047B58 File Offset: 0x00045D58
		public OMRegionProperties(int indexLevel, ConsistencyType cType)
		{
			this._indexLevel = indexLevel;
			this._consistencyType = cType;
			this._oType = ObjectType.SerializedBufferRefType;
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x00047BC4 File Offset: 0x00045DC4
		public OMRegionProperties()
		{
			this._consistencyType = ConsistencyType.StrongConsistency;
			this._oType = ObjectType.SerializedBufferRefType;
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x00047C28 File Offset: 0x00045E28
		public OMRegionProperties(OMRegionProperties props)
		{
			this._consistencyType = props._consistencyType;
			this._isEvictable = props._isEvictable;
			this._isExpirableItemsFound = props._isExpirableItemsFound;
			this._indexLevel = props._indexLevel;
			this._oType = props._oType;
			this._expirationType = props._expirationType;
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x00047CC8 File Offset: 0x00045EC8
		public OMRegionProperties(INamedCacheConfiguration props)
		{
			this._oType = ObjectType.SerializedBufferRefType;
			this._isEvictable = props.EvictionType != EvictionType.None;
			this._consistencyType = props.Consistency;
			this._isExpirableItemsFound = props.IsExpirable;
			this._expirationType = props.ExpirationType;
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x0600178D RID: 6029 RVA: 0x00047D5A File Offset: 0x00045F5A
		// (set) Token: 0x0600178E RID: 6030 RVA: 0x00047D62 File Offset: 0x00045F62
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

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x0600178F RID: 6031 RVA: 0x00047D6B File Offset: 0x00045F6B
		// (set) Token: 0x06001790 RID: 6032 RVA: 0x00047D73 File Offset: 0x00045F73
		public int IndexLevel
		{
			get
			{
				return this._indexLevel;
			}
			set
			{
				this._indexLevel = value;
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06001791 RID: 6033 RVA: 0x00047D7C File Offset: 0x00045F7C
		// (set) Token: 0x06001792 RID: 6034 RVA: 0x00047D84 File Offset: 0x00045F84
		public int RootDirBitMaskSize
		{
			get
			{
				return this._regionRootDirBitMaskSize;
			}
			set
			{
				this._regionRootDirBitMaskSize = value;
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06001793 RID: 6035 RVA: 0x00047D8D File Offset: 0x00045F8D
		// (set) Token: 0x06001794 RID: 6036 RVA: 0x00047D95 File Offset: 0x00045F95
		public int SubDirBitMaskSize
		{
			get
			{
				return this._regionSubDirBitMaskSize;
			}
			set
			{
				this._regionSubDirBitMaskSize = value;
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06001795 RID: 6037 RVA: 0x00047D9E File Offset: 0x00045F9E
		// (set) Token: 0x06001796 RID: 6038 RVA: 0x00047DA6 File Offset: 0x00045FA6
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

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06001797 RID: 6039 RVA: 0x00047DAF File Offset: 0x00045FAF
		// (set) Token: 0x06001798 RID: 6040 RVA: 0x00047DB7 File Offset: 0x00045FB7
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

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06001799 RID: 6041 RVA: 0x00047DC0 File Offset: 0x00045FC0
		// (set) Token: 0x0600179A RID: 6042 RVA: 0x00047DC8 File Offset: 0x00045FC8
		public bool IsExpirableItemsFound
		{
			get
			{
				return this._isExpirableItemsFound;
			}
			set
			{
				this._isExpirableItemsFound = value;
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x0600179B RID: 6043 RVA: 0x00047DD1 File Offset: 0x00045FD1
		// (set) Token: 0x0600179C RID: 6044 RVA: 0x00047DD9 File Offset: 0x00045FD9
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

		// Token: 0x0600179D RID: 6045 RVA: 0x00047DE2 File Offset: 0x00045FE2
		[OnDeserializing]
		private void OnDeserializingMethod(StreamingContext context)
		{
			this._expirationType = ConfigManager.RGB_EXPTYPE;
		}

		// Token: 0x04000D16 RID: 3350
		private static int INDEXLEVEL = ConfigManager.REGN_INDEXLEVEL;

		// Token: 0x04000D17 RID: 3351
		private static int ROOTDIRBITMASKSIZE = 4;

		// Token: 0x04000D18 RID: 3352
		private static int SUBDIRBITMASKSIZE = 4;

		// Token: 0x04000D19 RID: 3353
		[DataMember]
		private ObjectType _oType;

		// Token: 0x04000D1A RID: 3354
		[DataMember]
		private int _indexLevel = OMRegionProperties.INDEXLEVEL;

		// Token: 0x04000D1B RID: 3355
		[DataMember]
		private int _regionRootDirBitMaskSize = OMRegionProperties.ROOTDIRBITMASKSIZE;

		// Token: 0x04000D1C RID: 3356
		[DataMember]
		private int _regionSubDirBitMaskSize = OMRegionProperties.SUBDIRBITMASKSIZE;

		// Token: 0x04000D1D RID: 3357
		[DataMember]
		private ConsistencyType _consistencyType;

		// Token: 0x04000D1E RID: 3358
		[DataMember]
		private bool _isEvictable = ConfigManager.RGN_EVICTABLE;

		// Token: 0x04000D1F RID: 3359
		[DataMember]
		private bool _isExpirableItemsFound = ConfigManager.RGN_EXPIRABLE;

		// Token: 0x04000D20 RID: 3360
		[DataMember]
		private ExpirationType _expirationType = ConfigManager.RGB_EXPTYPE;
	}
}
