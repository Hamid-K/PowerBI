using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Text;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200026B RID: 619
	[DataContract(Name = "AOMCacheItem", Namespace = "http://schemas.microsoft.com/velocity/namespace")]
	internal abstract class AOMCacheItem : ADMCacheItem, IOMCacheItem, IBaseDataNode
	{
		// Token: 0x060014A9 RID: 5289 RVA: 0x00036894 File Offset: 0x00034A94
		public AOMCacheItem()
		{
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x00040661 File Offset: 0x0003E861
		public AOMCacheItem(AOMCacheItem item)
			: base(item)
		{
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x0004066A File Offset: 0x0003E86A
		public AOMCacheItem(InternalCacheItemVersion version, long TTL, long extnTimeout)
			: base(version, TTL, extnTimeout)
		{
		}

		// Token: 0x060014AC RID: 5292
		public abstract void GetKeyValueTag(ref Key key, ref object value, ref DataCacheTag[] tags);

		// Token: 0x060014AD RID: 5293 RVA: 0x00003CAB File Offset: 0x00001EAB
		public override void Init(Key key, object value, long TTL, long extnTimeout, object[] tags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x060014AE RID: 5294 RVA: 0x00040678 File Offset: 0x0003E878
		public TimeSpan TimeBeforeExpiry
		{
			get
			{
				long timeToLive = base.TimeToLive;
				if (timeToLive == 9223372036854775807L)
				{
					return TimeSpan.MaxValue;
				}
				return Utility.ConvertCounterToTimeSpan(base.TimeToLive, Stopwatch.GetTimestamp());
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x060014AF RID: 5295 RVA: 0x000406B0 File Offset: 0x0003E8B0
		public TimeSpan TimeToExtend
		{
			get
			{
				long extensionTimeout = base.ExtensionTimeout;
				if (extensionTimeout == 9223372036854775807L)
				{
					return TimeSpan.MaxValue;
				}
				return Utility.ConvertCounterToTimeSpan(base.ExtensionTimeout, 0L);
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x060014B0 RID: 5296
		public abstract object[] Tags { get; }

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x060014B1 RID: 5297
		// (set) Token: 0x060014B2 RID: 5298
		public abstract int Size { get; set; }

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x060014B3 RID: 5299
		public abstract object Value { get; }

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x060014B4 RID: 5300
		internal abstract int ValueSize { get; }

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x060014B5 RID: 5301
		// (set) Token: 0x060014B6 RID: 5302
		public abstract IOMRegion Region { get; set; }

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x060014B7 RID: 5303
		public abstract string RegionName { get; }

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x060014B8 RID: 5304 RVA: 0x000406E3 File Offset: 0x0003E8E3
		public string CacheName
		{
			get
			{
				if (this.Region != null)
				{
					return this.Region.CacheName;
				}
				return null;
			}
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x000406FA File Offset: 0x0003E8FA
		public bool IsItemExpired()
		{
			return ExpirationType.SlidingExpiration != this.Region.ExpirationType && base.IsExpired();
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x00040715 File Offset: 0x0003E915
		public bool IsItemExpired(long currentTimeStampValue)
		{
			return ExpirationType.SlidingExpiration != this.Region.ExpirationType && base.IsExpired(currentTimeStampValue);
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x0003657E File Offset: 0x0003477E
		public bool IsItemExpiredForEviction()
		{
			return base.IsExpired();
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x00040731 File Offset: 0x0003E931
		public bool IsItemExpiredForEviction(long currentTimeStampValue)
		{
			return base.IsExpired(currentTimeStampValue);
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x0004073A File Offset: 0x0003E93A
		public void UpdateTTLOnAccess()
		{
			if (ExpirationType.SlidingExpiration == this.Region.ExpirationType)
			{
				base.UpdateTTL();
			}
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x00040750 File Offset: 0x0003E950
		public void UpdateTTLOnAccess(long currentTimeStampValue)
		{
			if (ExpirationType.SlidingExpiration == this.Region.ExpirationType)
			{
				base.UpdateTTL(currentTimeStampValue);
			}
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x00040768 File Offset: 0x0003E968
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("OMCacheItem = [Base(" + base.ToString());
			stringBuilder.Append(") ");
			if (this.Region != null && this.Region.State != null)
			{
				stringBuilder.Append("RegionRange = ");
				stringBuilder.Append(this.Region.State);
			}
			if (this.CacheName != null)
			{
				stringBuilder.Append(":Cache = ");
				stringBuilder.Append(this.CacheName);
			}
			if (this.RegionName != null)
			{
				stringBuilder.Append(":Region = ");
				stringBuilder.Append(this.RegionName);
			}
			if (this.Key != null)
			{
				stringBuilder.Append(":Key = ");
				stringBuilder.Append(this.Key.ToString());
			}
			if (this.Tags != null && this.Tags.Length > 0)
			{
				for (int i = 0; i < this.Tags.Length; i++)
				{
					if (this.Tags[i] != null)
					{
						stringBuilder.Append(":Tag = ");
						stringBuilder.Append(this.Tags[i]);
					}
				}
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x060014C0 RID: 5312
		public abstract Key Key { get; }

		// Token: 0x060014C1 RID: 5313 RVA: 0x00040896 File Offset: 0x0003EA96
		protected new void Clean()
		{
			base.Clean();
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x0004089E File Offset: 0x0003EA9E
		protected new void Init()
		{
			base.Init();
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x000408A6 File Offset: 0x0003EAA6
		public override int GetHashCode()
		{
			return this.Key.GetHashCode();
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x000408B4 File Offset: 0x0003EAB4
		public override bool Equals(object obj)
		{
			if (object.ReferenceEquals(this, obj))
			{
				return true;
			}
			AOMCacheItem aomcacheItem = obj as AOMCacheItem;
			if (aomcacheItem != null)
			{
				return this.Key.Equals(aomcacheItem.Key);
			}
			return this.Key.Equals(obj);
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x060014C5 RID: 5317 RVA: 0x000408F4 File Offset: 0x0003EAF4
		object IBaseDataNode.Key
		{
			get
			{
				return this.Key;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x060014C6 RID: 5318 RVA: 0x00008948 File Offset: 0x00006B48
		public object Data
		{
			get
			{
				return this;
			}
		}

		// Token: 0x04000C52 RID: 3154
		public const int PointersOverhead = 16;
	}
}
