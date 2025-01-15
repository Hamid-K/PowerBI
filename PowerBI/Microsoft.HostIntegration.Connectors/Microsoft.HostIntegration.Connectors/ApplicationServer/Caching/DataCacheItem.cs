using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Runtime.Serialization;
using System.Threading;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002E5 RID: 741
	[DataContract(Name = "DataCacheItem", Namespace = "http://schemas.microsoft.com/velocity/namespace")]
	public sealed class DataCacheItem : IBinarySerializable
	{
		// Token: 0x06001BA9 RID: 7081 RVA: 0x000538D7 File Offset: 0x00051AD7
		public DataCacheItem()
		{
		}

		// Token: 0x06001BAA RID: 7082 RVA: 0x000538E8 File Offset: 0x00051AE8
		internal DataCacheItem(Key key, object value, ValueFlagsVersion flagsVer, InternalCacheItemVersion ver, object[] tags, string regionName, string cacheName, TimeSpan timeout, TimeSpan extensionTimeout)
		{
			this._keyObject = key;
			this._serializedValue = (byte[][])value;
			this._serializationVersion = flagsVer;
			this._version = ver;
			this._tags = (DataCacheTag[])tags;
			this._regionName = regionName;
			this._cacheName = cacheName;
			this._timeout = timeout;
			this._extensionTimeout = extensionTimeout;
			if (this._timeout < TimeSpan.Zero)
			{
				this._timeout = TimeSpan.Zero;
			}
			if (extensionTimeout < TimeSpan.Zero)
			{
				this._extensionTimeout = TimeSpan.Zero;
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06001BAB RID: 7083 RVA: 0x00053987 File Offset: 0x00051B87
		// (set) Token: 0x06001BAC RID: 7084 RVA: 0x0005398F File Offset: 0x00051B8F
		internal Key KeyObject
		{
			get
			{
				return this._keyObject;
			}
			set
			{
				this._keyObject = value;
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06001BAD RID: 7085 RVA: 0x00053998 File Offset: 0x00051B98
		internal byte[][] SerializedValue
		{
			get
			{
				return this._serializedValue;
			}
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06001BAE RID: 7086 RVA: 0x000539A0 File Offset: 0x00051BA0
		public string Key
		{
			get
			{
				return this._keyObject.StringValue;
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06001BAF RID: 7087 RVA: 0x000539AD File Offset: 0x00051BAD
		internal bool IsValueDeserialized
		{
			get
			{
				return this._value != null;
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06001BB0 RID: 7088 RVA: 0x000539BC File Offset: 0x00051BBC
		public object Value
		{
			get
			{
				if (this._value == null)
				{
					DataCacheObjectSerializationProvider dataCacheSerializationProvider = DataCacheItemFactory.GetDataCacheSerializationProvider(this.CacheName);
					this._value = dataCacheSerializationProvider.DeserializeUserObject(this._serializedValue, this._serializationVersion);
				}
				return this._value;
			}
		}

		// Token: 0x06001BB1 RID: 7089 RVA: 0x000539FB File Offset: 0x00051BFB
		internal void NormalizeValue(ValueFlagsVersion version)
		{
			this._serializedValue = ValueFlagsUtility.NormalizeUserData(this._serializedValue, this._serializationVersion, version);
			this._serializationVersion = version;
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06001BB2 RID: 7090 RVA: 0x00053A1C File Offset: 0x00051C1C
		public ReadOnlyCollection<DataCacheTag> Tags
		{
			get
			{
				if (this._tags == null)
				{
					return new ReadOnlyCollection<DataCacheTag>(new List<DataCacheTag>());
				}
				return new ReadOnlyCollection<DataCacheTag>(new List<DataCacheTag>(this._tags));
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06001BB3 RID: 7091 RVA: 0x00053A41 File Offset: 0x00051C41
		public DataCacheItemVersion Version
		{
			get
			{
				if (this._userCacheItemVersion == null)
				{
					Interlocked.CompareExchange<DataCacheItemVersion>(ref this._userCacheItemVersion, new DataCacheItemVersion(this._version), null);
				}
				return this._userCacheItemVersion;
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06001BB4 RID: 7092 RVA: 0x00053A6F File Offset: 0x00051C6F
		// (set) Token: 0x06001BB5 RID: 7093 RVA: 0x00053A77 File Offset: 0x00051C77
		public string RegionName
		{
			get
			{
				return this._regionName;
			}
			internal set
			{
				this._regionName = value;
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06001BB6 RID: 7094 RVA: 0x00053A80 File Offset: 0x00051C80
		// (set) Token: 0x06001BB7 RID: 7095 RVA: 0x00053A88 File Offset: 0x00051C88
		public string CacheName
		{
			get
			{
				return this._cacheName;
			}
			internal set
			{
				this._cacheName = value;
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06001BB8 RID: 7096 RVA: 0x00053A91 File Offset: 0x00051C91
		public TimeSpan Timeout
		{
			get
			{
				return this._timeout;
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06001BB9 RID: 7097 RVA: 0x00053A99 File Offset: 0x00051C99
		public TimeSpan ExtensionTimeout
		{
			get
			{
				return this._extensionTimeout;
			}
		}

		// Token: 0x06001BBA RID: 7098 RVA: 0x00053AA4 File Offset: 0x00051CA4
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}:{1}:{2}", new object[] { this.CacheName, this.RegionName, this.Key });
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06001BBB RID: 7099 RVA: 0x00053AE4 File Offset: 0x00051CE4
		public int Size
		{
			get
			{
				int num = 0;
				num += ((this.Key != null) ? this.Key.Length : 0);
				num += Utility.Get2DByteArraySize(this.SerializedValue);
				num += (string.IsNullOrEmpty(this.CacheName) ? 0 : this.CacheName.Length);
				num += (string.IsNullOrEmpty(this.RegionName) ? 0 : this.RegionName.Length);
				if (this.Tags != null)
				{
					foreach (DataCacheTag dataCacheTag in this.Tags)
					{
						num += dataCacheTag.Length;
					}
				}
				num += ((null != this.Version) ? this.Version.Size : 0);
				return num;
			}
		}

		// Token: 0x06001BBC RID: 7100 RVA: 0x00053BC0 File Offset: 0x00051DC0
		[OnDeserializing]
		private void SetDefaultSerializationVersion(StreamingContext c)
		{
			this._serializationVersion = ValueFlagsVersion.EitherType;
		}

		// Token: 0x06001BBD RID: 7101 RVA: 0x00053BCC File Offset: 0x00051DCC
		void IBinarySerializable.ReadStream(ISerializationReader reader)
		{
			uint num = reader.ReadUInt32();
			if ((num & 4U) == 0U)
			{
				this._keyObject = new Key();
				((IBinarySerializable)this._keyObject).ReadStream(reader);
			}
			int num2 = reader.ReadInt32();
			if (num2 > 0)
			{
				using (ChunkStream chunkStream = new ChunkStream(reader, num2))
				{
					this._serializedValue = chunkStream.ToChunkedArray();
					this._serializationVersion = ValueFlagsVersion.EitherType;
				}
			}
			this._version.ReadStream(reader);
			int num3 = reader.ReadInt32();
			if (num3 > 0)
			{
				this._tags = new DataCacheTag[num3];
				for (int i = 0; i < num3; i++)
				{
					this._tags[i] = new DataCacheTag(reader.ReadString(), false);
				}
			}
			if ((num & 1U) == 0U)
			{
				this._cacheName = reader.ReadString();
			}
			if ((num & 2U) == 0U)
			{
				this._regionName = reader.ReadString();
			}
			this._timeout = new TimeSpan(reader.ReadInt64());
			this._extensionTimeout = new TimeSpan(reader.ReadInt64());
		}

		// Token: 0x06001BBE RID: 7102 RVA: 0x00053CCC File Offset: 0x00051ECC
		void IBinarySerializable.WriteStream(ISerializationWriter writer)
		{
			uint nullableFlagsList = this.GetNullableFlagsList();
			writer.Write(nullableFlagsList);
			if (this._keyObject != null)
			{
				((IBinarySerializable)this._keyObject).WriteStream(writer);
			}
			if (this._serializedValue != null)
			{
				writer.Write(this._serializedValue);
			}
			else
			{
				writer.Write(0);
			}
			this._version.WriteStream(writer);
			if (this._tags != null)
			{
				writer.Write(this._tags.Length);
				for (int i = 0; i < this._tags.Length; i++)
				{
					writer.Write(this._tags[i].ToString());
				}
			}
			else
			{
				writer.Write(0);
			}
			writer.Write(this._cacheName);
			writer.Write(this._regionName);
			writer.Write(this._timeout.Ticks);
			writer.Write(this._extensionTimeout.Ticks);
		}

		// Token: 0x06001BBF RID: 7103 RVA: 0x00053DA4 File Offset: 0x00051FA4
		private uint GetNullableFlagsList()
		{
			uint num = 0U;
			if (this.CacheName == null)
			{
				num |= 1U;
			}
			if (this.RegionName == null)
			{
				num |= 2U;
			}
			if (this.Key == null)
			{
				num |= 4U;
			}
			return num;
		}

		// Token: 0x04000EAF RID: 3759
		[DataMember]
		internal Key _keyObject;

		// Token: 0x04000EB0 RID: 3760
		[DataMember]
		internal byte[][] _serializedValue;

		// Token: 0x04000EB1 RID: 3761
		[DataMember]
		internal InternalCacheItemVersion _version;

		// Token: 0x04000EB2 RID: 3762
		[DataMember]
		internal DataCacheTag[] _tags;

		// Token: 0x04000EB3 RID: 3763
		[DataMember]
		internal string _regionName;

		// Token: 0x04000EB4 RID: 3764
		[DataMember]
		internal string _cacheName;

		// Token: 0x04000EB5 RID: 3765
		[DataMember]
		private TimeSpan _timeout;

		// Token: 0x04000EB6 RID: 3766
		[DataMember]
		private TimeSpan _extensionTimeout;

		// Token: 0x04000EB7 RID: 3767
		private object _value;

		// Token: 0x04000EB8 RID: 3768
		private DataCacheItemVersion _userCacheItemVersion;

		// Token: 0x04000EB9 RID: 3769
		private ValueFlagsVersion _serializationVersion = ValueFlagsVersion.EitherType;

		// Token: 0x020002E6 RID: 742
		private static class NullableBitList
		{
			// Token: 0x04000EBA RID: 3770
			internal const int CacheName = 1;

			// Token: 0x04000EBB RID: 3771
			internal const int RegioName = 2;

			// Token: 0x04000EBC RID: 3772
			internal const int Key = 4;
		}
	}
}
