using System;
using System.Runtime.CompilerServices;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000024 RID: 36
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class DateColumn : Column<Date>
	{
		// Token: 0x06000111 RID: 273 RVA: 0x000040FA File Offset: 0x000022FA
		internal DateColumn(int maxRowCount)
			: base(maxRowCount)
		{
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00004103 File Offset: 0x00002303
		public override ColumnType Type
		{
			get
			{
				return ColumnType.Date;
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00004107 File Offset: 0x00002307
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public unsafe override void AddValue(DBTYPE type, void* value, int length)
		{
			base.AddValue(OleDbConvert.GetDate(*(DBDATE*)value));
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000411C File Offset: 0x0000231C
		public unsafe override DBSTATUS GetValue(int row, IDataConvert dataConvert, Binding binding, [global::System.Runtime.CompilerServices.Nullable(0)] byte* destValue, out DBLENGTH destLength)
		{
			Date value = base.GetValue(row);
			DBDATE date = OleDbConvert.GetDate(value);
			DBLENGTH dbDate = DbLength.DbDate;
			if (binding.DestType == DBTYPE.R8)
			{
				return base.ConvertDateTimeToDouble(value.DateTime, destValue, out destLength);
			}
			DBSTATUS dbstatus;
			dataConvert.DataConvert(DBTYPE.DBDATE, binding.DestType, dbDate, out destLength, (void*)(&date), (void*)destValue, binding.DestMaxLength, DBSTATUS.S_OK, out dbstatus, binding.Precision, binding.Scale, DBDATACONVERT.DEFAULT);
			return dbstatus;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00004189 File Offset: 0x00002389
		protected override void Serialize(PageWriter writer, Date[] values, int offset, int count)
		{
			writer.WriteArray(values, offset, count);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00004195 File Offset: 0x00002395
		protected override void Deserialize(PageReader reader, Date[] values, int offset, int count)
		{
			reader.ReadArray(values, offset, count);
		}
	}
}
