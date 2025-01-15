using System;

namespace Microsoft.OleDb
{
	// Token: 0x02001E67 RID: 7783
	internal class UInt32Column : Bit32Column
	{
		// Token: 0x0600BFAC RID: 49068 RVA: 0x0026ADA9 File Offset: 0x00268FA9
		public UInt32Column(int maxRowCount)
			: base(DBTYPE.UI4, maxRowCount)
		{
		}

		// Token: 0x17002F02 RID: 12034
		// (get) Token: 0x0600BFAD RID: 49069 RVA: 0x00002475 File Offset: 0x00000675
		public override ColumnType Type
		{
			get
			{
				return ColumnType.UInt32;
			}
		}

		// Token: 0x0600BFAE RID: 49070 RVA: 0x0026A681 File Offset: 0x00268881
		public void AddValue(uint value)
		{
			base.Add32(value);
		}

		// Token: 0x0600BFAF RID: 49071 RVA: 0x0026ADB4 File Offset: 0x00268FB4
		public override void AddValue(object value)
		{
			this.AddValue((uint)value);
		}

		// Token: 0x0600BFB0 RID: 49072 RVA: 0x0026ADC2 File Offset: 0x00268FC2
		public override void AddUInt32(uint value)
		{
			this.AddValue(value);
		}

		// Token: 0x0600BFB1 RID: 49073 RVA: 0x0026ADCB File Offset: 0x00268FCB
		public override bool TryAddValue(object value)
		{
			if (value is uint)
			{
				this.AddValue((uint)value);
				return true;
			}
			return false;
		}

		// Token: 0x0600BFB2 RID: 49074 RVA: 0x0026ADE4 File Offset: 0x00268FE4
		public override object GetObject(int row)
		{
			return base.Get32(row);
		}

		// Token: 0x0600BFB3 RID: 49075 RVA: 0x0026ADF4 File Offset: 0x00268FF4
		public unsafe override DBSTATUS GetValue(int row, IManagedDataConvert dataConvert, Binding binding, byte* destValue, out DBLENGTH destLength)
		{
			uint num = base.Get32(row);
			if (binding.DestType == DBTYPE.UI4)
			{
				destLength = DbLength.Four;
				*(int*)destValue = (int)num;
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
				*(double*)destValue = num;
				return DBSTATUS.S_OK;
			}
			return dataConvert.DataConvert(num, binding, destValue, out destLength);
		}
	}
}
