using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Threading;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000258 RID: 600
	[DataContract(Name = "BaseOperationNotification", Namespace = "http://schemas.microsoft.com/velocity/namespace")]
	public class BaseOperationNotification
	{
		// Token: 0x06001443 RID: 5187 RVA: 0x0003F9D1 File Offset: 0x0003DBD1
		internal BaseOperationNotification(string cacheName, CacheEventType opType, InternalCacheItemVersion version)
		{
			this._cacheName = cacheName;
			this._operationType = (int)opType;
			this._internalVersion = version;
			this._version = new DataCacheItemVersion(version);
		}

		// Token: 0x06001444 RID: 5188 RVA: 0x0003F9FA File Offset: 0x0003DBFA
		public BaseOperationNotification(string cacheName, DataCacheOperations opType, DataCacheItemVersion version)
		{
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			this._cacheName = cacheName;
			this._operationType = (int)opType;
			this._internalVersion = version.InternalVersion;
			this._version = version;
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x00002061 File Offset: 0x00000261
		internal BaseOperationNotification()
		{
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x0003FA38 File Offset: 0x0003DC38
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "{0}:{1}:{2}", new object[] { this._cacheName, this.InternalOperationType, this._internalVersion });
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06001447 RID: 5191 RVA: 0x0003FA81 File Offset: 0x0003DC81
		public string CacheName
		{
			get
			{
				return this._cacheName;
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06001448 RID: 5192 RVA: 0x0003FA89 File Offset: 0x0003DC89
		public DataCacheOperations OperationType
		{
			get
			{
				return (DataCacheOperations)this._operationType;
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06001449 RID: 5193 RVA: 0x0003FA91 File Offset: 0x0003DC91
		public DataCacheItemVersion Version
		{
			get
			{
				if (this._version == null)
				{
					Interlocked.CompareExchange<DataCacheItemVersion>(ref this._version, new DataCacheItemVersion(this._internalVersion), null);
				}
				return this._version;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x0600144A RID: 5194 RVA: 0x0003FA89 File Offset: 0x0003DC89
		internal CacheEventType InternalOperationType
		{
			get
			{
				return (CacheEventType)this._operationType;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x0600144B RID: 5195 RVA: 0x0003FABF File Offset: 0x0003DCBF
		internal InternalCacheItemVersion InternalVersion
		{
			get
			{
				return this._internalVersion;
			}
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x0003FAC7 File Offset: 0x0003DCC7
		internal virtual void ReadStreamNoCacheName(string cacheName, ISerializationReader reader)
		{
			this._cacheName = cacheName;
			this._operationType = reader.ReadInt32();
			this._internalVersion.ReadStream(reader);
			this._version = new DataCacheItemVersion(this._internalVersion);
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x0003FAF9 File Offset: 0x0003DCF9
		internal virtual void WriteStreamNoCacheName(ISerializationWriter writer)
		{
			writer.Write(this._operationType);
			this._internalVersion.WriteStream(writer);
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x0600144E RID: 5198 RVA: 0x00002B16 File Offset: 0x00000D16
		internal virtual int Count
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x0003FB14 File Offset: 0x0003DD14
		internal static CacheEventType GetEventType(ReqType reqType, bool newElement)
		{
			if (reqType <= ReqType.COMPACT_PARTITION)
			{
				switch (reqType)
				{
				case ReqType.ADD:
					return CacheEventType.AddEvent;
				case ReqType.PUT:
					if (newElement)
					{
						return CacheEventType.AddEvent;
					}
					return CacheEventType.ReplaceEvent;
				case ReqType.GET:
				case ReqType.GET_CACHE_ITEM:
				case ReqType.GET_ALL:
				case ReqType.GET_AND_LOCK:
				case ReqType.GET_NEXT_BATCH:
				case ReqType.GET_IF_NEWER:
				case ReqType.BULK_GET:
					break;
				case ReqType.RESET_TIMEOUT:
					return CacheEventType.ResetTimeEvent;
				case ReqType.PUT_AND_UNLOCK:
					if (newElement)
					{
						return CacheEventType.AddEvent;
					}
					return CacheEventType.ReplaceEvent;
				case ReqType.UNLOCK:
					return CacheEventType.UnLockEvent;
				case ReqType.REMOVE:
				case ReqType.LOCKED_REMOVE:
					return CacheEventType.RemoveEvent;
				case ReqType.CREATE_REGION:
					return CacheEventType.CreateRegionEvent;
				case ReqType.REMOVE_REGION:
					return CacheEventType.RemoveRegionEvent;
				case ReqType.CLEAR_REGION:
					return CacheEventType.ClearRegionEvent;
				default:
					switch (reqType)
					{
					case ReqType.EVICT_BATCH:
						return CacheEventType.BulkEvictionEvent;
					case ReqType.COMPACT_PARTITION:
						return CacheEventType.CompactPartitionEvent;
					}
					break;
				}
			}
			else
			{
				if (reqType == ReqType.WRITE_BEHIND_CHECKPOINT)
				{
					return CacheEventType.OpFailedEvent;
				}
				if (reqType == ReqType.PARTITION_CLEAR_CACHE)
				{
					return CacheEventType.PartitionClearCache;
				}
				if (reqType == ReqType.REPLACE)
				{
					return CacheEventType.ReplaceEvent;
				}
			}
			return CacheEventType.OpFailedEvent;
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x0003FBD3 File Offset: 0x0003DDD3
		internal void ReadStream(ISerializationReader reader)
		{
			this._cacheName = reader.ReadString();
			this._operationType = reader.ReadInt32();
			this._internalVersion.ReadStream(reader);
			this._version = new DataCacheItemVersion(this._internalVersion);
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x0003FC0A File Offset: 0x0003DE0A
		internal void WriteStream(ISerializationWriter writer)
		{
			writer.Write(this._cacheName);
			writer.Write(this._operationType);
			this._internalVersion.WriteStream(writer);
		}

		// Token: 0x04000C14 RID: 3092
		internal const int MinLengthVwFormat = 20;

		// Token: 0x04000C15 RID: 3093
		[DataMember]
		private string _cacheName;

		// Token: 0x04000C16 RID: 3094
		[DataMember]
		private int _operationType;

		// Token: 0x04000C17 RID: 3095
		[DataMember]
		private InternalCacheItemVersion _internalVersion;

		// Token: 0x04000C18 RID: 3096
		private DataCacheItemVersion _version;
	}
}
