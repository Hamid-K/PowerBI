using System;
using System.IO;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200028D RID: 653
	internal class SerializedCacheItem : AOMCacheItem
	{
		// Token: 0x060017D9 RID: 6105 RVA: 0x000485C4 File Offset: 0x000467C4
		public SerializedCacheItem()
		{
			this._headPageId = -1L;
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x000485D4 File Offset: 0x000467D4
		protected override void Finalize()
		{
			try
			{
				if (!AppDomain.CurrentDomain.IsFinalizingForUnload() && this._headPageId != -1L)
				{
					Pool<SerializedCacheItem> cacheItemPool = this.CacheItemPool;
					this.Clean();
					cacheItemPool.PutObjectInPool(this);
					GC.ReRegisterForFinalize(this);
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x0004862C File Offset: 0x0004682C
		internal new void Clean()
		{
			this.ReleaseRecordStream();
			this._region = null;
			this._dataOffset = 0;
			this._size = 0;
			this._tagOffset = 0;
			base.Clean();
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x00048656 File Offset: 0x00046856
		private void ReleaseRecordStream()
		{
			if (this._headPageId != -1L)
			{
				RecordStream.ReleaseStream(this.GetAllocator(), this._headPageId);
				this._headPageId = -1L;
			}
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x0004867B File Offset: 0x0004687B
		public override void Reinit(Key key, object value, long TTL, long extnTimeout, object[] tags)
		{
			this.ReleaseRecordStream();
			this.Init(key, value, TTL, extnTimeout, tags);
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x00048690 File Offset: 0x00046890
		public override void Init(Key key, object value, long TTL, long extnTimeout, object[] tags)
		{
			base.Init();
			this.SetKeyValueTag(key, (byte[][])value, (DataCacheTag[])tags);
			base.TimeToLive = TTL;
			base.ExtensionTimeout = extnTimeout;
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x00048690 File Offset: 0x00046890
		public void InitValue(Key key, object value, long TTL, long extnTimeout, object[] tags)
		{
			base.Init();
			this.SetKeyValueTag(key, (byte[][])value, (DataCacheTag[])tags);
			base.TimeToLive = TTL;
			base.ExtensionTimeout = extnTimeout;
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x000486BB File Offset: 0x000468BB
		public void Init(IOMRegion region, Key key, object value, long TTL, long extnTimeout, object[] tags)
		{
			base.Init();
			this._region = region;
			this.SetKeyValueTag(key, (byte[][])value, (DataCacheTag[])tags);
			base.TimeToLive = TTL;
			base.ExtensionTimeout = extnTimeout;
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x000486EE File Offset: 0x000468EE
		public void Init(IOMRegion region, Key key)
		{
			base.Init();
			this._region = region;
			this.SetKeyValueTag(key, null, null);
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x00048708 File Offset: 0x00046908
		public override void GetKeyValueTag(ref Key key, ref object value, ref DataCacheTag[] tags)
		{
			if (this._headPageId != -1L)
			{
				using (BinaryReader binaryReader = this.GetBinaryReader())
				{
					key = new Key(binaryReader.ReadString(), base.HashCode);
					value = this.ReadChunkedArray(binaryReader);
					tags = this.ReadTagsArray(binaryReader);
				}
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x060017E3 RID: 6115 RVA: 0x00048768 File Offset: 0x00046968
		public override object[] Tags
		{
			get
			{
				return this.ReadTags();
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x060017E4 RID: 6116 RVA: 0x00048770 File Offset: 0x00046970
		// (set) Token: 0x060017E5 RID: 6117 RVA: 0x00003CAB File Offset: 0x00001EAB
		public override int Size
		{
			get
			{
				return this._size;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x060017E6 RID: 6118 RVA: 0x00048778 File Offset: 0x00046978
		public override object Value
		{
			get
			{
				return this.ReadValue();
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x060017E7 RID: 6119 RVA: 0x00048780 File Offset: 0x00046980
		internal override int ValueSize
		{
			get
			{
				return this._tagOffset - this._dataOffset;
			}
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x00048790 File Offset: 0x00046990
		internal void SetKeyValueTag(Key key, byte[][] value, DataCacheTag[] tags)
		{
			base.HashCode = key.GetHashCode();
			using (BinaryWriter binaryWriter = this.GetBinaryWriter())
			{
				SerializedCacheItem.WriteKey(key, binaryWriter);
				this._dataOffset = (int)binaryWriter.BaseStream.Position;
				SerializedCacheItem.WriteChunkedArray(value, binaryWriter);
				this._tagOffset = (int)binaryWriter.BaseStream.Position;
				SerializedCacheItem.WriteTags(tags, binaryWriter);
				base.IsTagsPresent = tags != null;
				this._size = ((RecordStream)binaryWriter.BaseStream).TotalSize;
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x060017E9 RID: 6121 RVA: 0x00048828 File Offset: 0x00046A28
		// (set) Token: 0x060017EA RID: 6122 RVA: 0x00048830 File Offset: 0x00046A30
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

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x060017EB RID: 6123 RVA: 0x00048839 File Offset: 0x00046A39
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

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x060017EC RID: 6124 RVA: 0x00048850 File Offset: 0x00046A50
		public override Key Key
		{
			get
			{
				return this.ReadKey();
			}
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x00048858 File Offset: 0x00046A58
		public override int GetHashCode()
		{
			return base.HashCode;
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x00048860 File Offset: 0x00046A60
		private BinaryReader GetBinaryReader()
		{
			return new BinaryReader(new RecordStream(this.GetAllocator(), this._headPageId, false));
		}

		// Token: 0x060017EF RID: 6127 RVA: 0x0004887C File Offset: 0x00046A7C
		private BinaryWriter GetBinaryWriter()
		{
			RecordStream recordStream = new RecordStream(this.GetAllocator());
			this._headPageId = recordStream.ToStreamId();
			return new BinaryWriter(recordStream);
		}

		// Token: 0x060017F0 RID: 6128 RVA: 0x000488A7 File Offset: 0x00046AA7
		private MemoryAllocator GetAllocator()
		{
			return this.Allocator;
		}

		// Token: 0x060017F1 RID: 6129 RVA: 0x000488AF File Offset: 0x00046AAF
		private static void WriteKey(Key key, BinaryWriter bw)
		{
			bw.Write(key.StringValue);
			bw.Flush();
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x000488C4 File Offset: 0x00046AC4
		private static void WriteChunkedArray(byte[][] value, BinaryWriter bw)
		{
			if (value == null)
			{
				int num = 0;
				bw.Write(num);
			}
			else
			{
				bw.Write(value.Length);
				for (int i = 0; i < value.Length; i++)
				{
					bw.Write(value[i].Length);
					bw.Write(value[i]);
				}
			}
			bw.Flush();
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x00048910 File Offset: 0x00046B10
		private static void WriteTags(DataCacheTag[] tags, BinaryWriter bw)
		{
			if (tags != null)
			{
				bw.Write(tags.Length);
				for (int i = 0; i < tags.Length; i++)
				{
					bw.Write(tags[i].GetHashCode());
					bw.Write(tags[i].ToString());
				}
			}
			else
			{
				bw.Write(0);
			}
			bw.Flush();
		}

		// Token: 0x060017F4 RID: 6132 RVA: 0x00048964 File Offset: 0x00046B64
		private Key ReadKey()
		{
			Key key = null;
			if (this._headPageId != -1L)
			{
				using (BinaryReader binaryReader = this.GetBinaryReader())
				{
					key = new Key(binaryReader.ReadString(), base.HashCode);
				}
			}
			return key;
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x000489B4 File Offset: 0x00046BB4
		private object ReadValue()
		{
			byte[][] array = null;
			if (this._headPageId != -1L)
			{
				using (BinaryReader binaryReader = this.GetBinaryReader())
				{
					array = this.ReadChunkedArray(binaryReader);
				}
			}
			return array;
		}

		// Token: 0x060017F6 RID: 6134 RVA: 0x000489FC File Offset: 0x00046BFC
		private byte[][] ReadChunkedArray(BinaryReader reader)
		{
			byte[][] array = null;
			reader.BaseStream.Seek((long)this._dataOffset, SeekOrigin.Begin);
			int num = reader.ReadInt32();
			if (num > 0)
			{
				array = new byte[num][];
				for (int i = 0; i < num; i++)
				{
					int num2 = reader.ReadInt32();
					array[i] = reader.ReadBytes(num2);
				}
				return array;
			}
			return array;
		}

		// Token: 0x060017F7 RID: 6135 RVA: 0x00048A54 File Offset: 0x00046C54
		private object[] ReadTags()
		{
			object[] array = null;
			if (base.IsTagsPresent && this._headPageId != -1L)
			{
				using (BinaryReader binaryReader = this.GetBinaryReader())
				{
					array = this.ReadTagsArray(binaryReader);
				}
			}
			return array;
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x00048AA4 File Offset: 0x00046CA4
		private DataCacheTag[] ReadTagsArray(BinaryReader reader)
		{
			DataCacheTag[] array = null;
			reader.BaseStream.Seek((long)this._tagOffset, SeekOrigin.Begin);
			int num = reader.ReadInt32();
			if (num > 0)
			{
				array = new DataCacheTag[num];
				for (int i = 0; i < num; i++)
				{
					int num2 = reader.ReadInt32();
					array[i] = new DataCacheTag(reader.ReadString(), num2, false);
				}
			}
			return array;
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x060017F9 RID: 6137 RVA: 0x00048AFD File Offset: 0x00046CFD
		private MemoryAllocator Allocator
		{
			get
			{
				return ((BufferMemoryManager)this._region.MemoryManager).Allocator;
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x060017FA RID: 6138 RVA: 0x00048B14 File Offset: 0x00046D14
		private Pool<SerializedCacheItem> CacheItemPool
		{
			get
			{
				return ((SerializedCacheItemFactory)this._region.MemoryManager.GetCacheItemFactory()).Pool;
			}
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x00048830 File Offset: 0x00046A30
		internal void Init(IOMRegion region)
		{
			this._region = region;
		}

		// Token: 0x04000D37 RID: 3383
		private const long NIL = -1L;

		// Token: 0x04000D38 RID: 3384
		private IOMRegion _region;

		// Token: 0x04000D39 RID: 3385
		private int _size;

		// Token: 0x04000D3A RID: 3386
		private int _dataOffset;

		// Token: 0x04000D3B RID: 3387
		private int _tagOffset;

		// Token: 0x04000D3C RID: 3388
		private long _headPageId;
	}
}
