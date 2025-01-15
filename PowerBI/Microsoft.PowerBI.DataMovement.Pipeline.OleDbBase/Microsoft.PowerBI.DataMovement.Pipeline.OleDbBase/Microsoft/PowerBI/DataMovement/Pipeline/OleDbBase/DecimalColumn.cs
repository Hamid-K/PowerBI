using System;
using System.Runtime.CompilerServices;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000029 RID: 41
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class DecimalColumn : Column<decimal>
	{
		// Token: 0x06000130 RID: 304 RVA: 0x000043F4 File Offset: 0x000025F4
		internal DecimalColumn(int maxRowCount)
			: base(maxRowCount)
		{
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000131 RID: 305 RVA: 0x000043FD File Offset: 0x000025FD
		public override ColumnType Type
		{
			get
			{
				return ColumnType.Decimal;
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00004401 File Offset: 0x00002601
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public unsafe override void AddValue(DBTYPE type, void* value, int length)
		{
			base.AddValue(*(decimal*)value);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00004410 File Offset: 0x00002610
		public unsafe override DBSTATUS GetValue(int row, IDataConvert dataConvert, Binding binding, [global::System.Runtime.CompilerServices.Nullable(0)] byte* destValue, out DBLENGTH destLength)
		{
			decimal value = base.GetValue(row);
			DBLENGTH @decimal = DbLength.Decimal;
			if (binding.DestType == DBTYPE.R8)
			{
				*(double*)destValue = (double)value;
				destLength = DbLength.Double;
				return DBSTATUS.S_OK;
			}
			DBSTATUS dbstatus;
			dataConvert.DataConvert(DBTYPE.DECIMAL, binding.DestType, @decimal, out destLength, (void*)(&value), (void*)destValue, binding.DestMaxLength, DBSTATUS.S_OK, out dbstatus, binding.Precision, binding.Scale, DBDATACONVERT.DEFAULT);
			return dbstatus;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00004479 File Offset: 0x00002679
		public override decimal GetDecimal(int row)
		{
			return base.GetValue(row);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00004482 File Offset: 0x00002682
		protected override void Serialize(PageWriter writer, decimal[] values, int offset, int count)
		{
			writer.WriteArray(values, offset, count);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000448E File Offset: 0x0000268E
		protected override void Deserialize(PageReader reader, decimal[] values, int offset, int count)
		{
			reader.ReadArray(values, offset, count);
		}
	}
}
