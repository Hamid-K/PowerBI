using System;
using System.Linq;
using Microsoft.Apache.Parquet.Format;
using Microsoft.Apache.Thrift;
using Microsoft.Mashup.Engine1.Library.Table;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Parquet
{
	// Token: 0x02001F71 RID: 8049
	internal class ParquetSummary
	{
		// Token: 0x06010E1B RID: 69147 RVA: 0x003A2D10 File Offset: 0x003A0F10
		public static RecordValue MakeSummary(byte[] footer)
		{
			FileMetaData fileMetaData = new FileMetaData();
			fileMetaData.Read(new FastProtocol(footer));
			return RecordValue.New(Keys.New(new string[] { "Version", "Schema", "NumRows", "RowGroups", "KeyValueMetadata", "CreatedBy", "ColumnOrders" }), new Value[]
			{
				ParquetSummary.AsValue(fileMetaData.Version),
				ParquetSummary.AsValue(fileMetaData.Schema),
				ParquetSummary.AsValue(fileMetaData.NumRows),
				ParquetSummary.AsValue(fileMetaData.RowGroups),
				ParquetSummary.AsValue(fileMetaData.KeyValueMetadata),
				TextValue.NewOrNull(fileMetaData.CreatedBy),
				ParquetSummary.AsValue(fileMetaData.ColumnOrders)
			});
		}

		// Token: 0x06010E1C RID: 69148 RVA: 0x003A2DDF File Offset: 0x003A0FDF
		private static Value AsValue(int? value)
		{
			if (value != null)
			{
				return NumberValue.New(value.Value);
			}
			return Value.Null;
		}

		// Token: 0x06010E1D RID: 69149 RVA: 0x003A2DFC File Offset: 0x003A0FFC
		private static Value AsValue(SchemaElement[] schema)
		{
			return ParquetSummary.AsValue<SchemaElement>(schema, Keys.New(new string[] { "Type", "TypeLength", "RepetitionType", "Name", "NumChildren", "ConvertedType", "Scale", "Precision", "FieldId", "LogicalType" }), (SchemaElement s) => ListValue.New(new Value[]
			{
				ParquetSummary.AsValue(s.Type),
				ParquetSummary.AsValue(s.TypeLength),
				ParquetSummary.AsValue(s.RepetitionType),
				TextValue.NewOrNull(s.Name),
				ParquetSummary.AsValue(s.NumChildren),
				ParquetSummary.AsValue(s.ConvertedType),
				ParquetSummary.AsValue(s.Scale),
				ParquetSummary.AsValue(s.Precision),
				ParquetSummary.AsValue(s.FieldId),
				ParquetSummary.AsValue(s.LogicalType)
			}));
		}

		// Token: 0x06010E1E RID: 69150 RVA: 0x003A2E8B File Offset: 0x003A108B
		private static Value AsValue(long? value)
		{
			if (value != null)
			{
				return NumberValue.New(value.Value);
			}
			return Value.Null;
		}

		// Token: 0x06010E1F RID: 69151 RVA: 0x003A2EA8 File Offset: 0x003A10A8
		private static Value AsValue(RowGroup[] rowGroups)
		{
			return ParquetSummary.AsValue<RowGroup>(rowGroups, Keys.New(new string[] { "Columns", "TotalByteSize", "NumRows", "SortingColumns", "FileOffset", "TotalCompressedSize", "Ordinal" }), (RowGroup r) => ListValue.New(new Value[]
			{
				ParquetSummary.AsValue(r.Columns),
				ParquetSummary.AsValue(r.TotalByteSize),
				ParquetSummary.AsValue(r.NumRows),
				ParquetSummary.AsValue(r.SortingColumns),
				ParquetSummary.AsValue(r.FileOffset),
				ParquetSummary.AsValue(r.TotalCompressedSize),
				ParquetSummary.AsValue(r.Ordinal)
			}));
		}

		// Token: 0x06010E20 RID: 69152 RVA: 0x003A2F1D File Offset: 0x003A111D
		private static Value AsValue<T>(T[] values, Keys keys, Func<T, ListValue> ctor)
		{
			if (values == null)
			{
				return Value.Null;
			}
			return TableModule.Table.FromRows.Invoke(ListValue.New(values.Select(ctor)), ListValue.New(keys.ToArray<string>()));
		}

		// Token: 0x06010E21 RID: 69153 RVA: 0x003A2F49 File Offset: 0x003A1149
		private static Value AsValue<T>(T[] values, Func<T, Value> ctor)
		{
			if (values == null)
			{
				return Value.Null;
			}
			return ListValue.New(values.Select(ctor));
		}

		// Token: 0x06010E22 RID: 69154 RVA: 0x003A2F60 File Offset: 0x003A1160
		private static Value AsValue(KeyValue[] keyValues)
		{
			if (keyValues == null)
			{
				return Value.Null;
			}
			if (keyValues.Length == 0)
			{
				return RecordValue.Empty;
			}
			RecordBuilder recordBuilder = new RecordBuilder(keyValues.Length);
			for (int i = 0; i < keyValues.Length; i++)
			{
				recordBuilder.Add(keyValues[i].Key, TextValue.NewOrNull(keyValues[i].Value), (keyValues[i] == null) ? TypeValue.Null : TypeValue.Text);
			}
			return recordBuilder.ToRecord();
		}

		// Token: 0x06010E23 RID: 69155 RVA: 0x003A2FCC File Offset: 0x003A11CC
		private static Value AsValue(ColumnOrder[] columnOrders)
		{
			return ParquetSummary.AsValue<ColumnOrder>(columnOrders, new Func<ColumnOrder, Value>(ParquetSummary.AsValue));
		}

		// Token: 0x06010E24 RID: 69156 RVA: 0x003A2FE0 File Offset: 0x003A11E0
		private static Value AsValue(ColumnOrder columnOrder)
		{
			if (columnOrder == null)
			{
				return Value.Null;
			}
			if (columnOrder.TYPE_ORDER != null)
			{
				return RecordValue.New(Keys.New("ColumnOrder"), new Value[] { TextValue.New("TYPE_ORDER") });
			}
			return RecordValue.New(Keys.New("ColumnOrder"), new Value[] { Value.Null });
		}

		// Token: 0x06010E25 RID: 69157 RVA: 0x003A3040 File Offset: 0x003A1240
		private static Value AsValue(ColumnMetaData columnMetaData)
		{
			if (columnMetaData == null)
			{
				return Value.Null;
			}
			return RecordValue.New(Keys.New(new string[]
			{
				"Type", "Encodings", "PathInSchema", "Codec", "NumValues", "TotalUncompressedSize", "TotalCompressedSize", "KeyValueMetadata", "DataPageOffset", "IndexPageOffset",
				"DictionaryPageOffset", "Statistics", "EncodingStats", "BloomFilterOffset", "BloomFilterLength"
			}), new Value[]
			{
				ParquetSummary.AsValue(columnMetaData.Type),
				ParquetSummary.AsValue(columnMetaData.Encodings),
				ParquetSummary.AsValue(columnMetaData.PathInSchema),
				ParquetSummary.AsValue(columnMetaData.Codec),
				ParquetSummary.AsValue(columnMetaData.NumValues),
				ParquetSummary.AsValue(columnMetaData.TotalUncompressedSize),
				ParquetSummary.AsValue(columnMetaData.TotalCompressedSize),
				ParquetSummary.AsValue(columnMetaData.KeyValueMetadata),
				ParquetSummary.AsValue(columnMetaData.DataPageOffset),
				ParquetSummary.AsValue(columnMetaData.IndexPageOffset),
				ParquetSummary.AsValue(columnMetaData.DictionaryPageOffset),
				ParquetSummary.AsValue(columnMetaData.Statistics),
				ParquetSummary.AsValue(columnMetaData.EncodingStats),
				ParquetSummary.AsValue(columnMetaData.BloomFilterOffset),
				ParquetSummary.AsValue(columnMetaData.BloomFilterLength)
			});
		}

		// Token: 0x06010E26 RID: 69158 RVA: 0x003A31C4 File Offset: 0x003A13C4
		private static Value AsValue(ColumnChunk[] columnChunks)
		{
			return ParquetSummary.AsValue<ColumnChunk>(columnChunks, Keys.New(new string[] { "FilePath", "FileOffset", "MetaData", "OffsetIndexOffset", "OffsetIndexLength", "ColumnIndexOffset", "ColumnIndexLength" }), (ColumnChunk c) => ListValue.New(new Value[]
			{
				TextValue.NewOrNull(c.FilePath),
				ParquetSummary.AsValue(c.FileOffset),
				ParquetSummary.AsValue(c.MetaData),
				ParquetSummary.AsValue(c.OffsetIndexOffset),
				ParquetSummary.AsValue(c.OffsetIndexLength),
				ParquetSummary.AsValue(c.ColumnIndexOffset),
				ParquetSummary.AsValue(c.ColumnIndexLength)
			}));
		}

		// Token: 0x06010E27 RID: 69159 RVA: 0x003A3239 File Offset: 0x003A1439
		private static Value AsValue(SortingColumn[] sortingColumns)
		{
			return ParquetSummary.AsValue<SortingColumn>(sortingColumns, Keys.New("ColumnIdx", "Descending", "NullsFirst"), (SortingColumn s) => ListValue.New(new Value[]
			{
				ParquetSummary.AsValue(s.ColumnIdx),
				ParquetSummary.AsValue(s.Descending),
				ParquetSummary.AsValue(s.NullsFirst)
			}));
		}

		// Token: 0x06010E28 RID: 69160 RVA: 0x003A3274 File Offset: 0x003A1474
		private static Value AsValue(short? value)
		{
			if (value != null)
			{
				return NumberValue.New((int)value.Value);
			}
			return Value.Null;
		}

		// Token: 0x06010E29 RID: 69161 RVA: 0x003A3291 File Offset: 0x003A1491
		private static Value AsValue(bool? value)
		{
			if (value != null)
			{
				return LogicalValue.New(value.Value);
			}
			return Value.Null;
		}

		// Token: 0x06010E2A RID: 69162 RVA: 0x003A32B0 File Offset: 0x003A14B0
		private static Value AsValue(Microsoft.Apache.Parquet.Format.Type? type)
		{
			if (type != null)
			{
				return TextValue.New(type.Value.ToString());
			}
			return Value.Null;
		}

		// Token: 0x06010E2B RID: 69163 RVA: 0x003A32E8 File Offset: 0x003A14E8
		private static Value AsValue(CompressionCodec? codec)
		{
			if (codec != null)
			{
				return TextValue.New(codec.Value.ToString());
			}
			return Value.Null;
		}

		// Token: 0x06010E2C RID: 69164 RVA: 0x003A331E File Offset: 0x003A151E
		private static Value AsValue(string[] values)
		{
			return ParquetSummary.AsValue<string>(values, (string v) => TextValue.NewOrNull(v));
		}

		// Token: 0x06010E2D RID: 69165 RVA: 0x003A3345 File Offset: 0x003A1545
		private static Value AsValue(Encoding[] encodings)
		{
			return ParquetSummary.AsValue<Encoding>(encodings, (Encoding e) => TextValue.New(e.ToString()));
		}

		// Token: 0x06010E2E RID: 69166 RVA: 0x003A336C File Offset: 0x003A156C
		private static Value AsValue(Encoding? encoding)
		{
			if (encoding != null)
			{
				return TextValue.New(encoding.Value.ToString());
			}
			return Value.Null;
		}

		// Token: 0x06010E2F RID: 69167 RVA: 0x003A33A4 File Offset: 0x003A15A4
		private static Value AsValue(Statistics statistics)
		{
			if (statistics == null)
			{
				return Value.Null;
			}
			return RecordValue.New(Keys.New(new string[] { "Max", "Min", "NullCount", "DistinctCount", "MaxValue", "MinValue" }), new Value[]
			{
				ParquetSummary.AsValue(statistics.Max),
				ParquetSummary.AsValue(statistics.Min),
				ParquetSummary.AsValue(statistics.NullCount),
				ParquetSummary.AsValue(statistics.DistinctCount),
				ParquetSummary.AsValue(statistics.MaxValue),
				ParquetSummary.AsValue(statistics.MinValue)
			});
		}

		// Token: 0x06010E30 RID: 69168 RVA: 0x003A3454 File Offset: 0x003A1654
		private static Value AsValue(byte[] value)
		{
			if (value != null)
			{
				return BinaryValue.New(value);
			}
			return Value.Null;
		}

		// Token: 0x06010E31 RID: 69169 RVA: 0x003A3465 File Offset: 0x003A1665
		private static Value AsValue(PageEncodingStats[] pageEncodingStats)
		{
			return ParquetSummary.AsValue<PageEncodingStats>(pageEncodingStats, Keys.New("PageType", "Encoding", "Count"), (PageEncodingStats p) => ListValue.New(new Value[]
			{
				ParquetSummary.AsValue(p.PageType),
				ParquetSummary.AsValue(p.Encoding),
				ParquetSummary.AsValue(p.Count)
			}));
		}

		// Token: 0x06010E32 RID: 69170 RVA: 0x003A34A0 File Offset: 0x003A16A0
		private static Value AsValue(PageType? pageType)
		{
			if (pageType != null)
			{
				return TextValue.New(pageType.Value.ToString());
			}
			return Value.Null;
		}

		// Token: 0x06010E33 RID: 69171 RVA: 0x003A34D8 File Offset: 0x003A16D8
		private static Value AsValue(FieldRepetitionType? fieldRepetitionType)
		{
			if (fieldRepetitionType != null)
			{
				return TextValue.New(fieldRepetitionType.Value.ToString());
			}
			return Value.Null;
		}

		// Token: 0x06010E34 RID: 69172 RVA: 0x003A3510 File Offset: 0x003A1710
		private static Value AsValue(ConvertedType? convertedType)
		{
			if (convertedType != null)
			{
				return TextValue.New(convertedType.Value.ToString());
			}
			return Value.Null;
		}

		// Token: 0x06010E35 RID: 69173 RVA: 0x003A3548 File Offset: 0x003A1748
		private static Value AsValue(LogicalType logicalType)
		{
			if (logicalType == null)
			{
				return Value.Null;
			}
			if (logicalType.STRING != null)
			{
				return RecordValue.New(Keys.New("LogicalType"), new Value[] { TextValue.New("STRING") });
			}
			if (logicalType.MAP != null)
			{
				return RecordValue.New(Keys.New("LogicalType"), new Value[] { TextValue.New("MAP") });
			}
			if (logicalType.LIST != null)
			{
				return RecordValue.New(Keys.New("LogicalType"), new Value[] { TextValue.New("LIST") });
			}
			if (logicalType.ENUM != null)
			{
				return RecordValue.New(Keys.New("LogicalType"), new Value[] { TextValue.New("ENUM") });
			}
			if (logicalType.DECIMAL != null)
			{
				return ParquetSummary.AsValue(logicalType.DECIMAL);
			}
			if (logicalType.DATE != null)
			{
				return RecordValue.New(Keys.New("LogicalType"), new Value[] { TextValue.New("DATE") });
			}
			if (logicalType.TIME != null)
			{
				return ParquetSummary.AsValue(logicalType.TIME);
			}
			if (logicalType.TIMESTAMP != null)
			{
				return ParquetSummary.AsValue(logicalType.TIMESTAMP);
			}
			if (logicalType.INTEGER != null)
			{
				return ParquetSummary.AsValue(logicalType.INTEGER);
			}
			if (logicalType.UNKNOWN != null)
			{
				return RecordValue.New(Keys.New("LogicalType"), new Value[] { TextValue.New("UNKNOWN") });
			}
			if (logicalType.JSON != null)
			{
				return RecordValue.New(Keys.New("LogicalType"), new Value[] { TextValue.New("JSON") });
			}
			if (logicalType.BSON != null)
			{
				return RecordValue.New(Keys.New("LogicalType"), new Value[] { TextValue.New("BSON") });
			}
			if (logicalType.UUID != null)
			{
				return RecordValue.New(Keys.New("LogicalType"), new Value[] { TextValue.New("UUID") });
			}
			return RecordValue.New(Keys.New("LogicalType"), new Value[] { Value.Null });
		}

		// Token: 0x06010E36 RID: 69174 RVA: 0x003A3750 File Offset: 0x003A1950
		private static Value AsValue(DecimalType decimalType)
		{
			return RecordValue.New(Keys.New("LogicalType", "Precision", "Scale"), new Value[]
			{
				TextValue.New("DECIMAL"),
				ParquetSummary.AsValue(decimalType.Precision),
				ParquetSummary.AsValue(decimalType.Scale)
			});
		}

		// Token: 0x06010E37 RID: 69175 RVA: 0x003A37A8 File Offset: 0x003A19A8
		private static Value AsValue(TimeType timeType)
		{
			return RecordValue.New(Keys.New("LogicalType", "Unit", "IsAdjustedToUTC"), new Value[]
			{
				TextValue.New("TIME"),
				ParquetSummary.AsValue(timeType.Unit),
				ParquetSummary.AsValue(timeType.IsAdjustedToUTC)
			});
		}

		// Token: 0x06010E38 RID: 69176 RVA: 0x003A3800 File Offset: 0x003A1A00
		private static Value AsValue(TimestampType timestampType)
		{
			return RecordValue.New(Keys.New("LogicalType", "Unit", "IsAdjustedToUTC"), new Value[]
			{
				TextValue.New("TIMESTAMP"),
				ParquetSummary.AsValue(timestampType.Unit),
				ParquetSummary.AsValue(timestampType.IsAdjustedToUTC)
			});
		}

		// Token: 0x06010E39 RID: 69177 RVA: 0x003A3858 File Offset: 0x003A1A58
		private static Value AsValue(IntType integerType)
		{
			Keys keys = Keys.New("LogicalType", "BitWidth", "IsSigned");
			Value[] array = new Value[3];
			array[0] = TextValue.New("INTEGER");
			int num = 1;
			sbyte? bitWidth = integerType.BitWidth;
			array[num] = ParquetSummary.AsValue((bitWidth != null) ? new short?((short)bitWidth.GetValueOrDefault()) : null);
			array[2] = ParquetSummary.AsValue(integerType.IsSigned);
			return RecordValue.New(keys, array);
		}

		// Token: 0x06010E3A RID: 69178 RVA: 0x003A38D0 File Offset: 0x003A1AD0
		private static Value AsValue(TimeUnit timeUnit)
		{
			if (timeUnit == null)
			{
				return Value.Null;
			}
			if (timeUnit.MILLIS != null)
			{
				return RecordValue.New(Keys.New("TimeUnit"), new Value[] { TextValue.New("MILLIS") });
			}
			if (timeUnit.MICROS != null)
			{
				return RecordValue.New(Keys.New("TimeUnit"), new Value[] { TextValue.New("MICROS") });
			}
			if (timeUnit.NANOS != null)
			{
				return RecordValue.New(Keys.New("TimeUnit"), new Value[] { TextValue.New("NANOS") });
			}
			return RecordValue.New(Keys.New("TimeUnit"), new Value[] { Value.Null });
		}
	}
}
