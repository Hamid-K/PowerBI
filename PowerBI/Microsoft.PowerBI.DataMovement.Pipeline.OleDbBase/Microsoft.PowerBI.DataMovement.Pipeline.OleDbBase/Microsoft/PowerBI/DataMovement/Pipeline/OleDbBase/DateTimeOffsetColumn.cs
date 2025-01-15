using System;
using System.Runtime.CompilerServices;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000026 RID: 38
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class DateTimeOffsetColumn : Column<DateTimeOffset>
	{
		// Token: 0x0600011D RID: 285 RVA: 0x00004244 File Offset: 0x00002444
		internal DateTimeOffsetColumn(int maxRowCount)
			: base(maxRowCount)
		{
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600011E RID: 286 RVA: 0x0000424D File Offset: 0x0000244D
		public override ColumnType Type
		{
			get
			{
				return ColumnType.DateTimeOffset;
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00004251 File Offset: 0x00002451
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public unsafe override void AddValue(DBTYPE type, void* value, int length)
		{
			base.AddValue(OleDbConvert.GetDateTimeOffset(*(DBTIMESTAMPOFFSET*)value));
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00004264 File Offset: 0x00002464
		public unsafe override DBSTATUS GetValue(int row, IDataConvert dataConvert, Binding binding, [global::System.Runtime.CompilerServices.Nullable(0)] byte* destValue, out DBLENGTH destLength)
		{
			DBTIMESTAMPOFFSET timeStampOffset = OleDbConvert.GetTimeStampOffset(base.GetValue(row));
			DBLENGTH timeStampOffset2 = DbLength.TimeStampOffset;
			DBSTATUS dbstatus;
			dataConvert.DataConvert(DBTYPE.DBTIMESTAMPOFFSET, binding.DestType, timeStampOffset2, out destLength, (void*)(&timeStampOffset), (void*)destValue, binding.DestMaxLength, DBSTATUS.S_OK, out dbstatus, binding.Precision, binding.Scale, DBDATACONVERT.DEFAULT);
			return dbstatus;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000042B4 File Offset: 0x000024B4
		protected override void Serialize(PageWriter writer, DateTimeOffset[] values, int offset, int count)
		{
			writer.WriteArray(values, offset, count);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000042C0 File Offset: 0x000024C0
		protected override void Deserialize(PageReader reader, DateTimeOffset[] values, int offset, int count)
		{
			reader.ReadArray(values, offset, count);
		}
	}
}
