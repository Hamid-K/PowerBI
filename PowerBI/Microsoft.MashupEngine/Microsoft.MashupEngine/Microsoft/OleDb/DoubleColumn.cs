using System;

namespace Microsoft.OleDb
{
	// Token: 0x02001E6B RID: 7787
	internal class DoubleColumn : Bit64Column
	{
		// Token: 0x0600BFCE RID: 49102 RVA: 0x0026B039 File Offset: 0x00269239
		public DoubleColumn(int maxRowCount)
			: base(DBTYPE.R8, maxRowCount)
		{
		}

		// Token: 0x17002F06 RID: 12038
		// (get) Token: 0x0600BFCF RID: 49103 RVA: 0x0014025A File Offset: 0x0013E45A
		public override ColumnType Type
		{
			get
			{
				return ColumnType.Double;
			}
		}

		// Token: 0x0600BFD0 RID: 49104 RVA: 0x0026B043 File Offset: 0x00269243
		public void AddValue(double value)
		{
			base.Add64(value);
		}

		// Token: 0x0600BFD1 RID: 49105 RVA: 0x0026B04C File Offset: 0x0026924C
		public override void AddValue(object value)
		{
			this.AddValue((double)value);
		}

		// Token: 0x0600BFD2 RID: 49106 RVA: 0x0026B05A File Offset: 0x0026925A
		public override void AddDouble(double value)
		{
			this.AddValue(value);
		}

		// Token: 0x0600BFD3 RID: 49107 RVA: 0x0026B063 File Offset: 0x00269263
		public override bool TryAddValue(object value)
		{
			if (value is double)
			{
				this.AddValue((double)value);
				return true;
			}
			return false;
		}

		// Token: 0x0600BFD4 RID: 49108 RVA: 0x0026B07C File Offset: 0x0026927C
		public unsafe override double GetDouble(int row)
		{
			ulong num = base.Get64(row);
			return *(double*)(&num);
		}

		// Token: 0x0600BFD5 RID: 49109 RVA: 0x0026B095 File Offset: 0x00269295
		public override object GetObject(int row)
		{
			return this.GetDouble(row);
		}

		// Token: 0x0600BFD6 RID: 49110 RVA: 0x0026B0A4 File Offset: 0x002692A4
		public unsafe override DBSTATUS GetValue(int row, IManagedDataConvert dataConvert, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			ulong num = base.Get64(row);
			double num2 = *(double*)(&num);
			if (binding.DestType == DBTYPE.R8)
			{
				destLength = DbLength.Eight;
				*(double*)destValue = num2;
				return DBSTATUS.S_OK;
			}
			return dataConvert.DataConvert(num2, binding, destValue, out destLength);
		}
	}
}
