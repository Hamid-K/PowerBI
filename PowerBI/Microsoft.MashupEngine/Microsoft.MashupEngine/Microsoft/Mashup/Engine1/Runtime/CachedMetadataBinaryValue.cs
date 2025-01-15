using System;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001295 RID: 4757
	internal class CachedMetadataBinaryValue : StreamedBinaryValue
	{
		// Token: 0x06007CF7 RID: 31991 RVA: 0x001ACBE0 File Offset: 0x001AADE0
		public CachedMetadataBinaryValue(IPersistentCache persistentCache, string key, BinaryValue value)
		{
			this.persistentCache = persistentCache;
			this.key = key;
			this.metakey = key + "\\\\meta";
			this.value = value;
		}

		// Token: 0x06007CF8 RID: 31992 RVA: 0x001ACC10 File Offset: 0x001AAE10
		public override Stream Open()
		{
			Stream stream;
			if (!this.persistentCache.TryGetValue(this.key, out stream))
			{
				RecordValue recordValue;
				this.CacheValue(out stream, out recordValue);
			}
			return stream;
		}

		// Token: 0x170021FB RID: 8699
		// (get) Token: 0x06007CF9 RID: 31993 RVA: 0x001ACC44 File Offset: 0x001AAE44
		public override RecordValue MetaValue
		{
			get
			{
				if (this.metadata != null)
				{
					return this.metadata;
				}
				Stream stream;
				if (this.persistentCache.TryGetValue(this.metakey, out stream))
				{
					using (StreamValueReader streamValueReader = new StreamValueReader(stream))
					{
						return new ValueTreeDeserializer(streamValueReader).ReadRecord();
					}
				}
				Stream stream2;
				RecordValue recordValue;
				this.CacheValue(out stream2, out recordValue);
				stream2.Close();
				return recordValue;
			}
		}

		// Token: 0x06007CFA RID: 31994 RVA: 0x001ACCC0 File Offset: 0x001AAEC0
		private void CacheValue(out Stream stream, out RecordValue metadata)
		{
			stream = new MemoryStream();
			using (Stream stream2 = this.persistentCache.Add(this.key, this.value.Open()))
			{
				stream2.CopyTo(stream);
			}
			stream.Seek(0L, SeekOrigin.Begin);
			Stream stream3 = this.persistentCache.BeginAdd();
			StreamValueWriter streamValueWriter = new StreamValueWriter(stream3);
			new ValueTreeSerializer(streamValueWriter, 5).WriteRecord(this.value.MetaValue);
			streamValueWriter.Flush();
			this.persistentCache.EndAdd(this.metakey, stream3);
			this.metadata = this.value.MetaValue;
			metadata = this.metadata;
		}

		// Token: 0x06007CFB RID: 31995 RVA: 0x001ACD88 File Offset: 0x001AAF88
		public override bool TryGetLength(out long length)
		{
			return this.value.TryGetLength(out length);
		}

		// Token: 0x040044E2 RID: 17634
		private readonly IPersistentCache persistentCache;

		// Token: 0x040044E3 RID: 17635
		private readonly string key;

		// Token: 0x040044E4 RID: 17636
		private readonly string metakey;

		// Token: 0x040044E5 RID: 17637
		private readonly BinaryValue value;

		// Token: 0x040044E6 RID: 17638
		private RecordValue metadata;
	}
}
