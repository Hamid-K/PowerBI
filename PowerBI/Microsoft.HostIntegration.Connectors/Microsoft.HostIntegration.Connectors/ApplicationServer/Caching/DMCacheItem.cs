using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000275 RID: 629
	[DataContract(Name = "DMCacheItem", Namespace = "http://schemas.microsoft.com/velocity/namespace")]
	internal class DMCacheItem : MDHObjectNode
	{
		// Token: 0x06001541 RID: 5441 RVA: 0x00040B50 File Offset: 0x0003ED50
		public DMCacheItem(object key, InternalCacheItemVersion version, long ttl, long extnTimeout)
			: base(key.GetHashCode())
		{
			base.Key = key;
			this.Version = version;
			this.TimeToLive = ttl;
			this.ExtensionTimeout = extnTimeout;
			base.IsCommitted = true;
		}

		// Token: 0x06001542 RID: 5442 RVA: 0x00040BB8 File Offset: 0x0003EDB8
		public DMCacheItem(object key, InternalCacheItemVersion version)
			: base(key.GetHashCode())
		{
			base.Key = key;
			this.Version = version;
			base.IsCommitted = true;
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x00040C14 File Offset: 0x0003EE14
		public DMCacheItem(object key)
			: base(key.GetHashCode())
		{
			base.Key = key;
			this.Version = default(InternalCacheItemVersion);
			this._ttl = long.MaxValue;
			base.IsCommitted = true;
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x00040C84 File Offset: 0x0003EE84
		public DMCacheItem()
		{
			this.Version = default(InternalCacheItemVersion);
			this._ttl = long.MaxValue;
			base.IsCommitted = true;
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x00040CE8 File Offset: 0x0003EEE8
		public DMCacheItem(ADMCacheItem item)
			: base(item)
		{
			this._DMLockHandle = item.GetDMLockHandle();
			this._lockExpirationTime = item.LockExpirationTime;
			this._version = item.Version;
			this._ttl = item.TimeToLive;
			this._extensionTimeout = item.ExtensionTimeout;
			this._lastAccessedTicks = item.LastAccess;
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06001546 RID: 5446 RVA: 0x00040D6F File Offset: 0x0003EF6F
		// (set) Token: 0x06001547 RID: 5447 RVA: 0x00040D77 File Offset: 0x0003EF77
		public InternalCacheItemVersion Version
		{
			get
			{
				return this._version;
			}
			set
			{
				this._version = value;
			}
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x00040D80 File Offset: 0x0003EF80
		public DataCacheLockHandle GetLockHandle()
		{
			return new DataCacheLockHandle(this._DMLockHandle, this.Version);
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06001549 RID: 5449 RVA: 0x00040D93 File Offset: 0x0003EF93
		// (set) Token: 0x0600154A RID: 5450 RVA: 0x00040D9B File Offset: 0x0003EF9B
		public long TimeToLive
		{
			get
			{
				return this._ttl;
			}
			set
			{
				this._ttl = value;
				if (Provider.IsEnabled(TraceLevel.Verbose))
				{
					EventLogWriter.WriteVerbose<long, DMCacheItem, long>("DistributedCache.DataManager", "Assigning ttl = {0} to {1} at time = {2}.", value, this, Stopwatch.GetTimestamp());
				}
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x0600154B RID: 5451 RVA: 0x00040DC2 File Offset: 0x0003EFC2
		// (set) Token: 0x0600154C RID: 5452 RVA: 0x00040DCA File Offset: 0x0003EFCA
		public long ExtensionTimeout
		{
			get
			{
				return this._extensionTimeout;
			}
			set
			{
				this._extensionTimeout = value;
				if (Provider.IsEnabled(TraceLevel.Verbose))
				{
					EventLogWriter.WriteVerbose<long, DMCacheItem, long>("DistributedCache.DataManager", "Assigning extensionTimeout = {0} to {1} at time = {2}.", value, this, Stopwatch.GetTimestamp());
				}
			}
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x00040DF4 File Offset: 0x0003EFF4
		public override bool Equals(object obj)
		{
			DMCacheItem dmcacheItem = (DMCacheItem)obj;
			return base.Key.Equals(dmcacheItem.Key) && this.Version.Equals(dmcacheItem.Version);
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x00040E31 File Offset: 0x0003F031
		public override int GetHashCode()
		{
			return base.Key.GetHashCode();
		}

		// Token: 0x0600154F RID: 5455 RVA: 0x00040E3E File Offset: 0x0003F03E
		public bool IsExpired()
		{
			return this.IsExpired(Stopwatch.GetTimestamp());
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06001550 RID: 5456 RVA: 0x00040E4B File Offset: 0x0003F04B
		// (set) Token: 0x06001551 RID: 5457 RVA: 0x00040E58 File Offset: 0x0003F058
		public bool IsLockPlaceHolderObject
		{
			get
			{
				return this._flag.IsLockPlaceHolderObject;
			}
			set
			{
				this._flag.IsLockPlaceHolderObject = value;
			}
		}

		// Token: 0x06001552 RID: 5458 RVA: 0x00040E66 File Offset: 0x0003F066
		public bool IsExpired(long currentTimeStampValue)
		{
			return Utility.IsExpiredTimeStamp(currentTimeStampValue, this.TimeToLive) && this.IsLockInvalidOrExpired(currentTimeStampValue);
		}

		// Token: 0x06001553 RID: 5459 RVA: 0x00040E7F File Offset: 0x0003F07F
		private bool IsLockInvalidOrExpired(long currentTimeStampValue)
		{
			return !this.IsLocked() || this.IsCurrentLockExpired(currentTimeStampValue);
		}

		// Token: 0x06001554 RID: 5460 RVA: 0x00040E94 File Offset: 0x0003F094
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(" DMCacheItem => [");
			stringBuilder.Append(" Key = ");
			stringBuilder.Append(base.Key);
			stringBuilder.Append(" Version = ");
			stringBuilder.Append(this.Version);
			stringBuilder.Append(" LockID = ");
			stringBuilder.Append(this._DMLockHandle.LockID);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x00040F1C File Offset: 0x0003F11C
		internal bool IsLockHandleEqual(DataCacheLockHandle handle)
		{
			return handle.Handle.LockID == this._DMLockHandle.LockID && this.Version.CompareTo(handle.Version) == 0;
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x00040F60 File Offset: 0x0003F160
		internal void SetLock(TimeSpan timeSpan)
		{
			this._lockExpirationTime = Utility.AddTimeSpanToCurrentCounter(timeSpan);
			int num = this._DMLockHandle.LockID;
			num *= -1;
			if (num == 2147483647)
			{
				num = 0;
			}
			num++;
			this._DMLockHandle.LockID = num;
		}

		// Token: 0x06001557 RID: 5463 RVA: 0x00040FA3 File Offset: 0x0003F1A3
		internal void ClearLock()
		{
			this._DMLockHandle.LockID = this._DMLockHandle.LockID * -1;
			if (this._DMLockHandle.LockID == -2147483648)
			{
				this._DMLockHandle.LockID = 0;
			}
		}

		// Token: 0x06001558 RID: 5464 RVA: 0x00040FDB File Offset: 0x0003F1DB
		internal bool IsLocked()
		{
			return this._DMLockHandle.LockID > 0;
		}

		// Token: 0x06001559 RID: 5465 RVA: 0x00040FEB File Offset: 0x0003F1EB
		internal bool IsCurrentLockExpired()
		{
			return Utility.IsExpiredTimeStamp(this._lockExpirationTime);
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x00040FF8 File Offset: 0x0003F1F8
		private bool IsCurrentLockExpired(long currentTimeStampValue)
		{
			return Utility.IsExpiredTimeStamp(currentTimeStampValue, this._lockExpirationTime);
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x00041006 File Offset: 0x0003F206
		internal bool IsCurrentLockNotExpired()
		{
			return !this.IsCurrentLockExpired();
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x00041011 File Offset: 0x0003F211
		internal void SetLockHandle(DataCacheLockHandle lockHandle)
		{
			this._DMLockHandle = lockHandle.Handle;
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x0004101F File Offset: 0x0003F21F
		internal void SetLockTimeOut(TimeSpan timeSpan)
		{
			this._lockExpirationTime = Utility.AddTimeSpanToCurrentCounter(timeSpan);
		}

		// Token: 0x0600155E RID: 5470 RVA: 0x0004102D File Offset: 0x0003F22D
		internal DMLockHandle GetDMLockHandle()
		{
			return this._DMLockHandle;
		}

		// Token: 0x0600155F RID: 5471 RVA: 0x00041035 File Offset: 0x0003F235
		internal void SetLockHandle(DMLockHandle lockHandle)
		{
			this._DMLockHandle = lockHandle;
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06001560 RID: 5472 RVA: 0x0004103E File Offset: 0x0003F23E
		// (set) Token: 0x06001561 RID: 5473 RVA: 0x00041046 File Offset: 0x0003F246
		internal long LockExpirationTime
		{
			get
			{
				return this._lockExpirationTime;
			}
			set
			{
				this._lockExpirationTime = value;
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06001562 RID: 5474 RVA: 0x0004104F File Offset: 0x0003F24F
		// (set) Token: 0x06001563 RID: 5475 RVA: 0x00041057 File Offset: 0x0003F257
		internal CacheItemFlag Flags
		{
			get
			{
				return this._flag;
			}
			set
			{
				this._flag = value;
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06001564 RID: 5476 RVA: 0x00041060 File Offset: 0x0003F260
		public long LastAccess
		{
			get
			{
				return this._lastAccessedTicks;
			}
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x00041068 File Offset: 0x0003F268
		internal void UpdateLastAccessTime()
		{
			this._lastAccessedTicks = DateTime.UtcNow.Ticks;
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x00041088 File Offset: 0x0003F288
		internal void ForcedUnsetLockHandle(DataCacheLockHandle dataCacheLockHandle)
		{
			int num = Math.Abs(dataCacheLockHandle.Handle.LockID);
			int num2 = Math.Abs(this._DMLockHandle.LockID);
			num2 = Math.Max(num, num2);
			this._DMLockHandle.LockID = num2 * -1;
		}

		// Token: 0x04000C61 RID: 3169
		[DataMember]
		private DMLockHandle _DMLockHandle = default(DMLockHandle);

		// Token: 0x04000C62 RID: 3170
		[DataMember]
		private long _lockExpirationTime;

		// Token: 0x04000C63 RID: 3171
		[DataMember]
		private InternalCacheItemVersion _version = default(InternalCacheItemVersion);

		// Token: 0x04000C64 RID: 3172
		[DataMember]
		private long _ttl;

		// Token: 0x04000C65 RID: 3173
		[DataMember]
		private long _lastAccessedTicks = DateTime.UtcNow.Ticks;

		// Token: 0x04000C66 RID: 3174
		[DataMember]
		private long _extensionTimeout;
	}
}
