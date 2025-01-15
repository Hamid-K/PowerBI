using System;
using Microsoft.OleDb.Serialization;

namespace Microsoft.OleDb
{
	// Token: 0x02001E6E RID: 7790
	internal class DateColumn : Column<Date>
	{
		// Token: 0x0600BFE5 RID: 49125 RVA: 0x0026B27C File Offset: 0x0026947C
		public DateColumn(int maxRowCount)
			: base(maxRowCount)
		{
		}

		// Token: 0x17002F09 RID: 12041
		// (get) Token: 0x0600BFE6 RID: 49126 RVA: 0x001AA8D9 File Offset: 0x001A8AD9
		public override ColumnType Type
		{
			get
			{
				return ColumnType.Date;
			}
		}

		// Token: 0x0600BFE7 RID: 49127 RVA: 0x0026B285 File Offset: 0x00269485
		public override void AddDate(Date value)
		{
			base.AddValue(value);
		}

		// Token: 0x0600BFE8 RID: 49128 RVA: 0x0026B28E File Offset: 0x0026948E
		public unsafe override void AddValue(DBTYPE type, void* value, int length)
		{
			base.AddValue(OleDbConvert.GetDate(*(DBDATE*)value));
		}

		// Token: 0x0600BFE9 RID: 49129 RVA: 0x0026B2A4 File Offset: 0x002694A4
		public unsafe override DBSTATUS GetValue(int row, IManagedDataConvert dataConvert, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			Date value = base.GetValue(row);
			if (binding.DestType == DBTYPE.DATE)
			{
				return DataConversions.ConvertOADateToDate(OleDbConvert.SafeToOADate(value.DateTime), destValue, out destLength);
			}
			if (binding.DestType == DBTYPE.VARIANT)
			{
				return DataConversions.ConvertOADateToVariant(OleDbConvert.SafeToOADate(value.DateTime), destValue, out destLength);
			}
			return dataConvert.DataConvert(value, binding, destValue, out destLength);
		}

		// Token: 0x0600BFEA RID: 49130 RVA: 0x0026B304 File Offset: 0x00269504
		protected override void Serialize(PageWriter writer, Date[] values, int offset, int count)
		{
			writer.WriteArray(values, offset, count);
		}

		// Token: 0x0600BFEB RID: 49131 RVA: 0x0026B310 File Offset: 0x00269510
		protected override void Deserialize(PageReader reader, Date[] values, int offset, int count)
		{
			reader.ReadArray(values, offset, count);
		}
	}
}
