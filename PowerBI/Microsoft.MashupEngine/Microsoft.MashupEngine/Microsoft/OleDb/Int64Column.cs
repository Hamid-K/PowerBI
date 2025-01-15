using System;

namespace Microsoft.OleDb
{
	// Token: 0x02001E68 RID: 7784
	internal class Int64Column : Bit64Column
	{
		// Token: 0x0600BFB4 RID: 49076 RVA: 0x0026AE6B File Offset: 0x0026906B
		public Int64Column(int maxRowCount)
			: base(DBTYPE.I8, maxRowCount)
		{
		}

		// Token: 0x17002F03 RID: 12035
		// (get) Token: 0x0600BFB5 RID: 49077 RVA: 0x0000244F File Offset: 0x0000064F
		public override ColumnType Type
		{
			get
			{
				return ColumnType.Int64;
			}
		}

		// Token: 0x0600BFB6 RID: 49078 RVA: 0x0026AE76 File Offset: 0x00269076
		public void AddValue(long value)
		{
			base.Add64(value);
		}

		// Token: 0x0600BFB7 RID: 49079 RVA: 0x0026AE7F File Offset: 0x0026907F
		public override void AddValue(object value)
		{
			this.AddValue((long)value);
		}

		// Token: 0x0600BFB8 RID: 49080 RVA: 0x0026AE8D File Offset: 0x0026908D
		public override void AddInt64(long value)
		{
			this.AddValue(value);
		}

		// Token: 0x0600BFB9 RID: 49081 RVA: 0x0026AE96 File Offset: 0x00269096
		public override bool TryAddValue(object value)
		{
			if (value is long)
			{
				this.AddValue((long)value);
				return true;
			}
			return false;
		}

		// Token: 0x0600BFBA RID: 49082 RVA: 0x0026AEAF File Offset: 0x002690AF
		public override long GetInt64(int row)
		{
			return (long)base.Get64(row);
		}

		// Token: 0x0600BFBB RID: 49083 RVA: 0x0026AEB8 File Offset: 0x002690B8
		public override object GetObject(int row)
		{
			return this.GetInt64(row);
		}

		// Token: 0x0600BFBC RID: 49084 RVA: 0x0026AEC8 File Offset: 0x002690C8
		public unsafe override DBSTATUS GetValue(int row, IManagedDataConvert dataConvert, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			long num = (long)base.Get64(row);
			if (binding.DestType == DBTYPE.I8)
			{
				destLength = DbLength.Eight;
				*(long*)destValue = num;
				return DBSTATUS.S_OK;
			}
			return dataConvert.DataConvert(num, binding, destValue, out destLength);
		}
	}
}
