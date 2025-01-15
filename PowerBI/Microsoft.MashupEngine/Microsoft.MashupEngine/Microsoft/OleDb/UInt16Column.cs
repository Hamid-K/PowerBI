using System;

namespace Microsoft.OleDb
{
	// Token: 0x02001E65 RID: 7781
	internal class UInt16Column : Bit16Column
	{
		// Token: 0x0600BF9B RID: 49051 RVA: 0x0026AC12 File Offset: 0x00268E12
		public UInt16Column(int maxRowCount)
			: base(DBTYPE.UI2, maxRowCount)
		{
		}

		// Token: 0x17002F00 RID: 12032
		// (get) Token: 0x0600BF9C RID: 49052 RVA: 0x00002461 File Offset: 0x00000661
		public override ColumnType Type
		{
			get
			{
				return ColumnType.UInt16;
			}
		}

		// Token: 0x0600BF9D RID: 49053 RVA: 0x0026AC1D File Offset: 0x00268E1D
		public void AddValue(ushort value)
		{
			base.Add16(value);
		}

		// Token: 0x0600BF9E RID: 49054 RVA: 0x0026AC26 File Offset: 0x00268E26
		public override void AddValue(object value)
		{
			this.AddValue((ushort)value);
		}

		// Token: 0x0600BF9F RID: 49055 RVA: 0x0026AC34 File Offset: 0x00268E34
		public override void AddUInt16(ushort value)
		{
			this.AddValue(value);
		}

		// Token: 0x0600BFA0 RID: 49056 RVA: 0x0026AC3D File Offset: 0x00268E3D
		public override bool TryAddValue(object value)
		{
			if (value is ushort)
			{
				this.AddValue((ushort)value);
				return true;
			}
			return false;
		}

		// Token: 0x0600BFA1 RID: 49057 RVA: 0x0026AC56 File Offset: 0x00268E56
		public override object GetObject(int row)
		{
			return base.Get16(row);
		}

		// Token: 0x0600BFA2 RID: 49058 RVA: 0x0026AC64 File Offset: 0x00268E64
		public unsafe override DBSTATUS GetValue(int row, IManagedDataConvert dataConvert, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			ushort num = base.Get16(row);
			if (binding.DestType == DBTYPE.UI2)
			{
				destLength = DbLength.Two;
				*(short*)destValue = (short)num;
				return DBSTATUS.S_OK;
			}
			if (binding.DestType == DBTYPE.I8)
			{
				destLength = DbLength.Eight;
				*(long*)destValue = (long)((ulong)num);
				return DBSTATUS.S_OK;
			}
			if (binding.DestType == DBTYPE.R8)
			{
				destLength = DbLength.Eight;
				*(double*)destValue = (double)num;
				return DBSTATUS.S_OK;
			}
			return dataConvert.DataConvert(num, binding, destValue, out destLength);
		}
	}
}
