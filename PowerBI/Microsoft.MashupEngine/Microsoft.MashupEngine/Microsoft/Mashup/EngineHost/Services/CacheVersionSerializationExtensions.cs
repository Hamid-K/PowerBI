using System;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001993 RID: 6547
	internal static class CacheVersionSerializationExtensions
	{
		// Token: 0x0600A621 RID: 42529 RVA: 0x00225B58 File Offset: 0x00223D58
		public static void WriteCacheVersion(this BinaryWriter writer, CacheVersion version)
		{
			LongCacheVersion longCacheVersion = version as LongCacheVersion;
			if (longCacheVersion != null)
			{
				writer.WriteByte(1);
				writer.WriteInt64(LongCacheVersion.ToLong(longCacheVersion));
				return;
			}
			MultiLevelCacheVersion multiLevelCacheVersion = version as MultiLevelCacheVersion;
			if (multiLevelCacheVersion != null)
			{
				writer.WriteByte(2);
				writer.WriteCacheVersion(MultiLevelCacheVersion.GetVersion1(multiLevelCacheVersion));
				writer.WriteCacheVersion(MultiLevelCacheVersion.GetVersion2(multiLevelCacheVersion));
				return;
			}
			writer.WriteByte(0);
		}

		// Token: 0x0600A622 RID: 42530 RVA: 0x00225BB4 File Offset: 0x00223DB4
		public static CacheVersion ReadCacheVersion(this BinaryReader reader)
		{
			switch (reader.ReadByte())
			{
			case 0:
				return null;
			case 1:
				return LongCacheVersion.New(reader.ReadInt64());
			case 2:
				return new MultiLevelCacheVersion(reader.ReadCacheVersion(), reader.ReadCacheVersion());
			default:
				throw new NotSupportedException();
			}
		}

		// Token: 0x02001994 RID: 6548
		private enum CacheVersionKind : byte
		{
			// Token: 0x04005673 RID: 22131
			None,
			// Token: 0x04005674 RID: 22132
			Long,
			// Token: 0x04005675 RID: 22133
			MultiLevel
		}
	}
}
