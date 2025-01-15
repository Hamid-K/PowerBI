using System;
using System.Data;
using Microsoft.Data.Serialization;
using Microsoft.OleDb.Serialization;

namespace Microsoft.OleDb
{
	// Token: 0x02001E7D RID: 7805
	internal class TypedRecordColumn : Column
	{
		// Token: 0x0600C0BE RID: 49342 RVA: 0x0026C60E File Offset: 0x0026A80E
		public TypedRecordColumn(int maxRowCount, TableSchema schema)
		{
			this.maxRowCount = maxRowCount;
			this.schema = schema;
			this.columns = Column.CreateColumns(schema, maxRowCount);
		}

		// Token: 0x17002F20 RID: 12064
		// (get) Token: 0x0600C0BF RID: 49343 RVA: 0x0026C631 File Offset: 0x0026A831
		public override int RowCount
		{
			get
			{
				return this.maxRowCount;
			}
		}

		// Token: 0x17002F21 RID: 12065
		// (get) Token: 0x0600C0C0 RID: 49344 RVA: 0x00243BDB File Offset: 0x00241DDB
		public override ColumnType Type
		{
			get
			{
				return ColumnType.TypedRecord;
			}
		}

		// Token: 0x0600C0C1 RID: 49345 RVA: 0x0026C63C File Offset: 0x0026A83C
		public override void AddNull()
		{
			for (int i = 0; i < this.columns.Length; i++)
			{
				Column.AddValueToColumn(this.columns[i], DBNull.Value);
			}
		}

		// Token: 0x0600C0C2 RID: 49346 RVA: 0x0026C670 File Offset: 0x0026A870
		public override void AddValue(object value)
		{
			IDataRecord dataRecord = (IDataRecord)value;
			for (int i = 0; i < this.columns.Length; i++)
			{
				Column.AddValueToColumn(this.columns[i], dataRecord.GetValue(i));
			}
		}

		// Token: 0x0600C0C3 RID: 49347 RVA: 0x0026C6AB File Offset: 0x0026A8AB
		public override bool TryAddValue(object value)
		{
			if (value is IDataRecord)
			{
				this.AddValue(value);
				return true;
			}
			return false;
		}

		// Token: 0x0600C0C4 RID: 49348 RVA: 0x0000EE09 File Offset: 0x0000D009
		public unsafe override void AddValue(DBTYPE type, void* value, int length)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600C0C5 RID: 49349 RVA: 0x0026C6C0 File Offset: 0x0026A8C0
		public override void Clear()
		{
			Column[] array = this.columns;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Clear();
			}
		}

		// Token: 0x0600C0C6 RID: 49350 RVA: 0x0026C6EC File Offset: 0x0026A8EC
		public override void Serialize(PageWriter writer)
		{
			Column[] array = this.columns;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Serialize(writer);
			}
		}

		// Token: 0x0600C0C7 RID: 49351 RVA: 0x0026C718 File Offset: 0x0026A918
		public override void Deserialize(PageReader reader)
		{
			Column[] array = this.columns;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Deserialize(reader);
			}
		}

		// Token: 0x0600C0C8 RID: 49352 RVA: 0x0026C744 File Offset: 0x0026A944
		public override object GetObject(int row)
		{
			object[] array = new object[this.columns.Length];
			for (int i = 0; i < this.columns.Length; i++)
			{
				array[i] = this.columns[i].GetObject(row);
			}
			return new DataRecord(this.schema, array);
		}

		// Token: 0x0600C0C9 RID: 49353 RVA: 0x0026C790 File Offset: 0x0026A990
		public unsafe override DBSTATUS GetValue(int row, IManagedDataConvert dataConvert, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			if (binding.DestType == DBTYPE.STRUCTUREDENTITY && PageReaderStructuredEntityRowset.TryGetData(row, binding, destValue, dataConvert, (int i) => this.columns[i]))
			{
				destLength = binding.DestMaxLength;
				return DBSTATUS.S_OK;
			}
			destLength = DbLength.Zero;
			return DBSTATUS.E_CANTCONVERTVALUE;
		}

		// Token: 0x0600C0CA RID: 49354 RVA: 0x00002105 File Offset: 0x00000305
		public override bool IsNull(int row)
		{
			return false;
		}

		// Token: 0x0400614E RID: 24910
		private readonly int maxRowCount;

		// Token: 0x0400614F RID: 24911
		private readonly TableSchema schema;

		// Token: 0x04006150 RID: 24912
		private readonly Column[] columns;
	}
}
