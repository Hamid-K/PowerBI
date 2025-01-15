using System;
using Microsoft.OleDb.Serialization;

namespace Microsoft.OleDb
{
	// Token: 0x02001E76 RID: 7798
	internal class CurrencyColumn : Column
	{
		// Token: 0x0600C027 RID: 49191 RVA: 0x0026B804 File Offset: 0x00269A04
		public CurrencyColumn(int maxRowCount)
		{
			this.column = new DecimalColumn(maxRowCount);
		}

		// Token: 0x17002F12 RID: 12050
		// (get) Token: 0x0600C028 RID: 49192 RVA: 0x001422C0 File Offset: 0x001404C0
		public override ColumnType Type
		{
			get
			{
				return ColumnType.Currency;
			}
		}

		// Token: 0x17002F13 RID: 12051
		// (get) Token: 0x0600C029 RID: 49193 RVA: 0x0026B818 File Offset: 0x00269A18
		public override int RowCount
		{
			get
			{
				return this.column.RowCount;
			}
		}

		// Token: 0x0600C02A RID: 49194 RVA: 0x0026B825 File Offset: 0x00269A25
		public override void Clear()
		{
			this.column.Clear();
		}

		// Token: 0x0600C02B RID: 49195 RVA: 0x0026B832 File Offset: 0x00269A32
		public override void AddNull()
		{
			this.column.AddNull();
		}

		// Token: 0x0600C02C RID: 49196 RVA: 0x0026B83F File Offset: 0x00269A3F
		public override void AddValue(object value)
		{
			this.AddCurrency((Currency)value);
		}

		// Token: 0x0600C02D RID: 49197 RVA: 0x0026B84D File Offset: 0x00269A4D
		public override void AddCurrency(Currency value)
		{
			this.column.AddValue(value.Value);
		}

		// Token: 0x0600C02E RID: 49198 RVA: 0x0026B861 File Offset: 0x00269A61
		public unsafe override void AddValue(DBTYPE type, void* value, int length)
		{
			this.column.AddValue(*(long*)value / 10000m);
		}

		// Token: 0x0600C02F RID: 49199 RVA: 0x0026B884 File Offset: 0x00269A84
		public override bool TryAddValue(object value)
		{
			if (value is Currency)
			{
				this.column.AddValue(((Currency)value).Value);
				return true;
			}
			return false;
		}

		// Token: 0x0600C030 RID: 49200 RVA: 0x0026B8B5 File Offset: 0x00269AB5
		public override object GetObject(int row)
		{
			return new Currency(this.column.GetValue(row));
		}

		// Token: 0x0600C031 RID: 49201 RVA: 0x0026B8CD File Offset: 0x00269ACD
		public unsafe override DBSTATUS GetValue(int row, IManagedDataConvert dataConvert, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			return this.column.GetValue(row, dataConvert, binding, destValue, out destLength);
		}

		// Token: 0x0600C032 RID: 49202 RVA: 0x00002105 File Offset: 0x00000305
		public override bool IsNull(int row)
		{
			return false;
		}

		// Token: 0x0600C033 RID: 49203 RVA: 0x0026B8E1 File Offset: 0x00269AE1
		public override void Serialize(PageWriter writer)
		{
			this.column.Serialize(writer);
		}

		// Token: 0x0600C034 RID: 49204 RVA: 0x0026B8EF File Offset: 0x00269AEF
		public override void Deserialize(PageReader reader)
		{
			this.column.Deserialize(reader);
		}

		// Token: 0x04006140 RID: 24896
		private DecimalColumn column;
	}
}
