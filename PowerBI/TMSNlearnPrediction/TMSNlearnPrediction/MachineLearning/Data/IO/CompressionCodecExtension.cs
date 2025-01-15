using System;
using System.IO;
using Microsoft.MachineLearning.Data.IO.Zlib;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data.IO
{
	// Token: 0x02000307 RID: 775
	public static class CompressionCodecExtension
	{
		// Token: 0x06001157 RID: 4439 RVA: 0x0005FDF0 File Offset: 0x0005DFF0
		public static Stream CompressStream(this CompressionKind compression, Stream stream)
		{
			switch (compression)
			{
			case CompressionKind.None:
				return new SubsetStream(stream, null);
			case CompressionKind.Deflate:
				return new ZDeflateStream(stream, Constants.Level.BestCompression, Constants.Strategy.DefaultStrategy, 9, false, 15);
			case CompressionKind.Zlib:
				return new ZDeflateStream(stream, Constants.Level.BestCompression, Constants.Strategy.DefaultStrategy, 9, true, 15);
			default:
				throw Contracts.Except("unrecognized compression codec {0}", new object[] { compression });
			}
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x0005FE5C File Offset: 0x0005E05C
		public static Stream DecompressStream(this CompressionKind compression, Stream stream)
		{
			switch (compression)
			{
			case CompressionKind.None:
				return new SubsetStream(stream, null);
			case CompressionKind.Deflate:
				return new ZInflateStream(stream, false);
			case CompressionKind.Zlib:
				return new ZInflateStream(stream, true);
			default:
				throw Contracts.Except("unrecognized compression codec {0}", new object[] { compression });
			}
		}
	}
}
