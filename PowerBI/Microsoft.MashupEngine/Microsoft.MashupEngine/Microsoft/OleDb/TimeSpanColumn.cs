using System;
using Microsoft.OleDb.Serialization;

namespace Microsoft.OleDb
{
	// Token: 0x02001E71 RID: 7793
	internal class TimeSpanColumn : Column<TimeSpan>
	{
		// Token: 0x0600BFFA RID: 49146 RVA: 0x0026B3F5 File Offset: 0x002695F5
		public TimeSpanColumn(int maxRowCount)
			: base(maxRowCount)
		{
		}

		// Token: 0x17002F0C RID: 12044
		// (get) Token: 0x0600BFFB RID: 49147 RVA: 0x0012AF0D File Offset: 0x0012910D
		public override ColumnType Type
		{
			get
			{
				return ColumnType.TimeSpan;
			}
		}

		// Token: 0x0600BFFC RID: 49148 RVA: 0x0026B3FE File Offset: 0x002695FE
		public override void AddTimeSpan(TimeSpan value)
		{
			base.AddValue(value);
		}

		// Token: 0x0600BFFD RID: 49149 RVA: 0x0026B408 File Offset: 0x00269608
		public unsafe override void AddValue(DBTYPE type, void* value, int length)
		{
			long ticks = ((DBDURATION*)value)->ticks;
			base.AddValue(new TimeSpan(ticks));
		}

		// Token: 0x0600BFFE RID: 49150 RVA: 0x0026B428 File Offset: 0x00269628
		public unsafe override DBSTATUS GetValue(int row, IManagedDataConvert dataConvert, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			TimeSpan value = base.GetValue(row);
			return dataConvert.DataConvert(value, binding, destValue, out destLength);
		}

		// Token: 0x0600BFFF RID: 49151 RVA: 0x0026B449 File Offset: 0x00269649
		protected override void Serialize(PageWriter writer, TimeSpan[] values, int offset, int count)
		{
			writer.WriteArray(values, offset, count);
		}

		// Token: 0x0600C000 RID: 49152 RVA: 0x0026B455 File Offset: 0x00269655
		protected override void Deserialize(PageReader reader, TimeSpan[] values, int offset, int count)
		{
			reader.ReadArray(values, offset, count);
		}
	}
}
