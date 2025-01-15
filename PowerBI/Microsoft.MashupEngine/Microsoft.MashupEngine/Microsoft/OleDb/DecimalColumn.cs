using System;
using Microsoft.OleDb.Serialization;

namespace Microsoft.OleDb
{
	// Token: 0x02001E73 RID: 7795
	internal class DecimalColumn : Column<decimal>
	{
		// Token: 0x0600C00A RID: 49162 RVA: 0x0026B4E9 File Offset: 0x002696E9
		public DecimalColumn(int maxRowCount)
			: base(maxRowCount)
		{
		}

		// Token: 0x17002F0E RID: 12046
		// (get) Token: 0x0600C00B RID: 49163 RVA: 0x0014213C File Offset: 0x0014033C
		public override ColumnType Type
		{
			get
			{
				return ColumnType.Decimal;
			}
		}

		// Token: 0x0600C00C RID: 49164 RVA: 0x0026B4F2 File Offset: 0x002696F2
		public override void AddDecimal(decimal value)
		{
			base.AddValue(value);
		}

		// Token: 0x0600C00D RID: 49165 RVA: 0x0026B4FB File Offset: 0x002696FB
		public unsafe override void AddValue(DBTYPE type, void* value, int length)
		{
			base.AddValue(*(decimal*)value);
		}

		// Token: 0x0600C00E RID: 49166 RVA: 0x0026B50C File Offset: 0x0026970C
		public unsafe override DBSTATUS GetValue(int row, IManagedDataConvert dataConvert, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			decimal value = base.GetValue(row);
			DBLENGTH @decimal = DbLength.Decimal;
			if (binding.DestType == DBTYPE.R8)
			{
				*(double*)destValue = (double)value;
				destLength = DbLength.Double;
				return DBSTATUS.S_OK;
			}
			return dataConvert.DataConvert(value, binding, destValue, out destLength);
		}

		// Token: 0x0600C00F RID: 49167 RVA: 0x0026B554 File Offset: 0x00269754
		public override decimal GetDecimal(int row)
		{
			return base.GetValue(row);
		}

		// Token: 0x0600C010 RID: 49168 RVA: 0x0026B55D File Offset: 0x0026975D
		protected override void Serialize(PageWriter writer, decimal[] values, int offset, int count)
		{
			writer.WriteArray(values, offset, count);
		}

		// Token: 0x0600C011 RID: 49169 RVA: 0x0026B569 File Offset: 0x00269769
		protected override void Deserialize(PageReader reader, decimal[] values, int offset, int count)
		{
			reader.ReadArray(values, offset, count);
		}
	}
}
