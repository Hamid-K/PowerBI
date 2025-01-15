using System;
using Microsoft.OleDb.Serialization;

namespace Microsoft.OleDb
{
	// Token: 0x02001E6D RID: 7789
	internal class DateTimeColumn : Column<DateTime>
	{
		// Token: 0x0600BFDD RID: 49117 RVA: 0x0026B168 File Offset: 0x00269368
		public DateTimeColumn(int maxRowCount)
			: base(maxRowCount)
		{
		}

		// Token: 0x17002F08 RID: 12040
		// (get) Token: 0x0600BFDE RID: 49118 RVA: 0x00227072 File Offset: 0x00225272
		public override ColumnType Type
		{
			get
			{
				return ColumnType.DateTime;
			}
		}

		// Token: 0x0600BFDF RID: 49119 RVA: 0x0026B171 File Offset: 0x00269371
		public override void AddDateTime(DateTime value)
		{
			base.AddValue(value);
		}

		// Token: 0x0600BFE0 RID: 49120 RVA: 0x0026B17A File Offset: 0x0026937A
		public unsafe override void AddValue(DBTYPE type, void* value, int length)
		{
			if (type == DBTYPE.DBTIMESTAMP)
			{
				base.AddValue(OleDbConvert.GetDateTime(*(DBTIMESTAMP*)value));
				return;
			}
			if (type == DBTYPE.DATE)
			{
				base.AddValue(OleDbConvert.DateTimeFromOADate(*(double*)value));
			}
		}

		// Token: 0x0600BFE1 RID: 49121 RVA: 0x0026B1A7 File Offset: 0x002693A7
		public override DateTime GetDateTime(int row)
		{
			return base.GetValue(row);
		}

		// Token: 0x0600BFE2 RID: 49122 RVA: 0x0026B1B0 File Offset: 0x002693B0
		public unsafe override DBSTATUS GetValue(int row, IManagedDataConvert dataConvert, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			DateTime value = base.GetValue(row);
			if (binding.DestType == DBTYPE.DATE)
			{
				destLength = DbLength.Double;
				double? num = OleDbConvert.SafeToOADate(value);
				if (num != null)
				{
					*(double*)destValue = num.Value;
					return DBSTATUS.S_OK;
				}
				*(double*)destValue = 0.0;
				return DBSTATUS.S_ISNULL;
			}
			else
			{
				if (binding.DestType == DBTYPE.VARIANT)
				{
					double? num2 = OleDbConvert.SafeToOADate(value);
					Variant.Init((VARIANT*)destValue);
					if (num2 != null)
					{
						double value2 = num2.Value;
						((VARIANT*)destValue)->vt = VARTYPE.DATE;
						((VARIANT*)destValue)->value64 = (ulong)(*(long*)(&value2));
					}
					else
					{
						((VARIANT*)destValue)->vt = VARTYPE.EMPTY;
					}
					destLength = DbLength.Variant;
					return DBSTATUS.S_OK;
				}
				return dataConvert.DataConvert(value, binding, destValue, out destLength);
			}
		}

		// Token: 0x0600BFE3 RID: 49123 RVA: 0x0026B264 File Offset: 0x00269464
		protected override void Serialize(PageWriter writer, DateTime[] values, int offset, int count)
		{
			writer.WriteArray(values, offset, count);
		}

		// Token: 0x0600BFE4 RID: 49124 RVA: 0x0026B270 File Offset: 0x00269470
		protected override void Deserialize(PageReader reader, DateTime[] values, int offset, int count)
		{
			reader.ReadArray(values, offset, count);
		}
	}
}
