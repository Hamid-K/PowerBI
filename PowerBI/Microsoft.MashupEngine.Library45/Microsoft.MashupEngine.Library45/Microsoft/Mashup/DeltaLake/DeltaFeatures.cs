using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Data.DeltaLake;
using Microsoft.Data.DeltaLake.Commands;
using Microsoft.Data.DeltaLake.Types;
using Microsoft.Mashup.Common;

namespace Microsoft.Mashup.DeltaLake
{
	// Token: 0x02001F09 RID: 7945
	internal static class DeltaFeatures
	{
		// Token: 0x06010BA6 RID: 68518 RVA: 0x0039A9A8 File Offset: 0x00398BA8
		public static string[] UnsupportedReadFeatures(Snapshot snapshot)
		{
			Protocol protocol = snapshot.GetProtocol();
			if (protocol.MinReaderVersion > 3)
			{
				return new string[] { string.Format(CultureInfo.InvariantCulture, "minReaderVersion({0})", protocol.MinReaderVersion) };
			}
			HashSet<string> readerFeatures = DeltaFeatures.GetReaderFeatures(protocol, snapshot.GetMetadata());
			readerFeatures.ExceptWith(DeltaFeatures.supportedReaderFeatures);
			string[] array = readerFeatures.ToArray<string>();
			Array.Sort<string>(array);
			return array;
		}

		// Token: 0x06010BA7 RID: 68519 RVA: 0x0039AA0C File Offset: 0x00398C0C
		public static string[] UnsupportedWriteFeatures(Snapshot snapshot)
		{
			Protocol protocol = snapshot.GetProtocol();
			if (protocol.MinReaderVersion > 7)
			{
				return new string[] { string.Format(CultureInfo.InvariantCulture, "minReaderVersion({0})", protocol.MinReaderVersion) };
			}
			HashSet<string> writerFeatures = DeltaFeatures.GetWriterFeatures(protocol, snapshot);
			writerFeatures.ExceptWith(DeltaFeatures.supportedWriterFeatures);
			string[] array = writerFeatures.ToArray<string>();
			Array.Sort<string>(array);
			return array;
		}

		// Token: 0x06010BA8 RID: 68520 RVA: 0x0039AA6C File Offset: 0x00398C6C
		private static HashSet<string> GetReaderFeatures(Protocol protocol, Metadata metadata)
		{
			HashSet<string> hashSet = new HashSet<string>();
			if (protocol.MinReaderVersion == 3)
			{
				if (protocol.ReaderFeatures != null)
				{
					hashSet.UnionWith(protocol.ReaderFeatures);
				}
			}
			else if (protocol.MinReaderVersion >= 2)
			{
				if (DeltaFeatures.AnyTableProperty(metadata, (string k, string v) => string.Equals(k, "delta.columnMapping.mode", StringComparison.OrdinalIgnoreCase)))
				{
					hashSet.Add("columnMapping");
				}
			}
			return hashSet;
		}

		// Token: 0x06010BA9 RID: 68521 RVA: 0x0039AADC File Offset: 0x00398CDC
		private static HashSet<string> GetWriterFeatures(Protocol protocol, Snapshot snapshot)
		{
			if (protocol.MinWriterVersion == 7)
			{
				return new HashSet<string>(protocol.WriterFeatures ?? EmptyArray<string>.Instance);
			}
			Metadata metadata = snapshot.GetMetadata();
			HashSet<string> hashSet = new HashSet<string>();
			if (protocol.MinWriterVersion >= 2)
			{
				if (DeltaFeatures.AnyTableProperty(metadata, (string k, string v) => string.Equals(k, "delta.appendOnly", StringComparison.OrdinalIgnoreCase)))
				{
					hashSet.Add("appendOnly");
				}
			}
			if (protocol.MinWriterVersion >= 2)
			{
				if (DeltaFeatures.AnyColumnProperty(metadata, (string k, object v) => string.Equals(k, "delta.invariants", StringComparison.OrdinalIgnoreCase)))
				{
					hashSet.Add("invariants");
				}
			}
			if (protocol.MinWriterVersion >= 3)
			{
				if (DeltaFeatures.AnyTableProperty(metadata, (string k, string v) => k.StartsWith("delta.constraints.", StringComparison.OrdinalIgnoreCase)))
				{
					hashSet.Add("checkConstraints");
				}
			}
			if (protocol.MinWriterVersion >= 4)
			{
				if (DeltaFeatures.AnyColumnProperty(metadata, (string k, object v) => string.Equals(k, "delta.generationExpression", StringComparison.OrdinalIgnoreCase)))
				{
					hashSet.Add("generatedColumns");
				}
			}
			if (protocol.MinWriterVersion >= 4 && snapshot.GetCDCFiles().Length != 0)
			{
				hashSet.Add("changeDataFeed");
			}
			if (protocol.MinWriterVersion >= 5)
			{
				if (DeltaFeatures.AnyTableProperty(metadata, (string k, string v) => string.Equals(k, "delta.columnMapping.mode", StringComparison.OrdinalIgnoreCase)))
				{
					hashSet.Add("columnMapping");
				}
			}
			if (protocol.MinWriterVersion >= 6)
			{
				if (DeltaFeatures.AnyColumnProperty(metadata, (string k, object v) => k.StartsWith("delta.identity.", StringComparison.OrdinalIgnoreCase)))
				{
					hashSet.Add("identityColumns");
				}
			}
			return hashSet;
		}

		// Token: 0x06010BAA RID: 68522 RVA: 0x0039AC9C File Offset: 0x00398E9C
		private static bool AnyTableProperty(Metadata metadata, Func<string, string, bool> predicate)
		{
			return metadata.Configuration.Any((KeyValuePair<string, string> kv) => predicate(kv.Key, kv.Value));
		}

		// Token: 0x06010BAB RID: 68523 RVA: 0x0039ACD0 File Offset: 0x00398ED0
		private static bool AnyColumnProperty(Metadata metadata, Func<string, object, bool> predicate)
		{
			return new DeltaFeatures.ColumnPropertyVisitor(predicate).VisitStruct(metadata.Schema);
		}

		// Token: 0x04006444 RID: 25668
		public const string AppendOnlyFeature = "appendOnly";

		// Token: 0x04006445 RID: 25669
		public const string ColumnMappingFeature = "columnMapping";

		// Token: 0x04006446 RID: 25670
		public const string ColumnMappingMode = "delta.columnMapping.mode";

		// Token: 0x04006447 RID: 25671
		public const string ColumnMappingByName = "name";

		// Token: 0x04006448 RID: 25672
		public const string DeletionVectorsFeature = "deletionVectors";

		// Token: 0x04006449 RID: 25673
		public const string TimestampNtzFeature = "timestampNtz";

		// Token: 0x0400644A RID: 25674
		private static readonly HashSet<string> supportedWriterFeatures = new HashSet<string> { "appendOnly", "columnMapping", "deletionVectors", "timestampNtz" };

		// Token: 0x0400644B RID: 25675
		private static readonly HashSet<string> supportedReaderFeatures = new HashSet<string> { "columnMapping", "deletionVectors", "timestampNtz" };

		// Token: 0x02001F0A RID: 7946
		private struct ColumnPropertyVisitor : ITypeVisitor<bool>
		{
			// Token: 0x06010BAD RID: 68525 RVA: 0x0039AD69 File Offset: 0x00398F69
			public ColumnPropertyVisitor(Func<string, object, bool> predicate)
			{
				this.predicate = predicate;
			}

			// Token: 0x06010BAE RID: 68526 RVA: 0x0039AD72 File Offset: 0x00398F72
			public bool VisitArray(ArrayType array)
			{
				return array.ElementType.Accept<bool>(this);
			}

			// Token: 0x06010BAF RID: 68527 RVA: 0x0039AD8A File Offset: 0x00398F8A
			public bool VisitMap(MapType map)
			{
				return map.KeyType.Accept<bool>(this) || map.ValueType.Accept<bool>(this);
			}

			// Token: 0x06010BB0 RID: 68528 RVA: 0x0000FA11 File Offset: 0x0000DC11
			public bool VisitScalar(ScalarType scalar)
			{
				return false;
			}

			// Token: 0x06010BB1 RID: 68529 RVA: 0x0039ADBC File Offset: 0x00398FBC
			public bool VisitStruct(StructType @struct)
			{
				Func<string, object, bool> localPredicate = this.predicate;
				Func<KeyValuePair<string, object>, bool> <>9__0;
				foreach (StructField structField in @struct.Fields)
				{
					IEnumerable<KeyValuePair<string, object>> metadata = structField.Metadata;
					Func<KeyValuePair<string, object>, bool> func;
					if ((func = <>9__0) == null)
					{
						func = (<>9__0 = (KeyValuePair<string, object> kv) => localPredicate(kv.Key, kv.Value));
					}
					if (metadata.Any(func) || structField.DataType.Accept<bool>(this))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x0400644C RID: 25676
			private Func<string, object, bool> predicate;
		}
	}
}
