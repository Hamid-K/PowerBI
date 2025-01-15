using System;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000280 RID: 640
	[DataContract(Name = "ObjectCacheItem", Namespace = "http://schemas.microsoft.com/velocity/namespace")]
	internal class ObjectCacheItem : AOMCacheItem
	{
		// Token: 0x0600162C RID: 5676 RVA: 0x00044426 File Offset: 0x00042626
		public ObjectCacheItem()
		{
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x0004442E File Offset: 0x0004262E
		public ObjectCacheItem(Key key)
			: this(null, key)
		{
		}

		// Token: 0x0600162E RID: 5678 RVA: 0x00044438 File Offset: 0x00042638
		public ObjectCacheItem(IOMRegion region, Key key)
		{
			this._region = region;
			this._key = key;
			this._size += ObjectCacheItem.GetKeySize(key);
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x00044464 File Offset: 0x00042664
		public ObjectCacheItem(IOMRegion region, Key key, object value, long TTL, long extnTimeout, object[] tags)
			: base(InternalCacheItemVersion.Null, TTL, extnTimeout)
		{
			this._region = region;
			this._key = key;
			this._size += ObjectCacheItem.GetKeySize(key);
			this.SetValue(value, false);
			this.SetTags(tags, false);
		}

		// Token: 0x06001630 RID: 5680 RVA: 0x000444B4 File Offset: 0x000426B4
		public ObjectCacheItem(IOMRegion region, Key key, object value, long TTL, long extnTimeout, object[] tags, ObjectType oType)
			: base(InternalCacheItemVersion.Null, TTL, extnTimeout)
		{
			this._region = region;
			this._key = key;
			if (oType == ObjectType.CLRObjectRefType)
			{
				this._size = 1;
				this.SetValue(value, true);
				this.SetTags(tags, true);
				return;
			}
			this._size = 0;
			this._size += 16 * IntPtr.Size;
			this._size += ObjectCacheItem.GetKeySize(key);
			this.SetValue(value, false);
			this.SetTags(tags, false);
		}

		// Token: 0x06001631 RID: 5681 RVA: 0x0004453B File Offset: 0x0004273B
		private static int GetKeySize(Key key)
		{
			return key.Length * 2 + 4 * IntPtr.Size;
		}

		// Token: 0x06001632 RID: 5682 RVA: 0x00044550 File Offset: 0x00042750
		public ObjectCacheItem(AOMCacheItem item)
			: base(item)
		{
			ObjectCacheItem objectCacheItem = (ObjectCacheItem)item;
			this._key = objectCacheItem._key;
			this._region = objectCacheItem._region;
			this._value = objectCacheItem._value;
			this._tags = objectCacheItem._tags;
			this._size = objectCacheItem._size;
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x000445A7 File Offset: 0x000427A7
		public override void GetKeyValueTag(ref Key key, ref object value, ref DataCacheTag[] tags)
		{
			key = this._key;
			value = this._value;
			tags = this._tags;
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x000445C1 File Offset: 0x000427C1
		public override void Init(Key key, object value, long TTL, long extnTimeout, object[] tags)
		{
			key = this.Key;
			this.SetValue(value, false);
			this.SetTags(tags, false);
			base.TimeToLive = TTL;
			base.ExtensionTimeout = extnTimeout;
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06001635 RID: 5685 RVA: 0x000445EB File Offset: 0x000427EB
		public override object[] Tags
		{
			get
			{
				return this._tags;
			}
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x000445F4 File Offset: 0x000427F4
		internal void SetTags(object[] value, bool isCLRReference)
		{
			if (this._tags != null && !isCLRReference)
			{
				for (int i = 0; i < this._tags.Length; i++)
				{
					if (this._tags[i] != null)
					{
						this._size -= this._tags[i].Length;
					}
				}
			}
			this._tags = (DataCacheTag[])value;
			if (this._tags != null && !isCLRReference)
			{
				for (int j = 0; j < this._tags.Length; j++)
				{
					if (this._tags[j] != null)
					{
						this._size += this._tags[j].Length;
					}
				}
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06001637 RID: 5687 RVA: 0x00044691 File Offset: 0x00042891
		// (set) Token: 0x06001638 RID: 5688 RVA: 0x00044699 File Offset: 0x00042899
		public override int Size
		{
			get
			{
				return this._size;
			}
			set
			{
				this._size = value;
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06001639 RID: 5689 RVA: 0x000446A2 File Offset: 0x000428A2
		public override object Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x0600163A RID: 5690 RVA: 0x000446AC File Offset: 0x000428AC
		internal override int ValueSize
		{
			get
			{
				int num = 0;
				byte[][] array = this._value as byte[][];
				if (array != null)
				{
					for (int i = 0; i < array.Length; i++)
					{
						num += array[i].Length;
					}
				}
				return num;
			}
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x000446E4 File Offset: 0x000428E4
		internal void SetValue(object value, bool isCLRReference)
		{
			if (this._value != null && this._value is byte[][] && !isCLRReference)
			{
				int num = 0;
				byte[][] array = this._value as byte[][];
				for (int i = 0; i < array.Length; i++)
				{
					num += array[i].Length;
				}
				this._size -= num;
			}
			this._value = value;
			if (this._value != null && this._value is byte[][] && !isCLRReference)
			{
				int num2 = 0;
				byte[][] array2 = this._value as byte[][];
				for (int j = 0; j < array2.Length; j++)
				{
					num2 += array2[j].Length;
				}
				this._size += num2;
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x0600163C RID: 5692 RVA: 0x00044794 File Offset: 0x00042994
		// (set) Token: 0x0600163D RID: 5693 RVA: 0x0004479C File Offset: 0x0004299C
		public override IOMRegion Region
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

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x0600163E RID: 5694 RVA: 0x000447A5 File Offset: 0x000429A5
		public override string RegionName
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

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x0600163F RID: 5695 RVA: 0x000447BC File Offset: 0x000429BC
		public override Key Key
		{
			get
			{
				return this._key;
			}
		}

		// Token: 0x04000C84 RID: 3204
		[DataMember]
		private IOMRegion _region;

		// Token: 0x04000C85 RID: 3205
		[DataMember]
		private object _value;

		// Token: 0x04000C86 RID: 3206
		[DataMember]
		private DataCacheTag[] _tags;

		// Token: 0x04000C87 RID: 3207
		[DataMember]
		private Key _key;

		// Token: 0x04000C88 RID: 3208
		[DataMember]
		private int _size;
	}
}
