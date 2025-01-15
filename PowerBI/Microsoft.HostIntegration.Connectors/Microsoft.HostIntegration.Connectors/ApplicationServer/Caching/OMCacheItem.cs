using System;
using System.Runtime.Serialization;
using System.Text;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000276 RID: 630
	[DataContract(Name = "OMCacheItem", Namespace = "http://schemas.microsoft.com/velocity/namespace")]
	internal sealed class OMCacheItem : DMCacheItem
	{
		// Token: 0x06001567 RID: 5479 RVA: 0x000410D0 File Offset: 0x0003F2D0
		public OMCacheItem(AOMCacheItem item)
			: base(item)
		{
			this._region = new NetworkOmRegion(item.RegionName, item.CacheName);
			this._size = item.Size;
			Key key = null;
			item.GetKeyValueTag(ref key, ref this._value, ref this._tags);
			base.Key = key;
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06001568 RID: 5480 RVA: 0x00041124 File Offset: 0x0003F324
		public object[] Tags
		{
			get
			{
				return this._tags;
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06001569 RID: 5481 RVA: 0x0004112C File Offset: 0x0003F32C
		// (set) Token: 0x0600156A RID: 5482 RVA: 0x00041134 File Offset: 0x0003F334
		public int Size
		{
			get
			{
				return this._size;
			}
			internal set
			{
				this._size = value;
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x0600156B RID: 5483 RVA: 0x0004113D File Offset: 0x0003F33D
		public object Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x0600156C RID: 5484 RVA: 0x00041145 File Offset: 0x0003F345
		// (set) Token: 0x0600156D RID: 5485 RVA: 0x0004114D File Offset: 0x0003F34D
		public NetworkOmRegion Region
		{
			get
			{
				return this._region;
			}
			set
			{
				this._region = value;
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x0600156E RID: 5486 RVA: 0x00041156 File Offset: 0x0003F356
		public string RegionName
		{
			get
			{
				if (this._region != null)
				{
					return this._region.RegionName;
				}
				return null;
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x0600156F RID: 5487 RVA: 0x0004116D File Offset: 0x0003F36D
		public string CacheName
		{
			get
			{
				if (this._region != null)
				{
					return this._region.CacheName;
				}
				return null;
			}
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x00041184 File Offset: 0x0003F384
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("OMCacheItem = [Base(" + base.ToString());
			stringBuilder.Append(") ");
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
			if (base.Key != null)
			{
				stringBuilder.Append(":Key = ");
				stringBuilder.Append(base.Key.ToString());
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

		// Token: 0x04000C67 RID: 3175
		[DataMember]
		private NetworkOmRegion _region;

		// Token: 0x04000C68 RID: 3176
		[DataMember]
		private object _value;

		// Token: 0x04000C69 RID: 3177
		[DataMember]
		private DataCacheTag[] _tags;

		// Token: 0x04000C6A RID: 3178
		[DataMember]
		private int _size;
	}
}
