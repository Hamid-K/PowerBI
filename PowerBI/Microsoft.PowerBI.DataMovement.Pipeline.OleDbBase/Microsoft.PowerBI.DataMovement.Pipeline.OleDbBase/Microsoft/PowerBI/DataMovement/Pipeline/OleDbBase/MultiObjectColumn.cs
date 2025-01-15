using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000030 RID: 48
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class MultiObjectColumn : Column
	{
		// Token: 0x0600018E RID: 398 RVA: 0x00004F9A File Offset: 0x0000319A
		internal MultiObjectColumn(int maxRowCount)
		{
			this.dictionary = new Dictionary<ColumnType, int>();
			this.columns = new List<Column>();
			this.columnIndices = new int[maxRowCount];
			this.rowIndices = new int[maxRowCount];
			this.rowCount = 0;
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00004FD7 File Offset: 0x000031D7
		public override ColumnType Type
		{
			get
			{
				return ColumnType.Object;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00004FDB File Offset: 0x000031DB
		public override int RowCount
		{
			get
			{
				return this.rowCount;
			}
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00004FE3 File Offset: 0x000031E3
		public override void Clear()
		{
			this.columns.Clear();
			this.dictionary.Clear();
			this.rowCount = 0;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00005004 File Offset: 0x00003204
		public override void AddValue(object value)
		{
			ColumnType columnType = Column.GetColumnType(value.GetType());
			int columnIndex = this.GetColumnIndex(columnType);
			Column column = this.columns[columnIndex];
			int num = column.RowCount;
			this.columnIndices[this.rowCount] = columnIndex;
			this.rowIndices[this.rowCount] = num;
			this.rowCount++;
			column.AddValue(value);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00005068 File Offset: 0x00003268
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public unsafe override void AddValue(DBTYPE type, void* value, int length)
		{
			ColumnType columnType = Column.GetColumnType(type);
			int columnIndex = this.GetColumnIndex(columnType);
			Column column = this.columns[columnIndex];
			int num = column.RowCount;
			this.columnIndices[this.rowCount] = columnIndex;
			this.rowIndices[this.rowCount] = num;
			this.rowCount++;
			column.AddValue(type, value, length);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000050C9 File Offset: 0x000032C9
		public override bool TryAddValue(object value)
		{
			this.AddValue(value);
			return true;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000050D3 File Offset: 0x000032D3
		public override void AddNull()
		{
			this.columnIndices[this.rowCount] = -1;
			this.rowIndices[this.rowCount] = -1;
			this.rowCount++;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00005100 File Offset: 0x00003300
		private int GetColumnIndex(ColumnType columnType)
		{
			int count;
			if (!this.dictionary.TryGetValue(columnType, out count))
			{
				count = this.dictionary.Count;
				this.dictionary.Add(columnType, count);
				this.columns.Add(Column.Create(columnType, false, this.rowIndices.Length));
			}
			return count;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00005151 File Offset: 0x00003351
		public override bool GetBoolean(int row)
		{
			return this.columns[this.columnIndices[row]].GetBoolean(this.rowIndices[row]);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00005173 File Offset: 0x00003373
		public override byte GetByte(int row)
		{
			return this.columns[this.columnIndices[row]].GetByte(this.rowIndices[row]);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00005195 File Offset: 0x00003395
		public override short GetInt16(int row)
		{
			return this.columns[this.columnIndices[row]].GetInt16(this.rowIndices[row]);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x000051B7 File Offset: 0x000033B7
		public override int GetInt32(int row)
		{
			return this.columns[this.columnIndices[row]].GetInt32(this.rowIndices[row]);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x000051D9 File Offset: 0x000033D9
		public override long GetInt64(int row)
		{
			return this.columns[this.columnIndices[row]].GetInt64(this.rowIndices[row]);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x000051FB File Offset: 0x000033FB
		public override float GetFloat(int row)
		{
			return this.columns[this.columnIndices[row]].GetFloat(this.rowIndices[row]);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000521D File Offset: 0x0000341D
		public override Guid GetGuid(int row)
		{
			return this.columns[this.columnIndices[row]].GetGuid(this.rowIndices[row]);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000523F File Offset: 0x0000343F
		public override double GetDouble(int row)
		{
			return this.columns[this.columnIndices[row]].GetDouble(this.rowIndices[row]);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00005261 File Offset: 0x00003461
		public override decimal GetDecimal(int row)
		{
			return this.columns[this.columnIndices[row]].GetDecimal(this.rowIndices[row]);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00005283 File Offset: 0x00003483
		public override DateTime GetDateTime(int row)
		{
			return this.columns[this.columnIndices[row]].GetDateTime(this.rowIndices[row]);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x000052A8 File Offset: 0x000034A8
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

		// Token: 0x060001A2 RID: 418 RVA: 0x000052E0 File Offset: 0x000034E0
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

		// Token: 0x060001A3 RID: 419 RVA: 0x0000531C File Offset: 0x0000351C
		public unsafe override DBSTATUS GetValue(int row, IDataConvert dataConvert, Binding binding, [global::System.Runtime.CompilerServices.Nullable(0)] byte* destValue, out DBLENGTH destLength)
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

		// Token: 0x060001A4 RID: 420 RVA: 0x00005365 File Offset: 0x00003565
		public override bool IsNull(int row)
		{
			return this.columnIndices[row] == -1;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00005374 File Offset: 0x00003574
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

		// Token: 0x060001A6 RID: 422 RVA: 0x000053FC File Offset: 0x000035FC
		public override void Deserialize(PageReader reader)
		{
			this.rowCount = reader.ReadInt32();
			int num = reader.ReadInt32();
			reader.ReadArray(this.columnIndices, 0, this.rowCount);
			reader.ReadArray(this.rowIndices, 0, this.rowCount);
			for (int i = 0; i < num; i++)
			{
				ColumnType columnType = (ColumnType)reader.ReadInt32();
				Column column = Column.Create(columnType, false, this.rowIndices.Length);
				column.Deserialize(reader);
				this.dictionary.Add(columnType, i);
				this.columns.Add(column);
			}
		}

		// Token: 0x04000048 RID: 72
		private readonly Dictionary<ColumnType, int> dictionary;

		// Token: 0x04000049 RID: 73
		private readonly List<Column> columns;

		// Token: 0x0400004A RID: 74
		private readonly int[] columnIndices;

		// Token: 0x0400004B RID: 75
		private readonly int[] rowIndices;

		// Token: 0x0400004C RID: 76
		private int rowCount;
	}
}
