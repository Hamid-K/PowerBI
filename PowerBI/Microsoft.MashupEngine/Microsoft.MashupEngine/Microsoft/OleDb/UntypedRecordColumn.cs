using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.Serialization;
using Microsoft.OleDb.Serialization;

namespace Microsoft.OleDb
{
	// Token: 0x02001E7E RID: 7806
	internal class UntypedRecordColumn : Column
	{
		// Token: 0x0600C0CC RID: 49356 RVA: 0x0026C7E8 File Offset: 0x0026A9E8
		public UntypedRecordColumn(bool hasMetadata, int maxRowCount)
		{
			this.maxRowCount = maxRowCount;
			this.counts = new Int32Column(maxRowCount);
			this.names = new List<StringColumn>();
			this.values = new List<Column>();
			this.hasMetadata = hasMetadata;
			this.AddPage();
		}

		// Token: 0x17002F22 RID: 12066
		// (get) Token: 0x0600C0CD RID: 49357 RVA: 0x00243C33 File Offset: 0x00241E33
		public override ColumnType Type
		{
			get
			{
				return ColumnType.UntypedRecord;
			}
		}

		// Token: 0x17002F23 RID: 12067
		// (get) Token: 0x0600C0CE RID: 49358 RVA: 0x0026C826 File Offset: 0x0026AA26
		public override int RowCount
		{
			get
			{
				return this.counts.RowCount;
			}
		}

		// Token: 0x0600C0CF RID: 49359 RVA: 0x00002105 File Offset: 0x00000305
		public override bool IsNull(int row)
		{
			return false;
		}

		// Token: 0x0600C0D0 RID: 49360 RVA: 0x0026C833 File Offset: 0x0026AA33
		public override void AddNull()
		{
			this.AddValue(DataRecord.Empty);
		}

		// Token: 0x0600C0D1 RID: 49361 RVA: 0x0026C6AB File Offset: 0x0026A8AB
		public override bool TryAddValue(object value)
		{
			if (value is IDataRecord)
			{
				this.AddValue(value);
				return true;
			}
			return false;
		}

		// Token: 0x0600C0D2 RID: 49362 RVA: 0x0026C840 File Offset: 0x0026AA40
		public override void AddValue(object value)
		{
			IDataRecord dataRecord = (IDataRecord)value;
			int num = this.maxRowCount * this.currentPageIndex + this.currentPageSize;
			for (int i = 0; i < dataRecord.FieldCount; i++)
			{
				this.AddValue(dataRecord.GetName(i), dataRecord.GetValue(i));
			}
			this.counts.AddInt32(num + dataRecord.FieldCount);
		}

		// Token: 0x0600C0D3 RID: 49363 RVA: 0x0026C8A4 File Offset: 0x0026AAA4
		private void AddValue(string name, object value)
		{
			if (this.currentPageSize == this.maxRowCount)
			{
				this.currentPageIndex++;
				this.currentPageSize = 0;
			}
			if (this.currentPageIndex == this.names.Count)
			{
				this.AddPage();
			}
			this.names[this.currentPageIndex].AddValue(name);
			this.values[this.currentPageIndex].AddValue(value);
			this.currentPageSize++;
		}

		// Token: 0x0600C0D4 RID: 49364 RVA: 0x0026C929 File Offset: 0x0026AB29
		private void AddPage()
		{
			this.names.Add(new StringColumn(this.maxRowCount));
			this.values.Add(Column.AugmentColumn(new ObjectColumn(this.maxRowCount), true, this.hasMetadata, this.maxRowCount));
		}

		// Token: 0x0600C0D5 RID: 49365 RVA: 0x0000EE09 File Offset: 0x0000D009
		public unsafe override void AddValue(DBTYPE type, void* value, int length)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600C0D6 RID: 49366 RVA: 0x0026C96C File Offset: 0x0026AB6C
		public override void Clear()
		{
			this.counts.Clear();
			foreach (StringColumn stringColumn in this.names)
			{
				stringColumn.Clear();
			}
			foreach (Column column in this.values)
			{
				column.Clear();
			}
			this.currentPageSize = 0;
			this.currentPageIndex = 0;
		}

		// Token: 0x0600C0D7 RID: 49367 RVA: 0x0026CA14 File Offset: 0x0026AC14
		public IDataRecord GetRecord(int row)
		{
			int num = ((row == 0) ? 0 : this.counts.GetInt32(row - 1));
			int @int = this.counts.GetInt32(row);
			string[] array = new string[@int - num];
			object[] array2 = new object[@int - num];
			int num2 = num / this.maxRowCount;
			int num3 = num % this.maxRowCount;
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.names[num2].GetString(num3);
				array2[i] = this.values[num2].GetObject(num3);
				num3++;
				if (num3 == this.maxRowCount)
				{
					num2++;
					num3 = 0;
				}
			}
			return new DataRecord(array, array2);
		}

		// Token: 0x0600C0D8 RID: 49368 RVA: 0x0026CAC4 File Offset: 0x0026ACC4
		public override object GetObject(int row)
		{
			return this.GetRecord(row);
		}

		// Token: 0x0600C0D9 RID: 49369 RVA: 0x0026CACD File Offset: 0x0026ACCD
		public unsafe override DBSTATUS GetValue(int row, IManagedDataConvert dataConvert, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			destLength = DbLength.Zero;
			return DBSTATUS.E_CANTCONVERTVALUE;
		}

		// Token: 0x0600C0DA RID: 49370 RVA: 0x0026CADC File Offset: 0x0026ACDC
		public bool TryGetColumn(int row, string name, out Column value, out int newRow)
		{
			int i = ((row == 0) ? 0 : this.counts.GetInt32(row - 1));
			int @int = this.counts.GetInt32(row);
			int num = i / this.maxRowCount;
			int num2 = i % this.maxRowCount;
			while (i < @int)
			{
				if (this.names[num].GetString(num2) == name)
				{
					value = this.values[num];
					newRow = num2;
					return true;
				}
				i++;
				num2++;
				if (num2 == this.maxRowCount)
				{
					num++;
					num2 = 0;
				}
			}
			value = UntypedRecordColumn.nullColumn;
			newRow = 0;
			return false;
		}

		// Token: 0x0600C0DB RID: 49371 RVA: 0x0026CB74 File Offset: 0x0026AD74
		public override void Serialize(PageWriter writer)
		{
			this.counts.Serialize(writer);
			writer.WriteInt32(this.currentPageIndex);
			for (int i = 0; i <= this.currentPageIndex; i++)
			{
				this.names[i].Serialize(writer);
				this.values[i].Serialize(writer);
			}
		}

		// Token: 0x0600C0DC RID: 49372 RVA: 0x0026CBD0 File Offset: 0x0026ADD0
		public override void Deserialize(PageReader reader)
		{
			this.counts.Deserialize(reader);
			this.currentPageIndex = reader.ReadInt32();
			while (this.names.Count <= this.currentPageIndex)
			{
				this.AddPage();
			}
			for (int i = 0; i <= this.currentPageIndex; i++)
			{
				this.names[i].Deserialize(reader);
				this.values[i].Deserialize(reader);
			}
			this.currentPageSize = this.names[this.currentPageIndex].RowCount;
		}

		// Token: 0x04006151 RID: 24913
		private static readonly Column nullColumn = new NullColumn(1);

		// Token: 0x04006152 RID: 24914
		private readonly int maxRowCount;

		// Token: 0x04006153 RID: 24915
		private readonly Int32Column counts;

		// Token: 0x04006154 RID: 24916
		private readonly List<StringColumn> names;

		// Token: 0x04006155 RID: 24917
		private readonly List<Column> values;

		// Token: 0x04006156 RID: 24918
		private readonly bool hasMetadata;

		// Token: 0x04006157 RID: 24919
		private int currentPageSize;

		// Token: 0x04006158 RID: 24920
		private int currentPageIndex;
	}
}
