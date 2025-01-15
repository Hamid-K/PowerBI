using System;
using System.Globalization;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001125 RID: 4389
	internal class SeekableBinaryValue : StreamedBinaryValue
	{
		// Token: 0x060072BE RID: 29374 RVA: 0x0018A395 File Offset: 0x00188595
		public SeekableBinaryValue(IPersistentCache cache, string key, BinaryValue value)
		{
			this.cache = cache;
			this.pointerToContentKey = key;
			this.content = value;
		}

		// Token: 0x060072BF RID: 29375 RVA: 0x0018A3B2 File Offset: 0x001885B2
		public Stream Open(out string contentKey)
		{
			Stream stream = this.Open();
			contentKey = this.contentKey;
			return stream;
		}

		// Token: 0x060072C0 RID: 29376 RVA: 0x0018A3C4 File Offset: 0x001885C4
		public override Stream Open()
		{
			Stream stream;
			if (this.TryEnsureContentKey() && this.cache.TryGetValue(this.contentKey, out stream))
			{
				return PersistentCacheEntryStream.New(stream, this.contentKey);
			}
			stream = this.content.Open();
			if (PersistentCacheEntryStream.IsCachedStream(stream, out this.contentKey))
			{
				return stream;
			}
			DateTime dateTime;
			if (stream.CanSeek && DataSource.TryGetLastWriteTimeUtc(this.content, out dateTime))
			{
				this.contentKey = PersistentCacheKey.SeekableBinaryValue.Qualify(this.pointerToContentKey, stream.Length.ToString(CultureInfo.InvariantCulture), dateTime.Ticks.ToString(CultureInfo.InvariantCulture));
				return PersistentCacheEntryStream.NewNonCached(stream, this.contentKey);
			}
			this.contentKey = PersistentCacheKey.SeekableBinaryValue.Qualify(this.pointerToContentKey, Guid.NewGuid().ToString());
			using (Stream stream2 = stream)
			{
				stream = this.cache.BeginAdd();
				stream2.CopyTo(stream);
				stream = this.cache.EndAdd(this.contentKey, stream);
			}
			Stream stream3 = this.cache.BeginAdd();
			new BinaryWriter(stream3).Write(this.contentKey);
			this.cache.EndAdd(this.pointerToContentKey, stream3);
			return PersistentCacheEntryStream.New(stream, this.contentKey);
		}

		// Token: 0x060072C1 RID: 29377 RVA: 0x0018A540 File Offset: 0x00188740
		public override bool TryGetLength(out long length)
		{
			return this.content.TryGetLength(out length);
		}

		// Token: 0x060072C2 RID: 29378 RVA: 0x0018A550 File Offset: 0x00188750
		private bool TryEnsureContentKey()
		{
			if (this.contentKey != null)
			{
				return true;
			}
			Stream stream;
			if (this.cache.TryGetValue(this.pointerToContentKey, out stream))
			{
				using (BinaryReader binaryReader = new BinaryReader(stream))
				{
					this.contentKey = binaryReader.ReadString();
					return true;
				}
				return false;
			}
			return false;
		}

		// Token: 0x04003F40 RID: 16192
		private readonly IPersistentCache cache;

		// Token: 0x04003F41 RID: 16193
		private readonly string pointerToContentKey;

		// Token: 0x04003F42 RID: 16194
		private readonly BinaryValue content;

		// Token: 0x04003F43 RID: 16195
		private string contentKey;
	}
}
