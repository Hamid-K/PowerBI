using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Library.Parquet.Schema;
using Microsoft.Mashup.Engine1.Runtime;
using ParquetSharp;

namespace Microsoft.Mashup.Engine1.Library.Parquet
{
	// Token: 0x02001F47 RID: 8007
	internal class ParquetRecordEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
	{
		// Token: 0x06010D20 RID: 68896 RVA: 0x0039EFFB File Offset: 0x0039D1FB
		private ParquetRecordEnumerator(ParquetRecordReader reader)
		{
			this.reader = reader;
		}

		// Token: 0x06010D21 RID: 68897 RVA: 0x0039F00A File Offset: 0x0039D20A
		public static ParquetRecordEnumerator New(StreamOwningParquetFileReader fileReader, SchemaElement schema, int[] columnSelection, RowCount skipCount)
		{
			return new ParquetRecordEnumerator(ParquetRecordReader.New(fileReader, schema, columnSelection, skipCount));
		}

		// Token: 0x17002C89 RID: 11401
		// (get) Token: 0x06010D22 RID: 68898 RVA: 0x0039F01A File Offset: 0x0039D21A
		public IValueReference Current
		{
			get
			{
				return this.currentValue;
			}
		}

		// Token: 0x17002C8A RID: 11402
		// (get) Token: 0x06010D23 RID: 68899 RVA: 0x0039F022 File Offset: 0x0039D222
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x06010D24 RID: 68900 RVA: 0x0039F02C File Offset: 0x0039D22C
		public bool MoveNext()
		{
			this.reader.Read();
			ParquetRecordReaderState state = this.reader.State;
			if (state == ParquetRecordReaderState.End)
			{
				this.currentValue = null;
				return false;
			}
			if (state == ParquetRecordReaderState.RecordStart)
			{
				this.currentValue = this.reader.CurrentSchemaElement.ToValue(this.ReadRecord()).AsRecord;
				return true;
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06010D25 RID: 68901 RVA: 0x0039F089 File Offset: 0x0039D289
		public void Dispose()
		{
			this.reader.Dispose();
		}

		// Token: 0x06010D26 RID: 68902 RVA: 0x001D2D64 File Offset: 0x001D0F64
		public void Reset()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06010D27 RID: 68903 RVA: 0x0039F098 File Offset: 0x0039D298
		private RecordValue ReadRecord()
		{
			GroupSchemaElement groupSchemaElement = (GroupSchemaElement)this.reader.CurrentSchemaElement;
			Keys fieldKeys = groupSchemaElement.FieldKeys;
			Value[] array = new Value[fieldKeys.Length];
			this.reader.Read();
			while (this.reader.State != ParquetRecordReaderState.RecordEnd && this.reader.State != ParquetRecordReaderState.NestedRecordEnd)
			{
				string currentFieldName = this.reader.CurrentFieldName;
				int num = fieldKeys.IndexOfKey(currentFieldName);
				array[num] = this.ReadValue();
				this.reader.Read();
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == null)
				{
					Repetition repetition = groupSchemaElement.Fields[i].Repetition;
					if (repetition > Repetition.Optional)
					{
						if (repetition != Repetition.Repeated)
						{
							throw new InvalidOperationException();
						}
						if (groupSchemaElement.Fields[i].TypeValue.IsTableType)
						{
							array[i] = ListValue.Empty.ToTable(groupSchemaElement.Fields[i].TypeValue.AsTableType);
						}
						else
						{
							array[i] = ListValue.Empty;
						}
					}
					else
					{
						array[i] = Value.Null;
					}
				}
			}
			return RecordValue.New(groupSchemaElement.RecordTypeValue, array);
		}

		// Token: 0x06010D28 RID: 68904 RVA: 0x0039F1B4 File Offset: 0x0039D3B4
		private Value ReadList()
		{
			TypeValue typeValue = this.reader.CurrentSchemaElement.TypeValue;
			List<IValueReference> list = new List<IValueReference>();
			this.reader.Read();
			while (this.reader.State != ParquetRecordReaderState.NestedListEnd)
			{
				list.Add(this.ReadValue());
				this.reader.Read();
			}
			ListValue listValue = ListValue.New(list);
			if (typeValue.IsTableType)
			{
				return listValue.ToTable(typeValue.AsTableType);
			}
			return listValue;
		}

		// Token: 0x06010D29 RID: 68905 RVA: 0x0039F227 File Offset: 0x0039D427
		private Value ReadPrimitive()
		{
			return this.reader.CurrentFieldValue;
		}

		// Token: 0x06010D2A RID: 68906 RVA: 0x0039F234 File Offset: 0x0039D434
		private Value ReadValue()
		{
			switch (this.reader.State)
			{
			case ParquetRecordReaderState.NestedRecordStart:
				return this.reader.CurrentSchemaElement.ToValue(this.ReadRecord());
			case ParquetRecordReaderState.NestedListStart:
				return this.ReadList();
			case ParquetRecordReaderState.Primitive:
				return this.ReadPrimitive();
			}
			throw new InvalidOperationException();
		}

		// Token: 0x040064E6 RID: 25830
		private readonly ParquetRecordReader reader;

		// Token: 0x040064E7 RID: 25831
		private RecordValue currentValue;
	}
}
