using System;

namespace Microsoft.OleDb
{
	// Token: 0x02001E64 RID: 7780
	internal class Int16Column : Bit16Column
	{
		// Token: 0x0600BF92 RID: 49042 RVA: 0x0026AB3F File Offset: 0x00268D3F
		public Int16Column(int maxRowCount)
			: base(DBTYPE.I2, maxRowCount)
		{
		}

		// Token: 0x17002EFF RID: 12031
		// (get) Token: 0x0600BF93 RID: 49043 RVA: 0x000023C4 File Offset: 0x000005C4
		public override ColumnType Type
		{
			get
			{
				return ColumnType.Int16;
			}
		}

		// Token: 0x0600BF94 RID: 49044 RVA: 0x0026AB49 File Offset: 0x00268D49
		public void AddValue(short value)
		{
			base.Add16(value);
		}

		// Token: 0x0600BF95 RID: 49045 RVA: 0x0026AB52 File Offset: 0x00268D52
		public override void AddValue(object value)
		{
			this.AddValue((short)value);
		}

		// Token: 0x0600BF96 RID: 49046 RVA: 0x0026AB60 File Offset: 0x00268D60
		public override void AddInt16(short value)
		{
			this.AddValue(value);
		}

		// Token: 0x0600BF97 RID: 49047 RVA: 0x0026AB69 File Offset: 0x00268D69
		public override bool TryAddValue(object value)
		{
			if (value is short)
			{
				this.AddValue((short)value);
				return true;
			}
			return false;
		}

		// Token: 0x0600BF98 RID: 49048 RVA: 0x0026AB82 File Offset: 0x00268D82
		public override short GetInt16(int row)
		{
			return (short)base.Get16(row);
		}

		// Token: 0x0600BF99 RID: 49049 RVA: 0x0026AB8C File Offset: 0x00268D8C
		public override object GetObject(int row)
		{
			return this.GetInt16(row);
		}

		// Token: 0x0600BF9A RID: 49050 RVA: 0x0026AB9C File Offset: 0x00268D9C
		public unsafe override DBSTATUS GetValue(int row, IManagedDataConvert dataConvert, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			short num = (short)base.Get16(row);
			if (binding.DestType == DBTYPE.I2)
			{
				destLength = DbLength.Two;
				*(short*)destValue = num;
				return DBSTATUS.S_OK;
			}
			if (binding.DestType == DBTYPE.I8)
			{
				destLength = DbLength.Eight;
				*(long*)destValue = (long)num;
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
