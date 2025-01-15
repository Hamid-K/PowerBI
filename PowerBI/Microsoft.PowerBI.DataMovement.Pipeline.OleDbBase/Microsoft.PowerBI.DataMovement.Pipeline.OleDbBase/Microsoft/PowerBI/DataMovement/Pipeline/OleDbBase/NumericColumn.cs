using System;
using System.Runtime.CompilerServices;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x0200002B RID: 43
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class NumericColumn : Column<Number>
	{
		// Token: 0x06000145 RID: 325 RVA: 0x0000474F File Offset: 0x0000294F
		internal NumericColumn(int maxRowCount)
			: base(maxRowCount)
		{
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00004758 File Offset: 0x00002958
		public override ColumnType Type
		{
			get
			{
				return ColumnType.Numeric;
			}
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000475C File Offset: 0x0000295C
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public unsafe override void AddValue(DBTYPE type, void* value, int length)
		{
			base.AddValue(new Number(*(NUMERIC*)value));
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00004770 File Offset: 0x00002970
		public unsafe override DBSTATUS GetValue(int row, IDataConvert dataConvert, Binding binding, [global::System.Runtime.CompilerServices.Nullable(0)] byte* destValue, out DBLENGTH destLength)
		{
			Number value = base.GetValue(row);
			DBSTATUS dbstatus;
			switch (value.Kind)
			{
			case NumberKind.Decimal:
			{
				decimal num = value.ToDecimal();
				dataConvert.DataConvert(DBTYPE.DECIMAL, binding.DestType, DbLength.Decimal, out destLength, (void*)(&num), (void*)destValue, binding.DestMaxLength, DBSTATUS.S_OK, out dbstatus, binding.Precision, binding.Scale, DBDATACONVERT.DEFAULT);
				break;
			}
			case NumberKind.Double:
			{
				double num2 = value.ToDouble();
				dataConvert.DataConvert(DBTYPE.R8, binding.DestType, DbLength.Eight, out destLength, (void*)(&num2), (void*)destValue, binding.DestMaxLength, DBSTATUS.S_OK, out dbstatus, binding.Precision, binding.Scale, DBDATACONVERT.DEFAULT);
				break;
			}
			case NumberKind.Numeric:
			{
				NUMERIC numeric = value.ToNumeric();
				dataConvert.DataConvert(DBTYPE.NUMERIC, binding.DestType, DbLength.Numeric, out destLength, (void*)(&numeric), (void*)destValue, binding.DestMaxLength, DBSTATUS.S_OK, out dbstatus, binding.Precision, binding.Scale, DBDATACONVERT.DEFAULT);
				break;
			}
			default:
				throw new InvalidOperationException();
			}
			return dbstatus;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000485E File Offset: 0x00002A5E
		protected override void Serialize(PageWriter writer, Number[] values, int offset, int count)
		{
			writer.WriteArray(values, offset, count);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000486A File Offset: 0x00002A6A
		protected override void Deserialize(PageReader reader, Number[] values, int offset, int count)
		{
			reader.ReadArray(values, offset, count);
		}
	}
}
