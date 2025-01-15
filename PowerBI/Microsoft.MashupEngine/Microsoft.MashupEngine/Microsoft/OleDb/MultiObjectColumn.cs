using System;
using System.Collections.Generic;
using Microsoft.OleDb.Serialization;

namespace Microsoft.OleDb
{
	// Token: 0x02001E7C RID: 7804
	internal class MultiObjectColumn : Column
	{
		// Token: 0x0600C090 RID: 49296 RVA: 0x0026C012 File Offset: 0x0026A212
		public MultiObjectColumn(int maxRowCount)
		{
			this.dictionary = new Dictionary<ColumnType, int>();
			this.columns = new List<Column>();
			this.columnIndices = new int[maxRowCount];
			this.rowIndices = new int[maxRowCount];
			this.rowCount = 0;
		}

		// Token: 0x17002F1E RID: 12062
		// (get) Token: 0x0600C091 RID: 49297 RVA: 0x002436D1 File Offset: 0x002418D1
		public override ColumnType Type
		{
			get
			{
				return ColumnType.Object;
			}
		}

		// Token: 0x17002F1F RID: 12063
		// (get) Token: 0x0600C092 RID: 49298 RVA: 0x0026C04F File Offset: 0x0026A24F
		public override int RowCount
		{
			get
			{
				return this.rowCount;
			}
		}

		// Token: 0x0600C093 RID: 49299 RVA: 0x0026C057 File Offset: 0x0026A257
		public override void Clear()
		{
			this.columns.Clear();
			this.dictionary.Clear();
			this.rowCount = 0;
		}

		// Token: 0x0600C094 RID: 49300 RVA: 0x0026C078 File Offset: 0x0026A278
		public override void AddValue(object value)
		{
			ColumnType columnType = Column.GetColumnType(value.GetType());
			this.PrepareForAdd(columnType).AddValue(value);
		}

		// Token: 0x0600C095 RID: 49301 RVA: 0x0026C09E File Offset: 0x0026A29E
		public override void AddBoolean(bool value)
		{
			this.PrepareForAdd(ColumnType.Boolean).AddBoolean(value);
		}

		// Token: 0x0600C096 RID: 49302 RVA: 0x0026C0AD File Offset: 0x0026A2AD
		public override void AddByte(byte value)
		{
			this.PrepareForAdd(ColumnType.Byte).AddByte(value);
		}

		// Token: 0x0600C097 RID: 49303 RVA: 0x0026C0BC File Offset: 0x0026A2BC
		public override void AddUInt16(ushort value)
		{
			this.PrepareForAdd(ColumnType.UInt16).AddUInt16(value);
		}

		// Token: 0x0600C098 RID: 49304 RVA: 0x0026C0CB File Offset: 0x0026A2CB
		public override void AddUInt32(uint value)
		{
			this.PrepareForAdd(ColumnType.UInt32).AddUInt32(value);
		}

		// Token: 0x0600C099 RID: 49305 RVA: 0x0026C0DA File Offset: 0x0026A2DA
		public override void AddUInt64(ulong value)
		{
			this.PrepareForAdd(ColumnType.UInt64).AddUInt64(value);
		}

		// Token: 0x0600C09A RID: 49306 RVA: 0x0026C0E9 File Offset: 0x0026A2E9
		public override void AddSByte(sbyte value)
		{
			this.PrepareForAdd(ColumnType.SByte).AddSByte(value);
		}

		// Token: 0x0600C09B RID: 49307 RVA: 0x0026C0F8 File Offset: 0x0026A2F8
		public override void AddInt16(short value)
		{
			this.PrepareForAdd(ColumnType.Int16).AddInt16(value);
		}

		// Token: 0x0600C09C RID: 49308 RVA: 0x0026C107 File Offset: 0x0026A307
		public override void AddInt32(int value)
		{
			this.PrepareForAdd(ColumnType.Int32).AddInt32(value);
		}

		// Token: 0x0600C09D RID: 49309 RVA: 0x0026C116 File Offset: 0x0026A316
		public override void AddInt64(long value)
		{
			this.PrepareForAdd(ColumnType.Int64).AddInt64(value);
		}

		// Token: 0x0600C09E RID: 49310 RVA: 0x0026C125 File Offset: 0x0026A325
		public override void AddFloat(float value)
		{
			this.PrepareForAdd(ColumnType.Single).AddFloat(value);
		}

		// Token: 0x0600C09F RID: 49311 RVA: 0x0026C135 File Offset: 0x0026A335
		public override void AddDouble(double value)
		{
			this.PrepareForAdd(ColumnType.Double).AddDouble(value);
		}

		// Token: 0x0600C0A0 RID: 49312 RVA: 0x0026C145 File Offset: 0x0026A345
		public override void AddDecimal(decimal value)
		{
			this.PrepareForAdd(ColumnType.Decimal).AddDecimal(value);
		}

		// Token: 0x0600C0A1 RID: 49313 RVA: 0x0026C155 File Offset: 0x0026A355
		public override void AddNumber(Number value)
		{
			this.PrepareForAdd(ColumnType.Numeric).AddNumber(value);
		}

		// Token: 0x0600C0A2 RID: 49314 RVA: 0x0026C165 File Offset: 0x0026A365
		public override void AddCurrency(Currency value)
		{
			this.PrepareForAdd(ColumnType.Currency).AddCurrency(value);
		}

		// Token: 0x0600C0A3 RID: 49315 RVA: 0x0026C175 File Offset: 0x0026A375
		public override void AddDate(Date value)
		{
			this.PrepareForAdd(ColumnType.Date).AddDate(value);
		}

		// Token: 0x0600C0A4 RID: 49316 RVA: 0x0026C185 File Offset: 0x0026A385
		public override void AddTime(Time value)
		{
			this.PrepareForAdd(ColumnType.Time).AddTime(value);
		}

		// Token: 0x0600C0A5 RID: 49317 RVA: 0x0026C195 File Offset: 0x0026A395
		public override void AddDateTime(DateTime value)
		{
			this.PrepareForAdd(ColumnType.DateTime).AddDateTime(value);
		}

		// Token: 0x0600C0A6 RID: 49318 RVA: 0x0026C1A5 File Offset: 0x0026A3A5
		public override void AddDateTimeOffset(DateTimeOffset value)
		{
			this.PrepareForAdd(ColumnType.DateTimeOffset).AddDateTimeOffset(value);
		}

		// Token: 0x0600C0A7 RID: 49319 RVA: 0x0026C1B5 File Offset: 0x0026A3B5
		public override void AddTimeSpan(TimeSpan value)
		{
			this.PrepareForAdd(ColumnType.TimeSpan).AddTimeSpan(value);
		}

		// Token: 0x0600C0A8 RID: 49320 RVA: 0x0026C1C5 File Offset: 0x0026A3C5
		public override void AddGuid(Guid value)
		{
			this.PrepareForAdd(ColumnType.Guid).AddGuid(value);
		}

		// Token: 0x0600C0A9 RID: 49321 RVA: 0x0026C1D8 File Offset: 0x0026A3D8
		public unsafe override void AddValue(DBTYPE type, void* value, int length)
		{
			ColumnType columnType = Column.GetColumnType(type);
			this.PrepareForAdd(columnType).AddValue(type, value, length);
		}

		// Token: 0x0600C0AA RID: 49322 RVA: 0x0026C1FB File Offset: 0x0026A3FB
		public override bool TryAddValue(object value)
		{
			this.AddValue(value);
			return true;
		}

		// Token: 0x0600C0AB RID: 49323 RVA: 0x0026C205 File Offset: 0x0026A405
		public override void AddNull()
		{
			this.columnIndices[this.rowCount] = -1;
			this.rowIndices[this.rowCount] = -1;
			this.rowCount++;
		}

		// Token: 0x0600C0AC RID: 49324 RVA: 0x0026C234 File Offset: 0x0026A434
		private int GetColumnIndex(ColumnType columnType)
		{
			int count;
			if (!this.dictionary.TryGetValue(columnType, out count))
			{
				count = this.dictionary.Count;
				this.dictionary.Add(columnType, count);
				this.columns.Add(Column.Create(columnType, false, false, this.rowIndices.Length));
			}
			return count;
		}

		// Token: 0x0600C0AD RID: 49325 RVA: 0x0026C288 File Offset: 0x0026A488
		private Column PrepareForAdd(ColumnType columnType)
		{
			int columnIndex = this.GetColumnIndex(columnType);
			Column column = this.columns[columnIndex];
			int num = column.RowCount;
			this.columnIndices[this.rowCount] = columnIndex;
			this.rowIndices[this.rowCount] = num;
			this.rowCount++;
			return column;
		}

		// Token: 0x0600C0AE RID: 49326 RVA: 0x0026C2DA File Offset: 0x0026A4DA
		public override bool GetBoolean(int row)
		{
			return this.columns[this.columnIndices[row]].GetBoolean(this.rowIndices[row]);
		}

		// Token: 0x0600C0AF RID: 49327 RVA: 0x0026C2FC File Offset: 0x0026A4FC
		public override byte GetByte(int row)
		{
			return this.columns[this.columnIndices[row]].GetByte(this.rowIndices[row]);
		}

		// Token: 0x0600C0B0 RID: 49328 RVA: 0x0026C31E File Offset: 0x0026A51E
		public override short GetInt16(int row)
		{
			return this.columns[this.columnIndices[row]].GetInt16(this.rowIndices[row]);
		}

		// Token: 0x0600C0B1 RID: 49329 RVA: 0x0026C340 File Offset: 0x0026A540
		public override int GetInt32(int row)
		{
			return this.columns[this.columnIndices[row]].GetInt32(this.rowIndices[row]);
		}

		// Token: 0x0600C0B2 RID: 49330 RVA: 0x0026C362 File Offset: 0x0026A562
		public override long GetInt64(int row)
		{
			return this.columns[this.columnIndices[row]].GetInt64(this.rowIndices[row]);
		}

		// Token: 0x0600C0B3 RID: 49331 RVA: 0x0026C384 File Offset: 0x0026A584
		public override float GetFloat(int row)
		{
			return this.columns[this.columnIndices[row]].GetFloat(this.rowIndices[row]);
		}

		// Token: 0x0600C0B4 RID: 49332 RVA: 0x0026C3A6 File Offset: 0x0026A5A6
		public override Guid GetGuid(int row)
		{
			return this.columns[this.columnIndices[row]].GetGuid(this.rowIndices[row]);
		}

		// Token: 0x0600C0B5 RID: 49333 RVA: 0x0026C3C8 File Offset: 0x0026A5C8
		public override double GetDouble(int row)
		{
			return this.columns[this.columnIndices[row]].GetDouble(this.rowIndices[row]);
		}

		// Token: 0x0600C0B6 RID: 49334 RVA: 0x0026C3EA File Offset: 0x0026A5EA
		public override decimal GetDecimal(int row)
		{
			return this.columns[this.columnIndices[row]].GetDecimal(this.rowIndices[row]);
		}

		// Token: 0x0600C0B7 RID: 49335 RVA: 0x0026C40C File Offset: 0x0026A60C
		public override DateTime GetDateTime(int row)
		{
			return this.columns[this.columnIndices[row]].GetDateTime(this.rowIndices[row]);
		}

		// Token: 0x0600C0B8 RID: 49336 RVA: 0x0026C430 File Offset: 0x0026A630
		public override string GetString(int row)
		{
			int num = this.columnIndices[row];
			if (num == -1)
			{
				return null;
			}
			int num2 = this.rowIndices[row];
			return this.columns[num].GetString(num2);
		}

		// Token: 0x0600C0B9 RID: 49337 RVA: 0x0026C468 File Offset: 0x0026A668
		public override object GetObject(int row)
		{
			int num = this.columnIndices[row];
			if (num == -1)
			{
				return DBNull.Value;
			}
			int num2 = this.rowIndices[row];
			return this.columns[num].GetObject(num2);
		}

		// Token: 0x0600C0BA RID: 49338 RVA: 0x0026C4A4 File Offset: 0x0026A6A4
		public unsafe override DBSTATUS GetValue(int row, IManagedDataConvert dataConvert, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			int num = this.columnIndices[row];
			if (num == -1)
			{
				destLength = DbLength.Zero;
				return DBSTATUS.S_ISNULL;
			}
			int num2 = this.rowIndices[row];
			return this.columns[num].GetValue(num2, dataConvert, binding, destValue, out destLength);
		}

		// Token: 0x0600C0BB RID: 49339 RVA: 0x0026C4ED File Offset: 0x0026A6ED
		public override bool IsNull(int row)
		{
			return this.columnIndices[row] == -1;
		}

		// Token: 0x0600C0BC RID: 49340 RVA: 0x0026C4FC File Offset: 0x0026A6FC
		public override void Serialize(PageWriter writer)
		{
			writer.WriteInt32(this.rowCount);
			writer.WriteInt32(this.columns.Count);
			writer.WriteArray(this.columnIndices, 0, this.rowCount);
			writer.WriteArray(this.rowIndices, 0, this.rowCount);
			for (int i = 0; i < this.columns.Count; i++)
			{
				Column column = this.columns[i];
				writer.WriteInt32((int)column.Type);
				column.Serialize(writer);
			}
		}

		// Token: 0x0600C0BD RID: 49341 RVA: 0x0026C584 File Offset: 0x0026A784
		public override void Deserialize(PageReader reader)
		{
			this.rowCount = reader.ReadInt32();
			int num = reader.ReadInt32();
			reader.ReadArray(this.columnIndices, 0, this.rowCount);
			reader.ReadArray(this.rowIndices, 0, this.rowCount);
			for (int i = 0; i < num; i++)
			{
				ColumnType columnType = (ColumnType)reader.ReadInt32();
				Column column = Column.Create(columnType, false, false, this.rowIndices.Length);
				column.Deserialize(reader);
				this.dictionary.Add(columnType, i);
				this.columns.Add(column);
			}
		}

		// Token: 0x04006149 RID: 24905
		private readonly Dictionary<ColumnType, int> dictionary;

		// Token: 0x0400614A RID: 24906
		private readonly List<Column> columns;

		// Token: 0x0400614B RID: 24907
		private readonly int[] columnIndices;

		// Token: 0x0400614C RID: 24908
		private readonly int[] rowIndices;

		// Token: 0x0400614D RID: 24909
		private int rowCount;
	}
}
