using System;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200025A RID: 602
	[Serializable]
	internal class BulkOpNotificationEvent : BaseOperationNotification, IBinarySerializable
	{
		// Token: 0x06001454 RID: 5204 RVA: 0x0003FC30 File Offset: 0x0003DE30
		public BulkOpNotificationEvent(string cacheName, CacheEventType type, InternalCacheItemVersion version)
			: base(cacheName, type, version)
		{
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x0003FC3B File Offset: 0x0003DE3B
		public BulkOpNotificationEvent()
		{
		}

		// Token: 0x06001456 RID: 5206 RVA: 0x0003FC44 File Offset: 0x0003DE44
		void IBinarySerializable.WriteStream(ISerializationWriter writer)
		{
			base.WriteStream(writer);
			writer.Write(this.BatchOperation.RegionNames.Count);
			for (int i = 0; i < this.BatchOperation.RegionNames.Count; i++)
			{
				writer.Write(this.BatchOperation.RegionNames[i]);
				int itemCount = this.BatchOperation.GetItemCount(this.BatchOperation.RegionNames[i]);
				writer.Write(itemCount);
				IEnumerator<EvictedElement> enumerator = this.BatchOperation.GetEnumerator(this.BatchOperation.RegionNames[i]);
				int num = 0;
				while (enumerator.MoveNext())
				{
					EvictedElement evictedElement = enumerator.Current;
					writer.Write(evictedElement.Key.ToString());
					evictedElement.Version.WriteStream(writer);
					num++;
				}
			}
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x0003FD20 File Offset: 0x0003DF20
		void IBinarySerializable.ReadStream(ISerializationReader reader)
		{
			this._eventList = new List<DataCacheOperationDescriptor>(65536);
			InternalCacheItemVersion @null = InternalCacheItemVersion.Null;
			base.ReadStream(reader);
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				string text = reader.ReadString();
				int num2 = reader.ReadInt32();
				for (int j = 0; j < num2; j++)
				{
					string text2 = reader.ReadString();
					@null.ReadStream(reader);
					this._eventList.Add(new DataCacheOperationDescriptor(base.CacheName, text, text2, CacheEventType.RemoveEvent, base.InternalVersion));
				}
			}
		}

		// Token: 0x06001458 RID: 5208 RVA: 0x0001D7BC File Offset: 0x0001B9BC
		public object Clone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06001459 RID: 5209 RVA: 0x0003FDAD File Offset: 0x0003DFAD
		// (set) Token: 0x0600145A RID: 5210 RVA: 0x0003FDB5 File Offset: 0x0003DFB5
		public IBatchOperation BatchOperation
		{
			get
			{
				return this._operation;
			}
			set
			{
				this._operation = value;
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x0600145B RID: 5211 RVA: 0x0003FDBE File Offset: 0x0003DFBE
		public List<DataCacheOperationDescriptor> EventList
		{
			get
			{
				return this._eventList;
			}
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x0003FDC8 File Offset: 0x0003DFC8
		internal List<DataCacheOperationDescriptor> GetSimpleEvents()
		{
			List<DataCacheOperationDescriptor> list = new List<DataCacheOperationDescriptor>(this._operation.Count);
			foreach (EvictedElement evictedElement in this._operation)
			{
				DataCacheOperationDescriptor dataCacheOperationDescriptor = new DataCacheOperationDescriptor(base.CacheName, evictedElement.RegionName, evictedElement.Key.StringValue, CacheEventType.RemoveEvent, base.InternalVersion);
				list.Add(dataCacheOperationDescriptor);
			}
			return list;
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x0600145D RID: 5213 RVA: 0x0003FE31 File Offset: 0x0003E031
		internal override int Count
		{
			get
			{
				return this._operation.Count;
			}
		}

		// Token: 0x04000C19 RID: 3097
		private IBatchOperation _operation;

		// Token: 0x04000C1A RID: 3098
		private List<DataCacheOperationDescriptor> _eventList;
	}
}
