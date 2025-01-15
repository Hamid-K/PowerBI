using System;
using ParquetSharp;

namespace Microsoft.Mashup.Engine1.Library.Parquet.Interop
{
	// Token: 0x02001FEA RID: 8170
	internal static class ColumnWriterExtensions
	{
		// Token: 0x060110DB RID: 69851 RVA: 0x003AD38C File Offset: 0x003AB58C
		public static void WriteBatch(this ColumnWriter writer, int numValues, ArraySegment values)
		{
			switch (writer.Type)
			{
			case PhysicalType.Boolean:
			{
				ColumnWriter<bool> columnWriter = (BoolColumnWriter)writer;
				ArraySegment<bool> arraySegment = values.Cast<bool>();
				columnWriter.WriteBatch(numValues, arraySegment);
				return;
			}
			case PhysicalType.Int32:
			{
				ColumnWriter<int> columnWriter2 = (Int32ColumnWriter)writer;
				ArraySegment<int> arraySegment2 = values.Cast<int>();
				columnWriter2.WriteBatch(numValues, arraySegment2);
				return;
			}
			case PhysicalType.Int64:
			{
				ColumnWriter<long> columnWriter3 = (Int64ColumnWriter)writer;
				ArraySegment<long> arraySegment3 = values.Cast<long>();
				columnWriter3.WriteBatch(numValues, arraySegment3);
				return;
			}
			case PhysicalType.Int96:
			{
				ColumnWriter<Int96> columnWriter4 = (Int96ColumnWriter)writer;
				ArraySegment<Int96> arraySegment4 = values.Cast<Int96>();
				columnWriter4.WriteBatch(numValues, arraySegment4);
				return;
			}
			case PhysicalType.Float:
			{
				ColumnWriter<float> columnWriter5 = (FloatColumnWriter)writer;
				ArraySegment<float> arraySegment5 = values.Cast<float>();
				columnWriter5.WriteBatch(numValues, arraySegment5);
				return;
			}
			case PhysicalType.Double:
			{
				ColumnWriter<double> columnWriter6 = (DoubleColumnWriter)writer;
				ArraySegment<double> arraySegment6 = values.Cast<double>();
				columnWriter6.WriteBatch(numValues, arraySegment6);
				return;
			}
			case PhysicalType.ByteArray:
			{
				ColumnWriter<ByteArray> columnWriter7 = (ByteArrayColumnWriter)writer;
				ArraySegment<ByteArray> arraySegment7 = values.Cast<ByteArray>();
				columnWriter7.WriteBatch(numValues, arraySegment7);
				return;
			}
			case PhysicalType.FixedLenByteArray:
			{
				ColumnWriter<FixedLenByteArray> columnWriter8 = (FixedLenByteArrayColumnWriter)writer;
				ArraySegment<FixedLenByteArray> arraySegment8 = values.Cast<FixedLenByteArray>();
				columnWriter8.WriteBatch(numValues, arraySegment8);
				return;
			}
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x060110DC RID: 69852 RVA: 0x003AD48C File Offset: 0x003AB68C
		public static void WriteBatch(this ColumnWriter writer, int numValues, ArraySegment<short>? defLevels, ArraySegment<short>? repLevels, ArraySegment values)
		{
			switch (writer.Type)
			{
			case PhysicalType.Boolean:
			{
				ColumnWriter<bool> columnWriter = (BoolColumnWriter)writer;
				ArraySegment<bool> arraySegment = values.Cast<bool>();
				columnWriter.WriteBatch(numValues, defLevels, repLevels, arraySegment);
				return;
			}
			case PhysicalType.Int32:
			{
				ColumnWriter<int> columnWriter2 = (Int32ColumnWriter)writer;
				ArraySegment<int> arraySegment2 = values.Cast<int>();
				columnWriter2.WriteBatch(numValues, defLevels, repLevels, arraySegment2);
				return;
			}
			case PhysicalType.Int64:
			{
				ColumnWriter<long> columnWriter3 = (Int64ColumnWriter)writer;
				ArraySegment<long> arraySegment3 = values.Cast<long>();
				columnWriter3.WriteBatch(numValues, defLevels, repLevels, arraySegment3);
				return;
			}
			case PhysicalType.Int96:
			{
				ColumnWriter<Int96> columnWriter4 = (Int96ColumnWriter)writer;
				ArraySegment<Int96> arraySegment4 = values.Cast<Int96>();
				columnWriter4.WriteBatch(numValues, defLevels, repLevels, arraySegment4);
				return;
			}
			case PhysicalType.Float:
			{
				ColumnWriter<float> columnWriter5 = (FloatColumnWriter)writer;
				ArraySegment<float> arraySegment5 = values.Cast<float>();
				columnWriter5.WriteBatch(numValues, defLevels, repLevels, arraySegment5);
				return;
			}
			case PhysicalType.Double:
			{
				ColumnWriter<double> columnWriter6 = (DoubleColumnWriter)writer;
				ArraySegment<double> arraySegment6 = values.Cast<double>();
				columnWriter6.WriteBatch(numValues, defLevels, repLevels, arraySegment6);
				return;
			}
			case PhysicalType.ByteArray:
			{
				ColumnWriter<ByteArray> columnWriter7 = (ByteArrayColumnWriter)writer;
				ArraySegment<ByteArray> arraySegment7 = values.Cast<ByteArray>();
				columnWriter7.WriteBatch(numValues, defLevels, repLevels, arraySegment7);
				return;
			}
			case PhysicalType.FixedLenByteArray:
			{
				ColumnWriter<FixedLenByteArray> columnWriter8 = (FixedLenByteArrayColumnWriter)writer;
				ArraySegment<FixedLenByteArray> arraySegment8 = values.Cast<FixedLenByteArray>();
				columnWriter8.WriteBatch(numValues, defLevels, repLevels, arraySegment8);
				return;
			}
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x060110DD RID: 69853 RVA: 0x003AD59C File Offset: 0x003AB79C
		public static void WriteBatchSpaced(this ColumnWriter writer, int numValues, ArraySegment<short> defLevels, ArraySegment<short> repLevels, ArraySegment<byte> validBits, long validBitsOffset, ArraySegment values)
		{
			switch (writer.Type)
			{
			case PhysicalType.Boolean:
			{
				ColumnWriter<bool> columnWriter = (BoolColumnWriter)writer;
				ArraySegment<bool> arraySegment = values.Cast<bool>();
				columnWriter.WriteBatchSpaced(numValues, defLevels, repLevels, validBits, validBitsOffset, arraySegment);
				return;
			}
			case PhysicalType.Int32:
			{
				ColumnWriter<int> columnWriter2 = (Int32ColumnWriter)writer;
				ArraySegment<int> arraySegment2 = values.Cast<int>();
				columnWriter2.WriteBatchSpaced(numValues, defLevels, repLevels, validBits, validBitsOffset, arraySegment2);
				return;
			}
			case PhysicalType.Int64:
			{
				ColumnWriter<long> columnWriter3 = (Int64ColumnWriter)writer;
				ArraySegment<long> arraySegment3 = values.Cast<long>();
				columnWriter3.WriteBatchSpaced(numValues, defLevels, repLevels, validBits, validBitsOffset, arraySegment3);
				return;
			}
			case PhysicalType.Int96:
			{
				ColumnWriter<Int96> columnWriter4 = (Int96ColumnWriter)writer;
				ArraySegment<Int96> arraySegment4 = values.Cast<Int96>();
				columnWriter4.WriteBatchSpaced(numValues, defLevels, repLevels, validBits, validBitsOffset, arraySegment4);
				return;
			}
			case PhysicalType.Float:
			{
				ColumnWriter<float> columnWriter5 = (FloatColumnWriter)writer;
				ArraySegment<float> arraySegment5 = values.Cast<float>();
				columnWriter5.WriteBatchSpaced(numValues, defLevels, repLevels, validBits, validBitsOffset, arraySegment5);
				return;
			}
			case PhysicalType.Double:
			{
				ColumnWriter<double> columnWriter6 = (DoubleColumnWriter)writer;
				ArraySegment<double> arraySegment6 = values.Cast<double>();
				columnWriter6.WriteBatchSpaced(numValues, defLevels, repLevels, validBits, validBitsOffset, arraySegment6);
				return;
			}
			case PhysicalType.ByteArray:
			{
				ColumnWriter<ByteArray> columnWriter7 = (ByteArrayColumnWriter)writer;
				ArraySegment<ByteArray> arraySegment7 = values.Cast<ByteArray>();
				columnWriter7.WriteBatchSpaced(numValues, defLevels, repLevels, validBits, validBitsOffset, arraySegment7);
				return;
			}
			case PhysicalType.FixedLenByteArray:
			{
				ColumnWriter<FixedLenByteArray> columnWriter8 = (FixedLenByteArrayColumnWriter)writer;
				ArraySegment<FixedLenByteArray> arraySegment8 = values.Cast<FixedLenByteArray>();
				columnWriter8.WriteBatchSpaced(numValues, defLevels, repLevels, validBits, validBitsOffset, arraySegment8);
				return;
			}
			default:
				throw new InvalidOperationException();
			}
		}
	}
}
