using System;
using System.Globalization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000269 RID: 617
	[Serializable]
	public class DataCacheOperationDescriptor : BaseOperationNotification, IBinarySerializable
	{
		// Token: 0x06001491 RID: 5265 RVA: 0x000404F0 File Offset: 0x0003E6F0
		internal DataCacheOperationDescriptor(string cacheName, string regionName, string key, CacheEventType opType, InternalCacheItemVersion version)
			: base(cacheName, opType, version)
		{
			this._regionName = regionName;
			this._key = key;
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x0004050B File Offset: 0x0003E70B
		public DataCacheOperationDescriptor(string cacheName, string regionName, string key, DataCacheOperations opType, DataCacheItemVersion version)
			: base(cacheName, opType, version)
		{
			this._regionName = regionName;
			this._key = key;
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x0003FC3B File Offset: 0x0003DE3B
		internal DataCacheOperationDescriptor()
		{
		}

		// Token: 0x06001494 RID: 5268 RVA: 0x00040528 File Offset: 0x0003E728
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "{0}:{1}:{2}:{3}:{4}", new object[] { base.CacheName, base.InternalOperationType, this._regionName, this._key, base.InternalVersion });
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06001495 RID: 5269 RVA: 0x00040583 File Offset: 0x0003E783
		public string RegionName
		{
			get
			{
				return this._regionName;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06001496 RID: 5270 RVA: 0x0004058B File Offset: 0x0003E78B
		public string Key
		{
			get
			{
				return this._key;
			}
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x00040593 File Offset: 0x0003E793
		void IBinarySerializable.WriteStream(ISerializationWriter writer)
		{
			base.WriteStream(writer);
			this.WriteAddedFields(writer);
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x000405A3 File Offset: 0x0003E7A3
		void IBinarySerializable.ReadStream(ISerializationReader reader)
		{
			base.ReadStream(reader);
			this.ReadAddedFields(reader);
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x000405B3 File Offset: 0x0003E7B3
		internal override void WriteStreamNoCacheName(ISerializationWriter writer)
		{
			base.WriteStreamNoCacheName(writer);
			this.WriteAddedFields(writer);
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x000405C4 File Offset: 0x0003E7C4
		private void WriteAddedFields(ISerializationWriter writer)
		{
			bool flag = true;
			bool flag2 = true;
			if (this._regionName == null)
			{
				flag = false;
			}
			writer.Write(flag);
			if (this._key == null)
			{
				flag2 = false;
			}
			writer.Write(flag2);
			if (flag)
			{
				writer.Write(this._regionName);
			}
			if (flag2)
			{
				writer.Write(this._key);
			}
		}

		// Token: 0x0600149B RID: 5275 RVA: 0x00040615 File Offset: 0x0003E815
		internal override void ReadStreamNoCacheName(string cacheName, ISerializationReader reader)
		{
			base.ReadStreamNoCacheName(cacheName, reader);
			this.ReadAddedFields(reader);
		}

		// Token: 0x0600149C RID: 5276 RVA: 0x00040628 File Offset: 0x0003E828
		private void ReadAddedFields(ISerializationReader reader)
		{
			bool flag = reader.ReadBoolean();
			bool flag2 = reader.ReadBoolean();
			if (flag)
			{
				this._regionName = reader.ReadString();
			}
			if (flag2)
			{
				this._key = reader.ReadString();
			}
		}

		// Token: 0x04000C50 RID: 3152
		private string _regionName;

		// Token: 0x04000C51 RID: 3153
		private string _key;
	}
}
