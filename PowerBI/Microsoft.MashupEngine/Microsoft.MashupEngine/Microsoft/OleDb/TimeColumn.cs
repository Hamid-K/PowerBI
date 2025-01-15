using System;
using Microsoft.OleDb.Serialization;

namespace Microsoft.OleDb
{
	// Token: 0x02001E6F RID: 7791
	internal class TimeColumn : Column<Time>
	{
		// Token: 0x0600BFEC RID: 49132 RVA: 0x0026B31C File Offset: 0x0026951C
		public TimeColumn(int maxRowCount)
			: base(maxRowCount)
		{
		}

		// Token: 0x17002F0A RID: 12042
		// (get) Token: 0x0600BFED RID: 49133 RVA: 0x0006808E File Offset: 0x0006628E
		public override ColumnType Type
		{
			get
			{
				return ColumnType.Time;
			}
		}

		// Token: 0x0600BFEE RID: 49134 RVA: 0x0026B325 File Offset: 0x00269525
		public override void AddTime(Time value)
		{
			base.AddValue(value);
		}

		// Token: 0x0600BFEF RID: 49135 RVA: 0x0026B32E File Offset: 0x0026952E
		public unsafe override void AddValue(DBTYPE type, void* value, int length)
		{
			if (type == DBTYPE.DBTIME2)
			{
				base.AddValue(OleDbConvert.GetTime(*(DBTIME2*)value));
				return;
			}
			base.AddValue(OleDbConvert.GetTime(*(DBTIME*)value));
		}

		// Token: 0x0600BFF0 RID: 49136 RVA: 0x0026B35C File Offset: 0x0026955C
		public unsafe override DBSTATUS GetValue(int row, IManagedDataConvert dataConvert, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			Time value = base.GetValue(row);
			return dataConvert.DataConvert(value, binding, destValue, out destLength);
		}

		// Token: 0x0600BFF1 RID: 49137 RVA: 0x0026B37D File Offset: 0x0026957D
		protected override void Serialize(PageWriter writer, Time[] values, int offset, int count)
		{
			writer.WriteArray(values, offset, count);
		}

		// Token: 0x0600BFF2 RID: 49138 RVA: 0x0026B389 File Offset: 0x00269589
		protected override void Deserialize(PageReader reader, Time[] values, int offset, int count)
		{
			reader.ReadArray(values, offset, count);
		}
	}
}
