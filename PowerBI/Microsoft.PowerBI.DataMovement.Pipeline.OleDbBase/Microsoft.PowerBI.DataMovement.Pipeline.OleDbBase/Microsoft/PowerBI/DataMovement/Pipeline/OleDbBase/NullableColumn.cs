using System;
using System.Runtime.CompilerServices;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000011 RID: 17
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class NullableColumn : Column
	{
		// Token: 0x06000064 RID: 100 RVA: 0x000030B3 File Offset: 0x000012B3
		internal NullableColumn(Column column, int maxRowCount)
		{
			this.column = column;
			this.values = new ulong[(maxRowCount + 63) / 64];
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000065 RID: 101 RVA: 0x000030D4 File Offset: 0x000012D4
		public override ColumnType Type
		{
			get
			{
				return this.column.Type;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000066 RID: 102 RVA: 0x000030E1 File Offset: 0x000012E1
		public override int RowCount
		{
			get
			{
				return this.rowCount;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000067 RID: 103 RVA: 0x000030E9 File Offset: 0x000012E9
		private int ValueCount
		{
			get
			{
				return (this.rowCount + 63) / 64;
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000030F7 File Offset: 0x000012F7
		public override void Clear()
		{
			if (this.hasNull)
			{
				Array.Clear(this.values, 0, this.ValueCount);
				this.hasNull = false;
			}
			this.rowCount = 0;
			this.column.Clear();
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000312C File Offset: 0x0000132C
		public override void AddNull()
		{
			this.column.AddNull();
			int num = this.rowCount / 64;
			ulong num2 = 1UL << this.rowCount % 64;
			this.values[num] |= num2;
			this.rowCount++;
			this.hasNull = true;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003184 File Offset: 0x00001384
		public override void AddValue(object value)
		{
			this.column.AddValue(value);
			this.rowCount++;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000031A0 File Offset: 0x000013A0
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public unsafe override void AddValue(DBTYPE type, void* value, int length)
		{
			this.column.AddValue(type, value, length);
			this.rowCount++;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000031BE File Offset: 0x000013BE
		public override bool TryAddValue(object value)
		{
			return this.column.TryAddValue(value);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000031CC File Offset: 0x000013CC
		public override bool IsNull(int row)
		{
			if (this.hasNull)
			{
				int num = row / 64;
				ulong num2 = 1UL << row % 64;
				return (this.values[num] & num2) > 0UL;
			}
			return false;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003201 File Offset: 0x00001401
		public unsafe override DBSTATUS GetValue(int row, IDataConvert dataConvert, Binding binding, [global::System.Runtime.CompilerServices.Nullable(0)] byte* destValue, out DBLENGTH destLength)
		{
			if (this.IsNull(row))
			{
				destLength = DbLength.Zero;
				return DBSTATUS.S_ISNULL;
			}
			return this.column.GetValue(row, dataConvert, binding, destValue, out destLength);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000322C File Offset: 0x0000142C
		public override bool GetBoolean(int row)
		{
			return this.column.GetBoolean(row);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000323A File Offset: 0x0000143A
		public override byte GetByte(int row)
		{
			return this.column.GetByte(row);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003248 File Offset: 0x00001448
		public override short GetInt16(int row)
		{
			return this.column.GetInt16(row);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003256 File Offset: 0x00001456
		public override int GetInt32(int row)
		{
			return this.column.GetInt32(row);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003264 File Offset: 0x00001464
		public override long GetInt64(int row)
		{
			return this.column.GetInt64(row);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003272 File Offset: 0x00001472
		public override float GetFloat(int row)
		{
			return this.column.GetFloat(row);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003280 File Offset: 0x00001480
		public override Guid GetGuid(int row)
		{
			return this.column.GetGuid(row);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000328E File Offset: 0x0000148E
		public override double GetDouble(int row)
		{
			return this.column.GetDouble(row);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000329C File Offset: 0x0000149C
		public override decimal GetDecimal(int row)
		{
			return this.column.GetDecimal(row);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000032AA File Offset: 0x000014AA
		public override DateTime GetDateTime(int row)
		{
			return this.column.GetDateTime(row);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000032B8 File Offset: 0x000014B8
		public override string GetString(int row)
		{
			if (this.IsNull(row))
			{
				return null;
			}
			return this.column.GetString(row);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000032D1 File Offset: 0x000014D1
		public override object GetObject(int row)
		{
			if (this.IsNull(row))
			{
				return DBNull.Value;
			}
			return this.column.GetObject(row);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000032F0 File Offset: 0x000014F0
		public override void Serialize(PageWriter writer)
		{
			writer.WriteInt32(this.rowCount);
			if (this.hasNull)
			{
				writer.WriteInt32(1);
				writer.WriteArray(this.values, 0, this.ValueCount);
			}
			else
			{
				writer.WriteInt32(0);
			}
			this.column.Serialize(writer);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003340 File Offset: 0x00001540
		public override void Deserialize(PageReader reader)
		{
			this.rowCount = reader.ReadInt32();
			this.hasNull = reader.ReadInt32() == 1;
			if (this.hasNull)
			{
				reader.ReadArray(this.values, 0, this.ValueCount);
			}
			this.column.Deserialize(reader);
		}

		// Token: 0x0400002B RID: 43
		private const int BitsPerValue = 64;

		// Token: 0x0400002C RID: 44
		private readonly Column column;

		// Token: 0x0400002D RID: 45
		private readonly ulong[] values;

		// Token: 0x0400002E RID: 46
		private int rowCount;

		// Token: 0x0400002F RID: 47
		private bool hasNull;
	}
}
