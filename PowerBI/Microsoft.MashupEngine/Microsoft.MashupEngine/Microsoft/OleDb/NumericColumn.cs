using System;
using Microsoft.OleDb.Serialization;

namespace Microsoft.OleDb
{
	// Token: 0x02001E75 RID: 7797
	internal class NumericColumn : Column<Number>
	{
		// Token: 0x0600C020 RID: 49184 RVA: 0x0026B72C File Offset: 0x0026992C
		public NumericColumn(int maxRowCount)
			: base(maxRowCount)
		{
		}

		// Token: 0x17002F11 RID: 12049
		// (get) Token: 0x0600C021 RID: 49185 RVA: 0x00140DB6 File Offset: 0x0013EFB6
		public override ColumnType Type
		{
			get
			{
				return ColumnType.Numeric;
			}
		}

		// Token: 0x0600C022 RID: 49186 RVA: 0x0026B735 File Offset: 0x00269935
		public override void AddNumber(Number number)
		{
			base.AddValue(number);
		}

		// Token: 0x0600C023 RID: 49187 RVA: 0x0026B73E File Offset: 0x0026993E
		public unsafe override void AddValue(DBTYPE type, void* value, int length)
		{
			base.AddValue(new Number(*(NUMERIC*)value));
		}

		// Token: 0x0600C024 RID: 49188 RVA: 0x0026B754 File Offset: 0x00269954
		public unsafe override DBSTATUS GetValue(int row, IManagedDataConvert dataConvert, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			Number value = base.GetValue(row);
			if (binding.DestType == DBTYPE.R8)
			{
				*(double*)destValue = value.ToDouble();
				destLength = DbLength.Double;
				return DBSTATUS.S_OK;
			}
			switch (value.Kind)
			{
			case NumberKind.Decimal:
			{
				decimal num = value.ToDecimal();
				return dataConvert.DataConvert(num, binding, destValue, out destLength);
			}
			case NumberKind.Double:
			{
				double num2 = value.ToDouble();
				return dataConvert.DataConvert(num2, binding, destValue, out destLength);
			}
			case NumberKind.Numeric:
			{
				NUMERIC numeric = value.ToNumeric();
				return dataConvert.DataConvert(numeric, binding, destValue, out destLength);
			}
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0600C025 RID: 49189 RVA: 0x0026B7EC File Offset: 0x002699EC
		protected override void Serialize(PageWriter writer, Number[] values, int offset, int count)
		{
			writer.WriteArray(values, offset, count);
		}

		// Token: 0x0600C026 RID: 49190 RVA: 0x0026B7F8 File Offset: 0x002699F8
		protected override void Deserialize(PageReader reader, Number[] values, int offset, int count)
		{
			reader.ReadArray(values, offset, count);
		}
	}
}
