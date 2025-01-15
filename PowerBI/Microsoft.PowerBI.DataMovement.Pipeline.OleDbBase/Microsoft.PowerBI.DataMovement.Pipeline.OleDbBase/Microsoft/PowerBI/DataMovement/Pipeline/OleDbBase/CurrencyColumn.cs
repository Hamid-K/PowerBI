using System;
using System.Runtime.CompilerServices;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x0200002C RID: 44
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class CurrencyColumn : Column
	{
		// Token: 0x0600014B RID: 331 RVA: 0x00004876 File Offset: 0x00002A76
		internal CurrencyColumn(int maxRowCount)
		{
			this.column = new DecimalColumn(maxRowCount);
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600014C RID: 332 RVA: 0x0000488A File Offset: 0x00002A8A
		public override ColumnType Type
		{
			get
			{
				return ColumnType.Currency;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600014D RID: 333 RVA: 0x0000488E File Offset: 0x00002A8E
		public override int RowCount
		{
			get
			{
				return this.column.RowCount;
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000489B File Offset: 0x00002A9B
		public override void Clear()
		{
			this.column.Clear();
		}

		// Token: 0x0600014F RID: 335 RVA: 0x000048A8 File Offset: 0x00002AA8
		public override void AddNull()
		{
			this.column.AddNull();
		}

		// Token: 0x06000150 RID: 336 RVA: 0x000048B8 File Offset: 0x00002AB8
		public override void AddValue(object value)
		{
			this.column.AddValue(((Currency)value).Value);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000048DE File Offset: 0x00002ADE
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public unsafe override void AddValue(DBTYPE type, void* value, int length)
		{
			this.column.AddValue(*(long*)value / 10000m);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00004904 File Offset: 0x00002B04
		public override bool TryAddValue(object value)
		{
			if (value is Currency)
			{
				this.column.AddValue(((Currency)value).Value);
				return true;
			}
			return false;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00004935 File Offset: 0x00002B35
		public override object GetObject(int row)
		{
			return new Currency(this.column.GetValue(row));
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00004950 File Offset: 0x00002B50
		public unsafe override DBSTATUS GetValue(int row, IDataConvert dataConvert, Binding binding, [global::System.Runtime.CompilerServices.Nullable(0)] byte* destValue, out DBLENGTH destLength)
		{
			DBSTATUS value = this.column.GetValue(row, dataConvert, binding, destValue, out destLength);
			if (value != DBSTATUS.S_OK)
			{
				return value;
			}
			if (binding.DestType == DBTYPE.VARIANT)
			{
				decimal value2 = this.column.GetValue(row);
				dataConvert.DataConvert(DBTYPE.DECIMAL, DBTYPE.CY, DbLength.Decimal, out destLength, (void*)(&value2), (void*)(&((VARIANT*)destValue)->Value64), DbLength.Eight, DBSTATUS.S_OK, out value, binding.Precision, binding.Scale, DBDATACONVERT.DEFAULT);
				((VARIANT*)destValue)->Type = VARTYPE.CY;
			}
			return value;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000049C5 File Offset: 0x00002BC5
		public override bool IsNull(int row)
		{
			return false;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000049C8 File Offset: 0x00002BC8
		public override void Serialize(PageWriter writer)
		{
			this.column.Serialize(writer);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000049D6 File Offset: 0x00002BD6
		public override void Deserialize(PageReader reader)
		{
			this.column.Deserialize(reader);
		}

		// Token: 0x04000040 RID: 64
		private DecimalColumn column;
	}
}
