using System;
using System.Runtime.CompilerServices;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x0200002F RID: 47
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class ObjectColumn : Column
	{
		// Token: 0x06000172 RID: 370 RVA: 0x00004C88 File Offset: 0x00002E88
		internal ObjectColumn(int maxRowCount)
		{
			this.maxRowCount = maxRowCount;
			this.nullColumn = new NullColumn();
			this.multiColumn = null;
			this.column = this.nullColumn;
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00004CB5 File Offset: 0x00002EB5
		public override ColumnType Type
		{
			get
			{
				return ColumnType.Object;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00004CB9 File Offset: 0x00002EB9
		public override int RowCount
		{
			get
			{
				return this.column.RowCount;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00004CC6 File Offset: 0x00002EC6
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

		// Token: 0x06000176 RID: 374 RVA: 0x00004CE7 File Offset: 0x00002EE7
		public override void AddValue(object value)
		{
			if (!this.column.TryAddValue(value))
			{
				this.Expand(Column.GetColumnType(value.GetType()));
				this.column.AddValue(value);
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00004D14 File Offset: 0x00002F14
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public unsafe override void AddValue(DBTYPE type, void* value, int length)
		{
			if (type == DBTYPE.VARIANT)
			{
				this.AddValue(Variant.GetObject((VARIANT*)value));
				return;
			}
			if (this.column.Type != ColumnType.Object && Column.GetColumnType(type) != this.column.Type)
			{
				this.Expand(Column.GetColumnType(type));
			}
			this.column.AddValue(type, value, length);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00004D6F File Offset: 0x00002F6F
		public override bool TryAddValue(object value)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00004D78 File Offset: 0x00002F78
		private void Expand(ColumnType columnType)
		{
			Column column;
			if (this.column == this.nullColumn)
			{
				column = Column.Create(columnType, false, this.maxRowCount);
			}
			else
			{
				column = this.MultiColumn;
			}
			ObjectColumn.CopyColumn(this.column, column);
			this.column.Clear();
			this.column = column;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00004DC8 File Offset: 0x00002FC8
		public override void AddNull()
		{
			this.column.AddNull();
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00004DD5 File Offset: 0x00002FD5
		public override bool IsNull(int row)
		{
			return false;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00004DD8 File Offset: 0x00002FD8
		public override void Clear()
		{
			this.column.Clear();
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00004DE5 File Offset: 0x00002FE5
		public override object GetObject(int row)
		{
			return this.column.GetObject(row);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00004DF3 File Offset: 0x00002FF3
		public override bool GetBoolean(int row)
		{
			return this.column.GetBoolean(row);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00004E01 File Offset: 0x00003001
		public override byte GetByte(int row)
		{
			return this.column.GetByte(row);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00004E0F File Offset: 0x0000300F
		public override short GetInt16(int row)
		{
			return this.column.GetInt16(row);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00004E1D File Offset: 0x0000301D
		public override int GetInt32(int row)
		{
			return this.column.GetInt32(row);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00004E2B File Offset: 0x0000302B
		public override long GetInt64(int row)
		{
			return this.column.GetInt64(row);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00004E39 File Offset: 0x00003039
		public override float GetFloat(int row)
		{
			return this.column.GetFloat(row);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00004E47 File Offset: 0x00003047
		public override Guid GetGuid(int row)
		{
			return this.column.GetGuid(row);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00004E55 File Offset: 0x00003055
		public override double GetDouble(int row)
		{
			return this.column.GetDouble(row);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00004E63 File Offset: 0x00003063
		public override decimal GetDecimal(int row)
		{
			return this.column.GetDecimal(row);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00004E71 File Offset: 0x00003071
		public override DateTime GetDateTime(int row)
		{
			return this.column.GetDateTime(row);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00004E7F File Offset: 0x0000307F
		public override string GetString(int row)
		{
			return this.column.GetString(row);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00004E8D File Offset: 0x0000308D
		public unsafe override DBSTATUS GetValue(int row, IDataConvert dataConvert, Binding binding, [global::System.Runtime.CompilerServices.Nullable(0)] byte* destValue, out DBLENGTH destLength)
		{
			return this.column.GetValue(row, dataConvert, binding, destValue, out destLength);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00004EA1 File Offset: 0x000030A1
		public override void Serialize(PageWriter writer)
		{
			writer.WriteInt32((int)this.column.Type);
			this.column.Serialize(writer);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00004EC0 File Offset: 0x000030C0
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
				this.column = Column.Create(columnType, false, this.maxRowCount);
			}
			this.column.Deserialize(reader);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00004F1C File Offset: 0x0000311C
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

		// Token: 0x0600018D RID: 397 RVA: 0x00004F5C File Offset: 0x0000315C
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

		// Token: 0x04000044 RID: 68
		private int maxRowCount;

		// Token: 0x04000045 RID: 69
		private NullColumn nullColumn;

		// Token: 0x04000046 RID: 70
		private MultiObjectColumn multiColumn;

		// Token: 0x04000047 RID: 71
		private Column column;
	}
}
