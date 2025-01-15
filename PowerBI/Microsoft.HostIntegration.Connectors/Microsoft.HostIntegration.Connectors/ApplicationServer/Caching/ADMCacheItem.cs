using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001ED RID: 493
	[DataContract(Name = "DMCacheItem", Namespace = "http://schemas.microsoft.com/velocity/namespace")]
	internal class ADMCacheItem : AMDHObjectNode, IEvictionCandidate
	{
		// Token: 0x06000FFD RID: 4093 RVA: 0x00036214 File Offset: 0x00034414
		public ADMCacheItem(InternalCacheItemVersion version, long ttl, long extnTimeout)
		{
			this.Version = version;
			this._ttl = ttl;
			this._extensionTimeout = extnTimeout;
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x00036268 File Offset: 0x00034468
		public ADMCacheItem(InternalCacheItemVersion version)
		{
			this.Version = version;
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x000362B0 File Offset: 0x000344B0
		public ADMCacheItem()
		{
			this.Version = default(InternalCacheItemVersion);
			this._ttl = long.MaxValue;
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x0003630C File Offset: 0x0003450C
		public ADMCacheItem(ADMCacheItem item)
			: base(item)
		{
			this._DMLockHandle = item._DMLockHandle;
			this._lockExpirationTime = item._lockExpirationTime;
			this._version = item._version;
			this._ttl = item._ttl;
			this._lastAccessedTicks = item._lastAccessedTicks;
			this._extensionTimeout = item._extensionTimeout;
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x00003CAB File Offset: 0x00001EAB
		public virtual void Init(Key key, object value, long TTL, long extnTimeout, object[] tags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x00003CAB File Offset: 0x00001EAB
		public virtual void Reinit(Key key, object value, long TTL, long extnTimeout, object[] tags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06001003 RID: 4099 RVA: 0x00036393 File Offset: 0x00034593
		// (set) Token: 0x06001004 RID: 4100 RVA: 0x0003639B File Offset: 0x0003459B
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

		// Token: 0x06001005 RID: 4101 RVA: 0x000363A4 File Offset: 0x000345A4
		public DataCacheLockHandle GetLockHandle()
		{
			return new DataCacheLockHandle(this._DMLockHandle, this.Version);
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06001006 RID: 4102 RVA: 0x000363B7 File Offset: 0x000345B7
		// (set) Token: 0x06001007 RID: 4103 RVA: 0x000363BF File Offset: 0x000345BF
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
					EventLogWriter.WriteVerbose<long, ADMCacheItem, long>("DistributedCache.DataManager", "Assigning ttl = {0} to {1} at time = {2}.", value, this, Stopwatch.GetTimestamp());
				}
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06001008 RID: 4104 RVA: 0x000363E6 File Offset: 0x000345E6
		// (set) Token: 0x06001009 RID: 4105 RVA: 0x000363EE File Offset: 0x000345EE
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
					EventLogWriter.WriteVerbose<long, ADMCacheItem, long>("DistributedCache.DataManager", "Assigning extensionTimeout = {0} to {1} at time = {2}.", value, this, Stopwatch.GetTimestamp());
				}
			}
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x00036415 File Offset: 0x00034615
		public bool IsExpired()
		{
			return this.IsExpired(Stopwatch.GetTimestamp());
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x0600100B RID: 4107 RVA: 0x00036422 File Offset: 0x00034622
		// (set) Token: 0x0600100C RID: 4108 RVA: 0x0003642F File Offset: 0x0003462F
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

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x0600100D RID: 4109 RVA: 0x0003643D File Offset: 0x0003463D
		// (set) Token: 0x0600100E RID: 4110 RVA: 0x0003644A File Offset: 0x0003464A
		public bool IsRtLockPlaceHolderObject
		{
			get
			{
				return this._flag.IsRtLockPlaceHolderObject;
			}
			set
			{
				this._flag.IsRtLockPlaceHolderObject = value;
			}
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x00036458 File Offset: 0x00034658
		public bool IsExpired(long currentTimeStampValue)
		{
			return Utility.IsExpiredTimeStamp(currentTimeStampValue, this.TimeToLive) && this.IsLockInvalidOrExpired(currentTimeStampValue);
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x00036471 File Offset: 0x00034671
		public void UpdateTTL()
		{
			this.TimeToLive = Utility.AddTimeSpanToCurrentCounter(this._extensionTimeout);
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x00036484 File Offset: 0x00034684
		public void UpdateTTL(long currentTimeStampValue)
		{
			this.TimeToLive = Utility.AddTimeSpanToCurrentCounter(currentTimeStampValue, this._extensionTimeout);
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x00036498 File Offset: 0x00034698
		private bool IsLockInvalidOrExpired(long currentTimeStampValue)
		{
			return !this.IsLocked() || this.IsCurrentLockExpired(currentTimeStampValue);
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x000364AC File Offset: 0x000346AC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(" DMCacheItem => [");
			stringBuilder.Append(" Version = ");
			stringBuilder.Append(this.Version);
			stringBuilder.Append(" LockID = ");
			stringBuilder.Append(this._DMLockHandle.LockID);
			stringBuilder.Append(" IsCommitted = ");
			stringBuilder.Append(base.IsCommitted ? "T" : "F");
			stringBuilder.Append(" Flags = ");
			stringBuilder.Append(this._flag);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x0003655E File Offset: 0x0003475E
		public void UpdateLastAccessTime(DateTime accessTime)
		{
			this._lastAccessedTicks = accessTime.Ticks;
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06001015 RID: 4117 RVA: 0x0003656D File Offset: 0x0003476D
		// (set) Token: 0x06001016 RID: 4118 RVA: 0x00036575 File Offset: 0x00034775
		public long LastAccess
		{
			get
			{
				return this._lastAccessedTicks;
			}
			set
			{
				this._lastAccessedTicks = value;
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06001017 RID: 4119 RVA: 0x00008948 File Offset: 0x00006B48
		public object GetItemToEvict
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06001018 RID: 4120 RVA: 0x0003657E File Offset: 0x0003477E
		public bool ImmediateCleanup
		{
			get
			{
				return this.IsExpired();
			}
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x00036588 File Offset: 0x00034788
		internal bool IsLockHandleEqual(DataCacheLockHandle handle)
		{
			return handle.Handle.LockID == this._DMLockHandle.LockID && this.Version.CompareTo(handle.Version) == 0;
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x000365CC File Offset: 0x000347CC
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

		// Token: 0x0600101B RID: 4123 RVA: 0x0003660F File Offset: 0x0003480F
		internal void SetReadThroughLock(TimeSpan timeSpan)
		{
			this._lockExpirationTime = Utility.AddTimeSpanToCurrentCounter(timeSpan);
			int lockID = this._DMLockHandle.LockID;
			this._DMLockHandle.LockID = ADMCacheItem.GetUniqueLockid();
			this.IsRtLockPlaceHolderObject = true;
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x00036640 File Offset: 0x00034840
		private static int GetUniqueLockid()
		{
			int num = Interlocked.Increment(ref ADMCacheItem._uniqueLockId);
			if (num < 0)
			{
				lock (ADMCacheItem._uniqueLockIdLock)
				{
					if (ADMCacheItem._uniqueLockId < 0)
					{
						Interlocked.Exchange(ref ADMCacheItem._uniqueLockId, 0);
					}
					num = Interlocked.Increment(ref ADMCacheItem._uniqueLockId);
				}
			}
			return num;
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x000366A8 File Offset: 0x000348A8
		internal void ClearLock()
		{
			this._DMLockHandle.LockID = this._DMLockHandle.LockID * -1;
			if (this._DMLockHandle.LockID == -2147483648)
			{
				this._DMLockHandle.LockID = 0;
			}
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x000366E0 File Offset: 0x000348E0
		internal bool IsLocked()
		{
			return this._DMLockHandle.LockID > 0;
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x000366F0 File Offset: 0x000348F0
		internal bool IsCurrentLockExpired()
		{
			return Utility.IsExpiredTimeStamp(this._lockExpirationTime);
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x000366FD File Offset: 0x000348FD
		private bool IsCurrentLockExpired(long currentTimeStampValue)
		{
			return Utility.IsExpiredTimeStamp(currentTimeStampValue, this._lockExpirationTime);
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x0003670B File Offset: 0x0003490B
		internal bool IsCurrentLockNotExpired()
		{
			return !this.IsCurrentLockExpired();
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x00036716 File Offset: 0x00034916
		internal void SetLockHandle(DataCacheLockHandle lockHandle)
		{
			this._DMLockHandle = lockHandle.Handle;
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x00036724 File Offset: 0x00034924
		internal void SetLockTimeOut(TimeSpan timeSpan)
		{
			this._lockExpirationTime = Utility.AddTimeSpanToCurrentCounter(timeSpan);
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x00036732 File Offset: 0x00034932
		internal DMLockHandle GetDMLockHandle()
		{
			return this._DMLockHandle;
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x0003673A File Offset: 0x0003493A
		internal void SetLockHandle(DMLockHandle lockHandle)
		{
			this._DMLockHandle = lockHandle;
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06001026 RID: 4134 RVA: 0x00036743 File Offset: 0x00034943
		// (set) Token: 0x06001027 RID: 4135 RVA: 0x0003674B File Offset: 0x0003494B
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

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06001028 RID: 4136 RVA: 0x00036754 File Offset: 0x00034954
		// (set) Token: 0x06001029 RID: 4137 RVA: 0x0003675C File Offset: 0x0003495C
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

		// Token: 0x0600102A RID: 4138 RVA: 0x00036765 File Offset: 0x00034965
		internal bool TakeCommitLock()
		{
			if (this._flag.IsCommitLocked)
			{
				return false;
			}
			this._flag.IsCommitLocked = true;
			return true;
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x00036783 File Offset: 0x00034983
		internal void ReleaseCommitLock()
		{
			this._flag.IsCommitLocked = false;
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x00036794 File Offset: 0x00034994
		internal void UpdateLastAccessTime()
		{
			this._lastAccessedTicks = DateTime.UtcNow.Ticks;
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x000367B4 File Offset: 0x000349B4
		internal void ForcedUnsetLockHandle(DataCacheLockHandle dataCacheLockHandle)
		{
			int num = Math.Abs(dataCacheLockHandle.Handle.LockID);
			int num2 = Math.Abs(this._DMLockHandle.LockID);
			num2 = Math.Max(num, num2);
			this._DMLockHandle.LockID = num2 * -1;
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x000367FC File Offset: 0x000349FC
		protected new void Clean()
		{
			this._DMLockHandle = default(DMLockHandle);
			this._lastAccessedTicks = 0L;
			this._lockExpirationTime = 0L;
			this._ttl = 0L;
			this._extensionTimeout = 0L;
			this._version = default(InternalCacheItemVersion);
			base.Clean();
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x0003683C File Offset: 0x00034A3C
		protected new void Init()
		{
			base.Init();
		}

		// Token: 0x04000AAB RID: 2731
		[DataMember]
		private DMLockHandle _DMLockHandle = default(DMLockHandle);

		// Token: 0x04000AAC RID: 2732
		[DataMember]
		private long _lockExpirationTime;

		// Token: 0x04000AAD RID: 2733
		[DataMember]
		private InternalCacheItemVersion _version = default(InternalCacheItemVersion);

		// Token: 0x04000AAE RID: 2734
		[DataMember]
		private long _ttl;

		// Token: 0x04000AAF RID: 2735
		[DataMember]
		private long _lastAccessedTicks = DateTime.UtcNow.Ticks;

		// Token: 0x04000AB0 RID: 2736
		[DataMember]
		private long _extensionTimeout;

		// Token: 0x04000AB1 RID: 2737
		private static int _uniqueLockId;

		// Token: 0x04000AB2 RID: 2738
		private static object _uniqueLockIdLock = new object();
	}
}
