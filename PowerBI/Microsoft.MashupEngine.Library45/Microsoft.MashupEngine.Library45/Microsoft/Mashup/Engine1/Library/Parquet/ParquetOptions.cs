using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;
using ParquetSharp;

namespace Microsoft.Mashup.Engine1.Library.Parquet
{
	// Token: 0x02001F37 RID: 7991
	internal static class ParquetOptions
	{
		// Token: 0x06010CBB RID: 68795 RVA: 0x0039D788 File Offset: 0x0039B988
		public static bool TryConvertCompressionOption(Value value, out object compression)
		{
			if (value.IsNull)
			{
				compression = global::ParquetSharp.Compression.Uncompressed;
				return true;
			}
			try
			{
				CompressionKind value2 = Library.CompressionType.Type.GetValue(value.AsNumber);
				global::ParquetSharp.Compression compression2;
				if (ParquetOptions.CompressionMap.TryGetValue(value2, out compression2))
				{
					Value value3;
					if (compression2 == global::ParquetSharp.Compression.Lz4 && value.TryGetMetaField("Hadoop", out value3) && value3.IsLogical && value3.AsBoolean)
					{
						compression = global::ParquetSharp.Compression.Lz4Hadoop;
					}
					else
					{
						compression = compression2;
					}
					return true;
				}
			}
			catch (ValueException)
			{
			}
			compression = null;
			return false;
		}

		// Token: 0x06010CBC RID: 68796 RVA: 0x0039D820 File Offset: 0x0039BA20
		public static int GetMaxDepth(OptionsRecord options)
		{
			Value value;
			if (options.TryGetValue("MaxDepth", out value) && !value.IsNull)
			{
				return value.AsInteger32;
			}
			return 32;
		}

		// Token: 0x06010CBD RID: 68797 RVA: 0x0039D850 File Offset: 0x0039BA50
		public static bool GetColumnNameEncodingOption(OptionsRecord options, bool defaultIfNull = false)
		{
			Value value;
			if (options.TryGetValue("LegacyColumnNameEncoding", out value) && !value.IsNull)
			{
				return value.AsBoolean;
			}
			return defaultIfNull;
		}

		// Token: 0x06010CBE RID: 68798 RVA: 0x0039D87C File Offset: 0x0039BA7C
		public static TypeMapping GetTypeMapping(OptionsRecord options)
		{
			Value value;
			if (!options.TryGetValue("TypeMapping", out value) || value.IsNull)
			{
				return TypeMapping.Default;
			}
			if (value.IsText && value.AsString.Equals("Sql"))
			{
				return TypeMapping.Sql;
			}
			throw ValueException.NewExpressionError<Message1>(Strings.InvalidOptionValue(PiiFree.New("TypeMapping")), value, null);
		}

		// Token: 0x040064AF RID: 25775
		public const string CompressionKey = "Compression";

		// Token: 0x040064B0 RID: 25776
		public const string MaxDepthKey = "MaxDepth";

		// Token: 0x040064B1 RID: 25777
		public const string LegacyColumnNameEncodingKey = "LegacyColumnNameEncoding";

		// Token: 0x040064B2 RID: 25778
		public const string PreserveOrderKey = "PreserveOrder";

		// Token: 0x040064B3 RID: 25779
		public const string TypeMappingKey = "TypeMapping";

		// Token: 0x040064B4 RID: 25780
		public const string UseStatisticsKey = "UseStatistics";

		// Token: 0x040064B5 RID: 25781
		private const string HadoopKey = "Hadoop";

		// Token: 0x040064B6 RID: 25782
		public const int MaxDepthDefault = 32;

		// Token: 0x040064B7 RID: 25783
		public const string TypeMappingSql = "Sql";

		// Token: 0x040064B8 RID: 25784
		private static readonly Dictionary<CompressionKind, global::ParquetSharp.Compression> CompressionMap = new Dictionary<CompressionKind, global::ParquetSharp.Compression>
		{
			{
				CompressionKind.None,
				global::ParquetSharp.Compression.Uncompressed
			},
			{
				CompressionKind.Snappy,
				global::ParquetSharp.Compression.Snappy
			},
			{
				CompressionKind.GZip,
				global::ParquetSharp.Compression.Gzip
			},
			{
				CompressionKind.Brotli,
				global::ParquetSharp.Compression.Brotli
			},
			{
				CompressionKind.LZ4,
				global::ParquetSharp.Compression.Lz4
			},
			{
				CompressionKind.Zstandard,
				global::ParquetSharp.Compression.Zstd
			}
		};
	}
}
