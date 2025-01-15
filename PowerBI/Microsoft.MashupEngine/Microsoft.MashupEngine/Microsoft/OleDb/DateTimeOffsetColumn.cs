using System;
using Microsoft.OleDb.Serialization;

namespace Microsoft.OleDb
{
	// Token: 0x02001E70 RID: 7792
	internal class DateTimeOffsetColumn : Column<DateTimeOffset>
	{
		// Token: 0x0600BFF3 RID: 49139 RVA: 0x0026B395 File Offset: 0x00269595
		public DateTimeOffsetColumn(int maxRowCount)
			: base(maxRowCount)
		{
		}

		// Token: 0x17002F0B RID: 12043
		// (get) Token: 0x0600BFF4 RID: 49140 RVA: 0x000E78AE File Offset: 0x000E5AAE
		public override ColumnType Type
		{
			get
			{
				return ColumnType.DateTimeOffset;
			}
		}

		// Token: 0x0600BFF5 RID: 49141 RVA: 0x0026B39E File Offset: 0x0026959E
		public override void AddDateTimeOffset(DateTimeOffset value)
		{
			base.AddValue(value);
		}

		// Token: 0x0600BFF6 RID: 49142 RVA: 0x0026B3A7 File Offset: 0x002695A7
		public unsafe override void AddValue(DBTYPE type, void* value, int length)
		{
			base.AddValue(OleDbConvert.GetDateTimeOffset(*(DBTIMESTAMPOFFSET*)value));
		}

		// Token: 0x0600BFF7 RID: 49143 RVA: 0x0026B3BC File Offset: 0x002695BC
		public unsafe override DBSTATUS GetValue(int row, IManagedDataConvert dataConvert, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			DateTimeOffset value = base.GetValue(row);
			return dataConvert.DataConvert(value, binding, destValue, out destLength);
		}

		// Token: 0x0600BFF8 RID: 49144 RVA: 0x0026B3DD File Offset: 0x002695DD
		protected override void Serialize(PageWriter writer, DateTimeOffset[] values, int offset, int count)
		{
			writer.WriteArray(values, offset, count);
		}

		// Token: 0x0600BFF9 RID: 49145 RVA: 0x0026B3E9 File Offset: 0x002695E9
		protected override void Deserialize(PageReader reader, DateTimeOffset[] values, int offset, int count)
		{
			reader.ReadArray(values, offset, count);
		}
	}
}
