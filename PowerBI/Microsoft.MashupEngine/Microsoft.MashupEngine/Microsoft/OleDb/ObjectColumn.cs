using System;
using Microsoft.OleDb.Marshallers;
using Microsoft.OleDb.Serialization;

namespace Microsoft.OleDb
{
	// Token: 0x02001E7B RID: 7803
	internal class ObjectColumn : Column
	{
		// Token: 0x0600C05F RID: 49247 RVA: 0x0026BB59 File Offset: 0x00269D59
		public ObjectColumn(int maxRowCount)
		{
			this.maxRowCount = maxRowCount;
			this.nullColumn = new NullColumn(0);
			this.multiColumn = null;
			this.column = this.nullColumn;
		}

		// Token: 0x17002F1B RID: 12059
		// (get) Token: 0x0600C060 RID: 49248 RVA: 0x002436D1 File Offset: 0x002418D1
		public override ColumnType Type
		{
			get
			{
				return ColumnType.Object;
			}
		}

		// Token: 0x17002F1C RID: 12060
		// (get) Token: 0x0600C061 RID: 49249 RVA: 0x0026BB87 File Offset: 0x00269D87
		public override int RowCount
		{
			get
			{
				return this.column.RowCount;
			}
		}

		// Token: 0x17002F1D RID: 12061
		// (get) Token: 0x0600C062 RID: 49250 RVA: 0x0026BB94 File Offset: 0x00269D94
		private Column MultiColumn
		{
			get
			{
				if (this.multiColumn == null)
				{
					this.multiColumn = new MultiObjectColumn(this.maxRowCount);
				}
				return this.multiColumn;
			}
		}

		// Token: 0x0600C063 RID: 49251 RVA: 0x0026BBB5 File Offset: 0x00269DB5
		public override void AddValue(object value)
		{
			if (!this.column.TryAddValue(value))
			{
				this.Expand(Column.GetColumnType(value.GetType()));
				this.column.AddValue(value);
			}
		}

		// Token: 0x0600C064 RID: 49252 RVA: 0x0026BBE2 File Offset: 0x00269DE2
		public override void AddBoolean(bool value)
		{
			this.PrepareForAdd(ColumnType.Boolean);
			this.column.AddBoolean(value);
		}

		// Token: 0x0600C065 RID: 49253 RVA: 0x0026BBF7 File Offset: 0x00269DF7
		public override void AddByte(byte value)
		{
			this.PrepareForAdd(ColumnType.Byte);
			this.column.AddByte(value);
		}

		// Token: 0x0600C066 RID: 49254 RVA: 0x0026BC0C File Offset: 0x00269E0C
		public override void AddUInt16(ushort value)
		{
			this.PrepareForAdd(ColumnType.UInt16);
			this.column.AddUInt16(value);
		}

		// Token: 0x0600C067 RID: 49255 RVA: 0x0026BC21 File Offset: 0x00269E21
		public override void AddUInt32(uint value)
		{
			this.PrepareForAdd(ColumnType.UInt32);
			this.column.AddUInt32(value);
		}

		// Token: 0x0600C068 RID: 49256 RVA: 0x0026BC36 File Offset: 0x00269E36
		public override void AddUInt64(ulong value)
		{
			this.PrepareForAdd(ColumnType.UInt64);
			this.column.AddUInt64(value);
		}

		// Token: 0x0600C069 RID: 49257 RVA: 0x0026BC4B File Offset: 0x00269E4B
		public override void AddSByte(sbyte value)
		{
			this.PrepareForAdd(ColumnType.SByte);
			this.column.AddSByte(value);
		}

		// Token: 0x0600C06A RID: 49258 RVA: 0x0026BC60 File Offset: 0x00269E60
		public override void AddInt16(short value)
		{
			this.PrepareForAdd(ColumnType.Int16);
			this.column.AddInt16(value);
		}

		// Token: 0x0600C06B RID: 49259 RVA: 0x0026BC75 File Offset: 0x00269E75
		public override void AddInt32(int value)
		{
			this.PrepareForAdd(ColumnType.Int32);
			this.column.AddInt32(value);
		}

		// Token: 0x0600C06C RID: 49260 RVA: 0x0026BC8A File Offset: 0x00269E8A
		public override void AddInt64(long value)
		{
			this.PrepareForAdd(ColumnType.Int64);
			this.column.AddInt64(value);
		}

		// Token: 0x0600C06D RID: 49261 RVA: 0x0026BC9F File Offset: 0x00269E9F
		public override void AddFloat(float value)
		{
			this.PrepareForAdd(ColumnType.Single);
			this.column.AddFloat(value);
		}

		// Token: 0x0600C06E RID: 49262 RVA: 0x0026BCB5 File Offset: 0x00269EB5
		public override void AddDouble(double value)
		{
			this.PrepareForAdd(ColumnType.Double);
			this.column.AddDouble(value);
		}

		// Token: 0x0600C06F RID: 49263 RVA: 0x0026BCCB File Offset: 0x00269ECB
		public override void AddDecimal(decimal value)
		{
			this.PrepareForAdd(ColumnType.Decimal);
			this.column.AddDecimal(value);
		}

		// Token: 0x0600C070 RID: 49264 RVA: 0x0026BCE1 File Offset: 0x00269EE1
		public override void AddNumber(Number value)
		{
			this.PrepareForAdd(ColumnType.Numeric);
			this.column.AddNumber(value);
		}

		// Token: 0x0600C071 RID: 49265 RVA: 0x0026BCF7 File Offset: 0x00269EF7
		public override void AddCurrency(Currency value)
		{
			this.PrepareForAdd(ColumnType.Currency);
			this.column.AddCurrency(value);
		}

		// Token: 0x0600C072 RID: 49266 RVA: 0x0026BD0D File Offset: 0x00269F0D
		public override void AddDate(Date value)
		{
			this.PrepareForAdd(ColumnType.Date);
			this.column.AddDate(value);
		}

		// Token: 0x0600C073 RID: 49267 RVA: 0x0026BD23 File Offset: 0x00269F23
		public override void AddTime(Time value)
		{
			this.PrepareForAdd(ColumnType.Time);
			this.column.AddTime(value);
		}

		// Token: 0x0600C074 RID: 49268 RVA: 0x0026BD39 File Offset: 0x00269F39
		public override void AddDateTime(DateTime value)
		{
			this.PrepareForAdd(ColumnType.DateTime);
			this.column.AddDateTime(value);
		}

		// Token: 0x0600C075 RID: 49269 RVA: 0x0026BD4F File Offset: 0x00269F4F
		public override void AddDateTimeOffset(DateTimeOffset value)
		{
			this.PrepareForAdd(ColumnType.DateTimeOffset);
			this.column.AddDateTimeOffset(value);
		}

		// Token: 0x0600C076 RID: 49270 RVA: 0x0026BD65 File Offset: 0x00269F65
		public override void AddTimeSpan(TimeSpan value)
		{
			this.PrepareForAdd(ColumnType.TimeSpan);
			this.column.AddTimeSpan(value);
		}

		// Token: 0x0600C077 RID: 49271 RVA: 0x0026BD7B File Offset: 0x00269F7B
		public override void AddGuid(Guid value)
		{
			this.PrepareForAdd(ColumnType.Guid);
			this.column.AddGuid(value);
		}

		// Token: 0x0600C078 RID: 49272 RVA: 0x0026BD91 File Offset: 0x00269F91
		public unsafe override void AddValue(DBTYPE type, void* value, int length)
		{
			if (type == DBTYPE.VARIANT)
			{
				this.AddValue(VariantMarshaller.Instance.GetManaged((IntPtr)value));
				return;
			}
			this.PrepareForAdd(Column.GetColumnType(type));
			this.column.AddValue(type, value, length);
		}

		// Token: 0x0600C079 RID: 49273 RVA: 0x0000EE09 File Offset: 0x0000D009
		public override bool TryAddValue(object value)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600C07A RID: 49274 RVA: 0x0026BDC9 File Offset: 0x00269FC9
		private void PrepareForAdd(ColumnType columnType)
		{
			if (this.column.Type != ColumnType.Object && columnType != this.column.Type)
			{
				this.Expand(columnType);
			}
		}

		// Token: 0x0600C07B RID: 49275 RVA: 0x0026BDF0 File Offset: 0x00269FF0
		private void Expand(ColumnType columnType)
		{
			Column column;
			if (this.column == this.nullColumn)
			{
				column = Column.Create(columnType, false, false, this.maxRowCount);
			}
			else
			{
				column = this.MultiColumn;
			}
			ObjectColumn.CopyColumn(this.column, column);
			this.column.Clear();
			this.column = column;
		}

		// Token: 0x0600C07C RID: 49276 RVA: 0x0026BE41 File Offset: 0x0026A041
		public override void AddNull()
		{
			this.column.AddNull();
		}

		// Token: 0x0600C07D RID: 49277 RVA: 0x00002105 File Offset: 0x00000305
		public override bool IsNull(int row)
		{
			return false;
		}

		// Token: 0x0600C07E RID: 49278 RVA: 0x0026BE4E File Offset: 0x0026A04E
		public override void Clear()
		{
			this.column.Clear();
		}

		// Token: 0x0600C07F RID: 49279 RVA: 0x0026BE5B File Offset: 0x0026A05B
		public override object GetObject(int row)
		{
			return this.column.GetObject(row);
		}

		// Token: 0x0600C080 RID: 49280 RVA: 0x0026BE69 File Offset: 0x0026A069
		public override bool GetBoolean(int row)
		{
			return this.column.GetBoolean(row);
		}

		// Token: 0x0600C081 RID: 49281 RVA: 0x0026BE77 File Offset: 0x0026A077
		public override byte GetByte(int row)
		{
			return this.column.GetByte(row);
		}

		// Token: 0x0600C082 RID: 49282 RVA: 0x0026BE85 File Offset: 0x0026A085
		public override short GetInt16(int row)
		{
			return this.column.GetInt16(row);
		}

		// Token: 0x0600C083 RID: 49283 RVA: 0x0026BE93 File Offset: 0x0026A093
		public override int GetInt32(int row)
		{
			return this.column.GetInt32(row);
		}

		// Token: 0x0600C084 RID: 49284 RVA: 0x0026BEA1 File Offset: 0x0026A0A1
		public override long GetInt64(int row)
		{
			return this.column.GetInt64(row);
		}

		// Token: 0x0600C085 RID: 49285 RVA: 0x0026BEAF File Offset: 0x0026A0AF
		public override float GetFloat(int row)
		{
			return this.column.GetFloat(row);
		}

		// Token: 0x0600C086 RID: 49286 RVA: 0x0026BEBD File Offset: 0x0026A0BD
		public override Guid GetGuid(int row)
		{
			return this.column.GetGuid(row);
		}

		// Token: 0x0600C087 RID: 49287 RVA: 0x0026BECB File Offset: 0x0026A0CB
		public override double GetDouble(int row)
		{
			return this.column.GetDouble(row);
		}

		// Token: 0x0600C088 RID: 49288 RVA: 0x0026BED9 File Offset: 0x0026A0D9
		public override decimal GetDecimal(int row)
		{
			return this.column.GetDecimal(row);
		}

		// Token: 0x0600C089 RID: 49289 RVA: 0x0026BEE7 File Offset: 0x0026A0E7
		public override DateTime GetDateTime(int row)
		{
			return this.column.GetDateTime(row);
		}

		// Token: 0x0600C08A RID: 49290 RVA: 0x0026BEF5 File Offset: 0x0026A0F5
		public override string GetString(int row)
		{
			return this.column.GetString(row);
		}

		// Token: 0x0600C08B RID: 49291 RVA: 0x0026BF03 File Offset: 0x0026A103
		public unsafe override DBSTATUS GetValue(int row, IManagedDataConvert dataConvert, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			return this.column.GetValue(row, dataConvert, binding, destValue, out destLength);
		}

		// Token: 0x0600C08C RID: 49292 RVA: 0x0026BF17 File Offset: 0x0026A117
		public override void Serialize(PageWriter writer)
		{
			writer.WriteInt32((int)this.column.Type);
			this.column.Serialize(writer);
		}

		// Token: 0x0600C08D RID: 49293 RVA: 0x0026BF38 File Offset: 0x0026A138
		public override void Deserialize(PageReader reader)
		{
			ColumnType columnType = (ColumnType)reader.ReadInt32();
			if (columnType == ColumnType.Null)
			{
				this.column = this.nullColumn;
			}
			else if (columnType == ColumnType.Object)
			{
				this.column = this.MultiColumn;
			}
			else
			{
				this.column = Column.Create(columnType, false, false, this.maxRowCount);
			}
			this.column.Deserialize(reader);
		}

		// Token: 0x0600C08E RID: 49294 RVA: 0x0026BF94 File Offset: 0x0026A194
		private Column ExpandColumn(Type type)
		{
			Column column;
			if (this.column == this.nullColumn)
			{
				column = Column.Create(type, false, this.maxRowCount);
			}
			else
			{
				column = this.MultiColumn;
			}
			ObjectColumn.CopyColumn(this.column, column);
			return column;
		}

		// Token: 0x0600C08F RID: 49295 RVA: 0x0026BFD4 File Offset: 0x0026A1D4
		private static void CopyColumn(Column sourceColumn, Column destColumn)
		{
			int rowCount = sourceColumn.RowCount;
			for (int i = 0; i < rowCount; i++)
			{
				if (sourceColumn.IsNull(i))
				{
					destColumn.AddNull();
				}
				else
				{
					destColumn.AddValue(sourceColumn.GetObject(i));
				}
			}
		}

		// Token: 0x04006145 RID: 24901
		private int maxRowCount;

		// Token: 0x04006146 RID: 24902
		private NullColumn nullColumn;

		// Token: 0x04006147 RID: 24903
		private MultiObjectColumn multiColumn;

		// Token: 0x04006148 RID: 24904
		private Column column;
	}
}
