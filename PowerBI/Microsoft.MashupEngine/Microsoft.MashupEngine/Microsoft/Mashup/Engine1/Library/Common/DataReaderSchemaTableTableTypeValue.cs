using System;
using System.Data;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001047 RID: 4167
	internal static class DataReaderSchemaTableTableTypeValue
	{
		// Token: 0x06006CCA RID: 27850 RVA: 0x00176464 File Offset: 0x00174664
		public static TableTypeValue New(IDataReader dataReader)
		{
			return DataReaderSchemaTableTableTypeValue.New(TableSchema.FromDataReader(dataReader));
		}

		// Token: 0x06006CCB RID: 27851 RVA: 0x00176474 File Offset: 0x00174674
		public static TableTypeValue New(TableSchema schema)
		{
			int columnCount = schema.ColumnCount;
			RecordBuilder recordBuilder = new RecordBuilder(columnCount);
			for (int i = 0; i < columnCount; i++)
			{
				SchemaColumn column = schema.GetColumn(i);
				TypeValue typeValue = ValueMarshaller.GetMType(column.DataType);
				if (column.Nullable)
				{
					typeValue = typeValue.Nullable;
				}
				recordBuilder.Add(column.Name, RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
				{
					typeValue,
					LogicalValue.False
				}), TypeValue.Record);
			}
			return TableTypeValue.New(RecordTypeValue.New(recordBuilder.ToRecord()));
		}
	}
}
