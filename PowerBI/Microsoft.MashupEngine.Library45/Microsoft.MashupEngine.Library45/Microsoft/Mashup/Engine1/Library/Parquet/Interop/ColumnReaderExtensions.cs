using System;
using ParquetSharp;

namespace Microsoft.Mashup.Engine1.Library.Parquet.Interop
{
	// Token: 0x02001FE9 RID: 8169
	internal static class ColumnReaderExtensions
	{
		// Token: 0x060110D9 RID: 69849 RVA: 0x003AD164 File Offset: 0x003AB364
		public static long ReadBatch(this ColumnReader reader, long batchSize, ArraySegment values, out long valuesRead)
		{
			switch (reader.Type)
			{
			case PhysicalType.Boolean:
			{
				ColumnReader<bool> columnReader = (BoolColumnReader)reader;
				ArraySegment<bool> arraySegment = values.Cast<bool>();
				return columnReader.ReadBatch(batchSize, arraySegment, out valuesRead);
			}
			case PhysicalType.Int32:
			{
				ColumnReader<int> columnReader2 = (Int32ColumnReader)reader;
				ArraySegment<int> arraySegment2 = values.Cast<int>();
				return columnReader2.ReadBatch(batchSize, arraySegment2, out valuesRead);
			}
			case PhysicalType.Int64:
			{
				ColumnReader<long> columnReader3 = (Int64ColumnReader)reader;
				ArraySegment<long> arraySegment3 = values.Cast<long>();
				return columnReader3.ReadBatch(batchSize, arraySegment3, out valuesRead);
			}
			case PhysicalType.Int96:
			{
				ColumnReader<Int96> columnReader4 = (Int96ColumnReader)reader;
				ArraySegment<Int96> arraySegment4 = values.Cast<Int96>();
				return columnReader4.ReadBatch(batchSize, arraySegment4, out valuesRead);
			}
			case PhysicalType.Float:
			{
				ColumnReader<float> columnReader5 = (FloatColumnReader)reader;
				ArraySegment<float> arraySegment5 = values.Cast<float>();
				return columnReader5.ReadBatch(batchSize, arraySegment5, out valuesRead);
			}
			case PhysicalType.Double:
			{
				ColumnReader<double> columnReader6 = (DoubleColumnReader)reader;
				ArraySegment<double> arraySegment6 = values.Cast<double>();
				return columnReader6.ReadBatch(batchSize, arraySegment6, out valuesRead);
			}
			case PhysicalType.ByteArray:
			{
				ColumnReader<ByteArray> columnReader7 = (ByteArrayColumnReader)reader;
				ArraySegment<ByteArray> arraySegment7 = values.Cast<ByteArray>();
				return columnReader7.ReadBatch(batchSize, arraySegment7, out valuesRead);
			}
			case PhysicalType.FixedLenByteArray:
			{
				ColumnReader<FixedLenByteArray> columnReader8 = (FixedLenByteArrayColumnReader)reader;
				ArraySegment<FixedLenByteArray> arraySegment8 = values.Cast<FixedLenByteArray>();
				return columnReader8.ReadBatch(batchSize, arraySegment8, out valuesRead);
			}
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x060110DA RID: 69850 RVA: 0x003AD26C File Offset: 0x003AB46C
		public static long ReadBatch(this ColumnReader reader, long batchSize, ArraySegment<short>? defLevels, ArraySegment<short>? repLevels, ArraySegment values, out long valuesRead)
		{
			switch (reader.Type)
			{
			case PhysicalType.Boolean:
			{
				ColumnReader<bool> columnReader = (BoolColumnReader)reader;
				ArraySegment<bool> arraySegment = values.Cast<bool>();
				return columnReader.ReadBatch(batchSize, defLevels, repLevels, arraySegment, out valuesRead);
			}
			case PhysicalType.Int32:
			{
				ColumnReader<int> columnReader2 = (Int32ColumnReader)reader;
				ArraySegment<int> arraySegment2 = values.Cast<int>();
				return columnReader2.ReadBatch(batchSize, defLevels, repLevels, arraySegment2, out valuesRead);
			}
			case PhysicalType.Int64:
			{
				ColumnReader<long> columnReader3 = (Int64ColumnReader)reader;
				ArraySegment<long> arraySegment3 = values.Cast<long>();
				return columnReader3.ReadBatch(batchSize, defLevels, repLevels, arraySegment3, out valuesRead);
			}
			case PhysicalType.Int96:
			{
				ColumnReader<Int96> columnReader4 = (Int96ColumnReader)reader;
				ArraySegment<Int96> arraySegment4 = values.Cast<Int96>();
				return columnReader4.ReadBatch(batchSize, defLevels, repLevels, arraySegment4, out valuesRead);
			}
			case PhysicalType.Float:
			{
				ColumnReader<float> columnReader5 = (FloatColumnReader)reader;
				ArraySegment<float> arraySegment5 = values.Cast<float>();
				return columnReader5.ReadBatch(batchSize, defLevels, repLevels, arraySegment5, out valuesRead);
			}
			case PhysicalType.Double:
			{
				ColumnReader<double> columnReader6 = (DoubleColumnReader)reader;
				ArraySegment<double> arraySegment6 = values.Cast<double>();
				return columnReader6.ReadBatch(batchSize, defLevels, repLevels, arraySegment6, out valuesRead);
			}
			case PhysicalType.ByteArray:
			{
				ColumnReader<ByteArray> columnReader7 = (ByteArrayColumnReader)reader;
				ArraySegment<ByteArray> arraySegment7 = values.Cast<ByteArray>();
				return columnReader7.ReadBatch(batchSize, defLevels, repLevels, arraySegment7, out valuesRead);
			}
			case PhysicalType.FixedLenByteArray:
			{
				ColumnReader<FixedLenByteArray> columnReader8 = (FixedLenByteArrayColumnReader)reader;
				ArraySegment<FixedLenByteArray> arraySegment8 = values.Cast<FixedLenByteArray>();
				return columnReader8.ReadBatch(batchSize, defLevels, repLevels, arraySegment8, out valuesRead);
			}
			default:
				throw new InvalidOperationException();
			}
		}
	}
}
