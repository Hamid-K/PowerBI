using System;
using System.Runtime.CompilerServices;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000027 RID: 39
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class TimeSpanColumn : Column<TimeSpan>
	{
		// Token: 0x06000123 RID: 291 RVA: 0x000042CC File Offset: 0x000024CC
		internal TimeSpanColumn(int maxRowCount)
			: base(maxRowCount)
		{
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000124 RID: 292 RVA: 0x000042D5 File Offset: 0x000024D5
		public override ColumnType Type
		{
			get
			{
				return ColumnType.TimeSpan;
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000042DC File Offset: 0x000024DC
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public unsafe override void AddValue(DBTYPE type, void* value, int length)
		{
			long ticks = ((DBDURATION*)value)->Ticks;
			base.AddValue(new TimeSpan(ticks));
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000042FC File Offset: 0x000024FC
		public unsafe override DBSTATUS GetValue(int row, IDataConvert dataConvert, Binding binding, [global::System.Runtime.CompilerServices.Nullable(0)] byte* destValue, out DBLENGTH destLength)
		{
			DBDURATION dbduration;
			dbduration.Ticks = base.GetValue(row).Ticks;
			DBLENGTH duration = DbLength.Duration;
			DBSTATUS dbstatus;
			dataConvert.DataConvert(DBTYPE.DBDURATION, binding.DestType, duration, out destLength, (void*)(&dbduration), (void*)destValue, binding.DestMaxLength, DBSTATUS.S_OK, out dbstatus, binding.Precision, binding.Scale, DBDATACONVERT.DEFAULT);
			return dbstatus;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00004355 File Offset: 0x00002555
		protected override void Serialize(PageWriter writer, TimeSpan[] values, int offset, int count)
		{
			writer.WriteArray(values, offset, count);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00004361 File Offset: 0x00002561
		protected override void Deserialize(PageReader reader, TimeSpan[] values, int offset, int count)
		{
			reader.ReadArray(values, offset, count);
		}
	}
}
