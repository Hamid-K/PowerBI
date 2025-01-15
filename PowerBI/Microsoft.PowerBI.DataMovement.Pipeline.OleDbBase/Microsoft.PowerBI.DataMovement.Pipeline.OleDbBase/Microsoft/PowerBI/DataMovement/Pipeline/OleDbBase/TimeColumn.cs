using System;
using System.Runtime.CompilerServices;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000025 RID: 37
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class TimeColumn : Column<Time>
	{
		// Token: 0x06000117 RID: 279 RVA: 0x000041A1 File Offset: 0x000023A1
		internal TimeColumn(int maxRowCount)
			: base(maxRowCount)
		{
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000118 RID: 280 RVA: 0x000041AA File Offset: 0x000023AA
		public override ColumnType Type
		{
			get
			{
				return ColumnType.Time;
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000041AE File Offset: 0x000023AE
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public unsafe override void AddValue(DBTYPE type, void* value, int length)
		{
			if (type == DBTYPE.DBTIME2)
			{
				base.AddValue(OleDbConvert.GetTime(*(DBTIME2*)value));
				return;
			}
			base.AddValue(OleDbConvert.GetTime(*(DBTIME*)value));
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000041DC File Offset: 0x000023DC
		public unsafe override DBSTATUS GetValue(int row, IDataConvert dataConvert, Binding binding, [global::System.Runtime.CompilerServices.Nullable(0)] byte* destValue, out DBLENGTH destLength)
		{
			DBTIME2 time = OleDbConvert.GetTime(base.GetValue(row));
			DBLENGTH dbTime = DbLength.DbTime2;
			DBSTATUS dbstatus;
			dataConvert.DataConvert(DBTYPE.DBTIME2, binding.DestType, dbTime, out destLength, (void*)(&time), (void*)destValue, binding.DestMaxLength, DBSTATUS.S_OK, out dbstatus, binding.Precision, binding.Scale, DBDATACONVERT.DEFAULT);
			return dbstatus;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000422C File Offset: 0x0000242C
		protected override void Serialize(PageWriter writer, Time[] values, int offset, int count)
		{
			writer.WriteArray(values, offset, count);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00004238 File Offset: 0x00002438
		protected override void Deserialize(PageReader reader, Time[] values, int offset, int count)
		{
			reader.ReadArray(values, offset, count);
		}
	}
}
